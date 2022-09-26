using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElvisController : MonoBehaviour
{
    private Vector3 _target;
    public float speed = 1.0f;
    private Camera _camera;
    public UnityEngine.AI.NavMeshAgent agent;
    public List<Transform> playerTargets = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        var position = transform.position;
        SetNewTarget (new Vector3(
            position.x + 10,
            position.y,
            position.z + 10
        ));
    }

// Update is called once per frame
   

    void Update()
    {
        if (playerTargets.Count > 0)
        {
            if (playerTargets[0] != null)
            {
                agent.SetDestination(playerTargets[0].position);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerTargets.Add(col.transform);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (Transform transform in playerTargets.ToArray())
            {
                if (transform == col.transform)
                {
                    playerTargets.Remove(col.transform);
                }
            }
        }
    }

    void SetNewTarget(Vector3 newTarget)
    {
        _target = newTarget;
        transform.LookAt(_target);
    }
}

