﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using SimpleJSON;

public class SelectionManager : MonoBehaviour
{

    private string URL = "https://api.le-systeme-solaire.net/rest/bodies/";
    public Text Name;
    public Text Mass;
    public Text Radius;

    private string sunID = "soleil";
    private string mercuryID = "mercure";
    private string venusID = "venus";
    private string earthID = "terre";
    private string marsID = "mars";
    private string jupiterID = "jupiter";
    private string saturnID = "saturne";
    private string uranusID = "uranus";
    private string neptuneID = "neptune";

    string[] planets = new string[] { "soleil", "mercure", "venus", "terre", "mars", "jupiter", "saturne", "uranus", "neptune" };



    [SerializeField] private string selectableTag = "Selectable";
    private Transform _selection;
    private string planetName;

    private void Start()
    {
        Debug.Log("started");
        Name = GameObject.Find("planetName").GetComponent<Text>();
        Mass = GameObject.Find("planetMass").GetComponent<Text>();
        Radius = GameObject.Find("planetRadius").GetComponent<Text>();
        // planetName = "soleil";
        // StartCoroutine(getPlanetInformation(planetName));
    }

    private void Update()
    {
        //if(_selection != null)
        //{
        //    var selectionRenderer = _selection.GetComponent<Renderer>();
        //    Debug.Log(selectionRenderer.name);
        //    _selection = null;
        //}

        

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        Debug.Log(selectionRenderer.name);
                        planetName = selectionRenderer.name;
                        if (planetName == "Sun")
                        {
                            planetName = "soleil";
                        }
                        else if (planetName == "Jupiter")
                        {
                            planetName = "jupiter";
                        }
                        StartCoroutine(getPlanetInformation(planetName));

                        string factname = "";

                        if(planetName == "soleil"){
                            factname = "Sun";
                        }else if(planetName == "mercure"){
                            factname = "Mercury";
                        }else if(planetName == "venus"){
                            factname = "Venus";
                        }else if(planetName == "terre"){
                            factname = "Earth";
                        }else if(planetName == "mars"){
                            factname = "Mars";
                        }else if(planetName == "jupiter"){
                            factname = "Jupiter";
                        }else if(planetName == "saturne"){
                            factname = "Saturn";
                        }else if(planetName == "neptune"){
                            factname = "Neptune";
                        }

                        StartCoroutine(GetPlanetFacts(factname));
                    }

                    _selection = selection;
                }
            }
        }

    }

    IEnumerator getPlanetInformation(string targetID)
    {
        string planetURL = URL + targetID;

        UnityWebRequest planetRequest = UnityWebRequest.Get(planetURL);

        yield return planetRequest.SendWebRequest();

        if (planetRequest.isNetworkError || planetRequest.isHttpError)
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

    public void requestPlanetInformation(int planetNumber)
    {
        if(planetNumber == 0)
        {
            Name.text = "Planet Name";
            Mass.text = "Planet Mass";
            Radius.text = "Planet Radius";
        }
        else
        {
            int trueNumber = planetNumber - 1;
            planetName = planets[trueNumber];
            StartCoroutine(getPlanetInformation(planetName));
        }
        
    }

    

    //// , IPointerDownHandler
    //void Start()
    //{
    //    //addPhysicsRaycaster();
    //}

    IEnumerator GetPlanetFacts(string pname){
        UnityWebRequest getjsondata = UnityWebRequest.Get("https://tarunapp.github.io/api/planets.json");
        yield return getjsondata.SendWebRequest();

        if(getjsondata.isNetworkError){
            Debug.Log("Error " + getjsondata.error);
        }
        else{
            // Debug.Log("200 " + getjsondata.downloadHandler.text);
            // string[] arr = new string[] {};
            var jsondata = JSON.Parse(getjsondata.downloadHandler.text);
            // Debug.Log(test["Mercury"].Value);

            int planetlen = jsondata[pname].Count;
            int randfact = randnum(planetlen);

            string planetdata = jsondata[pname][randfact].Value;
            Debug.Log("Planet Fact: " + planetdata);
            // Debug.Log(planetdata);
            // fact.text = "Fact";
            // fact.text = planetinfo;
            // int total = randnum(planetlen);

            // Debug.Log(total);

            // planetinfo = test["Mercury"][x].Value;
            // Debug.Log(planetinfo);
        }
    }

     int randnum(int mainlen){
        Random.seed = System.DateTime.Now.Millisecond;
        int x = Random.Range(0,mainlen);
        return x;
    }

    // void test(){
    //     // Debug.Log(planetinfo);
    //     StartCoroutine(GetPlanetFacts("https://tarunapp.github.io/api/planets.json"));
    //     // string new1 = test["Mercury"][x].Value;
    // }


    //void addPhysicsRaycaster()
    //{
    //    PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
    //    if (physicsRaycaster == null)
    //    {
    //        Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
    //    }
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    //}
}