using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    float bullet_speed;

    // Use this for initialization
    void Start () {
        bullet_speed = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        //Get the bullet's current position
        Vector2 position = transform.position;
        
        //Compute the bullet's new position
        position = new Vector2(position.x, position.y + bullet_speed * Time.deltaTime);

        //Update the bullet's position
        transform.position = position;

        //This is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //If the bullet went outside of the screen on the top, then destroy the bullet.
        if (transform.position.y > max.y) {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision of player bullet with an enemy ship.
        if (col.tag == "EnemyShipTag")
        {
            //Destroy the player bullet
            Destroy(gameObject);
        }
    }
}
