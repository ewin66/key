using System;
using System.Text;
using CommonLib;
namespace MedicalRecordManage.Object
{
/// <summary>
/// ����������
/// �� �� �ߣ�Roger
/// �������ڣ�2013-03-07 10:24
/// </summary>
[Serializable, BindTable("EMR_RecordBorrow", ChName="�������ļ�¼��")]
public class EMR_RecordBorrow
{
	private string _ID;
	///<summary>
	///Id,Id
	///</summary>
	[BindField("ID", ChName = "Id", DefaultValue = "SYS_GUID()", DbType = "VarChar", Key = true)]
	public string ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			this._ID = value;
		}
	}


	private string _NOOFINPAT="";
	///<summary>
	///��������
	///</summary>
	[BindField("NOOFINPAT", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string NOOFINPAT
	{
		get
		{
			return this._NOOFINPAT;
		}
		set
		{
			this._NOOFINPAT = value;
		}
	}


	private DateTime _APPLYDATE;
	///<summary>
	///����ʱ��
	///</summary>
	[BindField("APPLYDATE", ChName = "", DefaultValue = "SysDate", DbType = "DateTime", Key = false)]
	public DateTime APPLYDATE
	{
		get
		{
			return this._APPLYDATE;
		}
		set
		{
			this._APPLYDATE = value;
		}
	}


	private string _APPLYDOCID="";
	///<summary>
	///����ҽ��Id
	///</summary>
	[BindField("APPLYDOCID", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string APPLYDOCID
	{
		get
		{
			return this._APPLYDOCID;
		}
		set
		{
			this._APPLYDOCID = value;
		}
	}


	private string _APPLYCONTENT="";
	///<summary>
	///��������
	///</summary>
	[BindField("APPLYCONTENT", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string APPLYCONTENT
	{
		get
		{
			return this._APPLYCONTENT;
		}
		set
		{
			this._APPLYCONTENT = value;
		}
	}


	private int _APPLYTIMES;
	///<summary>
	///��������
	///</summary>
	[BindField("APPLYTIMES", ChName = "", DefaultValue = "0", DbType = "Int16", Key = false)]
	public int APPLYTIMES
	{
		get
		{
			return this._APPLYTIMES;
		}
		set
		{
			this._APPLYTIMES = value;
		}
	}


	private string _APPROVEDOCID="";
	///<summary>
	///�����
	///</summary>
	[BindField("APPROVEDOCID", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string APPROVEDOCID
	{
		get
		{
			return this._APPROVEDOCID;
		}
		set
		{
			this._APPROVEDOCID = value;
		}
	}


	private DateTime _APPROVEDATE;
	///<summary>
	///���ʱ��
	///</summary>
	[BindField("APPROVEDATE", ChName = "", DefaultValue = "SysDate", DbType = "DateTime", Key = false)]
	public DateTime APPROVEDATE
	{
		get
		{
			return this._APPROVEDATE;
		}
		set
		{
			this._APPROVEDATE = value;
		}
	}


	private string _APPROVECONTENT="";
	///<summary>
	///��˲�ͨ������
	///</summary>
	[BindField("APPROVECONTENT", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string APPROVECONTENT
	{
		get
		{
			return this._APPROVECONTENT;
		}
		set
		{
			this._APPROVECONTENT = value;
		}
	}


	private int _STATUS;
	///<summary>
	///״̬
	///</summary>
	[BindField("STATUS", ChName = "", DefaultValue = "0", DbType = "Int16", Key = false)]
	public int STATUS
	{
		get
		{
			return this._STATUS;
		}
		set
		{
			this._STATUS = value;
		}
	}


	private string _YANQIFLAG="";
	///<summary>
	///���ڱ�ʶ
	///</summary>
	[BindField("YANQIFLAG", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string YANQIFLAG
	{
		get
		{
			return this._YANQIFLAG;
		}
		set
		{
			this._YANQIFLAG = value;
		}
	}


	private string _APPLYTABEID="";
	///<summary>
	///��������ID
	///</summary>
	[BindField("APPLYTABEID", ChName = "", DefaultValue = "''", DbType = "VarChar", Key = false)]
	public string APPLYTABEID
	{
		get
		{
			return this._APPLYTABEID;
		}
		set
		{
			this._APPLYTABEID = value;
		}
	}



}
}
