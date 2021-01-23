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
}
