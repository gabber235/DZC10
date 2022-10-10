using UnityEngine;

public class Sprinkler : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public void StartSprinkler()
    {
        _particleSystem.Play();
    }
}