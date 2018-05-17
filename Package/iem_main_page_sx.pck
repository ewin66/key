CREATE OR REPLACE PACKAGE iem_main_page_sx IS
  TYPE empcurtyp IS REF CURSOR;

  /*
  * ���빦������ҳ ����Ӥ����Ϣ
  */
  PROCEDURE usp_insert_iem_main_ObsBaby(v_iem_mainpage_no NUMERIC,
                                        v_TC              VARCHAR, --̥��
                                        v_CC              VARCHAR, -- ����
                                        v_TB              VARCHAR, -- ̥��
                                        v_CFHYPLD         VARCHAR, --�����������Ѷ�
                                        v_MIDWIFERY       VARCHAR, --�Ӳ���
                                        v_SEX             VARCHAR, --�Ա�
                                        v_APJ             VARCHAR, -- ����������
                                        v_HEIGH           VARCHAR, --��
                                        v_WEIGHT          VARCHAR, --����
                                        v_CCQK            VARCHAR, --�������
                                        v_BITHDAY         VARCHAR, --����ʱ��
                                        v_FMFS            VARCHAR, --     ���䷽ʽ
                                        v_CYQK            VARCHAR,
                                        v_create_user     VARCHAR);

  /*
  * ά��������ҳ��Ϣ MOdify by xlb 2013-07-03 ��������Ž����� �����ֶ�
  */
  PROCEDURE usp_Edit_Iem_BasicInfo_sx(v_edittype        varchar2 default '',
                                      v_IEM_MAINPAGE_NO varchar2 default '', ---- '������ҳ��ʶ';
                                      v_PATNOOFHIS      varchar2 default '', ---- '������';
                                      v_NOOFINPAT       varchar2 default '', ---- '������ҳ���';
                                      v_PAYID           varchar2 default '', ---- 'ҽ�Ƹ��ʽID';
                                      v_SOCIALCARE      varchar2 default '', ---- '�籣����';

                                      v_INCOUNT varchar2 default '', ---- '��Ժ����';
                                      v_NAME    varchar2 default '', ---- '��������';
                                      v_SEXID   varchar2 default '', ---- '�Ա�';
                                      v_BIRTH   varchar2 default '', ---- '����';
                                      v_MARITAL varchar2 default '', ---- '����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����';

                                      v_JOBID         varchar2 default '', ---- 'ְҵ';
                                      v_NATIONALITYID varchar2 default '', ---- '����ID';
                                      v_NATIONID      varchar2 default '', --����
                                      v_IDNO          varchar2 default '', ---- '���֤����';
                                      v_ORGANIZATION  varchar2 default '', ---- '������λ';
                                      v_OFFICEPLACE   varchar2 default '', ---- '������λ��ַ';

                                      v_OFFICETEL      varchar2 default '', ---- '������λ�绰';
                                      v_OFFICEPOST     varchar2 default '', ---- '������λ�ʱ�';
                                      v_CONTACTPERSON  varchar2 default '', ---- '��ϵ������';
                                      v_RELATIONSHIP   varchar2 default '', ---- '����ϵ�˹�ϵ';
                                      v_CONTACTADDRESS varchar2 default '', ---- '��ϵ�˵�ַ';

                                      v_CONTACTTEL varchar2 default '', ---- '��ϵ�˵绰';
                                      v_ADMITDATE  varchar2 default '', ---- '��Ժʱ��';
                                      v_ADMITDEPT  varchar2 default '', ---- '��Ժ����';
                                      v_ADMITWARD  varchar2 default '', ---- '��Ժ����';
                                      v_TRANS_DATE varchar2 default '', ---- 'תԺʱ��';

                                      v_TRANS_ADMITDEPT varchar2 default '', ---- 'תԺ�Ʊ�';
                                      v_TRANS_ADMITWARD varchar2 default '', ---- 'תԺ����';
                                      v_OUTWARDDATE     varchar2 default '', ---- '��Ժʱ��';
                                      v_OUTHOSDEPT      varchar2 default '', ---- '��Ժ����';
                                      v_OUTHOSWARD      varchar2 default '', ---- '��Ժ����';

                                      v_ACTUALDAYS               varchar2 default '', ---- 'ʵ��סԺ����';
                                      v_PATHOLOGY_DIAGNOSIS_NAME varchar2 default '', ---- '�����������';
                                      v_PATHOLOGY_OBSERVATION_SN varchar2 default '', ---- '������� ';
                                      v_ALLERGIC_DRUG            varchar2 default '', ---- '����ҩ��';
                                      v_SECTION_DIRECTOR         varchar2 default '', ---- '������';

                                      v_DIRECTOR               varchar2 default '', ---- '������������ҽʦ';
                                      v_VS_EMPLOYEE_CODE       varchar2 default '', ---- '����ҽʦ';
                                      v_RESIDENT_EMPLOYEE_CODE varchar2 default '', ---- 'סԺҽʦ';
                                      v_REFRESH_EMPLOYEE_CODE  varchar2 default '', ---- '����ҽʦ';
                                      v_DUTY_NURSE             varchar2 default '', ---- '���λ�ʿ';

                                      v_INTERNE                varchar2 default '', ---- 'ʵϰҽʦ';
                                      v_CODING_USER            varchar2 default '', ---- '����Ա';
                                      v_MEDICAL_QUALITY_ID     varchar2 default '', ---- '��������';
                                      v_QUALITY_CONTROL_DOCTOR varchar2 default '', ---- '�ʿ�ҽʦ';
                                      v_QUALITY_CONTROL_NURSE  varchar2 default '', ---- '�ʿػ�ʿ';

                                      v_QUALITY_CONTROL_DATE varchar2 default '', ---- '�ʿ�ʱ��';
                                      v_XAY_SN               varchar2 default '', ---- 'x�߼���';
                                      v_CT_SN                varchar2 default '', ---- 'CT����';
                                      v_MRI_SN               varchar2 default '', ---- 'mri����';
                                      v_DSA_SN               varchar2 default '', ---- 'Dsa����';

                                      v_BLOODTYPE      varchar2 default '', ---- 'Ѫ��';
                                      v_RH             varchar2 default '', ---- 'Rh';
                                      v_IS_COMPLETED   varchar2 default '', ---- '��ɷ� y/n ';
                                      v_COMPLETED_TIME varchar2 default '', ---- '���ʱ��';
                                      v_VALIDE         varchar2 default '1', ---- '���Ϸ� 1/0';

                                      v_CREATE_USER   varchar2 default '', ---- '�����˼�¼��';
                                      v_CREATE_TIME   varchar2 default '', ---- '����ʱ��';
                                      v_MODIFIED_USER varchar2 default '', ---- '�޸Ĵ˼�¼��';
                                      v_MODIFIED_TIME varchar2 default '', ---- '�޸�ʱ��';
                                      v_ZYMOSIS       varchar2 default '', ---- 'ҽԺ��Ⱦ��';

                                      v_HURT_TOXICOSIS_ELE_ID   varchar2 default '', ---- '���ˡ��ж����ⲿ����';
                                      v_HURT_TOXICOSIS_ELE_Name varchar2 default '', ---- '���ˡ��ж����ⲿ����';
                                      v_ZYMOSISSTATE            varchar2 default '', ---- 'ҽԺ��Ⱦ��״̬';
                                      v_PATHOLOGY_DIAGNOSIS_ID  varchar2 default '', ---- '������ϱ��';
                                      v_MONTHAGE                varchar2 default '', ---- '�����䲻��1����ģ� ����(��)';
                                      v_WEIGHT                  varchar2 default '', ---- '��������������(��)';

                                      v_INWEIGHT         varchar2 default '', ---- '��������Ժ����(��)';
                                      v_INHOSTYPE        varchar2 default '', ---- '��Ժ;��:1.����  2.����  3.����ҽ�ƻ���ת��  9.����';
                                      v_OUTHOSTYPE       varchar2 default '', ---- '��Ժ��ʽ �� 1.ҽ����Ժ  2.ҽ��תԺ 3.ҽ��ת���������������/��������Ժ 4.��ҽ����Ժ5.����9.����';
                                      v_RECEIVEHOSPITAL  varchar2 default '', ---- '2.ҽ��תԺ�������ҽ�ƻ������ƣ�';
                                      v_RECEIVEHOSPITAL2 varchar2 default '', ---- '3.ҽ��ת���������������/��������Ժ�������ҽ�ƻ�����;

                                      v_AGAININHOSPITAL       varchar2 default '', ---- '�Ƿ��г�Ժ31������סԺ�ƻ� �� 1.��  2.��';
                                      v_AGAININHOSPITALREASON varchar2 default '', ---- '��Ժ31������סԺ�ƻ� Ŀ��:            ';
                                      v_BEFOREHOSCOMADAY      varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ��';
                                      v_BEFOREHOSCOMAHOUR     varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ     Сʱ';
                                      v_BEFOREHOSCOMAMINUTE   varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ����';

                                      v_LATERHOSCOMADAY    varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ��';
                                      v_LATERHOSCOMAHOUR   varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    Сʱ';
                                      v_LATERHOSCOMAMINUTE varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ����';
                                      v_CARDNUMBER         varchar2 default '', ---- '��������';
                                      v_ALLERGIC_FLAG      varchar2 default '', ---- 'ҩ�����1.�� 2.��';

                                      v_AUTOPSY_FLAG     varchar2 default '', ---- '��������ʬ�� �� 1.��  2.��';
                                      v_CSD_PROVINCEID   varchar2 default '', ---- '������ ʡ';
                                      v_CSD_CITYID       varchar2 default '', ---- '������ ��';
                                      v_CSD_DISTRICTID   varchar2 default '', ---- '������ ��';
                                      v_CSD_PROVINCENAME varchar2 default '', ---- '������ ʡ����';

                                      v_CSD_CITYNAME     varchar2 default '', ---- '������ ������';
                                      v_CSD_DISTRICTNAME varchar2 default '', ---- '������ ������';
                                      v_XZZ_PROVINCEID   varchar2 default '', ---- '��סַ ʡ';
                                      v_XZZ_CITYID       varchar2 default '', ---- '��סַ ��';
                                      v_XZZ_DISTRICTID   varchar2 default '', ---- '��סַ ��';

                                      v_XZZ_PROVINCENAME varchar2 default '', ---- '��סַ ʡ����';
                                      v_XZZ_CITYNAME     varchar2 default '', ---- '��סַ ������';
                                      v_XZZ_DISTRICTNAME varchar2 default '', ---- '��סַ ������';
                                      v_XZZ_TEL          varchar2 default '', ---- '��סַ �绰';
                                      v_XZZ_POST         varchar2 default '', ---- '��סַ �ʱ�';

                                      v_HKDZ_PROVINCEID   varchar2 default '', ---- '���ڵ�ַ ʡ';
                                      v_HKDZ_CITYID       varchar2 default '', ---- '���ڵ�ַ ��';
                                      v_HKDZ_DISTRICTID   varchar2 default '', ---- '���ڵ�ַ ��';
                                      v_HKDZ_PROVINCENAME varchar2 default '', ---- '���ڵ�ַ ʡ����';
                                      v_HKDZ_CITYNAME     varchar2 default '', ---- '���ڵ�ַ ������';

                                      v_HKDZ_DISTRICTNAME varchar2 default '', ---- '���ڵ�ַ ������';
                                      v_HKDZ_POST         varchar2 default '', ---- '�������ڵ��ʱ�';
                                      v_JG_PROVINCEID     varchar2 default '', ---- '���� ʡ����';
                                      v_JG_CITYID         varchar2 default '', ---- '���� ������';
                                      v_JG_PROVINCENAME   varchar2 default '', ---- '���� ʡ����';
                                      v_JG_CITYNAME       varchar2 default '', ---- '���� ������'
                                      v_Age               varchar2 default '',

                                      v_CURE_TYPE   VARCHAR2 default '', ----  Y    ������� �� 1.��ҽ�� 1.1 ��ҽ   1.2����ҽ��    2.����ҽ     3.��ҽ
                                      v_MZZYZD_NAME VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ�
                                      v_MZZYZD_CODE VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ� ����
                                      v_MZXYZD_NAME VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ�
                                      v_MZXYZD_CODE VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ� ����
                                      v_SSLCLJ      VARCHAR2 default '', ---- Y   ʵʩ�ٴ�·������ 1. ��ҽ  2. ��ҽ  3 ��
                                      v_ZYZJ        VARCHAR2 default '', ---- Y   ʹ��ҽ�ƻ�����ҩ�Ƽ����� 1.��  2. ��

                                      v_ZYZLSB VARCHAR2 default '', ---- Y   ʹ����ҽ�����豸����  1.�� 2. ��
                                      v_ZYZLJS VARCHAR2 default '', ---- Y   ʹ����ҽ���Ƽ������� 1. ��  2. ��
                                      v_BZSH   VARCHAR2 default '', ---- Y   ��֤ʩ������ 1.��  2. ��
                                       v_outHosStatus VARCHAR2,---��Ժ״��
                                      v_JBYNZZ VARCHAR2,
                                     v_MZYCY VARCHAR2,
                                      v_InAndOutHos VARCHAR2,
                                     v_LCYBL VARCHAR2,
                                     v_FSYBL VARCHAR2,
                                      v_qJCount VARCHAR2,
                                      v_successCount VARCHAR2,
                                      v_InPatLY VARCHAR2,
                                      v_asaScore VARCHAR2,
                                      o_result OUT empcurtyp);

  /*
  *��ѯ������ҳ��Ϣ
  **********/
  ---Modify by xlb 2013-07-02 �����ֶ�
  PROCEDURE usp_getieminfo_sx(v_noofinpat INT,
                              o_result    OUT empcurtyp,
                              o_result1   OUT empcurtyp,
                              o_result2   OUT empcurtyp,
                              o_result3   OUT empcurtyp);

  PROCEDURE usp_edit_iem_mainpage_oper_sx(v_iem_mainpage_no     NUMERIC,
                                          v_operation_code      VARCHAR,
                                          v_operation_date      VARCHAR,
                                          v_operation_name      VARCHAR,
                                          v_execute_user1       VARCHAR,
                                          v_execute_user2       VARCHAR,
                                          v_execute_user3       VARCHAR,
                                          v_anaesthesia_type_id NUMERIC,
                                          v_close_level         NUMERIC,
                                          v_anaesthesia_user    VARCHAR,
                                          --v_Valide numeric ,
                                          v_create_user     VARCHAR,
                                          v_OPERATION_LEVEL varchar,
                                          --v_Create_Time varchar(19)
                                          --v_Cancel_User varchar(10) ,
                                          v_OperInTimes VARCHAR2
                                          --v_Cancel_Time varchar(19)
                                          );

  PROCEDURE usp_edif_iem_mainpage_diag_sx(v_iem_mainpage_no   VARCHAR,
                                          v_diagnosis_type_id VARCHAR,
                                          v_diagnosis_code    VARCHAR,
                                          v_diagnosis_name    VARCHAR,
                                          v_status_id         VARCHAR,
                                          v_order_value       VARCHAR,
                                          --v_Valide numeric ,
                                          v_create_user VARCHAR,
                                          v_type        varchar,
                                          v_typeName    varchar
                                          --v_Create_Time varchar(19) ,
                                          --v_Cancel_User varchar(10) ,
                                          --v_Cancel_Time varchar(19)
                                          );

  --���²�����ҳ��Ϣ�󣬶Բ�����Ϣ���������ͬ�� add by ywk ����һ������������ 15:20:27
  PROCEDURE usp_Edit_Iem_PaientInfo_sx(v_NOOFINPAT      varchar2 default '', ---- '������ҳ���';
                                       v_NAME           varchar2 default '', ---- '��������';
                                       v_SEXID          varchar2 default '', ---- '�Ա�';
                                       v_BIRTH          varchar2 default '', ---- '����';
                                       v_Age            INTEGER default 1, --����
                                       v_IDNO           varchar2 default '', ---- '���֤����';
                                       v_MARITAL        varchar2 default '', ---- '����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����';
                                       v_JOBID          varchar2 default '', ---- 'ְҵ';
                                       v_CSD_PROVINCEID varchar2 default '', ---- '������ ʡ';
                                       v_CSD_CITYID     varchar2 default '', ---- '������ ��';
                                       v_NATIONID       varchar2 default '', --����
                                       v_NATIONALITYID  varchar2 default '', ---- '����ID';
                                       v_JG_PROVINCEID  varchar2 default '', ---- '���� ʡ';
                                       v_JG_CITYID      varchar2 default '', ---- '���� ��';
                                       v_OFFICEPLACE    varchar2 default '', ---- '������λ��ַ';
                                       v_OFFICETEL      varchar2 default '', ---- '������λ�绰';
                                       v_OFFICEPOST     varchar2 default '', ---- '������λ�ʱ�';
                                       v_HKDZ_POST      varchar2 default '', ---- '�������ڵ��ʱ�';
                                       v_CONTACTPERSON  varchar2 default '', ---- '��ϵ������';
                                       v_RELATIONSHIP   varchar2 default '', ---- '����ϵ�˹�ϵ';
                                       v_CONTACTADDRESS varchar2 default '', ---- '��ϵ�˵�ַ';
                                       v_CONTACTTEL     varchar2 default '', ---- '��ϵ�˵绰';
                                       v_ADMITDEPT      varchar2 default '', ---- '��Ժ����';
                                       v_ADMITWARD      varchar2 default '', ---- '��Ժ����';
                                       v_ADMITDATE      varchar2 default '', ---- '��Ժʱ��';
                                       v_OUTWARDDATE    varchar2 default '', ---- '��Ժʱ��';
                                       v_OUTHOSDEPT     varchar2 default '', ---- '��Ժ����';
                                       v_OUTHOSWARD     varchar2 default '', ---- '��Ժ����';
                                       v_ACTUALDAYS     varchar2 default '', ---- 'ʵ��סԺ����';
                                       v_AgeStr         varchar2 default '', ---- '���� ��ȷ������;2012��5��9��9:31:03 ����ά�� �����޸ģ�
                                       v_PatId          varchar2 default '', --�����ĸ��ʽ add by ywk 2012��5��14�� 16:02:13
                                       v_CardNo         varchar2 default '', --��������
                                       -----add by ywk  2012��5��16��9:45:27 Inpatient��l�����Ӳ�����ҳ����Ӧ���ֶ�
                                       v_Districtid     varchar2 default '', --�����ء��ء�
                                       v_xzzproviceid   varchar2 default '', --����סַʡ
                                       v_xzzcityid      varchar2 default '', --����סַ��
                                       v_xzzdistrictid  varchar2 default '', --����סַ��
                                       v_xzztel         varchar2 default '', --����סַ�绰
                                       v_hkdzproviceid  varchar2 default '', --����סַʡ
                                       v_hkzdcityid     varchar2 default '', --����סַ��
                                       v_hkzddistrictid varchar2 default '', --����סַ��
                                       v_xzzpost        varchar2 default '', --��סַ�ʱ�
                                       v_isupdate       varchar2 default '' ---2012��5��24�� 17:19:10 ywk �Ƿ�������֤���ֶ�

                                       );

  --�����ҳĬ�ϱ��������
  --add by ywk 2012��5��17�� 09:36:46

  PROCEDURE usp_GetDefaultInpatient(o_result OUT empcurtyp);

  --���ݲ�����ҳ��š�ȡ�ò��˵���Ϣ ������䲡����ҳ
  PROCEDURE usp_GetInpatientByNo(v_noofinpat varchar2 default '',
                                 o_result    OUT empcurtyp);

  PROCEDURE usp_AddOrModIemFeeZY(v_id        varchar,
                                 v_NOOFINPAT varchar,
                                 v_TOTAL     varchar,
                                 v_OWNFEE    varchar,
                                 v_YBYLFY    varchar,
                                 v_ZYBZLZF   varchar,
                                 v_ZYBZLZHZF varchar,
                                 v_YBZLFY    varchar,
                                 v_CARE      varchar,
                                 v_ZHQTFY    varchar,
                                 v_BLZDF     varchar,
                                 v_SYSZDF    varchar,
                                 v_YXXZDF    varchar,
                                 v_LCZDF     varchar,
                                 v_FSSZLF    varchar,
                                 v_LCWLZLF   varchar,
                                 v_SSZLF     varchar,
                                 v_MZF       varchar,
                                 v_SSF       varchar,
                                 v_KFF       varchar,
                                 v_ZYZDF     varchar,
                                 v_ZYZLF     varchar,
                                 v_ZYWZ      varchar,
                                 v_ZYGS      varchar,
                                 v_ZCYJF     varchar,
                                 v_ZYTNZL    varchar,
                                 v_ZYGCZL    varchar,
                                 v_ZYTSZL    varchar,
                                 v_ZYQT      varchar,
                                 v_ZYTSTPJG  varchar,
                                 v_BZSS      varchar,
                                 v_XYF       varchar,
                                 v_KJYWF     varchar,
                                 v_CPMEDICAL varchar,
                                 v_YLJGZYZJF varchar,
                                 v_CMEDICAL  varchar,
                                 v_BLOODFEE  varchar,
                                 v_XDBLZPF   varchar,
                                 v_QDBLZPF   varchar,
                                 v_NXYZLZPF  varchar,
                                 v_XBYZLZPF  varchar,
                                 v_JCYYCXCLF varchar,
                                 v_ZLYYCXCLF varchar,
                                 v_SSYYCXCLF varchar,
                                 v_OTHERFEE  varchar,
                                 v_VALID     varchar);

  PROCEDURE usp_GetIemFeeZYbyInpat(v_noofinpat varchar,
                                   o_result    OUT empcurtyp);

