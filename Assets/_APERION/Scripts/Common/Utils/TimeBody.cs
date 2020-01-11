using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    [RequireComponent(typeof(Rigidbody))]
    public class TimeBody : MonoBehaviour
    {
        private bool isRecording = false;

        [SerializeField]
        private List<PointInTime> pointsInTime = new List<PointInTime>();
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            ScenePlaybackManager.StartRecordingCallback += StartRecording;
            ScenePlaybackManager.StopRecordingCallback += StopRecording;
            ScenePlaybackManager.RewindToPointCallback += RewindToPoint;
        }

        private void OnDisable()
        {
            ScenePlaybackManager.StartRecordingCallback -= StartRecording;
            ScenePlaybackManager.StopRecordingCallback -= StopRecording;
            ScenePlaybackManager.RewindToPointCallback -= RewindToPoint;
        }

       
        private void FixedUpdate()
        {
            if (isRecording)
            {
                Record();
            }          
        }

        private void Record()
        {
            if (pointsInTime.Count > Mathf.Round(ScenePlaybackManager.Instance.recordTime / Time.fixedDeltaTime))
            {
                pointsInTime.RemoveAt(pointsInTime.Count - 1);
            }

            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        }

        //private void Rewind()
        //{
        //    PointInTime p = pointsInTime[0];

        //    transform.position = p.position;
        //    transform.rotation = p.rotation;

        //    pointsInTime.RemoveAt(0);
        //}

        private void RewindToPoint()
        {
            var timelinePercent = ScenePlaybackManager.Instance.timelinePercent;

            if (timelinePercent < 0 || timelinePercent > 1)
            {
                Debug.Log("Invalid timeline percent point");
                return;
            }

            int point = Mathf.RoundToInt(timelinePercent * pointsInTime.Count);

            PointInTime p = pointsInTime[point];

            //transform.position = p.position;
            //transform.rotation = p.rotation;

            transform.position = Vector3.Lerp(transform.position, p.position, Time.deltaTime * 3F);
            transform.rotation = Quaternion.Slerp(transform.rotation, p.rotation, Time.deltaTime * 3F);
        }

        public void StartRecording()
        {
            isRecording = true;

            //rb.isKinematic = true;
            //rb.useGravity = false;
        }

        public void StopRecording()
        {
            isRecording = false;

            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}


