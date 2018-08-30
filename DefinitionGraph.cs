using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NFive.SDK.Plugins.Configuration;
using NFive.SDK.Plugins.Models;

namespace NFive.SDK.Plugins
{
	[PublicAPI]
	public class DefinitionGraph
	{
		public List<Definition> Definitions { get; set; }

		public static DefinitionGraph Load(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
			if (!File.Exists(path)) throw new FileNotFoundException("Unable to find the plugin lock file", path);

			return Yaml.Deserialize<DefinitionGraph>(File.ReadAllText(path));
		}

		public void Save(string path)
		{
			File.WriteAllText(path, Yaml.Serialize(this));
		}
	}
}
