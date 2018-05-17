using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DrectSoft.FrameWork.WinForm.Plugin;

namespace DrectSoft.Core
{
   /// <summary>
   /// ϵͳ���ýӿ�
   /// </summary>
   public interface IAppConfigDesign
   {
      /// <summary>
      /// ���ý���ؼ�
      /// </summary>
      Control DesignUI { get;}

      /// <summary>
      /// �������ü���
      /// </summary>
      /// <param name="app"></param>
      /// <param name="configs"></param>
      void Load(IEmrHost app, Dictionary<string, EmrAppConfig> configs);

      /// <summary>
      /// �ӿ��ڱ�����ĵ����õ�ChangedConfigs
      /// ����ӿ��ڼ�ʱ����ChangedConfigs,�˷�������ʵ��(��Ҫ�׳�δʵ���쳣)
      /// </summary>
      void Save();

      /// <summary>
      /// �������ü���
      /// </summary>
      Dictionary<string, EmrAppConfig> ChangedConfigs { get;}

      /// <summary>
      /// ���ö���(����з���,û����null)
      /// </summary>
      object ConfigObj { get;}
   }

}

