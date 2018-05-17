using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.BizBus.Service;

namespace DrectSoft.FrameWork.Config
{
    /// <summary>
    /// ����URI������
    /// </summary>
   public class UriUtil
   {
       /// <summary>
       /// ��ȡ����URI��ַ
       /// </summary>
       /// <param name="baseuripath"></param>
       /// <param name="packageinfo"></param>
       /// <param name="desc"></param>
       /// <returns></returns>
      public static string GetServiceUri(string baseuripath, ServicePackageInfo packageinfo, ServiceDesc desc)
      {
         //�����URI��ַ������URI·��+��������ƣ�����ؼ���
         return baseuripath + packageinfo.Name + "/" + desc.Key;
      }

       /// <summary>
       /// ��ȡָ������URI
       /// </summary>
       /// <param name="baseuripath"></param>
       /// <param name="packagename"></param>
       /// <param name="servicekey"></param>
       /// <returns></returns>
      public static string GetServiceUri(string baseuripath, string packagename, string servicekey)
      {
         return baseuripath + packagename +"/"+ servicekey;
      }
   }
}
