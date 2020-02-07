using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    //public ObjectPooler[] platformPool; 
    public GameObject[] platforms; 
    public GameObject player; 
    public GameObject platformGenerationPoint; 

    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


 
    void Update () {
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
            Instantiate(platforms[0], new Vector3(platformGenerationPoint.transform.position.x, 0, 0), Quaternion.identity);
            // execute block of code here
        }
    }

}
