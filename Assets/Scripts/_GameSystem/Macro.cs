public static class Macro {
	
	public const int IDX_GAMELOOP = 0;
	public const int IDX_STARTMENU = 1;
	public const int IDX_GAMEOVERMENU = 2;
	public const int IDX_SETTINGSMENU = 3;	
	// Assumption:
	//  The last sequence of build scenes are all Level scenes
	//  beginning at this index
	public const int IDX_FIRSTLEVEL = 4;

	public static int[] IDX_All_FRESHSTART = {IDX_GAMELOOP, 
											  IDX_STARTMENU}; // starting the app

	private static int[] range_menus = {1, 3};
	private static int[] range_levels = {4, 5};


	public static bool IsMenu(int buildIndex)
	{
		return (buildIndex >= range_menus[0]  && buildIndex <= range_menus[1]);
	}

	public static bool IsLevel(int buildIndex)
	{
		return (buildIndex >= range_levels[0]  && buildIndex <= range_levels[1]);
	}
}