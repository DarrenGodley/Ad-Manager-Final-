using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : Singleton<DataManager>
{
    public Text goldAmountText;

    private int goldAmount = 0;

    public void AddGold(int amount)
    {
        goldAmount += amount;
        goldAmountText.text = goldAmount.ToString();
    }
}