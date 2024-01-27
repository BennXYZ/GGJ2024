using UnityEngine;

class HeadUpDisplay : MonoBehaviour
{
    [field:SerializeField]
    public StealthStateDisplay StealthStateDisplay { get; private set; }

    private void Start()
    {
        GameManager.Instance.CurrentLevel.OnStealthStateChanged.AddListener(StealthStateChanged);
        StealthStateDisplay.SetStealthState(StealthState.Idle);
    }

    private void StealthStateChanged()
    {
        StealthStateDisplay.SetStealthState(GameManager.Instance.CurrentLevel.CurrentStealthState);
    }
}
