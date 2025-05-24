using UnityEngine;
using UnityEngine.AI;

public class TriggerChaseEnemy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isPlayerInRange = false;

    private Vector3 startPosition;
    private float idleTime = 0f;
    private float returnDelay = 5f;
    private Vector3 lastPosition;

    private bool shouldCheckReturn = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Palyer");
        if (playerObj != null )
        {
            player = playerObj.transform;
        }

        startPosition = transform.position;
        lastPosition = transform.position;
    }

    
    void Update()
    {
        if (isPlayerInRange && player != null)
        {
            agent.SetDestination(player.position);
            idleTime = 0f;
        }
        else
        {
            
             

            if (Vector3.Distance(transform.position, lastPosition) < 0.05f)
            {
                idleTime += Time.deltaTime;

                if (idleTime >= returnDelay)
                {
                    agent.SetDestination(startPosition);
                    //shouldCheckReturn = false;
                }
            }
            else
            {
                idleTime = 0f;
            }
            
            lastPosition = transform.position ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palyer"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Palyer"))
        {
            isPlayerInRange = false;
            //shouldCheckReturn = true;

            agent.ResetPath();
            agent.velocity = Vector3.zero;

        }
    }
}
