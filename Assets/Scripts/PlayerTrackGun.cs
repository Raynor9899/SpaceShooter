using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackGun : MonoBehaviour
{

    public GameObject PlayerTrackGunGo;
    // Use this for initialization
    void Start()
    {
        Invoke("FireTrackBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Function to fire an enemy bullet
    void FireTrackBullet()
    {
        //get a reference to the player's ship
        GameObject enemyShip = GameObject.Find("EnemyGo");

        if (enemyShip != null)
        { //if the player is not dead
            //Instantiate an enemy bullet
            GameObject bullet = (GameObject)Instantiate(PlayerTrackGunGo);

            //Set the bullet's initial position
            bullet.transform.position = transform.position;

            //Compute the bullet's direction towards the player's ship
            Vector2 direction = enemyShip.transform.position - bullet.transform.position;

            //Set the bullet's direction
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
