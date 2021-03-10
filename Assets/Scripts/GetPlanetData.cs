using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetPlanetData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
    	// Buttons from panel will control the data so that the request can be made to get Planet Data
    	// Usage of API: https://api.le-system-solaire.net/rest/bodies/<planet name>
    	StartCoroutine(GetRequest("https://api.le-system-solaire.net/rest/bodies/Earth"));
    }
    
    IEnumerator GetRequest(string uri){
    	UnityWebRequest uwr = UnityWebRequest.Get(uri);
    	yield return uwr.SendWebRequest();

    	if(uwr.isNetworkError){
    		Debug.Log("Error " + uwr.error);
    	}
    	else{
    		Debug.Log("200 " + uwr.downloadHandler.text);
    	}
    }
}
