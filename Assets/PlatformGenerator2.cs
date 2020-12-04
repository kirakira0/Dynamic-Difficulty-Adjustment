using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator2 : MonoBehaviour
{
    public Transform PLATFORM_GENERATION_POINT; 
    public GameObject[] PLATFORMS; 
    private int PLATFORM_SPEED = 5; 
    
    // public void Generate(string type, string height) {
    //     Instantiate(GetPlatformType(type), GetPlatformHeight(height), Quaternion.identity);
    // }

    public void Generate(Agent2.Platform platform) {
        Instantiate(GetPlatformType(platform.GetType()), GetPlatformHeight(platform.GetHeight()), Quaternion.identity);
    }

    public void Print(Agent2.Platform platform) {
        Debug.Log(platform.GetType());
    }

    // public void Generate(Platform platform) {
    //     Instantiate(GetPlatformType(platform.GetPlatformType()), GetPlatformHeight(platform.GetPlatformHeight()), Quaternion.identity);
    // }

    public void GenerateSequence() {
        
    }

    private GameObject GetPlatformType(string type) {
        switch (type.ToLower()) {
            case "short":
                return PLATFORMS[0];
            case "med":
                return PLATFORMS[1];
            case "medium":
                return PLATFORMS[1];
            case "long":
                return PLATFORMS[2];
            default:
                return PLATFORMS[1];
        }
    }

    private Vector3 GetPlatformHeight(string height) {
        switch (height.ToLower()) {
            case "low":
                return new Vector3(PLATFORM_GENERATION_POINT.transform.position.x, PLATFORM_GENERATION_POINT.transform.position.y - 3f, 0);
            case "med":
                return new Vector3(PLATFORM_GENERATION_POINT.transform.position.x, PLATFORM_GENERATION_POINT.transform.position.y, 0);
            case "mid":
                return new Vector3(PLATFORM_GENERATION_POINT.transform.position.x, PLATFORM_GENERATION_POINT.transform.position.y, 0);
            case "high":
                return new Vector3(PLATFORM_GENERATION_POINT.transform.position.x, PLATFORM_GENERATION_POINT.transform.position.y + 3f, 0);
            default:
                return new Vector3(PLATFORM_GENERATION_POINT.transform.position.x, PLATFORM_GENERATION_POINT.transform.position.y, 0);
        }
    }
}
