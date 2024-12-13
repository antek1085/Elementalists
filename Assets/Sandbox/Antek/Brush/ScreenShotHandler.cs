using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotHandler : MonoBehaviour
{
    static ScreenShotHandler instance;
    public NeuralNetworkImageCheck runModel;
    [SerializeField] RawImage image;
    private Texture2D texture2D;
    int pngNumber;
    string path;
    
    void Awake()
    {
        instance = this;
        path = Directory.GetCurrentDirectory() + "/Assets"+"/Screenshots";
        texture2D = new Texture2D(512, 512, TextureFormat.RGB24, false);
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
    }

    private IEnumerator CoroutineScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture texture = image.texture;
        
        RenderTexture renderTexture = RenderTexture.GetTemporary(
        texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        /*texture2D.width = texture.width;
        texture2D.height = texture.height;*/
            
        Graphics.Blit(texture, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        
        /*byte[] byteArray = texture2D.EncodeToPNG();*/
        /*System.IO.File.WriteAllBytes( path + "/cameraScreenshot"+ pngNumber +".png" , byteArray);*/
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
