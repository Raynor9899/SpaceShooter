using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {

    public GameObject EnemyBulletGo;
	// Use this for initialization
	void Start () {
        //Fire an enemy bullet after 1 seconds
        Invoke("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function to fire an enemy bullet
    void FireEnemyBullet() {
        //get a reference to the player's ship
        GameObject playerShip = GameObject.Find("PlayerGo");

        if (playerShip != null) { //if the player is not dead
            //Instantiate an enemy bullet
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGo);

            //Set the bullet's initial position
            bullet.transform.position = transform.position;

            //Compute the bullet's direction towards the player's ship
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            //Set the bullet's direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
