using UnityEngine;

public class Stat : MonoBehaviour {
    
    private StatType m_name;
    private bool hasLinearGrowth = true;

    private double m_finalStat;
    private double m_visibleStat;
    private double m_baseStat;
    private double m_growingRatio;
    private double m_equipBuff;
    private double m_effectBuff;

    #region Getters
    public StatType Name { get { return m_name; } }
    public double FinalStat { get { return m_finalStat; } }
    public double VisibleStat { get { return m_visibleStat; } }
    public double BaseStat { get { return m_baseStat; } }
    public double GrowingRatio { get { return m_growingRatio; } }
    public double EquipBuff { get { return m_equipBuff; } }
    public double EffectBuff { get { return m_effectBuff; } }
    #endregion

    #region Initializer
    public void InitStat(StatType n, double bS, double gR)
    {
        m_name = n;
        if (m_name == StatType.ATT || m_name == StatType.DEF)
        {
            hasLinearGrowth = false;
        }
        m_baseStat = bS;
        m_growingRatio = gR;
        m_equipBuff = 0;
        m_effectBuff = 1;
        UpdateStatValues();
    }
    #endregion

    #region Buffers
    public void UpdateEquipBuff (double buff)
    {
        m_equipBuff += buff;
        UpdateStatValues();
    }

    public void UpdateEffectBuff(double buff)
    {
        m_effectBuff += buff / 100;
        UpdateStatValues();
    }
    #endregion

    #region Updater
    private void UpdateStatValues()
    {
        m_visibleStat = m_baseStat + m_equipBuff;
        m_visibleStat = System.Math.Max(m_visibleStat, 0);

        double modStat = m_visibleStat;
        if (!hasLinearGrowth)
        {
            modStat = System.Math.Sqrt(modStat);
        }

        m_finalStat =
            Constants.StatConstantMultiplier[(int)m_name]
            * m_growingRatio
            * modStat
            * System.Math.Max(m_effectBuff, 0);
    }
    #endregion

}