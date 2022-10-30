using Tutorial;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour
{
    public int playerID;
    public int health;

    [HideInInspector] public bool shaker;
    [HideInInspector] public double lastDamTime;

    [SerializeField] private InputActionReference shakeActionReference;
    [HideInInspector] public bool dead;
    public UnityEvent GameOver;

    public Inventory Inventory;

    private SoundManager SM;

    public void Start()
    {
        Inventory = new Inventory(4, playerID);
        health = 5;

        shakeActionReference.action.Enable();
        shakeActionReference.action.performed += OnShake;
        SM = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }

    private void OnShake(InputAction.CallbackContext context)
    {
        if (!shaker){
            SM.playSoundEffect(2);
            return;
        }
        Inventory.MakeCockTail(SM);
        var tutorialManager = FindObjectOfType<TutorialManager>();
        if (tutorialManager == null || !tutorialManager.isActiveAndEnabled) return;

        tutorialManager.FinishStep(TutorialStep.Shake1, playerID, true);
        tutorialManager.FinishStep(TutorialStep.Shake2, playerID, true);
    }

    public void Damage(int damage)
    {
        if (dead) return;
        health -= damage;
        SM.playSoundEffect(13+playerID);

        if (health <= 0 && GameObject.Find("Restart Background")==null)
        {
            health = 0;
            dead = true;
            GameOver.Invoke();
        }

        // last dam time --> currrent playing time
        lastDamTime = Time.realtimeSinceStartup;
    }
}