using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    public abstract StealthState StealthState { get; }

    public EnemyBase Enemy { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    public Blackboard Blackboard => StateMachine.Blackboard;

    public virtual bool CanSee => true;

    public TEnemy GetEnemy<TEnemy>() where TEnemy : EnemyBase
    {
        return Enemy as TEnemy;
    }

    public void InitializeState(EnemyStateMachine stateMachine, EnemyBase enemy)
    {
        Enemy = enemy;
        StateMachine = stateMachine;
    }

    public void SetState<TEnemyState>() where TEnemyState : EnemyState
    {
        StateMachine.SetState<TEnemyState>();
    }

    public abstract void Begin();
    public abstract void Update();
    public abstract void End();
}

public enum StealthState
{
    Idle,
    Searching,
    Alert
}
