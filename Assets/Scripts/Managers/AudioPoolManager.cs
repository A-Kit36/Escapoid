using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolManager : MonoBehaviour
{
    public static AudioPoolManager Instance { get; private set; }

    [SerializeField] private int poolSize = 20;
    [SerializeField] private GameObject audioSourcePrefab; // we need a prefab with an audiosource component on it
    private List<AudioSource> audioSources;
    private int nextIndex = 0; // so we can go through the list

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        audioSources = new List<AudioSource>();

        //creating 20 (or poolsize) different objects to reuse time and time again
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioSourcePrefab); // object created
            obj.transform.SetParent(transform); // we set as his parent the gameObject which this script is attached to
            AudioSource audioSource = obj.GetComponent<AudioSource>(); // we get an audiosource component of that object
            audioSource.playOnAwake = false; // so it plays when we need it to
            audioSources.Add(audioSource); // adding the created object's audiosource to the list (our pool)
        }
    }

    public void PlayAudioClip(AudioClip clip, float volume = 1.0f)
    {
        AudioSource source = GetNextAvailableClip(); // we get a gameobject that's next on the list
        source.clip = clip; // sets the clip we need to play
        source.volume = volume; // sets the volume
        source.Play(); //plays the clip
    }

    private AudioSource GetNextAvailableClip()
    {
        AudioSource source = audioSources[nextIndex];
        nextIndex = (nextIndex + 1) % poolSize; // this way, when we get to 20, the index will turn 0 again
        return source; // we return the audiosource with the correct index
    }
}
