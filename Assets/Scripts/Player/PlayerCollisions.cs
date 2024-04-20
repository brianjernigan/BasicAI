using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private GameObject _lossPanel;
    [SerializeField] private GameObject _gamePanel;
    
    private const float DamageInterval = 1.5f;
    private float _damageTimer;
    
    private int Health { get; set; } = 4;
    
    private void ResetDamageTimer()
    {
        _damageTimer = DamageInterval;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        TakeDamage();
        ResetDamageTimer();
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        if (_damageTimer > 0)
        {
            _damageTimer -= Time.deltaTime;
        }
        else
        {
            TakeDamage();
            ResetDamageTimer();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        _damageTimer = 0;
    }

    private void TakeDamage()
    {
        Health = Mathf.Max(--Health, 0);
        UpdateHealthText(Health);
        
        if (Health > 0) return;
        
        _lossPanel.SetActive(true);
        _gamePanel.SetActive(false);
        Time.timeScale = 0;
    }
    
    private void UpdateHealthText(int health)
    {
        _healthText.text = $"Health: {health}";
    }
}
