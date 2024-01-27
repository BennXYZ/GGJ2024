using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public override StealthState StealthState => StealthState.Idle;

    int targetIndex = -1;

    public override void Begin()
    {
        GuardEnemy guard = GetEnemy<GuardEnemy>();
        if (guard != null)
        {
            if (guard.Path.Count == 0)
            {
                SetState<EnemyReturnToRoutineState>();
            }
            else
            {
                Vector3 guardPosition = guard.transform.position;

                float shortestDistance = float.MaxValue;
                for (int i = 0; i < guard.Path.Count; i++)
                {
                    float distance = Vector3.Distance(guardPosition, guard.Path[i].position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        targetIndex = i;
                    }
                }
                Enemy.Agent.SetDestination(guard.Path[targetIndex].position);
            }
        }
    }

    public override void End()
    {

    }

    public override void Update()
    {
        GuardEnemy guard = GetEnemy<GuardEnemy>();
        if (guard != null)
        {
            float distance = Vector3.Distance(guard.Path[targetIndex].position, Enemy.PositionOnGround);
            if (distance < 1.0f)
            {
                ++targetIndex;
                if (guard.Path.Count <= targetIndex)
                {
                    targetIndex = 0;
                }
                Enemy.Agent.SetDestination(guard.Path[targetIndex].position);
            }
        }
    }
}
