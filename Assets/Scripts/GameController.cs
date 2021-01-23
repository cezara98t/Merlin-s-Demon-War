using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static public GameController instance = null;

    public Deck playerDeck = new Deck();
    public Deck enemyDeck = new Deck();

    public List<CardData> cards = new List<CardData>();

    public Sprite[] healthNumbers = new Sprite[10];
    public Sprite[] damageNumbers = new Sprite[10];

    public Hand playersHand = new Hand();
    public Hand enemysHand = new Hand();

    public GameObject cardPefab = null;
    public Canvas canvas = null;

    public bool isPlayable = false;

    public Player player = null;
    public Player enemy = null;

    public GameObject effectLeftPrefab = null;
    public GameObject effectRightPrefab = null;
    public Sprite fireball = null;
    public Sprite iceball = null;
    public Sprite multiFireball = null;
    public Sprite multiIceball = null;
    public Sprite fireAndIce = null;

    private void Awake()
    {
        instance = this;
        playerDeck.Create();
        enemyDeck.Create();
        StartCoroutine(DealHands());
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    //TODO
    public void SkipTurn()
    {
        Debug.Log("skip turn");
    }

    internal IEnumerator DealHands()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 3; i++)
        {
            playerDeck.DealCard(playersHand);
            enemyDeck.DealCard(enemysHand);
            yield return new WaitForSeconds(1);
        }
        isPlayable = true;
    }

    //IN : playerOn - the player on which the card is
    //     hand - hand from which hand the card is
    internal bool UseCard(Card card, Player playerOn, Hand hand)
    {
        if (!IsCardValid(card, playerOn, hand)) return false;
        isPlayable = false;
        CastCard(card, playerOn, hand);
        return false;
    }

    internal bool IsCardValid(Card playedCard, Player playerOn, Hand hand)
    {
        bool valid = false;
        if (playedCard == null) return false;
        if (hand.isPlayers)
        {
            if (playedCard.cardData.cost <= player.mana)
            {
                if (playerOn.isPlayer && playedCard.cardData.isDefenceCard)
                    valid = true; 
                if (!playerOn.isPlayer && !playedCard.cardData.isDefenceCard)
                    valid = true;
            }
        }
        else
        {
            if (playedCard.cardData.cost <= enemy.mana)
            {
                if (!playerOn.isPlayer && playedCard.cardData.isDefenceCard)
                    valid = true;
                if (playerOn.isPlayer && !playedCard.cardData.isDefenceCard)
                    valid = true;
            }
        }
        return valid;
    }

    internal void CastCard(Card card, Player playerOn, Hand hand)
    {
        if (card.cardData.isMirrorCard)
        {
        }
        else
        {
            if (card.cardData.isDefenceCard)
            {

            }
            else // attack
            {

                CastAttackEffect(card, playerOn);
            }
        }
    }

    internal void CastAttackEffect(Card card, Player playerOn)
    {
        GameObject effectGO = null;
        if (playerOn.isPlayer)
            effectGO = Instantiate(effectRightPrefab, canvas.gameObject.transform);
        else
            effectGO = Instantiate(effectLeftPrefab, canvas.gameObject.transform);
        Effect effect = effectGO.GetComponent<Effect>();
        if (effect)
        {
            effect.targetPlayer = playerOn;
            effect.sourceCard = card;
            switch (card.cardData.damageType)
            {
                case CardData.DamageType.fire:
                    if (card.cardData.isMulti)
                        effect.effectImage.sprite = multiFireball;
                    else
                        effect.effectImage.sprite = fireball;
                    break;
                case CardData.DamageType.ice:
                    if (card.cardData.isMulti)
                        effect.effectImage.sprite = multiIceball;
                    else
                        effect.effectImage.sprite = iceball;
                    break;
                case CardData.DamageType.both:
                    effect.effectImage.sprite = fireAndIce;
                    break;
            }
        }
    }
}
