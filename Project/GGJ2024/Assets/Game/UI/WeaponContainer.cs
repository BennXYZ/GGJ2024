using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField]
    private AmmunitionWeaponUI ammunitionPrefab;

    [SerializeField]
    private CooldownWeaponUI cooldownPrefab;

    public void RefreshWeapons()
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        if (GameManager.Instance.CurrentLevel.Player == null)
            return;
        IReadOnlyList<PlayerWeapon> weapons = GameManager.Instance.CurrentLevel.Player.Weapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            switch (weapons[i].Type)
            {
                case PlayerWeapon.WeaponType.Ammunition:
                    AmmunitionWeaponUI ammunitionWeapon = Instantiate(ammunitionPrefab, transform);
                    ammunitionWeapon.SetWeapon(weapons[i] as PlayerAmmunitionWeapon);
                    break;

                case PlayerWeapon.WeaponType.Cooldown:
                    CooldownWeaponUI cooldownWeapon = Instantiate(cooldownPrefab, transform);
                    cooldownWeapon.SetWeapon(weapons[i] as PlayerCooldownWeapon);
                    break;
            }
        }
    }
}

public abstract class WeaponUI<TWeapon> : MonoBehaviour where TWeapon : PlayerWeapon
{
    [SerializeField]
    private Image IconImage;

    [SerializeField]
    private TextMeshProUGUI HotkeyDisplay;

    protected TWeapon Weapon { get; private set; }

    public void SetWeapon(TWeapon playerWeapon)
    {
        Weapon = playerWeapon;

        IconImage.sprite = Weapon.Sprite;

        string hotkey = Weapon.InputIdentifier.ToString();
        switch (Weapon.InputIdentifier)
        {
            case KeyCode.Mouse0:
                hotkey = "LMB";
                break;
            case KeyCode.Mouse1:
                hotkey = "RMB";
                break;
        }
        HotkeyDisplay.SetText(hotkey);

        InitializeWeapon();
    }

    protected abstract void InitializeWeapon();
}
