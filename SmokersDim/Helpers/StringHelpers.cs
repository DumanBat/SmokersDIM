public static class StringHelpers
{	
	public static string TruncateLongString(string str, int maxLength)
	{
		return str?[0..Math.Min(str.Length, maxLength)];
	}
}