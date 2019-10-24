using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.CCUI
{
    public class CamPositionControl : MonoBehaviour
    {
        [Header("OBJECTS")]
        public Transform currentMount;
        private CameraMotionControls freeCameraScript;
        private Camera mainCamera;

        [Header("SETTINGS")]
        [Tooltip("Set 1.1 for instant fly")]
        [Range(0.01f, 1.1f)] public float speed = 0.1f;
        public float extraFOV = 1.0f;

        Vector3 lastPosition;

        void Start()
        {
            mainCamera = this.GetComponent<Camera>();
            freeCameraScript = this.GetComponent<CameraMotionControls>();
            lastPosition = transform.position;
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, currentMount.position, speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speed);
            mainCamera.fieldOfView = 60 + extraFOV;
            lastPosition = transform.position;
        }

        public void SetMount(Transform newMount)
        {
            currentMount = newMount;
            freeCameraScript.enabled = false;
        }
    }
}