using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Palyer");
        if (playerObj != null )
        {
            player = playerObj.transform;
        }
    }

    
    void Update()
    {
        if(player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
