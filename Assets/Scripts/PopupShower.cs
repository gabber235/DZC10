using UnityEngine;

public class PopupShower : MonoBehaviour
{
    public TutorialSteps tutorialStep;
    private TutorialManager tutorialManager;

    private void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void Update()
    {
        if (tutorialManager.Step == tutorialStep && !gameObject.activeSelf)
            gameObject.SetActive(true);

        if (tutorialManager.Step != tutorialStep && gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}