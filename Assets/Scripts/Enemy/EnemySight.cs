using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private const float SightAngle = 20f;
    private const float SightDistance = 7.5f;
    
    private LineRenderer _lr;
    private GameObject _player;
    private EnemyStateHandler _esh;
    private EnemyController _ec;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _esh = GetComponent<EnemyStateHandler>();
        _lr = GetComponent<LineRenderer>();
        _ec = GetComponent<EnemyController>();
        _lr.positionCount = 3;
    }

    private void Update()
    {
        UpdateLineOfSightRender();
        if (!IsPlayerInSight()) return;
        _esh.ChangeState(EnemyStateHandler.EnemyState.Chasing);
        _ec.SetSpeed(_esh.CurrentState);
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
