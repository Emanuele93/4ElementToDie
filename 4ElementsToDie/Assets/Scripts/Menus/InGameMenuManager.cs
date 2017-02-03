using POLIMIGameCollective;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameMenuManager : Singleton<InGameMenuManager>
{

    public GameObject selectionCursor;

    [Header("Equip Panel")]
    public Button[] equipButtons = new Button[Constants.NO_EquipmentTypes];
    public Image[] equipIcons = new Image[Constants.NO_EquipmentTypes];
    public Image charSlot;

    [Header("Inventory Panel")]
    public Button[] inventoryButtons = new Button[Constants.MAX_InventorySize];
    public Image[] inventoryIcons = new Image[Constants.MAX_InventorySize];

    [Header("Description Panel")]
    public Text characterText;
    public Text itemText;

    [Header("Commands Info Panel")]
    public GameObject commandsPanel;
    public Text commandsText;

    private CharacterManager player;

    private int selection;
    private int lastSelection;
    private Item selectedItem;

    void OnEnable()
    {
        player = GameplayManager.Instance.m_player.GetComponent<CharacterManager>();
        DrawPlayer();
        DrawItems();
        Select(0);
    }

    public void Select(int index)
    {
        selection = index;
        lastSelection = selection;

        if (selection < inventoryButtons.Length)
        {
            //update selection
            inventoryButtons[selection].Select();
            selectedItem = player.Inventory[selection];

            //move the cursor
            var temp = selectionCursor.transform.position;
            temp.x = inventoryButtons[selection].transform.position.x;
            temp.y = inventoryButtons[selection].transform.position.y - 50;
            selectionCursor.transform.position = temp;

            //update commands panel
            if (selectedItem is Equipment)
            {
                commandsPanel.SetActive(true);
                commandsText.text = "Press   E                     Press   X  " + "\n" + "to   Equip                to   Destroy";
            }
            else if (selectedItem != null)
            {
                commandsPanel.SetActive(true);
                commandsText.text = "Press   X" + "\n" + "to   Destroy";
            }
            else
            {
                commandsPanel.SetActive(false);
            }
        }
        else if (selection <= inventoryButtons.Length + equipButtons.Length)
        {
            //update selection
            equipButtons[selection - inventoryButtons.Length].Select();
            switch (selection)
            {
                case 4:
                    selectedItem = player.Weapon;
                    break;
                case 5:
                    selectedItem = player.Armor;
                    break;
                case 6:
                    selectedItem = player.Accessory;
                    break;
                case 7:
                    selectedItem = player.Garment;
                    break;
            }

            //move the cursor
            var temp = selectionCursor.transform.position;
            temp.x = equipButtons[selection - inventoryButtons.Length].transform.position.x;
            temp.y = equipButtons[selection - inventoryButtons.Length].transform.position.y - 50;
            selectionCursor.transform.position = temp;

            //update commands panel
            if (selectedItem != null)
            {
                commandsPanel.SetActive(true);
                commandsText.text = "Press   Q" + "\n" + "to   Unequip";
            }
            else
            {
                commandsPanel.SetActive(false);
            }
        }
        DrawItemText();
    }

    private void DrawPlayer()
    {
        // character slot
        charSlot.sprite = player.BaseCharacterData.sprite;
        charSlot.GetComponent<Image>().preserveAspect = true;
        charSlot.GetComponent<Animator>().runtimeAnimatorController = player.BaseCharacterData.idleAnimation;
    }

    private void DrawItems()
    {
        // equipment slots
        if (player.Weapon != null)
        {
            equipIcons[0].sprite = player.Weapon.sprite;
            var color = equipIcons[0].color;
            color.a = 1;
            equipIcons[0].color = color;
        }
        else
        {
            equipIcons[0].sprite = null;
            var color = equipIcons[0].color;
            color.a = 0;
            equipIcons[0].color = color;
        }

        if (player.Armor != null)
        {
            equipIcons[1].sprite = player.Armor.sprite;
            var color = equipIcons[1].color;
            color.a = 1;
            equipIcons[1].color = color;
        }
        else
        {
            equipIcons[1].sprite = null;
            var color = equipIcons[1].color;
            color.a = 0;
            equipIcons[1].color = color;
        }

        if (player.Accessory != null)
        {
            equipIcons[2].sprite = player.Accessory.sprite;
            var color = equipIcons[2].color;
            color.a = 1;
            equipIcons[2].color = color;
        }
        else
        {
            equipIcons[2].sprite = null;
            var color = equipIcons[2].color;
            color.a = 0;
            equipIcons[2].color = color;
        }

        if (player.Garment != null)
        {
            equipIcons[3].sprite = player.Garment.sprite;
            var color = equipIcons[3].color;
            color.a = 1;
            equipIcons[3].color = color;
        }
        else
        {
            equipIcons[3].sprite = null;
            var color = equipIcons[3].color;
            color.a = 0;
            equipIcons[3].color = color;
        }


        // inventory slots
        for (int i = 0; i < player.Inventory.Length; i++)
        {
            if (player.Inventory[i] != null)
            {
                inventoryIcons[i].sprite = player.Inventory[i].sprite;
                var color = inventoryIcons[i].color;
                color.a = 1;
                inventoryIcons[i].color = color;
            }
            else
            {
                inventoryIcons[i].sprite = null;
                var color = inventoryIcons[i].color;
                color.a = 0;
                inventoryIcons[i].color = color;
            }
        }
        DrawPlayerText();
    }

    private void DrawItemText()
    {
        string text = "";

        if (selectedItem != null)
        {
            text += selectedItem.itemName + "\n";
            text += selectedItem.rarity;
            if (selectedItem is Equipment)
            {
                text += " " + ((Equipment)selectedItem).element;
                if (selectedItem is Weapon)
                {
                    text += " " + ((Weapon)selectedItem).attackType;
                }
                text += " " + selectedItem.GetType().ToString() + "\n\n";

                // stat buffs
                bool newLine = false;
                for (int i = 0; i < ((Equipment)selectedItem).statBuffs.Length; i++)
                {
                    //positive buffs
                    if (((Equipment)selectedItem).statBuffs[i] > 0)
                    {
                        text += "     + " + ((Equipment)selectedItem).statBuffs[i] + "   " + (StatType)i;
                        
                        if (newLine) text += "\n";
                        else text += "          ";
                        newLine = !newLine;
                    }
                    //negative buffs
                    else if (((Equipment)selectedItem).statBuffs[i] < 0)
                    {
                        text += "     -  " + -((Equipment)selectedItem).statBuffs[i] + "   " + (StatType)i;

                        if (newLine) text += "\n";
                        else text += "          ";
                        newLine = !newLine;
                    }
                }
                text += "\n\n";

                //abilities
                foreach (Ability a in ((Equipment)selectedItem).abilities)
                {
                    text += a.abilityName + " " + "    ";
                }
                text += "\n";
            }
            text += "\n";
            text += selectedItem.description + "\n";
            text += "" + selectedItem.price + " Gold";
        }
        itemText.text = text;
    }

    private void DrawPlayerText()
    {
        // character text
        string text = "";
        text += player.BaseCharacterData.characterName + "\n";
        text += "Element : " + player.Element + "\n\n";
        // primary stats
        double currentVitality = System.Math.Round(player.Stats[(int)StatType.VIT].FinalStat - player.Damage, 1);
        double totalVitality = System.Math.Round(player.Stats[(int)StatType.VIT].FinalStat, 1);
        text += "    VIT : " + currentVitality + " / " + totalVitality + "\n";
        text += "    ATT : " + player.Stats[(int)StatType.ATT].VisibleStat + "\n";
        text += "    DEF : " + player.Stats[(int)StatType.DEF].VisibleStat + "\n";
        text += "    SPD : " + player.Stats[(int)StatType.SPD].VisibleStat + "\n\n";
        // elemental affinities
        double elementsTotal = 0;
        for (int i = 0; i < System.Enum.GetValues(typeof(ElementType)).Length; i++)
        {
            elementsTotal += player.Stats[(int)StatType.FIRE + i].VisibleStat;
        }
        text += "    Elemental affinity :" + "\n";
        text += "    FIRE : " + player.Stats[(int)StatType.FIRE].VisibleStat;
        text += "    EARTH : " + player.Stats[(int)StatType.EARTH].VisibleStat + "\n";
        text += "    WATER : " + player.Stats[(int)StatType.WATER].VisibleStat;
        text += "    AIR : " + player.Stats[(int)StatType.AIR].VisibleStat + "\n";

        characterText.text = text;
    }
    
    void Update()
    {
        //item selection
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Select(lastSelection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            switch (selection)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Select((selection + 1) % inventoryButtons.Length);
                    break;
                case 4:
                case 6:
                    Select(selection + 1);
                    break;
                case 5:
                case 7:
                    Select(selection - 1);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            switch (selection)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Select((selection + inventoryButtons.Length - 1) % inventoryButtons.Length);
                    break;
                case 4:
                case 6:
                    Select(selection + 1);
                    break;
                case 5:
                case 7:
                    Select(selection - 1);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            switch (selection)
            {
                case 0:
                case 1:
                    Select(7);
                    break;
                case 2:
                case 3:
                    Select(6);
                    break;
                case 4:
                    Select(3);
                    break;
                case 5:
                    Select(0);
                    break;
                case 6:
                    Select(4);
                    break;
                case 7:
                    Select(5);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            switch (selection)
            {
                case 0:
                case 1:
                    Select(5);
                    break;
                case 2:
                case 3:
                    Select(4);
                    break;
                case 4:
                    Select(6);
                    break;
                case 5:
                    Select(7);
                    break;
                case 6:
                    Select(3);
                    break;
                case 7:
                    Select(0);
                    break;
            }
        }

        //item interaction
        else if (selection < inventoryButtons.Length)
        {
            if (selectedItem is Equipment)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.RemoveItem(selectedItem);
                    player.Equip((Equipment)selectedItem);
                    DrawItems();
                    Select(selection);
                }

                else if (Input.GetKeyDown(KeyCode.X))
                {
                    player.RemoveItem(selectedItem);
                    DrawItems();
                    Select(selection);
                }
            }
            else if (selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    player.RemoveItem(selectedItem);
                    DrawItems();
                    Select(selection);
                }
            }
        }

        else if (selection < inventoryButtons.Length + equipButtons.Length && selectedItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.Unequip((Equipment)selectedItem);
                DrawItems();
                Select(selection);
            }
        }

    }
}
