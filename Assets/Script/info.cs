[System.Serializable]
public class LevelStats
{
    public int deaths;
    public float time;

    public LevelStats(int deaths, float time)
    {
        this.deaths = deaths;
        this.time = time;
    }
}
