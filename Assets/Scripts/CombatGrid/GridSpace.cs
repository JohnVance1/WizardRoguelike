using UnityEngine;
using UnityEngine.Tilemaps;

public enum GridContentType
{
    None,
    Player,
    Enemy,
    Herb,
}


public class GridSpace
{
    public GridContentType contentType;

    public GridContent contents;

    public Herb herb;

    public bool IsHighlighted;

    public Tile highlightSprite;
    public Tile defaultSprite;
    public SpriteRenderer playerWeaponHighlight;
    public SpriteRenderer contentsSprite;

    public delegate void OnPlayerMouseEnterGridSpace(int x, int y);
    public OnPlayerMouseEnterGridSpace updateCurrentSpace;

    public delegate void OnGridHighlightReset();
    public OnGridHighlightReset onGridHighlightReset;


    public Vector2Int position;
    public Vector2Int gridPos;

    public GridSpace() { }

    public GridSpace(Vector2Int position, Vector2Int gridPos)
    {
        this.position = position;
        this.gridPos = gridPos;
        IsHighlighted = false;
        contentType = GridContentType.None;
    }

    

    
    public void UpdateContents(GridContentType newContentType)
    {
        contentType = newContentType;
    }
    public void HighlightSpace()
    {
        //playerMoveSprite.enabled = true;
    }

    public void WeaponHighlightSpace()
    {
        playerWeaponHighlight.enabled = true;
    }

    public void ResetHighlight()
    {
        //playerMoveSprite.enabled = false;
        playerWeaponHighlight.enabled = false;
    }

    public void CollectHerb()
    {
        //contentsSprite.sprite = null;
        UpdateContents(GridContentType.None);
        Debug.Log("Herb Added");
    }

    public void OnMouseEnter()
    {
        if (updateCurrentSpace != null)
        {
            //updateCurrentSpace(xPos, yPos);

        }
        //if (PlayerCombatGrid.Instance.state == PlayerState.UsePotion)
        //{
        //    PlayerCombatGrid.Instance.ShowAttackableSpaces();
        //}
        else
        {
            //highlightSprite.enabled = true;

        }
    }

    public void OnMouseExit()
    {
        //highlightSprite.enabled = false;
        if (onGridHighlightReset != null && PlayerCombatGrid.Instance.state == PlayerState.UsePotion)
        {
            onGridHighlightReset();

        }
    }

    //public void OnMouseDown()
    //{
    //    switch(contents)
    //    {
    //        case GridContents.Player:
    //            if (GridGameManager.Instance.PlayerTurn && PlayerCombatGrid.Instance.moveNums > 0 && PlayerCombatGrid.Instance.state == PlayerState.Idle)
    //            {
    //                PlayerCombatGrid.Instance.ShowMoveableSpaces();
    //                PlayerCombatGrid.Instance.state = PlayerState.Move;
    //            }
    //            break;
    //        case GridContents.Enemy:

    //            break;
    //        case GridContents.Herb:
    //            if (playerMoveSprite.enabled)
    //            {
    //                PlayerCombatGrid.Instance.AddItemToInventory(herb);
    //                contentsSprite.sprite = null;
    //                UpdateContents(GridContents.None);
    //                PlayerCombatGrid.Instance.SetPos(xPos, yPos);

    //            }
    //            break;
    //        case GridContents.None:
    //            if(playerMoveSprite.enabled && PlayerCombatGrid.Instance.state == PlayerState.Move)
    //            {
    //                PlayerCombatGrid.Instance.SetPos(xPos, yPos);
    //            }
    //            else if(PlayerCombatGrid.Instance.state == PlayerState.UsePotion)
    //            {
    //                // Use Potion
    //                if (onGridHighlightReset != null)
    //                {
    //                    onGridHighlightReset();

    //                }
    //                PlayerCombatGrid.Instance.state = PlayerState.Idle;
    //            }

    //            break;
    //        default:

    //            break;


    //    }
    //}
}
