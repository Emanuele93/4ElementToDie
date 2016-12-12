using POLIMIGameCollective;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelectMenuManager : Singleton<CharSelectMenuManager>
{

    [Header("Basic Characters")]
    public Character[] playableCharacters = new Character[System.Enum.GetValues(typeof(ElementType)).Length];

    [Header("Character Buttons")]
    public Button[] characterButtons = new Button[System.Enum.GetValues(typeof(ElementType)).Length];

    private int selection;

    void Start()
    {
        characterButtons[0].onClick.Invoke();
        Select(0);
    }

    public void Select(int index)
    {
        selection = index;
        characterButtons[selection].Select();
    }

    private void ConfirmSelection()
    {
        GameplayManager.chosenCharacter = playableCharacters[selection];
        SceneManager.LoadScene("Gameplay");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Select((selection + 1) % characterButtons.Length);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Select((selection + characterButtons.Length - 1) % characterButtons.Length);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ConfirmSelection();
        }
    }
}
