using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap levelTilemap;
    [SerializeField] private Tilemap obstaclesTilemap;
    [SerializeField] private Tilemap pushableObstaclesTilemap;
    [SerializeField] private Tilemap targetsTilemap;

    private PlayerMovement controls;

    private void Awake()
    {
        controls = new PlayerMovement();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>()); 
    }

    private void Move(Vector2 direction)
    {
        if (CanPush(direction))
        {

        }
        if (CanMove(direction))
        {
            transform.position += (Vector3) direction;
        }
       /* if (IsPlayerOnTilemap(targetsTilemap))
        {
            Debug.Log("You're on a target");
        }*/
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = levelTilemap.WorldToCell(transform.position + (Vector3) direction);

        if (!levelTilemap.HasTile(gridPosition) 
            || obstaclesTilemap.HasTile(gridPosition)
            || pushableObstaclesTilemap.HasTile(gridPosition))
        {
            return false;
        }

        return true;
    }

    private bool CanPush(Vector2 direction)
    {
        Vector3Int gridPosition = levelTilemap.WorldToCell(transform.position + (Vector3)direction);

        if (pushableObstaclesTilemap.HasTile(gridPosition))
        {
            var tile = pushableObstaclesTilemap.GetTile(gridPosition);
            Vector3Int newPosition = levelTilemap.WorldToCell(transform.position + (Vector3)(direction * 2));

            if(levelTilemap.HasTile(newPosition) && !obstaclesTilemap.HasTile(newPosition) && !pushableObstaclesTilemap.HasTile(newPosition))
            {
                pushableObstaclesTilemap.SetTile(newPosition, tile);
                pushableObstaclesTilemap.SetTile(gridPosition, null);
                transform.position += (Vector3)direction;

               /* if (targetsTilemap.HasTile(newPosition))
                {
                    Debug.Log("pushable obstacle is on target");
                }*/
            }

            return true;
        }
        return false;
    }

    private bool IsPlayerOnTilemap(Tilemap tilemap)
    {
        Vector3Int gridPosition = levelTilemap.WorldToCell(transform.position);

        if (tilemap.HasTile(gridPosition))
        {
            return true;
        }

        return false;
    }
}
