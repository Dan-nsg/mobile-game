using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public ArtManager.ArtType artType;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesEnd;
    public int piecesStartNumber = 3;
    public int piecesNumber = 5;
    public int piecesEndNumber = 1;
    public float timeBetweenPieces = .3f;

    [SerializeField] private int _index;

    private GameObject _currentLevel;

    private List<LevelPieceBase> _spawnedPieces;

    private void Awake() 
    {
        //SpawnNextLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;


            //0 >= 2
            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }


        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }


    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region
    private void CreateLevelPieces()
    {
        _spawnedPieces = new List<LevelPieceBase>();
        CleanSpawnedPieces();

        for(int i = 0; i < piecesStartNumber; i++)
        {
            CreateLevelPiece(levelPiecesStart);
        }
        for(int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPieces);
        }
        for(int i = 0; i < piecesEndNumber; i++)
        {
            CreateLevelPiece(levelPiecesEnd);
        }
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            
            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currentSetup.artType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
    }

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for(int i = 0; i < piecesNumber; i++)
        { 
            CreateLevelPiece(levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }
    #endregion

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }
}
