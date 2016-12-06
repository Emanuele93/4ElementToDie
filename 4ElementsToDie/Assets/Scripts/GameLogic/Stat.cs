using UnityEngine;

public class Stat : MonoBehaviour {
    
    private StatType m_name;
    private bool hasLinearGrowth = true;

    private double m_finalStat;
    private double m_baseStat;
    private double m_growingRatio;
    private double m_equipBuff;
    private double m_effectBuff;

    #region Getters
    public StatType Name { get { return m_name; } }
    public double FinalStat { get { return m_finalStat; } }
    public double BaseStat { get { return m_baseStat; } }
    public double GrowingRatio { get { return m_growingRatio; } }
    public double EquipBuff { get { return m_equipBuff; } }
    public double EffectBuff { get { return m_effectBuff; } }
    #endregion

    #region Constructor
    public Stat (StatType n, double bS, double gR)
    {
        m_name = n;
        if (m_name == StatType.ATT || m_name == StatType.DEF)
        {
            hasLinearGrowth = false;
        }
        m_baseStat = bS;
        m_growingRatio = gR;
        m_equipBuff = 0f;
        m_effectBuff = 1.0;
        UpdateFinalStat();
    }
    #endregion

    #region Buffers
    public void UpdateEquipBuff (double buff)
    {
        m_equipBuff += buff;
        UpdateFinalStat();
    }

    public void UpdateEffectBuff (double buff)
    {
        m_effectBuff *= buff;
        m_effectBuff = System.Math.Max( m_effectBuff, Constants.MIN_EFF_BUFF );
        UpdateFinalStat();
    }
    #endregion

    #region Updater
    private void UpdateFinalStat()
    {
        double modStat = m_baseStat + m_equipBuff;
        if (!hasLinearGrowth)
        {
            modStat = System.Math.Sqrt(modStat);
        }
        m_finalStat = Constants.STAT_K[(int) m_name] * m_growingRatio * modStat * m_effectBuff;
    }
    #endregion

}