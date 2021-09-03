using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Web : MonoBehaviour
{
    Coroutine coroutine;
    string writeResultsURL = "http://localhost:80/dda/WriteResults.php";

    public void WriteData(string result) {
        coroutine = StartCoroutine(WriteResults(result));
    }

    public IEnumerator WriteResults(string result) {
        WWWForm form = new WWWForm();

        form.AddField("game", result);
        
        using (UnityWebRequest www = UnityWebRequest.Post(writeResultsURL, form)) {

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
}


