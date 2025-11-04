using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject kingPrefab;
    public GameObject pawnPrefab;
    public GameObject bishopPrefab;
    public GameObject knightPrefab;
    public GameObject rookPrefab;

    private int currentLevel = 1;
    private const int maxLevel = 3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadLevel(currentLevel);
    }

    private void Update()
    {
        // Press N to go to the next level
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentLevel++;
            if (currentLevel > maxLevel)
                currentLevel = 1;

            LoadLevel(currentLevel);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        Debug.Log($"=== Loading Level {levelIndex} ===");
        BoardManager.Instance.ClearBoard();

        // remove all old pieces from scene
        foreach (var piece in GameObject.FindGameObjectsWithTag("Piece"))
            Destroy(piece);

        switch (levelIndex)
        {
            case 1:
                SpawnPiece(kingPrefab, new Vector2Int(4, 7));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 6));
                SpawnPiece(playerPrefab, new Vector2Int(4, 0));
                break;

            case 2:
                SpawnPiece(kingPrefab, new Vector2Int(4, 7));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 6));
                SpawnPiece(knightPrefab, new Vector2Int(5, 5));
                SpawnPiece(pawnPrefab, new Vector2Int(2, 5));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 6));
                SpawnPiece(playerPrefab, new Vector2Int(4, 0));
                break;

            case 3:
                SpawnPiece(kingPrefab, new Vector2Int(4, 7));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 6));
                SpawnPiece(knightPrefab, new Vector2Int(5, 5));
                SpawnPiece(rookPrefab, new Vector2Int(0, 7));
                SpawnPiece(pawnPrefab, new Vector2Int(2, 5));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 6));
                SpawnPiece(pawnPrefab, new Vector2Int(6, 6));
                SpawnPiece(playerPrefab, new Vector2Int(4, 0));
                break;
        }
    }

    private void SpawnPiece(GameObject prefab, Vector2Int boardPos)
    {
        Vector3 worldPos = new Vector3(boardPos.x - 3.5f, 0, boardPos.y - 3.5f);
        var piece = Instantiate(prefab, worldPos, Quaternion.identity);
        piece.tag = "Piece";
        BoardManager.Instance.RegisterPiece(piece);
    }
}
