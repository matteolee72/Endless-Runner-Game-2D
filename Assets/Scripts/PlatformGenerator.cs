using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int PlatformSelector;
    private float[] platformWidths;

    public ObjectPooler[] objPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    public StarGenerator theStarGenerator;
    public float randomStarThreshold;

    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    public float powerUpHeight;
    public ObjectPooler powerUpPool;
    public float powerUpThreshold;


    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[objPools.Length];

        for (int i = 0; i < objPools.Length; i++)
        {
            platformWidths[i] = objPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theStarGenerator = FindObjectOfType<StarGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            PlatformSelector = Random.Range(0, objPools.Length);

            heightChange = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } 
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if(Random.Range(0f, 100f) < powerUpThreshold)
            {
                GameObject newPowerUp = powerUpPool.getPooledObject();

                newPowerUp.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight / 2, powerUpHeight), 0f);

                newPowerUp.SetActive(true);
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[PlatformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(/*thePlatform*/ thePlatforms[PlatformSelector], transform.position, transform.rotation);


            GameObject newPlatform = objPools[PlatformSelector].getPooledObject(); //Using getPooledObject Function created in ObjectPooler.cs to get an inactive Pooled Object 

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if(Random.Range(0f, 100f) < randomStarThreshold)
            {
                theStarGenerator.spawnStars(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }
            
            if(Random.Range(0f, 100f) < randomSpikeThreshold)
            {

                float spikePositionX = Random.Range(-platformWidths[PlatformSelector] / 2 + 1f, platformWidths[PlatformSelector] / 2 - 1f);

                Vector3 spikePosition = new Vector3(spikePositionX, 0.5f, 0f);
                GameObject newSpike = spikePool.getPooledObject();
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);

            }



            transform.position = new Vector3(transform.position.x + (platformWidths[PlatformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}
