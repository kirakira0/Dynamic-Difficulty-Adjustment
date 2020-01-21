using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour 
{
    public string mainMenuLevel;
    public GameManager gameManager;
    public GameObject scoreManager; 
    public Text scoreDisplay; 

    public void Awake() { 
        scoreDisplay.text = "Final score: " + GameObject.Find("ScoreManager").GetComponent<ScoreManager>().scoreCount.ToString();
    }
    
    public void RestartGame()
    {
        gameManager.Reset();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }
}
