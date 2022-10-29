using UnityEngine;

namespace Tutorial
{
    public class TutorialEnd : MonoBehaviour
    {
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        [SerializeField] private Animator fadeToNext;

        // Start is called before the first frame update
        private void Start()
        {
            var tutorialManager = FindObjectOfType<TutorialManager>();
            tutorialManager.OnTutorialStepChanged += OnStepChanged;
        }

        private void OnStepChanged(TutorialStep step)
        {
            if (step == TutorialStep.End) fadeToNext.SetTrigger(FadeOut);
        }
    }
}