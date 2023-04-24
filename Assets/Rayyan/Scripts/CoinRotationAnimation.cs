using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotationAnimation : MonoBehaviour
{
    //public float coinDelayTime = 10f;
    public float rotationSpeed = 100f;
    [SerializeField] GameObject[] childrenCoin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotationControl();
    }
    void RotationControl()
    {
        GameObject[] parentCoinRow = GameObject.FindGameObjectsWithTag("CoinRow");
        childrenCoin = GameObject.FindGameObjectsWithTag("Coin");
        
        for(int i=0; i< parentCoinRow.Length-1; i++)
        {
            GameObject temp = parentCoinRow[i].gameObject;
            childrenCoin = new GameObject[temp.transform.childCount];
            for (int k =0; k<temp.transform.childCount; k++)
            {
                childrenCoin[k] = temp.transform.GetChild(k).gameObject;
                childrenCoin[k].transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
            }
           
        }
       
    }

}
