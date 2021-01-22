using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Web : MonoBehaviour
{
    string dateURL = "http://localhost:8080/DDA/GetDate.php";
    string usersURL = "http://localhost:8080/DDA/GetUsers.php";
    string registerPlayerURL = "http://localhost:8080/DDA/RegisterUser.php";
    
    // Use coroutine b/c we need to wait for a reply from the server (can't be done in 1 frame).
    void Start() {
        // StartCoroutine(GetData(dateURL));
        // StartCoroutine(GetData(usersURL));
        // StartCoroutine(RegisterPlayer(registerPlayerURL, "test #7"));
    }

    // public IEnumerator Hi() {
    //     yield return new WaitForSeconds(1.3f);
    //     Debug.Log("hi");
    // }

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

    public IEnumerator RegisterPlayer(string userIp) {
        WWWForm form = new WWWForm();
        form.AddField("userIp", userIp);
        
        using (UnityWebRequest www = UnityWebRequest.Post(registerPlayerURL, form)) {
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