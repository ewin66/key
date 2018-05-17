using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ��Դ�ַ�������
    /// </summary>
    public class ConstRes
    {
        public static readonly string[] transmode = new string[]{
         "zh_en/Chinese-simp to English",
         "zt_en/Chinese-trad to English",
         "en_zh/English to Chinese-simp",
         "en_zt/English to Chinese-trad",
         "en_nl/English to Dutch",
         "en_fr/English to French",
         "en_de/English to German",
         "en_el/English to Greek",
         "en_it/English to Italian",
         "en_ja/English to Japanese",
         "en_ko/English to Korean",
         "en_pt/English to Portuguese",
         "en_ru/English to Russian",
         "en_es/English to Spanish",
         "nl_en/Dutch to English",
         "nl_fr/Dutch to French",
         "fr_en/French to English",
         "fr_de/French to German",
         "fr_el/French to Greek",
         "fr_it/French to Italian",
         "fr_pt/French to Portuguese",
         "fr_nl/French to Dutch",
         "fr_es/French to Spanish",
         "de_en/German to English",
         "de_fr/German to French",
         "el_en/Greek to English",
         "el_fr/Greek to French",
         "it_en/Italian to English",
         "it_fr/Italian to French",
         "ja_en/Japanese to English",
         "ko_en/Korean to English",
         "pt_en/Portuguese to English",
         "pt_fr/Portuguese to French",
         "ru_en/Russian to English",
         "es_en/Spanish to English",
         "es_fr/Spanish to French",
        };

        public const string cstOrderType = "DrectSoft.Common.Eop.Order,DrectSoft.Core";
        public const string cstEmrModelType = "DrectSoft.Core.Model.EmrModel,DrectSoft.Core.ModelEntity";
        public const string cstPatientType = "DrectSoft.Common.Eop.Inpatient,DrectSoft.Core";

        #region showlist bl_zlkzgzk

        public const string cstTableNameQcRule = "ʱ�޹���";
        public const string cstSqlSelectQcRule = "select RuleCode, Description from QCRule";
        public const string cstShowlistColsQcRule = "RuleCode//�������//80///Description//��������//200";
        #endregion

        public const string cstEditStatusBrowse = "�༭״̬���������";
        public const string cstEditStatusNew = "�༭״̬����������";
        public const string cstEditStatusModify = "�༭״̬���޸Ĺ���";

        public const string cstFieldCaptionId = "����";
        public const string cstFieldCaptionDescript = "����";

        public const string cstSaveCheckNeedQcRuleId = "��������������";
        public const string cstSaveCheckNeedQcRuleName = "���������������";
        public const string cstSaveCheckExistSameRuleId = "�Ѿ�������ͬ����Ĺ���";
        public const string cstSaveCheckExistSameCondId = "�Ѿ�������ͬ����Ĺ�������";
        public const string cstSaveCheckExistSameResultId = "�Ѿ�������ͬ����Ĺ�����";
        public const string cstSaveCheckExistSameId = "�Ѿ�������ͬ����";
        public const string cstSaveSuccess = "����ɹ�";
        public const string cstLoadQcRulesFail = "��ȡ��������ʧ��";
        public const string cstConfirmDelete = "��ȷ��Ҫɾ����";

        #region DataViewTimeLimit

        public const string cstFieldDoctorId = "DocotorID";
        public const string cstFieldCaptionDoctorId = "סԺҽ��";
        public const string cstFieldFirstPage = "NoOfInpat";
        public const string cstFieldCaptionFirstPage = "��ҳ���";
        public const string cstFieldPatNameHospNo = "NoOfRecord";
        public const string cstFieldCaptionPatNameHospNo = "��������";
        public const string cstFieldInfo = "tipwarninfo";
        public const string cstFieldCaptionInfo = "��ʾ�򾯸���Ϣ";
        public const string cstFieldTipStatus = "tipstatus";
        public const string cstFieldCaptionTipStatus = "��ʾ״̬";
        public const string cstFieldTimeLimit = "TimeLimit";
        public const string cstFieldCaptionTimeLimit = "ʱ��";

        public const string cstFieldCondTime = "ConditionTime";
        public const string cstFieldResultTime = "ResultTime";
        public const string cstFieldPatName = "Name";
        public const string cstFieldPatHospNo = "NoOfRecord";
        public const string cstFieldTipStatus2 = "FoulState";
        public const string cstFieldDoctorId2 = "DocotorID";
        public const string cstFieldDoctorName = "UserName";
        public const string cstFieldTipInfo = "Reminder";
        public const string cstFieldWarnInfo = "FoulMessage";

        public const string cstOverTime = "����";
        public const string cstOnTime = "ʣ��";

        public const string cstDayChn = "��";
        public const string cstHourChn = "ʱ";
        public const string cstMinuteChn = "��";

        #endregion

        public const string cstAndOper = " [and] ";

        public const int cstCondTypeNo = 3900;
        public const int cstResultTypeNo = 4000;
        public const int cstDoctorLevelNo = 1999;
    }
}
