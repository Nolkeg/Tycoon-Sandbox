using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MoveOnGrid : MonoBehaviour
{
    public GridControl gridControl;
    public BoxCollider coll;
    
    [HorizontalGroup("localPosition", LabelWidth = 50)]
    public int localX, localZ, localY;

    private float offsetX = 0f, offsetZ = 0f;
    private Vector3 offset;
    
    private void Awake()
    {
        coll = GetComponent<BoxCollider>();
        if ((int)(coll.size.x * transform.localScale.x) % 2 == 0)
        {
            offsetX = -0.5f;
        }
        
        if ((int)(coll.size.z * transform.localScale.z) % 2 == 0)
        {
            offsetZ = -0.5f;
        }
        
        offset = new Vector3(offsetX,0,offsetZ);
        
    }

    private void Start()
    {
        transform.position = gridControl.SnapToNearestPoint(coll) + offset;
        UpdateLocalVariable(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            localZ = Mathf.Clamp(localZ + 1,0, gridControl.sizeX);
            MovePosition(new Vector3Int(localX, localY, localZ));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            localX = Mathf.Clamp(localX + 1,0, gridControl.sizeX);
            MovePosition(new Vector3Int(localX, localY, localZ));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            localZ = Mathf.Clamp(localZ - 1,0, gridControl.sizeX);
            MovePosition(new Vector3Int(localX, localY, localZ));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            localX = Mathf.Clamp(localX - 1,0, gridControl.sizeX);
            MovePosition(new Vector3Int(localX, localY, localZ));
        }
    }

    
    [Button(ButtonSizes.Large)]
    public void MovePosition(Vector3Int newPosition)
    {
        Vector3 tempPosition = transform.position;
        
        //try move
        transform.position = gridControl.grid.GetCellCenterWorld(newPosition) + offset;
        
        //if not within grid bound, return to lastPosition
        if (!gridControl.IsWithinGrid(coll))
        {
            //this is modified position(already have offset)
            transform.position = tempPosition;
            UpdateLocalVariable(tempPosition);
        }
    }

    public void UpdateLocalVariable(Vector3 worldPos)
    {
        //remove offset to get raw position
        Vector3Int localCellPosition = gridControl.grid.WorldToCell(worldPos-offset);
        localX = localCellPosition.x;
        localY = localCellPosition.y;
        localZ = localCellPosition.z;
    }
    
    
}
