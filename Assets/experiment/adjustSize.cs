using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class adjustSize : MonoBehaviour
{
    // Start is called before the first frame update

    private Slider scaleSlider;
    private Slider rotateSlider;

    public float scaleMin = 0.2f;
    public float scaleMax = 1.0f;
    public float rotMin = 0;
    public float rotMax = 360f;


    void Start()
    {
        scaleSlider = GameObject.Find("scaleSlider").GetComponent<Slider>();
        scaleSlider.minValue = scaleMin;
        scaleSlider.maxValue = scaleMax;

        scaleSlider.onValueChanged.AddListener(scaleSliderUpdate);

        rotateSlider = GameObject.Find("rotateSlider").GetComponent<Slider>();
        rotateSlider.minValue = scaleMin;
        rotateSlider.maxValue = scaleMax;

        rotateSlider.onValueChanged.AddListener(rotateSliderUpdate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void scaleSliderUpdate(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }

    public void rotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
    }

  
}
