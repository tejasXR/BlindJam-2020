using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    public class ScenePlaybackManager : MonoBehaviour
    {
        #region EVENTS

        public static event Action StartRecordingCallback;
        public static event Action StopRecordingCallback;
        public static event Action RewindToPointCallback;

        protected void OnStartRecording()
        {
            if (StartRecordingCallback != null)
                StartRecordingCallback?.Invoke();
        }

        protected void OnStopRecording()
        {
            if (StopRecordingCallback != null)
                StopRecordingCallback?.Invoke();
        }

        protected void OnRewindToPoint()
        {
            if (RewindToPointCallback != null)
                RewindToPointCallback?.Invoke();
        }

        #endregion

        public static ScenePlaybackManager Instance;

        public bool startPlaybackOnStart;

        public float playbackMultiplier;

        public float recordTime;// { get; private set; }

        [Range(0.0F, 1.0F)]
        public float timelinePercent;// { get; private set; }

        private float timeCounter;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //recordTime = 15F;
            timeCounter = 1;
        }

        private void Update()
        {
            if (startPlaybackOnStart)
            {             
                timeCounter -= Time.deltaTime / recordTime * playbackMultiplier;

                SetTimelinePercent(timeCounter);

                if (timeCounter <= 0)
                {
                    timeCounter = 1;
                }
            }
        }

        public void StartRecord()
        {
            OnStartRecording();
        }

        public void StopRecord()
        {
            OnStopRecording();
        }

        public void SetTimelinePercent(float _percent)
        {
            timelinePercent = _percent;

            //Debug.Log(_percent);

            OnRewindToPoint();
        }
    }
}


