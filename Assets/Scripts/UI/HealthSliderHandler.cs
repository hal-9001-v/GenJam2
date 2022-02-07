using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthSliderHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Slider _slider;
    [SerializeField] Health _health;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        if (_health)
        {
            _health.hurtAction += () =>
            {
                UpdateBar(_health.currentHealthPoints / _health.maxHealthPoins);
            };

            _health.deadAction += () =>
            {
                UpdateBar(0);
            };
        }
    }

    private void Start()
    {
        if (_health)
        {
            UpdateBar(_health.maxHealthPoins / _health.currentHealthPoints);
        }
    }


    public void UpdateBar(float fillAmount)
    {
        _slider.value = fillAmount;
    }
}
