using System;
using System.Collections.Generic;
using Library.Models;
using Nancy.Hosting.Self;
using Topshelf;

namespace Service.Sources {
	internal class Application {
		private const string MyServiceId = "demo.csharp";
		private static Library.Service _service;

		public static void Main() {
			// Start the Nancy server
			HostFactory.Run(x =>
			{
				x.UseLinuxIfAvailable();
				x.Service<NancyHost>(s =>
				{
					s.ConstructUsing(name => new NancyHost(new Uri("http://localhost:8888")));
					s.WhenStarted(tc => {
						_service = DeployService();
						_service.Deployed.ContinueWith(Console.WriteLine);

						Console.WriteLine("The service " + MyServiceId + " has been deployed.");

						tc.Start();
					});
					s.WhenStopped(async tc => {
						Console.WriteLine("Closing the server...");

						await _service.UndeployService();
						Console.WriteLine("Server closed and service undeployed.");

						tc.Stop();
					});
				});

				x.RunAsLocalSystem();
				x.SetDescription("Nancy-SelfHost example");
				x.SetDisplayName("Nancy-SelfHost Service");
				x.SetServiceName("Nancy-SelfHost");
			});
		}

		private static Library.Service DeployService() {
			var parameters = new DeployParameters {
				Id = MyServiceId,
				Path = Constants.Path,
				BasePath = Environment.GetEnvironmentVariable("EXTERNAL_URL"),
				Versions = new Versions {
					Client = "0.0.0",
					Server = Constants.ServerVersion
				},
				Ui = new UserInterfaceProperties {
					visible = true,
					iconName = "fas fa-hashtag",
					weight = 30,
					label = new Dictionary<string, string> {
						{"en-US", "C Sharp"},
						{"fr-FR", "C Sharp"}
					}
				}
			};

			return new Library.Service(parameters);
		}
	}
}