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

    // Start is called before the first frame update
    void Start()
    {
        Logger = GameObject.Find("Logger").GetComponent<Logger>(); 
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        Logger.Start();



        // platformGenerator.GenerateSequence(sbp1);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {

            Manager.SetPaused(false);
            
            List<Platform> sqn1 = new List<Platform>() {
                new Platform(Width.Medium, Height.Low),
                new Platform(Width.Short, Height.High),
                new Platform(Width.Long, Height.Middle)
            };  

            // sqn1.Add(new Platform(Width.Short, Height.Middle));
            Subpolicy sbp1 = new Subpolicy(sqn1);

            generateSequence = StartCoroutine(platformGenerator.GenerateSequence(sbp1));
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
