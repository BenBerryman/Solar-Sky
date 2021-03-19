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
    private bool solar = true;
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
        if (solar)
        {
            Debug.Log(planetScenes[0]);
            Debug.Log(planetScenes[1]);
            planetScenes[0].SetActive(false);
            planetScenes[1].SetActive(true);
            solar = false;
        }
        else
        {
            planetScenes[1].SetActive(false);
            planetScenes[0].SetActive(true);
            solar = true;
        }
    }
}
