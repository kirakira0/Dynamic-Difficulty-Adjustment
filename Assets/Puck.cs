using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    public GameObject startPlat;
    public GameObject shortPlat;
    public GameObject medPlat;
    public GameObject longPlat;
    
    private float platformGeneratorX; 
    private float platformGeneratorY; 
    private PlayerMovement player;
    private bool corIsRunning = false; 
    private Coroutine generateSequence; 
    private float acclimation = 0; 

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
    }

    void Update() {
        // Ensures only one Coroutine is running at one time. 
        if (!corIsRunning && !player.IsPaused()) {
            // generateSequence = StartCoroutine(Generate(medPlat, Height.mid));

            generateSequence = StartCoroutine(GenerateSequence(new List<Puck.Platform>() {new Puck.Platform(medPlat, Height.low), new Puck.Platform(shortPlat, Height.mid)}));

            // StartCoroutine(Generate(medPlat, Height.mid));
            corIsRunning = true;
        }
        else if (player.IsPaused()) {
            StopCoroutine(generateSequence);
            corIsRunning = false;
        }
    }

    IEnumerator GenerateSequence(List<Puck.Platform> sequence) {
        int sequenceIndex = 0; 
        for (int i = 0; i < 1000; i++) {
            if (acclimation < 1f) {
                if (sequenceIndex >= sequence.Count) {
                    sequenceIndex = 0;
                }
                Instantiate(sequence[sequenceIndex].GetPlatformType(), GetSpawnPoint(sequence[sequenceIndex].GetHeight()), Quaternion.identity);
                sequenceIndex++; 
            }
            yield return new WaitForSeconds(1.3f);
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
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY + 3, 0); 
                break;
            default:
                spawnPoint = new Vector3(platformGeneratorX, platformGeneratorY + 3, 0); 
                break;
        }
        return spawnPoint;
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
