using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Environment : MonoBehaviour
{
    public GameObject[] platforms; 
    public GameObject player; 
    public GameObject platformGenerationPoint; 

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
    }


    void Update () {
        
    }

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