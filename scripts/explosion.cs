using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;

    void Start()
    {
        explodes();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void explodes()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitcollider in hitcolliders)
        {
            Playerhp playershp = hitcollider.GetComponent<Playerhp>();

            if (!playershp) continue;

            playershp.Damagetaken(damage);

            break;
        }
    }
}



