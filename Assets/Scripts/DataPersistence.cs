using UnityEngine;

public static class DataPersistence
{
    public const string soundKey = "SoundMuted";
    public const string highScoreKey = "HighScore";
    public const string coinKey = "Coin";
    public const string gyroSensetiveKey = "GyroSensetive";
    public const string vibrationKey = "Vibration";

    public static void SaveInt(string key, int newData)
    {
        PlayerPrefs.SetInt(key, newData);
        PlayerPrefs.Save();
    }
    
    public static int LoadInt(string key, int defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        return defaultValue;
    }
}