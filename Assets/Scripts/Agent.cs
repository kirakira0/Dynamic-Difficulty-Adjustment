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
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RepeatCallToEnv() {
        Environment.Generate("short", "mid"); 
    }
}
