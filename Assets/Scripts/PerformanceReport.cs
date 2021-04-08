using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceReport 
{
    // TODO: Use a platform object as opposed to a list of strings.  
    public int index; 
    private List<Platform> sequence = new List<Platform>(); // type, height
    private int windows;
    private int coinsCollected;
    private int totalCoins; // Update at the end of each window.
    private float time; 
    private List<float> perWindowScore; 
    private int deathsPerSequence; 

    public PerformanceReport(int index, List<Platform> sequence) {
        this.index = index; 
        this.sequence = sequence;
        this.windows = 0;
        this.coinsCollected = 0;
        this.totalCoins = 0; 
        this.time = 0f; 
        this.perWindowScore = new List<float>();     
        this.deathsPerSequence = 0; 
    }

}
