using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public void PlayGame()
    {
        SceneManager.LoadScene("RunnerRoom");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
