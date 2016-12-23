using POLIMIGameCollective;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager> {
    
	[Header("Sounds and Audio Effects")]
    // Sound manager.
	public MusicManager musicManager;

    // confirmed selection from Character Selection Menu
    public static Character chosenCharacter;

	[Header("Characters Stats")]
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
       	
        m_ingameMenuScreen.SetActive(false);

        m_player.GetComponent<CharacterManager>().InitCharacter(chosenCharacter);
    }
	
	// Update is called once per frame
	void Update () {

        ///////////// TESTING
        //drop spawning
//        if (Input.GetKeyDown(KeyCode.V))
//            StartCoroutine(SpawnDrops(m_player.GetComponent<CharacterManager>()));
        //////////////////////

        if (Input.GetKeyDown(KeyCode.O))
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
			StartCoroutine(SpawnDrops(deadCharacter));
            deadCharacter.gameObject.SetActive(false);
            noKilledBosses[(int)deadCharacter.Element]++;
            //TODO: open the next area, obtain the boss crystal and so on.
        }
////        else if (deadCharacter.gameObject.CompareTag("Enemy"))
//		else {
//			StartCoroutine(SpawnDrops(deadCharacter));
//            deadCharacter.gameObject.SetActive(false);
//        }
		//        else if (deadCharacter.gameObject.CompareTag("Enemy"))
		else {
			StartCoroutine(SpawnDrops(deadCharacter));
            deadCharacter.gameObject.SetActive (false);
		}

    }
    #endregion

    #region Drops Management
    public IEnumerator SpawnDrops(CharacterManager character)
    {
        List<Drop> drops = new List<Drop>();

        foreach (Item i in character.Inventory)
        {
			
			if (i != null && ((Random.Range(0f, 100f) * 5f) <= i.dropRate))
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

		//character.gameObject.SetActive (false);

    }

    public void openChest(GameObject chest)
    {
        StartCoroutine(SpawnChestDrops(chest));
        Destroy(chest);
    }

    public IEnumerator SpawnChestDrops(GameObject chest)
    {
        List<Drop> drops = new List<Drop>();
        foreach (Item i in chest.GetComponent<chestEnemiesActivator>().objects)
        {

            if (i != null)
            {
                Debug.Log("Spawned " + i.itemName);

                //spawn the object
                GameObject go = ObjectPoolingManager.Instance.GetObject(m_drop.name);
                go.transform.position = chest.transform.position;
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

        //character.gameObject.SetActive (false);

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
		m_player.isDead = true;
		yield return new WaitForSeconds(1f);
        m_overlayText.text = "GAME OVER";
        m_ingameMenuScreen.SetActive(false);
        m_overlayScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
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
        yield return new WaitForSeconds(2f);
        m_ingameMenuScreen.SetActive(false);
        m_overlayScreen.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
    #endregion

	#region Game Music Management.
	public void PlayMusic(string musicName, float pitchVariance = 0) {
		MusicManager.Instance.PlayMusic (musicName, pitchVariance);
	}

	public void PlayMusicWithBackground(string musicName, float pitchVariance = 0) {
		MusicManager.Instance.PlayMusic (Constants.MUSIC_Background);
		MusicManager.Instance.PlayMusic (musicName, pitchVariance);
	}

	public void StopMusic(string musicName, float pitchVariance = 0) {
		MusicManager.Instance.StopMusic (musicName, pitchVariance);
	}

	public void StopAllMusic() {
		MusicManager.Instance.StopAll ();
	}
	#endregion
}
