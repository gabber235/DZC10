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

        private float _lastAttackTime;
        private NavMeshAgent _navMeshAgent;
        private Player _player;

        private SoundManager _sm;

        private void Start()
        {
            _animator = theEnemy.GetComponent<Animator>();
            _navMeshAgent = theEnemy.GetComponent<NavMeshAgent>();
            _elvisController = theEnemy.GetComponent<ElvisController>();
            _enemyController = theEnemy.GetComponent<EnemyController>();

            _sm = theEnemy.GetComponent<SoundManager>();
            _gruntCountdown = Random.Range(5000, 7000);
        }

        private void Update()
        {
            if (_enemyController.IsDancing)
            {
                enabled = false;
            }
            else
            {
                if (_gruntCountdown <= 0)
                {
                    _sm.playSoundEffect(0);
                    _gruntCountdown = Random.Range(5000, 7000);
                }
                else
                {
                    _gruntCountdown -= 1;
                }

                // if(_enemyController.IsWalking && StepCountdown <= 0){
                //     SM.audioSrc.PlayOneShot(SM.soundEffects[2]);
                //     StepCountdown = StepSoundDelay;
                // }else{
                //     StepCountdown -= 1;
                // }
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
            _sm.playSoundEffect(1);
        }
    }
}