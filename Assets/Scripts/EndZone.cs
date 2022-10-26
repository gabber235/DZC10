using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndZone : MonoBehaviour {

    [SerializeField] private int _playersPresent = 0;

    [SerializeField] private LevelManager _manager;
    [SerializeField] private Animator FadeToNext;
    
    // Start is called before the first frame update
    void Start() {
        _manager = FindObjectOfType<LevelManager>();

        this.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
    }

    public void OnTriggerEnter(Collider other) {
        _playersPresent++;
        
        if (_playersPresent == 2) {
            // Could have an end-screen here instead.
            FadeToNext.SetTrigger("FadeOut");
        }
    }

    public void OnTriggerExit(Collider other) {
        _playersPresent--;
    }
}
