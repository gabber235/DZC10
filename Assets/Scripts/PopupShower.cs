using Tutorial;
using UnityEngine;

public class PopupShower : MonoBehaviour
{
    public TutorialStep tutorialStep;
    private TutorialManager _tutorialManager;

    private void Start()
    {
        _tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void Update()
    {
        if (_tutorialManager.Step == tutorialStep && !gameObject.activeSelf)
            gameObject.SetActive(true);

        if (_tutorialManager.Step != tutorialStep && gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}