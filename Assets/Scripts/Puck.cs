using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Puck : MonoBehaviour
{
    public GameObject startPlat;
    public GameObject shortPlat;
    public GameObject medPlat;
    public GameObject longPlat;
    public Text subpolicyText;
    
    private float platformGeneratorX; 
    private float platformGeneratorY; 
    private PlayerMovement player;
    private bool corIsRunning = false; 
    private Coroutine generateSequence; 
    private float acclimation = 0; 

    private List<Puck.Platform> testSeq1;
    private List<Puck.Platform> testSeq2;
    private List<Puck.Platform> currentSequence;
   
    public enum Height {
        low,
        mid,
        high,
    }

    void Awake()
    {
        platformGeneratorX = GameObject.Find("Camera/PlatformGenerationPoint").transform.position.x;
        platformGeneratorY = GameObject.Find("MidYPos").transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        // Define potential sequences. 
        testSeq1 = new List<Puck.Platform>() {new Puck.Platform(medPlat, Height.low), new Puck.Platform(shortPlat, Height.mid)};
        testSeq2 = new List<Puck.Platform>() {new Puck.Platform(longPlat, Height.low),new Puck.Platform(medPlat, Height.high), new Puck.Platform(longPlat, Height.low), new Puck.Platform(shortPlat, Height.mid)};
        currentSequence = testSeq1;
    }

    void Update() {
        if (!corIsRunning && !player.IsPaused()) {
            if (acclimation < 2.0f) {
                // Player is not acclimated.
                generateSequence = StartCoroutine(GenerateSequence(testSeq1));
                subpolicyText.text = "CURRENT SUBPOLICY: " + "ML, SM";
            } else {
                // Once the player is acclimated.
                subpolicyText.text = "CURRENT SUBPOLICY: " + "LL, MH, LL, SM" + "\n" + "SWITCH FROM [SPB1] TO [SPB2]";
                acclimation = 0f; 
                currentSequence = testSeq2;
                generateSequence = StartCoroutine(GenerateSequence(testSeq2));
            }
            corIsRunning = true;
        }
        if (player.IsPaused()) {
            StopCoroutine(GenerateSequence(currentSequence));        
        }
        // Debug.Log(acclimation);
    }

    IEnumerator GenerateSequence(List<Puck.Platform> sequence) {
        int sequenceIndex = 0; 
        for (int i = 0; i < 1000; i++) {
            // If the player has not yet acclimated, generate new platforms as expected.
            if (acclimation < 2f && !player.IsPaused()) {
                if (sequenceIndex >= sequence.Count) {
                    sequenceIndex = 0;
                }
                Instantiate(sequence[sequenceIndex].GetPlatformType(), GetSpawnPoint(sequence[sequenceIndex].GetHeight()), Quaternion.identity);
                sequenceIndex++; 
                acclimation += 0.25f; 
                yield return new WaitForSeconds(1.3f);
            } 
            // If the player is acclimated, stop the coroutine. 
            else {
                corIsRunning = false;
                // Debug.Log("STOP COROUTINE");
                StopCoroutine(generateSequence);
            }
        } 
    }

    /**
     * Helper method that identifies the spawn point at which new platforms 
     * should be generated. 
     */ 
    private Vector3 GetSpawnPoint(Height height) {
        Vector3 spawnPoint;
        platformGeneratorX = GameObject.Find("Camera/PlatformGenerationPoint").transform.position.x;
        switch (height) {
            case Height.low:
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY - 3, 0); 
                break;
            case Height.mid:
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY, 0); 
                break;
            case Height.high:
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY + 2, 0); 
                break;
            default:
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY, 0); 
                break;
        }
        return spawnPoint;
    }

    public List<Puck.Platform> GetCurrentSequence() {
        return currentSequence;
    }

    // SUBCLASSES
    // ------------------------------------------------------------------------

    public class Platform {

        private GameObject type;
        private Height height; 

        public Platform(GameObject type, Height height) {
            this.type = type;
            this.height = height; 
        }

        public GameObject GetPlatformType() {
            return this.type;
        }

        public Height GetHeight() {
            return this.height;
        }
    }
}
