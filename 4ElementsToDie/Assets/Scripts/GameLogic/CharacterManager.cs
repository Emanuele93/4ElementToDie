using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

    private Character m_baseCharacterData;

    private ElementType m_element;
    private AttackType m_attackType;

    private Stat[] m_stats = new Stat[System.Enum.GetValues(typeof(StatType)).Length];

    private Equipment[] m_equipments;
    private Item[] m_inventory;

    private List<Ability> m_abilities;
    private List<Effect> m_activeEffects;

    #region Getters/Setters
    public ElementType Element
    {
        get { return m_element; }
        set { m_element = value; }
    }

    public AttackType AttackType
    {
        get { return m_attackType; }
        set { m_attackType = value; }
    }

    public Stat[] Stats
    {
        get { return m_stats; }
        set { m_stats = value; }
    }
    public Equipment[] Equipments
    {
        get { return m_equipments; }
        set { m_equipments = value; }
    }

    public Item[] Inventory
    {
        get { return m_inventory; }
        set { m_inventory = value; }
    }

    public List<Ability> Abilities
    {
        get { return m_abilities; }
        set { m_abilities = value; }
    }

    public List<Effect> ActiveEffects
    {
        get { return m_activeEffects; }
        set { m_activeEffects = value; }
    }
    #endregion

    #region Constructor
    public CharacterManager(Character c)
    {
        m_baseCharacterData = c;
        m_element = c.element;
        m_attackType = c.defaultAttackType;
        for (int i = 0; i < m_stats.Length; i++)
        {
            m_stats[i] = new Stat((StatType)i, c.baseStats[i], c.growingRatios[i]);
        }
        for (int i = 0; i < c.equipments.Length; i++)
        {
            Equip(c.equipments[i]);
        }
        m_inventory = c.inventory;
        m_abilities = c.abilities;
    }
    #endregion

    #region Equipment Adder/Remover
    public void Equip(Equipment equip)
    {
        //look for the right slot to equip
        EquipType slot;
        if (equip is Weapon)
        {
            slot = EquipType.Weapon;
            m_attackType = ((Weapon)equip).attackType;
        }
        else if (equip is Armor)
        {
            slot = EquipType.Armor;
        }
        else if (equip is Accessory)
        {
            slot = EquipType.Accessory;
        }
        else if (equip is Garment)
        {
            slot = EquipType.Garment;
        }
        else
        {
            throw new System.Exception("Error: unknown Equipment type");
        }
        if (m_equipments[(int)slot] != null)
        {
            //stats
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEquipBuff( equip.statBuffs[i] - m_equipments[(int)slot].statBuffs[i] );
            }
            //abilities
            for (int i= 0; i < m_equipments.Length; i++)
            {
                RemoveAbility(m_equipments[(int)slot].abilities[i]);
            }
            for (int i = 0; i < equip.abilities.Count; i++)
            {
                AddAbility(equip.abilities[i]);
            }
        }
        else
        {
            //stats
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEquipBuff( equip.statBuffs[i] );
            }
            //abilities
            for (int i = 0; i < equip.abilities.Count; i++)
            {
                AddAbility(equip.abilities[i]);
            }
        }
        //set the new equipment
        m_equipments[(int)slot] = equip;
    }

    public void Unequip(EquipType type)
    {
        if (m_equipments[(int)type] != null)
        {
            //reset to the default attack type if removing a Weapon
            if (type == EquipType.Weapon)
                m_attackType = m_baseCharacterData.defaultAttackType;
            //stats
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEquipBuff( - m_equipments[(int)type].statBuffs[i] );
            }
            //abilities
            for (int i = 0; i < m_equipments[(int)type].abilities.Count; i++)
            {
                RemoveAbility(m_equipments[(int)type].abilities[i]);
            }
        }
        //remove the equipment
        m_equipments[(int)type] = null;
    }
    #endregion

    #region Item Adder/Remover
    public void AddItem(Item item)
    {
        bool isFull = true;
        for (int i = 0; !isFull || i < m_inventory.Length; i++)
        {
            if (m_inventory[i] == null)
            {
                isFull = false;
                m_inventory[i] = item;
            }
        }
    }

    public void RemoveItem(int slot)
    {
        try
        {
            m_inventory[slot] = null;
        }
        catch
        {
            throw new System.Exception("Error: invalid inventory index");
        }
    }
    #endregion

    #region Ability Adder/Remover
    public void AddAbility(Ability ability)
    {
        m_abilities.Add(ability);
    }

    public void RemoveAbility(Ability ability)
    {
        m_abilities.Remove(ability);
    }
    #endregion
    
    #region ActiveEffect Adder/Remover
    public void AddActiveEffect(Effect effect)
    {
        if (effect != null)
        {
            m_activeEffects.Add(effect);
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEffectBuff( effect.statBuffs[i] );
            }
        }
    }

    public void RemoveActiveEffect(Effect effect)
    {
        if (m_activeEffects.Contains(effect) && effect != null)
        {
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEffectBuff( 1.0 / effect.statBuffs[i]);
            }
        }
        m_activeEffects.Remove(effect);
    }
    #endregion

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
