using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public DeathScreen DeathScreen;
    public void GameOver()
    {
        DeathScreen.Setup();
        //Time.timeScale = 0;
    }
}
