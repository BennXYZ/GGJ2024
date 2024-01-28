using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelRoot : MonoBehaviour
{
    [field: SerializeField]
    public int CoinCollectionGoal { get; private set; }

    public int CollectedCoins { get; private set; }

    public UnityEvent OnCoinCollectionGoalReached;
    public UnityEvent OnCoinsCollected;

    public StealthState CurrentStealthState { get; private set; }

    public UnityEvent OnStealthStateChanged;
    private PlayerController player;
    public UnityEvent OnPlayerSet;
    public UnityEvent OnPlayerDied;

    List<GasArea> gasAreas = new List<GasArea>();

    public void AddGasArea(GasArea gasArea)
    {
        if (!gasAreas.Contains(gasArea))
        {
            gasAreas.Add(gasArea);
        }
    }

    public List<EnemyBase> Enemies { get; } = new List<EnemyBase>();

    public void PlayerDied()
    {
        OnPlayerDied.Invoke();
    }

    public List<IGasReceiver> GasReceivers { get; } = new List<IGasReceiver>();

    public void AddEnemy(EnemyBase enemyBase)
    {
        if (!Enemies.Contains(enemyBase))
        {
            Enemies.Add(enemyBase);
            GasReceivers.Add(enemyBase);
        }
    }

    public void RemoveEnemy(EnemyBase enemyBase)
    {
        Enemies.Remove(enemyBase);
        GasReceivers.Remove(enemyBase);
    }

    public PlayerController Player
    {
        get => player;
        set
        {
            player = value;
            Blackboard.Player = value;
            OnPlayerSet.Invoke();
        }
    }

    public void RemoveGasArea(GasArea gasArea)
    {
        gasAreas.Remove(gasArea);
    }

    public Blackboard Blackboard { get; private set; } = new Blackboard();

    public void AddCoins(int count = 1)
    {
        CollectedCoins += count;
        OnCoinsCollected.Invoke();
        if (CoinCollectionGoal <= CollectedCoins)
            OnCoinCollectionGoalReached.Invoke();
    }

    private void Update()
    {
        for (int i = 0; i < gasAreas.Count; i++)
        {
            GasArea gasArea = gasAreas[i];
            List<IGasReceiver> inArea = new List<IGasReceiver>();
            for (int j = 0; j < GasReceivers.Count; j++)
            {
                IGasReceiver receiver = GasReceivers[j];
                float distance = Vector3.Distance(gasArea.transform.position, receiver.Position);
                if (distance < gasArea.Size)
                {
                    inArea.Add(receiver);
                }
            }
            gasArea.SetReceivers(inArea);
        }
    }

    private void Awake()
    {
        GameManager.Instance.CurrentLevel = this;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.CurrentLevel == this)
            GameManager.Instance.CurrentLevel = null;
    }

    public void OnEnemyStateChanged()
    {
        StealthState highestState = StealthState.Idle;

        for (int i = 0; i < Enemies.Count; i++)
        {
            StealthState state = Enemies[i].StealthState;
            if (highestState < state)
                highestState = state;
        }

        if (highestState != CurrentStealthState)
        {
            CurrentStealthState = highestState;
            OnStealthStateChanged.Invoke();
        }
    }
}

public class LevelObject : MonoBehaviour
{
    public LevelRoot Level { get; private set; }

    protected virtual void Start()
    {
        Level = GameManager.Instance.CurrentLevel;
    }
}
