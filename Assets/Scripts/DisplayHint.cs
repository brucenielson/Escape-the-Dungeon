using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHint : MonoBehaviour
{
    private TextMeshProUGUI uiTextBox;
    public string hintText;
    
    // Start is called before the first frame update
    void Start()
    {
        uiTextBox = GameObject.Find("Hint Text").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            uiTextBox.text = hintText;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        
        uiTextBox.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
