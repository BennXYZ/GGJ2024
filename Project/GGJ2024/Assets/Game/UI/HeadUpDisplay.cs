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

    [field: SerializeField]
    public WeaponContainer WeaponContainer { get; private set; }

    [SerializeField]
    GameObject deathUI;

    private void Start()
    {
        LevelRoot levelRoot = GameManager.Instance.CurrentLevel;
        levelRoot.OnStealthStateChanged.AddListener(StealthStateChanged);
        levelRoot.OnCoinsCollected.AddListener(CoinsCollected);
        levelRoot.OnCoinCollectionGoalReached.AddListener(GoalReached);
        levelRoot.OnPlayerSet.AddListener(PlayerSet);
        levelRoot.OnPlayerDied.AddListener(PlayerDied);
        CoinCounterDisplay.SetRequiredCount(levelRoot.CoinCollectionGoal);
        CoinCounterDisplay.SetOwnedCount(levelRoot.CollectedCoins);
        StealthStateDisplay.SetStealthState(StealthState.Idle);

        if (levelRoot.Player != null)
            PlayerSet();
    }

    public void PlayerDied()
    {
        if(!deathUI)
        {
            Debug.LogError("DeathUI not attached");
            return;
        }
        deathUI.SetActive(true);
    }

    private void PlayerSet()
    {
        WeaponContainer.RefreshWeapons();
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
