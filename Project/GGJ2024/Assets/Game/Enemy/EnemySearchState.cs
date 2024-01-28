using UnityEngine;

public class EnemySearchState : EnemyState
{
    public override StealthState StealthState => StealthState.Searching;

    bool hasReachedLastKnownLocation = false;
    float searchTime = 2.0f;
    Vector3 targetLocation;

    public override void Begin()
    {
        targetLocation = Blackboard.LastKnownLocation;
        Enemy.Agent.SetDestination(targetLocation);
        hasReachedLastKnownLocation = false;
        searchTime = 2.0f;
    }

    public override void End()
    {
        Enemy.Animator.SetFloat("Aggro", 0);
        Enemy.Agent.updateRotation = true;
    }

    public override void Update()
    {
        if (!hasReachedLastKnownLocation)
        {
            float distanceToTarget = Vector3.Distance(targetLocation, Enemy.PositionOnGround);
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
