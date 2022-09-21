using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour {

    [SerializeField] private Transform _attackPos;
    [SerializeField] private float _range;

    private Vector3 test;
    // Start is called before the first frame update
    void Start() {
        var test = _attackPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Collider[] enemyCols = Physics.OverlapSphere(_attackPos.position, _range);

            foreach (var col in enemyCols) {
                if (col.CompareTag("Enemy")) {
                    Debug.Log(col);
                }
            }
        }
    }
}
