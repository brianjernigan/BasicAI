//////////////////////////////////////////////
//Assignment/Lab/Project: BasicAI
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/22/2024
//////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;

    private readonly Vector3 _offset = new(0f, 10f, -1f);

    private void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        transform.position = _playerPosition.position + _offset;
        transform.rotation = Quaternion.Euler(75, 0, 0);
    }
}
