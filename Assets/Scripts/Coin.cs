using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int value = 1;
    public GameManager gameManager;
    private ParticleSystem particles;
    private SpriteRenderer spr;
    public AudioManager audioManager;
    public AudioClip audioClip;
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioManager.PlaySS(audioClip);
            gameManager.IncreasePoints(value);
            particles.Play();
            spr.enabled = false;
            
        }
    }
}
