using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlatformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController thePlayer;
    private Vector3 PlayerStartPoint;

    private PlatformDestroyer[] platformList;

    public ScoreManager theScoreManager;

    public DeathMenu theDeathMenu;

    public GameObject pauseButton;

    public bool powerUpReset;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = PlatformGenerator.position;
        PlayerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);

        theDeathMenu.gameObject.SetActive(true);

        pauseButton.SetActive(false);

        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        theDeathMenu.gameObject.SetActive(false);
        pauseButton.SetActive(true);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = PlayerStartPoint;
        PlatformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
        powerUpReset = true;
    }

    /*public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = PlayerStartPoint;
        PlatformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }*/

}
