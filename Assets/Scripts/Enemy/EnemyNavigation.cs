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
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class EnemyNavigation : MonoBehaviour
{
    private NavMeshAgent _nma;

    private const float WanderRadius = 100f;
    private const float MinDistanceFromLastPoint = 15f;
    private const float MinDistanceFromCurrentPosition = 15f;
    private const float YPos = 0.5f;

    private float _newDestinationTimer;
    private float _timer;
    private Vector3 _previousDestination;

    private EnemyStateHandler _esh;
    
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        _nma = GetComponent<NavMeshAgent>();
        _esh = GetComponent<EnemyStateHandler>();
    }
    
    private void InitializeMovement()
    {
        _newDestinationTimer = Random.Range(6, 10);
        _timer = _newDestinationTimer;
        _previousDestination = transform.position;
    }
    
    private void Start()
    {
        InitializeMovement();
    }

    private void Update()
    {
        if (_esh.CurrentState == EnemyStateHandler.EnemyState.Wandering)
        {
            Wander();
        }

        if (_esh.CurrentState == EnemyStateHandler.EnemyState.Chasing)
        {
            ChasePlayer();
        }
    }

    private void Wander()
    {
        UpdateWanderDestination();
    }

    private void ChasePlayer()
    {
        _nma.SetDestination(_player.transform.position);
    }

    private void UpdateWanderDestination()
    {
        _timer += Time.deltaTime;

        if (_timer >= _newDestinationTimer)
        {
            SetNewWanderDestination();
        }
    }

    private void SetNewWanderDestination()
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
