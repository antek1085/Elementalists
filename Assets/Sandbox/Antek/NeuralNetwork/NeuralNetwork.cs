using UnityEngine;
using UnityEngine.UIElements;

public class NeuralNetwork
{
   Layer[] layers;
   
   // Create the Neural Network

   public NeuralNetwork(params int[] layerSizes)
   {
      layers = new Layer[layerSizes.Length - 1];
      for (int i = 0; i < layers.Length; i++)
      {
         layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
      }
   }
   
   
   // Run the input Values through the network to calculate the output values

   double[] CalculateOutputs(double[] inputs)
   {
      foreach (Layer layer in layers)
      {
         inputs = layer.CalculateOutputs(inputs);
      }
      return inputs;
   }

   int Classify(double[] inputs)
   {
      double[] outputs = CalculateOutputs(inputs);
      return IndexOfMaxValue(inputs);
   }

   double Cost(DataPoint[] data)
   {
      double totalCost = 0;
      foreach (DataPoint dataPoint in data)
      {
         totalCost += Cost(dataPoint );
      }
      return totalCost / data.Length;
   }
}
