using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    // Słownik: klucz = obiekt (GameObject) figury, wartość = jej pozycja na planszy
    private Dictionary<GameObject, Vector3> piecePositions = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Dodanie figury do planszy (np. przy starcie)
    public void RegisterPiece(GameObject piece)
    {
        if (!piecePositions.ContainsKey(piece))
        {
            piecePositions.Add(piece, piece.transform.position);
            Debug.Log($"Zarejestrowano figurę: {piece.name} na pozycji {piece.transform.position}");
        }
    }

    // Aktualizacja pozycji po ruchu
    public void UpdatePiecePosition(GameObject piece, Vector3 newPos)
    {
        if (piecePositions.ContainsKey(piece))
        {
            piecePositions[piece] = newPos;
        }
        else
        {
            Debug.LogWarning($"Figura {piece.name} nie była zarejestrowana w BoardManagerze!");
        }
    }

    // Sprawdzenie, czy pole jest zajęte (do kolizji logicznej)
    public bool IsTileOccupied(Vector3 position)
    {
        foreach (var kvp in piecePositions)
        {
            if (Vector3.Distance(kvp.Value, position) < 0.1f)
                return true;
        }
        return false;
    }

    // Możesz też dodać metodę do debugowania
    public void PrintBoardState()
    {
        Debug.Log("=== STAN PLANSZY ===");
        foreach (var kvp in piecePositions)
        {
            Debug.Log($"{kvp.Key.name}: {kvp.Value}");
        }
    }

    public void PrintBoardVisual()
    {
        int size = 8;
        char[,] board = new char[size, size];

        // Wypełniamy pustymi polami
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                board[z, x] = '0';
            }
        }

        // Wstawiamy figury do tablicy
        foreach (var kvp in piecePositions)
        {
            Vector3 pos = kvp.Value;

            // przeliczamy współrzędne (środek to 0,0 → plansza 8x8 od -3.5 do 3.5)
            int gridX = Mathf.RoundToInt(pos.x + 3.5f);
            int gridZ = Mathf.RoundToInt(pos.z + 3.5f);

            char symbol = '0';

            string name = kvp.Key.name.ToLower();
            if (name.Contains("player")) symbol = 'P';
            else if (name.Contains("king")) symbol = 'K';
            else if (name.Contains("queen")) symbol = 'Q';
            else if (name.Contains("rook")) symbol = 'R';
            else if (name.Contains("knight")) symbol = 'N';
            else if (name.Contains("bishop")) symbol = 'B';

            if (gridX >= 0 && gridX < size && gridZ >= 0 && gridZ < size)
            {
                board[gridZ, gridX] = symbol;
            }
        }

        // Tworzymy string do wypisania
        string output = "";
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                output += board[z, x] + " ";
            }
            output += "\n";
        }

        Debug.Log(output);
    }
}
