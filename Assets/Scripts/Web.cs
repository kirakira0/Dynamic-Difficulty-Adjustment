using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Web : MonoBehaviour
{
    string dateURL = "http://192.168.64.2/DDA/GetDate.php";
    string usersURL = "http://192.168.64.2/DDA/GetUsers.php";
    
    // Use coroutine b/c we need to wait for a reply from the server (can't be done in 1 frame).
    void Start() {
        StartCoroutine(GetData(dateURL));
        StartCoroutine(GetData(usersURL));
    }

    IEnumerator GetData(string URL) {
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
}