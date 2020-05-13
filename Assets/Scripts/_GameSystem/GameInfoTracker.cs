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
		get => (gameRecord != null) ? gameRecord.score : 0;
		set => gameRecord.score = (gameRecord != null) ? value : 0;
	}

	public static int Level
	{
		get => (gameRecord != null) ? gameRecord.level : 0;
		set => gameRecord.level = (gameRecord != null) ? value : -1;
	}


	static GameInfoTracker() {}


	public static void NewGameRecord(int level)
	{
		gameRecord = new GameRecord(level);  
	}


	public static void UpdateScore(float newScore)
	{
		Debug.Log("new score: " + newScore.ToString());
		Score = newScore;
		Debug.Log("new score: " + GameInfoTracker.Score.ToString());

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