using UnityEngine;

public class EnemySleepState : EnemyState
{
    public override StealthState StealthState => StealthState.Idle;
    public override bool CanSee => false;

    float downedDuration = 0.0f;

    public override void Begin()
    {
        downedDuration = 10.0f;
        Enemy.SleepParticles.Play();
        Enemy.Agent.isStopped = true;
    }

    public override void End()
    {
        Enemy.SleepParticles.Stop();
        Enemy.Agent.isStopped = false;

    }

    public override void Update()
    {
        if (downedDuration > 0.0f)
            downedDuration -= Time.deltaTime;
        else if (Enemy.InGas)
            SetState<EnemyLaughState>();
        else
            SetState<EnemyIdleState>();
    }
}
