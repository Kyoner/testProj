using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HBar : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void ChangeHPBar(int maxHealth, int currHealth)
    {
        slider.value = (float)currHealth / maxHealth;
    }
}
