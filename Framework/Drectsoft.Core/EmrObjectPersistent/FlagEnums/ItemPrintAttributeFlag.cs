using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// ��Ŀ��ӡ����
   /// </summary>
   [Flags]
   public enum ItemPrintAttributeFlag
   {
      /// <summary>
      /// ����ʾƵ��
      /// </summary>
      NotShowFrequency = 1,
      /// <summary>
      /// ����ʾ����
      /// </summary>
      NotShowAmount = 2
   }
}
