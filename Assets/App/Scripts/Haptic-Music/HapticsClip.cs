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
        //float[] spectrum = new float[256];

        //AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        if (audioSource.isPlaying)
        {
            audioClip.GetData(samples, audioSource.timeSamples);

            for (int i = 0; i < audioVisualzers.Count; i++)
            {
                var dest = new Vector3(audioVisualzers[i].transform.position.x, samples[i] * amplitude * 10F, 0);

                audioVisualzers[i].transform.position = dest;
            }
            
            // Debugging to see if samples array changes over time
            Debug.Log(samples[128]);

        }


        //for (int i = 1; i < spectrum.Length - 1; i++)
        //{
        //    Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
        //    Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
        //    Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
        //    Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        //}
    }

    private void AudioHaptics()
    {
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.All);
    }
}
