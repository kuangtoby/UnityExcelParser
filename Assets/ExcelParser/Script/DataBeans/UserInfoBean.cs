using UnityEngine;
using System.Collections;
using ExcelParser;

public class UserInfoBean : IDataBean {
 
 

	private int id;
	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
 

	private string name;
	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}
 

	private int sex;
	public int Sex {
		get {
			return sex;
		}
		set {
			sex = value;
		}
	}
 
}
