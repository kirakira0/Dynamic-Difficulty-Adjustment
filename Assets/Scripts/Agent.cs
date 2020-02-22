using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Environment Environment; 
    public CoinGenerator CoinGenerator;
    Vector3 v = new Vector3(0, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Environment.test); 
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RepeatCallToEnv() {
        Environment.Generate("short", "mid"); 
        //CoinGenerator.SpawnCoins(v);
    }
}
