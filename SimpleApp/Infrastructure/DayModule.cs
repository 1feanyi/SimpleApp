using System;
using System.Web;

namespace SimpleApp.Infrastructure
{
    public class DayModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            // app.BeginRequest += (src, args) => { app.Context.Items["DayModule_Time"] = DateTime.Now; };
            app.PostMapRequestHandler += (src, args) => 
                                            { if (app.Context.Handler is IRequiresDate)
                                              {
                                                app.Context.Items["DayModule_Time"] = DateTime.Now;
                                              }
                                            };
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}