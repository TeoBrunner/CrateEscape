public interface ISaveService
{
    int LoadTopScore();
    void SaveTopScore(int topScore);

    string LoadCurrentCar();
    void SaveCurrentCar(string id);
}
