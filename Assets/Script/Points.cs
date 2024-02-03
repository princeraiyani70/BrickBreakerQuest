using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public Slider slider;
    public GameObject star1, star2, star3;

    public static Points instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        pointText.text = "POINTS: " + slider.value;

        if(slider.value == 90)
        {
            star1.SetActive(true);
        }
        else if(slider.value == 470) 
        {
            star2.SetActive(true);
        }
        else if (slider.value == 860)
        {
            star3.SetActive(true);
        }
    }
}
