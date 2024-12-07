using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotHandler : MonoBehaviour
{
    static ScreenShotHandler instance;
    public NetworkImageCheck runModel;
    [SerializeField] RawImage image;
    int pngNumber;
    string path;
    
    void Awake()
    {
        instance = this;
        path = Directory.GetCurrentDirectory() + "/Assets"+"/Screenshots";
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
    }

    private IEnumerator CoroutineScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture texture = image.texture;
        Texture2D texture2D;
        
        RenderTexture renderTexture = RenderTexture.GetTemporary(
        texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            
        Graphics.Blit(texture, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;

        texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        
        byte[] byteArray = texture2D.EncodeToPNG();
        System.IO.File.WriteAllBytes( path + "/cameraScreenshot"+ pngNumber +".png" , byteArray);
        runModel.RunModel(texture2D);
        Debug.Log("Save");
    }

    void TakeScreenshot()
    {
        StartCoroutine(CoroutineScreenShot());
    }

    public static void TakeScreenshot_Static()
    {
        instance.TakeScreenshot();
    }
    
}
