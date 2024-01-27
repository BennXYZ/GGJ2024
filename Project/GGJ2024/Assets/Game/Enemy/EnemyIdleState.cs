public class EnemyIdleState : EnemyState
{
    public override StealthState StealthState => StealthState.Idle;

    public override void Begin()
    {
        GuardEnemy guard = GetEnemy<GuardEnemy>();
        if (guard != null && guard.Path.Count > 0)
        {
            SetState<EnemyPatrolState>();
        }
    }

    public override void End()
    {
    }

    public override void Update()
    {
    }
}
