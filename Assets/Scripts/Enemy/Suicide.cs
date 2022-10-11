using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    private void OnTriggerStay(Collider coll)
    {
        var player = coll.gameObject.GetComponent<Player>();
        if (player == null) return;
        // Collider is way to big (for player detection) so we need to manually check the distance.
        if (Vector3.Distance(player.transform.position, transform.position) > 0.5) return;
        player.Damage(1);
        Destroy(gameObject);
    }
}
