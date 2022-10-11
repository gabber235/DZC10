using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour, IThrowCocktailTrigger
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public GameObject cocktailPrefab;

        // Start is called before the first frame update
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public GameObject CocktailPrefab => cocktailPrefab;

        public void OnCocktailHit(Interactor interactor)
        {
            GetComponent<Animator>().Play("Swing Dancing");
            GetComponent<ElvisController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }

        private void Damage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth == 0) EnemyDeath();
        }

        private void EnemyDeath()
        {
            Destroy(gameObject);
        }
    }
}