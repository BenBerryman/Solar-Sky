using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using UnityEngine.Networking;


public class API : MonoBehaviour
{
    private string URL = "https://api.le-systeme-solaire.net/rest/bodies/";
    public Text Name;
    public Text Mass;
    public Text Radius;
    public Button requestButton;

    private string sunID = "soleil";
    private string mercuryID = "mercure";
    private string venusID = "venus";
    private string earthID = "terre";
    private string marsID = "mars";
    private string jupiterID = "jupiter";
    private string saturnID = "saturne";
    private string uranusID = "uranus";
    private string neptuneID = "neptune";

    string[] planets = new string[] { "soleil", "mercure" , "venus" , "terre" , "mars" , "jupiter" , "saturne" , "uranus" , "neptune"};



    int randomN;

    private void Start()
    {
        
        Debug.Log("started");
        Name = GameObject.Find("Name").GetComponent<Text>();
        Mass = GameObject.Find("Mass").GetComponent<Text>();
        Radius = GameObject.Find("Radius").GetComponent<Text>();
        requestButton = GameObject.Find("requestButton").GetComponent<Button>();

        requestButton.onClick.AddListener(buttonClicked);
    }

    void turnOff()
    {
        Debug.Log("say something");
    }

    void buttonClicked()
    {
        int randomN = Random.Range(1, 9);
        StartCoroutine(getPlanetInformation(planets[randomN]));
    }

    IEnumerator getPlanetInformation(string targetID)
    {
        string planetURL = URL + targetID;

        UnityWebRequest planetRequest = UnityWebRequest.Get(planetURL);

        yield return planetRequest.SendWebRequest();

        if(planetRequest.isNetworkError || planetRequest.isHttpError)
        {
            Debug.LogError(planetRequest.error);
            yield break;
        }

        JSONNode planetInfo = JSON.Parse(planetRequest.downloadHandler.text);

        string planetName = planetInfo["englishName"];
        string planetMass = planetInfo["mass"]["massValue"];
        string planetRadius = planetInfo["meanRadius"];
        string planetMassExponent = planetInfo["mass"]["massExponent"];
        

        Name.text = "Planet Name: " + planetName;
        Mass.text = "Planet Mass: " + planetMass + " 10^" + planetMassExponent + " Kilograms";
        Radius.text = "Planet Radius: " + planetRadius + " Kilometres";
    }


}
