using UnityEngine;

namespace Tutorial
{
    public class TutorialEnd : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            var tutorialManager = FindObjectOfType<TutorialManager>();
            tutorialManager.OnTutorialStepChanged += OnStepChanged;
        }

        private void OnStepChanged(TutorialStep step)
        {
            if (step == TutorialStep.End)
                // TODO: Go the the next scene
                Debug.Log("Nicely done!");
        }
    }
}