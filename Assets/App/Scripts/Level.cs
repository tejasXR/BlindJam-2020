﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.BlindJam
{
    public class Level : MonoBehaviour
    {
        [SerializeField] GameObject levelGeometry;

        private void Start()
        {
            levelGeometry.SetActive(false);
        }

        private void OnEnable()
        {
            StartPoint.StartedCallback += EnableGeometry;
            EndPoint.EndedCallback += DisableLevel;
        }

        private void OnDisable()
        {
            StartPoint.StartedCallback -= EnableGeometry;
            EndPoint.EndedCallback -= DisableLevel;
        }

        private void EnableGeometry()
        {
            levelGeometry.SetActive(true);
        }

        private void DisableLevel()
        {
            gameObject.SetActive(false);
        }
    }
}


