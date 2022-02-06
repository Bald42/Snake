using UnityEngine;

public static class CustomDebug
{
    public static void LogAttention(string log)
    {
        LogWarning(log, "CC3333");
    }

    public static void LogTestChange(string log)
    {
        LogWarning(log, "33CC33");
    }

    public static void LogOnlyEditor(string log, Color color = default)
    {
#if UNITY_EDITOR
        Log(log, color);
#endif
    }

    private static void LogOnlyEditor(string log, string colorHex)
    {
#if UNITY_EDITOR
        Log(log, colorHex);
#endif
    }

    #region Default

    public static void Log(string log, Color color = default)
    {
        Debug.Log(log.ApplyColor(color));
    }

    public static void Log(string log, string colorHex)
    {
        Debug.Log(log.ApplyColor(colorHex));
    }

    private static void LogWarning(string log, Color color = default)
    {
        Debug.LogWarning(log.ApplyColor(color));
    }

    private static void LogWarning(string log, string colorHex)
    {
        Debug.LogWarning(log.ApplyColor(colorHex));
    }

    public static void LogError(string log, Color color = default)
    {
        Debug.LogError(log.ApplyColor(color));
    }

    public static void LogError(string log, string colorHex)
    {
        Debug.LogError(log.ApplyColor(colorHex));
    }

    #endregion

    #region Add colors

    private static string ApplyColor(this string log, Color color)
    {
        if (color != default)
        {
            log = $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{log}</color>";
        }
        return $"<b>{log}</b>";
    }

    private static string ApplyColor(this string log, string colorHex)
    {
        Color color = default;
        if (!colorHex.Contains("#"))
        {
            colorHex = $"#{colorHex}";
        }
        if (ColorUtility.TryParseHtmlString($"{colorHex}", out color))
        {
            log = $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{log}</color>";
        }
        return $"<b>{log}</b>";
    }

    #endregion
}