using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    public float ACCLIMATION_THRESHOLD = 0.2f; 
    public float PLATFORM_SPEED = 0.1f; 
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

    public Policy p1;
    public Policy p2;
    public Policy p3;

    public List<Policy> policies; 
    public Policy currentPolicy; 
    public int policyIndex = 0; 

    void Start()
    {
        Manager = GameObject.Find("Manager").GetComponent<Manager>();

        p1 = new Policy(1);
        p2 = new Policy(2);
        p3 = new Policy(3);

        // currentPolicy = sbp1;
        List<Policy> policies = new List<Policy>() { p1, p2, p3 };
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
            generateSequence = StartCoroutine(platformGenerator.GenerateSequence(currentPolicy));
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
        // WORK OUT THE MEAN 
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
     * Called when player has acclimated. 
     */ 
    public void NextPolicy() {
                
        policyIndex++; 

        if (policyIndex % 3 == 0) {
            currentPolicy = p1; 
        } else if (policyIndex % 3 == 1) {
            currentPolicy = p2; 
        } else {
            currentPolicy = p3; 
        }       

        Debug.Log("ACCLIMATED");

        StopCoroutine(generateSequence);
        this.acclimated = false;
        this.scores.Clear();
        this.scoreSD = 1;   
        generateSequence = StartCoroutine(platformGenerator.GenerateSequence(currentPolicy));
    }

    /**
     * Once the player has acclimated OR IF THE PLAYER DIES, add the supolicy to the list of 
     * encountered policies. 
    */ 

    public void AddPolicy() {

    }
}
