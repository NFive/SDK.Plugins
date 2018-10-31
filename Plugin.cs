using JetBrains.Annotations;
using NFive.SDK.Plugins.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace NFive.SDK.Plugins
{
	[PublicAPI]
	public class Plugin : Core.Plugins.Plugin
	{
		public List<Plugin> DependencyNodes { get; set; }

		public static Plugin Load(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
			if (!File.Exists(path)) throw new FileNotFoundException("Unable to find the plugin definition file", path);

			return Yaml.Deserialize<Plugin>(File.ReadAllText(path));
		}

		public void Save(string path)
		{
			File.WriteAllText(path, Yaml.Serialize(this));
		}
	}
}
