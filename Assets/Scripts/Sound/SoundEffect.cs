using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
    private AudioSource audioSource;

    void Awake() {
        this.audioSource = this.GetComponent<AudioSource>();
    }

    void Update() {
        if(!this.audioSource.isPlaying) Destroy(this.gameObject);        
    }
}
