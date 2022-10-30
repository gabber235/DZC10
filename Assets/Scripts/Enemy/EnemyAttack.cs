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

        private int _gruntCountdown;
        private int _stepCountdown;

        private float _lastAttackTime;
        private NavMeshAgent _navMeshAgent;
        private Player _player;

        private SoundManager _smSelf;
        private SoundManager _sm;

        private void Start()
        {
            _animator = theEnemy.GetComponent<Animator>();
            _navMeshAgent = theEnemy.GetComponent<NavMeshAgent>();
            _elvisController = theEnemy.GetComponent<ElvisController>();
            _enemyController = theEnemy.GetComponent<EnemyController>();
            _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
            _smSelf = theEnemy.GetComponent<SoundManager>();
            _gruntCountdown = Random.Range(1500, 2500);
            _stepCountdown = 150;
        }

        private void Update()
        {
            if (_enemyController.IsDancing)
            {
                enabled = false;
            }
            else
            {
                if (_gruntCountdown <= 0 && _enemyController.IsWalking)
                {
                    _smSelf.playSoundEffect(0, true);
                    _gruntCountdown = Random.Range(1500, 2500);
                }
                else
                {
                    _gruntCountdown -= 1;
                }

                if(_enemyController.IsWalking && _stepCountdown <= 0){
                    _sm.playSoundEffect(3);
                    _stepCountdown = 150;
                }else{
                    _stepCountdown -= 1;
                }
                CheckAttack();
            }
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (_enemyController.IsDancing)
            {
                enabled = false;
                return;
            }

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
            if (_enemyController.IsDancing)
            {
                enabled = false;
                return;
            }

            if (!coll.CompareTag("Player")) return;
            _animator.SetBool(Attacking, false);
            _elvisController.enabled = true;
            _navMeshAgent.enabled = true;
            _player = null;
        }

        private void CheckAttack()
        {
            if (_player == null) return;
            if (Time.realtimeSinceStartup - _lastAttackTime < 1.1) return;
            _lastAttackTime = Time.realtimeSinceStartup;
            _player.Damage(1);
            _sm.playSoundEffect(9);
        }
    }
}