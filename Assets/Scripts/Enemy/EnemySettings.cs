using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public float wanderSpeed = 1f;
    public float wanderAcceleration = 8f;
    public float wanderAngularSpeed = 100f;
    public float chaseSpeed = 2f;
    public float chaseAcceleration = 12f;
    public float chaseAngularSpeed = 200;
}
