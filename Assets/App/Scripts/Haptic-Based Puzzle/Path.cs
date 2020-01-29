using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.BlindJam
{
    public class Path : MonoBehaviour
    {       
        void Start()
        {
            HideChildMeshes();
        }

        // Hide all child meshes in the path on start
        private void HideChildMeshes()
        {
            var meshes = GetComponentInChildren<MeshRenderer>();
            meshes.enabled = false;
        }
    }
}

