
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace DrectSoft.Common.Library
{
   #region ShowListForm's mode
   /// <summary>
   /// ShowListForm����ʾģʽ
   /// </summary>
   [CLSCompliantAttribute(true)]
   public enum ShowListFormMode
   {
      /// <summary>
      /// ������ʾ
      /// </summary>
      Full, 
      /// <summary>
      /// ���ģʽ
      /// </summary>
      Concision
   }
   #endregion

   #region enum match type
   /// <summary>
   /// ShowList����ƥ�����ݵ�ģʽö��
   /// </summary>
   [CLSCompliantAttribute(true)]
   public enum ShowListMatchType
   {
      /// <summary>
      /// ��ȫƥ��
      /// </summary>
      Full,  
      /// <summary>
      /// ǰ������
      /// </summary>
      Begin, 
      /// <summary>
      /// ģ����ѯ
      /// </summary>
      Any 
   }
   #endregion

   #region enum 
   /// <summary>
   /// ShowListѡ�񴰿ڵĵ���ģʽ
   /// </summary>
   public enum ShowListCallType
   { 
      /// <summary>
      /// ���Ի�ShowListBox
      /// </summary>
      Initialize,  
      /// <summary>
      /// ��ͨ����ģʽ
      /// </summary>
      Normal,  
      /// <summary>
      /// ֱ����ʾShowListWindowģʽ
      /// </summary>
      ShowDirectly 
   }
   #endregion
}
