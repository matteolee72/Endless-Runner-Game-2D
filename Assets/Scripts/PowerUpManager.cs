using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    private bool doublePoints;
    private bool safeMode;

    private bool powerUpActive;

    private float powerUpLengthCounter;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    private GameManager theGameManager;

    private float normalPointsPerSecond;
    private float spikeThreshold;

    private PlatformDestroyer[] spikeList;

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = GameObject.FindObjectOfType<ScoreManager>();
        thePlatformGenerator = GameObject.FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
        normalPointsPerSecond = theScoreManager.pointsPerSecond;
        spikeThreshold = thePlatformGenerator.randomSpikeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if(powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;

            if(theGameManager.powerUpReset)
            {
                powerUpLengthCounter = 0;
                theGameManager.powerUpReset = false;
            }

            if(doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 2;
                theScoreManager.shouldDouble = true;
            }

            if(safeMode)
            {
                thePlatformGenerator.randomSpikeThreshold = 0;
            }

            if (powerUpLengthCounter <= 0)
            {

                theScoreManager.pointsPerSecond = normalPointsPerSecond;
                theScoreManager.shouldDouble = false;

                thePlatformGenerator.randomSpikeThreshold = spikeThreshold;

                powerUpActive = false;
            }
        }
    }

    public void ActivatePowerUp(bool points, bool safe, float time)
    {
        powerUpActive = true;

        doublePoints = points;
        safeMode = safe;
        powerUpLengthCounter = time;


        if(safeMode)
        {
            spikeList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }


    }

}
