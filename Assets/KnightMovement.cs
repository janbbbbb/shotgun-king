using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private static readonly Vector3[] KnightOffsets = new Vector3[]
    {
        new Vector3( 1, 0,  2),
        new Vector3( 2, 0,  1),
        new Vector3( 2, 0, -1),
        new Vector3( 1, 0, -2),
        new Vector3(-1, 0, -2),
        new Vector3(-2, 0, -1),
        new Vector3(-2, 0,  1),
        new Vector3(-1, 0,  2)
    };

    private void OnEnable()
    {
        GameManager.OnTurnChanged += HandleTurnChange;
    }

    private void OnDisable()
    {
        GameManager.OnTurnChanged -= HandleTurnChange;
    }

    private void HandleTurnChange()
    {
        if (GameManager.Instance.turnCounter % 2 != 0)
            return;
        Vector3[] moves = GenerateMoves(KnightOffsets);
        PrintMoves(moves);

        Vector3 selected = SelectMove(moves);
        Move(selected);
    }

    private Vector3[] GenerateMoves(Vector3[] offsets)
    {
        Vector3 current = transform.position;
        List<Vector3> result = new List<Vector3>();

        List<Vector3> occupiedPositions = BoardManager.Instance.GetAllPiecesWorldPositions();

        foreach (Vector3 offset in offsets)
        {
            Vector3 target = current + offset;

            if (Mathf.Abs(target.x) > 3.5f || Mathf.Abs(target.z) > 3.5f)
                continue;

            bool occupiedByOther = false;
            foreach (Vector3 pos in occupiedPositions)
            {
                GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
                Vector3 playerPos = player != null ? player.transform.position : Vector3.zero;

                if (Vector3.Distance(pos, target) < 0.1f && Vector3.Distance(pos, playerPos) > 0.1f)
                {
                    occupiedByOther = true;
                    break;
                }
            }

            if (!occupiedByOther)
            {
                result.Add(target);
            }
        }

        return result.ToArray();
    }

    private Vector3 SelectMove(Vector3[] possibleMoves)
    {
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player == null)
            return transform.position;

        Vector3 playerPos = player.transform.position;
        int[] points = new int[possibleMoves.Length];

        for (int i = 0; i < possibleMoves.Length; i++)
        {
            float distance = Vector3.Distance(possibleMoves[i], playerPos);
            points[i] = -Mathf.RoundToInt(distance);

            // ruch bij¹cy gracza – priorytet
            if (Vector3.Distance(possibleMoves[i], playerPos) < 0.1f)
            {
                points[i] += 100;
            }

            // jeœli masz logikê szacha, mo¿esz tu dodaæ punkty np. points[i] += 5;
        }

        int bestIndex = 0;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i] > points[bestIndex])
                bestIndex = i;
        }

        return possibleMoves[bestIndex];
    }

    private void Move(Vector3 newPos)
    {
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player != null && Vector3.Distance(player.transform.position, newPos) < 0.1f)
        {
            Destroy(player);
            Debug.Log("KOÑ WYGRA£ – GRACZ ZOSTA£ ZABITY");
        }

        transform.position = newPos;
        BoardManager.Instance.UpdatePiecePosition(gameObject, newPos);
    }

    private void PrintMoves(Vector3[] moves)
    {
        Debug.Log("Mo¿liwe ruchy konia:");
        foreach (var m in moves)
            Debug.Log(m);
    }
}