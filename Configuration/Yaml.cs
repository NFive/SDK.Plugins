using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NFive.SDK.Plugins.Configuration
{
	/// <summary>
	/// Yaml serialization helpers.
	/// </summary>
	public static class Yaml
	{
		/// <summary>
		/// Serializes the specified object.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>The Yaml string.</returns>
		public static string Serialize(object obj)
		{
			return new SerializerBuilder()
				.WithNamingConvention(UnderscoredNamingConvention.Instance)
				.WithTypeInspector(i => new BasePluginTypeInspector(i))
				.WithTypeConverter(new NameConverter())
				.WithTypeConverter(new TimeSpanConverter())
				.WithTypeConverter(new VersionConverter())
				.WithTypeConverter(new VersionRangeConverter())
				.WithTypeConverter(new IPAddressConverter())
				.WithTypeConverter(new SteamIdConverter())
				.WithTypeConverter(new TimeZoneInfoConverter())
				.WithTypeConverter(new CultureInfoConverter())
				//.EmitDefaults()
				.Build()
				.Serialize(obj);
		}

		/// <summary>
		/// Deserializes the specified Yaml.
		/// </summary>
		/// <typeparam name="T">The type to deserialize as.</typeparam>
		/// <param name="yml">The Yaml string.</param>
		/// <returns>The deserialized object.</returns>
		public static T Deserialize<T>(string yml) => Deserializer().Deserialize<T>(yml);

		/// <summary>
		/// Deserializes the specified Yaml.
		/// </summary>
		/// <param name="yml">The Yaml string.</param>
		/// <param name="type">The type to deserialize as.</param>
		/// <returns>The deserialized object.</returns>
		public static object Deserialize(string yml, Type type) => Deserializer().Deserialize(yml, type);

		private static IDeserializer Deserializer()
		{
			return new DeserializerBuilder()
				.WithNamingConvention(UnderscoredNamingConvention.Instance)
				.WithTypeInspector(i => new BasePluginTypeInspector(i))
				//.IgnoreUnmatchedProperties()
				.WithTypeConverter(new NameConverter())
				.WithTypeConverter(new TimeSpanConverter())
				.WithTypeConverter(new VersionConverter())
				.WithTypeConverter(new VersionRangeConverter())
				.WithTypeConverter(new IPAddressConverter())
				.WithTypeConverter(new SteamIdConverter())
				.WithTypeConverter(new TimeZoneInfoConverter())
				.WithTypeConverter(new CultureInfoConverter())
				.Build();
		}
	}
}
