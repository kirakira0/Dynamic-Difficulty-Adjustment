using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent2 : MonoBehaviour
{
    public PlatformGenerator2 PLATFORM_GENERATOR;

    void Start()
    {
        PLATFORM_GENERATOR.Generate(new Agent2.Platform("med", "low"));
    }

    void Update()
    {
        
    }

    
    public class Platform {

        private string type;
        private string height; 

        public Platform(string type, string height) {
            this.type = type;
            this.height = height; 
        }

        public string GetType() {
            return this.type;
        }

        public string GetHeight() {
            return this.height;
        }
    }
}
