using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
  private GameState gameState;
  private AudioController audioController;
  [SerializeField] private Balls[] balls;
  private bool ballsAreMoving = false;
  [SerializeField] UIController uiController;
  [SerializeField] Slider powerSlider;
  private float power;


  private void Start() {
    gameState = GameState.Instace;
    audioController = AudioController.Instace;
  }

  private void Update() {
    CheckBallsMovement();
    CheckRoundPoint();
    ShotHandler();
  }

  private void ShotHandler() {
    if(!gameState.ballsAreMoving) {
      if(Input.GetKey(KeyCode.Space)) {
        powerSlider.gameObject.SetActive(true);
        power = Mathf.PingPong(Time.time,0.9f) + 0.1f;
        powerSlider.value = power;
        power *= 2;
      } else if(Input.GetKeyUp(KeyCode.Space)) {
        gameState.shots++;
        TakeShot();
        powerSlider.gameObject.SetActive(false);
        uiController.UpdateShots();
      }
    }
  }
  private void TakeShot() {
    RecordBallsPositions();
    ResetBallState();
    gameState.roundPointWasGiven = false;
    Vector3 force;
    force = powerSlider.transform.position - balls[0].transform.position;
    force = new Vector3(force.x,0,force.z).normalized * power;
    balls[0].SetVelocity(force);
    gameState.lastHitForce = force;
    audioController.PlaySound(audioController.ballTableCollision, power/2);
  }

  private void ResetBallState() {
    foreach(var ball in balls) {
      ball.iWasHit = false;
    }
  }
  private void RecordBallsPositions() {
    gameState.whiteLastPosition = balls[0].transform.position;
    gameState.redLastPosition = balls[1].transform.position;
    gameState.yellowLastPosition = balls[2].transform.position;
  }

  private void CheckBallsMovement() {
    bool tempIsMoving = false;
    foreach(var ball in balls) {
      if(ball.ballIsMoving) {
        tempIsMoving = true;
      }
    }
    if(ballsAreMoving != tempIsMoving) { //if there was a change
      gameState.ballsAreMoving = tempIsMoving;
      if(gameState.gameIsOver) {
        uiController.GameOver();
      }
      if(gameState.shots > 0) {//cant have replay before 1st shot
        uiController.UpdateReplayButton();
      }
    }
    ballsAreMoving = tempIsMoving;
  }
  private void CheckRoundPoint() {
    if(!gameState.roundPointWasGiven && balls[1].iWasHit && balls[2].iWasHit) {
      gameState.points++;
      uiController.UpdatePoints();
      gameState.roundPointWasGiven = true;
    }
    if(gameState.points >= 3) {
      gameState.gameIsOver = true;
    }
  }
  public void Replay() {
    //set balls to previous recorded places
    balls[0].SetTransform(gameState.whiteLastPosition);
    balls[1].SetTransform(gameState.redLastPosition);
    balls[2].SetTransform(gameState.yellowLastPosition);

    //add force on white ball
    balls[0].SetVelocity(gameState.lastHitForce);

  }

}
