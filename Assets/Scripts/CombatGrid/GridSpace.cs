using UnityEngine;

public enum GridContents
{
    None,
    Player,
    Enemy,
    Herb,
}


[RequireComponent(typeof(Collider2D))]
public class GridSpace : Interactable
{
    private Color originalColor;

    public GridContents contents;

    public Herb herb;


    public SpriteRenderer highlightSprite;
    public SpriteRenderer playerMoveSprite;
    public SpriteRenderer playerWeaponHighlight;
    public SpriteRenderer contentsSprite;

    public delegate void OnPlayerMouseEnterGridSpace(int x, int y);
    public OnPlayerMouseEnterGridSpace updateCurrentSpace;

    public delegate void OnGridHighlightReset();
    public OnGridHighlightReset onGridHighlightReset;


    public int xPos;
    public int yPos;

    private void Awake()
    {
        highlightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerMoveSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        playerWeaponHighlight = transform.GetChild(2).GetComponent<SpriteRenderer>();
        contentsSprite = transform.GetChild(3).GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        if (herb != null)
        {
            contentsSprite.sprite = herb.defaultSprite;
        }
    }

    public void UpdateContents(GridContents newContents)
    {
        contents = newContents;
    }
    public void HighlightSpace()
    {
        playerMoveSprite.enabled = true;
    }

    public void WeaponHighlightSpace()
    {
        playerWeaponHighlight.enabled = true;
    }

    public void ResetHighlight()
    {
        playerMoveSprite.enabled = false;
        playerWeaponHighlight.enabled = false;
    }

    public override void OnMouseEnter()
    {
        if (updateCurrentSpace != null)
        {
            updateCurrentSpace(xPos, yPos);

        }
        if (PlayerCombatGrid.Instance.state == PlayerState.UsePotion)
        {
            PlayerCombatGrid.Instance.ShowAttackableSpaces();
        }
        else
        {
            highlightSprite.enabled = true;

        }
    }

    public override void OnMouseExit()
    {
        highlightSprite.enabled = false;
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
