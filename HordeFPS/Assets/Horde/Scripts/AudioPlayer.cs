using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }
    AudioSource source;

    [SerializeField]AudioClip ding;

    private void Awake()
    {
        Instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Ding()
    {
        source.clip = ding;
        source.loop = false;
        source.Play();
    }
}
