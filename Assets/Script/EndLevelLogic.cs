using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.Core.Extensions;

public class EndLevelLogic : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject statsPanel;

    [Header("Localization")]
    [SerializeField] private LocalizedString levelCompleteText;
    [SerializeField] private LocalizedString levelNumberText;
    [SerializeField] private LocalizedString timeTakenText;
    [SerializeField] private LocalizedString deathsText;

    public int levelNumber = 0;

    private float levelStartTime;
    private int deaths = 0;
    private bool levelFinished = false;

    void Start()
    {
        statsPanel.SetActive(false);
        levelStartTime = Time.time;
        resultText.text = "";
        resultText.enabled = false;
    }

    public void AddDeath()
    {
        deaths++;
    }

    public void LevelCompleted()
    {
        if (levelFinished) return;

        statsPanel.SetActive(true);
        levelFinished = true;

        float timeTaken = Time.time - levelStartTime;
        int minutes = (int)(timeTaken / 60);
        int seconds = (int)(timeTaken % 60);
        string formattedTime = $"{minutes}:{seconds:D2}";

        levelCompleteText.GetLocalizedStringAsync().Completed += op1 =>
        {
            string complete = op1.Result;

            levelNumberText.Arguments = new object[] { levelNumber };
            levelNumberText.GetLocalizedStringAsync().Completed += op2 =>
            {
                string level = op2.Result;

                timeTakenText.Arguments = new object[] { new { time = formattedTime } };
                timeTakenText.GetLocalizedStringAsync().Completed += op3 =>
                {
                    string time = op3.Result;

                    deathsText.Arguments = new object[] { deaths };
                    deathsText.GetLocalizedStringAsync().Completed += op4 =>
                    {
                        string deaths = op4.Result;

                        resultText.text = $"{complete}\n{level}\n{time}\n{deaths}";
                        resultText.enabled = true;
                    };
                };
            };
        };
        LevelStatsStorage.Instance.SaveLevelStats(levelNumber, deaths, timeTaken);
    }
}
