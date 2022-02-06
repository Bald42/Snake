using UnityEngine;

public static class StringExtension
{
    public static string ReplaceClone(this string baseString, string replaceString)
    {
        return baseString.Replace("(Clone)", replaceString);
    }
}