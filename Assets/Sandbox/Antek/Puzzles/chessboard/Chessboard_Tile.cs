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

    [SerializeField] Material tileToPressMaterialAfter;

    [SerializeField] Material tileMaterialSolvedPuzzle;
    Material materialComponent;

    [SerializeField] FMODUnity.EventReference teleportSoundEvent;
    [SerializeField] FMODUnity.EventReference solvedPuzzleSound;
    
    static List<GameObject> TilesToPress = new List<GameObject>();
    static int NumberOfTilesPressed;
    bool wasTilePressed;


    [SerializeField] GameObject objectToShow;

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
    }
    void Start()
    {
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
                        NumberOfTilesPressed += 1;
                        wasTilePressed = true;
                        GetComponent<MeshRenderer>().material = tileToPressMaterialAfter;
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

                        if (teleportSoundEvent.IsNull == false)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot(teleportSoundEvent, tpPoint.position);
                        }
                        Chessboard_Events.current.TileTrapStep();
                    }
                
                    break;
                
                case TileType.TilePath:
                    GetComponent<MeshRenderer>().material = tilePathMaterialCrossed;
                    break;
                
                default:
                    break;
            }   
        }
    } 
    void StepOnTileTrap()
    {
        GetComponent<MeshRenderer>().material = tileNeutralMaterial;
        NumberOfTilesPressed = 0;
        if(tileType == TileType.TileToPress) wasTilePressed = false;
    }

    void SolvedPuzzle()
    {
        tileType = TileType.NeutralTile;
        GetComponent<MeshRenderer>().material = tileMaterialSolvedPuzzle;
        FMODUnity.RuntimeManager.PlayOneShotAttached(solvedPuzzleSound, gameObject); //solved puzzle sound
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
 
}
