using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Ruchy pionka (+Z = do przodu)
    private static readonly Vector3[] PawnOffsets = new Vector3[]
    {
        new Vector3( 0, 0,  1),   // zwyk³y ruch
        new Vector3( 0, 0,  2),   // podwójny ruch ze startu
        new Vector3(-1, 0,  1),   // bicie w lewo
        new Vector3( 1, 0,  1),   // bicie w prawo
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
        Vector3[] moves = GenerateMoves(PawnOffsets);
        PrintMoves(moves);

        Vector3 selected = SelectMove(moves);
        Move(selected);
    }

    private Vector3[] GenerateMoves(Vector3[] offsets)
    {
        Vector3 current = transform.position;
        List<Vector3> result = new List<Vector3>();

        List<Vector3> occupiedPositions = BoardManager.Instance.GetAllPiecesWorldPositions();

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector3 target = current + offsets[i];

            // poza plansz¹ – pomijamy
            if (Mathf.Abs(target.x) > 3.5f || Mathf.Abs(target.z) > 3.5f)
                continue;

            // zwyk³y ruch: pole musi byæ puste
            if (i == 0)
            {
                if (!Physics.CheckBox(target, Vector3.one * 0.4f))
                    result.Add(target);
                continue;
            }

            // podwójny ruch: pionek musi staæ na linii startu i oba pola musz¹ byæ wolne
            if (i == 1)
            {
                bool isOnStartRank = Mathf.RoundToInt(current.z) == -6;
                Vector3 midSquare = current + new Vector3(0, 0, 1);

                if (isOnStartRank &&
                    !Physics.CheckBox(midSquare, Vector3.one * 0.4f) &&
                    !Physics.CheckBox(target, Vector3.one * 0.4f))
                {
                    result.Add(target);
                }
                continue;
            }

            // bicie: tylko jeœli na polu stoi gracz
            if (i == 2 || i == 3)
            {
                GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
                if (player != null)
                {
                    if (Vector3.Distance(player.transform.position, target) < 0.1f)
                        result.Add(target);
                }
                continue;
            }
        }

        return result.ToArray();
    }

    private Vector3 SelectMove(Vector3[] possibleMoves)
    {
        if (possibleMoves.Length == 0)
            return transform.position;

        int[] points = new int[possibleMoves.Length];

        for (int i = 0; i < possibleMoves.Length; i++)
        {
            Vector3 offset = possibleMoves[i] - transform.position;

            // zwyk³y ruch
            if (offset == PawnOffsets[0]) points[i] = 1;

            // podwójny ruch
            else if (offset == PawnOffsets[1]) points[i] = 2;

            // bicie
            else if (offset == PawnOffsets[2] || offset == PawnOffsets[3]) points[i] = 3;
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
        // Sprawdzenie, czy na polu jest gracz
        GameObject player = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (player != null && Vector3.Distance(player.transform.position, newPos) < 0.1f)
        {
            Destroy(player);
            Debug.Log("PION WYGRA£ – GRACZ ZOSTA£ ZABITY");
        }

        // Ruch pionka
        transform.position = newPos;
        BoardManager.Instance.UpdatePiecePosition(gameObject, newPos);
    }

    private void PrintMoves(Vector3[] moves)
    {
        Debug.Log("Mo¿liwe ruchy pionka:");
        foreach (var m in moves)
            Debug.Log(m);
    }
}