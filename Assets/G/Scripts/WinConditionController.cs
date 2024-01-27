using UnityEngine;
using UnityEngine.Tilemaps;

public class WinConditionController : MonoBehaviour
{

    [SerializeField] private Tilemap pushableObstaclesTilemap;
    [SerializeField] private Tilemap targetsTilemap;

    private void Update()
    {
       //Debug.Log(IsAllXTilesOnYTiles(pushableObstaclesTilemap, targetsTilemap));
       //Debug.Log(IsSomeXTilesOnYTiles(pushableObstaclesTilemap, targetsTilemap, 1));
       //Debug.Log(IsNoneXTilesOnYTiles(pushableObstaclesTilemap, targetsTilemap));
    }

    private bool IsAllXTilesOnYTiles(Tilemap xTilemap, Tilemap yTilemap)
    {
        int onTargetCount = 0;
        BoundsInt bounds = xTilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (xTilemap.HasTile(position) && yTilemap.HasTile(position))
            {
                onTargetCount++;
            }
        }

        if(TotalAmountOfTiles(targetsTilemap) == onTargetCount) 
        { 
            return true;
        }
        return false;
    }

    private bool IsSomeXTilesOnYTiles(Tilemap xTilemap, Tilemap yTilemap, int targetCount)
    {
        int onTargetCount = 0;
        BoundsInt bounds = xTilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (xTilemap.HasTile(position) && yTilemap.HasTile(position))
            {
                onTargetCount++;
            }
        }

        if (targetCount == onTargetCount)
        {
            return true;
        }
        return false;
    }

    private bool IsNoneXTilesOnYTiles(Tilemap xTilemap, Tilemap yTilemap)
    {
        int onTargetCount = 0;
        BoundsInt bounds = xTilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (xTilemap.HasTile(position) && yTilemap.HasTile(position))
            {
                onTargetCount++;
            }
        }

        if (onTargetCount == 0)
        {
            return true;
        }
        return false;
    }

    private int TotalAmountOfTiles(Tilemap tilemap)
    {
        int count = 0;
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                count++;
            }
        }
        return count;
    }
}
