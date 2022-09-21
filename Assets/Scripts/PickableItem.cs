using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows objects to be picked up from an inventory holder.
public class PickableItem : MonoBehaviour
{
    [SerializeField] public ItemType type;

    // When the player runs in to the item, they will pick it up.
    private void OnTriggerEnter(Collider collider)
    {
        var holder = collider.gameObject.GetComponent<IInventoryHolder>();
        if (holder == null) return;
        holder.Inventory.AddItemByType(type);
        Destroy(gameObject);
    }
}
