using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [Serializable]
    public class StepInfoDictionary : SerializableDictionary<TutorialStep, TutorialStepInfo>
    {
    }

    public class TutorialManager : MonoBehaviour
    {
        // The players that still need to finish the step
        public List<int> needsFinish = new();

        public StepInfoDictionary steps = new();

        private readonly Dictionary<TutorialStep, List<int>> _alreadyFinished = new();

        private TutorialStep _step;
        private SoundManager _sm = null;

        // Current step of the tutorial
        public TutorialStep Step
        {
            get => _step;
            private set
            {
                _step = value;
                needsFinish.Clear();
                if (_sm != null){
                    _sm.playSoundEffect(26);
                }
                
                switch (_step)
                {
                    case TutorialStep.Shake1:
                    case TutorialStep.Throw:
                    case TutorialStep.ServeMore:
                        needsFinish.Add(1);
                        break;
                    case TutorialStep.Shake2:
                        needsFinish.Add(2);
                        break;
                    default:
                        for (var i = 1; i <= 2; i++)
                            if (!(_alreadyFinished.TryGetValue(_step, out var list) && list.Contains(i)))
                                needsFinish.Add(i);
                        break;
                }

                OnTutorialStepChanged?.Invoke(_step);
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            Step = TutorialStep.Pickup;
            _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
        }

        public void FinishStep(TutorialStep step, int playerIndex, bool preFinish = false)
        {
            if (step != Step)
            {
                if (step <= Step || !preFinish) return;
                var list = _alreadyFinished.GetValueOrDefault(step, new List<int>());
                list.Add(playerIndex);
                _alreadyFinished[step] = list;

                return;
            }

            needsFinish.Remove(playerIndex);
            if (needsFinish.Count == 0) Step++;
        }

        public void ForceStep(TutorialStep step)
        {
            Step = step;
        }

        public event Action<TutorialStep> OnTutorialStepChanged;
    }

    public enum TutorialStep
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
}