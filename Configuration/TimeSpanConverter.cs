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
		/// <c>true</c> if the specified <see cref="Type" /> can be converted; otherwise, <c>false</c>.
		/// </returns>
		public bool Accepts(Type type) => type == typeof(TimeSpan);

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a Yaml parser.
		/// </summary>
		/// <returns>Deserialized <see cref="TimeSpan" /> object.</returns>
		public object ReadYaml(IParser parser, Type type)
		{
			var value = ((Scalar)parser.Current).Value.Trim();
			parser.MoveNext();

			if (TimeSpan.TryParseExact(value, @"dd\.hh\:mm\:ss\.FFFFFFF", null, out var t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.hh\:mm\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.h\:mm\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.h\:m\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.h\:m\:s\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"hh\:mm\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"h\:mm\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"h\:m\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"h\:m\:s\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"m\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"m\:s\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"mm\:ss\.FFFFFFF", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"mm\:s\.FFFFFFF", null, out t)) return t;

			if (TimeSpan.TryParseExact(value, @"dd\.hh\:mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.hh\:mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.h\:mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.h\:m\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"d\.h\:m\:s", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"hh\:mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"h\:mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"h\:m\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"h\:m\:s", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"m\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"m\:s", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"mm\:ss", null, out t)) return t;
			if (TimeSpan.TryParseExact(value, @"mm\:s", null, out t)) return t;

			if (MatchSuffix(value, "d", out var d)) return TimeSpan.FromDays(d);
			if (MatchSuffix(value, "day", out d)) return TimeSpan.FromDays(d);
			if (MatchSuffix(value, "days", out d)) return TimeSpan.FromDays(d);
			if (MatchSuffix(value, "h", out d)) return TimeSpan.FromHours(d);
			if (MatchSuffix(value, "hour", out d)) return TimeSpan.FromHours(d);
			if (MatchSuffix(value, "hours", out d)) return TimeSpan.FromHours(d);
			if (MatchSuffix(value, "m", out d)) return TimeSpan.FromMinutes(d);
			if (MatchSuffix(value, "min", out d)) return TimeSpan.FromMinutes(d);
			if (MatchSuffix(value, "mins", out d)) return TimeSpan.FromMinutes(d);
			if (MatchSuffix(value, "minute", out d)) return TimeSpan.FromMinutes(d);
			if (MatchSuffix(value, "minutes", out d)) return TimeSpan.FromMinutes(d);
			if (MatchSuffix(value, "s", out d)) return TimeSpan.FromSeconds(d);
			if (MatchSuffix(value, "sec", out d)) return TimeSpan.FromSeconds(d);
			if (MatchSuffix(value, "secs", out d)) return TimeSpan.FromSeconds(d);
			if (MatchSuffix(value, "second", out d)) return TimeSpan.FromSeconds(d);
			if (MatchSuffix(value, "seconds", out d)) return TimeSpan.FromSeconds(d);
			if (MatchSuffix(value, "ms", out d)) return TimeSpan.FromMilliseconds(d);
			if (MatchSuffix(value, "millisecond", out d)) return TimeSpan.FromMilliseconds(d);
			if (MatchSuffix(value, "milliseconds", out d)) return TimeSpan.FromMilliseconds(d);

			return TimeSpan.FromSeconds(double.Parse(value, NumberStyles.Integer));
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes the specified object's state to a Yaml emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(((TimeSpan)value).ToString()));
		}

		private static bool MatchSuffix(string value, string suffix, out double result)
		{
			if (value.EndsWith(suffix) && double.TryParse(value.Substring(0, value.Length - suffix.Length).Trim(), out result)) return true;

			result = 0;
			return false;
		}
	}
}
