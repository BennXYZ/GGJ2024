using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    float length;

    float time;

    private void Start()
    {
        length = audioSource.clip.length;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time + length + 1)
                Destroy(gameObject);
    }
}
