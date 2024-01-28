using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class GuardEnemy : EnemyBase
{
    [field:SerializeField]
    public List<Transform> Path { get; private set; } = new List<Transform>();

    [field:SerializeField]
    bool debugDrawPath = false;

    public GameObject deathTrigger;

    private void Awake()
    {
        StateMachine.AddState<EnemyIdleState>();
        StateMachine.AddState<EnemyLaughState>();
        StateMachine.AddState<EnemyHuntState>();
        if (Path.Count > 0)
            StateMachine.AddState<EnemyPatrolState>();
        StateMachine.AddState<EnemyReturnToRoutineState>();
        StateMachine.AddState<EnemySearchState>();
        StateMachine.AddState<EnemySleepState>();
        StateMachine.AddState<EnemyInvestigateAttractionState>();
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

    private void OnDrawGizmos()
    {
        if (debugDrawPath)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < Path.Count; i++)
            {
                int nextIndex = (i + 1) == Path.Count ? 0 : i + 1;
                if (Path[i] != null || Path[nextIndex] != null)
                {
                    Gizmos.DrawLine(Path[i].position, Path[nextIndex].position);
                }
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attractionRange);
    }
}
