using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DrectSoft.FrameWork.WinForm;
using System.Collections.ObjectModel;
using System.Drawing;
using DrectSoft.Resources;
using DrectSoft.FrameWork.WinForm.Plugin;
using System.ComponentModel;
using DrectSoft.Common.Eop;
using DrectSoft.Core;

namespace DrectSoft.FrameWork
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStartPlugIn
    {
        IPlugIn Run(IEmrHost host);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IFrameStartup
    {
        bool Run();
    }

    /// <summary>
    /// �������
    /// </summary>
    public interface IPlugIn
    {
        /// <summary>
        /// Assembly���ļ�����
        /// </summary>
        /// <value></value>
        string AssemblyFileName { get; }

        /// <summary>
        /// �����������
        /// </summary>
        /// <value></value>
        string StartClassType { get; }

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <value>���������Form</value>
        Form MainForm { get; }

        /// <summary>
        ///��ȡ��������Form�Ƿ���MdiChild��ʽ���� 
        /// </summary>
        /// <value></value>
        Boolean IsMdiChild { get; }

        /// <summary>
        /// ��ȡ��������Form�Ƿ���ShowModel��ʽ����
        /// </summary>
        /// <value></value>
        Boolean IsShowModel { get; }

        /// <summary>
        /// ��ȡ��Ҫ��ӵĲ˵�����
        /// </summary>
        /// <value>�˵�����</value>
        //Collection<ToolStripMenuItem> AddInMenuItems { get;}
        Collection<IPluginOwnerMenu> AddInMenuItems { get; }

        /// <summary>
        /// ��ȡ��Ҫ��ӵĹ���������
        /// </summary>
        /// <value>����������</value>
        //Collection<ToolStrip> AddInToolStrips { get;}        
        Collection<IPluginOwnerToolBar> AddInToolStrips { get; }

        /// <summary>
        /// ��ǰ���ʹ�õĸ��������һ����Dock��ʽ���ɵ�StartForm
        /// </summary>
        /// <value></value>
        Collection<IPlugIn> AssistPlugIn { get; }

        /// <summary>
        /// ���ص�ǰ��������в����һ����AssistPlugin����ص�Plugin
        /// </summary>
        /// <value></value>
        Collection<IPlugIn> ByLoadPlugIn { get; }

        /// <summary>
        /// ��ȡ�����ø�plugin��icon
        /// </summary>
        Image Icon { get; set; }

        /// <summary>
        /// ��ȡͼƬ
        /// </summary>
        /// <param name="imageName">ͼƬ����(������׺)�����磺"EMRTitle.bmp"</param>
        /// <returns></returns>
        Image GetImage(string imageName);

        /// <summary>
        /// ��ȡ��ͨ��ͼ�꣨��ico��β�ģ�
        /// </summary>
        /// <param name="iconName">ͼ�����ƣ�������׺��</param>
        /// <returns></returns>
        Image GetNormalIcon(string iconName);

        /// ��ȡС�ߴ�ͼ�꣨16��16��
        /// <param name="imageName">ͼƬ���ƣ�����Ҫָ���ߴ硢���ͺͺ�׺,���磺"Save"</param>
        /// <param name="imageType">ͼƬ����(һ��ͼ�궼��3�ţ�����״̬������״̬������)</param>
        Image GetSmallIcon(string imageName, IconType imageType);

        /// ��ȡ��ߴ�ͼ�꣨24��24��
        /// <param name="imageName">ͼƬ���ƣ�����Ҫָ���ߴ硢���ͺͺ�׺,���磺"Save"</param>
        /// <param name="imageType">ͼƬ���ͣ�һ��ͼ�궼��3�ţ�����״̬������״̬������)</param>
        Image GetMiddleIcon(string imageName, IconType imageType);

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        bool NeedSave();

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>�Ƿ����</returns>
        bool Save();

        /// <summary>
        /// ϵͳ����û��ı��¼�
        /// </summary>
        event UsersChangedHandeler UserChanged;
        /// <summary>
        /// ϵͳ����û��ı��¼�
        /// </summary>
        /// <param name="e"></param>
        void ExecuteUsersChangeEvent(UserArgs e);

        /// <summary>
        /// ѡ��Ĳ��˷����ı�
        /// </summary>
        event PatientChangedHandler PatientChanged;

        /// <summary>
        /// ѡ��Ĳ������ڷ����ı�
        /// </summary>
        event PatientChangingHandler PatientChanging;

        /// <summary>
        /// ִ�в��˸ı��¼�
        /// </summary>
        void ExecutePatientChangeEvent(PatientArgs e);

        /// <summary>
        /// ִ�в������ı��¼�
        /// </summary>
        /// <param name="e"></param>
        void ExecutePatientChangingEvent(CancelEventArgs e);
    }


    /// <summary>
    /// �ṩ���˸ı���¼�
    /// </summary>
    /// <param name="Sender">������</param>
    /// <param name="arg">���˲���</param>
    public delegate void PatientChangedHandler(object Sender, PatientArgs arg);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arg"></param>
    public delegate void UsersChangedHandeler(object sender, UserArgs arg);
    /// <summary>
    /// 
    /// </summary>
    public class UserArgs : EventArgs
    {
        private IUser m_userInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        public UserArgs(IUser users)
        {
            m_userInfo = users;
        }

        /// <summary>
        /// 
        /// </summary>
        public IUser UsersInfo { get { return m_userInfo; } }
    }


    /// <summary>
    /// ���˲���
    /// </summary>
    public class PatientArgs : EventArgs
    {
        private Inpatient m_patinfo;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="patinfo"></param>
        public PatientArgs(Inpatient patinfo)
        {
            m_patinfo = patinfo;
        }

        /// <summary>
        /// �����ҳ���
        /// </summary>
        public Inpatient PatInfo
        {
            get { return m_patinfo; }
        }
    }

    /// <summary>
    /// ���˸ı����¼�
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arg"></param>
    public delegate void PatientChangingHandler(object sender, CancelEventArgs arg);
}
