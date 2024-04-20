using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private int Health { get; set; } = 6;
    
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private Canvas _healthCanvas;

    private void Awake()
    {
        _healthCanvas = GetComponentInChildren<Canvas>();
        _healthCanvas.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
        UpdateHealthBarPosition();
    }
    
    private void UpdateHealthBarPosition()
    {
        var offset = new Vector3(0, 2.1f, 0);
        _healthCanvas.transform.position = transform.position + offset;
    }
    
    public void TakeDamage()
    {
        Health = Mathf.Max(--Health, 0);
        _healthBar.fillAmount = Health * .167f;
        
        if (Health > 0) return;
        
        gameObject.SetActive(false);
        CheckForWin();
    }

    private void CheckForWin()
    {
        var numActiveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numActiveEnemies != 0) return;
        _winPanel.SetActive(true);
        _gamePanel.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
