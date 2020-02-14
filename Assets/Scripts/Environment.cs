using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    //public ObjectPooler[] platformPool; 
    public GameObject[] platforms; 
    public GameObject player; 
    public GameObject platformGenerationPoint; 

    private float nextActionTime = 0.0f;
    public float period = 0.5f;

    // public Vector3 low; 
    // public Vector3 mid; 
    // public Vector3 high; 

    public int high = 3; 
    public int mid = 0; 
    public int low = -3;  

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

        Generate("short", "mid"); 
 
        //InvokeRepeating("RepeatedGeneration", 1.0f, 1.0f);

    }


    void Update () {
        
    }

    // void RepeatedGeneration()
    // {
    //     //instantiate creates the new platforms 
    //     Instantiate(type[i], new Vector3(platformGenerationPoint.transform.position.x, position[i], 0), Quaternion.identity);
    //     //log the updates platform generation position 
    //     Debug.Log(platformGenerationPoint.transform.position.x);
    //     //increment the index to work through the list 
    //     i++; 

    // }

    void Generate(string platform, string height) {
        int ypos = 0; 
        if (height == "low") {
            ypos = -3; 
        }
        else if (height == "high") {
            ypos = 3; 
        }
        else if (height == "mid") {
            ypos = 0; 
        }
        GameObject type = shortPlatform; 
        if (platform == "short") {
            type = shortPlatform; 
        }
        else if (platform == "medium") {
            type = mediumPlatform;
        }
        else if (platform == "long") {
            type = longPlatform; 
        }
        Instantiate(type, new Vector3(platformGenerationPoint.transform.position.x, ypos, 0), Quaternion.identity);
    }

}
