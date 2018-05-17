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
   /// ҽ��¼������
   /// </summary>
   [SerializableAttribute()]
   [DebuggerStepThroughAttribute()]
   [DesignerCategoryAttribute("code")]
   [XmlTypeAttribute(AnonymousType = true)]
   [XmlRootAttribute(Namespace = "http://medical.DrectSoft.com", IsNullable = false)]
   public partial class DoctorAdviceSetting
   {
      private GridCustomDrawSetting _customDrawSetting;

      private OrderGridSetting _gridSetting;

      private BusinessLogicSetting _businessSetting;

      /// <summary>
      /// ҽ���Ի���������
      /// </summary>
      public GridCustomDrawSetting CustomDrawSetting
      {
         get
         {
            return _customDrawSetting;
         }
         set
         {
            _customDrawSetting = value;
         }
      }

      /// <summary>
      /// ҽ��Grid����ʽ
      /// </summary>
      public OrderGridSetting GridSetting
      {
         get
         {
            return _gridSetting;
         }
         set
         {
            _gridSetting = value;
         }
      }

      /// <summary>
      /// ҵ���߼�����
      /// </summary>
      public BusinessLogicSetting BusinessLogic
      {
         get { return _businessSetting; }
         set { _businessSetting = value; }
      }

      /// <summary>
      /// ����ҽ�������������ߴ��ȡ��ҽ��������ߴ�
      /// </summary>
      public void CalcOtherVariables()
      {
         CustomDrawSetting.OutputSizeOfContent =
            new Size(GridSetting.WidthOfContentCell - CustomDrawSetting.Margin.Left - CustomDrawSetting.Margin.Right - CustomDrawSetting.GroupFlagWidth
            , GridSetting.RowHeight - CustomDrawSetting.Margin.Top - CustomDrawSetting.Margin.Bottom);

         int fontHeight = Convert.ToInt32(CustomDrawSetting.FontOfCancel.Font.GetHeight());
         CustomDrawSetting.BoundsOfCancel =
            new Rectangle(CustomDrawSetting.Margin.Left + CustomDrawSetting.StartPosOfCancel
            , (GridSetting.RowHeight - fontHeight) / 2
            , GridSetting.WidthOfContentCell - CustomDrawSetting.Margin.Left - CustomDrawSetting.Margin.Right - CustomDrawSetting.StartPosOfCancel
            , fontHeight);
      }
   }

}