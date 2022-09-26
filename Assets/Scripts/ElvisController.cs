using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElvisController : MonoBehaviour
{
    private Vector3 _target;
    public float speed = 1.0f;
    private Camera _camera;
    public UnityEngine.AI.NavMeshAgent agent;
    private readonly List<Transform> _playerTargets = new();

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
        if (_playerTargets.Count <= 0) return;
        agent.SetDestination(_playerTargets[0].position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _playerTargets.Add(col.transform);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (Transform transform in _playerTargets.ToArray())
            {
                if (transform == col.transform)
                {
                    _playerTargets.Remove(col.transform);
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

