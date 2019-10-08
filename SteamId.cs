using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NFive.SDK.Plugins
{
	public class SteamId
	{
		/// <summary>
		/// SteamID2 Regex
		/// </summary>
		private const string Steam2Regex = "^STEAM_0:[0-1]:([0-9]{1,10})$";

		/// <summary>
		/// SteamID32 Regex
		/// </summary>
		private const string Steam32Regex = "^U:1:([0-9]{1,10})$";

		/// <summary>
		/// SteamID64 Regex
		/// </summary>
		private const string Steam64Regex = "^7656119([0-9]{10})$";

		private readonly long id;

		public SteamId(string input)
		{
			if (Regex.IsMatch(input, Steam64Regex))
			{
				this.id = long.Parse(input);
			}
			else if (Regex.IsMatch(input, Steam2Regex))
			{
				this.id = Steam2ToSteam64(input);
			}
			else if (Regex.IsMatch(input, Steam32Regex))
			{
				this.id = Steam32ToSteam64(input);
			}
			else
			{
				throw new Exception();
			}
		}

		/// <summary>
		/// Converts Steam32 IDs to Steam64 IDs format.
		/// </summary>
		/// <param name="input">Steam32 ID</param>
		/// <returns>Returns the SteamID64(76561197960265728) in long type</returns>
		private static long Steam32ToSteam64(string input)
		{
			var steam32 = Convert.ToInt64(input.Substring(4));
			if (steam32 < 1L || !Regex.IsMatch("U:1:" + steam32.ToString(CultureInfo.InvariantCulture), "^U:1:([0-9]{1,10})$"))
			{
				return 0;
			}

			return steam32 + 76561197960265728L;
		}

		/// <summary>
		/// Converts Steam2 IDs to Steam64 IDs format.
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns>Returns the SteamID64(76561197960265728) in long type</returns>
		private static long Steam2ToSteam64(string accountId)
		{
			if (!Regex.IsMatch(accountId, "^STEAM_0:[0-1]:([0-9]{1,10})$"))
			{
				return 0;
			}

			return 76561197960265728L + Convert.ToInt64(accountId.Substring(10)) * 2L + Convert.ToInt64(accountId.Substring(8, 1));
		}

		public long ToSteam64() => this.id;
	}
}
