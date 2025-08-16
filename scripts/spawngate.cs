using System.Collections;
using UnityEngine;

public class spawngate : MonoBehaviour
{
    [SerializeField] GameObject robot;
    [SerializeField] float spawntime = 5f;
    [SerializeField] Transform spawnpoint;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawntime);

            Playerhp player = FindFirstObjectByType<Playerhp>();

            if (player == null)
                yield break;  // Stop spawning if player doesn't exist

            Instantiate(robot, spawnpoint.position, spawnpoint.rotation);
        }
    }

}
