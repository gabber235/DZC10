using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// Allows objects to be picked up from an inventory holder.
public class PickableItem : MonoBehaviour
{
    [SerializeField]
    [ItemAttribute]
    public string item;

    // When the player runs in to the item, they will pick it up.
    private void OnTriggerEnter(Collider coll)
    {
        var holder = coll.gameObject.GetComponent<IInventoryHolder>();
        if (holder == null) return;
        var added = holder.Inventory.AddItem(item);
        // Only if the item was added to the inventory, destroy it.
        if(added) Destroy(gameObject);
    }
}
