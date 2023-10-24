using System;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBlack : MonoBehaviour
{

    private readonly string agentTag = "Blue";
    private NavMeshAgent navMeshAgent;
    private GameObject targetAgent;
    public GameObject BlackImage;
    private Animator animator;

    public static bool BlackAgentIsMoving = false;


    private static int instanceCount = -1;
    private int spawnNumber;

    public static Action<int> Isdead;
    void Awake()
    {
        instanceCount++;
        spawnNumber = instanceCount;
    }

    void Start()
    {

        
        animator = GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        targetAgent = FindNearestAgent();
        if (targetAgent != null)
        {
            navMeshAgent.SetDestination(targetAgent.transform.position);

        }
    }

    void Update()
    {
        if (targetAgent == null)
        {
            targetAgent = FindNearestAgent();
            if (targetAgent != null)
            {
                navMeshAgent.SetDestination(targetAgent.transform.position);

            }
        }
        else
        {
            navMeshAgent.SetDestination(targetAgent.transform.position);


        }

        if (navMeshAgent.velocity != Vector3.zero)
        {

            animator.SetFloat("Walk", 1);
            BlackAgentIsMoving = true;
        }
        else
        {

            Debug.Log("Agent is not moving.");
            animator.SetFloat("Walk", 0);
            animator.SetFloat("Idle", 1);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(agentTag))
        {
            animator.SetFloat("Attack", 1);
            targetAgent = null;
        }
        else
        {
            animator.SetFloat("Attack", 0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        animator.SetFloat("Attack", 0);

        if (collision.gameObject.CompareTag(agentTag))
        {
            animator.SetFloat("Attack", 0);
        }
    }



    GameObject FindNearestAgent()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag(agentTag);
        GameObject nearestAgent = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject agent in agents)
        {
            float distance = Vector3.Distance(transform.position, agent.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestAgent = agent;
            }
        }

        return nearestAgent;
    }


    private void OnDestroy()
    {
        if (Isdead != null)
        {
            Isdead(spawnNumber);
        }
    }

}
