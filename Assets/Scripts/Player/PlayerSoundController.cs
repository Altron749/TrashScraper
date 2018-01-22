using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour {
    public AudioClip pickFlower;
    public AudioClip jump;
    private AudioSource AS;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }

    public void playJump()
    {
        AS.PlayOneShot(jump);
    }
    public void playPickFlower()
    {
        AS.PlayOneShot(pickFlower);
    }
}
