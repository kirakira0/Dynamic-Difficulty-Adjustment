using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy
{

    public int index;
    public List<Platform> sequence = new List<Platform>(); // type, height 
    public int coinsPerWindow; 

    public Policy(int index) {
        this.index = index; 
        switch (this.index) {
            case 1:
                Debug.Log("Case 1");
                this.sequence = new List<Platform>() {
                    new Platform(Width.Medium, Height.Low),
                    new Platform(Width.Short, Height.High),
                    new Platform(Width.Long, Height.Middle)
                }; 
                break;
            case 2:
                Debug.Log("Case 2");
                this.sequence = new List<Platform>() {
                    new Platform(Width.Medium, Height.Low),
                    new Platform(Width.Short, Height.Low),
                    new Platform(Width.Long, Height.High),
                    new Platform(Width.Short, Height.High)
                }; 
                break;
            case 3:
                Debug.Log("Case 3");
                this.sequence = new List<Platform>() {
                    new Platform(Width.Short, Height.Middle),
                    new Platform(Width.Long, Height.Middle),
                    new Platform(Width.Medium, Height.Middle)
                };  
                break;
            default:
                Debug.Log("Default case");
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
}
