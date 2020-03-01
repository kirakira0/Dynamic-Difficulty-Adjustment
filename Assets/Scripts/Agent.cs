using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Agent : MonoBehaviour
{
    private int i = 0; 

    public Environment Environment; 
    public CoinGenerator CoinGenerator;
    Vector3 v = new Vector3(0, 0, 1);
    // Start is called before the first frame update
    public Text totalCoinsText; 
    private int totalCoins; 

    public List<string> sequence = new List<string>();

    private string S = "small"; 
    private string M = "medium"; 
    private string L = "long"; 
    private string l = "low"; 
    private string m = "mid"; 
    private string h = "high"; 

    void Start()
    {
        Debug.Log(Environment.test); 
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.5f);
        //acclimation --> don't even swtich before a minimum number of iterations, stable coing collection percetage
        sequence.AddRange(new List<string>() {M, l, S, m, S, l});

        CalculateTotalCoins(); 
        
        Debug.Log(sequence[4]); 
 
    }

    // Update is called once per frame
    void Update()
    {
        totalCoinsText.text = "TOTAL CPS: " + totalCoins.ToString(); 

        if (Input.GetKeyDown(KeyCode.A)) {
            sequence.Clear(); 
            i = 0; 
            sequence.AddRange(new List<string>() {L, h, M, m, S, l, M, m});
            CalculateTotalCoins(); 
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            sequence.Clear(); 
            i = 0; 
            sequence.AddRange(new List<string>() {L, l, L, l});
            CalculateTotalCoins(); 
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

    void CalculateTotalCoins() {
        totalCoins = 0; 
        for (int j = 0; j < sequence.Count; j += 2) {
            if (sequence[j] == "long") {
                totalCoins += 7; 
            }
            else if (sequence[j] == "medium") {
                totalCoins += 5; 
            }
            else if (sequence[j] == "small") {
                totalCoins += 3; 
            }
        }
    }
}
