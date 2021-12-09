#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class EKPostProcess : MonoBehaviour
{
    [PostProcessBuild]
    public static void ChangePlist(BuildTarget buildTarget, string projectPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string plistPath = projectPath + "/Info.plist";
            PlistDocument plist = new PlistDocument();

            plist.ReadFromFile(plistPath);
            plist.root["ITSAppUsesNonExemptEncryption"] = new PlistElementBoolean(false);
            plist.WriteToFile(plistPath);
        }
    }
}
#endif