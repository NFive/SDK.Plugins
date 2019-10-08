using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NFive.SDK.Plugins.Configuration;

namespace NFive.SDK.Plugins
{
	/// <summary>
	/// Represents a loaded NFive plugin configuration including nested dependencies.
	/// </summary>
	/// <seealso cref="Core.Plugins.Plugin" />
	[PublicAPI]
	public class Plugin : Core.Plugins.Plugin
	{
		/// <summary>
		/// Gets or sets the dependency plugins.
		/// </summary>
		/// <value>
		/// The dependency plugins.
		/// </value>
		public List<Plugin> DependencyNodes { get; set; }

		/// <summary>
		/// Loads a <see cref="Plugin" /> from the specified definition file.
		/// </summary>
		/// <param name="path">The path to the plugin definition file.</param>
		/// <returns>The loaded <see cref="Plugin" />.</returns>
		/// <exception cref="ArgumentNullException">path - A valid file path must be specified.</exception>
		/// <exception cref="FileNotFoundException">Unable to find the plugin definition file.</exception>
		public static Plugin Load(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "A valid file path must be specified.");
			if (!File.Exists(path)) throw new FileNotFoundException("Unable to find the plugin definition file.", path);

			return Yaml.Deserialize<Plugin>(File.ReadAllText(path));
		}

		/// <summary>
		/// Serialize this instance and saves the to the specified path.
		/// </summary>
		/// <param name="path">The path to save the file at.</param>
		public void Save(string path)
		{
			File.WriteAllText(path, Yaml.Serialize(this));
		}
	}
}
