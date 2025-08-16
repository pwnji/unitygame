using System.Collections;
using UnityEngine;

public class turret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform turrethead;
    [SerializeField] Transform playertargetpoint;
    [SerializeField] Transform projectilespawn;
    [SerializeField] float firerate = 0.5f;
    [SerializeField] int damage = 1;
    Playerhp player;
    private void Start()
    {
        player = FindFirstObjectByType<Playerhp>();
        StartCoroutine(firetiming());
    }
    private void Update()
    {
        turrethead.LookAt(playertargetpoint);
    }

    IEnumerator firetiming()
    {
        while (player)
        {
            yield return new WaitForSeconds(1f);

            GameObject proj = Instantiate(projectile, projectilespawn.position, Quaternion.identity);
            projectile newprojectile = proj.GetComponent<projectile>();

            if (newprojectile != null)
            {
                proj.transform.LookAt(playertargetpoint);
                newprojectile.Init(damage);
            }
        }
    }


}
