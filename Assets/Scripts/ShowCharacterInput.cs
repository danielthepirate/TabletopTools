using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterInput : MonoBehaviour {

	[SerializeField] RectTransform inputFrame;
	[SerializeField] RectTransform outputFrame;
	[SerializeField] Button button;

	// Use this for initialization
	void Start() {
		button.onClick.AddListener(OnClick);
	}


	void OnClick() {
		inputFrame.gameObject.SetActive(true);
		outputFrame.gameObject.SetActive(false);
	}
}
