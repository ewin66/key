using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// �������ݱ�ǣ���Щ���ݱ������ˣ�
   /// </summary>
   [Flags]
   internal enum UpdateContentFlag
   {
      /// <summary>
      /// ��ʼʱ��
      /// </summary>
      StartDate = 0x01, 
      /// <summary>
      /// ҽ������
      /// </summary>
      Content = 0x02,   
      /// <summary>
      /// ֹͣʱ��
      /// </summary>
      CeaseDate = 0x04  
   }

   /// <summary>
   /// �༭������־
   /// </summary>
   [Flags]
   internal enum EditProcessFlag
   {
      /// <summary>
      /// ɾ��
      /// </summary>
      Delete = 0x01, 
      /// <summary>
      /// ȡ��
      /// </summary>
      Cancel = 0x02, 
      /// <summary>
      /// ֹͣ
      /// </summary>
      Cease = 0x04,  
      /// <summary>
      /// ����
      /// </summary>
      MoveUp = 0x08, 
      /// <summary>
      /// ����
      /// </summary>
      MoveDown = 0x10,  
      /// <summary>
      /// ����
      /// </summary>
      SetGroup = 0x20,  
      /// <summary>
      /// �鿪ʼ 
      /// </summary>
      GroupStart = 0x40,   
      /// <summary>
      /// �����
      /// </summary>
      GroupEnd = 0x80,  
      /// <summary>
      /// ȡ������
      /// </summary>
      CancelGroup = 0x100, 
      /// <summary>
      /// ����
      /// </summary>
      Save = 0x200,  
      /// <summary>
      /// ���
      /// </summary>
      Audit = 0x400, 
      /// <summary>
      /// ִ��
      /// </summary>
      Execute = 0x800,   
      /// <summary>
      /// ���Զ�ѡ(δ��)
      /// </summary>
      StartMultiSelect = 0x1000,
      /// <summary>
      /// �������
      /// </summary>
      Cut = 0x2000,
      /// <summary>
      /// ������
      /// </summary>
      Copy = 0x4000,
      /// <summary>
      /// ����ճ��
      /// </summary>
      Paste = 0x8000,
      /// <summary>
      /// �ǲ�ҩ���ܼ�¼
      /// </summary>
      IsHerbSummary = 0x10000,
      /// <summary>
      /// �ǲ�ҩ��ϸ��¼
      /// </summary>
      IsHerbDetail = 0x20000
   }

   /// <summary>
   /// ҽ�����ݸ����Զ�Ӧ�ı༭���Ƿ���ñ�־
   /// </summary>
   [Flags]
   internal enum EditorEnableFlag
   { 
      /// <summary>
      /// ����ѡ����Ŀ
      /// </summary>
      CanChoiceItem = 0x01, 
      /// <summary>
      /// ����ѡ���÷�
      /// </summary>
      CanChoiceUsage = 0x02, 
      /// <summary>
      /// ����ѡ��Ƶ��(����ѡƵ��ʱ��Ĭ��Ϊ����Ҫ�÷�������ҲĬ��Ϊ1)
      /// </summary>
      CanChoiceFrequency = 0x04, 
      /// <summary>
      /// ��Ҫ��������
      /// </summary>
      NeedInputAmount = 0x08,
      /// <summary>
      /// ��Ҫ��Ժ��ҩ����Ϣ
      /// </summary>
      NeedOutDruggeryInfo =0x10, 
      /// <summary>
      /// ��Ҫת�Ƶ���Ϣ
      /// </summary>
      NeedShiftDeptInfo = 0x20, 
      /// <summary>
      /// ��Ҫ������Ϣ
      /// </summary>
      NeedOperationInfo = 0x40, 
      /// <summary>
      /// ��Ҫ��Ժʱ��
      /// </summary>
      NeedLeaveHospitalTime = 0x80, 
      /// <summary>
      /// ������������
      /// </summary>
      CanInputEntrust = 0x100,
      /// <summary>
      /// ��������ִ������
      /// </summary>
      CanSetExecuteDays = 0x200
   }

   /// <summary>
   /// ��ѡ�м�¼�����е�����
   /// </summary>
   [Flags]
   internal enum AttributeOfSelectedFlag
   {
      /// <summary>
      /// ������ȡ��ҽ��
      /// </summary>
      HasCancelled = 0x001, 
      /// <summary>
      /// ������ֹͣҽ��
      /// </summary>
      HasCeased = 0x002, 
      /// <summary>
      /// ҽ����Ҫ��������
      /// </summary>
      NumIsSerial = 0x004, 
      /// <summary>
      /// �����ѷ���ļ�¼
      /// </summary>
      HasGrouped = 0x008, 
      /// <summary>
      /// ������һ���еĲ��ּ�¼�������ж�ͬ��ļ�¼��ȫ��ѡ��
      /// </summary>
      HasPieceOfGroup = 0x010, 
      /// <summary>
      /// �д�������������¼�еĵ�һ���ģ��ڵ�һ������֮ǰҲ��Ϊtrue
      /// </summary>
      HasFirstNew = 0x020,
      /// <summary>
      /// �д�������������¼�е����һ����
      /// </summary>
      HasLastNew = 0x040,
      /// <summary>
      /// ��������ҽ��(����ת�ơ���Ժ����ҩ������Ϣ��),Ӱ�쵽ҽ���Ƿ�����λ
      /// </summary>
      HasSpecial = 0x080,
      /// <summary>
      /// ������Ժҽ��
      /// </summary>
      HasLeaveHospital = 0x100, 
      /// <summary>
      /// ��Ժ��ҩҽ���Ƿ�ȫ��ѡ����
      /// </summary>
      SelectedAllOutDurg = 0x200, 
      /// <summary>
      /// ȫ���ǲ�ҩ����
      /// </summary>
      AllIsHerbDruggery = 0x400,
      /// <summary>
      /// ȫ������ҩ�ͳ�ҩ����
      /// </summary>
      AllIsOtherDruggery = 0x800,
      /// <summary>
      /// �����ֹͣʱ���
      /// </summary>
      HasCeaseInfo = 0x1000,
      /// <summary>
      /// �й������뵥��
      /// </summary>
      HasLinkToApply = 0x2000,
      /// <summary>
      /// �в�ҩ������Ϣ��¼
      /// </summary>
      HasHerbSummary = 0x4000,
      /// <summary>
      /// ��ͬһ����
      /// </summary>
      InSameGroup = 0x8000
   }

   /// <summary>
   /// OrderGrid Band������
   /// </summary>
   [SerializableAttribute()]
   [XmlTypeAttribute(AnonymousType = true)]
   public enum OrderGridBandName
   {
      /// <remarks/>
      bandBeginInfo,
      /// <remarks/>
      bandAuditInfo,
      /// <remarks/>
      bandExecuteInfo,
      /// <remarks/>
      bandCeaseInfo
   }

   /// <summary>
   /// Ƥ�Խ����־
   /// </summary>
   public enum SkinTestResultKind
   { 
      /// <summary>
      /// ����
      /// </summary>
      Plus,
      /// <summary>
      /// ����
      /// </summary>
      Minus,
      /// <summary>
      /// ����(3����Ч)
      /// </summary>
      MinusTreeDay
   }

   /// <summary>
   /// ��¼״̬��������ʾ���ݱ༭�������ͣ�
   /// </summary>
   public enum RecordState
   { 
      /// <summary>
      /// ����
      /// </summary>
      Added,
      /// <summary>
      /// �޸�
      /// </summary>
      Modified,
      /// <summary>
      /// ɾ��
      /// </summary>
      Deleted,
      /// <summary>
      /// ȡ��
      /// </summary>
      Cancelled
   }

   /// <summary>
   /// �����ύ����
   /// </summary>
   public enum DataCommitType
   { 
      /// <summary>
      /// ���
      /// </summary>
      Add,
      /// <summary>
      /// ����
      /// </summary>
      Insert,
      /// <summary>
      /// �޸�
      /// </summary>
      Modify
   }

   /// <summary>
   /// ҽ���༭������ģʽ
   /// </summary>
   public enum EditorCallModel
   { 
      /// <summary>
      /// �༭����ҽ��
      /// </summary>
      EditOrder,
      /// <summary>
      /// ��ѯ����ҽ��
      /// </summary>
      Query,
      /// <summary>
      /// �༭����ҽ������
      /// </summary>
      EditSuite
   }
}
