using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int secondsDelay = 1;

    // Start is called before the first frame update
    private void Start()
    {
        SpawnRandom();
    }

    private void SpawnRandom()
    {
        var index = Random.Range(0, prefabs.Length);
        var go = Instantiate(prefabs[index], transform.position, Quaternion.identity);
        var onDestroy = go.GetComponent<OnDestroyDispatcher>();
        if (onDestroy != null) onDestroy.OnObjectDestroyed += OnDestroyed;
    }

    private void OnDestroyed(GameObject go)
    {
        StartCoroutine(WaitSpawn());
    }

    private IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(secondsDelay);
        if (!isActiveAndEnabled)
            SpawnRandom();
    }
}