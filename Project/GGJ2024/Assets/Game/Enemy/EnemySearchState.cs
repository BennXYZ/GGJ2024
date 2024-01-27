using UnityEngine;

public class EnemySearchState : EnemyState
{
    public override StealthState StealthState => StealthState.Searching;

    bool hasReachedLastKnownLocation = false;
    float searchTime = 2.0f;

    public override void Begin()
    {
        Enemy.Agent.SetDestination(Blackboard.LastKnownLocation);
        hasReachedLastKnownLocation = false;
        searchTime = 2.0f;
    }

    public override void End()
    {
        Enemy.Agent.updateRotation = true;
    }

    public override void Update()
    {
        if (!hasReachedLastKnownLocation)
        {
            float distanceToTarget = Vector3.Distance(Blackboard.LastKnownLocation, Enemy.PositionOnGround);
            if (distanceToTarget < 1.0f)
                hasReachedLastKnownLocation = true;
        }
        else
        {
            if (searchTime > 0)
            {
                searchTime -= Time.deltaTime;
                Enemy.Agent.updateRotation = false;
                Enemy.transform.Rotate(Vector3.up, 180 * Time.deltaTime, Space.Self);
            }
            else
                SetState<EnemyReturnToRoutineState>();
        }
    }
}
