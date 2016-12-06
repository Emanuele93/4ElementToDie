public static class Constants {
    
    //table of elements' strengths/weaknesses relations
    public static readonly double[,] ELEMENTS_TABLE =
    {
    //def: F    E    W    A     //att:
        { 1.0, 2.0, 1.0, 0.5 }, // F
        { 0.5, 1.0, 2.0, 1.0 }, // E
        { 1.0, 0.5, 1.0, 2.0 }, // W
        { 2.0, 1.0, 0.5, 10 }   // A
    };

    //Stat's minimum FinalStat value
    public const double MIN_EFF_BUFF = 0.1;

    //multiplication factors of the various stats
    public static readonly double[] STAT_K =
        { 1.0, 1.0, 3.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
}
