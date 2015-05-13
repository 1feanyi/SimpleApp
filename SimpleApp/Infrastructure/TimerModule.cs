using System;
using System.Diagnostics;
using System.Web;

namespace SimpleApp.Infrastructure
{
    public class RequestTimerEventArgs : EventArgs
    {
        public float Duration { get; set; }
    }

    public class TimerModule : IHttpModule
    {
        public event EventHandler<RequestTimerEventArgs> RequestTimed;

        private Stopwatch timer;

        public void Init(HttpApplication app) //registers the HandleEvent method as a handler for the Begin/End events (event handler)
        {
            app.BeginRequest += HandleEvent;
            app.EndRequest += HandleEvent;
        }

        private void HandleEvent(object src, EventArgs args)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.CurrentNotification == RequestNotification.BeginRequest) // cue to start timer at application_start
            {
                timer = Stopwatch.StartNew();
            }
            else // triggered when the EndRequest event occurs
            {
                float duration = ((float)timer.ElapsedTicks) / Stopwatch.Frequency;
                ctx.Response.Write(string.Format("<div class = 'alert alert-success'>Elapsed: {0:F5} seconds</div>", ((float)timer.ElapsedTicks) / Stopwatch.Frequency));

                if (RequestTimed != null)
                {
                    RequestTimed(this, new RequestTimerEventArgs { Duration = duration });
                }
            }
        }

        public void Dispose()
        {
            // do nothing - no resources to release
        }
    }
}