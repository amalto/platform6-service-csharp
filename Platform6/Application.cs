using System;
using System.Collections.Generic;
using Library.Models;
using Mono.Unix;
using Mono.Unix.Native;
using Nancy.Hosting.Self;

namespace Service {
	internal class Application {
		private const string MyServiceId = "demo.csharp";

		public static void Main() {
			// Start the Nancy server
			StartServer();

//			// Deploy my service
//			var parameters = new DeployParameters {
//				Id = MyServiceId,
//				Username = "admin@amalto.com",
//				Path = "/" + MyServiceId + "/api/",
//				BasePath = "http://docker.for.mac.localhost:8000",
//				Versions = new Versions {
//					Client = "0.0.0",
//					Server = "0.0.1"
//				},
//				Ui = new UserInterfaceProperties {
//					visible = true,
//					iconName = "fas fa-hashtag",
//					weight = 30,
//					label = new Dictionary<string, string> {
//						{"en-US", "C Sharp"},
//						{"fr-FR", "C Sharp"}
//					}
//				}
//			};
//
//			var service = new Library.Service(parameters);
//			service.Deployed.ContinueWith(Console.WriteLine);
		}

		private static void StartServer() {
			var host = new NancyHost(new Uri("http://localhost:8888"));
			host.Start();

			if (Type.GetType("Mono.Runtime") != null) {
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
	}
}