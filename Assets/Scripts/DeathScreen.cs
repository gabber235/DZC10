using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathScreen : MonoBehaviour
{

    private AudioSource _as;
    public AudioClip GameOverBGM;
    public AudioClip GameOverStartSound;

    public void Setup()
    {
        gameObject.SetActive(true);
        _as = GameObject.Find("SM_BGM").GetComponent<AudioSource>();
        StartCoroutine(DeathScreenAudio());
    }

    IEnumerator DeathScreenAudio(){
        _as.Stop();
        _as.volume = 0.75f;
        _as.PlayOneShot(GameOverStartSound);
        _as.clip = GameOverBGM;
        yield return new WaitForSeconds(GameOverStartSound.length);
        _as.Play();
    }

    public void RestartButton()
    {
        //Time.timeScale = 1;
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}