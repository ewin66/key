using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// ҵ���߼�����
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(AnonymousType = true)]
   [XmlRootAttribute(Namespace = "http://medical.DrectSoft.com", IsNullable = false)]
   public partial class BusinessLogicSetting
   {
      #region bussiness
      /// <summary>
      /// Ĭ�ϵ���ʱҽ��Ƶ��(ST)����
      /// </summary>
      [Category("ҵ��"), DisplayName("��ʱҽ��Ĭ��Ƶ�δ���"), Description("����HIS�е�ҽ��Ƶ�δ���")]
      public string TempOrderFrequencyCode
      {
         get { return _tempOrderFrequencyCode; }
         set { _tempOrderFrequencyCode = value; }
      }
      private string _tempOrderFrequencyCode;

      /// <summary>
      /// ��ҽ���Ŀ�ʼʱ���ڵ�ǰʱ��֮ǰ�೤ʱ��ͽ�����ʾ����СʱΪ��λ
      /// </summary>
      [Category("ҵ��"), DisplayName("ҽ�����翪ʼʱ��"), Description("������ҽ��ʱ����ʼʱ�����ڵ�ǰʱ�䣬������������趨��ֵ����ϵͳ���ڱ���ҽ��ʱ������ʾ����СʱΪ��λ")]
      public int StartDateWarningHours
      {
         get { return _startDateWarningHours; }
         set { _startDateWarningHours = value; }
      }
      private int _startDateWarningHours;

      /// <summary>
      /// �Ƿ������ڳ���ҽ���������ҩ
      /// </summary>
      [Category("ҵ��"), DisplayName("�����ڳ���ҽ���������ҩ"), Description("")]
      public bool AllowLongHerbOrder
      {
         get { return _allowLongHerbOrder; }
         set { _allowLongHerbOrder = value; }
      }
      private bool _allowLongHerbOrder;

      /// <summary>
      /// ������Ŀʹ�����侯��
      /// </summary>
      [Category("ҵ��"), DisplayName("������Ŀʹ�����侯��"), Description("��������Ŀ����ע��ʹ������")]
      public bool EnableItemAgeWarning
      {
         get { return _enableItemAgeWarning; }
         set { _enableItemAgeWarning = value; }
      }
      private bool _enableItemAgeWarning;

      /// <summary>
      /// ��Ŀʹ�����侯������(���������µĶ�����)
      /// </summary>
      [Category("ҵ��"), DisplayName("��Ŀʹ�����侯������"), Description("������Ŀʹ�����侯�����Ч�����������µĶ�����")]
      public int MaxWarningAge
      {
         get { return _maxWarningAge; }
         set { _maxWarningAge = value; }
      }
      private int _maxWarningAge;

      /// <summary>
      /// ��Ҫ����ʹ��������Ƶ���Ŀ���룬���ʱ��"��"����
      /// </summary>
      [Category("ҵ��"), DisplayName("����ʹ��������Ƶ���Ŀ����"), Description("������Ŀʹ�����侯�����Ч�����ʱ��','����")]
      public string WaringItem
      {
         get { return _waringItem; }
         set { _waringItem = value; }
      }
      private string _waringItem;

      /// <summary>
      /// �������в��˶�ʹ�õ���ҽ��
      /// </summary>
      [Category("ҵ��"), DisplayName("�������в��˶�ʹ�õ���ҽ��"), Description("��Ϊ��ʱ��ҽ��ͬ�����̽����Զ�EMR��û��ҽ�����ݵĲ��˵Ĵ���"), DefaultValue(true)]
      public bool UsedForAllPatient
      {
         get { return _usedForAllPatient; }
         set { _usedForAllPatient = value; }
      }
      private bool _usedForAllPatient;

      /// <summary>
      /// ��ҩ��ֹʱ��
      /// </summary>
      [Category("ҵ��"), DisplayName("��ҩ��ֹʱ��"), Description("��ҩ��ֹʱ��(�������𡰵ĳ���ҽ��ʱ��Ҫ�˲���)"), DefaultValue(8)]
      public int BlockingTimeOfTakeDrug
      {
         get { return _blockingTimeOfTakeDrug; }
         set
         {
            if ((value < 0) || (value > 23))
               _blockingTimeOfTakeDrug = 8;
            _blockingTimeOfTakeDrug = value;
         }
      }
      private int _blockingTimeOfTakeDrug = 8;

      /// <summary>
      /// ���õ��Ӳ���ϵͳ��ҽ��ģ��
      /// </summary>
      [Category("ҵ��"), DisplayName("���õ��Ӳ���ϵͳ��ҽ��ģ��"), Description("��ѡ�����ѯҽ��ʱ��HIS��ȡ����"), DefaultValue(false)]
      public bool EnableEmrOrderModul
      {
         get { return _enableEmrOrderModul; }
         set { _enableEmrOrderModul = value; }
      }
      private bool _enableEmrOrderModul;
      #endregion

      #region interface
      /// <summary>
      /// �Ƿ����ӵ�HIS
      /// </summary>
      [Category("�ӿ�"), DisplayName("��HIS����"), Description("")]
      public bool ConnectToHis
      {
         get { return _connectToHis; }
         set { _connectToHis = value; }
      }
      private bool _connectToHis;

      /// <summary>
      /// ������ҽ��ʱ�Ƿ��Զ�ͬ����HIS�������޸����Զ�ͬ������Ϊ��ʱҪ�ֹ����ύ��ť
      /// </summary>
      [Category("�ӿ�"), DisplayName("����ʱ�Զ����͵�HIS"), Description("Ϊ��ʱ,��Ҫ���ύ��ť�ŻὫ�޸ĺ��ҽ�����͵�HIS")]
      public bool AutoSyncData
      {
         get { return _autoSyncData; }
         set { _autoSyncData = value; }
      }
      private bool _autoSyncData;

      /// <summary>
      /// ���ô�������(��ҪHIS�ṩ��Ӧ��������)
      /// </summary>
      [Category("�ӿ�"), DisplayName("���ô�������"), Description("��THIS4��������Ч��ʹ�õ���HIS�����õĴ�����������")]
      public bool EnableOrderRules
      {
         get { return _enableOrderRules; }
         set { _enableOrderRules = value; }
      }
      private bool _enableOrderRules;

      /// <summary>
      /// ������������ϸ(���ô�������ʱ����Ч)
      /// </summary>
      [Category("�ӿ�"), DisplayName("������������ϸ"), Description("���ô�������ʱ����Ч")]
      public bool SetLimitedDetail
      {
         get { return _setLimitedDetail; }
         set { _setLimitedDetail = value; }
      }
      private bool _setLimitedDetail;
      #endregion

      #region UI
      /// <summary>
      /// ҽ���������ģʽ��Fasle: ʹ��LookUpEditor  True: ʹ�õ�ѡ��ģʽ
      /// </summary>
      [Category("����"), DisplayName("��ѡ��ʽѡ��ҽ�����"), Description("�༭��ҽ��ʱѡ��ҽ�����ķ�ʽ, Ture:��ѡ��ģʽ, False:ѡ��ģʽ")]
      public bool UseRadioCatalogInputStyle
      {
         get { return _useRadioCatalogInputStyle; }
         set { _useRadioCatalogInputStyle = value; }
      }
      private bool _useRadioCatalogInputStyle;

      /// <summary>
      /// �Ƿ��Զ�����ҽ���еĲ�ҩ��ϸ(���غ���ʾ��һ�����ܼ�¼)
      /// </summary>
      [Category("����"), DisplayName("�Զ�����ҽ���еĲ�ҩ��ϸ"), Description("���Զ����أ���Ĭ�Ͻ�һ���ҩ�ϲ���һ�����ܼ�¼����ʾ")]
      public bool AutoHideHerbDetail
      {
         get { return _autoHideHerbDetail; }
         set { _autoHideHerbDetail = value; }
      }
      private bool _autoHideHerbDetail;
      #endregion

      #region others
      /// <summary>
      /// ����Ƿ�ʹ�������ĺ�����ҩ���
      /// </summary>
      [Category("����"), DisplayName("���������ĺ�����ҩ���"), Description("")]
      public bool UseMedicomPlug
      {
         get { return _useMedicomPlug; }
         set { _useMedicomPlug = value; }
      }
      private bool _useMedicomPlug;

      /// <summary>
      /// ʹ�ÿհ׵�ҽ����ӡģ��
      /// </summary>
      [Category("����"), DisplayName("ʹ�ÿհ׵�ҽ����ӡģ��"), Description("�趨�ڿհ�ֽ���ϴ�ӡҽ���������״�ʽ��ӡҽ��")]
      public bool UseEmptyOrderTemplate
      {
         get { return _useEmptyOrderTemplate; }
         set { _useEmptyOrderTemplate = value; }
      }
      private bool _useEmptyOrderTemplate;
      #endregion
   }
}