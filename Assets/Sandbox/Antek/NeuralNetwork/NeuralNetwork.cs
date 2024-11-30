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

   public void Learn(DataPoint[] trainingData, double learnRate)
   {
      const double h = 0.0001;
      double originalCost = Cost(trainingData);

      foreach (Layer layer in layers)
      {
         
         // Calculate the cost gradient for the current weights
         for (int nodeIn = 0; nodeIn < layer.numNodesIn; nodeIn++)
         {
            for (int nodeOut = 0; nodeOut < layer.numNodesOut; nodeOut++)
            {
               layer.weights[nodeIn, nodeOut] += h;
               double deltaCost = Cost(trainingData) - originalCost;
               layer.weights[nodeIn, nodeOut] -= h;
               layer.costGradientW[nodeIn, nodeOut] = deltaCost / h;
            }
         }

         
         // Calculate the cost gradient for the current biases
         for (int biasIndex = 0; biasIndex < layer.biases.Length; biasIndex++)
         {
            layer.biases[biasIndex] += h;
            double deltaCost = Cost(trainingData) - originalCost;
            layer.biases[biasIndex] -= h;
            layer.costGradientB[biasIndex] = deltaCost / h;
         }


         ApplyGradients(trainingData);
      }
   }


   void UpdateAllGradients(DataPoint dataPoint)
   {
      CalculateOutputs(dataPoint.inputs);

      Layer outputLayer = layers[layers.Length - 1];
      double[] nodeValues = outputLayer.CalculateOutputsLayerNodeValues(dataPoint.expectedOutputs);
      outputLayer.UpdateGradients(nodeValues);
   }
   
}
