using POLIMIGameCollective;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager> {

    [Header("Start with Splashscreen?")]
    public bool m_start_with_splashscreen = true;

    public GameObject m_SplashScreen;
    public GameObject m_MainMenuScreen;
    public GameObject m_CharacterSelectionScreen;
    public GameObject m_SettingsScreen;
    public GameObject m_CreditsScreen;

    public enum eMenuScreen { SplashScreen, MainMenu, CharacterSelection, Settings, Credits };

    private static bool m_has_shown_splashscreen = false;

    void Start()
    {
        if (!m_has_shown_splashscreen && m_start_with_splashscreen)
        {
            SwitchMenuTo(eMenuScreen.SplashScreen);
            m_has_shown_splashscreen = true;
        }
        else
        {
            SwitchMenuTo(eMenuScreen.MainMenu);
        }
        
        //MusicManager.Instance.StopAll();
        //MusicManager.Instance.PlayMusic(Constants.MUSIC_Menu);
    }

    // Switch the current screen to the target one
	public void SwitchMenuTo(eMenuScreen screen)
    {
        ClearScreens();
        switch (screen)
        {
            case eMenuScreen.SplashScreen:
                if (m_SplashScreen != null)
                    m_SplashScreen.SetActive(true);
                break;
            case eMenuScreen.MainMenu:
                if (m_MainMenuScreen != null)
                    m_MainMenuScreen.SetActive(true);
                break;
            case eMenuScreen.CharacterSelection:
                if (m_CharacterSelectionScreen != null)
                    m_CharacterSelectionScreen.SetActive(true);
                break;
            case eMenuScreen.Settings:
                if (m_SettingsScreen != null)
                    m_SettingsScreen.SetActive(true);
                break;
            case eMenuScreen.Credits:
                if (m_CreditsScreen != null)
                    m_CreditsScreen.SetActive(true);
                break;
        }
    }

    // Clear all the screens
    void ClearScreens()
    {
        if (m_SplashScreen != null)
            m_SplashScreen.SetActive(false);
        if (m_MainMenuScreen != null)
            m_MainMenuScreen.SetActive(false);
        if (m_CharacterSelectionScreen != null)
            m_CharacterSelectionScreen.SetActive(false);
        if (m_SettingsScreen != null)
            m_SettingsScreen.SetActive(false);
        if (m_CreditsScreen != null)
            m_CreditsScreen.SetActive(false);
    }


    public void MainMenu()
    {
        SwitchMenuTo(eMenuScreen.MainMenu);
    }

    public void Play()
    {
        SwitchMenuTo(eMenuScreen.CharacterSelection);
    }

    public void Settings()
    {
        SwitchMenuTo(eMenuScreen.Settings);
    }

    public void Credits()
    {
        SwitchMenuTo(eMenuScreen.Credits);
    }

}
