using System;
using UnityEngine.AI;

public class EnemyLaughState : EnemyState
{
    public override StealthState StealthState => StealthState.Idle;
    public override bool CanSee => false;

    public override void Begin()
    {
        // TODO: Play laugh animation
    }

    public override void End()
    {

    }

    public override void Update()
    {

    }
}
