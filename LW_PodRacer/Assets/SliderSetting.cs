using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : MonoBehaviour
{

    public Slider _slider;
    public Text _name;
    public Text _value;

    public bool _canSet = true;
    public bool canSet
    {
        get { return _canSet; }
        set { _canSet = value; _slider.interactable = value; }
    }

    void Awake()
    {
        _slider.interactable = _canSet;
        SFX_Controller.dict_Sliders[gameObject.name] = this;
        _name.text = gameObject.name;
        _slider.onValueChanged.AddListener(delegate { setValue(_slider.value); });
    }

    public void initSettingView(float min,float max,float defaultvalue)
    {
        _slider.minValue = min;
        _slider.maxValue = max;
        setValue(defaultvalue);
    }

    public void setValue(float value)
    {
        _slider.value = value;
        _value.text = value.ToString("F");
    }

    public float getValue()
    {
        return _slider.value;
    }

    public void AddListener_OnValueChanged(Action action)
    {
        _slider.onValueChanged.AddListener(delegate { action(); });

    }

    void Update()
    {

    }

}
