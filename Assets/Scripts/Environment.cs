using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Environment : MonoBehaviour
{
    //public ObjectPooler[] platformPool; 
    public GameObject[] platforms; 
    public GameObject player; 
    public GameObject platformGenerationPoint; 

    // private float nextActionTime = 0.0f;
    // public float period = 0.5f;
 

    public GameObject shortPlatform;
    public GameObject mediumPlatform; 
    public GameObject longPlatform;  

    public string test = "success"; 


    // Start is called before the first frame update
    void Start()
    {
        shortPlatform = platforms[0];
        mediumPlatform = platforms[1];
        longPlatform = platforms[2]; 

        //Generate("short", "mid"); 
 
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

    public void Generate(int index, List<string> sequence) {
        GameObject type = shortPlatform; 
        int ypos = 0;
        if (sequence[index] == "small") {
            type = shortPlatform; 
        }
        else if (sequence[index] == "medium") {
            type = mediumPlatform;
        }
        else if (sequence[index] == "large") {
            type = longPlatform; 
        } 
        if (sequence[index + 1] == "low") {
            ypos = -3; 
        }
        else if (sequence[index + 1] == "high") {
            ypos = 3; 
        }
        else if (sequence[index + 1] == "mid") {
            ypos = 0; 
        }
        
        Instantiate(type, new Vector3(platformGenerationPoint.transform.position.x, ypos, 0), Quaternion.identity);

    }

}