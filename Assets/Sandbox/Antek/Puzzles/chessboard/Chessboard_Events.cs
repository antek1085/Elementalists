using System;
using UnityEngine;

public class Chessboard_Events : MonoBehaviour
{
  public static Chessboard_Events current;
  void Awake()
  {
    current = this;
  }

  public event Action OnTileTrapStep;

  public void TileTrapStep()
  {
    if (OnTileTrapStep != null)
    {
      OnTileTrapStep();
    }
  }

  public event Action OnSolvingPuzzle;

  public void SolvingPuzzle()
  {
    if (OnSolvingPuzzle != null)
    {
      OnSolvingPuzzle();
    }
  }
}
