using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoTracker
{
	// Tracks the lastest played game's info
	//
	// Used by:
	// 1. gameover Scene for displaying final score
	// 2. player for its current status
	//
	// Tracks these information:
	// - player(s)
	// - score
	// - level
	// - level progress

	private static GameRecord gameRecord;


	public static void NewGameRecord(Player player, int level, AudioSource audio)
	{
		gameRecord = new GameRecord(player, level, audio);  
	}


	public static GameRecord GameRecord
	{
		get => gameRecord;
	}

	
	public static int Level
	{
		get => gameRecord.level;
	}

	public static int Score
	{
		get => gameRecord.score;
	}


	public static Player Player
	{
		get => gameRecord.player;
	}

}
