using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Passive game tracker that is updated by Player

public class GameRecord
{
	public int level;
	public float score;

	public GameRecord(int level)
	{
		this.level = level;
	}
}


public class GameInfoTracker
{
	private static GameRecord gameRecord;

	public static float Score
	{
		get => gameRecord.score;
		set => gameRecord.score = value;
	}

	public static int Level
	{
		get => gameRecord.level;
		set => gameRecord.level = value;
	}


	public static void NewGameRecord(int level)
	{
		gameRecord = new GameRecord(level);  
	}


	public static void UpdateScore(float newScore)
	{
		Score = newScore;
	}
}




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


// GameInfoTracker

	// store pointer to info providers
	// public static Player player;
	// public static int level;
	// public static GameRecord GameRecord

	// {
	// 	get => gameRecord;
	// }
	
	// public static int Level
	// {
	// 	get => gameRecord.level;
	// }

	// public static int Score
	// {
	// 	get => gameRecord.score;
	// }

	// public static Player Player
	// {
	// 	get => gameRecord.player;
	// }


// Game Record
	// public string levelName;
	// public AudioClip song;
	// public string songName;
	// public AudioSource audio;
	// public float audioPlayedTime;
	// public Player player;