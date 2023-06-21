using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsRandomiser : MonoBehaviour
{
    public string[] Headlines;
    public TMP_Text HeadlineText;
    // Start is called before the first frame update
    void Start()
    {
        int HeadlineIndex = Random.Range(0, Headlines.Length);
        HeadlineText.SetText(Headlines[HeadlineIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
