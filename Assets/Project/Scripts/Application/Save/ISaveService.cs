public interface ISaveService
{
    int LoadCurrency();
    void SaveCurrency(int currency);

    int LoadTopScore();
    void SaveTopScore(int topScore);

    string LoadCurrentCar();
    void SaveCurrentCar(string id);
}
