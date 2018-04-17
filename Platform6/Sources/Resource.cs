using System.Text;
using Nancy;

namespace Service.Sources {
	public class Resource : NancyModule {
		public Resource() : base(Constants.Path) {
			After.AddItemToEndOfPipeline(ctx => ctx.Response
				.WithHeader("Access-Control-Allow-Credentials", "true")
				.WithHeader("Access-Control-Allow-Origin", "*")
				.WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, HEAD")
				.WithHeader("Access-Control-Allow-Headers", "origin, content-type, accept, authorization, X-HTTP-Method-Override, Accept-Encoding, Content-Encoding"));

			Get["/portal"] = _ => {
				// Return the UI's script
				var jsonBytes = Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(@"../../Client/build/ServiceConfiguration.bundle.js"));

				return new Response {
					StatusCode = HttpStatusCode.OK,
					ContentType = "text/plain",
					Contents = c => c.Write(jsonBytes, 0, jsonBytes.Length)
				};
			};

			Options["/portal"] = _ => new Response {StatusCode = HttpStatusCode.OK};
		}
	}
}