using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale_Rotate_Slider : MonoBehaviour
{
    public static Scale_Rotate_Slider Instance;
    public  Slider ScaleSlider;
    public Slider RotateSlider;

    public float scaleMinValue;
    public float scaleMaxValue;

    public float rotMinValue;
    public float rotMaxValue;

    void Awake()
    {
        Instance = this;        
    }


    void Start()
    {
        ScaleSlider.minValue = scaleMinValue;
        ScaleSlider.maxValue = scaleMaxValue;
        ScaleSlider.onValueChanged.AddListener(ScaleSliderUpdate);

        RotateSlider.minValue = rotMinValue;
        RotateSlider.maxValue = rotMaxValue;
        RotateSlider.onValueChanged.AddListener(RotatSliderUpdate);
    }

    void ScaleSliderUpdate(float value){
        FurniturePlacementManager.Instance.currentSpawnedObject.transform.localScale = new Vector3(value, value, value);
    }

    void RotatSliderUpdate(float value){
        FurniturePlacementManager.Instance.currentSpawnedObject.transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
    }
}
