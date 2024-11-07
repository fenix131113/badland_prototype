namespace Core
{
    public class GameStats
    {
        public int CurrentLevel { get; private set; } = 1;

        public void IncreaseLevel() => CurrentLevel++;
    }
}