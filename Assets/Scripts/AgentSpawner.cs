using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject BluePlayer; 
    public GameObject BlackPlayer;

    public GameObject BlackImage;
    public GameObject BlueImage;

    public Transform verticalLayout;
    private GameObject spawnedBlueImage;
    private GameObject spawnedBlackImage;


    public static List<GameObject> spawnedAgentsBlue = new List<GameObject>();
    public static List<GameObject> spawnedAgentsBlack = new List<GameObject>();
    public static List<GameObject> spawnedImagesBlue = new List<GameObject>();
    public static List<GameObject> spawnedImagesBlack = new List<GameObject>();
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlueObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnBlackObject();
        }


    }

    void SpawnBlueObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {
                Vector3 spawnPosition = navHit.position;

                Instantiate(BluePlayer, spawnPosition, Quaternion.identity);

                spawnedBlueImage = Instantiate(BlueImage, verticalLayout);

                BlueImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hit.point.ToString();

                spawnedImagesBlue.Add(spawnedBlueImage);

            }
        }
    }
    void SpawnBlackObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {
                Vector3 spawnPosition = navHit.position;

                Instantiate(BlackPlayer, spawnPosition, Quaternion.identity);

                GameObject SpawnedBlackImage = Instantiate(BlackImage, verticalLayout);
                BlackImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hit.point.ToString();
                spawnedImagesBlack.Add(SpawnedBlackImage);
            }
        }
    }
    public static void DestroyAgentBlue(int index)
    {
        Debug.LogWarning("Blue Destroyed");
        if (index >= 0 && index < spawnedImagesBlue.Count)
        {
            GameObject gameObject = spawnedImagesBlue[index];
            if (gameObject != null)
            {
                gameObject.GetComponent<Image>().color = Color.red;

            }
        }
    }

    public static void DestroyAgentBlack(int index)
    {
        Debug.LogWarning("Black Destroyed");

        if (index >= 0 && index < spawnedImagesBlack.Count)
        {
            GameObject gameObject = spawnedImagesBlack[index];
            if (gameObject != null)
            {
                gameObject.GetComponent<Image>().color = Color.red;

            }
        }
    }

    private void OnEnable()
    {
        NavigationBlue.Isdead += (index) => DestroyAgentBlue(index);
        NavigationBlack.Isdead += (index) => DestroyAgentBlack(index);
    } 
    
    private void OnDisable()
    {
        NavigationBlue.Isdead -= (index) => DestroyAgentBlue(index);
        NavigationBlack.Isdead -= (index) => DestroyAgentBlack(index);
    }
}

