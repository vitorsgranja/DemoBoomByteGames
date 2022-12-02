using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
  public static AudioController Instace;

  [SerializeField] private AudioSource effectSource;

  public AudioClip testSound, ballCollision, ballTableCollision, PlayerHit;

  private void Awake() {
    if(Instace == null) {
      Instace = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }
  public void PlaySound(AudioClip clip,float volume = 1) {
    effectSource.PlayOneShot(clip,volume);
  }

  public void ChangeMasterVolume(float volume) {
    effectSource.volume = volume;
  }
  public float GetMasterVolume() {
    return effectSource.volume;
  }
}
