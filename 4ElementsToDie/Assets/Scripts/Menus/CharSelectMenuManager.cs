using POLIMIGameCollective;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CharSelectMenuManager : Singleton<CharSelectMenuManager>
{

    public GameObject selectionCursor;

    [Header("Character Buttons")]
    public Button[] characterButtons = new Button[System.Enum.GetValues(typeof(ElementType)).Length];

    [Header("Basic Characters")]
    public Character[] playableCharacters = new Character[System.Enum.GetValues(typeof(ElementType)).Length];
    
    private int selection;
    private int lastSelection;
    
    void Start()
    {
        Select(0);
    }

    void OnEnable()
    {
        Select(0);
    }

    public void Select(int index)
    {
        selection = index;
        lastSelection = selection;

		characterButtons[selection].Select();

        //move the cursor
        var temp = selectionCursor.transform.position;
        temp.x = characterButtons[selection].transform.position.x;
        selectionCursor.transform.position = temp;
    }

    private void ConfirmSelection()
    {
        GameplayManager.chosenCharacter = playableCharacters[selection];
        MusicManager.Instance.StopAll();
        MusicManager.Instance.PlayMusic(Constants.MUSIC_Background);
        SceneManager.LoadScene("Gameplay");
    }
    
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Select(lastSelection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
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
