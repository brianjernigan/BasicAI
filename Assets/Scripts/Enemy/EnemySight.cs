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
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private const float SightAngle = 20f;
    private const float SightDistance = 7.5f;
    
    private LineRenderer _lr;
    private EnemyStateHandler _esh;

    [SerializeField] private GameObject _player;

    private void Awake()
    {
        _esh = GetComponent<EnemyStateHandler>();
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = 3;
    }

    private void Update()
    {
        UpdateLineOfSightRender();
        if (!IsPlayerInSight()) return;
        _esh.ChangeState(EnemyStateHandler.EnemyState.Chasing);
    }

    private bool IsPlayerInSight()
    {
        var directionToPlayer = _player.transform.position - transform.position;
        var angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle >= SightAngle / 2) return false;
        
        return Physics.Raycast(transform.position, directionToPlayer.normalized, out var hit, SightDistance) && hit.transform.gameObject.CompareTag("Player");
    }

    private void UpdateLineOfSightRender()
    {
        var origin = transform.position;
        origin.y = 1.5f;
        _lr.SetPosition(0, origin + CalculateOffset(-SightAngle / 2));
        _lr.SetPosition(1, origin);
        _lr.SetPosition(2, origin + CalculateOffset(SightAngle / 2));
    }

    private Vector3 CalculateOffset(float angleOffset)
    {
        var forward = transform.forward;
        var direction = Quaternion.Euler(0, angleOffset, 0) * forward * SightDistance;
        return direction;
    }
}
