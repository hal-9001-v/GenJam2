using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthSliderHandler : MonoBehaviour
{
    private Slider _healthSlider;
    Health _health;

    private void Awake()
    {
        _health = GetComponentInParent<Health>();
        _healthSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        _healthSlider.value = ((float)_health.currentHealthPoints / (float)_health.maxHealthPoins) * 100;
    }
}
