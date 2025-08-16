using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
public class robotenemy : MonoBehaviour
{
    const string PlayerTag = "Player";

    FirstPersonController Player;
    NavMeshAgent agent;

    private void Awake()
    {
     agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        Player = FindFirstObjectByType<FirstPersonController>();
    }
    private void Update()
    {
        if (!Player) return;
        agent.SetDestination(Player.transform.position);
       
    }

    void OnTriggerEnter(Collider other)
    {
     if (other.CompareTag(PlayerTag))
        {
            enemyhp enemyhp = GetComponent<enemyhp>();
            enemyhp.kaboom();
        }
    }

}
