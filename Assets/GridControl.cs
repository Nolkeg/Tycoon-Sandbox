using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [HorizontalGroup("Grid Size", LabelWidth = 50)] [MinValue(1)]
    public int sizeX, sizeY, sizeZ;

    public BoxCollider coll;
    public Grid grid;
    private GridVisualize _visualize;

    void Awake()
    {
        grid = GetComponent<Grid>();
        coll = GetComponent<BoxCollider>();
        _visualize = GetComponent<GridVisualize>();
        SetNewGridSize(new Vector3(sizeX, sizeY, sizeZ));
    }


    [Button(ButtonSizes.Large)]
    public void SetNewGridSize(Vector3 newSize)
    {
        sizeX = (int) newSize.x;
        sizeY = (int) newSize.y;
        sizeZ = (int) newSize.z;
        coll.center = newSize / 2;
        coll.size = newSize + new Vector3(.1f, .1f, .1f);
        _visualize.UpdateVisualization();
    }


    [Button(ButtonSizes.Large)]
    public bool IsWithinGrid(BoxCollider otherColl)
    {
        Vector3[] vertexPosition = Utils.GetColliderVertexPositions(otherColl);
        bool result = true;
        for (int i = 0; i < vertexPosition.Length; i++)
        {
            //need all vertices to be in collider 
            result &= Utils.IsPointWithinCollider(coll, vertexPosition[i]);
        }

        return result;
    }

    [Button(ButtonSizes.Large)]
    public Vector3 SnapToNearestPoint(BoxCollider boxCollider)
    {
        Vector3Int cellPosition = grid.WorldToCell(boxCollider.transform.position);
        var localScale = boxCollider.transform.localScale;
       
        int minX = (int) Mathf.Floor(boxCollider.size.x * localScale.x / 2f);
        int minY = (int) Mathf.Floor(boxCollider.size.y * localScale.y / 2f);
        int minZ = (int) Mathf.Floor(boxCollider.size.z * localScale.z / 2f);
        int maxX = sizeX-1 - (int) Mathf.Floor(boxCollider.size.x * localScale.x / 2f);
        int maxY = sizeY-1 - (int) Mathf.Floor(boxCollider.size.y * localScale.y / 2f);
        int maxZ = sizeZ-1 - (int) Mathf.Floor(boxCollider.size.z * localScale.z / 2f);

        int x, y, z;
        x = Mathf.Clamp(cellPosition.x, minX, maxX);
        y = Mathf.Clamp(cellPosition.y, minY, maxY);
        z = Mathf.Clamp(cellPosition.z, minZ, maxZ);
        
        return grid.GetCellCenterWorld(new Vector3Int(x, y, z));
    }
}