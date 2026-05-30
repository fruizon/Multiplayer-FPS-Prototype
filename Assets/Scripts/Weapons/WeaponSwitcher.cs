using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;

    private int _currentWeaponIndex = -1;
    private Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;

    void Start()
    {
        SwitchWeapon(1);
    }

    void Update()
    {
        if (currentWeapon != null && !currentWeapon.CanSwitch())
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(1);
    }

    void SwitchWeapon(int index)
    {
        if (index == _currentWeaponIndex)
            return;

        if (currentWeapon != null && !currentWeapon.CanSwitch())
            return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }

        _currentWeaponIndex = index;
        currentWeapon = weapons[index].GetComponent<Weapon>();
    }
}