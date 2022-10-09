using System;
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

    [SerializeField] private List<Transform> _players;
    [SerializeField] private List<float> distToPlayers;
    
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
   

    void FixedUpdate()
    {
        // if (_playerTargets.Count <= 0) return;
        // agent.SetDestination(_playerTargets[0].position);


        for (int i = 0; i < 2; i++)
        {
            // TODO: fix moving player being missed by the raycast
            //  ==> SphereCast instead
            // TODO: computing the actual distance to the player will take more time than straight dist
            //  ==> should be fine since we only target those we can see straight on anyways
            
            //TODO: Assigning players will not work with spawners, so just grab them on Start instead
            //  Is a costly operation, but for this size it should not be too bad.
            
            RaycastHit hit;
            
            // Might want to layermask instead
            if (Physics.SphereCast(transform.position, 1f, _players[i].position - transform.position, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    distToPlayers[i] = Vector3.Distance(transform.position, _players[i].position);
                }
                else
                {
                    distToPlayers[i] = Mathf.Infinity;
                }
            }
        }

        if (float.IsPositiveInfinity(distToPlayers[0]) && float.IsPositiveInfinity(distToPlayers[1]))
        {
            return;
        }
        else
        {
            if (distToPlayers[0] <= distToPlayers[1])
            {
                agent.SetDestination(_players[0].position);
            }
            else
            {
                agent.SetDestination(_players[1].position);
            }
        }

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

