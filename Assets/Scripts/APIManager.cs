﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using SimpleJSON;

public class APIManager : MonoBehaviour
{

    private string URL = "https://api.le-systeme-solaire.net/rest/bodies/";
    public Text Name;
    public Text Mass;
    public Text Radius;
    public Text DaysInYear;
    public Text HoursInDay;
    public Text Fact;
    public Text Composition;

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
        DaysInYear = GameObject.Find("planetDaysInYear").GetComponent<Text>();
        HoursInDay = GameObject.Find("planetHoursInDay").GetComponent<Text>();
        Composition = GameObject.Find("planetComposition").GetComponent<Text>();
        // planetName = "soleil";
        // StartCoroutine(getPlanetInformation(planetName));
        // StartCoroutine(GetPlanetFacts("Sun"));
    }
    private bool test = false;
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
                        //StartCoroutine(GetPlanetFacts(ConvertBackToEnglish(planetName)));
                        


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
        float planetOrbit = planetInfo["sideralOrbit"];
        float planetRotation = planetInfo["sideralRotation"];

        Name.text = "Name                        " + planetName;
        Mass.text = "Mass                         " + cleanDecimal(planetMass) + " x 10^" + planetMassExponent + " kg";
        Radius.text = "Radius                     " + cleanDecimal(planetRadius) + " km";
        DaysInYear.text = "Earth Days in Year           " + cleanHoursAndDays(planetOrbit) + " Days";
        HoursInDay.text = "Hours in a day             " + cleanHoursAndDays(planetRotation) + " Hours";


        string pname = ConvertBackToEnglish(targetID);
        //==============================================================================
        UnityWebRequest getlanddata = UnityWebRequest.Get("https://tarunapp.github.io/api/planetstype.json");
        yield return getlanddata.SendWebRequest();

        if (getlanddata.isNetworkError)
        {
            Debug.Log("Error " + getlanddata.error);
        }
        else
        {
            // Debug.Log("200 " + getjsondata.downloadHandler.text);
            // string[] arr = new string[] {};
            var jsondata = JSON.Parse(getlanddata.downloadHandler.text);
            // Debug.Log(test["Mercury"].Value);

            string planetLandType = jsondata[pname];



            Composition.text = "Body Composition             " +planetLandType;
            Debug.Log("Planet Land Type: " + planetLandType);

            // Set the text feilds to the planet data when we get it.

        }
        //==============================================================================
        
        Debug.Log(pname + "-----------------");
        UnityWebRequest getjsondata = UnityWebRequest.Get("https://tarunapp.github.io/api/planets.json");
        yield return getjsondata.SendWebRequest();

        if (getjsondata.isNetworkError)
        {
            Debug.Log("Error " + getjsondata.error);
        }
        else
        {
            // Debug.Log("200 " + getjsondata.downloadHandler.text);
            // string[] arr = new string[] {};
            var jsondata = JSON.Parse(getjsondata.downloadHandler.text);
            // Debug.Log(test["Mercury"].Value);

            int planetlen = jsondata[pname].Count;
            int randfact = randnum(planetlen);

            string planetdata = jsondata[pname][randfact].Value;
            Fact.text = planetdata;
            Debug.Log("Planet Fact: " + planetdata);

            // Set the text feilds to the planet data when we get it.

        }
    }

    public string ConvertBackToEnglish(string planetName) {
        string factname = "";
        if (planetName == "soleil")
        {
            factname = "Sun";
        }
        else if (planetName == "mercure")
        {
            factname = "Mercury";
        }
        else if (planetName == "venus")
        {
            factname = "Venus";
        }
        else if (planetName == "terre")
        {
            factname = "Earth";
        }
        else if (planetName == "mars")
        {
            factname = "Mars";
        }
        else if (planetName == "jupiter")
        {
            factname = "Jupiter";
        }
        else if (planetName == "saturne")
        {
            factname = "Saturn";
        }
        else if (planetName == "uranus")
        {
            factname = "Uranus";
        }
        else if (planetName == "neptune")
        {
            factname = "Neptune";
        }
        return factname;
    }

    public double cleanHoursAndDays(float planetRotation) {
        return System.Math.Round(System.Math.Abs(planetRotation));
    }
    public string cleanDecimal(string deci) 
    {
        return float.Parse(deci).ToString("0");
    }

    public void requestPlanetInformation(int planetNumber)
    {
        if (planetNumber == 0)
        {
            Name.text = "Name";
            Mass.text = "Mass";
            Radius.text = "Radius";
            DaysInYear.text = "Earth Days in Year";
            HoursInDay.text = "Hours in a Day";
            Fact.text = "Fun Fact";
            Composition.text = "Body Composition";
        }
        else
        {
            int trueNumber = planetNumber - 1;
            planetName = planets[trueNumber];
            StartCoroutine(getPlanetInformation(planetName));
        }

    }



     IEnumerator GetPlanetFacts(string pname){
        Debug.Log(pname + "-----------------");
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

            // Set the text feilds to the planet data when we get it.
            
        }
    }

     int randnum(int mainlen){
        Random.seed = System.DateTime.Now.Millisecond;
        int x = Random.Range(0,mainlen);
        return x;
    }


    //// , IPointerDownHandler
    //void Start()
    //{
    //    //addPhysicsRaycaster();
    //}



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