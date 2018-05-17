using System.Collections.ObjectModel;
using System.Data;
using DrectSoft.Common.Eop;
using DrectSoft.Core;
using DrectSoft.FrameWork.Plugin.Manager;

namespace DrectSoft.FrameWork.WinForm.Plugin
{
    /// <summary>
    /// Ӧ�ó������ӿ�
    /// </summary>
    public interface IEmrHost
    {
        /// <summary>
        /// ����û�����
        /// </summary>
        /// <value></value>
        IUser User { get; }

        /// <summary>
        /// �����ƶ��������
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="startupClassName"></param>
        /// <returns></returns>
        bool LoadPlugIn(string assemblyName, string startupClassName);

        /// <summary>
        /// �����ƶ��������
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        bool LoadPlugIn(string typeName);

        /// <summary>
        /// ���ݲ�����ҳ��ż��ز�����Ϣ
        /// </summary>
        /// <param name="firstPageNo">������ҳ���</param>
        void ChoosePatient(decimal firstPageNo);

        /// <summary>
        /// ���ݲ�����ż��ز��˲���ȡ��Ϣ
        /// </summary>
        /// <param name="firstPageNo">������ҳ���</param>
        /// <param name="MyInp">������Ϣ</param>
        void ChoosePatient(string firstPageNo, out Inpatient MyInp);

        /// <summary>
        /// ���ݲ�����ҳ��ź�״̬���ز�����Ϣ
        /// </summary>
        /// <param name="firstPageNo">������ҳ���</param>
        /// <param name="FloderState">����״̬</param>
        void ChoosePatient(decimal firstPageNo, string FloderState);


        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        Inpatient CurrentPatientInfo { get; set; }

        /// <summary>
        /// �µĵ�ǰ������Ϣ
        /// </summary>
        Inpatient NEWCurrentPatientInfo { get; set; }

        /// <summary>
        /// ��ǰҽԺ�û���Ϣ
        /// </summary>
        HospitalInfo CurrentHospitalInfo { get; }

        /// <summary>
        /// ����Զ�����Ϣ��ʾ�Ķ���ӿڣ���MessageBox��һ���װ����Ϣ��ʾʱ��ֱ��ʹ��MessageBox����ʹ�ø÷�װ�Ķ���
        /// </summary>
        ICustomMessageBox CustomMessageBox { get; }

        /// <summary>
        /// ��÷������ݿ�ĸ�������
        /// </summary>
        IDataAccess SqlHelper { get; }


        /// <summary>
        /// ����Mac��ַ
        /// </summary>
        string MacAddress { get; }


        /// <summary>
        /// ��ȡ���ýӿ�
        /// </summary>
        IAppConfigReader AppConfig { get; }

        /// <summary>
        /// ���������Ϣ���ݼ������ݱ�������ʹ�á�
        /// ����:Inpatients[��ǰ���Ҳ����Ĳ�������]Beds[��ǰ���Ҳ����Ĵ�λ��Ϣ]
        /// </summary>
        DataSet PatientInfos { get; }

        /// <summary>
        /// ˢ�²�����Ϣ���ݼ�
        /// </summary>
        /// <returns>�ɹ�����  ʧ�ܣ�ʧ��ԭ��</returns>
        string RefreshPatientInfos();

        /// <summary>
        /// ��ǰ�ʺ���Ȩ�޵�MENU
        /// </summary>
        Collection<PlugInConfiguration> PrivilegeMenu { get; }

        /// <summary>
        /// ��ǰ���������
        /// </summary>
        DrectSoft.FrameWork.Plugin.PluginManager Manager { get; }

        /// <summary>
        /// �Ƿ���ֻ����ʽ�򿪲���
        /// </summary>
        bool EmrAllowEdit { get; set; }

        /// <summary>
        /// ��������õĹ�������
        /// </summary>
        PluginUtil PublicMethod { get; }

        /// <summary>
        /// �����༭��Ĭ������
        /// </summary>
        EmrDefaultSetting EmrDefaultSettings
        { get; }
        /// <summary>
        /// ��־������
        /// </summary>
        DrectSoftLog Logger { get; }

        /// <summary>
        /// ��ǰѡ�еĲ���ID�����ⲿ���������¼���벡����д�Ľ���
        /// </summary>
        string CurrentSelectedEmrID { get; set; }

        /// <summary>
        /// ����Ȩ��״̬
        /// </summary>
        string FloderState { get; set; }

        /// <summary>
        /// �����½Ƕ�̬����ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="dt">��ӵ�����</param>
        /// <param name="isClear">�Ƿ����ԭ��������</param>
        void ShowMessageWindow(DataTable dt, bool isClear);

    }
}
