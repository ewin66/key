using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.Common.Eop;

namespace DrectSoft.Core.OwnBedInfo
{
    internal class ConstResource
    {
        //���ڹ�����Ϣ���ֶ���
        internal const string FieldSyxh = "syxh";
        internal const string FieldGgxh = "ggxh";
        internal const string FieldJlzt = "jlzt";
        internal const string FieldYpmc = "ypmc";
        internal const string FieldYxbz = "yxbz";
        internal const string FieldKsrq = "ksrq";
        internal const string FieldJsrq = "jsrq";
        internal const string HasIrritability = "�й���";

        internal const string FieldMrss = "��������";
        internal const string FieldJrss = "��������";
        internal const string FieldShyt = "����1��";
        internal const string FieldShet = "����2��";
        internal const string FieldShst = "����3��";

        internal const string FieldJrcy = "���ճ�Ժ";
        internal const string FieldMrcy = "���ճ�Ժ";
        internal const string FieldThcy = "����Ժ";

        internal const string FieldWbl = "�޲���";

        internal const string FieldJrICU = "����ICU";
        internal const string FieldJrcf = "�������";
        internal const string FieldZkzt = "ת��״̬";

        internal const float OweLine = 0;
        internal const string AppName = "����һ��";

        //���Ʋ�������key
        internal const string ObstetricWardCode = "ObstetricWardCode";
        ////�Զ�ˢ��ʱ������
        //internal const string AUTOREFRESHSETTING = "AutoRefreshSetting";

        //internal const string REFRESHSETTINGKEY = "BedMapping";
        //internal const string REFRESHSETTINGSECTION = "Setting";
        //internal const string REFRESHSETTINGSECTIONKEY = "Key";
        //internal const string REFRESHSETTINGSECTIONVALUE = "Value";
        internal const string BedMappingSetting = "BedMappingSetting";
        //Ĭ��Ϊ1800����Զ�ˢ�¼��
        internal const int DefaultRefreshInterval = 1800;

        /// <summary>
        /// ת�����˵�״̬����
        /// </summary>      
        internal const int STShiftDept = (int)InpatientState.ShiftDept;
        /// <summary>
        /// �������˵�״̬����
        /// </summary>
        internal const int STOutWard = (int)InpatientState.OutWard;
        /// <summary>
        /// ���˳�Ժ��״̬����
        /// </summary>
        internal const int STBalanced = (int)Common.Eop.InpatientState.Balanced;
        internal const int ImageCount = 40;
        internal const int TimerInterval = 300;

        internal const string UpdateStartHint = "0����ǰˢ�µĲ�����Ϣ";

        internal const string ActionDoubleClicked = "OnDoubleClicked";
        internal const string ActionClicked = "Clicked";
    }
}