end;
/
CREATE OR REPLACE PACKAGE BODY iem_main_page_sx IS

  /*
  * ���벡����ҳ�� ����Ӥ����Ϣ
  */
  PROCEDURE usp_insert_iem_main_ObsBaby(v_iem_mainpage_no NUMERIC,
                                        v_TC              VARCHAR, --̥��
                                        v_CC              VARCHAR, -- ����
                                        v_TB              VARCHAR, -- ̥��
                                        v_CFHYPLD         VARCHAR, --�����������Ѷ�
                                        v_MIDWIFERY       VARCHAR, --�Ӳ���
                                        v_SEX             VARCHAR, --�Ա�
                                        v_APJ             VARCHAR, -- ����������
                                        v_HEIGH           VARCHAR, --��
                                        v_WEIGHT          VARCHAR, --����
                                        v_CCQK            VARCHAR, --�������
                                        v_BITHDAY         VARCHAR, --����ʱ��
                                        v_FMFS            VARCHAR, --     ���䷽ʽ
                                        v_CYQK            VARCHAR,
                                        v_create_user     VARCHAR) as
  begin
    insert into IEM_MAINPAGE_OBSTETRICSBABY
      (IEM_MAINPAGE_OBSBABYID,
       IEM_MAINPAGE_NO,
       TC, --̥��
       CC, -- ����
       TB, -- ̥��
       CFHYPLD, --�����������Ѷ�
       MIDWIFERY, --�Ӳ���
       SEX, --�Ա�
       APJ, -- ����������
       HEIGH, --��
       WEIGHT, --����
       CCQK, --�������
       BITHDAY, --����ʱ��
       FMFS, --      ���䷽ʽ
       CYQK, --��Ժ���
       create_user,
       create_time)
    values
      (SEQ_IEM_MAINPAGE_OBSBABY_ID.Nextval,
       v_IEM_MAINPAGE_NO,
       v_TC, --̥��
       v_CC, -- ����
       v_TB, -- ̥��
       v_CFHYPLD, --�����������Ѷ�
       v_MIDWIFERY, --�Ӳ���
       v_SEX, --�Ա�
       v_APJ, -- ����������
       v_HEIGH, --��
       v_WEIGHT, --����
       v_CCQK, --�������
       v_BITHDAY, --����ʱ��
       v_FMFS, --      ���䷽ʽ
       v_CYQK,
       v_create_user, -- varchar(10)
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss'));
  end;

  /**(*********�༭������ҳ������Ϣ********************************************/
  PROCEDURE usp_Edit_Iem_BasicInfo_sx(v_edittype        varchar2 default '',
                                      v_IEM_MAINPAGE_NO varchar2 default '', ---- '������ҳ��ʶ';
                                      v_PATNOOFHIS      varchar2 default '', ---- '������';
                                      v_NOOFINPAT       varchar2 default '', ---- '������ҳ���';
                                      v_PAYID           varchar2 default '', ---- 'ҽ�Ƹ��ʽID';
                                      v_SOCIALCARE      varchar2 default '', ---- '�籣����';

                                      v_INCOUNT varchar2 default '', ---- '��Ժ����';
                                      v_NAME    varchar2 default '', ---- '��������';
                                      v_SEXID   varchar2 default '', ---- '�Ա�';
                                      v_BIRTH   varchar2 default '', ---- '����';
                                      v_MARITAL varchar2 default '', ---- '����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����';

                                      v_JOBID         varchar2 default '', ---- 'ְҵ';
                                      v_NATIONALITYID varchar2 default '', ---- '����ID';
                                      v_NATIONID      varchar2 default '', --����
                                      v_IDNO          varchar2 default '', ---- '���֤����';
                                      v_ORGANIZATION  varchar2 default '', ---- '������λ';
                                      v_OFFICEPLACE   varchar2 default '', ---- '������λ��ַ';

                                      v_OFFICETEL      varchar2 default '', ---- '������λ�绰';
                                      v_OFFICEPOST     varchar2 default '', ---- '������λ�ʱ�';
                                      v_CONTACTPERSON  varchar2 default '', ---- '��ϵ������';
                                      v_RELATIONSHIP   varchar2 default '', ---- '����ϵ�˹�ϵ';
                                      v_CONTACTADDRESS varchar2 default '', ---- '��ϵ�˵�ַ';

                                      v_CONTACTTEL varchar2 default '', ---- '��ϵ�˵绰';
                                      v_ADMITDATE  varchar2 default '', ---- '��Ժʱ��';
                                      v_ADMITDEPT  varchar2 default '', ---- '��Ժ����';
                                      v_ADMITWARD  varchar2 default '', ---- '��Ժ����';
                                      v_TRANS_DATE varchar2 default '', ---- 'תԺʱ��';

                                      v_TRANS_ADMITDEPT varchar2 default '', ---- 'תԺ�Ʊ�';
                                      v_TRANS_ADMITWARD varchar2 default '', ---- 'תԺ����';
                                      v_OUTWARDDATE     varchar2 default '', ---- '��Ժʱ��';
                                      v_OUTHOSDEPT      varchar2 default '', ---- '��Ժ����';
                                      v_OUTHOSWARD      varchar2 default '', ---- '��Ժ����';

                                      v_ACTUALDAYS               varchar2 default '', ---- 'ʵ��סԺ����';
                                      v_PATHOLOGY_DIAGNOSIS_NAME varchar2 default '', ---- '�����������';
                                      v_PATHOLOGY_OBSERVATION_SN varchar2 default '', ---- '������� ';
                                      v_ALLERGIC_DRUG            varchar2 default '', ---- '����ҩ��';
                                      v_SECTION_DIRECTOR         varchar2 default '', ---- '������';

                                      v_DIRECTOR               varchar2 default '', ---- '������������ҽʦ';
                                      v_VS_EMPLOYEE_CODE       varchar2 default '', ---- '����ҽʦ';
                                      v_RESIDENT_EMPLOYEE_CODE varchar2 default '', ---- 'סԺҽʦ';
                                      v_REFRESH_EMPLOYEE_CODE  varchar2 default '', ---- '����ҽʦ';
                                      v_DUTY_NURSE             varchar2 default '', ---- '���λ�ʿ';

                                      v_INTERNE                varchar2 default '', ---- 'ʵϰҽʦ';
                                      v_CODING_USER            varchar2 default '', ---- '����Ա';
                                      v_MEDICAL_QUALITY_ID     varchar2 default '', ---- '��������';
                                      v_QUALITY_CONTROL_DOCTOR varchar2 default '', ---- '�ʿ�ҽʦ';
                                      v_QUALITY_CONTROL_NURSE  varchar2 default '', ---- '�ʿػ�ʿ';

                                      v_QUALITY_CONTROL_DATE varchar2 default '', ---- '�ʿ�ʱ��';
                                      v_XAY_SN               varchar2 default '', ---- 'x�߼���';
                                      v_CT_SN                varchar2 default '', ---- 'CT����';
                                      v_MRI_SN               varchar2 default '', ---- 'mri����';
                                      v_DSA_SN               varchar2 default '', ---- 'Dsa����';

                                      v_BLOODTYPE      varchar2 default '', ---- 'Ѫ��';
                                      v_RH             varchar2 default '', ---- 'Rh';
                                      v_IS_COMPLETED   varchar2 default '', ---- '��ɷ� y/n ';
                                      v_COMPLETED_TIME varchar2 default '', ---- '���ʱ��';
                                      v_VALIDE         varchar2 default '1', ---- '���Ϸ� 1/0';

                                      v_CREATE_USER   varchar2 default '', ---- '�����˼�¼��';
                                      v_CREATE_TIME   varchar2 default '', ---- '����ʱ��';
                                      v_MODIFIED_USER varchar2 default '', ---- '�޸Ĵ˼�¼��';
                                      v_MODIFIED_TIME varchar2 default '', ---- '�޸�ʱ��';
                                      v_ZYMOSIS       varchar2 default '', ---- 'ҽԺ��Ⱦ��';

                                      v_HURT_TOXICOSIS_ELE_ID   varchar2 default '', ---- '���ˡ��ж����ⲿ����';
                                      v_HURT_TOXICOSIS_ELE_Name varchar2 default '', ---- '���ˡ��ж����ⲿ����';
                                      v_ZYMOSISSTATE            varchar2 default '', ---- 'ҽԺ��Ⱦ��״̬';
                                      v_PATHOLOGY_DIAGNOSIS_ID  varchar2 default '', ---- '������ϱ��';
                                      v_MONTHAGE                varchar2 default '', ---- '�����䲻��1����ģ� ����(��)';
                                      v_WEIGHT                  varchar2 default '', ---- '��������������(��)';

                                      v_INWEIGHT         varchar2 default '', ---- '��������Ժ����(��)';
                                      v_INHOSTYPE        varchar2 default '', ---- '��Ժ;��:1.����  2.����  3.����ҽ�ƻ���ת��  9.����';
                                      v_OUTHOSTYPE       varchar2 default '', ---- '��Ժ��ʽ �� 1.ҽ����Ժ  2.ҽ��תԺ 3.ҽ��ת���������������/��������Ժ 4.��ҽ����Ժ5.����9.����';
                                      v_RECEIVEHOSPITAL  varchar2 default '', ---- '2.ҽ��תԺ�������ҽ�ƻ������ƣ�';
                                      v_RECEIVEHOSPITAL2 varchar2 default '', ---- '3.ҽ��ת���������������/��������Ժ�������ҽ�ƻ�����;

                                      v_AGAININHOSPITAL       varchar2 default '', ---- '�Ƿ��г�Ժ31������סԺ�ƻ� �� 1.��  2.��';
                                      v_AGAININHOSPITALREASON varchar2 default '', ---- '��Ժ31������סԺ�ƻ� Ŀ��:            ';
                                      v_BEFOREHOSCOMADAY      varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ��';
                                      v_BEFOREHOSCOMAHOUR     varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ     Сʱ';
                                      v_BEFOREHOSCOMAMINUTE   varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ����';

                                      v_LATERHOSCOMADAY    varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ��';
                                      v_LATERHOSCOMAHOUR   varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    Сʱ';
                                      v_LATERHOSCOMAMINUTE varchar2 default '', ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ����';
                                      v_CARDNUMBER         varchar2 default '', ---- '��������';
                                      v_ALLERGIC_FLAG      varchar2 default '', ---- 'ҩ�����1.�� 2.��';

                                      v_AUTOPSY_FLAG     varchar2 default '', ---- '��������ʬ�� �� 1.��  2.��';
                                      v_CSD_PROVINCEID   varchar2 default '', ---- '������ ʡ';
                                      v_CSD_CITYID       varchar2 default '', ---- '������ ��';
                                      v_CSD_DISTRICTID   varchar2 default '', ---- '������ ��';
                                      v_CSD_PROVINCENAME varchar2 default '', ---- '������ ʡ����';

                                      v_CSD_CITYNAME     varchar2 default '', ---- '������ ������';
                                      v_CSD_DISTRICTNAME varchar2 default '', ---- '������ ������';
                                      v_XZZ_PROVINCEID   varchar2 default '', ---- '��סַ ʡ';
                                      v_XZZ_CITYID       varchar2 default '', ---- '��סַ ��';
                                      v_XZZ_DISTRICTID   varchar2 default '', ---- '��סַ ��';

                                      v_XZZ_PROVINCENAME varchar2 default '', ---- '��סַ ʡ����';
                                      v_XZZ_CITYNAME     varchar2 default '', ---- '��סַ ������';
                                      v_XZZ_DISTRICTNAME varchar2 default '', ---- '��סַ ������';
                                      v_XZZ_TEL          varchar2 default '', ---- '��סַ �绰';
                                      v_XZZ_POST         varchar2 default '', ---- '��סַ �ʱ�';

                                      v_HKDZ_PROVINCEID   varchar2 default '', ---- '���ڵ�ַ ʡ';
                                      v_HKDZ_CITYID       varchar2 default '', ---- '���ڵ�ַ ��';
                                      v_HKDZ_DISTRICTID   varchar2 default '', ---- '���ڵ�ַ ��';
                                      v_HKDZ_PROVINCENAME varchar2 default '', ---- '���ڵ�ַ ʡ����';
                                      v_HKDZ_CITYNAME     varchar2 default '', ---- '���ڵ�ַ ������';

                                      v_HKDZ_DISTRICTNAME varchar2 default '', ---- '���ڵ�ַ ������';
                                      v_HKDZ_POST         varchar2 default '', ---- '�������ڵ��ʱ�';
                                      v_JG_PROVINCEID     varchar2 default '', ---- '���� ʡ����';
                                      v_JG_CITYID         varchar2 default '', ---- '���� ������';
                                      v_JG_PROVINCENAME   varchar2 default '', ---- '���� ʡ����';
                                      v_JG_CITYNAME       varchar2 default '', ---- '���� ������'
                                      v_Age               varchar2 default '',

                                      v_CURE_TYPE   VARCHAR2 default '', ----  Y    ������� �� 1.��ҽ�� 1.1 ��ҽ   1.2����ҽ��    2.����ҽ     3.��ҽ
                                      v_MZZYZD_NAME VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ�
                                      v_MZZYZD_CODE VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ� ����
                                      v_MZXYZD_NAME VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ�
                                      v_MZXYZD_CODE VARCHAR2 default '', ---- Y   �ţ���������ϣ���ҽ��ϣ� ����
                                      v_SSLCLJ      VARCHAR2 default '', ---- Y   ʵʩ�ٴ�·������ 1. ��ҽ  2. ��ҽ  3 ��
                                      v_ZYZJ        VARCHAR2 default '', ---- Y   ʹ��ҽ�ƻ�����ҩ�Ƽ����� 1.��  2. ��

                                      v_ZYZLSB VARCHAR2 default '', ---- Y   ʹ����ҽ�����豸����  1.�� 2. ��
                                      v_ZYZLJS VARCHAR2 default '', ---- Y   ʹ����ҽ���Ƽ������� 1. ��  2. ��
                                      v_BZSH   VARCHAR2 default '', ---- Y   ��֤ʩ������ 1.��  2. ��
                                      v_outHosStatus VARCHAR2,---��Ժ״��
                                     v_JBYNZZ VARCHAR2,
                                      v_MZYCY VARCHAR2,
                                     v_InAndOutHos VARCHAR2,
                                     v_LCYBL VARCHAR2,
                                      v_FSYBL VARCHAR2,
                                     v_qJCount VARCHAR2,
                                     v_successCount VARCHAR2,
                                     v_InPatLY VARCHAR2,
                                        v_asaScore VARCHAR2,
                                      o_result OUT empcurtyp) as
    mynoofclinic varchar2(50);

  begin
    if v_IDNO = '����' then
      mynoofclinic := '';
    else
      mynoofclinic := v_IDNO;
    end if;

    IF v_edittype = '1' THEN

      --����������ҳ������Ϣ
      insert into iem_mainpage_basicinfo_sx
        (IEM_MAINPAGE_NO, ---- '������ҳ��ʶ';
         PATNOOFHIS, ---- '������';
         NOOFINPAT, ---- '������ҳ���';
         PAYID, ---- 'ҽ�Ƹ��ʽID';
         SOCIALCARE, ---- '�籣����';

         INCOUNT, ---- '��Ժ����';
         NAME, ---- '��������';
         SEXID, ---- '�Ա�';
         BIRTH, ---- '����';
         MARITAL, ---- '����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����';

         JOBID, ---- 'ְҵ';
         NATIONALITYID, ---- '����ID';
         NATIONID, --����
         IDNO, ---- '���֤����';
         ORGANIZATION, ---- '������λ';
         OFFICEPLACE, ---- '������λ��ַ';

         OFFICETEL, ---- '������λ�绰';
         OFFICEPOST, ---- '������λ�ʱ�';
         CONTACTPERSON, ---- '��ϵ������';
         RELATIONSHIP, ---- '����ϵ�˹�ϵ';
         CONTACTADDRESS, ---- '��ϵ�˵�ַ';

         CONTACTTEL, ---- '��ϵ�˵绰';
         ADMITDATE, ---- '��Ժʱ��';
         ADMITDEPT, ---- '��Ժ����';
         ADMITWARD, ---- '��Ժ����';
         TRANS_DATE, ---- 'תԺʱ��';

         TRANS_ADMITDEPT, ---- 'תԺ�Ʊ�';
         TRANS_ADMITWARD, ---- 'תԺ����';
         OUTWARDDATE, ---- '��Ժʱ��';
         OUTHOSDEPT, ---- '��Ժ����';
         OUTHOSWARD, ---- '��Ժ����';

         ACTUALDAYS, ---- 'ʵ��סԺ����';
         PATHOLOGY_DIAGNOSIS_NAME, ---- '�����������';
         PATHOLOGY_OBSERVATION_SN, ---- '������� ';
         ALLERGIC_DRUG, ---- '����ҩ��';
         SECTION_DIRECTOR, ---- '������';

         DIRECTOR, ---- '������������ҽʦ';
         VS_EMPLOYEE_CODE, ---- '����ҽʦ';
         RESIDENT_EMPLOYEE_CODE, ---- 'סԺҽʦ';
         REFRESH_EMPLOYEE_CODE, ---- '����ҽʦ';
         DUTY_NURSE, ---- '���λ�ʿ';

         INTERNE, ---- 'ʵϰҽʦ';
         CODING_USER, ---- '����Ա';
         MEDICAL_QUALITY_ID, ---- '��������';
         QUALITY_CONTROL_DOCTOR, ---- '�ʿ�ҽʦ';
         QUALITY_CONTROL_NURSE, ---- '�ʿػ�ʿ';

         QUALITY_CONTROL_DATE, ---- '�ʿ�ʱ��';
         XAY_SN, ---- 'x�߼���';
         CT_SN, ---- 'CT����';
         MRI_SN, ---- 'mri����';
         DSA_SN, ---- 'Dsa����';

         BLOODTYPE, ---- 'Ѫ��';
         RH, ---- 'Rh';
         IS_COMPLETED, ---- '��ɷ� y/n ';
         COMPLETED_TIME, ---- '���ʱ��';
         VALIDE, ---- '���Ϸ� 1/0';

         CREATE_USER, ---- '�����˼�¼��';
         CREATE_TIME, ---- '����ʱ��';
         MODIFIED_USER, ---- '�޸Ĵ˼�¼��';
         MODIFIED_TIME, ---- '�޸�ʱ��';
         ZYMOSIS, ---- 'ҽԺ��Ⱦ��';

         HURT_TOXICOSIS_ELE_ID, ---- '���ˡ��ж����ⲿ����';
         HURT_TOXICOSIS_ELE_Name,
         ZYMOSISSTATE, ---- 'ҽԺ��Ⱦ��״̬';
         PATHOLOGY_DIAGNOSIS_ID, ---- '������ϱ��';
         MONTHAGE, ---- '�����䲻��1����ģ� ����(��)';
         WEIGHT, ---- '��������������(��)';

         INWEIGHT, ---- '��������Ժ����(��)';
         INHOSTYPE, ---- '��Ժ;��:1.����  2.����  3.����ҽ�ƻ���ת��  9.����';
         OUTHOSTYPE, ---- '��Ժ��ʽ �� 1.ҽ����Ժ  2.ҽ��תԺ 3.ҽ��ת���������������/��������Ժ 4.��ҽ����Ժ5.����9.����';
         RECEIVEHOSPITAL, ---- '2.ҽ��תԺ�������ҽ�ƻ������ƣ�';
         RECEIVEHOSPITAL2, ---- '3.ҽ��ת���������������/��������Ժ�������ҽ�ƻ�����;

         AGAININHOSPITAL, ---- '�Ƿ��г�Ժ31������סԺ�ƻ� �� 1.��  2.��';
         AGAININHOSPITALREASON, ---- '��Ժ31������סԺ�ƻ� Ŀ��:            ';
         BEFOREHOSCOMADAY, ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ��';
         BEFOREHOSCOMAHOUR, ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ     Сʱ';
         BEFOREHOSCOMAMINUTE, ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ����';

         LATERHOSCOMADAY, ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ��';
         LATERHOSCOMAHOUR, ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    Сʱ';
         LATERHOSCOMAMINUTE, ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ����';
         CARDNUMBER, ---- '��������';
         ALLERGIC_FLAG, ---- 'ҩ�����1.�� 2.��';

         AUTOPSY_FLAG, ---- '��������ʬ�� �� 1.��  2.��';
         CSD_PROVINCEID, ---- '������ ʡ';
         CSD_CITYID, ---- '������ ��';
         CSD_DISTRICTID, ---- '������ ��';
         CSD_PROVINCENAME, ---- '������ ʡ����';

         CSD_CITYNAME, ---- '������ ������';
         CSD_DISTRICTNAME, ---- '������ ������';
         XZZ_PROVINCEID, ---- '��סַ ʡ';
         XZZ_CITYID, ---- '��סַ ��';
         XZZ_DISTRICTID, ---- '��סַ ��';

         XZZ_PROVINCENAME, ---- '��סַ ʡ����';
         XZZ_CITYNAME, ---- '��סַ ������';
         XZZ_DISTRICTNAME, ---- '��סַ ������';
         XZZ_TEL, ---- '��סַ �绰';
         XZZ_POST, ---- '��סַ �ʱ�';

         HKDZ_PROVINCEID, ---- '���ڵ�ַ ʡ';
         HKDZ_CITYID, ---- '���ڵ�ַ ��';
         HKDZ_DISTRICTID, ---- '���ڵ�ַ ��';
         HKDZ_PROVINCENAME, ---- '���ڵ�ַ ʡ����';
         HKDZ_CITYNAME, ---- '���ڵ�ַ ������';

         HKDZ_DISTRICTNAME, ---- '���ڵ�ַ ������';
         HKDZ_POST, ---- '�������ڵ��ʱ�';
         JG_PROVINCEID, ---- '���� ʡ����';
         JG_CITYID, ---- '���� ������';
         JG_PROVINCENAME, ---- '���� ʡ����';
         JG_CITYNAME, ---- '���� ������';
         age,

         CURE_TYPE, ---- ������� �� 1.��ҽ�� 1.1 ��ҽ   1.2����ҽ��    2.����ҽ     3.��ҽ
         MZZYZD_NAME, ---- �ţ���������ϣ���ҽ��ϣ�
         MZZYZD_CODE, ---- �ţ���������ϣ���ҽ��ϣ� ����
         MZXYZD_NAME, ---- �ţ���������ϣ���ҽ��ϣ�
         MZXYZD_CODE, ---- �ţ���������ϣ���ҽ��ϣ� ����
         SSLCLJ, ---- ʵʩ�ٴ�·������ 1. ��ҽ  2. ��ҽ  3 ��
         ZYZJ, ---- ʹ��ҽ�ƻ�����ҩ�Ƽ����� 1.��  2. ��
         ZYZLSB, ---- ʹ����ҽ�����豸����  1.�� 2. ��
         ZYZLJS, ---- ʹ����ҽ���Ƽ������� 1. ��  2. ��
         BZSH, ---- ��֤ʩ������ 1.��  2. ��
         outHosStatus,----���˳�Ժ״�� Add by xlb 2013-07-03
       JBYNZZ,       ----�����Ƿ�������֢ Add by xlb 2013-07-03
         MZYCY,         --- �������Ժ Add by xlb 2013-07-03
       InAndOutHos,  --��Ժ���Ժ
        LCYBL,         ---�ٴ��벡��
        FSYBL,          ---�����벡��
        qJCount,        ---���ȴ���
        successCount,  ---�ɹ�����
         InPatLY,  ----������Դ
         asaScore  --ASA�ּ�����
         )
      values
        (seq_iem_mainpage_basicinfo_id.NEXTVAL, ---- '������ҳ��ʶ';
         v_PATNOOFHIS, ---- '������';
         v_NOOFINPAT, ---- '������ҳ���';
         v_PAYID, ---- 'ҽ�Ƹ��ʽID';
         v_SOCIALCARE, ---- '�籣����';
         v_INCOUNT, ---- '��Ժ����';
         v_NAME, ---- '��������';
         v_SEXID, ---- '�Ա�';
         v_BIRTH, ---- '����';
         v_MARITAL, ---- '����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����';
         v_JOBID, ---- 'ְҵ';
         v_NATIONALITYID, ---- '����ID';
         v_NATIONID, --����
         mynoofclinic, ---- '���֤����';
         v_ORGANIZATION, ---- '������λ';
         v_OFFICEPLACE, ---- '������λ��ַ';
         v_OFFICETEL, ---- '������λ�绰';
         v_OFFICEPOST, ---- '������λ�ʱ�';
         v_CONTACTPERSON, ---- '��ϵ������';
         v_RELATIONSHIP, ---- '����ϵ�˹�ϵ';
         v_CONTACTADDRESS, ---- '��ϵ�˵�ַ';
         v_CONTACTTEL, ---- '��ϵ�˵绰';
         v_ADMITDATE, ---- '��Ժʱ��';
         v_ADMITDEPT, ---- '��Ժ����';
         v_ADMITWARD, ---- '��Ժ����';
         v_TRANS_DATE, ---- 'תԺʱ��';
         v_TRANS_ADMITDEPT, ---- 'תԺ�Ʊ�';
         v_TRANS_ADMITWARD, ---- 'תԺ����';
         v_OUTWARDDATE, ---- '��Ժʱ��';
         v_OUTHOSDEPT, ---- '��Ժ����';
         v_OUTHOSWARD, ---- '��Ժ����';
         v_ACTUALDAYS, ---- 'ʵ��סԺ����';
         v_PATHOLOGY_DIAGNOSIS_NAME, ---- '�����������';
         v_PATHOLOGY_OBSERVATION_SN, ---- '������� ';
         v_ALLERGIC_DRUG, ---- '����ҩ��';
         v_SECTION_DIRECTOR, ---- '������';
         v_DIRECTOR, ---- '������������ҽʦ';
         v_VS_EMPLOYEE_CODE, ---- '����ҽʦ';
         v_RESIDENT_EMPLOYEE_CODE, ---- 'סԺҽʦ';
         v_REFRESH_EMPLOYEE_CODE, ---- '����ҽʦ';
         v_DUTY_NURSE, ---- '���λ�ʿ';
         v_INTERNE, ---- 'ʵϰҽʦ';
         v_CODING_USER, ---- '����Ա';
         v_MEDICAL_QUALITY_ID, ---- '��������';
         v_QUALITY_CONTROL_DOCTOR, ---- '�ʿ�ҽʦ';
         v_QUALITY_CONTROL_NURSE, ---- '�ʿػ�ʿ';
         v_QUALITY_CONTROL_DATE, ---- '�ʿ�ʱ��';
         v_XAY_SN, ---- 'x�߼���';
         v_CT_SN, ---- 'CT����';
         v_MRI_SN, ---- 'mri����';
         v_DSA_SN, ---- 'Dsa����';
         v_BLOODTYPE, ---- 'Ѫ��';
         v_RH, ---- 'Rh';
         v_IS_COMPLETED, ---- '��ɷ� y/n ';
         v_COMPLETED_TIME, ---- '���ʱ��';
         v_VALIDE, ---- '���Ϸ� 1/0';
         v_CREATE_USER, ---- '�����˼�¼��';
         TO_CHAR(SYSDATE, 'yyyy-mm-dd hh24:mi:ss'), ---- '����ʱ��';
         v_MODIFIED_USER, ---- '�޸Ĵ˼�¼��';
         v_MODIFIED_TIME, ---- '�޸�ʱ��';
         v_ZYMOSIS, ---- 'ҽԺ��Ⱦ��';
         v_HURT_TOXICOSIS_ELE_ID, ---- '���ˡ��ж����ⲿ����';
         v_HURT_TOXICOSIS_ELE_Name, ---- '���ˡ��ж����ⲿ����';
         v_ZYMOSISSTATE, ---- 'ҽԺ��Ⱦ��״̬';
         v_PATHOLOGY_DIAGNOSIS_ID, ---- '������ϱ��';
         v_MONTHAGE, ---- '�����䲻��1����ģ� ����(��)';
         v_WEIGHT, ---- '��������������(��)';
         v_INWEIGHT, ---- '��������Ժ����(��)';
         v_INHOSTYPE, ---- '��Ժ;��:1.����  2.����  3.����ҽ�ƻ���ת��  9.����';
         v_OUTHOSTYPE, ---- '��Ժ��ʽ �� 1.ҽ����Ժ  2.ҽ��תԺ 3.ҽ��ת���������������/��������Ժ 4.��ҽ����Ժ5.����9.����';
         v_RECEIVEHOSPITAL, ---- '2.ҽ��תԺ�������ҽ�ƻ������ƣ�';
         v_RECEIVEHOSPITAL2, ---- '3.ҽ��ת���������������/��������Ժ�������ҽ�ƻ�����;
         v_AGAININHOSPITAL, ---- '�Ƿ��г�Ժ31������סԺ�ƻ� �� 1.��  2.��';
         v_AGAININHOSPITALREASON, ---- '��Ժ31������סԺ�ƻ� Ŀ��:            ';
         v_BEFOREHOSCOMADAY, ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ��';
         v_BEFOREHOSCOMAHOUR, ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ     Сʱ';
         v_BEFOREHOSCOMAMINUTE, ---- '­�����˻��߻���ʱ�䣺 ��Ժǰ    ����';
         v_LATERHOSCOMADAY, ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ��';
         v_LATERHOSCOMAHOUR, ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    Сʱ';
         v_LATERHOSCOMAMINUTE, ---- '­�����˻��߻���ʱ�䣺 ��Ժ��    ����';
         v_CARDNUMBER, ---- '��������';
         v_ALLERGIC_FLAG, ---- 'ҩ�����1.�� 2.��';
         v_AUTOPSY_FLAG, ---- '��������ʬ�� �� 1.��  2.��';
         v_CSD_PROVINCEID, ---- '������ ʡ';
         v_CSD_CITYID, ---- '������ ��';
         v_CSD_DISTRICTID, ---- '������ ��';
         v_CSD_PROVINCENAME, ---- '������ ʡ����';
         v_CSD_CITYNAME, ---- '������ ������';
         v_CSD_DISTRICTNAME, ---- '������ ������';
         v_XZZ_PROVINCEID, ---- '��סַ ʡ';
         v_XZZ_CITYID, ---- '��סַ ��';
         v_XZZ_DISTRICTID, ---- '��סַ ��';
         v_XZZ_PROVINCENAME, ---- '��סַ ʡ����';
         v_XZZ_CITYNAME, ---- '��סַ ������';
         v_XZZ_DISTRICTNAME, ---- '��סַ ������';
         v_XZZ_TEL, ---- '��סַ �绰';
         v_XZZ_POST, ---- '��סַ �ʱ�';
         v_HKDZ_PROVINCEID, ---- '���ڵ�ַ ʡ';
         v_HKDZ_CITYID, ---- '���ڵ�ַ ��';
         v_HKDZ_DISTRICTID, ---- '���ڵ�ַ ��';
         v_HKDZ_PROVINCENAME, ---- '���ڵ�ַ ʡ����';
         v_HKDZ_CITYNAME, ---- '���ڵ�ַ ������';
         v_HKDZ_DISTRICTNAME, ---- '���ڵ�ַ ������';
         v_HKDZ_POST, ---- '�������ڵ��ʱ�';
         v_JG_PROVINCEID, ---- '���� ʡ����';
         v_JG_CITYID, ---- '���� ������';
         v_JG_PROVINCENAME, ---- '���� ʡ����';
         v_JG_CITYNAME,
         v_Age,

         v_CURE_TYPE, ---- ������� �� 1.��ҽ�� 1.1 ��ҽ   1.2����ҽ��    2.����ҽ     3.��ҽ
         v_MZZYZD_NAME, ---- �ţ���������ϣ���ҽ��ϣ�
         v_MZZYZD_CODE, ---- �ţ���������ϣ���ҽ��ϣ� ����
         v_MZXYZD_NAME, ---- �ţ���������ϣ���ҽ��ϣ�
         v_MZXYZD_CODE, ---- �ţ���������ϣ���ҽ��ϣ� ����
         v_SSLCLJ, ---- ʵʩ�ٴ�·������ 1. ��ҽ  2. ��ҽ  3 ��
         v_ZYZJ, ---- ʹ��ҽ�ƻ�����ҩ�Ƽ����� 1.��  2. ��
         v_ZYZLSB, ---- ʹ����ҽ�����豸����  1.�� 2. ��
         v_ZYZLJS, ---- ʹ����ҽ���Ƽ������� 1. ��  2. ��
         v_BZSH, ---- ��֤ʩ������ 1.��  2. ��
         v_outHosStatus,
        v_JBYNZZ ,
        v_MZYCY ,
        v_InAndOutHos,
        v_LCYBL,
        v_FSYBL,
        v_qJCount,
        v_successCount,
        v_InPatLY,
        v_asaScore
         );

      open o_result for
        select seq_iem_mainpage_basicinfo_id.currval from dual;
      ---�޸Ĳ�����ҳ������Ϣ
    ELSIF v_edittype = '2' THEN

      update iem_mainpage_basicinfo_sx
         set PATNOOFHIS               = v_PATNOOFHIS, --������
             NOOFINPAT                = v_NOOFINPAT, --������ҳ���
             PAYID                    = v_PAYID, --ҽ�Ƹ��ʽID
             SOCIALCARE               = v_SOCIALCARE, --�籣����
             INCOUNT                  = v_INCOUNT, --��Ժ����
             NAME                     = v_NAME, --��������
             SEXID                    = v_SEXID, --�Ա�
             BIRTH                    = v_BIRTH, --����
             MARITAL                  = v_MARITAL, --����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����
             JOBID                    = v_JOBID, --ְҵ
             NATIONALITYID            = v_NATIONALITYID, --����ID
             NATIONID                 = v_NATIONID, --����
             IDNO                     = mynoofclinic, --���֤����
             ORGANIZATION             = v_ORGANIZATION, --������λ
             OFFICEPLACE              = v_OFFICEPLACE, --������λ��ַ
             OFFICETEL                = v_OFFICETEL, --������λ�绰
             OFFICEPOST               = v_OFFICEPOST, --������λ�ʱ�
             CONTACTPERSON            = v_CONTACTPERSON, --��ϵ������
             RELATIONSHIP             = v_RELATIONSHIP, --����ϵ�˹�ϵ
             CONTACTADDRESS           = v_CONTACTADDRESS, --��ϵ�˵�ַ
             CONTACTTEL               = v_CONTACTTEL, --��ϵ�˵绰
             ADMITDATE                = v_ADMITDATE, --��Ժʱ��
             ADMITDEPT                = v_ADMITDEPT, --��Ժ����
             ADMITWARD                = v_ADMITWARD, --��Ժ����
             TRANS_DATE               = v_TRANS_DATE, --תԺʱ��
             TRANS_ADMITDEPT          = v_TRANS_ADMITDEPT, --תԺ�Ʊ�
             TRANS_ADMITWARD          = v_TRANS_ADMITWARD, --תԺ����
             OUTWARDDATE              = v_OUTWARDDATE, --��Ժʱ��
             OUTHOSDEPT               = v_OUTHOSDEPT, --��Ժ����
             OUTHOSWARD               = v_OUTHOSWARD, --��Ժ����
             ACTUALDAYS               = v_ACTUALDAYS, --ʵ��סԺ����
             PATHOLOGY_DIAGNOSIS_NAME = v_PATHOLOGY_DIAGNOSIS_NAME, --�����������
             PATHOLOGY_OBSERVATION_SN = v_PATHOLOGY_OBSERVATION_SN, --�������
             ALLERGIC_DRUG            = v_ALLERGIC_DRUG, --����ҩ��
             SECTION_DIRECTOR         = v_SECTION_DIRECTOR, --������
             DIRECTOR                 = v_DIRECTOR, --������������ҽʦ
             VS_EMPLOYEE_CODE         = v_VS_EMPLOYEE_CODE, --����ҽʦ
             RESIDENT_EMPLOYEE_CODE   = v_RESIDENT_EMPLOYEE_CODE, --סԺҽʦ
             REFRESH_EMPLOYEE_CODE    = v_REFRESH_EMPLOYEE_CODE, --����ҽʦ
             DUTY_NURSE               = v_DUTY_NURSE, --���λ�ʿ
             INTERNE                  = v_INTERNE, --ʵϰҽʦ
             CODING_USER              = v_CODING_USER, --����Ա
             MEDICAL_QUALITY_ID       = v_MEDICAL_QUALITY_ID, --��������
             QUALITY_CONTROL_DOCTOR   = v_QUALITY_CONTROL_DOCTOR, --�ʿ�ҽʦ
             QUALITY_CONTROL_NURSE    = v_QUALITY_CONTROL_NURSE, --�ʿػ�ʿ
             QUALITY_CONTROL_DATE     = v_QUALITY_CONTROL_DATE, --�ʿ�ʱ��
             XAY_SN                   = v_XAY_SN, --x�߼���
             CT_SN                    = v_CT_SN, --CT����
             MRI_SN                   = v_MRI_SN, --mri����
             DSA_SN                   = v_DSA_SN, --Dsa����
             BLOODTYPE                = v_BLOODTYPE, --Ѫ��
             RH                       = v_RH, --Rh
             IS_COMPLETED             = v_IS_COMPLETED, --��ɷ� y/n
             COMPLETED_TIME           = v_COMPLETED_TIME, --���ʱ��
             VALIDE                   = v_VALIDE, --���Ϸ� 1/0
             /* CREATE_USER              = v_CREATE_USER, --�����˼�¼��
             CREATE_TIME              = v_CREATE_TIME, --����ʱ��*/
             MODIFIED_USER           = v_MODIFIED_USER, --�޸Ĵ˼�¼��
             MODIFIED_TIME           = TO_CHAR(SYSDATE,
                                               'yyyy-mm-dd hh24:mi:ss'), --�޸�ʱ��
             ZYMOSIS                 = v_ZYMOSIS, --ҽԺ��Ⱦ��
             HURT_TOXICOSIS_ELE_ID   = v_HURT_TOXICOSIS_ELE_ID, --���ˡ��ж����ⲿ����
             HURT_TOXICOSIS_ELE_Name = v_HURT_TOXICOSIS_ELE_Name, --���ˡ��ж����ⲿ����
             ZYMOSISSTATE            = v_ZYMOSISSTATE, --ҽԺ��Ⱦ��״̬
             PATHOLOGY_DIAGNOSIS_ID  = v_PATHOLOGY_DIAGNOSIS_ID, --������ϱ��
             MONTHAGE                = v_MONTHAGE, --�����䲻��1����ģ� ����(��)
             WEIGHT                  = v_WEIGHT, --��������������(��)
             INWEIGHT                = v_INWEIGHT, --��������Ժ����(��)
             INHOSTYPE               = v_INHOSTYPE, --��Ժ;��:1.����  2.����  3.����ҽ�ƻ���ת��  9.����
             OUTHOSTYPE              = v_OUTHOSTYPE, --��Ժ��ʽ �� 1.ҽ����Ժ  2.ҽ��תԺ 3.ҽ��ת���������������/��������Ժ 4.��ҽ����Ժ5.����9.����
             RECEIVEHOSPITAL         = v_RECEIVEHOSPITAL, --2.ҽ��תԺ�������ҽ�ƻ������ƣ�
             RECEIVEHOSPITAL2        = v_RECEIVEHOSPITAL2, --3.ҽ��ת���������������/��������Ժ�������ҽ�ƻ������ƣ�
             AGAININHOSPITAL         = v_AGAININHOSPITAL, --�Ƿ��г�Ժ31������סԺ�ƻ� �� 1.��  2.��
             AGAININHOSPITALREASON   = v_AGAININHOSPITALREASON, --��Ժ31������סԺ�ƻ� Ŀ��:
             BEFOREHOSCOMADAY        = v_BEFOREHOSCOMADAY, --­�����˻��߻���ʱ�䣺 ��Ժǰ    ��
             BEFOREHOSCOMAHOUR       = v_BEFOREHOSCOMAHOUR, --­�����˻��߻���ʱ�䣺 ��Ժǰ     Сʱ
             BEFOREHOSCOMAMINUTE     = v_BEFOREHOSCOMAMINUTE, --­�����˻��߻���ʱ�䣺 ��Ժǰ    ����
             LATERHOSCOMADAY         = v_LATERHOSCOMADAY, --­�����˻��߻���ʱ�䣺 ��Ժ��    ��
             LATERHOSCOMAHOUR        = v_LATERHOSCOMAHOUR, --­�����˻��߻���ʱ�䣺 ��Ժ��    Сʱ
             LATERHOSCOMAMINUTE      = v_LATERHOSCOMAMINUTE, --­�����˻��߻���ʱ�䣺 ��Ժ��    ����
             CARDNUMBER              = v_CARDNUMBER, --��������
             ALLERGIC_FLAG           = v_ALLERGIC_FLAG, --ҩ�����1.�� 2.��
             AUTOPSY_FLAG            = v_AUTOPSY_FLAG, --��������ʬ�� �� 1.��  2.��
             CSD_PROVINCEID          = v_CSD_PROVINCEID, --������ ʡ
             CSD_CITYID              = v_CSD_CITYID, --������ ��
             CSD_DISTRICTID          = v_CSD_DISTRICTID, --������ ��
             CSD_PROVINCENAME        = v_CSD_PROVINCENAME, --������ ʡ����
             CSD_CITYNAME            = v_CSD_CITYNAME, --������ ������
             CSD_DISTRICTNAME        = v_CSD_DISTRICTNAME, --������ ������
             XZZ_PROVINCEID          = v_XZZ_PROVINCEID, --��סַ ʡ
             XZZ_CITYID              = v_XZZ_CITYID, --��סַ ��
             XZZ_DISTRICTID          = v_XZZ_DISTRICTID, --��סַ ��
             XZZ_PROVINCENAME        = v_XZZ_PROVINCENAME, --��סַ ʡ����
             XZZ_CITYNAME            = v_XZZ_CITYNAME, --��סַ ������
             XZZ_DISTRICTNAME        = v_XZZ_DISTRICTNAME, --��סַ ������
             XZZ_TEL                 = v_XZZ_TEL, --��סַ �绰
             XZZ_POST                = v_XZZ_POST, --��סַ �ʱ�
             HKDZ_PROVINCEID         = v_HKDZ_PROVINCEID, --���ڵ�ַ ʡ
             HKDZ_CITYID             = v_HKDZ_CITYID, --���ڵ�ַ ��
             HKDZ_DISTRICTID         = v_HKDZ_DISTRICTID, --���ڵ�ַ ��
             HKDZ_PROVINCENAME       = v_HKDZ_PROVINCENAME, --���ڵ�ַ ʡ����
             HKDZ_CITYNAME           = v_HKDZ_CITYNAME, --���ڵ�ַ ������
             HKDZ_DISTRICTNAME       = v_HKDZ_DISTRICTNAME, --���ڵ�ַ ������
             HKDZ_POST               = v_HKDZ_POST, --�������ڵ��ʱ�
             JG_PROVINCEID           = v_JG_PROVINCEID, --���� ʡ����
             JG_CITYID               = v_JG_CITYID, --���� ������
             JG_PROVINCENAME         = v_JG_PROVINCENAME, --���� ʡ����
             JG_CITYNAME             = v_JG_CITYNAME, --���� ������\
             age                     = v_Age,

             CURE_TYPE   = v_CURE_TYPE, ---- ������� �� 1.��ҽ�� 1.1 ��ҽ   1.2����ҽ��    2.����ҽ     3.��ҽ
             MZZYZD_NAME = v_MZZYZD_NAME, ---- �ţ���������ϣ���ҽ��ϣ�
             MZZYZD_CODE = v_MZZYZD_CODE, ---- �ţ���������ϣ���ҽ��ϣ� ����
             MZXYZD_NAME = v_MZXYZD_NAME, ---- �ţ���������ϣ���ҽ��ϣ�
             MZXYZD_CODE = v_MZXYZD_CODE, ---- �ţ���������ϣ���ҽ��ϣ� ����
             SSLCLJ      = v_SSLCLJ, ---- ʵʩ�ٴ�·������ 1. ��ҽ  2. ��ҽ  3 ��
             ZYZJ        = v_ZYZJ, ---- ʹ��ҽ�ƻ�����ҩ�Ƽ����� 1.��  2. ��
             ZYZLSB      = v_ZYZLSB, ---- ʹ����ҽ�����豸����  1.�� 2. ��
             ZYZLJS      = v_ZYZLJS, ---- ʹ����ҽ���Ƽ������� 1. ��  2. ��
             BZSH        = v_BZSH, ---- ��֤ʩ������ 1.��  2. ��
             outHosStatus=v_outHosStatus,
             JBYNZZ      = v_JBYNZZ ,
             MZYCY       = v_MZYCY ,
             InAndOutHos =v_InAndOutHos,
              LCYBL      = v_LCYBL,
               FSYBL=v_FSYBL,
        qJCount=v_qJCount,
        successCount=v_successCount,
         InPatLY=v_InPatLY,
         asaScore=v_asaScore
       where IEM_MAINPAGE_NO = v_IEM_MAINPAGE_NO;
      open o_result for
        select v_IEM_MAINPAGE_NO from dual;
    end if;

  END;

  /*****��ѯ������ҳ��Ϣ****************************************************************************/
  -----Modify by xlb �����Ž������ֶ� 2013-07-03
  PROCEDURE usp_getieminfo_sx(v_NoOfInpat INT,
                              o_result    OUT empcurtyp,
                              o_result1   OUT empcurtyp,
                              o_result2   OUT empcurtyp,
                              o_result3   OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��������
     ����˵��
     �������
      v_NoOfInpat varchar(40)--��ҳ���
     �������
     �����������
    ��������ͳ�����ݼ�

     ���õ�sp
     ����ʵ��
     exec usp_GetIemInfo  9
     �޸ļ�¼
    **********/
    v_infono NUMERIC;
  BEGIN
    OPEN o_result FOR
      SELECT '' FROM DUAL;

    OPEN o_result1 FOR
      SELECT '' FROM DUAL;

    OPEN o_result2 FOR
      SELECT '' FROM DUAL;

    OPEN o_result3 FOR
      SELECT '' FROM DUAL;

    SELECT MAX(imb.iem_mainpage_no)
      INTO v_infono
      FROM iem_mainpage_basicinfo_sx imb
     WHERE imb.noofinpat = v_noofinpat
       AND imb.valide = 1;

    --����˳�򲻿ɱ䣬����������
    --������Ϣ
    OPEN o_result FOR
      SELECT iem.iem_mainpage_no,
             iem.noofinpat,
             iem.socialcare,
             myinp.payid,
             pay.name payName,
             myinp.noofrecord,
             iem.incount,
             iem.patnoofhis,
             myinp.name,
             myinp.sexid,
             myinp.birth,
             myinp.nativetel, ---��סַ�绰edit 2012��6��25�� 08:46:26
             myinp.marital,
             myinp.jobid,
             job.name         jobName,
             myinp.provinceid csd_provinceid,
             myinp.countyid   csd_cityid,

             myinp.districtid csd_districtid, --��������

             csdpro.provincename csd_provincename,
             csdcity.cityname    csd_cityname,
             csddis.districtname csd_districtname,
             myinp.xzzproviceid xzz_provinceid,

             myinp.xzzcityid     xzz_cityid,
             myinp.xzzdistrictid xzz_districtid,

             xzzpro.provincename xzz_provincename,
             xzzcity.cityname    xzz_cityname,
             xzzdis.districtname xzz_districtname,

             myinp.xzztel  xzz_tel,
             myinp.xzzpost xzz_post,

             myinp.hkdzproviceid  hkdz_provinceid,
             myinp.hkzdcityid     hkdz_cityid,
             myinp.hkzddistrictid hkdz_districtid,

             hkzzpro.provincename hkdz_provincename,
             hkzzcity.cityname    hkdz_cityname,
             hkzzdis.districtname hkdz_districtname,
             myinp.nativepost hkdz_post,

             myinp.nativeplace_p jg_provinceid,

             myinp.nativeplace_c jg_cityid,

             jgpro.provincename jg_provincename,
             jgcity.cityname    jg_cityname,
             myinp.nationid,
             nation.name    NationName,

             myinp.nationalityid,
             nationality.name    nationalityName,

             myinp.idno,
             iem.organization,

             myinp.officeplace,

             myinp.officetel,

             myinp.officepost,

             myinp.contactperson,

             myinp.relationship,
             relationship.name  relationshipName,

             myinp.contactaddress ContactAddress,

             myinp.contacttel ContactTEL,

             myinp.admitdate,

             myinp.admitdept,
             AdmitDept.name  AdmitDeptName,

             myinp.admitward,
             admitward.name       admitwardName,
             iem.trans_admitdept,
             trans_admitdept.name trans_admitdeptName,
             myinp.outhosdate,
             myinp.outhosdept,
             outhosdept.name  outhosdeptName,
             myinp.outhosward,
             outhosward.name  outhoswardName,
             myinp.totaldays actualdays,

             iem.is_completed,
             iem.completed_time,
             iem.valide,
             iem.create_user,
             iem.create_time,

             iem.modified_user,
             iem.modified_time,
             iem.autopsy_flag,

             --2012�������������в�����ҳ��������
             iem.monthage,
             iem.weight,
             iem.inweight,
             iem.inhostype,
             iem.outhostype,

             iem.receivehospital,
             iem.receivehospital2,
             iem.againinhospital,
             iem.againinhospitalreason,
             iem.beforehoscomaday,

             iem.beforehoscomahour,
             iem.beforehoscomaminute,
             iem.laterhoscomaday,
             iem.laterhoscomahour,
             iem.laterhoscomaminute,
             iem.cardnumber,

             ---���ʵ��
             iem.pathology_diagnosis_id,
             iem.pathology_diagnosis_name,
             iem.pathology_observation_sn,
             iem.hurt_toxicosis_ele_id,
             iem.hurt_toxicosis_ele_name,

             iem.allergic_flag,
             iem.allergic_drug,
             iem.bloodtype,
             iem.rh,
             iem.section_director,

             section_director.name section_directorName,
             iem.director,
             director.name         directorName,
             iem.vs_employee_code  vs_employeeID,
             vs_employee.name      vs_employeeName,

             iem.resident_employee_code resident_employeeID,
             resident_employee.name     resident_employeeName,
             iem.refresh_employee_code  refresh_employeeID,
             refresh_employee.name      refresh_employeeName,
             iem.duty_nurse,

             duty_nurse.name  Duty_NurseName,
             iem.interne,
             interne.name     interneName,
             iem.coding_user,
             coding_user.name coding_userName,

             iem.medical_quality_id,
             iem.quality_control_doctor,
             quality_control_doctor.name quality_control_doctorName,
             iem.quality_control_nurse,
             quality_control_nurse.name  quality_control_nurseName,

             iem.quality_control_date,
             myinp.agestr age,

             iem.cure_type,
             iem.mzzyzd_name,
             iem.mzzyzd_code,
             iem.mzxyzd_name,
             iem.mzxyzd_code,
             iem.sslclj,
             iem.zyzj,

             iem.zyzlsb,
             iem.zyzljs,
             iem.bzsh,
             iem.outhosstatus,
             iem.jbynzz,
             iem.mzycy,
             iem.inandouthos,
             iem.lcybl,
             iem.fsybl,
             iem.qjcount,
             iem.successcount,
             iem.inpatly,
             iem.asascore,
             myinp.isbaby,
             myinp.mother

        FROM iem_mainpage_basicinfo_sx iem
      -- edit  by ywk ��������ҳ�����еģ����˱���Ҳ�е����ݣ��ڲ�����ҳ��ʾ��ʱ��ȡ���˱�������ݣ�
        left join inpatient myinp
          on myinp.noofinpat = iem.noofinpat
        left join dictionary_detail pay
          on pay.detailid = myinp.payid
         and pay.categoryid = '1'
        left join dictionary_detail job
          on job.detailid = myinp.jobid
         and job.categoryid = '41'
        left join dictionary_detail nation
          on nation.detailid = myinp.nationid
         and nation.categoryid = '42'
        left join dictionary_detail nationality
          on nationality.detailid = myinp.nationalityid
         and nationality.categoryid = '43'
        left join dictionary_detail relationship
          on relationship.detailid = myinp.relationship
         and relationship.categoryid = '44'
        left join department AdmitDept
          on AdmitDept.id = myinp.admitdept
        left join ward admitward
          on admitward.id = myinp.admitward
        left join department trans_admitdept
          on trans_admitdept.id = iem.trans_admitdept
        left join department outhosdept
          on outhosdept.id = myinp.outhosdept
        left join ward outhosward
          on outhosward.id = myinp.outhosward
        left join diagnosis zymosis
          on zymosis.markid = iem.zymosis
        left join users section_director
          on section_director.id = iem.section_director
        left join users director
          on director.id = iem.director
        left join users vs_employee
          on vs_employee.id = iem.vs_employee_code
        left join users resident_employee
          on resident_employee.id = iem.resident_employee_code
        left join users refresh_employee
          on refresh_employee.id = iem.refresh_employee_code

        left join users interne
          on interne.id = iem.interne
        left join users duty_nurse
          on duty_nurse.id = iem.duty_nurse
        left join users coding_user
          on coding_user.id = iem.coding_user
        left join users quality_control_doctor
          on quality_control_doctor.id = iem.quality_control_doctor
        left join users quality_control_nurse
          on quality_control_nurse.id = iem.quality_control_nurse

      ---ʡ���У��ص����ƴ�Inpatient����ȥ����ȡ���ƣ����������ѯ add by ywk 2012��5��17��10:58:03
        left join s_province csdpro
          on myinp.provinceid = csdpro.provinceid --�����ص�ʡ
        left join s_city csdcity
          on myinp.countyid = csdcity.cityid --�����ص���
        left join s_district csddis
          on myinp.districtid = csddis.districtid --�����ص���

        left join s_province jgpro
          on myinp.nativeplace_p = jgpro.provinceid --�����ʡ
        left join s_city jgcity
          on myinp.nativeplace_c = jgcity.cityid --�������

        left join s_province xzzpro
          on myinp.xzzproviceid = xzzpro.provinceid --��סַ��ʡ
        left join s_city xzzcity
          on myinp.xzzcityid = xzzcity.cityid --��סַ����
        left join s_district xzzdis
          on myinp.xzzdistrictid = xzzdis.districtid --��סַ����

        left join s_province hkzzpro
          on myinp.hkdzproviceid = hkzzpro.provinceid --����סַ��ʡ
        left join s_city hkzzcity
          on myinp.hkzdcityid = hkzzcity.cityid --����סַ����
        left join s_district hkzzdis
          on myinp.hkzddistrictid = hkzzdis.districtid --����סַ����

       WHERE iem.iem_mainpage_no = v_infono
         AND iem.valide = 1;

    --���
    OPEN o_result1 FOR
      SELECT diag.diagnosis_name,
             diag.status_id,
             (case
               when diag.status_id = '1' then
                '��'
               when diag.status_id = '2' then
                '�ٴ�δȷ��'
               when diag.status_id = '3' then
                '�������'
               when diag.status_id = '4' then
                '��'
               else
                ''
             end) Status_Name, --��Ժ����
             diag.diagnosis_code,
             diag.diagnosis_type_id,
             diag.order_value,
             diag.type,
             diag.typename
        FROM iem_mainpage_diagnosis_sx diag
       WHERE iem_mainpage_no = v_infono
         AND valide = 1
       ORDER BY order_value;

    OPEN o_result2 FOR

      SELECT iem.iem_mainpage_operation_no,
             iem.iem_mainpage_no,
             iem.operation_code,
             iem.operation_date,
             iem.operation_name,
             u1.name                       execute_user1_Name,
             iem.execute_user1,
             u2.name                       execute_user2_Name,
             iem.execute_user2,
             u3.name                       execute_user3_Name,
             iem.execute_user3,
             --dic.name anaesthesia_type_Name,
             ab.name anaesthesia_type_Name,
             iem.anaesthesia_type_id,
             (case
               when iem.close_level = '1' then
                'I/��'
               when iem.close_level = '2' then
                'II/��'
               when iem.close_level = '3' then
                'III/��'
               when iem.close_level = '4' then
                'I/��'
               when iem.close_level = '5' then
                'II/��'
               when iem.close_level = '6' then
                'III/��'
               when iem.close_level = '7' then
                'I/��'
               when iem.close_level = '8' then
                'II/��'
               when iem.close_level = '9' then
                'III/��'
               else
                ''
             end) close_level_Name,
             iem.operation_level,
             (case
               when iem.operation_level = '1' then --operation_level  edit by ywk 2012��4��18��14:03:50
                'һ������'
               when iem.operation_level = '2' then
                '��������'
               when iem.operation_level = '3' then
                '��������'
               when iem.operation_level = '4' then
                '�ļ�����'
               else
                ''
             end) operation_level_Name,
             iem.close_level,
             ua.name anaesthesia_user_Name,
             iem.anaesthesia_user,
             iem.valide,
             iem.create_user,
             iem.create_time,
             iem.cancel_user,
             iem.cancel_time,
             iem.operintimes
        FROM iem_mainpage_operation_sx iem
        left join users u1
          on iem.execute_user1 = u1.id
         and u1.valid = 1
        left join users u2
          on iem.execute_user2 = u2.id
         and u2.valid = 1
        left join users u3
          on iem.execute_user3 = u3.id
         and u3.valid = 1
        left join users ua
          on iem.anaesthesia_user = ua.id
         and ua.valid = 1

      ---������Ϣ���޸� edit by ywk 2012��4��18��10:22:32
        left join anesthesia ab
          on iem.anaesthesia_type_id = ab.id
       WHERE valide = 1
         and iem.iem_mainpage_no = v_infono;

    --����Ӥ����Ϣ
    OPEN o_result3 FOR
      SELECT *
        FROM IEM_MAINPAGE_OBSTETRICSBABY
       WHERE iem_mainpage_no = v_infono
         AND valide = 1;
  END;

  /*********************************************************************************/
  PROCEDURE usp_edit_iem_mainpage_oper_sx(v_iem_mainpage_no     NUMERIC,
                                          v_operation_code      VARCHAR,
                                          v_operation_date      VARCHAR,
                                          v_operation_name      VARCHAR,
                                          v_execute_user1       VARCHAR,
                                          v_execute_user2       VARCHAR,
                                          v_execute_user3       VARCHAR,
                                          v_anaesthesia_type_id NUMERIC,
                                          v_close_level         NUMERIC,
                                          v_anaesthesia_user    VARCHAR,
                                          --v_Valide numeric ,
                                          v_create_user     VARCHAR,
                                          v_OPERATION_LEVEL varchar,
                                          v_OperInTimes VARCHAR2
                                          --v_Create_Time varchar(19)
                                          --v_Cancel_User varchar(10) ,
                                          --v_Cancel_Time varchar(19)
                                          ) AS /**********
                                                                                               �汾��  1.0.0.0.0
                                                                                               ����ʱ��
                                                                                               ����
                                                                                               ��Ȩ
                                                                                               ����  ���빦������ҳ����TABLE
                                                                                               ����˵��
                                                                                               �������
                                                                                               �������
                                                                                               �����������

                                                                                               ���õ�sp
                                                                                               ����ʵ��

                                                                                               �޸ļ�¼
                                                                                              **********/
  BEGIN
    INSERT INTO iem_mainpage_operation_sx
      (iem_mainpage_operation_no,
       iem_mainpage_no,
       operation_code,
       operation_date,
       operation_name,
       execute_user1,
       execute_user2,
       execute_user3,
       anaesthesia_type_id,
       close_level,
       anaesthesia_user,
       valide,
       create_user,
       create_time,
       OPERATION_LEVEL,
       operintimes
       )
    VALUES
      (seq_iem_mainpage_operation_id.NEXTVAL,
       v_iem_mainpage_no, --numeric
       v_operation_code, -- varchar(60)
       v_operation_date,
       -- varchar(19)
       v_operation_name, -- varchar(300)
       v_execute_user1, -- varchar(20)
       v_execute_user2,
       -- varchar(20)
       v_execute_user3, -- varchar(20)
       v_anaesthesia_type_id, -- numeric
       v_close_level,
       -- numeric
       v_anaesthesia_user, -- varchar(20)
       1, -- numeric
       v_create_user, -- varchar(10)
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss'),
       v_OPERATION_LEVEL,
       v_OperInTimes
       );
  END;

  /*********************************************************************************/
  PROCEDURE usp_edif_iem_mainpage_diag_sx(v_iem_mainpage_no   VARCHAR,
                                          v_diagnosis_type_id VARCHAR,
                                          v_diagnosis_code    VARCHAR,
                                          v_diagnosis_name    VARCHAR,
                                          v_status_id         VARCHAR,
                                          v_order_value       VARCHAR,
                                          --v_Valide numeric ,
                                          v_create_user VARCHAR,
                                          v_type        varchar,
                                          v_typeName    varchar
                                          --v_Create_Time varchar(19) ,
                                          --v_Cancel_User varchar(10) ,
                                          --v_Cancel_Time varchar(19)
                                          ) AS /**********
                                                                                               �汾��  1.0.0.0.0
                                                                                               ����ʱ��
                                                                                               ����
                                                                                               ��Ȩ
                                                                                               ����  ���빦������ҳ���TABLE
                                                                                               ����˵��
                                                                                               �������
                                                                                               �������
                                                                                               �����������

                                                                                               ���õ�sp
                                                                                               ����ʵ��

                                                                                               �޸ļ�¼
                                                                                              **********/
  BEGIN
    INSERT INTO iem_mainpage_diagnosis_sx
      (iem_mainpage_diagnosis_no,
       iem_mainpage_no,
       diagnosis_type_id,
       diagnosis_code,
       diagnosis_name,
       status_id,
       order_value,
       valide,
       create_user,
       create_time,
       type,
       typeName)
    VALUES
      (seq_iem_mainpage_diagnosis_id.NEXTVAL,
       v_iem_mainpage_no, -- Iem_Mainpage_NO - numeric
       v_diagnosis_type_id,
       -- Diagnosis_Type_Id - numeric
       v_diagnosis_code,
       -- Diagnosis_Code - varchar(60)
       v_diagnosis_name, -- Diagnosis_Name - varchar(300)
       v_status_id, -- Status_Id - numeric
       v_order_value,
       -- Order_Value - numeric
       1,
       -- Valide - numeric
       v_create_user, -- Create_User - varchar(10)
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss'),
       v_type,
       v_typeName
       -- Create_Time - varchar(19)
       );
  END;

  --���²�����ҳ��Ϣ�󣬶Բ�����Ϣ���������ͬ�� add by ywk ����һ������������ 15:20:27
  PROCEDURE usp_Edit_Iem_PaientInfo_sx(v_NOOFINPAT      varchar2 default '', ---- '������ҳ���';
                                       v_NAME           varchar2 default '', ---- '��������';
                                       v_SEXID          varchar2 default '', ---- '�Ա�';
                                       v_BIRTH          varchar2 default '', ---- '����';
                                       v_Age            INTEGER default 1, --����
                                       v_IDNO           varchar2 default '', ---- '���֤����';
                                       v_MARITAL        varchar2 default '', ---- '����״�� 1.δ�� 2.�ѻ� 3.ɥż4.��� 9.����';
                                       v_JOBID          varchar2 default '', ---- 'ְҵ';
                                       v_CSD_PROVINCEID varchar2 default '', ---- '������ ʡ';
                                       v_CSD_CITYID     varchar2 default '', ---- '������ ��';
                                       v_NATIONID       varchar2 default '', --����
                                       v_NATIONALITYID  varchar2 default '', ---- '����ID';
                                       v_JG_PROVINCEID  varchar2 default '', ---- '���� ʡ';
                                       v_JG_CITYID      varchar2 default '', ---- '���� ��';
                                       v_OFFICEPLACE    varchar2 default '', ---- '������λ��ַ';
                                       v_OFFICETEL      varchar2 default '', ---- '������λ�绰';
                                       v_OFFICEPOST     varchar2 default '', ---- '������λ�ʱ�';
                                       v_HKDZ_POST      varchar2 default '', ---- '�������ڵ��ʱ�';
                                       v_CONTACTPERSON  varchar2 default '', ---- '��ϵ������';
                                       v_RELATIONSHIP   varchar2 default '', ---- '����ϵ�˹�ϵ';
                                       v_CONTACTADDRESS varchar2 default '', ---- '��ϵ�˵�ַ';
                                       v_CONTACTTEL     varchar2 default '', ---- '��ϵ�˵绰';
                                       v_ADMITDEPT      varchar2 default '', ---- '��Ժ����';
                                       v_ADMITWARD      varchar2 default '', ---- '��Ժ����';\
                                       v_ADMITDATE      varchar2 default '', ---- '��Ժʱ��';
                                       v_OUTWARDDATE    varchar2 default '', ---- '��Ժʱ��';
                                       v_OUTHOSDEPT     varchar2 default '', ---- '��Ժ����';
                                       v_OUTHOSWARD     varchar2 default '', ---- '��Ժ����';
                                       v_ACTUALDAYS     varchar2 default '', ---- 'ʵ��סԺ����';
                                       v_AgeStr         varchar2 default '', ---- '���� ��ȷ������;2012��5��9��9:31:03 ����ά�� �����޸ģ�
                                       v_PatId          varchar2 default '', --�����ĸ��ʽ add by ywk 2012��5��14�� 16:02:13
                                       v_CardNo         varchar2 default '', --��������
                                       -----add by ywk  2012��5��16��9:45:27 Inpatient��l�����Ӳ�����ҳ����Ӧ���ֶ�
                                       v_Districtid     varchar2 default '', --�����ء��ء�
                                       v_xzzproviceid   varchar2 default '', --����סַʡ
                                       v_xzzcityid      varchar2 default '', --����סַ��
                                       v_xzzdistrictid  varchar2 default '', --����סַ��
                                       v_xzztel         varchar2 default '', --����סַ�绰
                                       v_hkdzproviceid  varchar2 default '', --����סַʡ
                                       v_hkzdcityid     varchar2 default '', --����סַ��
                                       v_hkzddistrictid varchar2 default '', --����סַ��
                                       v_xzzpost        varchar2 default '', --��סַ�ʱ�
                                       v_isupdate       varchar2 default '' ---2012��5��24�� 17:19:10 ywk �Ƿ�������֤���ֶ�
                                       ) as
    myidno          varchar2(50);
    mymaterital     varchar2(50);
    myjobid         varchar2(50);
    myadmitdept     varchar2(50);
    mydamitward     varchar2(50);
    myouthosdept    varchar2(50);
    myouthosward    varchar2(50);
    myofficepalce   varchar2(250);
    myofficetel     varchar2(50);
    myofficepost    varchar2(50);
    mycontactperson varchar2(50);
    myadmitdate     varchar2(50);
    myouthosdate    varchar2(50);
    mynoofclinic    varchar2(50);
  begin
    /*select nvl(idno,v_IDNO)
     into myidno
     from inpatient
    where NoOfInpat = v_NOOFINPAT; --���֤����*/

    myidno := v_IDNO;

    /*select nvl(v_MARITAL, Marital)
     into mymaterital
     from inpatient
    where NoOfInpat = v_NOOFINPAT; --����*/
    mymaterital := v_MARITAL;
    myjobid     := v_JOBID;
    /* select nvl(v_JOBID, jobid)
     into myjobid
     from inpatient
    where NoOfInpat = v_NOOFINPAT; --ְҵ*/
    --  myadmitdept:=v_ADMITDEPT;
    select nvl(v_ADMITDEPT, AdmitDept)
      into myadmitdept
      from inpatient
     where NoOfInpat = v_NOOFINPAT; --��Ժ����(���������ҳ��༭����д�����ǿգ���ȡԭ����Inpatient���������)
    --mydamitward:=  v_ADMITWARD;
    select nvl(v_ADMITWARD, AdmitWard)
      into mydamitward
      from inpatient
     where NoOfInpat = v_NOOFINPAT; --��Ժ����
    --myouthosdept:=v_OUTHOSDEPT;
    select nvl(v_OUTHOSDEPT, OutHosDept)
      into myouthosdept
      from inpatient
     where NoOfInpat = v_NOOFINPAT; --��Ժ�ҿ�
    --  myouthosward:=v_OUTHOSWARD;
    select nvl(v_OUTHOSWARD, OutHosWard)
      into myouthosward
      from inpatient
     where NoOfInpat = v_NOOFINPAT; --��Ժ����
    /*  select nvl(v_OFFICEPLACE, OfficePlace)
     into myofficepalce
     from inpatient
    where NoOfInpat = v_NOOFINPAT; --������ַ*/
    myofficepalce := v_OFFICEPLACE;
    /*   select nvl(v_OFFICETEL, OfficeTEL)
     into myofficetel
     from inpatient
    where NoOfInpat = v_NOOFINPAT; --������λ�绰*/
    myofficetel  := v_OFFICETEL;
    myofficepost := v_OFFICEPOST;
    /*select nvl(v_OFFICEPOST, OfficePost)
     into myofficepost
     from inpatient
    where NoOfInpat =v_NOOFINPAT; --������λ�ʱ�*/
    mycontactperson := v_CONTACTPERSON;
    /* s\*elect nvl(v_CONTACTPERSON, ContactPerson)
     into mycontactperson
     from inpatient
    where NoOfInpat =*\ v_NOOFINPAT; --��ϵ������*/
    myadmitdate := v_ADMITDATE;
    /*select nvl(v_ADMITDATE, AdmitDate)
     into myadmitdate
     from inpatient
    where NoOfInpat = v_NOOFINPAT; --��Ժʱ��*/
    myouthosdate := v_OUTWARDDATE;
    select nvl(v_OUTWARDDATE, OutHosDate)
      into myouthosdate
      from inpatient
     where NoOfInpat = v_NOOFINPAT; --��Ժʱ��
    if v_isupdate = '0' then
      update inpatient
         set Name           = v_NAME,
             sexid          = v_SEXID,
             birth          = v_BIRTH,
             age            = v_Age,
             idno           = myidno,
             Marital        = mymaterital,
             jobid          = myjobid,
             ProvinceID     = v_CSD_PROVINCEID,
             CountyID       = v_CSD_CITYID,
             NationID       = v_NATIONID,
             NationalityID  = v_NATIONALITYID,
             Nativeplace_P  = v_JG_PROVINCEID,
             Nativeplace_C  = v_JG_CITYID,
             OfficePlace    = myofficepalce,
             OfficeTEL      = myofficetel,
             OfficePost     = myofficepost,
             NativePost     = v_HKDZ_POST,
             ContactPerson  = mycontactperson,
             Relationship   = v_RELATIONSHIP,
             ContactAddress = v_CONTACTADDRESS,
             ContactTEL     = v_CONTACTTEL,
             AdmitDept      = myadmitdept,
             AdmitWard      = mydamitward,
             AdmitDate      = myadmitdate,
             OutHosDate     = myouthosdate,
             OutHosDept     = myouthosdept,
             OutHosWard     = myouthosward,
             TotalDays      = v_ACTUALDAYS,
             AgeStr         = v_AgeStr,
             payid          = v_PatId,
             cardno         = v_CardNo,
             districtid     = v_Districtid,
             xzzproviceid   = v_xzzproviceid,
             xzzcityid      = v_xzzcityid,
             xzzdistrictid  = v_xzzdistrictid,
             xzztel         = v_xzztel,
             hkdzproviceid  = v_hkdzproviceid,
             hkzdcityid     = v_hkzdcityid,
             hkzddistrictid = v_hkzddistrictid,
             xzzpost        = v_xzzpost

       where NoOfInpat = v_NOOFINPAT;
      commit;
    end if;
    if v_isupdate = '1' then
      if myidno = '����' then
        mynoofclinic := '';
      else
        mynoofclinic := myidno;
      end if;
      update inpatient
         set Name           = v_NAME,
             sexid          = v_SEXID,
             birth          = v_BIRTH,
             age            = v_Age,
             idno           = mynoofclinic,
             Marital        = mymaterital,
             jobid          = myjobid,
             ProvinceID     = v_CSD_PROVINCEID,
             CountyID       = v_CSD_CITYID,
             NationID       = v_NATIONID,
             NationalityID  = v_NATIONALITYID,
             Nativeplace_P  = v_JG_PROVINCEID,
             Nativeplace_C  = v_JG_CITYID,
             OfficePlace    = myofficepalce,
             OfficeTEL      = myofficetel,
             OfficePost     = myofficepost,
             NativePost     = v_HKDZ_POST,
             ContactPerson  = mycontactperson,
             Relationship   = v_RELATIONSHIP,
             ContactAddress = v_CONTACTADDRESS,
             ContactTEL     = v_CONTACTTEL,
             AdmitDept      = myadmitdept,
             AdmitWard      = mydamitward,
             AdmitDate      = myadmitdate,
             OutHosDate     = myouthosdate,
             OutHosDept     = myouthosdept,
             OutHosWard     = myouthosward,
             TotalDays      = v_ACTUALDAYS,
             AgeStr         = v_AgeStr,
             payid          = v_PatId,
             cardno         = v_CardNo,
             districtid     = v_Districtid,
             xzzproviceid   = v_xzzproviceid,
             xzzcityid      = v_xzzcityid,
             xzzdistrictid  = v_xzzdistrictid,
             xzztel         = v_xzztel,
             hkdzproviceid  = v_hkdzproviceid,
             hkzdcityid     = v_hkzdcityid,
             hkzddistrictid = v_hkzddistrictid,
             xzzpost        = v_xzzpost,
             noofclinic     = mynoofclinic
       where NoOfInpat = v_NOOFINPAT;
      commit;
    end if;
    /*   open o_result for
    select v_IEM_MAINPAGE_NO from dual;*/
  End;

  --�����ҳĬ�ϱ��������
  --add by ywk 2012��5��17�� 09:36:46
  PROCEDURE usp_GetDefaultInpatient(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      select inp.*,
             pay.name             payname,
             job.name             jobname,
             nation.name          nationame,
             nationality.name     nationaltyname,
             relationship.name    relationshipname,
             csdpro.provincename  csd_provincename,
             csdcity.cityname     csd_cityname,
             csddis.districtname  csd_districtname,
             xzzpro.provincename  xzz_provincename,
             xzzcity.cityname     xzz_cityname,
             xzzdis.districtname  xzz_districtname,
             hkzzpro.provincename hkdz_provincename,
             hkzzcity.cityname    hkdz_cityname,
             hkzzdis.districtname hkdz_districtname,
             jgpro.provincename   jg_provincename,
             jgcity.cityname      jg_cityname,
             dia.name             outdiagname -- �������
        from inpatient_default inp
        left join dictionary_detail pay
          on pay.detailid = inp.payid
         and pay.categoryid = '1'
        left join dictionary_detail job
          on job.detailid = inp.jobid
         and job.categoryid = '41'
        left join dictionary_detail nation
          on nation.detailid = inp.nationid
         and nation.categoryid = '42'
        left join dictionary_detail nationality
          on nationality.detailid = inp.nationalityid
         and nationality.categoryid = '43'
        left join dictionary_detail relationship
          on relationship.detailid = inp.relationship
         and relationship.categoryid = '44'
        left join diagnosis dia
          on dia.icd = inp.clinicdiagnosis --add ywk 2012��6��15�� 11:44:07
      ---ʡ���У��ص����ƴ�Inpatient����ȥ����ȡ���ƣ����������ѯ add by ywk 2012��5��17��10:58:03
        left join s_province csdpro
          on inp.provinceid = csdpro.provinceid --�����ص�ʡ
        left join s_city csdcity
          on inp.countyid = csdcity.cityid --�����ص���
        left join s_district csddis
          on inp.districtid = csddis.districtid --�����ص���

        left join s_province jgpro
          on inp.nativeplace_p = jgpro.provinceid --�����ʡ
        left join s_city jgcity
          on inp.nativeplace_c = jgcity.cityid --�������

        left join s_province xzzpro
          on inp.xzzproviceid = xzzpro.provinceid --��סַ��ʡ
        left join s_city xzzcity
          on inp.xzzcityid = xzzcity.cityid --��סַ����
        left join s_district xzzdis
          on inp.xzzdistrictid = xzzdis.districtid --��סַ����

        left join s_province hkzzpro
          on inp.hkdzproviceid = hkzzpro.provinceid --����סַ��ʡ
        left join s_city hkzzcity
          on inp.hkzdcityid = hkzzcity.cityid --����סַ����
        left join s_district hkzzdis
          on inp.hkzddistrictid = hkzzdis.districtid; --����סַ����

  END;

  PROCEDURE usp_GetInpatientByNo(v_noofinpat varchar2 default '',
                                 o_result    OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      select inp.*,
             pay.name             payname,
             job.name             jobname,
             nation.name          nationame,
             nationality.name     nationaltyname,
             relationship.name    relationshipname,
             csdpro.provincename  csd_provincename,
             csdcity.cityname     csd_cityname,
             csddis.districtname  csd_districtname,
             xzzpro.provincename  xzz_provincename,
             xzzcity.cityname     xzz_cityname,
             xzzdis.districtname  xzz_districtname,
             hkzzpro.provincename hkdz_provincename,
             hkzzcity.cityname    hkdz_cityname,
             hkzzdis.districtname hkdz_districtname,
             jgpro.provincename   jg_provincename,
             jgcity.cityname      jg_cityname,
             dia.name             outdiagname, -- �������
             u1.name              CHIEFNAME,
             u2.name              attendname,
             u3.name              RESIDENTNAME
        from inpatient inp
        left join dictionary_detail pay
          on pay.detailid = inp.payid
         and pay.categoryid = '1'
        left join dictionary_detail job
          on job.detailid = inp.jobid
         and job.categoryid = '41'
        left join dictionary_detail nation
          on nation.detailid = inp.nationid
         and nation.categoryid = '42'
        left join dictionary_detail nationality
          on nationality.detailid = inp.nationalityid
         and nationality.categoryid = '43'
        left join dictionary_detail relationship
          on relationship.detailid = inp.relationship
         and relationship.categoryid = '44'
        left join diagnosis dia
          on dia.icd = inp.clinicdiagnosis --add ywk 2012��6��15�� 11:44:07
        left join users u1
          on u1.id = inp.chief
        left join users u2
          on u2.id = inp.attend
        left join users u3
          on u3.id = inp.resident
      ---ʡ���У��ص����ƴ�Inpatient����ȥ����ȡ���ƣ����������ѯ add by ywk 2012��5��17��10:58:03
        left join s_province csdpro
          on inp.provinceid = csdpro.provinceid --�����ص�ʡ
        left join s_city csdcity
          on inp.countyid = csdcity.cityid --�����ص���
        left join s_district csddis
          on inp.districtid = csddis.districtid --�����ص���

        left join s_province jgpro
          on inp.nativeplace_p = jgpro.provinceid --�����ʡ
        left join s_city jgcity
          on inp.nativeplace_c = jgcity.cityid --�������

        left join s_province xzzpro
          on inp.xzzproviceid = xzzpro.provinceid --��סַ��ʡ
        left join s_city xzzcity
          on inp.xzzcityid = xzzcity.cityid --��סַ����
        left join s_district xzzdis
          on inp.xzzdistrictid = xzzdis.districtid --��סַ����

        left join s_province hkzzpro
          on inp.hkdzproviceid = hkzzpro.provinceid --����סַ��ʡ
        left join s_city hkzzcity
          on inp.hkzdcityid = hkzzcity.cityid --����סַ����
        left join s_district hkzzdis
          on inp.hkzddistrictid = hkzzdis.districtid --����סַ����
       where inp.noofinpat = v_noofinpat;
  end;

  PROCEDURE usp_AddOrModIemFeeZY(v_id        varchar,
                                 v_NOOFINPAT varchar,
                                 v_TOTAL     varchar,
                                 v_OWNFEE    varchar,
                                 v_YBYLFY    varchar,
                                 v_ZYBZLZF   varchar,
                                 v_ZYBZLZHZF varchar,
                                 v_YBZLFY    varchar,
                                 v_CARE      varchar,
                                 v_ZHQTFY    varchar,
                                 v_BLZDF     varchar,
                                 v_SYSZDF    varchar,
                                 v_YXXZDF    varchar,
                                 v_LCZDF     varchar,
                                 v_FSSZLF    varchar,
                                 v_LCWLZLF   varchar,
                                 v_SSZLF     varchar,
                                 v_MZF       varchar,
                                 v_SSF       varchar,
                                 v_KFF       varchar,
                                 v_ZYZDF     varchar,
                                 v_ZYZLF     varchar,
                                 v_ZYWZ      varchar,
                                 v_ZYGS      varchar,
                                 v_ZCYJF     varchar,
                                 v_ZYTNZL    varchar,
                                 v_ZYGCZL    varchar,
                                 v_ZYTSZL    varchar,
                                 v_ZYQT      varchar,
                                 v_ZYTSTPJG  varchar,
                                 v_BZSS      varchar,
                                 v_XYF       varchar,
                                 v_KJYWF     varchar,
                                 v_CPMEDICAL varchar,
                                 v_YLJGZYZJF varchar,
                                 v_CMEDICAL  varchar,
                                 v_BLOODFEE  varchar,
                                 v_XDBLZPF   varchar,
                                 v_QDBLZPF   varchar,
                                 v_NXYZLZPF  varchar,
                                 v_XBYZLZPF  varchar,
                                 v_JCYYCXCLF varchar,
                                 v_ZLYYCXCLF varchar,
                                 v_SSYYCXCLF varchar,
                                 v_OTHERFEE  varchar,
                                 v_VALID     varchar) as
    v_count integer;
  begin
    select count(*)
      into v_count
      from iem_mainpage_feeinfoZY
     where iem_mainpage_feeinfoZY.Id = v_id
       and iem_mainpage_feeinfoZY.Valid = '1';
    if v_count <= 0 then
      insert into iem_mainpage_feeinfoZY
        (Id,
         Noofinpat,
         Total,
         Ownfee,
         Ybylfy,
         Zybzlzf,
         Zybzlzhzf,
         Ybzlfy,
         Care,
         Zhqtfy,
         Blzdf,
         Syszdf,
         Yxxzdf,
         Lczdf,
         Fsszlf,
         Lcwlzlf,
         Sszlf,
         Mzf,
         Ssf,
         kff,
         Zyzdf,
         Zyzlf,
         Zywz,
         Zygs,
         Zcyjf,
         Zytnzl,
         Zygczl,
         Zytszl,
         Zyqt,
         Zytstpjg,
         Bzss,
         Xyf,
         Kjywf,
         Cpmedical,
         Yljgzyzjf,
         Cmedical,
         Bloodfee,
         Xdblzpf,
         Qdblzpf,
         Nxyzlzpf,
         Xbyzlzpf,
         Jcyycxclf,
         Zlyycxclf,
         Ssyycxclf,
         Otherfee,
         Valid)
      values
        (v_id,
         v_NOOFINPAT,
         v_TOTAL,
         v_OWNFEE,
         v_YBYLFY,
         v_ZYBZLZF,
         v_ZYBZLZHZF,
         v_YBZLFY,
         v_CARE,
         v_ZHQTFY,
         v_BLZDF,
         v_SYSZDF,
         v_YXXZDF,
         v_LCZDF,
         v_FSSZLF,
         v_LCWLZLF,
         v_SSZLF,
         v_MZF,
         v_SSF,
         v_KFF,
         v_ZYZDF,
         v_ZYZLF,
         v_ZYWZ,
         v_ZYGS,
         v_ZCYJF,
         v_ZYTNZL,
         v_ZYGCZL,
         v_ZYTSZL,
         v_ZYQT,
         v_ZYTSTPJG,
         v_BZSS,
         v_XYF,
         v_KJYWF,
         v_CPMEDICAL,
         v_YLJGZYZJF,
         v_CMEDICAL,
         v_BLOODFEE,
         v_XDBLZPF,
         v_QDBLZPF,
         v_NXYZLZPF,
         v_XBYZLZPF,
         v_JCYYCXCLF,
         v_ZLYYCXCLF,
         v_SSYYCXCLF,
         v_OTHERFEE,
         v_VALID);
    else
      update iem_mainpage_feeinfoZY
         set TOTAL     = v_TOTAL,
             OWNFEE    = v_OWNFEE,
             YBYLFY    = v_YBYLFY,
             ZYBZLZF   = v_ZYBZLZF,
             ZYBZLZHZF = v_ZYBZLZHZF,
             YBZLFY    = v_YBZLFY,
             CARE      = v_CARE,
             ZHQTFY    = v_ZHQTFY,
             BLZDF     = v_BLZDF,
             SYSZDF    = v_SYSZDF,
             YXXZDF    = v_YXXZDF,
             LCZDF     = v_LCZDF,
             FSSZLF    = v_FSSZLF,
             LCWLZLF   = v_LCWLZLF,
             SSZLF     = v_SSZLF,
             MZF       = v_MZF,
             SSF       = v_SSF,
             KFF       = v_KFF,
             ZYZDF     = v_ZYZDF,
             ZYZLF     = v_ZYZLF,
             ZYWZ      = v_ZYWZ,
             ZYGS      = v_ZYGS,
             ZCYJF     = v_ZCYJF,
             ZYTNZL    = v_ZYTNZL,
             ZYGCZL    = v_ZYGCZL,
             ZYTSZL    = v_ZYTSZL,
             ZYQT      = v_ZYQT,
             ZYTSTPJG  = v_ZYTSTPJG,
             BZSS      = v_BZSS,
             XYF       = v_XYF,
             KJYWF     = v_KJYWF,
             CPMEDICAL = v_CPMEDICAL,
             YLJGZYZJF = v_YLJGZYZJF,
             CMEDICAL  = v_CMEDICAL,
             BLOODFEE  = v_BLOODFEE,
             XDBLZPF   = v_XDBLZPF,
             QDBLZPF   = v_QDBLZPF,
             NXYZLZPF  = v_NXYZLZPF,
             XBYZLZPF  = v_XBYZLZPF,
             JCYYCXCLF = v_JCYYCXCLF,
             ZLYYCXCLF = v_ZLYYCXCLF,
             SSYYCXCLF = v_SSYYCXCLF,
             OTHERFEE  = v_OTHERFEE
       where id = v_id;
    end if;
  end;

  PROCEDURE usp_GetIemFeeZYbyInpat(v_noofinpat varchar,
                                   o_result    OUT empcurtyp) as
  begin
    OPEN o_result FOR
      select *
        from iem_mainpage_feeinfozy
       where iem_mainpage_feeinfozy.valid = '1' and iem_mainpage_feeinfozy.noofinpat=v_noofinpat;
  end;

END;
/
