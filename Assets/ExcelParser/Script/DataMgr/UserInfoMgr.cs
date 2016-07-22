using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public class UserInfoMgr : DataMgrBase {


	protected override string GetXlsxPath ()
	{
		return "UserInfo";
	}


	protected override System.Type GetBeanType ()
	{
		return typeof(UserInfoBean);
	}


	public UserInfoBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (UserInfoBean)dataBean;
		}else{
			return null;
		}
	}



}
