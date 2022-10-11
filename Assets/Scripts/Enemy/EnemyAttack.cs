using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        public GameObject theEnemy;

        private float _lastAttackTime;
        private Player _player;

        private void Update()
        {
            if (_player == null) return;
            if (Time.realtimeSinceStartup - _lastAttackTime < 1.1) return;
            _lastAttackTime = Time.realtimeSinceStartup;
            _player.Damage(1);
        }

        private void OnTriggerEnter(Collider coll)
        {
            var player = coll.gameObject.GetComponent<Player>();
            if (player == null) return;
            _player = player;
            _lastAttackTime = Time.realtimeSinceStartup - 0.8f;
            theEnemy.GetComponent<Animator>().Play("Mutant Punch");
            theEnemy.GetComponent<ElvisController>().enabled = false;
            theEnemy.GetComponent<NavMeshAgent>().enabled = false;
        }

        private void OnTriggerExit(Collider coll)
        {
            if (!coll.CompareTag("Player")) return;
            theEnemy.GetComponent<Animator>().Play("Catwalk Walking");
            theEnemy.GetComponent<ElvisController>().enabled = true;
            theEnemy.GetComponent<NavMeshAgent>().enabled = true;
            _player = null;
        }
    }
}