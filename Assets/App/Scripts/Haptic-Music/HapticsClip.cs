using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class HapticsClip : MonoBehaviour
{
    //[SerializeField] AudioClip audioClip;
    [SerializeField] GameObject audioVisualizerPrefab;

    [Space(7)]
    [Range(0, 1)] [SerializeField] float amplitude;
    [Range(0, 1)] [SerializeField] float hapticPulseThreshold; 

    private List<GameObject> audioVisualzers = new List<GameObject>();
    private List<GameObject> audioVisualzersAverage = new List<GameObject>();
    //private AudioSource audioSource;
    private float[] samples = new float[256];

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CreateAudioVisualizer();
        CreateAudioVisualizerAverage();
    }

    private void Update()
    {
        //if (audioSource.isPlaying)
        {
            //audioClip.GetData(samples, audioSource.timeSamples);

            AudioListener.GetOutputData(samples, 0);

            AdjustAudioVisualerScale();
            AdjustAudioVisualizerAverageScale();
            AudioHaptics();
        }
    }

    private void AudioHaptics()
    {     
        if ((GetSampleAverage() * 10F) > hapticPulseThreshold)
        {
            Debug.Log(GetSampleAverage() * 10F);
            OVRInput.SetControllerVibration(.01F, GetSampleAverage() * 10F * amplitude, OVRInput.Controller.RTouch);
        }        
    }

    private float GetSampleAverage()
    {
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i];
        }

        float average = sum / samples.Length;

        return average;
    }

    private void CreateAudioVisualizer()
    {
        float positionX = 0;
        float prefabSize = audioVisualizerPrefab.transform.localScale.x;

        // Create audio visualization
        for (int i = 0; i < samples.Length; i++)
        {
            var gO = Instantiate(audioVisualizerPrefab, new Vector3(positionX, 0, 0), Quaternion.identity) as GameObject;
            gO.transform.parent = transform;
            audioVisualzers.Add(gO);

            positionX += prefabSize;
        }
    }

    private void CreateAudioVisualizerAverage()
    {
        float positionX = 0;
        float prefabSize = audioVisualizerPrefab.transform.localScale.x;

        // Create audio visualization
        for (int i = 0; i < samples.Length; i++)
        {
            var gO = Instantiate(audioVisualizerPrefab, new Vector3(positionX, 0, 3), Quaternion.identity) as GameObject;
            gO.transform.parent = transform;
            audioVisualzersAverage.Add(gO);

            positionX += prefabSize;
        }
    }

    private void AdjustAudioVisualerScale()
    {
        for (int i = 0; i < audioVisualzers.Count; i++)
        {
            var dest = new Vector3(audioVisualzers[i].transform.position.x, samples[i] * amplitude * 10F, 0);

            audioVisualzers[i].transform.position = dest;
        }
    }

    private void AdjustAudioVisualizerAverageScale()
    {
        for (int i = 0; i < audioVisualzersAverage.Count; i++)
        {
            var scale = new Vector3(audioVisualzersAverage[i].transform.localScale.x, GetSampleAverage() * amplitude * 10F, audioVisualzersAverage[i].transform.localScale.z);

            audioVisualzersAverage[i].transform.localScale = scale;
        }
    }
}
