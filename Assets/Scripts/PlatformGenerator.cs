using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    
    public Transform generationPoint;

    private float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public ObjectPooler[] objectPools;
    // randomiser for platform selection
    private int platformSelector;
    private float[] platformWidths;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;

    public float maxHeightChange;
    private float heightChange;

    public CoinGenerator coinGenerator;
    public float randomCoinThreshold;

	// Use this for initialization
	void Start ()
    {
        platformWidths = new float[objectPools.Length];
        
        for(var i = 0; i< objectPools.Length; i++)
        { 
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, objectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, heightChange, transform.position.z);

            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2), transform.position.y, transform.position.z);
        }
	}
}
