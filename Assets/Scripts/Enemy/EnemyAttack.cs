using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        public GameObject theEnemy;

        private Animator _animator;
        private ElvisController _elvisController;
        private EnemyController _enemyController;

        private float _lastAttackTime;
        private NavMeshAgent _navMeshAgent;
        private Player _player;

        private void Start()
        {
            _animator = theEnemy.GetComponent<Animator>();
            _navMeshAgent = theEnemy.GetComponent<NavMeshAgent>();
            _elvisController = theEnemy.GetComponent<ElvisController>();
            _enemyController = theEnemy.GetComponent<EnemyController>();
        }

        private void Update()
        {
            if (_enemyController.IsDancing)
            {
                enabled = false;
                return;
            }

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
            _animator.SetBool(Attacking, true);
            _elvisController.enabled = false;
            _navMeshAgent.enabled = false;
        }

        private void OnTriggerExit(Collider coll)
        {
            if (!coll.CompareTag("Player")) return;
            _animator.SetBool(Attacking, false);
            _elvisController.enabled = true;
            _navMeshAgent.enabled = true;
            _player = null;
        }
    }
}