using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControler : MonoBehaviour
{
    private float _maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private Image _healthBarFill;
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        if (_currentHealth == 0) { }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
