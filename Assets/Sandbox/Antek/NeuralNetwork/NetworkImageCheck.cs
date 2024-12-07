using System;
using UnityEngine;
using Unity.Sentis;

public class NetworkImageCheck : MonoBehaviour
{
     public ModelAsset onnxAsset;
     private Model runTimeModel;
     private Worker worker;

      private void Awake()
      {
          runTimeModel = ModelLoader.Load(onnxAsset);
      }
      
      public void RunModel(Texture2D imageToRecognise)
      {
          worker = new Worker(runTimeModel, BackendType.GPUCompute);
          
          Tensor<float> inputTensor = TextureConverter.ToTensor(imageToRecognise,
              new TextureTransform().SetTensorLayout(TensorLayout.NHWC));
          
          worker.Schedule(inputTensor);
          Tensor<float> outputTensorFire = worker.PeekOutput(0) as Tensor<float>;
          float[] outputData = outputTensorFire.DownloadToArray();
          worker.Dispose();
          
          // Fire Spell 0  ||| Water Spell 1
          Debug.Log(outputData[0] + "Fire");
          Debug.Log(outputData[1] + "Water");

      }

}
