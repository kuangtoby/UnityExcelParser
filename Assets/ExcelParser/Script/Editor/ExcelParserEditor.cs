using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using Excel;

namespace ExcelParser{

	public class TitleData{
		public string name;
		public string type;

	}


	public class ExcelParserEditor : Editor {

		public static void GenerateClass(string fileName,List<TitleData> titles)
		{
//			string fileName = "FileName";


			string targetPath = Application.dataPath+"/ExcelParser/Script/DataBeans/";
			string file = targetPath+fileName+"Bean.cs";

			if(!Directory.Exists(targetPath))
			{
				Debug.LogError("no path");
				return;

			}


			FileStream fileStream = new FileStream(file,FileMode.OpenOrCreate);

			StreamWriter outfile =  new StreamWriter(fileStream);


			outfile.WriteLine("using UnityEngine;");
			outfile.WriteLine("using System.Collections;");
			outfile.WriteLine("using ExcelParser;");
			outfile.WriteLine("");
			outfile.WriteLine("public class "+fileName+"Bean : IDataBean {");
			outfile.WriteLine(" ");
			outfile.WriteLine(" ");

			for (int i = 0; i < titles.Count; i++) {
				TitleData td = titles[i];
				string block = GeneratePropertyBlock(td);
				outfile.WriteLine(block);
				outfile.WriteLine(" ");
			}



//			outfile.WriteLine(" // Use this for initialization");
//			outfile.WriteLine(" void Start () {");
//			outfile.WriteLine(" ");
//			outfile.WriteLine(" }");
//			outfile.WriteLine(" ");         
//			outfile.WriteLine(" ");
//			outfile.WriteLine(" // Update is called once per frame");
//			outfile.WriteLine(" void Update () {");
//			outfile.WriteLine(" ");
//			outfile.WriteLine(" }");
			outfile.WriteLine("}");

            
			outfile.Close();
			fileStream.Close();



			GenerateMgrClass(fileName);
			AssetDatabase.Refresh();

		}

		public static void GenerateMgrClass(string fileName)
		{
			string targetPath = Application.dataPath+"/ExcelParser/Script/DataMgr/";
			string file = targetPath+fileName+"Mgr.cs";
			
			if(!Directory.Exists(targetPath))
			{
				Debug.LogError("no path");
				return;
				
			}
			
			
			FileStream fileStream = new FileStream(file,FileMode.OpenOrCreate);


			string templetePath = Application.dataPath+"/ExcelParser/Templete/MgrTemplete.txt";
			Debug.Log(templetePath);

//			FileStream templeteFileStream = new FileStream(templetePath,FileMode.OpenOrCreate);

//			TextAsset templete = (TextAsset)AssetDatabase.LoadAssetAtPath(templetePath,typeof(TextAsset))  ;

//			string classText = templete.ToString();

			string classText = File.ReadAllText(templetePath);

			classText = classText.Replace("{0}",fileName);


			StreamWriter outfile =  new StreamWriter(fileStream);

			outfile.Write(classText);
			outfile.Close();
			fileStream.Close();
			
			
			AssetDatabase.Refresh();

			Debug.Log("genereate mgr class success!");
		}


		[MenuItem("Assets/ExcelParser/Add C# Class from xlsx txt")]
		public static void ParseExcel(){
			var objs = Selection.objects;

			for (int i = 0; i < objs.Length; i++) {
				var obj = objs[i];
				if(obj is TextAsset){
					string context = obj.ToString();
					
					string fileName = obj.name.ToString();
					
					
					List<TitleData> titleDataList = LoadTxtData(context);
                    
                    GenerateClass(fileName,titleDataList);
					
				}
			}


		}


		public static List<TitleData> LoadTxtData(string dataTxt)
		{

			List<TitleData> titleDataList = new List<TitleData>();

			dataTxt = dataTxt.Replace("\r", "");
			dataTxt = dataTxt.Replace(" ", "");
			dataTxt = dataTxt.Replace(" ", "");
			string[] hList = dataTxt.Split('\n');


			string title = hList[2];
			string[] titles = title.Split('\t');
			string[] types = hList[0].Split('\t');
			//Debug.Log(hList.Length + "<<<hList.Length");
//			for (int i = 0; i < 3; i++)
//			{
//				if(i == 0||i==2){
//					string[] line = hList[i].Split('\t');
//					Dictionary<string, string> lineKeyValue = new Dictionary<string, string>();
//					for (int j = 0; j < line.Length; j++)
//					{
//
//
//					}
//				}
//				
//
//			}


			for(int i = 0;i<titles.Length;i++)
			{
				TitleData titleData = new TitleData();

				if(string.IsNullOrEmpty(titles[i]))
				{
					continue;
				}

				titleData.name = titles[i];

				string typeStr = types[i].ToLower();


				if(typeStr == "string" || typeStr == "int" || typeStr == "float")
				{
					titleData.type = typeStr.ToLower();
				}

				titleDataList.Add(titleData);

			}



			return titleDataList;

			
		}

		static string GeneratePropertyBlock(TitleData tileData){
			string propertyBlock = @"
	private {0} {2};
	public {0} {1} {
		get {
			return {2};
		}
		set {
			{2} = value;
		}
	}";

			string name = tileData.name;
			string bigName = name.Substring(0,1).ToUpper()+name.Substring(1);
//			Debug.Log(bigName);

//			string result = propertyBlock;
			propertyBlock = propertyBlock.Replace("{0}",tileData.type.ToString());
			propertyBlock = propertyBlock.Replace("{1}",bigName);
			propertyBlock = propertyBlock.Replace("{2}",name);


//			string result = string.Format(propertyBlock,tileData.type.ToString(),bigName,name);

//			Debug.Log(propertyBlock);

			return propertyBlock;
		}

		[MenuItem("Assets/ExcelParser/XlsxToTxt")]
		public static void XlsxToTxt()
		{
			var objs = Selection.objects;

			for (int i = 0; i < objs.Length; i++) {

				string path = AssetDatabase.GetAssetPath(objs[i]);

				if(path.EndsWith(".xlsx"))
				{
					string fileName = objs[i].name;

					string targetFile = path.Replace(".xlsx",".txt");

					int lastI = targetFile.LastIndexOf('/');
					targetFile = targetFile.Insert(lastI+1,"_txt/Resources/");

					string direct = Path.GetDirectoryName(targetFile);
					if(!Directory.Exists(direct)){
						Directory.CreateDirectory(direct);
					}



//					Debug.Log(targetFile);
//					return;

					FileStream targetFileStream = new FileStream(targetFile,FileMode.OpenOrCreate);


					FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
					IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

					DataSet result = excelReader.AsDataSet();
					int columns = result.Tables[0].Columns.Count;
					int rows = result.Tables[0].Rows.Count;

//					string xml = result.GetXml();
//					Debug.Log(xml);

					StringBuilder txtBuilder = new StringBuilder();

//						string col = "";
					for (int r = 0; r < rows; r++) {
						for (int c = 0; c < columns; c++) {
							txtBuilder.Append(result.Tables[0].Rows[r][c].ToString()).Append("\t");
//							col+=result.Tables[0].Rows[r][c].ToString()+"\t";

						}
//						col+="/n";
						txtBuilder.Append("\n");
					}

//					Debug.Log(txtBuilder);


					StreamWriter steamWriter = new StreamWriter(targetFileStream);

					steamWriter.Write(txtBuilder.ToString());



					steamWriter.Close();
					stream.Close();
					targetFileStream.Close();
				}
			}

			AssetDatabase.Refresh();
//			object xlsxs = selection
		}





	}





}
