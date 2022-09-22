using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerAttacking : MonoBehaviour {

    [SerializeField] private Transform _attackPos;
    [SerializeField] private float _range;
    
    // Coop stuff
    [SerializeField] private InputActionReference _attackActionReference;

    private Vector3 test;
    // Start is called before the first frame update
    void Start() {
        _attackActionReference.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackActionReference.action.WasPressedThisFrame()) {
            Collider[] enemyCols = Physics.OverlapSphere(_attackPos.position, _range);

            foreach (var col in enemyCols) {
                if (col.CompareTag("Enemy")) {
                    Debug.Log(col);
                }
            }
        }
    }
}
