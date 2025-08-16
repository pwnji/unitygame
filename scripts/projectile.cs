using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] float speed = 22.5f;
    [SerializeField] GameObject impactEffect;
    Rigidbody rb;
    int damage = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }
    public void Init(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Playerhp player = other.GetComponent<Playerhp>();
        player?.Damagetaken(damage);

        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
