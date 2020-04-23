using UnityEngine;
using UnityEngine.UI;

public class UI_Gameover_Score : MonoBehaviour
{
	private Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Start()
    {
        scoreText.text = GameInfoTracker.Score.ToString();
    }
}
