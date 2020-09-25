using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool doublePoints;
    public bool safeMode;

    public float powerUpLength;

    private PowerUpManager thePowerUpManager;

    public Sprite[] powerUpSprites;

    // Start is called before the first frame update
    void Start()
    {
        thePowerUpManager = GameObject.FindObjectOfType<PowerUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake ()
    {
        int powerUpSelector = Random.Range(0, 2);

        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelector];

        switch (powerUpSelector)
        {
            case 0: doublePoints = true;
                break;

            case 1: safeMode = true;
                break;
        }

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.name == "Player")
        {
            thePowerUpManager.ActivatePowerUp(doublePoints, safeMode, powerUpLength);
        }
        gameObject.SetActive(false);

    }

}
