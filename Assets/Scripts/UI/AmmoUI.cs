using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public WeaponSwitcher weaponSwitcher;
    public Text ammoText;

    void Update()
    {
        Weapon weapon = weaponSwitcher.CurrentWeapon;

        if (weapon == null)
        {
            ammoText.text = "";
            return;
        }

        ammoText.text = $"{weapon.currentAmmo} / {weapon.reserveAmmo}";
    }
}