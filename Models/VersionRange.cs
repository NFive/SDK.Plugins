using JetBrains.Annotations;
using SemVer;

namespace NFive.SDK.Plugins.Models
{
	[PublicAPI]
	public class VersionRange : Range
	{
		public VersionRange(string rangeSpec, bool loose = false) : base(rangeSpec, loose) { }

		public static implicit operator VersionRange(string value) => new VersionRange(value);
	}
}
