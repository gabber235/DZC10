using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // The players that still need to finish the step
    public List<int> needsFinish = new();


    private TutorialSteps _step;

    // Current step of the tutorial
    public TutorialSteps Step
    {
        get => _step;
        private set
        {
            _step = value;
            needsFinish.Clear();

            switch (_step)
            {
                case TutorialSteps.Shake1:
                case TutorialSteps.Throw:
                case TutorialSteps.Shake2:
                    needsFinish.Add(FindObjectOfType<Shaker>().currentShakerIndex + 1);
                    break;
                default:
                    needsFinish.Add(1);
                    needsFinish.Add(2);
                    break;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Step = TutorialSteps.Pickup;
    }

    public void FinishStep(TutorialSteps step, int playerIndex)
    {
        if (step != Step) return;
        needsFinish.Remove(playerIndex);
        if (needsFinish.Count == 0) Step++;
    }
}

public enum TutorialSteps
{
    Pickup, // Pickup an item from the ground
    PickupMore, // After picking up one item, pick up another different item
    Shake1, // Shake for the player who is holding the shaker
    Throw, // Throw the shaker to the other player
    Shake2, // Shake for the player who is holding the shaker
    Serve, // Serve the drink
    ServeMore, // Serve another drink
    End // End of tutorial
}