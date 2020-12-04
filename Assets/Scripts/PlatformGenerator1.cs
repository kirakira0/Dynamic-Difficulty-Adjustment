using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator1 : MonoBehaviour
{
    public GameObject[] platforms; 
    public GameObject platform;

    void Awake()
    {
        Platform p = new Platform(Width.Short, Height.Middle);
        // int width = platform.getWidth();
        // int height = platform.getHeight();
        switch (p.getWidth()) {
            case Width.Short:
                platform = platforms[0];
                break;
            case Width.Medium:
                platform = platforms[1];
                break;
            case Width.Long:
                platform = platforms[2];
                break;
            default:
                platform = platforms[1];
                break;
        }
        // Instantiate(platform, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
    }

    void Update()
    {
        
    }
}
