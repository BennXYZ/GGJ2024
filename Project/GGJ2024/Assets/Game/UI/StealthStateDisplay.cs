using UnityEngine;
using UnityEngine.UI;

public class StealthStateDisplay : MonoBehaviour
{
    [SerializeField]
    private Image display;

    [Header("Icons")]
    [SerializeField]
    private Sprite idle;
    [SerializeField]
    private Sprite searching;
    [SerializeField]
    private Sprite alert;

    public void SetStealthState(StealthState stealthState)
    {
        switch (stealthState)
        {
            case StealthState.Idle:
                display.sprite = idle;
                break;
            case StealthState.Searching:
                display.sprite = searching;
                break;
            case StealthState.Alert:
                display.sprite = alert;
                break;
        }
    }
}
