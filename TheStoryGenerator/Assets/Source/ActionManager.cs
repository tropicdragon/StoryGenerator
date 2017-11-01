using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionManager : MonoBehaviour {


	bool isProcessing = false; 

	public void execute(GameObject current_object, string action, string preposition, GameObject target_object){
		if (action == "Turn") {
			this.turn (current_object, target_object, preposition);
		} else if (action == "Walk") {
			this.walk (current_object, target_object, preposition, action);

		} else if (action == "Sleep") {
			this.sleep (current_object);
		} else if (action == "Die1") {
			this.die (current_object);
		} else if (action == "Die2") {
			this.perish (current_object);
		} else if (action == "Victory") {
			this.dance (current_object, preposition, target_object);
		} else if (action == "Alert") {
			this.alert (current_object);
		} else if (action == "Push") {
			this.push (current_object, target_object);
		} else if (action == "Kill") {
			this.kill (current_object, target_object);
		} else if (action == "Punch") {
			this.punch (current_object, target_object);
		} else if (action == "Sit") {
			this.sit (current_object);
		} else if (action == "Jump") {
			this.jump (current_object, preposition, target_object);
		} else if (action == "Happy") {
			this.happy (current_object);
		} else if (action == "Sad") {
			this.sad (current_object);
		} else if (action == "Wink") {
			this.wink (current_object, target_object, preposition);
		} else if (action == "Laugh") {
			this.laugh (current_object);
		} else if (action == "Joy") {
			this.joy (current_object);
		} else if (action == "Serious") {
			this.serious (current_object);
		} else if (action == "Annoyed") {
			this.annoyed (current_object);
		} else if (action == "Surprised") {
			this.surprised (current_object);
		} else if (action == "Cry") {
			this.cry (current_object);
		} else if (action == "Pleased") {
			this.pleased (current_object);
		} else if (action == "Teary") {
			this.teary (current_object);
		} else if (action == "Angry") {
			this.angry (current_object);
		} else if (action == "Kiss") {
			this.kiss (current_object);
		} else if (action == "Frustrated") {
			this.frustrated (current_object);
		} else if (action == "Drooling") {
			this.drooling (current_object);
		} else if (action == "Dizzy") {
			this.dizzy (current_object);
		} else if (action == "Confused") {
			this.confused (current_object);
		} else if (action == "Crazy") {
			this.crazy (current_object);
		} else if (action == "Evil") {
			this.evil (current_object);
		} else if (action == "Dead") {
			this.dead (current_object);
		} else if (action == "Greedy") {
			this.greedy (current_object);
		} else if (action == "Love") {
			this.love (current_object);
		} else if (action == "Stood") {
			this.stood (current_object);
		} else if (action == "Run") {
			this.run (current_object, target_object, preposition, action);
		} else if (action == "Wear") {
			this.wear (current_object, target_object, preposition);
		} else if (action == "Speak") {
			this.SpeakString (current_object, preposition);
		} else if (action == "Pick Up") {
			this.pickup (current_object, target_object);
		}
		else if (action == "Drop") {
			this.drop (current_object);
		}
		else if (action == "Give") {
			this.gave (current_object, target_object);
		}
	
	}

	public void gave(GameObject current_object, GameObject target_object){
		Animator current_anim = current_object.GetComponent<Animator> ();
		StartCoroutine(ActionManager.LerpToPositionGive(current_object, target_object, current_anim, 18));

	}

	public void drop(GameObject current_object){
		
		Transform child = current_object.transform.GetChild (2);
		Transform accessory_locator = (child.GetChild(2)).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0);
		accessory_locator.parent = null;
		accessory_locator.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
	}

	public void pickup(GameObject current_object, GameObject target_object){

		Animator current_anim = current_object.GetComponent<Animator> ();
		StartCoroutine(ActionManager.LerpToPositionPickUp (current_object, target_object, current_anim, 18));

	}

	public void SpeakString (GameObject current_object, string s) {
		VikingCrewTools.UI.SpeechBubbleManager.Instance.AddSpeechBubble(current_object.transform, s);
		System.Diagnostics.Process.Start("say", (s));
	}

	public void wear(GameObject current_object, GameObject target_object, string area){
		Transform child = current_object.transform.GetChild (2);
		if (area == "head") {
			Transform accessory_locator = (child.GetChild(2)).GetChild(0).GetChild(1).GetChild(0).GetChild(0);
			target_object.transform.SetParent (accessory_locator);
			target_object.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
			target_object.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			
		} else if (area == "weapon") {
			Transform accessory_locator = (child.GetChild(2)).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(4);
			target_object.transform.SetParent (accessory_locator);
			target_object.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
			target_object.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

		} else if (area == "accessory") {
			Transform accessory_locator = (child.GetChild(2)).GetChild(0).GetChild(0);
			target_object.transform.SetParent (accessory_locator);
			target_object.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
			target_object.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}

	}


	public void run(GameObject current_object, GameObject target_object, string preposition, string action){
		Animator current_anim = current_object.GetComponent<Animator> ();
		if (preposition != null && (preposition == "to" || preposition == "towards")) {

			this.turn (current_object, target_object, preposition);
			float distance = 0.65f;
			Vector3 target = new Vector3 (target_object.transform.position.x*distance, target_object.transform.position.y, target_object.transform.position.z*distance);
			IEnumerator coroutine = ActionManager.LerpToPosition (current_object, target, current_anim, 15);
			StartCoroutine(coroutine);

		} else {

			StartCoroutine (RotateToPosition(current_object, Quaternion.Euler(0, Random.Range (0.0f, 360.0f), 0)));
			StartCoroutine(ActionManager.RandomWalk(current_object, current_anim, 15));
		}
	}


	public void stood(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face01", typeof(Material));
		Animator current_anim = current_object.GetComponent<Animator> ();
		current_anim.SetInteger ("animation", 19);

	}


	public void love(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face27", typeof(Material));

	}


	public void greedy(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face26", typeof(Material));

	}

	public void dead(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face25", typeof(Material));

	}


	public void evil(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face23", typeof(Material));

	}

	public void crazy(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face22", typeof(Material));

	}


	public void confused(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face20", typeof(Material));

	}


	public void dizzy(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face19", typeof(Material));

	}


	public void drooling(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face18", typeof(Material));

	}

	public void frustrated(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face16", typeof(Material));

	}


	public void kiss(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face15", typeof(Material));

	}



	public void angry(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face14", typeof(Material));

	}


	public void teary(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face13", typeof(Material));

	}

	public void pleased(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face11", typeof(Material));

	}

	public void cry(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face10", typeof(Material));

	}

	public void surprised(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face09", typeof(Material));

	}


	public void annoyed(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face08", typeof(Material));

	}


	public void serious(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face07", typeof(Material));

	}


	public void joy(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face06", typeof(Material));

	}


	public void laugh(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face05", typeof(Material));

	}

	public void wink(GameObject current_object, GameObject target_object, string preposition){

		if (preposition != null && (preposition == "to" || preposition == "at")) {
			//current_object.transform.LookAt (target_object.transform);
			StartCoroutine (RotateToPosition(current_object, Quaternion.LookRotation( target_object.transform.position - current_object.transform.position )));

		}
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face04", typeof(Material));

	}

	public void sad(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face02", typeof(Material));

	}

	public void happy(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face03", typeof(Material));

	}

	public void jump(GameObject current_object, string preposition, GameObject target_object){
		Animator current_anim = current_object.GetComponent<Animator> ();
		if(preposition != null && preposition == "around"){
			StartCoroutine (RotateToPosition(current_object, Quaternion.Euler(0, Random.Range (0.0f, 360.0f), 0)));
			StartCoroutine(ActionManager.RandomWalk(current_object, current_anim, 9));

		}
		else if  (preposition != null && (preposition == "to" || preposition == "at" || preposition == "toward" || preposition == "towards")){
			this.turn (current_object, target_object, preposition);
			float distance = 0.65f;
			Vector3 target = new Vector3 (target_object.transform.position.x*distance, target_object.transform.position.y, target_object.transform.position.z*distance);
			IEnumerator coroutine = ActionManager.LerpToPosition (current_object, target, current_anim, 9);
			StartCoroutine(coroutine);
		}
		else{

			current_anim.SetInteger ("animation", 9);
		}

	}


	public void sit(GameObject current_object){
		Animator current_anim = current_object.GetComponent<Animator> ();
		current_anim.SetInteger ("animation", 10);
	}

	public void alert(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face24", typeof(Material));
		Animator current_anim = current_object.GetComponent<Animator> ();
		current_anim.SetInteger ("animation", 2);
	}

	public void punch(GameObject current_object, GameObject target_object){

		StartCoroutine(ActionManager.simultaneousAction(current_object, target_object, 12, 5, false));

	}



	public void kill(GameObject current_object, GameObject target_object){

		StartCoroutine(ActionManager.simultaneousAction(current_object, target_object, 13, 7, true));


	}

	public void push(GameObject current_object, GameObject target_object){

		StartCoroutine(ActionManager.simultaneousAction(current_object, target_object, 11, 5, false));
		
	}

	public void dance(GameObject current_object, string preposition, GameObject target_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face17", typeof(Material));
		Animator current_anim = current_object.GetComponent<Animator> ();
		if(preposition != null && preposition == "around"){
			StartCoroutine (RotateToPosition(current_object, Quaternion.Euler(0, Random.Range (0.0f, 360.0f), 0)));
			StartCoroutine(ActionManager.RandomWalk(current_object, current_anim, 3));
			
		}
		else if  (preposition != null && (preposition == "to" || preposition == "at" || preposition == "toward" || preposition == "towards")){
			this.turn (current_object, target_object, preposition);
			float distance = 0.65f;
			Vector3 target = new Vector3 (target_object.transform.position.x*distance, target_object.transform.position.y, target_object.transform.position.z*distance);
			IEnumerator coroutine = ActionManager.LerpToPosition (current_object, target, current_anim, 3);
			StartCoroutine(coroutine);
		}
		else{

			current_anim.SetInteger ("animation", 3);
		}

	}

	public void perish(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face25", typeof(Material));
		Animator current_anim = current_object.GetComponent<Animator> ();
		current_anim.SetInteger ("animation", 7);
	}

	public void die(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face25", typeof(Material));
		Animator current_anim = current_object.GetComponent<Animator> ();
		current_anim.SetInteger ("animation", 6);
	}

	public void sleep(GameObject current_object){
		Transform first_child = current_object.transform.GetChild (1);
		GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
		face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face11", typeof(Material));
		Animator current_anim = current_object.GetComponent<Animator> ();
		current_anim.SetInteger ("animation", 21);

	}

	public void turn(GameObject current_object, GameObject target_object, string preposition){

		if (preposition != null && (preposition == "to" || preposition == "at" || preposition == "toward" || preposition == "towards")) {
			StartCoroutine (RotateToPosition(current_object, Quaternion.LookRotation( target_object.transform.position - current_object.transform.position )));
		
		} else {
			
			StartCoroutine (RotateToPosition(current_object,    Quaternion.Euler(0, Random.Range (0.0f, 360.0f), 0)));

		}
	}

	public void walk(GameObject current_object, GameObject target_object, string preposition, string action){
		Animator current_anim = current_object.GetComponent<Animator> ();
		if (preposition != null && (preposition == "to" || preposition == "towards")) {

			this.turn (current_object, target_object, preposition);
			float distance = 0.65f;
			Vector3 target = new Vector3 (target_object.transform.position.x*distance, target_object.transform.position.y, target_object.transform.position.z*distance);
			IEnumerator coroutine = ActionManager.LerpToPosition (current_object, target, current_anim, 18);
			StartCoroutine(coroutine);
	
		} else {
			
			StartCoroutine (RotateToPosition(current_object, Quaternion.Euler(0, Random.Range (0.0f, 360.0f), 0)));
		
			StartCoroutine(ActionManager.RandomWalk(current_object, current_anim, 18));
		}
	}




	private static IEnumerator simultaneousAction(GameObject current_object, GameObject target_object, int animation1, int animation2, bool permanent){
		float t0 = 0;
		while (t0 < 0.5) {	
			t0 += Time.deltaTime; 
			current_object.transform.rotation = Quaternion.Slerp( current_object.transform.rotation,  Quaternion.LookRotation( target_object.transform.position - current_object.transform.position ), t0);
			yield return null;   

		}


		Animator current_anim = current_object.GetComponent<Animator> ();
		Animator target_anim = target_object.GetComponent<Animator> ();
		if (Vector3.Distance (current_object.transform.position, target_object.transform.position) > 2.5) {
			float t1 = 0;
			float speed = 0.60f;
			float distance = 0.85f;

			Vector3 target = new Vector3 (target_object.transform.position.x * distance, target_object.transform.position.y, target_object.transform.position.z * distance);
			current_anim.SetInteger ("animation", 18);

			while (t1 < (Vector3.Distance (current_object.transform.position, target_object.transform.position))) {
				t1 += Time.deltaTime;    
				target_object.transform.rotation = Quaternion.Slerp( target_object.transform.rotation,  Quaternion.LookRotation( current_object.transform.position - target_object.transform.position ), t1);
				current_object.transform.position = Vector3.Lerp (current_object.transform.position, target, speed * Time.deltaTime);
				yield return null;   
			}
		}

		float t2 = 0;

		while (t2 < 1) {
			t2 += Time.deltaTime * 0.50f;    
			current_anim.SetInteger ("animation", animation1);
			target_anim.SetInteger ("animation", animation2);
			yield return null;  
		}
		current_anim.SetInteger ("animation", 1);
		if (permanent == false)
			target_anim.SetInteger ("animation", 1);
		else {
			Transform first_child = target_object.transform.GetChild (1);
			GameObject face = first_child.GetChild (first_child.childCount - 1).gameObject;
			face.GetComponent<Renderer> ().material =  (Material)Resources.Load("Face25", typeof(Material));
		}
	}

	private static IEnumerator RandomWalk(GameObject current_object, Animator current_anim, int animation){
		float t = 0;
		int speed = 5;
		current_anim.SetInteger ("animation", animation);

		while (t < 2) {	
			t += Time.deltaTime;    
			current_object.transform.position += current_object.transform.forward * speed * Time.deltaTime;
			yield return null;   

		}
		current_anim.SetInteger ("animation", 1);

	}


	private static IEnumerator LerpToPosition(GameObject current_object, Vector3 target, Animator current_anim, int animation)
{		

		float t = 0;
		float speed = 0.75f;
		current_anim.SetInteger ("animation", animation);

	while(t < 3)
		{
		t += Time.deltaTime;    
		current_object.transform.position = Vector3.Lerp(current_object.transform.position, target, speed * Time.deltaTime);

		yield return null;   
	}
		current_anim.SetInteger ("animation", 1);

}


	private static IEnumerator LerpToPositionPickUp(GameObject current_object, GameObject target_object, Animator current_anim, int animation)
	{		
		float t0 = 0;
		while (t0 < 0.5) {	
			t0 += Time.deltaTime; 
			current_object.transform.rotation = Quaternion.Slerp( current_object.transform.rotation,  Quaternion.LookRotation( target_object.transform.position - current_object.transform.position ), t0);
			yield return null;   

		}

		float t = 0;
		float speed = 0.75f;
		current_anim.SetInteger ("animation", animation);

		while(t < 3)
		{
			t += Time.deltaTime;    
			current_object.transform.position = Vector3.Lerp(current_object.transform.position, target_object.transform.position, speed * Time.deltaTime);

			yield return null;   
		}
		current_anim.SetInteger ("animation", 1);
		Transform child = current_object.transform.GetChild (2);
		Transform accessory_locator = (child.GetChild(2)).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(4);
		target_object.transform.SetParent (accessory_locator);
		target_object.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		target_object.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

	}


	private static IEnumerator LerpToPositionGive(GameObject current_object, GameObject target_object, Animator current_anim, int animation)
	{		
		float t0 = 0;
		while (t0 < 0.5) {	
			t0 += Time.deltaTime; 
			current_object.transform.rotation = Quaternion.Slerp( current_object.transform.rotation,  Quaternion.LookRotation( target_object.transform.position - current_object.transform.position ), t0);
			yield return null;   

		}

		float t = 0;
		float speed = 0.75f;
		current_anim.SetInteger ("animation", animation);

		while(t < 3)
		{
			t += Time.deltaTime;    
			current_object.transform.position = Vector3.Lerp(current_object.transform.position, target_object.transform.position, speed * Time.deltaTime);

			yield return null;   
		}
		current_anim.SetInteger ("animation", 1);
		Transform child = current_object.transform.GetChild (2);
		Transform accessory_locator = (child.GetChild(2)).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0);
		accessory_locator.parent = null;
		accessory_locator.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		Transform child_target = target_object.transform.GetChild (2);
		Transform accessory_locator_target = (child_target.GetChild(2)).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(4);
		accessory_locator.SetParent (accessory_locator_target);
		accessory_locator.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		accessory_locator.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

	}

	private static IEnumerator RotateToPosition(GameObject current_object, Quaternion rotation)
	{	
		float t = 0;
		//float delay = 1.5f;
		while (t < 1) {	
			t += Time.deltaTime; 
			current_object.transform.rotation = Quaternion.Slerp( current_object.transform.rotation, rotation, t);
		
			yield return null;   

		}
				
	}


}

