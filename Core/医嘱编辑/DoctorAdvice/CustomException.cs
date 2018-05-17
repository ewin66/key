using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// �Զ������ݼ���쳣
   /// </summary>
   public class DataCheckException : Exception
   {
      /// <summary>
      /// �ⲿ�������ñ�У�����ݵ��к�
      /// </summary>
      public int RowIndex
      {
         get { return _rowIndex; }
         set { _rowIndex = value; }
      }
      private int _rowIndex;

      /// <summary>
      /// �ⲿ�����������汻У��ҽ�������
      /// </summary>
      public decimal OrderSerialNo
      {
         get { return _orderSerialNo; }
         set { _orderSerialNo = value; }
      }
      private decimal _orderSerialNo;

      /// <summary>
      /// ��ʾ������������ݵ�����
      /// </summary>
      public string DataName
      {
         get 
         {
            if (_dataName == null)
               return "";
            else
               return _dataName;
         }
      }
      private string _dataName;

      /// <summary>
      /// ���漶��0������  1������
      /// </summary>
      public int WarnningLevel
      {
         get
         {
            return _warnningLevel;
         }
      }
      private int _warnningLevel;

      public DataCheckException()
      {
         _warnningLevel = 1;
      }

      public DataCheckException(string message, string dataName)
         : base(message)
      {
         _dataName = dataName;
         _warnningLevel = 1;
      }

      public DataCheckException(string message, string dataName, int warnningLevel)
         : base(message)
      {
         _dataName = dataName;
         _warnningLevel = warnningLevel;
      }

      public DataCheckException(string message, string dataName, int warnningLevel, Exception inner)
         : base(message, inner)
      {
         _dataName = dataName;
         _warnningLevel = warnningLevel;
      }
   }

   /// <summary>
   /// �Զ������remoting�����쳣
   /// </summary>
   public class CallRemotingException : Exception
   {
      public CallRemotingException() : base ("")
      { }

      public CallRemotingException(string message): base(message)
      { }
   }
}
