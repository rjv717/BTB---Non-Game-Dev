using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SettingValue : MonoBehaviour {

    Text valueText;
    
	void Awake () {
        valueText = GetComponent<Text>();    		
	}
	
	public void UpdateValue (float f) {
        valueText.text = (f * 100).ToString("F0") + "%";
	}
}
