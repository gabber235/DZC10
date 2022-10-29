using Tutorial;
using UnityEngine;

// Allows objects to be picked up from an inventory holder.
namespace Items
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] [Item] public string item;
        public Optional<TutorialStep> completeStep;
        private int _seid;
        private SoundManager _sm;

        private void Start()
        {
            _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
            _seid = 0;
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

            if (item == "Lemon") _seid = 16;
            // if(item == "Strawberry") SEID = 17;
            if (item == "Pepper") _seid = 17;

            _sm.playSoundEffect(_seid);

            // Only if the item was added to the inventory, destroy it.
            if (!added) return;
            if (completeStep.Enabled)
                FindObjectOfType<TutorialManager>()?.FinishStep(completeStep.Value, holder.playerID);
            Destroy(gameObject);
        }
    }
}