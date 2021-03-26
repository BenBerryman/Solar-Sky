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
    public GameObject[] planetImages;
    void Start()
    {
        
        nextButton.onClick.AddListener(next);
        prevButton.onClick.AddListener(previous);
        UIPanel = GameObject.Find("UIPanel");
        UIPanel.SetActive(false);

        for (int i = 1; i < planetScenes.Length; i++) {
            planetScenes[i].SetActive(false);
        }

        for (int i = 0; i < planetImages.Length; i++)
        {
            planetImages[i].SetActive(false);
        }



    }

    void next()
    {

        planetScenes[planetNumber].SetActive(false);
        
        planetNumber++;
        
        if (planetNumber == 10)
        {
            planetNumber = 0;
            
        }
        if (planetNumber != 0) {
            UIPanel.SetActive(true);
        }else{
            UIPanel.SetActive(false);
        }
        planetScenes[planetNumber].SetActive(true);
        toggleImage(planetNumber);
    
        
        
        apimanager.requestPlanetInformation(planetNumber);
        
    }

    void toggleImage(int planetNumber) {
        //0, 1, 2, 3, 4, 5, 6 ,7, 8, 9
        if (planetNumber == 0)
        {
            planetImages[8].SetActive(false);
        }
        else if (planetNumber == 1) {
            planetImages[planetNumber - 1].SetActive(true);
        }
        else
        {
            planetImages[planetNumber-2].SetActive(false);
            planetImages[planetNumber-1].SetActive(true);
        }
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
        toggleImageBackwards(planetNumber);
        apimanager.requestPlanetInformation(planetNumber);
    }

    void toggleImageBackwards(int planetNumber) {
        //0, 1, 2, 3, 4, 5, 6 ,7, 8, 9
        if (planetNumber == 0)
        {
            planetImages[0].SetActive(false);
        }
        else if (planetNumber == 9)
        {
            planetImages[8].SetActive(true);
        }
        else
        {
            planetImages[planetNumber].SetActive(false);
            planetImages[planetNumber - 1].SetActive(true);
        }
    }

}
