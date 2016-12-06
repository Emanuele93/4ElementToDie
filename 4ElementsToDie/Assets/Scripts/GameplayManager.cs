using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using POLIMIGameCollective;

public class GameplayManager : Singleton<GameplayManager> {

	[Header ("Prefabs")]
	public GameObject mRangeAttack;
	public GameObject mAreaAttack;
	public GameObject mThrustAttack;
	public GameObject mSlashAttack;

	// Use this for initialization
	void Start () {
		ObjectPoolingManager.Instance.CreatePool (mRangeAttack, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (mAreaAttack, 10, 10);
		ObjectPoolingManager.Instance.CreatePool (mThrustAttack, 10, 10);
		ObjectPoolingManager.Instance.CreatePool (mSlashAttack, 10, 10);
		//MusicManager.Instance.PlayMusic ("GameplayMusic");
	}
	
	// Update is called once per frame
	void Update () {




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
}
