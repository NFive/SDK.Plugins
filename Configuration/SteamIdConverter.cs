using NFive.SDK.Core.Plugins;
using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace NFive.SDK.Plugins.Configuration
{
	/// <inheritdoc />
	/// <summary>
	/// Yaml converter for <see cref="Name" /> type.
	/// </summary>
	/// <seealso cref="IYamlTypeConverter" />
	public class SteamIdConverter : IYamlTypeConverter
	{
		/// <inheritdoc />
		/// <summary>
		/// Gets a value indicating whether the current converter supports converting the specified type.
		/// </summary>
		public bool Accepts(Type type)
		{
			return type == typeof(SteamId);
		}

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a Yaml parser.
		/// </summary>
		public object ReadYaml(IParser parser, Type type)
		{
			var value = ((Scalar)parser.Current).Value;
			parser.MoveNext();
			return new SteamId(value).ToSteam64();
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes the specified object's state to a Yaml emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{

			emitter.Emit(new Scalar(new SteamId(value.ToString()).ToSteam64().ToString()));
		}
	}
}