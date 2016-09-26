# UnityExcelParser
Parse xlsx to txt and generate class file in Unity.

xlsx file need to be in a specific format:
Row 1 is for property type,need be int,string or float.
Row 2 is for comments.
Row 3 is for property name.

How to generate or modifiy class for xlsx:
right click xxx.xlsx file in Project window,click "ExcelParser->GenerateClass from excel",
a xxx.txt,xxxBean.cs,xxxMgr.cs will be generated.

xxxBean.cs is for properties,
xxxMgr.cs is for get data from xlsx.

How to get data from xlsx:
xxxMgr.instance.InitData();//run at the first time
xxxMgr.instance.GetDataById(key);//get data according key


How to define custom function in xxxMgr class:

public  partial class  UserInfoMgr{
	//add custom funtions
}


You can find example code in Example.cs.



