//////////////////////////////////////////////
//Assignment/Lab/Project: BasicAI
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/22/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private GameObject _lossPanel;
    [SerializeField] private GameObject _gamePanel;

    private const float MoveSpeed = 2.5f;
    private const float RotateSpeed = 10f;

    private const float DamageInterval = 2f;
    private float _damageTimer;
    
    private bool IsWalking { get; set; }
    private int Health { get; set; } = 4;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    
    private void ResetDamageTimer()
    {
        _damageTimer = DamageInterval;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;

        _rb.velocity = movement * MoveSpeed;

        if (movement.magnitude > 0.1f)
        {
            IsWalking = true;
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, Quaternion.LookRotation(movement), RotateSpeed * Time.fixedDeltaTime));
        }
        else
        {
            IsWalking = false;
        }

        _anim.SetBool("IsWalking", IsWalking);
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

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
