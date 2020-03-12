using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class Agent : MonoBehaviour
{
    private int i = 0; 

    public Environment Environment; 
    private ScoreManager ScoreManager; 

    private float totalCoins; 
    private float acclimationScore; 
    private float[] recentAcclimationScores = new float[5];  


    Vector3 v = new Vector3(0, 0, 1);

    public Text totalCoinsText; 
    public Text coinsCollected; 
    public Text acclimationText; 
    public bool acclimated = false;
    public float acclimationThreshold = 0.3F;

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
        totalCoinsText.text = "TOTAL CPS: " + totalCoins.ToString(); 
        acclimationText.text = recentAcclimationScores.Length.ToString(); 
        checkAcclimation(recentAcclimationScores);
        if (acclimated == true) {  //IF ACCLIMATED
            ScoreManager.scoreCount = 0;
            for(int i = 0; i < recentAcclimationScores.Length; i++){
                recentAcclimationScores[i] = 0;
            }
            sequence.Clear(); 
            i = 0; 
            sequence.AddRange(new List<string>() {L, l, L, l});
            //     sequence.AddRange(new List<string>() {L, h, M, m, S, l, M, m});
            CalculateTotalCoins(); 
        }

    }

    public void checkAcclimation (float[] recentAcclimationScores) {
        Debug.Log("In Acclimation Checker");
        float variance = 0;
        float sum = 0;
        if(recentAcclimationScores.Length < 5){
            return;
        }
        for(int i = 0; i < 5; i++){
            sum = sum + recentAcclimationScores[i];
        }  
        float average = sum / 5;
        for(int i = 0; i < 5; i++){
            variance = variance + (float)Math.Pow(recentAcclimationScores[i] - average, 2);
        }
        if(variance <= acclimationThreshold){
            Debug.Log("variance: " + variance);
            acclimated = true;
            Debug.Log("ACCLIMATED!");
        }      
    }

    void RepeatCallToEnv() {
        Environment.Generate(i, sequence);
        if (i == 2) {
            acclimationScore = ScoreManager.scoreCount / totalCoins;
            for(int i = 0; i < recentAcclimationScores.Length; i++){
                if(recentAcclimationScores[i] != 0){
                    recentAcclimationScores[i]= acclimationScore; 
                }
            }
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
