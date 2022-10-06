using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CocktailThrowing : MonoBehaviour {

    private Transform _transform;
    private Vector3 startPos;
    private float _maxHeight;
    
    public Transform target {
        get { return _transform; }
        set {
            _transform = value;

            startPos = transform.position;
        }
    }

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            transform.position = Vector3.SmoothDamp(
                transform.position, 
                target.position, 
                ref _velocity, 
                0.1f
            );

            var distanceTotal = Vector3.Distance(target.position, startPos);
            var distanceCocktailToEnemy = Vector3.Distance(target.position, transform.position);
            var percentageTraveled = distanceTotal / distanceCocktailToEnemy;
        }
    }
}
