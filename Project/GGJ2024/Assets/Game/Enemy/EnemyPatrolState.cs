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
            if (guard.Path.Count <= targetIndex)
            {
                targetIndex = 0;
            }

            float distance = Vector3.Distance(guard.Path[targetIndex].position, Enemy.transform.position);
            if (distance < 1.0f)
            {
                ++targetIndex;
            }
        }
    }
}
