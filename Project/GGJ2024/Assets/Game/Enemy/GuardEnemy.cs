using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class GuardEnemy : EnemyBase
{
    [field:SerializeField]
    public List<Transform> Path { get; private set; } = new List<Transform>();

    private void Awake()
    {
        StateMachine.AddState<EnemyIdleState>();
        StateMachine.AddState<EnemyLaughState>();
        StateMachine.AddState<EnemyHuntState>();
        if (Path.Count > 0)
            StateMachine.AddState<EnemyPatrolState>();
        StateMachine.AddState<EnemyReturnToRoutineState>();
        StateMachine.AddState<EnemySearchState>();
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Begin<EnemyIdleState>(this);
    }

    protected override void Update()
    {
        base.Update();

        if (StateMachine.MaySeePlayer() && CanSeePlayer())
        {
            StateMachine.SetState<EnemyHuntState>();
        }
    }
}
