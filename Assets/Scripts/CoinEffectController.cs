using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffectController : MonoBehaviour
{
    public QuestionBoxController assignedBox;
    private AudioSource coinAudio;
    // Start is called before the first frame update
    void Start()
    {
        coinAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        coinAudio.PlayOneShot(coinAudio.clip);
        assignedBox.ResetQuestionBox();
        ShowCoin(false);
    }

    public void ShowCoin(bool state)
    {
        GetComponent<SpriteRenderer>().enabled = state;
        GetComponent<BoxCollider2D>().enabled = state;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
