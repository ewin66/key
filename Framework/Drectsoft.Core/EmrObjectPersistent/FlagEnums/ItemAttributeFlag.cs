using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��Ŀ����(�Զ�����λ��ʾ)
   /// </summary>
   [Flags]
   public enum ItemAttributeFlag
   {
      /// <summary>
      /// ������Ŀ
      /// </summary>
      GeneralItem = 1,
      /// <summary>
      /// ����ҽ��
      /// </summary>
      TextOrder = 2
   }
}
