using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int turns = 2;

    private static readonly Vector3[] QueenOffsets = new Vector3[]
    {
        // linie proste
        new Vector3( 1, 0, 0),
        new Vector3( 2, 0, 0),
        new Vector3( 3, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(-2, 0, 0),
        new Vector3(-3, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, 2),
        new Vector3(0, 0, 3),
        new Vector3(0, 0, -1),
        new Vector3(0, 0, -2),
        new Vector3(0, 0, -3),
        // przek¹tne
        new Vector3( 1, 0,  1),
        new Vector3( 2, 0,  2),
        new Vector3( 3, 0,  3),
        new Vector3(-1, 0,  1),
        new Vector3(-2, 0,  2),
        new Vector3(-3, 0,  3),
        new Vector3( 1, 0, -1),
        new Vector3( 2, 0, -2),
        new Vector3( 3, 0, -3),
        new Vector3(-1, 0, -1),
        new Vector3(-2, 0, -2),
        new Vector3(-3, 0, -3)
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
        Vector3[] moves = GenerateMoves(QueenOffsets);

        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            // NATYCHMIASTOWE BICIE GRACZA
            foreach (var move in moves)
            {
                if (Vector3.Distance(move, playerPos) < 0.1f)
                {
                    Debug.Log("KRÓLOWA BIJE GRACZA NATYCHMIAST (ignore turns)");
                    Move(move);
                    return;
                }
            }
        }

        // normalny ruch co X tur
        if (GameManager.Instance.turnCounter % turns != 0)
            return;

        Vector3 selected = SelectMove(moves);
        Move(selected);
    }

    private Vector3[] GenerateMoves(Vector3[] offsets)
    {
        Vector3 current = transform.position;
        List<Vector3> result = new List<Vector3>();
        List<Vector3> occupiedPositions = BoardManager.Instance.GetAllPiecesWorldPositions();
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        Vector3 playerPos = player != null ? player.transform.position : Vector3.zero;

        foreach (Vector3 offset in offsets)
        {
            Vector3 target = current + offset;

            if (Mathf.Abs(target.x) > 3.5f || Mathf.Abs(target.z) > 3.5f)
                continue;

            bool blocked = false;
            foreach (Vector3 pos in occupiedPositions)
            {
                if (Vector3.Distance(pos, target) < 0.2f && Vector3.Distance(pos, playerPos) > 0.1f)
                {
                    blocked = true;
                    break;
                }
            }

            if (!blocked)
                result.Add(target);
        }

        return result.ToArray();
    }

    private Vector3 SelectMove(Vector3[] possibleMoves)
    {
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player == null) return transform.position;

        Vector3 playerPos = player.transform.position;
        float[] points = new float[possibleMoves.Length];

        for (int i = 0; i < possibleMoves.Length; i++)
        {
            float distance = Vector3.Distance(possibleMoves[i], playerPos);
            points[i] = -distance;

            // priorytet na bicie
            if (Vector3.Distance(possibleMoves[i], playerPos) < 0.1f) points[i] += 100f;

            // kara za bycie w rogu planszy
            Vector3 pos = possibleMoves[i];
            if (Mathf.Abs(pos.x) > 3f && Mathf.Abs(pos.z) > 3f) points[i] -= 1.5f;
        }

        int bestIndex = 0;
        for (int i = 1; i < points.Length; i++)
            if (points[i] > points[bestIndex])
                bestIndex = i;

        return possibleMoves[bestIndex];
    }

    private void Move(Vector3 newPos)
    {
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player != null && Vector3.Distance(player.transform.position, newPos) < 0.1f)
        {
            Destroy(player);
            Debug.Log("KRÓLOWA WYGRA£A – GRACZ ZOSTA£ ZABITY");
        }

        transform.position = newPos;
        BoardManager.Instance.UpdatePiecePosition(gameObject, newPos);
    }
}