using JetBrains.Annotations;
using NFive.SDK.Plugins.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace NFive.SDK.Plugins
{
	/// <summary>
	/// Represents a loaded tree of plugins and their resolved dependencies.
	/// </summary>
	[PublicAPI]
	public class DefinitionGraph
	{
		/// <summary>
		/// Gets or sets the tree of plugins.
		/// </summary>
		/// <value>
		/// The tree of plugins.
		/// </value>
		public List<Plugin> Plugins { get; set; }

		/// <summary>
		/// Deserialized the specified lock file into a <see cref="DefinitionGraph"/>.
		/// </summary>
		/// <param name="path">The path to the lock file.</param>
		/// <returns>The deserialized <see cref="DefinitionGraph"/>.</returns>
		/// <exception cref="ArgumentNullException">A valid file path must be specified.</exception>
		/// <exception cref="FileNotFoundException">Unable to find the plugin lock file.</exception>
		public static DefinitionGraph Load(string path = ConfigurationManager.LockFile)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "A valid file path must be specified.");
			if (!File.Exists(path)) throw new FileNotFoundException("Unable to find the plugin lock file.", path);

			return Yaml.Deserialize<DefinitionGraph>(File.ReadAllText(path));
		}

		/// <summary>
		/// Serialize this instance and saves the file to the specified path.
		/// </summary>
		/// <param name="path">The path to save the file at.</param>
		public void Save(string path = ConfigurationManager.LockFile)
		{
			File.WriteAllText(path, Yaml.Serialize(this));
		}
	}
}
