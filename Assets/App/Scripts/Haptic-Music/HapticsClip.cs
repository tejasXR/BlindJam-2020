using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class HapticsClip : MonoBehaviour
{
    //[SerializeField] AudioClip audioClip;
    [SerializeField] GameObject audioVisualizerPrefab;
    [SerializeField] Material audioVisualMaterial;

    [Space(7)]
    [SerializeField] bool createAudioVisualizer;
    [SerializeField] bool createAudioVisualizerAverage;
    [SerializeField] bool createHapticsVisualizer;

    [Space(7)]
    [Range(0, 1)] [SerializeField] float amplitude;
    [Range(0, 1)] [SerializeField] float hapticPulseThreshold;
    [SerializeField] Transform hapticsVisualizerTransform;

    private List<GameObject> audioVisualzers = new List<GameObject>();
    private List<GameObject> audioVisualzersAverage = new List<GameObject>();
    private GameObject hapticsVisualizer;

    private float[] samples = new float[256];

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (createAudioVisualizer)
            CreateAudioVisualizer();

        if (createAudioVisualizerAverage)
            CreateAudioVisualizerAverage();

        if (createHapticsVisualizer)
            CreateHapticsVisualizer();
    }

    private void Update()
    {
        AudioListener.GetOutputData(samples, 0);

        AdjustAudioVisualerScale();
        AdjustAudioVisualizerAverageScale();
        AudioHaptics();
    }

    private void AudioHaptics()
    {     
        if ((GetSampleAverage() * 10F) > hapticPulseThreshold)
        {
            OVRInput.SetControllerVibration(.01F, GetSampleAverage() * 10F * amplitude, OVRInput.Controller.RTouch);
            AdjustHapticsVisualizerScale();
        }
        else
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
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
            gO.GetComponent<Renderer>().material = audioVisualMaterial;

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
            gO.GetComponent<Renderer>().material = audioVisualMaterial;

            audioVisualzersAverage.Add(gO);

            positionX += prefabSize;
        }
    }

    private void CreateHapticsVisualizer()
    {
        hapticsVisualizer = Instantiate(audioVisualizerPrefab, hapticsVisualizerTransform.position, Quaternion.identity) as GameObject;
        hapticsVisualizer.GetComponent<Renderer>().material = audioVisualMaterial;

        hapticsVisualizer.transform.parent = transform;
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

    private void AdjustHapticsVisualizerScale()
    {
        var scale = new Vector3(hapticsVisualizer.transform.localScale.x, GetSampleAverage() * 10F * amplitude, hapticsVisualizer.transform.localScale.z);

        hapticsVisualizer.transform.localScale = scale;
    }
}
