using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Settings : MonoBehaviour {

    public static Settings Instance;

    public struct DifficultyLevel
    {
        public float chanceAllLanesFree;
        public float chanceTwoLanesFree;
        public float chanceOneLaneFree;

        public float deadline;
    };

    internal DifficultyLevel[] DiffLevels;

    internal int gameDifficultyLevel = 1;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("titles");
        }
    }

    void Awake()
    {
        Instance = this;

        DiffLevels = new DifficultyLevel[3];

        DiffLevels[0].chanceAllLanesFree = 0.2f;
        DiffLevels[0].chanceTwoLanesFree = 0.5f;
        DiffLevels[0].chanceOneLaneFree = 0.3f;
        DiffLevels[2].deadline = 240f;

        DiffLevels[1].chanceAllLanesFree = 0.1f;
        DiffLevels[1].chanceTwoLanesFree = 0.3f;
        DiffLevels[1].chanceOneLaneFree = 0.6f;
        DiffLevels[2].deadline = 225f;

        DiffLevels[2].chanceAllLanesFree = 0f;
        DiffLevels[2].chanceTwoLanesFree = 0.2f;
        DiffLevels[2].chanceOneLaneFree = 0.8f;
        DiffLevels[2].deadline = 210f;

        // Load

        gameDifficultyLevel = PlayerPrefs.GetInt("SatisfactionSettings_gameDifficultyLevel", 1);
    }
}
