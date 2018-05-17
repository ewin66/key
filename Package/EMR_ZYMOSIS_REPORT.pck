CREATE OR REPLACE PACKAGE EMR_ZYMOSIS_REPORT IS
  TYPE empcurtyp IS REF CURSOR;

  /*
  * ���봫Ⱦ�����濨����
  */
  PROCEDURE usp_EditZymosis_Report(v_EditType         varchar,
                                   v_Report_ID        NUMERIC DEFAULT 0,
                                   v_Report_NO        varchar DEFAULT '', --���濨����
                                   v_Report_Type      varchar default '', --���濨����   1�����α���  2����������
                                   v_Noofinpat        varchar default '', --��ҳ���
                                   v_PatID            varchar default '', --סԺ��
                                   v_Name             varchar default '', --��������
                                   v_ParentName       varchar default '', --�ҳ�����
                                   v_IDNO             varchar default '', --���֤����
                                   v_Sex              varchar default '', --�����Ա�
                                   v_Birth            varchar default '', --��������
                                   v_Age              varchar default '', --ʵ������
                                   v_AgeUnit          varchar default '', --ʵ�����䵥λ
                                   v_Organization     varchar default '', --������λ
                                   v_OfficePlace      varchar default '', --��λ��ַ
                                   v_OfficeTEL        varchar default '', --��λ�绰
                                   v_AddressType      varchar default '', --�������ڵ���  1�������� 2����������������  3����ʡ��������  4����ʡ  5���۰�̨  6���⼮
                                   v_HomeTown         varchar default '', --����
                                   v_Address          varchar default '', --��ϸ��ַ[�� �ֵ� ���ƺ�]
                                   v_JobID            varchar default '', --ְҵ���루��ҳ��˳���¼��ţ�
                                   v_RecordType1      varchar default '', --��������  1�����Ʋ���  2���ٴ���ϲ���  3��ʵ����ȷ�ﲡ��  4��ԭЯ����
                                   v_RecordType2      varchar default '', --�������ࣨ���͸��ס�Ѫ���没��д��  1������  2������
                                   v_AttackDate       varchar default '', --�������ڣ���ԭЯ������������ڻ�������ڣ�
                                   v_DiagDate         varchar default '', --�������
                                   v_DieDate          varchar default '', --��������
                                   v_DiagICD10        varchar default '', --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
                                   v_DiagName         varchar default '', --��Ⱦ����������
                                   v_INFECTOTHER_FLAG varchar default '', --���޸�Ⱦ������[0�� 1��]
                                   v_Memo             varchar default '', --��ע
                                   v_Correct_flag     varchar default '', --������־��0��δ���� 1���Ѷ�����
                                   v_Correct_Name     varchar default '', --��������
                                   v_Cancel_Reason    varchar default '', --�˿�ԭ��
                                   v_ReportDeptCode   varchar default '', --������ұ��
                                   v_ReportDeptName   varchar default '', --�����������
                                   v_ReportDocCode    varchar default '', --����ҽ�����
                                   v_ReportDocName    varchar default '', --����ҽ������
                                   v_DoctorTEL        varchar default '', --����ҽ����ϵ�绰
                                   v_Report_Date      varchar default '', --�ʱ��
                                   v_State            varchar default '', --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
                                   v_StateName        varchar default '', --����״̬�������¼������ˮ��
                                   v_create_date      varchar default '', --����ʱ��
                                   v_create_UserCode  varchar default '', --������
                                   v_create_UserName  varchar default '', --������
                                   v_create_deptCode  varchar default '', --�����˿���
                                   v_create_deptName  varchar default '', --�����˿���
                                   v_Modify_date      varchar default '', --�޸�ʱ��
                                   v_Modify_UserCode  varchar default '', --�޸���
                                   v_Modify_UserName  varchar default '', --�޸���
                                   v_Modify_deptCode  varchar default '', --�޸��˿���
                                   v_Modify_deptName  varchar default '', --�޸��˿���
                                   v_Audit_date       varchar default '', --���ʱ��
                                   v_Audit_UserCode   varchar default '', --�����
                                   v_Audit_UserName   varchar default '', --�����
                                   v_Audit_deptCode   varchar default '', --����˿���
                                   v_Audit_deptName   varchar default '', --����˿���
                                   v_OtherDiag        varchar default '',
                                   o_result           OUT empcurtyp);

  PROCEDURE usp_GetInpatientByNofinpat(v_Noofinpat varchar default '', --��ҳ���
                                       o_result    OUT empcurtyp);

  PROCEDURE usp_geteditzymosisreport(v_report_type1    varchar default '',
                                     v_report_type2    varchar default '',
                                     v_name            varchar default '',
                                     v_patid           varchar default '',
                                     v_deptid          varchar default '',
                                     v_applicant       varchar default '',
                                     v_status          varchar default '',
                                     v_createdatestart varchar default '', --�������ϱ����ڿ�ʼ
                                     v_createdateend   varchar default '', --�������ϱ����ڽ���
                                     v_querytype       varchar default '', --��ѯ����
                                     o_result          OUT empcurtyp);

  PROCEDURE usp_getReportBrowse(v_report_type1 varchar default '',
                                v_report_type2 varchar default '',
                                v_recordtype1  varchar default '',
                                v_beginDate    varchar default '',
                                v_EndDate      varchar default '',
                                v_deptid       varchar default '',
                                v_diagICD      varchar default '',
                                v_status       varchar default '',
                                o_result       OUT empcurtyp);

  PROCEDURE usp_GetReportAnalyse(v_beginDate varchar default '',
                                 v_EndDate   varchar default '',
                                 o_result    OUT empcurtyp);

  --��ְҵͳ�ƴ�Ⱦ����Ϣ
  PROCEDURE usp_GetJobDisease(v_beginDate varchar default '',
                              v_EndDate   varchar default '',
                              v_DiagCode  varchar default '',
                              o_result    OUT empcurtyp);

  --�õ�������Ч�����
  PROCEDURE usp_GetDiagnosis(o_result OUT empcurtyp);

  --�õ���Ⱦ��������Ϣ
  PROCEDURE usp_GetDisease2(o_result OUT empcurtyp);

  --�õ�������Ч�����
  PROCEDURE usp_GetDiagnosisTo(v_categoryid varchar default '',
                               o_result     OUT empcurtyp);

  --�õ�������Ч�����
  PROCEDURE usp_GetDiagnosisTo_ZY(v_categoryid varchar default '',
                                  o_result     OUT empcurtyp);

  --�õ���Ⱦ��������Ϣ
  PROCEDURE usp_GetDisease2To(v_categoryid varchar default '',
                              o_result     OUT empcurtyp);

  --���没�ּ�¼
  PROCEDURE usp_SaveZymosisDiagnosis(v_markid   varchar default '',
                                     v_icd      varchar default '',
                                     v_name     varchar default '',
                                     v_py       varchar default '',
                                     v_wb       varchar default '',
                                     v_levelID  varchar default '',
                                     v_valid    varchar default '',
                                     v_statist  varchar default '',
                                     v_memo     varchar default '',
                                     v_namestr  varchar default '',
                                     v_upcount  integer,
                                     v_fukatype varchar default '');
  --���没�ּ�¼
  PROCEDURE usp_SaveZymosisDiagnosisTo(v_markid     varchar default '',
                                       v_icd        varchar default '',
                                       v_name       varchar default '',
                                       v_py         varchar default '',
                                       v_wb         varchar default '',
                                       v_levelID    varchar default '',
                                       v_valid      varchar default '',
                                       v_statist    varchar default '',
                                       v_memo       varchar default '',
                                       v_namestr    varchar default '',
                                       v_upcount    integer,
                                       v_categoryid integer);

  PROCEDURE usp_EditTherioma_Report(v_EditType              varchar,
                                    v_Report_ID             NUMERIC DEFAULT 0,
                                    v_REPORT_DISTRICTID     varchar DEFAULT '', --��Ⱦ���ϱ�������(��)����
                                    v_REPORT_DISTRICTNAME   varchar default '', --��Ⱦ���ϱ�������(��)����
                                    v_REPORT_ICD10          varchar default '', --��Ⱦ�����濨ICD-10����
                                    v_REPORT_ICD0           varchar default '', --��Ⱦ�����濨ICD-0����
                                    v_REPORT_CLINICID       varchar default '', --�����
                                    v_REPORT_PATID          varchar default '', --סԺ��
                                    v_REPORT_INDO           varchar default '', --���֤����
                                    v_REPORT_INPATNAME      varchar default '', --��������
                                    v_SEXID                 varchar default '', --�����Ա�
                                    v_REALAGE               varchar default '', --����ʵ������
                                    v_BIRTHDATE             varchar default '', --��������
                                    v_NATIONID              varchar default '', --����������
                                    v_NATIONNAME            varchar default '', --��������ȫ��
                                    v_CONTACTTEL            varchar default '', --��ͥ�绰
                                    v_MARTIAL               varchar default '', --����״��
                                    v_OCCUPATION            varchar default '', --����ְҵ
                                    v_OFFICEADDRESS         varchar default '', --������λ��ַ
                                    v_ORGPROVINCEID         varchar default '', --���ڵ�ַʡ�ݱ���
                                    v_ORGCITYID             varchar default '', --���ڵ�ַ�����б���
                                    v_ORGDISTRICTID         varchar default '', --�������ڵ����ر���
                                    v_ORGTOWNID             varchar default '', --�������ڵ���(�ֵ�)����
                                    v_ORGVILLIAGE           varchar default '', --�������ڵؾ�ί���Ӧ����
                                    v_ORGPROVINCENAME       varchar default '', --�������ڵ�ʡ��ȫ��
                                    v_ORGCITYNAME           varchar default '', --�������ڵ���ȫ����
                                    v_ORGDISTRICTNAME       varchar default '', --�������ڵ���(��)ȫ��
                                    v_ORGTOWN               varchar default '', --�������ڵ���ȫ��
                                    v_ORGVILLAGENAME        varchar default '', --�������ڵش�ȫ��
                                    v_XZZPROVINCEID         varchar default '', --��סַ����ʡ�ݱ���
                                    v_XZZCITYID             varchar default '', --��סַ�����б���
                                    v_XZZDISTRICTID         varchar default '', --��סַ������(��)����
                                    v_XZZTOWNID             varchar default '', --��סַ���������
                                    v_XZZVILLIAGEID         varchar default '', --����סַ���ڴ����
                                    v_XZZPROVINCE           varchar default '', --��סַ����ʡ��ȫ��
                                    v_XZZCITY               varchar default '', --��סַ������ȫ��
                                    v_XZZDISTRICT           varchar default '', --��סַ������ȫ��
                                    v_XZZTOWN               varchar default '', --��סַ������ȫ��
                                    v_XZZVILLIAGE           varchar default '', --��סַ���ڴ�ȫ��
                                    v_REPORT_DIAGNOSIS      varchar default '', --���
                                    v_PATHOLOGICALTYPE      varchar default '', --��������
                                    v_PATHOLOGICALID        varchar default '', --������ϲ����
                                    v_QZDIAGTIME_T          varchar default '', --ȷ��ʱ��_T��
                                    v_QZDIAGTIME_N          varchar default '', --ȷ��ʱ��_N��
                                    v_QZDIAGTIME_M          varchar default '', --ȷ��ʱ��_M��
                                    v_FIRSTDIADATE          varchar default '', --�״�ȷ��ʱ��
                                    v_REPORTINFUNIT         varchar default '', --���浥λ
                                    v_REPORTDOCTOR          varchar default '', --����ҽ��
                                    v_REPORTDATE            varchar default '', --����ʱ��
                                    v_DEATHDATE             varchar default '', --����ʱ��
                                    v_DEATHREASON           varchar default '', --����ԭ��
                                    v_REJEST                varchar default '', --����ժҪ
                                    v_REPORT_YDIAGNOSIS     varchar default '', --ԭ���
                                    v_REPORT_YDIAGNOSISDATA varchar default '', --ԭ�������
                                    v_REPORT_DIAGNOSISBASED varchar default '', --�������
                                    v_REPORT_NO             varchar default '', --��Ⱦ���ϱ�������
                                    v_REPORT_NOOFINPAT      varchar default '', --����ID
                                    v_STATE                 varchar default '', --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
                                    v_CREATE_DATE           varchar default '', --����������  
                                    v_CREATE_USERCODE       varchar default '', --������
                                    v_CREATE_USERNAME       varchar default '', --������
                                    v_CREATE_DEPTCODE       varchar default '', --�����˿���
                                    v_CREATE_DEPTNAME       varchar default '', --�����˿���
                                    v_MODIFY_DATE           varchar default '', --�޸�ʱ��
                                    v_MODIFY_USERCODE       varchar default '', --�޸���
                                    v_MODIFY_USERNAME       varchar default '', --�޸���
                                    v_MODIFY_DEPTCODE       varchar default '', --�޸��˿���
                                    v_MODIFY_DEPTNAME       varchar default '', --�޸��˿���
                                    v_AUDIT_DATE            varchar default '', --���ʱ��
                                    v_AUDIT_USERCODE        varchar default '', --�����
                                    v_AUDIT_USERNAME        varchar default '', --�����
                                    v_AUDIT_DEPTCODE        varchar default '', --����˿���
                                    v_AUDIT_DEPTNAME        varchar default '', --����˿���
                                    v_VAILD                 varchar default '', --״̬�Ƿ���Ч  1����Ч   0����Ч
                                    v_DIAGICD10             varchar default '', --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
                                    v_CANCELREASON          varchar default '', --���ԭ��   
                                    V_CARDTYPE              varchar default '', --��Ƭ�����������߷���                     
                                    V_clinicalstages        varchar default '',
                                    V_ReportDiagfunit       varchar default '',
                                    o_result                OUT empcurtyp);

  --�����б��濨  add  by  ywk 2013��8��21�� 15:47:00
  PROCEDURE usp_EditCardiovacular_Report(v_EditType      varchar, --��������
                                         v_REPORT_NO     varchar DEFAULT '', --���濨����                                 
                                         v_NOOFCLINIC    varchar DEFAULT '', --����� 
                                         v_PATID         varchar default '',
                                         v_NAME          varchar default '',
                                         v_IDNO          varchar default '',
                                         v_SEXID         varchar default '',
                                         v_BIRTH         varchar default '',
                                         v_AGE           varchar DEFAULT '',
                                         v_NATIONID      varchar default '',
                                         v_JOBID         varchar default '',
                                         v_OFFICEPLACE   varchar default '',
                                         v_CONTACTTEL    varchar default '',
                                         v_HKPROVICE     varchar default '',
                                         v_HKCITY        varchar default '',
                                         v_HKSTREET      varchar default '',
                                         v_HKADDRESSID   varchar default '',
                                         v_XZZPROVICE    varchar default '',
                                         v_XZZCITY       varchar default '',
                                         v_XZZSTREET     varchar default '',
                                         v_XZZADDRESSID  varchar default '',
                                         v_XZZCOMMITTEES varchar default '',
                                         v_XZZPARM         varchar default '',
                                         v_ICD             varchar default '',
                                         v_DIAGZWMXQCX     varchar default '',
                                         v_DIAGNCX         varchar default '',
                                         v_DIAGNGS         varchar default '',
                                         v_DIAGWFLNZZ      varchar default '',
                                         v_DIAGJXXJGS      varchar default '',
                                         v_DIAGXXCS        varchar default '',
                                         v_DIAGNOSISBASED  varchar default '',
                                         v_DIAGNOSEDATE    varchar default '',
                                         v_ISFIRSTSICK     varchar default '',
                                         v_DIAGHOSPITAL    varchar default '',
                                         v_OUTFLAG         varchar default '',
                                         v_DIEDATE         varchar default '',
                                         v_REPORTDEPT      varchar default '',
                                         v_REPORTUSERCODE  varchar default '',
                                         v_REPORTUSERNAME  varchar default '',
                                         v_REPORTDATE      varchar default '',
                                         v_CREATE_DATE     varchar default '',
                                         v_CREATE_USERCODE varchar default '',
                                         v_CREATE_USERNAME varchar default '',
                                         v_CREATE_DEPTCODE varchar default '',
                                         v_CREATE_DEPTNAME varchar default '',
                                         v_MODIFY_DATE     varchar default '',
                                         v_MODIFY_USERCODE varchar default '',
                                         v_MODIFY_USERNAME varchar default '',
                                         v_MODIFY_DEPTCODE varchar default '',
                                         v_MODIFY_DEPTNAME varchar default '',
                                         v_AUDIT_DATE      varchar default '',
                                         v_AUDIT_USERCODE  varchar default '',
                                         v_AUDIT_USERNAME  varchar default '',
                                         v_AUDIT_DEPTCODE  varchar default '',
                                         v_AUDIT_DEPTNAME  varchar default '',
                                         v_VAILD           varchar default '',
                                         v_CANCELREASON    varchar default '',
                                         v_STATE           varchar default '',
                                         v_CARDPARAM1      varchar default '',
                                         v_CARDPARAM2      varchar default '',
                                         v_CARDPARAM3      varchar default '',
                                         v_CARDPARAM4      varchar default '',
                                         v_CARDPARAM5      varchar default '',
                                         v_NOOFINPAT       varchar default '',
                                           v_REPORTID      varchar DEFAULT '',
                                         o_result          OUT empcurtyp);

  --������ȱ�ݱ��濨  add  by  jxh  2013-08-16   ��ʱ����
  PROCEDURE usp_EditTbirthdefects_Report(v_EditType         varchar,
                                         v_ID               NUMERIC DEFAULT 0,
                                         v_REPORT_NOOFINPAT varchar DEFAULT '', --���˱��
                                         v_REPORT_ID        varchar default '', --���濨���
                                         v_DIAG_CODE        varchar default '', --���濨��ϱ���
                                         v_STRING3          varchar default '', --Ԥ��
                                         v_STRING4          varchar default '', --Ԥ��
                                         v_STRING5          varchar default '', --Ԥ��
                                         v_REPORT_PROVINCE  varchar DEFAULT '', --�ϱ��濨ʡ��
                                         v_REPORT_CITY      varchar default '', --���濨�У��أ�
                                         v_REPORT_TOWN      varchar default '', --���濨����
                                         v_REPORT_VILLAGE   varchar default '', --���濨��
                                         v_REPORT_HOSPITAL  varchar default '', --���濨ҽԺ
                                         v_REPORT_NO        varchar default '', --���濨���
                                         v_MOTHER_PATID     varchar default '', --����סԺ��
                                         v_MOTHER_NAME      varchar default '', --����
                                         v_MOTHER_AGE       varchar default '', --����
                                         v_NATIONAL         varchar default '', --����
                                         v_ADDRESS_POST     varchar default '', --��ַand�ʱ�
                                         v_PREGNANTNO       varchar default '', --�д�
                                         v_PRODUCTIONNO     varchar default '', --����
                                         v_LOCALADD         varchar default '', --��ס��
                                         
                                         v_PERCAPITAINCOME         varchar default '', --���˾�����     
                                         v_EDUCATIONLEVEL          varchar default '', --�Ļ��̶�     
                                         v_CHILD_PATID             varchar default '', --����סԺ��     
                                         v_CHILD_NAME              varchar default '', --��������     
                                         v_ISBORNHERE              varchar default '', --�Ƿ�Ժ����     
                                         v_CHILD_SEX               varchar default '', --�����Ա�      
                                         v_BORN_YEAR               varchar default '', --������     
                                         v_BORN_MONTH              varchar default '', --  ������     
                                         v_BORN_DAY                varchar default '', --������      
                                         v_GESTATIONALAGE          varchar default '', --̥��     
                                         v_WEIGHT                  varchar default '', --����     
                                         v_BIRTHS                  varchar default '', --̥��     
                                         v_ISIDENTICAL             varchar default '', --�Ƿ�ͬ��      
                                         v_OUTCOME                 varchar default '', --ת��      
                                         v_INDUCEDLABOR            varchar default '', --�Ƿ�����     
                                         v_DIAGNOSTICBASIS         varchar default '', --������ݡ����ٴ�      
                                         v_DIAGNOSTICBASIS1        varchar default '', --������ݡ���������      
                                         v_DIAGNOSTICBASIS2        varchar default '', --������ݡ���ʬ��     
                                         v_DIAGNOSTICBASIS3        varchar default '', --������ݡ����������     
                                         v_DIAGNOSTICBASIS4        varchar default '', --������ݡ���������顪������     
                                         v_DIAGNOSTICBASIS5        varchar default '', --������ݡ���Ⱦɫ��      
                                         v_DIAGNOSTICBASIS6        varchar default '', --������ݡ�������     
                                         v_DIAGNOSTICBASIS7        varchar default '', --������ݡ���������������     
                                         v_DIAG_ANENCEPHALY        varchar default '', --����ȱ����ϡ������Ի���     
                                         v_DIAG_SPINA              varchar default '', --����ȱ����ϡ���������      
                                         v_DIAG_PENGOUT            varchar default '', --����ȱ����ϡ��������      
                                         v_DIAG_HYDROCEPHALUS      varchar default '', --����ȱ����ϡ����������Ի�ˮ     
                                         v_DIAG_CLEFTPALATE        varchar default '', --����ȱ����ϡ�������     
                                         v_DIAG_CLEFTLIP           varchar default '', --����ȱ����ϡ�������      
                                         v_DIAG_CLEFTMERGER        varchar default '', --����ȱ����ϡ������Ѻϲ�����     
                                         v_DIAG_SMALLEARS          varchar default '', --����ȱ����ϡ���С���������޶���     
                                         v_DIAG_OUTEREAR           varchar default '', --����ȱ����ϡ�������������Σ�С�����޶����⣩     
                                         v_DIAG_ESOPHAGEAL         varchar default '', --����ȱ����ϡ���ʳ����������խ     
                                         v_DIAG_RECTUM             varchar default '', --����ȱ����ϡ���ֱ�����ű�������խ�������޸أ�     
                                         v_DIAG_HYPOSPADIAS        varchar default '', --����ȱ����ϡ����������     
                                         v_DIAG_BLADDER            varchar default '', --����ȱ����ϡ��������ⷭ     
                                         v_DIAG_HORSESHOEFOOTLEFT  varchar default '', --����ȱ����ϡ��������ڷ���_��      
                                         v_DIAG_HORSESHOEFOOTRIGHT varchar default '', --����ȱ����ϡ��������ڷ���_��     
                                         v_DIAG_MANYPOINTLEFT      varchar default '', --����ȱ����ϡ�����ָ��ֺ��_��      
                                         v_DIAG_MANYPOINTRIGHT     varchar default '', --����ȱ����ϡ�����ָ��ֺ��_��     
                                         v_DIAG_LIMBSUPPERLEFT     varchar default '', --����ȱ����ϡ���֫�����_��֫ _��      
                                         v_DIAG_LIMBSUPPERRIGHT    varchar default '', --����ȱ����ϡ���֫�����_��֫ _��     
                                         v_DIAG_LIMBSLOWERLEFT     varchar default '', --����ȱ����ϡ���֫�����_��֫ _��      
                                         v_DIAG_LIMBSLOWERRIGHT    varchar default '', --����ȱ����ϡ���֫�����_��֫ _��     
                                         v_DIAG_HERNIA             varchar default '', --����ȱ����ϡ�������������     
                                         v_DIAG_BULGINGBELLY       varchar default '', --����ȱ����ϡ��������     
                                         v_DIAG_GASTROSCHISIS      varchar default '', --����ȱ����ϡ�������     
                                         v_DIAG_TWINS              varchar default '', --����ȱ����ϡ�������˫̥     
                                         v_DIAG_TSSYNDROME         varchar default '', --����ȱ����ϡ��������ۺ�����21-�����ۺ�����     
                                         v_DIAG_HEARTDISEASE       varchar default '', --����ȱ����ϡ������������ಡ�����ͣ�      
                                         v_DIAG_OTHER              varchar default '', --����ȱ����ϡ���������д����������ϸ������      
                                         v_DIAG_OTHERCONTENT       varchar default '', --����ȱ����ϡ�����������      
                                         v_FEVER                   varchar default '', --���գ���38�棩      
                                         v_VIRUSINFECTION          varchar default '', --������Ⱦ     
                                         v_ILLOTHER                varchar default '', --��������     
                                         v_SULFA                   varchar default '', --�ǰ���     
                                         v_ANTIBIOTICS             varchar default '', --������     
                                         v_BIRTHCONTROLPILL        varchar default '', --����ҩ      
                                         v_SEDATIVES               varchar default '', --��ҩ     
                                         v_MEDICINEOTHER           varchar default '', --��ҩ����      
                                         v_DRINKING                varchar default '', --����     
                                         v_PESTICIDE               varchar default '', --ũҩ      
                                         v_RAY                     varchar default '', --����      
                                         v_CHEMICALAGENTS          varchar default '', --��ѧ�Ƽ�     
                                         v_FACTOROTHER             varchar default '', --�����к�����      
                                         v_STILLBIRTHNO            varchar default '', --��̥����     
                                         v_ABORTIONNO              varchar default '', --��Ȼ��������     
                                         v_DEFECTSNO               varchar default '', --ȱ�ݶ�����     
                                         v_DEFECTSOF1              varchar default '', --ȱ����1     
                                         v_DEFECTSOF2              varchar default '', --ȱ����2     
                                         v_DEFECTSOF3              varchar default '', --ȱ����3     
                                         v_YCDEFECTSOF1            varchar default '', --�Ŵ�ȱ����1     
                                         v_YCDEFECTSOF2            varchar default '', --�Ŵ�ȱ����2     
                                         v_YCDEFECTSOF3            varchar default '', --�Ŵ�ȱ����3     
                                         v_KINSHIPDEFECTS1         varchar default '', --��ȱ�ݶ���Ե��ϵ1     
                                         v_KINSHIPDEFECTS2         varchar default '', --��ȱ�ݶ���Ե��ϵ2     
                                         v_KINSHIPDEFECTS3         varchar default '', --��ȱ�ݶ���Ե��ϵ3     
                                         v_COUSINMARRIAGE          varchar default '', --���׻���ʷ      
                                         v_COUSINMARRIAGEBETWEEN   varchar default '', --���׻���ʷ��ϵ     
                                         v_PREPARER                varchar default '', --�����      
                                         v_THETITLE1               varchar default '', --�����ְ��     
                                         v_FILLDATEYEAR            varchar default '', --���������      
                                         v_FILLDATEMONTH           varchar default '', --���������     
                                         v_FILLDATEDAY             varchar default '', --���������     
                                         v_HOSPITALREVIEW          varchar default '', --ҽԺ�����      
                                         v_THETITLE2               varchar default '', --ҽԺ�����ְ��     
                                         v_HOSPITALAUDITDATEYEAR   varchar default '', --ҽԺ���������     
                                         v_HOSPITALAUDITDATEMONTH  varchar default '', --ҽԺ���������      
                                         v_HOSPITALAUDITDATEDAY    varchar default '', --ҽԺ���������      
                                         v_PROVINCIALVIEW          varchar default '', --ʡ�������      
                                         v_THETITLE3               varchar default '', --ʡ�������ְ��     
                                         v_PROVINCIALVIEWDATEYEAR  varchar default '', --ʡ�����������      
                                         v_PROVINCIALVIEWDATEMONTH varchar default '', --ʡ�����������     
                                         v_PROVINCIALVIEWDATEDAY   varchar default '', --ʡ�����������     
                                         v_FEVERNO                 varchar default '', --���ն���      
                                         v_ISVIRUSINFECTION        varchar default '', --�Ƿ񲡶���Ⱦ     
                                         v_ISDIABETES              varchar default '', --�Ƿ�����      
                                         v_ISILLOTHER              varchar default '', --�Ƿ񻼲�����     
                                         v_ISSULFA                 varchar default '', --�Ƿ�ǰ���     
                                         v_ISANTIBIOTICS           varchar default '', --�Ƿ�����     
                                         v_ISBIRTHCONTROLPILL      varchar default '', --�Ƿ����ҩ      
                                         v_ISSEDATIVES             varchar default '', --�Ƿ���ҩ     
                                         v_ISMEDICINEOTHER         varchar default '', --�Ƿ��ҩ����      
                                         v_ISDRINKING              varchar default '', --�Ƿ�����     
                                         v_ISPESTICIDE             varchar default '', --�Ƿ�ũҩ      
                                         v_ISRAY                   varchar default '', --�Ƿ�����      
                                         v_ISCHEMICALAGENTS        varchar default '', --�Ƿ�ѧ�Ƽ�     
                                         v_ISFACTOROTHER           varchar default '', --�Ƿ������к�����      
                                         v_STATE                   varchar default '', -- "����״̬�� 1���������� 2���ύ 3������ 4��?to open this dialog next """     
                                         v_CREATE_DATE             varchar default '', --����ʱ��      
                                         v_CREATE_USERCODE         varchar default '', --������     
                                         v_CREATE_USERNAME         varchar default '', ---������      
                                         v_CREATE_DEPTCODE         varchar default '', --�����˿���     
                                         v_CREATE_DEPTNAME         varchar default '', --�����˿���     
                                         v_MODIFY_DATE             varchar default '', --�޸�ʱ��      
                                         v_MODIFY_USERCODE         varchar default '', --�޸���     
                                         v_MODIFY_USERNAME         varchar default '', --�޸���     
                                         v_MODIFY_DEPTCODE         varchar default '', --�޸��˿���     
                                         v_MODIFY_DEPTNAME         varchar default '', --�޸��˿���     
                                         v_AUDIT_DATE              varchar default '', --���ʱ��     
                                         v_AUDIT_USERCODE          varchar default '', --�����      
                                         v_AUDIT_USERNAME          varchar default '', --�����      
                                         v_AUDIT_DEPTCODE          varchar default '', --����˿���      
                                         v_AUDIT_DEPTNAME          varchar default '', --����˿���      
                                         v_VAILD                   varchar default '', --״̬�Ƿ���Ч  1����Ч   0����Ч     
                                         v_CANCELREASON            varchar default '', --���ԭ��     
                                         v_PRENATAL                varchar default '', --��ǰ     
                                         v_PRENATALNO              varchar default '', --��ǰ����     
                                         v_POSTPARTUM              varchar default '', --����     
                                         v_ANDTOSHOWLEFT           varchar default '', --��ָ��     
                                         v_ANDTOSHOWRIGHT          varchar default '', --��ָ��
                                         o_result                  OUT empcurtyp);

  --������---�����Ǽ��±��� add by ywk 2013��7��31�� 14:59:19
  PROCEDURE usp_GetTheriomaReportBYMonth( --v_searchtype varchar default '',--���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                         v_year          varchar default '', --��
                                         v_month         varchar default '', --��
                                         v_deptcode      varchar default '', --���ұ���
                                         v_diagstartdate varchar default '', --��Ͽ�ʼʱ��
                                         v_diagenddate   varchar default '', --��Ͻ���ʱ��
                                         o_result        OUT empcurtyp);

  --������---���������·������Ǽǲ� add by ywk 2013��8��2�� 11:29:02
  PROCEDURE usp_GetTheriomaReportBYNew( --v_searchtype varchar default '',--���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                       v_year   varchar default '', --��
                                       v_month  varchar default '', --��
                                       o_result OUT empcurtyp);

  --������---�����������������Ǽǲ� add by ywk 2013��8��2�� 11:29:02
  PROCEDURE usp_GetTheriomaReportBYDead( --v_searchtype varchar default '',--���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                        v_year   varchar default '', --��
                                        v_month  varchar default '', --��
                                        o_result OUT empcurtyp);

  --������---�����Ǽ��±��� add by ywk 2013��8��5�� 11:32:52����ҽԺ
  PROCEDURE usp_GetTheriomaReportBYMonthZX( --v_searchtype varchar default '',--���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                           v_year          varchar default '', --��
                                           v_month         varchar default '', --��
                                           v_deptcode      varchar default '', --���ұ���
                                           v_diagstartdate varchar default '', --��Ͽ�ʼʱ��
                                           v_diagenddate   varchar default '', --��Ͻ���ʱ��
                                           o_result        OUT empcurtyp);
                                           
   --������---�����Ǽ��±��� add by jxh 2013��9��12�� 14:05:52����ҽԺ
  PROCEDURE usp_CardiovascularReport( --v_searchtype varchar default '',--���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                           v_year          varchar default '', --��
                                           v_month         varchar default '', --��
                                           v_deptcode      varchar default '', --���ұ���
                                           v_diagstartdate varchar default '', --��Ͽ�ʼʱ��
                                           v_diagenddate   varchar default '', --��Ͻ���ʱ��
                                           o_result        OUT empcurtyp);

  --������޸İ��̲�����
  --������޸İ��̲�����
  PROCEDURE usp_AddOrModHIVReport(v_HIVID               varchar2,
                                  v_REPORTID            integer,
                                  v_REPORTNO            varchar2,
                                  v_MARITALSTATUS       varchar2,
                                  v_NATION              varchar2,
                                  v_CULTURESTATE        varchar2,
                                  v_HOUSEHOLDSCOPE      varchar2,
                                  v_HOUSEHOLDADDRESS    varchar2,
                                  v_ADDRESS             varchar2,
                                  v_CONTACTHISTORY      varchar2,
                                  v_VENERISMHISTORY     varchar2,
                                  v_INFACTWAY           varchar2,
                                  v_SAMPLESOURCE        varchar2,
                                  v_DETECTIONCONCLUSION varchar2,
                                  v_AFFIRMDATE          varchar2,
                                  v_AFFIRMLOCAL         varchar2,
                                  v_DIAGNOSEDATE        varchar2,
                                  v_DOCTOR              varchar2,
                                  v_WRITEDATE           varchar2,
                                  v_ALIKESYMBOL         varchar2,
                                  v_REMARK              varchar2,
                                  v_VAILD               varchar2,
                                  v_CREATOR             varchar2,
                                  v_CREATEDATE          varchar2,
                                  v_MENDER              varchar2,
                                  v_ALTERDATE           varchar2);

  --������޸�ɳ����ԭ���Ⱦ
  PROCEDURE usp_AddOrModSYYYTReport(v_SZDYYTID            varchar,
                                    v_REPORTID            integer,
                                    v_REPORTNO            varchar,
                                    v_MARITALSTATUS       varchar,
                                    v_NATION              varchar,
                                    v_CULTURESTATE        varchar,
                                    v_HOUSEHOLDSCOPE      varchar,
                                    v_HOUSEHOLDADDRESS    varchar,
                                    v_ADDRESS             varchar,
                                    v_SYYYTGR             varchar,
                                    v_CONTACTHISTORY      varchar,
                                    v_VENERISMHISTORY     varchar,
                                    v_INFACTWAY           varchar,
                                    v_SAMPLESOURCE        varchar,
                                    v_DETECTIONCONCLUSION varchar,
                                    v_AFFIRMDATE          varchar,
                                    v_AFFIRMLOCAL         varchar,
                                    v_VAILD               varchar,
                                    v_CREATOR             varchar,
                                    v_CREATEDATE          varchar,
                                    v_MENDER              varchar,
                                    v_ALTERDATE           varchar);

  --������޸��Ҹα����
  PROCEDURE usp_AddOrModHBVReport(v_HBVID varchar2,
                                  
                                  v_REPORTID      integer,
                                  v_HBSAGDATE     varchar2,
                                  v_FRISTDATE     varchar2,
                                  v_ALT           varchar2,
                                  v_ANTIHBC       varchar2,
                                  v_LIVERBIOPSY   varchar2,
                                  v_RECOVERYHBSAG varchar2,
                                  
                                  v_VAILD      varchar2,
                                  v_CREATOR    varchar2,
                                  v_CREATEDATE varchar2,
                                  v_MENDER     varchar2,
                                  v_ALTERDATE  varchar2);

  -- -������޸ļ���ʪ����Ŀ
  PROCEDURE usp_AddOrModJRSYReport(v_JRSY_ID             varchar,
                                   v_REPORTID            integer,
                                   v_REPORTNO            varchar,
                                   v_MARITALSTATUS       varchar,
                                   v_NATION              varchar,
                                   v_CULTURESTATE        varchar,
                                   v_HOUSEHOLDSCOPE      varchar,
                                   v_HOUSEHOLDADDRESS    varchar,
                                   v_ADDRESS             varchar,
                                   v_CONTACTHISTORY      varchar,
                                   v_VENERISMHISTORY     varchar,
                                   v_INFACTWAY           varchar,
                                   v_SAMPLESOURCE        varchar,
                                   v_DETECTIONCONCLUSION varchar,
                                   v_AFFIRMDATE          varchar,
                                   v_AFFIRMLOCAL         varchar,
                                   v_VAILD               varchar,
                                   v_CREATOR             varchar,
                                   v_CREATEDATE          varchar,
                                   v_MENDER              varchar,
                                   v_ALTERDATE           varchar);
  --������޸ļ���H1N1���б���
  PROCEDURE usp_AddOrModH1N1Report(v_H1N1ID         varchar2,
                                   v_REPORTID       integer,
                                   v_CASETYPE       varchar2,
                                   v_HOSPITALSTATUS varchar2,
                                   v_ISCURE         varchar2,
                                   v_ISOVERSEAS     varchar2,
                                   v_VAILD          varchar2,
                                   v_CREATOR        varchar2,
                                   v_CREATEDATE     varchar2,
                                   v_MENDER         varchar2,
                                   v_ALTERDATE      varchar2);

  --������޸�����ڲ������
  PROCEDURE usp_AddOrModHFMDReport(v_HFMDID     varchar2,
                                   v_REPORTID   integer,
                                   v_LABRESULT  varchar2,
                                   v_ISSEVERE   varchar2,
                                   v_VAILD      varchar2,
                                   v_CREATOR    varchar2,
                                   v_CREATEDATE varchar2,
                                   v_MENDER     varchar2,
                                   v_ALTERDATE  varchar2);

  --������޸�AFP�����
  PROCEDURE usp_AddOrModAFPReport(v_AFPID            varchar2,
                                  v_REPORTID         integer,
                                  v_HOUSEHOLDSCOPE   varchar2,
                                  v_HOUSEHOLDADDRESS varchar2,
                                  v_ADDRESS          varchar2,
                                  v_PALSYDATE        varchar2,
                                  v_PALSYSYMPTOM     varchar2,
                                  v_VAILD            varchar2,
                                  v_CREATOR          varchar2,
                                  v_CREATEDATE       varchar2,
                                  v_MENDER           varchar2,
                                  v_ALTERDATE        varchar2,
                                  v_DIAGNOSISDATE    varchar2);

