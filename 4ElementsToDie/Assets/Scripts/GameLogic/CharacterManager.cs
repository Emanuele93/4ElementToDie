using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

    private Character m_baseCharacterData;

    private ElementType m_element;
    private AttackType m_attackType;

    private Stat[] m_stats;
    private double m_damage;

    private Weapon m_weapon;
    private Armor m_armor;
    private Accessory m_accessory;
    private Garment m_garment;

    private Item[] m_inventory;
    private int[] m_keys;
    private int[] m_stones;

    private List<Ability> m_abilities;
    private List<Effect> m_activeEffects;

    #region Getters/Setters
    public Character BaseCharacterData
    {
        get { return m_baseCharacterData; }
        set { m_baseCharacterData = value; }
    }

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

    public double Damage
    {
        get { return m_damage; }
    }

    public Weapon Weapon
    {
        get { return m_weapon; }
    }

    public Armor Armor
    {
        get { return m_armor; }
    }

    public Accessory Accessory
    {
        get { return m_accessory; }
    }

    public Garment Garment
    {
        get { return m_garment; }
    }

    public Item[] Inventory
    {
        get { return m_inventory; }
    }

    public int[] Keys
    {
        get { return m_keys; }
        set { m_keys = value; }
    }

    public int[] Stones
    {
        get { return m_stones; }
        set { m_stones = value; }
    }

    public List<Ability> Abilities
    {
        get { return m_abilities; }
    }

    public List<Effect> ActiveEffects
    {
        get { return m_activeEffects; }
    }
    #endregion

    #region Initializer
    public void InitCharacter(Character c)
    {
        //base character info
        m_baseCharacterData = c;

        //sprite & animations
        gameObject.GetComponent<SpriteRenderer>().sprite = c.sprite;
        // TODO : animations

        //element
        m_element = c.element;

        //attack type
        m_attackType = c.defaultAttackType;

        //stats
        m_stats = new Stat[System.Enum.GetValues(typeof(StatType)).Length];
        for (int i = 0; i < m_stats.Length; i++)
        {
            m_stats[i] = gameObject.AddComponent<Stat>() as Stat;
            m_stats[i].InitStat((StatType)i, c.baseStats[i], c.growingRatios[i]);
        }

        //damage
        m_damage = 0.0;

        //equipments
        Equip(c.weapon);
        Equip(c.armor);
        Equip(c.accessory);
        Equip(c.garment);

        //inventory
        m_inventory = new Item[Constants.MAX_InventorySize];
        foreach (Item i in c.inventory)
        {
            AddItem(i);
        }
        m_keys = new int[System.Enum.GetValues(typeof(ElementType)).Length];
        m_stones = new int[System.Enum.GetValues(typeof(ElementType)).Length];

        //keys
        m_keys = new int[System.Enum.GetValues(typeof(ElementType)).Length];
        for (int i = 0; i < m_keys.Length; i++)
        {
            m_keys[i] = 5;
        }

        //stones
        m_stones = new int[System.Enum.GetValues(typeof(ElementType)).Length];
        m_stones[(int) m_element] += 1;

        //abilities
        m_abilities = new List<Ability>();
        foreach (Ability a in c.abilities)
        {
            AddAbility(a);
        }
    }
    #endregion

    #region Equipment Methods
    public void Equip(Equipment equip)
    {
        if (equip != null)
        {
            if (equip is Weapon)
            {
                if (m_weapon != null)
                {
                    Unequip(m_weapon);
                }
                m_weapon = (Weapon)equip;
                //also set the attack type if equipping a Weapon
                m_attackType = m_weapon.attackType;
            }
            else if (equip is Armor)
            {
                if (m_armor != null)
                {
                    Unequip(m_armor);
                }
                m_armor = (Armor)equip;
            }
            else if (equip is Accessory)
            {
                if (m_accessory != null)
                {
                    Unequip(m_accessory);
                }
                m_accessory = (Accessory)equip;
            }
            else if (equip is Garment)
            {
                if (m_garment != null)
                {
                    Unequip(m_garment);
                }
                m_garment = (Garment)equip;
            }

            //stats
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEquipBuff(equip.statBuffs[i]);
            }

            //abilities
            foreach (Ability a in equip.abilities)
            {
                AddAbility(a);
            }
        }
    }

    public void Unequip(Equipment equip)
    {
        if (equip!= null)
        {
            bool freeSlotFound = false;
            for (int i = 0; !freeSlotFound && i < m_inventory.Length; i++)
            {
                if (m_inventory[i] == null)
                {
                    freeSlotFound = true;
                }
            }

            if (freeSlotFound)
            {
                if (equip is Weapon)
                {
                    m_weapon = null;
                    //also reset the attack type if unequipping a Weapon
                    m_attackType = m_baseCharacterData.defaultAttackType;
                }
                else if (equip is Armor)
                {
                    m_armor = null;
                }
                else if (equip is Accessory)
                {
                    m_accessory = null;
                }
                else if (equip is Garment)
                {
                    m_garment = null;
                }

                //stats
                for (int i = 0; i < m_stats.Length; i++)
                {
                    m_stats[i].UpdateEquipBuff(-equip.statBuffs[i]);
                }

                //abilities
                foreach (Ability a in equip.abilities)
                {
                    RemoveAbility(a);
                }

                AddItem(equip);
            }
        }
    }
    #endregion

    #region Inventory Methods
    public bool AddItem(Item item)
    {
        bool freeSlotFound = false;
        if (item != null)
        {
            for (int i = 0; !freeSlotFound && i < m_inventory.Length; i++)
            {
                if (m_inventory[i] == null)
                {
                    freeSlotFound = true;
                    m_inventory[i] = item;
                }
            }
        }

        return freeSlotFound;
    }

    public bool RemoveItem(Item item)
    {
        bool itemFound = false;
        if (item != null)
        {
            for (int i = 0; !itemFound && i < m_inventory.Length; i++)
            {
                if (m_inventory[i] == item)
                {
                    itemFound = true;
                    m_inventory[i] = null;
                }
            }
        }
        return itemFound;
    }
    #endregion

    #region Ability Methods
    public void AddAbility(Ability ability)
    {
        if (ability != null)
        {
            m_abilities.Add(ability);
            if (ability.trigger == TriggerType.Passive)
            {
                AddActiveEffect(ability.effect);
            }
        }
    }

    public void RemoveAbility(Ability ability)
    {
        if (m_abilities.Contains(ability) && ability != null)
        {
            m_abilities.Remove(ability);
            if (ability.trigger == TriggerType.Passive || ability.target == TargetType.Self)
            {
                RemoveActiveEffect(ability.effect);
            }
        }
    }
    #endregion

    #region ActiveEffect Methods
    public void AddActiveEffect(Effect effect)
    {
        if (effect != null)
        {
            m_activeEffects.Add(effect);
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEffectBuff(effect.statBuffs[i]);
            }
        }
    }

    public void RemoveActiveEffect(Effect effect)
    {
        if (m_activeEffects.Contains(effect) && effect != null)
        {
            m_activeEffects.Remove(effect);
            for (int i = 0; i < m_stats.Length; i++)
            {
                m_stats[i].UpdateEffectBuff(1.0 / effect.statBuffs[i]);
            }
        }
    }
    #endregion

    #region Damage Methods
    public void ApplyDamage(double damage)
    {
        m_damage += damage;
        System.Math.Max(damage, 0.0);
    }

    public bool isDead()
    {
        return (m_damage >= m_stats[(int)StatType.VIT].FinalStat);
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
