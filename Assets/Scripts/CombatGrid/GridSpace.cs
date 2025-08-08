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

    public SpriteRenderer highlightSprite;
    public SpriteRenderer playerMoveSprite;


    public int xPos;
    public int yPos;

    private void Awake()
    {
        highlightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerMoveSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

    }

    void Start()
    {

    }

    public void UpdateContents(GridContents newContents)
    {
        contents = newContents;
    }

    public void HighlightSpace()
    {
        playerMoveSprite.enabled = true;
    }

    public void ResetHighlight()
    {
        playerMoveSprite.enabled = false;
    }

    public override void OnMouseEnter()
    {
        highlightSprite.enabled = true;
    }

    public override void OnMouseExit()
    {
        highlightSprite.enabled = false;

    }

    public void OnMouseDown()
    {
        switch(contents)
        {
            case GridContents.Player:
                if (GridGameManager.Instance.PlayerTurn)
                {
                    PlayerCombatGrid.Instance.ShowMoveableSpaces();
                }
                break;
            case GridContents.Enemy:

                break;
            case GridContents.Herb:

                break;
            case GridContents.None:
                if(highlightSprite.enabled)
                {
                    PlayerCombatGrid.Instance.SetPos(xPos, yPos);
                }
                break;
            default:

                break;


        }
    }
}
