using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFiring : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;

    private EnemyController _ec;
    private EnemyStateHandler _esh;
    
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit) || !hit.collider.CompareTag("Enemy")) return;
        _ec = hit.collider.GetComponent<EnemyController>();
        _esh = hit.collider.GetComponent<EnemyStateHandler>();
        _ec.TakeDamage();
        _esh.ChangeState(EnemyStateHandler.EnemyState.Chasing);
    }
}
