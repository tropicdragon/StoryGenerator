using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public class Translator : MonoBehaviour  {

	static ActionManager manager = (new GameObject("SomeObjName")).AddComponent<ActionManager>();

	static Dictionary<GameObject, List<string>> world_object_names = new Dictionary<GameObject, List<string>>();
	static Dictionary <string, List<GameObject>> world_inanimate_objects = new Dictionary <string, List<GameObject>> ();

	static Dictionary<string, string> actions = new Dictionary<string, string>(){
		{"sleep","Sleep"},
		{"sleeps","Sleep"},
		{"slept","Sleep"},
		{"asleep","Sleep"},
		{"dies", "Die1"},
		{"died", "Die1"},
		{"perished", "Die2"},
		{"perish", "Die2"},
		{"alert", "Alert"},
		{"victorious", "Victory"},
		{"dance", "Victory"},
		{"danced", "Victory"},
		{"dancing", "Victory"},
		{"ecstatic", "Victory"},
		{"excited", "Victory"},
		{"happy","Happy"},
		{"blocked", "Block"},
		{"blocks", "Block"},
		{"block", "Block"},
		{"push", "Push"},
		{"pushed", "Push"},
		{"pushes", "Push"},
		{"hurt", "Damage"},
		{"jumps", "Jump"},
		{"jump", "Jump"},
		{"leap", "Jump"},
		{"leaps", "Jump"},
		{"jumped", "Jump"},
		{"leaped", "Jump"},
		{"dashes", "Dash"},
		{"dashed", "Dash"},
		{"dash", "Dash"},
		{"sit", "Sit"},
		{"sits", "Sit"},
		{"sat", "Sit"},
		{"hit", "Punch"},
		{"hits", "Punch"},
		{"struck", "Punch"},
		{"strike", "Punch"},
		{"walk", "Walk"},
		{"walks", "Walk"},
		{"walked", "Walk"},
		{"ran", "Run"},
		{"run", "Run"},
		{"stood", "Stood"},
		{"stand", "Stood"},
		{"stopped", "Stood"},
		{"turned", "Turn"},
		{"turns", "Turn"},
		{"turn", "Turn"},
		{"look", "Turn"},
		{"looked", "Turn"},
		{"looks", "Turn"},
		{"killed", "Kill"},
		{"kills", "Kill"},
		{"murdered", "Kill"},
		{"murders", "Kill"},
		{"punched", "Punch"},
		{"punches", "Punch"},
		{"attack", "Attack"},
		{"attacks", "Attack"},
		{"angry", "Angry"},
		{"winked", "Wink"},
		{"winking", "Wink"},
		{"winks", "Wink"},
		{"sad", "Sad"},
		{"laughed", "Laugh"},
		{"laughs", "Laugh"},
		{"joyful", "Joy"},
		{"serious", "Serious"},
		{"annoyed", "Annoyed"},
		{"upset", "Annoyed"},
		{"surprised", "Surprised"},
		{"shocked", "Surprised"},
		{"crying", "Cry"},
		{"cried", "Cry"},
		{"pleased", "Pleased"},
		{"calm", "Pleased"},
		{"teary", "Teary"},
		{"broken-hearted", "Teary"},
		{"kiss", "Kiss"},
		{"kissed", "Kiss"},
		{"frustrated", "Frustrated"},
		{"hungry", "Drooling"},
		{"drooling", "Drooling"},
		{"drool", "Drooling"},
		{"insatiable", "Drooling"},
		{"dizzy", "Dizzy"},
		{"confused", "Confused"},
		{"tired", "Tired"},
		{"crazy", "Crazy"},
		{"evil", "Evil"},
		{"dead", "Dead"},
		{"greedy", "Greedy"},
		{"money", "Greedy"},
		{"love", "Love"},
		{"wore", "Wear"},
		{"wears", "Wear"},
		{"had", "Wear"},
		{"has", "Wear"},
		{"sports", "Wear"},
		{"sported", "Wear"},
		{"said", "Speak"},
		{"picked", "Pick Up"},
		{"picks", "Pick Up"},
		{"dropped", "Drop"},
		{"drops", "Drop"},
		{"gave", "Give"},
		{"gives", "Give"},
	};


	static Dictionary<string, string> entities = new Dictionary<string, string>()
	{
		{"cactus","Cactus"},
		{"cactuses","Cactus"},
		{"apple", "Apple"},
		{"apples", "Apple"},
		{"banana","Banana"},
		{"bananas","Banana"},
		{"branch","Branch"},
		{"branches","Branch"},
		{"carrot","Carrot"},
		{"carrots","Carrot"},
		{"cloud","Cloud"},
		{"clouds","Cloud"},
		{"corn","Corn"},
		{"mushroom","MushroomType"},
		{"mushrooms","MushroomType"},
		{"bush","Shrub"},
		{"bushes","Shrub"},
		{"shrub","Shrub"},
		{"shrubs","Shrub"},
		{"stone", "Stone"},
		{"stones", "Stone"},
		{"boulder", "Stone"},
		{"boulders", "Stone"},
		{"rock", "Stone"},
		{"rocks", "Stone"},
		{"stump","Stump"},
		{"stumps","Stump"},
		{"vine","Vine"},
		{"vines","Vine"},
		{"bear", "Bear"},
		{"bears", "Bear"},
		{"bunny", "Bunny"},
		{"bunnies", "Bunny"},
		{"cat", "Cat"},
		{"cats", "Cat"},
	};

	static Dictionary<string, string> wearables = new Dictionary<string, string>(){
		{"fish", "FishmaceA"},
		{"glasses", "GlassesA"},
		{"spectacles", "GlassesA"},
		{"backpack", "BackpackA"},
		{"bookbag", "BackpackA"},
		{"bag", "BagA"},
		{"purse", "BagA"},
		{"satchel", "BagA"},
		{"knife", "CleaverA"},
		{"cleaver", "CleaverA"},
		{"hood", "HatA"},
		{"hat", "MinihatA"},
		{"tophat", "MinihatA"},
		{"scarf", "ScarfA"},
		{"stick", "Walking stickA"},
		{"cane", "Walking stickA"},
	};

	static Dictionary<string, string> numbers = new Dictionary<string, string>()
	{	{"one", "1"},
		{"two", "2"},
		{"three", "3"},
		{"four", "4"},
		{"five", "5"},
		{"six", "6"},
		{"seven", "7"},
		{"eight", "8"},
		{"nine", "9"},
		{"ten", "10"},
		{"eleven", "11"},
		{"twelve", "12"},
		{"thirteen", "13"},
		{"fourteen", "14"},
		{"fifteen", "15"},
		{"sixteen", "16"},
		{"seventeen", "17"},
		{"eighteen", "18"},
		{"nineteen", "19"},
		{"twenty", "20"},
		{"thirty", "30"},
		{"forty", "40"},
		{"fifty", "50"},
		{"sixty", "60"},
		{"seventy", "70"},
		{"eighty", "80"},
		{"ninety", "90"},
		{"hundred", "100"},
		{"thousand", "1000"},
	};

	static GameObject previous_reference;

	public static void parse(string sentence){

		string[] sentences = Regex.Split(sentence, ", and");
		for (int a = 0; a < sentences.Length; a++) {
			sentence = sentences[a].ToLower ();

			if (sentence.Contains ("there is") || sentence.Contains ("there was") || sentence.Contains ("there were") || sentence.Contains ("there are")) {
				string phrase = "";
				int index = -1;
				if (sentence.IndexOf ("there is") != -1) {
					phrase = "there is";
					index = sentence.IndexOf ("there is");
				} else if (sentence.IndexOf ("there was") != -1) {
					phrase = "there was";
					index = sentence.IndexOf ("there was");
				} else if (sentence.IndexOf ("there were") != -1) {
					phrase = "there were";
					index = sentence.IndexOf ("there were");
				} else {
					phrase = "there are";
					index = sentence.IndexOf ("there are");
				}

				string existence_clause = sentence.Substring (index + phrase.Length);

				Translator.createSceneObjects (existence_clause);
			} else if (sentence.Contains ("name is") || sentence.Contains ("name was") || sentence.Contains ("was called") || sentence.Contains ("is called")) {
				string cleaned_sentence = (Regex.Replace (sentence, "[^a-zA-Z0-9% _]", string.Empty));
				string[] words = cleaned_sentence.Split (' ');
				world_object_names [previous_reference].Add (words [words.Length - 1]);	
			} else {
				//Some sort of action sentence
				string cleaned_sentence = (Regex.Replace (sentence, "[^a-zA-Z0-9% _]", string.Empty));
				string[] words = cleaned_sentence.Split (' ');
				GameObject current_object = null;
				int current_object_index = -1;
				bool broke_out = false;
				for (int i = 0; i < words.Length; i++) {
					if (broke_out == true) {
						break;
					}
					foreach (KeyValuePair<GameObject, List<string>> entry in world_object_names) {
						if (entry.Value.Contains (words [i])) {
							current_object = entry.Key;
							current_object_index = i;
							broke_out = true;
							break;
						}
					}
				}
				if (current_object != null) {
					for (int j = 0; j < words.Length; j++) {
						if (actions.ContainsKey (words [j])) {
							if (actions [words [j]] == "Wear") {
								for (int u = 0; u < words.Length; u++) {
									if (wearables.ContainsKey (words [u])) {
										string accessory = wearables [words [u]];
										if (accessory == "HatA") {
											GameObject pPrefab = Resources.Load ("HatA") as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "head", target_object);
										} else if (accessory == "CleaverA") {
											GameObject pPrefab = Resources.Load ("CleaverA") as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "weapon", target_object);
										} else if (accessory == "BackpackA" || (accessory == "BagA")) {
											int model_number = Random.Range (1, 4);
											GameObject pPrefab = Resources.Load (accessory + model_number) as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "accessory", target_object);
										} else if (accessory == "ScarfA") {
											int model_number = Random.Range (1, 3);
											GameObject pPrefab = Resources.Load (accessory + model_number) as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "accessory", target_object);
										} else if (accessory == "FishmaceA" || accessory == "Walking stickA") {
											int model_number = Random.Range (1, 4);
											GameObject pPrefab = Resources.Load (accessory + model_number) as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "weapon", target_object);
										} else if (accessory == "MinihatA" || accessory == "GlassesA") {
											int model_number = Random.Range (1, 4);
											GameObject pPrefab = Resources.Load (accessory + model_number) as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "head", target_object);
											
										} else if (accessory == "HatA") {
											GameObject pPrefab = Resources.Load ("HatA") as GameObject;
											GameObject target_object = GameObject.Instantiate (pPrefab);
											manager.execute (current_object, actions [words [j]], "head", target_object);
										}
										break;
									}
								}
								
							} else if (actions [words [j]] == "Speak") {
								string quote = Regex.Match (sentence, "\"([^\"]*)\"").Value;
								manager.execute (current_object, actions [words [j]], quote, null);

							}
							else if(actions [words [j]] == "Pick Up"){
								GameObject target_object = null;
								for (int r = 0; r < words.Length; r++) {
									if(entities.ContainsKey(words[r])){
										target_object = (world_inanimate_objects[entities[words[r]]])[0];
										break;
									}
								}
								manager.execute (current_object, actions [words [j]], null, target_object);
							}
							else{
								List<string> result = Translator.extractPreposition (words);
								GameObject target_object = findMatchingGameObject (result [1].Split (' '));
								if (target_object == null && words.Length > current_object_index+2) {
									target_object = findMatchingGameObject(new string[]{words[current_object_index+2]});
								}
								manager.execute (current_object, actions [words [j]], result [0], target_object);
							}
							break;
						}
					}
		
				}
			}
		}
	}

	private static GameObject findMatchingGameObject(string[] words){
		GameObject current_object = null;
		foreach (KeyValuePair<GameObject, List<string>> entry in world_object_names) {
			for (int i = 0; i < words.Length; i++) {
				if (entry.Value.Contains (words [i])) {
					current_object = entry.Key;
					break;
				}
			}
		}
		return current_object;
	}

	private static List<string> extractPreposition(string[] sentence){
		string string_sentence = string.Join (" ",sentence);
		string preposition = Regex.Match(string_sentence, @"\b(with|at|from|to|into|during|including|until|against|in|by|towards|toward|through|over|before|between|after|under|within|near|along|across|behind|near|around)").Value;
		string prep_phrase = null;
		//What happens if it's a prep phrase in the front of the sentence?
		if (preposition != null ) {
			if (Regex.Split(string_sentence,preposition).Length > 1) {
				prep_phrase = Regex.Split(string_sentence,preposition) [1];
			} else {
				prep_phrase = Regex.Split(string_sentence,",")[0];
			}

		}
	
		return new List<string>(new string[]{preposition, prep_phrase});
	}

	//Right now no choice but to hardcode.
	private static void createSceneObjects(string sentence){
		string cleaned_sentence = (Regex.Replace(sentence, "[^a-zA-Z0-9% _]", string.Empty));
		string[] words = cleaned_sentence.Split (' ');
		if (Translator.entities[words[words.Length-1]] != null) {
			string model_to_load = Translator.entities[words [words.Length - 1]];
			if (Translator.entities[words [words.Length - 1]] == "Cat" || Translator.entities[words [words.Length - 1]] == "Bunny" || Translator.entities[words [words.Length - 1]] == "Bear") {
				int model_number = Random.Range (0, 13);
				if (model_number < 10) {
					model_to_load += "0" + model_number.ToString ();
				} else {
					model_to_load +=  model_number.ToString ();
				}

			}

			//This might cause problem when there are multiple numbers in a sentence
			string iterations_string = Regex.Match(sentence, @"\d+").Value;
			
			int iterations = 1;
			if (iterations_string == null || iterations_string == "") {
				string number = Regex.Match(sentence, @"(one|two|three|four|five|six|seven|eight|nine|ten|eleven|twelve|thirteen|fourteen|fifteen|sixteen|seventeen|eighteen|nineteen|twenty|thirty|forty|fifty|sixty|seventy|eighty|ninety|hundred|thousand)").Value;
				if (number != null && number != "") {
					iterations_string = numbers [number];
				}

			}

			if (iterations_string != null && iterations_string != "") {
				int.TryParse(iterations_string, out iterations);
			}

			for (int i = 0; i < iterations; i++) {
				GameObject pPrefab = Resources.Load(model_to_load) as GameObject;
				Vector3 position = (Random.insideUnitSphere * 20) + (Camera.main.transform.position+(Camera.main.transform.forward*25));
				position.y = 0;
				GameObject entity = GameObject.Instantiate(pPrefab, position, Quaternion.identity);
				entity.transform.Rotate (0, 180, 0);
				previous_reference = entity; 
				world_object_names.Add(entity, new List<string>());
				if (Translator.entities [words [words.Length - 1]] != "Cat" && Translator.entities [words [words.Length - 1]] != "Bunny" && Translator.entities [words [words.Length - 1]] != "Bear") {
					if (world_inanimate_objects.ContainsKey (entities [words [words.Length - 1]])) {
						world_inanimate_objects [entities [words [words.Length - 1]]].Add (entity);
					} else {
						world_inanimate_objects.Add (entities [words [words.Length - 1]], new List<GameObject> ());
						world_inanimate_objects [entities [words [words.Length - 1]]].Add (entity);
					}
						

				}
			}
				
		}

	}


}
