public static class StringFormat
{
	public static string UppercaseFirst(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return string.Empty;
		}
		char[] a = s.ToCharArray();
		a[0] = char.ToUpper(a[0]);
		return new string(a);
	}
	
	public static string UppercaseWords(string value)
	{
		char[] array = value.ToCharArray();

		if (array.Length >= 1)
		{
			if (char.IsLower(array[0]))
			{
				array[0] = char.ToUpper(array[0]);
			}
		}

		for (int i = 1; i < array.Length; i++)
		{
			if (array[i - 1] == ' ')
			{
				if (char.IsLower(array[i]))
				{
					array[i] = char.ToUpper(array[i]);
				}
			}
		}
		return new string(array);
	}
}