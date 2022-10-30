using Tutorial;
using UnityEngine;

// Allows objects to be picked up from an inventory holder.
namespace Items
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] [Item] public string item;
        public Optional<TutorialStep> completeStep;
        private SoundManager _sm;

        private void Start()
        {
            _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
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

            var soundId = 0;
            if (item == ItemType.Lemon.Name) soundId = 16;
            if (item == ItemType.Strawberry.Name) soundId = 17;

            if (soundId > 0) _sm.playSoundEffect(soundId);

            // Only if the item was added to the inventory, destroy it.
            if (!added) return;
            if (completeStep.Enabled)
                FindObjectOfType<TutorialManager>()?.FinishStep(completeStep.Value, holder.playerID);
            Destroy(gameObject);
        }
    }
}