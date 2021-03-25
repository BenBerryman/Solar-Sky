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
    private GameObject UIPanel;
    void Start()
    {
        
        nextButton.onClick.AddListener(next);
        prevButton.onClick.AddListener(previous);
        UIPanel = GameObject.Find("UIPanel");
        UIPanel.SetActive(false);

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
        if (planetNumber != 0) {
            UIPanel.SetActive(true);
        }else{
            UIPanel.SetActive(false);
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
        if (planetNumber != 0)
        {
            UIPanel.SetActive(true);
        }else{
            UIPanel.SetActive(false);
        }
        planetScenes[planetNumber].SetActive(true);
        apimanager.requestPlanetInformation(planetNumber);
    }

}
