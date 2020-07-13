using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawn : MonoBehaviour
{
    public GameObject ennemyRobotPrefab;
    public List<GameObject> ennemyRobotList = new List<GameObject>();

    private Vector3 spawnEast;
    private Vector3 spawnWest;

    void Start()
    {
        // Spawns Robot locations
        spawnEast = new Vector3(27.5f, 0f, 12.5f);
        spawnWest = new Vector3(2.5f, 0f, 12.5f);

        // Create Robot Ennemies
        GameObject test1 = SpawnEnnemy(spawnEast);
        test1.name = "EnnemyEast";
        AddToList(test1);

        GameObject test2 = SpawnEnnemy(spawnWest);
        test2.name = "EnnemyWest";
        AddToList(test2);
    }


    void Update()
    {
        // If list empty go to main menu
        if (ennemyRobotList.Count == 0)
        {
            ScenesManager sm = GameObject.Find("Main Camera").GetComponent<ScenesManager>();
            StartCoroutine(sm.LoadSpecificScene(1, "MainMenu"));
        }
    }

    private GameObject SpawnEnnemy(Vector3 spawnLocation)
    {
        // Create Ennemy
        GameObject ennemy = Instantiate(ennemyRobotPrefab, spawnLocation, Quaternion.identity);
        ennemy.SetActive(true);

        return ennemy;
    }


    private List<GameObject> AddToList(GameObject objetToAdd)
    {
        ennemyRobotList.Add(objetToAdd);
        return ennemyRobotList;
    }


    public List<GameObject> RemoveToList(GameObject objetToRemove)
    {
        ennemyRobotList.Remove(objetToRemove);
        return ennemyRobotList;
    }
}