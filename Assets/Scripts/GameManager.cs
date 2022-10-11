using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DeathScreen deathScreen;

    public void GameOver()
    {
        deathScreen.Setup();
        //Time.timeScale = 0;
    }
}