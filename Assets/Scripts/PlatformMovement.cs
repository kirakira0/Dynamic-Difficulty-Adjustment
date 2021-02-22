using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Agent Agent; 
    private Manager Manager; 

    void Start() {
        Agent = GameObject.Find("Agent").GetComponent<Agent>();
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
    }    
    
    void Update()
    {
        if (Manager.GetPaused()) {
            transform.position = new Vector3(0, transform.position.y, 0);
        } else {
            transform.position = new Vector3(transform.position.x - Agent.PLATFORM_SPEED, transform.position.y, 0);
        }        
    }
}