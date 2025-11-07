using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject kingPrefab;
    public GameObject queenPrefab;
    public GameObject pawnPrefab;
    public GameObject bishopPrefab;
    public GameObject knightPrefab;
    public GameObject rookPrefab;

    private int currentLevel = 1;
    private const int maxLevel = 12;

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

        foreach (var piece in GameObject.FindGameObjectsWithTag("Piece"))
            Destroy(piece);

        switch (levelIndex)
        {
            case 1:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 1));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 2:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 1));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 3:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 4:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 5:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(pawnPrefab, new Vector2Int(2, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 6:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(bishopPrefab, new Vector2Int(5, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 7:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(2, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(6, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 8:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(rookPrefab, new Vector2Int(7, 0));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(6, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 9:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(rookPrefab, new Vector2Int(7, 0));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 1));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(6, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(1, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 10:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(rookPrefab, new Vector2Int(7, 0));
                SpawnPiece(pawnPrefab, new Vector2Int(1, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(2, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(6, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 11:
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 1));
                SpawnPiece(knightPrefab, new Vector2Int(5, 1));
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(rookPrefab, new Vector2Int(7, 0));
                SpawnPiece(pawnPrefab, new Vector2Int(0, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(1, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(2, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(3, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(4, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(5, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(6, 2));
                SpawnPiece(pawnPrefab, new Vector2Int(7, 2));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;

            case 12:
                SpawnPiece(rookPrefab, new Vector2Int(0, 0));
                SpawnPiece(knightPrefab, new Vector2Int(1, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(2, 0));
                SpawnPiece(queenPrefab, new Vector2Int(3, 0));
                SpawnPiece(kingPrefab, new Vector2Int(4, 0));
                SpawnPiece(bishopPrefab, new Vector2Int(5, 0));
                SpawnPiece(knightPrefab, new Vector2Int(6, 0));
                SpawnPiece(rookPrefab, new Vector2Int(7, 0));
                for (int i = 0; i < 8; i++)
                    SpawnPiece(pawnPrefab, new Vector2Int(i, 1));
                SpawnPiece(playerPrefab, new Vector2Int(4, 7));
                break;
        }
    }


        private void SpawnPiece(GameObject prefab, Vector2Int boardPos)
    {
        Vector3 worldPos = new Vector3(boardPos.x - 3.5f, 0, boardPos.y - 3.5f);
        GameObject piece = Instantiate(prefab, worldPos, Quaternion.Euler(90f, 180f, 0f));
        piece.tag = "Piece";
        BoardManager.Instance.RegisterPiece(piece);
    }

}
