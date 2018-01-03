using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //Reference to our game object
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGo;
    public GameObject scoreUITextGo;
         
    public enum GameManagerState {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

	// Use this for initialization
	void Start () {
        GMState = GameManagerState.Opening;
	}

    //Function to update the game manager state
    void UpdateGameManagerState() {
        switch (GMState) {

            case GameManagerState.Opening:
                //Hide Game Over
                GameOverGo.SetActive(false);
                //Set play button visible (active)
                playButton.SetActive(true);
                break;

            case GameManagerState.Gameplay:
                //Reset the score
                scoreUITextGo.GetComponent<GameScore>().Score = 0;
                
                //Hide play button on game play state
                playButton.SetActive(false);

                //Set the player visible(active) and init the player lives
                playerShip.GetComponent<PlayerControl>().Init();

                //Start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                break;

            case GameManagerState.GameOver:
                //Stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                //Display game over
                GameOverGo.SetActive(true); 
                //Change game manager state to Opening State after 6 seconds
                Invoke("ChangeToOpeningState", 6f);
                break;
        }
    }

    //Function to set the game manager state
    public void SetGameManagerState(GameManagerState state) {
        GMState = state;
        UpdateGameManagerState();
    }

    //Our play button will call this function
    //When the user clicks the button
    public void StartGamePlay() {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    //Function to change game manager state to opening state
    public void ChangeToOpeningState() {
        SetGameManagerState(GameManagerState.Opening);
    }
}
