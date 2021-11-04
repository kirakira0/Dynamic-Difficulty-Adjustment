using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SliderSetter : MonoBehaviour
{

    public Text myText;
    public string label; 

    private Slider slider;

    private float myValue; 
    private Agent Agent; 
    private PlayerController Player;  

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        Agent = GameObject.Find("Agent").GetComponent<Agent>(); 
        Player = GameObject.Find("Player").GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    public void Update()
    {
        if (this.name == "PlatformSpeed") {
            myText.text = "PLATFORM SPEED: " +  slider.value;
            Agent.PLATFORM_SPEED = slider.value;  
        } else if (this.name == "MaxJump") {
            myText.text = "JUMP FORCE: " +  slider.value;
            // Player.maxJumpValue = slider.value; 
        }
    }
}
