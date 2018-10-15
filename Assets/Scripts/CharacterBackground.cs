using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBackground {

	public int id;
	public string name;
	public int die;
	public int[] dieScore;
	public string[] detail;

	public string RollOnTable() {
		string output = null;

		int length = dieScore.Length;
		int dieRoll = TableRoll();

		for (int i = 0; i < length; i++) {

			if (dieRoll <= dieScore[i]) {
				output = detail[i];
				break;
			}
		}

		return output;
	}

	private int TableRoll() {
		int roll = 0;

		if (name == "Lifestyle") {
			roll = DieRoll(3, 6);
			ApplyLifestyleBonus(roll);
		}
		else if (name == "Memories") {
			roll = DieRoll(3, 6) + Loader().CharismaBonus;
		}
		else if(name == "Home") {
			roll = DieRoll(1, die) + Loader().LifestyleBonus;
		}
		else {
			roll = DieRoll(1, die);
		}

		if(name == "Parents") {

			if(roll > dieScore[0]) {
				Loader().Mother = LoadCharacterBackground.ParentState.unknown;
				Loader().Father = LoadCharacterBackground.ParentState.unknown;
			}
			else {
				Loader().Mother = LoadCharacterBackground.ParentState.alive;
				Loader().Father = LoadCharacterBackground.ParentState.alive;
			}
		}
		else if (name == "Family") {
			//reroll if you get raised by parents when you dont know who your parents are
			if (Loader().Mother == LoadCharacterBackground.ParentState.unknown) { 
				if( roll > dieScore[9]) {
					while(roll > dieScore[9]) {
						roll = DieRoll(1, die);
					}
				}
			}
			else {
				if (roll <= dieScore[9] && roll > dieScore[8]) {
					Loader().Father = LoadCharacterBackground.ParentState.dead;
				}
				else if (roll <= dieScore[8] && roll > dieScore[7]) {
					Loader().Mother = LoadCharacterBackground.ParentState.dead;
				}
				else if (roll <= dieScore[7]) {
					Loader().Father = LoadCharacterBackground.ParentState.dead;
					Loader().Mother = LoadCharacterBackground.ParentState.dead;
				}
			}
		}


		return roll;
	}

	private LoadCharacterBackground Loader() {
		return GameObject.FindObjectOfType<LoadCharacterBackground>();
	}

	private int DieRoll (int count, int die) {
		int roll = new int();

		for (int i = 0; i < count; i++) {
			roll += UnityEngine.Random.Range(1, die);
		}
		return roll;
	}

	private void ApplyLifestyleBonus(int roll) {
		int bonus = new int();
		LoadCharacterBackground loader = GameObject.FindObjectOfType<LoadCharacterBackground>();

		if (roll <= 3) {
			bonus = -40;
		}
		else if(roll <= 5) {
			bonus = -20;
		}
		else if (roll <= 8) {
			bonus = -10;
		}
		else if (roll <= 12) {
			bonus = 0;
		}
		else if (roll <= 15) {
			bonus = 10;
		}
		else if (roll <= 17) {
			bonus = 20;
		}
		else if (roll > 17) {
			bonus = 40;
		}

		loader.LifestyleBonus = bonus;
	}
}
