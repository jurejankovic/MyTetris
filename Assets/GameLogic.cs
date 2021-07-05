using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static float dropTime = 0.9f;
    public static float quickDropTime = 0.05f;

    public static int playgroundWidth = 10;
    public static int playgroundHeight = 21;

    public GameObject[] blocks;
    public Transform[,] grid = new Transform[playgroundWidth, playgroundHeight];
 
    // Start is called before the first frame update
    void Start()
    {
        SpawnBlock();
    }

    public void ClearLines() 
    {
        for (int y = 0; y < playgroundHeight; y++) 
        {
            if (IsLineComplete(y))
            {
                DestroyLine(y);
                MoveLines(y);
            }
        }
    }

    private void MoveLines(int y)
    {
        for (int i = y; i < playgroundHeight; i++)
        {
            for (int x = 0; x < playgroundWidth; x++)
            { 
                if (grid[x, y + 1] != null)
                {
                    grid[x, y] = grid[x, y + 1];
                    grid[x, y].gameObject.transform.position -= new Vector3(0, 1, 0);
                    grid[x, y + 1] = null;
                }
            }
        }
    }

    private void DestroyLine(int y)
    {
        for (int x = 0; x < playgroundWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
        }
    }

    bool IsLineComplete(int y)
    {
        for (int x = 0; x < playgroundWidth; x++)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        float guess = Random.Range(0, 1f);
        guess *= blocks.Length;

        Instantiate(blocks[Mathf.FloorToInt(guess)]);
    }
}
