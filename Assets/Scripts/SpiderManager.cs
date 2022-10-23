using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpiderManager : MonoBehaviour
{
    [SerializeField] AudioClip spiderDeath;
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerController.overGround == true)
        {
            SoundMaganer.Instance.PlayAudioWithVolumen(spiderDeath,0.3f);
        }
    }
}
