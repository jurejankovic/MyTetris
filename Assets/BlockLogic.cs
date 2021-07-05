using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    bool moveable = true;
    float timer = 0f;

    public GameLogic gameLogic;
    public GameObject rig;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    void RegisterBlock()
    {
        foreach(Transform subBlock in rig.transform)
        {
            gameLogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] = subBlock;
        }
    }

    bool CheckMoveIsValid()
    {
        foreach (Transform subBlock in rig.transform)
        {
            Debug.Log(subBlock.transform.position.x + ", " + subBlock.transform.position.y);

            if (subBlock.transform.position.x >= GameLogic.playgroundWidth ||
                subBlock.transform.position.x < 0 ||
                subBlock.transform.position.y < 0)
            {
                return false;
            }

            //    if (subBlock.position.y < GameLogic.playgroundHeight && gameLogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] != null)
            //    {
            //        return false;
            //    }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveable)
        {
            // Update the timer
            timer += 1 * Time.deltaTime;

            // Drop quick 
            if (Input.GetKey(KeyCode.DownArrow) && timer > GameLogic.quickDropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0;

                if (!CheckMoveIsValid())
                {
                    moveable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);

                    RegisterBlock();
                    gameLogic.SpawnBlock();
                }
            }
            // Drop natural ("low gravity")
            else if (timer > GameLogic.dropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0;

                if (!CheckMoveIsValid())
                {
                    moveable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);

                    RegisterBlock();
                    gameLogic.SpawnBlock();
                }
            }

            // Side movement
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.position -= new Vector3(1, 0, 0);

                if (!CheckMoveIsValid())
                {
                    moveable = false;
                    gameObject.transform.position += new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.position += new Vector3(1, 0, 0);

                if (!CheckMoveIsValid())
                {
                    moveable = false;
                    gameObject.transform.position -= new Vector3(1, 0, 0);
                }
            }

            // Rotataion
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rig.transform.eulerAngles += new Vector3(0, 0, 90);
            }
        }
    }
}
