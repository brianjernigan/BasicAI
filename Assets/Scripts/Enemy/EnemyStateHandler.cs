//////////////////////////////////////////////
//Assignment/Lab/Project: BasicAI
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/22/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateHandler : MonoBehaviour
{
    private NavMeshAgent _nma;
    private LineRenderer _lr;

    [SerializeField] private Material _spottedMat;
    [SerializeField] private EnemySettings _defaultSettings;
    
    public enum EnemyState
    {
        Wandering,
        Chasing
    }

    public EnemyState CurrentState { get; private set; } = EnemyState.Wandering;

    private void Awake()
    {
        _nma = GetComponent<NavMeshAgent>();
        _lr = GetComponent<LineRenderer>();
    }
    
    public void ChangeState(EnemyState newState)
    {
        if (newState == CurrentState) return;
        CurrentState = newState;
        OnStateChanged(CurrentState);
    }

    private void OnStateChanged(EnemyState currentState)
    {
        // Should never change back to wandering
        if (currentState != EnemyState.Chasing) return;
        ChangeSightConeColor();
        SetChaseSpeed();
    }

    private void ChangeSightConeColor()
    {
        _lr.material = _spottedMat;
    }

    private void SetChaseSpeed()
    {
        _nma.acceleration = _defaultSettings.chaseAcceleration;
        _nma.angularSpeed = _defaultSettings.chaseAngularSpeed;
        _nma.speed = _defaultSettings.chaseSpeed;
    }
}
