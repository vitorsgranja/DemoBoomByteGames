using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour {
  [SerializeField] private GameObject pauseMenu;
  [SerializeField] private GameObject gameOverMenu;
  [SerializeField] private SaveDataController saveScript;
  [SerializeField] private Slider volumeSlider;
  private AudioController audioController;
  private GameState gameState;

  [SerializeField] private TMP_Text pointsText, shotsText, timeText, loadText;
  [SerializeField] private Button replayButton;

  void Start() {
    audioController = AudioController.Instace;
    gameState = GameState.Instace;
    volumeSlider.value = audioController.GetMasterVolume();
    if(SceneManager.GetActiveScene().buildIndex == 0) { //if in menu scene
      LoadGame();
    } else {
      gameState.shots = 0;
      gameState.points = 0;
      gameState.time = 0;
    }
  }
  private void Update() {
    if(SceneManager.GetActiveScene().buildIndex == 1) { //if in game scene
      if(Input.GetKeyDown(KeyCode.Escape)) {
        PauseFunction();
      }
      TimeCounter();
    }
  }
  public void UpdateReplayButton() {
    replayButton.interactable = !gameState.ballsAreMoving;
  }

  public void PauseFunction() {
    if(gameState.gameIsPaused) {
      Resume();
    } else {
      Pause();
    }
  }
  private void Resume() {
    gameState.gameIsPaused = false;
    pauseMenu.SetActive(false);
    Time.timeScale = 1;
  }
  private void Pause() {
    gameState.gameIsPaused = true;
    pauseMenu.SetActive(true);
    Time.timeScale = 0;
  }
  public void ChangeScene(int scene) {
    if(gameState.gameIsPaused) {
      Resume();
    }
    SceneManager.LoadScene(scene);
  }
  public void QuitGame() {
    Application.Quit();
  }
  public void playTestSound() {
    audioController.PlaySound(audioController.testSound);
  }
  public void VolumeSlider(float volume) {
    audioController.ChangeMasterVolume(volume);
    audioController.PlaySound(audioController.testSound);
  }
  public void UpdatePoints() {
    pointsText.text = "Score: " + gameState.points;
  }
  public void UpdateShots() {
    shotsText.text = "Shots: " + gameState.shots;
  }
  private void TimeCounter() {
    if(!gameState.gameIsOver) {
    int min, sec;
    min = (int)Time.timeSinceLevelLoad / 60;
    sec = (int)Time.timeSinceLevelLoad % 60;
    timeText.text = "Time: " + min.ToString("00") + ":" + sec.ToString("00");
    }
  }
  public void GameOver() {
    replayButton.gameObject.SetActive(false);
    gameOverMenu.SetActive(true);
    SaveGame();
  }

  private void SaveGame() {
    gameState.time = (int)Time.timeSinceLevelLoad;
    saveScript.Save();
  }
  private void LoadGame() {
    saveScript.Load();
    if(gameState.pointsLast != 0) {
      int min, sec;
      min = gameState.timeLast / 60;
      sec = gameState.timeLast % 60;
      loadText.text = "Stats of last game:\n\nScore: " + gameState.pointsLast + "\nShots: " + gameState.shotsLast + "\nTime: " + min.ToString("00") + ":" + sec.ToString("00");
    } else {
      loadText.text = "Stats of last game:\n\nSave file not found!";
    }

  }
}