using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int turnCounter = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void NextTurn()
    {
        turnCounter++;
        Debug.Log("Tura: " + turnCounter);
        BoardManager.Instance.PrintBoardVisual();

        // generowanie ruchów konia po każdej turze
        var go = new GameObject("MovementLogic");           // create temporary object
        var logic = go.AddComponent<MovementLogic>();        // add component to it

        logic.Init(BoardManager.Instance.GetAllPiecesWorldPositions());
        logic.canJumpOver = true; // bo koń

        GameObject king = BoardManager.Instance.GetPieceByName("king");
        GameObject player = BoardManager.Instance.GetPieceByName("player");

        var moves = logic.GenerateKnightMoves(king, player);
        foreach (var m in moves)
            Debug.Log($"Move {m.Id}: {m.Distance}, distPoints={m.PointsForDistance}, checkPoints={m.PointsForCheck}");

        // 💡 destroy MovementLogic object after using it
        Destroy(go);
    }
}
