using POLIMIGameCollective;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager> {
    
    ///////// TESTING
    public Character testCharacter;
    ///////////////

    // confirmed selection from Character Selection Menu
    public static Character chosenCharacter;

    // Characters
    public Character AirPlayer;
	public Character FirePlayer;
	public Character EarthPlayer;
	public Character WaterPlayer;

    [Header("UI Screens")]
    public GameObject m_ingameMenuScreen;
    public GameObject m_overlayScreen;
    public Text m_overlayText;

    [Header("Player")]
    public Player m_player;

    [Header("Prefabs")]
    public GameObject m_SlashAttack;
    public GameObject m_ThrustAttack;
	public GameObject m_AreaAttack;
    public GameObject m_RangedAttack;
    public GameObject m_drop;
    
	// We create a dictionary where the keys will be the instance ID of the attacks (they're managed by the pooling manager)
	// and the values will be the CharacterManager of the attacker using that instance, this, in order to have the 
	// stats of the attacker.
	public Dictionary<int,CharacterManager> attackersDict = new Dictionary<int,CharacterManager> ();

    // Number of killed bosses, by element.
    private int[] noKilledBosses = new int[System.Enum.GetValues(typeof(ElementType)).Length];

    // Use this for initialization
    void Start ()
    {
        ObjectPoolingManager.Instance.CreatePool(m_SlashAttack, 30, 30);
        ObjectPoolingManager.Instance.CreatePool(m_ThrustAttack, 30, 30);
        ObjectPoolingManager.Instance.CreatePool (m_AreaAttack, 30, 30);
        ObjectPoolingManager.Instance.CreatePool(m_RangedAttack, 100, 100);
        ObjectPoolingManager.Instance.CreatePool (m_drop, 100, 100);
        //MusicManager.Instance.PlayMusic ("GameplayMusic");

        m_ingameMenuScreen.SetActive(false);

        m_player.GetComponent<CharacterManager>().InitCharacter(chosenCharacter);
        
        //TESTING
        // m_player.GetComponent<CharacterManager>().InitCharacter(WaterPlayer);
    }
	
	// Update is called once per frame
	void Update () {

        ///////////// TESTING
        //drop spawning
        if (Input.GetKeyDown(KeyCode.V))
            StartCoroutine(SpawnDrops(m_player.GetComponent<CharacterManager>()));
        //item discarding
        if (Input.GetKeyDown(KeyCode.B))
        {
            int random = Random.Range(0, 4);
            if (m_player.GetComponent<CharacterManager>().Inventory[random] != null)
                m_player.GetComponent<CharacterManager>().RemoveItem(m_player.GetComponent<CharacterManager>().Inventory[random]);
            else Debug.Log("Randomly selected an empty slot. Can't discard.");
        }
        //////////////////////

        if (Input.GetKeyDown(KeyCode.P))
        {
            m_ingameMenuScreen.SetActive(!m_ingameMenuScreen.activeInHierarchy);
        }

        //		if (Input.GetKeyDown (KeyCode.Alpha1))
        //			SfxManager.Instance.Play ("creature");
        //		else if (Input.GetKeyDown (KeyCode.Alpha2))
        //			SfxManager.Instance.Play ("jump");
        //		else if (Input.GetKeyDown (KeyCode.Alpha3))
        //			SfxManager.Instance.Play ("laser");
        //		else if (Input.GetKeyDown (KeyCode.Alpha4))
        //			SfxManager.Instance.Play ("lose");
        //		else if (Input.GetKeyDown (KeyCode.Alpha5))
        //			SfxManager.Instance.Play ("pickup");
        //		else if (Input.GetKeyDown (KeyCode.Alpha6))
        //			SfxManager.Instance.Play ("radar");
        //		else if (Input.GetKeyDown (KeyCode.Alpha7))
        //			SfxManager.Instance.Play ("rumble");
        //		else if (Input.GetKeyDown (KeyCode.Space)) {
        //			MusicManager.Instance.StopAll ();
        //			MusicManager.Instance.PlayMusic ("MenuMusic");
        //			SceneManager.LoadScene ("Menu");
        //		}
    }

    #region Attack Management
    public void ExecuteAttack(CharacterManager attacker, CharacterManager defender)
    {
        double damage = GameLogicManager.CalculateDamage(attacker, defender);
        defender.ApplyDamage(damage);
        //AbilityManager.Instance.CheckAbilityActivation(TriggerType.OnInflictedAttack, attacker, defender);
        //AbilityManager.Instance.CheckAbilityActivation(TriggerType.OnReceivedAttack, defender, attacker);
        if (defender.isDead())
        {
            //AbilityManager.Instance.CheckAbilityActivation(TriggerType.OnKill, attacker, defender);
            //AbilityManager.Instance.CheckAbilityActivation(TriggerType.OnDeath, defender, attacker);

            //check again in case of resurrection
            if (defender.isDead())
            {
                Kill(defender);
            }
        }
    }

    public void Kill(CharacterManager deadCharacter)
    {
        Debug.Log(deadCharacter + " is dead");
        if (deadCharacter.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GameOver());
        }
        else if (deadCharacter.gameObject.CompareTag("FinalBoss"))
        {
            StartCoroutine(Victory());
        }
        else if (deadCharacter.gameObject.CompareTag("Boss"))
        {
            SpawnDrops(deadCharacter);
            deadCharacter.gameObject.SetActive(false);
            noKilledBosses[(int)deadCharacter.Element]++;
            //TODO: open the next area, obtain the boss crystal and so on.
        }
        else if (deadCharacter.gameObject.CompareTag("Enemy"))
        {
            SpawnDrops(deadCharacter);
            deadCharacter.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Drops Management
    public IEnumerator SpawnDrops(CharacterManager character)
    {
        List<Drop> drops = new List<Drop>();

        foreach (Item i in character.Inventory)
        {
            if (i!= null && Random.Range(0, 100) <= i.dropRate)
            {
                Debug.Log("Spawned " + i.itemName);

                //spawn the object
                GameObject go = ObjectPoolingManager.Instance.GetObject (m_drop.name);
                go.transform.position = character.transform.position;
                go.transform.rotation = Quaternion.identity;
                go.GetComponent<SpriteRenderer>().sprite = i.sprite;
                go.SetActive(true);
                
                //define item
                Drop drop = go.GetComponent<Drop>() as Drop;
                drop.item = i;
                drops.Add(drop);

                //give a random direction to the explosion
                drop.direction = new Vector3(
                    UnityEngine.Random.Range(-1f, 1f),
                    UnityEngine.Random.Range(-1f, 1f), 
                    0f
                );

                //enable movement
                drop.shouldMove = true;
            }
        }

        yield return new WaitForSeconds(1);

        //disable movement
        foreach (Drop drop in drops)
        {
            drop.shouldMove = false;
        }
    }

    public void PickUpDrop(Drop drop)
    {
        if(m_player.GetComponent<CharacterManager>().AddItem(drop.item))
        {
            drop.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Game Ending Management
    IEnumerator GameOver()
    {
        //ClearArea();
        m_overlayText.text = "GAME OVER";
        m_ingameMenuScreen.SetActive(false);
        m_overlayScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        m_ingameMenuScreen.SetActive(false);
        m_overlayScreen.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator Victory()
    {
        //ClearArea();
        m_overlayText.text = "CONGRATULATIONS";
        m_ingameMenuScreen.SetActive(false);
        m_overlayScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        m_ingameMenuScreen.SetActive(false);
        m_overlayScreen.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
    #endregion

}
