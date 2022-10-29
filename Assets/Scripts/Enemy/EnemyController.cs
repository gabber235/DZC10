using System.Linq;
using Tutorial;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour, IThrowCocktailTrigger, IInteractionCondition
    {
        private static readonly int Dancing = Animator.StringToHash("Dancing");
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public GameObject cocktailPrefab;
        private Animator _animator;
        private ElvisController _elvisController;
        private NavMeshAgent _navMeshAgent;

        private SoundManager _sm;

        public bool IsDancing => currentHealth <= 0;

        // Start is called before the first frame update
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _elvisController = GetComponent<ElvisController>();
            currentHealth = maxHealth;
            _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();

            if (!(currentHealth <= 0)) return;
            EnemyDeath();
            _animator.Play("Swing Dancing", 0, Random.Range(0f, 1f));
        }

        public bool CanInteract(Interactor interactor)
        {
            return !IsDancing;
        }

        public GameObject CocktailPrefab => cocktailPrefab;

        public void OnCocktailHit(Interactor interactor)
        {
            _sm.playSoundEffect(18);
            _sm.playSoundEffect(21);
            Damage(1);

            var tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager == null || !tutorialManager.isActiveAndEnabled) return;

            var playerID = interactor.GetComponent<Player>().playerID;

            tutorialManager.FinishStep(TutorialStep.Serve, playerID, true);

            if (FindObjectsOfType<EnemyController>().All(c => c.IsDancing))
                tutorialManager.ForceStep(TutorialStep.End);
        }

        private void Damage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (IsDancing) EnemyDeath();
        }

        private void EnemyDeath()
        {
            if (_elvisController != null) _elvisController.enabled = false;
            if (_navMeshAgent != null) _navMeshAgent.enabled = false;
            if (_animator != null) _animator.SetBool(Dancing, true);
        }
    }
}