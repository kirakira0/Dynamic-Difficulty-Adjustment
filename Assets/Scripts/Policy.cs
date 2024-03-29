﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy
{
    public int index;
    public List<Platform> sequence = new List<Platform>(); // type, height 
    public int coinsPerWindow;

    // Variables that can only be filled after the player has completed a policy. 
    public int roundsAtStart;
    public List<float> scores = new List<float>(); // Get number of encountered windows with scores.Count.
    public bool acclimated; // False indicates that the player died in this policy. 

    public Policy(int index) {
        this.index = index; 
        this.acclimated = false;
        switch (this.index) {
            case 0:
                this.sequence = new List<Platform>() {
                    new Platform(Width.Short, Height.Middle),
                    new Platform(Width.Medium, Height.Low)
                };  
                break;
            case 1:
                this.sequence = new List<Platform>() {
                    new Platform(Width.Medium, Height.Low),
                    new Platform(Width.Short, Height.High),
                    new Platform(Width.Long, Height.Middle)
                }; 
                break;
            case 2:
                this.sequence = new List<Platform>() {
                    new Platform(Width.Medium, Height.Low),
                    new Platform(Width.Short, Height.Low),
                    new Platform(Width.Long, Height.High),
                    new Platform(Width.Short, Height.High)
                }; 
                break;
            case 3:
                this.sequence = new List<Platform>() {
                    new Platform(Width.Short, Height.Middle),
                    new Platform(Width.Long, Height.Middle),
                    new Platform(Width.Medium, Height.Middle)
                };  
                break;
            case 4:
                this.sequence = new List<Platform>() {
                    new Platform(Width.Long, Height.Middle),
                    new Platform(Width.Short, Height.Low),
                    new Platform(Width.Medium, Height.Low)
                };  
                break;
            case 5:
                this.sequence = new List<Platform>() {
                    new Platform(Width.Short, Height.High),
                    new Platform(Width.Short, Height.Middle),
                    new Platform(Width.Long, Height.Low),
                    new Platform(Width.Medium, Height.High)
                };  
                break;
            default:
                break;
        }
        this.coinsPerWindow = this.calculateCoinsPerWindow();
    }


    private int calculateCoinsPerWindow() {
        int total = 0; 
        for (int i = 0; i < this.sequence.Count; i++) {
            total += this.sequence[i].getNumCoins();
        }
        return total; 
    }

    public string GetStringRepresentation() {
        string result = "POLICY #" + this.index;
        for (int i = 0; i < this.sequence.Count; i++) {
            result += "[" + this.sequence[i].getWidth() + " " + this.sequence[i].getHeight() + "]";
        }
        return result;
    }

    public List<Platform> GetSequence() {
        return this.sequence; 
    }

    public override string ToString(){
        return "{'policy': {'index': " + this.index + ", 'scores': " + string.Join( ",", this.scores) + ", 'acclimated': " + this.acclimated + "}}";
    }  

}
