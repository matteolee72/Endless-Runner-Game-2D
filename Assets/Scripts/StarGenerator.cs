using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{

    public float distanceBetweenStars;

    public ObjectPooler starPool;

    public void spawnStars (Vector3 StartPosition)
    {
        GameObject star1 = starPool.getPooledObject();
        star1.transform.position = StartPosition;
        star1.SetActive(true);

        GameObject star2 = starPool.getPooledObject();
        star2.transform.position = new Vector3(StartPosition.x - distanceBetweenStars, StartPosition.y, StartPosition.z);
        star2.SetActive(true);

        GameObject star3 = starPool.getPooledObject();
        star3.transform.position = new Vector3(StartPosition.x + distanceBetweenStars, StartPosition.y, StartPosition.z);
        star3.SetActive(true);
    }

}
