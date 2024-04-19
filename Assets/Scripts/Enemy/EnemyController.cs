using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _nma;

    private const float WanderRadius = 100f;
    private const float MinDistanceFromLastPoint = 15f;
    private const float MinDistanceFromCurrentPosition = 15f;
    private const float YPos = 0.5f;

    private float _newDestinationTimer;
    private float _timer;
    private Vector3 _previousDestination;

    private EnemyStateHandler _esh;
    private GameObject _player;
    private Canvas _healthCanvas;

    private void Awake()
    {
        _esh = GetComponent<EnemyStateHandler>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _healthCanvas = GetComponentInChildren<Canvas>();
    }
    
    private void InitializeMovement()
    {
        _newDestinationTimer = Random.Range(8, 13);
        _timer = _newDestinationTimer;
        _previousDestination = transform.position;
    }

    private void SetSpeed(EnemyStateHandler.EnemyState currentState)
    {
        switch (currentState)
        {
            case EnemyStateHandler.EnemyState.Chasing:
                _nma.speed = 1.25f;
                _nma.angularSpeed = 160f;
                break;
            case EnemyStateHandler.EnemyState.Wandering:
                _nma.speed = 1f;
                _nma.angularSpeed = 100f;
                break;
        }
    }
    
    private void Start()
    {
        InitializeMovement();
        _healthCanvas.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void Update()
    {
        if (_esh.CurrentState == EnemyStateHandler.EnemyState.Wandering)
        {
            UpdateDestination();
        }

        if (_esh.CurrentState == EnemyStateHandler.EnemyState.Chasing)
        {
            _nma.SetDestination(_player.transform.position);
        }
        
        SetSpeed(_esh.CurrentState);
        UpdateHealthBarPosition();
    }

    private void UpdateHealthBarPosition()
    {
        var offset = new Vector3(0, 2.5f, 0);
        _healthCanvas.transform.position = transform.position + offset;
    }

    private void UpdateDestination()
    {
        _timer += Time.deltaTime;

        if (_timer >= _newDestinationTimer)
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        var newDestination = GetNewDestination();
        _nma.SetDestination(newDestination);
        _previousDestination = newDestination;
        _timer = 0;
    }

    private Vector3 GetNewDestination()
    {
        Vector3 randomDestination;

        do
        {
            randomDestination = GenerateRandomNavSphere(transform.position, WanderRadius, -1);
        } while (!IsValidDestination(randomDestination));

        return randomDestination;
    }
    
    private bool IsValidDestination(Vector3 destination)
    {
        var distanceFromPreviousDestination = Vector3.Distance(destination, _previousDestination);
        var distanceFromAgent = Vector3.Distance(destination, transform.position);

        return distanceFromPreviousDestination >= MinDistanceFromLastPoint &&
               distanceFromAgent >= MinDistanceFromCurrentPosition;
    }

    private Vector3 GenerateRandomNavSphere(Vector3 origin, float distance, int mask)
    {
        var randomDirection = Random.insideUnitSphere * distance;
        // Needed for not skewing distance calculations
        randomDirection.y = YPos;
        randomDirection += origin;
        NavMesh.SamplePosition(randomDirection, out var navHit, distance, mask);
        return navHit.position;
    }
}
