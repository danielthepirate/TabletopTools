using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacterBackground : MonoBehaviour {

	public CharacterBackground[] Data = new CharacterBackground[64];

	[Header("Input Fields")]
	[SerializeField] RectTransform InputRace;
	[SerializeField] RectTransform InputClass;
	[SerializeField] RectTransform InputBackground;
	[SerializeField] RectTransform InputCharisma;

	[Header("Display Fields")]
	[SerializeField] RectTransform Parents;
	[SerializeField] RectTransform ParentsHalfElf;
	[SerializeField] RectTransform ParentsHalfOrc;
	[SerializeField] RectTransform ParentsTiefling;
	[SerializeField] RectTransform Birthplace;
	[SerializeField] RectTransform Siblings;
	[SerializeField] RectTransform Family;
	[SerializeField] RectTransform OtherFamily;
	[SerializeField] RectTransform AbsentParents;
	[SerializeField] RectTransform AbsentMother;
	[SerializeField] RectTransform AbsentFather;
	[SerializeField] RectTransform Lifestyle;
	[SerializeField] RectTransform Home;
	[SerializeField] RectTransform Childhood;
	[SerializeField] RectTransform Background;
	[SerializeField] RectTransform Class;

	public int LifestyleBonus;
	public int CharismaBonus;
	public enum ParentState { alive, dead, unknown };
	public ParentState Mother;
	public ParentState Father;

	// Use this for initialization
	void Start () {

		TextAsset textData = Resources.Load<TextAsset>("CharacterBackgroundDataTable");
		string[] row = textData.text.Split(new char[] { '\n' });
		var lines = new List<string[]>();

		int tableIndex = 0;


		for (int lineIndex = 0; lineIndex < row.Length; lineIndex++) {

			string[] Line = row[lineIndex].Split(new char[] { ',' });
			lines.Add(Line);
		}

		string[][] sheet = lines.ToArray();

		for (int rowIndex = 0; rowIndex < row.Length - 2; rowIndex++) {

			if (IsDieRoll(sheet[rowIndex][0])) {
				LoadTable(tableIndex, rowIndex, sheet);

				tableIndex += 1;
			}
		}
	}

	private void LoadTable(int tableIndex, int startIndex, string[][] sheet) {

		int nextIndex = startIndex + 1;
		int subRowLength = 0;

		CharacterBackground cb = new CharacterBackground();

		cb.id = tableIndex;
		cb.die = int.Parse(sheet[startIndex][0].Replace("d",""));
		cb.name = sheet[startIndex][1];

		string testCell = sheet[nextIndex][0];

		while(testCell != "" && nextIndex < sheet.GetLength(0)) {
			nextIndex += 1;
			subRowLength += 1;
			testCell = sheet[nextIndex][0];
		}
		
		cb.dieScore = new int[subRowLength];
		cb.detail = new string[subRowLength];

		int subRow = 0;

		for (int row = startIndex + 1; row < startIndex + subRowLength + 1; row++) {

			cb.dieScore[subRow] = int.Parse(sheet[row][0]);
			cb.detail[subRow] = sheet[row][1];

			subRow += 1;
		}

		Data[tableIndex] = cb;
	}

	private bool IsDieRoll(string field) {
		return field.Contains("d");
	}


	public void GenerateBackground() {
		CalculateCharismaBonus();

		for (int i = 0; i < Data.Length; i++) {

			if (Data[i] == null) { break; }

			DisplayData(Data[i]);
		}

		ShowRelevantDetails();
	}

	private void CalculateCharismaBonus() {
		string input = InputCharisma.GetComponent<Text>().text;
		int charisma = new int();
		int mod = new int();

		if(input != "") {
			charisma = int.Parse(input);
		}
		else {
			charisma = 10;
		}

		mod = Mathf.FloorToInt((charisma - 10) / 2);
		CharismaBonus = mod;
	}

	private void DisplayData(CharacterBackground table) {
		Text displayText;
		RectTransform frame = new RectTransform();
		string detail = table.RollOnTable();
		frame = GetDetailFrame(table, frame);

		if (frame != null) {
			displayText = frame.Find("Body").GetComponent<Text>();
			displayText.text = detail;
		}

	}

	private void ShowRelevantDetails() {
		ParentsTiefling.gameObject.SetActive(false);
		ParentsHalfElf.gameObject.SetActive(false);
		ParentsHalfOrc.gameObject.SetActive(false);
		AbsentParents.gameObject.SetActive(false);
		AbsentMother.gameObject.SetActive(false);
		AbsentFather.gameObject.SetActive(false);

		if (InputRace.GetComponent<Text>().text == "Tiefling") {
			ParentsTiefling.gameObject.SetActive(true);
		}
		else if (InputRace.GetComponent<Text>().text == "Half-Elf") {
			ParentsHalfElf.gameObject.SetActive(true);
		}
		else if (InputRace.GetComponent<Text>().text == "Half-Orc") {
			ParentsHalfOrc.gameObject.SetActive(true);
		}

		if(Mother == ParentState.dead && Father == ParentState.dead) {
			AbsentParents.gameObject.SetActive(true);
		}
		else if(Mother == ParentState.dead) {
			AbsentMother.gameObject.SetActive(true);
		}
		else if(Father == ParentState.dead) {
			AbsentFather.gameObject.SetActive(true);
		}
	}

	private RectTransform GetDetailFrame(CharacterBackground table, RectTransform frame) {
		if (table.name == "Parents") {
			frame = Parents;
		}
		else if (table.name == "Half-Elf Parents") {
			frame = ParentsHalfElf;
		}
		else if (table.name == "Half-Orc Parents") {
			frame = ParentsHalfOrc;
		}
		else if(table.name == "Tiefling Parents") {
			frame = ParentsTiefling;
		}
		else if (table.name == "Birthplace") {
			frame = Birthplace;
		}
		else if (table.name == "Siblings") {
			frame = Siblings;
		}
		else if (table.name == "Family") {
			frame = Family;
		}
		else if (table.name == "Family Other") {
			frame = OtherFamily;
		}
		else if (table.name == "Absent Parents") {
			frame = AbsentParents;
		}
		else if (table.name == "Absent Mother") {
			frame = AbsentMother;
		}
		else if (table.name == "Absent Father") {
			frame = AbsentFather;
		}
		else if (table.name == "Lifestyle") {
			frame = Lifestyle;
		}
		else if (table.name == "Home") {
			frame = Home;
		}
		else if (table.name == "Memories") {
			frame = Childhood;
		}
		else if (table.name == "Background") {
			frame = Background;
		}
		else if (table.name == "Class Training") {
			frame = Class;
		}

		return frame;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
