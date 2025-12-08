using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject kingPrefab;
    private Vector3 playerPosition;
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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // sterujemy ruchem r�cznie
        targetPosition = transform.position;
        globalOldPosition = transform.position;
        globalNewPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public Vector3[] GenerateMoves(Vector3[] offsets)
    {
        Vector3 current = transform.position;
        List<Vector3> occupiedPositions = BoardManager.Instance.GetAllPiecesWorldPositions();
        // tymczasowa tablica o maksymalnym mo�liwym rozmiarze
        Vector3[] temp = new Vector3[offsets.Length];
        int count = 0;

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector3 target = current + offsets[i];

            // zakres planszy (8�8, centrum w 0,0 -> 3.5f)
            if (Mathf.Abs(target.x) <= 3.5f && Mathf.Abs(target.z) <= 3.5f)
            {
                // sprawdzamy czy pole nie jest zaj�te
                if (!Physics.CheckBox(target, Vector3.one * 0.4f))
                {
                    bool isOccupied = occupiedPositions.Contains(target);
                    if (!isOccupied)
                        temp[count++] = target;
                }
            }
        }

        // je�eli wszystkie miejsca zosta�y u�yte, zwracamy temp bez kopiowania
        if (count == offsets.Length)
            return temp;

        // inaczej kopiujemy tylko u�yte elementy do tablicy o w�a�ciwym rozmiarze
        Vector3[] result = new Vector3[count];
        System.Array.Copy(temp, result, count);
        return result;
    }
    
    private void OnEnable()
    {
        GameManager.OnTurnChanged += HandleTurnChange;
    }

    private void OnDisable()
    {
        GameManager.OnTurnChanged -= HandleTurnChange;
    }
    private Vector3 SelectMove(Vector3[] possibleMoves)
    {
        GameObject playerObject = BoardManager.Instance.GetPieceByName("playerPrefab(Clone)");
        if (playerObject == null)
        {
            Debug.LogWarning("Nie znaleziono gracza!");
            return Vector3.zero;
        }

        Vector3 playerPosition = playerObject.transform.position;
        float[] points = new float[possibleMoves.Length];

        for (int i = 0; i < possibleMoves.Length; i++)
        {
            float distance = Vector3.Distance(possibleMoves[i], playerPosition);

            // im dalej od gracza, tym lepiej
            points[i] = -Mathf.Round(distance);

            // bonus za ruch dający szacha
            if (IsCheck(possibleMoves[i]))
            {
                points[i] += 5f;
            }

            // kara za bycie w rogu planszy
            Vector3 pos = possibleMoves[i];
            if (Mathf.Abs(pos.x) > 3f && Mathf.Abs(pos.z) > 3f)
            {
                points[i] -= 1.5f;
            }
        }

        // wybieramy ruch z najwyższą punktacją
        int bestIndex = 0;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i] > points[bestIndex])
                bestIndex = i;
        }

        return possibleMoves[bestIndex];
    }

    // Przyk�adowa metoda sprawdzaj�ca szacha
    private bool IsCheck(Vector3 position)
    {
        // TODO: dodaj logik� sprawdzania czy ruch na 'position' powoduje szacha
        // Na razie zwraca false dla przyk�adu
        return false;
    }

    // ta funkcja zostanie wywo�ana automatycznie po ka�dej zmianie tury
    private void HandleTurnChange()
    {
        if (GameManager.Instance.turnCounter % 2 != 0)
            return;
        Vector3[] moves = GenerateMoves(KingOffsets);
        PrintMoves(moves);
        Debug.Log("Najlepszy ruch to:");
        Debug.Log(SelectMove(moves));
        Move(SelectMove(moves));

    }
    public void PrintMoves(Vector3[] possible)
    {
        Debug.Log("Ruchy krola/figury:");

        foreach (var move in possible)
        {
            Debug.Log(move);
        }
    }
    public void Move(Vector3 move) {
       GameObject kingObject = BoardManager.Instance.GetPieceByName("kingPrefab(Clone)");
        transform.position = move;
        BoardManager.Instance.UpdatePiecePosition(kingObject, move);
    }
}