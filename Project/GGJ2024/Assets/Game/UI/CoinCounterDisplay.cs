using TMPro;
using UnityEngine;

public class CoinCounterDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private int requiredCount;
    private int ownedCount;

    [SerializeField]
    Color defaultColor = Color.red;
    [SerializeField]
    Color reachedColor = Color.green;


    public void SetOwnedCount(int count)
    {
        ownedCount = count;
        RefreshText();
    }

    public void SetRequiredCount(int count)
    {
        requiredCount = count;
        RefreshText();
    }

    private void RefreshText()
    {
        text.SetText($"{ownedCount:000}/{requiredCount:000}");
        text.color = ownedCount < requiredCount ? defaultColor : reachedColor;
    }
}
