using System;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    public PlayerController Player { get; set; }

    public Vector3 LastKnownLocation;
}

public class EnemyStateMachine
{
    private Blackboard blackboard;

    public Blackboard Blackboard
    {
        get
        {
            if (blackboard == null)
                blackboard = GameManager.Instance.CurrentLevel.Blackboard;
            return blackboard;
        }
    }

    Dictionary<Type, EnemyState> availableStates = new Dictionary<Type, EnemyState>();
    EnemyState currentState;

    public StealthState StealthState => currentState?.StealthState ?? StealthState.Idle;

    public bool MaySeePlayer()
    {
        return currentState != null ? currentState.StealthState == StealthState.Idle && currentState.CanSee : false;
    }

    public EnemyBase Owner { get; private set; }

    public void Begin<TEnemyState>(EnemyBase owner) where TEnemyState : EnemyState
    {
        Owner = owner;

        foreach (var item in availableStates)
        {
            item.Value.InitializeState(this, owner);
        }

        SetState<TEnemyState>();
    }

    public void AddState<TEnemyState>() where TEnemyState : EnemyState, new()
    {
        if (!availableStates.ContainsKey(typeof(TEnemyState)))
        {
            availableStates.Add(typeof(TEnemyState), new TEnemyState());
        }
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void SetState<TEnemyState>() where TEnemyState : EnemyState
    {
        if (availableStates.ContainsKey(typeof(TEnemyState)))
        {
            EnemyState state = availableStates[typeof(TEnemyState)];
            if (state != currentState)
            {
                currentState?.End();
                currentState = state;
                currentState?.Begin();

                GameManager.Instance.CurrentLevel.OnEnemyStateChanged();
            }
        }
    }
}
