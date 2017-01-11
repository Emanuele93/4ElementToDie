using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public GameObject m_MainMenuScreen;
    public GameObject m_CharacterSelectionScreen;
    public GameObject m_SettingsScreen;
    public GameObject m_CreditsScreen;

    void Start()
    {
        m_MainMenuScreen.SetActive(true);
        m_CharacterSelectionScreen.SetActive(false);
        m_CreditsScreen.SetActive(false);
        m_SettingsScreen.SetActive(false);
    }

    public void Play()
    {
        m_MainMenuScreen.SetActive(false);
        m_CharacterSelectionScreen.SetActive(true);
        m_CreditsScreen.SetActive(false);
        m_SettingsScreen.SetActive(false);
    }

    public void Settings()
    {
        m_MainMenuScreen.SetActive(false);
        m_CharacterSelectionScreen.SetActive(false);
        m_SettingsScreen.SetActive(true);
        m_CreditsScreen.SetActive(false);
    }

    public void Credits()
    {
        m_MainMenuScreen.SetActive(false);
        m_CharacterSelectionScreen.SetActive(false);
        m_SettingsScreen.SetActive(false);
        m_CreditsScreen.SetActive(true);
    }
    
    public void Back()
    {
        m_MainMenuScreen.SetActive(true);
        m_CharacterSelectionScreen.SetActive(false);
        m_SettingsScreen.SetActive(false);
        m_CreditsScreen.SetActive(false);
    }

}
