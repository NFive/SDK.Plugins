using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NFive.SDK.Plugins.Configuration;

namespace NFive.SDK.Plugins
{
	[PublicAPI]
	public class DefinitionGraph
	{
		public List<Plugin> Plugins { get; set; }

		public static DefinitionGraph Load(string path = ConfigurationManager.LockFile)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
			if (!File.Exists(path)) throw new FileNotFoundException("Unable to find the plugin lock file", path);

			return Yaml.Deserialize<DefinitionGraph>(File.ReadAllText(path));
		}

		public void Save(string path = ConfigurationManager.LockFile)
		{
			File.WriteAllText(path, Yaml.Serialize(this));
		}
	}
}
