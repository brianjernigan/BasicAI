using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private float _sightRange = 5f;
    private float _sightAngle = 60f;
    private int _lineSegments = 50;
    
    [SerializeField] private LineRenderer _lr;

    private void Start()
    {
        _lr.positionCount = _lineSegments + 1;
        _lr.useWorldSpace = false;
        _lr.loop = true;
        _lr.startWidth = 0.1f;
        _lr.endWidth = 0.1f;
    }

    private void Update()
    {
        var startPos = Vector3.zero;
        var angleStep = _sightAngle / _lineSegments;
        var currentAngle = -_sightAngle / 2f;

        for (int i = 0; i <= _lineSegments; i++)
        {
            var angle = currentAngle * Mathf.Deg2Rad;
            var endPos = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * _sightRange;
            _lr.SetPosition(i, endPos);
            currentAngle += angleStep;
        }
    }
}
