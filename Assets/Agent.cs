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
    public float period = 0.5f;

    public Vector3 low; 
    public Vector3 mid; 
    public Vector3 high;  

    public GameObject shortPlatform;
    public GameObject mediumPlatform; 
    public GameObject longPlatform;  
    
    private int i = 0; 
    private int height; 

    private List<GameObject> type = new List<GameObject>(); 
    //private List<Vector3> position = new List<Vector3>(); 
    private List<int> position = new List<int>(); 


    // Start is called before the first frame update
    void Start()
    {
        shortPlatform = platforms[0];
        mediumPlatform = platforms[1];
        longPlatform = platforms[2]; 

        type.AddRange(new GameObject[] {
            shortPlatform, shortPlatform, shortPlatform, mediumPlatform, mediumPlatform, mediumPlatform, longPlatform, longPlatform, longPlatform
        });

        position.AddRange(new int[] {
            -3, 0, 3, -3, 0, 3, -3, 0, 3
        }); 
 

        InvokeRepeating("RepeatedGeneration", 1.0f, 1.0f);


    }


    void Update () {
        
    }

    void RepeatedGeneration()
    {
        //instantiate creates the new platforms 
        Instantiate(type[i], new Vector3(platformGenerationPoint.transform.position.x, position[i], 0), Quaternion.identity);
        //log the updates platform generation position 
        Debug.Log(platformGenerationPoint.transform.position.x);
        //increment the index to work through the list 
        i++; 

    }

}
