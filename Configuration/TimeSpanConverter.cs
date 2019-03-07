using System;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace NFive.SDK.Plugins.Configuration
{
	/// <inheritdoc />
	/// <summary>
	/// Yaml converter for <see cref="TimeSpan" /> type.
	/// </summary>
	/// <seealso cref="IYamlTypeConverter" />
	public class TimeSpanConverter : IYamlTypeConverter
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
			return type == typeof(TimeSpan);
		}

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a Yaml parser.
		/// </summary>
		/// <returns>Deserialized <see cref="TimeSpan"/> object.</returns>
		public object ReadYaml(IParser parser, Type type)
		{
			var value = ((Scalar)parser.Current).Value;
			parser.MoveNext();

			if (TimeSpan.TryParseExact(value, @"dd\.hh\:mm\:ss", null, out var t)) return t;
			if (TimeSpan.TryParseExact(value, @"hh\:mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"mm\:ss", null, out t)) return t;
			return TimeSpan.FromSeconds(double.Parse(value, NumberStyles.Integer));
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
			emitter.Emit(new Scalar(((TimeSpan)value).ToString()));
		}
	}
}
