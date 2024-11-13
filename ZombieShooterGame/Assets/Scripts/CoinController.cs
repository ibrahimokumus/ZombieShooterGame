using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{

    [SerializeField] Text coinCountText;
   
    public int coinCount;


    public void CoinCollector()
    {
        coinCount += 1;
        coinCountText.text = coinCount.ToString();
    }
}
