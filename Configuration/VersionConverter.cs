using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using Version = NFive.SDK.Core.Plugins.Version;

namespace NFive.SDK.Plugins.Configuration
{
	/// <inheritdoc />
	/// <summary>
	/// Yaml converter for <see cref="T:NFive.SDK.Core.Plugins.Version" />.
	/// </summary>
	/// <seealso cref="T:YamlDotNet.Serialization.IYamlTypeConverter" />
	public class VersionConverter : IYamlTypeConverter
	{
		/// <inheritdoc />
		/// <summary>
		/// Gets a value indicating whether the current converter supports converting the specified type.
		/// </summary>
		public bool Accepts(Type type)
		{
			return type == typeof(Version);
		}

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a YAML parser.
		/// </summary>
		public object ReadYaml(IParser parser, Type type)
		{
			var value = ((Scalar)parser.Current).Value;
			parser.MoveNext();

			var version = new SemVer.Version(value);

			return new Version
			{
				Major = version.Major,
				Minor = version.Minor,
				Patch = version.Patch,
				PreRelease = version.PreRelease,
				Build = version.Build
			};
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes the specified object's state to a YAML emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(((Version)value).ToString()));
		}
	}
}
