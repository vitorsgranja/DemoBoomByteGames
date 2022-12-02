using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation: MonoBehaviour {
  private GameState gameState;
  [SerializeField] private Transform gameCameraRef;
  [SerializeField] private float sensitivity = 10f;
  private bool rotatingCamera = false;
  private float currentRotation;
  private void Start() {
    gameState = GameState.Instace;
  }
  void LateUpdate() {
    if(!gameState.gameIsPaused) {
      if(Input.GetMouseButtonDown(1)) {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rotatingCamera = true;
      }
      if(Input.GetMouseButtonUp(1)) {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        rotatingCamera = false;
      }
      if(rotatingCamera) {
        currentRotation += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation = Mathf.Repeat(currentRotation,360);
      }
        gameCameraRef.rotation = Quaternion.Euler(0,currentRotation,0);
    }
  }
}
