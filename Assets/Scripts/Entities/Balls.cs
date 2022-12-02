using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour {
  private Rigidbody rb;
  private AudioController audioController;
  public bool ballIsMoving = false;
  [SerializeField] private GameController gameController;
  public int ballId;
  public bool iWasHit = false;
  private void Start() {
    rb = GetComponent<Rigidbody>();
    audioController = AudioController.Instace;
  }
  private void FixedUpdate() {
    //accelerates the proccess at witch balls stop when they are almost stationary
    if(rb.velocity.magnitude < 0.05f && rb.velocity.magnitude > 0.001) {
      rb.velocity -= rb.velocity.normalized / 1000;
      ballIsMoving = true;
    } else if(rb.velocity.magnitude == 0) {
      ballIsMoving = false;
    } else {
      ballIsMoving = true;
    }
  }
  public void SetVelocity(Vector3 newVelocity) {
    rb.velocity = newVelocity;
  }
  public void SetTransform(Vector3 pos) {
    this.gameObject.transform.position = pos;
  }
  private void OnCollisionEnter(Collision other) {
    float audioScale = Mathf.Clamp(other.relativeVelocity.magnitude / 2,0f,1f);
    if(other.gameObject.CompareTag("Ball") && other.gameObject.GetComponent<Balls>().ballId < ballId) { //only one of the balls call for the sound
      audioController.PlaySound(audioController.ballCollision,audioScale);
      if(other.gameObject.GetComponent<Balls>().ballId == 0) { //if i was hit by the white ball
        iWasHit = true;
      }
    }
    if(!other.gameObject.CompareTag("Ball")) {
      audioController.PlaySound(audioController.ballTableCollision,audioScale);
    }
  }
  private void OnCollisionExit(Collision other) {
    if(!iWasHit && other.gameObject.CompareTag("Ball") && other.gameObject.GetComponent<Balls>().ballId < ballId) { //only one of the balls call for the sound
      if(other.gameObject.GetComponent<Balls>().ballId == 0) { //if i was hit by the white ball
        iWasHit = true;
      }
    }
  }
}