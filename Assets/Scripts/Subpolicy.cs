using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subpolicy 
{
    // TODO: Use a platform object as opposed to a list of strings. 
    private List<Platform> sequence = new List<Platform>(); // type, height
    private int windows;
    private int coinsCollected;
    private int totalCoins; // Update at the end of each window.
    private float time; 
    private List<float> perWindowScore; 
    private int deathsPerSequence; 

    public Subpolicy(List<Platform> sequence) {
        this.sequence = sequence;
        this.windows = 0;
        this.coinsCollected = 0;
        this.totalCoins = this.calculateCoinsPerSequence(sequence); 
        this.time = 0f; 
        this.perWindowScore = new List<float>();     
        this.deathsPerSequence = 0; 
    }

    public int calculateCoinsPerSequence(List<Platform> sequence) {
        int total = 0; 
        for (int i = 0; i < sequence.Count; i++) {
            total += sequence[i].getNumCoins();
        }
        return total; 
    }

    // GETTERS ----------------------------------------------------------------
    public List<Platform> getSequence() {
        return this.sequence;
    }

    public int GetTotalCoins() {
        return this.totalCoins; 
    }

    public int getWindows() {
        return this.windows;
    }

    public string GetStringRepresentation() {
        string result = "";
        for (int i = 0; i < this.sequence.Count; i++) {
            result += "[" + this.sequence[i].getWidth() + " " + this.sequence[i].getHeight() + "]";
        }
        return result;
    }
 
    // SETTERS ----------------------------------------------------------------
    public void IncrementWindowCount() {
        this.windows++;
    }


}
