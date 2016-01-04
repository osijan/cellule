using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class followMouse : MonoBehaviour {
	//flag to check if the user has tapped / clicked. 
	//Set to true on click. Reset to false on reaching destination
	private bool flag = false;
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player or gameobject
	public float speed = 1;
	public static GameObject playerPrefab1;
	public GameObject playerPrefab;

	//vertical position of the gameobject
	private float yAxis;

	private int count;
	public Text countText;



	public void follow(){
		//check if the screen is clicked   
		// if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
		//{
		//declare a variable of RaycastHit struct
		RaycastHit hit;
		
		//Create a Ray where mouse is or on click
		
		//for unity editor
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		
		//Check if the ray hits any collider
		if(Physics.Raycast(ray,out hit))
		{
			//set a flag to indicate to move the gameobject
			flag = true;
			//save the click / tap position
			endPoint = hit.point;
			//do not change the y axis value based position, reset it to original y axis value
			endPoint.y = yAxis;
			
		}
		
		
		//check if the flag for movement is true and the current gameobject position is not same as the pointer
		if(flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude)){ //&& !(V3Equal(transform.position, endPoint))){
			//move the gameobject to the desired position
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1/(speed*(Vector3.Distance(gameObject.transform.position, endPoint))));
		}
		//set the movement indicator flag to false if the endPoint and current gameobject position are equal
		else if(flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude)) {
			flag = false;
			
		}

	}


	public GameObject player1;
	public GameObject player2;
	void Start(){

		player1 = GameObject.Find ("playerPrefab");
		CameraController.player = player1;
		//save the y axis value of gameobject
		yAxis = gameObject.transform.position.y;
		// used for score
		count = 0;
		SetCountText();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		follow ();
		if (Input.GetKeyDown ("space")) {
			StartCoroutine (jumpSpeed ());
		}
		if (Input.GetKeyDown ("e")) {
			speed=100000000000;

			player2 = GameObject.Instantiate(playerPrefab);//, new Vector3( 2.0F, 0, 0), Quaternion.identity) as GameObject;
			player2 = GameObject.Find("playerPrefab(Clone)");

			//CameraController mycamera = new CameraController();
			CameraController.player= player2;
			//playerPrefab.rigidbody.constraints = RigidbodyConstraints.FreezeAll;


		}
	
		if (Input.GetKeyDown ("f")) {
			speed=100000000000;
			CameraController.player = playerPrefab;
			playerPrefab = playerPrefab;
			speed = 15;
		}
	}

	IEnumerator  jumpSpeed()
	{

			speed -= 5;
			yield return new WaitForSeconds (1);
			speed += 5;
			

	}

	// Destroy everything that enters the trigger
	void OnTriggerEnter (Collider other) {


		//if (other.gameObject.CompareTag("Pick Up"))

		if (other.transform.localScale.magnitude < transform.localScale.magnitude)
		{
			// Make dot disappear
			other.gameObject.SetActive (false);
			//increase score
			count = count +1;
			SetCountText();
			//Change size and speed
			transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
			speed += 0.3f;



		}

}
	//Score Method
	void SetCountText()
	{
		countText.text= "Score: " +count.ToString() +" Speed: "+speed.ToString ();


	}


}



