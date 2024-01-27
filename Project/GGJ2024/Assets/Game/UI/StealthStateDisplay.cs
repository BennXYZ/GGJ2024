using UnityEngine;
using UnityEngine.UI;

public class StealthStateDisplay : MonoBehaviour
{
    [SerializeField]
    private Image display;

    [Header("Colors")]
    [SerializeField]
    private Color idle;
    [SerializeField]
    private Color searching;
    [SerializeField]
    private Color alert;

    public void SetStealthState(StealthState stealthState)
    {
        switch (stealthState)
        {
            case StealthState.Idle:
                display.color = idle;
                break;
            case StealthState.Searching:
                display.color = searching;
                break;
            case StealthState.Alert:
                display.color = alert;
                break;
        }
    }
}
