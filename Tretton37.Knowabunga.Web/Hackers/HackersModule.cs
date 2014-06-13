using Nancy;
using Nancy.ModelBinding;
using Tretton37.Knowabunga.Core;

namespace Tretton37.Knowabunga.Web.Hackers
{
    public class HackersModule : NancyModule
    {
        public HackersModule(IHackerService hackerService)
            : base("/hackers")
        {
            Get["/"] = _ =>
            {
                var model = new
                {
                    Hackers = hackerService.GetHackers()
                };

                return View["Views/Overview.cshtml", model];
            };

            Get["/add"] = _ =>
            {
                return View["Views/Add.cshtml"];
            };

            Post["/add"] = _ =>
            {
                var hacker = this.Bind<Hacker>();
                hacker.Available = true;

                hackerService.Add(hacker);

                return Response.AsRedirect("~/hackers/");
            };

            Get["/Edit/{id}"] = parameters =>
            {
                var hacker = hackerService.GetHacker(parameters.id);
                return View["Views/Edit.cshtml", hacker];
            };

            Post["/edit"] = _ =>
            {
                var hacker = this.Bind<Hacker>();

                hackerService.Update(hacker);

                return Response.AsRedirect("~/hackers/");
            };

            Post["/updateavailablilty"] = parameters =>
            {
                int id = Request.Form["id"];
                hackerService.ToggleAvailability(id);
                return HttpStatusCode.NoContent;
            };
        }
    }
}