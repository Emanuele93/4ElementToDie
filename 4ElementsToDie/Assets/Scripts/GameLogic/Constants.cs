public static class Constants {

    public const int MAX_InventorySize = 4;
    public const int NO_EquipmentTypes = 4;
    public const double MAX_PickupDropRange = 5.0;
    public const double MAX_EnemyAggroRange = 5.0;
    public const double MIN_EffectBuffValue = 0.1;
    public const double MIN_VisibleStatValue = 0;
    public static readonly double[] StatConstantMultiplier =
        { 1.0, 1.0, 3.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

    //table of elements' strengths/weaknesses relations
    public static readonly double[,] ElementsTable =
    {
    //Def: F    E    W    A     //Att:
        { 1.0, 2.0, 1.0, 0.5 }, // F
        { 0.5, 1.0, 2.0, 1.0 }, // E
        { 1.0, 0.5, 1.0, 2.0 }, // W
        { 2.0, 1.0, 0.5, 1.0 }  // A
    };

	public const string MUSIC_Menu = "MenuMusic";
	public const string MUSIC_Background = "BackgroundMusic";
	public const string MUSIC_AirArea = "AirAreaMusic";
	public const string MUSIC_EarthArea = "EarthAreaMusic";
	public const string MUSIC_FireArea = "FireAreaMusic";
	public const string MUSIC_WaterArea = "WaterAreaMusic";
}
