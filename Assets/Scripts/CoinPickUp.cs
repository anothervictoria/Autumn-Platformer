using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] int pointsPerCoin = 10;
    bool wasPickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !wasPickedUp)
        {
            wasPickedUp = true;
            FindObjectOfType<GameSession>().AddToScore(pointsPerCoin);
            PlayCoinSound();
            Destroy(gameObject);
        }
    }

    private void PlayCoinSound()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }
}
