using UnityEngine;

public class EnemyReturnToRoutineState : EnemyState
{
    public override StealthState StealthState => StealthState.Idle;

    Vector3 target;

    public override void Begin()
    {
        target = Enemy.StartPosition;

        GuardEnemy guard = GetEnemy<GuardEnemy>();
        if (guard != null)
        {
            if (guard.Path.Count > 0)
            {
                Vector3 guardPosition = guard.transform.position;

                float shortestDistance = float.MaxValue;
                for (int i = 0; i < guard.Path.Count; i++)
                {
                    float distance = Vector3.Distance(guardPosition, guard.Path[i].position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        target = guard.Path[i].position;
                    }
                }
            }
        }

        Enemy.Agent.SetDestination(target);
    }

    public override void End()
    {

    }

    public override void Update()
    {
        if (Vector3.Distance(Enemy.PositionOnGround, target) < 1.0f)
        {
            GuardEnemy guard = GetEnemy<GuardEnemy>();
            if (guard != null)
            {
                if (guard.Path.Count > 0)
                {
                    SetState<EnemyPatrolState>();
                }
            }

            SetState<EnemyIdleState>();
        }
    }
}
