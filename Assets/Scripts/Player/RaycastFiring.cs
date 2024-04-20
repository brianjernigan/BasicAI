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

public class RaycastFiring : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    
    private EnemyStateHandler _esh;
    private EnemyHealth _eh;
    private LineRenderer _lr;
    
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit) || !hit.collider.CompareTag("Enemy")) return;
        _esh = hit.collider.GetComponent<EnemyStateHandler>();
        _eh = hit.collider.GetComponent<EnemyHealth>();
        _lr = hit.collider.GetComponent<LineRenderer>();
        _eh.TakeDamage();
        _esh.ChangeState(EnemyStateHandler.EnemyState.Chasing);
    }
}
