using System;
using TMPro;
using UnityEngine;

public class AmmunitionWeaponUI : WeaponUI<PlayerAmmunitionWeapon>
{
    [SerializeField]
    private TextMeshProUGUI CountText;
    private int shownCount;

    protected override void InitializeWeapon()
    {
        shownCount = -1;
    }

    private void Update()
    {
        if (Weapon != null)
        {
            SetCount(Weapon.Ammunition);
        }
    }

    private void SetCount(int count)
    {
        if (shownCount != count)
        {
            shownCount = count;
            CountText.SetText(shownCount.ToString());
        }
    }
}
