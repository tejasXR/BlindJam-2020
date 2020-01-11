using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    public static class Utility
    {       
        public static float GetRandomFloat(float _min, float _max)
        {
            return Random.Range(_min, _max);
        }

        public static int GetRandomInt(int _min, int _max)
        {
            return Random.Range(_min, _max);
        }

        public static bool GetProbability(float probability)
        {
            if (GetRandomFloat(0, 100) <= probability)
            {
                return true;
            }
            
            return false;
        }

        public static Vector3 Vector3Lerp(Vector3 _fromPosition, Vector3 _toPosition, float _timeDelta)
        {
            return Vector3.Lerp(_fromPosition, _toPosition, Time.deltaTime * _timeDelta);
        }
    }
}


