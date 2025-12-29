using UnityEngine;

public class PlayerPrefsSaveService : ISaveService
{
    private const string TOP_SCORE_KEY = "TopScore";
    public int LoadTopScore()
    {
        return PlayerPrefs.GetInt(TOP_SCORE_KEY);
    }

    public void SaveTopScore(int topScore)
    {
        PlayerPrefs.SetInt(TOP_SCORE_KEY, topScore);
    }
}
