using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    public float ACCLIMATION_THRESHOLD = 0.2f; 
    public float PLATFORM_SPEED = 0.9f; 
    public Generator platformGenerator; 

    private Coroutine generateSequence;
    public bool acclimated = false; 
    private int coinsCollected = 0;
    public int totalCoinsCollected = 0; 
    public Queue<float> scores = new Queue<float>();
    public float scoreSD = 1;  
    public int subpolicies = 0; 
    public Subpolicy[] allPolicies; 
    
    private Manager Manager;
    private Web Web; 

    public Game gameToGenerate;
    public Round r0, r1, r2, r3, r4, r5, r6, r7, r8, r9;
    public Policy p0, p1, p2, p3, p4, p5;

    public List<Policy> policies; 
    public Policy currentPolicy; 
    public int roundIndex = 0;
    public int policyIndex = 0; 

    public string REPORT = "";

    void Start()
    {
        REPORT += "{\"game\": [{\"round\": [";
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        Web = GameObject.Find("Web").GetComponent<Web>();

        p0 = new Policy(0);
        p1 = new Policy(1);
        p2 = new Policy(2);
        p3 = new Policy(3);
        p4 = new Policy(4);
        p5 = new Policy(5);

        r0 = new Round(new List<Policy>() {p0, p1, p2});
        r1 = new Round(new List<Policy>() {p2, p3, p0});
        r2 = new Round(new List<Policy>() {p1, p0, p5});
        r3 = new Round(new List<Policy>() {p5, p4, p2});
        r4 = new Round(new List<Policy>() {p0, p2, p4});
        r5 = new Round(new List<Policy>() {p3, p1, p2});
        r6 = new Round(new List<Policy>() {p0, p2, p5});
        r7 = new Round(new List<Policy>() {p4, p3, p5});
        r8 = new Round(new List<Policy>() {p4, p1, p0});
        r9 = new Round(new List<Policy>() {p1, p3, p5});

        // gameToGenerate = new Game(new List<Round>() {r0, r1, r2, r3, r4, r5, r6, r7, r8, r9});
        gameToGenerate = new Game(new List<Round>() {r0, r1, r2});

        List<Policy> policies = new List<Policy>() { p0, p1, p2, p3, p4, p5 };
        currentPolicy = policies[policyIndex];

    }

    void Update() {
        // Handle game pause.
        if (Manager.GetPaused()) {
            // StopGeneration();
        }

        if(Input.GetKeyDown(KeyCode.Return) && Manager.GetPaused()) {
            // if cor running 
                // stop 
            if (platformGenerator.GetIsRunning()) {
                StopCoroutine(generateSequence);
            }

            Manager.SetPaused(false);
            generateSequence = StartCoroutine(platformGenerator.GenerateSequence(gameToGenerate.GetPolicy(roundIndex, policyIndex)));
            // Debug.Log("AGENT STARTS COUROUTINE");
        }

    }

    public void StopGeneration() {
        StopCoroutine(generateSequence);
    }

    // SETTERS ----------------------------------------------------------------
    public void IncrementCoinsCollected() {
        this.coinsCollected++;
        this.totalCoinsCollected++;  
    }

    public void ResetCoinsCollected() {
        this.coinsCollected = 0; 
    }

    // GETTERS ----------------------------------------------------------------

    public bool GetIsAcclimated() {
        return acclimated;
    }

    public int GetTotalCoinsCollected() {
        return this.totalCoinsCollected; 
    }

    public string GetCurrentPolicy() {
        return currentPolicy.GetStringRepresentation(); 
    }

    public int GetCoinsPerCurrentWindow() {
        return this.currentPolicy.coinsPerWindow; 
    }

    public int GetCoinsCollected() {
        return this.coinsCollected; 
    }

    public string ScoresToString() {
        string result = ""; 
        foreach (float value in this.scores) {
            result += "[" + value + "] ";
        }
        return result; 
    }

    public float CalculateStandardDeviation() {
        // Get sum of list. 
        float sum = 0; 
        foreach (float value in this.scores) {
            sum += value; 
        }
        float mean = (float)sum/this.scores.Count;
        // For each value, subtract mean and square result. 
        List<float> squaredDifferences = new List<float>();
        foreach (float value in this.scores) {
            squaredDifferences.Add((float)Math.Pow(value - mean, 2)); 
        }
        // Get mean of squared differences.
        float sumOfSquares = 0; 
        foreach (float value in squaredDifferences) {
            sumOfSquares += value; 
        }
        float meanOfSquares = (float)sumOfSquares/squaredDifferences.Count; 
        // Return square root of mean of squares. 
        return (float)Math.Pow(meanOfSquares, 0.5);       
    }

    /**
     * Called when player has acclimated/fallen. 
     */ 
    public Policy NextPolicy(Policy currentPolicy) {

        // Add current policy to report 
        REPORT += currentPolicy.ToString();

        // IF NO DEATH ...
        if (currentPolicy.acclimated) {
            // If no death, make new policy 
            policyIndex++;
            // If reached end of round ...
            if (policyIndex >= gameToGenerate.PoliciesInRound(roundIndex)) {
                REPORT += "]}, {\"round\": [";

                policyIndex = 0;
                roundIndex++;
            }
            else {
                REPORT += ", ";

            }
        // IF DEATH ...
        } else {
            REPORT += "]}, {\"round\": [";
            policyIndex = 0;
            roundIndex++;          
        }


        if (roundIndex >= gameToGenerate.RoundsInGame()) {
            REPORT = REPORT.Substring(0, REPORT.Length - 13);
            REPORT += "]}";
            Manager.HandleGameOver();
            Debug.Log("Game over");   
            Web.WriteData(REPORT);
        }

        Debug.Log(REPORT);

        return gameToGenerate.GetPolicy(roundIndex, policyIndex); 
    }
}
