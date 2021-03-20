using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //button1 will do an action to change what is on the screen, whatever is then displayed will determine the data that is shown to the user
    //button2
    // Start is called before the first frame update
    public Button yourButton;
    
    public GameObject[] planetScenes;
    void Start()
    {
        yourButton.onClick.AddListener(next);
        for (int i = 1; i < planetScenes.Length; i++) {
            planetScenes[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void next()
    {
        Debug.Log("called");
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
    }
}
