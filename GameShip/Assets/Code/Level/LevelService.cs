using UnityEngine.SceneManagement;

namespace ShipGame
{
    [System.Serializable]
    public class LevelService
    {
        public void Init(SpaceshipPlayer player)
        {
            player.OnDeath += RestartLevel;
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}