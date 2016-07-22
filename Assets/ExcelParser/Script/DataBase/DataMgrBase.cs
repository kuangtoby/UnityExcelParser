using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace ExcelParser
{
	public class DataMgrBase
	{


		public Dictionary<object,IDataBean> idDataDic = new Dictionary<object, IDataBean> ();


		/// <summary>
		/// Inits the data.
		/// </summary>
		public void InitData ()
		{


			Type dataBeanType = GetBeanType ();

			Debug.Log (GetXlsxPath ());

			TextAsset txt = Resources.Load (GetXlsxPath ()) as TextAsset;

			string dataTxt = txt.ToString ();

//			Debug.Log(dataTxt);


			dataTxt = dataTxt.Replace ("\r", "");
			dataTxt = dataTxt.Replace (" ", "");
			dataTxt = dataTxt.Replace (" ", "");
			string[] hList = dataTxt.Split ('\n');
			
			
			string title = hList [2];
			string[] titles = title.Split ('\t');
			string[] types = hList [0].Split ('\t');



			for (int col = 3; col < hList.Length; col++) {
				IDataBean dataBean = null;
				object key = null;

				string[] vals = hList [col].Split ('\t');

				if (vals.Length != titles.Length) {
					continue;
				}



				dataBean = (IDataBean)Activator.CreateInstance (dataBeanType);
				for (int row = 0; row < titles.Length; row++) {

					string titleName = titles [row];

					if (string.IsNullOrEmpty (titleName)) {
						continue;
					}

					string typeStr = types [row];
					string valStr = vals [row];

//					Debug.Log(valStr);

					if (string.IsNullOrEmpty (typeStr)) {
						continue;
					}


//					object val = null;






					string propertyName = titleName.Substring (0, 1).ToUpper () + titleName.Substring (1);

					PropertyInfo prop = dataBeanType.GetProperty (propertyName, BindingFlags.Public | BindingFlags.Instance);

					object val = Convert.ChangeType (valStr, prop.PropertyType);

//					Debug.Log("val:"+val+"      pN:"+propertyName);
					prop.SetValue (dataBean, val, null);

					//set dictionary id
					if (row == 0) {
						key = val;
					}

				}

				if (dataBean != null) {
					idDataDic.Add (key, dataBean);
				}
			}

		}


		/// <summary>
		/// Gets the xlsx txt path. Need overwrite.
		/// </summary>
		/// <returns>The xlsx path.</returns>
		protected virtual string GetXlsxPath ()
		{
			return "";
		}


		/// <summary>
		/// Gets the type of the bean.Need overwrite.
		/// </summary>
		/// <returns>The bean type.</returns>
		protected virtual Type GetBeanType ()
		{
			return null;
		}

		public IDataBean _GetDataById (object id)
		{
			if (idDataDic.ContainsKey (id)) {
				return idDataDic [id];
			} else {
				return null;
			}
		}

	}
}
