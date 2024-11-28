using System;
using UnityEngine;

public class Layer
{
    int numNodesIn, numNodesOut;
    public double[,] costGradientW;
    public double[] costGradientB;
    double[,] weights;
    double[] biases;
    
    //Create the Layer

    public Layer(int numNodesIn, int numNodesOut)
    {
        costGradientW = new double[numNodesIn, numNodesOut];
        weights = new double[numNodesIn, numNodesOut];
        costGradientB = new double[numNodesOut];
        biases = new double[numNodesOut];
        this.numNodesIn = numNodesIn;
        this.numNodesOut = numNodesOut;

        InitializeRandomWeights();
    }

    public void ApplyGradients(double learnRate)
    {
        for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++)
        {
            biases[nodeOut] -= costGradientB[nodeOut] * learnRate;
            for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++)
            {
                weights[nodeIn, nodeOut] = costGradientW[nodeIn, nodeOut] * learnRate;
            }
        }
    }

    public void InitializeRandomWeights()
    {
        System.Random rng = new System.Random();

        for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++)
        {
            for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++)
            {
                double randomValue = rng.NextDouble() * 2 - 1;

                weights[nodeIn, nodeOut] = randomValue / Sqrt(numNodesIn);
            }
        }
    }
    
    
    
    
    //Calculate the output of the layer

    public double[] CalculateOutputs(double[] inputs)
    {
        double[] activations = new double[numNodesOut];

        for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++)
        {
            double weightedInput = biases[nodeOut];
            for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++)
            {
                weightedInput += inputs[nodeIn] * weights[nodeIn, nodeOut];
            }
            activations[nodeOut] = ActivationFunction(weightedInput);
        }
        return activations;
    }

    //Sigmoid Fuction
    double ActivationFunction(double weightedInput)
    {
        return 1 / (1 + Exp(-weightedInput));
        return (weightedInput > 0) ? 1 : 0;
    }

    double NodeCost(double outputActivation, double expectedOutput)
    {
        double error = outputActivation - expectedOutput;
        return error * error;
    }
}
