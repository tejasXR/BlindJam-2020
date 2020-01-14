using UnityEngine;
using APERION.VR.INTERACTIVE;

namespace APERION.VR
{
    // A script that manages player object

    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public enum SpawnType
        {
            Floor,
            EyeLevel
        }

        public GameObject leftHand;
        public GameObject rightHand;
        public GameObject playerHead;
        public GameObject playerBody;

        [Space(7)]
        [SerializeField] SpawnType spawnType;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void Update()
        {
            SetPlayerBodyPosition();
            SetSpawnPosition();
        }

        public void SetPlayerPosition(Transform _positionTransform)
        {
            transform.position = new Vector3(_positionTransform.position.x, transform.position.y, _positionTransform.position.z);
        }

        public void SetPlayerRotation(Transform _rotationTransform)
        {
            var yAngleDifference = (_rotationTransform.eulerAngles.y - playerHead.transform.eulerAngles.y);
            
            var rotation = transform.eulerAngles;

            rotation.y += yAngleDifference;

            transform.eulerAngles = rotation;
        }

        public void ResetCameraTransform()
        {
            var rotation = playerHead.transform.rotation;

            rotation.z = 0;
            rotation.x = 0;

            transform.rotation = rotation;
        }        

        private void SetPlayerBodyPosition()
        {
            if (playerBody != null)
            {
                playerBody.transform.position = Vector3.Lerp(playerBody.transform.position, new Vector3(playerHead.transform.position.x, playerHead.transform.position.y - 0.65F, playerHead.transform.position.z), Time.deltaTime * 6F);
            }
        }

        private void SetSpawnPosition()
        {
            switch (spawnType)
            {
                case SpawnType.Floor:
                    transform.position = new Vector3(transform.position.x, 0F, transform.position.z);
                    break;
            }
        }
    }
}