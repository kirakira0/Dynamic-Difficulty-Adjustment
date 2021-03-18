using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReport 
{
    public int roundNumber; 
    public string sequence;
    public int subpolicyIndex;  
    public int windows; 
    public Queue<float> acclimationScores; 
    public int livesAtStart;
    public bool acclimated; 

    public RoundReport(int roundNumber, string sequence, int subpolicyIndex, int windows, Queue<float> acclimationScores, int livesAtStart, bool acclimated) {
        this.roundNumber = roundNumber; 
        this.sequence = sequence; 
        this.subpolicyIndex = subpolicyIndex;
        this.windows = windows; 
        this.acclimationScores = acclimationScores; 
        this.livesAtStart = livesAtStart; 
        this.acclimated = acclimated; 
    }

    public string GetReport() {
        string accl = this.acclimated ? " acclimate to the policy." : " not acclimate to the policy.";
        return "In round " + this.roundNumber + ", you encountered sequence #" + this.subpolicyIndex +
                ". You played through " + this.windows + " full windows and you did " + accl; 
    }


}