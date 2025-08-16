using UnityEngine;

[CreateAssetMenu(fileName = "weaponso", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject weaponPrefab;        // Weapon model prefab

    [Tooltip("Damage dealt per shot.")]
    public int damage = 1;

    [Tooltip("Time between shots in seconds.")]
    public float firerate = 0.5f;

    [Tooltip("Reference to the hit effect prefab.")]
    public GameObject hitfx;

    [Tooltip("Is the weapon automatic?")]
    public bool IsAutomatic = false;

    public bool CanZoom = false;

    public float ZoomAmount = 10f;

    public float zoomrotatespeed = .5f;

    public int magsize = 10;
}
