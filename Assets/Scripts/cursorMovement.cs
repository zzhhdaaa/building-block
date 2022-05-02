using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    GridElement lastHit;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "GridElement")
        {
            transform.position = hit.collider.transform.position;
            lastHit = hit.collider.GetComponent<GridElement>();

            this.rectTransform.sizeDelta = new Vector2(1.0f, lastHit.GetElementHeight());

            if (Input.GetMouseButtonDown(1))
            {
                SetCursorButton(6);
            }
        }
    }

    public void SetCursorButton(int input)
    {
        Coord coord = lastHit.GetCoord();
        /*
        int gridX = LevelGenerator.instance.gridX;
        int gridY = LevelGenerator.instance.gridY;
        int gridZ = LevelGenerator.instance.gridZ;
        */

        switch (input)
        {
            case 0:
                //Z+
                if (coord.z < LevelGenerator.instance.gridZ - 1)
                {
                    LevelGenerator.instance.gridElements[coord.y * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + (coord.z + 1) * LevelGenerator.instance.gridX + coord.x].SetEnable();
                }
                break;
            case 1:
                //Z-
                if (coord.z > 0)
                {
                    LevelGenerator.instance.gridElements[coord.y * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + (coord.z - 1) * LevelGenerator.instance.gridX + coord.x].SetEnable();
                }
                break;
            case 2:
                //Y+
                if (coord.y < LevelGenerator.instance.gridY - 1)
                {
                    LevelGenerator.instance.gridElements[(coord.y + 1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + coord.z * LevelGenerator.instance.gridX + coord.x].SetEnable();
                }
                break;
            case 3:
                //Y-
                if (coord.y > 0)
                {
                    LevelGenerator.instance.gridElements[(coord.y - 1) * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + coord.z * LevelGenerator.instance.gridX + coord.x].SetEnable();
                }
                break;
            case 4:
                //X+
                if (coord.x < LevelGenerator.instance.gridX - 1)
                {
                    LevelGenerator.instance.gridElements[coord.y * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + coord.z * LevelGenerator.instance.gridX + (coord.x + 1)].SetEnable();
                }
                break;
            case 5:
                //X-
                if (coord.x > 0)
                {
                    LevelGenerator.instance.gridElements[coord.y * LevelGenerator.instance.gridZ * LevelGenerator.instance.gridX + coord.z * LevelGenerator.instance.gridX + (coord.x - 1)].SetEnable();
                }
                break;
            case 6:
                if (coord.y > 0)
                {
                    lastHit.SetDisable();
                }
                break;
        }
    }
}
