using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    
    // Start is called before the first frame update
    void Start() {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float deltaHealth) {
        _currentHealth -= deltaHealth;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth == 0) {
            EnemyDeath();
        }
    }

    public void EnemyDeath() {
        // this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
