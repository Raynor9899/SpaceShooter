using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    GameObject scoreUITextGo;

    public GameObject ExplosionGo;

    float enemy_speed;

	// Use this for initialization
	void Start () {
        enemy_speed = 2f;

        scoreUITextGo = GameObject.FindGameObjectWithTag("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update () {
        //Get the enemy current position
        Vector2 position = transform.position;

        //Compute the enemy new position
        position = new Vector2(position.x, position.y - enemy_speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;

        //This is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //If the enemy went outside the screen on the bottom, the destroy the enemy
        if (transform.position.y < min.y) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col) {
        //Destroy collision of the enemy ship with the player ship, or with a player bullet.
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) {
            PlayExplosion();

            //add 100 point to the score
            scoreUITextGo.GetComponent<GameScore>().Score += 100;

            //Destroy this enemy ship
            Destroy(gameObject);
        }
    }

    void PlayExplosion() {
        GameObject explosion = (GameObject)Instantiate(ExplosionGo);

        //Set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
