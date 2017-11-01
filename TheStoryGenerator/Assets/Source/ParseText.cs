using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParseText : MonoBehaviour {

	public Button enterButton;
	public InputField field;
	// Use this for initialization
	void Start () {
		Button btn = enterButton.GetComponent<Button>();
		btn.onClick.AddListener(gatherInput);
		field = GameObject.Find ("InputField").GetComponent<InputField> ();
	}
	
	// Update is called once per frame
	void gatherInput() {
		Debug.Log(field.text);
		Translator.parse (field.text);
		field.text = "";

	}
}
