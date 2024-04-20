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

public class EnemyStateHandler : MonoBehaviour
{
    public enum EnemyState
    {
        Wandering,
        Chasing
    }

    public EnemyState CurrentState { get; private set; } = EnemyState.Wandering;

    public void ChangeState(EnemyState newState)
    {
        if (newState == CurrentState) return;
        CurrentState = newState;
    }
}
