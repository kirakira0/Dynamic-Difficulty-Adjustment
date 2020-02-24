using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private int i = 0; 

    public Environment Environment; 
    // Start is called before the first frame update

    public List<string> sequence = new List<string>();



    void Start()
    {
        Debug.Log(Environment.test); 
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.5f);
        //acclimation --> don't even swtich before a minimum number of iterations, stable coing collection percetage
        sequence.Add("medium");
        sequence.Add("low"); 
        sequence.Add("small"); 
        sequence.Add("mid"); 
        sequence.Add("small");
        sequence.Add("low"); 
        
        Debug.Log(sequence[4]); 
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RepeatCallToEnv() {
        // Environment.Generate("m", "low");
        Environment.Generate(i, sequence); 
        if (i + 2 < sequence.Count) {
            i += 2; 
        }
        else {
            i = 0; 
        }

    }
}