END;
/
CREATE OR REPLACE PACKAGE BODY EMR_ZYMOSIS_REPORT IS

  /*********************************************************************************/
  PROCEDURE usp_EditZymosis_Report(v_EditType         varchar,
                                   v_Report_ID        NUMERIC DEFAULT 0,
                                   v_Report_NO        varchar DEFAULT '', --���濨����
                                   v_Report_Type      varchar default '', --���濨����   1�����α���  2����������
                                   v_Noofinpat        varchar default '', --��ҳ���
                                   v_PatID            varchar default '', --סԺ��
                                   v_Name             varchar default '', --��������
                                   v_ParentName       varchar default '', --�ҳ�����
                                   v_IDNO             varchar default '', --���֤����
                                   v_Sex              varchar default '', --�����Ա�
                                   v_Birth            varchar default '', --��������
                                   v_Age              varchar default '', --ʵ������
                                   v_AgeUnit          varchar default '', --ʵ�����䵥λ
                                   v_Organization     varchar default '', --������λ
                                   v_OfficePlace      varchar default '', --��λ��ַ
                                   v_OfficeTEL        varchar default '', --��λ�绰
                                   v_AddressType      varchar default '', --�������ڵ���  1�������� 2����������������  3����ʡ��������  4����ʡ  5���۰�̨  6���⼮
                                   v_HomeTown         varchar default '', --����
                                   v_Address          varchar default '', --��ϸ��ַ[�� �ֵ� ���ƺ�]
                                   v_JobID            varchar default '', --ְҵ���루��ҳ��˳���¼��ţ�
                                   v_RecordType1      varchar default '', --��������  1�����Ʋ���  2���ٴ���ϲ���  3��ʵ����ȷ�ﲡ��  4��ԭЯ����
                                   v_RecordType2      varchar default '', --�������ࣨ���͸��ס�Ѫ���没��д��  1������  2������
                                   v_AttackDate       varchar default '', --�������ڣ���ԭЯ������������ڻ�������ڣ�
                                   v_DiagDate         varchar default '', --�������
                                   v_DieDate          varchar default '', --��������
                                   v_DiagICD10        varchar default '', --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
                                   v_DiagName         varchar default '', --��Ⱦ����������
                                   v_INFECTOTHER_FLAG varchar default '', --���޸�Ⱦ������[0�� 1��]
                                   v_Memo             varchar default '', --��ע
                                   v_Correct_flag     varchar default '', --������־��0��δ���� 1���Ѷ�����
                                   v_Correct_Name     varchar default '', --��������
                                   v_Cancel_Reason    varchar default '', --�˿�ԭ��
                                   v_ReportDeptCode   varchar default '', --������ұ��
                                   v_ReportDeptName   varchar default '', --�����������
                                   v_ReportDocCode    varchar default '', --����ҽ�����
                                   v_ReportDocName    varchar default '', --����ҽ������
                                   v_DoctorTEL        varchar default '', --����ҽ����ϵ�绰
                                   v_Report_Date      varchar default '', --�ʱ��
                                   v_State            varchar default '', --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
                                   v_StateName        varchar default '', --����״̬�������¼������ˮ��
                                   v_create_date      varchar default '', --����ʱ��
                                   v_create_UserCode  varchar default '', --������
                                   v_create_UserName  varchar default '', --������
                                   v_create_deptCode  varchar default '', --�����˿���
                                   v_create_deptName  varchar default '', --�����˿���
                                   v_Modify_date      varchar default '', --�޸�ʱ��
                                   v_Modify_UserCode  varchar default '', --�޸���
                                   v_Modify_UserName  varchar default '', --�޸���
                                   v_Modify_deptCode  varchar default '', --�޸��˿���
                                   v_Modify_deptName  varchar default '', --�޸��˿���
                                   v_Audit_date       varchar default '', --���ʱ��
                                   v_Audit_UserCode   varchar default '', --�����
                                   v_Audit_UserName   varchar default '', --�����
                                   v_Audit_deptCode   varchar default '', --����˿���
                                   v_Audit_deptName   varchar default '', --����˿���
                                   v_OtherDiag        varchar default '',
                                   o_result           OUT empcurtyp) AS
  
    v_Report_ID_new int;
  BEGIN
  
    --������Ⱦ�����濨
    IF v_edittype = '1' THEN
    
      select seq_Zymosis_Report_ID.Nextval into v_Report_ID_new from dual;
    
      insert into zymosis_report
        (Report_ID,
         Report_NO, --���濨����
         Report_Type, --���濨����   1�����α���  2����������
         Noofinpat, --��ҳ���
         PatID, --סԺ��
         Name, --��������
         ParentName, --�ҳ�����
         IDNO, --���֤����
         Sex, --�����Ա�
         Birth, --��������
         Age, --ʵ������
         Age_Unit, --ʵ�����䵥λ
         Organization, --������λ
         OfficePlace, --��λ��ַ
         OfficeTEL, --��λ�绰
         AddressType, --�������ڵ���  1�������� 2����������������  3����ʡ��������  4����ʡ  5���۰�̨  6���⼮
         HomeTown, --����
         Address, --��ϸ��ַ[�� �ֵ� ���ƺ�]
         JobID, --ְҵ���루��ҳ��˳���¼��ţ�
         RecordType1, --��������  1�����Ʋ���  2���ٴ���ϲ���  3��ʵ����ȷ�ﲡ��  4��ԭЯ����
         RecordType2, --�������ࣨ���͸��ס�Ѫ���没��д��  1������  2������
         AttackDate, --�������ڣ���ԭЯ������������ڻ�������ڣ�
         DiagDate, --�������
         DieDate, --��������
         DiagICD10, --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
         DiagName, --��Ⱦ����������
         INFECTOTHER_FLAG, --���޸�Ⱦ������[0�� 1��]
         Memo, --��ע
         Correct_flag, --������־��0��δ���� 1���Ѷ�����
         Correct_Name, --��������
         Cancel_Reason, --�˿�ԭ��
         ReportDeptCode, --������ұ��
         ReportDeptName, --�����������
         ReportDocCode, --����ҽ�����
         ReportDocName, --����ҽ������
         DoctorTEL, --����ҽ����ϵ�绰
         Report_Date, --�ʱ��
         State, --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
         create_date, --����ʱ��
         create_UserCode, --������
         create_UserName, --������
         create_deptCode, --�����˿���
         create_deptName, --�����˿���
         Modify_date, --�޸�ʱ��
         Modify_UserCode, --�޸���
         Modify_UserName, --�޸���
         Modify_deptCode, --�޸��˿���
         Modify_deptName, --�޸��˿���
         Audit_date, --���ʱ��
         Audit_UserCode, --�����
         Audit_UserName, --�����
         Audit_deptCode, --����˿���
         Audit_deptName, --����˿���
         OTHERDIAG)
      values
        (v_Report_ID_new,
         v_Report_NO, --���濨����
         v_Report_Type, --���濨����   1�����α���  2����������
         v_Noofinpat, --��ҳ���
         v_PatID, --סԺ��
         v_Name, --��������
         v_ParentName, --�ҳ�����
         v_IDNO, --���֤����
         v_Sex, --�����Ա�
         v_Birth, --��������
         v_Age, --ʵ������
         v_AgeUnit,
         v_Organization, --������λ
         v_OfficePlace, --��λ��ַ
         v_OfficeTEL, --��λ�绰
         v_AddressType, --�������ڵ���  1�������� 2����������������  3����ʡ��������  4����ʡ  5���۰�̨  6���⼮
         v_HomeTown, --����
         v_Address, --��ϸ��ַ[�� �ֵ� ���ƺ�]
         v_JobID, --ְҵ���루��ҳ��˳���¼��ţ�
         v_RecordType1, --��������  1�����Ʋ���  2���ٴ���ϲ���  3��ʵ����ȷ�ﲡ��  4��ԭЯ����
         v_RecordType2, --�������ࣨ���͸��ס�Ѫ���没��д��  1������  2������
         v_AttackDate, --�������ڣ���ԭЯ������������ڻ�������ڣ�
         v_DiagDate, --�������
         v_DieDate, --��������
         v_DiagICD10, --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
         v_DiagName, --��Ⱦ����������
         v_INFECTOTHER_FLAG, --���޸�Ⱦ������[0�� 1��]
         v_Memo, --��ע
         v_Correct_flag, --������־��0��δ���� 1���Ѷ�����
         v_Correct_Name, --��������
         v_Cancel_Reason, --�˿�ԭ��
         v_ReportDeptCode, --������ұ��
         v_ReportDeptName, --�����������
         v_ReportDocCode, --����ҽ�����
         v_ReportDocName, --����ҽ������
         v_DoctorTEL, --����ҽ����ϵ�绰
         v_Report_Date, --�ʱ��
         v_State, --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
         to_char(sysdate, 'yyyy-mm-dd HH24:mi:ss'), --����ʱ��
         v_create_UserCode, --������
         v_create_UserName, --������
         v_create_deptCode, --�����˿���
         v_create_deptName, --�����˿���
         v_Modify_date, --�޸�ʱ��
         v_Modify_UserCode, --�޸���
         v_Modify_UserName, --�޸���
         v_Modify_deptCode, --�޸��˿���
         v_Modify_deptName, --�޸��˿���
         v_Audit_date, --���ʱ��
         v_Audit_UserCode, --�����
         v_Audit_UserName, --�����
         v_Audit_deptCode, --����˿���
         v_Audit_deptName, --����˿���
         v_OtherDiag);
    
      --���봫Ⱦ�����濨������ˮ
      insert into Zymosis_Report_SN
        (Report_SN_ID, --����ˮ��
         Report_ID, --��Ⱦ�����濨���
         create_date, --����ʱ��
         create_UserCode, --������
         create_UserName, --������
         create_deptCode, --�����˿���
         create_deptName, --�����˿���
         State, --�޸�����
         Memo --��ע
         )
      values
        (seq_Zymosis_Report_SN_ID.Nextval, --����ˮ��
         v_Report_ID_new, --��Ⱦ�����濨���
         to_char(sysdate, 'yyyy-mm-dd HH24:mi:ss'), --����ʱ��
         v_create_UserCode, --������
         v_create_UserName, --������
         v_create_deptCode, --�����˿���
         v_create_deptName, --�����˿���
         v_StateName, --�޸�����
         '' --��ע
         );
    
      open o_result for
        select v_Report_ID_new from dual;
    
    end if;
  
    --�޸ı��洫Ⱦ�����濨��Ϣ
    IF v_edittype = '2' THEN
    
      update zymosis_report
         set Report_NO        = nvl(v_Report_NO, Report_NO), --���濨����
             Report_Type      = nvl(v_Report_Type, Report_Type), --���濨����   1�����α���  2����������
             Noofinpat        = nvl(v_Noofinpat, Noofinpat), --��ҳ���
             PatID            = nvl(v_PatID, PatID), --סԺ��
             Name             = nvl(v_Name, Name), --��������
             ParentName       = v_ParentName, --�ҳ�����
             IDNO             = v_IDNO, --���֤����
             Sex              = nvl(v_Sex, Sex), --�����Ա�
             Birth            = v_Birth, --��������
             Age              = v_Age, --ʵ������
             Age_Unit         = v_AgeUnit, --ʵ������
             Organization     = v_Organization, --������λ
             OfficePlace      = v_OfficePlace, --��λ��ַ
             OfficeTEL        = v_OfficeTEL, --��λ�绰
             AddressType      = nvl(v_AddressType, AddressType), --�������ڵ���  1�������� 2����������������  3����ʡ��������  4����ʡ  5���۰�̨  6���⼮
             HomeTown         = v_HomeTown, --����
             Address          = v_Address, --��ϸ��ַ[�� �ֵ� ���ƺ�]
             JobID            = v_JobID, --ְҵ���루��ҳ��˳���¼��ţ�
             RecordType1      = v_RecordType1, --��������  1�����Ʋ���  2���ٴ���ϲ���  3��ʵ����ȷ�ﲡ��  4��ԭЯ����
             RecordType2      = v_RecordType2, --�������ࣨ���͸��ס�Ѫ���没��д��  1������  2������
             AttackDate       = v_AttackDate, --�������ڣ���ԭЯ������������ڻ�������ڣ�
             DiagDate         = v_DiagDate, --�������
             DieDate          = v_DieDate, --��������
             DiagICD10        = v_DiagICD10, --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
             DiagName         = v_DiagName, --��Ⱦ����������
             INFECTOTHER_FLAG = v_INFECTOTHER_FLAG, --���޸�Ⱦ������[0�� 1��]
             Memo             = v_Memo, --��ע
             Correct_flag     = v_Correct_flag, --������־��0��δ���� 1���Ѷ�����
             Correct_Name     = v_Correct_Name, --��������
             Cancel_Reason    = v_Cancel_Reason, --�˿�ԭ��
             ReportDeptCode   = v_ReportDeptCode, --������ұ��
             ReportDeptName   = v_ReportDeptName, --�����������
             ReportDocCode    = v_ReportDocCode, --����ҽ�����
             ReportDocName    = v_ReportDocName, --����ҽ������
             DoctorTEL        = v_DoctorTEL, --����ҽ����ϵ�绰
             Report_Date      = v_Report_Date, --�ʱ��
             State            = v_State, --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
             create_date      = nvl(v_create_date, create_date), --����ʱ��
             create_UserCode  = nvl(v_create_UserCode, create_UserCode), --������
             create_UserName  = nvl(v_create_UserName, create_UserName), --������
             create_deptCode  = nvl(v_create_deptCode, create_deptCode), --�����˿���
             create_deptName  = nvl(v_create_deptName, create_deptName), --�����˿���
             Modify_date      = nvl(v_Modify_date, Modify_date), --�޸�ʱ��
             Modify_UserCode  = nvl(v_Modify_UserCode, Modify_UserCode), --�޸���
             Modify_UserName  = nvl(v_Modify_UserName, Modify_UserName), --�޸���
             Modify_deptCode  = nvl(v_Modify_deptCode, Modify_deptCode), --�޸��˿���
             Modify_deptName  = nvl(v_Modify_deptName, Modify_deptName), --�޸��˿���
             Audit_date       = nvl(v_Audit_date, Audit_date), --���ʱ��
             Audit_UserCode   = nvl(v_Audit_UserCode, Audit_UserCode), --�����
             Audit_UserName   = nvl(v_Audit_UserName, Audit_UserName), --�����
             Audit_deptCode   = nvl(v_Audit_deptCode, Audit_deptCode), --����˿���
             Audit_deptName   = nvl(v_Audit_deptName, Audit_deptName), --����˿���
             OtherDiag        = v_OtherDiag
      
       where Report_ID = v_Report_ID;
    
      --���봫Ⱦ�����濨������ˮ
      insert into Zymosis_Report_SN
        (Report_SN_ID, --����ˮ��
         Report_ID, --��Ⱦ�����濨���
         create_date, --����ʱ��
         create_UserCode, --������
         create_UserName, --������
         create_deptCode, --�����˿���
         create_deptName, --�����˿���
         State, --�޸�����
         Memo --��ע
         )
      values
        (seq_Zymosis_Report_SN_ID.Nextval, --����ˮ��
         v_Report_ID, --��Ⱦ�����濨���
         to_char(sysdate, 'yyyy-mm-dd HH24:mi:ss'), --����ʱ��
         v_Modify_UserCode, --������
         v_Modify_UserName, --������
         v_Modify_deptCode, --�����˿���
         v_Modify_deptName, --�����˿���
         v_StateName, --�޸�����
         '' --��ע
         );
      open o_result for
        select v_Report_ID from dual;
    
    end if;
  
    --���ϴ�Ⱦ�����濨��Ϣ
    IF v_edittype = '3' THEN
    
      update zymosis_report
         set /*Report_NO        = nvl(v_Report_NO, Report_NO), --���濨����*/      Report_Type = nvl(v_Report_Type,
             Report_Type), --���濨����   1�����α���  2����������
             Noofinpat        = nvl(v_Noofinpat, Noofinpat), --��ҳ���
             PatID            = nvl(v_PatID, PatID), --סԺ��
             Name             = nvl(v_Name, Name), --��������
             ParentName       = v_ParentName, --�ҳ�����
             IDNO             = v_IDNO, --���֤����
             Sex              = nvl(v_Sex, Sex), --�����Ա�
             Birth            = v_Birth, --��������
             Age              = v_Age, --ʵ������
             Age_Unit         = v_AgeUnit, --ʵ������
             Organization     = v_Organization, --������λ
             OfficePlace      = v_OfficePlace, --��λ��ַ
             OfficeTEL        = v_OfficeTEL, --��λ�绰
             AddressType      = nvl(v_AddressType, AddressType), --�������ڵ���  1�������� 2����������������  3����ʡ��������  4����ʡ  5���۰�̨  6���⼮
             HomeTown         = v_HomeTown, --����
             Address          = v_Address, --��ϸ��ַ[�� �ֵ� ���ƺ�]
             JobID            = v_JobID, --ְҵ���루��ҳ��˳���¼��ţ�
             RecordType1      = v_RecordType1, --��������  1�����Ʋ���  2���ٴ���ϲ���  3��ʵ����ȷ�ﲡ��  4��ԭЯ����
             RecordType2      = v_RecordType2, --�������ࣨ���͸��ס�Ѫ���没��д��  1������  2������
             AttackDate       = v_AttackDate, --�������ڣ���ԭЯ������������ڻ�������ڣ�
             DiagDate         = v_DiagDate, --�������
             DieDate          = v_DieDate, --��������
             DiagICD10        = nvl(v_DiagICD10, DiagICD10), --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
             DiagName         = nvl(v_DiagName, DiagName), --��Ⱦ����������
             INFECTOTHER_FLAG = v_INFECTOTHER_FLAG, --���޸�Ⱦ������[0�� 1��]
             Memo             = v_Memo, --��ע
             Correct_flag     = v_Correct_flag, --������־��0��δ���� 1���Ѷ�����
             Correct_Name     = v_Correct_Name, --��������
             Cancel_Reason    = v_Cancel_Reason, --�˿�ԭ��
             ReportDeptCode   = v_ReportDeptCode, --������ұ��
             ReportDeptName   = v_ReportDeptName, --�����������
             ReportDocCode    = v_ReportDocCode, --����ҽ�����
             ReportDocName    = v_ReportDocName, --����ҽ������
             DoctorTEL        = v_DoctorTEL, --����ҽ����ϵ�绰
             Report_Date      = v_Report_Date, --�ʱ��
             State            = v_State, --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
             create_date      = nvl(v_create_date, create_date), --����ʱ��
             create_UserCode  = nvl(v_create_UserCode, create_UserCode), --������
             create_UserName  = nvl(v_create_UserName, create_UserName), --������
             create_deptCode  = nvl(v_create_deptCode, create_deptCode), --�����˿���
             create_deptName  = nvl(v_create_deptName, create_deptName), --�����˿���
             Modify_date      = nvl(v_Modify_date, Modify_date), --�޸�ʱ��
             Modify_UserCode  = nvl(v_Modify_UserCode, Modify_UserCode), --�޸���
             Modify_UserName  = nvl(v_Modify_UserName, Modify_UserName), --�޸���
             Modify_deptCode  = nvl(v_Modify_deptCode, Modify_deptCode), --�޸��˿���
             Modify_deptName  = nvl(v_Modify_deptName, Modify_deptName), --�޸��˿���
             Audit_date       = nvl(v_Audit_date, Audit_date), --���ʱ��
             Audit_UserCode   = nvl(v_Audit_UserCode, Audit_UserCode), --�����
             Audit_UserName   = nvl(v_Audit_UserName, Audit_UserName), --�����
             Audit_deptCode   = nvl(v_Audit_deptCode, Audit_deptCode), --����˿���
             Audit_deptName   = nvl(v_Audit_deptName, Audit_deptName), --����˿���
             OtherDiag        = v_OtherDiag,
             vaild            = 0
      
       where Report_ID = v_Report_ID;
    
      insert into Zymosis_Report_SN
        (Report_SN_ID, --����ˮ��
         Report_ID, --��Ⱦ�����濨���
         create_date, --����ʱ��
         create_UserCode, --������
         create_UserName, --������
         create_deptCode, --�����˿���
         create_deptName, --�����˿���
         State, --�޸�����
         Memo --��ע
         )
      values
        (seq_Zymosis_Report_SN_ID.Nextval, --����ˮ��
         v_Report_ID, --��Ⱦ�����濨���
         to_char(sysdate, 'yyyy-mm-dd HH24:mi:ss'), --����ʱ��
         v_Modify_UserCode, --������
         v_Modify_UserName, --������
         v_Modify_deptCode, --�����˿���
         v_Modify_deptName, --�����˿���
         v_StateName, --�޸�����
         '' --��ע
         );
    
      open o_result for
        select v_Report_ID from dual;
    
    end if;
  
    --���ݴ���Ĵ�Ⱦ�����濨ID��ѯ���濨��Ϣ
    IF v_edittype = '4' THEN
    
      open o_result for
        select * from zymosis_report a where a.report_id = v_Report_ID;
    
    end if;
  
  end;

  /*********************************************************************************/

  PROCEDURE usp_GetInpatientByNofinpat(v_Noofinpat varchar default '', --��ҳ���
                                       o_result    OUT empcurtyp) as
    /*
    * �ڴ�Ⱦ�����濨ģ�����������濨ʱ����ݲ�����ҳ�Ż�ȡ������Ϣ
    */
  begin
  
    open o_result for
      select inp.noofinpat,
             inp.patid,
             inp.patnoofhis,
             inp.name,
             inp.sexid,
             inp.birth,
             (case
               when instr(inp.agestr, '��') > 1 then
                replace(inp.agestr, '��')
               when instr(inp.agestr, '��') > 1 then
                replace(inp.agestr, '����')
               else
                ''
             end) age,
             (case
               when instr(inp.agestr, '��') > 1 then
                '1'
               when instr(inp.agestr, '��') > 1 then
                '2'
               else
                ''
             end) ageUint,
             inp.idno,
             inp.officeplace organization,
             inp.officeplace,
             inp.officetel,
             inp.outhosdept,
             --inp.address,--ԭ���ĵ�ַ
             inp.nativeaddress, --edit by ywk 2012��3��26��15:26:44 ��Ϊ�����ַ
             inp.attend,
             to_char(sysdate, 'yyyy-mm-dd') reportdate
        from inpatient inp
       where inp.noofinpat = v_Noofinpat;
  end;

  PROCEDURE usp_geteditzymosisreport(v_report_type1    varchar default '',
                                     v_report_type2    varchar default '',
                                     v_name            varchar default '',
                                     v_patid           varchar default '',
                                     v_deptid          varchar default '',
                                     v_applicant       varchar default '',
                                     v_status          varchar default '',
                                     v_createdatestart varchar default '', --�������ϱ����ڿ�ʼ
                                     v_createdateend   varchar default '', --�������ϱ����ڽ���
                                     v_querytype       varchar default '', --��ѯ����
                                     o_result          OUT empcurtyp) as
  begin
    if v_querytype = '1' THEN
      --��ʱ������
      open o_result for
        SELECT report_id,
               report_no,
               CASE report_type
                 WHEN '1' THEN
                  '��������'
                 ELSE
                  '��������'
               END REPORTTYPENAME,
               noofinpat,
               patid,
               NAME,
               state,
               create_date,
               create_deptcode,
               create_deptcode
          FROM zymosis_report
         WHERE ((v_report_type1 IS NOT NULL AND
               zymosis_report.report_type = '1') OR
               (v_report_type2 IS NOT NULL AND
               zymosis_report.report_type = '2'))
           AND zymosis_report.name like '%' || v_name || '%'
           AND zymosis_report.patid like '%' || v_patid || '%'
           AND zymosis_report.create_deptcode like '%' || v_deptid || '%'
           AND (v_applicant IS NULL OR
               v_applicant IS NOT NULL AND
               zymosis_report.create_usercode = v_applicant)
           AND instr(v_status, zymosis_report.state) > 0
           AND create_date between v_createdatestart || ' 00:00:00' and
               v_createdateend || ' 23:59:59'; --����Ϊʱ��� 
    end if;
  
    if v_querytype = '2' THEN
      --��ʱ������
      open o_result for
        SELECT report_id,
               report_no,
               CASE report_type
                 WHEN '1' THEN
                  '��������'
                 ELSE
                  '��������'
               END REPORTTYPENAME,
               noofinpat,
               patid,
               NAME,
               state,
               create_date,
               create_deptcode,
               create_deptcode
          FROM zymosis_report
         WHERE ((v_report_type1 IS NOT NULL AND
               zymosis_report.report_type = '1') OR
               (v_report_type2 IS NOT NULL AND
               zymosis_report.report_type = '2'))
           AND zymosis_report.name like '%' || v_name || '%'
           AND zymosis_report.patid like '%' || v_patid || '%'
           AND zymosis_report.create_deptcode like '%' || v_deptid || '%'
           AND (v_applicant IS NULL OR
               v_applicant IS NOT NULL AND
               zymosis_report.create_usercode = v_applicant)
           AND instr(v_status, zymosis_report.state) > 0;
    end if;
  
  end;

  PROCEDURE usp_getReportBrowse(v_report_type1 varchar default '',
                                v_report_type2 varchar default '',
                                v_recordtype1  varchar default '',
                                v_beginDate    varchar default '',
                                v_EndDate      varchar default '',
                                v_deptid       varchar default '',
                                v_diagICD      varchar default '',
                                v_status       varchar default '',
                                o_result       OUT empcurtyp) as
  begin
  
    open o_result for
      select rep.name,
             rep.report_id,
             rep.report_no,
             CASE report_type
               WHEN '1' THEN
                '��������'
               ELSE
                '��������'
             END REPORTTYPENAME,
             rep.sex as sexid, --add by cyq 2012-11-16
             (case
               when rep.sex = '1' then
                '��'
               when rep.sex = '2' then
                'Ů'
             end) sexstr,
             rep.birth,
             (case
               when rep.birth is not null then
                GetYearAgeByBirthAndApplyDate(to_date(rep.birth,
                                                      'yyyy-mm-dd'),
                                              to_date(rep.create_date,
                                                      'yyyy-mm-dd HH24:mi:ss'))
               when rep.birth is null then
                rep.age || (case
                  when rep.age_unit = '1' then
                   '��'
                  when rep.age_unit = '2' then
                   '��'
                  when rep.age_unit = '3' then
                   '��'
                end)
             end) agestr,
             rep.diagicd10,
             rep.diagname,
             rep.address,
             rep.reportdeptcode,
             rep.reportdeptname,
             (case
               when rep.recordtype1 = '1' then
                '���Ʋ���'
               when rep.recordtype1 = '2' then
                '�ٴ���ϲ���'
               when rep.recordtype1 = '3' then
                'ʵ����ȷ�ﲡ��'
               when rep.recordtype1 = '4' then
                '��ԭЯ����'
             end) recordtype1,
             job.jobname jobname,
             rep.create_date,
             rep.create_usercode,
             rep.create_username,
             (case
               when rep.state = '1' then
                '����'
               when rep.state = '2' then
                '�ύ'
               when rep.state = '3' then
                '����'
               when rep.state = '4' then
                '���ͨ��'
               when rep.state = '5' then
                '���δͨ������'
               when rep.state = '6' then
                '�ϱ�'
               when rep.state = '7' then
                '����'
             end) stateName
        from zymosis_report rep
        left join zymosis_job job
          on rep.jobid = job.jobid
       WHERE ((v_report_type1 IS NOT NULL AND rep.report_type = '1') OR
             (v_report_type2 IS NOT NULL AND rep.report_type = '2'))
         AND v_begindate || ' 00:00:00' <= rep.create_date
         AND rep.create_date <= v_enddate || ' 23:59:59'
         and (rep.recordtype1 = v_recordtype1 or v_recordtype1 = '' or
             v_recordtype1 is null)
         AND (rep.reportdeptcode = v_deptid or v_deptid = '' or
             v_deptid is null)
         AND (rep.diagicd10 = v_diagICD or v_diagICD = '' or
             v_diagICD is null)
         AND instr(v_status, rep.state) > 0;
  
  end;

  ---��ȡ��Ⱦ��������Ϣ
  PROCEDURE usp_GetReportAnalyse(v_beginDate varchar default '',
                                 v_EndDate   varchar default '',
                                 o_result    OUT empcurtyp) as
  
    v_sql    VARCHAR2(4000);
    v_cnt    integer;
    v_diecnt integer;
  begin
  
    v_sql := 'truncate table tmp_Zymosis_Analyse ';
  
    EXECUTE IMMEDIATE v_sql;
  
    --�������д�Ⱦ����Ϣ
    INSERT INTO tmp_Zymosis_Analyse
      (level_id, level_name, ICD_Code, Name)
      select a.level_id,
             (case
               when a.level_id = 1 then
                '���ഫȾ��'
               when a.level_id = 2 then
                '���ഫȾ��'
               when a.level_id = 3 then
                '���ഫȾ��'
               else
                '������Ⱦ��'
             end) level_Name,
             a.icd,
             a.name
        from Zymosis_Diagnosis a;
  
    --���·�����
    UPDATE tmp_Zymosis_Analyse tmp
       SET Cnt = NVL((SELECT COUNT(distinct rep.noofinpat)
                       FROM zymosis_report rep
                      WHERE rep.diagicd10 = tmp.icd_code
                        and rep.vaild = 1
                        AND v_begindate || ' 00:00:00' <= rep.create_date
                        AND rep.create_date <= v_enddate || ' 23:59:59'
                        and rep.state in (4, 6)),
                     0);
  
    --����������
    UPDATE tmp_Zymosis_Analyse tmp
       SET Die_Cnt = NVL((SELECT COUNT(distinct rep.noofinpat)
                           FROM zymosis_report rep
                          WHERE rep.diagicd10 = tmp.icd_code
                            and rep.diedate is not null
                            and rep.vaild = 1
                            AND v_begindate || ' 00:00:00' <=
                                rep.create_date
                            AND rep.create_date <= v_enddate || ' 23:59:59'
                            and rep.state in (4, 6)),
                         0);
  
    --�����������ִ�Ⱦ��
    INSERT INTO tmp_Zymosis_Analyse
      (level_id, level_name, ICD_Code, Name)
      select distinct 4, '����' level_Name, '', a.otherdiag
        from zymosis_report a
       where a.diagicd10 is null;
  
    --���·�����
    UPDATE tmp_Zymosis_Analyse tmp
       SET Cnt = NVL((SELECT COUNT(distinct rep.noofinpat)
                       FROM zymosis_report rep
                      WHERE rep.otherdiag = tmp.name
                        and rep.vaild = 1
                        and rep.diagicd10 is null
                        AND v_begindate || ' 00:00:00' <= rep.create_date
                        AND rep.create_date <= v_enddate || ' 23:59:59'
                        and rep.state in (4, 6)),
                     0)
     where tmp.icd_code is null;
  
    --����������
    UPDATE tmp_Zymosis_Analyse tmp
       SET Die_Cnt = NVL((SELECT COUNT(distinct rep.noofinpat)
                           FROM zymosis_report rep
                          WHERE rep.otherdiag = tmp.name
                            and rep.diedate is not null
                            and rep.vaild = 1
                            and rep.diagicd10 is null
                            AND v_begindate || ' 00:00:00' <=
                                rep.create_date
                            AND rep.create_date <= v_enddate || ' 23:59:59'
                            and rep.state in (4, 6)),
                         0)
     where tmp.icd_code is null;
  
    select sum(cnt) into v_cnt from tmp_Zymosis_Analyse;
    select sum(die_cnt) into v_diecnt from tmp_Zymosis_Analyse;
  
    --���·�����ռ�ܷ������ٷֱ�
    UPDATE tmp_Zymosis_Analyse tmp
       SET Attack_rate = (case
                           when v_cnt = 0 then
                            0
                           else
                            to_number(cnt) / v_cnt * 100
                         end);
  
    --����������ռ�ܷ������ٷֱ�
    UPDATE tmp_Zymosis_Analyse tmp
       SET Die_Rate = (case
                        when v_diecnt = 0 then
                         0
                        else
                         to_number(Die_Cnt) / v_diecnt * 100
                      end);
  
    commit;
  
    open o_result for
    /* select a.level_id,
                                                                                                                                         a.level_name,
                                                                                                                                         a.ICD_Code,
                                                                                                                                         a.Name,
                                                                                                                                         a.cnt,
                                                                                                                                         a.attack_rate || '%' attack_rate,
                                                                                                                                         a.die_cnt,
                                                                                                                                         a.die_rate || '%' die_rate,
                                                                                                                                         (case
                                                                                                                                           when a.cnt = 0 then
                                                                                                                                            0
                                                                                                                                           else
                                                                                                                                            a.die_cnt / a.cnt * 100
                                                                                                                                         end) dieRate
                                                                                                                                    from tmp_Zymosis_Analyse a;*/
      select *
        from (select a.level_id,
                     a.level_name,
                     a.ICD_Code,
                     a.Name,
                     a.cnt,
                     a.attack_rate,
                     a.die_cnt,
                     a.die_rate,
                     round((case
                             when a.cnt = 0 then
                              0
                             else
                              a.die_cnt / a.cnt * 100
                           end),
                           4) dieRate
                from tmp_Zymosis_Analyse a
              
              union all
              
              select a.level_id,
                     '�ܼ�' level_name,
                     '' ICD_Code,
                     'С��' Name,
                     to_char(sum(a.cnt)) cnt,
                     sum(a.attack_rate) attack_rate,
                     to_char(sum(a.die_cnt)) die_cnt,
                     sum(a.die_rate) die_rate,
                     round((case
                             when sum(a.cnt) = 0 then
                              0
                             else
                              sum(a.die_cnt) / sum(a.cnt) * 100
                           end),
                           4) dieRate
                from tmp_Zymosis_Analyse a
               group by a.level_id)
       order by level_id, level_name;
  
  end;

  --��ְҵͳ�ƴ�Ⱦ�����
  PROCEDURE usp_GetJobDisease(v_beginDate varchar default '',
                              v_EndDate   varchar default '',
                              v_DiagCode  varchar default '',
                              o_result    OUT empcurtyp) as
    v_sql     VARCHAR2(4000);
    v_jobID   varchar2(4);
    v_jobName varchar2(14);
  begin
    v_sql := 'truncate table tmp_JobDisease ';
  
    EXECUTE IMMEDIATE v_sql;
  
    --�����α�
    declare
      cursor cr_JobDisease is
        select jobID, jobName from zymosis_job;
    
    begin
      open cr_JobDisease;
      fetch cr_JobDisease
        into v_jobID, v_jobName;
      while cr_JobDisease%found loop
      
        --�α�ѭ������ֵ
        INSERT INTO tmp_JobDisease
          (JobID, JobName, DiagID, DiagName, DiseaseCnt, DieCnt)
          select v_jobID jobid, v_jobName jobName, a.icd, a.name, 0, 0
            from Zymosis_Diagnosis a
           where v_DiagCode like '%,' || a.icd || ',%';
      
        fetch cr_JobDisease
          into v_jobID, v_jobName;
      end loop;
    
      close cr_JobDisease;
    
    end;
  
    commit;
  
    --���·�����
    UPDATE tmp_JobDisease tmp
       SET DiseaseCnt = NVL((SELECT COUNT(distinct rep.noofinpat)
                              FROM zymosis_report rep
                             WHERE rep.diagicd10 = tmp.diagid
                               and rep.jobid = tmp.jobid
                               and rep.vaild = 1
                               AND v_begindate || ' 00:00:00' <=
                                   rep.create_date
                               AND rep.create_date <=
                                   v_enddate || ' 23:59:59'
                               and rep.state in (4, 6)),
                            0);
  
    --����������
    UPDATE tmp_JobDisease tmp
       SET DieCnt = NVL((SELECT COUNT(distinct rep.noofinpat)
                          FROM zymosis_report rep
                         WHERE rep.diagicd10 = tmp.diagid
                           and rep.jobid = tmp.jobid
                           and rep.diedate is not null
                           and rep.vaild = 1
                           AND v_begindate || ' 00:00:00' <= rep.create_date
                           AND rep.create_date <= v_enddate || ' 23:59:59'
                           and rep.state in (4, 6)),
                        0);
    commit;
  
    open o_result for
    --select * from tmp_JobDisease;
      select * from tmp_JobDisease order by to_number(jobid);
    /*  --�������д�Ⱦ����Ϣ
    INSERT INTO tmp_JobDisease
      (level_id, level_name, ICD_Code, Name)
      select a.level_id,
             (case
               when a.level_id = 1 then
                '���ഫȾ��'
               when a.level_id = 2 then
                '���ഫȾ��'
               when a.level_id = 3 then
                '���ഫȾ��'
               else
                '������Ⱦ��'
             end) level_Name,
             a.icd,
             a.name
        from Zymosis_Diagnosis a;*/
  end;

  --�õ�������Ч�����
  PROCEDURE usp_GetDiagnosis(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT markid, icd, name, py, wb, memo, valid, statist, name namestr
        FROM DIAGNOSIS a
       where not exists
       (select 1 from zymosis_diagnosis d where d.icd = a.icd)
       order by icd;
  END;

  --�õ�������Ч�����
  PROCEDURE usp_GetDiagnosisTo(v_categoryid varchar default '',
                               o_result     OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT markid, icd, name, py, wb, memo, valid, statist, name namestr
        FROM DIAGNOSIS a
       where not exists (select 1
                from zymosis_diagnosis d
               where d.icd = a.icd
                 and (d.categoryid = v_categoryid or
                     v_categoryid = '' or v_categoryid is null))
         and a.valid = '1'
       order by icd;
  END;

  --�õ�������Ч�����
  PROCEDURE usp_GetDiagnosisTo_ZY(v_categoryid varchar default '',
                                  o_result     OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT ID as markid,
             ID AS icd,
             name,
             py,
             wb,
             memo,
             '' as statist,
             valid,
             name namestr
        FROM diagnosisofchinese a
       where not exists (select 1
                from zymosis_diagnosis d
               where d.icd = a.id
                 and (d.categoryid = v_categoryid or
                     v_categoryid = '' or v_categoryid is null))
         and a.valid = '1'
       order by icd;
  END;

  --�õ���Ⱦ��������Ϣ
  PROCEDURE usp_GetDisease2(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT (CASE
               WHEN a.level_id = 1 THEN
                '���ഫȾ��'
               WHEN a.level_id = 2 THEN
                '���ഫȾ��'
               WHEN a.level_id = 3 THEN
                '���ഫȾ��'
               ELSE
                '������Ⱦ��'
             END) level_name,
             a.level_id,
             a.icd,
             a.NAME,
             a.py,
             a.wb,
             a.markid,
             a.memo,
             a.statist,
             CASE a.valid
               WHEN 1 THEN
                '��Ч'
               ELSE
                '��Ч'
             END valid_name,
             a.valid,
             a.namestr,
             a.upcount,
             a.fukatype
        FROM zymosis_diagnosis a
       where a.categoryid = 1
       order by a.icd;
  END;

  PROCEDURE usp_GetDisease2To(v_categoryid varchar default '',
                              o_result     OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT (CASE
               WHEN a.level_id = 1 THEN
                '���ഫȾ��'
               WHEN a.level_id = 2 THEN
                '���ഫȾ��'
               WHEN a.level_id = 3 THEN
                '���ഫȾ��'
               ELSE
                '������Ⱦ��'
             END) level_name,
             a.level_id,
             a.icd,
             a.NAME,
             a.py,
             a.wb,
             a.markid,
             a.memo,
             a.statist,
             CASE a.valid
               WHEN 1 THEN
                '��Ч'
               ELSE
                '��Ч'
             END valid_name,
             a.valid,
             a.namestr,
             a.upcount,
             a.categoryid
      
        FROM zymosis_diagnosis a
       where a.categoryid = v_categoryid
       order by a.icd;
  END;

  --���没�ּ�¼
  PROCEDURE usp_SaveZymosisDiagnosis(v_markid   varchar default '',
                                     v_icd      varchar default '',
                                     v_name     varchar default '',
                                     v_py       varchar default '',
                                     v_wb       varchar default '',
                                     v_levelID  varchar default '',
                                     v_valid    varchar default '',
                                     v_statist  varchar default '',
                                     v_memo     varchar default '',
                                     v_namestr  varchar default '',
                                     v_upcount  integer,
                                     v_fukatype varchar default '') AS
    p_count INT;
  BEGIN
    SELECT COUNT(1) INTO p_count FROM zymosis_diagnosis WHERE icd = v_icd;
  
    IF p_count > 0 THEN
      UPDATE zymosis_diagnosis
         SET markid   = v_markid,
             icd      = v_icd,
             name     = v_name,
             py       = v_py,
             wb       = v_wb,
             level_id = v_levelID,
             valid    = v_valid,
             memo     = v_memo,
             statist  = v_statist,
             namestr  = v_namestr,
             upcount  = v_upcount,
             fukatype = v_fukatype
       WHERE icd = v_icd;
    ELSE
      INSERT INTO zymosis_diagnosis
        (markid,
         icd,
         name,
         py,
         wb,
         level_id,
         valid,
         memo,
         statist,
         namestr,
         upcount,
         fukatype)
      VALUES
        (v_markid,
         v_icd,
         v_name,
         v_py,
         v_wb,
         v_levelID,
         v_valid,
         v_memo,
         v_statist,
         v_namestr,
         v_upcount,
         v_fukatype);
    END IF;
  END;

  --���没�ּ�¼
  PROCEDURE usp_SaveZymosisDiagnosisTo(v_markid     varchar default '',
                                       v_icd        varchar default '',
                                       v_name       varchar default '',
                                       v_py         varchar default '',
                                       v_wb         varchar default '',
                                       v_levelID    varchar default '',
                                       v_valid      varchar default '',
                                       v_statist    varchar default '',
                                       v_memo       varchar default '',
                                       v_namestr    varchar default '',
                                       v_upcount    integer,
                                       v_categoryid integer) AS
    p_count INT;
  BEGIN
    SELECT COUNT(1)
      INTO p_count
      FROM zymosis_diagnosis
     WHERE icd = v_icd
       and categoryid = v_categoryid;
  
    IF p_count > 0 THEN
      UPDATE zymosis_diagnosis
         SET markid     = v_markid,
             icd        = v_icd,
             name       = v_name,
             py         = v_py,
             wb         = v_wb,
             level_id   = v_levelID,
             valid      = v_valid,
             memo       = v_memo,
             statist    = v_statist,
             namestr    = v_namestr,
             upcount    = v_upcount,
             categoryid = v_categoryid
       WHERE icd = v_icd
         and categoryid = v_categoryid;
    ELSE
      INSERT INTO zymosis_diagnosis
        (markid,
         icd,
         name,
         py,
         wb,
         level_id,
         valid,
         memo,
         statist,
         namestr,
         upcount,
         categoryid)
      VALUES
        (v_markid,
         v_icd,
         v_name,
         v_py,
         v_wb,
         v_levelID,
         v_valid,
         v_memo,
         v_statist,
         v_namestr,
         v_upcount,
         v_categoryid);
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_EditTbirthdefects_Report(v_EditType         varchar,
                                         v_ID               NUMERIC DEFAULT 0,
                                         v_REPORT_NOOFINPAT varchar DEFAULT '', --���˱��
                                         v_REPORT_ID        varchar default '', --���濨���
                                         v_DIAG_CODE        varchar default '', --���濨��ϱ���
                                         
                                         v_STRING3         varchar default '', --Ԥ��
                                         v_STRING4         varchar default '', --Ԥ��
                                         v_STRING5         varchar default '', --Ԥ��
                                         v_REPORT_PROVINCE varchar default '', --�ϱ��濨ʡ��
                                         v_REPORT_CITY     varchar default '', --���濨�У��أ�
                                         v_REPORT_TOWN     varchar default '', --���濨����
                                         v_REPORT_VILLAGE  varchar default '', --���濨��
                                         v_REPORT_HOSPITAL varchar default '', --���濨ҽԺ
                                         v_REPORT_NO       varchar default '', --���濨���
                                         v_MOTHER_PATID    varchar default '', --����סԺ��
                                         v_MOTHER_NAME     varchar default '', --����
                                         v_MOTHER_AGE      varchar default '', --����
                                         v_NATIONAL        varchar default '', --����
                                         v_ADDRESS_POST    varchar default '', --��ַand�ʱ�
                                         v_PREGNANTNO      varchar default '', --�д�
                                         v_PRODUCTIONNO    varchar default '', --����
                                         v_LOCALADD        varchar default '', --��ס��
                                         
                                         v_PERCAPITAINCOME         varchar default '', --���˾�����     
                                         v_EDUCATIONLEVEL          varchar default '', --�Ļ��̶�     
                                         v_CHILD_PATID             varchar default '', --����סԺ��     
                                         v_CHILD_NAME              varchar default '', --��������     
                                         v_ISBORNHERE              varchar default '', --�Ƿ�Ժ����     
                                         v_CHILD_SEX               varchar default '', --�����Ա�      
                                         v_BORN_YEAR               varchar default '', --������     
                                         v_BORN_MONTH              varchar default '', --  ������     
                                         v_BORN_DAY                varchar default '', --������      
                                         v_GESTATIONALAGE          varchar default '', --̥��     
                                         v_WEIGHT                  varchar default '', --����     
                                         v_BIRTHS                  varchar default '', --̥��     
                                         v_ISIDENTICAL             varchar default '', --�Ƿ�ͬ��      
                                         v_OUTCOME                 varchar default '', --ת��      
                                         v_INDUCEDLABOR            varchar default '', --�Ƿ�����     
                                         v_DIAGNOSTICBASIS         varchar default '', --������ݡ����ٴ�      
                                         v_DIAGNOSTICBASIS1        varchar default '', --������ݡ���������      
                                         v_DIAGNOSTICBASIS2        varchar default '', --������ݡ���ʬ��     
                                         v_DIAGNOSTICBASIS3        varchar default '', --������ݡ����������     
                                         v_DIAGNOSTICBASIS4        varchar default '', --������ݡ���������顪������     
                                         v_DIAGNOSTICBASIS5        varchar default '', --������ݡ���Ⱦɫ��      
                                         v_DIAGNOSTICBASIS6        varchar default '', --������ݡ�������     
                                         v_DIAGNOSTICBASIS7        varchar default '', --������ݡ���������������     
                                         v_DIAG_ANENCEPHALY        varchar default '', --����ȱ����ϡ������Ի���     
                                         v_DIAG_SPINA              varchar default '', --����ȱ����ϡ���������      
                                         v_DIAG_PENGOUT            varchar default '', --����ȱ����ϡ��������      
                                         v_DIAG_HYDROCEPHALUS      varchar default '', --����ȱ����ϡ����������Ի�ˮ     
                                         v_DIAG_CLEFTPALATE        varchar default '', --����ȱ����ϡ�������     
                                         v_DIAG_CLEFTLIP           varchar default '', --����ȱ����ϡ�������      
                                         v_DIAG_CLEFTMERGER        varchar default '', --����ȱ����ϡ������Ѻϲ�����     
                                         v_DIAG_SMALLEARS          varchar default '', --����ȱ����ϡ���С���������޶���     
                                         v_DIAG_OUTEREAR           varchar default '', --����ȱ����ϡ�������������Σ�С�����޶����⣩     
                                         v_DIAG_ESOPHAGEAL         varchar default '', --����ȱ����ϡ���ʳ����������խ     
                                         v_DIAG_RECTUM             varchar default '', --����ȱ����ϡ���ֱ�����ű�������խ�������޸أ�     
                                         v_DIAG_HYPOSPADIAS        varchar default '', --����ȱ����ϡ����������     
                                         v_DIAG_BLADDER            varchar default '', --����ȱ����ϡ��������ⷭ     
                                         v_DIAG_HORSESHOEFOOTLEFT  varchar default '', --����ȱ����ϡ��������ڷ���_��      
                                         v_DIAG_HORSESHOEFOOTRIGHT varchar default '', --����ȱ����ϡ��������ڷ���_��     
                                         v_DIAG_MANYPOINTLEFT      varchar default '', --����ȱ����ϡ�����ָ��ֺ��_��      
                                         v_DIAG_MANYPOINTRIGHT     varchar default '', --����ȱ����ϡ�����ָ��ֺ��_��     
                                         v_DIAG_LIMBSUPPERLEFT     varchar default '', --����ȱ����ϡ���֫�����_��֫ _��      
                                         v_DIAG_LIMBSUPPERRIGHT    varchar default '', --����ȱ����ϡ���֫�����_��֫ _��     
                                         v_DIAG_LIMBSLOWERLEFT     varchar default '', --����ȱ����ϡ���֫�����_��֫ _��      
                                         v_DIAG_LIMBSLOWERRIGHT    varchar default '', --����ȱ����ϡ���֫�����_��֫ _��     
                                         v_DIAG_HERNIA             varchar default '', --����ȱ����ϡ�������������     
                                         v_DIAG_BULGINGBELLY       varchar default '', --����ȱ����ϡ��������     
                                         v_DIAG_GASTROSCHISIS      varchar default '', --����ȱ����ϡ�������     
                                         v_DIAG_TWINS              varchar default '', --����ȱ����ϡ�������˫̥     
                                         v_DIAG_TSSYNDROME         varchar default '', --����ȱ����ϡ��������ۺ�����21-�����ۺ�����     
                                         v_DIAG_HEARTDISEASE       varchar default '', --����ȱ����ϡ������������ಡ�����ͣ�      
                                         v_DIAG_OTHER              varchar default '', --����ȱ����ϡ���������д����������ϸ������      
                                         v_DIAG_OTHERCONTENT       varchar default '', --����ȱ����ϡ�����������      
                                         v_FEVER                   varchar default '', --���գ���38�棩      
                                         v_VIRUSINFECTION          varchar default '', --������Ⱦ     
                                         v_ILLOTHER                varchar default '', --��������     
                                         v_SULFA                   varchar default '', --�ǰ���     
                                         v_ANTIBIOTICS             varchar default '', --������     
                                         v_BIRTHCONTROLPILL        varchar default '', --����ҩ      
                                         v_SEDATIVES               varchar default '', --��ҩ     
                                         v_MEDICINEOTHER           varchar default '', --��ҩ����      
                                         v_DRINKING                varchar default '', --����     
                                         v_PESTICIDE               varchar default '', --ũҩ      
                                         v_RAY                     varchar default '', --����      
                                         v_CHEMICALAGENTS          varchar default '', --��ѧ�Ƽ�     
                                         v_FACTOROTHER             varchar default '', --�����к�����      
                                         v_STILLBIRTHNO            varchar default '', --��̥����     
                                         v_ABORTIONNO              varchar default '', --��Ȼ��������     
                                         v_DEFECTSNO               varchar default '', --ȱ�ݶ�����     
                                         v_DEFECTSOF1              varchar default '', --ȱ����1     
                                         v_DEFECTSOF2              varchar default '', --ȱ����2     
                                         v_DEFECTSOF3              varchar default '', --ȱ����3     
                                         v_YCDEFECTSOF1            varchar default '', --�Ŵ�ȱ����1     
                                         v_YCDEFECTSOF2            varchar default '', --�Ŵ�ȱ����2     
                                         v_YCDEFECTSOF3            varchar default '', --�Ŵ�ȱ����3     
                                         v_KINSHIPDEFECTS1         varchar default '', --��ȱ�ݶ���Ե��ϵ1     
                                         v_KINSHIPDEFECTS2         varchar default '', --��ȱ�ݶ���Ե��ϵ2     
                                         v_KINSHIPDEFECTS3         varchar default '', --��ȱ�ݶ���Ե��ϵ3     
                                         v_COUSINMARRIAGE          varchar default '', --���׻���ʷ      
                                         v_COUSINMARRIAGEBETWEEN   varchar default '', --���׻���ʷ��ϵ     
                                         v_PREPARER                varchar default '', --�����      
                                         v_THETITLE1               varchar default '', --�����ְ��     
                                         v_FILLDATEYEAR            varchar default '', --���������      
                                         v_FILLDATEMONTH           varchar default '', --���������     
                                         v_FILLDATEDAY             varchar default '', --���������     
                                         v_HOSPITALREVIEW          varchar default '', --ҽԺ�����      
                                         v_THETITLE2               varchar default '', --ҽԺ�����ְ��     
                                         v_HOSPITALAUDITDATEYEAR   varchar default '', --ҽԺ���������     
                                         v_HOSPITALAUDITDATEMONTH  varchar default '', --ҽԺ���������      
                                         v_HOSPITALAUDITDATEDAY    varchar default '', --ҽԺ���������      
                                         v_PROVINCIALVIEW          varchar default '', --ʡ�������      
                                         v_THETITLE3               varchar default '', --ʡ�������ְ��     
                                         v_PROVINCIALVIEWDATEYEAR  varchar default '', --ʡ�����������      
                                         v_PROVINCIALVIEWDATEMONTH varchar default '', --ʡ�����������     
                                         v_PROVINCIALVIEWDATEDAY   varchar default '', --ʡ�����������     
                                         v_FEVERNO                 varchar default '', --���ն���      
                                         v_ISVIRUSINFECTION        varchar default '', --�Ƿ񲡶���Ⱦ     
                                         v_ISDIABETES              varchar default '', --�Ƿ�����      
                                         v_ISILLOTHER              varchar default '', --�Ƿ񻼲�����     
                                         v_ISSULFA                 varchar default '', --�Ƿ�ǰ���     
                                         v_ISANTIBIOTICS           varchar default '', --�Ƿ�����     
                                         v_ISBIRTHCONTROLPILL      varchar default '', --�Ƿ����ҩ      
                                         v_ISSEDATIVES             varchar default '', --�Ƿ���ҩ     
                                         v_ISMEDICINEOTHER         varchar default '', --�Ƿ��ҩ����      
                                         v_ISDRINKING              varchar default '', --�Ƿ�����     
                                         v_ISPESTICIDE             varchar default '', --�Ƿ�ũҩ      
                                         v_ISRAY                   varchar default '', --�Ƿ�����      
                                         v_ISCHEMICALAGENTS        varchar default '', --�Ƿ�ѧ�Ƽ�     
                                         v_ISFACTOROTHER           varchar default '', --�Ƿ������к�����      
                                         v_STATE                   varchar default '', -- "����״̬�� 1���������� 2���ύ 3������ 4��?to open this dialog next """     
                                         v_CREATE_DATE             varchar default '', --����ʱ��      
                                         v_CREATE_USERCODE         varchar default '', --������     
                                         v_CREATE_USERNAME         varchar default '', ---������      
                                         v_CREATE_DEPTCODE         varchar default '', --�����˿���     
                                         v_CREATE_DEPTNAME         varchar default '', --�����˿���     
                                         v_MODIFY_DATE             varchar default '', --�޸�ʱ��      
                                         v_MODIFY_USERCODE         varchar default '', --�޸���     
                                         v_MODIFY_USERNAME         varchar default '', --�޸���     
                                         v_MODIFY_DEPTCODE         varchar default '', --�޸��˿���     
                                         v_MODIFY_DEPTNAME         varchar default '', --�޸��˿���     
                                         v_AUDIT_DATE              varchar default '', --���ʱ��     
                                         v_AUDIT_USERCODE          varchar default '', --�����      
                                         v_AUDIT_USERNAME          varchar default '', --�����      
                                         v_AUDIT_DEPTCODE          varchar default '', --����˿���      
                                         v_AUDIT_DEPTNAME          varchar default '', --����˿���      
                                         v_VAILD                   varchar default '', --״̬�Ƿ���Ч  1����Ч   0����Ч     
                                         v_CANCELREASON            varchar default '', --���ԭ��     
                                         v_PRENATAL                varchar default '', --��ǰ     
                                         v_PRENATALNO              varchar default '', --��ǰ����     
                                         v_POSTPARTUM              varchar default '', --����     
                                         v_ANDTOSHOWLEFT           varchar default '', --��ָ��     
                                         v_ANDTOSHOWRIGHT          varchar default '', --��ָ��
                                         o_result                  OUT empcurtyp) AS
  
    v_ID_new int;
  BEGIN
  
    --������Ⱦ�����濨
    IF v_edittype = '1' THEN
    
      select SEQ_BIRTHDEFECTS.Nextval into v_ID_new from dual;
    
      insert into BIRTHDEFECTSCARD
        (ID, --���
         REPORT_NOOFINPAT, --������ҳ���
         REPORT_ID, --  ���濨���
         DIAG_CODE, --  ���ֱ��
         STRING3, --  Ԥ��4
         STRING4, --  Ԥ��5
         STRING5, --  Ԥ��6
         REPORT_PROVINCE, --  ���濨ʡ��
         REPORT_CITY, --  ���濨�У��أ�
         REPORT_TOWN, --  ���濨����-----10
         REPORT_VILLAGE, -- ���濨��
         REPORT_HOSPITAL, --  ���濨ҽԺ
         REPORT_NO, --���Ϸ��������
         MOTHER_PATID, -- ����סԺ��
         MOTHER_NAME, --  ����
         MOTHER_AGE, -- ����
         NATIONAL, -- ����
         ADDRESS_POST, -- ��ַand�ʱ�
         PREGNANTNO, -- �д�
         PRODUCTIONNO, -- ����-----20
         LOCALADD, -- ��ס��
         PERCAPITAINCOME, --  ���˾�����
         EDUCATIONLEVEL, -- �Ļ��̶�
         CHILD_PATID, --  ����סԺ��
         CHILD_NAME, -- ��������
         ISBORNHERE, -- �Ƿ�Ժ����
         CHILD_SEX, --�����Ա�
         BORN_YEAR, --  ������
         BORN_MONTH, -- ������
         BORN_DAY, -- ������--30
         
         GESTATIONALAGE, -- ̥��
         WEIGHT, -- ����
         BIRTHS, -- ̥��
         ISIDENTICAL, --  �Ƿ�ͬ��
         OUTCOME, --  ת��
         INDUCEDLABOR, -- �Ƿ�����
         DIAGNOSTICBASIS, --  ������ݡ����ٴ�
         DIAGNOSTICBASIS1, -- ������ݡ���������
         DIAGNOSTICBASIS2, -- ������ݡ���ʬ��
         DIAGNOSTICBASIS3, -- ������ݡ����������--40
         
         DIAGNOSTICBASIS4, -- ������ݡ���������顪������
         DIAGNOSTICBASIS5, -- ������ݡ���Ⱦɫ��
         DIAGNOSTICBASIS6, -- ������ݡ�������
         DIAGNOSTICBASIS7, -- ������ݡ���������������
         DIAG_ANENCEPHALY, -- ����ȱ����ϡ������Ի���
         DIAG_SPINA, -- ����ȱ����ϡ���������
         DIAG_PENGOUT, -- ����ȱ����ϡ��������
         DIAG_HYDROCEPHALUS, -- ����ȱ����ϡ����������Ի�ˮ
         DIAG_CLEFTPALATE, -- ����ȱ����ϡ�������
         DIAG_CLEFTLIP, --  ����ȱ����ϡ�������--50
         
         DIAG_CLEFTMERGER, -- ����ȱ����ϡ������Ѻϲ�����
         DIAG_SMALLEARS, -- ����ȱ����ϡ���С���������޶���
         DIAG_OUTEREAR, --  ����ȱ����ϡ�������������Σ�С�����޶����⣩
         DIAG_ESOPHAGEAL, --  ����ȱ����ϡ���ʳ����������խ
         DIAG_RECTUM, --  ����ȱ����ϡ���ֱ�����ű�������խ�������޸أ�
         DIAG_HYPOSPADIAS, -- ����ȱ����ϡ����������
         DIAG_BLADDER, -- ����ȱ����ϡ��������ⷭ
         DIAG_HORSESHOEFOOTLEFT, -- ����ȱ����ϡ��������ڷ���_�� 
         DIAG_HORSESHOEFOOTRIGHT, --  ����ȱ����ϡ��������ڷ���_��
         DIAG_MANYPOINTLEFT, -- ����ȱ����ϡ�����ָ��ֺ��_��--60
         
         DIAG_MANYPOINTRIGHT, --  ����ȱ����ϡ�����ָ��ֺ��_��
         DIAG_LIMBSUPPERLEFT, --  ����ȱ����ϡ���֫�����_��֫ _��
         DIAG_LIMBSUPPERRIGHT, -- ����ȱ����ϡ���֫�����_��֫ _��
         DIAG_LIMBSLOWERLEFT, --  ����ȱ����ϡ���֫�����_��֫ _��
         DIAG_LIMBSLOWERRIGHT, -- ����ȱ����ϡ���֫�����_��֫ _��
         DIAG_HERNIA, --  ����ȱ����ϡ�������������
         DIAG_BULGINGBELLY, --  ����ȱ����ϡ��������
         DIAG_GASTROSCHISIS, -- ����ȱ����ϡ�������
         DIAG_TWINS, -- ����ȱ����ϡ�������˫̥
         DIAG_TSSYNDROME, --  ����ȱ����ϡ��������ۺ�����21-�����ۺ�����--70
         
         DIAG_HEARTDISEASE, --  ����ȱ����ϡ������������ಡ�����ͣ�
         DIAG_OTHER, -- ����ȱ����ϡ���������д����������ϸ������
         DIAG_OTHERCONTENT, --  ����ȱ����ϡ�����������
         FEVER, --  ���գ���38�棩
         VIRUSINFECTION, -- ������Ⱦ
         ILLOTHER, -- ��������
         SULFA, --  �ǰ���
         ANTIBIOTICS, --  ������
         BIRTHCONTROLPILL, -- ����ҩ
         SEDATIVES, --  ��ҩ
         
         MEDICINEOTHER, --  ��ҩ����
         DRINKING, -- ����
         PESTICIDE, --  ũҩ
         RAY, --  ����
         CHEMICALAGENTS, -- ��ѧ�Ƽ�
         FACTOROTHER, --  �����к�����
         STILLBIRTHNO, -- ��̥����
         ABORTIONNO, -- ��Ȼ��������
         DEFECTSNO, --  ȱ�ݶ�����
         DEFECTSOF1, -- ȱ����1--90
         
         DEFECTSOF2, -- ȱ����2
         DEFECTSOF3, -- ȱ����3
         YCDEFECTSOF1, -- �Ŵ�ȱ����1
         YCDEFECTSOF2, -- �Ŵ�ȱ����2
         YCDEFECTSOF3, -- �Ŵ�ȱ����3
         KINSHIPDEFECTS1, --  ��ȱ�ݶ���Ե��ϵ1
         KINSHIPDEFECTS2, --  ��ȱ�ݶ���Ե��ϵ2
         KINSHIPDEFECTS3, --  ��ȱ�ݶ���Ե��ϵ3
         COUSINMARRIAGE, -- ���׻���ʷ
         COUSINMARRIAGEBETWEEN, --  ���׻���ʷ��ϵ
         PREPARER, -- �����
         THETITLE1, --  �����ְ��
         FILLDATEYEAR, -- ���������
         FILLDATEMONTH, --  ���������
         FILLDATEDAY, --  ���������
         HOSPITALREVIEW, -- ҽԺ�����
         THETITLE2, --  ҽԺ�����ְ��
         HOSPITALAUDITDATEYEAR, --  ҽԺ���������
         HOSPITALAUDITDATEMONTH, -- ҽԺ���������
         HOSPITALAUDITDATEDAY, -- ҽԺ���������
         PROVINCIALVIEW, -- ʡ�������
         THETITLE3, --  ʡ�������ְ��
         PROVINCIALVIEWDATEYEAR, -- ʡ�����������
         PROVINCIALVIEWDATEMONTH, --  ʡ�����������
         PROVINCIALVIEWDATEDAY, --  ʡ�����������
         FEVERNO, --  ���ն���
         ISVIRUSINFECTION, -- �Ƿ񲡶���Ⱦ
         ISDIABETES, -- �Ƿ�����
         ISILLOTHER, -- �Ƿ񻼲�����
         ISSULFA, --  �Ƿ�ǰ���--120
         
         ISANTIBIOTICS, --  �Ƿ�����
         ISBIRTHCONTROLPILL, -- �Ƿ����ҩ
         ISSEDATIVES, --  �Ƿ���ҩ
         ISMEDICINEOTHER, --  �Ƿ��ҩ����
         ISDRINKING, -- �Ƿ�����
         ISPESTICIDE, --  �Ƿ�ũҩ
         ISRAY, --  �Ƿ�����
         ISCHEMICALAGENTS, -- �Ƿ�ѧ�Ƽ�
         ISFACTOROTHER, --  �Ƿ������к�����
         STATE, --  "����״̬�� 1���������� 2���ύ 3������ 4��?to open this dialog next """
         CREATE_DATE, --  ����ʱ��
         CREATE_USERCODE, --  ������
         CREATE_USERNAME, --  ������
         CREATE_DEPTCODE, --  �����˿���
         CREATE_DEPTNAME, --  �����˿���
         MODIFY_DATE, --  �޸�ʱ��
         MODIFY_USERCODE, --  �޸���
         MODIFY_USERNAME, --  �޸���
         MODIFY_DEPTCODE, --  �޸��˿���
         MODIFY_DEPTNAME, --  �޸��˿���
         AUDIT_DATE, -- ���ʱ��
         AUDIT_USERCODE, -- �����
         AUDIT_USERNAME, -- �����
         AUDIT_DEPTCODE, -- ����˿���
         AUDIT_DEPTNAME, -- ����˿���
         VAILD, --  ״̬�Ƿ���Ч  1����Ч   0����Ч
         CANCELREASON, -- ���ԭ��
         PRENATAL, -- ��ǰ
         PRENATALNO, -- ��ǰ����
         POSTPARTUM, -- ����--150
         
         ANDTOSHOWLEFT, --  ��ָ��
         ANDTOSHOWRIGHT) --��ָ��
      
      values
        (v_ID_new, --���
         v_REPORT_NOOFINPAT,
         v_Report_ID,
         v_DIAG_CODE, --���濨��ϱ���
         v_STRING3, --Ԥ��
         v_STRING4, --Ԥ��
         v_STRING5, --Ԥ��
         v_REPORT_PROVINCE, --�ϱ��濨ʡ��
         v_REPORT_CITY, --���濨�У��أ�
         v_REPORT_TOWN, --���濨����--------10
         v_REPORT_VILLAGE, --���濨��
         v_REPORT_HOSPITAL, --���濨ҽԺ
         v_REPORT_NO, --���濨���
         v_MOTHER_PATID, --����סԺ��
         v_MOTHER_NAME, --����
         v_MOTHER_AGE, --����
         v_NATIONAL, --����
         v_ADDRESS_POST, --��ַand�ʱ�
         v_PREGNANTNO, --�д�
         v_PRODUCTIONNO, --����----------=20
         v_LOCALADD, --��ס��
         v_PERCAPITAINCOME, --  ���˾�����
         v_EDUCATIONLEVEL, -- �Ļ��̶�
         v_CHILD_PATID, --  ����סԺ��
         v_CHILD_NAME, -- ��������
         v_ISBORNHERE, --�Ƿ�Ժ����
         v_CHILD_SEX, --�����Ա�
         v_BORN_YEAR, --������
         v_BORN_MONTH, -- ������
         v_BORN_DAY, --������---------------30
         v_GESTATIONALAGE, --̥��
         v_WEIGHT, --����
         v_BIRTHS, --̥��
         v_ISIDENTICAL, --�Ƿ�ͬ��
         v_OUTCOME, --ת��
         v_INDUCEDLABOR, --�Ƿ�����
         v_DIAGNOSTICBASIS, --������ݡ����ٴ�
         v_DIAGNOSTICBASIS1, -- ������ݡ���������
         v_DIAGNOSTICBASIS2, --������ݡ���ʬ��
         v_DIAGNOSTICBASIS3, --������ݡ����������
         v_DIAGNOSTICBASIS4, --������ݡ���������顪������
         v_DIAGNOSTICBASIS5, --������ݡ���Ⱦɫ��
         v_DIAGNOSTICBASIS6, --������ݡ�������
         v_DIAGNOSTICBASIS7, --������ݡ���������������
         v_DIAG_ANENCEPHALY, --����ȱ����ϡ������Ի���
         v_DIAG_SPINA, --����ȱ����ϡ���������
         v_DIAG_PENGOUT, --����ȱ����ϡ��������
         v_DIAG_HYDROCEPHALUS, --����ȱ����ϡ����������Ի�ˮ
         v_DIAG_CLEFTPALATE, --����ȱ����ϡ�������
         v_DIAG_CLEFTLIP, --����ȱ����ϡ�������------------50
         v_DIAG_CLEFTMERGER, --����ȱ����ϡ������Ѻϲ�����
         v_DIAG_SMALLEARS, --����ȱ����ϡ���С���������޶���
         v_DIAG_OUTEREAR, --����ȱ����ϡ�������������Σ�С�����޶����⣩
         v_DIAG_ESOPHAGEAL, --����ȱ����ϡ���ʳ����������խ
         v_DIAG_RECTUM, --����ȱ����ϡ���ֱ�����ű�������խ�������޸أ�
         v_DIAG_HYPOSPADIAS, --����ȱ����ϡ����������
         v_DIAG_BLADDER, --����ȱ����ϡ��������ⷭ
         v_DIAG_HORSESHOEFOOTLEFT, --����ȱ����ϡ��������ڷ���_�� 
         v_DIAG_HORSESHOEFOOTRIGHT, --����ȱ����ϡ��������ڷ���_��
         v_DIAG_MANYPOINTLEFT, --����ȱ����ϡ�����ָ��ֺ��_��
         v_DIAG_MANYPOINTRIGHT, --����ȱ����ϡ�����ָ��ֺ��_��
         v_DIAG_LIMBSUPPERLEFT, --����ȱ����ϡ���֫�����_��֫ _��
         v_DIAG_LIMBSUPPERRIGHT, --����ȱ����ϡ���֫�����_��֫ _��
         v_DIAG_LIMBSLOWERLEFT, --  ����ȱ����ϡ���֫�����_��֫ _��
         v_DIAG_LIMBSLOWERRIGHT, -- ����ȱ����ϡ���֫�����_��֫ _��
         v_DIAG_HERNIA, --  ����ȱ����ϡ�������������
         v_DIAG_BULGINGBELLY, --����ȱ����ϡ��������
         v_DIAG_GASTROSCHISIS, --����ȱ����ϡ�������
         v_DIAG_TWINS, --����ȱ����ϡ�������˫̥
         v_DIAG_TSSYNDROME, --����ȱ����ϡ��������ۺ�����21-�����ۺ�����
         v_DIAG_HEARTDISEASE, --����ȱ����ϡ������������ಡ�����ͣ�
         v_DIAG_OTHER, --����ȱ����ϡ���������д����������ϸ������
         v_DIAG_OTHERCONTENT, --����ȱ����ϡ�����������
         v_FEVER, --���գ���38�棩
         v_VIRUSINFECTION, --������Ⱦ
         v_ILLOTHER, --��������
         v_SULFA, --�ǰ���
         v_ANTIBIOTICS, --������
         v_BIRTHCONTROLPILL, -- ����ҩ
         v_SEDATIVES, --��ҩ---------------80
         v_MEDICINEOTHER, --��ҩ����
         v_DRINKING, -- ����
         v_PESTICIDE, --  ũҩ
         v_RAY, --����
         v_CHEMICALAGENTS, --��ѧ�Ƽ�
         v_FACTOROTHER, --�����к�����
         v_STILLBIRTHNO, -- ��̥����
         v_ABORTIONNO, --��Ȼ��������
         v_DEFECTSNO, --ȱ�ݶ�����
         v_DEFECTSOF1, --ȱ����1--------------90
         v_DEFECTSOF2, --ȱ����2
         v_DEFECTSOF3, --ȱ����3
         v_YCDEFECTSOF1, --�Ŵ�ȱ����1
         v_YCDEFECTSOF2, -- �Ŵ�ȱ����2
         v_YCDEFECTSOF3, --�Ŵ�ȱ����3
         v_KINSHIPDEFECTS1, --��ȱ�ݶ���Ե��ϵ1
         v_KINSHIPDEFECTS2, --��ȱ�ݶ���Ե��ϵ2
         v_KINSHIPDEFECTS3, --��ȱ�ݶ���Ե��ϵ3
         v_COUSINMARRIAGE, --���׻���ʷ
         v_COUSINMARRIAGEBETWEEN, --���׻���ʷ��ϵ
         v_PREPARER, --�����
         v_THETITLE1, --�����ְ��
         v_FILLDATEYEAR, --���������
         v_FILLDATEMONTH, --���������
         v_FILLDATEDAY, --���������
         v_HOSPITALREVIEW, --ҽԺ�����
         v_THETITLE2, --ҽԺ�����ְ��
         v_HOSPITALAUDITDATEYEAR, --ҽԺ���������
         v_HOSPITALAUDITDATEMONTH, --ҽԺ���������
         v_HOSPITALAUDITDATEDAY, --ҽԺ���������
         v_PROVINCIALVIEW, --ʡ�������
         v_THETITLE3, --ʡ�������ְ��
         v_PROVINCIALVIEWDATEYEAR, --ʡ�����������
         v_PROVINCIALVIEWDATEMONTH, --ʡ�����������
         v_PROVINCIALVIEWDATEDAY, --ʡ�����������
         v_FEVERNO, --���ն���
         v_ISVIRUSINFECTION, --�Ƿ񲡶���Ⱦ
         v_ISDIABETES, --�Ƿ�����
         v_ISILLOTHER, --�Ƿ񻼲�����
         v_ISSULFA, --�Ƿ�ǰ���----------------120
         v_ISANTIBIOTICS, --�Ƿ�����
         v_ISBIRTHCONTROLPILL, --�Ƿ����ҩ
         v_ISSEDATIVES, --�Ƿ���ҩ
         v_ISMEDICINEOTHER, --�Ƿ��ҩ����
         v_ISDRINKING, --�Ƿ�����
         v_ISPESTICIDE, --�Ƿ�ũҩ
         v_ISRAY, --�Ƿ�����
         v_ISCHEMICALAGENTS, --�Ƿ�ѧ�Ƽ�
         v_ISFACTOROTHER, --�Ƿ������к�����
         v_STATE, --"����״̬�� 1���������� 2���ύ 3������ 4��?to open this dialog next """
         v_CREATE_DATE, --����ʱ��
         v_CREATE_USERCODE, --������
         v_CREATE_USERNAME, --������
         v_CREATE_DEPTCODE, --�����˿���
         v_CREATE_DEPTNAME, --�����˿���
         v_MODIFY_DATE, --�޸�ʱ��
         v_MODIFY_USERCODE, --�޸���
         v_MODIFY_USERNAME, --�޸���
         v_MODIFY_DEPTCODE, --�޸��˿���
         v_MODIFY_DEPTNAME, --�޸��˿���
         v_AUDIT_DATE, --���ʱ��
         v_AUDIT_USERCODE, --�����
         v_AUDIT_USERNAME, --�����
         v_AUDIT_DEPTCODE, -- ����˿���
         v_AUDIT_DEPTNAME, --����˿���
         v_VAILD, --״̬�Ƿ���Ч  1����Ч   0����Ч
         v_CANCELREASON, --���ԭ��
         v_PRENATAL, --��ǰ
         v_PRENATALNO, --��ǰ����
         v_POSTPARTUM, --����--------------150
         v_ANDTOSHOWLEFT, --��ָ��
         v_ANDTOSHOWRIGHT); --��ָ��
    
      open o_result for
        select v_ID_new from dual;
    
    end if;
  
    --�޸ı��洫Ⱦ�����濨��Ϣ
    IF v_edittype = '2' THEN
    
      update BIRTHDEFECTSCARD
         set REPORT_NOOFINPAT        = v_REPORT_NOOFINPAT,
             REPORT_ID               = v_Report_ID, --���濨���        
             DIAG_CODE               = v_DIAG_CODE,
             STRING3                 = v_STRING3,
             STRING4                 = v_STRING4,
             STRING5                 = v_STRING5,
             REPORT_PROVINCE         = v_REPORT_PROVINCE, --���濨ʡ��
             REPORT_CITY             = v_REPORT_CITY, --���濨�У��أ�
             REPORT_TOWN             = v_REPORT_TOWN, --���濨����
             REPORT_VILLAGE          = v_REPORT_VILLAGE, --���濨��
             REPORT_HOSPITAL         = v_REPORT_HOSPITAL, --���濨ҽԺ
             REPORT_NO               = v_REPORT_NO, --���Ϸ��������
             MOTHER_PATID            = v_MOTHER_PATID, --����סԺ��
             MOTHER_NAME             = v_MOTHER_NAME, --����
             MOTHER_AGE              = v_MOTHER_AGE, --����
             NATIONAL                = v_NATIONAL, --����
             ADDRESS_POST            = v_ADDRESS_POST, --��ַand�ʱ�
             PREGNANTNO              = v_PREGNANTNO, --�д�
             PRODUCTIONNO            = v_PRODUCTIONNO, --����
             LOCALADD                = v_LOCALADD, --��ס��
             PERCAPITAINCOME         = v_PERCAPITAINCOME, --���˾�����
             EDUCATIONLEVEL          = v_EDUCATIONLEVEL, --�Ļ��̶�
             CHILD_PATID             = v_CHILD_PATID, --����סԺ��
             CHILD_NAME              = v_CHILD_NAME, --  ��������
             ISBORNHERE              = v_ISBORNHERE, --�Ƿ�Ժ����
             CHILD_SEX               = v_CHILD_SEX, --�����Ա�
             BORN_YEAR               = v_BORN_YEAR, --������
             BORN_MONTH              = v_BORN_MONTH, --������
             BORN_DAY                = v_BORN_DAY, --������
             GESTATIONALAGE          = v_GESTATIONALAGE, --  ̥��
             WEIGHT                  = v_WEIGHT, --����
             BIRTHS                  = v_BIRTHS, --̥��
             ISIDENTICAL             = v_ISIDENTICAL, --�Ƿ�ͬ��
             OUTCOME                 = v_OUTCOME, --ת��
             INDUCEDLABOR            = v_INDUCEDLABOR, --�Ƿ�����
             DIAGNOSTICBASIS         = v_DIAGNOSTICBASIS, --������ݡ����ٴ�
             DIAGNOSTICBASIS1        = v_DIAGNOSTICBASIS1, --������ݡ���������
             DIAGNOSTICBASIS2        = v_DIAGNOSTICBASIS2, --������ݡ���ʬ��
             DIAGNOSTICBASIS3        = v_DIAGNOSTICBASIS3, --������ݡ����������
             DIAGNOSTICBASIS4        = v_DIAGNOSTICBASIS4, --������ݡ���������顪������
             DIAGNOSTICBASIS5        = v_DIAGNOSTICBASIS5, --������ݡ���Ⱦɫ��
             DIAGNOSTICBASIS6        = v_DIAGNOSTICBASIS6, --������ݡ�������
             DIAGNOSTICBASIS7        = v_DIAGNOSTICBASIS7, --������ݡ���������������
             DIAG_ANENCEPHALY        = v_DIAG_ANENCEPHALY, --����ȱ����ϡ������Ի���
             DIAG_SPINA              = v_DIAG_SPINA, --����ȱ����ϡ���������
             DIAG_PENGOUT            = v_DIAG_PENGOUT, --����ȱ����ϡ��������
             DIAG_HYDROCEPHALUS      = v_DIAG_HYDROCEPHALUS, --����ȱ����ϡ����������Ի�ˮ
             DIAG_CLEFTPALATE        = v_DIAG_CLEFTPALATE, --����ȱ����ϡ�������
             DIAG_CLEFTLIP           = v_DIAG_CLEFTLIP, --����ȱ����ϡ�������
             DIAG_CLEFTMERGER        = v_DIAG_CLEFTMERGER, --����ȱ����ϡ������Ѻϲ�����
             DIAG_SMALLEARS          = v_DIAG_SMALLEARS, --����ȱ����ϡ���С���������޶���
             DIAG_OUTEREAR           = v_DIAG_OUTEREAR, --����ȱ����ϡ�������������Σ�С�����޶����⣩
             DIAG_ESOPHAGEAL         = v_DIAG_ESOPHAGEAL, --����ȱ����ϡ���ʳ����������խ
             DIAG_RECTUM             = v_DIAG_RECTUM, --����ȱ����ϡ���ֱ�����ű�������խ�������޸أ�
             DIAG_HYPOSPADIAS        = v_DIAG_HYPOSPADIAS, --����ȱ����ϡ����������
             DIAG_BLADDER            = v_DIAG_BLADDER, --����ȱ����ϡ��������ⷭ
             DIAG_HORSESHOEFOOTLEFT  = v_DIAG_HORSESHOEFOOTLEFT, --����ȱ����ϡ��������ڷ���_�� 
             DIAG_HORSESHOEFOOTRIGHT = v_DIAG_HORSESHOEFOOTRIGHT, --  ����ȱ����ϡ��������ڷ���_��
             DIAG_MANYPOINTLEFT      = v_DIAG_MANYPOINTLEFT, --����ȱ����ϡ�����ָ��ֺ��_��
             DIAG_MANYPOINTRIGHT     = v_DIAG_MANYPOINTRIGHT, --����ȱ����ϡ�����ָ��ֺ��_��
             DIAG_LIMBSUPPERLEFT     = v_DIAG_LIMBSUPPERLEFT, --����ȱ����ϡ���֫�����_��֫ _��
             DIAG_LIMBSUPPERRIGHT    = v_DIAG_LIMBSUPPERRIGHT, --����ȱ����ϡ���֫�����_��֫ _��
             DIAG_LIMBSLOWERLEFT     = v_DIAG_LIMBSLOWERLEFT, --����ȱ����ϡ���֫�����_��֫ _��
             DIAG_LIMBSLOWERRIGHT    = v_DIAG_LIMBSLOWERRIGHT, ---����ȱ����ϡ���֫�����_��֫ _��
             DIAG_HERNIA             = v_DIAG_HERNIA, --����ȱ����ϡ�������������
             DIAG_BULGINGBELLY       = v_DIAG_BULGINGBELLY, --����ȱ����ϡ��������
             DIAG_GASTROSCHISIS      = v_DIAG_GASTROSCHISIS, --����ȱ����ϡ�������
             DIAG_TWINS              = v_DIAG_TWINS, --����ȱ����ϡ�������˫̥
             DIAG_TSSYNDROME         = v_DIAG_TSSYNDROME, --����ȱ����ϡ��������ۺ�����21-�����ۺ�����
             DIAG_HEARTDISEASE       = v_DIAG_HEARTDISEASE, --����ȱ����ϡ������������ಡ�����ͣ�
             DIAG_OTHER              = v_DIAG_OTHER, --����ȱ����ϡ���������д����������ϸ������
             DIAG_OTHERCONTENT       = v_DIAG_OTHERCONTENT, --����ȱ����ϡ�����������
             FEVER                   = v_FEVER, --���գ���38�棩
             VIRUSINFECTION          = v_VIRUSINFECTION, --������Ⱦ
             ILLOTHER                = v_ILLOTHER, --��������
             SULFA                   = v_SULFA, --�ǰ���
             ANTIBIOTICS             = v_ANTIBIOTICS, --������
             BIRTHCONTROLPILL        = v_BIRTHCONTROLPILL, --����ҩ
             SEDATIVES               = v_SEDATIVES, --��ҩ
             MEDICINEOTHER           = v_MEDICINEOTHER, --��ҩ����
             DRINKING                = v_DRINKING, --����
             PESTICIDE               = v_PESTICIDE, --ũҩ
             RAY                     = v_RAY, --����
             CHEMICALAGENTS          = v_CHEMICALAGENTS, --  ��ѧ�Ƽ�
             FACTOROTHER             = v_FACTOROTHER, --�����к�����
             STILLBIRTHNO            = v_STILLBIRTHNO, --  ��̥����
             ABORTIONNO              = v_ABORTIONNO, --��Ȼ��������
             DEFECTSNO               = v_DEFECTSNO, --ȱ�ݶ�����
             DEFECTSOF1              = v_DEFECTSOF1, --ȱ����1
             DEFECTSOF2              = v_DEFECTSOF2, --ȱ����2
             DEFECTSOF3              = v_DEFECTSOF3, --ȱ����3
             YCDEFECTSOF1            = v_YCDEFECTSOF1, --�Ŵ�ȱ����1
             YCDEFECTSOF2            = v_YCDEFECTSOF2, --�Ŵ�ȱ����2
             YCDEFECTSOF3            = v_YCDEFECTSOF3, --�Ŵ�ȱ����3
             KINSHIPDEFECTS1         = v_KINSHIPDEFECTS1, --��ȱ�ݶ���Ե��ϵ1
             KINSHIPDEFECTS2         = v_KINSHIPDEFECTS2, --��ȱ�ݶ���Ե��ϵ2
             KINSHIPDEFECTS3         = v_KINSHIPDEFECTS3, --��ȱ�ݶ���Ե��ϵ3
             COUSINMARRIAGE          = v_COUSINMARRIAGE, --���׻���ʷ
             COUSINMARRIAGEBETWEEN   = v_COUSINMARRIAGEBETWEEN, --���׻���ʷ��ϵ
             PREPARER                = v_PREPARER, --�����
             THETITLE1               = v_THETITLE1, --�����ְ��
             FILLDATEYEAR            = v_FILLDATEYEAR, --���������
             FILLDATEMONTH           = v_FILLDATEMONTH, --���������
             FILLDATEDAY             = v_FILLDATEDAY, --���������
             HOSPITALREVIEW          = v_HOSPITALREVIEW, --ҽԺ�����
             THETITLE2               = v_THETITLE2, --ҽԺ�����ְ��
             HOSPITALAUDITDATEYEAR   = v_HOSPITALAUDITDATEYEAR, --ҽԺ���������
             HOSPITALAUDITDATEMONTH  = v_HOSPITALAUDITDATEMONTH, --ҽԺ���������
             HOSPITALAUDITDATEDAY    = v_HOSPITALAUDITDATEDAY, --ҽԺ���������
             PROVINCIALVIEW          = v_PROVINCIALVIEW, --ʡ�������
             THETITLE3               = v_THETITLE3, --ʡ�������ְ��
             PROVINCIALVIEWDATEYEAR  = v_PROVINCIALVIEWDATEYEAR, --ʡ�����������
             PROVINCIALVIEWDATEMONTH = v_PROVINCIALVIEWDATEMONTH, --ʡ�����������
             PROVINCIALVIEWDATEDAY   = v_PROVINCIALVIEWDATEDAY, --  ʡ�����������
             FEVERNO                 = v_FEVERNO, --���ն���
             ISVIRUSINFECTION        = v_ISVIRUSINFECTION, --�Ƿ񲡶���Ⱦ
             ISDIABETES              = v_ISDIABETES, --�Ƿ�����
             ISILLOTHER              = v_ISILLOTHER, --�Ƿ񻼲�����
             ISSULFA                 = v_ISSULFA, --�Ƿ�ǰ���
             ISANTIBIOTICS           = v_ISANTIBIOTICS, --�Ƿ�����
             ISBIRTHCONTROLPILL      = v_ISBIRTHCONTROLPILL, --�Ƿ����ҩ
             ISSEDATIVES             = v_ISSEDATIVES, --�Ƿ���ҩ
             ISMEDICINEOTHER         = v_ISMEDICINEOTHER, --�Ƿ��ҩ����
             ISDRINKING              = v_ISDRINKING, --�Ƿ�����
             ISPESTICIDE             = v_ISPESTICIDE, --�Ƿ�ũҩ
             ISRAY                   = v_ISRAY, --�Ƿ�����
             ISCHEMICALAGENTS        = v_ISCHEMICALAGENTS, --�Ƿ�ѧ�Ƽ�
             ISFACTOROTHER           = v_ISFACTOROTHER, --�Ƿ������к�����
             STATE                   = v_STATE, --"����״̬�� 1���������� 2���ύ 3������ 4��?to open this dialog next """
             CREATE_DATE             = v_CREATE_DATE, --����ʱ��
             CREATE_USERCODE         = v_CREATE_USERCODE, --������
             CREATE_USERNAME         = v_CREATE_USERNAME, --������
             CREATE_DEPTCODE         = v_CREATE_DEPTCODE, --�����˿���
             CREATE_DEPTNAME         = v_CREATE_DEPTNAME, --�����˿���
             MODIFY_DATE             = v_MODIFY_DATE, --�޸�ʱ��
             MODIFY_USERCODE         = v_MODIFY_USERCODE, --�޸���
             MODIFY_USERNAME         = v_MODIFY_USERNAME, --�޸���
             MODIFY_DEPTCODE         = v_MODIFY_DEPTCODE, --�޸��˿���
             MODIFY_DEPTNAME         = v_MODIFY_DEPTNAME, --�޸��˿���
             AUDIT_DATE              = v_AUDIT_DATE, --���ʱ��
             AUDIT_USERCODE          = v_AUDIT_USERCODE, --  �����
             AUDIT_USERNAME          = v_AUDIT_USERNAME, --�����
             AUDIT_DEPTCODE          = v_AUDIT_DEPTCODE, --����˿���
             AUDIT_DEPTNAME          = v_AUDIT_DEPTNAME, --����˿���
             VAILD                   = v_VAILD, --״̬�Ƿ���Ч  1����Ч   0����Ч
             CANCELREASON            = v_CANCELREASON, --���ԭ��
             PRENATAL                = v_PRENATAL, --��ǰ
             PRENATALNO              = v_PRENATALNO, --��ǰ����
             POSTPARTUM              = v_POSTPARTUM, --����
             ANDTOSHOWLEFT           = v_ANDTOSHOWLEFT, --  ��ָ��
             ANDTOSHOWRIGHT          = v_ANDTOSHOWRIGHT --��ָ��
      
       where ID = v_ID;
    
      open o_result for
        select v_ID from dual;
    
    end if;
  
    --���ϴ�Ⱦ�����濨��Ϣ     
  
    --���ݴ���Ĵ�Ⱦ�����濨ID��ѯ���濨��Ϣ
    IF v_edittype = '4' THEN
    
      open o_result for
        select * from BIRTHDEFECTSCARD a where a.report_id = v_ID;
    
    end if;
  
  end;
  --------����ȱ�ݱ��濨

  PROCEDURE usp_EditTherioma_Report(v_EditType              varchar,
                                    v_Report_ID             NUMERIC DEFAULT 0,
                                    v_REPORT_DISTRICTID     varchar DEFAULT '', --��Ⱦ���ϱ�������(��)����
                                    v_REPORT_DISTRICTNAME   varchar default '', --��Ⱦ���ϱ�������(��)����
                                    v_REPORT_ICD10          varchar default '', --��Ⱦ�����濨ICD-10����
                                    v_REPORT_ICD0           varchar default '', --��Ⱦ�����濨ICD-0����
                                    v_REPORT_CLINICID       varchar default '', --�����
                                    v_REPORT_PATID          varchar default '', --סԺ��
                                    v_REPORT_INDO           varchar default '', --���֤����
                                    v_REPORT_INPATNAME      varchar default '', --��������
                                    v_SEXID                 varchar default '', --�����Ա�
                                    v_REALAGE               varchar default '', --����ʵ������
                                    v_BIRTHDATE             varchar default '', --��������
                                    v_NATIONID              varchar default '', --����������
                                    v_NATIONNAME            varchar default '', --��������ȫ��
                                    v_CONTACTTEL            varchar default '', --��ͥ�绰
                                    v_MARTIAL               varchar default '', --����״��
                                    v_OCCUPATION            varchar default '', --����ְҵ
                                    v_OFFICEADDRESS         varchar default '', --������λ��ַ
                                    v_ORGPROVINCEID         varchar default '', --���ڵ�ַʡ�ݱ���
                                    v_ORGCITYID             varchar default '', --���ڵ�ַ�����б���
                                    v_ORGDISTRICTID         varchar default '', --�������ڵ����ر���
                                    v_ORGTOWNID             varchar default '', --�������ڵ���(�ֵ�)����
                                    v_ORGVILLIAGE           varchar default '', --�������ڵؾ�ί���Ӧ����
                                    v_ORGPROVINCENAME       varchar default '', --�������ڵ�ʡ��ȫ��
                                    v_ORGCITYNAME           varchar default '', --�������ڵ���ȫ����
                                    v_ORGDISTRICTNAME       varchar default '', --�������ڵ���(��)ȫ��
                                    v_ORGTOWN               varchar default '', --�������ڵ���ȫ��
                                    v_ORGVILLAGENAME        varchar default '', --�������ڵش�ȫ��
                                    v_XZZPROVINCEID         varchar default '', --��סַ����ʡ�ݱ���
                                    v_XZZCITYID             varchar default '', --��סַ�����б���
                                    v_XZZDISTRICTID         varchar default '', --��סַ������(��)����
                                    v_XZZTOWNID             varchar default '', --��סַ���������
                                    v_XZZVILLIAGEID         varchar default '', --����סַ���ڴ����
                                    v_XZZPROVINCE           varchar default '', --��סַ����ʡ��ȫ��
                                    v_XZZCITY               varchar default '', --��סַ������ȫ��
                                    v_XZZDISTRICT           varchar default '', --��סַ������ȫ��
                                    v_XZZTOWN               varchar default '', --��סַ������ȫ��
                                    v_XZZVILLIAGE           varchar default '', --��סַ���ڴ�ȫ��
                                    v_REPORT_DIAGNOSIS      varchar default '', --���
                                    v_PATHOLOGICALTYPE      varchar default '', --��������
                                    v_PATHOLOGICALID        varchar default '', --������ϲ����
                                    v_QZDIAGTIME_T          varchar default '', --ȷ��ʱ��_T��
                                    v_QZDIAGTIME_N          varchar default '', --ȷ��ʱ��_N��
                                    v_QZDIAGTIME_M          varchar default '', --ȷ��ʱ��_M��
                                    v_FIRSTDIADATE          varchar default '', --�״�ȷ��ʱ��
                                    v_REPORTINFUNIT         varchar default '', --���浥λ
                                    v_REPORTDOCTOR          varchar default '', --����ҽ��
                                    v_REPORTDATE            varchar default '', --����ʱ��
                                    v_DEATHDATE             varchar default '', --����ʱ��
                                    v_DEATHREASON           varchar default '', --����ԭ��
                                    v_REJEST                varchar default '', --����ժҪ
                                    v_REPORT_YDIAGNOSIS     varchar default '', --ԭ���
                                    v_REPORT_YDIAGNOSISDATA varchar default '', --ԭ�������
                                    v_REPORT_DIAGNOSISBASED varchar default '', --�������
                                    v_REPORT_NO             varchar default '', --��Ⱦ���ϱ�������
                                    v_REPORT_NOOFINPAT      varchar default '', --����ID
                                    v_STATE                 varchar default '', --����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ�  7�����ϡ�
                                    v_CREATE_DATE           varchar default '', --����������  
                                    v_CREATE_USERCODE       varchar default '', --������
                                    v_CREATE_USERNAME       varchar default '', --������
                                    v_CREATE_DEPTCODE       varchar default '', --�����˿���
                                    v_CREATE_DEPTNAME       varchar default '', --�����˿���
                                    v_MODIFY_DATE           varchar default '', --�޸�ʱ��
                                    v_MODIFY_USERCODE       varchar default '', --�޸���
                                    v_MODIFY_USERNAME       varchar default '', --�޸���
                                    v_MODIFY_DEPTCODE       varchar default '', --�޸��˿���
                                    v_MODIFY_DEPTNAME       varchar default '', --�޸��˿���
                                    v_AUDIT_DATE            varchar default '', --���ʱ��
                                    v_AUDIT_USERCODE        varchar default '', --�����
                                    v_AUDIT_USERNAME        varchar default '', --�����
                                    v_AUDIT_DEPTCODE        varchar default '', --����˿���
                                    v_AUDIT_DEPTNAME        varchar default '', --����˿���
                                    v_VAILD                 varchar default '',
                                    v_DIAGICD10             varchar default '',
                                    v_CANCELREASON          varchar default '', --���ԭ��   
                                    V_CARDTYPE              varchar default '', --��Ƭ�����������߷���                   
                                    V_clinicalstages        varchar default '',
                                    V_ReportDiagfunit       varchar default '',
                                    o_result                OUT empcurtyp) AS
  
    v_Report_ID_new int;
  BEGIN
  
    --������Ⱦ�����濨
    IF v_edittype = '1' THEN
    
      select SEQ_THERIOMAID.Nextval into v_Report_ID_new from dual;
    
      insert into THERIOMAREPORTCARD
        (REPORT_ID, --���
         REPORT_DISTRICTID, --��Ⱦ���ϱ�������(��)����
         REPORT_DISTRICTNAME, --��Ⱦ���ϱ�������(��)����
         REPORT_ICD10, --��Ⱦ�����濨ICD-10����
         REPORT_ICD0, --��Ⱦ�����濨ICD-0����
         REPORT_CLINICID, --�����
         REPORT_PATID, --סԺ��
         REPORT_INDO, --���֤����
         REPORT_INPATNAME, --��������
         SEXID, --�����Ա�
         REALAGE, --����ʵ������(�����ʱδ��������Ϊ����������꣬�ѹ�������Ϊ�������һ�ꣻδ��һ����Ϊ0��)
         BIRTHDATE, --��������
         NATIONID, --����������
         NATIONNAME, --��������ȫ��
         CONTACTTEL, --��ͥ�绰
         MARTIAL, --����״��
         OCCUPATION, --����ְҵ
         OFFICEADDRESS, --������λ��ַ
         ORGPROVINCEID, --���ڵ�ַʡ�ݱ���
         ORGCITYID, --���ڵ�ַ�����б���
         ORGDISTRICTID, --�������ڵ����ر���
         ORGTOWNID, --�������ڵ���(�ֵ�)����
         ORGVILLIAGE, --�������ڵؾ�ί���Ӧ����
         ORGPROVINCENAME, --�������ڵ�ʡ��ȫ��
         ORGCITYNAME, --�������ڵ���ȫ����
         ORGDISTRICTNAME, --�������ڵ���(��)ȫ��
         ORGTOWN, --�������ڵ���ȫ��
         ORGVILLAGENAME, --�������ڵش�ȫ��
         XZZPROVINCEID, --��סַ����ʡ�ݱ���
         XZZCITYID, --��סַ�����б���
         XZZDISTRICTID, --��סַ������(��)����
         XZZTOWNID, --��סַ���������
         XZZVILLIAGEID, --��סַ���ڴ����
         XZZPROVINCE, --��סַ����ʡ��ȫ��
         XZZCITY, --��סַ������ȫ��
         XZZDISTRICT, --��סַ������ȫ��
         XZZTOWN, --��סַ������ȫ��
         XZZVILLIAGE, --��סַ���ڴ�ȫ��
         REPORT_DIAGNOSIS, --���
         PATHOLOGICALTYPE, --��������
         PATHOLOGICALID, --������ϲ����
         QZDIAGTIME_T, --ȷ��ʱ��_T��
         QZDIAGTIME_N, --ȷ��ʱ��_N��
         QZDIAGTIME_M, --ȷ��ʱ��_M��
         FIRSTDIADATE, --�״�ȷ��ʱ��
         REPORTINFUNIT, --���浥λ
         REPORTDOCTOR, --����ҽ��
         REPORTDATE, --����ʱ��
         DEATHDATE, --����ʱ��
         DEATHREASON, --����ԭ��
         REJEST, --����ժҪ
         REPORT_YDIAGNOSIS, --ԭ���
         REPORT_YDIAGNOSISDATA, --ԭ�������
         REPORT_DIAGNOSISBASED, --�������
         REPORT_NO, --��Ⱦ���ϱ�������
         REPORT_NOOFINPAT, --����ID
         STATE, --"����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ� 7�����ϡ�"
         CREATE_DATE, --����ʱ��
         CREATE_USERCODE, --������
         CREATE_USERNAME, --������
         CREATE_DEPTCODE, --�����˿���
         CREATE_DEPTNAME, --�����˿���
         MODIFY_DATE, --�޸�ʱ��
         MODIFY_USERCODE, --�޸���
         MODIFY_USERNAME, --�޸���
         MODIFY_DEPTCODE, --�޸��˿���
         MODIFY_DEPTNAME, --�޸��˿���
         AUDIT_DATE, --���ʱ��
         AUDIT_USERCODE, --�����
         AUDIT_USERNAME, --�����
         AUDIT_DEPTCODE, --����˿���
         AUDIT_DEPTNAME, --����˿���
         VAILD, --״̬�Ƿ���Ч  1����Ч   0����Ч
         DIAGICD10, --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
         CANCELREASON, --���ԭ��
         CARDTYPE,
         clinicalstages,
         ReportDiagfunit)
      values
        (v_Report_ID_new, --���
         v_REPORT_DISTRICTID, --��Ⱦ���ϱ�������(��)����
         v_REPORT_DISTRICTNAME, --��Ⱦ���ϱ�������(��)����
         v_REPORT_ICD10, --��Ⱦ�����濨ICD-10����
         v_REPORT_ICD0, --��Ⱦ�����濨ICD-0����
         v_REPORT_CLINICID, --�����
         v_REPORT_PATID, --סԺ��
         v_REPORT_INDO, --���֤����
         v_REPORT_INPATNAME, --��������
         v_SEXID, --�����Ա�
         v_REALAGE, --����ʵ������(�����ʱδ��������Ϊ����������꣬�ѹ�������Ϊ�������һ�ꣻδ��һ����Ϊ0��)
         v_BIRTHDATE, --��������
         v_NATIONID, --����������
         v_NATIONNAME, --��������ȫ��
         v_CONTACTTEL, --��ͥ�绰
         v_MARTIAL, --����״��
         v_OCCUPATION, --����ְҵ
         v_OFFICEADDRESS, --������λ��ַ
         v_ORGPROVINCEID, --���ڵ�ַʡ�ݱ���
         v_ORGCITYID, --���ڵ�ַ�����б���
         v_ORGDISTRICTID, --�������ڵ����ر���
         v_ORGTOWNID, --�������ڵ���(�ֵ�)����
         v_ORGVILLIAGE, --�������ڵؾ�ί���Ӧ����
         v_ORGPROVINCENAME, --�������ڵ�ʡ��ȫ��
         v_ORGCITYNAME, --�������ڵ���ȫ����
         v_ORGDISTRICTNAME, --�������ڵ���(��)ȫ��
         v_ORGTOWN, --�������ڵ���ȫ��
         v_ORGVILLAGENAME, --�������ڵش�ȫ��
         v_XZZPROVINCEID, --��סַ����ʡ�ݱ���
         v_XZZCITYID, --��סַ�����б���
         v_XZZDISTRICTID, --��סַ������(��)����
         v_XZZTOWNID, --��סַ���������
         v_XZZVILLIAGEID, --��סַ���ڴ����
         v_XZZPROVINCE, --��סַ����ʡ��ȫ��
         v_XZZCITY, --��סַ������ȫ��
         v_XZZDISTRICT, --��סַ������ȫ��
         v_XZZTOWN, --��סַ������ȫ��
         v_XZZVILLIAGE, --��סַ���ڴ�ȫ��
         v_REPORT_DIAGNOSIS, --���
         v_PATHOLOGICALTYPE, --��������
         v_PATHOLOGICALID, --������ϲ����
         v_QZDIAGTIME_T, --ȷ��ʱ��_T��
         v_QZDIAGTIME_N, --ȷ��ʱ��_N��
         v_QZDIAGTIME_M, --ȷ��ʱ��_M��
         v_FIRSTDIADATE, --�״�ȷ��ʱ��
         v_REPORTINFUNIT, --���浥λ
         v_REPORTDOCTOR, --����ҽ��
         v_REPORTDATE, --����ʱ��
         v_DEATHDATE, --����ʱ��
         v_DEATHREASON, --����ԭ��
         v_REJEST, --����ժҪ
         v_REPORT_YDIAGNOSIS, --ԭ���
         v_REPORT_YDIAGNOSISDATA, --ԭ�������
         v_REPORT_DIAGNOSISBASED, --�������
         v_REPORT_NO, --��Ⱦ���ϱ�������
         v_REPORT_NOOFINPAT, --����ID
         v_STATE, --"����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ� 7�����ϡ�"
         v_CREATE_DATE, --����ʱ��
         v_CREATE_USERCODE, --������
         v_CREATE_USERNAME, --������
         v_CREATE_DEPTCODE, --�����˿���
         v_CREATE_DEPTNAME, --�����˿���
         v_MODIFY_DATE, --�޸�ʱ��
         v_MODIFY_USERCODE, --�޸���
         v_MODIFY_USERNAME, --�޸���
         v_MODIFY_DEPTCODE, --�޸��˿���
         v_MODIFY_DEPTNAME, --�޸��˿���
         v_AUDIT_DATE, --���ʱ��
         v_AUDIT_USERCODE, --�����
         v_AUDIT_USERNAME, --�����
         v_AUDIT_DEPTCODE, --����˿���
         v_AUDIT_DEPTNAME, --����˿���
         v_VAILD, --״̬�Ƿ���Ч  1����Ч   0����Ч
         v_DIAGICD10, --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
         v_CANCELREASON, --���ԭ��
         V_CARDTYPE,
         V_clinicalstages,
         V_ReportDiagfunit);
    
      open o_result for
        select v_Report_ID_new from dual;
    
    end if;
  
    --�޸ı��洫Ⱦ�����濨��Ϣ
    IF v_edittype = '2' THEN
    
      update THERIOMAREPORTCARD
         set REPORT_DISTRICTID     = v_REPORT_DISTRICTID, --��Ⱦ���ϱ�������(��)����
             REPORT_DISTRICTNAME   = v_REPORT_DISTRICTNAME, --��Ⱦ���ϱ�������(��)����
             REPORT_ICD10          = v_REPORT_ICD10, --��Ⱦ�����濨ICD-10����
             REPORT_ICD0           = v_REPORT_ICD0, --��Ⱦ�����濨ICD-0����
             REPORT_CLINICID       = v_REPORT_CLINICID, --�����
             REPORT_PATID          = v_REPORT_PATID, --סԺ��
             REPORT_INDO           = v_REPORT_INDO, --���֤����
             REPORT_INPATNAME      = v_REPORT_INPATNAME, --��������
             SEXID                 = v_SEXID, --�����Ա�
             REALAGE               = v_REALAGE, --����ʵ������(�����ʱδ��������Ϊ����������꣬�ѹ�������Ϊ�������һ�ꣻδ��һ����Ϊ0��)
             BIRTHDATE             = v_BIRTHDATE, --��������
             NATIONID              = v_NATIONID, --����������
             NATIONNAME            = v_NATIONNAME, --��������ȫ��
             CONTACTTEL            = v_CONTACTTEL, --��ͥ�绰
             MARTIAL               = v_MARTIAL, --����״��
             OCCUPATION            = v_OCCUPATION, --����ְҵ
             OFFICEADDRESS         = v_OFFICEADDRESS, --������λ��ַ
             ORGPROVINCEID         = v_ORGPROVINCEID, --���ڵ�ַʡ�ݱ���
             ORGCITYID             = v_ORGCITYID, --���ڵ�ַ�����б���
             ORGDISTRICTID         = v_ORGDISTRICTID, --�������ڵ����ر���
             ORGTOWNID             = v_ORGTOWNID, --�������ڵ���(�ֵ�)����
             ORGVILLIAGE           = v_ORGVILLIAGE, --�������ڵؾ�ί���Ӧ����
             ORGPROVINCENAME       = v_ORGPROVINCENAME, --�������ڵ�ʡ��ȫ��
             ORGCITYNAME           = v_ORGCITYNAME, --�������ڵ���ȫ����
             ORGDISTRICTNAME       = v_ORGDISTRICTNAME, --�������ڵ���(��)ȫ��
             ORGTOWN               = v_ORGTOWN, --�������ڵ���ȫ��
             ORGVILLAGENAME        = v_ORGVILLAGENAME, --�������ڵش�ȫ��
             XZZPROVINCEID         = v_XZZPROVINCEID, --��סַ����ʡ�ݱ���
             XZZCITYID             = v_XZZCITYID, --��סַ�����б���
             XZZDISTRICTID         = v_XZZDISTRICTID, --��סַ������(��)����
             XZZTOWNID             = v_XZZTOWNID, --��סַ���������
             XZZVILLIAGEID         = v_XZZVILLIAGEID, --��סַ���ڴ����
             XZZPROVINCE           = v_XZZPROVINCE, --��סַ����ʡ��ȫ��
             XZZCITY               = v_XZZCITY, --��סַ������ȫ��
             XZZDISTRICT           = v_XZZDISTRICT, --����סַ������ȫ��
             XZZTOWN               = v_XZZTOWN, --��סַ������ȫ��
             XZZVILLIAGE           = v_XZZVILLIAGE, --��סַ���ڴ�ȫ��
             REPORT_DIAGNOSIS      = v_REPORT_DIAGNOSIS, --���
             PATHOLOGICALTYPE      = v_PATHOLOGICALTYPE, --��������
             PATHOLOGICALID        = v_PATHOLOGICALID, --������ϲ����
             QZDIAGTIME_T          = v_QZDIAGTIME_T, --ȷ��ʱ��_T��
             QZDIAGTIME_N          = v_QZDIAGTIME_N, --ȷ��ʱ��_N��
             QZDIAGTIME_M          = v_QZDIAGTIME_M, --ȷ��ʱ��_M��
             FIRSTDIADATE          = v_FIRSTDIADATE, --�״�ȷ��ʱ��
             REPORTINFUNIT         = v_REPORTINFUNIT, --���浥λ
             REPORTDOCTOR          = v_REPORTDOCTOR, --����ҽ��
             REPORTDATE            = v_REPORTDATE, --����ʱ��
             DEATHDATE             = v_DEATHDATE, --����ʱ��
             DEATHREASON           = v_DEATHREASON, --����ԭ��
             REJEST                = v_REJEST, --����ժҪ
             REPORT_YDIAGNOSIS     = v_REPORT_YDIAGNOSIS, --ԭ���
             REPORT_YDIAGNOSISDATA = v_REPORT_YDIAGNOSISDATA, --ԭ�������
             REPORT_DIAGNOSISBASED = v_REPORT_DIAGNOSISBASED, --�������                   
             REPORT_NO             = v_REPORT_NO, --��Ⱦ���ϱ�������
             REPORT_NOOFINPAT      = v_REPORT_NOOFINPAT, --����ID
             STATE                 = v_STATE, --"����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ� 7�����ϡ�"                           
             CREATE_DATE           = nvl(v_CREATE_DATE, CREATE_DATE), --����ʱ��
             CREATE_USERCODE       = nvl(v_CREATE_USERCODE, CREATE_USERCODE), --������
             CREATE_USERNAME       = nvl(v_CREATE_USERNAME, CREATE_USERNAME), --������
             CREATE_DEPTCODE       = nvl(v_CREATE_DEPTCODE, CREATE_DEPTCODE), --�����˿���
             CREATE_DEPTNAME       = nvl(v_CREATE_DEPTNAME, CREATE_DEPTNAME), --�����˿���
             MODIFY_DATE           = nvl(v_MODIFY_DATE, MODIFY_DATE), --�޸�ʱ��
             MODIFY_USERCODE       = nvl(v_MODIFY_USERCODE, MODIFY_USERCODE), --�޸���
             MODIFY_USERNAME       = nvl(v_MODIFY_USERNAME, MODIFY_USERNAME), --�޸���
             MODIFY_DEPTCODE       = nvl(v_MODIFY_DEPTCODE, MODIFY_DEPTCODE), --�޸��˿���
             MODIFY_DEPTNAME       = nvl(v_MODIFY_DEPTNAME, MODIFY_DEPTNAME), --�޸��˿���
             AUDIT_DATE            = nvl(v_AUDIT_DATE, AUDIT_DATE), --���ʱ��
             AUDIT_USERCODE        = nvl(v_AUDIT_USERCODE, AUDIT_USERCODE), --�����
             AUDIT_USERNAME        = nvl(v_AUDIT_USERNAME, AUDIT_USERNAME), --�����
             AUDIT_DEPTCODE        = nvl(v_AUDIT_DEPTCODE, AUDIT_DEPTCODE), --����˿���
             AUDIT_DEPTNAME        = nvl(v_AUDIT_DEPTNAME, AUDIT_DEPTNAME), --����˿���
             VAILD                 = v_VAILD,
             DIAGICD10             = v_DIAGICD10, --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
             CANCELREASON          = v_CANCELREASON, --���ԭ��
             CARDTYPE              = V_CARDTYPE,
             clinicalstages        = V_clinicalstages,
             ReportDiagfunit       = V_ReportDiagfunit
       where REPORT_ID = v_Report_ID;
    
      open o_result for
        select v_Report_ID from dual;
    
    end if;
  
    --���ϴ�Ⱦ�����濨��Ϣ
    IF v_edittype = '3' THEN
    
      update THERIOMAREPORTCARD
         set REPORT_DISTRICTID     = nvl(v_REPORT_DISTRICTID,
                                         REPORT_DISTRICTID), --��Ⱦ���ϱ�������(��)����
             REPORT_DISTRICTNAME   = nvl(v_REPORT_DISTRICTNAME,
                                         REPORT_DISTRICTNAME), --��Ⱦ���ϱ�������(��)����
             REPORT_ICD10          = nvl(v_REPORT_ICD10, REPORT_ICD10), --��Ⱦ�����濨ICD-10����
             REPORT_ICD0           = nvl(v_REPORT_ICD0, REPORT_ICD0), --��Ⱦ�����濨ICD-0����
             REPORT_CLINICID       = nvl(v_REPORT_CLINICID, REPORT_CLINICID), --�����
             REPORT_PATID          = v_REPORT_PATID, --סԺ��
             REPORT_INDO           = v_REPORT_INDO, --���֤����
             REPORT_INPATNAME      = v_REPORT_INPATNAME, --��������
             SEXID                 = nvl(v_SEXID, SEXID), --�����Ա�
             REALAGE               = v_REALAGE, --����ʵ������(�����ʱδ��������Ϊ����������꣬�ѹ�������Ϊ�������һ�ꣻδ��һ����Ϊ0��)
             BIRTHDATE             = v_BIRTHDATE, --��������
             NATIONID              = v_NATIONID, --����������
             NATIONNAME            = v_NATIONNAME, --��������ȫ��
             CONTACTTEL            = v_CONTACTTEL, --��ͥ�绰
             MARTIAL               = v_MARTIAL, --����״��
             OCCUPATION            = v_OCCUPATION, --����ְҵ
             OFFICEADDRESS         = v_OFFICEADDRESS, --������λ��ַ
             ORGPROVINCEID         = v_ORGPROVINCEID, --���ڵ�ַʡ�ݱ���
             ORGCITYID             = v_ORGCITYID, --���ڵ�ַ�����б���
             ORGDISTRICTID         = v_ORGDISTRICTID, --�������ڵ����ر���
             ORGTOWNID             = v_ORGTOWNID, --�������ڵ���(�ֵ�)����
             ORGVILLIAGE           = v_ORGVILLIAGE, --�������ڵؾ�ί���Ӧ����
             ORGPROVINCENAME       = v_ORGPROVINCENAME, --�������ڵ�ʡ��ȫ��
             ORGCITYNAME           = v_ORGCITYNAME, --�������ڵ���ȫ����
             ORGDISTRICTNAME       = v_ORGDISTRICTNAME, --�������ڵ���(��)ȫ��
             ORGTOWN               = v_ORGTOWN, --�������ڵ���ȫ��
             ORGVILLAGENAME        = v_ORGVILLAGENAME, --�������ڵش�ȫ��
             XZZPROVINCEID         = v_XZZPROVINCEID, --��סַ����ʡ�ݱ���
             XZZCITYID             = v_XZZCITYID, --��סַ�����б���
             XZZDISTRICTID         = v_XZZDISTRICTID, --��סַ������(��)����
             XZZTOWNID             = v_XZZTOWNID, --��סַ���������
             XZZVILLIAGEID         = v_XZZVILLIAGEID, --��סַ���ڴ����
             XZZPROVINCE           = v_XZZPROVINCE, --��סַ����ʡ��ȫ��
             XZZCITY               = v_XZZCITY, --��סַ������ȫ��
             XZZDISTRICT           = v_XZZDISTRICT, --����סַ������ȫ��
             XZZTOWN               = v_XZZTOWN, --��סַ������ȫ��
             XZZVILLIAGE           = v_XZZVILLIAGE, --��סַ���ڴ�ȫ��
             REPORT_DIAGNOSIS      = v_REPORT_DIAGNOSIS, --���
             PATHOLOGICALTYPE      = v_PATHOLOGICALTYPE, --��������
             PATHOLOGICALID        = v_PATHOLOGICALID, --������ϲ����
             QZDIAGTIME_T          = v_QZDIAGTIME_T, --ȷ��ʱ��_T��
             QZDIAGTIME_N          = v_QZDIAGTIME_N, --ȷ��ʱ��_N��
             QZDIAGTIME_M          = v_QZDIAGTIME_M, --ȷ��ʱ��_M��
             FIRSTDIADATE          = v_FIRSTDIADATE, --�״�ȷ��ʱ��
             REPORTINFUNIT         = v_REPORTINFUNIT, --���浥λ
             REPORTDOCTOR          = v_REPORTDOCTOR, --����ҽ��
             REPORTDATE            = v_REPORTDATE, --����ʱ��
             DEATHDATE             = v_DEATHDATE, --����ʱ��
             DEATHREASON           = v_DEATHREASON, --����ԭ��
             REJEST                = v_REJEST, --����ժҪ
             REPORT_YDIAGNOSIS     = v_REPORT_YDIAGNOSIS, --ԭ���
             REPORT_YDIAGNOSISDATA = v_REPORT_YDIAGNOSISDATA, --ԭ�������
             REPORT_DIAGNOSISBASED = v_REPORT_DIAGNOSISBASED, --�������                   
             REPORT_NO             = v_REPORT_NO, --��Ⱦ���ϱ�������
             REPORT_NOOFINPAT      = v_REPORT_NOOFINPAT, --����ID
             STATE                 = v_STATE, --"����״̬�� 1���������� 2���ύ 3������ 4�����ͨ�� 5�����δͨ������ 6���ϱ� 7�����ϡ�"                           
             CREATE_DATE           = nvl(v_CREATE_DATE, CREATE_DATE), --����ʱ��
             CREATE_USERCODE       = nvl(v_CREATE_USERCODE, CREATE_USERCODE), --������
             CREATE_USERNAME       = nvl(v_CREATE_USERNAME, CREATE_USERNAME), --������
             CREATE_DEPTCODE       = nvl(v_CREATE_DEPTCODE, CREATE_DEPTCODE), --�����˿���
             CREATE_DEPTNAME       = nvl(v_CREATE_DEPTNAME, CREATE_DEPTNAME), --�����˿���
             MODIFY_DATE           = nvl(v_MODIFY_DATE, MODIFY_DATE), --�޸�ʱ��
             MODIFY_USERCODE       = nvl(v_MODIFY_USERCODE, MODIFY_USERCODE), --�޸���
             MODIFY_USERNAME       = nvl(v_MODIFY_USERNAME, MODIFY_USERNAME), --�޸���
             MODIFY_DEPTCODE       = nvl(v_MODIFY_DEPTCODE, MODIFY_DEPTCODE), --�޸��˿���
             MODIFY_DEPTNAME       = nvl(v_MODIFY_DEPTNAME, MODIFY_DEPTNAME), --�޸��˿���
             AUDIT_DATE            = nvl(v_AUDIT_DATE, AUDIT_DATE), --���ʱ��
             AUDIT_USERCODE        = nvl(v_AUDIT_USERCODE, AUDIT_USERCODE), --�����
             AUDIT_USERNAME        = nvl(v_AUDIT_USERNAME, AUDIT_USERNAME), --�����
             AUDIT_DEPTCODE        = nvl(v_AUDIT_DEPTCODE, AUDIT_DEPTCODE), --����˿���
             AUDIT_DEPTNAME        = nvl(v_AUDIT_DEPTNAME, AUDIT_DEPTNAME), --����˿���
             VAILD                 = '0',
             DIAGICD10             = v_DIAGICD10, -- --��Ⱦ������(��Ӧ��Ⱦ����Ͽ�)
             CANCELREASON          = v_CANCELREASON, --���ԭ��
             CARDTYPE              = V_CARDTYPE,
             clinicalstages        = V_clinicalstages,
             ReportDiagfunit       = V_ReportDiagfunit
       where REPORT_ID = v_Report_ID;
    
      open o_result for
        select v_Report_ID from dual;
    
    end if;
  
    --���ݴ���Ĵ�Ⱦ�����濨ID��ѯ���濨��Ϣ
    IF v_edittype = '4' THEN
    
      open o_result for
        select * from THERIOMAREPORTCARD a where a.report_id = v_Report_ID;
    
    end if;
  
  end;

  /*********************************************************************************/
  --������---�����Ǽ��±��� add by ywk 2013��7��31�� 14:59:19
  PROCEDURE usp_GetTheriomaReportBYMonth( --v_searchtype    varchar default '', --���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                         v_year          varchar default '', --��
                                         v_month         varchar default '', --��
                                         v_deptcode      varchar default '', --���ұ���
                                         v_diagstartdate varchar default '', --��Ͽ�ʼʱ��
                                         v_diagenddate   varchar default '', --��Ͻ���ʱ��
                                         o_result        OUT empcurtyp) as
  begin
    --if v_searchtype = '1' then
    open o_result for
      select *
        from (select REPORT_INPATNAME as PATNAME,
                     sexinfo.name as SEXNAME,
                     REALAGE,
                     STATE,
                     reportdate,
                     material.name as MATRIAL,
                     jobinfo.name as JOB,
                     FIRSTDIADATE,
                     DEATHDATE,
                     DEATHREASON,
                     VAILD,
                     XZZPROVINCE || XZZCITY || XZZDISTRICT || XZZTOWN ||
                     XZZVILLIAGE as JZADDRESS,
                     REPORT_DIAGNOSIS,
                     '�Ž���ҽԺ' as HOSP,
                     (case
                       when instr(REPORT_DIAGNOSISBASED, '1', 1) <> 0 then
                        '�ٴ�'
                       when instr(REPORT_DIAGNOSISBASED, '2', 1) <> 0 then
                        'X�ߡ�CT���������ڿ���'
                       when instr(REPORT_DIAGNOSISBASED, '3', 1) <> 0 then
                        '������ʬ��'
                       when instr(REPORT_DIAGNOSISBASED, '4', 1) <> 0 then
                        '����������'
                       when instr(REPORT_DIAGNOSISBASED, '5', 1) <> 0 then
                        'ϸ��ѧ��ѪƬ'
                       when instr(REPORT_DIAGNOSISBASED, '6', 1) <> 0 then
                        '�����̷���'
                       when instr(REPORT_DIAGNOSISBASED, '7', 1) <> 0 then
                        '����ԭ����'
                       when instr(REPORT_DIAGNOSISBASED, '8', 1) <> 0 then
                        'ʬ�죨�в���'
                       when instr(REPORT_DIAGNOSISBASED, '9', 1) <> 0 then
                        '����'
                       when instr(REPORT_DIAGNOSISBASED, '0', 1) <> 0 then
                        '����������'
                       else
                        ''
                     end) as DIAGYJ,
                     PATHOLOGICALTYPE,
                     QZDIAGTIME_T || QZDIAGTIME_N || QZDIAGTIME_M as QZDIAGTIME,
                     '' as MEMO
              
                from theriomareportcard tmpcard
                left join dictionary_detail material
                  on material.detailid = tmpcard.MARTIAL
                 and material.categoryid = '4'
                 AND material.valid = 1
                left join dictionary_detail sexinfo
                  on sexinfo.detailid = tmpcard.sexid
                 and sexinfo.categoryid = '3'
                 and sexinfo.valid = 1
                left join dictionary_detail jobinfo
                  on jobinfo.detailid = tmpcard.occupation
                 and jobinfo.categoryid = '41'
                 and jobinfo.valid = 1) A
       where A.reportdate like v_year || '-' || v_month || '%'
         and A.VAILD = '1'
         and A.STATE not in ('3', '5', '7');
    -- and A.FIRSTDIADATE >v_diagstartdate and A.FIRSTDIADATE<v_diagenddate ;   --'2013-0%';
  end;
  --������---���������·������Ǽǲ� add by ywk 2013��8��2�� 11:29:02
  PROCEDURE usp_GetTheriomaReportBYNew( --v_searchtype    varchar default '', --���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                       v_year   varchar default '', --��
                                       v_month  varchar default '', --��
                                       o_result OUT empcurtyp) as
  begin
    open o_result for
      select *
        from (select REPORT_INPATNAME as PATNAME,
                     sexinfo.name as SEXNAME,
                     REALAGE,
                     REPORT_NO,
                     '' HIGHDIAG,
                     '' SFTIME,
                     '' SFINFO,
                     OFFICEADDRESS,
                     reportdate,
                     material.name as MATRIAL,
                     jobinfo.name as JOB,
                     FIRSTDIADATE,
                     DEATHDATE,
                     DEATHREASON,
                     VAILD,
                     XZZPROVINCE || XZZCITY || XZZDISTRICT || XZZTOWN ||
                     XZZVILLIAGE as JZADDRESS,
                     REPORT_DIAGNOSIS,
                     '�Ž���ҽԺ' as HOSP,
                     (case
                       when instr(REPORT_DIAGNOSISBASED, '1', 1) <> 0 then
                        '�ٴ�'
                       when instr(REPORT_DIAGNOSISBASED, '2', 1) <> 0 then
                        'X�ߡ�CT���������ڿ���'
                       when instr(REPORT_DIAGNOSISBASED, '3', 1) <> 0 then
                        '������ʬ��'
                       when instr(REPORT_DIAGNOSISBASED, '4', 1) <> 0 then
                        '����������'
                       when instr(REPORT_DIAGNOSISBASED, '5', 1) <> 0 then
                        'ϸ��ѧ��ѪƬ'
                       when instr(REPORT_DIAGNOSISBASED, '6', 1) <> 0 then
                        '�����̷���'
                       when instr(REPORT_DIAGNOSISBASED, '7', 1) <> 0 then
                        '����ԭ����'
                       when instr(REPORT_DIAGNOSISBASED, '8', 1) <> 0 then
                        'ʬ�죨�в���'
                       when instr(REPORT_DIAGNOSISBASED, '9', 1) <> 0 then
                        '����'
                       when instr(REPORT_DIAGNOSISBASED, '0', 1) <> 0 then
                        '����������'
                       else
                        ''
                     end) as DIAGYJ,
                     STATE,
                     PATHOLOGICALTYPE,
                     QZDIAGTIME_T || QZDIAGTIME_N || QZDIAGTIME_M as QZDIAGTIME,
                     '' as MEMO
              
                from theriomareportcard tmpcard
                left join dictionary_detail material
                  on material.detailid = tmpcard.MARTIAL
                 and material.categoryid = '4'
                 AND material.valid = 1
                left join dictionary_detail sexinfo
                  on sexinfo.detailid = tmpcard.sexid
                 and sexinfo.categoryid = '3'
                 and sexinfo.valid = 1
                left join dictionary_detail jobinfo
                  on jobinfo.detailid = tmpcard.occupation
                 and jobinfo.categoryid = '41'
                 and jobinfo.valid = 1) A
       where A.reportdate like v_year || '-' || v_month || '%'
         and A.DEATHDATE is null
         and A.VAILD = '1'
         and A.STATE not in ('3', '5', '7');
  end;

  --������---�����������������Ǽǲ� add by ywk 2013��8��2�� 11:29:02
  PROCEDURE usp_GetTheriomaReportBYDead( --v_searchtype    varchar default '', --���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                        v_year   varchar default '', --��
                                        v_month  varchar default '', --��
                                        o_result OUT empcurtyp) as
  begin
    open o_result for
      select *
        from (select REPORT_INPATNAME as PATNAME,
                     sexinfo.name as SEXNAME,
                     REALAGE,
                     REPORT_NO,
                     OFFICEADDRESS,
                     reportdate,
                     material.name as MATRIAL,
                     jobinfo.name as JOB,
                     FIRSTDIADATE,
                     DEATHDATE,
                     DEATHREASON,
                     BIRTHDATE,
                     VAILD,
                     '' DIEADDRESS,
                     '' CULTURAL,
                     XZZPROVINCE || XZZCITY || XZZDISTRICT || XZZTOWN ||
                     XZZVILLIAGE as JZADDRESS,
                     REPORT_DIAGNOSIS,
                     '�Ž���ҽԺ' as HOSP,
                     (case
                       when instr(REPORT_DIAGNOSISBASED, '1', 1) <> 0 then
                        '�ٴ�'
                       when instr(REPORT_DIAGNOSISBASED, '2', 1) <> 0 then
                        'X�ߡ�CT���������ڿ���'
                       when instr(REPORT_DIAGNOSISBASED, '3', 1) <> 0 then
                        '������ʬ��'
                       when instr(REPORT_DIAGNOSISBASED, '4', 1) <> 0 then
                        '����������'
                       when instr(REPORT_DIAGNOSISBASED, '5', 1) <> 0 then
                        'ϸ��ѧ��ѪƬ'
                       when instr(REPORT_DIAGNOSISBASED, '6', 1) <> 0 then
                        '�����̷���'
                       when instr(REPORT_DIAGNOSISBASED, '7', 1) <> 0 then
                        '����ԭ����'
                       when instr(REPORT_DIAGNOSISBASED, '8', 1) <> 0 then
                        'ʬ�죨�в���'
                       when instr(REPORT_DIAGNOSISBASED, '9', 1) <> 0 then
                        '����'
                       when instr(REPORT_DIAGNOSISBASED, '0', 1) <> 0 then
                        '����������'
                       else
                        ''
                     end) as DIAGYJ,
                     PATHOLOGICALTYPE,
                     QZDIAGTIME_T || QZDIAGTIME_N || QZDIAGTIME_M as QZDIAGTIME,
                     '' as MEMO,
                     STATE
              
                from theriomareportcard tmpcard
                left join dictionary_detail material
                  on material.detailid = tmpcard.MARTIAL
                 and material.categoryid = '4'
                 AND material.valid = 1
                left join dictionary_detail sexinfo
                  on sexinfo.detailid = tmpcard.sexid
                 and sexinfo.categoryid = '3'
                 and sexinfo.valid = 1
                left join dictionary_detail jobinfo
                  on jobinfo.detailid = tmpcard.occupation
                 and jobinfo.categoryid = '41'
                 and jobinfo.valid = 1) A
       where A.reportdate like v_year || '-' || v_month || '%'
         and A.DEATHDATE is not null
         and A.VAILD = '1'
         and A.STATE not in ('3', '5', '7');
  end;

  --������---�����Ǽ��±��� add by ywk 2013��8��5�� 11:33:25����ҽԺ
  PROCEDURE usp_GetTheriomaReportBYMonthZX( --v_searchtype    varchar default '', --���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                           v_year          varchar default '', --��
                                           v_month         varchar default '', --��
                                           v_deptcode      varchar default '', --���ұ���
                                           v_diagstartdate varchar default '', --��Ͽ�ʼʱ��
                                           v_diagenddate   varchar default '', --��Ͻ���ʱ��
                                           o_result        OUT empcurtyp) as
  begin
    --if v_searchtype = '1' then
    open o_result for
      select *
        from (select REPORT_INPATNAME as PATNAME,
                     sexinfo.name as SEXNAME,
                     REALAGE,
                     STATE,
                     reportdate,
                     material.name as MATRIAL,
                     jobinfo.name as JOB,
                     FIRSTDIADATE,
                     DEATHDATE,
                     DEATHREASON,
                     VAILD,
                     REPORT_NO,
                     REPORT_PATID,
                     BIRTHDATE,
                     CONTACTTEL,
                     users.name as REPORTDOCTORNAME,
                     REPORTDOCTOR,
                     REPORT_INDO,
                     CREATE_DEPTNAME as BGDEPT, --�������
                     REPORTDIAGFUNIT, --��ϵ�λ
                     d.name as DIAGUNIT, --��ϵ�λ����
                     XZZPROVINCE || XZZCITY || XZZDISTRICT || XZZTOWN ||
                     XZZVILLIAGE as JZADDRESS,
                     REPORT_DIAGNOSIS,
                     '�Ž���ҽԺ' as HOSP,
                     (case
                       when instr(REPORT_DIAGNOSISBASED, '1', 1) <> 0 then
                        '�ٴ�'
                       when instr(REPORT_DIAGNOSISBASED, '2', 1) <> 0 then
                        'X�ߡ�CT���������ڿ���'
                       when instr(REPORT_DIAGNOSISBASED, '3', 1) <> 0 then
                        '������ʬ��'
                       when instr(REPORT_DIAGNOSISBASED, '4', 1) <> 0 then
                        '����������'
                       when instr(REPORT_DIAGNOSISBASED, '5', 1) <> 0 then
                        'ϸ��ѧ��ѪƬ'
                       when instr(REPORT_DIAGNOSISBASED, '6', 1) <> 0 then
                        '�����̷���'
                       when instr(REPORT_DIAGNOSISBASED, '7', 1) <> 0 then
                        '����ԭ����'
                       when instr(REPORT_DIAGNOSISBASED, '8', 1) <> 0 then
                        'ʬ�죨�в���'
                       when instr(REPORT_DIAGNOSISBASED, '9', 1) <> 0 then
                        '����'
                       when instr(REPORT_DIAGNOSISBASED, '0', 1) <> 0 then
                        '����������'
                       else
                        ''
                     end) as DIAGYJ,
                     PATHOLOGICALTYPE,
                     QZDIAGTIME_T || QZDIAGTIME_N || QZDIAGTIME_M as QZDIAGTIME,
                     '' as MEMO
              
                from theriomareportcard tmpcard
                left join dictionary_detail material
                  on material.detailid = tmpcard.MARTIAL
                 and material.categoryid = '4'
                 AND material.valid = 1
                left join dictionary_detail sexinfo
                  on sexinfo.detailid = tmpcard.sexid
                 and sexinfo.categoryid = '3'
                 and sexinfo.valid = 1
                left join dictionary_detail jobinfo
                  on jobinfo.detailid = tmpcard.occupation
                 and jobinfo.categoryid = '41'
                 and jobinfo.valid = 1
                left join users
                  on tmpcard.reportdoctor = users.id
                left join department d
                  on tmpcard.ReportDiagfunit = d.id) A
       where A.reportdate like v_year || '-' || v_month || '%'
         and A.VAILD = '1'
         and A.STATE not in ('3', '5', '7');
    -- and A.FIRSTDIADATE >v_diagstartdate and A.FIRSTDIADATE<v_diagenddate ;   --'2013-0%';
  end;

------------------�����У����Ĳ�����-------------------add  by  jxh  2013-9-7  15:30
 PROCEDURE usp_CardiovascularReport( --v_searchtype    varchar default '', --���Ӵ��ֶ���Ҫ����Ϊi��������ҽԺ�������Ĳ�ѯ
                                           v_year          varchar default '', --��
                                           v_month         varchar default '', --��
                                           v_deptcode      varchar default '', --���ұ���
                                           v_diagstartdate varchar default '', --��Ͽ�ʼʱ��
                                           v_diagenddate   varchar default '', --��Ͻ���ʱ��
                                           o_result        OUT empcurtyp) as
  begin
    --if v_searchtype = '1' then
    open o_result for
      select *
        from (select   REPORT_NO,--���
                       PATID,--סԺ��
                       NAME,--����
                       sexinfo.name as SEXNAME,--�Ա�
                       AGE,--����
                       STATE,--״̬
                       jobinfo.name as JOB,--ְҵ
                       XZZPROVICE || XZZCITY || XZZSTREET || XZZCOMMITTEES ||
                       XZZPARM as JZADDRESS,--��ס��ַ
                       OFFICEPLACE,--��λ��ַ
                       z.name as DIAGNAME,--���    
                       DIAGNOSEDATE,--ȷ������  
                       DIAGHOSPITAL,--��ϵ�λ  
                       CASE diovcard.isfirstsick
                            WHEN '1' THEN
                             '��'
                             ELSE
                             '��'
                       END ISFIRSTSICK,
                       --ISFIRSTSICK,--�Ƿ��״η���   
                       REPORTDATE,--��������
                       REPORTUSERNAME,--����ҽʦ
                       CREATE_DEPTNAME,--�������
                       DIEDATE,--��������
                       VAILD --�Ƿ���Ч                   
                from cardiovascularcard diovcard    
                left join zymosis_diagnosis z on diovcard.Icd = z.icd           
                left join dictionary_detail sexinfo
                  on sexinfo.detailid = diovcard.sexid
                 and sexinfo.categoryid = '3'
                 and sexinfo.valid = 1
                left join dictionary_detail jobinfo
                  on jobinfo.detailid = diovcard.jobid
                 and jobinfo.categoryid = '41'
                 and jobinfo.valid = 1
                left join users
                  on diovcard.REPORTUSERCODE = users.id) A
               
       where A.reportdate like v_year || '-' || v_month || '%'
         and A.VAILD = '1'
         and A.STATE not in ('3', '5', '7');
    -- and A.FIRSTDIADATE >v_diagstartdate and A.FIRSTDIADATE<v_diagenddate ;   --'2013-0%';
  end;

  --������޸İ��̲�����
  PROCEDURE usp_AddOrModHIVReport(v_HIVID               varchar2,
                                  v_REPORTID            integer,
                                  v_REPORTNO            varchar2,
                                  v_MARITALSTATUS       varchar2,
                                  v_NATION              varchar2,
                                  v_CULTURESTATE        varchar2,
                                  v_HOUSEHOLDSCOPE      varchar2,
                                  v_HOUSEHOLDADDRESS    varchar2,
                                  v_ADDRESS             varchar2,
                                  v_CONTACTHISTORY      varchar2,
                                  v_VENERISMHISTORY     varchar2,
                                  v_INFACTWAY           varchar2,
                                  v_SAMPLESOURCE        varchar2,
                                  v_DETECTIONCONCLUSION varchar2,
                                  v_AFFIRMDATE          varchar2,
                                  v_AFFIRMLOCAL         varchar2,
                                  v_DIAGNOSEDATE        varchar2,
                                  v_DOCTOR              varchar2,
                                  v_WRITEDATE           varchar2,
                                  v_ALIKESYMBOL         varchar2,
                                  v_REMARK              varchar2,
                                  v_VAILD               varchar2,
                                  v_CREATOR             varchar2,
                                  v_CREATEDATE          varchar2,
                                  v_MENDER              varchar2,
                                  v_ALTERDATE           varchar2) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from zymosis_hiv z
     where z.hiv_id = v_HIVID;
    if v_count <= 0 then
      insert into zymosis_hiv
        (HIV_ID,
         REPORT_ID,
         REPORT_NO,
         MARITALSTATUS,
         NATION,
         CULTURESTATE,
         HOUSEHOLDSCOPE,
         HOUSEHOLDADDRESS,
         ADDRESS,
         CONTACTHISTORY,
         VENERISMHISTORY,
         INFACTWAY,
         SAMPLESOURCE,
         DETECTIONCONCLUSION,
         AFFIRMDATE,
         AFFIRMLOCAL,
         DIAGNOSEDATE,
         DOCTOR,
         WRITEDATE,
         ALIKESYMBOL,
         REMARK,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE)
      values
        (v_HIVID,
         v_REPORTID,
         v_REPORTNO,
         v_MARITALSTATUS,
         v_NATION,
         v_CULTURESTATE,
         v_HOUSEHOLDSCOPE,
         v_HOUSEHOLDADDRESS,
         v_ADDRESS,
         v_CONTACTHISTORY,
         v_VENERISMHISTORY,
         v_INFACTWAY,
         v_SAMPLESOURCE,
         v_DETECTIONCONCLUSION,
         v_AFFIRMDATE,
         v_AFFIRMLOCAL,
         v_DIAGNOSEDATE,
         v_DOCTOR,
         v_WRITEDATE,
         v_ALIKESYMBOL,
         v_REMARK,
         '1',
         v_CREATOR,
         v_CREATEDATE,
         v_MENDER,
         v_ALTERDATE);
    else
      update zymosis_hiv
         set REPORT_ID           = v_REPORTID,
             REPORT_NO           = v_REPORTNO,
             MARITALSTATUS       = v_MARITALSTATUS,
             NATION              = v_NATION,
             CULTURESTATE        = v_CULTURESTATE,
             HOUSEHOLDSCOPE      = v_HOUSEHOLDSCOPE,
             HOUSEHOLDADDRESS    = v_HOUSEHOLDADDRESS,
             ADDRESS             = v_ADDRESS,
             CONTACTHISTORY      = v_CONTACTHISTORY,
             VENERISMHISTORY     = v_VENERISMHISTORY,
             INFACTWAY           = v_INFACTWAY,
             SAMPLESOURCE        = v_SAMPLESOURCE,
             DETECTIONCONCLUSION = v_DETECTIONCONCLUSION,
             AFFIRMDATE          = v_AFFIRMDATE,
             AFFIRMLOCAL         = v_AFFIRMLOCAL,
             DIAGNOSEDATE        = v_DIAGNOSEDATE,
             DOCTOR              = v_DOCTOR,
             WRITEDATE           = v_WRITEDATE,
             ALIKESYMBOL         = v_ALIKESYMBOL,
             REMARK              = v_REMARK,
             MENDER              = v_MENDER,
             ALTERDATE           = v_ALTERDATE
       where HIV_ID = v_HIVID;
    
    end if;
  end;

  --������޸�ɳ����ԭ���Ⱦ
  PROCEDURE usp_AddOrModSYYYTReport(v_SZDYYTID            varchar,
                                    v_REPORTID            integer,
                                    v_REPORTNO            varchar,
                                    v_MARITALSTATUS       varchar,
                                    v_NATION              varchar,
                                    v_CULTURESTATE        varchar,
                                    v_HOUSEHOLDSCOPE      varchar,
                                    v_HOUSEHOLDADDRESS    varchar,
                                    v_ADDRESS             varchar,
                                    v_SYYYTGR             varchar,
                                    v_CONTACTHISTORY      varchar,
                                    v_VENERISMHISTORY     varchar,
                                    v_INFACTWAY           varchar,
                                    v_SAMPLESOURCE        varchar,
                                    v_DETECTIONCONCLUSION varchar,
                                    v_AFFIRMDATE          varchar,
                                    v_AFFIRMLOCAL         varchar,
                                    v_VAILD               varchar,
                                    v_CREATOR             varchar,
                                    v_CREATEDATE          varchar,
                                    v_MENDER              varchar,
                                    v_ALTERDATE           varchar) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from zymosis_szdyyt z
     where z.SZDYYT_ID = v_SZDYYTID;
    if v_count <= 0 then
      insert into zymosis_szdyyt
        (SZDYYT_ID,
         REPORT_ID,
         REPORT_NO,
         MARITALSTATUS,
         NATION,
         CULTURESTATE,
         HOUSEHOLDSCOPE,
         HOUSEHOLDADDRESS,
         ADDRESS,
         SYYYTGR,
         CONTACTHISTORY,
         VENERISMHISTORY,
         INFACTWAY,
         SAMPLESOURCE,
         DETECTIONCONCLUSION,
         AFFIRMDATE,
         AFFIRMLOCAL,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE)
      values
        (v_szdyytid,
         v_reportid,
         v_reportno,
         v_maritalstatus,
         v_nation,
         v_culturestate,
         v_householdscope,
         v_householdaddress,
         v_address,
         v_syyytgr,
         v_contacthistory,
         v_venerismhistory,
         v_infactway,
         v_samplesource,
         v_detectionconclusion,
         v_affirmdate,
         v_affirmlocal,
         '1',
         v_creator,
         v_createdate,
         v_mender,
         v_alterdate);
    else
      update zymosis_szdyyt
         set report_id           = v_reportid,
             report_no           = v_reportno,
             maritalstatus       = v_maritalstatus,
             nation              = v_nation,
             culturestate        = v_culturestate,
             householdscope      = v_householdscope,
             householdaddress    = v_householdaddress,
             address             = v_address,
             syyytgr             = v_syyytgr,
             contacthistory      = v_contacthistory,
             venerismhistory     = v_venerismhistory,
             infactway           = v_infactway,
             samplesource        = v_samplesource,
             detectionconclusion = v_detectionconclusion,
             affirmdate          = v_affirmdate,
             affirmlocal         = v_affirmlocal,
             vaild               = v_vaild,
             creator             = v_creator,
             createdate          = v_createdate,
             mender              = v_mender,
             alterdate           = v_alterdate
       where szdyyt_id = v_szdyytid;
    end if;
  end;

  --������޸��Ҹα���
  PROCEDURE usp_AddOrModHBVReport(v_HBVID varchar2,
                                  
                                  v_REPORTID      integer,
                                  v_HBSAGDATE     varchar2,
                                  v_FRISTDATE     varchar2,
                                  v_ALT           varchar2,
                                  v_ANTIHBC       varchar2,
                                  v_LIVERBIOPSY   varchar2,
                                  v_RECOVERYHBSAG varchar2,
                                  
                                  v_VAILD      varchar2,
                                  v_CREATOR    varchar2,
                                  v_CREATEDATE varchar2,
                                  v_MENDER     varchar2,
                                  v_ALTERDATE  varchar2) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from ZYMOSIS_HBV z
     where z.HBVID = v_HBVID
       and z.vaild = '1';
    if v_count <= 0 then
      insert into ZYMOSIS_HBV
        (HBVID,
         REPORTID,
         HBSAGDATE,
         FRISTDATE,
         ALT,
         ANTIHBC,
         LIVERBIOPSY,
         RECOVERYHBSAG,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE)
      values
        (v_HBVID,
         v_REPORTID,
         v_HBSAGDATE,
         v_FRISTDATE,
         v_ALT,
         v_ANTIHBC,
         v_LIVERBIOPSY,
         v_RECOVERYHBSAG,
         '1',
         v_CREATOR,
         v_CREATEDATE,
         v_MENDER,
         v_ALTERDATE);
    else
      update ZYMOSIS_HBV
         set REPORTID      = v_REPORTID,
             HBSAGDATE     = v_HBSAGDATE,
             FRISTDATE     = v_FRISTDATE,
             ALT           = v_ALT,
             ANTIHBC       = v_ANTIHBC,
             LIVERBIOPSY   = v_LIVERBIOPSY,
             RECOVERYHBSAG = v_RECOVERYHBSAG,
             MENDER        = v_MENDER,
             ALTERDATE     = v_ALTERDATE
       where HBVID = v_HBVID;
    
    end if;
  end;

  --������޸ļ���ʪ����Ŀ
  PROCEDURE usp_AddOrModJRSYReport(v_JRSY_ID             varchar,
                                   v_REPORTID            integer,
                                   v_REPORTNO            varchar,
                                   v_MARITALSTATUS       varchar,
                                   v_NATION              varchar,
                                   v_CULTURESTATE        varchar,
                                   v_HOUSEHOLDSCOPE      varchar,
                                   v_HOUSEHOLDADDRESS    varchar,
                                   v_ADDRESS             varchar,
                                   v_CONTACTHISTORY      varchar,
                                   v_VENERISMHISTORY     varchar,
                                   v_INFACTWAY           varchar,
                                   v_SAMPLESOURCE        varchar,
                                   v_DETECTIONCONCLUSION varchar,
                                   v_AFFIRMDATE          varchar,
                                   v_AFFIRMLOCAL         varchar,
                                   v_VAILD               varchar,
                                   v_CREATOR             varchar,
                                   v_CREATEDATE          varchar,
                                   v_MENDER              varchar,
                                   v_ALTERDATE           varchar) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from ZYMOSIS_JRSY z
     where z.JRSY_ID = v_JRSY_ID;
    if v_count <= 0 then
      insert into ZYMOSIS_JRSY
        (JRSY_ID,
         REPORT_ID,
         REPORT_NO,
         MARITALSTATUS,
         NATION,
         CULTURESTATE,
         HOUSEHOLDSCOPE,
         HOUSEHOLDADDRESS,
         ADDRESS,
         CONTACTHISTORY,
         VENERISMHISTORY,
         INFACTWAY,
         SAMPLESOURCE,
         DETECTIONCONCLUSION,
         AFFIRMDATE,
         AFFIRMLOCAL,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE)
      values
        (v_JRSY_ID,
         v_reportid,
         v_reportno,
         v_maritalstatus,
         v_nation,
         v_culturestate,
         v_householdscope,
         v_householdaddress,
         v_address,
         v_contacthistory,
         v_venerismhistory,
         v_infactway,
         v_samplesource,
         v_detectionconclusion,
         v_affirmdate,
         v_affirmlocal,
         '1',
         v_creator,
         v_createdate,
         v_mender,
         v_alterdate);
    else
      update ZYMOSIS_JRSY
         set report_id           = v_reportid,
             report_no           = v_reportno,
             maritalstatus       = v_maritalstatus,
             nation              = v_nation,
             culturestate        = v_culturestate,
             householdscope      = v_householdscope,
             householdaddress    = v_householdaddress,
             address             = v_address,
             contacthistory      = v_contacthistory,
             venerismhistory     = v_venerismhistory,
             infactway           = v_infactway,
             samplesource        = v_samplesource,
             detectionconclusion = v_detectionconclusion,
             affirmdate          = v_affirmdate,
             affirmlocal         = v_affirmlocal,
             vaild               = v_vaild,
             creator             = v_creator,
             createdate          = v_createdate,
             mender              = v_mender,
             alterdate           = v_alterdate
       where JRSY_ID = v_JRSY_ID;
    end if;
  end;

  --������޸ļ���H1N1���б���
  PROCEDURE usp_AddOrModH1N1Report(v_H1N1ID         varchar2,
                                   v_REPORTID       integer,
                                   v_CASETYPE       varchar2,
                                   v_HOSPITALSTATUS varchar2,
                                   v_ISCURE         varchar2,
                                   v_ISOVERSEAS     varchar2,
                                   v_VAILD          varchar2,
                                   v_CREATOR        varchar2,
                                   v_CREATEDATE     varchar2,
                                   v_MENDER         varchar2,
                                   v_ALTERDATE      varchar2) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from ZYMOSIS_H1N1 z
     where z.H1N1ID = v_H1N1ID
       and z.vaild = '1';
    if v_count <= 0 then
      insert into ZYMOSIS_H1N1
        (H1N1ID,
         REPORTID,
         CASETYPE,
         HOSPITALSTATUS,
         ISCURE,
         ISOVERSEAS,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE)
      values
        (v_H1N1ID,
         v_REPORTID,
         v_CASETYPE,
         v_HOSPITALSTATUS,
         v_ISCURE,
         v_ISOVERSEAS,
         '1',
         v_CREATOR,
         v_CREATEDATE,
         v_MENDER,
         v_ALTERDATE);
    else
      update ZYMOSIS_H1N1
         set REPORTID       = v_REPORTID,
             CASETYPE       = v_CASETYPE,
             HOSPITALSTATUS = v_HOSPITALSTATUS,
             ISCURE         = v_ISCURE,
             ISOVERSEAS     = v_ISOVERSEAS,
             MENDER         = v_MENDER,
             ALTERDATE      = v_ALTERDATE
       where H1N1ID = v_H1N1ID;
    end if;
  end;

  --������޸�����ڲ������
  PROCEDURE usp_AddOrModHFMDReport(v_HFMDID     varchar2,
                                   v_REPORTID   integer,
                                   v_LABRESULT  varchar2,
                                   v_ISSEVERE   varchar2,
                                   v_VAILD      varchar2,
                                   v_CREATOR    varchar2,
                                   v_CREATEDATE varchar2,
                                   v_MENDER     varchar2,
                                   v_ALTERDATE  varchar2) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from ZYMOSIS_HFMD z
     where z.HFMDID = v_HFMDID;
    if v_count <= 0 then
      insert into ZYMOSIS_HFMD
        (HFMDID,
         REPORTID,
         LABRESULT,
         ISSEVERE,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE)
      values
        (v_HFMDID,
         v_REPORTID,
         v_LABRESULT,
         v_ISSEVERE,
         '1',
         v_CREATOR,
         v_CREATEDATE,
         v_MENDER,
         v_ALTERDATE);
    else
      update ZYMOSIS_HFMD
         set REPORTID  = v_REPORTID,
             LABRESULT = v_LABRESULT,
             ISSEVERE  = v_ISSEVERE,
             MENDER    = v_MENDER,
             ALTERDATE = v_ALTERDATE
       where HFMDID = v_HFMDID;
    end if;
  end;

  --������޸�AFP�����
  PROCEDURE usp_AddOrModAFPReport(v_AFPID            varchar2,
                                  v_REPORTID         integer,
                                  v_HOUSEHOLDSCOPE   varchar2,
                                  v_HOUSEHOLDADDRESS varchar2,
                                  v_ADDRESS          varchar2,
                                  v_PALSYDATE        varchar2,
                                  v_PALSYSYMPTOM     varchar2,
                                  v_VAILD            varchar2,
                                  v_CREATOR          varchar2,
                                  v_CREATEDATE       varchar2,
                                  v_MENDER           varchar2,
                                  v_ALTERDATE        varchar2,
                                  v_DIAGNOSISDATE    varchar2) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from ZYMOSIS_AFP z
     where z.AFPID = v_AFPID;
    if v_count <= 0 then
      insert into ZYMOSIS_AFP
        (AFPID,
         REPORTID,
         HOUSEHOLDSCOPE,
         HOUSEHOLDADDRESS,
         ADDRESS,
         PALSYDATE,
         PALSYSYMPTOM,
         VAILD,
         CREATOR,
         CREATEDATE,
         MENDER,
         ALTERDATE,
         DIAGNOSISDATE)
      values
        (v_AFPID,
         v_REPORTID,
         v_HOUSEHOLDSCOPE,
         v_HOUSEHOLDADDRESS,
         v_ADDRESS,
         v_PALSYDATE,
         v_PALSYSYMPTOM,
         '1',
         v_CREATOR,
         v_CREATEDATE,
         v_MENDER,
         v_ALTERDATE,
         v_DIAGNOSISDATE);
    else
      update ZYMOSIS_AFP
         set REPORTID         = v_REPORTID,
             HOUSEHOLDSCOPE   = v_HOUSEHOLDSCOPE,
             HOUSEHOLDADDRESS = v_HOUSEHOLDADDRESS,
             ADDRESS          = v_ADDRESS,
             PALSYDATE        = v_PALSYDATE,
             PALSYSYMPTOM     = v_PALSYSYMPTOM,
             MENDER           = v_MENDER,
             ALTERDATE        = v_ALTERDATE,
             DIAGNOSISDATE    = v_DIAGNOSISDATE
       where AFPID = v_AFPID;
    end if;
  end;

  --�����б��濨  add  by  ywk 2013��8��21�� 15:47:00
  PROCEDURE usp_EditCardiovacular_Report(v_EditType      varchar, --��������
                                        
                                         v_REPORT_NO     varchar DEFAULT '', --���濨����                                 
                                         v_NOOFCLINIC    varchar DEFAULT '', --����� 
                                         v_PATID         varchar default '',
                                         v_NAME          varchar default '',
                                         v_IDNO          varchar default '',
                                         v_SEXID         varchar default '',
                                         v_BIRTH         varchar default '',
                                         v_AGE           varchar DEFAULT '',
                                         v_NATIONID      varchar default '',
                                         v_JOBID         varchar default '',
                                         v_OFFICEPLACE   varchar default '',
                                         v_CONTACTTEL    varchar default '',
                                         v_HKPROVICE     varchar default '',
                                         v_HKCITY        varchar default '',
                                         v_HKSTREET      varchar default '',
                                         v_HKADDRESSID   varchar default '',
                                         v_XZZPROVICE    varchar default '',
                                         v_XZZCITY       varchar default '',
                                         v_XZZSTREET     varchar default '',
                                         v_XZZADDRESSID  varchar default '',
                                         v_XZZCOMMITTEES varchar default '',
                                         v_XZZPARM         varchar default '',
                                         v_ICD             varchar default '',
                                         v_DIAGZWMXQCX     varchar default '',
                                         v_DIAGNCX         varchar default '',
                                         v_DIAGNGS         varchar default '',
                                         v_DIAGWFLNZZ      varchar default '',
                                         v_DIAGJXXJGS      varchar default '',
                                         v_DIAGXXCS        varchar default '',
                                         v_DIAGNOSISBASED  varchar default '',
                                         v_DIAGNOSEDATE    varchar default '',
                                         v_ISFIRSTSICK     varchar default '',
                                         v_DIAGHOSPITAL    varchar default '',
                                         v_OUTFLAG         varchar default '',
                                         v_DIEDATE         varchar default '',
                                         v_REPORTDEPT      varchar default '',
                                         v_REPORTUSERCODE  varchar default '',
                                         v_REPORTUSERNAME  varchar default '',
                                         v_REPORTDATE      varchar default '',
                                         v_CREATE_DATE     varchar default '',
                                         v_CREATE_USERCODE varchar default '',
                                         v_CREATE_USERNAME varchar default '',
                                         v_CREATE_DEPTCODE varchar default '',
                                         v_CREATE_DEPTNAME varchar default '',
                                         v_MODIFY_DATE     varchar default '',
                                         v_MODIFY_USERCODE varchar default '',
                                         v_MODIFY_USERNAME varchar default '',
                                         v_MODIFY_DEPTCODE varchar default '',
                                         v_MODIFY_DEPTNAME varchar default '',
                                         v_AUDIT_DATE      varchar default '',
                                         v_AUDIT_USERCODE  varchar default '',
                                         v_AUDIT_USERNAME  varchar default '',
                                         v_AUDIT_DEPTCODE  varchar default '',
                                         v_AUDIT_DEPTNAME  varchar default '',
                                         v_VAILD           varchar default '',
                                         v_CANCELREASON    varchar default '',
                                         v_STATE           varchar default '',
                                         v_CARDPARAM1      varchar default '',
                                         v_CARDPARAM2      varchar default '',
                                         v_CARDPARAM3      varchar default '',
                                         v_CARDPARAM4      varchar default '',
                                         v_CARDPARAM5      varchar default '',
                                         v_NOOFINPAT       varchar default '',
                                          v_REPORTID      varchar DEFAULT '',
                                         o_result          OUT empcurtyp) as
    v_Report_ID_new int;
  BEGIN
  
    --������Ⱦ�����濨
    IF v_edittype = '1' THEN
    
      select seq_cardiovascularcard_ID.Nextval
        into v_Report_ID_new
        from dual;
      --select 1 into v_Report_ID_new from dual;
      insert into CARDIOVASCULARCARD
        (ID,
         REPORT_NO,
         NOOFCLINIC,
         PATID,
         NAME,
         IDNO,
         SEXID,
         BIRTH,
         AGE,
         NATIONID,
         JOBID,
         OFFICEPLACE,
         CONTACTTEL,
         HKPROVICE,
         HKCITY,
         HKSTREET,
         HKADDRESSID,
         XZZPROVICE,
         XZZCITY,
         XZZSTREET,
         XZZADDRESSID,
         XZZCOMMITTEES,
         XZZPARM,
         ICD,
         DIAGZWMXQCX,
         DIAGNCX,
         DIAGNGS,
         DIAGWFLNZZ,
         DIAGJXXJGS,
         DIAGXXCS,
         DIAGNOSISBASED,
         DIAGNOSEDATE,
         ISFIRSTSICK,
         DIAGHOSPITAL,
         OUTFLAG,
         DIEDATE,
         REPORTDEPT,
         REPORTUSERCODE,
         REPORTUSERNAME,
         REPORTDATE,
         CREATE_DATE,
         CREATE_USERCODE,
         CREATE_USERNAME,
         CREATE_DEPTCODE,
         CREATE_DEPTNAME,
         MODIFY_DATE,
         MODIFY_USERCODE,
         MODIFY_USERNAME,
         MODIFY_DEPTCODE,
         MODIFY_DEPTNAME,
         AUDIT_DATE,
         AUDIT_USERCODE,
         AUDIT_USERNAME,
         AUDIT_DEPTCODE,
         AUDIT_DEPTNAME,
         VAILD,
         CANCELREASON,
         STATE,
         CARDPARAM1,
         CARDPARAM2,
         CARDPARAM3,
         CARDPARAM4,
         CARDPARAM5,
         NOOFINPAT)
      values
        (v_Report_ID_new,
         v_REPORT_NO,
         v_NOOFCLINIC,
         v_PATID,
         v_NAME,
         v_IDNO,
         v_SEXID,
         v_BIRTH,
         v_AGE,
         v_NATIONID,
         v_JOBID,
         v_OFFICEPLACE,
         v_CONTACTTEL,
         v_HKPROVICE,
         v_HKCITY,
         v_HKSTREET,
         v_HKADDRESSID,
         v_XZZPROVICE,
         v_XZZCITY,
         v_XZZSTREET,
         v_XZZADDRESSID,
         v_XZZCOMMITTEES,
         v_XZZPARM,
         v_ICD,
         v_DIAGZWMXQCX,
         v_DIAGNCX,
         v_DIAGNGS,
         v_DIAGWFLNZZ,
         v_DIAGJXXJGS,
         v_DIAGXXCS,
         v_DIAGNOSISBASED,
         v_DIAGNOSEDATE,
         v_ISFIRSTSICK,
         v_DIAGHOSPITAL,
         v_OUTFLAG,
         v_DIEDATE,
         v_REPORTDEPT,
         v_REPORTUSERCODE,
         v_REPORTUSERNAME,
         v_REPORTDATE,
         v_CREATE_DATE,
         v_CREATE_USERCODE,
         v_CREATE_USERNAME,
         v_CREATE_DEPTCODE,
         v_CREATE_DEPTNAME,
         v_MODIFY_DATE,
         v_MODIFY_USERCODE,
         v_MODIFY_USERNAME,
         v_MODIFY_DEPTCODE,
         v_MODIFY_DEPTNAME,
         v_AUDIT_DATE,
         v_AUDIT_USERCODE,
         v_AUDIT_USERNAME,
         v_AUDIT_DEPTCODE,
         v_AUDIT_DEPTNAME,
         v_VAILD,
         v_CANCELREASON,
         v_STATE,
         v_CARDPARAM1,
         v_CARDPARAM2,
         v_CARDPARAM3,
         v_CARDPARAM4,
         v_CARDPARAM5,
         v_NOOFINPAT);
    
      open o_result for
        select v_Report_ID_new from dual;
    
    end if;
  
    --�޸ı��������б��濨��Ϣ
    IF v_edittype = '2' THEN
      
      update CARDIOVASCULARCARD set  
         REPORT_NO=v_REPORT_NO,
         NOOFCLINIC=v_NOOFCLINIC,
         PATID=v_PATID,
         NAME=v_NAME,
         IDNO=v_IDNO,
         SEXID=v_SEXID,
         BIRTH=v_BIRTH,
         AGE=v_AGE,
         NATIONID=v_NATIONID,
         JOBID=v_JOBID,
         OFFICEPLACE=v_OFFICEPLACE,
         CONTACTTEL=v_CONTACTTEL,
         HKPROVICE=v_HKPROVICE,
         HKCITY=v_HKCITY,
         HKSTREET=v_HKSTREET,
         HKADDRESSID=v_HKADDRESSID,
         XZZPROVICE=v_XZZPROVICE,
         XZZCITY=v_XZZCITY,
         XZZSTREET=v_XZZSTREET,
         XZZADDRESSID=v_XZZADDRESSID,
         XZZCOMMITTEES=v_XZZCOMMITTEES,
         XZZPARM=v_XZZPARM,
         ICD=v_ICD,
         DIAGZWMXQCX=v_DIAGZWMXQCX,
         DIAGNCX=v_DIAGNCX,
         DIAGNGS=v_DIAGNGS,
         DIAGWFLNZZ=v_DIAGWFLNZZ,
         DIAGJXXJGS=v_DIAGJXXJGS,
         DIAGXXCS=v_DIAGXXCS,
         DIAGNOSISBASED=v_DIAGNOSISBASED,
         DIAGNOSEDATE=v_DIAGNOSEDATE,
         ISFIRSTSICK=v_ISFIRSTSICK,
         DIAGHOSPITAL=V_DIAGHOSPITAL,
         OUTFLAG=V_OUTFLAG,
         DIEDATE=V_DIEDATE,
         REPORTDEPT=V_REPORTDEPT,
         REPORTUSERCODE=V_REPORTUSERCODE,
         REPORTUSERNAME=V_REPORTUSERNAME,
         REPORTDATE=V_REPORTDATE,
         CREATE_DATE=nvl(v_CREATE_DATE, CREATE_DATE),
         CREATE_USERCODE=nvl(v_CREATE_USERCODE, CREATE_USERCODE),
         CREATE_USERNAME=nvl(v_CREATE_USERNAME, CREATE_USERNAME),
         CREATE_DEPTCODE=nvl(v_CREATE_DEPTCODE, CREATE_DEPTCODE),
         CREATE_DEPTNAME=nvl(v_CREATE_DEPTNAME, CREATE_DEPTNAME),
         MODIFY_DATE=nvl(v_MODIFY_DATE, MODIFY_DATE),
         MODIFY_USERCODE=nvl(v_MODIFY_USERCODE, MODIFY_USERCODE),
         MODIFY_USERNAME=nvl(v_MODIFY_USERNAME, MODIFY_USERNAME),
         MODIFY_DEPTCODE=nvl(v_MODIFY_DEPTCODE, MODIFY_DEPTCODE),
         MODIFY_DEPTNAME=nvl(v_MODIFY_DEPTNAME, MODIFY_DEPTNAME),
         AUDIT_DATE=nvl(v_AUDIT_DATE, AUDIT_DATE),
         AUDIT_USERCODE=nvl(v_AUDIT_USERCODE, AUDIT_USERCODE),
         AUDIT_USERNAME=nvl(v_AUDIT_USERNAME, AUDIT_USERNAME),
         AUDIT_DEPTCODE=nvl(v_AUDIT_DEPTCODE, AUDIT_DEPTCODE),
         AUDIT_DEPTNAME=nvl(v_AUDIT_DEPTNAME, AUDIT_DEPTNAME),
         VAILD=V_VAILD,
         CANCELREASON=V_CANCELREASON,
         STATE=V_STATE,
         CARDPARAM1=V_CARDPARAM1,
         CARDPARAM2=V_CARDPARAM2,
         CARDPARAM3=V_CARDPARAM3,
         CARDPARAM4=V_CARDPARAM4,
         CARDPARAM5=V_CARDPARAM5,
         NOOFINPAT=V_NOOFINPAT
      where ID=v_REPORTID;
    
      open o_result for
    
    select v_REPORTID from dual;
    end if;
  
    --���������б��濨��Ϣ
    IF v_edittype = '3' THEN
    
      update CARDIOVASCULARCARD ca
         set ca.vaild = 0
       where ca.id = v_REPORTID;
      --select 1 into v_Report_ID_new from dual;
    
      open o_result for
        select 1 into v_Report_ID_new from dual;
    end if;
  
    --���ݴ���������б��濨ID��ѯ���濨��Ϣ
    IF v_edittype = '4' THEN
    
      open o_result for
        select * from CARDIOVASCULARCARD a where a.id = v_REPORTID;
    
    end if;
  
  end;

END;
/
