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

    private const float MoveSpeed = 2.5f;
    private const float RotateSpeed = 10f;
    
    private bool IsWalking { get; set; }

    private void Awake()
    {
        Time.timeScale = 1f;
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

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
