using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UserInfoMgr.instance.InitData();

		UserInfoBean uib = UserInfoMgr.instance.GetDataById(1);
		Debug.Log(uib.Name);

		UserInfoMgr.instance.Test();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
