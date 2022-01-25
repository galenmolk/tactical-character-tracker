using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class DestructibleTilemap : MonoBehaviour
{
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 hitPosition = Vector3.zero;
        
        foreach (ContactPoint2D hit in other.contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            
            Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
            tilemap.SetTile(tilePosition, null);
        }
    }
}
