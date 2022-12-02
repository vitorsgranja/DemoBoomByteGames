using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveDataController : MonoBehaviour {

  private void Start() {
    SaveSystem.Init();
  }

  public void Save() {
    // Save
    int time = GameState.Instace.time;
    int points = GameState.Instace.points;
    int shots = GameState.Instace.shots;

    SaveObject saveObject = new SaveObject {
      time = time,
      points = points,
      shots = shots
    };
    string json = JsonUtility.ToJson(saveObject);
    SaveSystem.Save(json);
    Debug.Log("Saved!");
  }

  public void Load() {
    // Load
    string saveString = SaveSystem.Load();
    if(saveString != null) {
      Debug.Log("Loaded: " + saveString);

      SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

      GameState.Instace.timeLast = saveObject.time;
      GameState.Instace.pointsLast = saveObject.points;
      GameState.Instace.shotsLast = saveObject.shots;

    } else {
      Debug.Log("No save");
    }
  }


  private class SaveObject {
    public int time, points, shots;
  }
}