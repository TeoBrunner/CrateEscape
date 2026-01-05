using UnityEngine;

public class PlayerPrefsSaveService : ISaveService
{
    private const string CURRENCY_KEY = "Currency";
    private const string TOP_SCORE_KEY = "TopScore";
    private const string CURRENT_CAR_KEY = "CurrentCar";

    public int LoadCurrency()
    {
        return PlayerPrefs.GetInt(CURRENCY_KEY);
    }
    public void SaveCurrency(int currency)
    {
        PlayerPrefs.SetInt(CURRENCY_KEY, currency);
    }

    public int LoadTopScore()
    {
        return PlayerPrefs.GetInt(TOP_SCORE_KEY);
    }
    public void SaveTopScore(int topScore)
    {
        PlayerPrefs.SetInt(TOP_SCORE_KEY, topScore);
    }
    public string LoadCurrentCarId()
    {
        return PlayerPrefs.GetString(CURRENT_CAR_KEY);
    }
    public void SaveCurrentCar(string id)
    {
        PlayerPrefs.SetString(CURRENT_CAR_KEY, id);
    }
}
