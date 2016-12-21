using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectCollection : MonoBehaviour
{
    private List<GameObject>[] fireEnemies;
    private List<GameObject>[] waterEnemies;
    private List<GameObject>[] airEnemies;
    private List<GameObject>[] earthEnemies;
    private List<GameObject>[] fireEquipment;
    private List<GameObject>[] waterEquipment;
    private List<GameObject>[] airEquipment;
    private List<GameObject>[] earthEquipment;
    private List<GameObject> fireObject;
    private List<GameObject> waterObject;
    private List<GameObject> airObject;
    private List<GameObject> earthObject;

    private GameObject fireChest;
    private GameObject waterChest;
    private GameObject airChest;
    private GameObject earthChest;

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

        fireEquipment = new List<GameObject>[10];
        for (int i = 0; i < fireEquipment.Length; i++)
            fireEquipment[i] = new List<GameObject>();

        waterEquipment = new List<GameObject>[10];
        for (int i = 0; i < waterEquipment.Length; i++)
            waterEquipment[i] = new List<GameObject>();

        airEquipment = new List<GameObject>[10];
        for (int i = 0; i < airEquipment.Length; i++)
            airEquipment[i] = new List<GameObject>();

        earthEquipment = new List<GameObject>[10];
        for (int i = 0; i < earthEquipment.Length; i++)
            earthEquipment[i] = new List<GameObject>();

        fireObject = new List<GameObject>();
        waterObject = new List<GameObject>();
        airObject = new List<GameObject>();
        earthObject = new List<GameObject>();

        worlds = Resources.LoadAll("Enemies/FireEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
			go.tag = "Enemy";
            fireEnemies[go.GetComponent<Enemy>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Enemies/WaterEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            waterEnemies[go.GetComponent<Enemy>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Enemies/AirEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            airEnemies[go.GetComponent<Enemy>().difficulty - 1].Add(go);
        }

        worlds = Resources.LoadAll("Enemies/EarthEnemies", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            earthEnemies[go.GetComponent<Enemy>().difficulty - 1].Add(go);
        }
        
        worlds = Resources.LoadAll("Object/FireObject/Equipment", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            fireEquipment[go.GetComponent<tempStatsObject>().rarity - 1].Add(go);
        }


        worlds = Resources.LoadAll("Object/WaterObject/Equipment", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            waterEquipment[go.GetComponent<tempStatsObject>().rarity - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/AirObject/Equipment", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            airEquipment[go.GetComponent<tempStatsObject>().rarity - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/EarthObject/Equipment", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            earthEquipment[go.GetComponent<tempStatsObject>().rarity - 1].Add(go);
        }

        worlds = Resources.LoadAll("Object/FireObject/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            fireObject.Add(go);
        }

        worlds = Resources.LoadAll("Object/WaterObject/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            waterObject.Add(go);
        }

        worlds = Resources.LoadAll("Object/AirObject/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            airObject.Add(go);
        }

        worlds = Resources.LoadAll("Object/EarthObject/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            earthObject.Add(go);
        }

        fireChest = (Resources.LoadAll("Object/FireObject/chestFire", typeof(GameObject)))[0] as GameObject;
        airChest = (Resources.LoadAll("Object/AirObject/chestAir", typeof(GameObject)))[0] as GameObject;
        waterChest = (Resources.LoadAll("Object/WaterObject/chestWater", typeof(GameObject)))[0] as GameObject;
        earthChest = (Resources.LoadAll("Object/EarthObject/chestEarth", typeof(GameObject)))[0] as GameObject;
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

    public GameObject getFireEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (fireEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, fireEquipment[rarity].Count);
        return Instantiate(fireEquipment[rarity][equipmentsNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getWaterEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (waterEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, fireEquipment[rarity].Count);
        return Instantiate(waterEquipment[rarity][equipmentsNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getAirEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (airEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, fireEquipment[rarity].Count);
        return Instantiate(airEquipment[rarity][equipmentsNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getEarthEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (earthEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, earthEquipment[rarity].Count);
        return Instantiate(earthEquipment[rarity][equipmentsNumber], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getAirObject()
    {
        return Instantiate(airObject[Random.Range(0, airObject.Count)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getWaterObject()
    {
        return Instantiate(waterObject[Random.Range(0, waterObject.Count)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getFireObject()
    {
        return Instantiate(fireObject[Random.Range(0, fireObject.Count)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getEarthObject()
    {
        return Instantiate(earthObject[Random.Range(0, earthObject.Count)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getAirChest()
    {
        return Instantiate(airChest, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getWaterChest()
    {
        return Instantiate(waterChest, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getFireChest()
    {
        return Instantiate(fireChest, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    public GameObject getEarthChest()
    {
        return Instantiate(earthChest, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }
}
