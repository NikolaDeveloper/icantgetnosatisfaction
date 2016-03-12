    using UnityEngine;
using System.Collections;

public class SettingsMainMenu : MonoBehaviour {

    public static SettingsMainMenu Instance;

    internal Color mainCol, stripeCol, windowsCol;

    internal string trainName;

    internal int gameDifficultyLevel = 1;

    void Awake()
    {
        Instance = this;

        gameDifficultyLevel = PlayerPrefs.GetInt("SatisfactionSettings_gameDifficultyLevel", 1);

        mainCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_mainR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_mainG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_mainB", 1.0f));
        stripeCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_stripeR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_stripeG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_stripeB", 1.0f));
        windowsCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_windowsR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_windowsG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_windowsB", 1.0f));

        trainName = PlayerPrefs.GetString("SatisfactionSettings_trainName", "");
    }

    public void SetDifficultyLevel(int setDiffLevel)
    {
        gameDifficultyLevel = setDiffLevel;
        PlayerPrefs.SetInt("SatisfactionSettings_gameDifficultyLevel", gameDifficultyLevel);
    }

    public void SetTrainName(string name)
    {
        trainName = name;
        PlayerPrefs.SetString("SatisfactionSettings_trainName", trainName);
    }

    public void SetTrainColor(Color main, Color stripe, Color windows)
    {
        mainCol = main;
        stripeCol = stripe;
        windowsCol = windows;

        PlayerPrefs.SetFloat("SatisfactionSettings_mainR", mainCol.r);
        PlayerPrefs.SetFloat("SatisfactionSettings_mainG", mainCol.g);
        PlayerPrefs.SetFloat("SatisfactionSettings_mainB", mainCol.b);

        PlayerPrefs.SetFloat("SatisfactionSettings_stripeR", stripeCol.r);
        PlayerPrefs.SetFloat("SatisfactionSettings_stripeG", stripeCol.g);
        PlayerPrefs.SetFloat("SatisfactionSettings_stripeB", stripeCol.b);

        PlayerPrefs.SetFloat("SatisfactionSettings_windowsR", windowsCol.r);
        PlayerPrefs.SetFloat("SatisfactionSettings_windowsG", windowsCol.g);
        PlayerPrefs.SetFloat("SatisfactionSettings_windowsB", windowsCol.b);
    }
}
