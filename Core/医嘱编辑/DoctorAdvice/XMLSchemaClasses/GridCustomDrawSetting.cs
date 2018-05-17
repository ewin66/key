using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;
using System.Windows.Forms;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// ҽ��Grid�Ի���������
   /// </summary>
   [System.SerializableAttribute()]
   [System.Diagnostics.DebuggerStepThroughAttribute()]
   [System.ComponentModel.DesignerCategoryAttribute("code")]
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
   [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://medical.DrectSoft.com", IsNullable = false)]
   public partial class GridCustomDrawSetting
   {
      private TypeXmlFont _defaultFont;
      private int _minFontSize;
      private Padding _margin;
      private TypeXmlFont _fontOfCancel;
      private int _startPosOfCancel;
      private TypeCellColor _defaultColor;
      private TypeCellColor _newOrderColor;
      private TypeCellColor _auditedColor;
      private TypeCellColor _cancelledColor;
      private TypeCellColor _executedColor;
      private TypeCellColor _ceasedColor;
      private TypeCellColor _notSynchColor;
      private TypeColorPair _groupFlagColor;
      private int _groupFlagWidth;
      private bool _showRepeatInfo;
      private string _replaceOfRepeatInfo;

      /// <summary>
      /// ҽ������Ĭ������
      /// </summary>
      [Category("����"), DisplayName("ҽ������Ĭ������"), Description("������Ҫ��������ҽ�����ݵ�����"), Browsable(true)]
      public TypeXmlFont DefaultFont
      {
         get { return _defaultFont; }
         set { _defaultFont = value; }
      }

      /// <summary>
      /// ҽ��������С�ֺ�
      /// </summary>
      [Category("����"), DisplayName("ҽ��������С�ֺ�"), Description("Ϊ��������������ʾҽ������,���ڹ�����ҽ������ϵͳ���Զ���С�ֺŻ�������ʾ"), Browsable(true)]
      public int MinFontSize
      {
         get { return _minFontSize; }
         set { _minFontSize = value; }
      }

      /// <summary>
      /// ��������������뵥Ԫ�����ܵľ���
      /// </summary>
      [Category("��ʾ"), DisplayName("ҽ��������������뵥Ԫ�����ܵľ���"), Description("Ϊ������,���Ե������������߿�ľ���,������Ϊ��λ"), Browsable(true)]
      public Padding Margin
      {
         get { return _margin; }
         set { _margin = value; }
      }

      /// <summary>
      /// "ȡ��ҽ��"������
      /// </summary>
      [Category("����"), DisplayName("ȡ����Ϣ������"), Description("��ȡ��ҽ���ġ�ȡ����������"), Browsable(true)]
      public TypeXmlFont FontOfCancel
      {
         get { return _fontOfCancel; }
         set { _fontOfCancel = value; }
      }

      /// <summary>
      /// "ȡ��ҽ��"���ʱ����ʼλ��(������������������)
      /// </summary>
      [Category("��ʾ"), DisplayName("ȡ����Ϣ�����λ��"), Description("��ȡ��ҽ���ġ�ȡ���������ԭʼҽ�����ݵ����λ��,������Ϊ��λ"), Browsable(true)]
      public int StartPosOfCancel
      {
         get { return _startPosOfCancel; }
         set { _startPosOfCancel = value; }
      }

      /// <summary>
      /// Ĭ�ϵ���ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("Ĭ����ɫ"), Description("ҽ������Ĭ�ϵ�������ɫ"), Browsable(false)]
      public TypeCellColor DefaultColor
      {
         get { return _defaultColor; }
         set { _defaultColor = value; }
      }

      /// <summary>
      /// ��ҽ������ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("�¿�ҽ������ɫ"), Description(""), Browsable(false)]
      public TypeCellColor NewOrderColor
      {
         get { return _newOrderColor; }
         set { _newOrderColor = value; }
      }

      /// <summary>
      /// �����ҽ������ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("�����ҽ������ɫ"), Description(""), Browsable(false)]
      public TypeCellColor AuditedColor
      {
         get { return _auditedColor; }
         set { _auditedColor = value; }
      }

      /// <summary>
      /// ��ִ��ҽ������ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("��ִ��ҽ������ɫ"), Description(""), Browsable(false)]
      public TypeCellColor ExecutedColor
      {
         get { return _executedColor; }
         set { _executedColor = value; }
      }

      /// <summary>
      /// ��ֹͣҽ������ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("��ֹͣҽ������ɫ"), Description(""), Browsable(false)]
      public TypeCellColor CeasedColor
      {
         get { return _ceasedColor; }
         set { _ceasedColor = value; }
      }

      /// <summary>
      /// δͬ�������ݵ���ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("δͬ�����ݵ���ɫ"), Description("δͬ����ָ��û�з��͵�HIS"), Browsable(false)]
      public TypeCellColor NotSynchColor
      {
         get { return _notSynchColor; }
         set { _notSynchColor = value; }
      }

      /// <summary>
      /// "ȡ��ҽ��"����ɫ����
      /// </summary>
      [Category("��ʾ"), DisplayName("��ȡ��ҽ������ɫ"), Description(""), Browsable(false)]
      public TypeCellColor CancelledColor
      {
         get { return _cancelledColor; }
         set { _cancelledColor = value; }
      }

      /// <summary>
      /// �����ǵ���ɫ
      /// </summary>
      [Category("��ʾ"), DisplayName("�����ǵ���ɫ"), Description(""), Browsable(true)]
      public TypeColorPair GroupFlagColor
      {
         get { return _groupFlagColor; }
         set { _groupFlagColor = value; }
      }

      /// <summary>
      /// �����ǵĿ��
      /// </summary>
      [Category("��ʾ"), DisplayName("�����ǵ��߿��"), Description("����ҽ���ķ�����,������Ϊ��λ"), Browsable(true)]
      public int GroupFlagWidth
      {
         get { return _groupFlagWidth; }
         set { _groupFlagWidth = value; }
      }

      /// <summary>
      /// �ظ���Ϣ�Ƿ���ʾ���
      /// ���ڿ�ʼ���ڡ�ֹͣʱ�䡢�����ߵ���Ϣ��Ϊ�����࣬�������������Ϣ�ظ���ֻ��Ҫ�ڵ�һ����ʾ������Ϣ�������п��Բ���ʾ
      /// </summary>
      [Category("��ʾ"), DisplayName("���ظ���Ϣ"), Description("����ʱ,���ڿ�ʼ���ڡ������ߡ�ֹͣʱ�䶼��ͬ��������¼,����һ���ⶼ����ʾ�ظ�����Ϣ"), Browsable(true)]
      public bool ShowRepeatInfo
      {
         get { return _showRepeatInfo; }
         set { _showRepeatInfo = value; }
      }

      /// <summary>
      /// �����滻�ظ���Ϣ���ַ���
      /// </summary>
      [Category("��ʾ"), DisplayName("�ظ���Ϣ���滻�ַ���"), Description("�����滻�ظ���Ϣ������Ϊ��"), Browsable(true)]
      public string ReplaceOfRepeatInfo
      {
         get { return _replaceOfRepeatInfo; }
         set { _replaceOfRepeatInfo = value; }
      }

      /// <summary>
      /// ʵ���������������ߴ�(�۳���Margin)
      /// </summary>
      [XmlIgnore(), Browsable(false)]
      public Size OutputSizeOfContent
      {
         get { return _outputSizeOfContent; }
         set { _outputSizeOfContent = value; }
      }
      private Size _outputSizeOfContent;

      /// <summary>
      /// ȡ��ҽ�������λ��(����ڵ�Ԫ��ĳߴ�)
      /// </summary>
      [XmlIgnore(), Browsable(false)]
      public Rectangle BoundsOfCancel
      {
         get { return _boundsOfCancel; }
         set { _boundsOfCancel = value; }
      }
      private Rectangle _boundsOfCancel;
   }
}
