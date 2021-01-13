using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public GameObject startPlat;
    public GameObject shortPlat;
    public GameObject medPlat;
    public GameObject longPlat;
    
    private Vector3 platformGenerationPoint;

    void Awake()
    {
        platformGenerationPoint = GameObject.Find("Camera/PlatformGenerationPoint").transform.position;
        Instantiate(shortPlat, new Vector3(platformGenerationPoint.x, platformGenerationPoint.y, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
