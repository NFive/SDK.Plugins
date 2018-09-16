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
				.WithNamingConvention(new UnderscoredNamingConvention())
				.WithTypeConverter(new NameConverter())
				.WithTypeConverter(new VersionConverter())
				.WithTypeConverter(new VersionRangeConverter())
				.Build()
				.Serialize(obj);
		}

		/// <summary>
		/// Deserializes the specified Yaml.
		/// </summary>
		/// <typeparam name="T">Type to deserialize as.</typeparam>
		/// <param name="yml">The Yaml string.</param>
		/// <returns>The deserialized object.</returns>
		public static T Deserialize<T>(string yml)
		{
			return new DeserializerBuilder()
				.WithNamingConvention(new UnderscoredNamingConvention())
				//.IgnoreUnmatchedProperties()
				.WithTypeConverter(new NameConverter())
				.WithTypeConverter(new VersionConverter())
				.WithTypeConverter(new VersionRangeConverter())
				.Build()
				.Deserialize<T>(yml);
		}
	}
}
