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

    // Track enemies alive in this level
    private List<EnemyHealth> aliveEnemies = new List<EnemyHealth>();

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
        if (Input.GetKeyDown(KeyCode.N))
            NextLevel();
    }

    private void NextLevel()
    {
        currentLevel++;
        if (currentLevel > maxLevel)
            currentLevel = 1;

        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {
        Debug.Log($"=== Loading Level {levelIndex} ===");

        // Clear lists
        aliveEnemies.Clear();

        // Clear board
        BoardManager.Instance.ClearBoard();

        // Destroy old pieces
        foreach (var piece in GameObject.FindGameObjectsWithTag("Piece"))
            Destroy(piece);

        // Load level layout
        switch (levelIndex)
        {
            case 1:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 1));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 2:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 1));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 3:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 4:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 5:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(pawnPrefab, new Vector2Int(2, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 6:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(bishopPrefab, new Vector2Int(5, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 7:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(2, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(6, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 8:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(rookPrefab, new Vector2Int(7, 0));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(6, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 9:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(rookPrefab, new Vector2Int(7, 0));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 1));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(6, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(1, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 10:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(rookPrefab, new Vector2Int(7, 0));
                SpawnEnemy(pawnPrefab, new Vector2Int(1, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(2, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(3, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(4, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(5, 2));
                SpawnEnemy(pawnPrefab, new Vector2Int(6, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 11:
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 1));
                SpawnEnemy(knightPrefab, new Vector2Int(5, 1));
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(rookPrefab, new Vector2Int(7, 0));
                for (int i = 0; i < 8; i++)
                    SpawnEnemy(pawnPrefab, new Vector2Int(i, 2));
                SpawnPlayer(new Vector2Int(4, 7));
                break;

            case 12:
                SpawnEnemy(rookPrefab, new Vector2Int(0, 0));
                SpawnEnemy(knightPrefab, new Vector2Int(1, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(2, 0));
                SpawnEnemy(queenPrefab, new Vector2Int(3, 0));
                SpawnEnemy(kingPrefab, new Vector2Int(4, 0));
                SpawnEnemy(bishopPrefab, new Vector2Int(5, 0));
                SpawnEnemy(knightPrefab, new Vector2Int(6, 0));
                SpawnEnemy(rookPrefab, new Vector2Int(7, 0));
                for (int i = 0; i < 8; i++)
                    SpawnEnemy(pawnPrefab, new Vector2Int(i, 1));
                SpawnPlayer(new Vector2Int(4, 7));
                break;
        }
    }

    private void SpawnPlayer(Vector2Int pos)
    {
        SpawnPiece(playerPrefab, pos);
    }

    private void SpawnEnemy(GameObject prefab, Vector2Int pos)
    {
        GameObject obj = SpawnPiece(prefab, pos);

        EnemyHealth eh = obj.GetComponent<EnemyHealth>();
        if (eh != null)
        {
            aliveEnemies.Add(eh);

            // Correct signature: (EnemyHealth enemy)
            eh.OnDeath += (enemy) =>
            {
                aliveEnemies.Remove(enemy);

                if (aliveEnemies.Count == 0)
                    NextLevel();
            };
        }
    }

    private GameObject SpawnPiece(GameObject prefab, Vector2Int boardPos)
    {
        Vector3 worldPos = new Vector3(boardPos.x - 3.5f, 0, boardPos.y - 3.5f);
        GameObject piece = Instantiate(prefab, worldPos, Quaternion.Euler(90f, 180f, 0f));
        piece.tag = "Piece";
        BoardManager.Instance.RegisterPiece(piece);
        return piece;
    }
}
