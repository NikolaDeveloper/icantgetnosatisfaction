    using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMainMenu : MonoBehaviour {

    public static SettingsMainMenu Instance;

    internal Color mainCol, stripeCol, windowsCol;

    internal string trainName;

    internal int gameDifficultyLevel = 1;

    public struct DifficultyLevel
    {
        public float chanceAllLanesFree;
        public float chanceTwoLanesFree;
        public float chanceOneLaneFree;

        public float deadline;
    };

    internal DifficultyLevel[] DiffLevels;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Awake()
    {
        Instance = this;

        DiffLevels = new DifficultyLevel[3];

        DiffLevels[0].chanceAllLanesFree = 0.2f;
        DiffLevels[0].chanceTwoLanesFree = 0.5f;
        DiffLevels[0].chanceOneLaneFree = 0.3f;
        DiffLevels[0].deadline = 240f;

        DiffLevels[1].chanceAllLanesFree = 0.1f;
        DiffLevels[1].chanceTwoLanesFree = 0.3f;
        DiffLevels[1].chanceOneLaneFree = 0.6f;
        DiffLevels[1].deadline = 225f;

        DiffLevels[2].chanceAllLanesFree = 0f;
        DiffLevels[2].chanceTwoLanesFree = 0.2f;
        DiffLevels[2].chanceOneLaneFree = 0.8f;
        DiffLevels[2].deadline = 210f;

        gameDifficultyLevel = PlayerPrefs.GetInt("SatisfactionSettings_gameDifficultyLevel", 1);

        mainCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_mainR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_mainG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_mainB", 1.0f));
        stripeCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_stripeR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_stripeG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_stripeB", 1.0f));
        windowsCol = new Color(PlayerPrefs.GetFloat("SatisfactionSettings_windowsR", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_windowsG", 1.0f), PlayerPrefs.GetFloat("SatisfactionSettings_windowsB", 1.0f));

        trainName = PlayerPrefs.GetString("SatisfactionSettings_trainName", "");
    }

    public void SetDifficultyFromLoad()
    {
        GameObject easy = GameObject.FindGameObjectWithTag("easyCheck");
        GameObject medium = GameObject.FindGameObjectWithTag("mediumCheck");
        GameObject hard = GameObject.FindGameObjectWithTag("hardCheck");
        
        if (gameDifficultyLevel == 0)
        {
            easy.GetComponent<Toggle>().isOn = true;
        }
        else if (gameDifficultyLevel == 1)
        {
            medium.GetComponent<Toggle>().isOn = true;
        }
        else if (gameDifficultyLevel == 2)
        {
            hard.GetComponent<Toggle>().isOn = true;
        }
    }

    public void SetDifficultyEasy()
    {
        SetDifficultyLevel(0);
    }

    public void SetDifficultyMedium()
    {
        SetDifficultyLevel(1);
    }

    public void SetDifficultyHard()
    {
        SetDifficultyLevel(2);
    }

    public void SetDifficultyLevel(int setDiffLevel)
    {
        Debug.Log("diff set to " + setDiffLevel);
        gameDifficultyLevel = setDiffLevel;
        PlayerPrefs.SetInt("SatisfactionSettings_gameDifficultyLevel", gameDifficultyLevel);
        PlayerStats.GetInstance().deadline = DiffLevels[gameDifficultyLevel].deadline;
    }

    public void SetTrainName(string name)
    {
        trainName = name;
        PlayerPrefs.SetString("SatisfactionSettings_trainName", trainName);
    }

    public void SetTrainMainColor(Color main)
    {
        mainCol = main;

        PlayerPrefs.SetFloat("SatisfactionSettings_mainR", mainCol.r);
        PlayerPrefs.SetFloat("SatisfactionSettings_mainG", mainCol.g);
        PlayerPrefs.SetFloat("SatisfactionSettings_mainB", mainCol.b);
    }

    public void SetTrainStripeColor(Color stripe)
    {
        stripeCol = stripe;

        PlayerPrefs.SetFloat("SatisfactionSettings_stripeR", stripeCol.r);
        PlayerPrefs.SetFloat("SatisfactionSettings_stripeG", stripeCol.g);
        PlayerPrefs.SetFloat("SatisfactionSettings_stripeB", stripeCol.b);
    }

    public void SetTrainWindowsColor(Color windows)
    {
        windowsCol = windows;

        PlayerPrefs.SetFloat("SatisfactionSettings_windowsR", windowsCol.r);
        PlayerPrefs.SetFloat("SatisfactionSettings_windowsG", windowsCol.g);
        PlayerPrefs.SetFloat("SatisfactionSettings_windowsB", windowsCol.b);
    }
}
