using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DustImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Image dustImg {get {return GetComponent<Image> ();}}
	public bool isDust = false;

	private Color tempColor;

	public void OnPointerEnter(PointerEventData eventData){
		tempColor = dustImg.color;
		dustImg.color = Color.red;
		isDust = true;
	}
	public void OnPointerExit(PointerEventData eventData){
		dustImg.color = tempColor;
		isDust = false;
	}
}
