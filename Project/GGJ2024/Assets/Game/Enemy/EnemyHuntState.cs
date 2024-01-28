public class EnemyHuntState : EnemyState
{
    public override StealthState StealthState => StealthState.Alert;

    public override void Begin()
    {
        GuardEnemy guard = GetEnemy<GuardEnemy>();
        Enemy.Animator.SetFloat("Aggro", 1);
        if (guard == null)
        {
            SetState<EnemyReturnToRoutineState>();
        }
    }

    public override void End()
    {

    }

    public override void Update()
    {
        if (GetEnemy<GuardEnemy>().CanSeePlayer())
        {
            if (Blackboard.Player != null)
                Blackboard.LastKnownLocation = Blackboard.Player.transform.position;
            Enemy.Agent.SetDestination(Blackboard.LastKnownLocation);
        }
        else
        {
            SetState<EnemySearchState>();
        }
    }
}
