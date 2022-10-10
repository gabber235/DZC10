using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemAutoDispose : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_particleSystem.isStopped) Destroy(gameObject);
    }
}