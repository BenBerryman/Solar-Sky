using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class PlanetFacts : MonoBehaviour
{
    // Start is called before the first frame update

	public Button mainbutton;
	public TextMeshProUGUI fact;

	string planetinfo = "";

    void Start(){
    	// Buttons from panel will control the data so that the request can be made to get Planet Data
    	mainbutton.GetComponentInChildren<Text>().text = "Show Data";
    	Button btn = mainbutton.GetComponent<Button>();

		btn.onClick.AddListener(test);
    }
    
    IEnumerator GetRequest(string uri){
    	UnityWebRequest uwr = UnityWebRequest.Get(uri);
    	yield return uwr.SendWebRequest();

    	if(uwr.isNetworkError){
    		Debug.Log("Error " + uwr.error);
    	}
    	else{
    		// Debug.Log("200 " + uwr.downloadHandler.text);
    		string[] arr = new string[] {};
    		var test = JSON.Parse(uwr.downloadHandler.text);
    		// Debug.Log(test["Mercury"].Value);

    		int planetlen = test["Mercury"].Count;
    		int randfact = randnum(planetlen);

    		planetinfo = test["Mercury"][randfact].Value;
    		Debug.Log(planetinfo);
    		// fact.text = "Fact";
    		fact.text = planetinfo;
    		// int total = randnum(planetlen);

    		// Debug.Log(total);

    		// planetinfo = test["Mercury"][x].Value;
    		// Debug.Log(planetinfo);
    	}
    }

    void test(){
    	// Debug.Log(planetinfo);
		StartCoroutine(GetRequest("https://tarunapp.github.io/api/planets.json"));
    	// string new1 = test["Mercury"][x].Value;
    }

    int randnum(int mainlen){
    	Random.seed = System.DateTime.Now.Millisecond;
		int x = Random.Range(0,mainlen);
		return x;
    }
}
