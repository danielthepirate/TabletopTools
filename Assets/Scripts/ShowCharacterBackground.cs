using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterBackground : MonoBehaviour {

	[SerializeField] RectTransform inputFrame;
	[SerializeField] RectTransform outputFrame;
	[SerializeField] Button button;
	[SerializeField] LoadCharacterBackground loader;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(OnClick);
	}
	

	void OnClick() {
		loader.GenerateBackground();

		inputFrame.gameObject.SetActive(false);
		outputFrame.gameObject.SetActive(true);
	}
}
