using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelRoot : MonoBehaviour
{
    [field:SerializeField]
    public int CoinCollectionGoal { get; private set; }

    public int CollectedCoins { get; private set; }

    public UnityEvent OnCoinCollectionGoalReached;

    public List<EnemyBase> Enemies { get; } = new List<EnemyBase>();

    public void AddEnemy(EnemyBase enemyBase)
    {
        if (!Enemies.Contains(enemyBase))
            Enemies.Add(enemyBase);
    }

    public void RemoveEnemy(EnemyBase enemyBase)
    {
        Enemies.Remove(enemyBase);
    }

    public PlayerController Player { get; set; }

    public void AddCoins(int count = 1)
    {
        CollectedCoins += count;
        if (CoinCollectionGoal <= CollectedCoins)
            OnCoinCollectionGoalReached.Invoke();
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
}

public class LevelObject : MonoBehaviour
{
    public LevelRoot Level { get; private set; }

    protected virtual void Start()
    {
        Level = GameManager.Instance.CurrentLevel;
    }
}
