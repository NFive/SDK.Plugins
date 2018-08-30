using System.Collections.Generic;
using JetBrains.Annotations;

namespace NFive.SDK.Plugins.Models
{
	[PublicAPI]
	public class Server
	{
		public List<string> Main { get; set; }

		public List<string> Include { get; set; }
	}
}
