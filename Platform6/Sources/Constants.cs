namespace Service.Sources {
	public class Constants {
		public const string MyServiceId = "demo.csharp";
		public const string ServerVersion = "0.0.1";
		public const string BaseUrl = "/api/v" + ServerVersion;
		public const string Path = "/" + MyServiceId + BaseUrl;
	}
}