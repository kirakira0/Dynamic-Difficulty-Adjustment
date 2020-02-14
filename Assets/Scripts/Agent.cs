using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Environment Environment; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Environment.test); 
        Environment.Generate("short", "mid"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
