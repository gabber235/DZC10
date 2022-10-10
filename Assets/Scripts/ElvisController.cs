using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElvisController : MonoBehaviour {
    private Vector3 _target;
    public UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private List<Transform> players;
    [SerializeField] private List<float> distToPlayers;
    private int _playerCount;

    // Start is called before the first frame update
    void Awake() {
        players = GameObject.FindObjectsOfType<PlayerController>().Select(p => p.transform).ToList();
        distToPlayers = new List<float> { 0, 0 };
        _playerCount = players.Count;
    }
    
    void FixedUpdate() {
        for (int i = 0; i < _playerCount; i++) {

            RaycastHit hit;

            // Might want to layermask instead
            if (Physics.SphereCast(transform.position, 1f, players[i].position - transform.position, out hit)) {
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
            }
            else {
                agent.SetDestination(players[1].position);
            }
        }
    }
}

