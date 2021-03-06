using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// 药品性质(以二进制位表示)
   /// </summary>
   [FlagsAttribute]
   public enum DruggeryAttributeFlag
   {
      /// <summary>
      /// 皮试(有时间限制)
      /// </summary>
      SkinTestLimited = 1,
      /// <summary>
      /// 皮试(永久有效)
      /// </summary>
      SkinTestUnlimited = 2,
      /// <summary>
      /// 贵重药品
      /// </summary>
      Valuable = 4,
      /// <summary>
      /// OTC标志
      /// </summary>
      IsOTC = 8
   }
}
