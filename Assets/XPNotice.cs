using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPNotice : MonoBehaviour
{
    public TMP_Text Text;
    float t;
    public void SetXPValue(int XP)
    {
        Text.SetText("+ "+ XP.ToString() + " XP");
    }

    private void Start()
    {
        GameObject MainCanvas = GameObject.FindWithTag("MainCNV");
        transform.SetParent(MainCanvas.transform, false);

    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(0, 50, t), 0);


        t += 2 * Time.deltaTime;

        if (t > 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
