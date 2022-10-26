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

        private SoundManager SM;

        private int GruntCountdown;

        private void Start()
        {
            _animator = theEnemy.GetComponent<Animator>();
            _navMeshAgent = theEnemy.GetComponent<NavMeshAgent>();
            _elvisController = theEnemy.GetComponent<ElvisController>();
            _enemyController = theEnemy.GetComponent<EnemyController>();

            SM = theEnemy.GetComponent<SoundManager>();
            GruntCountdown = Random.Range(5000, 7000);
        }

        private void Update()
        {
            if(_enemyController.IsDancing){
                enabled = false;
            }else{
                if(GruntCountdown <= 0){
                    SM.playSoundEffect(0);
                    GruntCountdown = Random.Range(5000, 7000);
                }else{
                    GruntCountdown -= 1;
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

        private void CheckAttack(){
            if (_player == null) return;
            if (Time.realtimeSinceStartup - _lastAttackTime < 1.1) return;
            _lastAttackTime = Time.realtimeSinceStartup;
            _player.Damage(1);
            SM.playSoundEffect(1);
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
    }
}