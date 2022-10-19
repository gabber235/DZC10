using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void QuitGameButton() {
        Debug.Log("Quit game");
        Application.Quit();
        
    }

    public void NextLevelButton() {
        Debug.Log("Going to the next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMenuButton() {
        Debug.Log("Returning to menu");
        SceneManager.LoadScene(0);
    }
}
