using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotHandler : MonoBehaviour
{
    [HideInInspector] public Camera cam;
    [HideInInspector] public DeviceInfo device;
    [HideInInspector] public int width = 1024;
    [HideInInspector] public int height = 1024;
    [HideInInspector] public string path;
    [HideInInspector] public KeyCode screenshotKey = KeyCode.Space;

    private string imageName
    {
        get
        {
            string temp = System.DateTime.Now.ToString("HH-mm-ss") + ".png";
            return temp;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenShot();
        }
    }

    public void TakeScreenShot()
    {
        if (device == DeviceInfo.iPhone_5_5)
        {
            width = 1242;
            height = 2208;
        }
        else if (device == DeviceInfo.iPhone_6_5)
        {
            width = 1242;
            height = 2688;
        }
        else if (device == DeviceInfo.iPad)
        {
            width = 2048;
            height = 2732;
        }

        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture rt = new RenderTexture(width, height, 24);

        cam.targetTexture = rt;
        cam.Render();
        RenderTexture.active = rt;

        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        cam.targetTexture = null;
        RenderTexture.active = null;

        System.IO.File.WriteAllBytes(path + imageName, texture.EncodeToPNG());
    }
}

[System.Serializable]
public enum DeviceInfo
{
    custom,
    iPhone_5_5,
    iPhone_6_5,
    iPad
}