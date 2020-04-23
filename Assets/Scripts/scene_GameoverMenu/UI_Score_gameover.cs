using UnityEngine;
using UnityEngine.UI;

public class UI_Score_gameover : MonoBehaviour
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
