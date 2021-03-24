using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //button1 will do an action to change what is on the screen, whatever is then displayed will determine the data that is shown to the user
    //button2
    // Start is called before the first frame update
    public Button nextButton;
    // public Button prevButton;
    private int planetNumber = 0;
    public SelectionManager selectionManager;

    public GameObject[] planetScenes;
    void Start()
    {
        
        nextButton.onClick.AddListener(next);
        // prevButton.onClick.AddListener(previous);

        for (int i = 1; i < planetScenes.Length; i++) {
            planetScenes[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void next()
    {
        for (int i = 0; i < planetScenes.Length; i++)
        {
            Debug.Log(planetScenes[i].activeInHierarchy);
            if (planetScenes[i].activeInHierarchy) 
            {
                planetScenes[i].SetActive(false);
                planetScenes[(i+1)%planetScenes.Length].SetActive(true);
                break;
            }
        }

        planetNumber++;
        if(planetNumber == 10)
        {
            planetNumber = 0;
        }
        selectionManager.requestPlanetInformation(planetNumber);
    }

    void previous() 
    {
        for (int i = planetScenes.Length-1; i >= 0; i--)
        {
            Debug.Log(planetScenes[i].activeInHierarchy);
            if (planetScenes[i].activeInHierarchy)
            {
                planetScenes[i].SetActive(false);
                Debug.Log("calculated: " + ((i - 1) % planetScenes.Length));
                int newIndex = i - 1;
                if (newIndex < 0)
                {
                    planetScenes[planetScenes.Length - 1].SetActive(true);
                }
                else {
                    planetScenes[(i - 1) % planetScenes.Length].SetActive(true);
                }
                break;
            }
        }
        planetNumber--;
        if(planetNumber == -1)
        {
            planetNumber = 9;
        }
        selectionManager.requestPlanetInformation(planetNumber);
    }

}
