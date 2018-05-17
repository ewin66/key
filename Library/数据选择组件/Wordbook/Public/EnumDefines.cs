/*****************************************************************************\
**                                **
**                                                                           **
**             �����ֵ�����Ҫʹ�õ�ö���Ͷ���                                  **
**                                                                           **
**                                                                           **
\*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace DrectSoft.Wordbook
{
   #region enum type of string's characters
   /// <summary>
   /// �ַ������ַ�����ö��
   /// </summary>
   [CLSCompliantAttribute(true)]
   public enum StringType
   {
      /// <summary>
      /// �մ�
      /// </summary>
      Empty,
      /// <summary>
      /// ȫ����Ӣ����ĸ
      /// </summary>
      EnglishChar,
      /// <summary>
      /// ȫ�����������
      /// </summary>
      Numeric,
      /// <summary>
      /// ��ASCII�е���ĸ���������
      /// </summary>
      Char,
      /// <summary>
      /// �������֡�ȫ����ĸ����������
      /// </summary>
      Other
   }
   #endregion

   #region Wordbook Kind
   /// <summary>
   /// �ֵ������
   /// </summary>
   [CLSCompliantAttribute(true)]
   public enum WordbookKind
   {
      /// <summary>
      /// ��ͨ��Ԥ������ֵ���
      /// </summary>
      Normal,
      /// <summary>
      /// ����SQL������ɵ��ֵ���
      /// </summary>
      Sql,
      /// <summary>
      /// ����StringList���ɵ��ֵ���
      /// </summary>
      List
   }
   #endregion

   #region CompareOperator
   /// <summary>
   /// �Ƚ����������ö����
   /// </summary>
   [SerializableAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   [CLSCompliantAttribute(true)]
   public enum CompareOperator
   { 
      /// <summary>
      /// &lt;
      /// </summary>
      Less, 
      /// <summary>
      /// &gt;
      /// </summary>
      More, 
      /// <summary>
      /// &lt;=
      /// </summary>
      NotMore,
      /// <summary>
      /// &gt;=
      /// </summary>
      NotLess,
      /// <summary>
      /// &lt;&gt;
      /// </summary>
      NotEqual,
      /// <summary>
      /// =
      /// </summary>
      Equal,
      /// <summary>
      /// IN
      /// </summary>
      In,
      /// <summary>
      /// LIKE
      /// </summary>
      Like
   }
   #endregion
}
