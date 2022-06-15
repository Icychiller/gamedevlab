using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OhGodWhy : MonoBehaviour
{
    public GameObject burdenText;
    private AudioMixerSnapshot deathSnapshot;
    public AudioMixer mixer;
    public GameObject fakePrefab;
    // Start is called before the first frame update
    void Start()
    {
        deathSnapshot = mixer.FindSnapshot("Death");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deathSnapshot.TransitionTo(.5f);
            burdenText.gameObject.SetActive(true);
            GameObject anotherObject = Instantiate(fakePrefab,this.transform.position,Quaternion.identity); 
            CentralManager.centralManagerInstance.addPowerup(anotherObject.GetComponent<DestructionMush>().icon, 2, anotherObject.GetComponent<DestructionMush>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            burdenText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
