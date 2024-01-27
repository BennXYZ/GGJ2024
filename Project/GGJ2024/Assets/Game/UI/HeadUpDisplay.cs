using System;
using UnityEngine;

class HeadUpDisplay : MonoBehaviour
{
    [field:SerializeField]
    public StealthStateDisplay StealthStateDisplay { get; private set; }

    [field:SerializeField]
    public CoinCounterDisplay CoinCounterDisplay { get; private set; }
    [field:SerializeField]
    public GoalReachedNotify GoalReachedNotify { get; private set; }

    private void Start()
    {
        LevelRoot levelRoot = GameManager.Instance.CurrentLevel;
        levelRoot.OnStealthStateChanged.AddListener(StealthStateChanged);
        levelRoot.OnCoinsCollected.AddListener(CoinsCollected);
        levelRoot.OnCoinCollectionGoalReached.AddListener(GoalReached);
        CoinCounterDisplay.SetRequiredCount(levelRoot.CoinCollectionGoal);
        CoinCounterDisplay.SetOwnedCount(levelRoot.CollectedCoins);
        StealthStateDisplay.SetStealthState(StealthState.Idle);
    }

    private void GoalReached()
    {
        GoalReachedNotify.StartAnimation();
    }

    private void CoinsCollected()
    {
        CoinCounterDisplay.SetOwnedCount(GameManager.Instance.CurrentLevel.CollectedCoins);
    }

    private void StealthStateChanged()
    {
        StealthStateDisplay.SetStealthState(GameManager.Instance.CurrentLevel.CurrentStealthState);
    }
}
