using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using Version = NFive.SDK.Core.Plugins.Version;

namespace NFive.SDK.Plugins.Configuration
{
	/// <inheritdoc />
	/// <summary>
	/// Yaml converter for <see cref="Version" /> type.
	/// </summary>
	/// <seealso cref="IYamlTypeConverter" />
	public class VersionConverter : IYamlTypeConverter
	{
		/// <inheritdoc />
		/// <summary>
		/// Gets a value indicating whether the current converter supports converting the specified type.
		/// </summary>
		/// <param name="type">The type to check.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="Type" /> can be converted; otherwise, <c>false</c>.
		/// </returns>
		public bool Accepts(Type type)
		{
			return type == typeof(Version);
		}

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a Yaml parser.
		/// </summary>
		/// <returns>Deserialized <see cref="Version"/> object.</returns>
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
		/// Writes the specified object's state to a Yaml emitter.
		/// </summary>
		/// <param name="emitter"></param>
		/// <param name="value"></param>
		/// <param name="type"></param>
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(((Version)value).ToString()));
		}
	}
}
