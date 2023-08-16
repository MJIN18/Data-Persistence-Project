using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIMainScene : MonoBehaviour
{
    public Text currentPlayerName;
    public Text bestScoreText;

    private int currentScore;

    private static int bestPlayerScore;
    private static string bestPlayerName;

    public MainManager mainManager;

    private void Awake()
    {
        LoadPlayerBestScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerName.text = DataManager.Instance.PlayerName;
        SetBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainManager.m_GameOver)
        {
            CheckBestScore();
        }
    }

    public void CheckBestScore()
    {
        currentScore = DataManager.Instance.BestScore;
        if (currentScore > bestPlayerScore)
        {
            bestPlayerName = DataManager.Instance.PlayerName;
            bestPlayerScore = currentScore;

            bestScoreText.text = $"Best Score : {bestPlayerName} : {currentScore}";

            SaveBestScore(bestPlayerName, bestPlayerScore);
        }
    }

    private void SetBestScore()
    {
        if (string.IsNullOrEmpty(bestPlayerName) && bestPlayerScore == 0)
        {
            bestScoreText.text = "Best Score :";
        }
        else
        {
            bestScoreText.text = $"Best Score : {bestPlayerName} : {bestPlayerScore}";
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public string PlayerName;
        public int BestScore;
    }

    public void SaveBestScore(string playerName, int bestScore)
    {
        SaveData data = new SaveData();
        data.PlayerName = playerName;
        data.BestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public void LoadPlayerBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerScore = data.BestScore;
            bestPlayerName = data.PlayerName;
        }
    }
}
