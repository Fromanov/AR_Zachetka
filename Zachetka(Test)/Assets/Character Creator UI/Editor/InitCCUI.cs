using UnityEngine;
using UnityEditor;

public class InityCCUI : MonoBehaviour
{
    [InitializeOnLoad]
    public class InitOnLoad
    {
        static InitOnLoad()
        {
            if (!EditorPrefs.HasKey("CCUI.Installed"))
            {
                EditorPrefs.SetInt("CCUI.Installed", 1);
                EditorUtility.DisplayDialog("Hello there!", "Thank you for purchasing Character Creator UI.\r\rFirst of all, import TextMesh Pro from Package Manager if you haven't already.\r\rYou can import UMA integration from Character Creator > UMA > Integrations folder. I'd highly reccomend to use this package with UMA.\r\rYou can contact me at isa.steam@outlook.com for support.", "Got it!");
            }
        }
    }
}