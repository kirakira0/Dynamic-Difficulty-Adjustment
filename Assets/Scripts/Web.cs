using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Web : MonoBehaviour
{
    string dateURL = "http://localhost:8080/DDA/GetDate.php";
    string usersURL = "http://localhost:8080/DDA/GetUsers.php";
    string registerPlayerURL = "http://localhost:8080/DDA/RegisterUser.php";
    string addPlayerURL = "http://localhost:8080/DDA/AddPlayer.php";
    string insertScoreURL = "http://localhost:8080/DDA/InsertScore.php";

    string insertDataURL = "http://localhost:8080/DDA/InsertData.php";

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


    public IEnumerator InsertData(int totalCoins) {
        WWWForm form = new WWWForm();

        // int[] policies = new int[] { 1, 3, 5 };


        form.AddField("totalCoins", totalCoins);
        // form.AddField("policies[]", policies);

        using (UnityWebRequest www = UnityWebRequest.Post(insertDataURL, form)) {
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
                // Debug.Log(www.downloadHandler.insert_id);
                // Or retrieve as binary data
                byte[] results = www.downloadHandler.data;  
            }
        }
    }

    public IEnumerator InsertScore() {
        WWWForm form = new WWWForm();
        // form.AddField("coins", 0);
        
        using (UnityWebRequest www = UnityWebRequest.Post(insertScoreURL, form)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                // Show results as text.
                 Debug.Log(www.downloadHandler.text);
                // Debug.Log(www.downloadHandler.insert_id);
                // Or retrieve as binary data
                byte[] results = www.downloadHandler.data;  
            }
        }
    }     
}