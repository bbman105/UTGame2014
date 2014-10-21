using UnityEngine;
using System.Collections;

public class sortLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetComponent<MeshRenderer>().sortingLayerName = "Room";
        transform.GetComponent<MeshRenderer>().sortingOrder = 30;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
