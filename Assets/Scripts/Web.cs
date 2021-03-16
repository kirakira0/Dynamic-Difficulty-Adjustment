using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Web : MonoBehaviour
{
    string dateURL = "http://localhost:8080/DDA/GetDate.php";
    string usersURL = "http://localhost:8080/DDA/GetUsers.php";
    string registerPlayerURL = "http://localhost:8080/DDA/RegisterUser.php";
    string addPlayerURL = "http://localhost:8080/DDA/AddPlayer.php";

    public void Start() {
        StartCoroutine(AddPlayer());
    }

    public IEnumerator GetData(string URL) {
        using (UnityWebRequest www = UnityWebRequest.Get(URL)) {
            //yield return www.Send();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                // Show results as text.
                Debug.Log(www.downloadHandler.text);
                // Or retrieve as binary data
                byte[] results = www.downloadHandler.data;  
            }
        }
    }

    // public IEnumerator RegisterPlayer(string userIp, int score) {
    //     WWWForm form = new WWWForm();
    //     form.AddField("userIp", userIp);
    //     form.AddField("score", score);
        
    //     using (UnityWebRequest www = UnityWebRequest.Post(registerPlayerURL, form)) {
    //         yield return www.SendWebRequest();
    //         if (www.isNetworkError || www.isHttpError) {
    //             Debug.Log(www.error);
    //         } else {
    //             // Show results as text.
    //             Debug.Log(www.downloadHandler.text);
    //             // Or retrieve as binary data
    //             byte[] results = www.downloadHandler.data;  
    //         }
    //     }
    // }

    public IEnumerator AddPlayer() {
        WWWForm form = new WWWForm();
        form.AddField("coins", 0);
        
        using (UnityWebRequest www = UnityWebRequest.Post(addPlayerURL, form)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                // Show results as text.
                Debug.Log(www.downloadHandler.text);
                // Or retrieve as binary data
                byte[] results = www.downloadHandler.data;  
            }
        }
    }
      
}