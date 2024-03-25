namespace BDStore.Domain.utils;

public static class StringUtils
{
    public static string OnlyNumbers(this string str, string input)
        => new string(input.Where(char.IsDigit).ToArray());
}