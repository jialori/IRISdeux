using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRecord
{
	public Player player;
	public int level;
	public AudioSource audio;

	public string levelName;

	// public AudioClip song;
	public string songName;
	public float lastAudioTime; //
	public int score;


	public GameRecord(Player player, int level, AudioSource audio) 
	{
		this.player = player;
		this.level = level;
		this.audio = audio;
	}


	public float GetCurrentAudioTime()
	{
		// if audio is in, return audio.time; else return audiotime directly
		return 0;
	}

}
