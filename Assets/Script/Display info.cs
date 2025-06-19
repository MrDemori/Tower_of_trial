using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine;
using TMPro;

public class LevelStatsDisplay : MonoBehaviour
{
    public int levelNumber = 1;
    public TextMeshProUGUI statsText;
    [SerializeField] private LocalizedString statsFormatLocalized;
    [SerializeField] private LocalizedString statsEmptyLocalized;

    void Update()
    {
        LevelStats stats = LevelStatsStorage.Instance.GetStatsForLevel(levelNumber);
        if (stats != null)
        {
            int minutes = (int)(stats.time / 60);
            int seconds = (int)(stats.time % 60);
            string formattedTime = $"{minutes}:{seconds:D2}";
            statsFormatLocalized.Arguments = new object[]
            {
                new { time = formattedTime, deaths = stats.deaths }
            };

            statsFormatLocalized.GetLocalizedStringAsync().Completed += handle =>
            {
                statsText.text = handle.Result;
            };
        }
        else
        {
            statsEmptyLocalized.GetLocalizedStringAsync().Completed += handle =>
            {
                statsText.text = handle.Result;
            };
        }
    }
}