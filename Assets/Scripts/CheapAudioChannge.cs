using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheapAudioChannge : MonoBehaviour
{
    public AudioClip deathClip;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnPlayerDeath += ChangeAudioDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeAudioDeath()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        GameManager.OnPlayerDeath -= ChangeAudioDeath;
    }
}
