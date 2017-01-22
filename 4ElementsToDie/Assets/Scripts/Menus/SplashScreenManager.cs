using UnityEngine;
using System.Collections;

public class SplashScreenManager : MonoBehaviour {

	public GameObject[] screens = new GameObject[2];
	public float m_totalDuration = 6f;
	private float m_duration = 0f;

	void OnEnable() {
		for (int i = 0; i < screens.Length; i++)
			screens [i].SetActive(false);
		
		m_duration = m_totalDuration / screens.Length;
//		Debug.Log (m_splashscreen_showtime);

		StartCoroutine (FadeScreens ());
	}

	IEnumerator FadeScreens() {
		for (int i = 0; i < screens.Length; i++) {
			screens [i].SetActive(true);
			yield return new WaitForSeconds (m_duration);
			screens [i].SetActive(false);
			yield return null;
		}
		MainMenuManager.Instance.MainMenu ();
	}

	void OnDisable() {
		for (int i = 0; i < screens.Length; i++)
			screens [i].SetActive (false);
	}


}
