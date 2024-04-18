using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _nma;

    private float _wanderRadius = 100f;
    private float _wanderTimer = 5f;
    private float _minDistanceFromLastPoint = 20f;
    private float _minDistanceFromCurrentPosition = 20f;
    private float _timer;

    private Vector3 _previousDestination;

    private void Start()
    {
        _timer = _wanderTimer;
        _previousDestination = transform.position;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _wanderTimer)
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
        var nextDestination = Vector3.zero;
        var isValidDestination = false;

        while (!isValidDestination)
        {
            var randomDestination = GenerateRandomNavSphere(transform.position, _wanderRadius, -1);
            var distanceFromPrevious = Vector3.Distance(randomDestination, _previousDestination);
            var distanceFromAgent = Vector3.Distance(randomDestination, _nma.transform.position);

           if (distanceFromPrevious >=  _minDistanceFromLastPoint && distanceFromAgent >= _minDistanceFromCurrentPosition)
            {
                nextDestination = randomDestination;
                isValidDestination = true;
            }
        }

        return nextDestination;
    }

    private Vector3 GenerateRandomNavSphere(Vector3 origin, float distance, int mask)
    {
        var randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, mask);
        return navHit.position;
    }
}
