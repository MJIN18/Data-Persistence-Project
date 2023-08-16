using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerName;

    public TextMeshProUGUI bestScoreText;

    private static int bestPlayerScore;
    private static string bestPlayerName;

    [System.Serializable]
    public class SaveData
    {
        public string PlayerName;
        public int BestScore;
    }

    private void Awake()
    {
        LoadBestScore();
    }

    public void PlayerNameEntered()
    {
        DataManager.Instance.PlayerName = currentPlayerName.text;
    }
    public void StartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetBestScoreText()
    {
        if (string.IsNullOrEmpty(bestPlayerName) && bestPlayerScore == 0)
        {
            bestScoreText.text = "Best Score";
        }
        else
        {
            bestScoreText.text = $"Best Score : {bestPlayerName} : {bestPlayerScore}";
        }
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerScore = data.BestScore;
            bestPlayerName = data.PlayerName;
            SetBestScoreText();
        }
    }
}
