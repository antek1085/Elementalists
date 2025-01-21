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
        texture2D = new Texture2D(512, 512, TextureFormat.RGB24, false);
    }
    void OnEnable()
    {
        StartCoroutine(PlayerFeedbackCoroutine());
    }

    private IEnumerator CoroutineScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture texture = image.texture;
        
        RenderTexture renderTexture = RenderTexture.GetTemporary(
        texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        Graphics.Blit(texture, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        
        runModel.RunModel(texture2D);
    }
    
    private IEnumerator PlayerFeedbackCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Texture texture = image.texture;
        
        RenderTexture renderTexture = RenderTexture.GetTemporary(
        texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        Graphics.Blit(texture, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        
        runModel.RunModelPlayerFeedback(texture2D);
        StartCoroutine(PlayerFeedbackCoroutine());
    }

    void TakeScreenshot()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineScreenShot());
    }

    public static void TakeScreenshot_Static()
    {
        instance.TakeScreenshot();
    }
    
}
