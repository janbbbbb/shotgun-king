using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    public bool canJumpOver;

    public void Init(List<Vector3> positions)
    {
        // TODO: use the list of positions for logic
    }

    public List<MoveData> GenerateKnightMoves(GameObject king, GameObject player)
    {
        // Placeholder function so it compiles
        return new List<MoveData>();
    }
}

public class MoveData
{
    public int Id;
    public float Distance;
    public float PointsForDistance;
    public float PointsForCheck;
}
