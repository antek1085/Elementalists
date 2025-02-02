using System;
using UnityEngine;
using Unity.Sentis;

public class NeuralNetworkImageCheck : MonoBehaviour
{
     public ModelAsset onnxAsset;
     private Model runTimeModel;
     private Model feedBackModel;
     private Worker worker;
     private Worker feedBackWorker;
     

      private void Start()
      {
          runTimeModel = ModelLoader.Load(onnxAsset);
          feedBackModel = ModelLoader.Load(onnxAsset);
      }
      
      public void RunModel(Texture2D imageToRecognise)
      {
              Tensor<float> inputTensor = TextureConverter.ToTensor(imageToRecognise,
              new TextureTransform().SetTensorLayout(TensorLayout.NHWC));
              worker = new Worker(runTimeModel, BackendType.GPUCompute);
              
              worker.Schedule(inputTensor);
              
              inputTensor.Dispose();
              Tensor<float> outputTensorFire = worker.PeekOutput(0) as Tensor<float>;
              float[] outputData = outputTensorFire?.DownloadToArray();
              worker.Dispose();
              outputTensorFire.Dispose();

              // Fire Spell 0  ||| Water Spell 1

              DrawingSpellsEvent.current.SpellCast(outputData);
         
      }
      
      public void RunModelPlayerFeedback(Texture2D imageToRecognise)
      {
          Tensor<float> inputTensor = TextureConverter.ToTensor(imageToRecognise,
          new TextureTransform().SetTensorLayout(TensorLayout.NHWC));
          
          feedBackWorker = new Worker(feedBackModel, BackendType.GPUCompute);
          
          feedBackWorker.Schedule(inputTensor);
          inputTensor.Dispose();
          Tensor<float> outputTensorFire = feedBackWorker.PeekOutput(0) as Tensor<float>;
          float[] outputData = outputTensorFire?.DownloadToArray();
          feedBackWorker.Dispose();
          outputTensorFire.Dispose();

          // Fire Spell 0  ||| Water Spell 1

          DrawingSpellsEvent.current.SpellFloatUI(outputData);
      }

      void OnDestroy()
      {
          if (worker != null)
          {
              worker.Dispose();
          }
      }
      void OnDisable()
      {
          
      }

}
