using UnityEngine;

public abstract class pickup : MonoBehaviour
{
    [SerializeField] float rotatespeed = 100f;
    const string playerTag = "Player";


    private void Update()
    {
        transform.Rotate(0f, rotatespeed * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            WeaponController weaponController = other.GetComponentInChildren<WeaponController>();
            OnPickup(weaponController);
            Destroy(this.gameObject); // Destroy the pickup after it has been collected

        }
    }

    protected abstract void OnPickup(WeaponController weaponController);
}
