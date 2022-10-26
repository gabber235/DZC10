using Tutorial;
using UnityEngine;

// Allows objects to be picked up from an inventory holder.
public class PickableItem : MonoBehaviour
{
    private SoundManager SM;
    private int SEID;

    [SerializeField] [ItemAttribute] public string item;

    public Optional<TutorialStep> completeStep;
    private void Start(){
        SM = GameObject.Find("SM_SE").GetComponent<SoundManager>();
        SEID = 0;
    }

    private void FixedUpdate()
    {
        var trans = transform;
        // Rotate the item.
        trans.Rotate(Vector3.up, 1f);
        // Move the item up and down smoothly to simulate bobbing.
        var position = trans.position;
        position =
            new Vector3(position.x, Mathf.Sin(Time.time) * 0.25f + 0.5f, position.z);
        transform.position = position;
    }

    // When the player runs in to the item, they will pick it up.
    private void OnTriggerEnter(Collider coll)
    {
        var holder = coll.gameObject.GetComponent<Player>();
        if (holder == null) return;
        var added = holder.Inventory.AddItem(item);

        if(item == "Lemon") SEID = 16;
        // if(item == "Strawberry") SEID = 17;
        if(item == "Pepper") SEID = 17;

        SM.playSoundEffect(SEID);

        // Only if the item was added to the inventory, destroy it.
        if (!added) return;
        if (completeStep.Enabled) FindObjectOfType<TutorialManager>()?.FinishStep(completeStep.Value, holder.playerID);
        Destroy(gameObject);
    }
}