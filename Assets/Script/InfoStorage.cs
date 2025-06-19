using System.Collections.Generic;
using UnityEngine;

public class LevelStatsStorage : MonoBehaviour
{
    public static LevelStatsStorage Instance { get; private set; }

    private Dictionary<int, LevelStats> levelStats = new Dictionary<int, LevelStats>();

    private const string StatsKey = "LevelStatsData";

    [System.Serializable]
    private class StatsWrapper
    {
        public List<int> keys = new List<int>();
        public List<LevelStats> values = new List<LevelStats>();

        public StatsWrapper(Dictionary<int, LevelStats> dict)
        {
            foreach (var kvp in dict)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public Dictionary<int, LevelStats> ToDictionary()
        {
            Dictionary<int, LevelStats> result = new Dictionary<int, LevelStats>();
            for (int i = 0; i < keys.Count; i++)
            {
                result[keys[i]] = values[i];
            }
            return result;
        }
    }

    //private void Start()
    //{
    //    ClearAllStats();
    //}

    //public void ClearAllStats()
    //{
    //    levelStats.Clear();
    //    PlayerPrefs.DeleteKey(StatsKey); // Видаляє JSON із PlayerPrefs
    //    PlayerPrefs.Save();
    //    Debug.Log("[ClearAllStats] Всі статистики видалені.");
    //}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveLevelStats(int levelNumber, int deaths, float time)
    {
        Debug.Log($"[SaveLevelStats] Saving Level {levelNumber} | Deaths: {deaths} | Time: {time}");

        levelStats[levelNumber] = new LevelStats(deaths, time);
        SaveAllStats();
    }

    public LevelStats GetStatsForLevel(int levelNumber)
    {
        if (levelStats.TryGetValue(levelNumber, out LevelStats stats))
            return stats;
        return null;
    }

    private void SaveAllStats()
    {
        var wrapper = new StatsWrapper(levelStats);
        string json = JsonUtility.ToJson(wrapper);
        Debug.Log($"[SaveAllStats] JSON: {json}");

        PlayerPrefs.SetString(StatsKey, json);
        PlayerPrefs.Save();
    }

    private void LoadAllStats()
    {
        string json = PlayerPrefs.GetString(StatsKey, "");
        Debug.Log($"[LoadAllStats] JSON: {json}");

        if (!string.IsNullOrEmpty(json))
        {
            var wrapper = JsonUtility.FromJson<StatsWrapper>(json);
            levelStats = wrapper.ToDictionary();
        }
    }


}
