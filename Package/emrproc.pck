CREATE OR REPLACE PACKAGE emrproc IS
  -- global ��������
  /* ������ ������; */
  -- PROCEDURE����������
  /* PROCEDURE����(����...); */
  /* FUNCTION  ����(����...) RETURN ������; */
  TYPE empcurtyp IS REF CURSOR;

  PROCEDURE USP_EMR_PATRECFILE(v_flag             INTEGER,
                               v_indx             INTEGER,
                               v_NOOFINPAT        INTEGER,
                               v_TEMPLATEID       VARCHAR2,
                               v_FileNAME         VARCHAR2,
                               v_FileCONTENT      clob,
                               v_SORTID           VARCHAR2,
                               v_OWNER            VARCHAR2,
                               v_AUDITOR          VARCHAR2,
                               v_CREATETIME       CHAR,
                               v_AUDITTIME        CHAR,
                               v_VALID            INTEGER,
                               v_HASSUBMIT        INTEGER,
                               v_HASPRINT         INTEGER,
                               v_HASSIGN          INTEGER,
                               v_captiondatetime  varchar2,
                               v_FIRSTDAILYFLAG   varchar2,
                               v_py               varchar2,
                               v_wb               varchar2,
                               v_isyihuangoutong  varchar2,
                               v_isconfigpagesize varchar2,
                               v_DepartCode       varchar2,
                               v_wardCode         varchar2,
                               o_result           OUT empcurtyp);

  /*
  * ����  ����������Ĳ���
  */
  PROCEDURE usp_AuditApplyRecord(
                                 
                                 v_AuditType varchar, --�������
                                 v_ManID     varchar, --�����ID
                                 v_ID        numeric --��¼ID
                                 );

  /*
  * ����˵��      ɾ��ѡ����λ
  */
  PROCEDURE usp_DeleteJob(v_ID VARCHAR);

  /*
  * ����˵��      ɾ��ѡ����λ
  */

  /*
  * ����˵��      ɾ��ѡ����λ
  */
  PROCEDURE usp_DeleteJobPermission(v_ID VARCHAR);

  /*
  *  ����˵��      ɾ��ĳ��Ա����Ϣ
  */
  PROCEDURE usp_DeleteUserInfo(v_ID VARCHAR);

  /*
  *  ����  ��ȡ����ģ��ѡ����������
  */
  PROCEDURE usp_DepTemManager(v_dept varchar, o_result OUT empcurtyp);

  /*
  *  ����  �༭LONG_ORDERģ��
  */
  PROCEDURE usp_EditEmrLONG_ORDER(v_EditType         varchar default '', --��������
                                  v_LONGID           INTEGER default '',
                                  v_NOOFINPAT        INTEGER default '',
                                  v_GROUPID          INTEGER default '',
                                  v_GROUPFLAG        INTEGER default '',
                                  v_WARDID           VARCHAR2 default '',
                                  v_DEPTID           VARCHAR2 default '',
                                  v_TYPEDOCTOR       VARCHAR2 default '',
                                  v_TYPEDATE         CHAR default '',
                                  v_AUDITOR          VARCHAR2 default '',
                                  v_DATEOFAUDIT      CHAR default '',
                                  v_EXECUTOR         VARCHAR2 default '',
                                  v_EXECUTEDATE      CHAR default '',
                                  v_CANCELDOCTOR     VARCHAR2 default '',
                                  v_CANCELDATE       CHAR default '',
                                  v_CEASEDCOCTOR     VARCHAR2 default '',
                                  v_CEASEDATE        CHAR default '',
                                  v_CEASENURSE       VARCHAR2 default '',
                                  v_CEASEADUDITDATE  CHAR default '',
                                  v_STARTDATE        CHAR default '',
                                  v_TOMORROW         INTEGER default '',
                                  v_PRODUCTNO        INTEGER default '',
                                  v_NORMNO           INTEGER default '',
                                  v_MEDICINENO       INTEGER default '',
                                  v_DRUGNO           VARCHAR2 default '',
                                  v_DRUGNAME         VARCHAR2 default '',
                                  v_DRUGNORM         VARCHAR2 default '',
                                  v_ITEMTYPE         INTEGER default '',
                                  v_MINUNIT          VARCHAR2 default '',
                                  v_DRUGDOSE         INTEGER default '',
                                  v_DOSEUNIT         VARCHAR2 default '',
                                  v_UNITRATE         INTEGER default '',
                                  v_UNITTYPE         INTEGER default '',
                                  v_DRUGUSE          VARCHAR2 default '',
                                  v_BATCHNO          VARCHAR2 default '',
                                  v_EXECUTECOUNT     INTEGER default '',
                                  v_EXECUTECYCLE     INTEGER default '',
                                  v_CYCLEUNIT        INTEGER default '',
                                  v_DATEOFWEEK       CHAR default '',
                                  v_INNEREXECUTETIME VARCHAR2 default '',
                                  v_EXECUTEDEPT      VARCHAR2 default '',
                                  v_ENTRUST          VARCHAR2 default '',
                                  v_ORDERTYPE        INTEGER default '',
                                  v_ORDERSTATUS      INTEGER default '',
                                  v_SPECIALMARK      INTEGER default '',
                                  v_CEASEREASON      INTEGER default '',
                                  v_CURGERYID        INTEGER default '',
                                  v_CONTENT          VARCHAR2 default '',
                                  v_SYNCH            INTEGER default '',
                                  v_MEMO             VARCHAR2 default '',
                                  v_DJFL             VARCHAR2 default '',
                                  o_result           OUT empcurtyp);

  /*
  *  ����  �༭TEMP_ORDER��ʱҽ����
  */
  PROCEDURE usp_EditEmrTEMP_ORDER(v_EditType         VARCHAR2 default '', --��������
                                  v_TEMPID           INTEGER default '',
                                  v_NOOFINPAT        INTEGER default '',
                                  v_GROUPID          INTEGER default '',
                                  v_GROUPFLAG        INTEGER default '',
                                  v_WARDID           VARCHAR2 default '',
                                  v_DEPTID           VARCHAR2 default '',
                                  v_TYPEDOCTOR       VARCHAR2 default '',
                                  v_TYPEDATE         CHAR default '',
                                  v_AUDITOR          VARCHAR2 default '',
                                  v_DATEOFAUDIT      CHAR default '',
                                  v_EXECUTOR         VARCHAR2 default '',
                                  v_EXECUTEDATE      CHAR default '',
                                  v_CANCELDOCTOR     VARCHAR2 default '',
                                  v_CANCELDATE       CHAR default '',
                                  v_STARTDATE        CHAR default '',
                                  v_PRODUCTNO        INTEGER default '',
                                  v_NORMNO           INTEGER default '',
                                  v_MEDICINENO       INTEGER default '',
                                  v_DRUGNO           VARCHAR2 default '',
                                  v_DRUGNAME         VARCHAR2 default '',
                                  v_DRUGNORM         VARCHAR2 default '',
                                  v_ITEMTYPE         INTEGER default '',
                                  v_MINUNIT          VARCHAR2 default '',
                                  v_DRUGDOSE         INTEGER default '',
                                  v_DOSEUNIT         VARCHAR2 default '',
                                  v_UNITRATE         INTEGER default '',
                                  v_UNITTYPE         INTEGER default '',
                                  v_DRUGUSE          VARCHAR2 default '',
                                  v_BATCHNO          VARCHAR2 default '',
                                  v_EXECUTECOUNT     INTEGER default '',
                                  v_EXECUTECYCLE     INTEGER default '',
                                  v_CYCLEUNIT        INTEGER default '',
                                  v_DATEOFWEEK       CHAR default '',
                                  v_INNEREXECUTETIME VARCHAR2 default '',
                                  v_ZXTS             INTEGER default '',
                                  v_TOTALDOSE        INTEGER default '',
                                  v_ENTRUST          VARCHAR2 default '',
                                  v_ORDERTYPE        INTEGER default '',
                                  v_ORDERSTATUS      INTEGER default '',
                                  v_SPECIALMARK      INTEGER default '',
                                  v_CEASEID          INTEGER default '',
                                  v_CEASEDATE        CHAR default '',
                                  v_CONTENT          VARCHAR2 default '',
                                  v_SYNCH            INTEGER default '',
                                  v_MEMO             VARCHAR2 default '',
                                  v_FORMTYPE         VARCHAR2 default '',
                                  o_result           OUT empcurtyp);
  /*
  *  ����  �༭����ʷ��Ϣ
  */
  PROCEDURE usp_EditAllergyHistoryInfo(v_EditType     varchar, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                       v_ID           numeric, --Ψһ��
                                       v_NoOfInpat    numeric default '-1', --������ҳ���
                                       v_AllergyID    Int default -1, --��������
                                       v_AllergyLevel int default -1, --�����̶�
                                       v_Doctor       varchar default '', --����ҽ��
                                       v_AllergyPart  varchar default '', --������λ
                                       v_ReactionType varchar default '', --��Ӧ����
                                       v_Memo         varchar default '' --��ע
                                       );

  /*
  *  ����  �༭������Ϣ
  */
  PROCEDURE usp_EditFamilyHistoryInfo(v_EditType  varchar, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                      v_ID        numeric, --��ϵ�˱�ţ��ǲ�����ϵ�˵�Ψһ��ʶ
                                      v_NoOfInpat numeric default '-1', --��ҳ���(סԺ��ˮ��)(Inpatient.NoOfInpat)
                                      v_Relation  varchar default '', --�����ϵ
                                      v_Birthday  varchar default '', --�������ڣ���ǰ̨��ʾ���䣩
                                      v_DiseaseID varchar default '', --���ִ���
                                      v_Breathing int default '', --�Ƿ���(1:�ǣ�0��)
                                      v_Cause     varchar default '', --����ԭ��
                                      v_Memo      varchar default '' --��ע
                                      );

  /*
  *  ����  �༭����ʷ��Ϣ
  */
  PROCEDURE usp_EditIllnessHistoryInfo(v_EditType     varchar, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                       v_ID           numeric, --Ψһ��
                                       v_NoOfInpat    numeric default '-1', --������ҳ���
                                       v_DiagnosisICD varchar default '', --���ִ���
                                       v_Discuss      varchar default '', --��������
                                       v_DiseaseTime  varchar default '', --����ʱ��
                                       v_Cure         int default '', --�Ƿ�����
                                       v_Memo         varchar default '' --��ע
                                       );

  /*
  *  ����  ���滤����Ϣ����
  */
  PROCEDURE usp_EditNotesOnNursingInfoNew(v_NoOfInpat        numeric, --��ҳ���(סԺ��ˮ��)
                                          v_DateOfSurvey     varchar, --���������ڣ���ʽ2010-01-01��
                                          v_Indx             varchar, --���
                                          v_TimeSlot         varchar, --����ʱ���
                                          v_Temperature      varchar default '', --��������
                                          v_WayOfSurvey      int default 8800, --���²�����ʽ����
                                          v_Pulse            varchar default '', --����
                                          v_HeartRate        varchar default '', --����
                                          v_Breathe          varchar default '', --���ߺ���
                                          v_BloodPressure    varchar default '', --����Ѫѹ
                                          v_TimeOfShit       varchar default '', --����������ʽ��1*(3/2E ),'*'��ʾ���ʧ��
                                          v_CountIn          varchar default '', --����������
                                          v_CountOut         varchar default '', --�����ܳ���
                                          v_Drainage         varchar default '', --������
                                          v_Height           varchar default '', --�������
                                          v_Weight           varchar default '', --��������
                                          v_Allergy          varchar default '', --���߹�����
                                          v_DaysAfterSurgery varchar default '', --����������
                                          v_DayOfHospital    varchar default '', --סԺ����
                                          v_PhysicalCooling  varchar default '', --������
                                          v_DoctorOfRecord   varchar, --��¼ҽ��
                                          v_PhysicalHotting  varchar default '', --�������� edit by ywk
                                          v_PainInfo         varchar default '' --��ʹ
                                          );
  /*
  *  ����  ���滤����Ϣ����
  */
  PROCEDURE usp_EditNotesOnNursingInfo(v_NoOfInpat        numeric, --��ҳ���(סԺ��ˮ��)
                                       v_DateOfSurvey     varchar, --���������ڣ���ʽ2010-01-01��
                                       v_TimeSlot         varchar, --����ʱ���
                                       v_Temperature      varchar default '', --��������
                                       v_WayOfSurvey      int default 8800, --���²�����ʽ����
                                       v_Pulse            varchar default '', --����
                                       v_HeartRate        varchar default '', --����
                                       v_Breathe          varchar default '', --���ߺ���
                                       v_BloodPressure    varchar default '', --����Ѫѹ
                                       v_TimeOfShit       varchar default '', --����������ʽ��1*(3/2E ),'*'��ʾ���ʧ��
                                       v_CountIn          varchar default '', --����������
                                       v_CountOut         varchar default '', --�����ܳ���
                                       v_Drainage         varchar default '', --������
                                       v_Height           varchar default '', --�������
                                       v_Weight           varchar default '', --��������
                                       v_Allergy          varchar default '', --���߹�����
                                       v_DaysAfterSurgery varchar default '', --����������
                                       v_DayOfHospital    varchar default '', --סԺ����
                                       v_PhysicalCooling  varchar default '', --������
                                       v_DoctorOfRecord   varchar, --��¼ҽ��
                                       v_PhysicalHotting  varchar default '', --�������� edit by ywk
                                       v_PainInfo         varchar default '' --��ʹ
                                       );
  --��ʿ���ⵥ��Ϣά���С�����״̬��Ϣ��ά�� add by ywk 2012��4��23��14:05:18

  PROCEDURE usp_Edit_PatStates(v_EditType  varchar default '', --��������
                               v_CCode     varchar default '',
                               V_ID        varchar default '',
                               v_NoofInPat varchar default '',
                               v_DoTime    varchar default '',
                               v_Patid     varchar default '',
                               o_result    OUT empcurtyp);

  /*
  * �༭��һ��ϵ����Ϣ
  */
  PROCEDURE usp_EditPatientContacts(v_EditType   varchar, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                    v_ID         numeric default '0', --��ϵ�˱�ţ��ǲ�����ϵ�˵�Ψһ��ʶ
                                    v_NoOfInpat  numeric default '0', --��ҳ���(סԺ��ˮ��)(Inpatient.NoOfInpat)
                                    v_Name       varchar default '', --��ϵ����
                                    v_Sex        varchar default '', --�Ա�
                                    v_Relation   varchar default '', --��ϵ��ϵ(Dictionary_detail.DetailID, ID = '44')
                                    v_Address    varchar default '', --��ϵ��ַ
                                    v_WorkUnit   varchar default '', --��ϵ(��)��λ
                                    v_HomeTel    varchar default '', --��ϵ�˼�ͥ�绰
                                    v_WorkTel    varchar default '', --��ϵ�˹����绰
                                    v_PostalCode varchar default '', --��ϵ�ʱ�
                                    v_Tag        varchar default '' --��ϵ�˱�־
                                    );

  /*
  * �༭����ʷ��Ϣ
  */
  PROCEDURE usp_EditPersonalHistoryInfo(v_NoOfInpat    numeric, --������ҳ���
                                        v_Marital      varchar, --����״��
                                        v_NoOfChild    int, --��������
                                        v_JobHistory   varchar, --ְҵ����
                                        v_DrinkWine    int, --�Ƿ�����
                                        v_WineHistory  varchar, --����ʷ
                                        v_Smoke        int, --�Ƿ�����
                                        v_SmokeHistory varchar, --����ʷ
                                        v_BirthPlace   varchar, --�����أ�ʡ�У�
                                        v_PassPlace    varchar, --�����أ�ʡ�У�
                                        v_Memo         varchar default '' --��ע
                                        );

  /*
  * �༭����ʷ��Ϣ
  */
  PROCEDURE usp_EditSurgeryHistoryInfo(v_EditType    varchar, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                       v_ID          numeric, --Ψһ��
                                       v_NoOfInpat   numeric default '-1', --������ҳ���
                                       v_SurgeryID   varchar default '', --��������(Surgery.ID)
                                       v_DiagnosisID varchar default '', --������Diagnosis.ICD��
                                       v_Discuss     varchar default '', --����
                                       v_Doctor      varchar default '', --����ҽ��
                                       v_Memo        varchar default '' --��ע
                                       );

  /*
  * ���� .NETģ��ѡ��
  */
  PROCEDURE usp_Emr_ModelSearcher(v_flag   int,
                                  v_type   varchar,
                                  o_result OUT empcurtyp);

  /*
  * ���� ���ݲ�����ҳ��Ż�ȡ������Ϣ
  */
  PROCEDURE USP_EMR_GETPATINFO(v_NoOfinpat varchar, o_result OUT empcurtyp);

  /*
  * ���� ��ѯ���õĳ���ҽ��
  */
  procedure usp_Emr_QueryOrderSuites(v_DeptID   varchar,
                                     v_WardID   varchar,
                                     v_DoctorID varchar,
                                     v_yzlr     int default 1,
                                     o_result   OUT empcurtyp,
                                     o_result1  OUT empcurtyp);

  /*
  * ���� ����ҽ���ķ������
  */
  procedure usp_Emr_SetOrderGroupSerialNo(v_NoOfInpat numeric,
                                          v_OrderType int,
                                          v_onlynew   int default 1);

  /*
  * ��ȡ������Ĳ���
  */
  PROCEDURE usp_GetApplyRecordNew(v_DateBegin varchar, --��ʼ����
                                  v_DateEnd   varchar, --��������
                                  --v_DateInBegin  varchar, --��Ժ��ʼ����
                                  --v_DateInEnd    varchar, --��Ժ��������
                                  v_PatientName varchar, --��������
                                  v_DocName     VARCHAR, --����ҽʦ����
                                  v_OutHosDept  varchar, --��Ժ���ҿ���
                                  o_result      OUT empcurtyp);
  /*
  * ��ȡ������Ĳ���
  */
  PROCEDURE usp_GetApplyRecord(v_DateBegin  varchar, --��ʼ����
                               v_DateEnd    varchar, --��������
                               v_OutHosDept varchar, --��Ժ���ҿ���
                               o_result     OUT empcurtyp);

  /*
  * ����  ��ȡϵͳ��ǰ����
  */
  PROCEDURE usp_GetCurrSystemParam(v_GetType int, --��������
                                   o_result  OUT empcurtyp);

  /*
  * ����  ��ȡ���Ҳ���ʧ�ֵ�
  */
  PROCEDURE usp_GetDeptDeductPoint(v_DeptCode      varchar default '',
                                   v_DateTimeBegin varchar,
                                   v_DateTimeEnd   varchar,
                                   v_PointID       varchar default '',
                                   v_QCStatType    int,
                                   o_result        OUT empcurtyp);

  /*
  * ����  ��ȡҽ��������Ϣ����
  */
  PROCEDURE usp_GetDoctorTaskInfo(v_Wardid  varchar, --����
                                  v_Deptids varchar, --����
                                  v_UserID  varchar, --�û�ID
                                  v_Time    varchar, --ʱ��
                                  o_result  OUT empcurtyp);

  /*
  * ����  ��ȡҽԺ��Ϣ
  */
  PROCEDURE usp_GetHospitalInfo(o_result OUT empcurtyp);

  /*
  * ����  ��ȡ��������
  */
  PROCEDURE usp_GetIemInfo(v_NoOfInpat int,
                           o_result    OUT empcurtyp,
                           o_result1   OUT empcurtyp,
                           o_result2   OUT empcurtyp,
                           o_result3   OUT empcurtyp);

  /*
  * ����  ��ȡ������������δ�鵵����
  */
  PROCEDURE usp_GetInpatientFiling(v_DeptCode      varchar default '',
                                   v_InpatName     varchar default '',
                                   v_DateTimeBegin varchar,
                                   v_DateTimeEnd   varchar,
                                   v_QCStatType    int,
                                   o_result        OUT empcurtyp);

  /*
  * ����  ���ݴ����ʱ����Լ�������ҳ���  ��ѯ����ҽ��������Ϣ
  */
  PROCEDURE usp_GetInpatientReport(v_NoOfInpat     varchar default '',
                                   v_DateTimeBegin varchar default '',
                                   v_DateTimeEnd   varchar default '',
                                   v_SubmitDocID   varchar default '',
                                   v_ReportID      varchar default '',
                                   v_QCStatType    varchar default '',
                                   o_result        OUT empcurtyp);

  /*
  * ����    ������и�λ��Ϣ
  */
  PROCEDURE usp_GetJobPermissionInfo(v_JobId  VARCHAR,
                                     o_result out empcurtyp);

  /*
  * ����    ������и�λ��Ϣ
  */
  PROCEDURE usp_GetKnowledgePublicInfo(v_OrderType numeric, --�������
                                       o_result    out empcurtyp);

  /*
  * ����    ȡ��Ҫ��lookupeditor����ʾ������,��ð���ID��Name,Py,Memo������������APP�����ʱ��ͳһ�ķ���
  */
  PROCEDURE usp_GetLookUpEditorData(v_QueryType numeric, --��ѯ������
                                    v_QueryID   numeric default 0,
                                    o_result    out empcurtyp);

  /*********************************************************************************/
  PROCEDURE usp_getmedicalrrecordviewnew(v_deptcode        VARCHAR DEFAULT '',
                                         v_datetimebegin   VARCHAR,
                                         v_datetimeend     VARCHAR,
                                         v_datetimeinbegin VARCHAR,
                                         v_datetimeinend   VARCHAR,
                                         v_patientname     VARCHAR DEFAULT '',
                                         v_recordid        VARCHAR DEFAULT '',
                                         v_applydoctor     VARCHAR DEFAULT '',
                                         v_qcstattype      INT,
                                         --Add wwj ����ģ����ѯ
                                         v_patid   VARCHAR DEFAULT '',
                                         v_indiag  VARCHAR DEFAULT '', --��Ժ���
                                         v_outdiag VARCHAR DEFAULT '', --��Ժ���
                                         v_curdiag VARCHAR DEFAULT '', --��ǰ���
                                         o_result  OUT empcurtyp);

  /*
  * ����    �����һ���Ҷ�Ӧ���˵Ĺ鵵��δ�鵵���Ӳ���
  */
  PROCEDURE usp_GetMedicalRrecordView(v_DeptCode      varchar default '',
                                      v_DateTimeBegin varchar,
                                      v_DateTimeEnd   varchar,
                                      v_PatientName   varchar default '',
                                      v_RecordID      varchar default '',
                                      v_ApplyDoctor   varchar default '',
                                      v_QCStatType    int,
                                      --Add wwj ����ģ����ѯ
                                      v_PatID   varchar default '',
                                      v_outdiag VARCHAR DEFAULT '', --add by cyq 2012-12-07
                                      o_result  out empcurtyp);

  /*
  * ����    ����������ĵ��Ӳ�����Ϣ
  */
  PROCEDURE usp_GetMedicalRrecordViewFrm(v_GetType varchar, --��ȡ�������ͣ�1�����ҡ�2���������Ŀ�ġ�3���������޵�λ
                                         o_result  out empcurtyp);

  /*
  * ����    ��ȡ������Ϣ����
  */
  PROCEDURE usp_GetNotesOnNursingInfo(v_NoOfInpat    numeric, --��ҳ���(סԺ��ˮ��)
                                      v_DateOfSurvey varchar, --���������ڣ���ʽ2010-01-01��
                                      o_result       out empcurtyp);

  /*
  * ����    ��ȡ������Ϣ����
  */
  PROCEDURE usp_GetNursingRecordByDate(v_NoOfInpat numeric default '0', --��ҳ���
                                       v_BeginDate varchar, --��ʼʱ��
                                       v_EndDate   varchar, --��ֹʱ��
                                       o_result    out empcurtyp);

  /*
  * ����    ��ȡ����������Ŀ�������
  */
  PROCEDURE usp_GetNursingRecordParam(v_FrmType varchar, --�����������
                                      o_result  out empcurtyp);

  /*
  * ����    ��ȡ����������Ŀ�������
  */
  PROCEDURE usp_GetPatientBedInfoByDate(v_NoOfInpat    numeric default '0', --��ҳ���
                                        v_AllocateDate varchar, --ָ����ʱ��
                                        o_result       out empcurtyp);

  /*
  * ����    usp_GetPatientInfoForThreeMeas
  */
  PROCEDURE usp_GetPatientInfoForThreeMeas(v_NoOfInpat numeric default '0', --��ҳ���
                                           o_result    out empcurtyp);

  --���ݲ���name���ز���ICD
  PROCEDURE usp_getdiagicdbyname(v_name   varchar, --��������
                                 o_result OUT empcurtyp);

  --���ݲ���ICD���ز�������
  /*********add by wyt 2012-08-15************************************************************************/
  PROCEDURE usp_getdiagnamebyicd(v_icd    varchar, --���ֱ��
                                 o_result OUT empcurtyp);

  --��ѯĳ���ҽʦ��������
  /*************add by wyt 2012-08-15****************************************************************/
  PROCEDURE usp_getdiagbyattendphysician(v_userid varchar, --�û����
                                         o_result OUT empcurtyp);

  /*
  * ����    ��ȡ����������ؼ���ʼ������
  */
  PROCEDURE usp_GetRecordManageFrm(v_FrmType    varchar, --�������ͣ�1n��������Ϣ����ά�����塢2n�����˲���ʷ��Ϣ����
                                   v_PatNoOfHis numeric default '0', --��ҳ���
                                   v_CategoryID varchar default '', --(�ֵ�)������ ���򸸽ڵ���� ����ҳ���
                                   o_result     out empcurtyp);

  /*
  *���� ��ȡ���ҽʦδ�鵵��������������ѯ��
  */
  PROCEDURE usp_getattendrecordnoonfile(v_dateOutHosBegin VARCHAR, --��Ժ��ʼ����
                                        v_dateOutHosEnd   VARCHAR, --��Ժ��������
                                        v_dateInHosBegin  VARCHAR, --��Ժ��ʼ����
                                        v_dateInHosEnd    VARCHAR, --��Ժ��ֹ����
                                        v_deptid          VARCHAR, --���Ҵ���
                                        v_status          VARCHAR, --����״̬
                                        v_patientname     VARCHAR DEFAULT '', --��������
                                        v_recordid        VARCHAR DEFAULT '', --������
                                        v_indiag          VARCHAR DEFAULT '', --��Ժ���
                                        v_outdiag         VARCHAR DEFAULT '', --��Ժ���
                                        v_curdiag         VARCHAR DEFAULT '', --��ǰ���
                                        v_diaGroupStatus  VARCHAR, --��Ϸ���״̬
                                        v_PatientStatus   VARCHAR, --����״̬                                        
                                        v_ingroupid       VARCHAR, --����ID1
                                        v_outgroupid      VARCHAR, --����ID2
                                        o_result          OUT empcurtyp);

  /*
  *���� ��ȡδ�鵵��������������ѯ��
  */
  PROCEDURE usp_getrecordnoonfileWyt(v_dateOutHosBegin VARCHAR, --��Ժ��ʼ����
                                     v_dateOutHosEnd   VARCHAR, --��Ժ��������
                                     v_dateInHosBegin  VARCHAR, --��Ժ��ʼ����
                                     v_dateInHosEnd    VARCHAR, --��Ժ��ֹ����
                                     v_deptid          VARCHAR, --���Ҵ���
                                     v_indiag          VARCHAR, --��Ժ���
                                     v_outdiag         VARCHAR, --��Ժ���
                                     v_status          VARCHAR, --����״̬
                                     v_patientname     VARCHAR DEFAULT '', --��������
                                     v_patientid       VARCHAR DEFAULT '', --����ID
                                     v_recordid        VARCHAR DEFAULT '', --������
                                     v_physician       VARCHAR DEFAULT '', --סԺҽʦ��
                                     o_result          OUT empcurtyp);
  /*
  * ����    ��ȡδ�鵵����
  */
  PROCEDURE usp_GetRecordNoOnFile(v_DateBegin   varchar, --��ʼ����
                                  v_DateEnd     varchar, --��������
                                  v_DeptID      varchar, --���Ҵ���
                                  v_Status      varchar, --����״̬
                                  v_PatientName varchar default '', --��������
                                  v_RecordID    varchar default '', --������
                                  o_result      out empcurtyp);

  /*
  * ����    ��ȡδ�鵵����
  */
  PROCEDURE usp_GetRecordNoOnFileNew(v_DateBegin   varchar, --��ʼ����
                                     v_DateEnd     varchar, --��������
                                     v_DeptID      varchar, --���Ҵ���
                                     v_Status      varchar, --����״̬
                                     v_PatientName varchar default '', --��������
                                     v_RecordID    varchar default '', --������
                                     o_result      out empcurtyp);

  /*
  * ����    ��ȡ�ѹ鵵����
  */
  PROCEDURE usp_getrecordonfile(v_datebegin  VARCHAR, --��ʼ����
                                v_dateend    VARCHAR, --��������
                                v_patid      VARCHAR DEFAULT '', --סԺ����
                                v_name       VARCHAR DEFAULT '', --��������
                                v_sexid      VARCHAR DEFAULT '', --�����Ա�
                                v_agebegin   VARCHAR DEFAULT '', --��ʼ����
                                v_ageend     VARCHAR DEFAULT '', --��������
                                v_outhosdept VARCHAR DEFAULT '', --��Ժ���ҿ���
                                v_indiag     VARCHAR DEFAULT '', --��Ժ���
                                v_outdiag    VARCHAR DEFAULT '', --��Ժ���
                                v_surgeryid  VARCHAR DEFAULT '', --��������
                                v_physician  VARCHAR DEFAULT '', --סԺҽʦ����
                                o_result     OUT empcurtyp);

  /*
  * ����    ��ȡ��ǩ�չ黹����
  */
  PROCEDURE usp_GetSignInRecordNew(v_DateBegin   varchar, --��ʼ����
                                   v_DateEnd     varchar, --��������
                                   v_DateInBegin varchar, --��Ժ��ʼ����
                                   v_DateInEnd   varchar, --��Ժ��������
                                   v_PatientName varchar, --��������
                                   v_OutHosDept  varchar, --��Ժ���ҿ���
                                   o_result      out empcurtyp);

  /*
  * ����    ��ȡ��ǩ�չ黹����
  */
  PROCEDURE usp_GetSignInRecord(v_DateBegin  varchar, --��ʼ����
                                v_DateEnd    varchar, --��������
                                v_OutHosDept varchar, --��Ժ���ҿ���
                                o_result     out empcurtyp);
  /*
  * ����    ��ȡ��������
  */
  PROCEDURE usp_GetTemplatePersonGroup(v_UserID  varchar default '',
                                       o_result  OUT empcurtyp,
                                       o_result1 OUT empcurtyp);

  /*
  * ����    ������Ϣ�������̣�
  */
  PROCEDURE usp_Inpatient_Trigger(v_syxh varchar2 --������ҳ���
                                  );

  /*
  * ����    ����ͼƬ��Ϣ
  */
  PROCEDURE usp_InsertImage(v_Sort   int,
                            v_Name   varchar,
                            v_Memo   varchar default '',
                            v_ID     numeric default 0,
                            o_result OUT empcurtyp);

  /*
  * ����    �����λ��Ϣ
  */
  PROCEDURE usp_InsertJob(v_Id VARCHAR, v_Title VARCHAR, v_Memo VARCHAR);

  /*
  * ����    �����µ���Ȩ��Ϣ
  */
  PROCEDURE usp_InsertJobPermission(v_ID         VARCHAR,
                                    v_Moduleid   VARCHAR,
                                    v_Modulename VARCHAR);

  /*
  * ����    ��Ӳ��˻����ĵ�
  */
  procedure usp_InsertNursRecordTable(v_NoOfInpat  numeric, --��ҳ���(סԺ��ˮ��)
                                      v_name       varchar, --��¼�����ƣ�ģ������+����+ʱ�䣩
                                      v_Content    blob, --��������
                                      v_TemplateID numeric, --ģ��ID
                                      v_version    varchar, --ģ��汾(�Զ�������,Ĭ��Ϊ1.00.00,�������������1λ)
                                      v_Department varchar, --�����������Ҵ���
                                      v_SortID     numeric, --ģ��������
                                      v_Valid      int --��Ч��¼(CategoryDetail.ID CategoryID = 0)
                                      );

  /*
  * ����    ���������ĵ�ģ�壬������ID
  */
  procedure usp_InsertNursTableTemplate(v_Name     varchar, --ģ������
                                        v_Describe varchar, --ģ������
                                        v_Content  blob, --ģ������
                                        v_version  varchar, --ģ��汾(�Զ�������,Ĭ��Ϊ1.00.00,�������������1λ)
                                        v_SortID   varchar, --ģ��������
                                        v_Valid    int, --��Ч��¼(CategoryDetail.ID CategoryID = 0)
                                        o_result   OUT empcurtyp);
  /*
  * ����    ��ȡ��������
  */
  PROCEDURE usp_InsertTemplatePersonGroup(v_UserID             varchar default '',
                                          v_NodeID             int default 0,
                                          v_TemplatePersonID   int default 0,
                                          v_ParentNodeID       int default 0,
                                          v_Name               varchar default '',
                                          v_TypeID             int default 0,
                                          v_TemplatePersonName varchar default '',
                                          v_TemplatePersonMemo varchar default '');

  /*
  * ����    �û���¼����޸�
  */
  PROCEDURE usp_InsertUserLogIn(v_ID          varchar,
                                v_ModuleId    varchar,
                                v_HostName    varchar,
                                v_MACADDR     varchar,
                                v_Client_ip   varchar,
                                v_Reason_id   Varchar,
                                v_Create_user varchar);

  PROCEDURE usp_Insert_Iem_Mainpage_Basic(v_PatNoOfHis               numeric,
                                          v_NoOfInpat                numeric,
                                          v_PayID                    varchar,
                                          v_SocialCare               varchar,
                                          v_InCount                  int,
                                          v_Name                     varchar,
                                          v_SexID                    varchar,
                                          v_Birth                    char,
                                          v_Marital                  varchar,
                                          v_JobID                    varchar,
                                          v_ProvinceID               varchar,
                                          v_CountyID                 varchar,
                                          v_NationID                 varchar,
                                          v_NationalityID            varchar,
                                          v_IDNO                     varchar,
                                          v_Organization             varchar,
                                          v_OfficePlace              varchar,
                                          v_OfficeTEL                varchar,
                                          v_OfficePost               varchar,
                                          v_NativeAddress            varchar,
                                          v_NativeTEL                varchar,
                                          v_NativePost               varchar,
                                          v_ContactPerson            varchar,
                                          v_Relationship             varchar,
                                          v_ContactAddress           varchar,
                                          v_ContactTEL               varchar,
                                          v_AdmitDate                varchar,
                                          v_AdmitDept                varchar,
                                          v_AdmitWard                varchar,
                                          v_Days_Before              numeric,
                                          v_Trans_Date               varchar,
                                          v_Trans_AdmitDept          varchar,
                                          v_Trans_AdmitWard          varchar,
                                          v_Trans_AdmitDept_Again    varchar,
                                          v_OutWardDate              varchar,
                                          v_OutHosDept               varchar,
                                          v_OutHosWard               varchar,
                                          v_Actual_Days              numeric,
                                          v_Death_Time               varchar,
                                          v_Death_Reason             varchar,
                                          v_AdmitInfo                varchar,
                                          v_In_Check_Date            varchar,
                                          v_Pathology_Diagnosis_Name varchar,
                                          v_Pathology_Observation_Sn varchar,
                                          v_Ashes_Diagnosis_Name     varchar,
                                          v_Ashes_Anatomise_Sn       varchar,
                                          v_Allergic_Drug            varchar,
                                          v_Hbsag                    numeric,
                                          v_Hcv_Ab                   numeric,
                                          v_Hiv_Ab                   numeric,
                                          v_Opd_Ipd_Id               numeric,
                                          v_In_Out_Inpatinet_Id      numeric,
                                          v_Before_After_Or_Id       numeric,
                                          v_Clinical_Pathology_Id    numeric,
                                          v_Pacs_Pathology_Id        numeric,
                                          v_Save_Times               numeric,
                                          v_Success_Times            numeric,
                                          v_Section_Director         varchar,
                                          v_Director                 varchar,
                                          v_Vs_Employee_Code         varchar,
                                          v_Resident_Employee_Code   varchar,
                                          v_Refresh_Employee_Code    varchar,
                                          v_Master_Interne           varchar,
                                          v_Interne                  varchar,
                                          v_Coding_User              varchar,
                                          v_Medical_Quality_Id       numeric,
                                          v_Quality_Control_Doctor   varchar,
                                          v_Quality_Control_Nurse    varchar,
                                          v_Quality_Control_Date     varchar,
                                          v_Xay_Sn                   varchar,
                                          v_Ct_Sn                    varchar,
                                          v_Mri_Sn                   varchar,
                                          v_Dsa_Sn                   varchar,
                                          v_Is_First_Case            numeric,
                                          v_Is_Following             numeric,
                                          v_Following_Ending_Date    varchar,
                                          v_Is_Teaching_Case         numeric,
                                          v_Blood_Type_id            numeric,
                                          v_Rh                       numeric,
                                          v_Blood_Reaction_Id        numeric,
                                          v_Blood_Rbc                numeric,
                                          v_Blood_Plt                numeric,
                                          v_Blood_Plasma             numeric,
                                          v_Blood_Wb                 numeric,
                                          v_Blood_Others             varchar,
                                          v_Is_Completed             varchar,
                                          v_completed_time           varchar,
                                          --v_Valide numeric ,
                                          v_Create_User varchar,
                                          --v_Create_Time varchar(19)
                                          --v_Modified_User varchar(10) ,
                                          --v_Modified_Time varchar(19)
                                          o_result OUT empcurtyp);

  /*
  * ���빦������ҳ���TABLE
  */
  PROCEDURE usp_Insert_Iem_Mainpage_Diag(v_Iem_Mainpage_NO   numeric,
                                         v_Diagnosis_Type_Id numeric,
                                         v_Diagnosis_Code    varchar,
                                         v_Diagnosis_Name    varchar,
                                         v_Status_Id         numeric,
                                         v_Order_Value       numeric,
                                         --v_Valide numeric ,
                                         v_Create_User varchar
                                         --v_Create_Time varchar(19) ,
                                         --v_Cancel_User varchar(10) ,
                                         --v_Cancel_Time varchar(19)
                                         );
  /*
  * ���빦������ҳ����TABLE
  */
  PROCEDURE usp_Insert_Iem_MainPage_Oper(v_IEM_MainPage_NO     numeric,
                                         v_Operation_Code      varchar,
                                         v_Operation_Date      varchar,
                                         v_Operation_Name      varchar,
                                         v_Execute_User1       varchar,
                                         v_Execute_User2       varchar,
                                         v_Execute_User3       varchar,
                                         v_Anaesthesia_Type_Id numeric,
                                         v_Close_Level         numeric,
                                         v_Anaesthesia_User    varchar,
                                         --v_Valide numeric ,
                                         v_Create_User varchar
                                         --v_Create_Time varchar(19)
                                         --v_Cancel_User varchar(10) ,
                                         --v_Cancel_Time varchar(19)
                                         );

  /*********************************************************************************/
  /*�������ĳ�����˵ĵ�ǰ�����Ϣ(wyt 2012-08-15)*/
  PROCEDURE usp_updatecurrentdiaginfo(v_patient_id   VARCHAR2,
                                      v_patient_name VARCHAR2,
                                      v_diag_code    VARCHAR2,
                                      v_diag_content varchar2);

  /*�������ĳ������ҽʦ���ֲ鿴Ȩ����Ϣ(wyt 2012-08-13)*/
  PROCEDURE usp_updateattendphysicianinfo(v_id            VARCHAR2,
                                          v_name          VARCHAR2,
                                          v_py            VARCHAR2,
                                          v_wb            VARCHAR2,
                                          v_deptid        VARCHAR2,
                                          v_relatedisease varchar2);

  /*�������ĳ��Ա����Ϣ*/
  PROCEDURE usp_updateuserinfo(v_id           VARCHAR2,
                               v_name         VARCHAR2,
                               v_deptid       VARCHAR2,
                               v_wardid       VARCHAR2,
                               v_jobid        VARCHAR2,
                               v_Py           VARCHAR2,
                               v_Wb           VARCHAR2,
                               v_Sexy         VARCHAR2,
                               v_Birth        VARCHAR2,
                               v_Marital      VARCHAR2,
                               v_Idno         VARCHAR2,
                               v_Category     VARCHAR2,
                               v_Jobtitle     VARCHAR2,
                               v_Recipeid     VARCHAR2,
                               v_Recipemark   VARCHAR2,
                               v_Narcosismark VARCHAR2,
                               v_Grade        VARCHAR2,
                               v_Status       VARCHAR2,
                               v_Memo         varchar2);

  /*�޸ĸ�λ��Ϣ*/
  PROCEDURE usp_updatejobinfo(v_id    VARCHAR2,
                              v_title VARCHAR2,
                              v_memo  VARCHAR2);

  /*���²��˻����ĵ�*/
  procedure usp_UpdateNursRecordTable(v_ID        numeric, --��¼ID
                                      v_NoOfInpat numeric, --��ҳ���(סԺ��ˮ��)
                                      v_Content   blob --������
                                      );

  /*���»����ĵ�ģ��*/
  procedure usp_UpdateNursTableTemplate(v_ID       numeric, --ģ�����
                                        v_Name     varchar, --ģ������
                                        v_Describe varchar, --ģ������
                                        v_Content  blob, --ģ������
                                        v_version  varchar, --ģ��汾(�Զ�������,Ĭ��Ϊ1.00.00,�������������1λ)
                                        v_SortID   varchar --ģ��������
                                        );

  /*���µ��ڵĽ��Ĳ���*/
  PROCEDURE usp_updatedueapplyrecord(v_applydoctor VARCHAR2 --����ҽʦ����
                                     );

  /*���¹�������ҳ����TABLE*/
  PROCEDURE usp_update_iem_mainpage_oper(v_iem_mainpage_no NUMERIC,
                                         v_cancel_user     VARCHAR);

  /*���²�����ҳ���TABLE,�ڲ���֮ǰ����*/
  PROCEDURE usp_update_iem_mainpage_diag(v_iem_mainpage_no NUMERIC,
                                         v_cancel_user     VARCHAR2);

  /*���¹�������ҳ������ϢTABLE*/
  PROCEDURE usp_update_iem_mainpage_basic(v_iem_mainpage_no          NUMERIC,
                                          v_patnoofhis               NUMERIC,
                                          v_noofinpat                NUMERIC,
                                          v_payid                    VARCHAR,
                                          v_socialcare               VARCHAR,
                                          v_incount                  INT,
                                          v_name                     VARCHAR,
                                          v_sexid                    VARCHAR,
                                          v_birth                    CHAR,
                                          v_marital                  VARCHAR,
                                          v_jobid                    VARCHAR,
                                          v_provinceid               VARCHAR,
                                          v_countyid                 VARCHAR,
                                          v_nationid                 VARCHAR,
                                          v_nationalityid            VARCHAR,
                                          v_idno                     VARCHAR,
                                          v_organization             VARCHAR,
                                          v_officeplace              VARCHAR,
                                          v_officetel                VARCHAR,
                                          v_officepost               VARCHAR,
                                          v_nativeaddress            VARCHAR,
                                          v_nativetel                VARCHAR,
                                          v_nativepost               VARCHAR,
                                          v_contactperson            VARCHAR,
                                          v_relationship             VARCHAR,
                                          v_contactaddress           VARCHAR,
                                          v_contacttel               VARCHAR,
                                          v_admitdate                VARCHAR,
                                          v_admitdept                VARCHAR,
                                          v_admitward                VARCHAR,
                                          v_days_before              NUMERIC,
                                          v_trans_date               VARCHAR,
                                          v_trans_admitdept          VARCHAR,
                                          v_trans_admitward          VARCHAR,
                                          v_trans_admitdept_again    VARCHAR,
                                          v_outwarddate              VARCHAR,
                                          v_outhosdept               VARCHAR,
                                          v_outhosward               VARCHAR,
                                          v_actual_days              NUMERIC,
                                          v_death_time               VARCHAR,
                                          v_death_reason             VARCHAR,
                                          v_admitinfo                VARCHAR,
                                          v_in_check_date            VARCHAR,
                                          v_pathology_diagnosis_name VARCHAR,
                                          v_pathology_observation_sn VARCHAR,
                                          v_ashes_diagnosis_name     VARCHAR,
                                          v_ashes_anatomise_sn       VARCHAR,
                                          v_allergic_drug            VARCHAR,
                                          v_hbsag                    NUMERIC,
                                          v_hcv_ab                   NUMERIC,
                                          v_hiv_ab                   NUMERIC,
                                          v_opd_ipd_id               NUMERIC,
                                          v_in_out_inpatinet_id      NUMERIC,
                                          v_before_after_or_id       NUMERIC,
                                          v_clinical_pathology_id    NUMERIC,
                                          v_pacs_pathology_id        NUMERIC,
                                          v_save_times               NUMERIC,
                                          v_success_times            NUMERIC,
                                          v_section_director         VARCHAR,
                                          v_director                 VARCHAR,
                                          v_vs_employee_code         VARCHAR,
                                          v_resident_employee_code   VARCHAR,
                                          v_refresh_employee_code    VARCHAR,
                                          v_master_interne           VARCHAR,
                                          v_interne                  VARCHAR,
                                          v_coding_user              VARCHAR,
                                          v_medical_quality_id       NUMERIC,
                                          v_quality_control_doctor   VARCHAR,
                                          v_quality_control_nurse    VARCHAR,
                                          v_quality_control_date     VARCHAR,
                                          v_xay_sn                   VARCHAR,
                                          v_ct_sn                    VARCHAR,
                                          v_mri_sn                   VARCHAR,
                                          v_dsa_sn                   VARCHAR,
                                          v_is_first_case            NUMERIC,
                                          v_is_following             NUMERIC,
                                          v_following_ending_date    VARCHAR,
                                          v_is_teaching_case         NUMERIC,
                                          v_blood_type_id            NUMERIC,
                                          v_rh                       NUMERIC,
                                          v_blood_reaction_id        NUMERIC,
                                          v_blood_rbc                NUMERIC,
                                          v_blood_plt                NUMERIC,
                                          v_blood_plasma             NUMERIC,
                                          v_blood_wb                 NUMERIC,
                                          v_blood_others             VARCHAR,
                                          v_is_completed             VARCHAR,
                                          v_completed_time           VARCHAR,
                                          v_modified_user            VARCHAR);

  /*���²��˾�����Ϣ*/
  PROCEDURE usp_updatadiacrisisinfo(v_noofinpat      NUMERIC, --��ҳ���(סԺ��ˮ��)(Inpatient.NoOfInpat)
                                    v_admitdiagnosis VARCHAR2 --��Ժ���
                                    );

  /*���²��˻�����Ϣ*/
  PROCEDURE usp_updatabasepatientinfo(v_noofinpat     NUMERIC, --��ҳ���(סԺ��ˮ��)
                                      v_birth         VARCHAR, --��������
                                      v_marital       VARCHAR, --����״��
                                      v_nationid      VARCHAR, --�������
                                      v_nationalityid VARCHAR, --��������
                                      v_sexid         VARCHAR, --�����Ա�
                                      v_edu           VARCHAR, --�Ļ��̶�
                                      v_provinceid    VARCHAR, --(������)ʡ�д���
                                      v_countyid      VARCHAR, --(������)���ش���
                                      v_nativeplace_p VARCHAR, --����ʡ�д���
                                      v_nativeplace_c VARCHAR, --�������ش���
                                      v_payid         VARCHAR, --��������
                                      v_age           INT, --��������
                                      v_religion      VARCHAR, --�ڽ�����
                                      v_educ          NUMERIC, --(��)��������(��λ:��)
                                      v_idno          VARCHAR, --���֤��
                                      v_jobid         VARCHAR, --ְҵ����
                                      v_organization  VARCHAR, --������λ(��ȱ)
                                      v_officeplace   VARCHAR, --��λ��ַ
                                      v_officepost    VARCHAR, --��λ�ʱ�
                                      v_officetel     VARCHAR, --��λ�绰
                                      v_nativeaddress VARCHAR, --���ڵ�ַ
                                      v_nativetel     VARCHAR, --���ڵ绰
                                      v_nativepost    VARCHAR, --�����ʱ�
                                      v_address       VARCHAR --��ǰ��ַ
                                      );

  procedure usp_SymbolManager(v_type               varchar,
                              v_SymbolCategroyID   int default 0, --�����ַ���������
                              v_SymbolCategoryName varchar default '', --�����ַ���������
                              v_SymbolCategoryMemo varchar default '', --�����ַ����ͱ�ע
                              
                              --  v_SymbolRTF  varchar default '',--�����ַ�����ID(Inpatient.NoOfInpat)
                              --  v_SymbolCategroyID int  default 0,  --�����ַ�����
                              --  v_SymbolLength int  default  0,       --�����ַ�����
                              --  v_SymbolMemo varchar default  '' --�����ַ���ע
                              o_result OUT empcurtyp);

  /*���������Ա����Ϣ*/
  PROCEDURE usp_selectusers(v_DeptId   VARCHAR,
                            v_UserName VARCHAR,
                            o_result   OUT empcurtyp);

  /*�������Ա����Ϣ�������ʾ���Ӧ���ƣ�*/
  procedure usp_selectuserinfo(o_result out empcurtyp);

  /*������в�����Ϣ*/
  PROCEDURE usp_selectward(o_result OUT empcurtyp);

  /*������и�λ��Ϣ*/
  PROCEDURE usp_selectpermission(o_result OUT empcurtyp);

  /*���ĳ��Ա����Ϣ*/
  PROCEDURE usp_selectjob(v_id VARCHAR, o_result OUT empcurtyp);

  /*������в�����Ϣ*/
  PROCEDURE usp_selectdepartment(o_result OUT empcurtyp);

  /*�������Ա����Ϣ*/
  PROCEDURE usp_selectallusers2(o_result  OUT empcurtyp,
                                o_result1 OUT empcurtyp);

  /*�������Ա����Ϣ*/
  PROCEDURE usp_selectallusers(o_result OUT empcurtyp);

  /*������и�λ��Ϣ*/
  PROCEDURE usp_selectalljobs(o_result OUT empcurtyp);

  /*����������ĵ��Ӳ�����Ϣ*/
  PROCEDURE usp_saveapplyrecord(v_noofinpat   NUMERIC, --��ҳ���(סԺ��ˮ��)
                                v_applydoctor VARCHAR, --����ҽ������
                                v_deptid      VARCHAR, --���Ҵ���
                                v_applyaim    VARCHAR, --����Ŀ��
                                v_duetime     NUMERIC, --��������
                                v_unit        VARCHAR --���޵�λ
                                );

  /*��ȡ������Ϣά������ؼ���ʼ������*/
  PROCEDURE usp_redactpatientinfofrm(v_frmtype VARCHAR,
                                     --�������ͣ�1n��������Ϣ����ά�����塢2n�����˲���ʷ��Ϣ����
                                     v_noofinpat  NUMERIC DEFAULT '0', --��ҳ���
                                     v_categoryid VARCHAR DEFAULT '',
                                     --(�ֵ�)������ ���򸸽ڵ���� ����ҳ���
                                     o_result OUT empcurtyp);

  /*��ȡ��������ͳ������(�в�ѯ����)*/
  PROCEDURE usp_queryqcstatinfo(v_datetimebegin VARCHAR, --��ʼʱ��
                                v_datetimeend   VARCHAR, --����ʱ��
                                o_result        OUT empcurtyp);

  /*��ȡ��������ͳ������*/
  PROCEDURE usp_queryqcstatdetailinfo(v_datetimebegin VARCHAR, --��ʼʱ��
                                      v_datetimeend   VARCHAR, --����ʱ��
                                      v_qcstattype    INT, --ͳ����������
                                      o_result        OUT empcurtyp);

  /*��ȡ����Ǽ�����*/
  PROCEDURE usp_queryqcprobleminfo(v_noofinpat INT, o_result OUT empcurtyp);

  /*��ȡ�������Ʋ�������*/
  PROCEDURE usp_queryqcpatientinfo(v_datetimefrom VARCHAR,
                                   v_datetimeto   VARCHAR,
                                   v_deptid       VARCHAR,
                                   v_wardid       VARCHAR,
                                   v_bedid        VARCHAR,
                                   v_name         VARCHAR,
                                   v_archives     VARCHAR,
                                   o_result       OUT empcurtyp);

  /*������ҳ��Ż�ȡ���˶�Ӧ��Ϣ*/
  PROCEDURE usp_querypatientinfobynoofinp(v_noofinpat INT,
                                          o_result    OUT empcurtyp);

  /*��ȡ������ȡ���没���б�*/
  PROCEDURE usp_queryownmanagerpat2(v_querytype INT,
                                    v_userid    VARCHAR,
                                    v_deptid    VARCHAR,
                                    v_wardid    VARCHAR,
                                    o_result    OUT empcurtyp);

  /*��ȡ��Ӧ�����Ĵ�λ��Ϣ*/
  PROCEDURE usp_querynonarchivepatients(v_wardid  VARCHAR,
                                        v_deptids VARCHAR,
                                        o_result  OUT empcurtyp);

  /*�ʿ�TypeScore����*/
  PROCEDURE usp_qctypescore(v_typename        VARCHAR default '',
                            v_typeinstruction VARCHAR default '',
                            v_typecategory    INT default 0,
                            v_typeorder       INT default 0,
                            v_typememo        VARCHAR default '',
                            v_typestatus      INT,
                            v_typecode        VARCHAR);

  /*��ȡ������������*/
  PROCEDURE usp_qcoperprobleminfo(v_id             INT,
                                  v_noofinpat      INT,
                                  v_category       INT,
                                  v_status         INT,
                                  v_typecode       VARCHAR,
                                  v_description    VARCHAR,
                                  v_ansewercontent VARCHAR,
                                  v_confirmcontent VARCHAR,
                                  v_problemdate    VARCHAR,
                                  v_registerdate   VARCHAR,
                                  v_registeruser   VARCHAR,
                                  v_answerdate     VARCHAR,
                                  v_answeruser     VARCHAR,
                                  v_confirmdate    VARCHAR,
                                  v_confirmuser    VARCHAR,
                                  v_deldate        VARCHAR,
                                  v_deluser        VARCHAR,
                                  v_operstatus     INT);

  /*��ȡ��������ͳ������*/
  PROCEDURE usp_qcitemscore(v_itemname           VARCHAR,
                            v_iteminstruction    VARCHAR,
                            v_itemdefaultscore   INT,
                            v_itemstandardscore  INT,
                            v_itemcategory       INT,
                            v_itemdefaulttarget  INT,
                            v_itemtargetstandard INT,
                            v_itemscorestandard  INT,
                            v_itemorder          INT,
                            v_typecode           VARCHAR,
                            v_itemmemo           VARCHAR,
                            v_typestatus         INT,
                            v_itemcode           VARCHAR);

  /*��ȡ����ģ��ѡ����������*/
  PROCEDURE usp_msquerytemplate(v_id         VARCHAR,
                                v_user       VARCHAR,
                                v_type       INT,
                                v_department VARCHAR DEFAULT '',
                                o_result     OUT empcurtyp);

  /*
  * �����û�����
  */
  PROCEDURE usp_updateuserpassword(v_id      IN users.ID%TYPE,
                                   v_passwd  IN users.passwd%TYPE,
                                   v_regdate IN users.regdate%TYPE);

  /*
  * �õ��û��ʻ���Ϣ
  */
  PROCEDURE usp_getuseraccount(v_id     IN users.ID%TYPE,
                               o_result OUT empcurtyp);

  /*
  * ȡְ�����Ҷ�Ӧ����, ��δָ����������ͨ��ָ���Ŀ��ҹ��������еĲ���
  */
  PROCEDURE usp_getuserdeptandward(v_userid IN users.ID%TYPE,
                                   o_result OUT empcurtyp);

  /*
  *
  */
  PROCEDURE usp_getuseroutdeptandward(o_result OUT empcurtyp);

  /**ʹ�õ���ʱ��Ĵ洢����**/

  /*
  *��ȡְ�������Ϣ
  */
  PROCEDURE usp_GetUserInfo(v_userid  varchar,
                            o_result  OUT empcurtyp,
                            o_result1 OUT empcurtyp);

  /*
  *��ȡְ�������Ϣ
  */
  procedure usp_NursRecordOperate(v_ID        numeric default 0, --��¼ID
                                  v_NoOfInpat numeric default 0, --��ҳ���(סԺ��ˮ��)
                                  v_SortID    numeric default 0, --ģ��������
                                  v_Type      int, --��������
                                  o_result    OUT empcurtyp);
  /*
  *ҽ������ͳ�Ʒ���
  */
  PROCEDURE usp_MedQCAnalysis(v_DateTimeBegin varchar, --��ѯ��ʼ����
                              v_DateTimeEnd   varchar, --��ѯ��������
                              o_result        OUT empcurtyp);

  /*
  *��ȡ��Ӧ�����Ĵ�λ��Ϣ(����һ��ʱ���ã�
  */
  PROCEDURE usp_QueryBrowserInwardPatients(v_ID        varchar,
                                           v_QueryType int,
                                           v_wardid    varchar,
                                           v_Deptids   varchar,
                                           v_zyhm      varchar default '',
                                           v_hzxm      varchar default '',
                                           v_cyzd      varchar default '',
                                           v_ryrqbegin varchar default '',
                                           v_ryrqend   varchar default '',
                                           v_cyrqbegin varchar default '',
                                           v_cyrqend   varchar default '',
                                           o_result    OUT empcurtyp);
  /*
  *��ȡ��Ӧ�����Ĵ�λ��Ϣ
  */
  PROCEDURE usp_QueryHistoryPatients(v_wardid          varchar,
                                     v_deptids         varchar,
                                     v_PatID           varchar default '',
                                     v_PatName         varchar default '',
                                     v_OutDiagnosis    varchar default '',
                                     v_AdmitDatebegin  varchar default '',
                                     v_AdmitDatend     varchar default '',
                                     v_OutHosDatebegin varchar default '',
                                     v_OutHosDatend    varchar default '',
                                     o_result          OUT empcurtyp,
                                     o_result1         OUT empcurtyp);

  /*
  *��ȡ��Ӧ�����Ĵ�λ��Ϣ
  */
  PROCEDURE usp_QueryInwardPatients_old(v_wardid    varchar,
                                        v_Deptids   varchar,
                                        v_zyhm      varchar default '',
                                        v_hzxm      varchar default '',
                                        v_cyzd      varchar default '',
                                        v_ryrqbegin varchar default '',
                                        v_ryrqend   varchar default '',
                                        v_cyrqbegin varchar default '',
                                        v_cyrqend   varchar default '',
                                        --add by xjt
                                        v_ID        varchar default '',
                                        v_QueryType int default 0,
                                        v_QueryNur  varchar default '',
                                        v_QueryBed  varchar default 'Y',
                                        o_result    OUT empcurtyp);

  /*
    ��ȡ��Ӧ�����Ĵ�λ��Ϣ
  bwj 20121108 ���usp_QueryInwardPatients_old��ҽ������վ������ʱ�����Դ����������
  ͨ����ͼ
  */
  PROCEDURE usp_queryinwardpatients(v_wardid    VARCHAR,
                                    v_deptids   VARCHAR,
                                    v_zyhm      VARCHAR DEFAULT '',
                                    v_hzxm      VARCHAR DEFAULT '',
                                    v_cyzd      VARCHAR DEFAULT '',
                                    v_ryrqbegin VARCHAR DEFAULT '',
                                    v_ryrqend   VARCHAR DEFAULT '',
                                    v_cyrqbegin VARCHAR DEFAULT '',
                                    v_cyrqend   VARCHAR DEFAULT '',
                                    --add by xjt
                                    v_id        VARCHAR DEFAULT '',
                                    v_querytype INT DEFAULT 0,
                                    v_querynur  VARCHAR DEFAULT '',
                                    v_querybed  VARCHAR DEFAULT 'Y',
                                    o_result    OUT empcurtyp);

  /*
  xuliangliang2012-12-17�������Ͽ⵼��
  �������ɽ���Ҳ����������� ��like���
  */
  PROCEDURE usp_queryinwardpatientsQLS(v_wardid    VARCHAR,
                                       v_deptids   VARCHAR,
                                       v_zyhm      VARCHAR DEFAULT '',
                                       v_hzxm      VARCHAR DEFAULT '',
                                       v_cyzd      VARCHAR DEFAULT '',
                                       v_ryrqbegin VARCHAR DEFAULT '',
                                       v_ryrqend   VARCHAR DEFAULT '',
                                       v_cyrqbegin VARCHAR DEFAULT '',
                                       v_cyrqend   VARCHAR DEFAULT '',
                                       --add by xjt
                                       v_id        VARCHAR DEFAULT '',
                                       v_querytype INT DEFAULT 0,
                                       v_querynur  VARCHAR DEFAULT '',
                                       v_querybed  VARCHAR DEFAULT 'Y',
                                       o_result    OUT empcurtyp);

  /*
  *��ȡ��Ӧ�����Ĵ�λ��Ϣ
  */
  PROCEDURE usp_QueryInwardPatients2(v_wardid    varchar,
                                     v_Deptids   varchar,
                                     v_zyhm      varchar default '',
                                     v_hzxm      varchar default '',
                                     v_cyzd      varchar default '',
                                     v_ryrqbegin varchar default '',
                                     v_ryrqend   varchar default '',
                                     v_cyrqbegin varchar default '',
                                     v_cyrqend   varchar default '',
                                     --add by xjt
                                     v_ID        varchar default '',
                                     v_QueryType int default 0,
                                     v_QueryNur  varchar default '',
                                     v_QueryBed  varchar default 'Y',
                                     o_result    OUT empcurtyp);

  /*
  *��ȡ������ȡ���没���б�
  */
  PROCEDURE usp_QueryOwnManagerPat(v_QueryType int,
                                   v_UserID    varchar,
                                   v_DeptID    varchar,
                                   v_WardId    varchar,
                                   o_result    OUT empcurtyp);
  /*
  *��ȡ��������
  */
  PROCEDURE usp_QueryQCGrade(v_NoOfInpat int,
                             v_OperUser  varchar,
                             o_result    OUT empcurtyp);

  /*
  *��ȡ��Ӧ�����ĳ�Ժ����
  */
  PROCEDURE usp_QueryQuitPatientNoDoctor(v_wardid    varchar,
                                         v_Deptids   varchar,
                                         v_TimeFrom  varchar,
                                         v_TimeTo    varchar,
                                         v_PatientSN varchar default '', --������
                                         v_Name      varchar default '', --��������
                                         v_QueryType int default 0,
                                         o_result    OUT empcurtyp);

  /***************************************************************************/

  /*
  *���㲡������
  */
  PROCEDURE usp_Emr_CalcAge(v_csrq varchar,
                            v_dqrq varchar,
                            v_sjnl out int,
                            v_xsnl out varchar);
  /*
  *���㲡������
  */
  procedure usp_EMR_GetAge(v_csrq varchar,
                           v_dqrq varchar,
                           v_sjnl out int,
                           v_xsnl out varchar);

  --ҽʦ����վ�õ���Ժ����
  PROCEDURE usp_queryinwardpatientsout(v_wardid    VARCHAR,
                                       v_deptids   VARCHAR,
                                       v_id        VARCHAR DEFAULT '',
                                       v_querytype INT DEFAULT 0,
                                       o_result    OUT empcurtyp);

  ----ҽ������վ�л�û�����Ϣ��ȫ�����������ģ�
  PROCEDURE usp_GetMyConsultion(v_Deptids varchar, --���ű��
                                V_userid  varchar, --��¼�˱��
                                o_result  OUT empcurtyp);

  -----------------------------Ϊ�����趨Ӥ���Ĵ洢����  -add by ywk 2012��6��7�� 09:47:34------------------------------
  /*���¹�������ҳ������ϢTABLE*/
  PROCEDURE usp_editBabyinfo(v_Noofinpat  NUMERIC,
                             v_patnoofhis VARCHAR,
                             v_Noofclinic VARCHAR,
                             v_Noofrecord VARCHAR,
                             v_patid      VARCHAR,
                             v_Innerpix   VARCHAR,
                             v_outpix     VARCHAR,
                             
                             v_Name            VARCHAR,
                             v_py              VARCHAR,
                             v_wb              VARCHAR,
                             v_payid           VARCHAR,
                             v_ORIGIN          VARCHAR,
                             v_InCount         int DEFAULT 0,
                             v_sexid           VARCHAR,
                             v_Birth           VARCHAR,
                             v_Age             int DEFAULT 0,
                             v_AgeStr          VARCHAR,
                             v_IDNO            VARCHAR,
                             v_Marital         VARCHAR,
                             v_JobID           VARCHAR,
                             v_CSDProvinceID   VARCHAR,
                             v_CSDCityID       VARCHAR,
                             v_CSDDistrictID   VARCHAR,
                             v_NationID        VARCHAR,
                             v_NationalityID   VARCHAR,
                             v_JGProvinceID    VARCHAR,
                             v_JGCityID        VARCHAR,
                             v_Organization    VARCHAR,
                             v_OfficePlace     VARCHAR,
                             v_OfficeTEL       VARCHAR,
                             v_OfficePost      VARCHAR,
                             v_HKDZProvinceID  VARCHAR,
                             v_HKDZCityID      VARCHAR,
                             v_HKDZDistrictID  VARCHAR,
                             v_NATIVEPOST      VARCHAR,
                             v_NATIVETEL       VARCHAR,
                             v_NATIVEADDRESS   VARCHAR,
                             v_ADDRESS         VARCHAR,
                             v_ContactPerson   VARCHAR,
                             v_RelationshipID  VARCHAR,
                             v_ContactAddress  VARCHAR,
                             v_ContactTEL      VARCHAR,
                             v_CONTACTOFFICE   VARCHAR,
                             v_CONTACTPOST     VARCHAR,
                             v_OFFERER         VARCHAR,
                             v_SocialCare      VARCHAR,
                             v_INSURANCE       VARCHAR,
                             v_CARDNO          VARCHAR,
                             v_ADMITINFO       VARCHAR,
                             v_AdmitDeptID     VARCHAR,
                             v_AdmitWardID     VARCHAR,
                             v_ADMITBED        VARCHAR,
                             v_AdmitDate       VARCHAR,
                             v_INWARDDATE      VARCHAR,
                             v_ADMITDIAGNOSIS  VARCHAR,
                             v_OutWardDate     VARCHAR,
                             v_OutHosDeptID    VARCHAR,
                             v_OutHosWardID    VARCHAR,
                             v_OutBed          VARCHAR,
                             v_OUTHOSDATE      VARCHAR,
                             v_OUTDIAGNOSIS    VARCHAR,
                             v_TOTALDAYS       int DEFAULT 0,
                             v_CLINICDIAGNOSIS VARCHAR,
                             v_SOLARTERMS      VARCHAR,
                             v_ADMITWAY        VARCHAR,
                             v_OUTWAY          VARCHAR,
                             v_CLINICDOCTOR    VARCHAR,
                             v_RESIDENT        VARCHAR,
                             v_ATTEND          VARCHAR,
                             v_CHIEF           VARCHAR,
                             v_EDU             VARCHAR,
                             v_EDUC            int DEFAULT 0,
                             v_RELIGION        VARCHAR,
                             v_STATUS          int DEFAULT 0,
                             v_CRITICALLEVEL   VARCHAR,
                             v_ATTENDLEVEL     VARCHAR,
                             v_EMPHASIS        int DEFAULT 0,
                             v_ISBABY          int DEFAULT 0,
                             v_MOTHER          NUMERIC,
                             v_MEDICAREID      VARCHAR,
                             v_MEDICAREQUOTA   int DEFAULT 0,
                             v_VOUCHERSCODE    VARCHAR,
                             v_STYLE           VARCHAR,
                             v_OPERATOR        VARCHAR,
                             v_MEMO            VARCHAR,
                             v_CPSTATUS        int DEFAULT 0,
                             v_OUTWARDBED      int DEFAULT 0,
                             v_XZZProvinceID   VARCHAR,
                             v_XZZCityID       VARCHAR,
                             v_XZZDistrictID   VARCHAR,
                             v_XZZTEL          VARCHAR,
                             v_XZZPost         VARCHAR,
                             v_EditType        varchar);
  ----�ʿس�Ժδ�鵵������ѯ
  -----Add by xlb 2013-06-04
  PROCEDURE usp_GetOutHosButNotLocks(v_deptId    VARCHAR2, ---���Ҵ���
                                     v_sex       VARCHAR2, --�Ա�
                                     v_dateBegin VARCHAR2, --��Ժ��ʼʱ��
                                     v_dateEnd   VARCHAR2, --��Ժ����ʱ��
                                     v_patName   VARCHAR2, ----��������
                                     v_PatId     VARCHAR2,
                                     o_result    OUT empcurtyp);
END;
/
CREATE OR REPLACE PACKAGE BODY emrproc IS
  /*******************************************************************/
  PROCEDURE usp_emr_patrecfile(v_flag             INTEGER,
                               v_indx             INTEGER,
                               v_noofinpat        INTEGER,
                               v_templateid       VARCHAR2,
                               v_filename         VARCHAR2,
                               v_filecontent      CLOB,
                               v_sortid           VARCHAR2,
                               v_owner            VARCHAR2,
                               v_auditor          VARCHAR2,
                               v_createtime       CHAR,
                               v_audittime        CHAR,
                               v_valid            INTEGER,
                               v_hassubmit        INTEGER,
                               v_hasprint         INTEGER,
                               v_hassign          INTEGER,
                               v_captiondatetime  varchar2,
                               v_FIRSTDAILYFLAG   varchar2,
                               v_py               varchar2,
                               v_wb               varchar2,
                               v_isyihuangoutong  varchar2,
                               v_isconfigpagesize varchar2,
                               v_DepartCode       varchar2,
                               v_wardCode         varchar2,
                               o_result           OUT empcurtyp) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]
    [����]
    [��Ȩ]
    [����] ���»���벡���ļ�
    [����˵��]
    [�������]
      @NoOfInpat varchar(40)--��ҳ���
    [�������]
    [�����������]
    ��������ͳ�����ݼ�
    
    [���õ�sp]
    [����ʵ��]
    EXEC usp_Emr_PatRecFile 1,2,'T','FNAME','CONTENT','SORTID','OWN','AUDIT','CRAETE','AUDIT',1,1,1,1
    [�޸ļ�¼]
    **********/
    p_count integer default 0;
  BEGIN
    IF v_flag = 1 --����
     THEN
      --SELECT NVL(MAX(ID), 0) + 1 into p_count FROM recorddetail;
      select seq_recorddetail_id.nextval into p_count from dual;
      INSERT INTO recorddetail
        (ID,
         noofinpat,
         templateid,
         NAME,
         content,
         sortid,
         owner,
         auditor,
         createtime,
         audittime,
         valid,
         hassubmit,
         hasprint,
         hassign,
         captiondatetime,
         FIRSTDAILYFLAG,
         islock,
         isyihuangoutong,
         isconfigpagesize,
         departcode,
         wardcode)
      VALUES
        (p_count,
         v_noofinpat,
         v_templateid,
         v_filename,
         v_filecontent,
         v_sortid,
         v_owner,
         v_auditor,
         v_createtime,
         v_audittime,
         v_valid,
         v_hassubmit,
         v_hasprint,
         v_hassign,
         v_captiondatetime,
         v_FIRSTDAILYFLAG,
         '4700',
         v_isyihuangoutong,
         v_isconfigpagesize,
         v_DepartCode,
         v_wardCode);
    
    ELSIF v_flag = 2 THEN
      UPDATE recorddetail
         SET --content   = v_filecontent,
                    auditor = v_auditor,
             audittime       = v_audittime,
             valid           = v_valid,
             hassubmit       = v_hassubmit,
             hasprint        = v_hasprint,
             hassign         = v_hassign,
             NAME            = v_filename,
             captiondatetime = v_captiondatetime
       WHERE ID = v_indx;
    END IF;
  
    OPEN o_result FOR
      select p_count from dual;
    /*
    IF v_sortid = 'AC' AND v_FIRSTDAILYFLAG = 0 THEN --��֤���̼�¼�����ύ
    
        --IF p_count > 0 THEN
        UPDATE recorddetail
        SET recorddetail.hassubmit =
        (
            SELECT hassubmit FROM recorddetail
            WHERE recorddetail.noofinpat
                IN ( SELECT noofinpat FROM recorddetail WHERE id = v_indx )
                AND sortid = 'AC' AND firstdailyflag = 1 AND valid = 1 AND rownum = 1
        )
        WHERE recorddetail.id
        IN
        (
            SELECT id FROM recorddetail
            WHERE recorddetail.noofinpat
                IN ( SELECT noofinpat FROM recorddetail WHERE id = v_indx )
                AND sortid = 'AC' AND firstdailyflag = 0 AND valid = 1
        );
        --END IF;
    END IF;
    */
  END;

  PROCEDURE usp_auditapplyrecord(v_audittype VARCHAR, --�������
                                 v_manid     VARCHAR, --�����ID
                                 v_id        NUMERIC --��¼ID
                                 ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ����������Ĳ���
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_audittype = '5206' THEN
      --ǩ��
      UPDATE applyrecord
         SET singindate = TO_CHAR(SYSDATE, 'yyyy-mm-dd hh24:mi:ss'),
             singinman  = v_manid,
             status     = v_audittype
       WHERE ID = v_id;
    ELSE
      --���
      UPDATE applyrecord
         SET auditdate = TO_CHAR(SYSDATE, 'yyyy-mm-dd hh24:mi:ss'),
             auditman  = v_manid,
             status    = v_audittype
       WHERE ID = v_id;
    END IF;
  END;

  /***********************************************************************************/
  PROCEDURE usp_deletejob(v_id VARCHAR) AS
    /**********
     �汾��
     ����ʱ��
     ����
     ��Ȩ
     ����
     ����˵��      ɾ��ѡ����λ
     �������
     �����������
     ���õ�sp
     ����ʵ��
    **********/
  BEGIN
    UPDATE users
       SET jobid = REPLACE(jobid, v_id || ',', '')
     WHERE ID IN (SELECT ID FROM users WHERE jobid LIKE '%v_ID%');
  
    DELETE FROM job2permission WHERE ID = v_id;
  
    DELETE FROM jobs WHERE ID = v_id;
  END;

  /********************************************************************************/
  PROCEDURE usp_deletejobpermission(v_id VARCHAR) AS
    /**********
     �汾��
     ����ʱ��
     ����
     ��Ȩ
     ����
     ����˵��      ɾ��Ȩ�޿�����Ϣ
     �������
     �����������
     ���õ�sp
     ����ʵ��
    **********/
  BEGIN
    DELETE job2permission WHERE ID = v_id;
  END;

  /*********************************************************************************/
  PROCEDURE usp_deleteuserinfo(v_id VARCHAR) AS
    /**********
     �汾��
     ����ʱ��
     ����
     ��Ȩ
     ����
     ����˵��      ɾ��ĳ��Ա����Ϣ
     �������
     �����������
     ���õ�sp
     ����ʵ��
    **********/
  BEGIN
    DELETE users WHERE ID = v_id;
  END;

  /*********************************************************/
  PROCEDURE usp_deptemmanager(v_dept VARCHAR, o_result OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ����ģ��ѡ����������
     ����˵��
     �������
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_DepTemManager   3202
     �޸ļ�¼
    **********/
    v_sql VARCHAR2(4000) DEFAULT '';
  BEGIN
    droptable('tmp_templet');
    v_sql := 'create table tmp_templet as
    select b.Name,
           b.ID,
           a.ID templetID,
           a.Name templetName,
           a.Describe,
           b.PrevID as PrevID,
           nvl(F_DepTemManager(0, b.ID, nvl(b.PrevID, '''')), '''') AS groupname,
           nvl(F_DepTemManager(1, b.ID, nvl(b.PrevID, '''')), '''') AS groupname2
      from Model_Docment a
      left join ModelDirectory b on a.SortID = b.ID
     order by b.ID, groupname';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := ' select a.*,
         b.Department,
         (case
           when b.Department is null then
            ''0''
           else
            ''1''
         end) as checked
    from tmp_templet a
    left join (select t.* from Template_Department t where t.Department = ''' ||
             v_dept || ''') b on a.templetID = b.TemplateID';
  
    OPEN o_result FOR v_sql;
  END;

  /*****************************************************************************/
  PROCEDURE usp_EditEmrLONG_ORDER(v_EditType         varchar default '', --��������
                                  v_LONGID           INTEGER default '',
                                  v_NOOFINPAT        INTEGER default '',
                                  v_GROUPID          INTEGER default '',
                                  v_GROUPFLAG        INTEGER default '',
                                  v_WARDID           VARCHAR2 default '',
                                  v_DEPTID           VARCHAR2 default '',
                                  v_TYPEDOCTOR       VARCHAR2 default '',
                                  v_TYPEDATE         CHAR default '',
                                  v_AUDITOR          VARCHAR2 default '',
                                  v_DATEOFAUDIT      CHAR default '',
                                  v_EXECUTOR         VARCHAR2 default '',
                                  v_EXECUTEDATE      CHAR default '',
                                  v_CANCELDOCTOR     VARCHAR2 default '',
                                  v_CANCELDATE       CHAR default '',
                                  v_CEASEDCOCTOR     VARCHAR2 default '',
                                  v_CEASEDATE        CHAR default '',
                                  v_CEASENURSE       VARCHAR2 default '',
                                  v_CEASEADUDITDATE  CHAR default '',
                                  v_STARTDATE        CHAR default '',
                                  v_TOMORROW         INTEGER default '',
                                  v_PRODUCTNO        INTEGER default '',
                                  v_NORMNO           INTEGER default '',
                                  v_MEDICINENO       INTEGER default '',
                                  v_DRUGNO           VARCHAR2 default '',
                                  v_DRUGNAME         VARCHAR2 default '',
                                  v_DRUGNORM         VARCHAR2 default '',
                                  v_ITEMTYPE         INTEGER default '',
                                  v_MINUNIT          VARCHAR2 default '',
                                  v_DRUGDOSE         INTEGER default '',
                                  v_DOSEUNIT         VARCHAR2 default '',
                                  v_UNITRATE         INTEGER default '',
                                  v_UNITTYPE         INTEGER default '',
                                  v_DRUGUSE          VARCHAR2 default '',
                                  v_BATCHNO          VARCHAR2 default '',
                                  v_EXECUTECOUNT     INTEGER default '',
                                  v_EXECUTECYCLE     INTEGER default '',
                                  v_CYCLEUNIT        INTEGER default '',
                                  v_DATEOFWEEK       CHAR default '',
                                  v_INNEREXECUTETIME VARCHAR2 default '',
                                  v_EXECUTEDEPT      VARCHAR2 default '',
                                  v_ENTRUST          VARCHAR2 default '',
                                  v_ORDERTYPE        INTEGER default '',
                                  v_ORDERSTATUS      INTEGER default '',
                                  v_SPECIALMARK      INTEGER default '',
                                  v_CEASEREASON      INTEGER default '',
                                  v_CURGERYID        INTEGER default '',
                                  v_CONTENT          VARCHAR2 default '',
                                  v_SYNCH            INTEGER default '',
                                  v_MEMO             VARCHAR2 default '',
                                  v_DJFL             VARCHAR2 default '',
                                  o_result           OUT empcurtyp) as
  begin
    --���
    open o_result for
      select '' from dual;
    if v_EditType = '1' then
      insert into LONG_ORDER
      
        (LONGID,
         NOOFINPAT,
         GROUPID,
         GROUPFLAG,
         WARDID,
         DEPTID,
         TYPEDOCTOR,
         TYPEDATE,
         AUDITOR,
         DATEOFAUDIT,
         EXECUTOR,
         EXECUTEDATE,
         CANCELDOCTOR,
         CANCELDATE,
         CEASEDCOCTOR,
         CEASEDATE,
         CEASENURSE,
         CEASEADUDITDATE,
         STARTDATE,
         TOMORROW,
         PRODUCTNO,
         NORMNO,
         MEDICINENO,
         DRUGNO,
         DRUGNAME,
         DRUGNORM,
         ITEMTYPE,
         MINUNIT,
         DRUGDOSE,
         DOSEUNIT,
         UNITRATE,
         UNITTYPE,
         DRUGUSE,
         BATCHNO,
         EXECUTECOUNT,
         EXECUTECYCLE,
         CYCLEUNIT,
         DATEOFWEEK,
         INNEREXECUTETIME,
         EXECUTEDEPT,
         ENTRUST,
         ORDERTYPE,
         ORDERSTATUS,
         SPECIALMARK,
         CEASEREASON,
         CURGERYID,
         CONTENT,
         SYNCH,
         MEMO,
         DJFL)
      values
        (seq_long_order_id.nextval,
         v_NOOFINPAT,
         v_GROUPID,
         v_GROUPFLAG,
         v_WARDID,
         v_DEPTID,
         v_TYPEDOCTOR,
         v_TYPEDATE, --ʱ��
         v_AUDITOR,
         v_DATEOFAUDIT,
         v_EXECUTOR,
         v_EXECUTEDATE, --ʱ��
         v_CANCELDOCTOR,
         v_CANCELDATE, --ʱ��
         v_CEASEDCOCTOR,
         v_CEASEDATE, --ʱ��
         v_CEASENURSE,
         v_CEASEADUDITDATE, --ʱ��
         v_STARTDATE, --ʱ��
         v_TOMORROW,
         v_PRODUCTNO,
         v_NORMNO,
         v_MEDICINENO,
         v_DRUGNO,
         v_DRUGNAME,
         v_DRUGNORM,
         v_ITEMTYPE,
         v_MINUNIT,
         v_DRUGDOSE,
         v_DOSEUNIT,
         v_UNITRATE,
         v_UNITTYPE,
         v_DRUGUSE,
         v_BATCHNO,
         v_EXECUTECOUNT,
         v_EXECUTECYCLE,
         v_CYCLEUNIT,
         v_DATEOFWEEK,
         v_INNEREXECUTETIME, --ʱ��
         v_EXECUTEDEPT,
         v_ENTRUST,
         v_ORDERTYPE,
         v_ORDERSTATUS,
         v_SPECIALMARK,
         v_CEASEREASON,
         v_CURGERYID,
         v_CONTENT,
         v_SYNCH,
         v_MEMO,
         v_DJFL);
      --�޸�
    elsif v_EditType = '2' then
      update LONG_ORDER
         set NOOFINPAT        = nvl(v_NOOFINPAT, NOOFINPAT),
             GROUPID          = nvl(v_GROUPID, GROUPID),
             GROUPFLAG        = nvl(v_GROUPFLAG, GROUPFLAG),
             WARDID           = nvl(v_WARDID, WARDID),
             DEPTID           = nvl(v_DEPTID, DEPTID),
             TYPEDOCTOR       = nvl(v_TYPEDOCTOR, TYPEDOCTOR),
             TYPEDATE         = nvl(v_TYPEDATE, TYPEDATE), --ʱ��
             AUDITOR          = nvl(v_AUDITOR, AUDITOR),
             DATEOFAUDIT      = nvl(v_DATEOFAUDIT, DATEOFAUDIT),
             EXECUTOR         = nvl(v_EXECUTOR, EXECUTOR),
             EXECUTEDATE      = nvl(v_EXECUTEDATE, EXECUTEDATE), --ʱ��
             CANCELDOCTOR     = nvl(v_CANCELDOCTOR, CANCELDOCTOR),
             CANCELDATE       = nvl(v_CANCELDATE, CANCELDATE), --ʱ��
             CEASEDCOCTOR     = nvl(v_CEASEDCOCTOR, CEASEDCOCTOR),
             CEASEDATE        = nvl(v_CEASEDATE, CEASEDATE), --ʱ��
             CEASENURSE       = nvl(v_CEASENURSE, CEASENURSE),
             CEASEADUDITDATE  = nvl(v_CEASEADUDITDATE, CEASEADUDITDATE), --ʱ��
             STARTDATE        = nvl(v_STARTDATE, STARTDATE), --ʱ��
             TOMORROW         = nvl(v_TOMORROW, TOMORROW),
             PRODUCTNO        = nvl(v_PRODUCTNO, PRODUCTNO),
             NORMNO           = nvl(v_NORMNO, NORMNO),
             MEDICINENO       = nvl(v_MEDICINENO, MEDICINENO),
             DRUGNO           = nvl(v_DRUGNO, DRUGNO),
             DRUGNAME         = nvl(v_DRUGNAME, DRUGNAME),
             DRUGNORM         = nvl(v_DRUGNORM, DRUGNORM),
             ITEMTYPE         = nvl(v_ITEMTYPE, ITEMTYPE),
             MINUNIT          = nvl(v_MINUNIT, MINUNIT),
             DRUGDOSE         = nvl(v_DRUGDOSE, DRUGDOSE),
             DOSEUNIT         = nvl(v_DOSEUNIT, DOSEUNIT),
             UNITRATE         = nvl(v_UNITRATE, UNITRATE),
             UNITTYPE         = nvl(v_UNITTYPE, UNITTYPE),
             DRUGUSE          = nvl(v_DRUGUSE, DRUGUSE),
             BATCHNO          = nvl(v_BATCHNO, BATCHNO),
             EXECUTECOUNT     = nvl(v_EXECUTECOUNT, EXECUTECOUNT),
             EXECUTECYCLE     = nvl(v_EXECUTECYCLE, EXECUTECYCLE),
             CYCLEUNIT        = nvl(v_CYCLEUNIT, CYCLEUNIT),
             DATEOFWEEK       = nvl(v_DATEOFWEEK, DATEOFWEEK),
             INNEREXECUTETIME = nvl(v_INNEREXECUTETIME, INNEREXECUTETIME), --ʱ��
             EXECUTEDEPT      = nvl(v_EXECUTEDEPT, EXECUTEDEPT),
             ENTRUST          = nvl(v_ENTRUST, ENTRUST),
             ORDERTYPE        = nvl(v_ORDERTYPE, ORDERTYPE),
             ORDERSTATUS      = nvl(v_ORDERSTATUS, ORDERSTATUS),
             SPECIALMARK      = nvl(v_SPECIALMARK, SPECIALMARK),
             CEASEREASON      = nvl(v_CEASEREASON, CEASEREASON),
             CURGERYID        = nvl(v_CURGERYID, CURGERYID),
             CONTENT          = nvl(v_CONTENT, CONTENT),
             SYNCH            = nvl(v_SYNCH, SYNCH),
             MEMO             = nvl(v_MEMO, MEMO),
             DJFL             = nvl(v_DJFL, DJFL)
       where LONGID = V_LONGID;
      --ɾ��
    elsif v_EditType = '3' then
      delete LONG_ORDER where LONGID = V_LONGID;
      --��ѯ
    elsif v_EditType = '4' then
      open o_result for
        select * from LONG_ORDER where LONGID = V_LONGID;
    
    end if;
  end;

  /*****************************************************************************/
  PROCEDURE usp_EditEmrTEMP_ORDER(v_EditType         VARCHAR2 default '', --��������
                                  v_TEMPID           INTEGER default '',
                                  v_NOOFINPAT        INTEGER default '',
                                  v_GROUPID          INTEGER default '',
                                  v_GROUPFLAG        INTEGER default '',
                                  v_WARDID           VARCHAR2 default '',
                                  v_DEPTID           VARCHAR2 default '',
                                  v_TYPEDOCTOR       VARCHAR2 default '',
                                  v_TYPEDATE         CHAR default '',
                                  v_AUDITOR          VARCHAR2 default '',
                                  v_DATEOFAUDIT      CHAR default '',
                                  v_EXECUTOR         VARCHAR2 default '',
                                  v_EXECUTEDATE      CHAR default '',
                                  v_CANCELDOCTOR     VARCHAR2 default '',
                                  v_CANCELDATE       CHAR default '',
                                  v_STARTDATE        CHAR default '',
                                  v_PRODUCTNO        INTEGER default '',
                                  v_NORMNO           INTEGER default '',
                                  v_MEDICINENO       INTEGER default '',
                                  v_DRUGNO           VARCHAR2 default '',
                                  v_DRUGNAME         VARCHAR2 default '',
                                  v_DRUGNORM         VARCHAR2 default '',
                                  v_ITEMTYPE         INTEGER default '',
                                  v_MINUNIT          VARCHAR2 default '',
                                  v_DRUGDOSE         INTEGER default '',
                                  v_DOSEUNIT         VARCHAR2 default '',
                                  v_UNITRATE         INTEGER default '',
                                  v_UNITTYPE         INTEGER default '',
                                  v_DRUGUSE          VARCHAR2 default '',
                                  v_BATCHNO          VARCHAR2 default '',
                                  v_EXECUTECOUNT     INTEGER default '',
                                  v_EXECUTECYCLE     INTEGER default '',
                                  v_CYCLEUNIT        INTEGER default '',
                                  v_DATEOFWEEK       CHAR default '',
                                  v_INNEREXECUTETIME VARCHAR2 default '',
                                  v_ZXTS             INTEGER default '',
                                  v_TOTALDOSE        INTEGER default '',
                                  v_ENTRUST          VARCHAR2 default '',
                                  v_ORDERTYPE        INTEGER default '',
                                  v_ORDERSTATUS      INTEGER default '',
                                  v_SPECIALMARK      INTEGER default '',
                                  v_CEASEID          INTEGER default '',
                                  v_CEASEDATE        CHAR default '',
                                  v_CONTENT          VARCHAR2 default '',
                                  v_SYNCH            INTEGER default '',
                                  v_MEMO             VARCHAR2 default '',
                                  v_FORMTYPE         VARCHAR2 default '',
                                  o_result           OUT empcurtyp) as
  begin
    --���
    open o_result for
      select '' from dual;
  
    if v_EditType = '1' then
      insert into TEMP_ORDER
        (TEMPID,
         NOOFINPAT,
         GROUPID,
         GROUPFLAG,
         WARDID,
         DEPTID,
         TYPEDOCTOR,
         TYPEDATE,
         AUDITOR,
         DATEOFAUDIT,
         EXECUTOR,
         EXECUTEDATE,
         CANCELDOCTOR,
         CANCELDATE,
         STARTDATE,
         PRODUCTNO,
         NORMNO,
         MEDICINENO,
         DRUGNO,
         DRUGNAME,
         DRUGNORM,
         ITEMTYPE,
         MINUNIT,
         DRUGDOSE,
         DOSEUNIT,
         UNITRATE,
         UNITTYPE,
         DRUGUSE,
         BATCHNO,
         EXECUTECOUNT,
         EXECUTECYCLE,
         CYCLEUNIT,
         DATEOFWEEK,
         INNEREXECUTETIME,
         ZXTS,
         TOTALDOSE,
         ENTRUST,
         ORDERTYPE,
         ORDERSTATUS,
         SPECIALMARK,
         CEASEID,
         CEASEDATE,
         CONTENT,
         SYNCH,
         MEMO,
         FORMTYPE)
      values
        (seq_temp_order_id.nextval,
         v_NOOFINPAT,
         v_GROUPID,
         v_GROUPFLAG,
         v_WARDID,
         v_DEPTID,
         v_TYPEDOCTOR,
         v_TYPEDATE, --ʱ��
         v_AUDITOR,
         v_DATEOFAUDIT,
         v_EXECUTOR,
         v_EXECUTEDATE, --ʱ��
         v_CANCELDOCTOR,
         v_CANCELDATE, --ʱ��
         v_STARTDATE, --ʱ��
         v_PRODUCTNO,
         v_NORMNO,
         v_MEDICINENO,
         v_DRUGNO,
         v_DRUGNAME,
         v_DRUGNORM,
         v_ITEMTYPE,
         v_MINUNIT,
         v_DRUGDOSE,
         v_DOSEUNIT,
         v_UNITRATE,
         v_UNITTYPE,
         v_DRUGUSE,
         v_BATCHNO,
         v_EXECUTECOUNT,
         v_EXECUTECYCLE,
         v_CYCLEUNIT,
         v_DATEOFWEEK,
         v_INNEREXECUTETIME, --ʱ��
         v_ZXTS,
         v_TOTALDOSE,
         v_ENTRUST,
         v_ORDERTYPE,
         v_ORDERSTATUS,
         v_SPECIALMARK,
         v_CEASEID,
         v_CEASEDATE, --ʱ��
         v_CONTENT,
         v_SYNCH,
         v_MEMO,
         v_FORMTYPE);
      --�޸�
    elsif v_EditType = '2' then
      update TEMP_ORDER
         set NOOFINPAT        = nvl(v_NOOFINPAT, NOOFINPAT),
             GROUPID          = nvl(v_GROUPID, GROUPID),
             GROUPFLAG        = nvl(v_GROUPFLAG, GROUPFLAG),
             WARDID           = nvl(v_WARDID, WARDID),
             DEPTID           = nvl(v_DEPTID, DEPTID),
             TYPEDOCTOR       = nvl(v_TYPEDOCTOR, TYPEDOCTOR),
             TYPEDATE         = nvl(v_TYPEDATE, TYPEDATE), --ʱ��
             AUDITOR          = nvl(v_AUDITOR, AUDITOR),
             DATEOFAUDIT      = nvl(v_DATEOFAUDIT, DATEOFAUDIT),
             EXECUTOR         = nvl(v_EXECUTOR, EXECUTOR),
             EXECUTEDATE      = nvl(v_EXECUTEDATE, EXECUTEDATE), --ʱ��
             CANCELDOCTOR     = nvl(v_CANCELDOCTOR, CANCELDOCTOR),
             CANCELDATE       = nvl(v_CANCELDATE, CANCELDATE), --ʱ��
             STARTDATE        = nvl(v_STARTDATE, STARTDATE), --ʱ��
             PRODUCTNO        = nvl(v_PRODUCTNO, PRODUCTNO),
             NORMNO           = nvl(v_NORMNO, NORMNO),
             MEDICINENO       = nvl(v_MEDICINENO, MEDICINENO),
             DRUGNO           = nvl(v_DRUGNO, DRUGNO),
             DRUGNAME         = nvl(v_DRUGNAME, DRUGNAME),
             DRUGNORM         = nvl(v_DRUGNORM, DRUGNORM),
             ITEMTYPE         = nvl(v_ITEMTYPE, ITEMTYPE),
             MINUNIT          = nvl(v_MINUNIT, MINUNIT),
             DRUGDOSE         = nvl(v_DRUGDOSE, DRUGDOSE),
             DOSEUNIT         = nvl(v_DOSEUNIT, DOSEUNIT),
             UNITRATE         = nvl(v_UNITRATE, UNITRATE),
             UNITTYPE         = nvl(v_UNITTYPE, UNITTYPE),
             DRUGUSE          = nvl(v_DRUGUSE, DRUGUSE),
             BATCHNO          = nvl(v_BATCHNO, BATCHNO),
             EXECUTECOUNT     = nvl(v_EXECUTECOUNT, EXECUTECOUNT),
             EXECUTECYCLE     = nvl(v_EXECUTECYCLE, EXECUTECYCLE),
             CYCLEUNIT        = nvl(v_CYCLEUNIT, CYCLEUNIT),
             DATEOFWEEK       = nvl(v_DATEOFWEEK, DATEOFWEEK),
             INNEREXECUTETIME = nvl(v_INNEREXECUTETIME, INNEREXECUTETIME), --ʱ��
             ZXTS             = nvl(v_ZXTS, ZXTS),
             TOTALDOSE        = nvl(v_TOTALDOSE, TOTALDOSE),
             ENTRUST          = nvl(v_ENTRUST, ENTRUST),
             ORDERTYPE        = nvl(v_ORDERTYPE, ORDERTYPE),
             ORDERSTATUS      = nvl(v_ORDERSTATUS, ORDERSTATUS),
             SPECIALMARK      = nvl(v_SPECIALMARK, SPECIALMARK),
             CEASEID          = nvl(v_CEASEID, CEASEID),
             CEASEDATE        = nvl(v_CEASEDATE, CEASEDATE), --ʱ��
             CONTENT          = nvl(v_CONTENT, CONTENT),
             SYNCH            = nvl(v_SYNCH, SYNCH),
             MEMO             = nvl(v_MEMO, MEMO),
             FORMTYPE         = nvl(v_FORMTYPE, FORMTYPE)
       where TEMPID = v_TEMPID;
      --ɾ��
    elsif v_EditType = '3' then
      delete TEMP_ORDER where TEMPID = v_TEMPID;
      --�鿴
    elsif v_EditType = '4' then
      open o_result for
        select * from TEMP_ORDER where TEMPID = v_TEMPID;
    
    end if;
  end;

  /*****************************************************************************/
  PROCEDURE usp_editallergyhistoryinfo(v_edittype     VARCHAR, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                       v_id           NUMERIC, --Ψһ��
                                       v_noofinpat    NUMERIC DEFAULT '-1', --������ҳ���
                                       v_allergyid    INT DEFAULT -1, --��������
                                       v_allergylevel INT DEFAULT -1, --�����̶�
                                       v_doctor       VARCHAR DEFAULT '', --����ҽ��
                                       v_allergypart  VARCHAR DEFAULT '', --������λ
                                       v_reactiontype VARCHAR DEFAULT '', --��Ӧ����
                                       v_memo         VARCHAR DEFAULT '' --��ע
                                       ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭����ʷ��Ϣ
     ����˵��
     �������
     �������
     �����������
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_edittype = '1' THEN
      --���
      INSERT INTO allergyhistory
        (ID,
         noofinpat,
         allergyid,
         allergylevel,
         doctor,
         allergypart,
         reactiontype,
         memo)
      VALUES
        (seq_allergyhistory_id.NEXTVAL,
         v_noofinpat,
         v_allergyid,
         v_allergylevel,
         v_doctor,
         v_allergypart,
         v_reactiontype,
         v_memo);
    ELSIF (v_edittype = '2') THEN
      --�޸�
      UPDATE allergyhistory
         SET noofinpat    = v_noofinpat,
             allergyid    = v_allergyid,
             allergylevel = v_allergylevel,
             doctor       = v_doctor,
             allergypart  = v_allergypart,
             reactiontype = v_reactiontype,
             memo         = v_memo
       WHERE ID = v_id;
    ELSIF (v_edittype = '3') THEN
      --ɾ��
      DELETE allergyhistory WHERE ID = v_id;
    END IF;
  END;

  /******************************************************************************/
  PROCEDURE usp_editfamilyhistoryinfo(v_edittype  VARCHAR, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                      v_id        NUMERIC, --��ϵ�˱�ţ��ǲ�����ϵ�˵�Ψһ��ʶ
                                      v_noofinpat NUMERIC DEFAULT '-1',
                                      --��ҳ���(סԺ��ˮ��)(Inpatient.NoOfInpat)
                                      v_relation  VARCHAR DEFAULT '', --�����ϵ
                                      v_birthday  VARCHAR DEFAULT '', --�������ڣ���ǰ̨��ʾ���䣩
                                      v_diseaseid VARCHAR DEFAULT '', --���ִ���
                                      v_breathing INT DEFAULT '', --�Ƿ���(1:�ǣ�0��)
                                      v_cause     VARCHAR DEFAULT '', --����ԭ��
                                      v_memo      VARCHAR DEFAULT '' --��ע
                                      ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭������Ϣ
     ����˵��
     �������
     �������
     �����������
    
     exec usp_EditFamilyHistoryInfo v_EditType='3',v_ID='5'
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_edittype = '1' THEN
      --���
      INSERT INTO familyhistory
        (ID,
         noofinpat,
         relation,
         birthday,
         diseaseid,
         breathing,
         cause,
         memo)
      VALUES
        (seq_familyhistory_id.NEXTVAL,
         v_noofinpat,
         v_relation,
         v_birthday,
         v_diseaseid,
         v_breathing,
         v_cause,
         v_memo);
    ELSIF v_edittype = '2' THEN
      --�޸�
      UPDATE familyhistory
         SET noofinpat = v_noofinpat,
             relation  = v_relation,
             birthday  = v_birthday,
             diseaseid = v_diseaseid,
             breathing = v_breathing,
             cause     = v_cause,
             memo      = v_memo
       WHERE ID = v_id;
    ELSIF v_edittype = '3' THEN
      --ɾ��
      DELETE familyhistory WHERE ID = v_id;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_editillnesshistoryinfo(v_edittype     VARCHAR, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                       v_id           NUMERIC, --Ψһ��
                                       v_noofinpat    NUMERIC DEFAULT '-1', --������ҳ���
                                       v_diagnosisicd VARCHAR DEFAULT '', --���ִ���
                                       v_discuss      VARCHAR DEFAULT '', --��������
                                       v_diseasetime  VARCHAR DEFAULT '', --����ʱ��
                                       v_cure         INT DEFAULT '', --�Ƿ�����
                                       v_memo         VARCHAR DEFAULT '' --��ע
                                       ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭����ʷ��Ϣ
     ����˵��
     �������
     �������
     �����������
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_edittype = '1' THEN
      --���
      INSERT INTO illnesshistory
        (ID, noofinpat, diagnosisicd, discuss, diseasetime, cure, memo)
      VALUES
        (seq_illnesshistory_id.NEXTVAL,
         v_noofinpat,
         v_diagnosisicd,
         v_discuss,
         v_diseasetime,
         v_cure,
         v_memo);
    ELSIF v_edittype = '2' THEN
      --�޸�
      UPDATE illnesshistory
         SET noofinpat    = v_noofinpat,
             diagnosisicd = v_diagnosisicd,
             discuss      = v_discuss,
             diseasetime  = v_diseasetime,
             cure         = v_cure,
             memo         = v_memo
       WHERE ID = v_id;
    ELSIF v_edittype = '3' THEN
      --ɾ��
      DELETE illnesshistory WHERE ID = v_id;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_editnotesonnursinginfonew(v_noofinpat     NUMERIC, --��ҳ���(סԺ��ˮ��)
                                          v_dateofsurvey  VARCHAR, --���������ڣ���ʽ2010-01-01��
                                          v_indx          VARCHAR, --���
                                          v_timeslot      VARCHAR, --����ʱ���
                                          v_temperature   VARCHAR DEFAULT '', --��������
                                          v_wayofsurvey   INT DEFAULT 8800, --���²�����ʽ����
                                          v_pulse         VARCHAR DEFAULT '', --����
                                          v_heartrate     VARCHAR DEFAULT '', --����
                                          v_breathe       VARCHAR DEFAULT '', --���ߺ���
                                          v_bloodpressure VARCHAR DEFAULT '', --����Ѫѹ
                                          v_timeofshit    VARCHAR DEFAULT '',
                                          --����������ʽ��1*(3/2E ),'*'��ʾ���ʧ��
                                          v_countin          VARCHAR DEFAULT '', --����������
                                          v_countout         VARCHAR DEFAULT '', --�����ܳ���
                                          v_drainage         VARCHAR DEFAULT '', --������
                                          v_height           VARCHAR DEFAULT '', --�������
                                          v_weight           VARCHAR DEFAULT '', --��������
                                          v_allergy          VARCHAR DEFAULT '', --���߹�����
                                          v_daysaftersurgery VARCHAR DEFAULT '', --����������
                                          v_dayofhospital    VARCHAR DEFAULT '', --סԺ����
                                          v_physicalcooling  VARCHAR DEFAULT '', --������
                                          v_doctorofrecord   VARCHAR, --��¼ҽ��
                                          v_PhysicalHotting  varchar default '', --�������� edit by ywk
                                          v_PainInfo         varchar default '' --��ʹ
                                          ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ���滤����Ϣ����
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    --ɾ��ԭ����(Ӧ�ü��ϲ�����ҳ���ɾ��  edit by ywk 2012��4��20��14:55:41)
    DELETE notesonnursing
     WHERE dateofsurvey = v_dateofsurvey
          --AND notesonnursing.indx = v_indx
       and timeslot = v_timeslot
       and noofinpat = v_noofinpat;
  
    /* --���滤����Ϣ����
    INSERT INTO notesonnursing
      (ID,
       noofinpat,
       dateofsurvey,
       temperature,
       wayofsurvey,
       pulse,
       heartrate,
       breathe,
       timeslot,
       bloodpressure,
       timeofshit,
       countin,
       countout,
       drainage,
       height,
       weight,
       allergy,
       daysaftersurgery,
       dayofhospital,
       physicalcooling,
       doctorofrecord,
       physicalhotting,
       paininfo
       )
    VALUES
      (seq_notesonnursing_id.NEXTVAL,
       v_noofinpat,
       v_dateofsurvey,
       v_temperature,
       v_wayofsurvey,
       v_pulse,
       v_heartrate,
       v_breathe,
       v_timeslot,
       v_bloodpressure,
       v_timeofshit,
       v_countin,
       v_countout,
       v_drainage,
       v_height,
       v_weight,
       v_allergy,
       v_daysaftersurgery,
       v_dayofhospital,
       v_physicalcooling,
       v_doctorofrecord,
       v_PhysicalHotting,
       v_PainInfo
       );*/
    --���滤����Ϣ����
    INSERT INTO notesonnursing
      (ID,
       noofinpat,
       dateofsurvey,
       temperature,
       wayofsurvey,
       pulse,
       heartrate,
       breathe,
       timeslot,
       --bloodpressure,
       --timeofshit,
       --countin,
       --countout,
       --drainage,
       --height,
       --weight,
       --allergy,
       --daysaftersurgery,
       --dayofhospital,
       physicalcooling,
       doctorofrecord,
       physicalhotting,
       paininfo
       --indx
       )
    VALUES
      (seq_notesonnursing_id.NEXTVAL,
       v_noofinpat,
       v_dateofsurvey,
       v_temperature,
       v_wayofsurvey,
       v_pulse,
       v_heartrate,
       v_breathe,
       v_timeslot,
       --v_bloodpressure,
       --v_timeofshit,
       --v_countin,
       -- v_countout,
       --v_drainage,
       --v_height,
       -- v_weight,
       --v_allergy,
       -- v_daysaftersurgery,
       --v_dayofhospital,
       v_physicalcooling,
       v_doctorofrecord,
       v_PhysicalHotting,
       v_PainInfo
       --v_indx
       );
  END;

  /*********************************************************************************/
  PROCEDURE usp_editnotesonnursinginfo(v_noofinpat     NUMERIC, --��ҳ���(סԺ��ˮ��)
                                       v_dateofsurvey  VARCHAR, --���������ڣ���ʽ2010-01-01��
                                       v_timeslot      VARCHAR, --����ʱ���
                                       v_temperature   VARCHAR DEFAULT '', --��������
                                       v_wayofsurvey   INT DEFAULT 8800, --���²�����ʽ����
                                       v_pulse         VARCHAR DEFAULT '', --����
                                       v_heartrate     VARCHAR DEFAULT '', --����
                                       v_breathe       VARCHAR DEFAULT '', --���ߺ���
                                       v_bloodpressure VARCHAR DEFAULT '', --����Ѫѹ
                                       v_timeofshit    VARCHAR DEFAULT '',
                                       --����������ʽ��1*(3/2E ),'*'��ʾ���ʧ��
                                       v_countin          VARCHAR DEFAULT '', --����������
                                       v_countout         VARCHAR DEFAULT '', --�����ܳ���
                                       v_drainage         VARCHAR DEFAULT '', --������
                                       v_height           VARCHAR DEFAULT '', --�������
                                       v_weight           VARCHAR DEFAULT '', --��������
                                       v_allergy          VARCHAR DEFAULT '', --���߹�����
                                       v_daysaftersurgery VARCHAR DEFAULT '', --����������
                                       v_dayofhospital    VARCHAR DEFAULT '', --סԺ����
                                       v_physicalcooling  VARCHAR DEFAULT '', --������
                                       v_doctorofrecord   VARCHAR, --��¼ҽ��
                                       v_PhysicalHotting  varchar default '', --�������� edit by ywk
                                       v_PainInfo         varchar default '' --��ʹ
                                       ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ���滤����Ϣ����
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    --ɾ��ԭ����(Ӧ�ü��ϲ�����ҳ���ɾ��  edit by ywk 2012��4��20��14:55:41)
    DELETE notesonnursing
     WHERE dateofsurvey = v_dateofsurvey
       AND timeslot = v_timeslot
       and noofinpat = v_noofinpat;
  
    /* --���滤����Ϣ����
    INSERT INTO notesonnursing
      (ID,
       noofinpat,
       dateofsurvey,
       temperature,
       wayofsurvey,
       pulse,
       heartrate,
       breathe,
       timeslot,
       bloodpressure,
       timeofshit,
       countin,
       countout,
       drainage,
       height,
       weight,
       allergy,
       daysaftersurgery,
       dayofhospital,
       physicalcooling,
       doctorofrecord,
       physicalhotting,
       paininfo
       )
    VALUES
      (seq_notesonnursing_id.NEXTVAL,
       v_noofinpat,
       v_dateofsurvey,
       v_temperature,
       v_wayofsurvey,
       v_pulse,
       v_heartrate,
       v_breathe,
       v_timeslot,
       v_bloodpressure,
       v_timeofshit,
       v_countin,
       v_countout,
       v_drainage,
       v_height,
       v_weight,
       v_allergy,
       v_daysaftersurgery,
       v_dayofhospital,
       v_physicalcooling,
       v_doctorofrecord,
       v_PhysicalHotting,
       v_PainInfo
       );*/
    --���滤����Ϣ����
    INSERT INTO notesonnursing
      (ID,
       noofinpat,
       dateofsurvey,
       temperature,
       wayofsurvey,
       pulse,
       heartrate,
       breathe,
       timeslot,
       --bloodpressure,
       --timeofshit,
       --countin,
       --countout,
       --drainage,
       --height,
       --weight,
       --allergy,
       --daysaftersurgery,
       --dayofhospital,
       physicalcooling,
       doctorofrecord,
       physicalhotting,
       paininfo)
    VALUES
      (seq_notesonnursing_id.NEXTVAL,
       v_noofinpat,
       v_dateofsurvey,
       v_temperature,
       v_wayofsurvey,
       v_pulse,
       v_heartrate,
       v_breathe,
       v_timeslot,
       --v_bloodpressure,
       --v_timeofshit,
       --v_countin,
       -- v_countout,
       --v_drainage,
       --v_height,
       -- v_weight,
       --v_allergy,
       -- v_daysaftersurgery,
       --v_dayofhospital,
       v_physicalcooling,
       v_doctorofrecord,
       v_PhysicalHotting,
       v_PainInfo);
  END;

  --��ʿ���ⵥ��Ϣά���С�����״̬��Ϣ��ά�� add by ywk 2012��4��23��14:05:18

  PROCEDURE usp_Edit_PatStates(v_EditType  varchar default '', --��������
                               v_CCode     varchar default '',
                               V_ID        varchar default '',
                               v_NoofInPat varchar default '',
                               v_DoTime    varchar default '',
                               v_Patid     varchar default '',
                               o_result    OUT empcurtyp) as
  begin
    open o_result for
    
      select V_ID from dual;
    --���
    if v_EditType = '1' then
      INSERT INTO PatientStatus
        (ID, CCODE, NOOFINPAT, DoTime, Patid)
      VALUES
        (seq_Emr_ConfigPoint_ID.Nextval,
         v_CCode,
         v_NoofInPat,
         v_DoTime,
         v_Patid);
    
      --�޸�
    elsif v_EditType = '2' then
    
      UPDATE PatientStatus
         set CCODE     = v_CCode,
             DoTime    = v_DoTime,
             NOOFINPAT = v_NoofInPat,
             Patid     = v_Patid
       WHERE id = V_ID;
    
      --ɾ��
    elsif v_EditType = '3' then
    
      /*update Emr_ConfigPoint a set a.valid = 0 where ID = V_ID;*/
      delete from PatientStatus
       where CCODE = v_CCode
         and NOOFINPAT = v_NoofInPat
         and id = V_id
         and Patid = v_Patid;
      commit;
    
      /*--��ѯ
      elsif v_EditType = '4' then open o_result for
        select (case
                 when a.valid = 1 then
                  '��'
                 else
                  '��'
               end) validName,
               a.*
          from Emr_ConfigPoint a
         where  a.valid = '1'
         order by id;*/
    end if;
  end;

  /*********************************************************************************/
  PROCEDURE usp_editpatientcontacts(v_edittype  VARCHAR, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                    v_id        NUMERIC DEFAULT '0', --��ϵ�˱�ţ��ǲ�����ϵ�˵�Ψһ��ʶ
                                    v_noofinpat NUMERIC DEFAULT '0',
                                    --��ҳ���(סԺ��ˮ��)(Inpatient.NoOfInpat)
                                    v_name     VARCHAR DEFAULT '', --��ϵ����
                                    v_sex      VARCHAR DEFAULT '', --�Ա�
                                    v_relation VARCHAR DEFAULT '',
                                    --��ϵ��ϵ(Dictionary_detail.DetailID, ID = '44')
                                    v_address    VARCHAR DEFAULT '', --��ϵ��ַ
                                    v_workunit   VARCHAR DEFAULT '', --��ϵ(��)��λ
                                    v_hometel    VARCHAR DEFAULT '', --��ϵ�˼�ͥ�绰
                                    v_worktel    VARCHAR DEFAULT '', --��ϵ�˹����绰
                                    v_postalcode VARCHAR DEFAULT '', --��ϵ�ʱ�
                                    v_tag        VARCHAR DEFAULT '' --��ϵ�˱�־
                                    ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭��һ��ϵ����Ϣ
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
     exec usp_EditPatientContacts v_EditType='1',v_ID='0',v_NoOfInpat='75278',v_Name='����',v_Sex='2'
     ,v_Relation='05',v_Address='�Ͼ����ֿ�����ɽ��·���ֵֽ�',v_WorkUnit='�Ͼ����ֿ���ũҵ����'
     ,v_HomeTel='15851867666',v_WorkTel='3422937293',v_PostalCode='3402334',v_Tag='0'
     �޸ļ�¼
    **********/
  BEGIN
    IF v_edittype = '1' THEN
      --���
      INSERT INTO patientcontacts
        (ID,
         noofinpat,
         NAME,
         sex,
         relation,
         address,
         workunit,
         hometel,
         worktel,
         postalcode,
         tag)
      VALUES
        (seq_patientcontacts_id.NEXTVAL,
         v_noofinpat,
         v_name,
         v_sex,
         v_relation,
         v_address,
         v_workunit,
         v_hometel,
         v_worktel,
         v_postalcode,
         v_tag);
    ELSIF v_edittype = '2' THEN
      --�޸�
      UPDATE patientcontacts
         SET NAME       = v_name,
             sex        = v_sex,
             relation   = v_relation,
             address    = v_address,
             workunit   = v_workunit,
             hometel    = v_hometel,
             worktel    = v_worktel,
             postalcode = v_postalcode,
             tag        = v_tag
       WHERE ID = v_id;
    ELSIF v_edittype = '3' THEN
      --ɾ��
      DELETE patientcontacts WHERE ID = v_id;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_editpersonalhistoryinfo(v_noofinpat    NUMERIC, --������ҳ���
                                        v_marital      VARCHAR, --����״��
                                        v_noofchild    INT, --��������
                                        v_jobhistory   VARCHAR, --ְҵ����
                                        v_drinkwine    INT, --�Ƿ�����
                                        v_winehistory  VARCHAR, --����ʷ
                                        v_smoke        INT, --�Ƿ�����
                                        v_smokehistory VARCHAR, --����ʷ
                                        v_birthplace   VARCHAR, --�����أ�ʡ�У�
                                        v_passplace    VARCHAR, --�����أ�ʡ�У�
                                        v_memo         VARCHAR DEFAULT '' --��ע
                                        ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭����ʷ��Ϣ
     ����˵��
     �������
     �������
     �����������
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    --ɾ����ǰ��¼
    DELETE personalhistory WHERE noofinpat = v_noofinpat;
  
    --���
    INSERT INTO personalhistory
      (ID,
       noofinpat,
       marital,
       noofchild,
       jobhistory,
       drinkwine,
       winehistory,
       smoke,
       smokehistory,
       birthplace,
       passplace,
       memo)
    VALUES
      (seq_personalhistory_id.NEXTVAL,
       v_noofinpat,
       v_marital,
       v_noofchild,
       v_jobhistory,
       v_drinkwine,
       v_winehistory,
       v_smoke,
       v_smokehistory,
       v_birthplace,
       v_passplace,
       v_memo);
  END;

  /*********************************************************************************/
  PROCEDURE usp_editsurgeryhistoryinfo(v_edittype    VARCHAR, --�༭��Ϣ���ͣ�1����ӡ�2���޸ġ�3��ɾ��
                                       v_id          NUMERIC, --Ψһ��
                                       v_noofinpat   NUMERIC DEFAULT '-1', --������ҳ���
                                       v_surgeryid   VARCHAR DEFAULT '', --��������(Surgery.ID)
                                       v_diagnosisid VARCHAR DEFAULT '', --������Diagnosis.ICD��
                                       v_discuss     VARCHAR DEFAULT '', --����
                                       v_doctor      VARCHAR DEFAULT '', --����ҽ��
                                       v_memo        VARCHAR DEFAULT '' --��ע
                                       ) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭����ʷ��Ϣ
     ����˵��
     �������
     �������
     �����������
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_edittype = '1' THEN
      --���
      INSERT INTO surgeryhistory
        (ID, noofinpat, surgeryid, diagnosisid, discuss, doctor, memo)
      VALUES
        (seq_surgeryhistory_id.NEXTVAL,
         v_noofinpat,
         v_surgeryid,
         v_diagnosisid,
         v_discuss,
         v_doctor,
         v_memo);
    ELSIF v_edittype = '2' THEN
      --�޸�
      UPDATE surgeryhistory
         SET noofinpat   = v_noofinpat,
             surgeryid   = v_surgeryid,
             diagnosisid = v_diagnosisid,
             discuss     = v_discuss,
             doctor      = v_doctor,
             memo        = v_memo
       WHERE ID = v_id;
    ELSIF v_edittype = '3' THEN
      --ɾ��
      DELETE surgeryhistory WHERE ID = v_id;
    END IF;
  END;

  /************************************************************************************/
  PROCEDURE usp_emr_modelsearcher(v_flag   INT,
                                  v_type   VARCHAR,
                                  o_result OUT empcurtyp) AS
    /**********
     �汾�� 1.0.0.0.0
     ����ʱ�� 2007.02.8
     ���� �ܻ�
     ��Ȩ
     ���� .NETģ��ѡ��
     ����˵��
     NETģ��ѡ��
     ����˵��
      v_flag   int,  --ģ�ͷ������
      v_type  int,  --ģ�����
     ����ֵ
     �����������
     ���õ�sp
     ����ʵ��
    exec  usp_Emr_ModelSearcher  0,3800
     �޸���ʷ
    **********/
    v_sql    VARCHAR(500);
    v_dybm   VARCHAR(20);
    v_sortid VARCHAR(10);
    v_mbmc   VARCHAR(10);
    v_mbms   VARCHAR(10);
    v_mxmc   VARCHAR(10);
  BEGIN
    IF v_type = 3800 THEN
      BEGIN
        v_dybm   := 'Model_Atom';
        v_sortid := 'DisMode';
        v_mbmc   := 'jdxsm';
        v_mbms   := 'jdms';
      END;
    ELSIF v_type = 3801 THEN
      BEGIN
        v_dybm   := 'Model_Object';
        v_sortid := 'SortID';
        v_mbmc   := 'dxmc';
        v_mbms   := 'dxms';
      END;
    ELSIF v_type = 3802 THEN
      BEGIN
        v_dybm   := 'Model_Embed';
        v_sortid := 'SortID';
        v_mbmc   := 'qrmbmc';
        v_mbms   := 'qrmbms';
      END;
    ELSIF v_type = 3804 THEN
      BEGIN
        v_dybm   := 'Model_Docment';
        v_sortid := 'SortID';
        v_mbmc   := 'mbmc';
        v_mbms   := 'mbms';
      END;
    ELSIF v_type = 3805 THEN
      BEGIN
        v_dybm   := 'Model_Record';
        v_sortid := 'SortID';
        v_mbmc   := 'zhmbmc';
        v_mbms   := 'zhmbms';
      END;
    ELSIF v_type = 3806 THEN
      BEGIN
        v_dybm   := 'Model_Table';
        v_sortid := 'SortID';
        v_mbmc   := 'bgmc';
        v_mbms   := 'bgms';
      END;
    ELSIF v_type = 3807 THEN
      BEGIN
        v_dybm   := 'Model_Structure';
        v_sortid := 'SortID';
        v_mbmc   := 'dxmc';
        v_mbms   := 'dxms';
      END;
    END IF;
  
    IF v_flag = 0 THEN
      BEGIN
        v_sql := 'select a.ID,a.Name,a.Describe,b.ID CatalogID ,b.Name Catalog,a.' ||
                 v_sortid || ' PrevID,a.' || v_sortid || ' SortID';
      
        IF (v_type <> 3800) THEN
          v_sql := v_sql || ', a.Valid';
        ELSE
          v_sql := v_sql || ',1 Valid';
        END IF;
      
        v_sql := v_sql || ' from ' || v_dybm ||
                 ' a left join ModelDirectory b on a.' || v_sortid ||
                 ' = b.ID order by ID';
      
        OPEN o_result FOR v_sql;
      END;
    END IF;
  END;

  /************************************************************************************/
  PROCEDURE USP_EMR_GETPATINFO(v_NoOfinpat varchar, o_result OUT empcurtyp) AS
  
    v_addresstip VARCHAR(30); --���ڶ�ȡ��ַ�����ñ�ʶ
  
  begin
    select instr(value, '<IsReadAddressInfo>0</IsReadAddressInfo>', 1, 1)
      into v_addresstip
      from appcfg
     where configkey = 'EmrInputConfig';
    /* if v_addresstip = '0'--��ȡʡ����
    then */
    open o_result for
      SELECT b.noofinpat  noofinpat, --��ҳ���
             b.patnoofhis patnoofhis, --HIS��ҳ���
             b.patid      patid,
             --סԺ��
             b.NAME   patname, --����
             b.sexid  sex, --�����Ա�
             j.name   sexname, --�����Ա�����
             b.agestr agestr,
             --����
             b.py py, --ƴ��
             b.wb wb, --���
             b.status brzt, --����״̬
             e.NAME brztname, --����״̬����
             RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
             i.NAME wzjbmc, --Σ�ؼ�������
             --case when PatID is null then ''
             --     else 'һ������'
             --end hljb --������ ����
             /*
               cd.NAME hljb,CASE
             WHEN b.attendlevel IS NULL THEN   6105END attendlevel, --������*/
             case b.attendlevel
               when '1' then
                'һ������'
               when '2' then
                '��������'
               when '3' then
                '��������'
               when '4' then
                '�ؼ�����'
               else
                'һ������'
             end hljb, --������
             
             case b.attendlevel
               when '1' then
                '6101'
               when '2' then
                '6102'
               when '3' then
                '6103'
               when '4' then
                '6104'
               else
                '6101'
             end attendlevel, --������
             
             b.isbaby yebz, --Ӥ����־
             CASE
               WHEN b.isbaby = '0' THEN
                '��'
               WHEN b.isbaby IS NULL THEN
                ''
               ELSE
                '��'
             END yebzname,
             a.wardid bqdm, --��������
             a.deptid ksdm, --���Ҵ���
             --a.ID bedid, --��λ����
             b.outbed bedid, --��Ժ��λ
             dg.NAME  ksmc,
             --��������
             wh.NAME        bqmc, --��������
             a.formerward   ybqdm, --ԭ��������
             a.formerdeptid yksdm,
             --ԭ���Ҵ���
             a.formerdeptid ycwdm, --ԭ��λ����
             a.inbed        inbed, --ռ����־
             a.borrow       jcbz, --�贲��־
             a.sexinfo      cwlx, --��λ����
             -- SUBSTR(b.admitdate, 1, 16) admitdate,--��Ժʱ��
             
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'yyyy') || '��' ||
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'MM') || '��' ||
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'dd') || '��' || ' ' ||
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'hh24') || ':' ||
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'mi') admitdate, --��Ժʱ��
             
             f.NAME     ryzd, --��Ժ���
             f.NAME     zdmc, --�������
             b.resident zyysdm, --סԺҽ������
             c.NAME     zyys,
             --סԺҽ��
             c.NAME  cwys, --��λҽ��
             g.NAME  zzys, --����ҽʦ
             h.NAME  zrys, --����ҽʦ
             a.valid yxjl, --��Ч��¼
             --me.pzlx pzlx, --ƾ֤����
             dd1.NAME pzlx, --�������
             /*TO_CHAR((CASE
              WHEN INSTR(v_deptids, a.deptid) = 0 AND (b.noofinpat IS NULL) THEN
                '������������'
             END)) extra, --������Ϣ*/
             b.memo memo, --��ע
             (CASE b.cpstatus
               WHEN 0 THEN
                'δ����'
               WHEN 1 THEN
                'ִ����'
               WHEN 2 THEN
                '�˳�'
             END) cpstatus,
             CASE
               WHEN b.noofinpat IS NULL THEN
                ''
               ELSE
                '����д'
             END recordinfo,
             100 ye, --���
             (SELECT CASE
                       WHEN COUNT(qc.foulstate) > 0 THEN
                        '��'
                       ELSE
                        '��'
                     END
                FROM qcrecord qc
               WHERE qc.noofinpat = b.noofinpat
                 AND qc.valid = 1
                 AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
             
             b.organization Organization, --������λ
             b.officeplace, --������λ���£�
             
             --NVL(b.nativeaddress, b.officeplace) Address, --����סַ edit by ywk
             
             case
               when v_addresstip > 0 then
                b.nativeaddress
               else
                b.xzzaddress ---hkaddress
             end Address, --�ʺ�����:����סַ   edit by ywk
             b.officetel OFFICETEL, ---������λ�绰 add by ywk 2012��8��14�� 11:31:35
             b.xzztel TEL, --��סַ�绰 add  by ywk 2012��8��14�� 11:32:12 ԭ���ĵ绰�ͱ�־Ϊ��סַ�绰
             --NVL(b.nativetel, b.officetel) TEL, --�绰
             b.idno  IDNO, --���֤��
             j1.name JobName, --ְλ
             --b.inwarddate inwarddate, --���ʱ��
             to_char(to_date(b.inwarddate, 'yyyy-MM-dd hh24:mi:ss'), 'yyyy') || '��' ||
             to_char(to_date(b.inwarddate, 'yyyy-MM-dd hh24:mi:ss'), 'MM') || '��' ||
             to_char(to_date(b.inwarddate, 'yyyy-MM-dd hh24:mi:ss'), 'dd') || '��' || ' ' ||
             to_char(to_date(b.inwarddate, 'yyyy-MM-dd hh24:mi:ss'), 'hh24') || ':' ||
             to_char(to_date(b.inwarddate, 'yyyy-MM-dd hh24:mi:ss'), 'mi') inwarddate, --���ʱ��
             j2.name marital, --����
             --nvl(a2.name, a1.name) HOMETOWN, --������
             --a2.name || a1.name HOMETOWN,
             
             --case when v_addresstip = '0' then
             /*  case when a1.provincename = a2.cityname then a1.provincename
             else a1.provincename || a2.cityn*/ --end HOMETOWN,
             -- else  b.csdaddress
             -- end HOMETOWN,
             
             case
               when v_addresstip > 0 then
                case
                  when a1.provincename = a2.cityname then
                   a1.provincename
                  else
                   a1.provincename || a2.cityname
                end
               else
                b.csdaddress
             end HOMETOWN, --������
             
             case
               when v_addresstip > 0 then
                case
                  when a3.provincename = a4.cityname then
                   a3.provincename
                  else
                   a3.provincename || a4.cityname
                end --JiGuan, --����
               else
                b.jgaddress
             end JiGuan, --����
             
             /*  case when a3.provincename = a4.cityname then a3.provincename
             else a3.provincename || a4.cityname end JiGuan, --����
             --else b.jgaddress end*/ -- JiGuan,--����
             
             to_char(to_date(b.birth, 'yyyy-MM-dd hh24:mi:ss'), 'yyyy') || '��' ||
             to_char(to_date(b.birth, 'yyyy-MM-dd hh24:mi:ss'), 'MM') || '��' ||
             to_char(to_date(b.birth, 'yyyy-MM-dd hh24:mi:ss'), 'dd') || '��' birth, --��������
             j3.name nation, --����
             --trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')) INADMITDATE, --��Ժ����
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'yyyy') || '��' ||
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'MM') || '��' ||
             to_char(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'), 'dd') || '��' INADMITDATE, --��Ժ����
             b.contactaddress CONTACTADDRESS, --��ϵ�˵�ַ
             b.contactperson CONTACTPERSON, --��ϵ������
             b.contacttel CONTACTTEL, --��ϵ�˵绰
             b.incount INCOUNT, --סԺ����
             patdiag.diag_content DIAGCONTENT, --��Ҫ���
             d1.name ADMITDEPT, --��Ժ����
             w1.name ADMITWARD, --��Ժ����
             case
               --xll 2013-08-15 �޸�Ϊ סԺ��������һ��Ϊ1�� ����Ժ���� �����Ժ����
               when b.status in (1500, 1501) then --������Ժʱ �õ�ǰʱ��-��Ժʱ��+1  xull 2013-05-08 
               /* trunc(sysdate) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')) + 1*/
                decode(trunc(sysdate) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')),0,1,trunc(sysdate) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'))) 
               when b.outwarddate is not null then --����״̬ �����г���ʱ�� ����ʱ��-��Ժ+1
               /* trunc(to_date(b.outwarddate, 'yyyy-MM-dd hh24:mi:ss')) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')) + 1*/
                decode(trunc(to_date(b.outwarddate, 'yyyy-MM-dd hh24:mi:ss')) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')),0,1,trunc(to_date(b.outwarddate, 'yyyy-MM-dd hh24:mi:ss')) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'))) 
                
               when b.outhosdate is not null then --����״̬ �����г�Ժʱ�� ��Ժʱ��-��Ժ+1
               /* trunc(to_date(b.outhosdate, 'yyyy-MM-dd hh24:mi:ss')) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')) + 1*/
                decode(trunc(to_date(b.outhosdate, 'yyyy-MM-dd hh24:mi:ss')) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')),0,1,trunc(to_date(b.outhosdate, 'yyyy-MM-dd hh24:mi:ss')) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'))) 
               else --�����޷�Ԥ�� ֱ�ӵ�ǰʱ��-��Ժʱ��+1  xull 2013-05-08 һ�㲻����
               /* trunc(sysdate) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')) + 1*/
                 decode(trunc(sysdate) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss')),0,1,trunc(sysdate) -
                trunc(to_date(b.admitdate, 'yyyy-MM-dd hh24:mi:ss'))) 
             end indays, --סԺ����
             u1.name ClinicDoctor, --����ҽ��
             u2.name Resident, --סԺҽ��
             u3.name Attend, --����ҽʦ
             u4.name Chief, --����ҽʦ
             b.isbaby, ---����ȡ�õ�ǰ�û��ǲ���Ӥ����add by ywk
             b.mother, ---ĸ�׵���ҳ���Ҳȡ���� add by ywk 2012��11��23��15:54:23
             j5.name relations
        FROM /*
             bed a
             LEFT JOIN inpatient b ON a.noofinpat = b.noofinpat
                                  AND a.patnoofhis = b.patnoofhis
                                  and a.deptid = b.outhosdept
                                  AND a.inbed = 1301
                                  AND b.status IN (1501, 1504, 1505, 1506, 1507)
             */ inpatient b
        LEFT JOIN bed a
          ON a.noofinpat = b.noofinpat
         AND a.patnoofhis = b.patnoofhis
         AND a.deptid = b.outhosdept
         AND a.valid = '1'
        LEFT JOIN categorydetail e
          ON b.status = e.ID
         AND e.categoryid = '15'
      --LEFT JOIN medicareinfo me ON b.voucherscode = me.ID
        LEFT JOIN Dictionary_detail dd1
          ON dd1.categoryid = '1'
         AND b.payid = dd1.detailid
        LEFT JOIN department dg
          ON b.outhosdept = dg.ID
        LEFT JOIN ward wh
          ON b.outhosward = wh.id
      --   left join YY_SFXXMK e  on b.AttendLevel = e.sfxmdm
        LEFT JOIN diagnosis f
          ON f.icd = b.admitdiagnosis
        LEFT JOIN users c
          ON c.ID = b.resident
        LEFT JOIN dictionary_detail i
          ON i.detailid = b.criticallevel
         AND i.categoryid = '53'
        LEFT JOIN users g
          ON g.ID = b.attend
        LEFT JOIN users h
          ON h.ID = b.chief
        LEFT JOIN dictionary_detail j
          ON j.detailid = b.sexid
         AND j.categoryid = '3'
        LEFT JOIN dictionary_detail j1
          on j1.detailid = b.jobid
         AND j1.categoryid = '41'
        LEFT JOIN categorydetail cd
          ON b.attendlevel = cd.ID
         AND cd.categoryid = '63'
        LEFT JOIN dictionary_detail j2
          on j2.detailid = b.marital
         AND j2.categoryid = '4'
      --LEFT JOIN Areas a1 on a1.id = b.provinceid
      --                  AND a1.category = '1000'
      --LEFT JOIN Areas a2 on a2.id = b.countyid
      --                  AND a2.category = '1001'
        left outer JOIN s_province a1
          on a1.provinceid = b.provinceid
        left outer JOIN s_city a2
          on a2.cityid = b.countyid
      
        left outer join s_province a3
          on a3.provinceid = b.nativeplace_p
        left outer join s_city a4
          on a4.cityid = b.nativeplace_c
      
        LEFT JOIN dictionary_detail j3
          on j3.detailid = b.nationid
         AND j3.categoryid = '42'
        LEFT JOIN dictionary_detail j4
          on j4.detailid = b.nationalityid
         AND j4.categoryid = '43'
        LEFT JOIN patdiag
          on patdiag.diag_type_name = '��Ժ���'
         AND patdiag.diag_no = 1
         AND patdiag.diag_sub_no = 0
         AND patdiag.patient_id = b.noofinpat
        LEFT JOIN department d1
          on d1.id = b.admitdept
         AND d1.valid = '1'
        LEFT JOIN ward w1
          on w1.id = b.admitward
         AND w1.valid = '1'
        LEFT JOIN users u1
          on u1.id = b.clinicdoctor
         and u1.valid = '1'
        LEFT JOIN users u2
          on u2.id = b.resident
         and u2.valid = '1'
        LEFT JOIN users u3
          on u3.id = b.attend
         and u3.valid = '1'
        LEFT JOIN users u4
          on u4.id = b.chief
         and u4.valid = '1'
        LEFT JOIN Dictionary_detail j5
          on j5.detailid = b.relationship
         AND j5.categoryid = '44'
       WHERE b.noofinpat = v_NoOfinpat;
  
  end;

  /*********************************************************************************/
  procedure usp_Emr_QueryOrderSuites(v_DeptID   varchar,
                                     v_WardID   varchar,
                                     v_DoctorID varchar,
                                     v_yzlr     int default 1,
                                     o_result   OUT empcurtyp,
                                     o_result1  OUT empcurtyp) as
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��] 2006.07.24
    [����] �ܻ�
    [��Ȩ] Copyright ?
    [����] ��ѯ���õĳ���ҽ��
    [����˵��]
      ���ڰ�ָ���Ŀ��ҽ��в�ѯ����������¼����ϸ��¼���ݼ�
    [�������]
       v_DeptID  utKsdm  -- ���Ҵ���
      ,v_WardID  utKsdm  -- ��������
      ,v_DoctorID  utCzyh  -- ҽ������
      ,v_yzlr  utBz  -- �Ƿ���ҽ��¼��ģ�����(0: ��  1:��)
    [�������]
    [�����������]
      ����ҽ������¼���ݼ�
      ����ҽ����ϸ���ݼ�
    [���õ�sp]
    [����ʵ��]
      exec usp_Emr_QueryOrderSuites '2056', '2983', '001', 1
    **********/
    v_sql varchar2(4000);
  begin
    v_sql := 'truncate table tmp_Emr_QueryOrderSuites ';
    EXECUTE IMMEDIATE v_sql;
  
    -- �ڽ����Ͻ����ճ��ڡ���ʱ�ֱ���ʾ��Ӧ�ĳ���ҽ�����������������ݼ�ʱҪ���⴦�����������ݹ��˵���Ҫ
    insert into tmp_Emr_QueryOrderSuites
      select e.ID              GroupID,
             a.DetailID,
             a.Mark,
             a.SortMark,
             a.PlaceOfDrug,
             a.StandardOfDrug,
             a.ClinicIDOfDrug,
             a.DrugID,
             a.DrugName,
             a.ItemCategory,
             a.MinUnit,
             a.Dose,
             a.DoseUnit,
             a.MeasurementUnit,
             a.UnitCategory,
             a.UseageID,
             a.Frequency,
             a.EXECUTIONS,
             a.ExecuteCycle,
             a.CycleUnit,
             a.ZID,
             a.ExecuteDate,
             a.ExecuteDays,
             a.DrugGross,
             a.AdviceContent,
             a.AdviceCategory,
             e.Name,
             e.DeptID,
             e.WardID,
             e.DoctorID,
             e.UseRange,
             e.Memo,
             e.py,
             e.Wb,
             b.Name            yfmc,
             c.Name            pcmc --, d.Name yzbzmc--, f.Name yzlbmc
        from AdviceGroup e
        left join AdviceGroupDetail a
          on a.GroupID = e.ID
      --join YY_SJLBMXK d  on a.Mark = d.mxbh
      --join YY_SJLBMXK f  on a.AdviceCategory = f.mxbh
        left join DrugUseage b
          on a.UseageID = b.ID
        left join AdviceFrequency c
          on a.Frequency = c.ID
       where e.UseRange = 2900
          or (e.UseRange = 2901 and e.DeptID = v_DeptID)
          or (e.UseRange = 2903 and e.DoctorID = v_DoctorID);
  
    -- ��������
    if v_yzlr = 1 then
      open o_result for
        select distinct GroupID,
                        
                        Name,
                        DeptID,
                        WardID,
                        DoctorID,
                        UseRange,
                        Memo,
                        py,
                        Wb,
                        Mark,
                        (case AdviceCategory
                          when 3104 then
                           AdviceCategory
                          else
                           -1
                        end) AdviceCategory
          from tmp_Emr_QueryOrderSuites
         order by UseRange, Mark, AdviceCategory, Name;
    else
      open o_result for
        select distinct GroupID,
                        Name,
                        DeptID,
                        WardID,
                        DoctorID,
                        UseRange,
                        Memo,
                        py,
                        Wb
          from tmp_Emr_QueryOrderSuites
         order by UseRange, Name;
    end if;
  
    open o_result1 for
      select DetailID,
             GroupID,
             Mark,
             SortMark,
             PlaceOfDrug,
             StandardOfDrug,
             ClinicIDOfDrug,
             DrugID,
             DrugName,
             ItemCategory,
             MinUnit,
             Dose,
             DoseUnit,
             MeasurementUnit,
             UnitCategory,
             UseageID,
             Frequency,
             EXECUTIONS,
             ExecuteCycle,
             CycleUnit,
             ZID,
             ExecuteDate,
             ExecuteDays,
             DrugGross,
             AdviceContent,
             AdviceCategory
        from tmp_Emr_QueryOrderSuites
       where DetailID is not null
       order by GroupID, DetailID;
  
  end;

  /*********************************************************************************/
  procedure usp_Emr_SetOrderGroupSerialNo(v_NoOfInpat numeric,
                                          v_OrderType int,
                                          v_onlynew   int default 1) as
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��] 2007.02.14
    [����]
    [��Ȩ] Copyright ?
    [����] ����ҽ���ķ������
    [����˵��]
      ����ҽ���ķ����־��������ָ�����˵��³��ڻ���ʱҽ���ķ�����š�
      һ����ҽ���������ô˴洢���̽���ͳһ���ã�����������ǰ��ҽ������������
    [�������]
       v_NoOfInpat  utSyxh  -- EMR��ҳ���
      ,v_OrderType  utBz  -- ҽ����� 0: ��ʱ; 1: ����; 2: ȫ��
      ,v_onlynew utBz = 1 -- ֻ������ҽ�� 1: ��; 0: ��
    [�������]
    [�����������]
      �ɹ� -- T
      ʧ�� -- F, ʧ��ԭ��
    [���õ�sp]
    [����ʵ��]
       exec usp_Emr_SetOrderGroupSerialNo  32 , 1 ,1
    **********/
    v_tablename varchar2(40);
    v_cqlsbz    int;
    v_yzxh      int;
    v_GroupID   int;
    v_GroupFlag int;
    v_yznrlb    int;
    v_Memo      varchar2(50);
    --, v_lastcqlsbz utBz
    --, v_lastfzxh utXh12
    v_cyhzbz varchar2(50); -- ��ҩ������Ϣ��־
    v_sql    varchar2(4000);
  begin
  
    --yxy��ʱ����ʱʹ��ʵ���
    /*
     v_tablename := 'tmp_newfzxh';
    
    droptable(v_tablename);
     --������ʱ��
    
     v_sql := 'create table ' || v_tablename || '(
     cqlsbz  int    not null,  -- ������ʱ��־, 0: ��ʱ; 1: ����
     yzxh  int  not null,  -- ҽ�����
     GroupID  int  not null,  -- �������
     GroupFlag  int  not null,  -- �����־
     OrderType  int  not null,  -- ҽ�����(YY_SJLBMXK.mxbh, lbbh = 31)
     Memo  varchar(50)  null  ,  -- ��ע
     constraint pk_newfzxh primary key (yzxh, cqlsbz)
     )';
     execute immediate v_sql;*/
  
    v_sql := 'truncate table tmp_newfzxh ';
    EXECUTE IMMEDIATE v_sql;
  
    if ((v_OrderType = 0) or (v_OrderType = 2)) then
    
      insert into tmp_newfzxh
        (cqlsbz, yzxh, GroupID, GroupFlag, OrderType, Memo)
        select 0, TempID, GroupID, GroupFlag, OrderType, Memo
          from Temp_Order
         where NoOfInpat = v_NoOfInpat
           and ((v_onlynew = 0) or
               ((v_onlynew = 1) and (OrderStatus = 3200)))
         order by TempID;
    end if;
  
    if ((v_OrderType = 1) or (v_OrderType = 2)) then
      insert into tmp_newfzxh
        (cqlsbz, yzxh, GroupID, GroupFlag, OrderType, Memo)
        select 1, LongID, GroupID, GroupFlag, OrderType, Memo
          from Long_Order
         where NoOfInpat = v_NoOfInpat
           and ((v_onlynew = 0) or
               ((v_onlynew = 1) and (OrderStatus = 3200)))
         order by LongID;
    end if;
  
    --select v_lastcqlsbz = 0, v_lastfzxh = 0
    v_cyhzbz := '��ҩ����';
    declare
      cursor cr_fzxh is
        select cqlsbz, yzxh, GroupFlag, OrderType, Memo
          from tmp_newfzxh
         order by cqlsbz, yzxh
           for update of GroupID;
    
      --�α�
    begin
      open cr_fzxh;
      fetch cr_fzxh
        into v_cqlsbz, v_yzxh, v_GroupFlag, v_yznrlb, v_Memo;
      while cr_fzxh%found loop
        -- ���´��������ŵĹ������ǰ�������˳����д���δ���Ǵ�������ݷ����д�������������
        if (v_GroupFlag <= 3501) then
          -- �鿪ʼ������δ����,���������ҽ�����һ��
          begin
            if ((v_yznrlb = 3110) and (instr(v_Memo, v_cyhzbz) > 0)) then
            
              update tmp_newfzxh
                 set Memo = v_cyhzbz + to_char(v_GroupID)
               where current of cr_fzxh; -- ��ҩ���ܵ�Memo��Ҫ�����ҩ��ϸ�ķ������
            end if;
            v_GroupID := v_yzxh; --, v_lastfzxh = v_yzxh
          end;
        end if;
      
        update tmp_newfzxh
           set GroupID = v_GroupID
         where current of cr_fzxh; --yzxh = v_yzxh and cqlsbz = v_cqlsbz
      
        fetch cr_fzxh
          into v_cqlsbz, v_yzxh, v_GroupFlag, v_yznrlb, v_Memo;
      end loop;
    
      close cr_fzxh;
    
    end;
  
    update Temp_Order a
       set GroupID =
           (select b.GroupID
              from tmp_newfzxh b
             where a.TempID = b.yzxh
               and b.cqlsbz = 0)
     where exists (select 1
              from tmp_newfzxh b
             where a.TempID = b.yzxh
               and b.cqlsbz = 0);
  
    update Long_Order a
       set GroupID =
           (select b.GroupID
              from tmp_newfzxh b
             where a.LongID = b.yzxh
               and b.cqlsbz = 1)
     where exists (select 1
              from tmp_newfzxh b
             where a.LongID = b.yzxh
               and b.cqlsbz = 1);
  
  end;

  /*********************************************************************************/
  PROCEDURE usp_getapplyrecordnew(v_dateBegin VARCHAR, --��ʼ����
                                  v_dateEnd   VARCHAR, --��������
                                  --v_DateInBegin  varchar, --��Ժ��ʼ����
                                  --v_DateInEnd    varchar, --��Ժ��������
                                  v_patientName VARCHAR, --��������
                                  v_DocName     VARCHAR, --����ҽʦ����
                                  v_outhosdept  VARCHAR, --��Ժ���ҿ���
                                  o_result      OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������Ĳ���
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
     exec usp_GetApplyRecord v_DateBegin=N'2001-04-20',v_DateEnd=N'2011-04-27',v_OutHosDept=N'0000'
     exec usp_GetApplyRecord v_DateBegin=N'2011-04-20',v_DateEnd=N'2011-04-28',v_OutHosDept=N'0000'
     �޸ļ�¼
    **********/
  BEGIN
    --��ȡ������Ĳ���
    OPEN o_result FOR
      SELECT distinct ar.*,
                      u.id           as applydoctorid, --add by cyq 2012-12-06
                      u.NAME         AS applydoctorname,
                      u.py           as applydoctorpy, --add by cyq 2012-12-06
                      u.wb           as applydoctorwb, --add by cyq 2012-12-06
                      cd1.NAME       AS applyaimname,
                      cd2.NAME       AS unitname,
                      inp.noofrecord,
                      inp.incount,
                      inp.NAME,
                      inp.py         as patientNamePY,
                      inp.wb         as patientNameWB,
                      dd.NAME        AS inpsexname,
                      inp.NAME       AS inpname,
                      inp.sexid, --add by cyq 2012-11-16
                      inp.agestr,
                      dept.NAME      AS outhosdeptname,
                      inp.patid,
                      inp.admitdate,
                      inp.outhosdate,
                      d.NAME         AS outdiagnosisname,
                      --s.NAME AS surgeryname,
                      inp.outwarddate,
                      inp.noofinpat, --Add By wwj 2011-09-21
                      cd3.name        as statusname -- add by cyq 2012-11-14
        FROM applyrecord ar
        LEFT JOIN inpatient inp
          ON inp.noofinpat = ar.noofinpat
        LEFT JOIN department dept
          ON dept.ID = inp.outhosdept
        LEFT JOIN users u
          ON u.ID = ar.applydoctor
        LEFT JOIN categorydetail cd1
          ON cd1.categoryid = '50'
         AND cd1.ID = ar.applyaim
        LEFT JOIN categorydetail cd2
          ON cd2.categoryid = '51'
         AND cd2.ID = ar.unit
        LEFT JOIN categorydetail cd3
          on cd3.id = ar.status
         and cd3.categoryid = '52'
        LEFT JOIN dictionary_detail dd
          ON dd.categoryid = '3'
         AND dd.detailid = inp.sexid
        LEFT JOIN diagnosis d
          ON d.icd = inp.outdiagnosis
      --LEFT JOIN medicalrecord mr ON mr.noofinpat = inp.noofinpat
        LEFT JOIN recorddetail rd
          on rd.noofinpat = inp.noofinpat
      --LEFT JOIN diseasecfg dc ON dc.ID = mr.disease
      --LEFT JOIN surgery s ON s.ID = dc.surgeryid
       WHERE ar.applydate >= v_datebegin || ' 00:00:00 '
         AND ar.applydate < v_dateend || ' 24:00:00 '
         AND (inp.outhosdept = v_outhosdept OR v_outhosdept = '0000')
            --edit by cyq 2012-12-04
            --and (inp.Name = v_patientName or v_patientName = '' or v_patientName is null)
         and (inp.Name like '%' || v_patientName || '%' or
             inp.py like '%' || v_patientName || '%' or
             inp.wb like '%' || v_patientName || '%')
         and (u.NAME like '%' || v_DocName || '%' or
             u.py like '%' || v_DocName || '%' or
             u.wb like '%' || v_DocName || '%')
         AND ar.status = '5201'
       ORDER BY outwarddate;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getapplyrecord(v_datebegin  VARCHAR, --��ʼ����
                               v_dateend    VARCHAR, --��������
                               v_outhosdept VARCHAR, --��Ժ���ҿ���
                               o_result     OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������Ĳ���
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
     exec usp_GetApplyRecord v_DateBegin=N'2001-04-20',v_DateEnd=N'2011-04-27',v_OutHosDept=N'0000'
     exec usp_GetApplyRecord v_DateBegin=N'2011-04-20',v_DateEnd=N'2011-04-28',v_OutHosDept=N'0000'
     �޸ļ�¼
    **********/
  BEGIN
    --��ȡ������Ĳ���
    OPEN o_result FOR
      SELECT distinct ar.*,
                      u.NAME         AS applydoctorname,
                      cd1.NAME       AS applyaimname,
                      cd2.NAME       AS unitname,
                      inp.noofrecord,
                      inp.incount,
                      inp.NAME,
                      dd.NAME        AS inpsexname,
                      inp.NAME       AS inpname,
                      inp.agestr,
                      dept.NAME      AS outhosdeptname,
                      inp.patid,
                      inp.admitdate,
                      inp.outhosdate,
                      d.NAME         AS outdiagnosisname,
                      --s.NAME AS surgeryname,
                      inp.outwarddate,
                      inp.noofinpat --Add By wwj 2011-09-21
        FROM applyrecord ar
        LEFT JOIN inpatient inp
          ON inp.noofinpat = ar.noofinpat
        LEFT JOIN department dept
          ON dept.ID = inp.outhosdept
        LEFT JOIN users u
          ON u.ID = ar.applydoctor
        LEFT JOIN categorydetail cd1
          ON cd1.categoryid = '50'
         AND cd1.ID = ar.applyaim
        LEFT JOIN categorydetail cd2
          ON cd2.categoryid = '51'
         AND cd2.ID = ar.unit
        LEFT JOIN dictionary_detail dd
          ON dd.categoryid = '3'
         AND dd.detailid = inp.sexid
        LEFT JOIN diagnosis d
          ON d.icd = inp.outdiagnosis
      --LEFT JOIN medicalrecord mr ON mr.noofinpat = inp.noofinpat
        LEFT JOIN recorddetail rd
          on rd.noofinpat = inp.noofinpat
      --LEFT JOIN diseasecfg dc ON dc.ID = mr.disease
      --LEFT JOIN surgery s ON s.ID = dc.surgeryid
       WHERE ar.applydate >= v_datebegin || ' 00:00:00 '
         AND ar.applydate < v_dateend || ' 24:00:00 '
         AND (inp.outhosdept = v_outhosdept OR v_outhosdept = '0000')
         AND ar.status = '5201'
       ORDER BY outwarddate;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getcurrsystemparam(v_gettype INT, --��������
                                   o_result  OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ�� 2011-03-27
     ����   hjh
     ��Ȩ
     ����  ��ȡϵͳ��ǰ����
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_gettype = 1 THEN
      --��ȡ��ǰϵͳʱ��
      OPEN o_result FOR
        SELECT TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss') AS currserverdatetime
          FROM DUAL;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getdeptdeductpoint(v_deptcode      VARCHAR DEFAULT '',
                                   v_datetimebegin VARCHAR,
                                   v_datetimeend   VARCHAR,
                                   v_pointid       VARCHAR DEFAULT '',
                                   v_qcstattype    INT,
                                   o_result        OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ���Ҳ���ʧ�ֵ�
     ����˵��
     �������
      v_DeptCode varchar(10)    --���ұ��
     ,  v_DateTimeBegin varchar(9)  --��ʼʱ��
     ,  v_v_DateTimeEnd  varchar(9)  --����ʱ��
     ,  v_QCStatType int        --ͳ����������
     �������
     �����������
    ʧ��ͳ����Ϣ
    
     ���õ�sp
     ����ʵ��
     exec usp_GetInpatientFiling '','', '2007-06-01', '2012-06-30',2
     �޸ļ�¼
    **********/
  BEGIN
    IF v_qcstattype = 1 THEN
      --���Ҳ���ʧ��ͳ��
      OPEN o_result FOR
        SELECT dept.NAME deptname,
               point.deductpointresult,
               point.deductpointcnt,
               point.ID
          FROM dept_deductpoint point
          LEFT JOIN department dept
            ON TO_CHAR(dept.ID) = TO_CHAR(point.deptcode)
         WHERE TO_CHAR(TO_DATE(point.creattime, 'yyyy-mm-dd hh24:mi:ss'),
                       'yyyy-mm-dd') >= v_datetimebegin
           AND TO_CHAR(TO_DATE(point.creattime, 'yyyy-mm-dd hh24:mi:ss'),
                       'yyyy-mm-dd') <= v_datetimeend
           AND (TO_CHAR(dept.ID) = v_deptcode OR v_deptcode = '' OR
                v_deptcode IS NULL);
    ELSIF v_qcstattype = 2 THEN
      --���Ҳ���ʧ�ֵ���ϸ
      OPEN o_result FOR
        SELECT ROW_NUMBER() OVER(ORDER BY detail.ID ASC) AS rowid_,
               dept.NAME deptname,
               inp.patid,
               dd.NAME sexname,
               inp.NAME inpatientname,
               residentuser.NAME residentname,
               attenduser.NAME attendname,
               chiefuser.NAME chiefname,
               detail.deductpointcnt,
               detail.deductpointresult,
               inp.noofinpat
          FROM dept_deductpointdetail detail
          LEFT JOIN dept_deductpoint point
            ON detail.faid = point.ID
          LEFT JOIN department dept
            ON TO_CHAR(dept.ID) = TO_CHAR(point.deptcode)
          LEFT JOIN inpatient inp
            ON inp.noofinpat = detail.noofinpat
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN users residentuser
            ON residentuser.ID = inp.resident
          LEFT JOIN users attenduser
            ON attenduser.ID = inp.attend
          LEFT JOIN users chiefuser
            ON chiefuser.ID = inp.chief
         WHERE TO_CHAR(TO_DATE(point.creattime, 'yyyy-mm-dd hh24:mi:ss'),
                       'yyyy-mm-dd') >= v_datetimebegin
           AND TO_CHAR(TO_DATE(point.creattime, 'yyyy-mm-dd hh24:mi:ss'),
                       'yyyy-mm-dd') <= v_datetimeend
           AND (TO_CHAR(dept.ID) = v_deptcode OR v_deptcode = '' OR
                v_deptcode IS NULL)
           AND (TO_CHAR(point.ID) = v_pointid OR v_pointid = '' OR
                v_pointid IS NULL);
    ELSIF v_qcstattype = 3 THEN
      --���Ҳ���ʧ�ִ���
      OPEN o_result FOR
        SELECT point.ID, point.deductpointresult AS NAME, point.creattime
          FROM dept_deductpoint point
         WHERE TO_CHAR(TO_DATE(point.creattime, 'yyyy-mm-dd hh24:mi:ss'),
                       'yyyy-mm-dd') >= v_datetimebegin
           AND TO_CHAR(TO_DATE(point.creattime, 'yyyy-mm-dd hh24:mi:ss'),
                       'yyyy-mm-dd') <= v_datetimeend
           AND (TO_CHAR(point.deptcode) = v_deptcode OR v_deptcode = '' OR
                v_deptcode IS NULL)
           AND (TO_CHAR(point.ID) = v_pointid OR v_pointid = '' OR
                v_pointid IS NULL);
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getdoctortaskinfo(v_wardid  VARCHAR, --����
                                  v_deptids VARCHAR, --����
                                  v_userid  VARCHAR, --�û�ID
                                  v_time    VARCHAR, --ʱ��
                                  o_result  OUT empcurtyp) AS
  BEGIN
    --�����(�������)�� �����������ң�
    OPEN o_result FOR
      SELECT ip.NAME AS inpatientname,
             TO_CHAR(TO_DATE(ca.consulttime, 'yyyy-mm-dd hh24:mi:ss'),
                     'yyyy-mm-dd hh24:mi:ss') AS consulttime,
             cd.NAME AS consultstatus,
             cd1.NAME AS urgencytype,
             ip.NAME || '_' || cd1.NAME || '_' || ca.consulttime AS inpatientinfo,
             ca.stateid,
             ca.noofinpat,
             ca.consulttypeid,
             ca.consultapplysn
        FROM consultapply ca
        LEFT OUTER JOIN users u
          ON u.ID = ca.applyuser
         AND u.valid = '1'
        LEFT OUTER JOIN inpatient ip
          ON ip.noofinpat = ca.noofinpat
        LEFT OUTER JOIN categorydetail cd
          ON cd.categoryid = '67'
         AND ca.stateid = cd.ID
        LEFT OUTER JOIN categorydetail cd1
          ON cd1.categoryid = '66'
         AND cd1.ID = ca.urgencytypeid
       WHERE ca.valid = '1'
         AND ca.valid = '1'
         and substr(ca.consulttime, 1, 10) = substr(v_time, 1, 10)
         AND ip.status IN ('1501', '1502', '1504', '1505', '1506', '1507')
         and ((ip.outhosdept = v_deptids AND ca.stateid IN ('6720', '6730')) or
             ((INSTR(v_deptids, u.deptid) > 0 AND
             ca.stateid IN ('6720', '6730')) --�����,������
             OR (ca.stateid IN ('6730') --������
             AND EXISTS
              (SELECT *
                      FROM consultapplydepartment cad
                     WHERE cad.consultapplysn = ca.consultapplysn
                       AND INSTR(v_deptids, cad.departmentcode) > 0))));
  END;

  /************************************************************************************/
  PROCEDURE usp_gethospitalinfo(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM hospitalinfo hi;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getieminfo(v_noofinpat INT,
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
      FROM iem_mainpage_basicinfo imb
     WHERE imb.noofinpat = v_noofinpat
       AND imb.valide = 1;
  
    --����˳�򲻿ɱ䣬����������
    --������Ϣ
    OPEN o_result FOR
      SELECT *
        FROM iem_mainpage_basicinfo
       WHERE iem_mainpage_no = v_infono
         AND valide = 1;
  
    --���
    OPEN o_result1 FOR
      SELECT *
        FROM iem_mainpage_diagnosis
       WHERE iem_mainpage_no = v_infono
         AND valide = 1
       ORDER BY order_value;
  
    --����
    /*    OPEN o_result2 FOR
    SELECT *
      FROM iem_mainpage_operation
     WHERE iem_mainpage_no = v_infono
       AND valide = 1;*/
  
    OPEN o_result2 FOR
    
      SELECT iem.iem_mainpage_operation_no,
             iem.iem_mainpage_no,
             iem.operation_code,
             iem.operation_date,
             iem.operation_name,
             u1.name execute_user1_Name,
             iem.execute_user1,
             u2.name execute_user2_Name,
             iem.execute_user2,
             u3.name execute_user3_Name,
             iem.execute_user3,
             dic.name anaesthesia_type_Name,
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
             iem.close_level,
             ua.name anaesthesia_user_Name,
             iem.anaesthesia_user,
             iem.valide,
             iem.create_user,
             iem.create_time,
             iem.cancel_user,
             iem.cancel_time
        FROM iem_mainpage_operation iem
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
        left join dictionary_detail dic
          on iem.anaesthesia_type_id = dic.detailid
         and dic.categoryid = '30'
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
  PROCEDURE usp_getinpatientfiling(v_deptcode      VARCHAR DEFAULT '',
                                   v_InpatName     VARCHAR DEFAULT '',
                                   v_datetimebegin VARCHAR,
                                   v_datetimeend   VARCHAR,
                                   v_qcstattype    INT,
                                   o_result        OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������������δ�鵵����
     ����˵��
     �������
      v_DeptCode varchar(10)    --���ұ��
       v_NoOfInpat varchar(10) = '', --������ҳ���
      v_DateTimeBegin varchar(9) --��ʼʱ��
     ,  v_v_DateTimeEnd  varchar(9) --����ʱ��
     ,  v_QCStatType int --ͳ����������
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_GetInpatientFiling '','', '2011-02-24', '2011-03-24',1
     �޸ļ�¼
    **********/
  BEGIN
    IF v_qcstattype = 1 THEN
      --��Ժδ�鵵����
      OPEN o_result FOR
        SELECT --ROW_NUMBER() OVER(ORDER BY inp.noofinpat) AS rowid_,
        DISTINCT inp.outhosdept,
                 de.NAME        deptname,
                 inp.noofinpat  AS noofinpat,
                 inp.patnoofhis AS patnoofhis,
                 inp.patid      AS patid,
                 inp.NAME       AS NAME,
                 inp.sexid      AS sexid,
                 dd.NAME        AS sexname,
                 inp.agestr     AS agestr,
                 --TO_CHAR(inp.admitdate, 'yyyy-mm-dd') AS admitdate,
                 --TO_CHAR(inp.outhosdate, 'yyyy-mm-dd') AS outhosdate,
                 
                 --edit by cyq 2013-03-14
                 --to_date(inp.admitdate, 'yyyy-MM-dd hh24:mi:ss') AS admitdate,
                -- to_date(inp.outhosdate, 'yyyy-MM-dd hh24:mi:ss') AS outhosdate,
                
                substr(inp.admitdate,1,16) AS admitdate,
                substr(inp.outhosdate,1,16) AS outhosdate,
                
                 --trunc(to_date(inp.admitdate, 'yyyy-MM-dd hh24:mi:ss')) AS admitdate,
                 --trunc(to_date(inp.outhosdate, 'yyyy-MM-dd hh24:mi:ss')) AS outhosdate,
                 
                 --add by cyq 2013-03-14
                 --to_date(inp.inwarddate, 'yyyy-MM-dd hh24:mi:ss') AS inwarddate,
                -- to_date(inp.outwarddate, 'yyyy-MM-dd hh24:mi:ss') AS outwarddate,
                
                 substr(inp.inwarddate,1,16) AS inwarddate,
                  substr(inp.outwarddate,1,16) AS outwarddate,
                 1                 inhoscnt, ---סԺ����
                 residentuser.NAME residentname,
                 attenduser.NAME   attendname,
                 chiefuser.NAME    chiefname,
                 diag.NAME         admitdiag,
                 /*                 datediff('dd',
                          inp.admitdate,
                          NVL(trim(inp.outwarddate),
                              TO_CHAR(SYSDATE, 'yyyy-mm-dd'))) inhosdays,
                 NVL(datediff('dd',
                              trim(inp.outwarddate),
                              TO_CHAR(SYSDATE, 'yyyy-mm-dd')),
                     0) outhosdays,*/
                 
                 isday(datediff('dd',
                                inp.admitdate,
                                 NVL(trim(inp.outhosdate),
                                --NVL(trim(inp.outwarddate),
                                    TO_CHAR(SYSDATE, 'yyyy-mm-dd')))) inhosdays, --2013-6-24 zjy
                 isday(datediff('dd',
                               -- trim(inp.outwarddate), eidt by ywk
                                  trim(inp.outhosdate),
                                TO_CHAR(SYSDATE, 'yyyy-mm-dd'))) outhosdays, --2013-6-24 zjy
                 
                 inp.noofinpat NoOfInpat
          FROM inpatient inp
        --JOIN medicalrecord med ON inp.noofinpat = med.noofinpat
          LEFT JOIN recorddetail rd
            on rd.noofinpat = inp.noofinpat
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN users residentuser
            ON residentuser.ID = inp.resident
          LEFT JOIN users attenduser
            ON attenduser.ID = inp.attend
          LEFT JOIN users chiefuser
            ON chiefuser.ID = inp.chief
          LEFT JOIN department de
            ON de.ID = inp.outhosdept
          LEFT JOIN diagnosis diag
            ON diag.markid = inp.admitdiagnosis
         WHERE /*inp.status = 1503*/ --edit by ywk  2012��3��7��17:03:55
         inp.status in ('1502', '1503')
        --AND med.lockinfo IN (4700, 4702, 4703)
         AND (rd.islock IN (4700, 4702, 4703) or rd.islock is null)
        --AND TO_CHAR(inp.admitdate, 'yyyy-mm-dd') >= v_datetimebegin
        --AND TO_CHAR(inp.admitdate, 'yyyy-mm-dd') <= v_datetimeend
        
         AND trunc(to_date(inp.admitdate, 'yyyy-mm-dd hh24:mi:ss')) >=
         to_date(v_datetimebegin || ' 00:00:00', 'yyyy-mm-dd hh24:mi:ss')
         AND trunc(to_date(inp.admitdate, 'yyyy-mm-dd hh24:mi:ss')) <=
         to_date(v_datetimeend || ' 23:59:59', 'yyyy-mm-dd hh24:mi:ss')
        
         AND (TO_CHAR(outhosdept) = v_deptcode OR v_deptcode = '' OR
         v_deptcode IS NULL)
         AND (inp.patid = v_InpatName or v_InpatName = '' or
         v_InpatName is null)
        
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 2 THEN
      --�鵵����
      OPEN o_result FOR
        SELECT --ROW_NUMBER() OVER(ORDER BY inp.noofinpat) AS rowid_,
        distinct inp.outhosdept,
                 de.NAME        deptname,
                 inp.noofinpat  AS noofinpat,
                 inp.patnoofhis AS patnoofhis,
                 inp.patid      AS patid,
                 inp.NAME       AS NAME,
                 inp.sexid      AS sexid,
                 dd.NAME        AS sexname,
                 inp.agestr     AS agestr,
                 --TO_CHAR(inp.admitdate, 'yyyy-mm-dd') AS admitdate,
                 --TO_CHAR(inp.outhosdate, 'yyyy-mm-dd') AS outhosdate,
                 
                 --edit by cyq 2013-03-14
                 to_date(inp.admitdate, 'yyyy-MM-dd hh24:mi:ss') AS admitdate,
                 to_date(inp.outhosdate, 'yyyy-MM-dd hh24:mi:ss') AS outhosdate,
                 --trunc(to_date(inp.admitdate, 'yyyy-MM-dd hh24:mi:ss')) AS admitdate,
                 --trunc(to_date(inp.outhosdate, 'yyyy-MM-dd hh24:mi:ss')) AS outhosdate,
                 
                 --add by cyq 2013-03-14
                 to_date(inp.inwarddate, 'yyyy-MM-dd hh24:mi:ss') AS inwarddate,
                 to_date(inp.outwarddate, 'yyyy-MM-dd hh24:mi:ss') AS outwarddate,
                 
                 1 inhoscnt, ---סԺ����
                 residentuser.NAME residentname,
                 attenduser.NAME attendname,
                 chiefuser.NAME chiefname,
                 diag.NAME admitdiag,
                 datediff('dd',
                          inp.admitdate,
                          NVL(trim(inp.outwarddate),
                              TO_CHAR(SYSDATE, 'yyyy-mm-dd'))) inhosdays,
                 NVL(datediff('dd',
                              inp.outwarddate,
                              TO_CHAR(SYSDATE, 'yyyy-mm-dd')),
                     0) outhosdays,
                 inp.noofinpat NoOfInpat
          FROM inpatient inp
        --JOIN medicalrecord med ON inp.noofinpat = med.noofinpat
          LEFT JOIN recorddetail rd
            on rd.noofinpat = inp.noofinpat
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN users residentuser
            ON residentuser.ID = inp.resident
          LEFT JOIN users attenduser
            ON attenduser.ID = inp.attend
          LEFT JOIN users chiefuser
            ON chiefuser.ID = inp.chief
          LEFT JOIN department de
            ON de.ID = inp.outhosdept
          LEFT JOIN diagnosis diag
            ON diag.mapid = inp.admitdiagnosis
         WHERE inp.status = 1503
              --AND med.lockinfo = 4701
           AND rd.islock = '4701'
              --AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
              --AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
              
           AND trunc(to_date(inp.admitdate, 'yyyy-mm-dd hh24:mi:ss')) >=
               to_date(v_datetimebegin || ' 00:00:00',
                       'yyyy-mm-dd hh24:mi:ss')
           AND trunc(to_date(inp.admitdate, 'yyyy-mm-dd hh24:mi:ss')) <=
               to_date(v_datetimeend || ' 23:59:59',
                       'yyyy-mm-dd hh24:mi:ss')
              
           AND (TO_CHAR(outhosdept) = v_deptcode OR v_deptcode = '' OR
               v_deptcode IS NULL)
           AND (TO_CHAR(inp.noofinpat) = v_InpatName OR v_InpatName = '' OR
               v_InpatName IS NULL)
         ORDER BY inp.noofinpat;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getinpatientreport(v_noofinpat     VARCHAR DEFAULT '',
                                   v_datetimebegin VARCHAR DEFAULT '',
                                   v_datetimeend   VARCHAR DEFAULT '',
                                   v_submitdocid   VARCHAR DEFAULT '',
                                   v_reportid      VARCHAR DEFAULT '',
                                   v_qcstattype    VARCHAR DEFAULT '',
                                   o_result        OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ���ݴ����ʱ����Լ�������ҳ���  ��ѯ����ҽ��������Ϣ
     ����˵��
     �������
     v_NoOfInpat  varchar(10) = '', --������ҳ���
     v_DateTimeBegin  varchar(10) = '', --��ʼʱ��
     v_DateTimeEnd  varchar(10) = '', --����ʱ��
     v_SubmitDocID  varchar(10) = '', --�ͼ�ҽ��
     v_ReportID   varchar(12) = '', --����Ψһ��ʾ�У���ѯ�ӱ���Ϣʱ��ʹ�ã�
     v_QCStatType  varchar(3)  = '' --����   Ĭ�ϲ�ѯ������Ϣ   LIS����ѯ�������Ϣ  RIS����ѯ��������Ϣ
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
    usp_GetInpatientReport '62','','','','',''
    
     �޸ļ�¼
    **********/
  BEGIN
    -----��ѯ������Ϣ
    IF v_qcstattype IS NULL THEN
      OPEN o_result FOR
        SELECT report.hospitalno,
               report.noofinpat,
               inp.NAME,
               report.reportcatalog,
               report.reporttype,
               reportno,
               reportname,
               users.NAME           AS submitdocname,
               report.submitdate,
               report.releasedate,
               report.hadread,
               report.ID            reportid
          FROM inpatientreport report
          LEFT JOIN inpatient inp
            ON report.noofinpat = inp.noofinpat
          LEFT JOIN users users
            ON TO_CHAR(report.submitdocid) = TO_CHAR(users.ID)
         WHERE (TO_CHAR(inp.noofinpat) = v_noofinpat OR v_noofinpat = '' OR
               v_noofinpat IS NULL)
           AND TO_CHAR(TO_DATE(SUBSTR(report.submitdate, 1, 10),
                               'yyyy-mm-dd'),
                       'yyyy-mm-dd') >= v_datetimebegin
           AND TO_CHAR(TO_DATE(SUBSTR(report.submitdate, 1, 10),
                               'yyyy-mm-dd'),
                       'yyyy-mm-dd') <= v_datetimeend
           AND (TO_CHAR(report.submitdocid) = v_submitdocid OR
               v_submitdocid = '' OR v_submitdocid IS NULL);
      -----��ѯRIS��Ϣ
    ELSIF v_qcstattype = 'RIS' THEN
      OPEN o_result FOR
        SELECT (SELECT NAME FROM hospitalinfo WHERE ROWNUM <= 1) hospname,
               '��' || inp.NAME || '��' || '��鱨��' reporttitle,
               report.reportno applyno,
               report.ID techno,
               '��鱨��' technoname,
               '' wardorregdesc,
               inp.NAME patname,
               de.NAME sex,
               inp.agestr age,
               inp.noofrecord hospno,
               dept.NAME applydeptname,
               inp.outbed bedno,
               inp.outhosward ward,
               diag.NAME lczd,
               '' applydoctor,
               TO_CHAR(TO_DATE(SUBSTR(report.submitdate, 1, 10),
                               'yyyy-mm-dd'),
                       'yyyy-mm-dd') exectime,
               '' execdoctor,
               '' instrument,
               risreslut.line,
               risreslut.itemname,
               risreslut.RESULT
          FROM inpatientreportrisreslut risreslut
          LEFT JOIN inpatientreport report
            ON risreslut.reportid = report.ID
          LEFT JOIN inpatient inp
            ON inp.noofinpat = report.noofinpat
          LEFT JOIN dictionary_detail de
            ON de.categoryid = '3'
           AND de.detailid = inp.sexid
          LEFT JOIN department dept
            ON dept.ID = inp.outhosdept
          LEFT JOIN diagnosis diag
            ON diag.icd = inp.admitdiagnosis
          LEFT JOIN users users
            ON TO_CHAR(users.ID) = TO_CHAR(report.submitdocid)
         WHERE TO_CHAR(risreslut.reportid) = v_reportid;
      -----��ѯLIS��Ϣ
    ELSIF v_qcstattype = 'LIS' THEN
      OPEN o_result FOR
        SELECT (SELECT NAME FROM hospitalinfo WHERE ROWNUM <= 1) reporttitle,
               report.reportno applyno,
               report.ID technodesc,
               inp.NAME patname,
               de.NAME sexdesc,
               inp.agestr agedesc,
               inp.noofrecord hospno,
               dept.NAME applydeptname,
               inp.outbed bedno,
               '' sampledesc,
               TO_CHAR(TO_DATE(SUBSTR(report.submitdate, 1, 10),
                               'yyyy-mm-dd'),
                       'yyyy-mm-dd') receivetime,
               '' sampletime,
               diag.NAME clinicdesc,
               '' sampledescdesc,
               '' patpropnodesc,
               inp.outhosward warddesc,
               users.NAME todocname,
               '' execdocname,
               '' exectime,
               '' reporttime,
               '' verifiername,
               TO_CHAR(TO_DATE(SUBSTR(report.releasedate, 1, 10),
                               'yyyy-mm-dd'),
                       'yyyy-mm-dd') pubtime,
               '' comment_,
               report.reportname,
               lisreslut.line,
               lisreslut.itemname,
               lisreslut.RESULT,
               lisreslut.refervalue refer,
               lisreslut.unit,
               lisreslut.resultcolor
          FROM inpatientreportlisreslut lisreslut
          LEFT JOIN inpatientreport report
            ON lisreslut.reportid = report.ID
          LEFT JOIN inpatient inp
            ON inp.noofinpat = report.noofinpat
          LEFT JOIN dictionary_detail de
            ON de.categoryid = '3'
           AND de.detailid = inp.sexid
          LEFT JOIN department dept
            ON dept.ID = inp.outhosdept
          LEFT JOIN diagnosis diag
            ON diag.icd = inp.admitdiagnosis
          LEFT JOIN users users
            ON TO_CHAR(users.ID) = TO_CHAR(report.submitdocid)
         WHERE TO_CHAR(lisreslut.reportid) = v_reportid
         ORDER BY lisreslut.line;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getjobpermissioninfo(v_jobid  VARCHAR,
                                     o_result OUT empcurtyp) AS
    /**********
     �汾��
     ����ʱ��
     ����
     ��Ȩ
     ����
     ����˵��      ������и�λ��Ϣ
     �������
     �����������
     ���õ�sp
     ����ʵ��
    **********/
  BEGIN
    OPEN o_result FOR
      SELECT * FROM job2permission WHERE ID = v_jobid;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getknowledgepublicinfo(v_ordertype NUMERIC, --�������
                                       o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �༭������Ϣ
     ����˵��
     �������
     �������
     �����������
    
     exec usp_GetKnowledgePublicInfo v_OrderType=
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    OPEN o_result FOR
      SELECT node, title, parentnode, CONTEXT, ordervalue, ordertype
        FROM knowledgepublic
       WHERE ordertype = v_ordertype
         AND valid = 1
       ORDER BY node, ordervalue;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getlookupeditordata(v_querytype NUMERIC, --��ѯ������
                                    v_queryid   NUMERIC DEFAULT 0,
                                    o_result    OUT empcurtyp) AS /**********
                                                            �汾��  1.0.0.0.0
                                                            ����ʱ��
                                                            ����
                                                            ��Ȩ
                                                            ����  ��ȡ��Ҫ��lookupeditor����ʾ������,��ð���ID��Name,Py,Memo������������APP�����ʱ��ͳһ�ķ���
                                                            ����˵��
                                                            �������
                                                            �������
                                                            �����������
    
                                                            ���õ�sp
                                                            ����ʵ��
    
                                                            �޸ļ�¼
                                                            **********/
  BEGIN
    IF v_querytype = 1 THEN
      --��������(�� ҽ�Ƹ��ʽ)
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '1'
           AND valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 2 THEN
      --�����Ա�
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '3'
           AND valid = 1
         ORDER BY DETAILID;
    ELSIF v_querytype = 3 THEN
      --����״��
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '4'
           AND valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 4 THEN
      --ְҵ����
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '41'
           AND valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 5 THEN
      --ʡ�д���
      OPEN o_result FOR
        SELECT ID, NAME, py, memo
          FROM areas
         WHERE CATEGORY = 1000
         ORDER BY ID;
    ELSIF v_querytype = 6 THEN
      --�������
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '42'
           AND valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 7 THEN
      --��������
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '43'
           AND valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 8 THEN
      --��ϵ��ϵ
      OPEN o_result FOR
        SELECT detailid AS ID, NAME, py, memo
          FROM dictionary_detail
         WHERE categoryid = '44'
           AND valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 9 THEN
      --�ٴ�����
      OPEN o_result FOR
        SELECT ID, NAME, py, memo
          FROM department dept
         WHERE valid = 1
           AND EXISTS
         (SELECT 1 FROM dept2ward WHERE deptid = dept.ID)
         ORDER BY ID;
    ELSIF v_querytype = 10 THEN
      --�ٴ�����
      OPEN o_result FOR
      --SELECT ID, NAME, py, memo FROM ward WHERE valid = 1 ORDER BY ID;
        select a.ID, a.NAME, a.py, a.wb, b.deptid
          from ward a, dept2ward b
         where a.id = b.wardid
           and a.valid = '1'
         order by ID;
    ELSIF v_querytype = 11 THEN
      --������Ա������
      OPEN o_result FOR
        SELECT ID, NAME, py, memo FROM users WHERE valid = 1 ORDER BY ID;
    ELSIF v_querytype = 12 THEN
      --��ϣ�����
      OPEN o_result FOR
      /*SELECT icd AS ID, NAME, py, WB, memo, icd
                      FROM diagnosis
                     WHERE valid = 1
                     ORDER BY ID;*/
        SELECT icd AS ID, NAME, py, WB, memo, icd
          FROM diagnosis
         WHERE valid = 1
        union
        select diagnosis.icd as ID,
               diagnosisothername.name as Name,
               diagnosisothername.py,
               diagnosisothername.wb,
               '' as memo,
               diagnosis.icd as ICD
          from diagnosisothername
          left join diagnosis
            on diagnosis.icd = diagnosisothername.icdid
         where diagnosisothername.valid = '1'
         ORDER BY ID;
    ELSIF v_querytype = 13 THEN
      --���ش���
      OPEN o_result FOR
        SELECT ID, NAME, py, memo, parentid, parentname
          FROM areas
         WHERE CATEGORY = 1001
         ORDER BY ID;
    ELSIF v_querytype = 14 THEN
      --����ʽ,��Ҫ�޸�
      OPEN o_result FOR
      /*  SELECT detailid AS ID, NAME, py, memo
                                        FROM dictionary_detail
                                       WHERE categoryid = '30'
                                         AND valid = 1
                                       ORDER BY ID;*/
      --edit by ywk 2012��4��18��10:12:07
        select ID, NAME, py, wb
          from anesthesia
         where valid = 1
         order by ID desc;
    
    ELSIF v_querytype = 15 THEN
      --�п����ϵȼ�,��Ҫ�޸�
      OPEN o_result FOR
        SELECT 1 AS ID, 'I/��' NAME, 'yj' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 2 AS ID, 'II/��' NAME, 'ej' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 3 AS ID, 'III/��' NAME, 'sj' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 4 AS ID, 'I/��' NAME, 'yy' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 5 AS ID, 'II/��' NAME, 'ey' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 6 AS ID, 'III/��' NAME, 'sy' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 7 AS ID, 'I/��' NAME, 'yb' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 8 AS ID, 'II/��' NAME, 'eb' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 9 AS ID, 'III/��' NAME, 'sb' py, NULL memo
          FROM DUAL
         ORDER BY ID;
    ELSIF v_querytype = 16 THEN
      --
      OPEN o_result FOR
        SELECT ID, NAME, py, memo
          FROM categorydetail
         WHERE categoryid = v_queryid
         ORDER BY ID;
      --�����ж���
    elsif v_querytype = 17 then
    
      OPEN o_result FOR
        select a.id, a.name, a.py, a.memo from Toxicosis a order by id;
    
    elsif v_querytype = 18 then
      open o_result for
        SELECT 1 AS ID, 'һ������' NAME, 'yjss' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 2 AS ID, '��������' NAME, 'ejss' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 3 AS ID, '��������' NAME, 'sjss' py, NULL memo
          FROM DUAL
        UNION ALL
        SELECT 4 AS ID, '�ļ�����' NAME, 'sjss' py, NULL memo FROM DUAL;
    
    ELSIF v_querytype = 19 THEN
      --��ϣ����� ��ҽ
      OPEN o_result FOR
        select ID, NAME, py, memo
          from diagnosisofchinese
         WHERE valid = 1
        union
        select diagnosisofchinese.id as ID,
               diagnosischiothername.name as Name,
               diagnosischiothername.py as PY,
               '' as memo
          from diagnosischiothername
          left join diagnosisofchinese
            on diagnosisofchinese.id = diagnosischiothername.icdid where diagnosisofchinese.valid=1
         ORDER BY ID;
      --����ȡ�ֵ����Ƿ� add by ywk
    ELSIF v_querytype = 55 THEN
    
      OPEN o_result FOR
        select detailid as ID, NAME, py, memo
          from dictionary_detail
         WHERE valid = 1
           and categoryid = '69' --ȥ��δ֪ѡ�� add by ywk 2012��7��20�� 09:08:53
         ORDER BY ID;
    
    ELSIF v_querytype = 20 THEN
      --����������
      OPEN o_result FOR
       /* select ID, NAME, py, memo
          from operation
         WHERE valid = 1
         ORDER BY ID;*/
           select ID, NAME, py, memo
          from operation
         WHERE valid = 1
         union
           select operation.id as ID,operothername.name as Name,operothername.py as PY,'' as memo
          from operothername left join operation on operation.id=operothername.icdid where operation.valid=1  ORDER BY ID;
      --add by wyt 2012-08-27
    ELSIF v_querytype = 30 THEN
      --������Դ
      OPEN o_result FOR
        select detailid ID, NAME, py, memo
          from dictionary_detail
         WHERE categoryid = '2'
           and valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 31 THEN
      --�����벡����Ӧ
      OPEN o_result FOR
        select dept2ward.deptid,
               dept2ward.wardid,
               department.name  deptname,
               ward.name        wardname
          from dept2ward, department, ward
         WHERE department.id = deptid
           and ward.id = wardid
         ORDER BY deptid;
    ELSIF v_querytype = 32 THEN
      --����
      OPEN o_result FOR
        SELECT ID,
               NAME,
               py,
               wb,
               (CASE sort
                 WHEN 101 THEN
                  '�ٴ�'
                 WHEN 102 THEN
                  'ҽ��'
                 WHEN 103 THEN
                  'ҩ��'
                 WHEN 104 THEN
                  '����'
                 WHEN 105 THEN
                  '����'
                 ELSE
                  ''
               END) as sort
          FROM department dept
        /*          if sort = '101' then sort = '�ٴ�';
        elsif  sort = '102' then sort = 'ҽ��';
        elsif  sort = '103' then sort = 'ҩ��';
        elsif  sort = '104' then sort = '����';
        elsif  sort = '105' then sort = '����';
        end if;*/
         where valid = 1
         ORDER BY ID;
    ELSIF v_querytype = 33 THEN
      --����
      OPEN o_result FOR
        select a.ID, a.NAME, a.py, a.wb
          from ward a
         where a.valid = 1
         order by ID;
    END IF;
  END;

  /*  \*********************add by wyt 2012-08-16************************************************************\
  PROCEDURE usp_getmedicalrrecordviewnew(v_deptcode      VARCHAR DEFAULT '',
                                      v_datetimebegin VARCHAR,
                                      v_datetimeend   VARCHAR,
                                      v_datetimeinbegin VARCHAR,
                                      v_datetimeinend   VARCHAR,
                                      v_patientname   VARCHAR DEFAULT '',
                                      v_recordid      VARCHAR DEFAULT '',
                                      v_applydoctor   VARCHAR DEFAULT '',
                                      v_qcstattype    INT,
                                      v_patid  VARCHAR DEFAULT '',
                                      v_indiag    VARCHAR DEFAULT '', --��Ժ���
                                      v_outdiag    VARCHAR DEFAULT '', --��Ժ���
                                      v_curdiag    VARCHAR DEFAULT '', --��ǰ���
                                      o_result OUT empcurtyp) AS
    \**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �����һ���Ҷ�Ӧ���˵Ĺ鵵��δ�鵵���Ӳ���
     ����˵��
     �������
      v_DeptCode varchar(10)=''    --���ұ��
      v_DateTimeBegin varchar(9)       --��ʼʱ��
       v_v_DateTimeEnd  varchar(9)       --����ʱ��
       v_PatientName  varchar(20)='',   --��������
        v_RecordID  varchar(20)='',      --����
       v_QCStatType int,                 --ͳ���������ͣ�1���ѹ鵵��2���������
       v_ApplyDoctor     varchar(6) ,        --����ҽʦ����
  
       --Add wwj ����ģ����ѯ
       v_PatID           varchar(32),       --������
     �������
     �����������
    ��������ͳ�����ݼ�
  
     ���õ�sp
     ����ʵ��
     exec usp_GetMedicalRrecordView '', '2011-01-01', '2011-05-26','','','',1, ''
      exec usp_GetMedicalRrecordView '10', '', '','','','',3
      exec usp_GetMedicalRrecordView v_DeptCode='',v_DateTimeBegin='2001-03-01'
      ,v_DateTimeEnd='2011-03-10',v_QCStatType='1',v_PatientName='',v_RecordID='',v_ApplyDoctor=''
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-03-01',v_DateTimeEnd='2011-03-25'
      ,v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
  
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-03-01'
      ,v_DateTimeEnd='2011-04-27',v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
  
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-04-01'
      ,v_DateTimeEnd='2011-04-27',v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
  
     �޸ļ�¼
    **********\
    v_sql       VARCHAR(4000);
    v_where     VARCHAR(100) DEFAULT '';
    v_leftwhere VARCHAR(100) DEFAULT '';
    v_deptwhere VARCHAR(100) DEFAULT '';
  BEGIN
    IF  v_deptcode IS NOT NULL THEN
      v_where := 'and inp.OutHosDept=''' || v_deptcode || '''';
    END IF;
  
    IF v_patid IS NOT NULL THEN
      v_where := v_where || ' and inp.PatID like ' || '''%' || v_patid ||
                 '%''';
    END IF;
  
    IF v_patientname IS NOT NULL THEN
      v_where := v_where || ' and inp.Name like ' || '''%' || v_patientname ||
                 '%''';
    END IF;
  
    IF v_recordid IS NOT NULL THEN
      v_where := v_where || ' and inp.NoOfRecord=''' || v_recordid || '''';
    END IF;
  
    IF v_applydoctor IS NOT NULL THEN
      v_where := v_where || ' and ar.ApplyDoctor=''' || v_applydoctor || '''';
      --set v_LeftWhere=' and ar.ApplyDoctor='''+v_ApplyDoctor+''''
    END IF;
    IF v_qcstattype = 1 THEN
      --�����ѹ鵵��������
     OPEN o_result FOR
     SELECT DISTINCT ar.ApplyDate, --ROW_NUMBER() OVER (ORDER BY inp.NoOfInpat ASC) AS RowID_,
     inp.NoOfInpat AS NoOfInpat,
     inp.PatNoOfHis AS PatNoOfHis,
     inp.Name AS Name,
     dd.Name AS SexName,
     inp.InCount as InCount,
     inp.AgeStr AS AgeStr,
    inp.AdmitDate as AdmitDate,
    bed.WardId AS WardId,
    bed.DeptID AS DeptID,
    bed.ID  AS BedID,
    datediff('dd',inp.AdmitDate,nvl(trim(inp.OutWardDate),to_char(sysdate,'yyyy-mm-dd')))+1 inhosdays,
    inp.OutWardDate,
    CASE WHEN (SELECT COUNT (noofinpat) FROM recorddetail WHERE recorddetail.noofinpat = inp.noofinpat) > 0 THEN '�Ѿ���' ELSE 'δ����' END AS statusname
    ,inp.Resident,us3.Name as ResidentName
    ,inp.Attend,us1.Name as AttendName ,inp.Chief ,us2.Name as ChiefName,inp.Status
    ,inp.PatID RecordID --mr.ID as RecordID
    ,inp.NoOfRecord,inp.OutBed,inp.OutHosDept,de1.Name as DeptName,rd.islock
    ,ar. Status  as ApplyStatusID,cd1.Name as ApplyStatus
    FROM InPatient inp
    LEFT JOIN recorddetail rd ON inp.noofinpat = rd.noofinpat
    left join Dictionary_detail dd  ON dd.CategoryID = '3' AND inp.SexID = dd.DetailID
    left join Bed bed  ON inp.NoOfInpat = bed.NoOfInpat
    left join Ward ward  on bed.WardId = ward.ID
    left join Department de  on bed.DeptID = de.ID
    left join Users us1 on us1.ID=inp.Attend
    left join Users us2 on us2.ID=inp.Chief
    left join Users us3 on us3.ID=inp.Resident
    left join Department de1  on inp.OutHosDept = de1.ID
    --left join MedicalRecord mr on mr.NoOfInpat = inp.NoOfInpat
    --left join ApplyRecord ar on ar.NoOfInpat=inp.NoOfInpat
    left join CurrentDiag cur on cur.patient_id = inp.noofinpat
  
    --���������ļ�¼���õ��������ĵ�״̬
    left join ApplyRecord ar on ar.NoOfInpat=inp.NoOfInpat
    left join CategoryDetail cd1 on cd1.CategoryID = '52' and cd1.ID = ar. Status
  
    WHERE inp.Status IN  ('1502','1503')
  
    and rd.islock = '4701'
  
    and (inp.patid = v_patid or v_patid='' or v_patid is null)
  
    and (inp.admitdiagnosis = v_indiag or v_indiag='' or v_indiag is null)
  
    and (inp.outdiagnosis = v_outdiag or v_outdiag='' or v_outdiag is null)
  
    and (cur.diag_code = v_curdiag or v_curdiag='' or v_curdiag is null)
  
    and to_date(substr(nvl(trim(inp.OutWardDate),'1990-01-01'),1,10) , 'yyyy-mm-dd') >=  to_date(
               v_datetimebegin, 'yyyy-mm-dd')
    and to_date(substr(nvl(trim(inp.OutWardDate),'1990-01-01'),1,10) , 'yyyy-mm-dd') <=  to_date(
               v_datetimeend, 'yyyy-mm-dd')
    and to_date(substr(nvl(trim(inp.inwarddate),'1990-01-01'),1,10) , 'yyyy-mm-dd') >=  to_date(
               v_datetimeinbegin, 'yyyy-mm-dd')
    and to_date(substr(nvl(trim(inp.inwarddate),'1990-01-01'),1,10) , 'yyyy-mm-dd') <=  to_date(
               v_datetimeinend, 'yyyy-mm-dd')
     || v_where
  
     --' || v_where || '
     order by inp.NoOfInpat;
  
    ELSIF v_qcstattype = 2 THEN
      --������Ļ��߲���,�Ѿ��鵵�Ĳ���
    OPEN o_result FOR
    SELECT DISTINCT --ROW_NUMBER() OVER (ORDER BY inp.NoOfInpat ASC) AS RowID_,
    inp.NoOfInpat AS NoOfInpat,
    inp.PatNoOfHis AS PatNoOfHis,
    inp.PatID AS PatID,
    de.Name as DeptName,
    inp.Name AS Name,
    dd.Name AS SexName,
    inp.InCount as InCount,
    inp.AgeStr AS AgeStr,
    inp.AdmitDate as AdmitDate,
    inp.OutWardDate,
    inp.OutHosDept AS DeptID,
    ds.Name as DiagnosisName,
    inp.PatID RecordID, --mr.ID as RecordID
    inp.NoOfRecord
    FROM InPatient inp
    LEFT JOIN recorddetail rd ON inp.noofinpat = rd.noofinpat
    left join Dictionary_detail dd  ON dd.CategoryID = '3' AND inp.SexID = dd.DetailID
    left join Bed bed  ON inp.NoOfInpat = bed.NoOfInpat
    left join Ward ward  on bed.WardId = ward.ID
    left join Department de  on inp.OutHosDept = de.ID
    left join Diagnosis  ds on inp.OutDiagnosis=ds.MarkId
    left join MedicalRecord mr on mr.NoOfInpat = inp.NoOfInpat
    WHERE inp.Status IN  ('1502','1503')
               AND rd.islock ='4701'  order by inp.NoOfInpat;
               --|| v_where;
    END IF;
  END;
  */

  PROCEDURE usp_getmedicalrrecordviewnew(v_deptcode        VARCHAR DEFAULT '',
                                         v_datetimebegin   VARCHAR,
                                         v_datetimeend     VARCHAR,
                                         v_datetimeinbegin VARCHAR,
                                         v_datetimeinend   VARCHAR,
                                         v_patientname     VARCHAR DEFAULT '',
                                         v_recordid        VARCHAR DEFAULT '',
                                         v_applydoctor     VARCHAR DEFAULT '',
                                         v_qcstattype      INT,
                                         --Add wwj ����ģ����ѯ
                                         v_patid   VARCHAR DEFAULT '',
                                         v_indiag  VARCHAR DEFAULT '', --��Ժ���
                                         v_outdiag VARCHAR DEFAULT '', --��Ժ���
                                         v_curdiag VARCHAR DEFAULT '', --��ǰ���
                                         o_result  OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �����һ���Ҷ�Ӧ���˵Ĺ鵵��δ�鵵���Ӳ���
     ����˵��
     �������
      v_DeptCode varchar(10)=''    --���ұ��
      v_DateTimeBegin varchar(9)       --��ʼʱ��
       v_v_DateTimeEnd  varchar(9)       --����ʱ��
       v_PatientName  varchar(20)='',   --��������
        v_RecordID  varchar(20)='',      --����
       v_QCStatType int,                 --ͳ���������ͣ�1���ѹ鵵��2���������
       v_ApplyDoctor     varchar(6) ,        --����ҽʦ����
    
       --Add wwj ����ģ����ѯ
       v_PatID           varchar(32),       --������
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_GetMedicalRrecordView '', '2011-01-01', '2011-05-26','','','',1, ''
      exec usp_GetMedicalRrecordView '10', '', '','','','',3
      exec usp_GetMedicalRrecordView v_DeptCode='',v_DateTimeBegin='2001-03-01'
      ,v_DateTimeEnd='2011-03-10',v_QCStatType='1',v_PatientName='',v_RecordID='',v_ApplyDoctor=''
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-03-01',v_DateTimeEnd='2011-03-25'
      ,v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
    
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-03-01'
      ,v_DateTimeEnd='2011-04-27',v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
    
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-04-01'
      ,v_DateTimeEnd='2011-04-27',v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
    
     �޸ļ�¼
    **********/
    v_sql       VARCHAR(4000);
    v_where     VARCHAR(1000) DEFAULT '';
    v_leftwhere VARCHAR(100) DEFAULT '';
    v_deptwhere VARCHAR(100) DEFAULT '';
  BEGIN
    IF v_deptcode IS NOT NULL THEN
      v_where := 'and inp.OutHosDept=''' || v_deptcode || '''';
    END IF;
  
    IF v_patid IS NOT NULL THEN
      v_where := v_where || ' and inp.PatID like ' || '''%' || v_patid ||
                 '%''';
    END IF;
  
    IF v_patientname IS NOT NULL THEN
      v_where := v_where || ' and inp.Name like ' || '''%' || v_patientname ||
                 '%''';
    END IF;
  
    IF v_recordid IS NOT NULL THEN
      v_where := v_where || ' and inp.NoOfRecord like ' || '''%' ||
                 v_recordid || '%''';
    END IF;
  
    IF v_indiag IS NOT NULL THEN
      v_where := v_where || ' and inp.admitdiagnosis=''' || v_indiag || '''';
    END IF;
  
    IF v_outdiag IS NOT NULL THEN
      v_where := v_where || ' and id.diagnosis_code=''' || v_outdiag || '''';
    END IF;
  
    IF v_curdiag IS NOT NULL THEN
      v_where := v_where || ' and cur.diag_code=''' || v_curdiag || '''';
    END IF;
  
    IF v_applydoctor IS NOT NULL THEN
      v_where := v_where || ' and ar.ApplyDoctor=''' || v_applydoctor || '''';
      --set v_LeftWhere=' and ar.ApplyDoctor='''+v_ApplyDoctor+''''
    END IF;
  
    IF v_qcstattype = '1' THEN
      --�����ѹ鵵��������
      v_sql := N' SELECT DISTINCT ar.ApplyDate, --ROW_NUMBER() OVER (ORDER BY inp.NoOfInpat ASC) AS RowID_,
      inp.NoOfInpat AS NoOfInpat,inp.PatNoOfHis AS PatNoOfHis
    ,inp.Name AS Name,inp.SexID,inp.ADMITDIAGNOSIS, id.diagnosis_code ,dd.Name AS SexName,inp.InCount as InCount, inp.AgeStr AS AgeStr
    ,inp.AdmitDate as AdmitDate, bed.WardId AS WardId, bed.DeptID AS DeptID, bed.ID  AS BedID
    ,datediff(''dd'',inp.AdmitDate,nvl(trim(inp.OutWardDate),to_char(sysdate,''yyyy-mm-dd'')))+1 inhosdays,inp.OutWardDate
    ,CASE WHEN (SELECT COUNT (noofinpat) FROM recorddetail WHERE recorddetail.noofinpat = inp.noofinpat) > 0 THEN ''�Ѿ���'' ELSE ''δ����'' END AS statusname
    ,inp.Resident,us3.Name as ResidentName
    ,inp.Attend,us1.Name as AttendName ,inp.Chief ,us2.Name as ChiefName,inp.Status
    ,inp.PatID RecordID --mr.ID as RecordID
    ,inp.NoOfRecord,inp.OutBed,inp.OutHosDept,de1.Name as DeptName,rd.islock
    ,ar. Status  as ApplyStatusID,cd1.Name as ApplyStatus
    FROM InPatient inp
    LEFT JOIN recorddetail rd ON inp.noofinpat = rd.noofinpat
    left join Dictionary_detail dd  ON dd.CategoryID = ''3'' AND inp.SexID = dd.DetailID
    left join Bed bed  ON inp.NoOfInpat = bed.NoOfInpat
    left join Ward ward  on bed.WardId = ward.ID
    left join Department de  on bed.DeptID = de.ID
    left join Users us1 on us1.ID=inp.Attend
    left join Users us2 on us2.ID=inp.Chief
    left join Users us3 on us3.ID=inp.Resident
    left join Department de1  on inp.OutHosDept = de1.ID
    left join CurrentDiag cur on cur.patient_id = inp.noofinpat
    --left join MedicalRecord mr on mr.NoOfInpat = inp.NoOfInpat
    --left join ApplyRecord ar on ar.NoOfInpat=inp.NoOfInpat

    --���������ļ�¼���õ��������ĵ�״̬
    left join ApplyRecord ar on ar.NoOfInpat=inp.NoOfInpat
    left join CategoryDetail cd1 on cd1.CategoryID = ''52'' and cd1.ID = ar. Status

    --zyx 2013-1-11 �õ���Ժ���ȡiem_mainpage_diagnosis_2012�е��ֶ�
      left join iem_mainpage_basicinfo_2012 ib  on ib.noofinpat = inp.noofinpat
      left join iem_mainpage_diagnosis_2012 id on id.iem_mainpage_no = ib.iem_mainpage_no
         and id.valide = 1
         and id.diagnosis_type_id <> 13

    WHERE inp.Status IN  (''1502'',''1503'')

    and rd.islock = ''4701''



    and to_date(substr(nvl(trim(inp.OutWardDate),''1990-01-01''),1,10) , ''yyyy-mm-dd'') >=  to_date(''' ||
               v_datetimebegin ||
               ''', ''yyyy-mm-dd'')
    and to_date(substr(nvl(trim(inp.OutWardDate),''1990-01-01''),1,10) , ''yyyy-mm-dd'') <=  to_date(''' ||
               v_datetimeend ||
               ''', ''yyyy-mm-dd'')

    and to_date(substr(nvl(trim(inp.inwarddate),''1990-01-01''),1,10) , ''yyyy-mm-dd'') >=  to_date(''' ||
               v_datetimeinbegin ||
               ''', ''yyyy-mm-dd'')
    and to_date(substr(nvl(trim(inp.inwarddate),''1990-01-01''),1,10) , ''yyyy-mm-dd'') <=  to_date(''' ||
               v_datetimeinend || ''', ''yyyy-mm-dd'')

     ' || v_where || '
     order by inp.NoOfInpat';
    
    ELSIF v_qcstattype = '2' THEN
      --������Ļ��߲���,�Ѿ��鵵�Ĳ���
      v_sql := N' SELECT DISTINCT --ROW_NUMBER() OVER (ORDER BY inp.NoOfInpat ASC) AS RowID_,
     inp.NoOfInpat AS NoOfInpat,inp.PatNoOfHis AS PatNoOfHis,inp.PatID AS PatID,de.Name as DeptName
    ,inp.Name AS Name,inp.SexID,dd.Name AS SexName,inp.InCount as InCount, inp.AgeStr AS AgeStr
    ,inp.AdmitDate as AdmitDate,inp.OutWardDate, inp.OutHosDept AS DeptID,ds.Name as DiagnosisName
    ,inp.PatID RecordID --mr.ID as RecordID
    ,inp.NoOfRecord
    FROM InPatient inp
    LEFT JOIN recorddetail rd ON inp.noofinpat = rd.noofinpat
    left join Dictionary_detail dd  ON dd.CategoryID = ''3'' AND inp.SexID = dd.DetailID
    left join Bed bed  ON inp.NoOfInpat = bed.NoOfInpat
    left join Ward ward  on bed.WardId = ward.ID
    left join Department de  on inp.OutHosDept = de.ID
    left join Diagnosis  ds on inp.OutDiagnosis=ds.MarkId
    left join MedicalRecord mr on mr.NoOfInpat = inp.NoOfInpat

     left join iem_mainpage_basicinfo_2012 ib  on ib.noofinpat = inp.noofinpat
      left join iem_mainpage_diagnosis_2012 id on id.iem_mainpage_no = ib.iem_mainpage_no
         and id.valide = 1
         and id.diagnosis_type_id <> 13

    WHERE inp.Status IN  (''1502'',''1503'')' || v_where ||
               ' AND rd.islock =''4701''  order by inp.NoOfInpat';
    END IF;
  
    OPEN o_result FOR v_sql;
  END;

  /**********edit by wyt 2012-08-16***********************************************************************/
  PROCEDURE usp_getmedicalrrecordview(v_deptcode      VARCHAR DEFAULT '',
                                      v_datetimebegin VARCHAR,
                                      v_datetimeend   VARCHAR,
                                      v_patientname   VARCHAR DEFAULT '',
                                      v_recordid      VARCHAR DEFAULT '',
                                      v_applydoctor   VARCHAR DEFAULT '',
                                      v_qcstattype    INT,
                                      --Add wwj ����ģ����ѯ
                                      v_patid   VARCHAR DEFAULT '',
                                      v_outdiag VARCHAR DEFAULT '',
                                      o_result  OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �����һ���Ҷ�Ӧ���˵Ĺ鵵��δ�鵵���Ӳ���
     ����˵��
     �������
      v_DeptCode varchar(10)=''    --���ұ��
      v_DateTimeBegin varchar(9)       --��ʼʱ��
       v_v_DateTimeEnd  varchar(9)       --����ʱ��
       v_PatientName  varchar(20)='',   --��������
        v_RecordID  varchar(20)='',      --����
       v_QCStatType int,                 --ͳ���������ͣ�1���ѹ鵵��2���������
       v_ApplyDoctor     varchar(6) ,        --����ҽʦ����
    
       --Add wwj ����ģ����ѯ
       v_PatID           varchar(32),       --������
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_GetMedicalRrecordView '', '2011-01-01', '2011-05-26','','','',1, ''
      exec usp_GetMedicalRrecordView '10', '', '','','','',3
      exec usp_GetMedicalRrecordView v_DeptCode='',v_DateTimeBegin='2001-03-01'
      ,v_DateTimeEnd='2011-03-10',v_QCStatType='1',v_PatientName='',v_RecordID='',v_ApplyDoctor=''
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-03-01',v_DateTimeEnd='2011-03-25'
      ,v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
    
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-03-01'
      ,v_DateTimeEnd='2011-04-27',v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
    
      exec usp_GetMedicalRrecordView v_DeptCode='3202',v_DateTimeBegin='2011-04-01'
      ,v_DateTimeEnd='2011-04-27',v_QCStatType='2',v_PatientName='',v_RecordID='',v_ApplyDoctor='00'
    
     �޸ļ�¼
    **********/
    v_sql       VARCHAR(4000);
    v_where     VARCHAR(4000) DEFAULT '';
    v_leftwhere VARCHAR(100) DEFAULT '';
    v_deptwhere VARCHAR(100) DEFAULT '';
  BEGIN
    IF v_deptcode IS NOT NULL THEN
      v_where := 'and inp.OutHosDept=''' || v_deptcode || '''';
    END IF;
  
    IF v_patid IS NOT NULL THEN
      v_where := v_where || ' and inp.PatID like ' || '''%' || v_patid ||
                 '%''';
    END IF;
  
    IF v_patientname IS NOT NULL THEN
      v_where := v_where || ' and inp.Name like ' || '''%' || v_patientname ||
                 '%''';
    END IF;
  
    IF v_recordid IS NOT NULL THEN
      v_where := v_where || ' and inp.NoOfRecord=''' || v_recordid || '''';
    END IF;
  
    IF v_applydoctor IS NOT NULL THEN
      v_where := v_where || ' and ar.ApplyDoctor=''' || v_applydoctor || '''';
      --set v_LeftWhere=' and ar.ApplyDoctor='''+v_ApplyDoctor+''''
    END IF;
  
    IF v_outdiag IS NOT NULL THEN
      v_where := v_where || ' and inp.Outdiagnosis=''' || v_outdiag || '''';
    END IF;
  
    IF v_qcstattype = '1' THEN
      --�����ѹ鵵��������(-- edit by cyq 2012-11-15 ���sexid��)
      v_sql := N' SELECT DISTINCT ar.ApplyDate, --ROW_NUMBER() OVER (ORDER BY inp.NoOfInpat ASC) AS RowID_,
      inp.NoOfInpat AS NoOfInpat,inp.PatNoOfHis AS PatNoOfHis
    ,inp.Name AS Name,inp.SexID,dd.Name AS SexName,inp.InCount as InCount, inp.AgeStr AS AgeStr
    ,inp.AdmitDate as AdmitDate, bed.WardId AS WardId, bed.DeptID AS DeptID, bed.ID  AS BedID
    ,datediff(''dd'',inp.AdmitDate,nvl(trim(inp.OutWardDate),to_char(sysdate,''yyyy-mm-dd'')))+1 inhosdays,inp.OutWardDate
    ,CASE WHEN (SELECT COUNT (noofinpat) FROM recorddetail WHERE recorddetail.noofinpat = inp.noofinpat) > 0 THEN ''�Ѿ���'' ELSE ''δ����'' END AS statusname
    ,inp.Resident,us3.Name as ResidentName
    ,inp.Attend,us1.Name as AttendName ,inp.Chief ,us2.Name as ChiefName,inp.Status
    ,inp.PatID RecordID --mr.ID as RecordID
    ,inp.NoOfRecord,inp.OutBed,inp.OutHosDept,inp.Outdiagnosis as DiagnosisCode,Diagnosis.Name as DIAGNOSISNAME,de1.Name as DeptName,rd.islock
    ,ar. Status  as ApplyStatusID,cd1.Name as ApplyStatus
    FROM InPatient inp
    LEFT JOIN recorddetail rd ON inp.noofinpat = rd.noofinpat
    left join Dictionary_detail dd  ON dd.CategoryID = ''3'' AND inp.SexID = dd.DetailID
    left join Bed bed  ON inp.NoOfInpat = bed.NoOfInpat
    left join Ward ward  on bed.WardId = ward.ID
    left join Department de  on bed.DeptID = de.ID
    left join Users us1 on us1.ID=inp.Attend
    left join Users us2 on us2.ID=inp.Chief
    left join Users us3 on us3.ID=inp.Resident
    left join Department de1  on inp.OutHosDept = de1.ID
    left join Diagnosis dia on inp.Outdiagnosis=dia.icd --add by cyq 2012-12-07
    --left join MedicalRecord mr on mr.NoOfInpat = inp.NoOfInpat
    --left join ApplyRecord ar on ar.NoOfInpat=inp.NoOfInpat

    --���������ļ�¼���õ��������ĵ�״̬
    left join ApplyRecord ar on ar.NoOfInpat=inp.NoOfInpat
    left join CategoryDetail cd1 on cd1.CategoryID = ''52'' and cd1.ID = ar. Status

    WHERE inp.Status IN  (''1502'',''1503'')

    and rd.islock = ''4701''

    and to_date(substr(nvl(trim(ar.ApplyDate),''1990-01-01''),1,10) , ''yyyy-mm-dd'') >=  to_date(''' ||
               v_datetimebegin ||
               ''', ''yyyy-mm-dd'')
    and to_date(substr(nvl(trim(ar.ApplyDate),''1990-01-01''),1,10) , ''yyyy-mm-dd'') <=  to_date(''' ||
               v_datetimeend || ''', ''yyyy-mm-dd'')

     ' || v_where || '
     order by inp.NoOfInpat';
    
    ELSIF v_qcstattype = '2' THEN
      --������Ļ��߲���,�Ѿ��鵵�Ĳ���(-- edit by cyq 2012-11-15 ���sexid��)
      v_sql := N' SELECT DISTINCT --ROW_NUMBER() OVER (ORDER BY inp.NoOfInpat ASC) AS RowID_,
     inp.NoOfInpat AS NoOfInpat,inp.PatNoOfHis AS PatNoOfHis,inp.PatID AS PatID,de.Name as DeptName
    ,inp.Name AS Name,inp.SexID,dd.Name AS SexName,inp.InCount as InCount, inp.AgeStr AS AgeStr
    ,inp.AdmitDate as AdmitDate,inp.OutWardDate, inp.OutHosDept AS DeptID,inp.OutDiagnosis as DiagnosisCode,ds.Name as DiagnosisName
    ,inp.PatID RecordID --mr.ID as RecordID
    ,inp.NoOfRecord
    FROM InPatient inp
    LEFT JOIN recorddetail rd ON inp.noofinpat = rd.noofinpat
    left join Dictionary_detail dd  ON dd.CategoryID = ''3'' AND inp.SexID = dd.DetailID
    left join Bed bed  ON inp.NoOfInpat = bed.NoOfInpat
    left join Ward ward  on bed.WardId = ward.ID
    left join Department de  on inp.OutHosDept = de.ID
    left join Diagnosis  ds on inp.OutDiagnosis=ds.MarkId
    left join MedicalRecord mr on mr.NoOfInpat = inp.NoOfInpat
    WHERE inp.Status IN  (''1502'',''1503'')' || v_where ||
               ' AND rd.islock =''4701''  order by inp.NoOfInpat';
    END IF;
  
    OPEN o_result FOR v_sql;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getmedicalrrecordviewfrm(v_gettype VARCHAR,
                                         --��ȡ�������ͣ�1�����ҡ�2���������Ŀ�ġ�3���������޵�λ��4����������
                                         o_result OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ����������ĵ��Ӳ�����Ϣ
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
    exec usp_GetMedicalRrecordViewFrm '1'
     �޸ļ�¼
    **********/
  BEGIN
    IF v_gettype = '1' THEN
      --��ѯ���� ȫԺ + �ٴ�����
      OPEN o_result FOR
        SELECT '0000' AS ID, 'ȫԺ' AS NAME, 'qy' as PY, 'wgbpf' as WB
          FROM DUAL
        UNION
        SELECT ID, NAME, PY, WB
          FROM department, dept2ward dw
         WHERE department.ID = dw.deptid
           and department.valid = 1
         ORDER BY ID;
    ELSIF v_gettype = '2' THEN
      --��ѯ����Ŀ��
      OPEN o_result FOR
        SELECT ID, NAME, PY, WB
          FROM categorydetail
         WHERE categoryid = '50'
         ORDER BY ID;
    ELSIF v_gettype = '3' THEN
      --����ʱ�䵥λ
      OPEN o_result FOR
        SELECT ID, NAME, PY, WB
          FROM categorydetail
         WHERE categoryid = '51'
         ORDER BY ID;
    ELSIF v_gettype = '4' THEN
      --��ѯ��������
      OPEN o_result FOR
        SELECT patid ID, NAME, PY, WB FROM inpatient ORDER BY ID;
    ELSIF v_gettype = '5' THEN
      --��ѯ������
      OPEN o_result FOR
        SELECT patid ID FROM inpatient ORDER BY ID;
    
    ELSIF v_gettype = '6' THEN
      --��ѯ��Ժ���
      OPEN o_result FOR
        SELECT inpatient.patid,
               inpatient.name,
               ADMITDIAGNOSIS  ICD,
               diagnosis.name  diagnosis
          FROM inpatient, diagnosis
         where inpatient.admitdiagnosis = diagnosis.icd;
    
    ELSIF v_gettype = '7' THEN
      --��ѯ��Ժ���
      OPEN o_result FOR
        SELECT inpatient.patid,
               inpatient.name,
               OUTDIAGNOSIS    ICD,
               diagnosis.name  diagnosis
          FROM inpatient, diagnosis
         where inpatient.OUTDIAGNOSIS = diagnosis.icd;
    
      /*   ELSIF v_gettype = '8' THEN
      --��ѯ��ǰ���
      OPEN o_result FOR
        SELECT
        patient_id patid,
        patient_name name,
        Diag_code ICD,
        Diag_content diagnosis
          FROM CurrentDiag;*/
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getnotesonnursinginfo(v_noofinpat    NUMERIC, --��ҳ���(סԺ��ˮ��)
                                      v_dateofsurvey VARCHAR, --���������ڣ���ʽ2010-01-01��
                                      o_result       OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������Ϣ����
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    --��ȡ������Ϣ����
    OPEN o_result FOR
      SELECT *
        FROM notesonnursing
       WHERE dateofsurvey = v_dateofsurvey
         AND noofinpat = v_noofinpat;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getnursingrecordbydate(v_noofinpat NUMERIC DEFAULT '0', --��ҳ���
                                       v_begindate VARCHAR, --��ʼʱ��
                                       v_enddate   VARCHAR, --��ֹʱ��
                                       o_result    OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT non.*,
             CASE
               WHEN non.wayofsurvey = '8800' THEN
                '8801'
               ELSE
                TO_CHAR(non.wayofsurvey)
             END testtype --������
        FROM notesonnursing non
       WHERE non.noofinpat = v_noofinpat
         AND to_char(to_date(v_begindate, 'yyyy-MM-dd'), 'yyyy-MM-dd') <=
             non.dateofsurvey
         AND non.dateofsurvey <=
             to_char(to_date(v_enddate, 'yyyy-MM-dd'), 'yyyy-MM-dd')
      /*AND to_date(v_begindate,'yyyy-MM-dd') <= to_date(non.dateofsurvey,'yyyy-MM-dd')
      AND to_date(non.dateofsurvey,'yyyy-MM-dd') <= to_date(v_enddate,'yyyy-MM-dd')*/
       ORDER BY non.dateofsurvey, TO_NUMBER(non.timeslot);
  END;

  /*********************************************************************************/
  PROCEDURE usp_getnursingrecordparam(v_frmtype VARCHAR, --�����������
                                      o_result  OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ����������Ŀ�������
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_frmtype = '1' THEN
      --��ȡ����������¼ʱ���
      OPEN o_result FOR
        SELECT configkey, VALUE
          FROM appcfg
         WHERE configkey IN ('VITALSIGNSRECORDTIME', 'MODIFYNURSINGINFO');
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getpatientbedinfobydate(v_noofinpat    NUMERIC DEFAULT '0', --��ҳ���
                                        v_allocatedate VARCHAR, --ָ����ʱ��
                                        o_result       OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT *
        FROM (SELECT bi.formerbedid, --������
                     d.NAME AS deptname, --��������
                     (SELECT hi.NAME FROM hospitalinfo hi) AS hospitalname
                FROM bedinfo bi
                LEFT OUTER JOIN department d
                  ON d.ID = bi.formerdeptid
               WHERE bi.noofinpat = v_noofinpat
                 AND bi.startdate <= v_allocatedate
               ORDER BY bi.startdate DESC) temp
       WHERE ROWNUM <= 1;
  END;

  /*********add by wyt 2012-08-15************************************************************************/
  PROCEDURE usp_getdiagicdbyname(v_name   varchar, --��������
                                 o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT diag.icd FROM diagnosis diag WHERE diag.name = v_name;
  END;

  /*********add by wyt 2012-08-15************************************************************************/
  PROCEDURE usp_getdiagnamebyicd(v_icd    varchar, --���ֱ��
                                 o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT ICD, NAME, PY, WB FROM diagnosis WHERE icd = v_icd;
  END;

  /**********************add by wyt 2012-08-15***********************************************************/
  PROCEDURE usp_getdiagbyattendphysician(v_userid varchar, --�û����
                                         o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT atd.relatedisease
        FROM ATTENDINGPHYSICIAN atd
       WHERE atd.id = v_userid;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getpatientinfoforthreemeas(v_noofinpat NUMERIC DEFAULT '0', --��ҳ���
                                           o_result    OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
    /*  SELECT dd.NAME AS gender,
                                 d.NAME AS dept_name,
                                 ip.admitbed,
                                 ip.NAME AS patient_name,
                                 ip.agestr,
                                 ip.admitdate,
                                 ip.patid,
                                 ip.outhosdate,
                                 ip.admitbed,
                                 ip.noofinpat
                            FROM inpatient ip
                            LEFT OUTER JOIN dictionary_detail dd ON dd.detailid = ip.sexid
                                                                AND dd.categoryid = '3'
                          --�Ա�
                            LEFT OUTER JOIN department d ON d.ID = ip.admitdept
                          --����
                           WHERE ip.noofinpat = v_noofinpat; --������ҳ��ŵõ����˵Ļ�����Ϣ*/
    --����������ݵ��޸ģ��Ʊ�ʹ�λ�İ󶨣�edit by ywk 2012��4��17��16:32:44
      SELECT dd.NAME AS gender,
             d.NAME AS dept_name,
             ip.outhosdept AS dept_id,
             ip.admitbed,
             ip.NAME AS patient_name,
             ip.agestr,
             ip.admitdate,
             ip.patid,
             ip.outhosdate,
             ip.admitbed,
             ip.outbed,
             ip.noofinpat,
             nvl(ip.inwarddate, ip.admitdate) inwarddate, --�������
             ip.isbaby, --�Ƿ���Ӥ�� add by ywk  2012��7��30�� 13:38:14
             ip.mother, --ĸ�׵�Noofinpat
             ip.birth as BirthDay,
             im.weight
        FROM inpatient ip
        LEFT OUTER JOIN dictionary_detail dd
          ON dd.detailid = ip.sexid
         AND dd.categoryid = '3'
      --�Ʊ�
      --LEFT OUTER JOIN department d ON d.ID = ip.admitdept
        LEFT OUTER JOIN department d
          ON d.ID = ip.outhosdept
        LEFT OUTER JOIN iem_mainpage_basicinfo_2012 im
          ON ip.noofinpat = im.noofinpat
      --����
       WHERE ip.noofinpat = v_noofinpat;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getrecordmanagefrm(v_frmtype VARCHAR,
                                   --�������ͣ�1n��������Ϣ����ά�����塢2n�����˲���ʷ��Ϣ����
                                   v_patnoofhis NUMERIC DEFAULT '0', --��ҳ���
                                   v_categoryid VARCHAR DEFAULT '',
                                   --(�ֵ�)������ ���򸸽ڵ���� ����ҳ���
                                   o_result OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ����������ؼ���ʼ������
     ����˵��
     �������
     �������
     �����������
    
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  BEGIN
    IF v_frmtype = '1' THEN
      --��ȡ�ֵ����Ϣ
      OPEN o_result FOR
        SELECT categoryid, CAST(ID AS VARCHAR(4)) AS detailid, NAME, py, wb
          FROM categorydetail
         WHERE categoryid = v_categoryid
           AND ID != '4701';
    ELSIF v_frmtype = '2' THEN
      --��ȡ��������
      OPEN o_result FOR
        SELECT * FROM operation ORDER BY NAME;--surgeryԭ����Ȼ������� edit by ywk 2013��9��15�� 15:25:10
    END IF;
  END;

  --��ȡ���ҽʦ��ѯ�ļ�¼
  /*PROCEDURE usp_getattendrecordnoonfile(v_dateOutHosBegin   VARCHAR, --��Ժ��ʼ����
                                   v_dateOutHosEnd     VARCHAR, --��Ժ��������
                                   v_dateInHosBegin   VARCHAR,--��Ժ��ʼ����
                                   v_dateInHosEnd   VARCHAR,--��Ժ��ֹ����
                                   v_deptid      VARCHAR, --���Ҵ���
                                   v_status      VARCHAR, --����״̬
                                   v_patientname VARCHAR DEFAULT '', --��������
                                   v_recordid    VARCHAR DEFAULT '', --������
                                   v_indiag    VARCHAR DEFAULT '', --��Ժ���
                                   v_outdiag    VARCHAR DEFAULT '', --��Ժ���
                                   v_curdiag    VARCHAR DEFAULT '', --��ǰ���
                                   o_result      OUT empcurtyp) AS
                                   v_ds varchar(15);
  BEGIN
     --��ȡδ�鵵����
     OPEN o_result FOR
       select distinct inp.noofinpat,
                       inp.patid,
                       inp.noofrecord,
                       inp.name,
                       /*inp.sexid,*/ --����SexName�ֶΣ����ֶο�ʡ  by XLB 2012-12-12
  /*                      inp.agestr,
   inp.incount,
   inp.admitdate,
   inp.outbed,
   inp.OutHosDept,
  /* inp.OutDiagnosis,*/ --��ǰ����ǰ����Ҫ by XLB 2012-12-12
  /*                     inp.Attend,
  inp.Chief,
  inp.Resident,
   inp.Status,
   dept.Name as OutHosDeptName,
   dd.Name as SexName,
   d.Name as DiagnosisName,  --edit by ywk   2012��12��4��9:16:07
   case
     when cd.Name is null then
      'δ�鵵'
     when cd.Name = '' then
      'δ�鵵'
     else
      cd.Name
   end LockInfoName,
   /*  datediff('yy',
              inp.Birth,
              to_char(sysdate, 'yyyy-mm-dd')) as InpAgeNum,*/
  /*                      datediff('dd',
                         inp.AdmitDate,
                         nvl(trim(inp.OutWardDate),
                             to_char(sysdate, 'yyyy-mm-dd'))) + 1 as InHosDay,
                datediff('dd',
                         nvl(trim(inp.OutWardDate),
                             to_char(sysdate, 'yyyy-mm-dd')),
                         to_char(sysdate, 'yyyy-mm-dd')) as OutHosDay,
                case
                  when (SELECT count(NoOfInpat)
                          FROM MedicalRecord
                         where MedicalRecord.NoOfInpat = inp.NoOfInpat) > 0 then
                   '�Ѿ���'
                  else
                   'δ����'
                end as StatusName,
                us3.Name as ResidentName,
                us1.Name as AttendName,
                us2.Name as ChiefName
                --,
                --mr.ID as RecordID,
                --b.ID     AS BedID,
                --b.WardId as WardId
  
  from (
  select InPatient.noofinpat,
                InPatient.patid,
                InPatient.noofrecord,
                InPatient.name,
                InPatient.sexid,
                InPatient.agestr,
                InPatient.incount,
                InPatient.admitdate,
                InPatient.outbed,
                InPatient.OutHosDept,
                InPatient.OutDiagnosis,
                InPatient.Attend,
                InPatient.Chief,
                InPatient.Resident,
                InPatient.OutWardDate,
                InPatient.admitdiagnosis,
                Inpatient.Status
                from InPatient
  where trim(InPatient.OutWardDate) is not null
   and trim(InPatient.inwarddate) is not null                 --Ϊ�˶Բ���״̬���в�ѯ��ɾ����InPatient.Status in ('1502', '1503') by XLB
    /*and InPatient.Name like '%' || v_patientname
    and InPatient.Name like '%' || v_patientname*/ --by ��� 2012-12-12
  /*         and InPatient.Name like '%' || v_patientname || '%'
       and to_date(substr(nvl(trim(InPatient.OutWardDate), sysdate), 1, 10),
                  'yyyy-mm-dd') >= to_date(v_dateOutHosBegin, 'yyyy-mm-dd')
      and to_date(substr(nvl(trim(InPatient.OutWardDate), '1990-01-01'), 1, 10),
                  'yyyy-mm-dd') < to_date(v_dateOutHosEnd, 'yyyy-mm-dd')
      and to_date(substr(nvl(trim(InPatient.inwarddate), sysdate), 1, 10),
                  'yyyy-mm-dd') >= to_date(v_dateInHosBegin, 'yyyy-mm-dd')
      and to_date(substr(nvl(trim(InPatient.inwarddate), '1990-01-01'), 1, 10),
                  'yyyy-mm-dd') < to_date(v_dateInHosEnd, 'yyyy-mm-dd')
      and InPatient.NoOfRecord like '%' || v_recordid || '%'
      and InPatient.OutHosDept like '%'||v_deptid||'%'
      ) inp
    left  join Department dept on dept.ID = inp.OutHosDept
   left  join Dictionary_detail dd on dd.CategoryID = '3'
                                   and dd.DetailID = inp.SexID
    left join recorddetail rd on rd.noofinpat = inp.NoOfInpat
   left  join CategoryDetail cd on cd.CategoryID = '47'
                                and cd.ID = rd.islock
   left join Users us1 on us1.ID = inp.Attend
   left  join Users us2 on us2.ID = inp.Chief
   left join Users us3 on us3.ID = inp.Resident
   left join iem_mainpage_basicinfo_2012 ib on ib.noofinpat=inp.noofinpat
   left  join iem_mainpage_diagnosis_2012 id on id.iem_mainpage_no=ib.iem_mainpage_no
   left   join Diagnosis d on d.ICD =id.diagnosis_code and d.valid=1 and id.valide=1 and id.diagnosis_type_id<>13--���ڳ�Ժ��ϵĻ�ȡӦ�ð��������õ����� edit by ywk 2012��12��4��9:16:59
  left  join PATDIAG pd on inp.noofinpat= pd.patient_id
    where
      (rd.islock IN (v_status) or rd.islock is null)
  
        and(id.diagnosis_code = v_outdiag or v_outdiag='' or v_outdiag is null)
        /* or (idd.diagnosis_code = v_outdiag or v_outdiag='' or v_outdiag is null))*/
  -- by cyq 2012-12-07
  /*         and id.diagnosis_type_id in ('7','8')
         --and (id.diagnosis_type_id in ('7','8') or id.diagnosis_type_id = '' or id.diagnosis_type_id is null)
         and (pd.diag_code = v_curdiag or v_curdiag='' or v_curdiag is null)
          and (inp.OutHosDept = v_deptid or v_deptid = '0000' or v_deptid = '' or v_deptid is null)
        and (inp.admitdiagnosis = v_indiag or v_indiag='' or v_indiag is null)
       order by InHosDay;
  END;
  */
  PROCEDURE usp_getattendrecordnoonfile(v_dateOutHosBegin VARCHAR, --��Ժ��ʼ����
                                        v_dateOutHosEnd   VARCHAR, --��Ժ��������
                                        v_dateInHosBegin  VARCHAR, --��Ժ��ʼ����
                                        v_dateInHosEnd    VARCHAR, --��Ժ��ֹ����
                                        v_deptid          VARCHAR, --���Ҵ���
                                        v_status          VARCHAR, --����״̬
                                        v_patientname     VARCHAR DEFAULT '', --��������
                                        v_recordid        VARCHAR DEFAULT '', --������
                                        v_indiag          VARCHAR DEFAULT '', --��Ժ���
                                        v_outdiag         VARCHAR DEFAULT '', --��Ժ���
                                        v_curdiag         VARCHAR DEFAULT '', --��ǰ���
                                        v_diaGroupStatus  VARCHAR, --��Ϸ���״̬
                                        v_PatientStatus   VARCHAR, --����״̬
                                        v_ingroupid       VARCHAR, --����ID1
                                        v_outgroupid      VARCHAR, --����ID2
                                        o_result          OUT empcurtyp) AS
    v_sql varchar(5000);
  BEGIN
    OPEN o_result FOR --v_sql;
      select distinct inp.noofinpat,
                      inp.patid,
                      inp.noofrecord,
                      inp.name,
                      inp.sexid,
                      inp.agestr,
                      inp.incount,
                      inp.admitdate,
                      inp.outwarddate, --Add by xlb ��Ժʱ�� 2013-06-19
                      inp.outbed,
                      inp.OutHosDept,
                      inp.OutDiagnosis,
                      inp.admitdiagnosis,
                      inp.Attend,
                      inp.Chief,
                      inp.Resident,
                      inp.Status,
                      dept.Name as OutHosDeptName,
                      dd.Name as SexName,
                      case
                        when cd.Name is null then
                         'δ�鵵'
                        when cd.Name = '' then
                         'δ�鵵'
                        else
                         cd.Name
                      end LockInfoName,
                      datediff('dd',
                               inp.AdmitDate,
                               --nvl(trim(inp.OutWardDate),
                               nvl(trim(inp.outhosdate), -- �˴��ó�Ժʱ���ȥ��Ժʱ��õ���Ժ����  2013-3-14 by tj
                                   to_char(sysdate, 'yyyy-mm-dd'))) + 1 as InHosDay,
                      datediff('dd',
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd')),
                               to_char(sysdate, 'yyyy-mm-dd')) as OutHosDay,
                      case
                        when (SELECT count(NoOfInpat)
                                FROM MedicalRecord
                               where MedicalRecord.NoOfInpat = inp.NoOfInpat) > 0 then
                         '�Ѿ���'
                        else
                         'δ����'
                      end as StatusName,
                      us3.Name as ResidentName,
                      us1.Name as AttendName,
                      us2.Name as ChiefName,
                      id.diagnosis_type_id,
                      id.order_value,
                      id.diagnosis_code,
                      diag.name AdmitDiagnosisName,
                      id.diagnosis_name DiagnosisName
        from InPatient inp
        left join diagnosis diag
          on diag.icd = inp.admitdiagnosis
        left join Department dept
          on dept.ID = inp.OutHosDept
        left join Dictionary_detail dd
          on dd.CategoryID = '3'
         and dd.DetailID = inp.SexID
        left join recorddetail rd
          on rd.noofinpat = inp.NoOfInpat
        left join CategoryDetail cd
          on cd.CategoryID = '47'
         and cd.ID = rd.islock
        left join Users us1
          on us1.ID = inp.Attend
        left join Users us2
          on us2.ID = inp.Chief
        left join Users us3
          on us3.ID = inp.Resident
        left join iem_mainpage_basicinfo_2012 ib
          on ib.noofinpat = inp.noofinpat
        left join iem_mainpage_diagnosis_2012 id
          on id.iem_mainpage_no = ib.iem_mainpage_no
         and id.valide = 1
         and id.diagnosis_type_id <> 13
        left join Diagnosis d
          on d.ICD = id.diagnosis_code
         and d.valid = 1
         and id.valide = 1
         and id.diagnosis_type_id <> 13
        left join PATDIAG pd
          on inp.noofinpat = pd.patient_id
       where trim(inp.inwarddate) is not null --Ϊ�˶Բ���״̬���в�ѯ��ɾ����InPatient.Status in ('1502', '1503') by XLB
         and inp.Name like '%' || v_patientname || '%'
         and to_date(substr(nvl(trim(inp.OutWardDate),
                                '9999-12-31 23:59:59'),
                            0,
                            19),
                     'yyyy-mm-dd hh24:mi:ss') >=
             to_date(v_dateOutHosBegin, 'yyyy-mm-dd hh24:mi:ss')
         and to_date(substr(nvl(trim(inp.OutWardDate),
                                '9999-12-31 23:59:59'),
                            0,
                            19),
                     'yyyy-mm-dd hh24:mi:ss') <=
             to_date(v_dateOutHosEnd, 'yyyy-mm-dd hh24:mi:ss')
         and to_date(substr(nvl(trim(inp.inwarddate), '1000-01-01 00:00:00'),
                            0,
                            19),
                     'yyyy-mm-dd hh24:mi:ss') >=
             to_date(v_dateInHosBegin, 'yyyy-mm-dd hh24:mi:ss')
         and to_date(substr(nvl(trim(inp.inwarddate), '1000-01-01 00:00:00'),
                            0,
                            19),
                     'yyyy-mm-dd hh24:mi:ss') <=
             to_date(v_dateInHosEnd, 'yyyy-mm-dd hh24:mi:ss')
         and inp.NoOfRecord like '%' || v_recordid || '%'
         and inp.OutHosDept like '%' || v_deptid || '%'
         and (rd.islock IN (v_status) or rd.islock is null)
         and (id.diagnosis_code = v_outdiag or v_outdiag = '' or
              v_outdiag is null) /**
                                                                                              or (idd.diagnosis_code = v_outdiag or v_outdiag = '' or
                                                                                                  v_outdiag is null)) **/
            -- by cyq 2012-12-07
         and id.diagnosis_type_id in ('7', '8')
            --and (id.diagnosis_type_id in ('7','8') or id.diagnosis_type_id = '' or id.diagnosis_type_id is null)
         and (pd.diag_code = v_curdiag or v_curdiag = '' or
              v_curdiag is null)
         and (inp.OutHosDept = v_deptid or v_deptid = '0000' or
              v_deptid = '' or v_deptid is null)
         and (inp.admitdiagnosis = v_indiag or v_indiag = '' or
              v_indiag is null)
         and ((v_PatientStatus = '1' and inp.Status in ('1500', '1501')) or
              (v_PatientStatus = '2' and inp.Status in ('1502', '1503')) or
              v_PatientStatus = '0') --����״̬
         and ((v_diaGroupStatus = '1' and
              ((inp.admitdiagnosis in
              (select a.COLUMN_VALUE
                    from table (select split_string(dis.diseaseids, '$')
                                  from diseasesgroup dis
                                 where dis.valid = 1
                                   and dis.id = v_ingroupid) a)) or
              v_ingroupid = '' or v_ingroupid is null) and
              
              ((id.diagnosis_code in
              (select a.COLUMN_VALUE
                    from table (select split_string(dis.diseaseids, '$')
                                  from diseasesgroup dis
                                 where dis.valid = 1
                                   and dis.id = v_outgroupid) a)) or
              v_outgroupid = '' or v_outgroupid is null)) or
              v_diaGroupStatus = '0')
      --order by InHosDay;
       order by inp.noofinpat, id.order_value;
    /*    --��ȡδ�鵵����  wangji ע�� 2013 1 21
    OPEN o_result FOR
      select distinct inp.noofinpat,
                      inp.patid,
                      inp.noofrecord,
                      inp.name,
                      inp.sexid,
                      inp.agestr,
                      inp.incount,
                      inp.admitdate,
                      inp.outbed,
                      inp.OutHosDept,
                      inp.OutDiagnosis,\* *\--��ǰ����ǰ����Ҫ by XLB 2012-12-12   by zyx 2013-1-11
                      inp.admitdiagnosis,
                      inp.Attend,
                      inp.Chief,
                      inp.Resident,
                      inp.Status,
                      dept.Name    as OutHosDeptName,
                      dd.Name      as SexName,
                      --d.Name as DiagnosisName,  --edit by ywk   2012��12��4��9:16:07
                      case
                        when cd.Name is null then
                         'δ�鵵'
                        when cd.Name = '' then
                         'δ�鵵'
                        else
                         cd.Name
                      end LockInfoName,
                      \*  datediff('yy',
                      inp.Birth,
                      to_char(sysdate, 'yyyy-mm-dd')) as InpAgeNum,*\
                      datediff('dd',
                               inp.AdmitDate,
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd'))) + 1 as InHosDay,
                      datediff('dd',
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd')),
                               to_char(sysdate, 'yyyy-mm-dd')) as OutHosDay,
                      case
                        when (SELECT count(NoOfInpat)
                                FROM MedicalRecord
                               where MedicalRecord.NoOfInpat = inp.NoOfInpat) > 0 then
                         '�Ѿ���'
                        else
                         'δ����'
                      end as StatusName,
                      us3.Name as ResidentName,
                      us1.Name as AttendName,
                      us2.Name as ChiefName,
                      id.diagnosis_type_id,
                      id.order_value,
                      id.diagnosis_code,
                      id.diagnosis_name DiagnosisName
      --,
      --mr.ID as RecordID,
      --b.ID     AS BedID,
      --b.WardId as WardId
    
        from (select InPatient.noofinpat,
                      InPatient.patid,
                      InPatient.noofrecord,
                      InPatient.name,
                      InPatient.sexid,
                      InPatient.agestr,
                      InPatient.incount,
                      InPatient.admitdate,
                      InPatient.outbed,
                      InPatient.OutHosDept,
                      InPatient.OutDiagnosis,
                      InPatient.Attend,
                      InPatient.Chief,
                      InPatient.Resident,
                      InPatient.OutWardDate,
                      InPatient.admitdiagnosis,
                      Inpatient.Status --��Ҫ���ж��β�ѯ XLB 2012-12-10
                 from InPatient
                where
                trim(InPatient.inwarddate) is not null --Ϊ�˶Բ���״̬���в�ѯ��ɾ����InPatient.Status in ('1502', '1503') by XLB
             and InPatient.Name like '%' || v_patientname || '%'
             and to_date(substr(nvl(trim(InPatient.OutWardDate), '2999-12-01'),
                               1,
                               10),
                        'yyyy-mm-dd') >=
                to_date(v_dateOutHosBegin, 'yyyy-mm-dd')
             and to_date(substr(nvl(trim(InPatient.OutWardDate), '1990-01-01'),
                               1,
                               10),
                        'yyyy-mm-dd') <
                to_date(v_dateOutHosEnd, 'yyyy-mm-dd')
             and to_date(substr(nvl(trim(InPatient.inwarddate), '2999-12-01'),
                               1,
                               10),
                        'yyyy-mm-dd') >=
                to_date(v_dateInHosBegin, 'yyyy-mm-dd')
             and to_date(substr(nvl(trim(InPatient.inwarddate), '1990-01-01'),
                               1,
                               10),
                        'yyyy-mm-dd') < to_date(v_dateInHosEnd, 'yyyy-mm-dd')
             and InPatient.NoOfRecord like '%' || v_recordid || '%'
             and InPatient.OutHosDept like '%' || v_deptid || '%') inp
        left join Department dept
          on dept.ID = inp.OutHosDept
        left join Dictionary_detail dd
          on dd.CategoryID = '3'
         and dd.DetailID = inp.SexID
        left join recorddetail rd
          on rd.noofinpat = inp.NoOfInpat
        left join CategoryDetail cd
          on cd.CategoryID = '47'
         and cd.ID = rd.islock
        left join Users us1
          on us1.ID = inp.Attend
        left join Users us2
          on us2.ID = inp.Chief
        left join Users us3
          on us3.ID = inp.Resident
        left join iem_mainpage_basicinfo_2012 ib
          on ib.noofinpat = inp.noofinpat
        left join iem_mainpage_diagnosis_2012 id
          on id.iem_mainpage_no = ib.iem_mainpage_no
         and id.valide = 1
         and id.diagnosis_type_id <> 13
        left join Diagnosis d
          on d.ICD = id.diagnosis_code
         and d.valid = 1
         and id.valide = 1
         and id.diagnosis_type_id <> 13 --���ڳ�Ժ��ϵĻ�ȡӦ�ð��������õ����� edit by ywk 2012��12��4��9:16:59
        left join PATDIAG pd
          on inp.noofinpat = pd.patient_id
       where (rd.islock IN (v_status) or rd.islock is null)
    
         and (id.diagnosis_code = v_outdiag or v_outdiag = '' or
             v_outdiag is null)
            \* or (idd.diagnosis_code = v_outdiag or v_outdiag='' or v_outdiag is null))*\
            -- by cyq 2012-12-07
         and id.diagnosis_type_id in ('7', '8')
            --and (id.diagnosis_type_id in ('7','8') or id.diagnosis_type_id = '' or id.diagnosis_type_id is null)
         and (pd.diag_code = v_curdiag or v_curdiag = '' or
             v_curdiag is null)
         and (inp.OutHosDept = v_deptid or v_deptid = '0000' or
             v_deptid = '' or v_deptid is null)
         and (inp.admitdiagnosis = v_indiag or v_indiag = '' or
             v_indiag is null)
       order by InHosDay;*/
  END;

  PROCEDURE usp_getrecordnoonfileWyt(v_dateOutHosBegin VARCHAR, --��Ժ��ʼ����
                                     v_dateOutHosEnd   VARCHAR, --��Ժ��������
                                     v_dateInHosBegin  VARCHAR, --��Ժ��ʼ����
                                     v_dateInHosEnd    VARCHAR, --��Ժ��ֹ����
                                     v_deptid          VARCHAR, --���Ҵ���
                                     v_indiag          VARCHAR, --��Ժ���
                                     v_outdiag         VARCHAR, --��Ժ���
                                     v_status          VARCHAR, --����״̬
                                     v_patientname     VARCHAR DEFAULT '', --��������
                                     v_patientid       VARCHAR DEFAULT '', --����ID
                                     v_recordid        VARCHAR DEFAULT '', --������
                                     v_physician       VARCHAR DEFAULT '', --סԺҽʦ����
                                     o_result          OUT empcurtyp) AS
  BEGIN
    --��ȡδ�鵵����
  
    OPEN o_result FOR
    
      select distinct rd.islock,
                      inp.*,
                      dept.Name as OutHosDeptName,
                      dd.Name as SexName,
                      d.Name as DiagnosisName,
                      case
                        when cd.Name is null then
                         'δ�鵵'
                        when cd.Name = '' then
                         'δ�鵵'
                        else
                         cd.Name
                      end LockInfoName,
                      datediff('yy',
                               inp.Birth,
                               to_char(sysdate, 'yyyy-mm-dd')) as InpAgeNum,
                      datediff('dd',
                               inp.AdmitDate,
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd'))) + 1 as InHosDay,
                      datediff('dd',
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd')),
                               to_char(sysdate, 'yyyy-mm-dd')) as OutHosDay,
                      case
                        when (SELECT count(NoOfInpat)
                                FROM MedicalRecord
                               where MedicalRecord.NoOfInpat = inp.NoOfInpat) > 0 then
                         '�Ѿ���'
                        else
                         'δ����'
                      end as StatusName,
                      us3.Name as ResidentName,
                      us1.Name as AttendName,
                      us2.Name as ChiefName,
                      b.ID AS BedID,
                      b.WardId as WardId
      
        from InPatient inp
        left join Department dept
          on dept.ID = inp.OutHosDept
        left join Dictionary_detail dd
          on dd.CategoryID = '3'
         and dd.DetailID = inp.SexID
        left join Diagnosis d
          on d.ICD = inp.OutDiagnosis
        left join recorddetail rd
          on rd.noofinpat = inp.NoOfInpat
         and rd.valid = 1 ---add by cyq 2013-05-06
        left join CategoryDetail cd
          on cd.CategoryID = '47'
         and cd.ID = rd.islock
        left join Bed b
          ON inp.NoOfInpat = b.NoOfInpat
        left join Users us1
          on us1.ID = inp.Attend
        left join Users us2
          on us2.ID = inp.Chief
        left join Users us3
          on us3.ID = inp.Resident
      
       where 1 = 1
            --edit by cyq 2012-12-07
            --and to_date(substr(nvl(trim(inp.OutWardDate), '1990-01-01'), 1, 10),'yyyy-mm-dd') >= to_date(v_dateOutHosBegin, 'yyyy-mm-dd')
            --and to_date(substr(nvl(trim(inp.OutWardDate), '1990-01-01'), 1, 10),'yyyy-mm-dd') < to_date(v_dateOutHosEnd, 'yyyy-mm-dd')
            --and to_date(substr(nvl(trim(inp.inwarddate), '1990-01-01'), 1, 10),'yyyy-mm-dd') >= to_date(v_dateInHosBegin, 'yyyy-mm-dd')
            --and to_date(substr(nvl(trim(inp.inwarddate), '1990-01-01'), 1, 10),'yyyy-mm-dd') < to_date(v_dateInHosEnd, 'yyyy-mm-dd')
         and to_date(nvl(trim(inp.OutWardDate), '1990-01-01 00:00:00'),
                     'yyyy-MM-dd hh24:mi:ss') >=
             to_date(v_dateOutHosBegin || ' 00:00:00',
                     'yyyy-MM-dd hh24:mi:ss')
         and to_date(nvl(trim(inp.OutWardDate), '1990-01-01 00:00:00'),
                     'yyyy-MM-dd hh24:mi:ss') <=
             to_date(v_dateOutHosEnd || ' 23:59:59',
                     'yyyy-MM-dd hh24:mi:ss')
         and to_date(nvl(trim(inp.inwarddate), '1990-01-01 00:00:00'),
                     'yyyy-MM-dd hh24:mi:ss') >=
             to_date(v_dateInHosBegin || ' 00:00:00',
                     'yyyy-MM-dd hh24:mi:ss')
         and to_date(nvl(trim(inp.inwarddate), '1990-01-01 00:00:00'),
                     'yyyy-MM-dd hh24:mi:ss') <=
             to_date(v_dateInHosEnd || ' 23:59:59', 'yyyy-MM-dd hh24:mi:ss')
            
         and trim(inp.OutWardDate) is not null
         and trim(inp.inwarddate) is not null
         and (inp.admitdiagnosis = v_indiag or v_indiag = '' or
             v_indiag is null)
         and (inp.outdiagnosis = v_outdiag or v_outdiag = '' or
             v_outdiag is null)
         and inp.Status in ('1502', '1503')
         and (inp.OutHosDept = v_deptid or v_deptid = '0000')
         and (rd.islock IN (v_status) or rd.islock is null) --edit by cyq 2012-12-04
         and inp.Name like '%' || v_patientname || '%' --edit by cyq 2012-12-04
            --and (inp.Name = v_patientname or v_patientname = '' or v_patientname is null)
         and (inp.noofinpat = v_patientid or v_patientid = '' or
             v_patientid is null)
         and inp.NoOfRecord like '%' || v_recordid || '%' --edit by cyq 2012-12-04
         and (inp.resident = v_physician or v_physician = '' or
             v_physician is null)
       order by OutWardDate;
  
  END;

  /*********************************************************************************/
  PROCEDURE usp_getrecordnoonfile(v_datebegin   VARCHAR, --��ʼ����
                                  v_dateend     VARCHAR, --��������
                                  v_deptid      VARCHAR, --���Ҵ���
                                  v_status      VARCHAR, --����״̬
                                  v_patientname VARCHAR DEFAULT '', --��������
                                  v_recordid    VARCHAR DEFAULT '', --������
                                  o_result      OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡδ�鵵����
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
     exec usp_GetRecordNoOnFileNew v_DateBegin='2011-04-01',v_DateEnd='2011-04-26',v_DeptID='0000',v_Status='4700,4702,4703'
    
     �޸ļ�¼
    **********/
  
  BEGIN
    --��ȡδ�鵵����
  
    OPEN o_result FOR
    
      select distinct rd.islock,
                      inp.*,
                      dept.Name as OutHosDeptName,
                      dd.Name as SexName,
                      d.Name as DiagnosisName,
                      case
                        when cd.Name is null then
                         'δ�鵵'
                        when cd.Name = '' then
                         'δ�鵵'
                        else
                         cd.Name
                      end LockInfoName,
                      datediff('yy',
                               inp.Birth,
                               to_char(sysdate, 'yyyy-mm-dd')) as InpAgeNum,
                      datediff('dd',
                               inp.AdmitDate,
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd'))) + 1 as InHosDay,
                      datediff('dd',
                               nvl(trim(inp.OutWardDate),
                                   to_char(sysdate, 'yyyy-mm-dd')),
                               to_char(sysdate, 'yyyy-mm-dd')) as OutHosDay,
                      case
                        when (SELECT count(NoOfInpat)
                                FROM MedicalRecord
                               where MedicalRecord.NoOfInpat = inp.NoOfInpat) > 0 then
                         '�Ѿ���'
                        else
                         'δ����'
                      end as StatusName,
                      us3.Name as ResidentName,
                      us1.Name as AttendName,
                      us2.Name as ChiefName,
                      --mr.ID as RecordID,
                      b.ID     AS BedID,
                      b.WardId as WardId
      
        from InPatient inp
        left join Department dept
          on dept.ID = inp.OutHosDept
        left join Dictionary_detail dd
          on dd.CategoryID = '3'
         and dd.DetailID = inp.SexID
        left join Diagnosis d
          on d.ICD = inp.OutDiagnosis
        left join recorddetail rd
          on rd.noofinpat = inp.NoOfInpat
        left join CategoryDetail cd
          on cd.CategoryID = '47'
         and cd.ID = rd.islock
        left join Bed b
          ON inp.NoOfInpat = b.NoOfInpat
        left join Users us1
          on us1.ID = inp.Attend
        left join Users us2
          on us2.ID = inp.Chief
        left join Users us3
          on us3.ID = inp.Resident
      
       where to_date(substr(nvl(trim(inp.OutWardDate), '1990-01-01'), 1, 10),
                     'yyyy-mm-dd') >= to_date(v_datebegin, 'yyyy-mm-dd')
         and to_date(substr(nvl(trim(inp.OutWardDate), '1990-01-01'), 1, 10),
                     'yyyy-mm-dd') < to_date(v_dateend, 'yyyy-mm-dd')
         and trim(inp.OutWardDate) is not null
         and inp.Status in ('1502', '1503')
         and (inp.OutHosDept = v_deptid or v_deptid = '0000')
         and (rd.islock IN (v_status) or rd.islock is null)
         and (inp.Name = v_patientname or v_patientname = '' or
              v_patientname is null)
         and (inp.NoOfRecord = v_recordid or v_recordid = '' or
              v_recordid is null)
       order by OutWardDate;
  
  END;

  /*********************************************************************************/
  PROCEDURE usp_getrecordnoonfilenew(v_datebegin   VARCHAR, --��ʼ����
                                     v_dateend     VARCHAR, --��������
                                     v_deptid      VARCHAR, --���Ҵ���
                                     v_status      VARCHAR, --����״̬
                                     v_patientname VARCHAR DEFAULT '', --��������
                                     v_recordid    VARCHAR DEFAULT '', --������
                                     o_result      OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡδ�鵵����
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
     exec usp_GetRecordNoOnFileNew v_DateBegin='2011-04-01',v_DateEnd='2011-04-26',v_DeptID='0000',v_Status='4700,4703'
    
     �޸ļ�¼
    **********/
    v_sql VARCHAR(4000);
  BEGIN
    --��ȡδ�鵵����
    v_sql := 'select mr.LockInfo,inp.*,dept.Name as OutHosDeptName,dd.Name as SexName
      ,d.Name as DiagnosisName,cd.Name as LockInfoName
      ,datediff(yy,inp.Birth,to_char(sysdate,''yyyy-mm-dd'')) as InpAgeNum
      ,datediff(dd,inp.AdmitDate,nvl(trim(inp.OutWardDate),to_char(sysdate,''yyyy-mm-dd'')))+1 as InHosDay
      ,datediff(dd,inp.OutWardDate,to_char(sysdate,''yyyy-mm-dd'')) as OutHosDay
      ,case when (SELECT count(NoOfInpat)  FROM MedicalRecord where MedicalRecord.NoOfInpat = inp.NoOfInpat)>0
      then ''�Ѿ���'' else ''δ����'' end as StatusName
      ,us3.Name as ResidentName,us1.Name as AttendName,us2.Name as ChiefName
      ,mr.ID as RecordID,b.ID  AS BedID,b.WardId as WardId
  from InPatient inp
    left join  Department dept on dept.ID=inp.OutHosDept
    left join  Dictionary_detail dd on dd.CategoryID=''3'' and dd.DetailID=inp.SexID
    left join  Diagnosis d on d.ICD=inp.OutDiagnosis
    left join  MedicalRecord mr on mr.NoOfInpat=inp.NoOfInpat
    left join  CategoryDetail cd on cd.CategoryID=''47'' and cd.ID=mr.LockInfo
    left join  Bed b ON inp.NoOfInpat = b.NoOfInpat
    left join Users us1 on us1.ID=inp.Attend
    left join Users us2 on us2.ID=inp.Chief
    left join Users us3 on us3.ID=inp.Resident
  where inp.OutWardDate>= ''' || v_datebegin || ' 00:00:00''' || '
        and inp.OutWardDate<''' || v_dateend || ' 24:00:00''' || '
        and inp.Status in(''1502'',''1503'')
        and (inp.OutHosDept=''' || v_deptid || ''' or ''' ||
             v_deptid || '''=''0000'')
        and  mr.LockInfo in (' || v_status || ')
        and (inp.Name=''' || v_patientname || ''' or ''' ||
             v_patientname || '''=''''or ' || v_patientname ||
             ' is null)
        and (inp.NoOfRecord=''' || v_recordid || ''' or ''' ||
             v_recordid || '''='''' or ' || v_recordid ||
             ' is null)
  order by OutWardDate';
  
    OPEN o_result FOR v_sql;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getrecordonfile(v_datebegin  VARCHAR, --��ʼ����
                                v_dateend    VARCHAR, --��������
                                v_patid      VARCHAR DEFAULT '', --סԺ����
                                v_name       VARCHAR DEFAULT '', --��������
                                v_sexid      VARCHAR DEFAULT '', --�����Ա�
                                v_agebegin   VARCHAR DEFAULT '', --��ʼ����
                                v_ageend     VARCHAR DEFAULT '', --��������
                                v_outhosdept VARCHAR DEFAULT '', --��Ժ���ҿ���
                                v_indiag     VARCHAR DEFAULT '', --��Ժ���
                                v_outdiag    VARCHAR DEFAULT '', --��Ժ���
                                v_surgeryid  VARCHAR DEFAULT '', --��������
                                v_physician  VARCHAR DEFAULT '', --סԺҽʦ����
                                o_result     OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ�ѹ鵵����
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
    
     exec usp_GetRecordOnFile v_DateBegin='2011-03-02'
         ,v_DateEnd='2011-03-10',v_PatID='',v_Name='',v_SexID=''
         ,v_AgeBegin='',v_AgeEnd='',v_OutHosDept='0000',v_OutDiagnosis=''
         ,v_SurgeryID=''
     �޸ļ�¼
    **********/
  BEGIN
    --��ȡ�ѹ鵵����
    OPEN o_result FOR
      SELECT DISTINCT inp.*,
                      dept.NAME AS outhosdeptname,
                      dd.NAME   AS sexname,
                      u1.NAME   AS residentname,
                      u2.NAME   AS attendname,
                      u3.NAME   AS chiefname,
                      d1.NAME   AS indiagnosisname,
                      d2.NAME   AS outdiagnosisname,
                      --mr.lockdate
                      rd.islock
        FROM inpatient inp
        LEFT JOIN department dept
          ON dept.ID = inp.outhosdept
        LEFT JOIN dictionary_detail dd
          ON dd.categoryid = '3'
         AND dd.detailid = inp.sexid
        LEFT JOIN users u1
          ON u1.ID = inp.resident
        LEFT JOIN users u2
          ON u2.ID = inp.attend
        LEFT JOIN users u3
          ON u3.ID = inp.chief
        LEFT JOIN diagnosis d1
          ON d1.icd = inp.admitdiagnosis
        LEFT JOIN diagnosis d2
          ON d2.icd = inp.outdiagnosis
        LEFT JOIN recorddetail rd
          ON rd.noofinpat = inp.noofinpat
       WHERE 1 = 1
            --and inp.outwarddate >= v_datebegin || ' 00:00:00 ' --edit by cyq 2012-12-06
            --AND inp.outwarddate < v_dateend || ' 24:00:00 '  --edit by cyq 2012-12-06
            --AND (inp.patid = v_patid OR v_patid = '' OR v_patid IS NULL) --edit by cyq 2012-12-06
            --AND (inp.NAME = v_name OR v_name = '' OR v_name IS NULL) --edit by cyq 2012-12-06
         and TO_DATE(inp.outwarddate, 'yyyy-MM-dd hh24:mi:ss') >=
             TO_DATE(v_datebegin || ' 00:00:00', 'yyyy-MM-dd hh24:mi:ss')
         and TO_DATE(inp.outwarddate, 'yyyy-MM-dd hh24:mi:ss') <=
             TO_DATE(v_dateend || ' 23:59:59', 'yyyy-MM-dd hh24:mi:ss')
         and inp.patid like '%' || v_patid || '%'
         and inp.Name like '%' || v_name || '%'
            
         AND (inp.sexid = v_sexid OR v_sexid = '' OR v_sexid IS NULL)
         AND (datediff('yy',
                       inp.birth,
                       TO_CHAR(SYSDATE, 'yyyy-mm-dd hh24:mi:ss')) - 1 >=
             v_agebegin OR v_agebegin is null)
         AND (datediff('yy',
                       inp.birth,
                       TO_CHAR(SYSDATE, 'yyyy-mm-dd hh24:mi:ss')) - 1 <=
             v_ageend OR v_ageend is null)
         AND (inp.outhosdept = v_outhosdept OR v_outhosdept = '0000')
         AND (inp.admitdiagnosis = v_indiag OR v_indiag is null OR
             v_indiag = '')
         AND (inp.outdiagnosis = v_outdiag OR v_outdiag is null OR
             v_outdiag = '')
         AND inp.status IN ('1502', '1503')
         AND (inp.resident = v_physician OR v_physician is null or
             v_physician = '')
         AND rd.islock = '4701'
       ORDER BY outwarddate;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getsigninrecordnew(v_dateBegin   VARCHAR, --��ʼ����
                                   v_dateEnd     VARCHAR, --��������
                                   v_dateInBegin VARCHAR, --��Ժ��ʼ����
                                   v_dateInEnd   VARCHAR, --��Ժ��������
                                   v_patientName VARCHAR, --��������
                                   v_outhosdept  VARCHAR, --��Ժ���ҿ���
                                   o_result      OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��ǩ�չ黹����
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
    
     exec usp_GetSignInRecord v_DateBegin=N'2011-03-02',v_DateEnd=N'2011-03-09',v_OutHosDept=N'0000'
    
     �޸ļ�¼
    **********/
  BEGIN
    --��ȡ��ǩ�չ黹����
    OPEN o_result FOR
      SELECT DISTINCT ar.*,
                      u1.NAME        AS applydoctorname,
                      u2.NAME        AS auditmanname,
                      cd1.NAME       AS applyaimname,
                      cd2.NAME       AS unitname,
                      inp.noofrecord,
                      inp.incount,
                      inp.NAME,
                      inp.sexid, --add by cyq 2012-11-16
                      dd.NAME        AS inpsexname,
                      inp.agestr,
                      dept.NAME      AS outhosdeptname,
                      inp.patid,
                      inp.admitdate,
                      inp.outhosdate,
                      d.NAME         AS outdiagnosisname,
                      --s.NAME AS surgeryname,
                      inp.NAME      AS inpname,
                      inp.noofinpat
        FROM applyrecord ar
        LEFT JOIN inpatient inp
          ON inp.noofinpat = ar.noofinpat
        LEFT JOIN department dept
          ON dept.ID = inp.outhosdept
        LEFT JOIN users u1
          ON u1.ID = ar.applydoctor
        LEFT JOIN users u2
          ON u2.ID = ar.auditman
        LEFT JOIN categorydetail cd1
          ON cd1.categoryid = '50'
         AND cd1.ID = ar.applyaim
        LEFT JOIN categorydetail cd2
          ON cd2.categoryid = '51'
         AND cd2.ID = ar.unit
        LEFT JOIN dictionary_detail dd
          ON dd.categoryid = '3'
         AND dd.detailid = inp.sexid
        LEFT JOIN diagnosis d
          ON d.icd = inp.outdiagnosis
      --LEFT JOIN medicalrecord mr ON mr.noofinpat = inp.noofinpat
        LEFT JOIN recorddetail rd
          ON rd.noofinpat = inp.noofinpat
      --LEFT JOIN diseasecfg dc ON dc.ID = mr.disease
      --LEFT JOIN surgery s ON s.ID = dc.surgeryid
       WHERE ar.applydate >= v_datebegin || ' 00:00:00 '
         AND ar.applydate < v_dateend || ' 24:00:00'
         And to_date(substr(nvl(trim(inp.inwarddate), '1990-01-01'), 1, 10),
                     'yyyy-mm-dd') >= to_date(v_dateinbegin, 'yyyy-mm-dd')
         and to_date(substr(nvl(trim(inp.inwarddate), '1990-01-01'), 1, 10),
                     'yyyy-mm-dd') < to_date(v_dateinend, 'yyyy-mm-dd')
         AND (inp.outhosdept = v_outhosdept OR v_outhosdept = '0000')
         and (inp.Name = v_patientName or v_patientName = '' or
             v_patientName is null)
         AND ar.status = '5205'
       ORDER BY outhosdeptname;
  END;

  /*********************************************************************************/
  PROCEDURE usp_getsigninrecord(v_datebegin  VARCHAR, --��ʼ����
                                v_dateend    VARCHAR, --��������
                                v_outhosdept VARCHAR, --��Ժ���ҿ���
                                o_result     OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��ǩ�չ黹����
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
    
     exec usp_GetSignInRecord v_DateBegin=N'2011-03-02',v_DateEnd=N'2011-03-09',v_OutHosDept=N'0000'
    
     �޸ļ�¼
    **********/
  BEGIN
    --��ȡ��ǩ�չ黹����
    OPEN o_result FOR
      SELECT DISTINCT ar.*,
                      u1.NAME        AS applydoctorname,
                      u2.NAME        AS auditmanname,
                      cd1.NAME       AS applyaimname,
                      cd2.NAME       AS unitname,
                      inp.noofrecord,
                      inp.incount,
                      inp.NAME,
                      dd.NAME        AS inpsexname,
                      inp.agestr,
                      dept.NAME      AS outhosdeptname,
                      inp.patid,
                      inp.admitdate,
                      inp.outhosdate,
                      d.NAME         AS outdiagnosisname,
                      --s.NAME AS surgeryname,
                      inp.NAME      AS inpname,
                      inp.noofinpat
        FROM applyrecord ar
        LEFT JOIN inpatient inp
          ON inp.noofinpat = ar.noofinpat
        LEFT JOIN department dept
          ON dept.ID = inp.outhosdept
        LEFT JOIN users u1
          ON u1.ID = ar.applydoctor
        LEFT JOIN users u2
          ON u2.ID = ar.auditman
        LEFT JOIN categorydetail cd1
          ON cd1.categoryid = '50'
         AND cd1.ID = ar.applyaim
        LEFT JOIN categorydetail cd2
          ON cd2.categoryid = '51'
         AND cd2.ID = ar.unit
        LEFT JOIN dictionary_detail dd
          ON dd.categoryid = '3'
         AND dd.detailid = inp.sexid
        LEFT JOIN diagnosis d
          ON d.icd = inp.outdiagnosis
      --LEFT JOIN medicalrecord mr ON mr.noofinpat = inp.noofinpat
        LEFT JOIN recorddetail rd
          ON rd.noofinpat = inp.noofinpat
      --LEFT JOIN diseasecfg dc ON dc.ID = mr.disease
      --LEFT JOIN surgery s ON s.ID = dc.surgeryid
       WHERE ar.applydate >= v_datebegin || ' 00:00:00 '
         AND ar.applydate < v_dateend || ' 24:00:00'
         AND (inp.outhosdept = v_outhosdept OR v_outhosdept = '0000')
         AND ar.status = '5205'
       ORDER BY outhosdeptname;
  END;

  /*********************************************************************************/
  PROCEDURE usp_gettemplatepersongroup(v_userid  VARCHAR DEFAULT '',
                                       o_result  OUT empcurtyp,
                                       o_result1 OUT empcurtyp) AS /**********
                                                                                         �汾��  1.0.0.0.0
                                                                                         ����ʱ��
                                                                                         ����
                                                                                         ��Ȩ
                                                                                         ����  ��ȡ��������
                                                                                         ����˵��
                                                                                         �������
                                                                                           v_NoOfInpat    numeric(9,0),     ��ҳ���
                                                                                         v_NodeID       int,      �ڵ�ID
                                                                                         v_TemplateID   varchar(64),   ģ��ID
                                                                                         v_ParentNodeID int,      ���ڵ�ID
                                                                                         v_Name         varchar(64),   �ڵ�����
                                                                                         v_UserID       varchar(18),     �û�ID
                                                                                         v_TypeID       int       ����ж��ǲ��뻹��ɾ��
                                                                                         �������
                                                                                         �����������
                                                                                         ��������ͳ�����ݼ�
    
                                                                                         ���õ�sp
                                                                                         ����ʵ��
                                                                                         �޸ļ�¼
                                                                                         **********/
  BEGIN
    OPEN o_result FOR
      SELECT '' FROM DUAL;
  
    OPEN o_result1 FOR
      SELECT '' FROM DUAL;
  
    OPEN o_result FOR
      SELECT t.ID,
             t.nodeid,
             t.parentnodeid,
             tsp.templateid AS templateid,
             t.templatepersonid,
             tsp.NAME,
             t.nodename,
             t.userid,
             tsp.sortid,
             tsp.memo
        FROM templatepersongroup t
        LEFT OUTER JOIN template_person tsp
          ON tsp.ID = t.templatepersonid
         AND tsp.valid = '1'
       WHERE t.userid = v_userid
       ORDER BY t.nodeid;
  
    OPEN o_result1 FOR
      SELECT ID, templateid, NAME, memo, '��' AS used, sortid
        FROM template_person
       WHERE userid = v_userid
            /*wwj*/ --and SortID <> '0000'
         AND valid = 1;
  END;
  /**********************************************************************************************/
  PROCEDURE usp_Inpatient_Trigger(v_syxh varchar2 --������ҳ���
                                  ) as
  
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �޸Ĳ��˻�����Ϣ��������ʱ��ͬ�����¶�Ӧ��Ϣ
     ����˵��
     �������
     �������
     �����������
    
    
    
     ���õ�sp
     ����ʵ��
      exec usp_Inpatient_Trigger ''
     �޸ļ�¼
    **********/
    v_csrq     varchar(19);
    v_ryrq     varchar(19);
    v_brnl     int;
    v_xsnl     varchar(19);
    v_InnerPIX varchar(24);
  begin
    if v_syxh is null then
      return;
    end if;
    ---����������Ϣ
    select inp.birth, inp.admitdate
      into v_csrq, v_ryrq
      from inpatient inp
     where inp.noofinpat = v_syxh
       and rownum = 1;
  
    EMRPROC.usp_EMR_GetAge(v_csrq, v_ryrq, v_brnl, v_xsnl);
  
    update InPatient
       set Age = v_brnl, AgeStr = v_xsnl
     where NoOfInpat = v_syxh;
  
    commit;
  
    -- ��¼��λ��Ϣ
    -- �ҳ����˴�λ����ҡ�������Ϣ�б仯�Ĳ���
  
    -- ���µ�ǰ��λ��Ϣ
    /*  update BedInfo set EndDate = to_char(sysdate,'yyyy-mm-dd HH24:mi:ss'), NewDept = b.OutHosDept, NewWard = b.OutHosWard, NewBed = b.OutBed, Mark = 0
    from BedInfo a, inpatient b
    where c.NoOfInpat = b.NoOfInpat
      and a.Mark = 1*/
  
    update BedInfo a
       set (EndDate, NewDept, NewWard, NewBed, Mark) =
           (select to_char(sysdate, 'yyyy-mm-dd HH24:mi:ss'),
                   b.OutHosDept,
                   b.OutHosWard,
                   b.OutBed,
                   0
              from inpatient b
             where a.noofinpat = b.NoOfInpat)
     where a.Mark = 1
       and exists
     (select 1 from inpatient b where a.noofinpat = b.NoOfInpat);
  
    -- �������µĴ�λ��Ϣ��¼
    insert into BedInfo
      (ID,
       NoOfInpat,
       FormerDeptID,
       FormerWard,
       FormerBedID,
       StartDate,
       Mark)
      select seq_bedinfo_id.nextval,
             a.NoOfInpat,
             a.OutHosDept,
             a.OutHosWard,
             a.OutBed,
             to_char(sysdate, 'yyyy-mm-dd HH24:mi:ss'),
             1
        from inpatient a
       where a.NoOfInpat = v_syxh;
  
    -- �����������
  
    -- ���籣���ŵģ��ҳ��籣������ͬ����СסԺ��
    select min(a.PatID)
      into v_InnerPIX
      from inpatient a
     where a.idno =
           (select b.idno from inpatient b where b.noofinpat = v_syxh);
  
    -- ���¹�����סԺ����
    update InPatient a
       set InnerPIX = v_InnerPIX
     where a.NoOfInpat = v_syxh;
  
  end;

  /*********************************************************************************/
  PROCEDURE usp_insertimage(v_sort   INT,
                            v_name   VARCHAR,
                            v_memo   VARCHAR DEFAULT '',
                            v_id     NUMERIC DEFAULT 0,
                            o_result OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����
     ����˵��  ����ͼƬ��Ϣ
     �������
    v_Sort int         --��ѯ��ʽ
    v_Name varchar(64)       --ģ������
    v_Content varchar(max)    --ģ�����ݣ�����ģ�
    v_Memo varchar(255)=''  --ģ������
    v_Image image         --ģ������
    v_ID numeric(12,0)=''       --ģ��ID
    
     �������
     �����������
    ͼƬ������ģ���ͼƬ��Ϣ���б������
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    **********/
  
  BEGIN
    IF v_sort = 0 THEN
      INSERT into imagelibrary
        (id, name, memo, valid)
      VALUES
        (seq_imagelibrary_id.NEXTVAL, v_name, v_memo, 1);
    
      open o_result for
        select seq_imagelibrary_id.currval from dual;
    END IF;
  
    IF v_sort = 1 THEN
      UPDATE imagelibrary SET NAME = v_name, memo = v_memo WHERE ID = v_id;
    
      open o_result for
        select v_id from dual;
    END IF;
  
    IF v_sort = 2 THEN
      UPDATE imagelibrary SET NAME = v_name, memo = v_memo WHERE ID = v_id;
    
      open o_result for
        select v_id from dual;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_insertjob(v_id VARCHAR, v_title VARCHAR, v_memo VARCHAR) AS /**********
                                                                                                                �汾��
                                                                                                                ����ʱ��
                                                                                                                ����
                                                                                                                ��Ȩ
                                                                                                                ����
                                                                                                                ����˵��      �����λ��Ϣ
                                                                                                                �������
                                                                                                                �����������
                                                                                                                ���õ�sp
                                                                                                                ����ʵ��
                                                                                                               **********/
  BEGIN
    INSERT INTO jobs (ID, title, memo) VALUES (v_id, v_title, v_memo);
  END;

  /*********************************************************************************/
  PROCEDURE usp_insertjobpermission(v_id         VARCHAR,
                                    v_moduleid   VARCHAR,
                                    v_modulename VARCHAR) AS /**********
                                                                                                            �汾��
                                                                                                            ����ʱ��
                                                                                                            ����
                                                                                                            ��Ȩ
                                                                                                            ����
                                                                                                            ����˵��      �����µ���Ȩ��Ϣ
                                                                                                            �������
                                                                                                            �����������
                                                                                                            ���õ�sp
                                                                                                            ����ʵ��
                                                                                                           **********/
  BEGIN
    INSERT INTO job2permission
      (ID, moduleid, modulename)
    VALUES
      (v_id, v_moduleid, v_modulename);
  END;

  /*********************************************************************************/
  PROCEDURE usp_insertnursrecordtable(v_noofinpat  NUMERIC, --��ҳ���(סԺ��ˮ��)
                                      v_name       VARCHAR, --��¼�����ƣ�ģ������+����+ʱ�䣩
                                      v_content    BLOB, --��������
                                      v_templateid NUMERIC, --ģ��ID
                                      v_version    VARCHAR,
                                      --ģ��汾(�Զ�������,Ĭ��Ϊ1.00.00,�������������1λ)
                                      v_department VARCHAR, --�����������Ҵ���
                                      v_sortid     NUMERIC, --ģ��������
                                      v_valid      INT --��Ч��¼(CategoryDetail.ID CategoryID = 0)
                                      ) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]2011-06-10
    [����]hjh
    [��Ȩ]
    [����]��Ӳ��˻����ĵ�
    [����˵��]
    [�������]
    [�������]
    [�����������]
    
    
    [���õ�sp]
    [����ʵ��]
    
    [�޸ļ�¼]
    **********/
  BEGIN
    --��Ӳ��˻����ĵ���Ϣ
    INSERT INTO nursrecordtable
      (noofinpat,
       templateid,
       content,
       department,
       VERSION,
       sortid,
       valid,
       NAME)
    VALUES
      (v_noofinpat,
       v_templateid,
       v_content,
       v_department,
       v_version,
       v_sortid,
       v_valid,
       v_name);
  END;

  /*********************************************************************************/
  PROCEDURE usp_insertnurstabletemplate(v_name     VARCHAR, --ģ������
                                        v_describe VARCHAR, --ģ������
                                        v_content  BLOB, --ģ������
                                        v_version  VARCHAR,
                                        --ģ��汾(�Զ�������,Ĭ��Ϊ1.00.00,�������������1λ)
                                        v_sortid VARCHAR, --ģ��������
                                        v_valid  INT, --��Ч��¼(CategoryDetail.ID CategoryID = 0)
                                        o_result OUT empcurtyp) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]2011-06-10
    [����]hjh
    [��Ȩ]
    [����]���������ĵ�ģ�壬������ID
    [����˵��]
    [�������]
    [�������]
    [�����������]
    
    
    [���õ�sp]
    [����ʵ��]
    
    [�޸ļ�¼]
    **********/
  BEGIN
    --����ģ��
    INSERT INTO template_table
      (ID, NAME, describe, content, VERSION, sortid, valid)
    VALUES
      (seq_template_table_id.NEXTVAL,
       v_name,
       v_describe,
       v_content,
       v_version,
       v_sortid,
       v_valid);
  
    --��������ģ��ID
    OPEN o_result FOR
      SELECT temp.ID
        FROM (SELECT ID FROM template_table WHERE valid = 1 ORDER BY ID DESC) temp
       WHERE ROWNUM < 1;
  END;

  /*********************************************************************************/
  PROCEDURE usp_inserttemplatepersongroup(v_userid             VARCHAR DEFAULT '',
                                          v_nodeid             INT DEFAULT 0,
                                          v_templatepersonid   INT DEFAULT 0,
                                          v_parentnodeid       INT DEFAULT 0,
                                          v_name               VARCHAR DEFAULT '',
                                          v_typeid             INT DEFAULT 0,
                                          v_templatepersonname VARCHAR DEFAULT '',
                                          v_templatepersonmemo VARCHAR DEFAULT '') AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��������
     ����˵��
     �������
        v_NoOfInpat    numeric(9,0),     ��ҳ���
      v_NodeID       int,      �ڵ�ID
      v_TemplateID   varchar(64),   ģ��ID
      v_ParentNodeID int,      ���ڵ�ID
      v_Name         varchar(64),   �ڵ�����
      v_UserID       varchar(18),     �û�ID
      v_TypeID       int       ����ж��ǲ��뻹��ɾ��
     �������
     �����������
    ��������ͳ�����ݼ�
     execute usp_InsertTemplatePersonGroup '00', 0, '', 0, '', 2
     ���õ�sp
     ����ʵ��
     �޸ļ�¼
    **********/
    v_maxnodeid           INT;
    v_maxtemplatepersonid INT;
  BEGIN
    IF v_typeid = 1 THEN
      INSERT INTO templatepersongroup
        (ID, userid, nodeid, parentnodeid, templatepersonid, nodename)
      VALUES
        (seq_templatepersongroup_id.NEXTVAL,
         v_userid,
         v_nodeid,
         v_parentnodeid,
         v_templatepersonid,
         v_name);
    END IF;
  
    IF v_typeid = 2 THEN
      DELETE FROM templatepersongroup WHERE userid = v_userid;
    END IF;
  
    IF v_typeid = 3 THEN
      UPDATE template_person
         SET NAME = v_templatepersonname, memo = v_templatepersonmemo
       WHERE userid = v_userid
         AND valid = '1'
         AND ID = v_templatepersonid;
    END IF;
  
    IF v_typeid = 4 THEN
      BEGIN
        SELECT NVL(MAX(tp.nodeid) + 1, 0)
          INTO v_maxnodeid
          FROM templatepersongroup tp
         WHERE tp.userid = v_userid;
      
        SELECT NVL(MAX(tp.ID), 0)
          INTO v_maxtemplatepersonid
          FROM template_person tp
         WHERE tp.userid = v_userid
           AND tp.valid = '1'
           AND tp.sortmark = '0'
           AND tp.sharedid = '0';
      
        INSERT INTO templatepersongroup
          (ID, userid, nodeid, parentnodeid, nodename, templatepersonid)
        VALUES
          (seq_templatepersongroup_id.NEXTVAL,
           v_userid,
           v_maxnodeid,
           v_parentnodeid,
           v_name,
           v_maxtemplatepersonid);
      END;
    END IF;
  END;

  /*********************************************************************************/
  PROCEDURE usp_insertuserlogin(v_id          VARCHAR,
                                v_moduleid    VARCHAR,
                                v_hostname    VARCHAR,
                                v_macaddr     VARCHAR,
                                v_client_ip   VARCHAR,
                                v_reason_id   VARCHAR,
                                v_create_user VARCHAR) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �û���¼����޸�
     ����˵��
     �������
    v_ID varchar(4) NOT NULL,--ģ���¼��ID
    v_Module_id varchar(255) ,--ģ������
    v_MACADDR varchar(255) NOT NULL,--MAC��ַ
    v_HostName varchar(255) NOT NULL,--HostName
    v_Client_ip varchar(255) NOT NULL,--����Ip��ַ
    v_Reason_id Varchar(1) ,--��¼/�ǳ� reason 0/null ������1ǿ��
    v_Create_user varchar(4) NOT NULL --��¼��ID
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_InsertUserLogIn '00', '00', '00','00', '00', '00','00'
     �޸ļ�¼
    **********/
  BEGIN
    INSERT INTO user_login
      (ID, module_id, hostname, macaddr, client_ip, reason_id, create_user)
    VALUES
      (v_id,
       v_moduleid,
       v_hostname,
       v_macaddr,
       v_client_ip,
       v_reason_id,
       v_create_user);
  END;

  /*********************************************************************************/
  PROCEDURE usp_insert_iem_mainpage_basic(v_patnoofhis               NUMERIC,
                                          v_noofinpat                NUMERIC,
                                          v_payid                    VARCHAR,
                                          v_socialcare               VARCHAR,
                                          v_incount                  INT,
                                          v_name                     VARCHAR,
                                          v_sexid                    VARCHAR,
                                          v_birth                    CHAR,
                                          v_marital                  VARCHAR,
                                          v_jobid                    VARCHAR,
                                          v_provinceid               VARCHAR,
                                          v_countyid                 VARCHAR,
                                          v_nationid                 VARCHAR,
                                          v_nationalityid            VARCHAR,
                                          v_idno                     VARCHAR,
                                          v_organization             VARCHAR,
                                          v_officeplace              VARCHAR,
                                          v_officetel                VARCHAR,
                                          v_officepost               VARCHAR,
                                          v_nativeaddress            VARCHAR,
                                          v_nativetel                VARCHAR,
                                          v_nativepost               VARCHAR,
                                          v_contactperson            VARCHAR,
                                          v_relationship             VARCHAR,
                                          v_contactaddress           VARCHAR,
                                          v_contacttel               VARCHAR,
                                          v_admitdate                VARCHAR,
                                          v_admitdept                VARCHAR,
                                          v_admitward                VARCHAR,
                                          v_days_before              NUMERIC,
                                          v_trans_date               VARCHAR,
                                          v_trans_admitdept          VARCHAR,
                                          v_trans_admitward          VARCHAR,
                                          v_trans_admitdept_again    VARCHAR,
                                          v_outwarddate              VARCHAR,
                                          v_outhosdept               VARCHAR,
                                          v_outhosward               VARCHAR,
                                          v_actual_days              NUMERIC,
                                          v_death_time               VARCHAR,
                                          v_death_reason             VARCHAR,
                                          v_admitinfo                VARCHAR,
                                          v_in_check_date            VARCHAR,
                                          v_pathology_diagnosis_name VARCHAR,
                                          v_pathology_observation_sn VARCHAR,
                                          v_ashes_diagnosis_name     VARCHAR,
                                          v_ashes_anatomise_sn       VARCHAR,
                                          v_allergic_drug            VARCHAR,
                                          v_hbsag                    NUMERIC,
                                          v_hcv_ab                   NUMERIC,
                                          v_hiv_ab                   NUMERIC,
                                          v_opd_ipd_id               NUMERIC,
                                          v_in_out_inpatinet_id      NUMERIC,
                                          v_before_after_or_id       NUMERIC,
                                          v_clinical_pathology_id    NUMERIC,
                                          v_pacs_pathology_id        NUMERIC,
                                          v_save_times               NUMERIC,
                                          v_success_times            NUMERIC,
                                          v_section_director         VARCHAR,
                                          v_director                 VARCHAR,
                                          v_vs_employee_code         VARCHAR,
                                          v_resident_employee_code   VARCHAR,
                                          v_refresh_employee_code    VARCHAR,
                                          v_master_interne           VARCHAR,
                                          v_interne                  VARCHAR,
                                          v_coding_user              VARCHAR,
                                          v_medical_quality_id       NUMERIC,
                                          v_quality_control_doctor   VARCHAR,
                                          v_quality_control_nurse    VARCHAR,
                                          v_quality_control_date     VARCHAR,
                                          v_xay_sn                   VARCHAR,
                                          v_ct_sn                    VARCHAR,
                                          v_mri_sn                   VARCHAR,
                                          v_dsa_sn                   VARCHAR,
                                          v_is_first_case            NUMERIC,
                                          v_is_following             NUMERIC,
                                          v_following_ending_date    VARCHAR,
                                          v_is_teaching_case         NUMERIC,
                                          v_blood_type_id            NUMERIC,
                                          v_rh                       NUMERIC,
                                          v_blood_reaction_id        NUMERIC,
                                          v_blood_rbc                NUMERIC,
                                          v_blood_plt                NUMERIC,
                                          v_blood_plasma             NUMERIC,
                                          v_blood_wb                 NUMERIC,
                                          v_blood_others             VARCHAR,
                                          v_is_completed             VARCHAR,
                                          v_completed_time           VARCHAR,
                                          --v_Valide numeric ,
                                          v_create_user VARCHAR,
                                          --v_Create_Time varchar(19)
                                          --v_Modified_User varchar(10) ,
                                          --v_Modified_Time varchar(19)
                                          o_result OUT empcurtyp) AS /**********
                                                                                                         �汾��  1.0.0.0.0
                                                                                                         ����ʱ��
                                                                                                         ����
                                                                                                         ��Ȩ
                                                                                                         ����  ���빦������ҳ������ϢTABLE
                                                                                                         ����˵��
                                                                                                         �������
                                                                                                         �������
                                                                                                         �����������
    
                                                                                                         ���õ�sp
                                                                                                         ����ʵ��
    
                                                                                                         �޸ļ�¼
                                                                                                        **********/
  BEGIN
    INSERT INTO iem_mainpage_basicinfo
      (iem_mainpage_no,
       patnoofhis,
       noofinpat,
       payid,
       socialcare,
       incount,
       NAME,
       sexid,
       birth,
       marital,
       jobid,
       provinceid,
       countyid,
       nationid,
       nationalityid,
       idno,
       ORGANIZATION,
       officeplace,
       officetel,
       officepost,
       nativeaddress,
       nativetel,
       nativepost,
       contactperson,
       relationship,
       contactaddress,
       contacttel,
       admitdate,
       admitdept,
       admitward,
       days_before,
       trans_date,
       trans_admitdept,
       trans_admitward,
       trans_admitdept_again,
       outwarddate,
       outhosdept,
       outhosward,
       actual_days,
       death_time,
       death_reason,
       admitinfo,
       in_check_date,
       pathology_diagnosis_name,
       pathology_observation_sn,
       ashes_diagnosis_name,
       ashes_anatomise_sn,
       allergic_drug,
       hbsag,
       hcv_ab,
       hiv_ab,
       opd_ipd_id,
       in_out_inpatinet_id,
       before_after_or_id,
       clinical_pathology_id,
       pacs_pathology_id,
       save_times,
       success_times,
       section_director,
       director,
       vs_employee_code,
       resident_employee_code,
       refresh_employee_code,
       master_interne,
       interne,
       coding_user,
       medical_quality_id,
       quality_control_doctor,
       quality_control_nurse,
       quality_control_date,
       xay_sn,
       ct_sn,
       mri_sn,
       dsa_sn,
       is_first_case,
       is_following,
       following_ending_date,
       is_teaching_case,
       blood_type_id,
       rh,
       blood_reaction_id,
       blood_rbc,
       blood_plt,
       blood_plasma,
       blood_wb,
       blood_others,
       is_completed,
       completed_time,
       valide,
       create_user,
       create_time)
    VALUES
      (seq_iem_mainpage_basicinfo_id.NEXTVAL,
       v_patnoofhis,
       -- numeric
       v_noofinpat, -- numeric
       v_payid, -- varchar(4)
       v_socialcare, -- varchar(32)
       v_incount, -- int
       v_name,
       -- varchar(60)
       v_sexid, -- varchar(4)
       v_birth, -- char(10)
       v_marital, -- varchar(4)
       v_jobid, -- varchar(4)
       v_provinceid,
       -- varchar(10)
       v_countyid, -- varchar(10)
       v_nationid, -- varchar(4)
       v_nationalityid, -- varchar(4)
       v_idno,
       -- varchar(18)
       v_organization, -- varchar(64)
       v_officeplace, -- varchar(64)
       v_officetel, -- varchar(16)
       v_officepost,
       -- varchar(16)
       v_nativeaddress, -- varchar(64)
       v_nativetel, -- varchar(16)
       v_nativepost, -- varchar(16)
       v_contactperson, -- varchar(32)
       v_relationship, -- varchar(4)
       v_contactaddress,
       -- varchar(255)
       v_contacttel, -- varchar(16)
       v_admitdate, -- varchar(19)
       v_admitdept, -- varchar(12)
       v_admitward,
       -- varchar(12)
       v_days_before, -- numeric
       v_trans_date, -- varchar(19)
       v_trans_admitdept,
       -- varchar(12)
       v_trans_admitward, -- varchar(12)
       v_trans_admitdept_again, -- varchar(12)
       v_outwarddate, -- varchar(19)
       v_outhosdept, -- varchar(12)
       v_outhosward, -- varchar(12)
       v_actual_days,
       -- numeric
       v_death_time, -- varchar(19)
       v_death_reason, -- varchar(300)
       v_admitinfo, -- varchar(4)
       v_in_check_date, -- varchar(19)
       v_pathology_diagnosis_name, -- varchar(300)
       v_pathology_observation_sn, -- varchar(60)
       v_ashes_diagnosis_name,
       -- varchar(300)
       v_ashes_anatomise_sn, -- varchar(60)
       v_allergic_drug, -- varchar(300)
       v_hbsag, -- numeric
       v_hcv_ab,
       -- numeric
       v_hiv_ab, -- numeric
       v_opd_ipd_id, -- numeric
       v_in_out_inpatinet_id, -- numeric
       v_before_after_or_id, -- numeric
       v_clinical_pathology_id, -- numeric
       v_pacs_pathology_id, -- numeric
       v_save_times, -- numeric
       v_success_times,
       -- numeric
       v_section_director, -- varchar(20)
       v_director, -- varchar(20)
       v_vs_employee_code,
       -- varchar(20)
       v_resident_employee_code, -- varchar(20)
       v_refresh_employee_code,
       -- varchar(20)
       v_master_interne, -- varchar(20)
       v_interne, -- varchar(20)
       v_coding_user, -- varchar(20)
       v_medical_quality_id, -- numeric
       v_quality_control_doctor,
       -- varchar(20)
       v_quality_control_nurse, -- varchar(20)
       v_quality_control_date,
       -- varchar(19)
       v_xay_sn, -- varchar(300)
       v_ct_sn, -- varchar(300)
       v_mri_sn, -- varchar(300)
       v_dsa_sn, -- varchar(300)
       v_is_first_case,
       -- numeric
       v_is_following, -- numeric
       v_following_ending_date, -- varchar(19)
       v_is_teaching_case, -- numeric
       v_blood_type_id, -- numeric
       v_rh, -- numeric
       v_blood_reaction_id, -- numeric
       v_blood_rbc, -- numeric
       v_blood_plt, -- numeric
       v_blood_plasma, -- numeric
       v_blood_wb, -- numeric
       v_blood_others, -- varchar(60)
       v_is_completed, -- varchar(1)
       v_completed_time, -- varchar(19)
       1, -- numeric
       v_create_user,
       -- varchar(10)
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss') -- varchar(19)
       );
  
    OPEN o_result FOR
      SELECT seq_iem_mainpage_basicinfo_id.CURRVAL FROM DUAL;
  END;

  /*********************************************************************************/
  PROCEDURE usp_insert_iem_mainpage_diag(v_iem_mainpage_no   NUMERIC,
                                         v_diagnosis_type_id NUMERIC,
                                         v_diagnosis_code    VARCHAR,
                                         v_diagnosis_name    VARCHAR,
                                         v_status_id         NUMERIC,
                                         v_order_value       NUMERIC,
                                         --v_Valide numeric ,
                                         v_create_user VARCHAR
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
    INSERT INTO iem_mainpage_diagnosis
      (iem_mainpage_no,
       diagnosis_type_id,
       diagnosis_code,
       diagnosis_name,
       status_id,
       order_value,
       valide,
       create_user,
       create_time)
    VALUES
      (v_iem_mainpage_no, -- Iem_Mainpage_NO - numeric
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
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss')
       -- Create_Time - varchar(19)
       );
  END;

  /*********************************************************************************/
  PROCEDURE usp_insert_iem_mainpage_oper(v_iem_mainpage_no     NUMERIC,
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
                                         v_create_user VARCHAR
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
    INSERT INTO iem_mainpage_operation
      (iem_mainpage_no,
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
       create_time)
    VALUES
      (v_iem_mainpage_no, --numeric
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
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss'));
  END;

  /*********************************************************************************/
  /*�������ĳ�����˵ĵ�ǰ�����Ϣ(wyt 2012-08-13)*/
  PROCEDURE usp_updatecurrentdiaginfo(v_patient_id   VARCHAR2,
                                      v_patient_name VARCHAR2,
                                      v_diag_code    VARCHAR2,
                                      v_diag_content varchar2) AS
    v_tip VARCHAR(30);
  BEGIN
    -- select sysdate into v_tip  from dual;--Ϊ�˱�֤������������ע��
    DELETE CurrentDiag WHERE diag_code = v_diag_code;
    INSERT INTO CurrentDiag
      (patient_id, patient_name, diag_code, diag_content)
    values
      (v_patient_id, v_patient_name, v_diag_code, v_diag_content);
  END;

  /*********************************************************************************/
  /*�������ĳ�����ҽʦ���ֲ鿴Ȩ����Ϣ(wyt 2012-08-13)*/
  PROCEDURE usp_updateattendphysicianinfo(v_id            VARCHAR2,
                                          v_name          VARCHAR2,
                                          v_py            VARCHAR2,
                                          v_wb            VARCHAR2,
                                          v_deptid        VARCHAR2,
                                          v_relatedisease varchar2) AS
    v_tip VARCHAR(30);
  BEGIN
    --select sysdate into v_tip  from dual;--Ϊ�˱�֤������������ע��
    DELETE AttendingPhysician WHERE id = v_id;
    INSERT INTO AttendingPhysician
      (id, name, py, wb, deptid, relatedisease)
    values
      (v_id, v_name, v_py, v_wb, v_deptid, v_relatedisease);
  END;

  /*********************************************************************************/
  /*�������ĳ��Ա����Ϣ*/
  PROCEDURE usp_updateuserinfo(v_id           VARCHAR2,
                               v_name         VARCHAR2,
                               v_deptid       VARCHAR2,
                               v_wardid       VARCHAR2,
                               v_jobid        VARCHAR2,
                               v_Py           VARCHAR2,
                               v_Wb           VARCHAR2,
                               v_Sexy         VARCHAR2,
                               v_Birth        VARCHAR2,
                               v_Marital      VARCHAR2,
                               v_Idno         VARCHAR2,
                               v_Category     VARCHAR2,
                               v_Jobtitle     VARCHAR2,
                               v_Recipeid     VARCHAR2,
                               v_Recipemark   VARCHAR2,
                               v_Narcosismark VARCHAR2,
                               v_Grade        VARCHAR2,
                               v_Status       VARCHAR2,
                               v_Memo         varchar2
                               
                               ) AS
  BEGIN
    UPDATE users
       SET NAME   = v_name,
           deptid = v_deptid,
           wardid = v_wardid,
           jobid  = v_jobid,
           
           Py           = v_Py,
           Wb           = v_Wb,
           Sexy         = v_Sexy,
           Birth        = v_Birth,
           Marital      = v_Marital,
           Idno         = v_Idno,
           Category     = v_Category,
           Jobtitle     = v_Jobtitle,
           Recipeid     = v_Recipeid,
           Recipemark   = v_Recipemark,
           Narcosismark = v_Narcosismark,
           Grade        = v_Grade,
           Status       = v_Status,
           memo         = v_Memo
     WHERE ID = v_id;
  END;

  /***************************************************************************************/
  /*�޸ĸ�λ��Ϣ*/
  PROCEDURE usp_updatejobinfo(v_id    VARCHAR2,
                              v_title VARCHAR2,
                              v_memo  VARCHAR2) AS
  BEGIN
    UPDATE jobs SET title = v_title, memo = v_memo WHERE ID = v_id;
  END;

  /*****************************************************************************/
  PROCEDURE usp_updatenursrecordtable(v_id        NUMERIC, --��¼ID
                                      v_noofinpat NUMERIC, --��ҳ���(סԺ��ˮ��)
                                      v_content   BLOB --������
                                      ) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]2011-06-10
    [����]hjh
    [��Ȩ]
    [����]���²��˻����ĵ�
    [����˵��]
    [�������]
    [�������]
    [�����������]
    
    
    [���õ�sp]
    [����ʵ��]
    
    [�޸ļ�¼]
    **********/
  BEGIN
    --���²��˻����ĵ���Ϣ
    UPDATE nursrecordtable
       SET content = v_content
     WHERE valid = 1
       AND ID = v_id
       AND noofinpat = v_noofinpat;
  END;

  /***************************************************************************************/
  PROCEDURE usp_updatenurstabletemplate(v_id       NUMERIC, --ģ�����
                                        v_name     VARCHAR, --ģ������
                                        v_describe VARCHAR, --ģ������
                                        v_content  BLOB, --ģ������
                                        v_version  VARCHAR,
                                        --ģ��汾(�Զ�������,Ĭ��Ϊ1.00.00,�������������1λ)
                                        v_sortid VARCHAR --ģ��������
                                        ) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]2011-06-10
    [����]hjh
    [��Ȩ]
    [����]���»����ĵ�ģ��
    [����˵��]
    [�������]
    [�������]
    [�����������]
    
    
    [���õ�sp]
    [����ʵ��]
    
    [�޸ļ�¼]
    **********/
  BEGIN
    --����ģ��
    UPDATE template_table
       SET NAME     = v_name,
           describe = v_describe,
           content  = v_content,
           VERSION  = v_version,
           sortid   = v_sortid
     WHERE ID = v_id;
  END;

  /***************************************************************************************/

  /*���µ��ڵĽ��Ĳ���*/
  PROCEDURE usp_updatedueapplyrecord(v_applydoctor VARCHAR2 --����ҽʦ����
                                     ) AS
    v_currdatetime VARCHAR2(20);
  BEGIN
    v_currdatetime := TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss');
  
    UPDATE applyrecord
       SET status = '5205'
     WHERE (applydoctor = v_applydoctor OR v_applydoctor = '' OR
           v_applydoctor IS NULL)
       AND status = '5202'
       AND ((unit = '5101' AND
           datediff('mi', auditdate, v_currdatetime) >= duetime * 60) --Сʱ
           OR (unit = '5102' AND
           datediff('hh', auditdate, v_currdatetime) >= duetime * 24) --��
           OR (unit = '5103' AND
           datediff('dd', auditdate, v_currdatetime) / 7 >= duetime) --��
           OR (unit = '5104' AND
           datediff('dd', auditdate, v_currdatetime) >= duetime * 30) --��
           OR (unit = '5105' AND
           datediff('dd', auditdate, v_currdatetime) >= duetime * 360) --��
           );
  END;

  /***************************************************************************************/
  /*���¹�������ҳ����TABLE*/
  PROCEDURE usp_update_iem_mainpage_oper(v_iem_mainpage_no NUMERIC,
                                         v_cancel_user     VARCHAR) AS
  BEGIN
    UPDATE iem_mainpage_operation
       SET valide      = 0,
           cancel_user = v_cancel_user,
           cancel_time = TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss')
     WHERE iem_mainpage_no = v_iem_mainpage_no
       AND valide = 1;
  END;

  /***************************************************************************************/
  /*���²�����ҳ���TABLE,�ڲ���֮ǰ����*/
  PROCEDURE usp_update_iem_mainpage_diag(v_iem_mainpage_no NUMERIC,
                                         v_cancel_user     VARCHAR2) AS
  BEGIN
    UPDATE iem_mainpage_diagnosis
       SET cancel_user = v_cancel_user,
           valide      = 0,
           cancel_time = TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss')
     WHERE iem_mainpage_no = v_iem_mainpage_no
       AND valide = 1;
  END;

  /***************************************************************************************/
  /*���¹�������ҳ������ϢTABLE*/
  PROCEDURE usp_update_iem_mainpage_basic(v_iem_mainpage_no          NUMERIC,
                                          v_patnoofhis               NUMERIC,
                                          v_noofinpat                NUMERIC,
                                          v_payid                    VARCHAR,
                                          v_socialcare               VARCHAR,
                                          v_incount                  INT,
                                          v_name                     VARCHAR,
                                          v_sexid                    VARCHAR,
                                          v_birth                    CHAR,
                                          v_marital                  VARCHAR,
                                          v_jobid                    VARCHAR,
                                          v_provinceid               VARCHAR,
                                          v_countyid                 VARCHAR,
                                          v_nationid                 VARCHAR,
                                          v_nationalityid            VARCHAR,
                                          v_idno                     VARCHAR,
                                          v_organization             VARCHAR,
                                          v_officeplace              VARCHAR,
                                          v_officetel                VARCHAR,
                                          v_officepost               VARCHAR,
                                          v_nativeaddress            VARCHAR,
                                          v_nativetel                VARCHAR,
                                          v_nativepost               VARCHAR,
                                          v_contactperson            VARCHAR,
                                          v_relationship             VARCHAR,
                                          v_contactaddress           VARCHAR,
                                          v_contacttel               VARCHAR,
                                          v_admitdate                VARCHAR,
                                          v_admitdept                VARCHAR,
                                          v_admitward                VARCHAR,
                                          v_days_before              NUMERIC,
                                          v_trans_date               VARCHAR,
                                          v_trans_admitdept          VARCHAR,
                                          v_trans_admitward          VARCHAR,
                                          v_trans_admitdept_again    VARCHAR,
                                          v_outwarddate              VARCHAR,
                                          v_outhosdept               VARCHAR,
                                          v_outhosward               VARCHAR,
                                          v_actual_days              NUMERIC,
                                          v_death_time               VARCHAR,
                                          v_death_reason             VARCHAR,
                                          v_admitinfo                VARCHAR,
                                          v_in_check_date            VARCHAR,
                                          v_pathology_diagnosis_name VARCHAR,
                                          v_pathology_observation_sn VARCHAR,
                                          v_ashes_diagnosis_name     VARCHAR,
                                          v_ashes_anatomise_sn       VARCHAR,
                                          v_allergic_drug            VARCHAR,
                                          v_hbsag                    NUMERIC,
                                          v_hcv_ab                   NUMERIC,
                                          v_hiv_ab                   NUMERIC,
                                          v_opd_ipd_id               NUMERIC,
                                          v_in_out_inpatinet_id      NUMERIC,
                                          v_before_after_or_id       NUMERIC,
                                          v_clinical_pathology_id    NUMERIC,
                                          v_pacs_pathology_id        NUMERIC,
                                          v_save_times               NUMERIC,
                                          v_success_times            NUMERIC,
                                          v_section_director         VARCHAR,
                                          v_director                 VARCHAR,
                                          v_vs_employee_code         VARCHAR,
                                          v_resident_employee_code   VARCHAR,
                                          v_refresh_employee_code    VARCHAR,
                                          v_master_interne           VARCHAR,
                                          v_interne                  VARCHAR,
                                          v_coding_user              VARCHAR,
                                          v_medical_quality_id       NUMERIC,
                                          v_quality_control_doctor   VARCHAR,
                                          v_quality_control_nurse    VARCHAR,
                                          v_quality_control_date     VARCHAR,
                                          v_xay_sn                   VARCHAR,
                                          v_ct_sn                    VARCHAR,
                                          v_mri_sn                   VARCHAR,
                                          v_dsa_sn                   VARCHAR,
                                          v_is_first_case            NUMERIC,
                                          v_is_following             NUMERIC,
                                          v_following_ending_date    VARCHAR,
                                          v_is_teaching_case         NUMERIC,
                                          v_blood_type_id            NUMERIC,
                                          v_rh                       NUMERIC,
                                          v_blood_reaction_id        NUMERIC,
                                          v_blood_rbc                NUMERIC,
                                          v_blood_plt                NUMERIC,
                                          v_blood_plasma             NUMERIC,
                                          v_blood_wb                 NUMERIC,
                                          v_blood_others             VARCHAR,
                                          v_is_completed             VARCHAR,
                                          v_completed_time           VARCHAR,
                                          v_modified_user            VARCHAR) AS
  BEGIN
    UPDATE iem_mainpage_basicinfo
       SET patnoofhis               = v_patnoofhis,
           noofinpat                = v_noofinpat,
           payid                    = v_payid,
           socialcare               = v_socialcare,
           incount                  = v_incount,
           NAME                     = v_name,
           sexid                    = v_sexid,
           birth                    = v_birth,
           marital                  = v_marital,
           jobid                    = v_jobid,
           provinceid               = v_provinceid,
           countyid                 = v_countyid,
           nationid                 = v_nationid,
           nationalityid            = v_nationalityid,
           idno                     = v_idno,
           ORGANIZATION             = v_organization,
           officeplace              = v_officeplace,
           officetel                = v_officetel,
           officepost               = v_officepost,
           nativeaddress            = v_nativeaddress,
           nativetel                = v_nativetel,
           nativepost               = v_nativepost,
           contactperson            = v_contactperson,
           relationship             = v_relationship,
           contactaddress           = v_contactaddress,
           contacttel               = v_contacttel,
           admitdate                = v_admitdate,
           admitdept                = v_admitdept,
           admitward                = v_admitward,
           days_before              = v_days_before,
           trans_date               = v_trans_date,
           trans_admitdept          = v_trans_admitdept,
           trans_admitward          = v_trans_admitward,
           trans_admitdept_again    = v_trans_admitdept_again,
           outwarddate              = v_outwarddate,
           outhosdept               = v_outhosdept,
           outhosward               = v_outhosward,
           actual_days              = v_actual_days,
           death_time               = v_death_time,
           death_reason             = v_death_reason,
           admitinfo                = v_admitinfo,
           in_check_date            = v_in_check_date,
           pathology_diagnosis_name = v_pathology_diagnosis_name,
           pathology_observation_sn = v_pathology_observation_sn,
           ashes_diagnosis_name     = v_ashes_diagnosis_name,
           ashes_anatomise_sn       = v_ashes_anatomise_sn,
           allergic_drug            = v_allergic_drug,
           hbsag                    = v_hbsag,
           hcv_ab                   = v_hcv_ab,
           hiv_ab                   = v_hiv_ab,
           opd_ipd_id               = v_opd_ipd_id,
           in_out_inpatinet_id      = v_in_out_inpatinet_id,
           before_after_or_id       = v_before_after_or_id,
           clinical_pathology_id    = v_clinical_pathology_id,
           pacs_pathology_id        = v_pacs_pathology_id,
           save_times               = v_save_times,
           success_times            = v_success_times,
           section_director         = v_section_director,
           director                 = v_director,
           vs_employee_code         = v_vs_employee_code,
           resident_employee_code   = v_resident_employee_code,
           refresh_employee_code    = v_refresh_employee_code,
           master_interne           = v_master_interne,
           interne                  = v_interne,
           coding_user              = v_coding_user,
           medical_quality_id       = v_medical_quality_id,
           quality_control_doctor   = v_quality_control_doctor,
           quality_control_nurse    = v_quality_control_nurse,
           quality_control_date     = v_quality_control_date,
           xay_sn                   = v_xay_sn,
           ct_sn                    = v_ct_sn,
           mri_sn                   = v_mri_sn,
           dsa_sn                   = v_dsa_sn,
           is_first_case            = v_is_first_case,
           is_following             = v_is_following,
           following_ending_date    = v_following_ending_date,
           is_teaching_case         = v_is_teaching_case,
           blood_type_id            = v_blood_type_id,
           rh                       = v_rh,
           blood_reaction_id        = v_blood_reaction_id,
           blood_rbc                = v_blood_rbc,
           blood_plt                = v_blood_plt,
           blood_plasma             = v_blood_plasma,
           blood_wb                 = v_blood_wb,
           blood_others             = v_blood_others,
           is_completed             = v_is_completed,
           completed_time           = v_completed_time,
           modified_user            = v_modified_user,
           modified_time            = TO_CHAR(SYSDATE,
                                              'yyyy-mm-dd HH24:mi:ss')
     WHERE iem_mainpage_no = v_iem_mainpage_no
       AND valide = 1;
  END;

  /***************************************************************************************/
  /*���²��˾�����Ϣ*/
  PROCEDURE usp_updatadiacrisisinfo(v_noofinpat      NUMERIC, --��ҳ���(סԺ��ˮ��)(Inpatient.NoOfInpat)
                                    v_admitdiagnosis VARCHAR2 --��Ժ���
                                    ) AS
  BEGIN
    --�޸�
    UPDATE inpatient
       SET admitdiagnosis = v_admitdiagnosis
     WHERE noofinpat = v_noofinpat;
  END;

  /***************************************************************************************/
  /*���²��˻�����Ϣ*/
  PROCEDURE usp_updatabasepatientinfo(v_noofinpat     NUMERIC, --��ҳ���(סԺ��ˮ��)
                                      v_birth         VARCHAR, --��������
                                      v_marital       VARCHAR, --����״��
                                      v_nationid      VARCHAR, --�������
                                      v_nationalityid VARCHAR, --��������
                                      v_sexid         VARCHAR, --�����Ա�
                                      v_edu           VARCHAR, --�Ļ��̶�
                                      v_provinceid    VARCHAR, --(������)ʡ�д���
                                      v_countyid      VARCHAR, --(������)���ش���
                                      v_nativeplace_p VARCHAR, --����ʡ�д���
                                      v_nativeplace_c VARCHAR, --�������ش���
                                      v_payid         VARCHAR, --��������
                                      v_age           INT, --��������
                                      v_religion      VARCHAR, --�ڽ�����
                                      v_educ          NUMERIC, --(��)��������(��λ:��)
                                      v_idno          VARCHAR, --���֤��
                                      v_jobid         VARCHAR, --ְҵ����
                                      v_organization  VARCHAR, --������λ(��ȱ)
                                      v_officeplace   VARCHAR,
                                      --��λ��ַ
                                      v_officepost    VARCHAR, --��λ�ʱ�
                                      v_officetel     VARCHAR, --��λ�绰
                                      v_nativeaddress VARCHAR, --���ڵ�ַ
                                      v_nativetel     VARCHAR, --���ڵ绰
                                      v_nativepost    VARCHAR, --�����ʱ�
                                      v_address       VARCHAR --��ǰ��ַ
                                      ) AS
  BEGIN
    --�޸�
    UPDATE inpatient
       SET birth         = v_birth,
           marital       = v_marital,
           nationid      = v_nationid,
           nationalityid = v_nationalityid,
           sexid         = v_sexid,
           edu           = v_edu,
           provinceid    = v_provinceid,
           countyid      = v_countyid,
           nativeplace_p = v_nativeplace_p,
           nativeplace_c = v_nativeplace_c,
           payid         = v_payid,
           age           = v_age,
           religion      = v_religion,
           educ          = v_educ,
           idno          = v_idno,
           jobid         = v_jobid,
           ORGANIZATION  = v_organization,
           officeplace   = v_officeplace,
           officepost    = v_officepost,
           officetel     = v_officetel,
           nativeaddress = v_nativeaddress,
           nativetel     = v_nativetel,
           nativepost    = v_nativepost,
           address       = v_address
     WHERE noofinpat = v_noofinpat;
  END;

  /***************************************************************************************/
  /*��ѯԱ��*/
  PROCEDURE usp_selectusers(v_deptid   VARCHAR,
                            v_username VARCHAR,
                            o_result   OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT users.name      username,
             department.name deptname,
             users.deptid    deptid,
             users.id        userid,
             users.py,
             users.wb
        FROM users
        left join department
          on users.deptid = department.id
       WHERE (users.name = v_username or v_username = '' or
             v_username is null)
         and (users.deptid = v_deptid or v_deptid = '' or v_deptid = '0000' or
             v_deptid is null)
         and users.valid = 1;
  END;

  /*************************************************************************************/
  PROCEDURE usp_symbolmanager(v_type               VARCHAR,
                              v_symbolcategroyid   INT DEFAULT 0, --�����ַ���������
                              v_symbolcategoryname VARCHAR DEFAULT '', --�����ַ���������
                              v_symbolcategorymemo VARCHAR DEFAULT '', --�����ַ����ͱ�ע
                              --  v_SymbolRTF  varchar default '',--�����ַ�����ID(Inpatient.NoOfInpat)
                              --  v_SymbolCategroyID int  default 0,  --�����ַ�����
                              --  v_SymbolLength int  default  0,       --�����ַ�����
                              --  v_SymbolMemo varchar default  '' --�����ַ���ע
                              o_result OUT empcurtyp) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]
    [����]
    [��Ȩ]
    [����] �����ַ�����ģ���и�����롢��ȡ��ţ�
    [����˵��]
    [�������]
    [�������]
    [�����������]
    [���õ�sp]
    [����ʵ��]
    
    [�޸ļ�¼]
    **********/
  BEGIN
    OPEN o_result FOR
      SELECT '' FROM DUAL;
  
    IF v_type = 'InsertSymCategory' THEN
      --���������ַ�����
      INSERT INTO symbolcategory
        (ID, NAME, memo)
      VALUES
        (seq_symbolcategory_id.NEXTVAL,
         v_symbolcategoryname /* Name  */,
         v_symbolcategorymemo /* Memo  */);
      --��ѯ�����ַ������µı��
    ELSIF v_type = 'SelectSymCategoryID' THEN
      OPEN o_result FOR
        SELECT NVL((SELECT MAX(a.ID) + 1 FROM symbolcategory a), 1)
          FROM DUAL;
      --���������ַ����ͱ�Ų�ѯ�������ַ����
    ELSIF v_type = 'SelectSymbolID' THEN
      OPEN o_result FOR
        SELECT NVL((SELECT MAX(a.ID) + 1
                     FROM symbols a
                    WHERE a.categroyid = v_symbolcategroyid),
                   v_symbolcategroyid * 1000 + 0)
          FROM DUAL;
    END IF;
  END;

  /***************************************************************************************/
  /*������в�����Ϣ*/
  PROCEDURE usp_selectward(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM ward where ward.valid = 1;
  END;

  /***************************************************************************************/
  /*������и�λ��Ϣ*/
  PROCEDURE usp_selectpermission(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM job2permission;
  END;

  /***************************************************************************************/
  /*���ĳ��Ա����Ϣ*/
  PROCEDURE usp_selectjob(v_id VARCHAR, o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM jobs WHERE ID = v_id;
  END;

  /***************************************************************************************/
  /*������в�����Ϣ*/
  PROCEDURE usp_selectdepartment(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM department;
  END;

  /***************************************************************************************/
  /*�������Ա����Ϣ*/
  PROCEDURE usp_selectallusers2(o_result  OUT empcurtyp,
                                o_result1 OUT empcurtyp) AS
  BEGIN
    /*    OPEN o_result FOR
    SELECT a.*, b.NAME AS deptname, c.NAME AS wardname
      FROM users a
      LEFT JOIN department b ON a.deptid = b.ID
      LEFT JOIN ward c ON a.wardid = c.ID;*/
    OPEN o_result FOR
      select *
        from department a
       where exists (select 1
                from users b
               where a.valid = 1
                 and a.id = b.deptid)
       order by a.id;
  
    OPEN o_result1 FOR
      select * from users;
  
    /*      open o_result for
    
        SELECT a.deptid,b.NAME AS deptname,a.*,  c.NAME AS wardname
    FROM users a
    LEFT JOIN department b ON a.deptid = b.ID
    LEFT JOIN ward c ON a.wardid = c.ID
    order by b.id ;
    
    open o_result for select '' from dual;*/
  END;

  /***************************************************************************************/
  /*�������Ա����Ϣ*/
  PROCEDURE usp_selectallusers(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM users;
  END;

  /***************************************************************************************/
  /*�������Ա����Ϣ(�����ʾ������)*/
  procedure usp_selectuserinfo(o_result out empcurtyp) as
  begin
    open o_result for
      select users.id,
             users.name,
             users.birth,
             users.idno,
             users.marital,
             users.sexy,
             users.deptid,
             users.wardid,
             users.regdate,
             department.name deptname,
             ward.name       wardname,
             dict1.name      maritalname,
             dict2.name      sexname
        from users
        left join department
          on department.id = users.deptid
        left join ward
          on ward.id = users.wardid
        left join dictionary_detail dict1
          on dict1.categoryid = '4'
         and dict1.detailid = users.marital
        left join dictionary_detail dict2
          on dict2.categoryid = '3'
         and dict2.detailid = users.sexy
       where users.valid = 1;
  end;

  /***************************************************************************************/
  /*������и�λ��Ϣ*/
  PROCEDURE usp_selectalljobs(o_result OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
      SELECT * FROM jobs;
  END;

  /***************************************************************************************/
  /*����������ĵ��Ӳ�����Ϣ*/
  PROCEDURE usp_saveapplyrecord(v_noofinpat   NUMERIC, --��ҳ���(סԺ��ˮ��)
                                v_applydoctor VARCHAR, --����ҽ������
                                v_deptid      VARCHAR, --���Ҵ���
                                v_applyaim    VARCHAR, --����Ŀ��
                                v_duetime     NUMERIC, --��������
                                v_unit        VARCHAR --���޵�λ
                                ) AS
  BEGIN
    INSERT INTO applyrecord
      (ID,
       noofinpat,
       applydoctor,
       deptid,
       applyaim,
       duetime,
       unit,
       applydate,
       status)
    VALUES
      (seq_applyrecord_id.NEXTVAL,
       v_noofinpat,
       v_applydoctor,
       v_deptid,
       v_applyaim,
       v_duetime,
       v_unit,
       TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss'),
       '5201');
  END;

  /***************************************************************************************/
  /*��ȡ������Ϣά������ؼ���ʼ������*/
  PROCEDURE usp_redactpatientinfofrm(v_frmtype VARCHAR,
                                     --�������ͣ�1n��������Ϣ����ά�����塢2n�����˲���ʷ��Ϣ����
                                     v_noofinpat  NUMERIC DEFAULT '0', --��ҳ���
                                     v_categoryid VARCHAR DEFAULT '',
                                     --(�ֵ�)������ ���򸸽ڵ���� ����ҳ���
                                     o_result OUT empcurtyp) AS
  BEGIN
    --  print v_FrmType
    --  print v_CategoryID
    IF v_frmtype = '11' THEN
      OPEN o_result FOR
      --ʡ�д���(������Ϣ����ά������)
        SELECT parentid, ID, NAME, py, wb
          FROM areas
         WHERE TRIM(CATEGORY) = v_categoryid
         ORDER BY NAME;
    ELSIF v_frmtype = '12' THEN
      OPEN o_result FOR
      --���ش���(������Ϣ����ά������)
        SELECT parentid, ID, NAME, py, wb
          FROM areas
         WHERE TRIM(parentid) = v_categoryid
         ORDER BY NAME;
    ELSIF v_frmtype = '13' THEN
      OPEN o_result FOR
      --�ֵ������ϸ��(������Ϣ����ά������)
        SELECT categoryid, detailid, NAME, py, wb lens
          FROM dictionary_detail
         WHERE TRIM(categoryid) = v_categoryid
         ORDER BY detailid;
    ELSIF v_frmtype = '14' THEN
      OPEN o_result FOR
      --���˻�����Ϣ
        SELECT ip.*,
               u.NAME     AS clinicdoctorname,
               dd1.NAME   AS stylename,
               dd2.NAME   AS originname,
               d.NAME     AS clinicdiagnosisname,
               cd.NAME    AS statusname,
               dd3.NAME   AS criticallevelname,
               dd4.NAME   AS admitinfoname,
               dd5.NAME   AS admitwayname,
               dept1.NAME AS admitdeptname,
               w1.NAME    AS admitwardname,
               dept2.NAME AS outhosdeptname,
               w2.NAME    AS outhoswardname,
               dd6.NAME   AS gender,
               dd7.NAME   AS marriage,
               dd8.NAME   AS jobname
          FROM inpatient ip
          LEFT JOIN dictionary_detail dd1
            ON dd1.categoryid = '45'
           AND dd1.detailid = ip.style
          LEFT JOIN dictionary_detail dd2
            ON dd2.categoryid = '2'
           AND dd2.detailid = ip.origin
          LEFT JOIN users u
            ON u.ID = ip.clinicdoctor
          LEFT JOIN diagnosis d
        /*add by ywk 2013��4��9��16:51:44 */
            ON d.icd = ip.clinicdiagnosis
           and d.tumorid <> ' '
        /* ON d.icd = ip.clinicdiagnosis*/
          LEFT JOIN categorydetail cd
            ON cd.ID = ip.status
           AND cd.categoryid = '15'
          LEFT JOIN dictionary_detail dd3
            ON dd3.categoryid = '53'
           AND dd3.detailid = ip.criticallevel
          LEFT JOIN dictionary_detail dd4
            ON dd4.categoryid = '5'
           AND dd4.detailid = ip.admitinfo
          LEFT JOIN dictionary_detail dd5
            ON dd5.categoryid = '6'
           AND dd5.detailid = ip.admitway
          LEFT JOIN department dept1
            ON dept1.ID = ip.admitdept
          LEFT JOIN ward w1
            ON w1.ID = ip.admitward
          LEFT JOIN department dept2
            ON dept2.ID = ip.outhosdept
          LEFT JOIN ward w2
            ON w2.ID = ip.outhosward
        --Add By wwj 2011-05-17
          LEFT JOIN dictionary_detail dd6
            ON dd6.categoryid = '3'
           AND dd6.detailid = ip.sexid
          LEFT JOIN dictionary_detail dd7
            ON dd7.categoryid = '4'
           AND dd7.detailid = ip.marital
          LEFT JOIN dictionary_detail dd8
            ON dd8.categoryid = '41'
           AND dd8.detailid = ip.jobid
         WHERE TRIM(ip.noofinpat) = v_noofinpat;
    ELSIF v_frmtype = '15' THEN
      OPEN o_result FOR
      --��һ��ϵ����Ϣ(������Ϣ����ά������)
        SELECT CASE
                 WHEN tag = '1' THEN
                  '��'
                 ELSE
                  '��'
               END AS tagname,
               a.*
          FROM patientcontacts a
         WHERE TRIM(a.noofinpat) = v_noofinpat;
      --        select ID,pc.Name,dd1.Name as SexName,dd2.Name as RelationName,Address,WorkUnit,HomeTel,WorkTel,PostalCode,Tag
      --     from PatientContacts pc
      --     left join Dictionary_detail dd1 on dd1.CategoryID='3' and dd1.DetailID=Sex
      --     left join Dictionary_detail dd2 on dd2.CategoryID='44' and dd2.DetailID=Relation
      --     where NoOfInpat=v_NoOfInpat
    ELSIF v_frmtype = '16' THEN
      OPEN o_result FOR
      --��ȥ��ϲ���
        SELECT * FROM diagnosis;
    ELSIF v_frmtype = '21' THEN
      OPEN o_result FOR
      --��ȡ�ֵ����Ϣ
        SELECT categoryid, CAST(ID AS VARCHAR(4)) AS detailid, NAME, py, wb
          FROM categorydetail
         WHERE TRIM(categoryid) = v_categoryid;
    ELSIF v_frmtype = '22' THEN
      OPEN o_result FOR
      --��ȡ����ʷ
        SELECT cd.NAME relationexplain,
               datediff('yy', fh.birthday, TO_CHAR(SYSDATE, 'yyyy-mm-dd')) + 1 age,
               d.NAME diseasename,
               (CASE
                 WHEN breathing = 1 THEN
                  '��'
                 ELSE
                  '��'
               END) isbreathing,
               fh.*
          FROM familyhistory fh
          LEFT JOIN categorydetail cd
            ON cd.ID = fh.relation
           AND cd.categoryid = '62'
          LEFT JOIN diagnosis d
            ON d.icd = fh.diseaseid
         WHERE noofinpat = v_noofinpat;
    ELSIF v_frmtype = '23' THEN
      OPEN o_result FOR
      --��ȡ����ʷ
        SELECT * FROM personalhistory;
    ELSIF v_frmtype = '24' THEN
      OPEN o_result FOR
      --��ȡ����ʷ
        SELECT ah.*, cd1.NAME AS allergyname, cd2.NAME AS allergylevelname
          FROM allergyhistory ah
          LEFT JOIN categorydetail cd1
            ON cd1.ID = ah.allergyid
           AND cd1.categoryid = '60'
          LEFT JOIN categorydetail cd2
            ON cd2.ID = ah.allergylevel
           AND cd2.categoryid = '61'
         WHERE noofinpat = v_noofinpat;
    ELSIF v_frmtype = '25' THEN
      OPEN o_result FOR
      --��ȡ��������
        SELECT * FROM surgery;
    ELSIF v_frmtype = '26' THEN
      OPEN o_result FOR
      --��ȡ����ʷ
        SELECT sh.*, s.NAME AS surgeryname, d.NAME AS diagnosisname
          FROM surgeryhistory sh
          LEFT JOIN surgery s
            ON s.ID = sh.surgeryid
          LEFT JOIN diagnosis d
            ON d.icd = sh.diagnosisid
         WHERE noofinpat = v_noofinpat;
    ELSIF v_frmtype = '27' THEN
      OPEN o_result FOR
      --��ȡ����ʷ
        SELECT ih.*,
               d.NAME AS diagnosisname,
               (CASE
                 WHEN cure = 1 THEN
                  '��'
                 ELSE
                  '��'
               END) iscure
          FROM illnesshistory ih
          LEFT JOIN diagnosis d
            ON d.icd = ih.diagnosisicd
         WHERE noofinpat = v_noofinpat;
    END IF;
  END;

  /***************************************************************************************/
  /*��ȡ��������ͳ������(�в�ѯ����)*/
  PROCEDURE usp_queryqcstatinfo(v_datetimebegin VARCHAR, --��ʼʱ��
                                v_datetimeend   VARCHAR, --����ʱ��
                                o_result        OUT empcurtyp) AS
  BEGIN
    OPEN o_result FOR
    --��Ժ����
      SELECT COUNT(*) statnumber, '��Ժ����' statitem, 1 qctype
        FROM inpatient
       WHERE status IN (1501, 1505, 1506, 1507)
      UNION
      ----��Ժ���ύ
      --SELECT count(*) statnumber,'��Ժ���ύ' statitem,2 qctype FROM InPatient inp JOIN MedicalRecord med
      --ON inp.NoOfInpat = med.NoOfInpat AND med.LockInfo IN (4701) AND CONVERT(varchar(10),AdmitDate,102) >= v_DateTimeBegin
      --AND CONVERT(varchar(10),AdmitDate,102) <= v_DateTimeEnd
      --WHERE inp.Status IN  (1502,1503)
      --UNION
      --����Ժ����
      SELECT COUNT(*) statnumber, '����Ժ����' statitem, 2 qctype
        FROM inpatient
       WHERE status IN (1501, 1505, 1506, 1507)
      --AND CONVERT(varchar(10),AdmitDate,102) >= v_DateTimeBegin
      --AND CONVERT(varchar(10),AdmitDate,102) <= v_DateTimeEnd
      UNION
      --����Υ����
      SELECT COUNT(*) statnumber, 'Υ�没��' statitem, 3 qctype
        FROM qcrecord a
        JOIN inpatient b
          ON a.noofinpat = b.noofinpat
         AND b.status IN (1500, 1501, 1504, 1505, 1506, 1507)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
        JOIN qcrule d
          ON a.rulecode = d.rulecode
        LEFT JOIN users c
          ON a.docotorid = c.ID
       WHERE a.valid != 0
         AND a.foulstate IN (1, 3)
      --
      UNION
      --Σ�ز���
      SELECT COUNT(*) statnumber, 'Σ�ز���' statitem, 4 qctype
        FROM inpatient
       WHERE criticallevel = 1
         AND status IN (1500, 1501, 1504, 1505, 1506, 1507)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
      UNION
      --����������,��Ҫ�޸�
      SELECT COUNT(*) statnumber, '��������' statitem, 5 qctype
        FROM inpatient
       WHERE status IN (1500, 1501, 1504, 1505, 1506, 1507)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
         AND (TO_NUMBER(SYSDATE - admitdate) > 30)
      --
      UNION
      --����������,��Ҫ�޸�
      SELECT COUNT(*) statnumber, '��������' statitem, 6 qctype
        FROM inpatient
       WHERE status IN (1500, 1501, 1504, 1505, 1506, 1507)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
         AND (TO_NUMBER(SYSDATE - admitdate) > 30)
      --סԺ����30��
      UNION
      SELECT COUNT(*) statnumber, 'סԺ����30��' statitem, 7 qctype
        FROM inpatient
       WHERE status IN (1500, 1501, 1504, 1505, 1506, 1507)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
         AND (TO_NUMBER(SYSDATE - admitdate) > 30)
      --��Ժδ�ύ
      UNION
      SELECT COUNT(*) statnumber, '��Ժδ�ύ' statitem, 8 qctype
        FROM inpatient inp
        JOIN medicalrecord med
          ON inp.noofinpat = med.noofinpat
         AND med.lockinfo IN (4700, 4702, 4703)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
       WHERE inp.status IN (1502, 1503)
      --�鵵����
      UNION
      SELECT COUNT(*) statnumber, '�鵵����' statitem, 9 qctype
        FROM inpatient inp
        JOIN medicalrecord med
          ON inp.noofinpat = med.noofinpat
         AND med.lockinfo IN (4700, 4702, 4703)
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') >= v_datetimebegin
         AND TO_CHAR(admitdate, 'yyyy.mm.dd') <= v_datetimeend
       WHERE inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
       ORDER BY qctype;
  END;

  /***************************************************************************************/
  /*��ȡ��������ͳ������*/
  PROCEDURE usp_queryqcstatdetailinfo(v_datetimebegin VARCHAR, --��ʼʱ��
                                      v_datetimeend   VARCHAR, --����ʱ��
                                      v_qcstattype    INT, --ͳ����������
                                      o_result        OUT empcurtyp) AS
  BEGIN
    IF v_qcstattype = 1 THEN
      OPEN o_result FOR
      --��Ժ���� isnull(,'-')
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 2 THEN
      OPEN o_result FOR
      --��Ժ���ύ
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          JOIN medicalrecord med
            ON inp.noofinpat = med.noofinpat
           AND med.lockinfo IN (4701)
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1502, 1503)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 3 THEN
      OPEN o_result FOR
      --����Υ����
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM qcrecord a
          JOIN inpatient inp
            ON a.noofinpat = inp.noofinpat
           AND inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
          JOIN qcrule d
            ON a.rulecode = d.rulecode
          LEFT JOIN users c
            ON a.docotorid = c.ID
         WHERE a.valid != 0
           AND a.foulstate IN (1, 3)
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 4 THEN
      OPEN o_result FOR
      --Σ�ز���
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.criticallevel = 1
           AND inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 5 THEN
      OPEN o_result FOR
      --����������,��Ҫ�޸�
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
           AND (TO_NUMBER(SYSDATE - inp.admitdate) > 30)
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 6 THEN
      OPEN o_result FOR
      --����������,��Ҫ�޸�
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
           AND (TO_NUMBER(SYSDATE - inp.admitdate) > 30)
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 7 THEN
      OPEN o_result FOR
      --סԺ����30��
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
           AND (TO_NUMBER(SYSDATE - inp.admitdate) > 30)
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 8 THEN
      OPEN o_result FOR
      --��Ժδ�ύ
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          JOIN medicalrecord med
            ON inp.noofinpat = med.noofinpat
           AND med.lockinfo IN (4700, 4702, 4703)
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1502, 1503)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
         ORDER BY inp.noofinpat;
    ELSIF v_qcstattype = 9 THEN
      OPEN o_result FOR
      --�鵵����
        SELECT inp.noofinpat AS noofinpat,
               inp.patnoofhis AS patnoofhis,
               inp.patid AS patid,
               inp.NAME AS NAME,
               inp.sexid AS sexid,
               dd.NAME AS sexname,
               inp.agestr AS agestr,
               bed.wardid AS wardid,
               bed.deptid AS deptid,
               bed.ID AS bedid,
               inp.admitdate AS admitdate,
               bed.wardid,
               bed.deptid,
               ward.NAME AS wardname,
               de.NAME AS deptname,
               NULL cycletimes,
               NULL deduction,
               (SELECT COUNT(noofinpat)
                  FROM recorddetail
                 WHERE recorddetail.noofinpat = inp.noofinpat) AS copies,
               NVL(inp.NAME, '-') || '/' || NVL(dd.NAME, '-') || '/' ||
               NVL(inp.agestr, '-') || '/' || NVL(ward.NAME, '-') || '/' ||
               NVL(de.NAME, '-') || '/' || NVL(bed.ID, '-') || '��/' ||
               NVL(inp.admitdate, '-') || '��Ժ' AS showinfo
          FROM inpatient inp
          JOIN medicalrecord med
            ON inp.noofinpat = med.noofinpat
           AND med.lockinfo IN (4700, 4702, 4703)
          LEFT JOIN dictionary_detail dd
            ON dd.categoryid = '3'
           AND inp.sexid = dd.detailid
          LEFT JOIN bed bed
            ON inp.noofinpat = bed.noofinpat
           AND inp.patnoofhis = bed.patnoofhis
           AND bed.inbed = 1301
          LEFT JOIN ward ward
            ON bed.wardid = ward.ID
          LEFT JOIN department de
            ON bed.deptid = de.ID
         WHERE inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') >= v_datetimebegin
           AND TO_CHAR(inp.admitdate, 'yyyy.mm.dd') <= v_datetimeend
         ORDER BY inp.noofinpat;
    END IF;
  END;

  /***************************************************************************************/
  /*��ȡ����Ǽ�����*/
  PROCEDURE usp_queryqcprobleminfo(v_noofinpat INT, o_result OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ����Ǽ�����
     ����˵��
     �������
      v_NoOfInpat int
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryQCProblemInfo 2
     �޸ļ�¼
    **********/
  BEGIN
    OPEN o_result FOR
      SELECT qcd.ID,
             ip.NAME,
             ip.patid,
             TO_CHAR(qcd.problemdate, 'yyyy.mm.dd') AS problemdate,
             qcd.typecode AS typecode,
             qct.typename AS typename,
             TO_CHAR(qcd.registerdate, 'yyyy.mm.dd') AS registerdate,
             us1.NAME AS registername,
             TO_CHAR(qcd.answerdate, 'yyyy.mm.dd') AS answerdate,
             us2.NAME AS ansewername,
             TO_CHAR(qcd.confirmdate, 'yyyy.mm.dd') AS confirmdate,
             us3.NAME AS confirmuser,
             qcd.CATEGORY,
             qcd.noofinpat,
             CASE qcd.CATEGORY
               WHEN 0 THEN
                '�ύ'
               WHEN 1 THEN
                '���'
               WHEN 2 THEN
                '����'
             END categoryname,
             qcd.status,
             CASE qcd.status
               WHEN 0 THEN
                '�Ǽ�'
               WHEN 1 THEN
                '��'
             END statusname,
             qcd.description,
             qcd.ansewercontent,
             qcd.confirmcontent
        FROM qcproblemdescription qcd
        JOIN inpatient ip
          ON qcd.noofinpat = ip.noofinpat
        LEFT JOIN users us1
          ON qcd.registeruser = us1.ID
        LEFT JOIN users us2
          ON qcd.answeruser = us2.ID
        LEFT JOIN users us3
          ON qcd.confirmuser = us3.ID
        JOIN qcscoretype qct
          ON qcd.typecode = qct.typecode
       WHERE qcd.noofinpat = v_noofinpat
         AND qcd.CATEGORY NOT IN (2)
       ORDER BY qcd.registerdate;
  END;

  /***************************************************************************************/
  /*��ȡ�������Ʋ�������*/
  PROCEDURE usp_queryqcpatientinfo(v_datetimefrom VARCHAR,
                                   v_datetimeto   VARCHAR,
                                   v_deptid       VARCHAR,
                                   v_wardid       VARCHAR,
                                   v_bedid        VARCHAR,
                                   v_name         VARCHAR,
                                   v_archives     VARCHAR,
                                   o_result       OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ�������Ʋ�������
     ����˵��
     �������
      v_DateTimeFrom varchar(9) --��ʼʱ��
     ,v_DateTimeTo  varchar(9) --����ʱ��
     ,v_DeptId varchar(10) --����
     ,v_WardId varchar(10) --����
     ,v_bedId varchar (10) --��λ
     ,v_name varchar(20) --����
     ,v_archives varchar(1)--�鵵����
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryQCPatientInfo '2007-06-01', '2010-06-30','','','','',''
     �޸ļ�¼
    ,�����ʿش��� CycleTimes,�����ʿؿ۷� Deduction,��д������� Copies
    **********/
    v_sql VARCHAR(3000);
  BEGIN
    v_sql := 'SELECT inp.NoOfInpat AS NoOfInpat,inp.PatNoOfHis AS PatNoOfHis,inp.PatID AS PatID
,inp.Name AS Name,inp.SexID AS SexID,dd.Name AS SexName,inp.AgeStr AS AgeStr
, bed.WardId AS WardId, bed.DeptID AS DeptID, bed.ID  AS BedID,inp.AdmitDate AS AdmitDate
,bed.WardId,bed.DeptID,ward.Name AS wardname,de.Name AS deptname
,NULL CycleTimes,NULL Deduction
,(SELECT count(NoOfInpat)  FROM RecordDetail where RecordDetail.NoOfInpat = inp.NoOfInpat) as Copies
, isnull(inp.Name,''-'') ||''/''|| isnull(dd.Name,''-'')||''/''|| isnull(inp.AgeStr,''-'')
||''/''||isnull(ward.Name,''-'')||''/''||isnull(de.Name,''-'') ||''/'' || isnull(bed.ID,''-'') ||''��/''
|| isnull(inp.AdmitDate,''-'') || ''��Ժ'' as showinfo
FROM InPatient inp';
  
    IF v_archives = '0' THEN
      --δ�鵵
      BEGIN
        v_sql := v_sql ||
                 ' JOIN MedicalRecord med ON inp.NoOfInpat = med.NoOfInpat AND med.LockInfo IN (4700)';
      END;
    ELSIF v_archives = '1' THEN
      --�ѹ鵵
      BEGIN
        v_sql := v_sql ||
                 ' JOIN MedicalRecord med ON inp.NoOfInpat = med.NoOfInpat AND med.LockInfo IN (4701)';
      END;
    ELSIF v_archives = '2' THEN
      --�����鵵
      BEGIN
        v_sql := v_sql ||
                 ' JOIN MedicalRecord med ON inp.NoOfInpat = med.NoOfInpat AND med.LockInfo IN (4702)';
      END;
    END IF;
  
    v_sql := v_sql ||
             ' LEFT JOIN Dictionary_detail dd  ON dd.CategoryID = ''3'' AND inp.SexID = dd.DetailID
LEFT JOIN Bed bed  ON inp.NoOfInpat = bed.NoOfInpat and inp.PatNoOfHis =bed.PatNoOfHis and bed.InBed = 1301';
  
    IF v_bedid != '' AND v_bedid IS NOT NULL THEN
      BEGIN
        v_sql := v_sql || ' and isnull(bed.ID,'''') like ''' || '%' ||
                 v_bedid || '''';
      END;
    END IF;
  
    v_sql := v_sql || ' left join Ward ward  on bed.WardId = ward.ID';
  
    IF v_wardid != '' AND v_wardid IS NOT NULL THEN
      BEGIN
        v_sql := v_sql || ' and isnull(ward.ID,'''') = ''' || v_wardid || '''';
      END;
    END IF;
  
    v_sql := v_sql || ' left join Department de  on bed.DeptID = de.ID';
  
    IF v_deptid != '' AND v_deptid IS NOT NULL THEN
      BEGIN
        v_sql := v_sql || ' and isnull(de.ID,'''') = ''' || v_deptid || '''';
      END;
    END IF;
  
    v_sql := v_sql ||
             ' WHERE inp.Status NOT IN (1509) AND CONVERT(varchar(10),inp.AdmitDate,102) >= ''' ||
             v_datetimefrom || '''';
    v_sql := v_sql || ' AND CONVERT(varchar(10),inp.AdmitDate,102) <= ''' ||
             v_datetimeto || '''';
  
    IF v_name != '' AND v_name IS NOT NULL THEN
      BEGIN
        v_sql := v_sql || ' and isnull(inp.Name,'''') like ''' || '%' ||
                 v_name || '%' || '''';
      END;
    END IF;
  
    v_sql := v_sql || ' order by inp.NoOfInpat';
  
    OPEN o_result FOR v_sql;
  END;

  /***************************************************************************************/
  /*������ҳ��Ż�ȡ���˶�Ӧ��Ϣ*/
  PROCEDURE usp_querypatientinfobynoofinp(v_noofinpat INT, --��ҳ���
                                          o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ������ҳ��Ż�ȡ���˶�Ӧ��Ϣ
     ����˵��
     �������
     v_NoOfInpat int --��ҳ���
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryPatientInfoByNoOfInpat 2
     �޸ļ�¼
    �����������
    **********/
  BEGIN
    OPEN o_result FOR
      SELECT inp.noofinpat AS noofinpat,
             inp.patnoofhis AS patnoofhis,
             inp.patid AS patid,
             inp.NAME AS NAME,
             inp.sexid AS sexid,
             dd.NAME AS sexname,
             inp.agestr AS agestr,
             bed.wardid AS wardid,
             bed.deptid AS deptid,
             bed.ID AS bedid,
             inp.admitdate AS admitdate,
             bed.wardid,
             bed.deptid,
             ward.NAME AS wardname,
             de.NAME AS deptname,
             inp.outhosdate AS outhosdate,
             did.NAME AS levelname,
             'һ������' AS carelevel,
             dia.NAME AS dianame,
             users.NAME AS doctorname,
             cad.NAME AS statusname
        FROM inpatient inp
        LEFT JOIN dictionary_detail dd
          ON dd.categoryid = '3'
         AND inp.sexid = dd.detailid
        LEFT JOIN bed bed
          ON inp.noofinpat = bed.noofinpat
         AND inp.patnoofhis = bed.patnoofhis
         AND bed.inbed = 1301
        LEFT JOIN ward ward
          ON bed.wardid = ward.ID
        LEFT JOIN department de
          ON bed.deptid = de.ID
        LEFT JOIN dictionary_detail did
          ON inp.criticallevel = did.detailid
         AND did.categoryid = '53'
        LEFT JOIN diagnosis dia
          ON inp.admitdiagnosis = dia.icd
        LEFT JOIN users
          ON inp.resident = users.ID
         AND users.valid = 1
        LEFT JOIN categorydetail cad
          ON inp.status = cad.ID
         AND cad.categoryid = '15'
       WHERE inp.noofinpat = v_noofinpat
       ORDER BY inp.noofinpat;
  END;

  /***************************************************************************************/
  /*��ȡ������ȡ���没���б�*/
  PROCEDURE usp_queryownmanagerpat2(v_querytype INT,
                                    v_userid    VARCHAR,
                                    v_deptid    VARCHAR,
                                    v_wardid    VARCHAR,
                                    o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������ȡ���没���б�
     ����˵��
     �������
     QueryType int     ��ѯ���
     UserID varchar(8) �û�ID
     DeptID varchar(6)     ���Ҵ���
     WardId varchar(6)     ��������
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    �����������
    **********/
    v_sql VARCHAR(3000);
  BEGIN
    IF v_querytype = 0 THEN
      --��ó��ֹ���Ĳ���
      BEGIN
        v_sql := 'select
    a.NoOfInpat  NoOfInpat--��ҳ���
  , a.PatNoOfHis PatNoOfHis --HIS��ҳ���
  , a.PatID PatID --סԺ��
  , a.Name PatName --����
  , a.SexID Sex  --�����Ա�
  , b.Name SexName  --�����Ա�����
  , a.AgeStr AgeStr  --����
  , rtrim(a.CriticalLevel) wzjb --Σ�ؼ���
  , a.Status arzt --����״̬
  , c.Name wzjbmc --Σ�ؼ�������
  , substring(a.AdmitDate,1,16) AdmitDate  --��Ժ����
  , d.ICD ryzd --��Ժ���
  , d.Name zdmc --�������
  --, e.Note pzlx --ƾ֤����
  , dd1.NAME pzlx --�������
  , f.DeptID ksdm
from InPatient a
left join Dictionary_detail b  on b.DetailID = a.SexID and b.CategoryID = ''3''
left join Dictionary_detail c  on c.DetailID = a.CriticalLevel and c.CategoryID = ''53''
left join Diagnosis d  on d.ICD = a.AdmitDiagnosis
--left join MedicareInfo e  ON a.VouchersCode = e.ID
LEFT JOIN Dictionary_detail dd1 ON dd1.categoryid = ''1'' AND a.payid = dd1.detailid
left join Bed f   on f.NoOfInpat = a.NoOfInpat and f.PatNoOfHis = a.PatNoOfHis and f.InBed = 1301
where a.Status in (1501,1504,1505,1506,1507)
and  not exists (select 1 from Doctor_AssignPatient da where da.NoOfInpat=a.NoOfInpat and ID=' ||
                 v_userid || ' and Valid=1)
and f.DeptID=''' || v_deptid || ''' and  f.WardId=''' ||
                 v_wardid || '''
';
      
        --exec sp_executesql v_sql
        OPEN o_result FOR v_sql;
      END;
    END IF;
  
    IF v_querytype = 1 THEN
      --��÷ֹܲ���
      BEGIN
        INSERT INTO doctor_assignpatient
          SELECT v_userid, noofinpat, 1, SYSDATE, v_userid, 1
            FROM inpatient a
           WHERE resident = v_userid
             AND status IN (1501, 1504, 1505, 1506, 1507)
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient da
                   WHERE da.noofinpat = a.noofinpat
                     AND ID = v_userid
                     AND valid = 1);
      
        OPEN o_result FOR
          SELECT a.noofinpat  noofinpat, --��ҳ���
                 a.patnoofhis patnoofhis,
                 
                 --HIS��ҳ���
                 a.patid  patid, --סԺ��
                 a.NAME   patname, --����
                 a.sexid  sex, --�����Ա�
                 b.NAME   sexname, --�����Ա�����
                 a.agestr agestr, --����
                 a.status arzt,
                 
                 --����״̬
                 c.NAME wzjbmc, --Σ�ؼ�������
                 c.detailid wzjb, --Σ�ؼ������
                 SUBSTR(a.admitdate, 1, 16) admitdate, --��Ժ����
                 d.icd ryzd,
                 
                 --��Ժ���
                 d.NAME zdmc, --�������
                 --e.note pzlx, --ƾ֤����
                 dd1.NAME pzlx, --�������
                 g.NAME   ksmc, --��������
                 h.NAME   bqmc,
                 
                 --��������
                 a.outbed bedid, --��Ժ��λ��
                 (SELECT CASE
                           WHEN COUNT(qc.foulstate) > 0 THEN
                            '��'
                           ELSE
                            '��'
                         END
                    FROM qcrecord qc
                   WHERE qc.noofinpat = da.noofinpat
                     AND qc.valid = 1
                     AND qc.foulstate = 1) AS iswarn --�Ƿ�Υ��
            FROM doctor_assignpatient da
            JOIN inpatient a
              ON da.noofinpat = a.noofinpat
             AND valid = 1
             AND da.ID = v_userid
            LEFT JOIN dictionary_detail b
              ON b.detailid = a.sexid
             AND b.categoryid = '3'
            LEFT JOIN dictionary_detail c
              ON c.detailid = a.criticallevel
             AND c.categoryid = '53'
            LEFT JOIN diagnosis d
              ON d.icd = a.admitdiagnosis
          --LEFT JOIN medicareinfo e ON a.voucherscode = e.ID
          
            LEFT JOIN Dictionary_detail dd1
              ON dd1.categoryid = '1'
             AND a.payid = dd1.detailid
            LEFT JOIN bed f
              ON f.noofinpat = a.noofinpat
             AND f.patnoofhis = a.patnoofhis
             AND f.inbed = 1301
            LEFT JOIN department g
              ON f.deptid = g.ID
            LEFT JOIN ward h
              ON f.wardid = g.ID
           WHERE a.status IN (1501, 1504, 1505, 1506, 1507)
          --group by  a.NoOfInpat,a.PatNoOfHis,a.PatID,a.Name,a.SexID,b.Name,a.AgeStr,a.Status,c.Name,substring(a.AdmitDate,1,16),d.ICD,d.Name,e.pzlx,g.Name,h.Name
          --and f.DeptID in DeptID
          ;
      END;
    END IF;
  END;

  /***************************************************************************************/
  /*��ȡ��Ӧ�����Ĵ�λ��Ϣ*/
  PROCEDURE usp_querynonarchivepatients(v_wardid  VARCHAR,
                                        v_deptids VARCHAR,
                                        o_result  OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��Ӧ�����Ĵ�λ��Ϣ
     ����˵��
     �������
      v_wardid  varchar(6)   --��������
      v_deptids varchar(255)  --���Ҵ��뼯��(����: '����1','����2')
     �������
     �����������
    ����δ�鵵�������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryInwardPatients  '2821', '7006'
     �޸ļ�¼
    
    **********/
    v_wardid_in VARCHAR2(6);
  BEGIN
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    
      OPEN o_result FOR
        SELECT b.noofinpat  noofinpat, --��ҳ���
               b.patnoofhis patnoofhis,
               
               --HIS��ҳ���
               b.patid  patid, --סԺ��
               b.NAME   inpatname, --����
               b.sexid  sexid, --�����Ա�
               b.agestr agestr, --����
               b.status status, --����״̬
               f.NAME   statusname,
               
               --����״̬����
               b.criticallevel criticallevel, --Σ�ؼ���
               h.NAME          criticallevelname,
               
               --Σ�ؼ�������
               z.NAME residentname, --סԺҽ��
               SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
               b.admitdiagnosis admitdiagnosis, --��Ժ���
               SUBSTR(b.outhosdate, 1, 16) outhosdate, --��Ժ����
               b.outbed outbed,
               
               --��Ժ��λ
               b.memo memo
        --��ע
          FROM inpatient b
         INNER JOIN medicalrecord k
            ON b.noofinpat = k.noofinpat
           AND k.lockinfo IN (4700, 4702, 4703)
          LEFT JOIN categorydetail f
            ON b.status = f.ID
          LEFT JOIN users z
            ON z.ID = b.resident
          LEFT JOIN dictionary_detail h
            ON h.detailid = b.criticallevel
           AND h.categoryid = '53'
         WHERE b.outhosdept = v_deptids
           AND (v_wardid_in = '' OR b.outhosward = v_wardid_in OR
               v_wardid_in IS NULL)
           AND b.status IN (1502, 1503, 1504);
    END IF;
  END;

  /***************************************************************************************/
  /*�ʿ�TypeScore����*/
  PROCEDURE usp_qctypescore(v_typename        VARCHAR default '',
                            v_typeinstruction VARCHAR default '',
                            v_typecategory    INT default 0,
                            v_typeorder       INT default 0,
                            v_typememo        VARCHAR default '',
                            v_typestatus      INT,
                            v_typecode        VARCHAR) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  �ʿ�TypeScore����
     ����˵��
     �������
     v_TypeName varchar(40)
     ,v_TypeInstruction  varchar(60)
     ,v_TypeCategory int
     ,v_TypeOrder int
     ,v_TypeMemo varchar(60)
    ,v_TypeStatus int
    ,v_TypeCode varchar(4)
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QCTypeScore  'test', 'test',0,0,'test',0
     �޸ļ�¼
    **********/
    v_typecodetemp VARCHAR(4);
  BEGIN
    IF v_typestatus = 0 THEN
      --����
      BEGIN
        SELECT 'T' || lpad((nvl(max(substr(typecode, 2)), 0) + 1), 3, '0')
          INTO v_typecodetemp
          FROM qcscoretype;
      
        INSERT INTO qcscoretype
          (typecode,
           typename,
           typeinstruction,
           typecategory,
           typeorder,
           typememo)
        VALUES
          (v_typecodetemp,
           v_typename,
           v_typeinstruction,
           v_typecategory,
           v_typeorder,
           v_typememo);
      END;
    ELSIF v_typestatus = 1 THEN
      --�޸�
      BEGIN
        UPDATE qcscoretype
           SET typename        = v_typename,
               typeinstruction = v_typeinstruction,
               typecategory    = v_typecategory,
               typeorder       = v_typeorder,
               typememo        = v_typememo
         WHERE typecode = v_typecode;
      END;
    ELSIF v_typestatus = 2 THEN
      --ɾ��
      BEGIN
        UPDATE qcscoretype SET valide = 0 WHERE typecode = v_typecode;
      END;
    END IF;
  END;

  /***************************************************************************************/
  /*��ȡ������������*/
  PROCEDURE usp_qcoperprobleminfo(v_id             INT,
                                  v_noofinpat      INT,
                                  v_category       INT,
                                  v_status         INT,
                                  v_typecode       VARCHAR,
                                  v_description    VARCHAR,
                                  v_ansewercontent VARCHAR,
                                  v_confirmcontent VARCHAR,
                                  v_problemdate    VARCHAR,
                                  v_registerdate   VARCHAR,
                                  v_registeruser   VARCHAR,
                                  v_answerdate     VARCHAR,
                                  v_answeruser     VARCHAR,
                                  v_confirmdate    VARCHAR,
                                  v_confirmuser    VARCHAR,
                                  v_deldate        VARCHAR,
                                  v_deluser        VARCHAR,
                                  v_operstatus     INT) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������������
     ����˵��
     �������
      v_DateTimeBegin varchar(9) --��ʼʱ��
     ,v_v_DateTimeEnd  varchar(9) --����ʱ��
     ,v_QCStatType int --ͳ����������
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QCOperProblemInfo '2007-06-01', '2010-06-30',1
     �޸ļ�¼
    **********/
    v_doctorid VARCHAR(6);
  BEGIN
    IF v_operstatus = 0 THEN
      --�ύ
      BEGIN
        SELECT OPERATOR
          INTO v_doctorid
          FROM medicalrecord
         WHERE noofinpat = v_noofinpat
           AND ROWNUM < 2;
      
        INSERT INTO qcproblemdescription
          (ID,
           noofinpat,
           CATEGORY,
           status,
           typecode,
           description,
           problemdate,
           registerdate,
           registeruser,
           doctor_id)
        VALUES
          (seq_qcproblemdescription_id.NEXTVAL,
           v_noofinpat,
           v_category,
           v_status,
           v_typecode,
           v_description,
           v_problemdate,
           v_registerdate,
           v_registeruser,
           v_doctorid);
      END;
    ELSIF v_operstatus = 1 THEN
      -- ���
      BEGIN
        UPDATE qcproblemdescription
           SET confirmcontent = v_confirmcontent,
               confirmdate    = v_confirmdate,
               confirmuser    = v_confirmuser,
               CATEGORY       = v_category
         WHERE ID = v_id;
      END;
    ELSIF v_operstatus = 2 THEN
      --����
      BEGIN
        UPDATE qcproblemdescription
           SET confirmcontent = v_confirmcontent,
               deldate        = v_deldate,
               deluser        = v_deluser,
               CATEGORY       = v_category
         WHERE ID = v_id;
      END;
    END IF;
  END;

  /***************************************************************************************/
  /*��ȡ��������ͳ������*/
  PROCEDURE usp_qcitemscore(v_itemname           VARCHAR,
                            v_iteminstruction    VARCHAR,
                            v_itemdefaultscore   INT,
                            v_itemstandardscore  INT,
                            v_itemcategory       INT,
                            v_itemdefaulttarget  INT,
                            v_itemtargetstandard INT,
                            v_itemscorestandard  INT,
                            v_itemorder          INT,
                            v_typecode           VARCHAR,
                            v_itemmemo           VARCHAR,
                            v_typestatus         INT,
                            v_itemcode           VARCHAR) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��������ͳ������
     ����˵��
     �������
      v_ItemName varchar(40)
     ,v_ItemInstruction varchar(60)
     ,v_ItemDefaultScore int
     ,v_ItemStandardScore int
     ,v_ItemCategory int
     ,v_ItemDefaultTarget int
     ,v_ItemTargetStandard int
     ,v_ItemScoreStandard int
     ,v_ItemOrder int
     ,v_TypeCode varchar(4)
     ,v_ItemMemo varchar(60)
    ,v_TypeStatus int
     ,v_ItemCode varchar(5)
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QCItemScore  'test', 'test',3,4,5,6,7,8,9,'E001','test',0
     �޸ļ�¼
    **********/
    v_itemcodetemp VARCHAR(5);
  BEGIN
    IF v_typestatus = 0 THEN
      BEGIN
        SELECT 'I' || lpad((nvl(max(substr(itemcode, 2)), 0) + 1), 4, '0')
          INTO v_itemcodetemp
          FROM qcscoreitem;
      
        INSERT INTO qcscoreitem
          (itemcode,
           itemname,
           iteminstruction,
           itemdefaultscore,
           itemstandardscore,
           itemcategory,
           itemdefaulttarget,
           itemtargetstandard,
           itemscorestandard,
           itemorder,
           typecode,
           itemmemo)
        VALUES
          (v_itemcodetemp,
           v_itemname,
           v_iteminstruction,
           v_itemdefaultscore,
           v_itemstandardscore,
           v_itemcategory,
           v_itemdefaulttarget,
           v_itemtargetstandard,
           v_itemscorestandard,
           v_itemorder,
           v_typecode,
           v_itemmemo);
      END;
    ELSIF v_typestatus = 1 THEN
      --�޸�
      BEGIN
        UPDATE qcscoreitem
           SET itemname           = v_itemname,
               iteminstruction    = v_iteminstruction,
               itemdefaultscore   = v_itemdefaultscore,
               itemstandardscore  = v_itemstandardscore,
               itemcategory       = v_itemcategory,
               itemdefaulttarget  = v_itemdefaulttarget,
               itemtargetstandard = v_itemtargetstandard,
               itemscorestandard  = v_itemscorestandard,
               itemorder          = v_itemorder,
               typecode           = v_typecode,
               itemmemo           = v_itemmemo
         WHERE itemcode = v_itemcode;
      END;
    ELSIF v_typestatus = 2 THEN
      --ɾ��
      BEGIN
        UPDATE qcscoreitem SET valide = 0 WHERE itemcode = v_itemcode;
      END;
    END IF;
  END;

  /***************************************************************************************/
  /*��ȡ����ģ��ѡ����������*/
  PROCEDURE usp_msquerytemplate(v_id         VARCHAR,
                                v_user       VARCHAR,
                                v_type       INT,
                                v_department VARCHAR DEFAULT '',
                                o_result     OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ����ģ��ѡ����������
     ����˵��
     �������
      v_InitStauts int     --��ʼ��ID
      ,v_ID VARCHAR(12) --ModelDirectory.ID
      ,v_SortID VARCHAR(12) --Model_Docment.SortID
      ,v_Department varchar(12)   --����ID
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_MSQueryTemplate  0,'RM0',''
     �޸ļ�¼
    **********/
    v_sql VARCHAR(400);
  BEGIN
    IF v_type = 0 THEN
      BEGIN
        v_sql := 'SELECT ID AS TemplateID ,Name, null SortID from ModelDirectory
WHERE Valid = 1 AND ID IN (' || v_id || ')';
      
        OPEN o_result FOR v_sql;
      END;
    ELSIF v_type = 1 THEN
      BEGIN
        v_sql := 'SELECT a.ID AS TemplateID,a.Name, a.SortID, b.Memo from Model_Docment a,Template_Department b
WHERE a.ID=b.TemplateID and a.Valid = 1 AND a.SortID like ''' || v_id ||
                 '%'' and b.Department =''' || v_department ||
                 '''  order by a.SortID ';
      
        OPEN o_result FOR v_sql;
      END;
    ELSIF v_type = 2 THEN
      OPEN o_result FOR
        SELECT ID AS templateid, NAME, NULL sortid
          FROM modeldirectory
         WHERE valid = 1
           AND ID = v_id;
    ELSIF v_type = 3 THEN
      OPEN o_result FOR
        SELECT ID AS templateid, NAME, NULL sortid
          FROM templatesort_person
         WHERE mark = '0'
           AND valid = '1'
           AND userid = v_user;
    ELSIF v_type = 4 THEN
      OPEN o_result FOR
        SELECT templateid, NAME, sortid
          FROM template_person
         WHERE sortid = v_id
           AND valid = '1'
           AND userid = v_user;
    END IF;
  END;

  /***************************************************************************************/
  /*
  * �����û�����
  *
  */
  PROCEDURE usp_updateuserpassword(v_id      IN users.ID%TYPE,
                                   v_passwd  IN users.passwd%TYPE,
                                   v_regdate IN users.regdate%TYPE) IS
  BEGIN
    UPDATE users
       SET passwd = v_passwd, regdate = v_regdate
     WHERE ID = v_id;
  END;

  /***************************************************************************************/
  /*
  * �õ��û��ʻ���Ϣ
  */
  PROCEDURE usp_getuseraccount(v_id     IN users.ID%TYPE,
                               o_result OUT empcurtyp) IS
  BEGIN
    OPEN o_result FOR
      SELECT d.ID        userid,
             d.NAME      username,
             d.passwd,
             d.deptid,
             d.wardid,
             b.NAME      deptname,
             c.NAME      wardname,
             d.regdate,
             d.ID,
             d.jobid,
             d.valid,
             tu.masterid,
             tu.memo,
             d.status
        FROM users d
        LEFT JOIN department b
          ON d.deptid = b.ID
        LEFT JOIN ward c
          ON d.wardid = c.ID
        LEFT JOIN tempusers tu
          ON tu.userid = d.ID
         AND TO_DATE(tu.startdate || ' 00:00:00', 'yyyy-mm-dd HH24:mi:ss') <=
             SYSDATE
         AND TO_DATE(tu.enddate || ' 23:59:59', 'yyyy-mm-dd HH24:mi:ss') >=
             SYSDATE
       WHERE d.ID = v_id;
  END;

  /***************************************************************************************/
  /*
  * ȡְ�����Ҷ�Ӧ����, ��δָ����������ͨ��ָ���Ŀ��ҹ��������еĲ���
  */
  PROCEDURE usp_getuserdeptandward(v_userid IN users.ID%TYPE,
                                   o_result OUT empcurtyp) IS
  BEGIN
    OPEN o_result FOR
      SELECT a.deptid, b.NAME deptname, a.wardid, c.NAME wardname
        FROM user2dept a
        LEFT JOIN department b
          ON a.deptid = b.ID
        LEFT JOIN ward c
          ON a.wardid = c.ID
       WHERE userid = v_userid
      --AND deptid <> ''
      UNION
      SELECT DISTINCT a.deptid,
                      b.NAME       deptname,
                      c.outhosward wardid,
                      d.NAME       wardname
        FROM user2dept a, department b, inpatient c, ward d
       WHERE a.userid = v_userid
         AND a.deptid = b.ID
         AND (a.wardid IS NULL OR a.wardid = '')
         AND c.outhosdept = a.deptid
         AND c.status NOT IN (1500, 1503, 1508, 1509)
         AND c.outhosward = d.ID
       ORDER BY deptname, wardname;
  END;

  /***************************************************************************************/
  /*
  * ȡ��Ժ���˵����п��ҡ�������Ӧ��ϵ
  */
  PROCEDURE usp_getuseroutdeptandward(o_result OUT empcurtyp) IS
  BEGIN
    OPEN o_result FOR
    /*SELECT DISTINCT a.outhosdept deptid,
                                          b.NAME       deptname,
                                          a.outhosward wardid,
                                          c.NAME       wardname
                            FROM inpatient a, department b, ward c
                           WHERE a.status NOT IN ('1500', '1503', '1508', '1509')
                             AND a.outhosdept = b.ID
                             AND a.outhosward = c.ID
                           ORDER BY deptid, wardid;*/
      SELECT distinct department.id   deptid,
                      department.name deptname,
                      ward.id         wardid,
                      ward.name       wardname
        from department, ward, dept2ward
       where department.id = dept2ward.deptid
         and dept2ward.wardid = ward.id
         and ward.valid = '1'
         and department.valid = '1';
  END;

  /******************************************************************************************************************
  ****************************************************************************************************************/
  /**ʹ�õ���ʱ��Ĵ洢����**/

  /*
  *��ȡְ�������Ϣ
  */
  PROCEDURE usp_getuserinfo(v_userid  VARCHAR,
                            o_result  OUT empcurtyp,
                            o_result1 OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��  2011.5.10
     ����
     ��Ȩ
     ����  ��ȡְ�������Ϣ
     ����˵��
     �������
      v_userid ְ������
     �������
     �����������
     ���õ�sp
        usp_GetUserInfo '00'
     ����ʵ��
    
    **********/
    v_status   INT;
    v_masterid VARCHAR(16);
    v_flag     INT DEFAULT - 1;
    v_sql      VARCHAR(4000);
  BEGIN
    OPEN o_result FOR
      SELECT '' FROM DUAL;
  
    OPEN o_result1 FOR
      SELECT '' FROM DUAL;
  
    /*create table tmp_getuserinfo(
    userid    varchar(12) null,
    masterid  varchar(12) null,
    status    int      null
    );*/
    v_sql := 'truncate table tmp_getuserinfo ';
  
    EXECUTE IMMEDIATE v_sql;
  
    INSERT INTO tmp_getuserinfo
      SELECT a.ID, b.masterid, a.status
        FROM users a
        LEFT JOIN tempusers b
          ON a.ID = b.userid
       WHERE a.ID = v_userid;
  
    SELECT status INTO v_status FROM tmp_getuserinfo;
  
    SELECT masterid INTO v_masterid FROM tmp_getuserinfo;
  
    IF (v_status = 0) THEN
      --���˺�Ϊ��ʱ�˺�
      BEGIN
        SELECT 1
          INTO v_flag
          FROM tempusers
         WHERE userid = v_userid
           AND startdate < SYSDATE
           AND enddate > SYSDATE;
      
        IF v_flag > 0 THEN
          BEGIN
            OPEN o_result FOR
              SELECT '���˺���δ����' FROM DUAL;
          
            RETURN;
          END;
        END IF;
      END;
    END IF;
  
    --��ȡ�û�������Ϣ
    OPEN o_result FOR
      SELECT d.status,
             d.ID       userid,
             a.masterid masterid,
             d.NAME     username,
             d.passwd,
             d.deptid,
             d.wardid,
             b.NAME     deptname,
             c.NAME     wardname,
             d.regdate,
             d.ID,
             d.jobid,
             d.valid
        FROM tmp_getuserinfo a, users d
        LEFT JOIN department b
          ON d.deptid = b.ID
        LEFT JOIN ward c
          ON d.wardid = c.ID
       WHERE a.userid = d.ID;
  
    --��ȡ�û�������Ϣ
    IF (v_userid = '00') THEN
      BEGIN
        OPEN o_result FOR
          SELECT DISTINCT a.outhosdept deptid,
                          b.NAME       deptname,
                          a.outhosward wardid,
                          c.NAME       wardname
            FROM inpatient a, department b, ward c
           WHERE a.status NOT IN (1500, 1503, 1508, 1509)
             AND a.outhosdept = b.ID
             AND a.outhosward = c.ID
           ORDER BY deptid, wardid;
      END;
    ELSE
      BEGIN
        --�������ʱ�û�����ȡ�丽����ʦ���˺���Ϣ
        IF (v_status = 1) THEN
          v_masterid := v_userid;
        
          OPEN o_result FOR
            SELECT a.deptid, b.NAME deptname, a.wardid, c.NAME wardname
              FROM user2dept a
              LEFT JOIN department b
                ON a.deptid = b.ID
              LEFT JOIN ward c
                ON a.wardid = b.ID
             WHERE userid = v_masterid
               AND deptid <> ''
            UNION
            SELECT DISTINCT a.deptid,
                            b.NAME       deptname,
                            c.outhosward wardid,
                            d.NAME       wardname
              FROM user2dept a, department b, inpatient c, ward d
             WHERE a.userid = v_masterid
               AND a.deptid = b.ID
               AND (a.wardid IS NULL OR a.wardid = '')
               AND c.outhosdept = a.deptid
               AND c.status NOT IN (1500, 1503, 1508, 1509)
               AND c.outhosward = d.ID
             ORDER BY deptname, wardname;
        END IF;
      END;
    END IF;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_nursrecordoperate(v_id        NUMERIC DEFAULT 0, --��¼ID
                                  v_noofinpat NUMERIC DEFAULT 0, --��ҳ���(סԺ��ˮ��)
                                  v_sortid    NUMERIC DEFAULT 0, --ģ��������
                                  v_type      INT, --��������
                                  o_result    OUT empcurtyp) AS
    /**********
    [�汾��] 1.0.0.0.0
    [����ʱ��]2011-06-10
    [����]hjh
    [��Ȩ]
    [����]����
    [����˵��]
    [�������]
    [�������]
    [�����������]
    
    
    [���õ�sp]
    [����ʵ��]
    
    [�޸ļ�¼]
    **********/
  BEGIN
    OPEN o_result FOR
      SELECT '' FROM DUAL;
  
    IF v_type = 1 THEN
      --��ģ��
      OPEN o_result FOR
        SELECT content
          FROM template_table
         WHERE valid = 1
           AND ID = v_id;
    ELSIF v_type = 2 THEN
      --�򿪲��˻���
      OPEN o_result FOR
        SELECT *
          FROM nursrecordtable
         WHERE valid = 1
           AND noofinpat = v_noofinpat
           AND ID = v_id;
    ELSIF v_type = 3 THEN
      --�����ݿ��ж�ȡģ����Ϣ�б�
      OPEN o_result FOR
        SELECT tts.NAME AS sortname, tt.*
          FROM template_table tt
          LEFT JOIN templatetablesort tts
            ON tts.ID = tt.sortid
         WHERE tt.valid = 1
           AND (v_sortid = 0 OR tt.sortid = v_sortid)
         ORDER BY tt.sortid;
    ELSIF v_type = 4 THEN
      --��ȡ���˻����
      OPEN o_result FOR
        SELECT *
          FROM nursrecordtable
         WHERE valid = 1
           AND noofinpat = v_noofinpat
         ORDER BY NAME;
    ELSIF v_type = 5 THEN
      --ɾ��ģ��
      UPDATE template_table SET valid = 0 WHERE ID = v_id;
    ELSIF v_type = 6 THEN
      --ɾ�����������
      UPDATE nursrecordtable
         SET valid = 0
       WHERE ID = v_id
         AND noofinpat = v_noofinpat;
    ELSIF v_type = 7 THEN
      --��ȡ�����ĵ�ģ�����
      OPEN o_result FOR
        SELECT * FROM templatetablesort WHERE valid = 1 ORDER BY ID;
    END IF;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_medqcanalysis(v_datetimebegin VARCHAR, --��ѯ��ʼ����
                              v_datetimeend   VARCHAR, --��ѯ��������
                              o_result        OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��   2011-03-03
     ����     yxy
     ��Ȩ     YidanSoft
     ����  ҽ������ͳ�Ʒ���
     ����˵��
     �������
     �������
     �����������
    ҽ������ͳ�Ʒ���
    
     ���õ�sp
     ����ʵ��
    usp_MedQCAnalysis   '2009-11-25','2010-11-25'
     �޸ļ�¼
        Medical quality statistic analysis
    
    
    **********/
    v_sql VARCHAR2(4000);
  BEGIN
    --DeptCode DeptName InHos NewInHos NewOutHos BedCnt  EmptyBedCnt  AddBedCnt DieCnt SurgeryCnt GraveCnt OutHosFail
    --����ҽ��ͳ�Ʒ�����ʱ��
    /*  create table #temptable(DeptCode varchar(10) COLLATE Chinese_PRC_BIN
    NOT NULL, --���Ҵ���
    DeptName varchar(100) NOT NULL, --��������
    InHos int null, --��Ժ����
    NewInHos int null, --����Ժ����
    NewOutHos int null, --�³�Ժ����
    BedCnt int null, --��λ��
    EmptyBedCnt int null, --�մ�λ��
    AddBedCnt int null, --�Ӵ���
    DieCnt int null, --��������
    SurgeryCnt int null, --��������
    GraveCnt int null, --Σ����
    OutHosFail int null --��Ժδ�ύ��
    );*/
    v_sql := 'truncate table tmp_MedQCAnalysis ';
  
    EXECUTE IMMEDIATE v_sql;
  
    --������в����Ŀ���
    INSERT INTO tmp_medqcanalysis
      (deptcode, deptname)
      SELECT dept.ID, dept.NAME
        FROM department dept
       WHERE EXISTS
       (SELECT 1 FROM dept2ward ward WHERE dept.ID = ward.deptid);
  
    --������Ժ����
    UPDATE tmp_medqcanalysis
       SET inhos = NVL((SELECT COUNT(1)
                         FROM inpatient inp
                        WHERE inp.status IN (1500, 1501, 1505, 1506, 1507) --, 1504
                             --                        AND CONVERT(varchar(10),inp.AdmitDate,102) >= v_DateTimeBegin
                             --                        AND CONVERT(varchar(10),inp.AdmitDate,102) <= v_DateTimeEnd
                          AND inp.outhosdept = deptcode),
                       0);
  
    --����Ժ����
    UPDATE tmp_medqcanalysis
       SET newinhos = NVL((SELECT COUNT(1)
                            FROM inpatient inp
                           WHERE inp.status IN (1500, 1501) --,1504, 1505, 1506, 1507
                             AND trunc(TO_DATE(inp.admitdate,
                                               'yyyy-MM-dd hh24:mi:ss')) >=
                                 TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
                             AND trunc(TO_DATE(inp.admitdate,
                                               'yyyy-MM-dd hh24:mi:ss')) <=
                                 TO_DATE(v_datetimeend, 'yyyy-MM-dd')
                             AND inp.outhosdept = deptcode),
                          0);
  
    --�³�Ժ����
    UPDATE tmp_medqcanalysis
       SET newouthos = NVL((SELECT COUNT(1)
                             FROM inpatient inp
                            WHERE inp.status IN (1502, 1503)
                              AND trunc(TO_DATE(inp.admitdate,
                                                'yyyy-MM-dd hh24:mi:ss')) >=
                                  TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
                              AND trunc(TO_DATE(inp.admitdate,
                                                'yyyy-MM-dd hh24:mi:ss')) <=
                                  TO_DATE(v_datetimeend, 'yyyy-MM-dd')
                              AND inp.outhosdept = deptcode),
                           0);
  
    --��λ��
    UPDATE tmp_medqcanalysis
       SET bedcnt = NVL((SELECT COUNT(1)
                          FROM bed bed
                         WHERE bed.valid = 1
                           AND bed.deptid = deptcode),
                        0);
  
    --�մ�λ��
    UPDATE tmp_medqcanalysis
       SET emptybedcnt = NVL((SELECT COUNT(1)
                               FROM bed bed
                              WHERE bed.valid = 1
                                AND bed.inbed = 1300
                                AND bed.deptid = deptcode),
                             0);
  
    --�Ӵ���
    UPDATE tmp_medqcanalysis
       SET addbedcnt = NVL((SELECT COUNT(1)
                             FROM bed bed
                            WHERE bed.valid = 1
                              AND bed.style = 1202
                              AND bed.deptid = deptcode),
                           0);
  
    --��������  ����������
    UPDATE tmp_medqcanalysis
       SET diecnt = NVL((SELECT COUNT(1)
                          FROM inpatient inp
                          left join iem_mainpage_basicinfo_2012 imb
                            on imb.noofinpat = inp.noofinpat
                         WHERE inp.status IN /*��ΰ�� �޸�Ӧ������Ժ��*/
                               (1500,
                                1501,
                                1502,
                                1503,
                                1504,
                                1505,
                                1506,
                                1507)
                           AND trunc(TO_DATE(inp.admitdate,
                                             'yyyy-MM-dd hh24:mi:ss')) >=
                               TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
                           AND trunc(TO_DATE(inp.admitdate,
                                             'yyyy-MM-dd hh24:mi:ss')) <=
                               TO_DATE(v_datetimeend, 'yyyy-MM-dd')
                           and imb.outhostype = '5'
                              /*AND SYSDATE -
                              to_date(inp.admitdate,
                                      'yyyy-MM-dd hh24:mi:ss') > 30*/
                           AND inp.outhosdept = deptcode),
                        0);
  
    --��������
    UPDATE tmp_medqcanalysis
    /*SET surgerycnt = NVL((SELECT COUNT(1)
      FROM inpatient inp
     WHERE inp.status IN
           (1500, 1501, 1504, 1505, 1506, 1507)
       AND trunc(TO_DATE(inp.admitdate,
                         'yyyy-MM-dd hh24:mi:ss')) >=
           TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
       AND trunc(TO_DATE(inp.admitdate,
                         'yyyy-MM-dd hh24:mi:ss')) <=
           TO_DATE(v_datetimeend, 'yyyy-MM-dd')
       AND SYSDATE -
           to_date(inp.admitdate,
                   'yyyy-MM-dd hh24:mi:ss') > 30
       AND inp.outhosdept = deptcode),
    0);*/
    
    ---edit by ywk 2012��10��12�� 10:21:47
    
    /*select count (*) from(select inp.noofinpat,inp.outhosdept
     FROM inpatient inp
     left join iem_mainpage_basicinfo_2012 bas on inp.noofinpat=bas.noofinpat
     left join iem_mainpage_operation_2012 ope on bas.iem_mainpage_no=bas.iem_mainpage_no
    WHERE inp.status IN
          (1500, 1501, 1504, 1505, 1506, 1507) and ope.operation_code is not null and inp.outhosdept='2401'
          group by inp.noofinpat,inp.outhosdept)*/
    
    ---edit by ���� 2012��10��26��
    /*  SET surgerycnt=nvl((SELECT COUNT(1) from  ( select * from (select inp.noofinpat  noo,inp.outhosdept
       FROM inpatient inp
       left join iem_mainpage_basicinfo_2012 bas on inp.noofinpat=bas.noofinpat
       left join iem_mainpage_operation_2012 ope on ope.iem_mainpage_no=bas.iem_mainpage_no
      WHERE inp.outhosdept = deptcode and  inp.status IN (1500, 1501, 1504, 1505, 1506, 1507)
      and ope.operation_code is not null
         AND trunc(TO_DATE(inp.admitdate,
                      'yyyy-MM-dd hh24:mi:ss')) >=
        TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
    AND trunc(TO_DATE(inp.admitdate,
                      'yyyy-MM-dd hh24:mi:ss')) <=
        TO_DATE(v_datetimeend, 'yyyy-MM-dd')  group by inp.noofinpat,inp.outhosdept)A  where A.outhosdept=deptcode )),0);*/
    /*(select count(*)
    from (select inp.noofinpat noo, inp.outhosdept
            FROM inpatient inp
            left join iem_mainpage_basicinfo_2012 bas
              on inp.noofinpat = bas.noofinpat
            left join iem_mainpage_operation_2012 ope
              on ope.iem_mainpage_no =
                 bas.iem_mainpage_no
           WHERE inp.outhosdept = deptcode
             and inp.status IN (1500,
                                1501,
                                1504,
                                1505,
                                1506,
                                1507)
             and ope.operation_code is not null
             AND trunc(TO_DATE(inp.admitdate,
                               'yyyy-MM-dd hh24:mi:ss')) >=
                 TO_DATE(v_datetimebegin,
                         'yyyy-MM-dd')
             AND trunc(TO_DATE(inp.admitdate,
                               'yyyy-MM-dd hh24:mi:ss')) <=
                 TO_DATE(v_datetimeend,
                         'yyyy-MM-dd')
           group by inp.noofinpat, inp.outhosdept))*/
    
       SET surgerycnt = nvl((select count(distinct(inp.noofinpat))
                              FROM inpatient inp
                              left join iem_mainpage_basicinfo_2012 bas
                                on inp.noofinpat = bas.noofinpat
                              left join iem_mainpage_operation_2012 ope
                                on ope.iem_mainpage_no = bas.iem_mainpage_no
                             WHERE inp.outhosdept = deptcode
                               and inp.status IN (1500,
                                                  1501,
                                                  1502,
                                                  1503,
                                                  1504,
                                                  1505,
                                                  1506,
                                                  1507)
                               and ope.operation_code is not null
                               AND trunc(TO_DATE(inp.admitdate,
                                                 'yyyy-MM-dd hh24:mi:ss')) >=
                                   TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
                               AND trunc(TO_DATE(inp.admitdate,
                                                 'yyyy-MM-dd hh24:mi:ss')) <=
                                   TO_DATE(v_datetimeend, 'yyyy-MM-dd')),
                            0);
  
    --Σ�ز���
    UPDATE tmp_medqcanalysis
       SET gravecnt = NVL((SELECT COUNT(1)
                            FROM inpatient inp
                           WHERE inp.criticallevel = 1
                             AND inp.status IN
                                 (1500, 1501, 1504, 1505, 1506, 1507)
                             AND trunc(TO_DATE(inp.admitdate,
                                               'yyyy-MM-dd hh24:mi:ss')) >=
                                 TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
                             AND trunc(TO_DATE(inp.admitdate,
                                               'yyyy-MM-dd hh24:mi:ss')) <=
                                 TO_DATE(v_datetimeend, 'yyyy-MM-dd')
                             AND inp.outhosdept = deptcode),
                          0);
  
    --��Ժδ�ύ
    UPDATE tmp_medqcanalysis
    /*   SET outhosfail = NVL((SELECT COUNT(1)
      FROM inpatient inp
     WHERE inp.status IN (1502, 1503)
       AND trunc(TO_DATE(inp.admitdate,
                         'yyyy-MM-dd hh24:mi:ss')) >=
           TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
       AND trunc(TO_DATE(inp.admitdate,
                         'yyyy-MM-dd hh24:mi:ss')) <=
           TO_DATE(v_datetimeend, 'yyyy-MM-dd')
       AND inp.outhosdept = deptcode),
    0);*/
    
    -----edit by ywk 2012��10��12�� 09:59:53
    
    -----edit by wj 2012��10��26��  ע��
    /* select decode(count(b.id), 0 ,'δд','��д' ) RECORDSTATE,
          '' QC ,
          a.NoOfInpat AS NOOFINPAT,
          a.PatID as PATID,
          a.Name PATNAME,
          decode(a.sexid,1,'��',2,'Ů','δ֪') PATSEX,
          1 AS INHOSPITAL,
          dept.name DEPTNAME,
          diag.name PATDIAGNAME,
          a.admitdate INHOSPITALTIME,
          a.outhosdate OUTHOSPITALTIME,
          to_char(to_date(a.admitdate,'yyyy-mm-dd HH24:mi:ss') + 1, 'yyyy-mm-dd') SUREDIAGTIME,
          users.name DOCNAME,
          a.AgeStr PATAGE,
          datediff('dd',a.admitdate, NVL(trim(a.outwarddate), TO_CHAR(SYSDATE, 'yyyy-mm-dd'))) INCOUNT
     from INPATIENT    a,
          RecordDetail b,
          diagnosis    diag,
          department   dept,
          users        users
    where a.NOOFINPAT = b.NoOfInpat(+)
      and a.AdmitDiagnosis = diag.markid(+)
      and a.outhosdept = dept.id(+)
      and a.Resident = users.id(+)
      and a.status in (1502, 1503)
      and b.hassubmit = 4600
      AND trunc(TO_DATE(a.admitdate,'yyyy-MM-dd hh24:mi:ss')) >= TO_DATE('2001-01-01', 'yyyy-MM-dd')
      AND trunc(TO_DATE(a.admitdate,'yyyy-MM-dd hh24:mi:ss')) <= TO_DATE('2012-11-11', 'yyyy-MM-dd')
      AND a.outhosdept =2401
    group by a.NoOfInpat,
             a.PatID,
             a.NAME,
             a.SEXID,
             A.AGESTR,
             a.OUTBED,
             a.AdmitDiagnosis,
             a.admitdate,
             a.outwarddate,
             diag.name,
             dept.name,
             a.outhosdate,
             users.name*/
    /* SET outhosfail = NVL((select count(*)
      from (select (case
                     when count(b.ID) > 0 then
                      '��д'
                     else
                      'δд'
                   end) RECORDSTATE,
                   '' QC,
                   a.NoOfInpat AS NOOFINPAT,
                   a.PatID as PATID,
                   a.Name PATNAME,
                   (case
                     when a.SexID = 1 then
                      '��'
                     when a.SexID = 2 then
                      'Ů'
                     else
                      'δ֪'
                   end) as PATSEX,
                   1 AS INHOSPITAL,
                   dept.name DEPTNAME,
                   diag.name PATDIAGNAME,
                   a.admitdate INHOSPITALTIME,
                   a.outhosdate OUTHOSPITALTIME,
                   to_char(to_date(a.admitdate,
                                   'yyyy-mm-dd HH24:mi:ss') + 1,
                           'yyyy-mm-dd') SUREDIAGTIME,
                   users.name DOCNAME,
                   a.AgeStr PATAGE,
                   datediff('dd',
                            a.admitdate,
                            NVL(trim(a.outwarddate),
                                TO_CHAR(SYSDATE,
                                        'yyyy-mm-dd'))) INCOUNT
              from INPATIENT    a,
                   RecordDetail b,
                   diagnosis    diag,
                   department   dept,
                   users        users
             where a.NOOFINPAT = b.NoOfInpat(+)
               and a.AdmitDiagnosis = diag.markid(+)
               and a.outhosdept = dept.id(+)
               and a.Resident = users.id(+)
               and a.status in (1502, 1503)
               and b.hassubmit = 4600
               AND trunc(TO_DATE(a.admitdate,
                                 'yyyy-MM-dd hh24:mi:ss')) >=
                   TO_DATE(v_datetimebegin,
                           'yyyy-MM-dd')
               AND trunc(TO_DATE(a.admitdate,
                                 'yyyy-MM-dd hh24:mi:ss')) <=
                   TO_DATE(v_datetimeend,
                           'yyyy-MM-dd')
               AND a.outhosdept = deptcode
             group by a.NoOfInpat,
                      a.PatID,
                      a.NAME,
                      a.SEXID,
                      A.AGESTR,
                      a.OUTBED,
                      a.AdmitDiagnosis,
                      a.admitdate,
                      a.outwarddate,
                      diag.name,
                      dept.name,
                      a.outhosdate,
                      users.name)),
    0);*/
       SET outhosfail = NVL((select count(distinct(a.NoOfInpat))
                              from INPATIENT    a,
                                   RecordDetail b,
                                   diagnosis    diag,
                                   department   dept,
                                   users        users
                             where a.NOOFINPAT = b.NoOfInpat(+)
                               and a.AdmitDiagnosis = diag.markid(+)
                               and a.outhosdept = dept.id(+)
                               and a.Resident = users.id(+)
                               and a.status in (1502, 1503)
                               and b.hassubmit = 4600
                               AND trunc(TO_DATE(a.admitdate,
                                                 'yyyy-MM-dd hh24:mi:ss')) >=
                                   TO_DATE(v_datetimebegin, 'yyyy-MM-dd')
                               AND trunc(TO_DATE(a.admitdate,
                                                 'yyyy-MM-dd hh24:mi:ss')) <=
                                   TO_DATE(v_datetimeend, 'yyyy-MM-dd')
                               AND a.outhosdept = deptcode),
                            0);
  
    --����Ҫ����ʾ������---test by ywk  2012��10��8�� 17:28:55
    /* update tmp_medqcanalysis
    set inhos       = trunc(dbms_random.value(0, 100), 0),
        newinhos    = trunc(dbms_random.value(0, 30), 0),
        newouthos   = trunc(dbms_random.value(0, 20), 0),
        bedcnt      = trunc(dbms_random.value(100, 200), 0),
        emptybedcnt = trunc(dbms_random.value(0, 100), 0),
        addbedcnt   = trunc(dbms_random.value(0, 100), 0),
        diecnt      = trunc(dbms_random.value(0, 20), 0),
        surgerycnt  = trunc(dbms_random.value(0, 100), 0),
        gravecnt    = trunc(dbms_random.value(0, 20), 0),
        outhosfail  = trunc(dbms_random.value(20, 50), 0),
        kssr        = trunc(dbms_random.value(100000, 800000), 0),
        yzb         = trunc(dbms_random.value(10, 40), 0);*/
  
    OPEN o_result FOR
      SELECT '_' deptcode,
             '�ܼ�' deptname,
             SUM(inhos) inhos,
             SUM(newinhos) newinhos,
             SUM(newouthos) newouthos,
             SUM(bedcnt) bedcnt,
             SUM(emptybedcnt) emptybedcnt,
             SUM(addbedcnt) addbedcnt,
             SUM(diecnt) diecnt,
             SUM(surgerycnt) surgerycnt,
             SUM(gravecnt) gravecnt,
             SUM(outhosfail) outhosfail,
             SUM(kssr) kssr,
             ceil(SUM(yzb) / count(1)) yzb
        FROM tmp_medqcanalysis
      UNION ALL
      SELECT deptcode,
             deptname,
             inhos,
             newinhos,
             newouthos,
             bedcnt,
             emptybedcnt,
             addbedcnt,
             diecnt,
             surgerycnt,
             gravecnt,
             outhosfail,
             kssr,
             yzb
        FROM tmp_medqcanalysis;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_querybrowserinwardpatients(v_id        VARCHAR,
                                           v_querytype INT,
                                           v_wardid    VARCHAR,
                                           v_deptids   VARCHAR,
                                           v_zyhm      VARCHAR DEFAULT '',
                                           v_hzxm      VARCHAR DEFAULT '',
                                           v_cyzd      VARCHAR DEFAULT '',
                                           v_ryrqbegin VARCHAR DEFAULT '',
                                           v_ryrqend   VARCHAR DEFAULT '',
                                           v_cyrqbegin VARCHAR DEFAULT '',
                                           v_cyrqend   VARCHAR DEFAULT '',
                                           o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��Ӧ�����Ĵ�λ��Ϣ(����һ��ʱ���ã�
    -- ����˵��
     �������
      v_Wardid   varchar(6)   --��������
      v_Deptids varchar(255)  --���Ҵ��뼯��(����: '����1','����2')
      v_zyhm  varchar(30)=''  --סԺ��
      v_hzxm  varchar(30)=''  --����
      v_cyzd  varchar(30)=''  --��Ժ���
      v_cyrqbegin varchar(19)=''  --��Ժ������Сֵ
      v_cyrqend varchar(19)=''  --��Ժ�������ֵ
      v_ryrqbegin varchar(19)=''  --��Ժ������Сֵ
      v_ryrqend varchar(19)=''  --��Ժ�������ֵ
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryBrowserInwardPatients 0,0,'2911', '3202'
     �޸ļ�¼
    �����������
    **********/
    v_existdata INTEGER;
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_wardid_in VARCHAR(40);
    v_sql       VARCHAR(4000);
  BEGIN
    v_sql := 'truncate table tmp_QueryBrowserinwardPat ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryBrowserInward_extraop ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryBrowserinwardPatExist ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    SELECT COUNT(*)
      INTO v_existdata
      FROM doctor_assignpatient
     WHERE ID = v_id
       AND valid = 1;
  
    v_dqrq := TO_CHAR(SYSDATE, 'yyyy-mm-dd');
    v_ksrq := TO_CHAR(SYSDATE - 3, 'yyyy-mm-dd');
    v_jsrq := TO_CHAR(SYSDATE + 2, 'yyyy-mm-dd');
    v_now  := TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss');
  
    -- ���ҳ����˼�¼��Ȼ���ȡ���˵ĸ�����Ϣ����������Ժ��ת�Ƶȣ�
    INSERT INTO tmp_querybrowserinwardpat
      SELECT b.noofinpat  noofinpat, --��ҳ���
             b.patnoofhis patnoofhis, --HIS��ҳ���
             b.patid      patid,
             --סԺ��
             b.NAME   patname, --����
             b.sexid  sex, --�����Ա�
             b.sexid  sexname, --�����Ա�����
             b.agestr agestr,
             --����
             b.py py, --ƴ��
             b.wb wb, --���
             b.status brzt, --����״̬
             RTRIM(b.criticallevel) wzjb,
             --Σ�ؼ���
             i.NAME wzjbmc, --Σ�ؼ�������
             /*
             (CASE
               WHEN patid IS NULL THEN
                ''
               ELSE
                'һ������'
             END) hljb, --������
             */
             
             case b.attendlevel
               when '1' then
                'һ������'
               when '2' then
                '��������'
               when '3' then
                '��������'
               when '4' then
                '�ؼ�����'
               else
                'һ������'
             end hljb, --������
             
             b.isbaby yebz, --Ӥ����־
             a.wardid bqdm, --��������
             a.deptid ksdm,
             --���Ҵ���
             --a.ID           bedid, --��λ����
             b.outbed       bedid, --��λ����
             a.formerward   ybqdm, --ԭ��������
             a.formerdeptid yksdm,
             --ԭ���Ҵ���
             a.formerdeptid ycwdm, --ԭ��λ����
             a.inbed inbed, --ռ����־
             a.borrow jcbz, --�贲��־
             a.sexinfo cwlx, --��λ����
             SUBSTR(b.admitdate, 1, 16) admitdate,
             --��Ժ����
             f.NAME     ryzd, --��Ժ���
             f.NAME     zdmc, --�������
             b.resident zyysdm, --סԺҽ������
             c.NAME     zyys,
             --סԺҽ��
             c.NAME  cwys, --��λҽ��
             g.NAME  zzys, --����ҽʦ
             h.NAME  zrys, --����ҽʦ
             a.valid yxjl, --��Ч��¼
             --me.pzlx pzlx, --ƾ֤����
             dd1.NAME pzlx, --�������
             TO_CHAR(CASE
                       WHEN INSTR(v_deptids, a.deptid) = 0 AND (b.noofinpat IS NULL) THEN
                        '������������'
                       ELSE
                        ''
                     END) extra, --������Ϣ
             b.memo memo, --��ע
             (CASE b.cpstatus
               WHEN 0 THEN
                'δ����'
               WHEN 1 THEN
                'ִ����'
               WHEN 2 THEN
                '�˳�'
             END) cpstatus,
             (SELECT CASE
                       WHEN COUNT(qc.foulstate) > 0 THEN
                        '��'
                       ELSE
                        '��'
                     END
                FROM qcrecord qc
               WHERE qc.noofinpat = b.noofinpat
                 AND qc.valid = 1
                 AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
             100 ye --���
        FROM bed a
        LEFT JOIN inpatient b
          ON a.noofinpat = b.noofinpat
         AND a.patnoofhis = b.patnoofhis
         AND INSTR(v_deptids, RTRIM(b.outhosdept)) > 0
         AND a.inbed = 1301
      --LEFT JOIN medicareinfo me ON b.voucherscode = me.ID
      
        LEFT JOIN Dictionary_detail dd1
          ON dd1.categoryid = '1'
         AND b.payid = dd1.detailid
      --   left join YY_SFXXMK e  on b.AttendLevel = e.sfxmdm
        LEFT JOIN diagnosis f
          ON f.icd = b.admitdiagnosis
        LEFT JOIN users c
          ON c.ID = b.resident
        LEFT JOIN dictionary_detail i
          ON i.detailid = b.criticallevel
         AND i.categoryid = '53'
        LEFT JOIN users g
          ON g.ID = a.attend
        LEFT JOIN users h
          ON h.ID = a.chief
        LEFT JOIN dictionary_detail j
          ON j.detailid = b.sexid
         AND j.categoryid = '3'
       WHERE a.wardid = v_wardid_in
         AND a.valid = 1;
  
    -- ���������Ϣ:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢����ʱ���ڵ�ǰ����-3��+1֮��
    INSERT INTO tmp_querybrowserinward_extraop
      SELECT a.noofinpat,
             datediff('dd', v_now, TO_CHAR(a.entrust, 'yyyy-mm-dd')) diff
        FROM temp_order a, tmp_querybrowserinwardpat b
       WHERE a.noofinpat = b.noofinpat
         AND a.startdate BETWEEN v_ksrq AND v_jsrq
         AND a.ordertype = 3105
         AND a.orderstatus IN (3201, 3202);
  
    UPDATE tmp_querybrowserinwardpat a
       SET extra =
           (SELECT a.extra || (CASE b.diff
                     WHEN 1 THEN
                      '��������'
                     WHEN 0 THEN
                      '��������'
                     WHEN -1 THEN
                      '����1��'
                     WHEN -2 THEN
                      '����2��'
                     WHEN -3 THEN
                      '����3��'
                     ELSE
                      ''
                   END) || ' '
              FROM tmp_querybrowserinward_extraop b
             WHERE a.noofinpat = b.noofinpat
               AND ROWNUM <= 1);
  
    -- ����Ժҽ��:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢��Ժʱ���ڵ�ǰ����0ʱ֮��
    v_sql := 'truncate table tmp_QueryBrowserInward_extraop ';
  
    EXECUTE IMMEDIATE v_sql;
  
    INSERT INTO tmp_querybrowserinward_extraop
      SELECT a.noofinpat,
             datediff('dd', v_now, TO_CHAR(a.entrust, 'yyyy-mm-dd')) diff
        FROM temp_order a, tmp_querybrowserinwardpat b
       WHERE a.noofinpat = b.noofinpat
         AND a.startdate >= v_dqrq
         AND a.ordertype = 3113
         AND a.orderstatus IN (3201, 3202);
  
    UPDATE tmp_querybrowserinwardpat a
       SET extra =
           (SELECT a.extra || (CASE b.diff
                     WHEN 0 THEN
                      '���ճ�Ժ'
                     WHEN 1 THEN
                      '���ճ�Ժ'
                     ELSE
                      TO_CHAR(b.diff) || '����Ժ'
                   END) || ' '
              FROM tmp_querybrowserinward_extraop b
             WHERE a.noofinpat = b.noofinpat
               AND ROWNUM <= 1);
  
    -- ���ת��ҽ��:���ݲ���״̬
    UPDATE tmp_querybrowserinwardpat a
       SET extra =
           (SELECT a.extra || b.NAME || ' '
              FROM categorydetail b
             WHERE b.categoryid = a.brzt
               AND ROWNUM < 1)
     WHERE a.brzt IN (1505, 1506, 1507);
  
    -- ��鲡���Ƿ��в���
    UPDATE tmp_querybrowserinwardpat a
       SET extra = a.extra || '�޲���'
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS
     (SELECT 1 FROM recorddetail b WHERE a.noofinpat = b.noofinpat);
  
    IF v_querytype = 0 THEN
      --ȫ��
      BEGIN
        OPEN o_result FOR
          SELECT * FROM tmp_querybrowserinwardpat order by bedid;
        --ORDER BY TO_CHAR(bedid, '0000');
      END;
    ELSE
      --�ֹ�
      IF (v_existdata = 0) THEN
        BEGIN
          OPEN o_result FOR
            SELECT * FROM tmp_querybrowserinwardpat order by bedid;
          --ORDER BY TO_CHAR(bedid, '0000');
        END;
      ELSE
        BEGIN
          INSERT INTO tmp_querybrowserinwardpatexist
            SELECT b.noofinpat  noofinpat, --��ҳ���
                   b.patnoofhis patnoofhis,
                   --HIS��ҳ���
                   b.patid  patid, --סԺ��
                   b.NAME   patname, --����
                   b.sexid  sex, --�����Ա�
                   b.sexid  sexname, --�����Ա�����
                   b.agestr agestr, --����
                   b.py     py, --ƴ��
                   b.wb     wb,
                   --���
                   b.status brzt, --����״̬
                   RTRIM(b.criticallevel) wzjb,
                   --Σ�ؼ���
                   i.NAME wzjbmc, --Σ�ؼ�������
                   /*
                   (CASE
                     WHEN patid IS NULL THEN
                      ''
                     ELSE
                      'һ������'
                   END) hljb, --������
                   */
                   
                   case b.attendlevel
                     when '1' then
                      'һ������'
                     when '2' then
                      '��������'
                     when '3' then
                      '��������'
                     when '4' then
                      '�ؼ�����'
                     else
                      'һ������'
                   end hljb, --������
                   
                   b.isbaby yebz, --Ӥ����־
                   a.wardid bqdm, --��������
                   a.deptid ksdm, --���Ҵ���
                   --a.ID         bedid, --��λ����
                   b.outbed     bedid, --��λ����
                   a.formerward ybqdm,
                   --ԭ��������
                   a.formerdeptid yksdm, --ԭ���Ҵ���
                   a.formerdeptid ycwdm,
                   --ԭ��λ����
                   a.inbed   inbed, --ռ����־
                   a.borrow  jcbz, --�贲��־
                   a.sexinfo cwlx,
                   --��λ����
                   SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
                   f.NAME ryzd,
                   --��Ժ���
                   f.NAME     zdmc, --�������
                   b.resident zyysdm, --סԺҽ������
                   c.NAME     zyys,
                   --סԺҽ��
                   c.NAME  cwys, --��λҽ��
                   g.NAME  zzys, --����ҽʦ
                   h.NAME  zrys, --����ҽʦ
                   a.valid yxjl,
                   --��Ч��¼
                   --me.pzlx pzlx, --ƾ֤����
                   dd1.NAME pzlx, --�������
                   TO_CHAR(CASE
                             WHEN INSTR(v_deptids, a.deptid) = 0 AND
                                  (b.noofinpat IS NULL) THEN
                              '������������'
                             ELSE
                              ''
                           END) extra, --������Ϣ
                   b.memo memo, --��ע
                   100 ye --���
              FROM doctor_assignpatient da
              LEFT JOIN bed a
                ON da.noofinpat = a.noofinpat
               AND a.valid = 1
              LEFT JOIN inpatient b
                ON a.noofinpat = b.noofinpat
               AND a.patnoofhis = b.patnoofhis
               AND INSTR(v_deptids, RTRIM(b.outhosdept)) > 0
               AND a.inbed = 1301
            --LEFT JOIN medicareinfo me ON b.voucherscode = me.ID
              LEFT JOIN Dictionary_detail dd1
                ON dd1.categoryid = '1'
               AND b.payid = dd1.detailid
            --   left join YY_SFXXMK e  on b.AttendLevel = e.sfxmdm
              LEFT JOIN diagnosis f
                ON f.icd = b.admitdiagnosis
              LEFT JOIN users c
                ON c.ID = b.resident
              LEFT JOIN dictionary_detail i
                ON i.detailid = b.criticallevel
               AND i.categoryid = '53'
              LEFT JOIN users g
                ON g.ID = a.attend
              LEFT JOIN users h
                ON h.ID = a.chief
              LEFT JOIN dictionary_detail j
                ON j.detailid = b.sexid
               AND j.categoryid = '3'
             WHERE da.ID = v_id
               AND da.valid = 1;
        
          -- ���������Ϣ:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢����ʱ���ڵ�ǰ����-3��+1֮��
          v_sql := 'truncate table tmp_QueryBrowserInward_extraop ';
        
          EXECUTE IMMEDIATE v_sql;
        
          INSERT INTO tmp_querybrowserinward_extraop
            SELECT a.noofinpat,
                   datediff('dd', v_now, TO_CHAR(SUBSTR(a.entrust, 1, 19))) diff
              FROM temp_order a, tmp_querybrowserinwardpatexist b
             WHERE a.noofinpat = b.noofinpat
               AND a.startdate BETWEEN v_ksrq AND v_jsrq
               AND a.ordertype = 3105
               AND a.orderstatus IN (3201, 3202);
        
          UPDATE tmp_querybrowserinwardpatexist a
             SET extra =
                 (SELECT a.extra || (CASE b.diff
                           WHEN 1 THEN
                            '��������'
                           WHEN 0 THEN
                            '��������'
                           WHEN -1 THEN
                            '����1��'
                           WHEN -2 THEN
                            '����2��'
                           WHEN -3 THEN
                            '����3��'
                           ELSE
                            ''
                         END) || ' '
                    FROM tmp_querybrowserinward_extraop b
                   WHERE a.noofinpat = b.noofinpat
                     AND ROWNUM < 1);
        
          -- ����Ժҽ��:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢��Ժʱ���ڵ�ǰ����0ʱ֮��
          v_sql := 'truncate table tmp_QueryBrowserInward_extraop ';
        
          EXECUTE IMMEDIATE v_sql;
        
          INSERT INTO tmp_querybrowserinward_extraop
            SELECT a.noofinpat,
                   datediff('dd', v_now, TO_CHAR(a.entrust, 'yyyy-mm-dd')) diff
              FROM temp_order a, tmp_querybrowserinwardpatexist b
             WHERE a.noofinpat = b.noofinpat
               AND a.startdate >= v_dqrq
               AND a.ordertype = 3113
               AND a.orderstatus IN (3201, 3202);
        
          UPDATE tmp_querybrowserinwardpatexist a
             SET extra =
                 (SELECT a.extra || (CASE b.diff
                           WHEN 0 THEN
                            '���ճ�Ժ'
                           WHEN 1 THEN
                            '���ճ�Ժ'
                           ELSE
                            TO_CHAR(b.diff) || '����Ժ'
                         END) || ' '
                    FROM tmp_querybrowserinward_extraop b
                   WHERE a.noofinpat = b.noofinpat
                     AND ROWNUM < 1);
        
          -- ���ת��ҽ��:���ݲ���״̬
          UPDATE tmp_querybrowserinwardpatexist a
             SET extra =
                 (SELECT a.extra || b.NAME || ' '
                    FROM categorydetail b
                   WHERE b.categoryid = a.brzt
                     AND ROWNUM < 1)
           WHERE a.brzt IN (1505, 1506, 1507);
        
          -- ��鲡���Ƿ��в���
          UPDATE tmp_querybrowserinwardpatexist a
             SET extra = a.extra || '�޲���'
           WHERE noofinpat IS NOT NULL
             AND NOT EXISTS (SELECT 1
                    FROM recorddetail b
                   WHERE a.noofinpat = b.noofinpat);
        
          OPEN o_result FOR
            SELECT * FROM tmp_querybrowserinwardpatexist order by bedid;
          --ORDER BY TO_CHAR(bedid, '0000');
        END;
      END IF;
    END IF;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_queryhistorypatients(v_wardid          VARCHAR,
                                     v_deptids         VARCHAR,
                                     v_patid           VARCHAR DEFAULT '',
                                     v_patname         VARCHAR DEFAULT '',
                                     v_outdiagnosis    VARCHAR DEFAULT '',
                                     v_admitdatebegin  VARCHAR DEFAULT '',
                                     v_admitdatend     VARCHAR DEFAULT '',
                                     v_outhosdatebegin VARCHAR DEFAULT '',
                                     v_outhosdatend    VARCHAR DEFAULT '',
                                     o_result          OUT empcurtyp,
                                     o_result1         OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��Ӧ�����Ĵ�λ��Ϣ
     ����˵��
     �������
      v_wardid   varchar(6)   --��������
      v_deptids varchar(255)  --���Ҵ��뼯��(����: '����1','����2')
      v_PatID  varchar(30)=''  --סԺ��
      v_PatName  varchar(30)=''  --����
      v_OutDiagnosis  varchar(30)=''  --��Ժ���
      v_OutHosDatebegin varchar(19)=''  --��Ժ������Сֵ
      v_OutHosDatend varchar(19)=''  --��Ժ�������ֵ
      v_AdmitDatebegin varchar(19)=''  --��Ժ������Сֵ
      v_AdmitDatend varchar(19)=''  --��Ժ�������ֵ
     �������
     �����������
    �ڱ�����ס���Ĳ��˵Ĳ�����Ϣ
    ���˵�����סԺ��Ϣ
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryInwardPatients   '2922','3225'
     �޸ļ�¼
    
    **********/
    v_wardid_in        VARCHAR2(6);
    v_npatid           VARCHAR(30);
    v_npatname         VARCHAR(30);
    v_noutdiagnosis    VARCHAR(19);
    v_nouthosdatebegin VARCHAR(19);
    v_nouthosdatend    VARCHAR(19);
    v_nadmitdatebegin  VARCHAR(19);
    v_nadmitdatend     VARCHAR(19);
    v_sql              VARCHAR(400);
  BEGIN
    OPEN o_result FOR
      SELECT '' FROM DUAL;
  
    OPEN o_result1 FOR
      SELECT '' FROM DUAL;
  
    v_sql := 'truncate table tmp_QueryHistory_pats ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryHistory_result ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    --��ѯ��ʷ����
    v_npatid           := RTRIM(v_patid);
    v_npatname         := RTRIM(v_patname);
    v_noutdiagnosis    := RTRIM(v_outdiagnosis);
    v_nouthosdatebegin := RTRIM(v_outhosdatebegin);
    v_nouthosdatend    := RTRIM(v_outhosdatend);
    v_nadmitdatebegin  := RTRIM(v_admitdatebegin);
    v_nadmitdatend     := RTRIM(v_admitdatend);
  
    -- �ҳ���Ժ���Ժ���ڱ����ҵĲ��ˣ�������Ժ�ͳ�Ժ��
    INSERT INTO tmp_queryhistory_pats
      SELECT DISTINCT innerpix
        FROM inpatient
       WHERE (outhosdept = v_deptids OR admitdept = v_deptids)
         AND (v_npatid = '' OR v_npatid IS NULL OR
             patid LIKE v_npatid || '%')
         AND (v_npatname = '' OR v_npatname IS NULL OR
             NAME LIKE v_npatname || '%')
         AND (v_noutdiagnosis = '' OR v_noutdiagnosis IS NULL OR
             outdiagnosis LIKE v_noutdiagnosis)
         AND (v_nouthosdatebegin = '' OR v_nouthosdatebegin IS NULL OR
             (outhosdate >= v_nouthosdatebegin AND
             outhosdate <= v_nouthosdatend))
         AND (v_nadmitdatebegin = '' OR v_nadmitdatebegin IS NULL OR
             (admitdate >= v_nadmitdatebegin AND
             admitdate <= v_nadmitdatend));
  
    -- �ҳ����˵�����סԺ��Ϣ
    INSERT INTO tmp_queryhistory_result
      SELECT b.noofinpat, --��ҳ���
             b.patnoofhis, --HIS��ҳ���
             b.patid, --סԺ��
             b.NAME, --����
             b.sexid, --�����Ա�
             b.nativeaddress, --���ڵ�ַ
             SUBSTR(b.admitdate, 1, 16) admitdate,
             --��Ժ����
             c.NAME admitdept, --��Ժ����
             d.NAME admitward, --��Ժ����
             e.NAME admitdiagnosis,
             --��Ժ���
             SUBSTR(b.outhosdate, 1, 16) outhosdate, --��Ժ����
             f.NAME outhosdept,
             --��Ժ����
             g.NAME outhosward, --��Ժ����
             h.NAME outdiagnosis --��Ժ���
        FROM tmp_queryhistory_pats a
        JOIN inpatient b
          ON a.innerpix = b.innerpix
         AND b.status IN (1502, 1503)
        LEFT JOIN department c
          ON b.admitdept = c.ID
        LEFT JOIN ward d
          ON b.outhosward = d.ID
        LEFT JOIN diagnosis e
          ON b.admitdiagnosis = e.icd
        LEFT JOIN department f
          ON b.outhosdept = f.ID
        LEFT JOIN ward g
          ON b.outhosward = g.ID
        LEFT JOIN diagnosis h
          ON b.outdiagnosis = h.icd;
  
    -- ���ȷ��ػ�����Ϣ��¼
    OPEN o_result FOR
      SELECT patid, --סԺ��
             NAME, --����
             sexid, --�����Ա�
             nativeaddress --���ڵ�ַ
        FROM tmp_queryhistory_result
       WHERE noofinpat IN (SELECT MAX(noofinpat)
                             FROM tmp_queryhistory_result
                            GROUP BY patid)
       ORDER BY patid;
  
    -- �ٷ�������סԺ��Ϣ
    OPEN o_result1 FOR
      SELECT * FROM tmp_queryhistory_result ORDER BY patid, admitdate;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_queryinwardpatients_old(v_wardid    VARCHAR,
                                        v_deptids   VARCHAR,
                                        v_zyhm      VARCHAR DEFAULT '',
                                        v_hzxm      VARCHAR DEFAULT '',
                                        v_cyzd      VARCHAR DEFAULT '',
                                        v_ryrqbegin VARCHAR DEFAULT '',
                                        v_ryrqend   VARCHAR DEFAULT '',
                                        v_cyrqbegin VARCHAR DEFAULT '',
                                        v_cyrqend   VARCHAR DEFAULT '',
                                        --add by xjt
                                        v_id        VARCHAR DEFAULT '',
                                        v_querytype INT DEFAULT 0,
                                        v_querynur  VARCHAR DEFAULT '',
                                        v_querybed  VARCHAR DEFAULT 'Y',
                                        o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��Ӧ�����Ĵ�λ��Ϣ
     ����˵��
     �������
      v_Wardid   varchar(6)   --��������
      v_Deptids varchar(255)  --���Ҵ��뼯��(����: '����1','����2')
      v_zyhm  varchar(30)=''  --סԺ��
      v_hzxm  varchar(30)=''  --����
      v_cyzd  varchar(30)=''  --��Ժ���
      v_cyrqbegin varchar(19)=''  --��Ժ������Сֵ
      v_cyrqend varchar(19)=''  --��Ժ�������ֵ
      v_ryrqbegin varchar(19)=''  --��Ժ������Сֵ
      v_ryrqend varchar(19)=''  --��Ժ�������ֵ
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryInwardPatients  '2911', '3202','','','','','','','','',3,'Y'
     �޸ļ�¼
    �����������
    **********/
    /*v_wardid_in VARCHAR(6);
        v_dqrq      VARCHAR(10);
        v_ksrq      VARCHAR(10);
        v_jsrq      VARCHAR(10);
        v_now       VARCHAR(24);
        v_sql       VARCHAR(4000);
      BEGIN
       \* v_sql := 'truncate table tmp_QueryInwardPatients ';
    
        EXECUTE IMMEDIATE v_sql;
    *\
        v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
    
        EXECUTE IMMEDIATE v_sql;
    
        IF v_wardid IS NULL THEN
          v_wardid_in := '';
        END IF;
    
        \*-- ���ҳ����˼�¼��Ȼ���ȡ���˵ĸ�����Ϣ����������Ժ��ת�Ƶȣ�
        INSERT INTO tmp_queryinwardpatients
          SELECT distinct b.noofinpat noofinpat, --��ҳ���
                 b.patnoofhis patnoofhis, --HIS��ҳ���
                 b.patid patid, --סԺ��
                 b.name patname, --����
                 b.sexid sex, --�����Ա�
                 j.name sexname, --�����Ա�����
                 b.agestr agestr, --����
                 b.py py, --ƴ��
                 b.wb wb, --���
                 b.status brzt, --����״̬
                 e.name brztname, --����״̬����
                 RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
                 i.name wzjbmc, --Σ�ؼ�������
                 case b.attendlevel
                   when '1' then
                    'һ������'
                   when '2' then
                    '��������'
                   when '3' then
                    '��������'
                   when '4' then
                    '�ؼ�����'
                   else
                    'һ������'
                 end hljb, --������
    
                 case b.attendlevel
                   when '1' then
                    '6101'
                   when '2' then
                    '6102'
                   when '3' then
                    '6103'
                   when '4' then
                    '6104'
                   else
                    '6101'
                 end attendlevel, --������
                 b.isbaby yebz, --Ӥ����־
                 CASE
                   WHEN b.isbaby = '0' THEN
                    '��'
                   WHEN b.isbaby IS NULL THEN
                    ''
                   ELSE
                    '��'
                 END yebzname,
                 b.outhosward bqdm, --��������
                 b.outhosdept ksdm, --���Ҵ���
                 case when length(b.outbed) = 1 then '0' || b.outbed else b.outbed end bedid, --��λ����
                 dg.NAME ksmc, --��������
                 wh.NAME bqmc, --��������
                 a.formerward ybqdm, --ԭ��������
                 a.formerdeptid yksdm, --ԭ���Ҵ���
                 a.formerdeptid ycwdm, --ԭ��λ����
                 --a.inbed inbed, --ռ����־
                 '' inbed,--ռ����־ add by ywk  2012��11��5��15:02:39
                 a.borrow jcbz, --�贲��־
                 a.sexinfo cwlx, --��λ����
                 SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
                 f.NAME ryzd, --��Ժ���
                 f.NAME zdmc, --�������
                 b.resident zyysdm, --סԺҽ������
                 c.NAME zyys, --סԺҽ��
                 c.NAME cwys, --��λҽ��
                 g.NAME zzys, --����ҽʦ
                 h.NAME zrys, --����ҽʦ
                 a.valid yxjl, --��Ч��¼
                 --me.pzlx pzlx, --ƾ֤����
                 dd1.NAME pzlx, --�������
                 CASE
                   WHEN b.outhosdept = v_deptids AND b.outhosward = v_wardid THEN
                    '������'
                   ELSE
                    '������������'
                 END extra, --������Ϣ
                 b.memo memo, --��ע
                 CASE b.cpstatus
                   WHEN 0 THEN
                    'δ����'
                   WHEN 1 THEN
                    'ִ����'
                   WHEN 2 THEN
                    '�˳�'
                 END cpstatus,
                 CASE
                   WHEN b.noofinpat IS NULL THEN
                    ''
                   ELSE
                    '����д'
                 END recordinfo,
                 100 ye, --���
                 (SELECT CASE
                           WHEN COUNT(qc.foulstate) > 0 THEN
                            '��'
                           ELSE
                            '��'
                         END
                    FROM qcrecord qc
                   WHERE qc.noofinpat = b.noofinpat
                     AND qc.valid = 1
                     AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
                 b.incount as rycs --��Ժ����
            FROM inpatient b
           LEFT JOIN bed a ON a.id = b.outbed
           AND a.deptid = v_deptids AND a.wardid = v_wardid
           AND a.noofinpat = b.noofinpat AND a.valid = '1'
            LEFT JOIN Dictionary_detail dd1 ON dd1.categoryid = '1'
                                           AND b.payid = dd1.detailid
            LEFT JOIN categorydetail e ON b.status = e.ID
                                      AND e.categoryid = '15'
            LEFT JOIN department dg ON b.outhosdept = dg.id
            LEFT JOIN ward wh ON b.outhosward = wh.id
            LEFT JOIN diagnosis f ON f.icd = b.admitdiagnosis
            LEFT JOIN users c ON c.ID = b.resident
            LEFT JOIN dictionary_detail i ON i.detailid = b.criticallevel
                                         AND i.categoryid = '53'
            LEFT JOIN users g ON b.attend = g.id
            LEFT JOIN users h ON b.chief = h.id
            LEFT JOIN dictionary_detail j ON j.detailid = b.sexid
                                         AND j.categoryid = '3'
            LEFT JOIN categorydetail cd ON b.attendlevel = cd.ID
                                       AND cd.categoryid = '63'
           WHERE
             b.outhosdept = v_deptids AND b.outhosward = v_wardid
             AND b.status in ('1500','1501') AND b.isbaby != '1'
           ORDER BY bedid;
        \*
          v_dqrq := TO_CHAR(SYSDATE, 'yyyy-mm-dd');
          v_ksrq := TO_CHAR(SYSDATE - 3, 'yyyy-mm-dd');
          v_jsrq := TO_CHAR(SYSDATE + 2, 'yyyy-mm-dd');
          v_now  := TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss');
    
          -- ���������Ϣ:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢����ʱ���ڵ�ǰ����-3��+1֮��
          INSERT INTO tmp_queryinwardpats_extraop
            SELECT a.noofinpat,
                   datediff('dd', v_now, TO_CHAR(SUBSTR(a.entrust, 1, 19))) diff
              FROM temp_order a, tmp_queryinwardpatients b
             WHERE a.noofinpat = b.noofinpat
               AND a.startdate BETWEEN v_ksrq AND v_jsrq
               AND a.ordertype = 3105
               AND a.orderstatus IN (3201, 3202);
    
          UPDATE tmp_queryinwardpatients a
             SET extra = (SELECT a.extra || (CASE b.diff
                                   WHEN 1 THEN
                                    '��������'
                                   WHEN 0 THEN
                                    '��������'
                                   WHEN -1 THEN
                                    '����1��'
                                   WHEN -2 THEN
                                    '����2��'
                                   WHEN -3 THEN
                                    '����3��'
                                   ELSE
                                    ''
                                 END) || ' '
                            FROM tmp_queryinwardpats_extraop b
                           WHERE a.noofinpat = b.noofinpat
                             AND ROWNUM < 1);
    
          -- ����Ժҽ��:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢��Ժʱ���ڵ�ǰ����0ʱ֮��
          v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
    
          EXECUTE IMMEDIATE v_sql;
    
          INSERT INTO tmp_queryinwardpats_extraop
            SELECT a.noofinpat,
                   datediff('dd', v_now, TO_CHAR(a.entrust, 'yyyy-mm-dd')) diff
              FROM temp_order a, tmp_queryinwardpatients b
             WHERE a.noofinpat = b.noofinpat
               AND a.startdate >= v_dqrq
               AND a.ordertype = 3113
               AND a.orderstatus IN (3201, 3202);
    
          UPDATE tmp_queryinwardpatients a
             SET extra = (SELECT a.extra || (CASE b.diff
                                   WHEN 0 THEN
                                    '���ճ�Ժ'
                                   WHEN 1 THEN
                                    '���ճ�Ժ'
                                   ELSE
                                    TO_CHAR(b.diff) || '����Ժ'
                                 END) || ' '
                            FROM tmp_queryinwardpats_extraop b
                           WHERE a.noofinpat = b.noofinpat
                             AND ROWNUM < 1);
    
          -- ���ת��ҽ��:���ݲ���״̬
          UPDATE tmp_queryinwardpatients a
             SET extra = (SELECT a.extra || b.NAME || ' '
                            FROM categorydetail b
                           WHERE b.categoryid = a.brzt
                             AND ROWNUM < 1)
           WHERE a.brzt IN (1505, 1506, 1507);
        *\
        -- ��鲡���Ƿ��в���
        UPDATE tmp_queryinwardpatients a
           SET extra = extra || '�޲���', recordinfo = '�޲���'
         WHERE noofinpat IS NOT NULL
           AND NOT EXISTS (SELECT 1
                  FROM recorddetail b
                 WHERE a.noofinpat = b.noofinpat
                   and b.valid = '1');
    *\
        IF v_querytype = 0 THEN
          --ȫ��
          BEGIN
            OPEN o_result FOR
              \*SELECT rownum ID, b.*
              FROM
              (
                  SELECT a.* FROM tmp_queryinwardpatients a
                   WHERE a.noofinpat IS NOT NULL
                   ORDER BY a.bedid
              ) b;*\
              SELECT rownum ID, c.*
              FROM
              (
                  SELECT a.*,
                    b.formerward ybqdm, --ԭ��������
                    b.formerdeptid yksdm, --ԭ���Ҵ���
                    b.formerdeptid ycwdm, --ԭ��λ����
    
                    b.deptid beddeptid,
                    b.wardid bedwardid,
    
                    b.borrow jcbz, --�贲��־
                    b.sexinfo cwlx, --��λ����
                    b.valid yxjl --��Ч��¼
                   FROM view_tmp_queryinwardpatients_2 a
                 LEFT JOIN bed b ON b.id = a.outbed
           AND b.deptid = v_deptids AND b.wardid = v_wardid
           AND b.noofinpat = a.noofinpat AND b.valid = '1'
                   WHERE a.noofinpat IS NOT NULL
                    and b.deptid=v_deptids and b.wardid=v_wardid
                  and a.outhosdept=v_deptids and a.outhosward=v_wardid
                   ORDER BY a.bedid
              ) c;
          END;
        ELSIF v_querytype = 1 THEN
          --�ֹ�
          BEGIN
            OPEN o_result FOR
              SELECT rownum ID, d.* FROM
              (
                SELECT c.* FROM
                (
                    \*SELECT a.* FROM tmp_queryinwardpatients a
                      JOIN doctor_assignpatient da ON a.noofinpat = da.noofinpat
                                            AND da.ID = v_id
                                            AND da.valid = 1
    
                    UNION
                    --Modified By wwj 2011-09-02
                    SELECT b.*
                      FROM tmp_queryinwardpatients b
                     where zyysdm = v_id
                       and noofinpat not in (select noofinpat
                                         from Doctor_AssignPatient
                                        where valid = '1')*\
                    SELECT a.*,
    
                      b.formerward ybqdm, --ԭ��������
                    b.formerdeptid yksdm, --ԭ���Ҵ���
                    b.formerdeptid ycwdm, --ԭ��λ����
    
                    b.deptid beddeptid,
                    b.wardid bedwardid,
    
                    b.borrow jcbz, --�贲��־
                    b.sexinfo cwlx, --��λ����
                    b.valid yxjl --��Ч��¼
                     FROM view_tmp_queryinwardpatients_2 a
                     LEFT JOIN bed b ON b.id = a.outbed
           AND b.deptid = v_deptids AND b.wardid = v_wardid
           AND b.noofinpat = a.noofinpat AND b.valid = '1'
    
                      JOIN doctor_assignpatient da ON a.noofinpat = da.noofinpat
                                            AND da.ID = v_id
                                            AND da.valid = 1
                                            and b.deptid=v_deptids and b.wardid=v_wardid
                                            and a.outhosdept=v_deptids and a.outhosward=v_wardid
    
                    UNION
                    --Modified By wwj 2011-09-02
                    SELECT a.*,
                      b.formerward ybqdm, --ԭ��������
                    b.formerdeptid yksdm, --ԭ���Ҵ���
                    b.formerdeptid ycwdm, --ԭ��λ����
    
                    b.deptid beddeptid,
                    b.wardid bedwardid,
    
                    b.borrow jcbz, --�贲��־
                    b.sexinfo cwlx, --��λ����
                    b.valid yxjl --��Ч��¼
    
                      FROM view_tmp_queryinwardpatients_2 a
                      LEFT JOIN bed b ON b.id = a.outbed
           AND b.deptid = v_deptids AND b.wardid = v_wardid
           AND b.noofinpat = a.noofinpat AND b.valid = '1'
                     where zyysdm = v_id
                       and a.noofinpat not in (select noofinpat
                                         from Doctor_AssignPatient
                                        where valid = '1')
                                         and b.deptid=v_deptids and b.wardid=v_wardid
                                            and a.outhosdept=v_deptids and a.outhosward=v_wardid
                 ) c
                 ORDER BY c.bedid --���ҵĻ��ߡ����մ�λ������
              ) d;
          END;
        ELSIF v_querytype = 2 THEN
          --δ����ҽ���Ļ���
          BEGIN
            OPEN o_result FOR
              \*SELECT *
                FROM tmp_queryinwardpatients a
               WHERE a.noofinpat IS NOT NULL
                 AND NOT EXISTS (SELECT 1
                        FROM doctor_assignpatient d
                       WHERE d.valid = 1
                         AND d.noofinpat = a.noofinpat)
               ORDER BY a.bedid;*\
               SELECT a.*,
                 b.formerward ybqdm, --ԭ��������
                    b.formerdeptid yksdm, --ԭ���Ҵ���
                    b.formerdeptid ycwdm, --ԭ��λ����
    
                    b.deptid beddeptid,
                    b.wardid bedwardid,
    
                    b.borrow jcbz, --�贲��־
                    b.sexinfo cwlx, --��λ����
                    b.valid yxjl --��Ч��¼
                FROM view_tmp_queryinwardpatients_2 a
                LEFT JOIN bed b ON b.id = a.outbed
           AND b.deptid = v_deptids AND b.wardid = v_wardid
           AND b.noofinpat = a.noofinpat AND b.valid = '1'
               WHERE a.noofinpat IS NOT NULL
                 AND NOT EXISTS (SELECT 1
                        FROM doctor_assignpatient d
                       WHERE d.valid = 1
                         AND d.noofinpat = a.noofinpat)
                         and b.deptid=v_deptids and b.wardid=v_wardid
                                            and a.outhosdept=v_deptids and a.outhosward=v_wardid
               ORDER BY a.bedid;
          END;
        ELSIF v_querytype = 3 THEN
          --����
          BEGIN
            IF v_querybed = 'N' THEN
              --�������մ�
              BEGIN
                OPEN o_result FOR
                 \* SELECT *
                    FROM tmp_QueryInwardPatients a
                   WHERE a.NoOfInpat IS NOT NULL
                   ORDER BY a.bedid;*\
                   SELECT a.*,
                     b.formerward ybqdm, --ԭ��������
                    b.formerdeptid yksdm, --ԭ���Ҵ���
                    b.formerdeptid ycwdm, --ԭ��λ����
    
                    b.deptid beddeptid,
                    b.wardid bedwardid,
    
                    b.borrow jcbz, --�贲��־
                    b.sexinfo cwlx, --��λ����
                    b.valid yxjl --��Ч��¼
                    FROM view_tmp_QueryInwardPatients_2 a
                    LEFT JOIN bed b ON b.id = a.outbed
           AND b.deptid = v_deptids AND b.wardid = v_wardid
           AND b.noofinpat = a.noofinpat AND b.valid = '1'
                   WHERE a.NoOfInpat IS NOT NULL
                    --and a.beddeptid=v_deptids and a.bedwardid=v_wardid
                    and b.deptid=v_deptids and b.wardid=v_wardid
                                          and a.outhosdept=v_deptids and a.outhosward=v_wardid
                   ORDER BY a.bedid;
              END;
            END IF;
    
            IF v_querybed = 'Y' THEN
              --�����մ�
              BEGIN
                OPEN o_result FOR
                  \*SELECT * FROM tmp_QueryInwardPatients a ORDER BY a.bedid;*\
                  SELECT a.*,
                    b.formerward ybqdm, --ԭ��������
                    b.formerdeptid yksdm, --ԭ���Ҵ���
                    b.formerdeptid ycwdm, --ԭ��λ����
    
                    b.deptid beddeptid,
                    b.wardid bedwardid,
    
                    b.borrow jcbz, --�贲��־
                    b.sexinfo cwlx, --��λ����
                    b.valid yxjl --��Ч��¼
                   FROM view_tmp_QueryInwardPatients_2 a
                   LEFT JOIN bed b ON b.id = a.outbed
           AND b.deptid = v_deptids AND b.wardid = v_wardid
           AND b.noofinpat =a.noofinpat AND b.valid = '1'
    
                    ORDER BY a.bedid;
              END;
            END IF;
          END;
        END IF;
      END;*/
    -----------------------======
    v_wardid_in VARCHAR(6);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(4000);
  BEGIN
    v_sql := 'truncate table tmp_QueryInwardPatients ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    -- ���ҳ����˼�¼��Ȼ���ȡ���˵ĸ�����Ϣ����������Ժ��ת�Ƶȣ�
    INSERT INTO tmp_queryinwardpatients
      SELECT distinct b.noofinpat noofinpat, --��ҳ���
                      b.patnoofhis patnoofhis, --HIS��ҳ���
                      b.patid patid, --סԺ��
                      b.name patname, --����
                      b.sexid sex, --�����Ա�
                      j.name sexname, --�����Ա�����
                      b.agestr agestr, --����
                      b.py py, --ƴ��
                      b.wb wb, --���
                      b.status brzt, --����״̬
                      e.name brztname, --����״̬����
                      RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
                      i.name wzjbmc, --Σ�ؼ�������
                      --cd.name hljb,
                      
                      --CASE WHEN b.attendlevel IS NULL THEN 6105 ELSE to_number(b.attendlevel)
                      --END attendlevel, --������
                      
                      case b.attendlevel
                        when '1' then
                         'һ������'
                        when '2' then
                         '��������'
                        when '3' then
                         '��������'
                        when '4' then
                         '�ؼ�����'
                        else
                         'һ������'
                      end hljb, --������
                      
                      case b.attendlevel
                        when '1' then
                         '6101'
                        when '2' then
                         '6102'
                        when '3' then
                         '6103'
                        when '4' then
                         '6104'
                        else
                         '6101'
                      end attendlevel, --������
                      b.isbaby yebz, --Ӥ����־
                      CASE
                        WHEN b.isbaby = '0' THEN
                         '��'
                        WHEN b.isbaby IS NULL THEN
                         ''
                        ELSE
                         '��'
                      END yebzname,
                      --a.wardid bqdm, --��������
                      --a.deptid ksdm, --���Ҵ���
                      
                      b.outhosward bqdm, --��������
                      b.outhosdept ksdm, --���Ҵ���
                      
                      --a.ID bedid, --��λ����
                      case
                        when length(b.outbed) = 1 then
                         '0' || b.outbed
                        else
                         b.outbed
                      end bedid, --��λ����
                      dg.NAME ksmc, --��������
                      wh.NAME bqmc, --��������
                      a.formerward ybqdm, --ԭ��������
                      a.formerdeptid yksdm, --ԭ���Ҵ���
                      a.formerdeptid ycwdm, --ԭ��λ����
                      a.inbed inbed, --ռ����־
                      a.borrow jcbz, --�贲��־
                      a.sexinfo cwlx, --��λ����
                      SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
                      f.NAME ryzd, --��Ժ���
                      f.NAME zdmc, --�������
                      b.resident zyysdm, --סԺҽ������
                      c.NAME zyys, --סԺҽ��
                      c.NAME cwys, --��λҽ��
                      g.NAME zzys, --����ҽʦ
                      h.NAME zrys, --����ҽʦ
                      a.valid yxjl, --��Ч��¼
                      --me.pzlx pzlx, --ƾ֤����
                      dd1.NAME pzlx, --�������
                      /*
                      TO_CHAR((CASE
                                WHEN INSTR(v_deptids, a.deptid) = 0 AND (b.noofinpat IS NULL) THEN
                                 '������������'
                                ELSE
                                 ''
                              END)) extra, --������Ϣ
                      */
                      CASE
                        WHEN b.outhosdept = v_deptids AND
                             b.outhosward = v_wardid THEN
                         '������'
                        ELSE
                         '������������'
                      END extra, --������Ϣ
                      b.memo memo, --��ע
                      CASE b.cpstatus
                        WHEN 0 THEN
                         'δ����'
                        WHEN 1 THEN
                         'ִ����'
                        WHEN 2 THEN
                         '�˳�'
                      END cpstatus,
                      CASE
                        WHEN b.noofinpat IS NULL THEN
                         ''
                        ELSE
                         '����д'
                      END recordinfo,
                      100 ye, --���
                      (SELECT CASE
                                WHEN COUNT(qc.foulstate) > 0 THEN
                                 '��'
                                ELSE
                                 '��'
                              END
                         FROM qcrecord qc
                        WHERE qc.noofinpat = b.noofinpat
                          AND qc.valid = 1
                          AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
                      b.incount as rycs --��Ժ����
        FROM inpatient b
        LEFT JOIN bed a
          ON a.id = b.outbed
         AND a.deptid = v_deptids
         AND a.wardid = v_wardid
         AND a.noofinpat = b.noofinpat
         AND a.valid = '1'
      /*LEFT JOIN inpatient b ON a.noofinpat = b.noofinpat
                             AND a.patnoofhis = b.patnoofhis
                                --AND INSTR(v_deptids, RTRIM(b.outhosdept)) > 0
                                --AND b.status IN (1501, 1504, 1505, 1506, 1507) --��Ժ
                             AND a.inbed = 1301 --ռ��
      -- 1501 �����ִ�,1504 ȡ������,1505 ����ICU,1506 �������,1507 ת��״̬
        */
        LEFT JOIN Dictionary_detail dd1
          ON dd1.categoryid = '1'
         AND b.payid = dd1.detailid
        LEFT JOIN categorydetail e
          ON b.status = e.ID
         AND e.categoryid = '15'
        LEFT JOIN department dg
          ON b.outhosdept = dg.id
        LEFT JOIN ward wh
          ON b.outhosward = wh.id
        LEFT JOIN diagnosis f
          ON f.icd = b.admitdiagnosis
        LEFT JOIN users c
          ON c.ID = b.resident
        LEFT JOIN dictionary_detail i
          ON i.detailid = b.criticallevel
         AND i.categoryid = '53'
        LEFT JOIN users g
          ON b.attend = g.id
        LEFT JOIN users h
          ON b.chief = h.id
        LEFT JOIN dictionary_detail j
          ON j.detailid = b.sexid
         AND j.categoryid = '3'
        LEFT JOIN categorydetail cd
          ON b.attendlevel = cd.ID
         AND cd.categoryid = '63'
       WHERE /*((b.outhosdept = v_deptids AND b.outhosward = v_wardid) OR
                                           (v_deptids IN
                                           (SELECT bedinfo.newdept
                                                FROM bedinfo
                                               WHERE bedinfo.noofinpat = b.noofinpat) AND
                                           v_wardid IN
                                           (SELECT bedinfo.newward
                                                FROM bedinfo
                                               WHERE bedinfo.noofinpat = b.noofinpat)))
                                       AND a.valid = 1;*/
       b.outhosdept = v_deptids
       AND b.outhosward = v_wardid
       AND b.status in ('1500', '1501')
       AND b.isbaby != '1'
       ORDER BY bedid;
    /*
      v_dqrq := TO_CHAR(SYSDATE, 'yyyy-mm-dd');
      v_ksrq := TO_CHAR(SYSDATE - 3, 'yyyy-mm-dd');
      v_jsrq := TO_CHAR(SYSDATE + 2, 'yyyy-mm-dd');
      v_now  := TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss');
    
      -- ���������Ϣ:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢����ʱ���ڵ�ǰ����-3��+1֮��
      INSERT INTO tmp_queryinwardpats_extraop
        SELECT a.noofinpat,
               datediff('dd', v_now, TO_CHAR(SUBSTR(a.entrust, 1, 19))) diff
          FROM temp_order a, tmp_queryinwardpatients b
         WHERE a.noofinpat = b.noofinpat
           AND a.startdate BETWEEN v_ksrq AND v_jsrq
           AND a.ordertype = 3105
           AND a.orderstatus IN (3201, 3202);
    
      UPDATE tmp_queryinwardpatients a
         SET extra = (SELECT a.extra || (CASE b.diff
                               WHEN 1 THEN
                                '��������'
                               WHEN 0 THEN
                                '��������'
                               WHEN -1 THEN
                                '����1��'
                               WHEN -2 THEN
                                '����2��'
                               WHEN -3 THEN
                                '����3��'
                               ELSE
                                ''
                             END) || ' '
                        FROM tmp_queryinwardpats_extraop b
                       WHERE a.noofinpat = b.noofinpat
                         AND ROWNUM < 1);
    
      -- ����Ժҽ��:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢��Ժʱ���ڵ�ǰ����0ʱ֮��
      v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
    
      EXECUTE IMMEDIATE v_sql;
    
      INSERT INTO tmp_queryinwardpats_extraop
        SELECT a.noofinpat,
               datediff('dd', v_now, TO_CHAR(a.entrust, 'yyyy-mm-dd')) diff
          FROM temp_order a, tmp_queryinwardpatients b
         WHERE a.noofinpat = b.noofinpat
           AND a.startdate >= v_dqrq
           AND a.ordertype = 3113
           AND a.orderstatus IN (3201, 3202);
    
      UPDATE tmp_queryinwardpatients a
         SET extra = (SELECT a.extra || (CASE b.diff
                               WHEN 0 THEN
                                '���ճ�Ժ'
                               WHEN 1 THEN
                                '���ճ�Ժ'
                               ELSE
                                TO_CHAR(b.diff) || '����Ժ'
                             END) || ' '
                        FROM tmp_queryinwardpats_extraop b
                       WHERE a.noofinpat = b.noofinpat
                         AND ROWNUM < 1);
    
      -- ���ת��ҽ��:���ݲ���״̬
      UPDATE tmp_queryinwardpatients a
         SET extra = (SELECT a.extra || b.NAME || ' '
                        FROM categorydetail b
                       WHERE b.categoryid = a.brzt
                         AND ROWNUM < 1)
       WHERE a.brzt IN (1505, 1506, 1507);
    */
    -- ��鲡���Ƿ��в���
    UPDATE tmp_queryinwardpatients a
       SET extra = extra || '�޲���', recordinfo = '�޲���'
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS (SELECT 1
              FROM recorddetail b
             WHERE a.noofinpat = b.noofinpat
               and b.valid = '1');
  
    IF v_querytype = 0 THEN
      --ȫ��
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, b.*
            FROM (SELECT a.*
                    FROM tmp_queryinwardpatients a
                   WHERE a.noofinpat IS NOT NULL
                   ORDER BY a.bedid) b;
      END;
    ELSIF v_querytype = 1 THEN
      --�ֹ�
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, d.*
            FROM (SELECT c.*
                    FROM (SELECT a.*
                            FROM tmp_queryinwardpatients a
                            JOIN doctor_assignpatient da
                              ON a.noofinpat = da.noofinpat
                             AND da.ID = v_id
                             AND da.valid = 1
                          
                          UNION
                          --Modified By wwj 2011-09-02
                          SELECT b.*
                            FROM tmp_queryinwardpatients b
                           where zyysdm = v_id
                             and noofinpat not in
                                 (select noofinpat
                                    from Doctor_AssignPatient
                                   where valid = '1')) c
                   ORDER BY c.bedid --���ҵĻ��ߡ����մ�λ������
                  ) d;
      END;
    ELSIF v_querytype = 2 THEN
      --δ����ҽ���Ļ���
      BEGIN
        OPEN o_result FOR
          SELECT *
            FROM tmp_queryinwardpatients a
           WHERE a.noofinpat IS NOT NULL
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient d
                   WHERE d.valid = 1
                     AND d.noofinpat = a.noofinpat)
           ORDER BY a.bedid;
      END;
    ELSIF v_querytype = 3 THEN
      --����
      BEGIN
        IF v_querybed = 'N' THEN
          --�������մ�
          BEGIN
            OPEN o_result FOR
              SELECT *
                FROM tmp_QueryInwardPatients a
               WHERE a.NoOfInpat IS NOT NULL
               ORDER BY a.bedid;
          END;
        END IF;
      
        IF v_querybed = 'Y' THEN
          --�����մ�
          BEGIN
            OPEN o_result FOR
              SELECT * FROM tmp_QueryInwardPatients a ORDER BY a.bedid;
          END;
        END IF;
      END;
    END IF;
  END;

  -------======================================

  /*
  bwj 20121108 ���ҽ������վ������ʱ�����Դ����������
  ͨ����ͼ
  */
  PROCEDURE usp_queryinwardpatients(v_wardid    VARCHAR,
                                    v_deptids   VARCHAR,
                                    v_zyhm      VARCHAR DEFAULT '',
                                    v_hzxm      VARCHAR DEFAULT '',
                                    v_cyzd      VARCHAR DEFAULT '',
                                    v_ryrqbegin VARCHAR DEFAULT '',
                                    v_ryrqend   VARCHAR DEFAULT '',
                                    v_cyrqbegin VARCHAR DEFAULT '',
                                    v_cyrqend   VARCHAR DEFAULT '',
                                    --add by xjt
                                    v_id        VARCHAR DEFAULT '',
                                    v_querytype INT DEFAULT 0,
                                    v_querynur  VARCHAR DEFAULT '',
                                    v_querybed  VARCHAR DEFAULT 'Y',
                                    o_result    OUT empcurtyp)
  
   AS
    v_table     varchar(500);
    v_wardid_in VARCHAR(6);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(8000);
    v_cfgValue  clob;
  BEGIN
    --ͨ��appcfg�ж��ǹ���������ɽ�Ĵ洢���� xuliangliang 2012-12-17
    select ap.VALUE
      into v_cfgValue
      from appcfg ap
     where ap.CONFIGKEY = 'ConfigQLSEMRPROC';
    if v_cfgValue = '1' then
      emrproc.usp_queryinwardpatientsQLS(v_wardid,
                                         v_deptids,
                                         v_zyhm,
                                         v_hzxm,
                                         v_cyzd,
                                         v_ryrqbegin,
                                         v_ryrqend,
                                         v_cyrqbegin,
                                         v_cyrqend,
                                         --add by xjt
                                         v_id,
                                         v_querytype,
                                         v_querynur,
                                         v_querybed,
                                         o_result);
    else
      begin
      
        v_table := 'tmp_' || to_char(sysdate, 'yyyyMMddHH24mimiss') ||
                   dbms_random.string('a', 3);
        execute immediate 'create table ' || v_table ||
                          ' as select t.*,''yyyy-MM-dd HH:mm:ss'' as inwarddate,''111111111111111111'' as idinfo from tmp_QueryInwardPatients t where 1<>1';
        --' as select * from tmp_QueryInwardPatients where 1<>1'; --edit by cyq 2013-03-13 ������������
        IF v_wardid IS NULL THEN
          v_wardid_in := '';
        END IF;
      
        v_sql := 'INSERT INTO ' || v_table ||
                 ' SELECT distinct b.noofinpat noofinpat,' --��ҳ���
                 || 'b.patnoofhis patnoofhis,' --HIS��ҳ���
                 || 'b.patid patid,' --סԺ��
                 || 'b.name patname,' --����
                 || 'b.sexid sex,' --�����Ա�
                 || 'j.name sexname,' --�����Ա�����
                 || 'b.agestr agestr,' --����
                 || 'b.py py,' --ƴ��
                 || 'b.wb wb,' --���
                 || 'b.status brzt,' --����״̬
                 || 'e.name brztname,' --����״̬����
                 || 'RTRIM(b.criticallevel) wzjb,' --Σ�ؼ���
                 || 'i.name wzjbmc,'; --Σ�ؼ�������
        --cd.name hljb,
      
        --CASE WHEN b.attendlevel IS NULL THEN 6105 ELSE to_number(b.attendlevel)
        --END attendlevel, --������
      
        v_sql := v_sql || ' case b.attendlevel' || '   when ''1'' then ' ||
                 '    ''һ������''' || '   when ''2'' then' || ' ''��������''' ||
                 ' when ''3'' then' || '  ''��������''' || '  when ''4'' then' ||
                 '    ''�ؼ�����'' ' || '   else ' || ' ''''' || ' end hljb,' --������
                 || ' case b.attendlevel' || ' when ''1'' then ' ||
                 '    ''6101''' || ' when ''2'' then ' || ' ''6102''' ||
                 ' when ''3'' then' || '  ''6103''' || '   when ''4'' then' ||
                 '    ''6104''' || '  else ' || '    ''6101''' ||
                 ' end attendlevel,' --������
                 || '  b.isbaby yebz, ' --Ӥ����־
                 || '  CASE ' || '   WHEN b.isbaby = ''0'' THEN ' ||
                 ' ''��''' || ' WHEN b.isbaby IS NULL THEN ' || '  ''''' ||
                 ' ELSE ' || '  ''��''' || ' END yebzname,';
        --a.wardid bqdm, --��������
        --a.deptid ksdm, --���Ҵ���
      
        v_sql := v_sql || 'b.outhosward bqdm,' --��������
                 || 'b.outhosdept ksdm,'; --���Ҵ���';
      
        --a.ID bedid, --��λ����
        v_sql := v_sql ||
                 ' case when length(b.outbed) = 1 and isnumber(b.outbed) = 1 then ''0'' || b.outbed else b.outbed end bedid,' --��λ����
                 || 'dg.NAME ksmc,' --��������
                 || 'wh.NAME bqmc,' --��������
                 || 'a.formerward ybqdm,' --ԭ��������
                 || 'a.formerdeptid yksdm,' --ԭ���Ҵ���
                 || 'a.formerdeptid ycwdm,' --ԭ��λ����
                 || 'a.inbed inbed,' --ռ����־
                 || 'a.borrow jcbz,' --�贲��־
                 || 'a.sexinfo cwlx,' --��λ����
                 || 'SUBSTR(b.admitdate, 1, 16) admitdate,' --��Ժ����
                 || 'f.NAME ryzd,' --��Ժ���
                 || 'f.NAME zdmc,' --�������
                 || 'b.resident zyysdm,' --סԺҽ������
                 || 'c.NAME zyys,' --סԺҽ��
                 || 'c.NAME cwys,' --��λҽ��
                 || 'g.NAME zzys,' --����ҽʦ
                 || 'h.NAME zrys,' --����ҽʦ
                 || 'a.valid yxjl,' --��Ч��¼
                --me.pzlx pzlx, --ƾ֤����
                 || 'dd1.NAME pzlx,'; --�������';
        /*
        TO_CHAR((CASE
                  WHEN INSTR(v_deptids, a.deptid) = 0 AND (b.noofinpat IS NULL) THEN
                   '������������'
                  ELSE
                   ''
                END)) extra, --������Ϣ
        */
        v_sql := v_sql || '  CASE
               WHEN b.outhosdept =''' || v_deptids ||
                 ''' AND b.outhosward =''' || v_wardid ||
                 ''' THEN
                ''������''
               ELSE
               ''������������''
             END extra,' --������Ϣ
                 || 'b.memo memo,' --��ע
                 || ' CASE b.cpstatus
               WHEN 0 THEN
                ''δ����''
               WHEN 1 THEN
                ''ִ����''
               WHEN 2 THEN
                ''�˳�''
             END cpstatus,
             CASE
               WHEN b.noofinpat IS NULL THEN
                ''''
               ELSE
                ''����д''
             END recordinfo,
             100 ye,' --���
                 || '(SELECT CASE
                       WHEN COUNT(qc.foulstate) > 0 THEN
                        ''��''
                       ELSE
                        ''��''
                     END
                FROM qcrecord qc
               WHERE qc.noofinpat = b.noofinpat
                 AND qc.valid = 1
                 AND qc.foulstate = 1) AS iswarn,' --�Ƿ�Υ��
                 || 'b.incount as rycs, ' --��Ժ����
                 || 'substr(b.inwarddate,1,16) ,' -- ������� add by cyq 2013-03-13
                 || 'b.idno idinfo '; --���ID
        v_sql := v_sql || '  FROM
      (select * from inpatient where ' || 'outhosdept =''' ||
                 v_deptids || ''' AND outhosward =''' || v_wardid ||
                 ''' AND status in (''1500'',''1501'') AND isbaby != ''1'') b
       LEFT JOIN( select formerward,formerdeptid,inbed,borrow,sexinfo,valid,noofinpat,id from bed where
        deptid =''' || v_deptids || ''' AND wardid =''' ||
                 v_wardid ||
                 ''' and valid = ''1'')
             a ON a.id = b.outbed AND a.noofinpat = b.noofinpat ';
        /*LEFT JOIN inpatient b ON a.noofinpat = b.noofinpat
                               AND a.patnoofhis = b.patnoofhis
                                  --AND INSTR(v_deptids, RTRIM(b.outhosdept)) > 0
                                  --AND b.status IN (1501, 1504, 1505, 1506, 1507) --��Ժ
                               AND a.inbed = 1301 --ռ��
        -- 1501 �����ִ�,1504 ȡ������,1505 ����ICU,1506 �������,1507 ת��״̬
          */
      
        v_sql := v_sql || ' LEFT JOIN (select name,detailid from Dictionary_detail where categoryid = ''1'') dd1 ON
                                       b.payid = dd1.detailid
        LEFT JOIN (select name,id from categorydetail where categoryid = ''15'') e ON b.status = e.ID
        LEFT JOIN (select name,id from department) dg ON b.outhosdept = dg.id
        LEFT JOIN (select name,id from ward) wh ON b.outhosward = wh.id
        LEFT JOIN (select name,icd from diagnosis) f ON f.icd = b.admitdiagnosis
        LEFT JOIN (select id,name from users) c ON c.ID = b.resident
        LEFT JOIN (select name,detailid from dictionary_detail where categoryid = ''53'') i ON i.detailid = b.criticallevel
        LEFT JOIN (select id,name from users) g ON b.attend = g.id
        LEFT JOIN (select id,name from users) h ON b.chief = h.id
        LEFT JOIN (select name, detailid from dictionary_detail where categoryid = ''3'')  j ON j.detailid = b.sexid
        LEFT JOIN (select name,id from categorydetail where categoryid = ''63'') cd ON b.attendlevel = cd.ID
       WHERE 1=1 '; /*((b.outhosdept = v_deptids AND b.outhosward = v_wardid) OR
                                           (v_deptids IN
                                           (SELECT bedinfo.newdept
                                                FROM bedinfo
                                               WHERE bedinfo.noofinpat = b.noofinpat) AND
                                           v_wardid IN
                                           (SELECT bedinfo.newward
                                                FROM bedinfo
                                               WHERE bedinfo.noofinpat = b.noofinpat)))
                                       AND a.valid = 1;*/
        /*  v_sql:=v_sql||' b.outhosdept = '||v_deptids||' AND b.outhosward ='|| v_wardid
        ||' AND b.status in (''1500'',''1501'') AND b.isbaby != ''1''*/
        v_sql := v_sql || ' ORDER BY bedid';
      
        /*
        dbms_output.put_line(substr(v_sql, 1, 1000));
        dbms_output.put_line(substr(v_sql, 1001, 1000));
        dbms_output.put_line(substr(v_sql, 2001, 1000));
        dbms_output.put_line(substr(v_sql, 3001, 1000));
        */
      
        execute immediate v_sql;
        -- ��鲡���Ƿ��в���
        v_sql := 'UPDATE ' || v_table || ' a
       SET extra = extra || ''�޲���'', recordinfo = ''�޲���''
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS (SELECT 1
              FROM recorddetail b
             WHERE a.noofinpat = b.noofinpat
               and b.valid = ''1'')';
      
        execute immediate v_sql;
        IF v_querytype = 0 THEN
          --ȫ��
          BEGIN
            OPEN o_result FOR 'SELECT rownum ID, b.*
          FROM
          (
              SELECT a.* FROM ' || v_table || ' a
               WHERE a.noofinpat IS NOT NULL
               ORDER BY a.bedid
          ) b';
          END;
        ELSIF v_querytype = 1 THEN
          --�ֹ�
          BEGIN
            OPEN o_result FOR ' SELECT rownum ID, d.* FROM
          (
            SELECT c.* FROM
            (
                SELECT a.* FROM ' || v_table || ' a
                  JOIN doctor_assignpatient da ON a.noofinpat = da.noofinpat
                                        AND da.ID =''' || v_id || ''' AND da.valid = 1

                UNION
                --Modified By wwj 2011-09-02
                SELECT b.*
                  FROM ' || v_table || ' b
                 where zyysdm =''' || v_id || '''   and noofinpat not in (select noofinpat
                                     from Doctor_AssignPatient
                                    where valid = ''1'')
             ) c
             ORDER BY c.bedid --���ҵĻ��ߡ����մ�λ������
          ) d';
          END;
        ELSIF v_querytype = 2 THEN
          --δ����ҽ���Ļ���
          BEGIN
            OPEN o_result FOR 'SELECT *
            FROM ' || v_table || ' a
           WHERE a.noofinpat IS NOT NULL
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient d
                   WHERE d.valid = 1
                     AND d.noofinpat = a.noofinpat)
           ORDER BY a.bedid';
          END;
        ELSIF v_querytype = 3 THEN
          --����
          BEGIN
            IF v_querybed = 'N' THEN
              --�������մ�
              BEGIN
                OPEN o_result FOR 'SELECT *
                FROM ' || v_table || ' a
               WHERE a.NoOfInpat IS NOT NULL
               ORDER BY a.bedid';
              END;
            END IF;
          
            IF v_querybed = 'Y' THEN
              --�����մ�
              BEGIN
                OPEN o_result FOR ' SELECT * FROM ' || v_table || ' a ORDER BY a.bedid';
              END;
            END IF;
          END;
        END IF;
      --����ɾ��add by ywk 
        execute immediate 'drop table ' || v_table ||' purge';
      end;
    end if;
  END;

  /*
  xuliangliang2012-12-17�������Ͽ⵼��
  �������ɽ���Ҳ����������� ��like���
  */
  PROCEDURE usp_queryinwardpatientsQLS(v_wardid    VARCHAR,
                                       v_deptids   VARCHAR,
                                       v_zyhm      VARCHAR DEFAULT '',
                                       v_hzxm      VARCHAR DEFAULT '',
                                       v_cyzd      VARCHAR DEFAULT '',
                                       v_ryrqbegin VARCHAR DEFAULT '',
                                       v_ryrqend   VARCHAR DEFAULT '',
                                       v_cyrqbegin VARCHAR DEFAULT '',
                                       v_cyrqend   VARCHAR DEFAULT '',
                                       --add by xjt
                                       v_id        VARCHAR DEFAULT '',
                                       v_querytype INT DEFAULT 0,
                                       v_querynur  VARCHAR DEFAULT '',
                                       v_querybed  VARCHAR DEFAULT 'Y',
                                       o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��Ӧ�����Ĵ�λ��Ϣ
     ����˵��
     �������
      v_Wardid   varchar(6)   --��������
      v_Deptids varchar(255)  --���Ҵ��뼯��(����: '����1','����2')
      v_zyhm  varchar(30)=''  --סԺ��
      v_hzxm  varchar(30)=''  --����
      v_cyzd  varchar(30)=''  --��Ժ���
      v_cyrqbegin varchar(19)=''  --��Ժ������Сֵ
      v_cyrqend varchar(19)=''  --��Ժ�������ֵ
      v_ryrqbegin varchar(19)=''  --��Ժ������Сֵ
      v_ryrqend varchar(19)=''  --��Ժ�������ֵ
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryInwardPatients  '2911', '3202','','','','','','','','',3,'Y'
     �޸ļ�¼
    �����������
    **********/
    v_wardid_in VARCHAR(6);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(4000);
  BEGIN
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    IF v_querytype = 0 THEN
      --ȫ��
      BEGIN
        OPEN o_result FOR
        
          SELECT rownum         ID,
                 a.*,
                 b.formerward   ybqdm, --ԭ��������
                 b.formerdeptid yksdm, --ԭ���Ҵ���
                 b.formerdeptid ycwdm, --ԭ��λ����
                 
                 b.deptid beddeptid,
                 b.wardid bedwardid,
                 
                 b.borrow  jcbz, --�贲��־
                 b.sexinfo cwlx, --��λ����
                 b.valid   yxjl --��Ч��¼
            FROM (select *
                    from view_tmp_queryinwardpatients_2
                   where view_tmp_queryinwardpatients_2.noofinpat IS NOT NULL
                     and view_tmp_queryinwardpatients_2.outhosdept like
                         '%' || v_deptids || '%'
                     AND view_tmp_queryinwardpatients_2.outhosward like
                         '%' || v_wardid || '%') a
            LEFT JOIN (select bed.id,
                              bed.borrow,
                              bed.formerward,
                              bed.formerdeptid,
                              bed.deptid,
                              bed.wardid,
                              bed.sexinfo,
                              bed.valid,
                              bed.noofinpat
                         from bed
                        where bed.valid = '1'
                          AND bed.deptid = v_deptids
                          AND bed.wardid = v_wardid
                       
                       ) b
              ON b.id = a.outbed
             AND b.noofinpat = a.noofinpat
           ORDER BY a.bedid;
      
      END;
    ELSIF v_querytype = 1 THEN
      --�ֹ�
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, d.*
            FROM (SELECT c.*
                    FROM (
                          /*SELECT a.* FROM tmp_queryinwardpatients a
                            JOIN doctor_assignpatient da ON a.noofinpat = da.noofinpat
                                                  AND da.ID = v_id
                                                  AND da.valid = 1
                          
                          UNION
                          --Modified By wwj 2011-09-02
                          SELECT b.*
                            FROM tmp_queryinwardpatients b
                           where zyysdm = v_id
                             and noofinpat not in (select noofinpat
                                               from Doctor_AssignPatient
                                              where valid = '1')*/
                          SELECT a.*,
                                  
                                  b.formerward   ybqdm, --ԭ��������
                                  b.formerdeptid yksdm, --ԭ���Ҵ���
                                  b.formerdeptid ycwdm, --ԭ��λ����
                                  
                                  b.deptid beddeptid,
                                  b.wardid bedwardid,
                                  
                                  b.borrow  jcbz, --�贲��־
                                  b.sexinfo cwlx, --��λ����
                                  b.valid   yxjl --��Ч��¼
                            FROM (select *
                                     from view_tmp_queryinwardpatients_2
                                    where view_tmp_queryinwardpatients_2.outhosdept like
                                          '%' || v_deptids || '%'
                                      AND view_tmp_queryinwardpatients_2.outhosward like
                                          '%' || v_wardid || '%'
                                   
                                   ) a
                            LEFT JOIN (select *
                                         from bed
                                        where bed.valid = '1'
                                          AND bed.deptid = v_deptids
                                          AND bed.wardid = v_wardid
                                       
                                       ) b
                              ON b.id = a.outbed
                             AND b.noofinpat = a.noofinpat
                          
                            JOIN doctor_assignpatient da
                              ON a.noofinpat = da.noofinpat
                             AND da.ID = v_id
                             AND da.valid = 1
                          
                          UNION
                          --Modified By wwj 2011-09-02
                          SELECT a.*,
                                 b.formerward   ybqdm, --ԭ��������
                                 b.formerdeptid yksdm, --ԭ���Ҵ���
                                 b.formerdeptid ycwdm, --ԭ��λ����
                                 
                                 b.deptid beddeptid,
                                 b.wardid bedwardid,
                                 
                                 b.borrow  jcbz, --�贲��־
                                 b.sexinfo cwlx, --��λ����
                                 b.valid   yxjl --��Ч��¼
                          
                            FROM (select *
                                    from view_tmp_queryinwardpatients_2
                                   where view_tmp_queryinwardpatients_2.outhosdept like
                                         '%' || v_deptids || '%'
                                     AND view_tmp_queryinwardpatients_2.outhosward like
                                         '%' || v_wardid || '%') a
                            LEFT JOIN (select *
                                         from bed
                                        where bed.valid = '1'
                                          AND bed.deptid = v_deptids
                                          AND bed.wardid = v_wardid) b
                              ON b.id = a.outbed
                             AND b.noofinpat = a.noofinpat
                             AND b.valid = '1'
                           where zyysdm = v_id
                             and a.noofinpat not in
                                 (select noofinpat
                                    from Doctor_AssignPatient
                                   where valid = '1')) c
                   ORDER BY c.bedid --���ҵĻ��ߡ����մ�λ������
                  ) d;
      END;
    ELSIF v_querytype = 2 THEN
      --δ����ҽ���Ļ���
      BEGIN
        OPEN o_result FOR
        /*SELECT *
                                                    FROM tmp_queryinwardpatients a
                                                   WHERE a.noofinpat IS NOT NULL
                                                     AND NOT EXISTS (SELECT 1
                                                            FROM doctor_assignpatient d
                                                           WHERE d.valid = 1
                                                             AND d.noofinpat = a.noofinpat)
                                                   ORDER BY a.bedid;*/
          SELECT a.*,
                 b.formerward   ybqdm, --ԭ��������
                 b.formerdeptid yksdm, --ԭ���Ҵ���
                 b.formerdeptid ycwdm, --ԭ��λ����
                 
                 b.deptid beddeptid,
                 b.wardid bedwardid,
                 
                 b.borrow  jcbz, --�贲��־
                 b.sexinfo cwlx, --��λ����
                 b.valid   yxjl --��Ч��¼
            FROM (select *
                    from view_tmp_queryinwardpatients_2
                   where view_tmp_queryinwardpatients_2.outhosdept like
                         '%' || v_deptids || '%'
                     AND view_tmp_queryinwardpatients_2.outhosward like
                         '%' || v_wardid || '%') a
            LEFT JOIN (select *
                         from bed
                        where bed.valid = '1'
                          AND bed.deptid = v_deptids
                          AND bed.wardid = v_wardid
                       
                       ) b
              ON b.id = a.outbed
             AND b.noofinpat = a.noofinpat
           WHERE a.noofinpat IS NOT NULL
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient d
                   WHERE d.valid = 1
                     AND d.noofinpat = a.noofinpat)
          
           ORDER BY a.bedid;
      END;
    ELSIF v_querytype = 3 THEN
      --����
      BEGIN
        IF v_querybed = 'N' THEN
          --�������մ�
          BEGIN
            OPEN o_result FOR
            /* SELECT *
                                                                            FROM tmp_QueryInwardPatients a
                                                                           WHERE a.NoOfInpat IS NOT NULL
                                                                           ORDER BY a.bedid;*/
              SELECT a.*,
                     b.formerward   ybqdm, --ԭ��������
                     b.formerdeptid yksdm, --ԭ���Ҵ���
                     b.formerdeptid ycwdm, --ԭ��λ����
                     
                     b.deptid beddeptid,
                     b.wardid bedwardid,
                     
                     b.borrow  jcbz, --�贲��־
                     b.sexinfo cwlx, --��λ����
                     b.valid   yxjl --��Ч��¼
                FROM (select *
                        from view_tmp_queryinwardpatients_2
                       where view_tmp_queryinwardpatients_2.outhosdept like
                             '%' || v_deptids || '%'
                         AND view_tmp_queryinwardpatients_2.outhosward like
                             '%' || v_wardid || '%') a
                LEFT JOIN (select *
                             from bed
                            where bed.valid = '1'
                              AND bed.deptid = v_deptids
                              AND bed.wardid = v_wardid
                           
                           ) b
                  ON b.id = a.outbed
                 AND b.noofinpat = a.noofinpat
               WHERE a.NoOfInpat IS NOT NULL
               ORDER BY a.bedid;
          END;
        END IF;
      
        IF v_querybed = 'Y' THEN
          --�����մ�
          BEGIN
            OPEN o_result FOR
            /*SELECT * FROM tmp_QueryInwardPatients a ORDER BY a.bedid;*/
              SELECT a.*,
                     b.formerward   ybqdm, --ԭ��������
                     b.formerdeptid yksdm, --ԭ���Ҵ���
                     b.formerdeptid ycwdm, --ԭ��λ����
                     
                     b.deptid beddeptid,
                     b.wardid bedwardid,
                     
                     b.borrow  jcbz, --�贲��־
                     b.sexinfo cwlx, --��λ����
                     b.valid   yxjl --��Ч��¼
                FROM (select *
                        from view_tmp_queryinwardpatients_2
                       where view_tmp_queryinwardpatients_2.outhosdept like
                             '%' || v_deptids || '%'
                         AND view_tmp_queryinwardpatients_2.outhosward like
                             '%' || v_wardid || '%') a
                LEFT JOIN (select *
                             from bed
                            where bed.valid = '1'
                              AND bed.deptid = v_deptids
                              AND bed.wardid = v_wardid
                           
                           ) b
                  ON b.id = a.outbed
                 AND b.noofinpat = a.noofinpat
              
               ORDER BY a.bedid;
          END;
        END IF;
      END;
    END IF;
  END;

  -----------------------======
  /*v_wardid_in VARCHAR(6);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(4000);
  BEGIN
    v_sql := 'truncate table tmp_QueryInwardPatients ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    -- ���ҳ����˼�¼��Ȼ���ȡ���˵ĸ�����Ϣ����������Ժ��ת�Ƶȣ�
    INSERT INTO tmp_queryinwardpatients
      SELECT distinct b.noofinpat noofinpat, --��ҳ���
             b.patnoofhis patnoofhis, --HIS��ҳ���
             b.patid patid, --סԺ��
             b.name patname, --����
             b.sexid sex, --�����Ա�
             j.name sexname, --�����Ա�����
             b.agestr agestr, --����
             b.py py, --ƴ��
             b.wb wb, --���
             b.status brzt, --����״̬
             e.name brztname, --����״̬����
             RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
             i.name wzjbmc, --Σ�ؼ�������
             --cd.name hljb,
  
             --CASE WHEN b.attendlevel IS NULL THEN 6105 ELSE to_number(b.attendlevel)
             --END attendlevel, --������
  
             case b.attendlevel
               when '1' then
                'һ������'
               when '2' then
                '��������'
               when '3' then
                '��������'
               when '4' then
                '�ؼ�����'
               else
                'һ������'
             end hljb, --������
  
             case b.attendlevel
               when '1' then
                '6101'
               when '2' then
                '6102'
               when '3' then
                '6103'
               when '4' then
                '6104'
               else
                '6101'
             end attendlevel, --������
             b.isbaby yebz, --Ӥ����־
             CASE
               WHEN b.isbaby = '0' THEN
                '��'
               WHEN b.isbaby IS NULL THEN
                ''
               ELSE
                '��'
             END yebzname,
             --a.wardid bqdm, --��������
             --a.deptid ksdm, --���Ҵ���
  
             b.outhosward bqdm, --��������
             b.outhosdept ksdm, --���Ҵ���
  
             --a.ID bedid, --��λ����
             case when length(b.outbed) = 1 then '0' || b.outbed else b.outbed end bedid, --��λ����
             dg.NAME ksmc, --��������
             wh.NAME bqmc, --��������
             a.formerward ybqdm, --ԭ��������
             a.formerdeptid yksdm, --ԭ���Ҵ���
             a.formerdeptid ycwdm, --ԭ��λ����
             a.inbed inbed, --ռ����־
             a.borrow jcbz, --�贲��־
             a.sexinfo cwlx, --��λ����
             SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
             f.NAME ryzd, --��Ժ���
             f.NAME zdmc, --�������
             b.resident zyysdm, --סԺҽ������
             c.NAME zyys, --סԺҽ��
             c.NAME cwys, --��λҽ��
             g.NAME zzys, --����ҽʦ
             h.NAME zrys, --����ҽʦ
             a.valid yxjl, --��Ч��¼
             --me.pzlx pzlx, --ƾ֤����
             dd1.NAME pzlx, --�������
             \*
                                                                 TO_CHAR((CASE
                                                                           WHEN INSTR(v_deptids, a.deptid) = 0 AND (b.noofinpat IS NULL) THEN
                                                                            '������������'
                                                                           ELSE
                                                                            ''
                                                                         END)) extra, --������Ϣ
                                                                 *\
             CASE
               WHEN b.outhosdept = v_deptids AND b.outhosward = v_wardid THEN
                '������'
               ELSE
                '������������'
             END extra, --������Ϣ
             b.memo memo, --��ע
             CASE b.cpstatus
               WHEN 0 THEN
                'δ����'
               WHEN 1 THEN
                'ִ����'
               WHEN 2 THEN
                '�˳�'
             END cpstatus,
             CASE
               WHEN b.noofinpat IS NULL THEN
                ''
               ELSE
                '����д'
             END recordinfo,
             100 ye, --���
             (SELECT CASE
                       WHEN COUNT(qc.foulstate) > 0 THEN
                        '��'
                       ELSE
                        '��'
                     END
                FROM qcrecord qc
               WHERE qc.noofinpat = b.noofinpat
                 AND qc.valid = 1
                 AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
             b.incount as rycs --��Ժ����
        FROM inpatient b
       LEFT JOIN bed a ON a.id = b.outbed
       AND a.deptid = v_deptids AND a.wardid = v_wardid
       AND a.noofinpat = b.noofinpat AND a.valid = '1'
        \*LEFT JOIN inpatient b ON a.noofinpat = b.noofinpat
                             AND a.patnoofhis = b.patnoofhis
                                --AND INSTR(v_deptids, RTRIM(b.outhosdept)) > 0
                                --AND b.status IN (1501, 1504, 1505, 1506, 1507) --��Ժ
                             AND a.inbed = 1301 --ռ��
      -- 1501 �����ִ�,1504 ȡ������,1505 ����ICU,1506 �������,1507 ת��״̬
        *\
        LEFT JOIN Dictionary_detail dd1 ON dd1.categoryid = '1'
                                       AND b.payid = dd1.detailid
        LEFT JOIN categorydetail e ON b.status = e.ID
                                  AND e.categoryid = '15'
        LEFT JOIN department dg ON b.outhosdept = dg.id
        LEFT JOIN ward wh ON b.outhosward = wh.id
        LEFT JOIN diagnosis f ON f.icd = b.admitdiagnosis
        LEFT JOIN users c ON c.ID = b.resident
        LEFT JOIN dictionary_detail i ON i.detailid = b.criticallevel
                                     AND i.categoryid = '53'
        LEFT JOIN users g ON b.attend = g.id
        LEFT JOIN users h ON b.chief = h.id
        LEFT JOIN dictionary_detail j ON j.detailid = b.sexid
                                     AND j.categoryid = '3'
        LEFT JOIN categorydetail cd ON b.attendlevel = cd.ID
                                   AND cd.categoryid = '63'
       WHERE \*((b.outhosdept = v_deptids AND b.outhosward = v_wardid) OR
             (v_deptids IN
             (SELECT bedinfo.newdept
                  FROM bedinfo
                 WHERE bedinfo.noofinpat = b.noofinpat) AND
             v_wardid IN
             (SELECT bedinfo.newward
                  FROM bedinfo
                 WHERE bedinfo.noofinpat = b.noofinpat)))
         AND a.valid = 1;*\
         b.outhosdept = v_deptids AND b.outhosward = v_wardid
         AND b.status in ('1500','1501') AND b.isbaby != '1'
       ORDER BY bedid;
    \*
      v_dqrq := TO_CHAR(SYSDATE, 'yyyy-mm-dd');
      v_ksrq := TO_CHAR(SYSDATE - 3, 'yyyy-mm-dd');
      v_jsrq := TO_CHAR(SYSDATE + 2, 'yyyy-mm-dd');
      v_now  := TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss');
  
      -- ���������Ϣ:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢����ʱ���ڵ�ǰ����-3��+1֮��
      INSERT INTO tmp_queryinwardpats_extraop
        SELECT a.noofinpat,
               datediff('dd', v_now, TO_CHAR(SUBSTR(a.entrust, 1, 19))) diff
          FROM temp_order a, tmp_queryinwardpatients b
         WHERE a.noofinpat = b.noofinpat
           AND a.startdate BETWEEN v_ksrq AND v_jsrq
           AND a.ordertype = 3105
           AND a.orderstatus IN (3201, 3202);
  
      UPDATE tmp_queryinwardpatients a
         SET extra = (SELECT a.extra || (CASE b.diff
                               WHEN 1 THEN
                                '��������'
                               WHEN 0 THEN
                                '��������'
                               WHEN -1 THEN
                                '����1��'
                               WHEN -2 THEN
                                '����2��'
                               WHEN -3 THEN
                                '����3��'
                               ELSE
                                ''
                             END) || ' '
                        FROM tmp_queryinwardpats_extraop b
                       WHERE a.noofinpat = b.noofinpat
                         AND ROWNUM < 1);
  
      -- ����Ժҽ��:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢��Ժʱ���ڵ�ǰ����0ʱ֮��
      v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
  
      EXECUTE IMMEDIATE v_sql;
  
      INSERT INTO tmp_queryinwardpats_extraop
        SELECT a.noofinpat,
               datediff('dd', v_now, TO_CHAR(a.entrust, 'yyyy-mm-dd')) diff
          FROM temp_order a, tmp_queryinwardpatients b
         WHERE a.noofinpat = b.noofinpat
           AND a.startdate >= v_dqrq
           AND a.ordertype = 3113
           AND a.orderstatus IN (3201, 3202);
  
      UPDATE tmp_queryinwardpatients a
         SET extra = (SELECT a.extra || (CASE b.diff
                               WHEN 0 THEN
                                '���ճ�Ժ'
                               WHEN 1 THEN
                                '���ճ�Ժ'
                               ELSE
                                TO_CHAR(b.diff) || '����Ժ'
                             END) || ' '
                        FROM tmp_queryinwardpats_extraop b
                       WHERE a.noofinpat = b.noofinpat
                         AND ROWNUM < 1);
  
      -- ���ת��ҽ��:���ݲ���״̬
      UPDATE tmp_queryinwardpatients a
         SET extra = (SELECT a.extra || b.NAME || ' '
                        FROM categorydetail b
                       WHERE b.categoryid = a.brzt
                         AND ROWNUM < 1)
       WHERE a.brzt IN (1505, 1506, 1507);
    *\
    -- ��鲡���Ƿ��в���
    UPDATE tmp_queryinwardpatients a
       SET extra = extra || '�޲���', recordinfo = '�޲���'
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS (SELECT 1
              FROM recorddetail b
             WHERE a.noofinpat = b.noofinpat
               and b.valid = '1');
  
    IF v_querytype = 0 THEN
      --ȫ��
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, b.*
          FROM
          (
              SELECT a.* FROM tmp_queryinwardpatients a
               WHERE a.noofinpat IS NOT NULL
               ORDER BY a.bedid
          ) b;
      END;
    ELSIF v_querytype = 1 THEN
      --�ֹ�
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, d.* FROM
          (
            SELECT c.* FROM
            (
                SELECT a.* FROM tmp_queryinwardpatients a
                  JOIN doctor_assignpatient da ON a.noofinpat = da.noofinpat
                                        AND da.ID = v_id
                                        AND da.valid = 1
  
                UNION
                --Modified By wwj 2011-09-02
                SELECT b.*
                  FROM tmp_queryinwardpatients b
                 where zyysdm = v_id
                   and noofinpat not in (select noofinpat
                                     from Doctor_AssignPatient
                                    where valid = '1')
             ) c
             ORDER BY c.bedid --���ҵĻ��ߡ����մ�λ������
          ) d;
      END;
    ELSIF v_querytype = 2 THEN
      --δ����ҽ���Ļ���
      BEGIN
        OPEN o_result FOR
          SELECT *
            FROM tmp_queryinwardpatients a
           WHERE a.noofinpat IS NOT NULL
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient d
                   WHERE d.valid = 1
                     AND d.noofinpat = a.noofinpat)
           ORDER BY a.bedid;
      END;
    ELSIF v_querytype = 3 THEN
      --����
      BEGIN
        IF v_querybed = 'N' THEN
          --�������մ�
          BEGIN
            OPEN o_result FOR
              SELECT *
                FROM tmp_QueryInwardPatients a
               WHERE a.NoOfInpat IS NOT NULL
               ORDER BY a.bedid;
          END;
        END IF;
  
        IF v_querybed = 'Y' THEN
          --�����մ�
          BEGIN
            OPEN o_result FOR
              SELECT * FROM tmp_QueryInwardPatients a ORDER BY a.bedid;
          END;
        END IF;
      END;
    END IF;
  END;*/

  --�����������ң���һ���������ܻ��ж������
  PROCEDURE usp_queryinwardpatients2(v_wardid    VARCHAR,
                                     v_deptids   VARCHAR,
                                     v_zyhm      VARCHAR DEFAULT '',
                                     v_hzxm      VARCHAR DEFAULT '',
                                     v_cyzd      VARCHAR DEFAULT '',
                                     v_ryrqbegin VARCHAR DEFAULT '',
                                     v_ryrqend   VARCHAR DEFAULT '',
                                     v_cyrqbegin VARCHAR DEFAULT '',
                                     v_cyrqend   VARCHAR DEFAULT '',
                                     --add by xjt
                                     v_id        VARCHAR DEFAULT '',
                                     v_querytype INT DEFAULT 0,
                                     v_querynur  VARCHAR DEFAULT '',
                                     v_querybed  VARCHAR DEFAULT 'Y',
                                     o_result    OUT empcurtyp) AS
    v_wardid_in VARCHAR(6);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(4000);
  BEGIN
    v_sql := 'truncate table tmp_QueryInwardPatients ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryInwardPats_extraop ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    -- ���ҳ����˼�¼��Ȼ���ȡ���˵ĸ�����Ϣ����������Ժ��ת�Ƶȣ�
    INSERT INTO tmp_queryinwardpatients
      SELECT distinct b.noofinpat noofinpat, --��ҳ���
                      b.patnoofhis patnoofhis, --HIS��ҳ���
                      b.patid patid, --סԺ��
                      b.name patname, --����
                      b.sexid sex, --�����Ա�
                      j.name sexname, --�����Ա�����
                      b.agestr agestr, --����
                      b.py py, --ƴ��
                      b.wb wb, --���
                      b.status brzt, --����״̬
                      e.name brztname, --����״̬����
                      RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
                      i.name wzjbmc, --Σ�ؼ�������
                      case b.attendlevel
                        when '1' then
                         'һ������'
                        when '2' then
                         '��������'
                        when '3' then
                         '��������'
                        when '4' then
                         '�ؼ�����'
                        else
                         ''
                      end hljb, --������
                      
                      case b.attendlevel
                        when '1' then
                         '6101'
                        when '2' then
                         '6102'
                        when '3' then
                         '6103'
                        when '4' then
                         '6104'
                        else
                         '6101'
                      end attendlevel, --������
                      b.isbaby yebz, --Ӥ����־
                      CASE
                        WHEN b.isbaby = '0' THEN
                         '��'
                        WHEN b.isbaby IS NULL THEN
                         ''
                        ELSE
                         '��'
                      END yebzname,
                      b.outhosward bqdm, --��������
                      b.outhosdept ksdm, --���Ҵ���
                      case
                        when length(b.outbed) = 1 then
                         '0' || b.outbed
                        else
                         b.outbed
                      end bedid, --��λ����
                      dg.NAME ksmc, --��������
                      wh.NAME bqmc, --��������
                      a.formerward ybqdm, --ԭ��������
                      a.formerdeptid yksdm, --ԭ���Ҵ���
                      a.formerdeptid ycwdm, --ԭ��λ����
                      a.inbed inbed, --ռ����־
                      a.borrow jcbz, --�贲��־
                      a.sexinfo cwlx, --��λ����
                      SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
                      f.NAME ryzd, --��Ժ���
                      f.NAME zdmc, --�������
                      b.resident zyysdm, --סԺҽ������
                      c.NAME zyys, --סԺҽ��
                      c.NAME cwys, --��λҽ��
                      g.NAME zzys, --����ҽʦ
                      h.NAME zrys, --����ҽʦ
                      a.valid yxjl, --��Ч��¼
                      --me.pzlx pzlx, --ƾ֤����
                      dd1.NAME pzlx, --�������
                      CASE
                        WHEN b.outhosdept = v_deptids AND
                             b.outhosward = v_wardid THEN
                         '������'
                        ELSE
                         '������������'
                      END extra, --������Ϣ
                      b.memo memo, --��ע
                      CASE b.cpstatus
                        WHEN 0 THEN
                         'δ����'
                        WHEN 1 THEN
                         'ִ����'
                        WHEN 2 THEN
                         '�˳�'
                      END cpstatus,
                      CASE
                        WHEN b.noofinpat IS NULL THEN
                         ''
                        ELSE
                         '����д'
                      END recordinfo,
                      100 ye, --���
                      (SELECT CASE
                                WHEN COUNT(qc.foulstate) > 0 THEN
                                 '��'
                                ELSE
                                 '��'
                              END
                         FROM qcrecord qc
                        WHERE qc.noofinpat = b.noofinpat
                          AND qc.valid = 1
                          AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
                      b.incount as rycs --��Ժ����
        FROM inpatient b
        LEFT JOIN bed a
          ON a.id = b.outbed
         AND a.deptid = v_deptids
         AND a.wardid = v_wardid
         AND a.noofinpat = b.noofinpat
         AND a.valid = '1'
        LEFT JOIN Dictionary_detail dd1
          ON dd1.categoryid = '1'
         AND b.payid = dd1.detailid
        LEFT JOIN categorydetail e
          ON b.status = e.ID
         AND e.categoryid = '15'
        LEFT JOIN department dg
          ON b.outhosdept = dg.id
        LEFT JOIN ward wh
          ON b.outhosward = wh.id
        LEFT JOIN diagnosis f
          ON f.icd = b.admitdiagnosis
        LEFT JOIN users c
          ON c.ID = b.resident
        LEFT JOIN dictionary_detail i
          ON i.detailid = b.criticallevel
         AND i.categoryid = '53'
        LEFT JOIN users g
          ON b.attend = g.id
        LEFT JOIN users h
          ON b.chief = h.id
        LEFT JOIN dictionary_detail j
          ON j.detailid = b.sexid
         AND j.categoryid = '3'
        LEFT JOIN categorydetail cd
          ON b.attendlevel = cd.ID
         AND cd.categoryid = '63'
       WHERE --b.outhosdept = v_deptids AND
       b.outhosward = v_wardid
       AND b.status in ('1500', '1501')
       AND b.isbaby != '1'
       ORDER BY bedid;
  
    -- ��鲡���Ƿ��в���
    UPDATE tmp_queryinwardpatients a
       SET extra = extra || '�޲���', recordinfo = '�޲���'
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS (SELECT 1
              FROM recorddetail b
             WHERE a.noofinpat = b.noofinpat
               and b.valid = '1');
  
    IF v_querytype = 0 THEN
      --ȫ��
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, b.*
            FROM (SELECT a.*
                    FROM tmp_queryinwardpatients a
                   WHERE a.noofinpat IS NOT NULL
                   ORDER BY a.bedid) b;
      END;
    ELSIF v_querytype = 1 THEN
      --�ֹ�
      BEGIN
        OPEN o_result FOR
          SELECT rownum ID, d.*
            FROM (SELECT c.*
                    FROM (SELECT a.*
                            FROM tmp_queryinwardpatients a
                            JOIN doctor_assignpatient da
                              ON a.noofinpat = da.noofinpat
                             AND da.ID = v_id
                             AND da.valid = 1
                          
                          UNION
                          --Modified By wwj 2011-09-02
                          SELECT b.*
                            FROM tmp_queryinwardpatients b
                           where zyysdm = v_id
                             and noofinpat not in
                                 (select noofinpat
                                    from Doctor_AssignPatient
                                   where valid = '1')) c
                   ORDER BY c.bedid --���ҵĻ��ߡ����մ�λ������
                  ) d;
      END;
    ELSIF v_querytype = 2 THEN
      --δ����ҽ���Ļ���
      BEGIN
        OPEN o_result FOR
          SELECT *
            FROM tmp_queryinwardpatients a
           WHERE a.noofinpat IS NOT NULL
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient d
                   WHERE d.valid = 1
                     AND d.noofinpat = a.noofinpat)
           ORDER BY a.bedid;
      END;
    ELSIF v_querytype = 3 THEN
      --����
      BEGIN
        IF v_querybed = 'N' THEN
          --�������մ�
          BEGIN
            OPEN o_result FOR
              SELECT *
                FROM tmp_QueryInwardPatients a
               WHERE a.NoOfInpat IS NOT NULL
               ORDER BY a.bedid;
          END;
        END IF;
      
        IF v_querybed = 'Y' THEN
          --�����մ�
          BEGIN
            OPEN o_result FOR
              SELECT * FROM tmp_QueryInwardPatients a ORDER BY a.bedid;
          END;
        END IF;
      END;
    END IF;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_queryownmanagerpat(v_querytype INT,
                                   v_userid    VARCHAR,
                                   v_deptid    VARCHAR,
                                   v_wardid    VARCHAR,
                                   o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ������ȡ���没���б�
     ����˵��
     �������
     v_QueryType int     ��ѯ���
     v_UserID varchar(8) �û�ID
     v_DeptID varchar(6)     ���Ҵ���
     v_WardId varchar(6)     ��������
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
    
     �޸ļ�¼
    �����������
    **********/
    v_sql VARCHAR(3000);
  BEGIN
    v_sql := 'truncate table tmp_QueryOwnManagerPat ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_querytype = 0 THEN
      --��ó��ֹ���Ĳ���
      BEGIN
        v_sql := 'select
    a.NoOfInpat  NoOfInpat--��ҳ���
  , a.PatNoOfHis PatNoOfHis --HIS��ҳ���
  , a.PatID PatID --סԺ��
  , a.Name PatName --����
  , a.SexID Sex  --�����Ա�
  , b.Name SexName  --�����Ա�����
  , a.AgeStr AgeStr  --����
  , rtrim(a.CriticalLevel) wzjb --Σ�ؼ���
  , a.Status arzt --����״̬
  , c.Name wzjbmc --Σ�ؼ�������
  , substr(a.AdmitDate,1,16) AdmitDate  --��Ժ����
  , d.ICD ryzd --��Ժ���
  , d.Name zdmc --�������
  --, e.Note pzlx --ƾ֤����
  , dd1.NAME pzlx --�������
  , f.DeptID ksdm
from InPatient a
left join Dictionary_detail b  on b.DetailID = a.SexID and b.CategoryID = ''3''
left join Dictionary_detail c  on c.DetailID = a.CriticalLevel and c.CategoryID = ''53''
left join Diagnosis d  on d.ICD = a.AdmitDiagnosis
--left join MedicareInfo e  ON a.VouchersCode = e.ID
LEFT JOIN Dictionary_detail dd1 ON dd1.categoryid = ''1'' AND a.payid = dd1.detailid
left join Bed f   on f.NoOfInpat = a.NoOfInpat and f.PatNoOfHis = a.PatNoOfHis and f.InBed = 1301
where a.Status in (1501,1504,1505,1506,1507)
and  not exists (select 1 from Doctor_AssignPatient da where da.NoOfInpat=a.NoOfInpat and ID=' ||
                 v_userid || ' and Valid=1)
and f.DeptID=''' || v_deptid || ''' and  f.WardId=''' ||
                 v_wardid || '''
';
      
        --exec sp_executesql v_sql
        OPEN o_result FOR v_sql;
      END;
    END IF;
  
    IF v_querytype = 1 THEN
      --��÷ֹܲ���
      BEGIN
        INSERT INTO doctor_assignpatient
          SELECT v_userid, noofinpat, 1, SYSDATE, v_userid, 1
            FROM inpatient a
           WHERE resident = v_userid
             AND status IN (1501, 1504, 1505, 1506, 1507)
             AND NOT EXISTS (SELECT 1
                    FROM doctor_assignpatient da
                   WHERE da.noofinpat = a.noofinpat
                     AND ID = v_userid
                     AND valid = 1);
      
        INSERT INTO tmp_queryownmanagerpat
          SELECT a.noofinpat  noofinpat, --��ҳ���
                 a.patnoofhis patnoofhis,
                 --HIS��ҳ���
                 a.patid    patid, --סԺ��
                 a.NAME     patname, --����
                 a.sexid    sex, --�����Ա�
                 b.NAME     sexname, --�����Ա�����
                 a.agestr   agestr, --����
                 a.status   arzt, --����״̬
                 ce.NAME    arztname, --����״̬name
                 c.NAME     wzjbmc, --Σ�ؼ�������
                 c.detailid wzjb,
                 --Σ�ؼ������
                 SUBSTR(a.admitdate, 1, 16) admitdate, --��Ժ����
                 d.icd ryzd,
                 --��Ժ���
                 d.NAME zdmc, --�������
                 --e.note pzlx, --ƾ֤����
                 dd1.NAME pzlx, --�������
                 g.NAME   ksmc, --��������
                 h.NAME   bqmc,
                 --��������
                 a.outbed bedid, --��Ժ��λ��
                 (SELECT CASE
                           WHEN COUNT(qc.foulstate) > 0 THEN
                            '��'
                           ELSE
                            '��'
                         END
                    FROM qcrecord qc
                   WHERE qc.noofinpat = da.noofinpat
                     AND qc.valid = 1
                     AND qc.foulstate = 1) AS iswarn --�Ƿ�Υ��
                ,
                 '����д' recordinfo --���޲���
            FROM doctor_assignpatient da
            JOIN inpatient a
              ON da.noofinpat = a.noofinpat
             AND valid = 1
             AND da.ID = v_userid
            LEFT JOIN categorydetail ce
              ON a.status = ce.ID
             AND ce.categoryid = '15'
            LEFT JOIN dictionary_detail b
              ON b.detailid = a.sexid
             AND b.categoryid = '3'
            LEFT JOIN dictionary_detail c
              ON c.detailid = a.criticallevel
             AND c.categoryid = '53'
            LEFT JOIN diagnosis d
              ON d.icd = a.admitdiagnosis
          --LEFT JOIN medicareinfo e ON a.voucherscode = e.ID
            LEFT JOIN Dictionary_detail dd1
              ON dd1.categoryid = '1'
             AND a.payid = dd1.detailid
            LEFT JOIN bed f
              ON f.noofinpat = a.noofinpat
             AND f.patnoofhis = a.patnoofhis
             AND f.inbed = 1301
            LEFT JOIN department g
              ON f.deptid = g.ID
            LEFT JOIN ward h
              ON f.wardid = g.ID
           WHERE a.status IN (1501, 1504, 1505, 1506, 1507);
      
        -- ��鲡���Ƿ��в���
        UPDATE tmp_queryownmanagerpat a
           SET recordinfo = '�޲���'
         WHERE noofinpat IS NOT NULL
           AND NOT EXISTS
         (SELECT 1 FROM recorddetail b WHERE a.noofinpat = b.noofinpat);
      
        OPEN o_result FOR
          SELECT * FROM tmp_queryownmanagerpat order by bedid;
        --ORDER BY TO_CHAR(bedid, '0000');
      END;
    END IF;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_queryqcgrade(v_noofinpat INT,
                             v_operuser  VARCHAR,
                             o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��������
     ����˵��
     �������
      v_NoOfInpat varchar(40)--��ҳ���,
      v_OperUser varchar(6) --������
     �������
     �����������
    ��������ͳ�����ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryQCGrade  9,'00'
     �޸ļ�¼
    **********/
    v_doctorid VARCHAR(6);
    v_sql      VARCHAR2(400);
  BEGIN
    v_sql := 'truncate table tmp_QueryQCGrade ';
  
    EXECUTE IMMEDIATE v_sql;
  
    --����������Ŀ(INSERT �����ֱ�)
    INSERT INTO tmp_queryqcgrade
      SELECT qct.typecode,
             qct.typename,
             qct.typeinstruction,
             qct.typecategory,
             qct.typeorder,
             qct.typememo,
             qci.itemcode,
             qci.itemname,
             qci.iteminstruction, --��Ŀ˵��
             qci.itemdefaultscore,
             qci.itemdefaulttarget,
             --ָ���׼��ָ���
             '00' ower,
             'YIDAN' owername,
             qci.itemtargetstandard,
             qci.itemstandardscore,
             qci.itemorder,
             qci.itemmemo
        FROM qcscoretype qct
        JOIN qcscoreitem qci
          ON qct.typecode = qci.typecode
         AND qct.valide = 1
         AND qci.valide = 1
         AND NOT EXISTS
       (SELECT 1
                FROM qcgrade
               WHERE qcgrade.itemcode = itemcode
                 AND qcgrade.noofinpat = v_noofinpat)
       ORDER BY qct.typecode, qci.itemorder;
  
    SELECT OPERATOR
      INTO v_doctorid
      FROM medicalrecord
     WHERE noofinpat = v_noofinpat
       AND ROWNUM < 1;
  
    INSERT INTO qcgrade
      (noofinpat, doctor_id, typecode, itemcode, create_time, create_user)
      SELECT v_noofinpat,
             v_doctorid,
             tmp_queryqcgrade.typecode,
             tmp_queryqcgrade.itemcode,
             SYSDATE,
             v_operuser
        FROM tmp_queryqcgrade;
  
    --����������Ŀ
    OPEN o_result FOR
      SELECT qcg.ID,
             qcg.noofinpat,
             qct.typecode,
             qct.typename,
             --���ִ���code&name
             qci.itemcode,
             qci.itemname, --��Ŀ����code&name
             qci.iteminstruction, --��Ŀ˵��
             qcg.doctor_id       AS doctor_id, --ҽʦ����
             users.NAME          AS doctorname,
             --ҽʦ����
             qci.itemdefaultscore, --��׼��
             qcg.grade, --����
             qci.itemtargetstandard,
             qci.itemstandardscore, --ָ���׼��ָ���
             qci.itemorder,
             qci.itemmemo --����˵��
        FROM qcgrade qcg
        JOIN qcscoretype qct
          ON qcg.typecode = qct.typecode
         AND qct.valide = 1
        JOIN qcscoreitem qci
          ON qcg.typecode = qci.typecode
         AND qcg.itemcode = qci.itemcode
         AND qci.valide = 1
        LEFT JOIN users
          ON qcg.doctor_id = users.ID
         AND users.valid = 1
       WHERE qcg.noofinpat = v_noofinpat
       ORDER BY qcg.typecode, qci.itemorder;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_queryquitpatientnodoctor(v_wardid    VARCHAR,
                                         v_deptids   VARCHAR,
                                         v_timefrom  VARCHAR,
                                         v_timeto    VARCHAR,
                                         v_PatientSN varchar default '', --������
                                         v_Name      varchar default '', --��������
                                         v_querytype INT DEFAULT 0,
                                         o_result    OUT empcurtyp) AS
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��
     ����
     ��Ȩ
     ����  ��ȡ��Ӧ�����ĳ�Ժ����
     ����˵��
     �������
      v_Wardid   varchar(6)   --��������
      v_Deptids varchar(255)  --���Ҵ��뼯��(����: '����1','����2')
        v_TimeFrom varchar(24),--��ʼʱ��
        v_TimeTo varchar(24),--����ʱ��
        v_QueryType int = 0,--����
     �������
     �����������
    �ڲ����Ĳ������ݼ�
    
     ���õ�sp
     ����ʵ��
     exec usp_QueryQuitPatientNoDoctor  '2911', '3202' ,'2011-05-09','2011-05-09',1
     �޸ļ�¼
    �����������
    **********/
    v_wardid_in VARCHAR(40);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(400);
  BEGIN
    v_sql := 'truncate table tmp_QueryQuitPatientNoDoctor ';
  
    EXECUTE IMMEDIATE v_sql;
  
    v_sql := 'truncate table tmp_QueryQuitPatientNo_extraop ';
  
    EXECUTE IMMEDIATE v_sql;
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    -- ���ҳ����˼�¼��Ȼ���ȡ���˵ĸ�����Ϣ����������Ժ��ת�Ƶȣ�
    INSERT INTO tmp_queryquitpatientnodoctor
      SELECT b.noofinpat  noofinpat, --��ҳ���
             b.patnoofhis patnoofhis, --HIS��ҳ���
             b.patid      patid,
             --סԺ��
             b.NAME   patname, --����
             b.sexid  sex, --�����Ա�
             dd2.name sexname, --�����Ա�����
             b.agestr agestr,
             --����
             b.py py, --ƴ��
             b.wb wb, --���
             b.status brzt, --����״̬
             e.NAME brztname, --����״̬����
             RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
             i.NAME wzjbmc, --Σ�ؼ�������
             /*
             (CASE
               WHEN patid IS NULL THEN
                ''
               ELSE
                'һ������'
             END) hljb, --������
             */
             case b.attendlevel
               when '1' then
                'һ������'
               when '2' then
                '��������'
               when '3' then
                '��������'
               when '4' then
                '�ؼ�����'
               else
                'һ������'
             end hljb, --������
             
             b.isbaby yebz, --Ӥ����־
             (CASE
               WHEN b.isbaby = '0' THEN
                '��'
               WHEN b.isbaby IS NULL THEN
                ''
               ELSE
                '��'
             END) yebzname,
             b.outhosward bqdm, --��������
             b.outhosdept ksdm, --���Ҵ���
             b.outbed bedid, --��λ����
             dg.NAME ksmc, --��������
             wh.NAME bqmc, --��������
             b.admitward ybqdm, --ԭ��������
             b.admitdept yksdm, --ԭ���Ҵ���
             b.admitbed ycwdm, --ԭ��λ����
             
             --b.InBed InBed  --ռ����־
             --                ,
             --                a.Borrow jcbz --�贲��־
             --                ,
             --                a.SexInfo cwlx --��λ����
             --                ,
             SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
             --SUBSTR(b.outhosdate, 1, 16) outhosdate, --��Ժ����ad by ywk 2013��7��26�� 15:27:18
             f.NAME     ryzd, --��Ժ���
             f.NAME     zdmc, --�������
             b.resident zyysdm, --סԺҽ������
             c.NAME     zyys, --סԺҽ��
             c.NAME     cwys,
             --��λҽ��
             g.NAME zzys, --����ҽʦ
             h.NAME zrys, --����ҽʦ
             --                b.Valid yxjl --��Ч��¼
             --                ,
             --me.pzlx pzlx, --ƾ֤����
             dd1.NAME pzlx, --�������
             TO_CHAR(CASE
                       WHEN INSTR(v_deptids, b.outhosdept) = 0 AND
                            (b.noofinpat IS NULL) THEN
                        '������������'
                       ELSE
                        ''
                     END) extra, --������Ϣ
             b.memo memo, --��ע
             (CASE b.cpstatus
               WHEN 0 THEN
                'δ����'
               WHEN 1 THEN
                'ִ����'
               WHEN 2 THEN
                '�˳�'
             END) cpstatus,
             CASE
               WHEN b.noofinpat IS NULL THEN
                ''
               ELSE
                '����д'
             END recordinfo,
             100 ye, --���
             (SELECT CASE
                       WHEN COUNT(qc.foulstate) > 0 THEN
                        '��'
                       ELSE
                        '��'
                     END
                FROM qcrecord qc
               WHERE qc.noofinpat = b.noofinpat
                 AND qc.valid = 1
                 AND qc.foulstate = 1) AS iswarn --�Ƿ�Υ��
        FROM inpatient b
        LEFT JOIN categorydetail e
          ON b.status = e.ID
         AND e.categoryid = '15'
      --LEFT JOIN medicareinfo me ON b.voucherscode = me.ID
        LEFT JOIN Dictionary_detail dd1
          ON dd1.categoryid = '1'
         AND b.payid = dd1.detailid
        LEFT JOIN dictionary_detail dd2
          on dd2.categoryid = '3'
         AND b.sexid = dd2.detailid
        LEFT JOIN department dg
          ON b.outhosdept = dg.ID
      /*LEFT JOIN ward wh ON b.outhosward = dg.ID*/
        LEFT JOIN ward wh
          ON wh.id = dg.id --�������ظ����ݼ� edit by ywk 2012��3��7��10:14:15
      --   left join YY_SFXXMK e  on b.AttendLevel = e.sfxmdm
        LEFT JOIN diagnosis f
          ON f.icd = b.admitdiagnosis
        LEFT JOIN users c
          ON c.ID = b.resident
        LEFT JOIN dictionary_detail i
          ON i.detailid = b.criticallevel
         AND i.categoryid = '53'
        LEFT JOIN users g
          ON g.ID = b.attend
        LEFT JOIN users h
          ON h.ID = b.chief
        LEFT JOIN dictionary_detail j
          ON j.detailid = b.sexid
         AND j.categoryid = '3'
       WHERE (b.OutHosDept = v_deptids or b.admitdept=v_deptids)
         AND (b.OutHosWard = v_wardid or b.admitward=v_wardid)--add by ywk  ��Ժ��Ժ���ڴ˿���Ӧ�ÿ��Բ����
            /*����ҽԺ����Ҫ��ԭ������Ժ���ڸ�Ϊ��Ժ���� add by ywk 2012��11��21��13:34:11 */
            /*  AND (v_timefrom IS NULL OR
                TO_CHAR(TO_DATE(b.admitdate, 'yyyy-mm-dd hh24:mi:ss'),
                         'yyyy-mm-dd') >= SUBSTR(v_timefrom, 1, 10))
            AND (v_timeto IS NULL OR
                TO_CHAR(TO_DATE(b.admitdate, 'yyyy-mm-dd hh24:mi:ss'),
                         'yyyy-mm-dd') <= SUBSTR(v_timeto, 1, 10))*/
         AND (v_timefrom IS NULL OR
             TO_CHAR(TO_DATE(b.outhosdate, 'yyyy-mm-dd hh24:mi:ss'),
                      'yyyy-mm-dd') >= SUBSTR(v_timefrom, 1, 10))
         AND (v_timeto IS NULL OR
             TO_CHAR(TO_DATE(b.outhosdate, 'yyyy-mm-dd hh24:mi:ss'),
                      'yyyy-mm-dd') <= SUBSTR(v_timeto, 1, 10))
            
         AND b.patid like '%' || v_PatientSN || '%'
         AND b.name like '%' || v_Name || '%';
    /*
      v_dqrq := TO_CHAR(SYSDATE, 'yyyy-mm-dd');
      v_ksrq := TO_CHAR(SYSDATE - 3, 'yyyy-mm-dd');
      v_jsrq := TO_CHAR(SYSDATE + 2, 'yyyy-mm-dd');
      v_now  := TO_CHAR(SYSDATE, 'yyyy-mm-dd HH24:mi:ss');
    
      -- ���������Ϣ:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢����ʱ���ڵ�ǰ����-3��+1֮��
      INSERT INTO tmp_queryquitpatientno_extraop
        SELECT a.noofinpat,
               datediff('dd', v_now, SUBSTR(a.entrust, 1, 19)) diff
          FROM temp_order a, tmp_queryquitpatientnodoctor b
         WHERE a.noofinpat = b.noofinpat
           AND a.startdate BETWEEN v_ksrq AND v_jsrq
           AND a.ordertype = 3105
           AND a.orderstatus IN (3201, 3202);
    
      UPDATE tmp_queryquitpatientnodoctor a
         SET extra = (SELECT a.extra || (CASE b.diff
                               WHEN 1 THEN
                                '��������'
                               WHEN 0 THEN
                                '��������'
                               WHEN -1 THEN
                                '����1��'
                               WHEN -2 THEN
                                '����2��'
                               WHEN -3 THEN
                                '����3��'
                               ELSE
                                ''
                             END) || ' '
                        FROM tmp_queryquitpatientno_extraop b
                       WHERE a.noofinpat = b.noofinpat
                         AND ROWNUM < 1);
    
      -- ����Ժҽ��:��ʱҽ��������˺���ִ�еģ���ʼʱ�䡢��Ժʱ���ڵ�ǰ����0ʱ֮��
      v_sql := 'truncate table tmp_QueryQuitPatientNo_extraop ';
    
      EXECUTE IMMEDIATE v_sql;
    
      INSERT INTO tmp_queryquitpatientno_extraop
        SELECT a.noofinpat,
               datediff('dd', v_now, SUBSTR(a.entrust, 1, 10)) diff
          FROM temp_order a, tmp_queryquitpatientnodoctor b
         WHERE a.noofinpat = b.noofinpat
           AND a.startdate >= v_dqrq
           AND a.ordertype = 3113
           AND a.orderstatus IN (3201, 3202);
    
      UPDATE tmp_queryquitpatientnodoctor a
         SET extra = (SELECT a.extra || (CASE b.diff
                               WHEN 0 THEN
                                '���ճ�Ժ'
                               WHEN 1 THEN
                                '���ճ�Ժ'
                               ELSE
                                TO_CHAR(b.diff) || '����Ժ'
                             END) || ' '
                        FROM tmp_queryquitpatientno_extraop b
                       WHERE a.noofinpat = b.noofinpat
                         AND ROWNUM < 1);
    
      -- ���ת��ҽ��:���ݲ���״̬
      UPDATE tmp_queryquitpatientnodoctor a
         SET extra = (SELECT a.extra || b.NAME || ' '
                        FROM categorydetail b
                       WHERE b.categoryid = a.brzt
                         AND ROWNUM < 1)
       WHERE a.brzt IN (1505, 1506, 1507);
    */
    -- ��鲡���Ƿ��в���
    UPDATE tmp_queryquitpatientnodoctor a
       SET extra = a.extra || '�޲���', recordinfo = '�޲���'
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS
     (SELECT 1 FROM recorddetail b WHERE a.noofinpat = b.noofinpat);
  
    IF v_querytype = 0 THEN
      --��Ժĩ����ҽ��
      BEGIN
        OPEN o_result FOR
          SELECT a.*, inp.inwarddate
            FROM tmp_queryquitpatientnodoctor a
            left join inpatient inp
              on a.noofinpat = inp.noofinpat
             and a.zyysdm IS NULL
           order by a.bedid;
      END;
    ELSE
      --ȫ������
      BEGIN
        OPEN o_result FOR
          SELECT a.*, substr(inp.inwarddate,1,16) as inwarddate,substr(inp.outhosdate,1,16) as outhosdate
            FROM tmp_queryquitpatientnodoctor a
            left join inpatient inp
              on a.noofinpat = inp.noofinpat
             and a.brzt in ('1502', '1503')
              where( inp.islock<>4701 or inp.islock  is null)
           order by bedid;
      END;
    END IF;
  END;

  /****************************************************************************************************/
  PROCEDURE usp_emr_calcage(v_csrq VARCHAR,
                            v_dqrq VARCHAR,
                            v_sjnl OUT INT,
                            v_xsnl OUT VARCHAR) AS
    v_date3    VARCHAR(24);
    v_date4    VARCHAR(24);
    v_yy       INT;
    v_mm       INT;
    v_dd       INT;
    v_hh       INT;
    v_mi       INT;
    v_tempdate VARCHAR(24);
    v_decmm    INT;
    v_decyy    INT;
    v_dechh    INT;
    v_decmi    INT;
    /**********
     �汾��  1.0.0.0.0
     ����ʱ��  2010.8.10
     ����  �ܻ�
     ��Ȩ
     ����  ���㲡������
     ����˵��
      ���ݲ���ͳ�Ƶ�Ҫ�󣬼��㲡�˵ľ�ȷ�����Լ�������ʾ�����䡣
      ����Ӥ�׶����ߣ��������ֻ����ͳ�ļ��꣬���������ٴ���ͳ�Ʒ�������Ҫ��
     �������
       v_csrq varchar(24)Time  --��������
      ,v_dqrq varchar(24)Time  --��ǰ����(�����������ڵ�����)
      ,v_sjnl int = null output  --������׼ȷ����(��ʽΪYYYYMMDD,YYYY��ʾ�����ֵ,MM��ʾ�µ���ֵ,DD��ʾ�յ���ֵ)
      ,v_xsnl varchar(16) = null output  --��ʾ����(����ʵ�������ʾ������,��XXX��,XX��XX��,XX��)
     �������
     �޸�
    
        ���ױ���Ժ�����������󡪡�����ʱ����2Сʱ���ڵĲ��ˣ����侫ȷ�����ӣ�����������48Сʱ���ڵĲ��ˣ����侫ȷ��Сʱ��
     �����������
     ���õ�sp
     ����ʵ��
      declare v_csrq varchar(24), v_dqrq varchar(24), v_sjnl int, v_xsnl varchar(16)
      select v_csrq = '1978-08-01', v_dqrq = '1978-09-01'
      exec usp_EmrCalcAge v_csrq, v_dqrq, v_sjnl output, v_xsnl output
      select v_sjnl, v_xsnl
    **********/
  BEGIN
    --select v_date3 = substring(v_csrq, 1, 10)
    --  , v_date4 = substring(v_dqrq, 1, 10)
    v_date3 := SUBSTR(v_csrq, 1, 16);
    v_date4 := SUBSTR(v_dqrq, 1, 16);
  
    IF (isdate(v_date3) <> 1) OR (isdate(v_date4) <> 1) OR
       (v_date3 > v_date4) THEN
      BEGIN
        v_sjnl := 0;
        v_xsnl := '';
        RETURN;
      END;
    END IF;
  
    v_decmm := 0;
    v_decyy := 0;
    v_dechh := 0;
    v_decmi := 0;
  
    -- ����
    --TO_char(to_timestamp('2013-05-01 11:31:11','yyyy-mm-dd HH24:mi:ss'),'mi')
    -- IF TO_CHAR(v_date4, 'mi') >= TO_CHAR(v_date3, 'mi') THEN
    --������ʱ���������  add by ywk 2013��6��26�� 11:35:14
    IF TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'mi') >=
       TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'mi') THEN
      v_mi := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'mi') -
              TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'mi');
    ELSE
      BEGIN
        v_decmi := 1;
        v_mi    := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'),
                           'mi') + 60 -
                   TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'),
                           'mi');
      END;
    END IF;
  
    --Сʱ
    IF (TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'hh24') -
       v_decmi) >=
       TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'hh24') THEN
      v_hh := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'),
                      'hh24') - v_decmi -
              TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'),
                      'hh24');
    ELSE
      BEGIN
        v_dechh := 1;
        v_hh    := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'),
                           'hh24') - v_decmi + 24 -
                   TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'),
                           'hh24');
      END;
    END IF;
  
    --��
    IF (TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'dd') -
       v_dechh) >=
       TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'dd') THEN
      v_dd := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'dd') -
              v_dechh -
              TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'dd');
    ELSE
      BEGIN
        v_decmm := 1;
      
        v_dd := TO_CHAR(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'),
                        'dd') - v_dechh +
                datediff('dd',
                         v_date3,
                         to_char(ADD_MONTHS(to_date(v_date3,
                                                    'yyyy-mm-dd HH24:mi:ss'),
                                            1),
                                 'yyyy-mm-dd HH24:mi:ss')) -
                TO_CHAR(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'),
                        'dd');
        /*v_decmm := 1;
        v_dd := TO_char(to_timestamp(v_date4,'yyyy-mm-dd HH24:mi:ss'),'dd') 
        - v_dechh +
                   datediff('dd', v_date3
                   , ADD_MONTHS(to_date(v_date3,'yyyy-mm-dd HH24:mi:ss'), 1)) -
                   TO_char(to_timestamp(v_date3,'yyyy-mm-dd HH24:mi:ss'),'dd');*/
      END;
    END IF;
  
    --��
    IF (TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'mm') -
       v_decmm) >=
       TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'mm') THEN
      v_mm := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'), 'mm') -
              v_decmm -
              TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'), 'mm');
    ELSE
      BEGIN
        v_decyy := 1;
        v_mm    := TO_char(to_timestamp(v_date4, 'yyyy-mm-dd HH24:mi:ss'),
                           'mm') - v_decmm + 12 -
                   TO_char(to_timestamp(v_date3, 'yyyy-mm-dd HH24:mi:ss'),
                           'mm');
      END;
    END IF;
  
    --��
    v_yy   := datediff('yy', v_date3, v_date4) - v_decyy;
    v_sjnl := v_yy * 10000 + v_mm * 100 + v_dd;
  
    IF v_yy > 1 THEN
      -- 2�꼰���ϵ����
      v_xsnl := TO_CHAR(v_yy) || '��';
    ELSIF v_yy = 1 THEN
      v_xsnl := TO_CHAR(v_yy * 12 + v_mm) || '����';
    ELSIF (v_mm > 1) OR ((v_mm = 1) AND (v_dd = 0)) THEN
      v_xsnl := TO_CHAR(v_mm) || '����';
    ELSIF v_mm = 1 THEN
      v_xsnl := TO_CHAR(v_mm) || '����' || TO_CHAR(v_dd) || '��';
    ELSIF v_dd > 1 THEN
      v_xsnl := TO_CHAR(v_dd) || '��';
    ELSIF v_dd = 1 THEN
      v_xsnl := TO_CHAR(v_dd * 24 + v_hh) || '��Сʱ';
    ELSIF v_hh > 1 THEN
      v_xsnl := TO_CHAR(v_hh) || '��Сʱ';
    ELSIF v_hh = 1 THEN
      v_xsnl := TO_CHAR(v_hh * 60 + v_mi) || '����';
    ELSE
      v_xsnl := TO_CHAR(v_mi) || '����';
    END IF;
  
    RETURN;
  END;

  /***********************************************************************************/
  procedure usp_EMR_GetAge(v_csrq varchar,
                           v_dqrq varchar,
                           v_sjnl out int,
                           v_xsnl out varchar) as
    d_csrq date;
    d_dqrq date;
  
  begin
    d_csrq := to_date(substr(v_csrq, 1, 10), 'yyyy-mm-dd');
    d_dqrq := to_date(substr(v_dqrq, 1, 10), 'yyyy-mm-dd');
  
    v_sjnl := TRUNC(Months_between(d_dqrq, d_csrq) / 12);
    v_xsnl := v_sjnl || '��' /*||
                                                                    (TRUNC(d_dqrq) - ADD_MONTHS(d_csrq, v_sjnl * 12)) || '��'*/
     ;
  
  end usp_EMR_GetAge;

  --ҽʦ����վ�õ���Ժ����
  PROCEDURE usp_queryinwardpatientsout(v_wardid    VARCHAR,
                                       v_deptids   VARCHAR,
                                       v_id        VARCHAR DEFAULT '',
                                       v_querytype INT DEFAULT 0,
                                       o_result    OUT empcurtyp) AS
    v_wardid_in VARCHAR(6);
    v_dqrq      VARCHAR(10);
    v_ksrq      VARCHAR(10);
    v_jsrq      VARCHAR(10);
    v_now       VARCHAR(24);
    v_sql       VARCHAR(4000);
  BEGIN
  
    IF v_wardid IS NULL THEN
      v_wardid_in := '';
    END IF;
  
    v_sql := 'truncate table tmp_QueryInwardPatients ';
  
    EXECUTE IMMEDIATE v_sql;
  
    INSERT INTO tmp_queryinwardpatients
      SELECT b.noofinpat noofinpat, --��ҳ���
             b.patnoofhis patnoofhis, --HIS��ҳ���
             b.patid patid, --סԺ��
             b.name patname, --����
             b.sexid sex, --�����Ա�
             j.name sexname, --�����Ա�����
             b.agestr agestr, --����
             b.py py, --ƴ��
             b.wb wb, --���
             b.status brzt, --����״̬
             e.name brztname, --����״̬����
             RTRIM(b.criticallevel) wzjb, --Σ�ؼ���
             i.name wzjbmc, --Σ�ؼ�������
             --cd.name hljb,
             
             --CASE WHEN b.attendlevel IS NULL THEN 6105 ELSE to_number(b.attendlevel)
             --END attendlevel, --������
             
             case b.attendlevel
               when '1' then
                'һ������'
               when '2' then
                '��������'
               when '3' then
                '��������'
               when '4' then
                '�ؼ�����'
               else
                'һ������'
             end hljb, --������
             
             case b.attendlevel
               when '1' then
                '6101'
               when '2' then
                '6102'
               when '3' then
                '6103'
               when '4' then
                '6104'
               else
                '6101'
             end attendlevel, --������
             b.isbaby yebz, --Ӥ����־
             CASE
               WHEN b.isbaby = '0' THEN
                '��'
               WHEN b.isbaby IS NULL THEN
                ''
               ELSE
                '��'
             END yebzname,
             a.wardid bqdm, --��������
             a.deptid ksdm, --���Ҵ���
             --a.ID bedid, --��λ����
             b.outbed bedid, --��λ����
             dg.NAME ksmc, --��������
             wh.NAME bqmc, --��������
             a.formerward ybqdm, --ԭ��������
             a.formerdeptid yksdm, --ԭ���Ҵ���
             a.formerdeptid ycwdm, --ԭ��λ����
             a.inbed inbed, --ռ����־
             a.borrow jcbz, --�贲��־
             a.sexinfo cwlx, --��λ����
             SUBSTR(b.admitdate, 1, 16) admitdate, --��Ժ����
             f.NAME ryzd, --��Ժ���
             f.NAME zdmc, --�������
             b.resident zyysdm, --סԺҽ������
             c.NAME zyys, --סԺҽ��
             c.NAME cwys, --��λҽ��
             g.NAME zzys, --����ҽʦ
             h.NAME zrys, --����ҽʦ
             a.valid yxjl, --��Ч��¼
             --me.pzlx pzlx, --ƾ֤����
             dd1.NAME pzlx, --�������
             CASE
               WHEN b.outhosdept = v_deptids AND b.outhosward = v_wardid THEN
                '������'
               ELSE
                '������������'
             END extra, --������Ϣ
             b.memo memo, --��ע
             CASE b.cpstatus
               WHEN 0 THEN
                'δ����'
               WHEN 1 THEN
                'ִ����'
               WHEN 2 THEN
                '�˳�'
             END cpstatus,
             CASE
               WHEN b.noofinpat IS NULL THEN
                ''
               ELSE
                '����д'
             END recordinfo,
             100 ye, --���
             (SELECT CASE
                       WHEN COUNT(qc.foulstate) > 0 THEN
                        '��'
                       ELSE
                        '��'
                     END
                FROM qcrecord qc
               WHERE qc.noofinpat = b.noofinpat
                 AND qc.valid = 1
                 AND qc.foulstate = 1) AS iswarn, --�Ƿ�Υ��
             b.incount as rycs --��Ժ����
        FROM inpatient b
        LEFT JOIN bed a
          ON a.noofinpat = b.noofinpat
         AND a.patnoofhis = b.patnoofhis
         AND a.inbed = 1301 --ռ��
        LEFT JOIN Dictionary_detail dd1
          ON dd1.categoryid = '1'
         AND b.payid = dd1.detailid
        LEFT JOIN categorydetail e
          ON b.status = e.ID
         AND e.categoryid = '15'
        LEFT JOIN department dg
          ON b.outhosdept = dg.ID
        LEFT JOIN ward wh
          ON b.outhosward = dg.ID
        LEFT JOIN diagnosis f
          ON f.icd = b.admitdiagnosis
        LEFT JOIN users c
          ON c.ID = b.resident
        LEFT JOIN dictionary_detail i
          ON i.detailid = b.criticallevel
         AND i.categoryid = '53'
        LEFT JOIN users g
          ON g.ID = b.attend
        LEFT JOIN users h
          ON h.ID = b.chief
        LEFT JOIN dictionary_detail j
          ON j.detailid = b.sexid
         AND j.categoryid = '3'
        LEFT JOIN categorydetail cd
          ON b.attendlevel = cd.ID
         AND cd.categoryid = '63'
       WHERE ((b.outhosdept = v_deptids AND b.outhosward = v_wardid) OR
             (v_deptids IN
             (SELECT bedinfo.newdept
                  FROM bedinfo
                 WHERE bedinfo.noofinpat = b.noofinpat) AND
             v_wardid IN
             (SELECT bedinfo.newward
                  FROM bedinfo
                 WHERE bedinfo.noofinpat = b.noofinpat)))
         AND a.valid is null;
  
    -- ��鲡���Ƿ��в���
    UPDATE tmp_queryinwardpatients a
       SET extra = extra || '�޲���', recordinfo = '�޲���'
     WHERE noofinpat IS NOT NULL
       AND NOT EXISTS (SELECT 1
              FROM recorddetail b
             WHERE a.noofinpat = b.noofinpat
               and b.valid = '1');
  
    OPEN o_result FOR
      SELECT *
        FROM tmp_queryinwardpatients a
       WHERE a.noofinpat IS NOT NULL
            /*AND NOT EXISTS (SELECT 1
             FROM doctor_assignpatient d
            WHERE d.valid = 1
              AND d.noofinpat = a.noofinpat)*/
         AND a.zyysdm = v_id
         AND ((
             /*
             EXISTS
             (
                 select 1 from recorddetail
                 where recorddetail.noofinpat = a.noofinpat and recorddetail.valid = '1'
                 having count(*) > 0
             )*/
              a.recordinfo != '�޲���' AND EXISTS
              (select 1
                  from recorddetail
                 where recorddetail.noofinpat = a.noofinpat
                   and recorddetail.valid = '1'
                   and recorddetail.islock = '4700' having count(*) > 0)) OR
             (
             /*
             EXISTS
             (
                 select 1 from recorddetail
                 where recorddetail.noofinpat = a.noofinpat and recorddetail.valid = '1'
                 having count(*) = 0
             )*/
              a.recordinfo = '�޲���'))
       ORDER BY a.bedid;
  END;

  ----ҽ������վ�л�û�����Ϣ��ȫ�����������ģ�
  PROCEDURE usp_GetMyConsultion(v_Deptids varchar, --���ű��
                                V_userid  varchar, --��¼�˱��
                                o_result  OUT empcurtyp) as
  BEGIN
    OPEN o_result FOR
    
      SELECT ip.NAME AS inpatientname,
             
             ip.py,
             ip.wb,
             TO_CHAR(TO_DATE(ca.consulttime, 'yyyy-mm-dd hh24:mi:ss'),
                     'yyyy-mm-dd hh24:mi:ss') AS consulttime,
             cd.NAME AS consultstatus,
             cd1.NAME AS urgencytype,
             ip.NAME || '_' || cd1.NAME || '_' || ca.consulttime AS inpatientinfo,
             ca.stateid,
             u.name as applyusername,
             aa.name as shouyaoren,
             ca.noofinpat,
             ca.modifieduser,
             ca.finishtime,
             ca.consulttypeid,
             ca.applyuser,
             ca.applydept,
             ca.audituserid,
             ca.applytime,
             (case
               when ca.ispay = '1' then
                '�ѽɷ�'
               when ca.ispay = '0' then
                'δ�ɷ�'
               when ca.ispay is null then
                'δ�ɷ�'
             end) MyPay,
             ca.ispay,
             ca.consultapplysn,
             hh.departmentcode, --��������
             hh.employeelevelid, --�����˼���
             hh.employeecode, --�����˹���
             -- hh.issignin         --�Ƿ�ǩ��
             ip.outhosdept,
             ip.outhosward
        FROM consultapply ca
        LEFT OUTER JOIN users u
          ON u.ID = ca.applyuser
        LEFT OUTER JOIN inpatient ip
          ON ip.noofinpat = ca.noofinpat
        LEFT OUTER JOIN categorydetail cd
          ON cd.categoryid = '67'
         AND ca.stateid = cd.ID
        left outer JOIN consultapplydepartment hh
          on ca.consultapplysn = hh.consultapplysn
         AND hh.valid = '1'
        left outer join users aa
          on aa.id = hh.employeecode
        LEFT OUTER JOIN categorydetail cd1
          ON cd1.categoryid = '66'
         AND cd1.ID = ca.urgencytypeid
       WHERE ca.valid = '1'
         AND ip.status IN
             ('1500', '1501', '1502', '1504', '1505', '1506', '1507')
         and (stateid = '6730' or stateid = '6770' or stateid = '6720' or
             stateid = '6750' or stateid = '6740' or
             stateid = '6741' and
             to_date(finishtime, 'yyyy-mm-dd hh24:mi:ss') >
             trunc(sysdate) - 3)
            /*and u.id=V_userid*/
         and (u.id = V_userid or ca.applydept = v_Deptids)
      union
      SELECT ip.NAME AS inpatientname,
             
             ip.py,
             ip.wb,
             TO_CHAR(TO_DATE(ca.consulttime, 'yyyy-mm-dd hh24:mi:ss'),
                     'yyyy-mm-dd hh24:mi:ss') AS consulttime,
             cd.NAME AS consultstatus,
             cd1.NAME AS urgencytype,
             ip.NAME || '_' || cd1.NAME || '_' || ca.consulttime AS inpatientinfo,
             ca.stateid,
             u.name as applyusername,
             aa.name as shouyaoren,
             ca.noofinpat,
             ca.modifieduser,
             ca.finishtime,
             ca.consulttypeid,
             ca.applyuser,
             ca.applydept,
             ca.audituserid,
             ca.applytime,
             (case
               when ca.ispay = '1' then
                '�ѽɷ�'
               when ca.ispay = '0' then
                'δ�ɷ�'
               when ca.ispay is null then
                'δ�ɷ�'
             end) MyPay,
             ca.ispay,
             ca.consultapplysn,
             hh.departmentcode, --��������
             hh.employeelevelid, --�����˼���
             hh.employeecode, --�����˹���
             --hh.issignin         --�Ƿ�ǩ��
             ip.outhosdept,
             ip.outhosward
        FROM consultapply ca
        LEFT OUTER JOIN users u
          ON u.ID = ca.applyuser
         AND u.valid = '1'
        LEFT OUTER JOIN inpatient ip
          ON ip.noofinpat = ca.noofinpat
        LEFT OUTER JOIN categorydetail cd
          ON cd.categoryid = '67'
         AND ca.stateid = cd.ID
        LEFT OUTER JOIN categorydetail cd1
          ON cd1.categoryid = '66'
         AND cd1.ID = ca.urgencytypeid
        left outer JOIN consultapplydepartment hh
          on ca.consultapplysn = hh.consultapplysn
         AND hh.valid = '1'
        left outer join users aa
          on aa.id = hh.employeecode
       WHERE ca.valid = '1'
         and hh.departmentcode = v_Deptids
         AND ip.status IN
             ('1500', '1501', '1502', '1504', '1505', '1506', '1507')
         and (stateid = '6730' or stateid = '6770' or stateid = '6720' or
             stateid = '6750' or stateid = '6740' or
             stateid = '6741' and
             to_date(finishtime, 'yyyy-mm-dd hh24:mi:ss') >
             trunc(sysdate) - 3)
         and (hh.employeecode = V_userid or (hh.employeecode is null)); -- or hh.employeelevelid=u.grade));
  
  END;

  PROCEDURE usp_editBabyinfo(v_Noofinpat  NUMERIC,
                             v_patnoofhis VARCHAR,
                             v_Noofclinic VARCHAR,
                             v_Noofrecord VARCHAR,
                             v_patid      VARCHAR,
                             v_Innerpix   VARCHAR,
                             v_outpix     VARCHAR,
                             
                             v_Name            VARCHAR,
                             v_py              VARCHAR,
                             v_wb              VARCHAR,
                             v_payid           VARCHAR,
                             v_ORIGIN          VARCHAR,
                             v_InCount         int DEFAULT 0,
                             v_sexid           VARCHAR,
                             v_Birth           VARCHAR,
                             v_Age             int DEFAULT 0,
                             v_AgeStr          VARCHAR,
                             v_IDNO            VARCHAR,
                             v_Marital         VARCHAR,
                             v_JobID           VARCHAR,
                             v_CSDProvinceID   VARCHAR,
                             v_CSDCityID       VARCHAR,
                             v_CSDDistrictID   VARCHAR,
                             v_NationID        VARCHAR,
                             v_NationalityID   VARCHAR,
                             v_JGProvinceID    VARCHAR,
                             v_JGCityID        VARCHAR,
                             v_Organization    VARCHAR,
                             v_OfficePlace     VARCHAR,
                             v_OfficeTEL       VARCHAR,
                             v_OfficePost      VARCHAR,
                             v_HKDZProvinceID  VARCHAR,
                             v_HKDZCityID      VARCHAR,
                             v_HKDZDistrictID  VARCHAR,
                             v_NATIVEPOST      VARCHAR,
                             v_NATIVETEL       VARCHAR,
                             v_NATIVEADDRESS   VARCHAR,
                             v_ADDRESS         VARCHAR,
                             v_ContactPerson   VARCHAR,
                             v_RelationshipID  VARCHAR,
                             v_ContactAddress  VARCHAR,
                             v_ContactTEL      VARCHAR,
                             v_CONTACTOFFICE   VARCHAR,
                             v_CONTACTPOST     VARCHAR,
                             v_OFFERER         VARCHAR,
                             v_SocialCare      VARCHAR,
                             v_INSURANCE       VARCHAR,
                             v_CARDNO          VARCHAR,
                             v_ADMITINFO       VARCHAR,
                             v_AdmitDeptID     VARCHAR,
                             v_AdmitWardID     VARCHAR,
                             v_ADMITBED        VARCHAR,
                             v_AdmitDate       VARCHAR,
                             v_INWARDDATE      VARCHAR,
                             v_ADMITDIAGNOSIS  VARCHAR,
                             v_OutWardDate     VARCHAR,
                             v_OutHosDeptID    VARCHAR,
                             v_OutHosWardID    VARCHAR,
                             v_OutBed          VARCHAR,
                             v_OUTHOSDATE      VARCHAR,
                             v_OUTDIAGNOSIS    VARCHAR,
                             v_TOTALDAYS       int DEFAULT 0,
                             v_CLINICDIAGNOSIS VARCHAR,
                             v_SOLARTERMS      VARCHAR,
                             v_ADMITWAY        VARCHAR,
                             v_OUTWAY          VARCHAR,
                             v_CLINICDOCTOR    VARCHAR,
                             v_RESIDENT        VARCHAR,
                             v_ATTEND          VARCHAR,
                             v_CHIEF           VARCHAR,
                             v_EDU             VARCHAR,
                             v_EDUC            int DEFAULT 0,
                             v_RELIGION        VARCHAR,
                             v_STATUS          int DEFAULT 0,
                             v_CRITICALLEVEL   VARCHAR,
                             v_ATTENDLEVEL     VARCHAR,
                             v_EMPHASIS        int DEFAULT 0,
                             v_ISBABY          int DEFAULT 0,
                             v_MOTHER          NUMERIC,
                             v_MEDICAREID      VARCHAR,
                             v_MEDICAREQUOTA   int DEFAULT 0,
                             v_VOUCHERSCODE    VARCHAR,
                             v_STYLE           VARCHAR,
                             v_OPERATOR        VARCHAR,
                             v_MEMO            VARCHAR,
                             v_CPSTATUS        int DEFAULT 0,
                             v_OUTWARDBED      int DEFAULT 0,
                             v_XZZProvinceID   VARCHAR,
                             v_XZZCityID       VARCHAR,
                             v_XZZDistrictID   VARCHAR,
                             v_XZZTEL          VARCHAR,
                             v_XZZPost         VARCHAR,
                             v_EditType        varchar) as
    m_babycount int;
  begin
    select count(*)
      into m_babycount
      from inpatient
     where mother = v_MOTHER;
    /* select nvl(v_ADMITDEPT, AdmitDept)
     into myadmitdept
     from inpatient
    where NoOfInpat = v_NOOFINPAT;*/
    --1��������2�Ǳ༭
    IF v_EditType = '1' THEN
      insert into inpatient
        (NOOFINPAT,
         PATNOOFHIS,
         NOOFCLINIC,
         NOOFRECORD,
         PATID,
         INNERPIX,
         OUTPIX,
         NAME,
         PY,
         WB,
         PAYID,
         ORIGIN,
         INCOUNT,
         SEXID,
         BIRTH,
         AGE,
         AGESTR,
         IDNO,
         MARITAL,
         JOBID,
         PROVINCEID,
         COUNTYID,
         NATIONID,
         NATIONALITYID,
         NATIVEPLACE_P,
         NATIVEPLACE_C,
         ORGANIZATION,
         OFFICEPLACE,
         OFFICETEL,
         OFFICEPOST,
         NATIVEADDRESS,
         NATIVETEL,
         NATIVEPOST,
         ADDRESS,
         CONTACTPERSON,
         RELATIONSHIP,
         CONTACTADDRESS,
         CONTACTOFFICE,
         CONTACTTEL,
         CONTACTPOST,
         OFFERER,
         SOCIALCARE,
         INSURANCE,
         CARDNO,
         ADMITINFO,
         ADMITDEPT,
         ADMITWARD,
         ADMITBED,
         ADMITDATE,
         INWARDDATE,
         ADMITDIAGNOSIS,
         OUTHOSDEPT,
         OUTHOSWARD,
         OUTBED,
         OUTWARDDATE,
         OUTHOSDATE,
         OUTDIAGNOSIS,
         TOTALDAYS,
         CLINICDIAGNOSIS,
         SOLARTERMS,
         ADMITWAY,
         OUTWAY,
         CLINICDOCTOR,
         RESIDENT,
         ATTEND,
         CHIEF,
         EDU,
         EDUC,
         RELIGION,
         STATUS,
         CRITICALLEVEL,
         ATTENDLEVEL,
         EMPHASIS,
         ISBABY,
         MOTHER,
         MEDICAREID,
         MEDICAREQUOTA,
         VOUCHERSCODE,
         STYLE,
         OPERATOR,
         MEMO,
         CPSTATUS,
         OUTWARDBED,
         DISTRICTID,
         XZZPROVICEID,
         XZZCITYID,
         XZZDISTRICTID,
         XZZTEL,
         HKDZPROVICEID,
         HKZDCITYID,
         HKZDDISTRICTID,
         XZZPOST
         -- babycount
         )
      values
        (SEQ_INPATIENT_ID.NEXTVAL,
         v_patnoofhis,
         v_Noofclinic,
         v_Noofrecord,
         v_patid,
         v_Innerpix,
         v_outpix,
         v_Name,
         v_py,
         v_wb,
         v_payid,
         v_ORIGIN,
         v_InCount,
         v_sexid,
         v_Birth,
         v_Age,
         v_AgeStr,
         v_IDNO,
         v_Marital,
         v_JobID,
         v_CSDProvinceID,
         v_CSDCityID,
         v_NationID,
         v_NationalityID,
         v_JGProvinceID,
         v_JGCityID,
         v_Organization,
         v_OfficePlace,
         v_OfficeTEL,
         v_OfficePost,
         v_NATIVEADDRESS,
         v_NATIVETEL,
         v_NATIVEPOST,
         v_ADDRESS,
         v_ContactPerson,
         v_RelationshipID,
         v_ContactAddress,
         v_CONTACTOFFICE,
         v_ContactTEL,
         v_CONTACTPOST,
         v_OFFERER,
         v_SocialCare,
         v_INSURANCE,
         v_CARDNO,
         v_ADMITINFO,
         v_AdmitDeptID,
         v_AdmitWardID,
         v_ADMITBED,
         v_AdmitDate,
         v_INWARDDATE,
         v_ADMITDIAGNOSIS,
         v_OutHosDeptID,
         v_OutHosWardID,
         v_OutBed,
         v_OutWardDate,
         v_OUTHOSDATE,
         v_OUTDIAGNOSIS,
         v_TOTALDAYS,
         v_CLINICDIAGNOSIS,
         v_SOLARTERMS,
         v_ADMITWAY,
         v_OUTWAY,
         v_CLINICDOCTOR,
         v_RESIDENT,
         v_ATTEND,
         v_CHIEF,
         v_EDU,
         v_EDUC,
         v_RELIGION,
         v_STATUS,
         v_CRITICALLEVEL,
         v_ATTENDLEVEL,
         v_EMPHASIS,
         v_ISBABY,
         v_MOTHER,
         v_MEDICAREID,
         v_MEDICAREQUOTA,
         v_VOUCHERSCODE,
         v_STYLE,
         v_OPERATOR,
         v_memo,
         v_CPSTATUS,
         v_OUTWARDBED,
         v_CSDDistrictID,
         v_XZZProvinceID,
         v_XZZCityID,
         v_XZZDistrictID,
         v_XZZTEL,
         v_HKDZProvinceID,
         v_HKDZCityID,
         v_HKDZDistrictID,
         v_XZZPost
         -- m_babycount+1
         );
      commit;
      update inpatient
         set babycount = m_babycount + 1
       where noofinpat = v_MOTHER;
      commit;
    ELSIF v_EditType = '2' THEN
      --�༭
      update inpatient
         set name   = v_Name,
             Age    = v_Age,
             birth  = v_Birth,
             sexid  = v_sexid,
             Agestr = v_AgeStr
       where noofinpat = v_Noofinpat;
      commit;
    elsif v_EditType = '3' then
      -- ɾ��
      delete inpatient where noofinpat = v_Noofinpat;
      commit;
      update inpatient
         set babycount = m_babycount - 1
       where noofinpat = v_MOTHER;
      commit;
    end if;
  end;
  ----�ʿس�Ժδ�鵵������ѯ
  -----Add by xlb 2013-06-04
  PROCEDURE usp_GetOutHosButNotLocks(v_deptId    VARCHAR2, ---���Ҵ���
                                     v_sex       VARCHAR2, --�Ա�
                                     v_dateBegin VARCHAR2, --��Ժ��ʼʱ��
                                     v_dateEnd   VARCHAR2, --��Ժ����ʱ��
                                     v_patName   VARCHAR2, ----��������
                                     v_PatId     VARCHAR2,
                                     o_result    OUT empcurtyp) as
    p_begindate VARCHAR2(50);
    p_EndDate   VARCHAR2(50);
  begin
    p_begindate := v_dateBegin || ' 00:00:00';
    p_EndDate   := v_dateEnd || ' 23:59:59';
    open o_result for
      select (case
               when count(b.ID) > 0 then
                '��д'
               else
                'δд'
             end) RECORDSTATE,
             '' QC,
             a.NoOfInpat AS NOOFINPAT,
             a.PatID as PATID,
             a.Name PATNAME,
             (case
               when a.SexID = 1 then
                '��'
               when a.SexID = 2 then
                'Ů'
               else
                'δ֪'
             end) as PATSEX,
             1 AS INHOSPITAL,
             dept.name DEPTNAME,
             diag.name PATDIAGNAME,
             a.admitdate INHOSPITALTIME,
             a.outhosdate OUTHOSPITALTIME,
             
             bas.iem_mainpage_no      as IEMNO,
             diagnosis.diagnosis_name as DIAGNOSISNAME,
             --wmsys.wm_concat(diagnosis.diagnosis_name) as DIAGNOSISNAME ,
             
             to_char(to_date(a.admitdate, 'yyyy-mm-dd HH24:mi:ss') + 1,
                     'yyyy-mm-dd') SUREDIAGTIME,
             users.name DOCNAME,
             a.AgeStr PATAGE,
             datediff('dd',
                      a.admitdate,
                      NVL(trim(a.outwarddate),
                          TO_CHAR(SYSDATE, 'yyyy-mm-dd'))) INCOUNT
      
        from INPATIENT                   a,
             RecordDetail                b,
             diagnosis                   diag,
             department                  dept,
             users                       users,
             iem_mainpage_basicinfo_2012 bas,
             iem_mainpage_diagnosis_2012 diagnosis
       where a.NOOFINPAT = b.NoOfInpat(+)
         and a.AdmitDiagnosis = diag.markid(+)
         and a.outhosdept = dept.id(+)
         and a.Resident = users.id(+)
            
         and a.NOOFINPAT = bas.NOOFINPAT(+)
         and bas.iem_mainpage_no = diagnosis.iem_mainpage_no(+)
         and diagnosis.diagnosis_type_id <> 13
            
         and a.status in (1502, 1503)
         and a.islock in (4700, 4702, 4703)
         and (a.OutHosDept = v_deptId or v_deptId = '' or v_deptId is null)
         AND (a.sexid = v_sex OR v_sex = '' OR v_sex IS NULL)
         and to_date(substr(nvl(trim(a.outhosdate), '1990-01-01 00:00:00'),
                            1,
                            19),
                     'yyyy-mm-dd hh24:mi:ss') >=
             to_date(p_begindate, 'yyyy-mm-dd hh24:mi:ss')
         and to_date(substr(nvl(trim(a.outhosdate), '1990-01-01 00:00:00'),
                            1,
                            19),
                     'yyyy-mm-dd hh24:mi:ss') <
             to_date(p_EndDate, 'yyyy-mm-dd hh24:mi:ss') --ԭ������p_begindate edit by ywk 
         and (a.Name = v_patName or v_patName = '' or v_patName is null)
         and (a.patid = v_PatId or v_PatId = '' or v_PatId is null)
       group by a.NoOfInpat,
                a.PatID,
                a.NAME,
                a.SEXID,
                A.AGESTR,
                a.OUTBED,
                a.AdmitDiagnosis,
                a.admitdate,
                a.outwarddate,
                diag.name,
                dept.name,
                a.outhosdate,
                bas.iem_mainpage_no,
                diagnosis.diagnosis_name,
                users.name;
  
  end;

END;
/
