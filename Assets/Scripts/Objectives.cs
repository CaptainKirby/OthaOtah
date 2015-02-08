using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Objectives : MonoBehaviour {
	[HideInInspector]
	public bool getDrink;
	[HideInInspector]
	public bool deliverDrink;
	public GameObject arrow;
	public GameObject barLocation;
	public bool spawnDrinkArrow;
	public bool spawnDeliverArrow;
	private GameObject drinkArrow;
	private GameObject deliverArrow;

	public List<GameObject> clients;
	public int curClient;
	public GameObject curClientObj;
	public bool end;
	public GUIText guiText1;
	public GUIText guiText2;
	private GameObject dirLight;

	private BottleManager bMngr;

	public GameObject drinksSpeech;
	public GameObject needPowerSpeech;
	void Start () 
	{
		bMngr = GetComponent<BottleManager>();
		dirLight = GameObject.Find("Directional light");
		barLocation = GameObject.FindGameObjectWithTag("pickup");
		getDrink = true;
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("client"))
		{
			clients.Add(g);
		}

		StartCoroutine(Speech(drinksSpeech));
	}
	
	void Update () 
	{
		if(end)
		{
			StartCoroutine(End());
		}
		if(getDrink && !spawnDrinkArrow && !end)
		{
//			Debug.Log ("WAT");
			spawnDrinkArrow = true;
			drinkArrow = Instantiate(arrow, barLocation.transform.position, barLocation.transform.rotation) as GameObject;
			if(drinkArrow.activeSelf == false)
			{
				drinkArrow.SetActive(true);
			}
			if(deliverArrow != null)
			{
				deliverArrow.SetActive(false);
			}
		}

		if(!getDrink && !deliverDrink)
		{

			drinkArrow.SetActive(false);
			deliverDrink= true;
//			Debug.Log("Ghhhhh");

						spawnDeliverArrow = false;
		}

		if(deliverDrink && !spawnDeliverArrow)
		{
			Debug.Log ("WAT2");

			spawnDeliverArrow = true;
			if(deliverArrow == null)
			{
				deliverArrow = Instantiate(arrow, new Vector3(clients[curClient].transform.position.x,clients[curClient].transform.position.y+4,clients[curClient].transform.position.z) , Quaternion.identity) as GameObject;
			}
			if(deliverArrow.activeSelf == false)
			{

				deliverArrow.SetActive(true);
				deliverArrow.transform.position = new Vector3(clients[curClient].transform.position.x,clients[curClient].transform.position.y+4,clients[curClient].transform.position.z) ;
			}
			curClientObj = clients[curClient].gameObject;
//			curClient ++;
		}

//		if(!deliverDrink && getDrink && spawnDrinkArrow)
//		{
//			spawnDrinkArrow = false;
//		}

	}
	IEnumerator Speech(GameObject obj)
	{
		yield return new WaitForSeconds(1);
		GameObject sB = Instantiate(obj, new Vector3(this.transform.position.x, this.transform.position.y + 3.5f, this.transform.position.z), Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(4);
		sB.SetActive(false);

	}
	IEnumerator End()
	{
		dirLight.GetComponent<Light>().color = Color.black;
		RenderSettings.ambientLight = Color.black;
		yield return new WaitForSeconds(2);
		GUIText g1 = Instantiate(guiText1, guiText1.transform.position, guiText1.transform.rotation) as GUIText;
		GUIText g2 = Instantiate(guiText2, guiText2.transform.position, guiText2.transform.rotation) as GUIText;
		g2.text = bMngr.successDeliveries.ToString() + " out of 9";

		yield return new WaitForSeconds(2);
		Application.LoadLevel(Application.loadedLevel);


	}

}
