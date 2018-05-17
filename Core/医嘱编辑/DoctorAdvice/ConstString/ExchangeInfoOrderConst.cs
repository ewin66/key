using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// �й�ҽ����Ϣ����ľ�̬�ַ���
   /// </summary>
   public static class ExchangeInfoOrderConst
   {
      /// <summary>
      /// EMRϵͳ����
      /// </summary>
      public const string EmrSystemName = "NetWorkStudio-EMR-1.0";
      /// <summary>
      /// HISϵͳ����
      /// </summary>
      public const string HisSystemName = "NetWorkStudio-HIS-4.0";
      /// <summary>
      /// ��Ϣ������HIS�м��ҽ��
      /// </summary>
      public const string MsgCheckData = "CHECKADVISES2HIS";
      /// <summary>
      /// ��Ϣ��������ҽ����HIS
      /// </summary>
      public const string MsgSaveData = "SAVEADVISES2HIS";
      /// <summary>
      /// ��Ϣ������ȡ������չ��Ϣ
      /// </summary>
      public const string MsgGetExtraInfo = "GETPATEXTRAINFO";
      /// <summary>
      /// ��Ϣ�������²�����Ϣ
      /// </summary>
      public const string MsgUpdatePatient = "UPDATEPATINFO";
      /// <summary>
      /// ��Ϣ������ȡҽ����ӡ��������
      /// </summary>
      public const string MsgGetPrintSettings = "GETORDERPRINTSETTINGS";
      /// <summary>
      /// ��Ϣ��������ҽ����ӡ����ѯ�͸��£�
      /// </summary>
      public const string MsgProcessOrderPrint = "PROCESSORDERPRINT";
      /// <summary>
      /// ��Ϣ������ȡ��������
      /// </summary>
      public const string MsgGetRecipeRules = "GETRECIPERULES";
      /// <summary>
      /// ��Ϣ������HIS��ȡҽ������
      /// </summary>
      public const string MsgGetHisOrder = "GETHISORDER";
      /// <summary>
      /// Ĭ�ϵ���Ϣ��������
      /// </summary>
      public const string EncodingName = "utf-8";

      /// <summary>
      /// Ĭ��ʹ�õı����ʽ
      /// </summary>
      public static Encoding DefaultEncoding = Encoding.GetEncoding(EncodingName);
   }
}
