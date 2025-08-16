using Cinemachine;
using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponData startingweapon;
    [SerializeField] WeaponData weaponso;
    [SerializeField] CinemachineVirtualCamera playerfollowcamera;
    [SerializeField] Camera weaponcamera;
    [SerializeField] GameObject zoomvig;
    [SerializeField] TMP_Text ammoText; // Text to display ammo count, if needed
    private Transform currentMuzzlePoint;
    private Animator animator;
    private StarterAssetsInputs starterAssetsInputs;
    private WeaponShooter weaponShooter;
    FirstPersonController firstPersonController;
    

    [SerializeField] private ParticleSystem flash; // Assigned in Inspector (shared muzzle flash)
    [SerializeField] private Transform weaponHolder; // Assigned in Inspector (where models spawn)

    private const string ShotAnimation = "shoot";
    private float timeSinceLastShot = 0f;
    float defaultFOV;
    private bool canShoot = true;
    float defaultrotatespeed;
    int currentammo;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponentInChildren<Animator>(true);
        defaultFOV = playerfollowcamera.m_Lens.FieldOfView;
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultrotatespeed = firstPersonController.RotationSpeed;

    }
    private void Start()
    {
        SwitchWeapon(startingweapon);
        StartCoroutine(AssignFlashNextFrame());
    }

    private IEnumerator AssignFlashNextFrame()
    {
        yield return null; // wait one frame

        if (weaponHolder.childCount > 0 && flash != null)
        {
            Transform currentWeapon = weaponHolder.GetChild(0);
            currentMuzzlePoint = currentWeapon.Find("MuzzlePoint");

            if (currentMuzzlePoint != null)
            {
                flash.transform.SetParent(currentMuzzlePoint, false);
                flash.transform.localPosition = Vector3.zero;
                flash.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogWarning("MuzzlePoint not found on initial weapon.");
            }
        }
    }


    void Update()
    {
        
        HandleShot();
        Handlezoom();
    }
    public void AmmoAmount(int amountChange)
    {
        currentammo = Mathf.Min(currentammo + amountChange, weaponso.magsize);
        ammoText.text = currentammo.ToString("D2");
    }


    public void SwitchWeapon(WeaponData newWeaponData)
    {
        // Destroy the old weapon mesh
        foreach (Transform child in weaponHolder)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the new weapon prefab
        GameObject newWeaponObj = Instantiate(newWeaponData.weaponPrefab, weaponHolder);

        // Update weapon data and shooter
        weaponso = newWeaponData;
        weaponShooter = newWeaponObj.GetComponentInChildren<WeaponShooter>(true);

        if (weaponShooter == null)
            Debug.LogWarning("WeaponShooter not found in new weapon prefab!");

        if (flash == null)
            flash = GetComponentInChildren<ParticleSystem>(true);

        if (flash == null)
            Debug.LogWarning("Muzzle flash ParticleSystem not found after weapon switch!");

        currentMuzzlePoint = newWeaponObj.transform.Find("MuzzlePoint");
        if (currentMuzzlePoint == null)
        {
            Debug.LogWarning("MuzzlePoint not found in weapon prefab!");
        }
        else
        {
            flash.transform.SetParent(currentMuzzlePoint, false);
            flash.transform.localPosition = Vector3.zero;
            flash.transform.localRotation = Quaternion.identity;
        }

        // ✅ Reset ammo for the new weapon
        currentammo = newWeaponData.magsize;
        ammoText.text = currentammo.ToString("D2");
    }



    private void HandleShot()
    {
        if (weaponso == null || weaponShooter == null) return;

        if (weaponso.IsAutomatic)
        {
            if (starterAssetsInputs.shoot && timeSinceLastShot >= weaponso.firerate && currentammo > 0 )
            {
                FireWeapon();
                AmmoAmount(-1);
            }
        }
        else
        {
            if (starterAssetsInputs.shoot && canShoot && timeSinceLastShot >= weaponso.firerate && currentammo > 0)
            {
                FireWeapon();
                AmmoAmount(-1); // ✅ subtract ammo
                canShoot = false;
            }

            if (!starterAssetsInputs.shoot)
            {
                canShoot = true;
            }
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void FireWeapon()
    {
        weaponShooter.Shot(weaponso);

        if (animator != null)
            animator.Play(ShotAnimation, 0, 0f);

        if (flash != null)
            flash.Play();

        timeSinceLastShot = 0f;
    }
    void Handlezoom()
    {
        if (!weaponso.CanZoom) return;
        if (starterAssetsInputs.Zoom)
        {
            playerfollowcamera.m_Lens.FieldOfView = weaponso.ZoomAmount;
            weaponcamera.fieldOfView = weaponso.ZoomAmount;
            zoomvig.SetActive(true);
            firstPersonController.changerotatespeed(weaponso.zoomrotatespeed);
        }
        else
        {
            playerfollowcamera.m_Lens.FieldOfView = defaultFOV;
            weaponcamera.fieldOfView = defaultFOV;
            zoomvig.SetActive(false);
            firstPersonController.changerotatespeed(defaultrotatespeed);
        }
    }
}
