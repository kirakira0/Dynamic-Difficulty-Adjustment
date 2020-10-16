using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Agent : MonoBehaviour
{
    private int i = 0; 

    public Environment Environment; 
    private ScoreManager ScoreManager; 

    private float totalCoins; 
    private float acclimationScore; 
    private Queue recentAcclimationScores = new Queue();  


    Vector3 v = new Vector3(0, 0, 1);

    public Text totalCoinsText; 
    public Text coinsCollected; 
    public Text acclimationText; 

    public List<string> sequence = new List<string>();

    private string S = "small"; 
    private string M = "medium"; 
    private string L = "long"; 
    private string l = "low"; 
    private string m = "mid"; 
    private string h = "high"; 

    void Start()
    {
        ScoreManager = FindObjectOfType<ScoreManager>();
        Debug.Log(Environment.test); 
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.5f);
        //acclimation --> don't even swtich before a minimum number of iterations, stable coing collection percetage
        // sequence.AddRange(new List<string>() {M, l, S, m, S, l});
        sequence.AddRange(new List<string>() {M, l, M, m});
        CalculateTotalCoins();    
        Debug.Log(i); 
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Return)) {
        //     CancelInvoke();
        // }

        totalCoinsText.text = "TOTAL CPS: " + totalCoins.ToString(); 
        acclimationText.text = recentAcclimationScores.Count.ToString(); 

        if (recentAcclimationScores.Count > 2) {  //IF ACCLIMATED
            ScoreManager.scoreCount = 0;
            recentAcclimationScores.Clear(); 
            sequence.Clear(); 
            i = 0; 
            sequence.AddRange(new List<string>() {L, l, L, l});
            //     sequence.AddRange(new List<string>() {L, h, M, m, S, l, M, m});
            CalculateTotalCoins(); 
        }

    }

    void RepeatCallToEnv() {
        Environment.Generate(i, sequence);
        if (i == 2) {
            acclimationScore = ScoreManager.scoreCount / totalCoins;
            recentAcclimationScores.Enqueue(acclimationScore); 
            ScoreManager.scoreCount = 0;
            acclimationScore = 0;   
        } 
        if (i + 2 < sequence.Count) {
            i += 2; 
        }
        else {  //END OF THE SEQUENCE
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
