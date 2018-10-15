using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampInputAttribute : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Text inputText = GetComponent<Text>();
		string input = inputText.text;

		//int value;

		//value = int.Parse(input);

		//value = Mathf.Clamp(value, 1, 20);
		//inputText.text = value.ToString();
	}
}
