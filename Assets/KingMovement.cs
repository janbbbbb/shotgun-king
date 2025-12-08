using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject kingPrefab;
    private Vector3 playerPosition;
    public int turns = 3;

    private static readonly Vector3[] KingOffsets = new Vector3[]
    {
        new Vector3( 1, 0,  0),
        new Vector3( 1, 0,  1),
        new Vector3( 0, 0,  1),
        new Vector3(-1, 0,  1),
        new Vector3(-1, 0,  0),
        new Vector3(-1, 0, -1),
        new Vector3( 0, 0, -1),
        new Vector3( 1, 0, -1)
    };

    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private Vector3 globalOldPosition;
    private Vector3 globalNewPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        targetPosition = transform.position;
        globalOldPosition = transform.position;
        globalNewPosition = transform.position;
    }

    private void OnEnable()
    {
        GameManager.OnTurnChanged += HandleTurnChange;
    }

    private void OnDisable()
    {
        GameManager.OnTurnChanged -= HandleTurnChange;
    }

    public Vector3[] GenerateMoves(Vector3[] offsets)
    {
        Vector3 current = transform.position;
        List<Vector3> occupiedPositions = BoardManager.Instance.GetAllPiecesWorldPositions();

        Vector3[] temp = new Vector3[offsets.Length];
        int count = 0;

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector3 target = current + offsets[i];

            if (Mathf.Abs(target.x) <= 3.5f && Mathf.Abs(target.z) <= 3.5f)
            {
                if (!Physics.CheckBox(target, Vector3.one * 0.4f))
                {
                    bool isOccupied = occupiedPositions.Contains(target);
                    if (!isOccupied)
                        temp[count++] = target;
                }
            }
        }

        if (count == offsets.Length)
            return temp;

        Vector3[] result = new Vector3[count];
        System.Array.Copy(temp, result, count);
        return result;
    }

    private Vector3 SelectMove(Vector3[] possibleMoves)
    {
        GameObject playerObject = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (playerObject == null)
            return Vector3.zero;

        Vector3 playerPosition = playerObject.transform.position;
        float[] points = new float[possibleMoves.Length];

        for (int i = 0; i < possibleMoves.Length; i++)
        {
            float distance = Vector3.Distance(possibleMoves[i], playerPosition);

            points[i] = -Mathf.Round(distance);

            if (IsCheck(possibleMoves[i]))
            {
                points[i] += 5f;
            }

            Vector3 pos = possibleMoves[i];
            if (Mathf.Abs(pos.x) > 3f && Mathf.Abs(pos.z) > 3f)
            {
                points[i] -= 1.5f;
            }
        }

        int bestIndex = 0;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i] > points[bestIndex])
                bestIndex = i;
        }

        return possibleMoves[bestIndex];
    }

    private bool IsCheck(Vector3 position)
    {
        return false;
    }

    private void HandleTurnChange()
    {
        Vector3[] moves = GenerateMoves(KingOffsets);

        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            // NATYCHMIASTOWE BICIE GRACZA
            foreach (var move in moves)
            {
                if (Vector3.Distance(move, playerPos) < 0.1f)
                {
                    Debug.Log("KRÓL BIJE GRACZA NATYCHMIAST (ignore turns)");
                    Move(move);
                    return;
                }
            }
        }

        // normalny ruch co X tur
        if (GameManager.Instance.turnCounter % turns != 0)
            return;

        PrintMoves(moves);

        Vector3 selected = SelectMove(moves);
        Debug.Log("Najlepszy ruch to:");
        Debug.Log(selected);
        Move(selected);
    }

    public void PrintMoves(Vector3[] possible)
    {
        Debug.Log("Ruchy króla:");
        foreach (var move in possible)
            Debug.Log(move);
    }

    public void Move(Vector3 move)
    {
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player != null && Vector3.Distance(player.transform.position, move) < 0.1f)
        {
            Destroy(player);
            Debug.Log("KRÓL ZABIŁ GRACZA");
        }

        GameObject kingObject = BoardManager.Instance.GetPieceByName("kingPrefab(Clone)");
        transform.position = move;
        BoardManager.Instance.UpdatePiecePosition(kingObject, move);
    }
}