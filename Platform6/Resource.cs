using Nancy;

namespace Service {
	public class Resource : NancyModule {
		private const string MyServiceId = "demo.csharp";

		public Resource() : base("/" + MyServiceId + "/api/v0.0.1") {
			Get["/portal"] = _ => new Response{StatusCode = HttpStatusCode.OK};
		}
	}
}