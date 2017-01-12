using UnityEngine;
using System.Collections;

public class Catch : MonoBehaviour {

	[SerializeField]
	LayerMask maskPlayer;

	[SerializeField]
	Camera mainCamera;

	GameObject hitAgent;
	NavgateCo hitNav;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0) == true) {
			Ray mouseRay = mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit agentHit;

			if (Physics.Raycast (mouseRay, out agentHit, 100, maskPlayer)) {
				hitAgent = agentHit.collider.gameObject;

				if (hitAgent != null) {
					hitNav = hitAgent.GetComponent<NavgateCo> ();
					hitNav.onCatch = true;
				}
			}

		}
	
	}
}
