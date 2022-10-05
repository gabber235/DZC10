using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Player))]
public class Interactor : MonoBehaviour {

    [SerializeField] private Transform _interactingPos;
    [SerializeField] private float _range;
    
    // Coop stuff
    [SerializeField] private InputActionReference _attackActionReference;
    
    
    public Interactable Interactable { get; private set; }
    
    public Color interactableColor = Color.green;


    private void Start() {
        _attackActionReference.action.Enable();

        _attackActionReference.action.performed += OnInteract;
    }

    private void Update()
    {
        // Set the interactable to the closest one. If there is none within the radius, set it to null.
        var maxInteractables = 10;
        var hitColliders = new Collider[maxInteractables];
        var numColliders = Physics.OverlapSphereNonAlloc(_interactingPos.transform.position, _range, hitColliders);
        Interactable = hitColliders
            .Take(numColliders)
            .Select(c => c.GetComponent<Interactable>())
            .Where(i => i != null)
            .OrderBy(i => Vector3
                .Distance(_interactingPos.transform.position, i.transform.position)).FirstOrDefault();
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        // TODO Trigger interactable
        var interactable = Interactable;
        if (interactable == null) return;
        if (interactable.IsDestroyed()) return;
        var trigger = interactable.GetComponent<IInteractingTrigger>();
        trigger?.OnInteract(this);
    }
}
