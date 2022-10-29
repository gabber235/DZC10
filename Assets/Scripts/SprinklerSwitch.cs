using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class SprinklerSwitch : MonoBehaviour, IInteractionCondition, IInteractingTrigger
{
    public GameObject ElevatorObj;
    private bool _isSprinklerOn;
    private Pipe _pipe;
    private List<Sprinkler> _sprinklers;

    private SoundManager SM;

    // Start is called before the first frame update
    private void Start()
    {
        _pipe = FindObjectOfType<Pipe>();
        if (_pipe == null)
            Debug.LogError("Pipe not found");

        _sprinklers = new List<Sprinkler>(FindObjectsOfType<Sprinkler>());
        SM = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }


    public void OnInteract(Interactor interactor)
    {
        if (_isSprinklerOn) return;
        if (_pipe.CocktailsLeft > 0)
        {
            SM.playSoundEffect(2);
            return;
        }

        foreach (var sprinkler in _sprinklers) sprinkler.StartSprinkler();
        _isSprinklerOn = true;

        foreach (var enemy in FindObjectsOfType<EnemyController>()) enemy.Dance();

        ElevatorObj.GetComponent<Animator>().Play("ElevatorOpening");
        SM.playSoundEffect(7);
        SM.playSoundEffect(22);
        SM.playSoundEffect(23);
    }

    public bool CanInteract(Interactor interactor)
    {
        return !_isSprinklerOn && _pipe.CocktailsLeft == 0;
    }
}