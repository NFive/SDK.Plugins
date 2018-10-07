using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using NFive.SDK.Core.Controllers;
using NFive.SDK.Plugins.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.TypeInspectors;

namespace NFive.SDK.Plugins.Configuration
{
	[PublicAPI]
	public static class ConfigurationManager
	{
		public const string DefinitionFile = "nfive.yml";
		public const string LockFile = "nfive.lock";
		public const string ResourceFile = "__resource.lua";
		public const string PluginPath = "plugins";
		public const string ConfigurationPath = "config";

		public static object Load(string path, Type type)
		{
			path = Path.Combine("config", path);

			if (!File.Exists(path)) throw new FileNotFoundException("Unable to find configuration file", path);

			var deserializer = new DeserializerBuilder()
				.WithNamingConvention(new UnderscoredNamingConvention())
				//.IgnoreUnmatchedProperties()
				.Build();

			return deserializer.Deserialize(File.ReadAllText(path), type);
		}

		public static object Load(Name name, string file, Type type)
		{
			return Load(Path.Combine(name.Vendor, name.Project, $"{file}.yml"), type);
		}

		public static T Load<T>(string name)
		{
			return (T)Load(name, typeof(T));
		}

		public static object InitializeConfig(Name pluginName, Type type)
		{
			var configurationObject = (ControllerConfiguration)Activator.CreateInstance(type);
			// Generate default configuration if necessary.
			if (!File.Exists(Path.Combine("config", pluginName.Vendor, pluginName.Project, $"{configurationObject.FileName}.yml")))
			{
				GenerateDefaultFile(pluginName, configurationObject.FileName, type);
			}
			// Load configuration
			return Load(pluginName, configurationObject.FileName, type);
		}

		public static void GenerateDefaultFile(Name pluginName, string fileName, Type type)
		{
			Directory.CreateDirectory(Path.Combine("config", pluginName.Vendor, pluginName.Project));

			var yml = new SerializerBuilder()
				.WithNamingConvention(new UnderscoredNamingConvention())
				.EmitDefaults()
				.WithTypeInspector(i => new PluginTypeInspector(i))
				.Build()
				.Serialize(Activator.CreateInstance(type));

			File.WriteAllText(Path.Combine("config", pluginName.Vendor, pluginName.Project, $"{fileName}.yml"), yml);
		}
	}

	public class PluginTypeInspector : TypeInspectorSkeleton
	{
		private readonly ITypeInspector inspector;

		public PluginTypeInspector(ITypeInspector inspector)
		{
			this.inspector = inspector;
		}

		public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container) => this.inspector
			.GetProperties(type, container)
			.Where(p => p.CanWrite)
			.Where(p => p.Name != "file_name");
	}
}
