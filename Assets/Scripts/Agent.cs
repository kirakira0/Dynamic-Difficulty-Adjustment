using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float PLATFORM_SPEED = 0.1f; 
    public Generator platformGenerator; 

    private Coroutine generateSequence;
    private bool acclimated = false; 
    private Logger Logger;
    private Manager Manager; 

    private Subpolicy sbp1;
    private Subpolicy sbp2;
    private Subpolicy sbp3;
    private Subpolicy currentSubpolicy; 


    // Start is called before the first frame update
    void Start()
    {
        Logger = GameObject.Find("Logger").GetComponent<Logger>(); 
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        Logger.Start();

        List<Platform> sqn1 = new List<Platform>() {
            new Platform(Width.Medium, Height.Low),
            new Platform(Width.Short, Height.High),
            new Platform(Width.Long, Height.Middle)
        };  
        sbp1 = new Subpolicy(sqn1);

        List<Platform> sqn2 = new List<Platform>() {
            new Platform(Width.Medium, Height.Low),
            new Platform(Width.Short, Height.Low),
            new Platform(Width.Long, Height.High),
            new Platform(Width.Short, Height.High)
        };  
        sbp2 = new Subpolicy(sqn2);

        List<Platform> sqn3 = new List<Platform>() {
            new Platform(Width.Short, Height.Middle),
            new Platform(Width.Long, Height.Middle),
            new Platform(Width.Medium, Height.Middle)
        };  
        sbp3 = new Subpolicy(sqn3);

        currentSubpolicy = sbp1;

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            currentSubpolicy = sbp1;
        } 
        else if (Input.GetKeyDown(KeyCode.W)) {
            currentSubpolicy = sbp2;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            currentSubpolicy = sbp3;
        }

        if(Input.GetKeyDown(KeyCode.Return)) {

            Manager.SetPaused(false);

            generateSequence = StartCoroutine(platformGenerator.GenerateSequence(currentSubpolicy));
            Debug.Log("AGENT STARTS COUROUTINE");
        }

    }

    public void StopGeneration() {
        StopCoroutine(generateSequence);
    }

    public bool getIsAcclimated() {
        return acclimated;
    }
}
