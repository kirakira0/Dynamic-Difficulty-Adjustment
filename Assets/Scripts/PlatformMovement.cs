using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Agent AGENT; 

    void Start() {
        AGENT = GameObject.Find("Agent").GetComponent<Agent>();
    }    
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x - AGENT.PLATFORM_SPEED, transform.position.y, 0);
        
    }
}