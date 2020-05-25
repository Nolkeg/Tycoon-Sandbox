using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GridVisualize : MonoBehaviour
{
    public GameObject cellCenter;
    public GameObject cellOrigin;
    public Grid grid;
    public GridControl gridControl;
    public Color gridColor;
    
    [OnValueChanged("ShowVisualization")]
    public bool showVisual;

   
    public void ShowVisualization()
    {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(showVisual);
        }
        SceneView.RepaintAll();
    }
    
    [Button(ButtonSizes.Large)]
    public void UpdateVisualization()
    {
        if (grid == null)
            grid = GetComponent<Grid>();
        
        foreach (Transform child in transform) {
            DestroyImmediate(child.gameObject);
        }
        
        
        for (int x = 0; x <= gridControl.sizeX; x++)
        {
            for (int y = 0; y <= gridControl.sizeY; y++)
            {
                for (int z = 0; z <= gridControl.sizeZ; z++)
                {
                    Vector3Int localCellPosition = new Vector3Int(x, y, z);
                    Vector3 worldCenter = grid.GetCellCenterWorld(localCellPosition);
                    Vector3 cellOriginPos = grid.CellToWorld(localCellPosition);

                    if (!(x == gridControl.sizeX || y == gridControl.sizeY || z == gridControl.sizeZ))
                    {
                        Instantiate(cellCenter, worldCenter, Quaternion.identity, transform);
                    }
                    
                    Instantiate(cellOrigin, cellOriginPos, Quaternion.identity, transform);
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        if (!showVisual) return;
        Gizmos.color = gridColor;
        for (int x = 0; x <= gridControl.sizeX; x++)
        {
            for (int y = 0; y <= gridControl.sizeY; y++)
            {
                for (int z = 0; z <= gridControl.sizeZ; z++)
                {
                    Vector3Int localCellPosition = new Vector3Int(x, y, z);
                    Vector3 cellOriginPos = grid.CellToWorld(localCellPosition);
                    if(x < gridControl.sizeX)
                        Gizmos.DrawLine(cellOriginPos,grid.CellToWorld(localCellPosition+new Vector3Int(1,0,0)));
                    if(y < gridControl.sizeY)
                        Gizmos.DrawLine(cellOriginPos,grid.CellToWorld(localCellPosition+new Vector3Int(0,1,0)));
                    if(z < gridControl.sizeZ)
                        Gizmos.DrawLine(cellOriginPos,grid.CellToWorld(localCellPosition+new Vector3Int(0,0,1)));
                }
            }
        }
        
    }
    
}
