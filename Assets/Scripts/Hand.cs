using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hand
{
    public Card[] cards = new Card[3];
    public Transform[] positions = new Transform[3];
    public string[] animations = new string[3];
    public bool isPlayers;
}
