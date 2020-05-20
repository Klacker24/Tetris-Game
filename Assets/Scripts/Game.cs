using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
    // Start is called before the first frame update
    void Start()
    {
        SpawnNextTetromino();
    }

    public bool CheckIsAboveGrid(Tetrominos tetromino)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            foreach(Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if(pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsRowFull (int y)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            if(grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteMinoAt (int y)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void MoveRoWDown (int y)
    {
        for(int x=0; x< gridWidth; ++x)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i< gridHeight; ++i)
        {
            MoveRoWDown(i);
        }
    }

    public void DeleteRow()
    {
        for(int y = 0; y< gridHeight; ++y)
        {
            if(IsRowFull(y))
            {
                DeleteMinoAt(y);

                MoveAllRowsDown(y+1);

                --y;
                ScoreScript.scoreValue += 10;
            }
        }
    }

    public void UpdateGrid(Tetrominos tetromino)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for(int x = 0; x < gridWidth; ++x)
            {
                if(grid[x,y] != null)
                {
                    if(grid[x,y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            if(pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {
        if(pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    
    public void SpawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    }

    public bool CheckIsInsideGrid (Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1, 8);

        string randomTetrominoName = "Tetrominos/I Tetromino";

        switch(randomTetromino)
        {
            case 1:
                randomTetrominoName = "Tetrominos/I Tetromino";
                break;
            case 2:
                randomTetrominoName = "Tetrominos/J Tetromino";
                break;
            case 3:
                randomTetrominoName = "Tetrominos/S Tetromino";
                break;
            case 4:
                randomTetrominoName = "Tetrominos/Z Tetromino";
                break;
            case 5:
                randomTetrominoName = "Tetrominos/L Tetromino";
                break;
            case 6:
                randomTetrominoName = "Tetrominos/O Tetromino";
                break;
            case 7:
                randomTetrominoName = "Tetrominos/T Tetromino";
                break;
        }
        return randomTetrominoName;
    }

    [System.Obsolete]
    public void GameOver()
    {
        Application.LoadLevel("Game Over");
    }
}
