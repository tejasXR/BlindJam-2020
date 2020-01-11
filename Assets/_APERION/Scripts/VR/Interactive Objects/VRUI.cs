using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.UI
{
    /// <summary>
    /// Base class for any UI element in VR space
    /// </summary>    
    [RequireComponent(typeof(RectTransform))]
    public class VRUI : MonoBehaviour
    {
        public virtual void Awake()
        {
            CheckIfUILayer();
            StartCoroutine(Create2DCollider());
        }

        private void CheckIfUILayer()
        {
            // Check is object is on UI layer
            if (gameObject.layer.ToString() != "UI")
            {
                gameObject.layer = LayerMask.NameToLayer("UI");
            }            
        }
        
        private IEnumerator Create2DCollider()
        {
            // Wait until next frame when layout groups have resized
            yield return new WaitForEndOfFrame();

            // Get current UI dimensions 
            var rectTransform = GetComponent<RectTransform>();
            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;

            // Size a new 2d collider component with the same dimensions
            var collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(width, height);
        }
    }
}

