using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class ElvisController : MonoBehaviour
    {
        public NavMeshAgent agent;
        [SerializeField] private List<Transform> players;
        [SerializeField] private List<float> distToPlayers;

        [SerializeField] private bool debug;
        private int _playerCount;
        private Vector3 _target;

        private Vector3 _targetPos;

        // Start is called before the first frame update
        private void Awake()
        {
            players = FindObjectsOfType<PlayerController>().Select(p => p.transform).ToList();
            distToPlayers = players.Select(_ => Mathf.Infinity).ToList();
            _playerCount = players.Count;

            agent.SetDestination(transform.localPosition);
        }

        private void FixedUpdate()
        {
            var pos = transform.position;
            for (var i = 0; i < _playerCount; i++)
            {
#if UNITY_EDITOR
                if (debug) Debug.DrawRay(pos, players[i].position - pos, Color.yellow);
#endif

                if (!Physics.SphereCast(transform.position, 0.5f, players[i].position - transform.position,
                        out var hit))
                    continue;

                if (hit.collider.CompareTag("Player"))
                    distToPlayers[i] = Vector3.Distance(transform.position, players[i].position);
                else
                    distToPlayers[i] = Mathf.Infinity;
            }

            if (float.IsPositiveInfinity(distToPlayers[0]) && float.IsPositiveInfinity(distToPlayers[1])) return;

            var min = distToPlayers.Min();
            var closestIndex = distToPlayers.FindIndex(d => Math.Abs(d - min) < 0.001f);

            if (closestIndex != -1)
            {
                agent.SetDestination(players[closestIndex].position);
                _targetPos = players[closestIndex].position;
                agent.isStopped = false;
                _targetPos = agent.destination;
            }

            if (agent.remainingDistance < 1)
                // Attack logic
                agent.isStopped = true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_targetPos, 0.5f);
        }
#endif
    }
}