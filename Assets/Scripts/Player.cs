using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDropHandler
{
    public Image playerImage = null;
    public Image mirrorImage = null;
    public Image healthNumberImage = null;
    public Image glowImage = null;

    public int health = 5;
    public int mana = 1;

    public bool isPlayer;
    public bool isFire; // whether an enemy is a fire monster or not

    public GameObject[] manaBalls = new GameObject[5];

    private Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    internal void PlayHitAnim()
    {
        if(animator != null)
        {
            animator.SetTrigger("Hit");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameController.instance.isPlayable) return;
        GameObject obj = eventData.pointerDrag;
        if (obj)
        {
            Card card = obj.GetComponent<Card>();
            if (card)
            {
                GameController.instance.UseCard(card, this, GameController.instance.playersHand);
            }
        }
    }
}
