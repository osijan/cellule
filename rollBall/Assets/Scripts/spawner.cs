using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
	public Transform spawnWhat;
	public int spawn = 20;
	// Use this for initialization
	void Start () {

		for (int x = 0; x < spawn; x++) {
			Instantiate (spawnWhat, new Vector3 (Random.Range (-10.0F, 10.0F), 0, Random.Range (-10.0F, 10.0F)), Quaternion.identity);
		}


	
	}

		
	
	
	//int x;
	// Update is called once per frame
	void Update () {
		//GameObject thePlayer = GameObject.Find("Player");
		//followMouse followMouse = thePlayer.GetComponent<followMouse> ();



	
	}
}
