using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ElvisController : MonoBehaviour {
    private Vector3 _target;
    public UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private List<Transform> players;
    [SerializeField] private List<float> distToPlayers;
    private int _playerCount;

    [SerializeField] private Vector3 _targetPos;

    [SerializeField] private float navDist;
    // Start is called before the first frame update
    void Awake() {
        players = GameObject.FindObjectsOfType<PlayerController>().Select(p => p.transform).ToList();
        distToPlayers = new List<float> { Mathf.Infinity, Mathf.Infinity };
        _playerCount = players.Count;

        agent.SetDestination(transform.localPosition);
    }
    
    void Update() {
        for (int i = 0; i < _playerCount; i++) {
    
            RaycastHit hit;
            Debug.DrawRay(transform.position, players[i].position - transform.position, Color.yellow);
    
            if (Physics.SphereCast(transform.position, 1f,players[i].position - transform.position, out hit)) {
                if (hit.collider.CompareTag("Player")) {
                    distToPlayers[i] = Vector3.Distance(transform.position, players[i].position);
                }
                else {
                    distToPlayers[i] = Mathf.Infinity;
                }
            }
        }
    
        if (float.IsPositiveInfinity(distToPlayers[0]) && float.IsPositiveInfinity(distToPlayers[1])) {
            return;
        }
        else {
            if (distToPlayers[0] <= distToPlayers[1]) {
                agent.SetDestination(players[0].position);
                _targetPos = players[0].position;
                agent.isStopped = false;
            }
            else {
                agent.SetDestination(players[1].position);
                _targetPos = players[1].position;
                agent.isStopped = false;
            }
        }
        
        navDist = agent.remainingDistance;
        _targetPos = agent.destination;
        
        if (agent.remainingDistance < 0.3)
        {
            // Attack logic
            agent.isStopped = true;
        }
    }
}

