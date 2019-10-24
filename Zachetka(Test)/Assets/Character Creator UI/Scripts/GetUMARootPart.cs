using UnityEngine;

namespace Michsky.UI.CCUI
{
    public class GetUMARootPart : MonoBehaviour
    {
        [Header("SETTINGS")]
        public string rootPartName;

        GameObject tempObj;

        void LateUpdate()
        {
            tempObj = GameObject.Find(rootPartName);
            if(tempObj != null)
            {
                gameObject.transform.SetParent(tempObj.transform);
                Destroy(this);
            }
        }
    }
}