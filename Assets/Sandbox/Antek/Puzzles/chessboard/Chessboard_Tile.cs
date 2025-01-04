using System;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard_Tile : MonoBehaviour
{ 
    public enum TileType
    {
        TileToPress,
        TileTrap,
        TilePath,
        NeutralTile,
    }
    
    [SerializeField] TileType tileType;
    [Tooltip("if Value is set to null it will tp to the parent")]
    [SerializeField] Transform tpPoint;
    
    [SerializeField] Material tileNeutralMaterial;
    [Tooltip("Material when player step on Right Tile")]
    [SerializeField] Material tilePathMaterialCrossed;

    [SerializeField] Material tileMaterialSolvedPuzzle;
    Material materialComponent;
    
    static List<GameObject> TilesToPress = new List<GameObject>();
    int NumberOfTilesPressed;
    bool wasTilePressed;

    void Awake()
    {
       materialComponent = GetComponent<MeshRenderer>().material;
       materialComponent = tileNeutralMaterial;
        switch (tileType)
        {

            case TileType.TileToPress:
                TilesToPress.Add(gameObject);
                wasTilePressed = false;
                break;
            
            case TileType.TileTrap:
                if (tpPoint == null)
                {
                    tpPoint = transform.parent;
                }
                break;
            
            case TileType.TilePath:
                
                break;
            
            default:
                break;
        }
        Chessboard_Events.current.OnTileTrapStep += StepOnTileTrap;
        Chessboard_Events.current.OnSolvingPuzzle += SolvedPuzzle;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(tileType);
            switch (tileType)
            {

                case TileType.TileToPress:
                    if (!wasTilePressed)
                    {
                        NumberOfTilesPressed++;
                        wasTilePressed = true;
                        if (NumberOfTilesPressed == TilesToPress.Count)
                        {
                            Chessboard_Events.current.SolvingPuzzle();
                        }
                    }
                    break;
                
                case TileType.TileTrap:
                    if (tpPoint != null)
                    {
                        other.GetComponent<Transform>().transform.parent.position = tpPoint.position;
                        Chessboard_Events.current.TileTrapStep();
                    }
                
                    break;
                
                case TileType.TilePath:
                    materialComponent = tilePathMaterialCrossed;
                    break;
                
                default:
                    break;
            }   
        }
    } 
    void StepOnTileTrap()
    {
        materialComponent = tileNeutralMaterial;
        NumberOfTilesPressed = 0;
        if(tileType == TileType.TileToPress) wasTilePressed = false;
    }

    void SolvedPuzzle()
    {
        tileType = TileType.NeutralTile;
        materialComponent = tileMaterialSolvedPuzzle;
    }
}
