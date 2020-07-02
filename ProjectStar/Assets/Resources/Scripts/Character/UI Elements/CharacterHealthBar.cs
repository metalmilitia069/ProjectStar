using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthBar : MonoBehaviour
{

    public Slider Slider;


    public bool isSliderStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Slider>().maxValue = GetComponent<CharacterStats>().characterStatsVariables.maxHealth;
        Slider.maxValue = GetComponentInParent<CharacterStats>().characterStatsVariables.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Slider>().value = GetComponent<CharacterStats>().characterStatsVariables.health;

        

        Slider.maxValue = GetComponentInParent<CharacterStats>().characterStatsVariables.maxHealth;
        Slider.value = GetComponentInParent<CharacterStats>().characterStatsVariables.health;
        this.transform.LookAt(Camera.main.transform);
    }
}
