using System;
using UnityEngine;

public class Layer
{
   /* public int numNodesIn, numNodesOut;
    public double[,] costGradientW;
    public double[] costGradientB;
    public double[,] weights;
    public double[] biases;
    
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
                weights[nodeIn, nodeOut] -= costGradientW[nodeIn, nodeOut] * learnRate;
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
        return 1 / (1 + System.Math.Exp(-weightedInput));
    }

    double ActivationDerivative(double weightedInput)
    {
        double activation = ActivationFunction(weightedInput);
        return activation * (1 - activation);
    }
    

    double NodeCost(double outputActivation, double expectedOutput)
    {
        double error = outputActivation - expectedOutput;
        return error * error;
    }

    double NodeCostDerivative(double outputActivation, double expectedOutput)
    {
        return 2 * (outputActivation - expectedOutput);
    }
    
    
    public double[] CalculateOutputsLayerNodeValues(double[] expectedOutputs)
    {
        double[] nodeValues = new double[expectedOutputs.Length];

        for (int i = 0; i < nodeValues.Length; i++)
        {
            double costDerivative = NodeCostDerivative(activations[i], expectedOutputs[i]);
            double actiavtionDerivative = ActivationDerivative(weightedInputs[i]);
            nodeValues[i] = actiavtionDerivative * costDerivative;
        }

        return nodeValues;
    }

    public void UpdateGradients(double[] nodeValues)
    {
        for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++)
        {
            for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++)
            {
                double derevativeCostWrtWeight = inputs[nodeIn] * nodeValues[nodeOut];
                costGradientW[nodeIn, nodeOut] += derevativeCostWrtWeight;
            }

            double derivativeCostWrtBias = 1 * nodeValues[nodeOut];
            costGradientB[nodeOut] += derivativeCostWrtBias;
        }
    } */
}
