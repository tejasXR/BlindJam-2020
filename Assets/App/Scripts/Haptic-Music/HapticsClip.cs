using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HapticsClip : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject audioVisualizerPrefab;

    [Range(0, 1)]
    [SerializeField] float amplitude; 

    private List<GameObject> audioVisualzers = new List<GameObject>();
    private AudioSource audioSource;
    private float[] samples = new float[256];

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        float positionX = 0;
        float prefabSize = audioVisualizerPrefab.transform.localScale.x;

        for (int i = 0; i < samples.Length; i++)
        {
            var gO = Instantiate(audioVisualizerPrefab, new Vector3(positionX, 0, 0), Quaternion.identity) as GameObject;
            gO.transform.parent = transform;
            audioVisualzers.Add(gO);
            
            positionX += prefabSize;

        }
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            audioClip.GetData(samples, audioSource.timeSamples);

            for (int i = 0; i < audioVisualzers.Count; i++)
            {
                var dest = new Vector3(audioVisualzers[i].transform.position.x, samples[i] * amplitude * 10F, 0);

                audioVisualzers[i].transform.position = dest;
            }
            
            // Debugging to see if samples array changes over time
            //Debug.Log(samples[128]);


            AudioHaptics();
        }
    }

    private void AudioHaptics()
    {
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i];
        }

        float average = sum / samples.Length;

        OVRInput.SetControllerVibration(average, amplitude, OVRInput.Controller.All);
    }
}
