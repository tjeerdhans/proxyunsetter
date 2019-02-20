using System;
using System.Net;
using System.Text;
using System.Threading;

namespace ProxyUnsetter.Helpers
{

    // https://www.technical-recipes.com/2016/creating-a-web-server-in-c/
    internal class PacHttpServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;

        public PacHttpServer(int port)
        {
            _responderMethod = r => "FindProxyForURL(url, host) { return \"DIRECT\"; }";
            _listener.Prefixes.Add($"http://localhost:{port}/");
            _listener.Start();
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem(c =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                if (ctx == null)
                                {
                                    return;
                                }

                                var response = _responderMethod(ctx.Request);
                                var buf = Encoding.UTF8.GetBytes(response);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch
                            {
                                // ignored
                            }
                            finally
                            {
                                // always close the stream
                                ctx?.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch
                {
                    // ignored
                }
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
