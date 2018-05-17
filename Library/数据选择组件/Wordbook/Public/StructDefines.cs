/*****************************************************************************\
**             Yindansoft & DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             �����ֵ�����Ҫʹ�õĽṹ�嶨��                                  **
**                                                                           **
**                                                                           **
\*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace DrectSoft.Wordbook
{
   #region SingleCondition
   ///// <summary>
   ///// CustomDrawʱʹ�õĵ����ж������ṹ��
   ///// </summary>
   //public struct SingleCondition
   //{
   //   /// <summary>
   //   /// ����(���ݼ���ԭʼ����)
   //   /// </summary>
   //   public string ColumnName
   //   {
   //      get { return columnName; }
   //   }
   //   private string columnName;

   //   /// <summary>
   //   /// �����Ƚϵ�ֵ(ͳһ���ַ������бȽ�)
   //   /// </summary>
   //   public string Value
   //   {
   //      get { return this.value; }
   //   }
   //   private string value;

   //   /// <summary>
   //   /// ����CustomDrawʱʹ�õĵ����ж������ṹ�塣
   //   /// ����û�жԴ����ֵ����У��,�����б�֤�߼���ȷ������
   //   /// </summary>
   //   /// <param name="column">���ݼ���ԭʼ����</param>
   //   /// <param name="equalValue">�����Ƚϵ�ֵ</param>
   //   public SingleCondition(string column, string equalValue)
   //   {
   //      columnName = column;
   //      value = equalValue;
   //   }
   //}
   #endregion

   #region CustomDrawSetting
   ///// <summary>
   ///// CustomDraw������Ϣ�Ľṹ��
   ///// </summary>
   //public struct CustomDrawSetting
   //{
   //   /// <summary>
   //   /// CustomDraw��Ҫ���������
   //   /// </summary>
   //   public Collection<SingleCondition> Conditions
   //   {
   //      get { return conditions; }
   //      //set
   //      //{
   //      //   conditions.Clear();
   //      //   for (int i = 0; i < value.Count; i++)
   //      //   {
   //      //      conditions.Add(value[i]);
   //      //   }
   //      //}
   //   }
   //   private Collection<SingleCondition> conditions;

   //   /// <summary>
   //   /// CustomDraw��ǰ��ɫ
   //   /// </summary>
   //   public Color ForeColor
   //   {
   //      get { return foreColor; }
   //      //set { foreColor = value; }
   //   }
   //   private Color foreColor;

   //   /// <summary>
   //   /// CustomDraw�ı���ɫ
   //   /// </summary>
   //   public Color BackColor
   //   {
   //      get { return backColor; }
   //      //set { backColor = value; }
   //   }
   //   private Color backColor;

   //   public CustomDrawSetting(Collection<SingleCondition> drawCondition, Color fontColor, Color cellBackColor)
   //   {
   //      conditions = new Collection<SingleCondition>();
   //      for (int i = 0; i < drawCondition.Count; i++)
   //      {
   //         conditions.Add(drawCondition[i]);
   //      }
   //      foreColor = fontColor;//SystemColors.ControlText;
   //      backColor = cellBackColor;// SystemColors.ControlLightLight;
   //   }
   //}
   #endregion

   #region WordbookInfo
   /// <summary>
   /// �ֵ���Ϣ�Ľṹ��
   /// </summary>
   public struct WordbookInfo
   {
      /// <summary>
      /// ������������
      /// </summary>
      public string Catalog
      {
         get { return _catalog; }
      }
      private string _catalog;

      /// <summary>
      /// �ֵ����ƣ���ʾ���ƣ�
      /// </summary>
      public string Name
      {
         get { return _name; }
      }
      private string _name;

      /// <summary>
      /// ��Ӧ�ֵ����͵�����
      /// </summary>
      public string TypeName
      {
         get { return _typeName; }
      }
      private string _typeName;

      /// <summary>
      /// �ô������Ϣ����һ���ֵ���Ϣ�ṹ��
      /// </summary>
      /// <param name="catalog">������������</param>
      /// <param name="name">�ֵ�����</param>
      /// <param name="typeName">�ֵ����͵�����</param>
      public WordbookInfo(string catalog, string name, string typeName)
      {
         _catalog = catalog;
         _name = name;
         _typeName = typeName;
      }

      /// <summary>
      /// ȷ�������ֵ���Ϣ�ṹ���Ƿ������ͬ��ֵ
      /// </summary>
      /// <param name="obj">�뵱ǰ����ȽϵĽṹ��</param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj == null)
            return false;
         WordbookInfo p = (WordbookInfo)obj;
         if ((object)p == null)
            return false;
         // Return true if the fields match:
         return (Catalog == p.Catalog)
            && (Name == p.Name)
            && (TypeName == p.TypeName);
      }

      /// <summary>
      /// ���ظ�ʵ���Ĺ�ϣ����
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return TypeName.GetHashCode();
      }

      /// <summary>
      /// ���������"=="
      /// </summary>
      /// <param name="para1"></param>
      /// <param name="para2"></param>
      /// <returns></returns>
      public static bool operator ==(WordbookInfo para1, WordbookInfo para2)
      {
         // If both are null, or both are same instance, return true.
         if (Object.ReferenceEquals(para1, para2))
            return true;
         // If one is null, but not both, return false.
         else if (((object)para1 == null) || ((object)para2 == null))
            return false;
         // Otherwise, compare values and return:
         else
         {
            return (para1.Catalog == para2.Catalog)
            && (para1.Name == para2.Name)
            && (para1.TypeName == para2.TypeName);
         }
      }

      /// <summary>
      /// ���������"!="
      /// </summary>
      /// <param name="para1"></param>
      /// <param name="para2"></param>
      /// <returns></returns>
      public static bool operator !=(WordbookInfo para1, WordbookInfo para2)
      {
         return !(para1 == para2);
      }
   }
   #endregion
}
