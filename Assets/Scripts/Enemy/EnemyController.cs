using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour, IThrowCocktailTrigger, IInteractionCondition
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public GameObject cocktailPrefab;

        public bool IsDancing => currentHealth <= 0;


        // Start is called before the first frame update
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public bool CanInteract(Interactor interactor)
        {
            return !IsDancing;
        }

        public GameObject CocktailPrefab => cocktailPrefab;

        public void OnCocktailHit(Interactor interactor)
        {
            Damage(1);
        }

        private void Damage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (IsDancing) EnemyDeath();
        }

        private void EnemyDeath()
        {
            GetComponent<ElvisController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().SetBool("Dancing", true);
        }
    }
}