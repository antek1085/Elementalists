using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotHandler : MonoBehaviour
{
    static ScreenShotHandler instance;
    [SerializeField] RawImage image;
    
    void Awake()
    {
        instance = this;
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

        texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        
        byte[] byteArray = texture2D.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/cameraScreenshot.png",byteArray);
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
