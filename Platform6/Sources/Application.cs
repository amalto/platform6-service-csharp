using System;
using System.Collections.Generic;
using Library.Models;
using Mono.Unix;
using Mono.Unix.Native;
using Nancy.Hosting.Self;

namespace Service.Sources {
	internal class Application {
		private const string MyServiceId = "demo.csharp";
		private static Library.Service _service;

		public static void Main() {
			// Start the Nancy server
			var host = new NancyHost(new Uri("http://localhost:8888"));
			host.Start();

			if (Type.GetType("Mono.Runtime") != null) {
				// Deploy my service
				_service = DeployService();
				_service.Deployed.ContinueWith(Console.WriteLine);

				UnixSignal.WaitAny(GetUnixTerminationSignals());
			}
			else Console.ReadLine();
		}

		private static UnixSignal[] GetUnixTerminationSignals() {
			return new[] {
				new UnixSignal(Signum.SIGINT),
				new UnixSignal(Signum.SIGTERM),
				new UnixSignal(Signum.SIGQUIT),
				new UnixSignal(Signum.SIGHUP)
			};
		}

		private static Library.Service DeployService() {
			var parameters = new DeployParameters {
				Id = MyServiceId,
				Username = "admin@amalto.com",
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