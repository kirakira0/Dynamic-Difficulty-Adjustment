using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Agent : MonoBehaviour
{    
    private int i = 0; 

    public Environment Environment; 
    public PlayerController PlayerController; 
    private ScoreManager ScoreManager; 

    private float totalCoins; 
    private float acclimationScore; 
    private Queue recentAcclimationScores = new Queue();  

    Vector3 v = new Vector3(0, 0, 1);

    public Text totalCoinsText; 
    public Text coinsCollected; 
    public Text acclimationText; 

    public List<string> sequence = new List<string>();

    private string S = "small"; 
    private string M = "medium"; 
    private string L = "long"; 
    private string l = "low"; 
    private string m = "mid"; 
    private string h = "high"; 

    void Awake()
    {
        ScoreManager = FindObjectOfType<ScoreManager>();
        InvokeRepeating("RepeatCallToEnv", 1.0f, 1.5f);
        //acclimation --> don't even switch before a minimum number of iterations, stable coin collection percetage
        // sequence.AddRange(new List<string>() {M, l, S, m, S, l});
        sequence.AddRange(new List<string>() {M, l, M, m});
        CalculateTotalCoins();    
    }

    private void handlePlayerFall() {
        CancelInvoke();
        PlayerController.moveSpeed = 0;
        PlayerController.transform.position = new Vector3(PlayerController.transform.position.x + 1.0f, PlayerController.transform.position.y + 11.0f, 0);
        // PlayerController.myRigidBody.gravityScale = 0;

        Vector2 position = PlayerController.transform.position;
        Vector2 direction = PlayerController.transform.TransformDirection(Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, 1000f);

        PlayerController.inDeathcatcher = false; 
        InvokeRepeating("RepeatCallToEnv", 1f, 1.5f);
        PlayerController.moveSpeed = PlayerController.MOVE_SPEED;

    }

    void Update()
    {
        if (PlayerController.inDeathcatcher) {
            this.handlePlayerFall();     
        }

        totalCoinsText.text = "TOTAL CPS: " + totalCoins.ToString(); 
        acclimationText.text = recentAcclimationScores.Count.ToString(); 

        // What to do when plauyer is acclimated.
        // Hard coded for now but later will change so agent is picking from 
        //  a range of sequence options. 
        if (recentAcclimationScores.Count > 2) {  //IF ACCLIMATED
            ScoreManager.scoreCount = 0;
            recentAcclimationScores.Clear(); 
            sequence.Clear(); 
            i = 0; 
            sequence.AddRange(new List<string>() {L, l, L, l});
            // sequence.AddRange(new List<string>() {L, h, M, m, S, l, M, m});
            CalculateTotalCoins(); 
        }

    }

    // Tells the environment what platforms to generate. 
    void RepeatCallToEnv() {
        Environment.Generate(i, sequence);
        // We now calculate platform generation at the end of a sequence 
        //  as opposed to after a ceratin number of seconds. 
        // We can also adjust the "window" size just by multiplying the 
        //  length of the sequence by some number. 
        if (i == 2) {
            acclimationScore = ScoreManager.scoreCount / totalCoins;
            // Add the current acclimation score to the queue. 
            recentAcclimationScores.Enqueue(acclimationScore); 
            ScoreManager.scoreCount = 0;
            acclimationScore = 0;   
        } 
        if (i + 2 < sequence.Count) {
            i += 2; 
        }
        else {  //END OF THE SEQUENCE
            i = 0;
        }

    }

    void CalculateTotalCoins() {
        totalCoins = 0; 
        for (int j = 0; j < sequence.Count; j += 2) {
            if (sequence[j] == "long") {
                totalCoins += 7; 
            }
            else if (sequence[j] == "medium") {
                totalCoins += 5; 
            }
            else if (sequence[j] == "small") {
                totalCoins += 3; 
            }
        }
    }
}
