# UnityExcelParser
Parse xlsx to txt and generate class file in Unity.

Xlsx need to be in a specific format:
Row 1 is for property type,need be int,string or float.
Row 2 is for comments.
Row 3 is for property name.

how to generate:
right click xxx.xlsx file in Project window,click "ExcelParser->GenerateClass from excel",
a xxx.txt,xxxBean.cs,xxxMgr.cs will be generated.

xxxBean.cs is for properties,
xxxMgr.cs is for init data from xlsx.


支持在Unity中将xlsx文件转成txt文件，并生成对应的类文件。

xlsx需要按特定的格式：
第一行为类型，必须为int,string,float。
第二行为注释。
第三行为属性名称，与生成的类属性对应。

转换方法：
在Project种右击xxx.xlsx文件，选择ExcelParser->GenerateClass from excel,就会生成对应的txt文件xxx.txt，xxxBean.cs文件,xxxMgr.cs文件。

其中xxxBean.cs为数据结构类，与xlsx属性对应
xxxMgr.cs为管理类，用于初始化数据和取数据


