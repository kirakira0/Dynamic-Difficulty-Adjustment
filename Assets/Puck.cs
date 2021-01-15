using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public GameObject startPlat;
    public GameObject shortPlat;
    public GameObject medPlat;
    public GameObject longPlat;
    
    private float platformGeneratorX; 
    private float platformGeneratorY; 
    private Vector3 lowPoint;
    private Vector3 midPoint;
    private Vector3 highPoint;

    private enum Height {
        low,
        mid,
        high,
    }

    void Awake()
    {
        platformGeneratorX = GameObject.Find("Camera/PlatformGenerationPoint").transform.position.x;
        platformGeneratorY = GameObject.Find("Camera/PlatformGenerationPoint").transform.position.y;

        // UpdateCoordinates();
        // StartCoroutine(Generate(shortPlat, highPoint));
        StartCoroutine(Generate(shortPlat, Height.high));

 

    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateCoordinates() {
        platformGeneratorX = GameObject.Find("Camera/PlatformGenerationPoint").transform.position.x;
        lowPoint = new Vector3(platformGeneratorX, platformGeneratorY - 3, 0); 
        midPoint = new Vector3(platformGeneratorX, platformGeneratorY, 0);
        highPoint = new Vector3(platformGeneratorX, platformGeneratorY + 3, 0); 
    }

    IEnumerator Generate(GameObject platform, Height height) {
        for (int i = 0; i < 100; i++) {
            yield return new WaitForSeconds(1f);

            Vector3 spawnPoint;

            platformGeneratorX = GameObject.Find("Camera/PlatformGenerationPoint").transform.position.x;

            if (height == Height.high) {
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY + 3, 0); 
            } else {
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY + 3, 0); 
            }


            // UpdateCoordinates();
            // Debug.Log(spawnPoint);
            Instantiate(platform, spawnPoint, Quaternion.identity);
        } 
    }
}
