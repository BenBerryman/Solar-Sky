using System.Collections;
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
        Name = GameObject.Find("Name").GetComponent<Text>();
        Mass = GameObject.Find("Mass").GetComponent<Text>();
        Radius = GameObject.Find("Radius").GetComponent<Text>();
        planetName = "soleil";
        StartCoroutine(getPlanetInformation(planetName));
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