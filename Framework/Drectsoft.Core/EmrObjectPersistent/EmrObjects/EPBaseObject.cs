using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Globalization;
using System.ComponentModel;

namespace DrectSoft.Common.Eop
{
   /// <summary>
   /// EmrPersistentBaseObject, �����־���
   /// TODO: ��Ҫ�����ӽӿ���,���Ż�����
   /// </summary>
   public abstract class EPBaseObject : ISupportInitialize
   {
      #region properties
      /// <summary>
      /// ����
      /// </summary>
      [Browsable(false), DisplayName("����")]
      public string Code
      {
         get { return _code; }
         set
         {
            if (value == null)
               _code = String.Empty;
            else
               _code = value;
            Name = "";
         }
      }
      private string _code;

      /// <summary>
      /// ����
      /// </summary>
      [DisplayName("����")]
      public string Name
      {
         get { return _name; }
         set { _name = value; }
      }
      private string _name;

      /// <summary>
      /// �Ƿ���Ч
      /// </summary>
      [Browsable(false)]
      public bool Enabled
      {
         get { return _enabled; }
         set { _enabled = value; }
      }
      private bool _enabled;

      /// <summary>
      /// ��ע
      /// </summary>
      [DisplayName("��ע")]
      public string Memo
      {
         get { return _memo; }
         set { _memo = value; }
      }
      private string _memo;

      /// <summary>
      /// ������������Ƿ��ѳ�ʼ��
      /// </summary>
      [Browsable(false)]
      public virtual bool KeyInitialized
      {
         get
         {
            if (String.IsNullOrEmpty(Code))
               return false;
            else
               return true;
         }
      }

      /// <summary>
      /// ��ʵ����ƥ��ġ���ȡDB�����ݵ�SQL���
      /// </summary>
      [Browsable(false)]
      public abstract string InitializeSentence
      {
         get;
      }

      /// <summary>
      /// ��DataTable�а�����ֵ���˼�¼������
      /// </summary>
      [Browsable(false)]
      public abstract string FilterCondition
      {
         get;
      }

      /// <summary>
      /// �������ж��Ƿ���������ʼ������
      /// </summary>
      protected bool IsInit
      {
         get { return _isInit; }
      }
      private bool _isInit = false;
      #endregion

      #region ctors
      /// <summary>
      /// 
      /// </summary>
      protected EPBaseObject()
      { }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      protected EPBaseObject(string code)
      {
         Code = code;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="code"></param>
      /// <param name="name"></param>
      protected EPBaseObject(string code, string name)
      {
         Code = code;
         Name = name;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sourceRow"></param>
      protected EPBaseObject(DataRow sourceRow)
      {
         if (sourceRow != null)
            Initialize(sourceRow);
      }
      #endregion

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sqlName"></param>
      /// <returns></returns>
      protected static string GetQuerySentenceFromXml(string sqlName)
      {
         return PersistentObjectFactory.GetQuerySentenceByName(sqlName);
      }

      /// <summary>
      /// ͳһ�ĸ�ʽ��������������
      /// </summary>
      /// <param name="fieldName"></param>
      /// <param name="codeValue"></param>
      /// <returns></returns>
      protected static string FormatFilterString(string fieldName, string codeValue)
      {
         if (String.IsNullOrEmpty(fieldName))
            return "";
         if (String.IsNullOrEmpty(codeValue))
            return fieldName.Trim() + " = ''";
         else
            return fieldName.Trim() + " = '" + codeValue.Trim() + "'";
      }

      #region public methods

      /// <summary>
      /// �ô����DataRow��ʼ������(DataRow����Ҫ�����Ľṹ��ȫƥ�䣬�ڲ�ֻ��ʼ���ֶ���ƥ�������)
      /// </summary>
      /// <param name="sourceRow">������ʼ���ݵ�DataRow</param>
      public void Initialize(DataRow sourceRow)
      {
         PersistentObjectFactory.InitializeObjectProperty(this, sourceRow);
      }

      ///// <summary>
      ///// �Ե�ǰ����Ϊ��������һ���¶��󣬱����ؼ����Ե�ֵ
      ///// </summary>
      ///// <returns></returns>
      //public abstract EPBaseObject Copy();

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return String.Format(CultureInfo.CurrentCulture, "{0}[{1}]", Name, Code);
      }

      /// <summary>
      /// ��¡����
      /// </summary>
      /// <returns></returns>
      internal Object Clone()
      {
         return PersistentObjectFactory.CloneEopBaseObject(this);
      }

      /// <summary>
      /// ��������ȡ��ӦDataRow������ʼ����������
      /// </summary>
      public virtual void ReInitializeProperties()
      {
         if (PersistentObjectFactory.SqlExecutor != null)
         {
            DataRow row = PersistentObjectFactory.SqlExecutor.GetRecord(
               InitializeSentence, FilterCondition, true);
            if (row != null)
               Initialize(row);
         }
      }

      /// <summary>
      /// ��ʼ�����е����ԣ������������͵������Լ�������
      /// </summary>
      public abstract void ReInitializeAllProperties();
      #endregion

      #region ISupportInitialize ��Ա

      /// <summary>
      /// 
      /// </summary>
      public virtual void BeginInit()
      {
         _isInit = true;
      }

      /// <summary>
      /// 
      /// </summary>
      public virtual void EndInit()
      {
         _isInit = false;
      }

      #endregion
   }
}
