using POLIMIGameCollective;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManager : Singleton<InGameMenuManager>
{
    
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
    public Text commandInfoText;

    private CharacterManager player;

    private int selection;
    private Item selectedItem;

    void Start()
    {
        player = GameplayManager.Instance.m_player.GetComponent<CharacterManager>();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        DrawPlayer();
        DrawItems();
        //Select(0);
    }

    public void Select(int index)
    {
        selection = index;
        if (selection < inventoryButtons.Length)
        {
            inventoryButtons[selection].Select();
            selectedItem = player.Inventory[selection];

            if (selectedItem is Equipment)
            {
                commandInfoText.text = "Press E to equip, press T to throw away";
            }
            else if (selectedItem != null)
            {
                commandInfoText.text = "Press T to throw away";
            }
            else
            {
                commandInfoText.text = "";
            }
        }
        else if (selection <= inventoryButtons.Length + equipButtons.Length)
        {
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

            if (selectedItem != null)
            {
                commandInfoText.text = "Press U to unequip, press T to throw away";
            }
            else
            {
                commandInfoText.text = "";
            }
        }
        DrawItemText();
    }

    private void DrawPlayer()
    {
        // character slot
        charSlot.sprite = player.BaseCharacterData.sprite;
        charSlot.GetComponent<Image>().preserveAspect = true;
        //charSlot.GetComponent<Animator>().runtimeAnimatorController = player.BaseCharacterData.idleAnimation;
    }

    private void DrawItems()
    {
        // equipment slots
        if (player.Weapon != null)
        {
            equipIcons[0].sprite = player.Weapon.sprite;
            Color color = equipIcons[0].color;
            color.a = 1;
            equipIcons[0].color = color;
        }
        else
        {
            equipIcons[0].sprite = null;
            Color color = equipIcons[0].color;
            color.a = 0;
            equipIcons[0].color = color;
        }

        if (player.Armor != null)
        {
            equipIcons[1].sprite = player.Armor.sprite;
            Color color = equipIcons[1].color;
            color.a = 1;
            equipIcons[1].color = color;
        }
        else
        {
            equipIcons[1].sprite = null;
            Color color = equipIcons[1].color;
            color.a = 0;
            equipIcons[1].color = color;
        }

        if (player.Accessory != null)
        {
            equipIcons[2].sprite = player.Accessory.sprite;
            Color color = equipIcons[2].color;
            color.a = 1;
            equipIcons[2].color = color;
        }
        else
        {
            equipIcons[2].sprite = null;
            Color color = equipIcons[2].color;
            color.a = 0;
            equipIcons[2].color = color;
        }

        if (player.Garment != null)
        {
            equipIcons[3].sprite = player.Garment.sprite;
            Color color = equipIcons[3].color;
            color.a = 1;
            equipIcons[3].color = color;
        }
        else
        {
            equipIcons[3].sprite = null;
            Color color = equipIcons[3].color;
            color.a = 0;
            equipIcons[3].color = color;
        }


        // inventory slots
        for (int i = 0; i < player.Inventory.Length; i++)
        {
            if (player.Inventory[i] != null)
            {
                inventoryIcons[i].sprite = player.Inventory[i].sprite;
                Color color = inventoryIcons[i].color;
                color.a = 1;
                inventoryIcons[i].color = color;
            }
            else
            {
                inventoryIcons[i].sprite = null;
                Color color = inventoryIcons[i].color;
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
                for (int i = 0; i < ((Equipment)selectedItem).statBuffs.Length; i++)
                {
                    //positive buffs
                    if (((Equipment)selectedItem).statBuffs[i] > 0)
                    {
                        text += "    + " + ((Equipment)selectedItem).statBuffs[i] + " " + (StatType)i;
                    }
                    //negative buffs
                    else if (((Equipment)selectedItem).statBuffs[i] < 0)
                    {
                        text += "    -  " + -((Equipment)selectedItem).statBuffs[i] + " " + (StatType)i;
                    }
                }
                text += "\n\n";

                //abilities
                foreach (Ability a in ((Equipment)selectedItem).abilities)
                {
                    text += a.abilityName + " " + a.level + "    ";
                }
                text += "\n";
            }
            text += "\n";
            text += selectedItem.description + "\n";
            text += "[" + selectedItem.price + " Gold]";
        }
        itemText.text = text;
    }

    private void DrawPlayerText()
    {
        // character text
        string text = "";
        text += player.BaseCharacterData.characterName + "\n";
        text += "Element : " + player.Element + "\n\n";
        double currentVitality = player.Stats[(int)StatType.VIT].FinalStat - player.Damage;
        text += "    VIT : " + currentVitality + " / " + player.Stats[(int)StatType.VIT].FinalStat + "\n";
        text += "    ATT : " + player.Stats[(int)StatType.ATT].VisibleStat + "\n";
        text += "    DEF : " + player.Stats[(int)StatType.DEF].VisibleStat + "\n";
        text += "    SPD : " + player.Stats[(int)StatType.SPD].VisibleStat + "\n\n";
        text += "    Elemental affinity :" + "\n";
        text += "    FIRE : " + player.Stats[(int)StatType.FirePOW].VisibleStat;
        text += "    EARTH : " + player.Stats[(int)StatType.EarthPOW].VisibleStat + "\n";
        text += "    WATER : " + player.Stats[(int)StatType.WaterPOW].VisibleStat;
        text += "    AIR : " + player.Stats[(int)StatType.AirPOW].VisibleStat + "\n";

        //abilities
        for (int i = 0; i < player.Abilities.Count; i++)
        {
            text += player.Abilities[i].abilityName + " " + player.Abilities[i].level + "\n";
        }

        //abilities
        foreach (Ability a in player.Abilities)
        {
            text += a.abilityName + " " + a.level + "\n";
        }
        characterText.text = text;
    }
    
    void Update()
    {
        if (selection < inventoryButtons.Length)
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

                else if (Input.GetKeyDown(KeyCode.T))
                {
                    player.RemoveItem(selectedItem);
                    DrawItems();
                    Select(selection);
                }
            }
            else if (selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    player.RemoveItem(selectedItem);
                    DrawItems();
                    Select(selection);
                }
            }
        }

        else if (selection < inventoryButtons.Length + equipButtons.Length && selectedItem != null)
        {

            if (Input.GetKeyDown(KeyCode.U))
            {
                player.Unequip((Equipment)selectedItem);
                DrawItems();
                Select(selection);
            }

            else if (Input.GetKeyDown(KeyCode.T))
            {
                player.RemoveItem(selectedItem);
                DrawItems();
                Select(selection);
            }
        }

    }
}
