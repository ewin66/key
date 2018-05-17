using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.Plugin.Manager;
using System.Collections.ObjectModel;

namespace DrectSoft.FrameWork.Plugin
{
    //public class MenuPluginInfo:PluginInfo
    //{

    //}

    /// <summary>
    /// ����˵��ӿ�
    /// </summary>
    public interface IPlugInMenuInfo
    {
       /// <summary>
       /// �����Ϣ
       /// </summary>
       PlugInConfiguration MenuInfo { get;}

       /// <summary>
       /// �Ӳ˵�
       /// </summary>
       Collection<IPlugInMenuInfo> SubItems { get;}

       /// <summary>
       /// ��ʾ
       /// </summary>
       string Text { get;set;}

       /// <summary>
       /// ����(��λ)
       /// </summary>
       string Name { get;}
    }
}
