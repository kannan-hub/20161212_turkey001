using UnityEngine;
using System.Collections;

public class Navgate : MonoBehaviour {

	public GameObject[] targets;
	private int targetIndex;

	float closeDistance = 1.0f;

	private Vector3 targetPos;

	NavMeshAgent agent;

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
		agent = GetComponent<NavMeshAgent>();

		agent.speed = 3;
		agent.acceleration = 5;

		Init (targets);
	}
	
	// Update is called once per frame
	void Update () {
		m_Anim.SetFloat ("Move", agent.velocity.sqrMagnitude);
		agent.SetDestination (targetPos);

		Vector3 offset = targetPos - transform.position;
		float sqrLen = offset.sqrMagnitude;

		if (sqrLen < closeDistance * closeDistance) {
			if (targetIndex < targets.Length - 1) {
				targetIndex++;
				targetPos = targets [targetIndex].transform.position;
			} else {
				targetIndex = 0;
				targetPos = targets [targetIndex].transform.position;
			}
		}
	}
}
