using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private int i = 0; 

    public Environment Environment; 
    public CoinGenerator CoinGenerator;
    Vector3 v = new Vector3(0, 0, 1);
    // Start is called before the first frame update

    public List<string> sequence = new List<string>();

    private string s = "small"; 
    private string me = "medium"; 
    private string l = "long"; 
    private string lo = "low"; 
    private string mi = "mid"; 
    private string h = "high"; 

    void Start()
    {
        Debug.Log(Environment.test); 
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.5f);
        //acclimation --> don't even swtich before a minimum number of iterations, stable coing collection percetage
        sequence.AddRange(new List<string>() {me, lo, s, mi, s, lo});


        
        Debug.Log(sequence[4]); 
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            sequence.Clear(); 
            sequence.AddRange(new List<string>() {l, h, me, mi, s, lo, me, mi});
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            sequence.Clear(); 
            sequence.AddRange(new List<string>() {me, lo, s, mi, s, lo});
        }
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
