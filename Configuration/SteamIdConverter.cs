using System;
using System.Text.RegularExpressions;
using NFive.SDK.Core.Configuration;
using NFive.SDK.Core.Plugins;
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
		private static readonly Regex Steam2Regex = new Regex("^STEAM_0:[0-1]:([0-9]{1,10})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex Steam32Regex = new Regex("^\\[?U:1:([0-9]{1,10})\\]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex Steam64Regex = new Regex("^7656119([0-9]{10})$", RegexOptions.Compiled);

		/// <inheritdoc />
		/// <summary>
		/// Gets a value indicating whether the current converter supports converting the specified type.
		/// </summary>
		public bool Accepts(Type type) => type == typeof(SteamId);

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a Yaml parser.
		/// </summary>
		public object ReadYaml(IParser parser, Type type)
		{
			var value = ((Scalar)parser.Current).Value.Trim();
			parser.MoveNext();

			long id;

			if (Steam64Regex.IsMatch(value))
			{
				id = long.Parse(value);
			}
			else if (Steam2Regex.IsMatch(value))
			{
				id = SteamId.FromSteamId2(value);
			}
			else if (Steam32Regex.IsMatch(value))
			{
				id = SteamId.FromSteamId32(value);
			}
			else
			{
				throw new YamlException("YML input not in valid SteamID 2, 32 or 64 format");
			}

			return new SteamId(id);
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes the specified object's state to a Yaml emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(value.ToString()));
		}
	}
}
