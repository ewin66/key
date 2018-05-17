using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// ���˲����ඨ��
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   [CLSCompliantAttribute(true)]
   public class FilterParameter
   {
      #region properties
      /// <summary>
      /// ������Ӧ�����ݼ��ֶ�����
      /// </summary>
      public string FieldName
      {
         get { return _fieldName; }
         set { _fieldName = value; }
      }
      private string _fieldName;

      /// <summary>
      /// �������ơ���������û�������ʱ���ò���ֵ������Ӧȡ��������
      /// </summary>
      public string Caption
      {
         get { return _caption; }
         set { _caption = value; }
      }
      private string _caption;

      /// <summary>
      /// ��ǲ����Ƿ����ַ���
      /// </summary>
      public bool IsString
      {
         get { return _isString; }
         set { _isString = value; }
      }
      private bool _isString;

      /// <summary>
      /// �Բ���������
      /// </summary>
      public string Description
      {
         get { return _description; }
         set { _description = value; }
      }
      private string _description;

      /// <summary>
      /// �˲�����ȡֵ��Դ��ָ����������������û����ò���ֵ��ʱ����Ҫ����Ϣ���ṩ�������ܣ�
      /// </summary>
      public int DataCatalog
      {
         get { return _dataCatalog; }
         set { _dataCatalog = value; }
      }
      private int _dataCatalog;

      /// <summary>
      /// ������ͨ�û��༭����ֵ
      /// </summary>
      public bool AllowUserEdit
      {
         get { return _allowUserEdit; }
         set { _allowUserEdit = value; }
      }
      private bool _allowUserEdit;

      /// <summary>
      /// �����Ƿ���Ч
      /// </summary>
      public bool Enabled
      {
         get { return _enabled; }
         set { _enabled = value; }
      }
      private bool _enabled;

      /// <summary>
      /// �����
      /// </summary>
      [XmlElementAttribute("OperatorName")]
      public CompareOperator Operator
      {
         get { return _operator; }
         set { _operator = value; }
      }
      private CompareOperator _operator;
            
      /// <summary>
      /// ����ֵ�������ò���ֵʱ����ܱ�֤��ʽ���߼�����ȷ��
      /// ��֧���Է�Χ��ʽ��������(��: "id1"��"id1,id2,id3"��"id1,id2��id3,id4%",
      /// ����ʹ�ð�ǵ�","��"%"��ȫ�ǵ�"��"��"%"ֻ���ַ��Ͳ�����Ч)�������������"In"
      /// </summary>
      [XmlElementAttribute("DefaultValue", typeof(string))]
      public object Value
      {
         get { return this._value; }
         set 
         {
            string temp;
            if (value == null)
               temp = "";
            else
               temp = value.ToString();
            // �ȶ������ֵ���м��
            CheckValue(temp);

            if ((!IsString)
               && (CommonOperation.GetStringType(temp.ToString()) == StringType.Empty))
               _value = "-1"; // ���ַ��͵Ĳ���ֵ����Ϊ�գ�Ĭ��Ϊ-1
            else
               _value = value;
            //// �Բ���ֵ���м򵥵ļ����滻
            //if (IsString)
            //{
            //   if (temp[0] != '\'')
            //      _value = "'" + value;
            //   else 
            //      _value = value;

            //   if (!temp.EndsWith("'"))
            //      _value = _value + "'";
            //}
            //else
            //{
            //   if (CommonOperation.GetStringType(temp) == StringType.Empty)
            //      temp = "-1";

            //   // ���ʹ��In����������������","�����򶼲����쳣
            //   if ((CommonOperation.GetStringType(temp) != StringType.Numeric)
            //      && ((OperatorName != CompareOperator.In)
            //      || (CommonOperation.GetStringType(temp.Replace(',', (char)0)) == StringType.Numeric)))
            //      throw new ArithmeticException("���ַ�����ֵ����ֵ�Ͳ���");
            //   _value = temp; 
            //}
         }
      }
      private object _value;

      /// <summary>
      /// �����������ʽ�е�ֵ��������ַ��͵Ĳ��������Բ���ֵ���ϵ����ţ�
      /// </summary>
      [XmlIgnoreAttribute()]
      public string ParameterValue
      {
         get
         {
            if (!IsString)
               return Value.ToString(); // ��ֵ�Ͳ���Ҫ����

            if (Operator != CompareOperator.In)
               return "'" + Value.ToString().Trim() + "'";
            else
            { 
               // ��ȥ���ո��ĩβ�ġ�,����Ȼ�󽫡�,���͡������ֱ��滻Ϊ��','���͡�'��'��
               string temp = Value.ToString().Replace(" ", "");
               if (temp[temp.Length - 1] == ',')
                  temp = temp.Substring(0, temp.Length - 1);
               temp = temp.Replace(",", "','");
               temp = temp.Replace("��", "'��'");
               return "'" + temp + "'";
            }
         }
      }
      #endregion

      /// <summary>
      /// ��鴫��Ĳ���ֵ�Ƿ���ȷ
      /// </summary>
      /// <param name="paraValue"></param>
      private void CheckValue(object paraValue)
      {
         string temp = paraValue.ToString();
         // ����','��Ϊ�ָ�������ֳ�����������
         string[] separator1 = new string[] { "," };
         string[] separator2 = new string[] { "��" };
         string[] values = temp.Split(separator1, StringSplitOptions.None);
         string[] rangs;
         if ((values.Length > 1) && (Operator != CompareOperator.In))
            throw new ArgumentException(MessageStringManager.GetString("CoulNotSetParameterRange"));

         foreach (string condition in values)
         {
            // ��鷶Χ�����Ƿ���ȷ
            if (condition.Contains("��"))
            {
               rangs = condition.Split(separator2, StringSplitOptions.None);
               if (rangs.Length != 2)
                  throw new ArgumentException(MessageStringManager.GetString("WrongParameterRange"));
               if (condition.Contains("%"))
                  throw new ArgumentException(MessageStringManager.GetString("CouldNotUseWildcardInRange"));
               CheckValueType(rangs[0]);
               CheckValueType(rangs[1]);
            }
            else // ��鵥���������Ƿ�����Ҫ��
               CheckValueType(condition);
         }
      }

      /// <summary>
      /// ��鴫��Ĳ���ֵ�Ƿ���ϲ������͵�Ҫ��
      /// ����Ҫ�Ƿ�ֹ���ַ������ݸ�����ֵ�Ͳ�����
      /// </summary>
      /// <param name="paraValue"></param>
      private void CheckValueType(string paraValue)
      {
         if ((!IsString)
            && (CommonOperation.GetStringType(paraValue) != StringType.Numeric)
            && (CommonOperation.GetStringType(paraValue) != StringType.Empty))
            throw new ArithmeticException(MessageStringManager.GetString("CouldNotSetStringToDigital"));
      }

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      public FilterParameter()
      { }

      /// <summary>
      /// �������˲���
      /// </summary>
      /// <param name="fieldName">������Ӧ�����ݼ��ֶ���</param>
      /// <param name="name">����������</param>
      /// <param name="isString">��ǲ����Ƿ����ַ���</param>
      /// <param name="description">����������</param>
      /// <param name="allowUserEdit">������ͨ�û��༭����ֵ</param>
      /// <param name="sign">�����</param>
      public FilterParameter(string fieldName, string name, bool isString
         , string description, bool allowUserEdit, CompareOperator sign )
      {
         if (String.IsNullOrEmpty(fieldName))
            throw new ArgumentNullException(MessageStringManager.GetString("NullFieldName"));
         if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException(MessageStringManager.GetString("NullParameterName"));
         if ((!isString) && (sign == CompareOperator.Like))
            throw new ArgumentException(MessageStringManager.GetString("UseLikeOperatprOnDigitalParameter"));

         _fieldName = fieldName;
         _caption = name;
         _isString = isString;
         _description = description;
         _allowUserEdit = allowUserEdit;
         _operator = sign;
         _dataCatalog = -1;

         //_enable = false;
      }

      /// <summary>
      /// �������˲����������ó�ʼֵ
      /// </summary>
      /// <param name="fieldName">������Ӧ�����ݼ��ֶ���</param>
      /// <param name="name">����������</param>
      /// <param name="isString">��ǲ����Ƿ����ַ���</param>
      /// <param name="description">����������</param>
      /// <param name="allowUserEdit">������ͨ�û��༭����ֵ</param>
      /// <param name="sign">�����</param>
      /// <param name="value">��ʼֵ</param>
      public FilterParameter(string fieldName, string name, bool isString
         , string description, bool allowUserEdit, CompareOperator sign, object value) 
         : this(fieldName, name, isString, description, allowUserEdit, sign)
      {
         Value = value;
      }

      /// <summary>
      /// ����ȡֵ��Դ��ָ�����������Ĺ��˲�����
      /// �������Ĭ��Ϊ���ַ����ͣ���������ͨ�û��༭ֵ
      /// </summary>
      /// <param name="fieldName">������Ӧ�����ݼ��ֶ���</param>
      /// <param name="name">����������</param>
      /// <param name="dataSort">ָ�������������Դ</param>
      /// <param name="description">����������</param>
      /// <param name="sign">�����</param>
      public FilterParameter(string fieldName, string name, int dataSort
         , string description, CompareOperator sign)
         : this(fieldName, name, false, description, true, sign)
      {
         _dataCatalog = dataSort;
      }

      /// <summary>
      /// ����ȡֵ��Դ��ָ�����������Ĺ��˲����������ó�ֵ��
      /// �������Ĭ��Ϊ���ַ����ͣ���������ͨ�û��༭ֵ
      /// </summary>
      /// <param name="fieldName">������Ӧ�����ݼ��ֶ���</param>
      /// <param name="name">����������</param>
      /// <param name="dataSort">ָ�������������Դ</param>
      /// <param name="description">����������</param>
      /// <param name="sign">�����</param>
      /// <param name="value">��ʼֵ</param>
      public FilterParameter(string fieldName, string name, int dataSort
         , string description, CompareOperator sign, object value)
         : this(fieldName, name, dataSort, description, sign)
      {
         Value = value;
      }
      #endregion

      /// <summary>
      /// ����"=="
      /// </summary>
      /// <param name="para1"></param>
      /// <param name="para2"></param>
      /// <returns></returns>
      public static bool operator ==(FilterParameter para1, FilterParameter para2)
      {
         // If one is null, but not both, return false.
         if ((object)para1 == null)
            return false;
         if ((object)para2 == null)
            return false;
         // If both are null, or both are same instance, return true.
         if (Object.ReferenceEquals(para1, para2))
            return true;
         // Otherwise, compare values and return:
         else
         {
            return (para1.FieldName == para2.FieldName)
            && (para1.Caption == para2.Caption)
            && (para1.IsString == para2.IsString)
            && (para1.Operator == para2.Operator);
         }
      }

      /// <summary>
      /// ����"!="
      /// </summary>
      /// <param name="para1"></param>
      /// <param name="para2"></param>
      /// <returns></returns>
      public static bool operator !=(FilterParameter para1, FilterParameter para2)
      {
         return !(para1 == para2);
      }
      
      /// <summary>
      /// ȷ������FilterParameter�����Ƿ���ͬ
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(Object obj)
      {
         // If parameter is null, or cannot be cast to FilterParameter,
         // return false.
         if (obj == null) 
            return false;
         FilterParameter p = obj as FilterParameter;
         if ((object)p == null) 
            return false;
         // Return true if the fields match:
         return (FieldName == p.FieldName)
            && (Caption == p.Caption)
            && (IsString == p.IsString)
            && (Operator == p.Operator);
      }

      /// <summary>
      /// ���ظ�ʵ���Ĺ�ϣ����
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return FieldName.GetHashCode() 
            ^ Caption.GetHashCode() 
            ^ IsString.GetHashCode() 
            ^ Operator.GetHashCode();
      }

      /// <summary>
      /// Clone
      /// </summary>
      /// <returns></returns>
      public FilterParameter Clone()
      {
         FilterParameter cln = new FilterParameter();
         cln.FieldName = this.FieldName;
         cln.Caption = this.Caption;
         cln.AllowUserEdit = this.AllowUserEdit;
         cln.DataCatalog = this.DataCatalog;
         cln.Description = this.Description;
         cln.Enabled = this.Enabled;
         cln.Operator = this.Operator;
         //cln.ParameterValue = this.ParameterValue;
         cln.Value = this.Value;
         cln.IsString = this.IsString;

         return cln;
      }

   }
}
