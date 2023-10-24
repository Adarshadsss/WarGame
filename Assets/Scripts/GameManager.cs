using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private GameObject[] Blueagents;
    private GameObject[] Blackagents;
    public GameObject winboardBlue;
    public GameObject winboardBlack;

    public AgentSpawner agentSpawner;
    private void Start()
    {
        agentSpawner.enabled = false;
    }

    public void StartButtonClicked()
    {
        agentSpawner.enabled = true;
    }
    private void Update()
    {

        Blueagents = GameObject.FindGameObjectsWithTag("Blue");
        Blackagents = GameObject.FindGameObjectsWithTag("Black");
        if (NavigationBlack.BlackAgentIsMoving && NavigationBlue.BlueAgentIsMoving)
        {
            if (Blueagents.Length <= 0)
            {
                for (int i = 0; i < Blackagents.Length; i++)
                {
                    winboardBlack.gameObject.SetActive(true);
                    Blackagents[i].GetComponent<NavigationBlack>().enabled = false;
                    Blackagents[i].GetComponent<LifeSystem>().enabled = false;
                    Blackagents[i].GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                    Blackagents[i].GetComponent<Animator>().SetFloat("Walk", 0);
                    Blackagents[i].GetComponent<Animator>().SetFloat("Attack", 0);
                    Blackagents[i].GetComponent<Animator>().SetFloat("Idle", 1);
                    agentSpawner.enabled = false;

                }
            }
            if (Blackagents.Length <= 0)
            {

                for (int i = 0; i < Blueagents.Length; i++)
                {
                    winboardBlue.gameObject.SetActive(true);
                    Blueagents[i].GetComponent<NavigationBlue>().enabled = false;
                    Blueagents[i].GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                    Blueagents[i].GetComponent<LifeSystem>().enabled = false;

                    Blueagents[i].GetComponent<Animator>().SetFloat("Walk", 0);
                    Blueagents[i].GetComponent<Animator>().SetFloat("Attack", 0);
                    Blueagents[i].GetComponent<Animator>().SetFloat("Idle", 1);
                    agentSpawner.enabled = false;

                }
            }
        }
    }
}
