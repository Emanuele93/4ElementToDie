using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectCollection : MonoBehaviour
{
    private List<Character>[] fireEnemies;
    private List<Character>[] waterEnemies;
    private List<Character>[] airEnemies;
    private List<Character>[] earthEnemies;
    private List<Item>[] fireEquipment;
    private List<Item>[] waterEquipment;
    private List<Item>[] airEquipment;
    private List<Item>[] earthEquipment;
    private List<GameObject> fireObject;
    private List<GameObject> waterObject;
    private List<GameObject> airObject;
    private List<GameObject> earthObject;

    private GameObject fireChest;
    private GameObject waterChest;
    private GameObject airChest;
    private GameObject earthChest;

    private GameObject[] secondaryDropKey;
    private GameObject[] secondaryDropCoin;
    private GameObject[] secondaryDropHeart;

    public GameObject enemyPrefab;
    public GameplayManager gameplayManager;

    void Start()
    {
        GameObject go;
        Character ch;
        Item it;
        Object[] worlds;

        fireEnemies = new List<Character>[4];
        for (int i = 0; i < fireEnemies.Length; i++)
            fireEnemies[i] = new List<Character>();

        waterEnemies = new List<Character>[4];
        for (int i = 0; i < waterEnemies.Length; i++)
            waterEnemies[i] = new List<Character>();

        airEnemies = new List<Character>[4];
        for (int i = 0; i < airEnemies.Length; i++)
            airEnemies[i] = new List<Character>();

        earthEnemies = new List<Character>[4];
        for (int i = 0; i < earthEnemies.Length; i++)
            earthEnemies[i] = new List<Character>();

        fireEquipment = new List<Item>[4];
        for (int i = 0; i < fireEquipment.Length; i++)
            fireEquipment[i] = new List<Item>();

        waterEquipment = new List<Item>[4];
        for (int i = 0; i < waterEquipment.Length; i++)
            waterEquipment[i] = new List<Item>();

        airEquipment = new List<Item>[4];
        for (int i = 0; i < airEquipment.Length; i++)
            airEquipment[i] = new List<Item>();

        earthEquipment = new List<Item>[4];
        for (int i = 0; i < earthEquipment.Length; i++)
            earthEquipment[i] = new List<Item>();

        fireObject = new List<GameObject>();
        waterObject = new List<GameObject>();
        airObject = new List<GameObject>();
        earthObject = new List<GameObject>();

        worlds = Resources.LoadAll("Enemies/FireEnemies/Easy", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            fireEnemies[0].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/FireEnemies/Medium", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            fireEnemies[1].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/FireEnemies/Hard", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            fireEnemies[2].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/FireEnemies/Boss", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            fireEnemies[3].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/WaterEnemies/Easy", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            waterEnemies[0].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/WaterEnemies/Medium", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            waterEnemies[1].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/WaterEnemies/Hard", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            waterEnemies[2].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/WaterEnemies/Boss", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            waterEnemies[3].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/EarthEnemies/Easy", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            earthEnemies[0].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/EarthEnemies/Medium", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            earthEnemies[1].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/EarthEnemies/Hard", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            earthEnemies[2].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/EarthEnemies/Boss", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            earthEnemies[3].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/AirEnemies/Easy", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            airEnemies[0].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/AirEnemies/Medium", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            airEnemies[1].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/AirEnemies/Hard", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            airEnemies[2].Add(ch);
        }

        worlds = Resources.LoadAll("Enemies/AirEnemies/Boss", typeof(Character));
        foreach (Object world in worlds)
        {
            ch = world as Character;
            airEnemies[3].Add(ch);
        }

        //--------------------------------------------------------------------------------------------------------
        
        worlds = Resources.LoadAll("Items/FireItem/Equipments/Common", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            fireEquipment[0].Add(it);
        }

        worlds = Resources.LoadAll("Items/FireItem/Equipments/Rare", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            fireEquipment[1].Add(it);
        }

        worlds = Resources.LoadAll("Items/FireItem/Equipments/Epic", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            fireEquipment[2].Add(it);
        }

        worlds = Resources.LoadAll("Items/FireItem/Equipments/Legendary", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            fireEquipment[3].Add(it);
        }

        worlds = Resources.LoadAll("Items/WaterItem/Equipments/Common", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            waterEquipment[0].Add(it);
        }

        worlds = Resources.LoadAll("Items/WaterItem/Equipments/Rare", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            waterEquipment[1].Add(it);
        }

        worlds = Resources.LoadAll("Items/WaterItem/Equipments/Epic", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            waterEquipment[2].Add(it);
        }

        worlds = Resources.LoadAll("Items/WaterItem/Equipments/Legendary", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            waterEquipment[3].Add(it);
        }

        worlds = Resources.LoadAll("Items/EarthItem/Equipments/Common", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            earthEquipment[0].Add(it);
        }

        worlds = Resources.LoadAll("Items/EarthItem/Equipments/Rare", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            earthEquipment[1].Add(it);
        }

        worlds = Resources.LoadAll("Items/EarthItem/Equipments/Epic", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            earthEquipment[2].Add(it);
        }

        worlds = Resources.LoadAll("Items/EarthItem/Equipments/Legendary", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            earthEquipment[3].Add(it);
        }

        worlds = Resources.LoadAll("Items/AirItem/Equipments/Common", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            airEquipment[0].Add(it);
        }

        worlds = Resources.LoadAll("Items/AirItem/Equipments/Rare", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            airEquipment[1].Add(it);
        }

        worlds = Resources.LoadAll("Items/AirItem/Equipments/Epic", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            airEquipment[2].Add(it);
        }

        worlds = Resources.LoadAll("Items/AirItem/Equipments/Legendary", typeof(Item));
        foreach (Object world in worlds)
        {
            it = world as Item;
            airEquipment[3].Add(it);
        }

        //----------------------------------------------------------------------------------------

        worlds = Resources.LoadAll("Items/FireItem/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            fireObject.Add(go);
        }

        worlds = Resources.LoadAll("Items/AirItem/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            airObject.Add(go);
        }

        worlds = Resources.LoadAll("Items/WaterItem/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            waterObject.Add(go);
        }

        worlds = Resources.LoadAll("Items/EarthItem/Object", typeof(GameObject));
        foreach (Object world in worlds)
        {
            go = world as GameObject;
            earthObject.Add(go);
        }
        
        fireChest = (Resources.LoadAll("Items/FireItem/chestFire", typeof(GameObject)))[0] as GameObject;
        airChest = (Resources.LoadAll("Items/AirItem/chestAir", typeof(GameObject)))[0] as GameObject;
        waterChest = (Resources.LoadAll("Items/WaterItem/chestWater", typeof(GameObject)))[0] as GameObject;
        earthChest = (Resources.LoadAll("Items/EarthItem/chestEarth", typeof(GameObject)))[0] as GameObject;
        
        fireChest.GetComponent<chestEnemiesActivator>().gm = gameplayManager;
        airChest.GetComponent<chestEnemiesActivator>().gm = gameplayManager;
        waterChest.GetComponent<chestEnemiesActivator>().gm = gameplayManager;
        earthChest.GetComponent<chestEnemiesActivator>().gm = gameplayManager;

        //--------------------------------------------------------------------------------------------------

        secondaryDropKey = new GameObject[System.Enum.GetValues(typeof(ElementType)).Length];
        secondaryDropCoin = new GameObject[System.Enum.GetValues(typeof(ElementType)).Length];
        secondaryDropHeart = new GameObject[System.Enum.GetValues(typeof(ElementType)).Length];
        secondaryDropKey[(int)ElementType.Fire] = (Resources.LoadAll("Items/FireItem/Object/keyFire", typeof(GameObject)))[0] as GameObject;
        secondaryDropCoin[(int)ElementType.Fire] = (Resources.LoadAll("Items/FireItem/Object/CoinFire", typeof(GameObject)))[0] as GameObject;
        secondaryDropHeart[(int)ElementType.Fire] = (Resources.LoadAll("Items/FireItem/Object/heartFire", typeof(GameObject)))[0] as GameObject;
        secondaryDropKey[(int)ElementType.Water] = (Resources.LoadAll("Items/WaterItem/Object/keyWater", typeof(GameObject)))[0] as GameObject;
        secondaryDropCoin[(int)ElementType.Water] = (Resources.LoadAll("Items/WaterItem/Object/CoinWater", typeof(GameObject)))[0] as GameObject;
        secondaryDropHeart[(int)ElementType.Water] = (Resources.LoadAll("Items/WaterItem/Object/heartWater", typeof(GameObject)))[0] as GameObject;
        secondaryDropKey[(int)ElementType.Air] = (Resources.LoadAll("Items/AirItem/Object/keyAir", typeof(GameObject)))[0] as GameObject;
        secondaryDropCoin[(int)ElementType.Air] = (Resources.LoadAll("Items/AirItem/Object/CoinAir", typeof(GameObject)))[0] as GameObject;
        secondaryDropHeart[(int)ElementType.Air] = (Resources.LoadAll("Items/AirItem/Object/heartAir", typeof(GameObject)))[0] as GameObject;
        secondaryDropKey[(int)ElementType.Earth] = (Resources.LoadAll("Items/EarthItem/Object/keyEarth", typeof(GameObject)))[0] as GameObject;
        secondaryDropCoin[(int)ElementType.Earth] = (Resources.LoadAll("Items/EarthItem/Object/CoinEarth", typeof(GameObject)))[0] as GameObject;
        secondaryDropHeart[(int)ElementType.Earth] = (Resources.LoadAll("Items/EarthItem/Object/heartEarth", typeof(GameObject)))[0] as GameObject;

        gameplayManager.setSecondaryDropCoin(secondaryDropCoin);
        gameplayManager.setSecondaryDropKey(secondaryDropKey);
        gameplayManager.setSecondaryDropHeart(secondaryDropHeart);
    }

    public GameObject getFireEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (fireEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, fireEnemies[difficulty].Count);
        GameObject go = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        go.GetComponent<CharacterManager>().InitCharacter(fireEnemies[difficulty][enemyNumber]);
        return go;
    }

    public GameObject getWaterEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (waterEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, waterEnemies[difficulty].Count);
        GameObject go = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        go.GetComponent<CharacterManager>().InitCharacter(waterEnemies[difficulty][enemyNumber]);
        return go;
    }

    public GameObject getAirEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (airEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, airEnemies[difficulty].Count);
        GameObject go = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        go.GetComponent<CharacterManager>().InitCharacter(airEnemies[difficulty][enemyNumber]);
        return go;
    }

    public GameObject getEarthEnemy(int diff)
    {
        int difficulty = Random.Range(0, diff - 1);
        while (earthEnemies[difficulty].Count == 0)
            difficulty--;
        int enemyNumber = Random.Range(0, earthEnemies[difficulty].Count);
        GameObject go = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        go.GetComponent<CharacterManager>().InitCharacter(earthEnemies[difficulty][enemyNumber]);
        return go;
    }

    public Item getFireEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (fireEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, fireEquipment[rarity].Count);
        return fireEquipment[rarity][equipmentsNumber];
    }

    public Item getWaterEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (waterEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, waterEquipment[rarity].Count);
        return waterEquipment[rarity][equipmentsNumber];
    }

    public Item getAirEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (airEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, airEquipment[rarity].Count);
        return airEquipment[rarity][equipmentsNumber];
    }

    public Item getEarthEquipment(int rar)
    {
        int rarity = Random.Range(0, rar - 1);
        while (earthEquipment[rarity].Count == 0)
            rarity--;
        int equipmentsNumber = Random.Range(0, earthEquipment[rarity].Count);
        return earthEquipment[rarity][equipmentsNumber];
    }

    public GameObject getAirObject()
    {
        return Instantiate(airObject[Random.Range(0, airObject.Count)]);
    }

    public GameObject getWaterObject()
    {
        return Instantiate(waterObject[Random.Range(0, waterObject.Count)]);
    }

    public GameObject getFireObject()
    {
        return Instantiate(fireObject[Random.Range(0, fireObject.Count)]);
    }

    public GameObject getEarthObject()
    {
        return Instantiate(earthObject[Random.Range(0, earthObject.Count)]);
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
