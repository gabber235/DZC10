using System.Collections.Generic;
using UnityEngine;

public class SprinklerSwitch : MonoBehaviour, IInteractionCondition, IInteractingTrigger
{
    private bool _isSprinklerOn;
    private Pipe _pipe;
    private List<Sprinkler> _sprinklers;

    public GameObject ElevatorObj;

    // Start is called before the first frame update
    private void Start()
    {
        _pipe = FindObjectOfType<Pipe>();
        if (_pipe == null)
            Debug.LogError("Pipe not found");

        _sprinklers = new List<Sprinkler>(FindObjectsOfType<Sprinkler>());
    }


    public void OnInteract(Interactor interactor)
    {
        if (_isSprinklerOn) return;
        if (_pipe.CocktailsLeft > 0) return;

        foreach (var sprinkler in _sprinklers) sprinkler.StartSprinkler();
        _isSprinklerOn = true;
        
        ElevatorObj.GetComponent<Animator>().Play("ElevatorOpening");
    }

    public bool CanInteract(Interactor interactor)
    {
        return !_isSprinklerOn && _pipe.CocktailsLeft == 0;
    }
}