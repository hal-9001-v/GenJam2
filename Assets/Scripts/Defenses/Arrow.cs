using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   private Rigidbody2D _rb2D;
    private float _speed;
    
   private void Awake() {
       _rb2D = GetComponent<Rigidbody2D>();
        _speed = 30000;
   }

   private void Update() {
        float step = _speed * Time.deltaTime;
        var desiredVelocity = (Vector2) transform.up *step - _rb2D.velocity;
        _rb2D.AddForce(desiredVelocity, ForceMode2D.Impulse); 
        
      }

   private void OnTriggerEnter2D(Collider2D other) {
       
       switch(other.tag){
           
           case "EnemyType1": 
           case "EnemyType2": 
                other.gameObject.GetComponent<Enemy>().Hurt();
            break;
           case "EnemyType3": 
                other.gameObject.GetComponent<Enemy>().Hurt();
                other.gameObject.GetComponent<Enemy>().Hurt();
           break;

       }
   }
}
