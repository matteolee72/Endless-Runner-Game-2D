using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float scoreCount;
    public float highScoreCount;

    public Text scoreText;
    public Text highScore;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    public bool shouldDouble;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("Highscore") != null) //Checks if "Highscore" has a float assigned
        {
            highScoreCount = PlayerPrefs.GetFloat("Highscore"); //Sets HighScoreCount as float assigned to "Highscore"
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("Highscore", highScoreCount); //Assigns High Score to string "Highscore"
        }

        scoreText.text = "Score: " + Mathf.Round (scoreCount);
        highScore.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    public void addScore(int pointsToAdd)
    {
        if(shouldDouble)
        {
            pointsToAdd = pointsToAdd * 2;
        }
        scoreCount += pointsToAdd;
    }
}
