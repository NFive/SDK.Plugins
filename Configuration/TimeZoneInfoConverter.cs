using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using TimeZoneConverter;

namespace NFive.SDK.Plugins.Configuration
{
	/// <inheritdoc />
	/// <summary>
	/// Yaml converter for <see cref="TimeZoneInfo" />.
	/// </summary>
	/// <seealso cref="IYamlTypeConverter" />
	public class TimeZoneInfoConverter : IYamlTypeConverter
	{
		/// <inheritdoc />
		/// <summary>
		/// Gets a value indicating whether the current converter supports converting the specified type.
		/// </summary>
		public bool Accepts(Type type) => type == typeof(TimeZoneInfo);

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a YAML parser.
		/// </summary>
		public object ReadYaml(IParser parser, Type type)
		{
			var value = ((Scalar)parser.Current).Value;
			parser.MoveNext();

			return TZConvert.GetTimeZoneInfo(value);
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes the specified object's state to a YAML emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(((TimeZoneInfo)value).Id));
		}
	}
}
