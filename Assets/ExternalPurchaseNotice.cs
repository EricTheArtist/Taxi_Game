using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class ExternalPurchaseNotice : MonoBehaviour
{
    public GameObject Notice;

    int CoinsToBuy = 5000;
    int CurrentBalance;
    int NewBalance;
    public TMP_Text ProductName;
    public TMP_Text run_amount_text;
    public TMP_Text run_amount_shadow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void externalPurchaseSucced()
    {
        Notice.SetActive(true);
    }

    public void ExternalCoinsProduct5000(Product product)
    {
        ProductName.SetText(product.definition.id.ToString());
        CurrentBalance = PlayerPrefs.GetInt("Main Amount");
        NewBalance = CurrentBalance + CoinsToBuy;
        PlayerPrefs.SetInt("Main Amount", NewBalance);

        // updates text on screen
        run_amount_text.SetText(NewBalance.ToString());
        run_amount_shadow.SetText(NewBalance.ToString());
    }

    public void CloseExternalPurchaseNotice()
    {
        Notice.SetActive(false);
    }
}
