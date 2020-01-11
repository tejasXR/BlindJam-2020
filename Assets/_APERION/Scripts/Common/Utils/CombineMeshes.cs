using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    public static class CombineMeshes
    {
        public static GameObject Combine(GameObject _objectToCombine, Transform _parent, Material _material)
        {
            MeshFilter[] meshFilters = _objectToCombine.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            Matrix4x4 pTransform = _parent.worldToLocalMatrix;

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = pTransform * meshFilters[i].transform.localToWorldMatrix;
                i++;
            }

            var combinedMesh = new GameObject("Combined Mesh");
            combinedMesh.AddComponent<MeshFilter>();
            combinedMesh.AddComponent<MeshRenderer>();
            
            combinedMesh.transform.GetComponent<MeshFilter>().mesh = new Mesh();
            combinedMesh.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            combinedMesh.transform.GetComponent<MeshRenderer>().material = _material;
            combinedMesh.transform.gameObject.SetActive(true);

            return combinedMesh;
        }
    }
}


