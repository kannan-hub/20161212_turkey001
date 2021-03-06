using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NavgateCo : MonoBehaviour {

	public GameObject[] targets;
	private int targetIndex;
	private Vector3 targetPos;

	public GameObject gameManager;
	public GameObject parent;
	public GameObject dest;
	public DustImage dustImage;

	public bool onCatch = false;
	private bool getDest = false;
	private bool arriveDest = false;

	const float closeDistance = 1.0f;

	NavMeshAgent agent;
	Rigidbody agentRig;
	Camera mainCamera;

	private Animator _anim;
	private Animator m_Anim{
		get{
			if (_anim == null) {
				_anim = GetComponentInChildren<Animator> ();
			}
			return _anim;
		}
	}

	public void Init(GameObject[] tars){
		targets = tars;
		targetIndex = 0;
		targetPos = targets [targetIndex].transform.position;
	}

	// Use this for initialization
	void Start () {
		Coroutine corutine;

		corutine = StartCoroutine (UpdateNavMesh ());
	}

	void Disable(){
		StopCoroutine (UpdateNavMesh ());
	}
	
	IEnumerator UpdateNavMesh () {
		agent = GetComponent<NavMeshAgent>();
		agentRig = agent.GetComponent<Rigidbody> ();
		mainCamera = Camera.main;

		agentRig.isKinematic = true;

		agent.speed = 3;
		agent.acceleration = 5;

		Init (targets);


		while (true) {
			m_Anim.SetFloat ("Move", agent.velocity.sqrMagnitude);
			agent.SetDestination (targetPos);

			Vector3 offset = targetPos - transform.position;
			float sqrLen = offset.sqrMagnitude;

			if (sqrLen < closeDistance * closeDistance) {
				if (getDest) {
					arriveDest = true;
				}

				if (targetIndex < targets.Length - 1) {
					targetIndex++;
					targetPos = targets [targetIndex].transform.position;
				} else {
					targetPos = dest.transform.position;
					getDest = true;
				}
			}

			if (onCatch) {
				StartCoroutine (CatchAgent ());
			} else {
				onCatch = false;
			}

			if (onCatch && dustImage.isDust) {
				//deathTime = deathTimeInitial;
				gameManager.GetComponent<GameManager> ().CalcScore (gameObject.tag, true);
				parent.SendMessage ("Subtract");
				Destroy (gameObject);
			}

			if (arriveDest) {
				gameManager.GetComponent<GameManager> ().CalcScore (gameObject.tag, false);
				parent.SendMessage ("Subtract");
				Destroy (gameObject);
			}

			yield return null;
		}
	}

	IEnumerator CatchAgent() {
		
		RaycastHit rayHit = new RaycastHit ();
		NavMeshHit navHit = new NavMeshHit ();

		yield return new WaitWhile (() => Input.GetMouseButtonDown(0) == false);
		agent.Stop ();

		yield return new WaitWhile (() => {
			if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
				mainCamera.ScreenPointToRay(Input.mousePosition).direction, out rayHit, 100,
				Physics.DefaultRaycastLayers)){
				agent.transform.localPosition = rayHit.point + Vector3.up * 2;
			}
			return Input.GetMouseButtonUp(0) == false;
		});

		onCatch = false;
		agent.updatePosition = false;
		agentRig.isKinematic = false;

		yield return new WaitWhile (() => {
			return NavMesh.SamplePosition (agent.transform.localPosition, out navHit, 0.1f, NavMesh.AllAreas) == false;
		});

		agent.Resume ();
		agent.Warp (navHit.position);
		agent.updatePosition = true;
		agentRig.isKinematic = true;

	}
}
