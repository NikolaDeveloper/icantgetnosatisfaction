using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    public static Settings Instance;

    public struct DifficultyLevel
    {
        public float chanceAllLanesFree;
        public float chanceTwoLanesFree;
        public float chanceOneLaneFree;
    };

    internal DifficultyLevel[] DiffLevels;

    internal int gameDifficultyLevel = 1;

    internal Color mainCol, stripeCol, windowsCol;

    internal string trainName;

    void Awake()
    {
        Instance = this;

        DiffLevels = new DifficultyLevel[3];

        DiffLevels[0].chanceAllLanesFree = 0.2f;
        DiffLevels[0].chanceTwoLanesFree = 0.5f;
        DiffLevels[0].chanceOneLaneFree = 0.3f;

        DiffLevels[1].chanceAllLanesFree = 0.1f;
        DiffLevels[1].chanceTwoLanesFree = 0.3f;
        DiffLevels[1].chanceOneLaneFree = 0.6f;

        DiffLevels[2].chanceAllLanesFree = 0f;
        DiffLevels[2].chanceTwoLanesFree = 0.2f;
        DiffLevels[2].chanceOneLaneFree = 0.8f;

        // Load

        gameDifficultyLevel = PlayerPrefs.GetInt("SatisfactionSettings_gameDifficultyLevel", 1);

        mainCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_mainR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_mainG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_mainB", 1.0f));
        stripeCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_stripeR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_stripeG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_stripeB", 1.0f));
        windowsCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_windowsR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_windowsG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_windowsB", 1.0f));

        trainName = PlayerPrefs.GetString("SatisfactionSettings_trainName", "");
    }

    void SetTrainName(string name)
    {
        trainName = name;
        PlayerPrefs.SetString("SatisfactionSettings_trainName", trainName);
    }

    void SetDifficultyLevel(int setDiffLevel)
    {
        gameDifficultyLevel = setDiffLevel;
        PlayerPrefs.SetInt("SatisfactionSettings_gameDifficultyLevel", gameDifficultyLevel);
    }

    void SetTrainColor(Color main, Color stripe, Color windows)
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
