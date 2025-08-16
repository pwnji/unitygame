using Cinemachine;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{

    CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shot(WeaponData weaponData)
    {
        RaycastHit hit;

        impulseSource.GenerateImpulse();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            // Offset slightly so it doesn't clip into the surface
            Vector3 spawnPos = hit.point + hit.normal * 0.01f;

            // Rotate so the effect faces outward from the surface
            Quaternion rotation = Quaternion.LookRotation(hit.normal);

            // Spawn hit effect with correct position & rotation
            GameObject hitFXInstance = Instantiate(weaponData.hitfx, spawnPos, rotation);

            // Optional: Destroy after a short time if it's temporary
            Destroy(hitFXInstance, 2f);

            // Apply damage if target has enemyhp
            enemyhp enemy = hit.transform.GetComponentInParent<enemyhp>();
            enemy?.Damagetaken(weaponData.damage);
        }
    }
}

