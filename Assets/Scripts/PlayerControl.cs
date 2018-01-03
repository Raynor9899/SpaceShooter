using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject GameManagerGo;

    public GameObject PlayerBulletGo; //This is player's bullet prefab.
    public GameObject BulletPosition1;
    public GameObject BulletPosition2;
    public GameObject BulletTrack;
    public GameObject ExplosionGo;

    //Reference to the livesUI text
    public Text LivesUIText;

    const int MaxLives = 3;//Maximum player lives
    public int lives;//current player lives

    public float player_speed;

    public void Init() {
        lives = MaxLives;

        //Update the lives UI text
        LivesUIText.text = lives.ToString();

        //Reset the player position to the center of the screen
        transform.position = new Vector2(0, 0);

        //Set this player game object to active
        gameObject.SetActive(true);
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Invoke("FireTrackBullet", 1f);
        //Fire bullets when the spacebar is pressed.
        if (Input.GetKeyDown("space")) {
            //Play the laser sound effect
            gameObject.GetComponent<AudioSource>().Play();

            //Instantiate the first bullet
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGo);
            bullet01.transform.position = BulletPosition1.transform.position; //Set the bullet initial position.

            //Instantiate the second bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGo);
            bullet02.transform.position = BulletPosition2.transform.position; //Set the bullet initial position.
        }

        float x = Input.GetAxisRaw("Horizontal"); //The value will be -1, 0, 1. (for left, no input, right)
        float y = Input.GetAxisRaw("Vertical"); //The value will be -1, 0, 1. (for up, no input, down)

        //Now based on the input we compute a direction vector, and we normalize it to get a unit vector.
        Vector2 direction = new Vector2(x, y).normalized;

        //Now we call the function that compute and sets the player's position.
        Move(direction);
	}

    void Move(Vector2 direction) {
        //Find the screen limits to the player's movement(left, right, up, and the bottom edges of the screen)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//This is bottom-left point (corner) of the screen.
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//This is Top-right point (corner) of the screen.

        max.x = max.x - 0.225f; //Substract the player sprite half width.
        min.x = min.x + 0.225f; //Add the palyer sprite half width.

        max.y = max.y - 0.285f; //Substract the player sprite half height.
        min.y = min.y + 0.285f; //Add the palyer sprite half height.

        //Get the player's cuttent position.
        Vector2 pos = transform.position;

        //Calculate the new position.
        pos += direction * player_speed * Time.deltaTime;

        //Make sure the new position is not outside the screen.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the player's position.
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D col) {
        //Detect collision of the player ship with an enemy ship, or with an enemy bullet
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag")) {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();

            if (lives == 0) {
                //Change game manager state to game over state
                GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                //hide the player's ship
                gameObject.SetActive(false);
            }
            
        }
    }

    //Function to instantiate the explosion
    void PlayExplosion(){
        GameObject explosion = (GameObject)Instantiate(ExplosionGo);

        //Set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
