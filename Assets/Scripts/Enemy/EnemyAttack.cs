using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject the_enemy;

    void OnTriggerEnter(Collider colli)
    {
        if (colli.CompareTag("Player"))
        {
            the_enemy.GetComponent<Animator>().Play("Mutant Punch");
            the_enemy.GetComponent<ElvisController>().enabled = false;
            the_enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        var player = coll.gameObject.GetComponent<Player>();
        if (player == null) return;
        // Collider is way to big (for player detection) so we need to manually check the distance.
        if (Vector3.Distance(player.transform.position, transform.position) > 0.5) return;
        player.Damage(1);
    }

    void OnTriggerExit(Collider colli)
    {
        if (colli.CompareTag("Player"))
        {
            the_enemy.GetComponent<Animator>().Play("Catwalk Walking");
            the_enemy.GetComponent<ElvisController>().enabled = true;
            the_enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        
    }
}
