using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
  public static GameState Instace;
  public bool gameIsPaused = false;
  public bool ballsAreMoving = false;
  public bool roundPointWasGiven = false;
  public bool gameIsOver = false;

  public int points, shots, time;
  public int pointsLast, shotsLast, timeLast;
  public Vector3 redLastPosition, yellowLastPosition, whiteLastPosition;
  public Vector3 lastHitForce;



  private void Awake() {
    if(Instace == null) {
      Instace = this;
      DontDestroyOnLoad(gameObject);
    } else {
      GameState.Instace.gameIsOver = false;
      Destroy(gameObject);
    }
  }
}
