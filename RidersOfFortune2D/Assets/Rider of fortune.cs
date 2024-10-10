using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Rideroffortune : MonoBehaviour
{
	public TMP_Text MapBoard;

	public TMP_Text PlayerPositionNav;

	public TMP_Text DiceRoll;

	public TMP_Text YarraPlayer;

	public TMP_Text WeaponAttack;

	public TMP_Text TotalAttack;

	public TMP_Text MonsterHP;

	public TMP_Text DragonHP;

	public TMP_Text RollDieAttack;

	public TMP_Text Continue;

	private string currentMapBoard;

	public TMP_Text Mapboard2;

	public TMP_Text travelDice;

	public TMP_Text DisplayTextMessage;

	public TMP_Text endingMessage;

	public TMP_Text AttackText;

	public GameObject TravelButton;

	public GameObject DismountedButton;

	public GameObject ContinueButton;

	public GameObject QuitButton;

	private int choice;

	private int MAP_SIZE = 28;

	enum Weapon { K = 1, C = 3, F = 4, B = 5, G = 6, S = 7 };

	private char[] Map = { 'P', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm', 'm','D' };

	private char[] MapCopy = { 'P', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ','D', };

	private char[] Board = { 'P', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', 'D' };

	private char[] BoardCopy = {'P', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', 'D', };

	private int PlayerPosition = 0;

	private int rolldice = 0;

	private int YarraXP = 0;

	private int monsterHP = 0;

	private int dragonHP = 10;

	private int rollDieAttack = 0;

	private int totalAttack = 0;

	private Weapon attack = Weapon.K;

	private char[] PopA = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ' };

	private char[] PopB = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ' };

	private int NumEmptySpaces = 7;

	private int NumWeapons = 5;

	private int NumOfDragons = 1;

	public string[] array = new string[28];

	void Start()
	{
		assignEmptySpaces();
		assignWeapons();
		displayMenu();

		for (int index = 0; index < MAP_SIZE; index++)
		{
			MapCopy[index] = Map[index];
		}

		ContinueButton.SetActive(false);
		QuitButton.SetActive(false);
		TravelButton.SetActive(true);
		DismountedButton.SetActive(true);

		Update();
	}

	void Update()
	{
		displayMenu();
		choice = 0;

		/*if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			choice = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			choice = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			choice = 3;
		}

		if (choice != 0)
		{
			if (choice == 1)
			{
				monsterHP = 0;
				rollDieAttack = 0;
				totalAttack = 0;
				traverseBoard();
				//displayMenu();
			}
			else if (choice == 2)
			{
				dismountSpace();
				//displayMenu();
			}
			else if (choice == 3)
			{
				ContinueOrQuit();
			}
		}*/
	}

	private int randomNumber(int min, int max)
	{
		return Random.Range(min, max);
	}

	private void assignEmptySpaces()
	{
		for (int index = 0; index < NumEmptySpaces; index++)
		{
			int ram = randomNumber(1, 26);
			bool assigned = false;
			while (!assigned)
			{
				if (Map[ram] == 'm')
				{
					Map[ram] = 'e';
					assigned = true;
				}
				else
				{
					ram = randomNumber(1, 26);
				}
			}
		}
	}

	private void assignEmpySpacesBoard()
	{
		for (int index = 0; index < NumEmptySpaces; index++)
		{
			int ram = randomNumber(1, 26);
			bool assigned = false;
			while (!assigned)
			{
				if (Board[ram] == '*')
				{
					Board[ram] = 'e';
					assigned = true;
				}
				else
				{
					ram = randomNumber(1, 26);
				}
			}
		}
	
	}

	private void assignWeapons()
	{
		bool crossBow = false;
		bool flail = false;
		bool BroadSword = false;
		bool DragonSlayer = false;
		bool Spell_of_The_Gods = false;
		for (int index = 0; index < NumWeapons; index++)
		{
			int ram = randomNumber(1, 26);

			bool assigned = false;
			while (!assigned)
			{
				if (Map[ram] == 'm')
				{
					assigned = true;
					if (!crossBow)
					{
						Map[ram] = 'C';
						crossBow = true;
					}
					else if (!flail)
					{
						Map[ram] = 'F';
						flail = true;
					}
					else if (!BroadSword)
					{
						Map[ram] = 'B';
						BroadSword = true;
					}
					else if (!DragonSlayer)
					{
						Map[ram] = 'G';
						DragonSlayer = true;
					}
					else if (!Spell_of_The_Gods)
					{
						Map[ram] = 'S';
						Spell_of_The_Gods = true;
					}
				}
				else
				{
					ram = randomNumber(1, 26);
				}
			}
		}
	}

	private void printMap()
	{
		currentMapBoard = "";
		for (int index = 0; index < MAP_SIZE; index++)
		{
			currentMapBoard += Map[index] + " ";
		}
		MapBoard.text = currentMapBoard;
	}

	private void printBoard()
	{
		// make a map board with the array of the map board and print it 
		// with the player position as 'P' and the dragon as 'D
		// and the empty spaces as 'm' and the weapons as 'C', 'F', 'B', 'G', 'S
		// and the monsters as 'm' and the player position as 'P' and the dragon as 'D

		for (int index = 0; index < MAP_SIZE; index++)
		{
			Board[index] = BoardCopy[index];
		}
		Board[PlayerPosition] = 'P';
		Board[MAP_SIZE - 1] = 'D';

		string currentMapBoard = "";
		for (int index = 0; index < MAP_SIZE; index++)
		{
			currentMapBoard += Board[index] + " ";
		}
		Mapboard2.text = currentMapBoard;
	}

	private void printPop()
	{
		char[] PopB = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ' };
		for (int index = 0; index < MAP_SIZE; index++)
		{
			PopA[index] = PopB[index];
		}
		PopA[PlayerPosition] = 'P';
		string currentPop = "";
		for (int index = 0; index < MAP_SIZE; index++)
		{
			currentPop += PopA[index] + " ";
		}

		PlayerPositionNav.text = currentPop;
		PlayerPositionNav.text = "P" + PlayerPosition;
		DiceRoll.text = "Dice Roll: " + rolldice;
		travelDice.text = "Travel Dice: " + rolldice;
		YarraPlayer.text = "Yarra's XP: " + YarraXP;
		WeaponAttack.text = "Weapon Attack: +" + (int)attack;
		TotalAttack.text = "Total Attack: (DieRoll = " + rollDieAttack + "+ Yarra's XP " + YarraXP + " + Weapon Attack = " + (int)attack + ") = " + totalAttack;
		MonsterHP.text = "Monster HP: " + monsterHP;
		DragonHP.text = "Dragon HP: " + dragonHP;
		RollDieAttack.text = "Roll Die Attack: " + rollDieAttack;
	}

	private void displayMenu()
	{
		printMap();
		printPop();
		printBoard();
	}

	public void traverseBoard()
	{
		rolldice = randomNumber(1, 6);

		if (PlayerPosition == 0 && rolldice != 1)
		{
			PlayerPosition = rolldice - 1;
		}
		else if (PlayerPosition == 0 && rolldice == 1)
		{
			PlayerPosition = rolldice;
		}
		else if (PlayerPosition + rolldice > MAP_SIZE - 1)
		{
			PlayerPosition = (PlayerPosition + rolldice) - MAP_SIZE;
			YarraXP = 0;
			attack = Weapon.K;
			for (int index = 0; index < MAP_SIZE; index++)
			{
				Map[index] = MapCopy[index];
			}
			for (int index = 0; index < MAP_SIZE; index++)
			{
				Board[index] = BoardCopy[index];
			}
		}
		else if (PlayerPosition + rolldice == MAP_SIZE)
		{
			PlayerPosition = MAP_SIZE - 1;
		}
		else
		{
			PlayerPosition = PlayerPosition + rolldice;
		}
	}

	public void dismountSpace()
	{
		if (Map[PlayerPosition] == 'P')
		{
			Debug.Log(" Space 0. YarraXP= 0. Weapon K: Knife +1");
		}

		else if (Map[PlayerPosition] == 'C' || Map[PlayerPosition] == 'F' || Map[PlayerPosition] == 'B' || Map[PlayerPosition] == 'G' ||
			Map[PlayerPosition] == 'S')
		{
			if (Map[PlayerPosition] == 'C' && attack < Weapon.C)
			{
				attack = Weapon.C;
				Map[PlayerPosition] = '3';
				Board[PlayerPosition] = '3';
			}
			else if (Map[PlayerPosition] == 'F' && attack < Weapon.F)
			{
				attack = Weapon.F;
				Map[PlayerPosition] = '4';
				Board[PlayerPosition] = '4';
			}
			else if (Map[PlayerPosition] == 'B' && attack < Weapon.B)
			{
				attack = Weapon.B;
				Map[PlayerPosition] = '5';
				Board[PlayerPosition] = '5';
			}
			else if (Map[PlayerPosition] == 'G' && attack < Weapon.G)
			{
				attack = Weapon.G;
				Map[PlayerPosition] = '6';
				Board[PlayerPosition] = '6';
			}
			else if (Map[PlayerPosition] == 'S' && attack < Weapon.S)
			{
				attack = Weapon.S;
				Map[PlayerPosition] = '7';
				Board[PlayerPosition] = '7';
			}
			else
			{
				Debug.Log("You already have a better weapon.");
				DisplayTextMessage.text = "You already have a better weapon.";
			}

			if (Map[PlayerPosition] == 'C' && attack > Weapon.C)
			{
				Debug.Log("You already have a better weapon.");
				DisplayTextMessage.text = "You already have a better weapon.";
			}
			if (Map[PlayerPosition] == 'F' && attack > Weapon.F)
			{
				Debug.Log("You already have a better weapon.");
				DisplayTextMessage.text = "You already have a better weapon.";
			}
			if (Map[PlayerPosition] == 'B' && attack > Weapon.B)
			{
				Debug.Log("You already have a better weapon.");
				DisplayTextMessage.text = "You already have a better weapon.";
			}
			if (Map[PlayerPosition] == 'D' && attack > Weapon.G)
			{
				Debug.Log("You already have a better weapon.");
				DisplayTextMessage.text = "You already have a better weapon.";
			}
			if (Map[PlayerPosition] == 'S' && attack > Weapon.S)
			{
				Debug.Log("You already have a better weapon.");
				DisplayTextMessage.text = "You already have a better weapon.";
			}
			
			else if (Map[PlayerPosition] == '3')
			{
				Debug.Log(" You've got Weapon C: Crossbow! ");
				DisplayTextMessage.text = "You've got Weapon C: Crossbow!";
			}

			else if (Map[PlayerPosition] == '4')
			{
				Debug.Log(" You've got Weapon F: Flail! ");
				DisplayTextMessage.text = "You've got Weapon F: Flail!";
			}

			else if (Map[PlayerPosition] == '5')
			{
				Debug.Log(" You've got Weapon B: Broad Sword! ");
				DisplayTextMessage.text = "You've got Weapon B: Broad Sword!";
			}
			else if (Map[PlayerPosition] == '6')
			{
				Debug.Log(" You've got Weapon G: Dragon Slayer! ");
				DisplayTextMessage.text = "You've got Weapon G: Dragon Slayer!";
			}
			else if (Map[PlayerPosition] == '7')
			{
				Debug.Log(" You've got Weapon S: Spell of the gods! ");
				DisplayTextMessage.text = "You've got Weapon S: Spell of the gods!";
			}
		}
		
		else if (Map[PlayerPosition] == 'e')
		{
			Map[PlayerPosition] = '1';
			Board[PlayerPosition] = '1';
			YarraXP++;
			Debug.Log("There is nothing here. You gain 1 XP.\n");
			DisplayTextMessage.text = "There is nothing here. You gain 1 XP.";
		}
		else if (Map[PlayerPosition] == '1')
		{
			Debug.Log("You've got 1 XP.\n");
			DisplayTextMessage.text = "You've got 1 XP.";
		}

		else if (Map[PlayerPosition] == 'm')
		{
			do
			{
				monsterHP = randomNumber(3, 7);
				rollDieAttack = randomNumber(1, 6);
				totalAttack = rollDieAttack + YarraXP + (int)attack;
			} while (totalAttack == monsterHP);

			Debug.Log("Total Attack= " + totalAttack + " (Dice Roll= " + rollDieAttack + " + YarraXP =" + YarraXP +" + Weapon= " + (int)attack + ") MonsterHP: " + monsterHP);
			AttackText.text = "Total Attack= " + totalAttack + " (Dice Roll= " + rollDieAttack + " + YarraXP =" + YarraXP + " + Weapon= " + (int)attack + ") MonsterHP: " + monsterHP;

			if (totalAttack > monsterHP)
			{
				Map[PlayerPosition] = '2';
				Board[PlayerPosition] = '2';
				YarraXP += 2;
				Debug.Log("You killed the monster! You gain 2 XP.");
				DisplayTextMessage.text = "You killed the monster! You gain 2 XP.";
			}
			else
			{
				Debug.Log("You were killed by the monster! GAMEOVER.\n");
				DisplayTextMessage.text = "You were killed by the monster! GAMEOVER.";
				//exit(0);
				ContinueOrQuit();
			}
		}

		else if (Map[PlayerPosition] == '2')
		{
			Debug.Log("You've got 2 XP. time to travel");
			DisplayTextMessage.text = "You've got 2 XP. time to travel";
		}

		else if (Map[PlayerPosition] == 'D' && YarraXP >= 5)
		{
			do
			{
				rollDieAttack = randomNumber(1, 6);
				totalAttack = rollDieAttack + YarraXP + (int)attack;
			} while (totalAttack == dragonHP);

			Debug.Log("Total Attack= " + totalAttack + " (Dice Roll= " + rollDieAttack + " + YarraXP =" + YarraXP +" + Weapon= " + (int)attack + ") DragonHP: " + dragonHP);
			AttackText.text = "Total Attack= " + totalAttack + " (Dice Roll= " + rollDieAttack + " + YarraXP =" + YarraXP +" + Weapon= " + (int)attack + ") DragonHP: " + dragonHP;


			if (totalAttack > dragonHP)
			{
				Map[PlayerPosition] = '$';
				Board[PlayerPosition] = '$';
				Debug.Log("You killed the dragon! You have gotten the teasure of wisdom");
				DisplayTextMessage.text = "You killed the dragon! You have gotten the teasure of wisdom";

				string dragonDefeatedd1 =
					"Due to your bravery and experience, you have defeated the dragon.";
				string dragonDefeatedd2 =
					"Your reward is the treasure of wisdom.";
				string dragonDefeatedd3 =
					"Congratulations! Your Quest is complete!";
				string dragonDefeatedMessage = dragonDefeatedd1 + dragonDefeatedd2 + dragonDefeatedd3;
				Debug.Log(dragonDefeatedMessage);
				endingMessage.text = dragonDefeatedMessage;
				//exit(0);
				ContinueOrQuit();
			}
			else
			{
				DisplayTextMessage.text = "You were killed by the dragon! GAMEOVER!";
				//exit(0);
				ContinueOrQuit();
			}
		}
		
		else if ((Map[PlayerPosition] == 'D') && (YarraXP < 5))
		{
			DisplayTextMessage.text = "You were killed by the dragon! GAMEOVER. (Yarra need 5 XP to fight the dragon.)\n";

			string xpGameOver1 =
				"The dragon is too strong for you.";
			string xpGameOver2 =
				"You need to gain more experience before you can fight the dragon.";
			string xpGameOver3 =
				"Come back when you have more experience.";
			string xpGameOverMessage = xpGameOver1 + xpGameOver2 + xpGameOver3;
			Debug.Log(xpGameOverMessage);
			endingMessage.text = xpGameOverMessage;
			//exit(0);
			ContinueOrQuit();
		}
	}

	private void ContinueOrQuit()
	{
		ContinueButton.SetActive(true);
		QuitButton.SetActive(true);
		TravelButton.SetActive(false);
		DismountedButton.SetActive(false);
		Continue.text = "Would you like to continue or quit?\n1. Continue\n2. Quit";
		choice = 0;
		/*if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			choice = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			choice = 2;
		}

		if (choice == 1)
		{
			resetGame();
		}
		else if (choice == 2)
		{
			Application.Quit();
		}*/
	}



	public void resetGame()
	{
		PlayerPosition = 0;
		Continue.text = "";
		DisplayTextMessage.text = "";
		endingMessage.text = "";
		AttackText.text = "";
		rolldice = 0;
		YarraXP = 0;
		attack = Weapon.K;
		monsterHP = 0;
		dragonHP = 10;
		rollDieAttack = 0;
		totalAttack = 0;
		ContinueButton.SetActive(false);
		QuitButton.SetActive(false);
		TravelButton.SetActive(true);
		DismountedButton.SetActive(true);
		for (int index = 0; index < MAP_SIZE; index++)
		{
			Map[index] = MapCopy[index];
		}
		for (int index = 0; index < MAP_SIZE; index++)
		{
			Board[index] = BoardCopy[index];
		}
	}

/****************/

}
