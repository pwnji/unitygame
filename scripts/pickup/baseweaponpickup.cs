using UnityEngine;

public class BaseWeaponPickup : pickup
{
    [SerializeField] private WeaponData weaponData;

    const string PlayerTag = "Player";
    protected override void OnPickup(WeaponController weaponController)
    { 
        weaponController.SwitchWeapon(weaponData);
    }
}
