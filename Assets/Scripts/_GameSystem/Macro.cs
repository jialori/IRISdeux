public static class Macro {

	/*	
	 *  Assumptions
	 * 	- The last sequence of build scenes are all Level scenes
	 *  - ...
	 *  - 
	 *  - 
	 */	
	
	public const int IDX_GAMELOOP = 0;
	public const int IDX_STARTMENU = 1;
	public const int IDX_GAMEOVERMENU = 2;
	public const int IDX_SETTINGSMENU = 3;	
	// The beginning index of the sequence of level scenes
	public const int IDX_FIRSTLEVEL = 4;


	/* User-defined sets */
	public static int[] IDX_All_STANDARDSTART = {
												IDX_GAMELOOP, 
											  	IDX_STARTMENU

											  	}; // Standard Start


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