using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    [SerializeField] AudioClip pickUpEffectSound = null;
    AudioSource audioPlayer;


    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();

        if (pickUpEffectSound != null)
        {
            audioPlayer.clip = pickUpEffectSound;
            audioPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0) Destroy(gameObject);
    }
}
