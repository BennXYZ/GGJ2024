using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLaughState : EnemyState
{
    public override StealthState StealthState => StealthState.Idle;
    public override bool CanSee => false;

    private float laughDuration = 1.0f;

    public override void Begin()
    {
        Enemy.Agent.isStopped = true;

        laughDuration = 1;
        Enemy.Animator.SetFloat("Aggro", 0);
        Enemy.Animator.SetBool("Laughing", true);
        (Enemy as GuardEnemy).deathTrigger.SetActive(false);

        Enemy.AttractPeople();
        Enemy.PlayLaughSound();

        // TODO: Play laugh animation
    }

    public override void End()
    {
        Enemy.Animator.SetBool("Laughing", false);
    }

    public override void Update()
    {
        if (laughDuration > 0.0f)
            laughDuration -= Time.deltaTime;
        else
            SetState<EnemySleepState>();
    }
}
