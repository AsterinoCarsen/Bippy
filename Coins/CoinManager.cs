using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text display;
    int potentialCoins;
    public int currentCoins;

    void Start()
    {
        potentialCoins = GetComponentsInChildren<Transform>().Length - 1;
        // Update text
        Add(0);
    }

    // Add 1 to currentcoins and update UI
    public void Add(int amount)
    {
        currentCoins += amount;
        display.text = currentCoins + " / " + potentialCoins;
    }
}
