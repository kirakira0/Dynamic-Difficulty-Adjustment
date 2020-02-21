using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private int i = 0; 

    public Environment Environment; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Environment.test); 
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.5f);
        //acclimation --> don't even swtich before a minimum number of iterations, stable coing collection percetage 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RepeatCallToEnv() {
        if (i == 0) {
            Environment.Generate("m", "low"); 
        }
        if (i == 1) {
            Environment.Generate("m", "mid"); 
        }
        if (i == 2) {
            Environment.Generate("m", "high"); 
            i = -1; 
        }
        i += 1; 
    }
}
