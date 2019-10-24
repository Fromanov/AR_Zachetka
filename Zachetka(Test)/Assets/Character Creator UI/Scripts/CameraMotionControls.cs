using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.CCUI
{
    public enum ZoomMode
    {
        CameraFieldOfView,
        ZAxisDistance
    }

    public class CameraMotionControls : MonoBehaviour
    {
        [Header("AUTO ROTATION")]
        [Tooltip("Toggles whether the camera will automatically rotate around it's target")]
        public bool autoRotate = true;
        [Tooltip("The speed at which the camera will auto-pan.")]
        public float rotationSpeed = 0.1f;
        [Tooltip("The rotation along the y-axis the camera will have at start.")]
        public float startRotation = 180;
        [Header("MANUAL ROTATION")]
        [Tooltip("The smoothness coming to a stop of the camera afer the uses pans the camera and releases. Lower values result in significantly smoother results. This means the camera will take longer to stop rotating")]
        public float rotationSmoothing = 2f;
        [Tooltip("The object the camera will focus on.")]
        public Transform target;
        [Tooltip("How sensative the camera-panning is when the user pans -- the speed of panning.")]
        public float rotationSensitivity = 1f;
        [Tooltip("The min and max distance along the Y-axis the camera is allowed to move when panning.")]
        public Vector2 rotationLimit = new Vector2(5, 80);
        [Tooltip("The position along the Z-axis the camera game object is.")]
        public float zAxisDistance = 0.45f;
        [Header("ZOOMING")]
        public Slider zoomSlider;
        public bool allowMouseWheel = true;
        public float wheelScrollSpeed = 0.5f;

        new private Camera camera;
        private float cameraFieldOfView;
        new private Transform transform;
        private float xVelocity;
        private float yVelocity;
        private float xRotationAxis;
        private float yRotationAxis;
        private float backToNormalSpeed;

        private void Awake()
        {
            camera = GetComponent<Camera>();
            transform = GetComponent<Transform>();
        }

        public void DisableRotating()
        {
            backToNormalSpeed = rotationSensitivity;
            rotationSensitivity = 0;
        }

        public void EnableRotating()
        {
            rotationSensitivity = backToNormalSpeed;
        }

        public void ZoomWithSlider()
        { 
            zAxisDistance = zoomSlider.value;
        }

        private void Start()
        {
            cameraFieldOfView = camera.fieldOfView;
            //Sets the camera's rotation along the y axis.
            //The reason we're dividing by rotationSpeed is because we'll be multiplying by rotationSpeed in LateUpdate.
            //So we're just accouinting for that at start.
            xRotationAxis = startRotation / rotationSpeed;
        }

        private void LateUpdate()
        {
            //If auto rotation is enabled, just increment the xVelocity value by the rotationSensitivity.
            //As that value's tied to the camera's rotation, it'll rotate automatically.
            if (autoRotate)
            {
                xVelocity += rotationSensitivity * Time.deltaTime;
            }

            if (target)
            {
                Quaternion rotation;
                Vector3 position;
                float deltaTime = Time.deltaTime;

                //We only really want to capture the position of the cursor when the screen when the user is holding down left click/touching the screen
                //That's why we're checking for that before campturing the mouse/finger position.
                //Otherwise, on a computer, the camera would move whenever the cursor moves. 
                if (Input.GetMouseButton(0))
                {
                    xVelocity += Input.GetAxis("Mouse X") * rotationSensitivity;
                    yVelocity -= Input.GetAxis("Mouse Y") * rotationSensitivity;
                }

                xRotationAxis += xVelocity;
                yRotationAxis += yVelocity;

                //Clamp the rotation along the y-axis between the limits we set. 
                //Limits of 360 or -360 on any axis will allow the camera to rotate unrestricted
                yRotationAxis = ClampAngleBetweenMinAndMax(yRotationAxis, rotationLimit.x, rotationLimit.y);

                rotation = Quaternion.Euler(yRotationAxis, xRotationAxis * rotationSpeed, 0);
                position = rotation * new Vector3(0f, 0f, -zAxisDistance) + target.position;

                transform.rotation = rotation;
                transform.position = position;

                xVelocity = Mathf.Lerp(xVelocity, 0, deltaTime * rotationSmoothing);
                yVelocity = Mathf.Lerp(yVelocity, 0, deltaTime * rotationSmoothing);

                float minWheelValue = zoomSlider.minValue;
                minWheelValue = minWheelValue + wheelScrollSpeed;

                if (allowMouseWheel == true)
                {
                    if (Input.GetAxis("Mouse ScrollWheel") > 0f && zAxisDistance >= minWheelValue)
                    {
                        zAxisDistance -= wheelScrollSpeed;
                        zoomSlider.value = zAxisDistance;
                    }

                    else if (Input.GetAxis("Mouse ScrollWheel") < 0f && zoomSlider.value <= zoomSlider.maxValue)
                    {
                        zAxisDistance += wheelScrollSpeed;
                        zoomSlider.value = zAxisDistance;
                        zAxisDistance = zoomSlider.value;
                    }
                }
            }        
        }

        //Prevents the camera from locking after rotating a certain amount if the rotation limits are set to 360 degrees.
        private float ClampAngleBetweenMinAndMax(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }

            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }
    }
}