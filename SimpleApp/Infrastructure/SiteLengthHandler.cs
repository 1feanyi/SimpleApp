using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SimpleApp.Infrastructure
{
    public class SiteLengthHandler : HttpTaskAsyncHandler
    {
        public override async Task ProcessRequestAsync(HttpContext context) // async method
        {
            string data = await new HttpClient().GetStringAsync("http://www.google.com"); // reaches out to the destination may be a site, file, db, api, etc
            context.Response.ContentType = "text/html";
            context.Response.Write(string.Format("<span>Length: {0}</span>", data.Length));
        }
    }
}