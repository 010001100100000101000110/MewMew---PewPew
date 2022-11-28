using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICombo : MonoBehaviour
{
    Text comboText;
    Combo comboProperties;
    int comboValue;
    
    void Start()
    {
        comboText = GetComponent<Text>();
        comboProperties = FindObjectOfType<Combo>();
    }

    void Update()
    {
        comboValue = comboProperties.comboCounter;
        comboText.text = "X" + comboValue.ToString();
    }
}
