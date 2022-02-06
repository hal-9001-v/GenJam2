using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthSliderHandler : MonoBehaviour
{
    private Slider _healthSlider;

    Enemy _enemy;
    BaseDefense _defense;

    int _id;
    // 0 = enemy
    // 1 = defense
    private void Awake() {
        _healthSlider = GetComponent<Slider>();
        _enemy = GetComponentInParent<Enemy>();
        _defense = GetComponentInParent<BaseDefense>();

        if(_enemy != null) _id = 0; else _id = 1;
        
        
    }

    private void Update() {

        if(_id == 0) {

            _healthSlider.value = ((float)_enemy.health/(float)_enemy.totalHealth)*100;

        }
        else if(_id==1){

            _healthSlider.value = ((float)_defense.health/(float)_defense.totalHealth)*100;

        }

    }
}
