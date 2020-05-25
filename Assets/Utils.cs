using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static bool IsPointWithinCollider(Collider collider, Vector3 point)
    {
        return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
    }
    
    public static Vector3[] GetColliderVertexPositions (BoxCollider collider)
    {
        var vertices = new Vector3[8];

        Vector3 colliderSize = collider.size;
        vertices[0] = collider.transform.TransformPoint(collider.center + new Vector3(colliderSize.x, -colliderSize.y, colliderSize.z) * 0.5f);
        vertices[1] = collider.transform.TransformPoint(collider.center + new Vector3(-colliderSize.x, -colliderSize.y, colliderSize.z)*0.5f);
        vertices[2] = collider.transform.TransformPoint(collider.center + new Vector3(-colliderSize.x, -colliderSize.y, -colliderSize.z)*0.5f);
        vertices[3] = collider.transform.TransformPoint(collider.center + new Vector3(colliderSize.x, -colliderSize.y, -colliderSize.z)*0.5f);
        vertices[4] = collider.transform.TransformPoint(collider.center + new Vector3(colliderSize.x, colliderSize.y, colliderSize.z)*0.5f);
        vertices[5] = collider.transform.TransformPoint(collider.center + new Vector3(-colliderSize.x, colliderSize.y, colliderSize.z)*0.5f);
        vertices[6] = collider.transform.TransformPoint(collider.center + new Vector3(-colliderSize.x, colliderSize.y, -colliderSize.z)*0.5f);
        vertices[7] = collider.transform.TransformPoint(collider.center + new Vector3(colliderSize.x, colliderSize.y, -colliderSize.z)*0.5f);
        
        return vertices;
    }
}
