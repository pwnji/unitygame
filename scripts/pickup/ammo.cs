using UnityEngine;

public class ammo : pickup
{
    [SerializeField] int ammoAmount = 100; // Amount of ammo to give
    protected override void OnPickup(WeaponController weaponController)
    {
        weaponController.AmmoAmount(ammoAmount);
    }
}
