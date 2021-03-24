using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button nextButton;
    public Button prevButton;
    private int planetNumber = 0;
    public APIManager apimanager;

    public GameObject[] planetScenes;
    void Start()
    {
        
        nextButton.onClick.AddListener(next);
        prevButton.onClick.AddListener(previous);

        for (int i = 1; i < planetScenes.Length; i++) {
            planetScenes[i].SetActive(false);
        }
    }

    void next()
    {

        planetScenes[planetNumber].SetActive(false);
        planetNumber++;
        if(planetNumber == 10)
        {
            planetNumber = 0;
        }
        planetScenes[planetNumber].SetActive(true);
        apimanager.requestPlanetInformation(planetNumber);
    }

    void previous() 
    {
        
        planetScenes[planetNumber].SetActive(false);
        planetNumber--;
        if(planetNumber == -1)
        {
            planetNumber = 9;
        }
        planetScenes[planetNumber].SetActive(true);
        apimanager.requestPlanetInformation(planetNumber);
    }

}
