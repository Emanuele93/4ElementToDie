using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectCollection : MonoBehaviour
{
    private List<GameObject>[] fireEnemies;
    private List<GameObject>[] waterEnemies;
    private List<GameObject>[] airEnemies;
    private List<GameObject>[] earthEnemies;
    private List<GameObject>[] fireObject;
    private List<GameObject>[] waterObject;
    private List<GameObject>[] airObject;
    private List<GameObject>[] earthObject;

    void Start()
    {
        GameObject go;
        Object[] worlds;

        fireEnemies = new List<GameObject>[10];
        for (int i = 0; i < fireEnemies.Length; i++)
            fireEnemies[i] = new List<GameObject>();

        waterEnemies = new List<GameObject>[10];
        for (int i = 0; i < waterEnemies.Length; i++)
            waterEnemies[i] = new List<GameObject>();

        airEnemies = new List<GameObject>[10];
        for (int i = 0; i < airEnemies.Length; i++)
            airEnemies[i] = new List<GameObject>();

        earthEnemies = new List<GameObject>[10];
        for (int i = 0; i < earthEnemies.Length; i++)
            earthEnemies[i] = new List<GameObject>();

        fireObject = new List<GameObject>[10];
        for (int i = 0; i < fireObject.Length; i++)
            fireObject[i] = new List<GameObject>();

        waterObject = new List<GameObject>[10];
        for (int i = 0; i < waterObject.Length; i++)
            waterObject[i] = new List<GameObject>();

        airObject = new List<GameObject>[10];
        for (int i = 0; i < airObject.Length; i++)
            airObject[i] = new List<GameObject>();

        earthObject = new List<GameObject>[10];
        for (int i = 0; i < earthObject.Length; i++)
            earthObject[i] = new List<GameObject>();

        worlds = Resources.LoadAll("Enemies/FireEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            fireEnemies[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Enemies/WaterEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            waterEnemies[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Enemies/AirEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            airEnemies[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Enemies/EarthEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            earthEnemies[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/FireObject", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            fireObject[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/WaterObject", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            waterObject[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/AirObject", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            airObject[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/EarthObject", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            earthObject[go.GetComponent<tempStats>().difficulty - 1].Add(go);
        }
    }

    public GameObject getFireEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (fireEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, fireEnemies[difficulty].Count);
        return Instantiate(fireEnemies[difficulty][enemyNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getWaterEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (waterEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, fireEnemies[difficulty].Count);
        return Instantiate(waterEnemies[difficulty][enemyNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getAirEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (airEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, fireEnemies[difficulty].Count);
        return Instantiate(airEnemies[difficulty][enemyNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getEarthEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (earthEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, fireEnemies[difficulty].Count);
        return Instantiate(earthEnemies[difficulty][enemyNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }
}
