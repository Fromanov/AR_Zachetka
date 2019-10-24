using UnityEngine;

namespace Michsky.UI.CCUI
{
    public class CameraAdjuster : MonoBehaviour
    {
        public CameraMotionControls cameraMotionControls;

        public void ToggleAutoRotate(bool isOn)
        {
            cameraMotionControls.autoRotate = isOn;
        }

        public void SetRotationSpeed(string speed)
        {
            cameraMotionControls.rotationSpeed = float.Parse(speed);
        }

        public void SetRotationSmoothing(string speed)
        {
            cameraMotionControls.rotationSmoothing = float.Parse(speed);
        }

        public void SetRotationSensitivity(string speed)
        {
            cameraMotionControls.rotationSensitivity = float.Parse(speed);
        }

        public void SetCameraDistance(string speed)
        {
            cameraMotionControls.zAxisDistance = float.Parse(speed);
        } 

        public void SetRotationLimitX(string range)
        {
            cameraMotionControls.rotationLimit.x = float.Parse(range);
        }

        public void SetRotationLimitY(string range)
        {
            cameraMotionControls.rotationLimit.y = float.Parse(range);
        }
    }
}