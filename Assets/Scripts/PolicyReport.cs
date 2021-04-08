using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyReport 
{
    public int policyIndex;
    public List<int> lives; // Ex. [1, 2]
    public List<int> windows; // Ex. [5, 2]
    public List<Queue<float>> aScores; // Most recent acclimation scores.
    public bool deathOccured; 

    public PolicyReport(int policyIndex) {
        this.policyIndex = policyIndex; 
        this.lives = new List<int>(); 
        this.windows = new List<int>(); 
        this.aScores = new List<Queue<float>>(); 
        this.deathOccured = false; 
    }

    public void AddLife(int lifeIndex) {
        this.lives.Add(lifeIndex);
        if (this.lives.Count > 1) {
            this.deathOccured = true; 
        }
    }

    public void AddWindowCount(int windows) {
        this.windows.Add(windows);
    }

    public void AddAScores(Queue<float> aScores) {
        this.aScores.Add(aScores);
    }

    public string GetReport() {
        // Get representation of lives list. 
        string livesString = "[";
        foreach (var s in this.lives) {
            livesString += " " + s + " "; 
        }
        livesString += "]"; 

        // Get representation of window list. 
        int w = 0;
        foreach (var s in this.windows) {
            w += s; 
        }
        string windowString = "[ " + w + " ]"; 

        string result = "";
        if (this.windows.Count > 0) {
            result += "You encountered policy index " + this.policyIndex + 
                ". Index of first life spent on this subpolicy: " + livesString +
                ". Windows encountered in this policy: " + windowString;  
                // + ". Acclimation scores: " + this.aScores; 
            // result += "You encountered policy index " + this.policyIndex;
        } else {
            result += "You did not acclimate to any subpolicies with the lives allotted."; 
        }
        return result; 
    }
}
