using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float PLATFORM_SPEED = 0.1f; 
    public Generator platformGenerator; 

    private Coroutine generateSequence;
    public bool acclimated = false; 
    private int coinsCollected = 0;
    public Queue<float> scores = new Queue<float>();
    public float scoreSD = 1;  
    
    private Logger Logger;
    private Manager Manager; 

    private Subpolicy sbp1;
    private Subpolicy sbp2;
    private Subpolicy sbp3;
    private Subpolicy currentSubpolicy; 
    public List<Subpolicy> sbpList; 
    public int policyIndex = 0; 

    public List<Platform> sqn1 = new List<Platform>(); 
    public List<Platform> sqn2 = new List<Platform>(); 
    public List<Platform> sqn3 = new List<Platform>(); 



    // Start is called before the first frame update
    void Start()
    {
        Logger = GameObject.Find("Logger").GetComponent<Logger>(); 
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        Logger.Start();

        sqn1 = new List<Platform>() {
            new Platform(Width.Medium, Height.Low),
            new Platform(Width.Short, Height.High),
            new Platform(Width.Long, Height.Middle)
        };  
        sbp1 = new Subpolicy(sqn1);

        sqn2 = new List<Platform>() {
            new Platform(Width.Medium, Height.Low),
            new Platform(Width.Short, Height.Low),
            new Platform(Width.Long, Height.High),
            new Platform(Width.Short, Height.High)
        };  
        sbp2 = new Subpolicy(sqn2);

        sqn3 = new List<Platform>() {
            new Platform(Width.Short, Height.Middle),
            new Platform(Width.Long, Height.Middle),
            new Platform(Width.Medium, Height.Middle)
        };  
        sbp3 = new Subpolicy(sqn3);

        // currentSubpolicy = sbp1;
        List<Subpolicy> sbpList = new List<Subpolicy>() { sbp1, sbp2, sbp3 };
        currentSubpolicy = sbpList[policyIndex];

    }

    void Update() {

        // Handle game pause.
        if (Manager.GetPaused()) {
            // StopGeneration();
        }


        if(Input.GetKeyDown(KeyCode.Return) && Manager.GetPaused()) {
            Manager.SetPaused(false);
            generateSequence = StartCoroutine(platformGenerator.GenerateSequence(currentSubpolicy));
            // Debug.Log("AGENT STARTS COUROUTINE");
        }

    }

    public void StopGeneration() {
        StopCoroutine(generateSequence);
    }

    // SETTERS ----------------------------------------------------------------
    public void IncrementCoinsCollected() {
        this.coinsCollected++; 
    }

    public void ResetCoinsCollected() {
        this.coinsCollected = 0; 
    }



    // GETTERS ----------------------------------------------------------------

    public bool GetIsAcclimated() {
        return acclimated;
    }

    public string GetCurrentSubpolicy() {
        return currentSubpolicy.GetStringRepresentation(); 
    }

    public int GetCoinsPerCurrentWindow() {
        return this.currentSubpolicy.GetTotalCoins(); 
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
            currentSubpolicy = sbp1; 
        } else if (policyIndex % 3 == 1) {
            currentSubpolicy = sbp2; 
        } else {
            currentSubpolicy = sbp3; 
        }       

        Debug.Log("ACCLIMATED");

        StopCoroutine(generateSequence);
        this.acclimated = false;
        this.scores.Clear();
        this.scoreSD = 1;   
        generateSequence = StartCoroutine(platformGenerator.GenerateSequence(currentSubpolicy));
    }
}
