create or replace package emr_commonnote IS
  TYPE empcurtype IS REF CURSOR;

  --ͨ������������ȡ����Ԫ
  PROCEDURE usp_GetDateElement(v_ElementId    VARCHAR2,
                               v_ElementName  VARCHAR2,
                               v_ElementClass VARCHAR2,
                               v_ElementPYM   VARCHAR2,
                               o_result       OUT empcurtype);

  --ͨ������������ȡ����Ԫ
  PROCEDURE usp_GetDateElementOne(v_ElementFlow VARCHAR2,
                                  o_result      OUT empcurtype);

  --ͨ������ԪID�����ǹ����ڸ�����Ԫ
  PROCEDURE usp_ValiDateElement(v_ElementId VARCHAR2,
                                o_result    OUT empcurtype);

  --��������Ԫ
  PROCEDURE usp_InsertElement(v_ElementFlow     VARCHAR2,
                              v_ElementId       VARCHAR2,
                              v_ElementName     VARCHAR2,
                              v_ElementType     VARCHAR2,
                              v_ElementForm     VARCHAR2,
                              v_ElementRange    VARCHAR2,
                              v_ElementDescribe VARCHAR2,
                              v_ElementClass    VARCHAR2,
                              v_IsDataElemet    VARCHAR2,
                              v_ElementPYM      VARCHAR2);
  --�޸�����Ԫ
  PROCEDURE usp_UpDateElement(v_ElementFlow     VARCHAR2,
                              v_ElementId       VARCHAR2,
                              v_ElementName     VARCHAR2,
                              v_ElementType     VARCHAR2,
                              v_ElementForm     VARCHAR2,
                              v_ElementRange    VARCHAR2,
                              v_ElementDescribe VARCHAR2,
                              v_ElementClass    VARCHAR2,
                              v_IsDataElemet    VARCHAR2,
                              v_ElementPYM      VARCHAR2,
                              v_Valid           VARCHAR2);

  --��ȡ�򵥰�ͨ�õ�
  PROCEDURE usp_GetSimpleCommonNote(v_CommonNoteName VARCHAR2,
                                    o_result         OUT empcurtype);

  --��ȡͨ�õ���Ӧ���ҺͲ���
  PROCEDURE usp_GetCommonNoteSite(v_CommonNoteFlow VARCHAR2,
                                  o_result         OUT empcurtype,
                                  o_result1        OUT empcurtype);

  --��ȡ��ϸͨ�õ�
  PROCEDURE usp_GetDetailCommonNote(v_CommonNoteFlow VARCHAR2,
                                    o_result         OUT empcurtype,
                                    o_result1        OUT empcurtype,
                                    o_result2        OUT empcurtype);

  --������޸�ͨ�õ�
  PROCEDURE usp_AddOrModCommonNote(v_CommonNoteFlow   VARCHAR2,
                                   v_CommonNoteName   VARCHAR2,
                                   v_PrinteModelName  VARCHAR2,
                                   v_ShowType         VARCHAR2,
                                   v_CreateDoctorID   VARCHAR2,
                                   v_CreateDoctorName VARCHAR2,
                                   v_UsingFlag        VARCHAR2,
                                   v_Valide           VARCHAR2,
                                   v_PYM              varchar2,
                                   v_WBM              varchar2,
                                   v_UsingPicSign     varchar2,
                                   v_UsingCheckDoc    varchar2);

  --������޸�ͨ�õ���ǩ
  PROCEDURE usp_AddOrModCommonNote_Tab(v_CommonNote_Tab_Flow VARCHAR2,
                                       v_CommonNoteFlow      VARCHAR2,
                                       v_CommonNoteTabName   VARCHAR2,
                                       v_UsingRole           VARCHAR2,
                                       v_ShowType            VARCHAR2,
                                       v_OrderCode           VARCHAR2,
                                       v_CreateDoctorID      VARCHAR2,
                                       v_CreateDoctorName    VARCHAR2,
                                       v_Valide              VARCHAR2,
                                       v_RowMax              VARCHAR2);

  --������޸�ͨ�õ���Ŀ
  PROCEDURE usp_AddOrModCommonNote_Item(v_CommonNote_Item_Flow VARCHAR2,
                                        v_CommonNote_Tab_Flow  VARCHAR2,
                                        v_CommonNoteFlow       VARCHAR2,
                                        v_DataElementFlow      VARCHAR2,
                                        v_DataElementId        VARCHAR2,
                                        v_DataElementName      VARCHAR2,
                                        v_OrderCode            VARCHAR2,
                                        v_IsValidate           VARCHAR2,
                                        v_CreateDoctorID       VARCHAR2,
                                        v_CreateDoctorName     VARCHAR2,
                                        v_Valide               VARCHAR2,
                                        v_OtherName            varchar2);

  --������޸�ͨ�õ�����
  PROCEDURE usp_AddOrModCommonNote_Site(v_Site_Flow      VARCHAR2,
                                        v_CommonNoteFlow VARCHAR2,
                                        v_RelationType   VARCHAR2,
                                        v_Site_ID        VARCHAR2,
                                        v_Valide         VARCHAR2);

  --ɾ����ͨ�õ������ÿ���
  PROCEDURE usp_DelCommonNote_Site(v_CommonNoteFlow VARCHAR2);

  --��ȡ���п��ҺͲ���
  PROCEDURE usp_GetAllDepartAndAreas(o_result  OUT empcurtype,
                                     o_result1 OUT empcurtype);

  --��ȡ��ǰ���һ����µ��������õ��� v_type 01���� 02����
  PROCEDURE usp_GetCommonNoteForDeptWard(v_SiteCode varchar2,
                                         v_Type     varchar2,
                                         o_result   out empcurtype);

  --ͨ�����õ���ˮ�Ż�ȡ���˵���
  PROCEDURE usp_GetSimInCommonNote(v_noofInpant varchar2,
                                   o_result     out empcurtype);

  PROCEDURE usp_GetSimInCommonNoteByFlow(v_noofInpant     varchar2,
                                         v_commonnoteflow varchar2,
                                         o_result         out empcurtype);

  --�������õ���ˮ�Ż�ȡͨ�õ���ϸ��Ϣ
  PROCEDURE usp_GetDetailInCommonNoteByDay(v_InCommonNoteFlow     varchar2,
                                           v_incommonnote_tabflow varchar2,
                                           v_StartDate            varchar2,
                                           v_EndDate              varchar2,
                                           o_result               out empcurtype,
                                           o_result1              out empcurtype,
                                           o_result2              out empcurtype);

  --�������õ���ˮ�Ż�ȡͨ�õ���ϸ��Ϣ
  PROCEDURE usp_GetDetailInCommonNote(v_InCommonNoteFlow varchar2,
                                      o_result           out empcurtype,
                                      o_result1          out empcurtype,
                                      o_result2          out empcurtype);

  --��ӻ��޸Ĳ������õ�
  PROCEDURE usp_AddorModInCommon(v_InCommonNoteFlow varchar2,
                                 v_CommonNoteFlow   varchar2,
                                 v_InCommonNoteName varchar2,
                                 v_PrinteModelName  varchar2,
                                 v_NoofInpatient    varchar2,
                                 v_InPatientName    varchar2,
                                 v_CurrDepartID     varchar2,
                                 v_CurrDepartName   varchar2,
                                 v_CurrWardID       varchar2,
                                 v_CurrWardName     varchar2,
                                 v_CreateDoctorID   varchar2,
                                 v_CreateDoctorName varchar2,
                                 v_CreateDateTime   varchar2,
                                 v_Valide           varchar2,
                                 v_CheckDocId       varchar2,
                                 v_CheckDocName     varchar2);

  --��ӻ��޸Ĳ������õ�tab
  PROCEDURE usp_AddorModInCommonTab(v_InCommonNote_Tab_Flow varchar2,
                                    v_CommonNote_Tab_Flow   varchar2,
                                    v_InCommonNoteFlow      varchar2,
                                    v_CommonNoteTabName     varchar2,
                                    v_UsingRole             varchar2,
                                    v_ShowType              varchar2,
                                    v_OrderCode             varchar2,
                                    v_CreateDoctorID        varchar2,
                                    v_CreateDoctorName      varchar2,
                                    v_CreateDateTime        varchar2,
                                    v_Valide                varchar2);

  --��ӻ��޸Ĳ������õ���Ŀ
  PROCEDURE usp_AddorModInCommonitem(v_InCommonNote_Item_Flow varchar2,
                                     v_InCommonNote_Tab_Flow  varchar2,
                                     v_InCommonNoteFlow       varchar2,
                                     v_CommonNote_Item_Flow   varchar2,
                                     v_CommonNote_Tab_Flow    varchar2,
                                     v_CommonNoteFlow         varchar2,
                                     v_DataElementFlow        varchar2,
                                     v_DataElementId          varchar2,
                                     v_DataElementName        varchar2,
                                     v_OtherName              varchar2,
                                     v_OrderCode              varchar2,
                                     v_IsValidate             varchar2,
                                     v_CreateDoctorID         varchar2,
                                     v_CreateDoctorName       varchar2,
                                     v_CreateDateTime         varchar2,
                                     v_Valide                 varchar2,
                                     v_ValueXml               clob,
                                     v_RecordDate             varchar2,
                                     v_RecordTime             varchar2,
                                     v_RecordDoctorId         varchar2,
                                     v_RecordDoctorName       varchar2,
                                     v_GroupFlow              varchar2);

  --��ӻ��޸�ʹ�õ����� xll 20130325
  PROCEDURE usp_AddInCommonType(v_CommonNote_Item_Flow  varchar2,
                                v_InCommonNote_Tab_Flow varchar2,
                                v_DataElementFlow       varchar2,
                                v_OtherName             varchar2);

  --��ȡ������Ϣ ����������Ϣ�����ڿ�����Ϣ
  PROCEDURE usp_GetInpatient(v_noofInpat varchar2, o_result out empcurtype);

  --��ȡһ��ĵ�������
  PROCEDURE usp_GetInCommomItemDay(v_commonNoteFlow varchar2,
                                   v_date           varchar2,
                                   v_dept           varchar2,
                                   v_ward           varchar2,
                                   o_result         out empcurtype);

  --������Ŀ��ȡ������Ϣ
  PROCEDURE usp_GetInpatientSim(v_incommonnoteitemflow varchar2,
                                o_result               out empcurtype);

  --��ȡ���˵�����һ������
  PROCEDURE usp_GetIncommonNew(v_noofinpat      varchar2,
                               v_commonnoteflow varchar2,
                               o_result         out empcurtype);

  --��ȡĳ������д��ڶ�����
  PROCEDURE usp_GetIncommonTabCount(v_incommonnote_tab_flow varchar2,
                                    o_result                out empcurtype);

  --��ȡ��ǰ������ǰ���ҵ����в���
  --xlb 2013-01-18
  PROCEDURE usp_GetDeptAndWardInpatient(
                                        
                                        v_outhosdept varchar,
                                        v_outhosward varchar,
                                        o_result     out empcurtype);

  --����ͬ��ʱ������Ӥ��״̬��ĸ��һ�� ��ͨ�õ����޹�  xll 20130307
  PROCEDURE usp_ChangeBabyStatus;

  PROCEDURE usp_AddOrModCommonNoteCount(v_CommonNoteCountFlow VARCHAR2,
                                        v_CommonNoteFlow      VARCHAR2,
                                        v_ItemCount           VARCHAR2,
                                        v_Hour12Name          VARCHAR2,
                                        v_Hour12Time          VARCHAR2,
                                        v_Hour24Name          VARCHAR2,
                                        v_Hour24Time          VARCHAR2,
                                        v_Valide              VARCHAR2);

  --������ʷ��¼
  PROCEDURE usp_AddPrintHistory(v_phflow          VARCHAR2,
                                v_printrecordflow VARCHAR2,
                                v_startpage       integer,
                                v_endpage         integer,
                                v_printpages      integer,
                                v_printdocid      VARCHAR2,
                                v_printdatetime   VARCHAR2,
                                v_printtype       VARCHAR2);

  --���뻤���ݵ���ʷ��¼
  PROCEDURE usp_AddInCommHistory(v_historyflow      varchar2,
                                 v_rowflow          varchar2,
                                 v_createdoctorid   varchar2,
                                 v_createdoctorname varchar2,
                                 v_createdatetime   varchar2,
                                 v_recorddate       varchar2,
                                 v_recordtime       varchar2,
                                 v_recorddoctorid   varchar2,
                                 v_recorddoctorname varchar2,
                                  v_valide varchar2 );

  --���뻤���ݵ���ʷ��¼ ��
  PROCEDURE usp_AddInCommColHistory(v_incommonnote_item_flow varchar2,
                                    v_hisrowflow             varchar2,
                                    v_valuexml               varchar2,
                                    v_commonnote_item_flow   varchar2,
                                    v_incommonnote_tab_flow  varchar2,
                                    v_incommonnoteflow       varchar2);
                                    
                                    --���˻����ݴӵڼ�ҳ��ʼ��ӡ
  PROCEDURE usp_AddOrModIncommPagefrom(v_incommonnoteflow   VARCHAR2,
                                   v_PageFrom   VARCHAR2) ;

end EMR_CommonNote;
/
create or replace package body emr_commonnote is

  --ͨ������������ȡ����Ԫ
  PROCEDURE usp_GetDateElement(v_ElementId    VARCHAR2,
                               v_ElementName  VARCHAR2,
                               v_ElementClass VARCHAR2,
                               v_ElementPYM   VARCHAR2,
                               o_result       OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select *
        from dateelement d
       where d.elementid like '%' || v_ElementId || '%'
         and d.elementname like '%' || v_ElementName || '%'
         and d.elementclass like '%' || v_ElementClass || '%'
         and d.elementpym like '%' || v_ElementPYM || '%'
         and d.valid = '1';
  END;

  --ͨ������������ȡ����Ԫ
  PROCEDURE usp_GetDateElementOne(v_ElementFlow VARCHAR2,
                                  o_result      OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select * from dateelement d where d.elementflow = v_ElementFlow;
  END;

  --ͨ������ԪID�����ǹ����ڸ�����Ԫ
  PROCEDURE usp_ValiDateElement(v_ElementId VARCHAR2,
                                o_result    OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select *
        from dateelement d
       where d.elementid = v_ElementId
         and d.valid = '1';
  END;

  --��������Ԫ
  PROCEDURE usp_InsertElement(v_ElementFlow     VARCHAR2,
                              v_ElementId       VARCHAR2,
                              v_ElementName     VARCHAR2,
                              v_ElementType     VARCHAR2,
                              v_ElementForm     VARCHAR2,
                              v_ElementRange    VARCHAR2,
                              v_ElementDescribe VARCHAR2,
                              v_ElementClass    VARCHAR2,
                              v_IsDataElemet    VARCHAR2,
                              v_ElementPYM      VARCHAR2) AS
  BEGIN
    insert into dateelement
    values
      (v_ElementFlow,
       v_ElementId,
       v_ElementName,
       v_ElementType,
       v_ElementForm,
       v_ElementRange,
       v_ElementDescribe,
       v_ElementClass,
       v_IsDataElemet,
       v_ElementPYM,
       '1');
  END;

  --�޸�����Ԫ
  PROCEDURE usp_UpDateElement(v_ElementFlow     VARCHAR2,
                              v_ElementId       VARCHAR2,
                              v_ElementName     VARCHAR2,
                              v_ElementType     VARCHAR2,
                              v_ElementForm     VARCHAR2,
                              v_ElementRange    VARCHAR2,
                              v_ElementDescribe VARCHAR2,
                              v_ElementClass    VARCHAR2,
                              v_IsDataElemet    VARCHAR2,
                              v_ElementPYM      VARCHAR2,
                              v_Valid           VARCHAR2) AS
  BEGIN
    update dateelement d
       set d.ElementId       = v_ElementId,
           d.ElementName     = v_ElementName,
           d.ElementType     = v_ElementType,
           d.ElementForm     = v_ElementForm,
           d.ElementRange    = v_ElementRange,
           d.ElementDescribe = v_ElementDescribe,
           d.ElementClass    = v_ElementClass,
           d.IsDataElemet    = v_IsDataElemet,
           d.ElementPYM      = v_ElementPYM,
           d.Valid           = v_Valid
     where d.elementflow = v_ElementFlow;
  END;

  --��ȡ�򵥰�ͨ�õ�
  PROCEDURE usp_GetSimpleCommonNote(v_CommonNoteName VARCHAR2,
                                    o_result         OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select *
        from COMMONNOTE cn
       Where (cn.commonnotename like '%' || v_CommonNoteName || '%' or
             cn.pym like '%' || v_CommonNoteName || '%' or
             cn.wbm like '%' || v_CommonNoteName || '%')
         and cn.valide = '1'
       order by cn.createdatetime;
  
  END;

  --��ȡͨ�õ���Ӧ���ҺͲ���
  PROCEDURE usp_GetCommonNoteSite(v_CommonNoteFlow VARCHAR2,
                                  o_result         OUT empcurtype,
                                  o_result1        OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select de.id, de.name
        from COMMONNOTE_SITE_REF cref
        left join department de
          on cref.site_id = de.id
       where cref.relationtype = '01'
         and cref.valide = '1'
         and cref.commonnoteflow = v_CommonNoteFlow; --����
  
    OPEN o_result1 FOR
      select ward.id, ward.name
        from COMMONNOTE_SITE_REF cref
        left join ward
          on cref.site_id = ward.id
       where cref.relationtype = '02'
         and cref.valide = '1'
         and cref.commonnoteflow = v_CommonNoteFlow; --����
  END;

  --��ȡ��ϸͨ�õ�
  PROCEDURE usp_GetDetailCommonNote(v_CommonNoteFlow VARCHAR2,
                                    o_result         OUT empcurtype,
                                    o_result1        OUT empcurtype,
                                    o_result2        OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select *
        from COMMONNOTE cn
       Where cn.commonnoteflow = v_CommonNoteFlow
         and cn.valide = '1';
    OPEN o_result1 FOR
      select *
        from COMMONNOTE_TAB ctab
       Where ctab.Commonnoteflow = v_CommonNoteFlow
         and ctab.valide = '1'
       order by ctab.ordercode asc;
    OPEN o_result2 FOR
      select *
        from COMMONNOTE_ITEM citem
       Where citem.Commonnoteflow = v_CommonNoteFlow
         and citem.valide = '1'
       order by citem.ordercode;
  END;

  --������޸�ͨ�õ�
  PROCEDURE usp_AddOrModCommonNote(v_CommonNoteFlow   VARCHAR2,
                                   v_CommonNoteName   VARCHAR2,
                                   v_PrinteModelName  VARCHAR2,
                                   v_ShowType         VARCHAR2,
                                   v_CreateDoctorID   VARCHAR2,
                                   v_CreateDoctorName VARCHAR2,
                                   v_UsingFlag        VARCHAR2,
                                   v_Valide           VARCHAR2,
                                   v_PYM              varchar2,
                                   v_WBM              varchar2,
                                   v_UsingPicSign     varchar2,
                                   v_UsingCheckDoc    varchar2) AS
    v_count integer;
  BEGIN
    select count(*)
      into v_count
      from COMMONNOTE
     where COMMONNOTE.Commonnoteflow = v_CommonNoteFlow;
    IF v_count <= 0 THEN
      insert into COMMONNOTE
      values
        (v_CommonNoteFlow,
         v_CommonNoteName,
         v_PrinteModelName,
         v_ShowType,
         v_CreateDoctorID,
         v_CreateDoctorName,
         to_char(sysdate, 'yyyyMMddhh24Miss'),
         v_UsingFlag,
         '1',
         v_PYM,
         v_WBM,
         v_UsingPicSign,
         v_UsingCheckDoc);
    ELSE
      update COMMONNOTE cn
         set cn.commonnotename  = v_CommonNoteName,
             cn.printemodelname = v_PrinteModelName,
             cn.showtype        = v_ShowType,
             cn.usingflag       = v_UsingFlag,
             cn.valide          = v_Valide,
             cn.pym             = v_PYM,
             cn.wbm             = v_WBM,
             cn.usingpicsign    = v_UsingPicSign,
             cn.usingcheckdoc   = v_UsingCheckDoc
       where cn.COMMONNOTEFLOW = v_CommonNoteFlow;
    end if;
  END;

  --������޸�ͨ�õ���ǩ
  PROCEDURE usp_AddOrModCommonNote_Tab(v_CommonNote_Tab_Flow VARCHAR2,
                                       v_CommonNoteFlow      VARCHAR2,
                                       v_CommonNoteTabName   VARCHAR2,
                                       v_UsingRole           VARCHAR2,
                                       v_ShowType            VARCHAR2,
                                       v_OrderCode           VARCHAR2,
                                       v_CreateDoctorID      VARCHAR2,
                                       v_CreateDoctorName    VARCHAR2,
                                       v_Valide              VARCHAR2,
                                       v_RowMax              VARCHAR2) AS
    v_count integer;
  BEGIN
    select count(*)
      into v_count
      from COMMONNOTE_TAB
     where COMMONNOTE_TAB.CommonNote_Tab_Flow = v_CommonNote_Tab_Flow;
    if v_count <= 0 then
      insert into COMMONNOTE_TAB
        (commonnote_tab_flow,
         commonnoteflow,
         commonnotetabname,
         usingrole,
         showtype,
         ordercode,
         createdoctorid,
         createdoctorname,
         createdatetime,
         valide,
         maxrows)
      values
        (v_CommonNote_Tab_Flow,
         v_CommonNoteFlow,
         v_CommonNoteTabName,
         v_UsingRole,
         v_ShowType,
         v_OrderCode,
         v_CreateDoctorID,
         v_CreateDoctorName,
         to_char(sysdate, 'yyyyMMddhh24Miss'),
         '1',
         v_RowMax);
    else
      update COMMONNOTE_TAB ctab
         set ctab.usingrole         = v_UsingRole,
             ctab.showtype          = v_ShowType,
             ctab.ordercode         = v_OrderCode,
             ctab.valide            = v_Valide,
             ctab.CommonNoteTabName = v_CommonNoteTabName,
             ctab.maxrows           = v_RowMax
       where ctab.commonnote_tab_flow = v_CommonNote_Tab_Flow;
    end if;
  END;

  --������޸�ͨ�õ���Ŀ
  PROCEDURE usp_AddOrModCommonNote_Item(v_CommonNote_Item_Flow VARCHAR2,
                                        v_CommonNote_Tab_Flow  VARCHAR2,
                                        v_CommonNoteFlow       VARCHAR2,
                                        v_DataElementFlow      VARCHAR2,
                                        v_DataElementId        VARCHAR2,
                                        v_DataElementName      VARCHAR2,
                                        v_OrderCode            VARCHAR2,
                                        v_IsValidate           VARCHAR2,
                                        v_CreateDoctorID       VARCHAR2,
                                        v_CreateDoctorName     VARCHAR2,
                                        v_Valide               VARCHAR2,
                                        v_OtherName            varchar2) AS
    v_count integer;
  BEGIN
    select count(*)
      into v_count
      from COMMONNOTE_ITEM
     where COMMONNOTE_ITEM.CommonNote_Item_Flow = v_CommonNote_Item_Flow;
    if v_count <= 0 then
      insert into COMMONNOTE_ITEM
      values
        (v_CommonNote_Item_Flow,
         v_CommonNote_Tab_Flow,
         v_CommonNoteFlow,
         v_DataElementFlow,
         v_DataElementId,
         v_DataElementName,
         v_OrderCode,
         v_IsValidate,
         v_CreateDoctorID,
         v_CreateDoctorName,
         to_char(sysdate, 'yyyyMMddhh24Miss'),
         '1',
         v_OtherName);
    else
      update COMMONNOTE_ITEM citem
         set citem.DataElementFlow = v_DataElementFlow,
             citem.DataElementId   = v_DataElementId,
             citem.DataElementName = v_DataElementName,
             citem.OrderCode       = v_OrderCode,
             citem.IsValidate      = v_IsValidate,
             citem.Valide          = v_Valide,
             citem.othername       = v_OtherName
       where citem.CommonNote_Item_Flow = v_CommonNote_Item_Flow;
    end if;
  END;

  --������޸�ͨ�õ������
  PROCEDURE usp_AddOrModCommonNote_Site(v_Site_Flow      VARCHAR2,
                                        v_CommonNoteFlow VARCHAR2,
                                        v_RelationType   VARCHAR2,
                                        v_Site_ID        VARCHAR2,
                                        v_Valide         VARCHAR2) AS
    sitecount integer;
  BEGIN
    select count(*)
      into sitecount
      from COMMONNOTE_SITE_REF
     where COMMONNOTE_SITE_REF.SITE_ID = v_Site_ID
       and COMMONNOTE_SITE_REF.COMMONNOTEFLOW = v_CommonNoteFlow
       and COMMONNOTE_SITE_REF.Valide = '1'
       and COMMONNOTE_SITE_REF.Relationtype = v_RelationType;
    if sitecount > 0 then
      update COMMONNOTE_SITE_REF cref
         set cref.valide = v_Valide
       where cref.site_id = v_Site_ID
         and cref.commonnoteflow = v_CommonNoteFlow;
    else
      Insert into COMMONNOTE_SITE_REF
      values
        (v_Site_Flow,
         v_CommonNoteFlow,
         v_RelationType,
         v_Site_ID,
         v_Valide);
    end if;
  END;

  --ɾ����ͨ�õ������ÿ���
  PROCEDURE usp_DelCommonNote_Site(v_CommonNoteFlow VARCHAR2) AS
  BEGIN
    update COMMONNOTE_SITE_REF cref
       set cref.valide = '0'
     where cref.commonnoteflow = v_CommonNoteFlow;
  END;

  --��ȡ���п��ҺͲ���
  PROCEDURE usp_GetAllDepartAndAreas(o_result  OUT empcurtype,
                                     o_result1 OUT empcurtype) AS
  BEGIN
    open o_result for
      select de.id, de.name from department de where de.valid = '1';
    open o_result1 for
      select ward.id, ward.name from ward where ward.valid = '1';
  END;

  --��ȡ��ǰ���һ����µ��������õ��� v_type 01���� 02����
  PROCEDURE usp_GetCommonNoteForDeptWard(v_SiteCode varchar2,
                                         v_Type     varchar2,
                                         o_result   out empcurtype) as
  begin
    open o_result for
      select *
        from commonnote cn
       where cn.commonnoteflow in
             (select comsite.commonnoteflow
                from commonnote_site_ref comsite
               where comsite.relationtype = v_Type
                 and comsite.site_id = v_SiteCode
                 and comsite.valide = '1')
         and cn.valide = '1';
  end;

  --ͨ�����õ���ˮ�Ż�ȡ���˵���
  PROCEDURE usp_GetSimInCommonNote(v_noofInpant varchar2,
                                   o_result     out empcurtype) as
  begin
    open o_result for
      select *
        from incommonnote icn
       where icn.noofinpatient = v_noofInpant
         and icn.valide = '1'
       order by icn.incommonnotename, icn.createdatetime;
  end;

  --ͨ�����õ���ˮ�Ż�ȡ���˵��� ĳ�ֵ�������
  PROCEDURE usp_GetSimInCommonNoteByFlow(v_noofInpant     varchar2,
                                         v_commonnoteflow varchar2,
                                         o_result         out empcurtype) as
  begin
    open o_result for
      select *
        from incommonnote icn
       where icn.noofinpatient = v_noofInpant
         and icn.commonnoteflow = v_commonnoteflow
         and icn.valide = '1'
       order by icn.createdatetime;
  end;

  --�������õ���ˮ�Ż�ȡͨ�õ���ϸ��Ϣ ����ʱ��
  PROCEDURE usp_GetDetailInCommonNoteByDay(v_InCommonNoteFlow     varchar2,
                                           v_incommonnote_tabflow varchar2,
                                           v_StartDate            varchar2,
                                           v_EndDate              varchar2,
                                           o_result               out empcurtype,
                                           o_result1              out empcurtype,
                                           o_result2              out empcurtype) as
  begin
    open o_result for
      select *
        from incommonnote icom
       where icom.incommonnoteflow = v_InCommonNoteFlow
         and icom.valide = '1';
    open o_result1 for
      select *
        from incommonnote_tab icomtab
       where icomtab.incommonnoteflow = v_InCommonNoteFlow
         and icomtab.valide = '1'
       order by icomtab.ordercode;
  
    if v_StartDate is null and v_EndDate is null then
      open o_result2 for
        select *
          from incommonnote_item_view icomitem
         where icomitem.incommonnoteflow = v_InCommonNoteFlow
           and icomitem.valide = '1'
           and icomitem.incommonnote_tab_flow = v_incommonnote_tabflow
         order by icomitem.ordercode,
                  icomitem.recorddate,
                  icomitem.recordtime;
    else
      open o_result2 for
        select *
          from incommonnote_item_view icomitem
         where icomitem.incommonnoteflow = v_InCommonNoteFlow
           and icomitem.valide = '1'
           and icomitem.incommonnote_tab_flow = v_incommonnote_tabflow
           and icomitem.recorddate >= v_StartDate
           and icomitem.recorddate <= v_EndDate
         order by icomitem.ordercode,
                  icomitem.recorddate,
                  icomitem.recordtime;
    end if;
  end;

  --�������õ���ˮ�Ż�ȡͨ�õ���ϸ��Ϣ
  PROCEDURE usp_GetDetailInCommonNote(v_InCommonNoteFlow varchar2,
                                      o_result           out empcurtype,
                                      o_result1          out empcurtype,
                                      o_result2          out empcurtype) as
  begin
    open o_result for
      select *
        from incommonnote icom
       where icom.incommonnoteflow = v_InCommonNoteFlow
         and icom.valide = '1';
    open o_result1 for
      select *
        from incommonnote_tab icomtab
       where icomtab.incommonnoteflow = v_InCommonNoteFlow
         and icomtab.valide = '1'
       order by icomtab.ordercode;
    open o_result2 for
      select *
        from incommonnote_item_view icomitem
       where icomitem.valide = '1'
         and icomitem.incommonnoteflow = v_InCommonNoteFlow
         and icomitem.incommonnote_tab_flow in
             (select icomtab.incommonnote_tab_flow
                from incommonnote_tab icomtab
               where icomtab.incommonnoteflow = v_InCommonNoteFlow
                 and icomtab.valide = '1')
       order by icomitem.ordercode,
                icomitem.recorddate,
                icomitem.recordtime;
  end;

  --��ӻ��޸Ĳ������õ�
  PROCEDURE usp_AddorModInCommon(v_InCommonNoteFlow varchar2,
                                 v_CommonNoteFlow   varchar2,
                                 v_InCommonNoteName varchar2,
                                 v_PrinteModelName  varchar2,
                                 v_NoofInpatient    varchar2,
                                 v_InPatientName    varchar2,
                                 v_CurrDepartID     varchar2,
                                 v_CurrDepartName   varchar2,
                                 v_CurrWardID       varchar2,
                                 v_CurrWardName     varchar2,
                                 v_CreateDoctorID   varchar2,
                                 v_CreateDoctorName varchar2,
                                 v_CreateDateTime   varchar2,
                                 v_Valide           varchar2,
                                 v_CheckDocId       varchar2,
                                 v_CheckDocName     varchar2) as
    i_count integer := 0;
  begin
    select count(*)
      into i_count
      from incommonnote
     where incommonnote.incommonnoteflow = v_InCommonNoteFlow;
    if i_count <= 0 then
      insert into incommonnote
      values
        (v_InCommonNoteFlow,
         v_CommonNoteFlow,
         v_InCommonNoteName,
         v_PrinteModelName,
         v_NoofInpatient,
         v_InPatientName,
         v_CurrDepartID,
         v_CurrDepartName,
         v_CurrWardID,
         v_CurrWardName,
         v_CreateDoctorID,
         v_CreateDoctorName,
         to_char(sysdate, 'yyyyMMddhh24Miss'),
         '1',
         v_CheckDocId,
         v_CheckDocName);
    else
      null;
    end if;
  end;

  --��ӻ��޸Ĳ������õ�tab
  PROCEDURE usp_AddorModInCommonTab(v_InCommonNote_Tab_Flow varchar2,
                                    v_CommonNote_Tab_Flow   varchar2,
                                    v_InCommonNoteFlow      varchar2,
                                    v_CommonNoteTabName     varchar2,
                                    v_UsingRole             varchar2,
                                    v_ShowType              varchar2,
                                    v_OrderCode             varchar2,
                                    v_CreateDoctorID        varchar2,
                                    v_CreateDoctorName      varchar2,
                                    v_CreateDateTime        varchar2,
                                    v_Valide                varchar2) as
    i_count integer := 0;
  begin
    select count(*)
      into i_count
      from incommonnote_tab
     where incommonnote_tab.InCommonNote_Tab_Flow = v_InCommonNote_Tab_Flow;
    if i_count <= 0 then
      insert into incommonnote_tab
      values
        (v_InCommonNote_Tab_Flow,
         v_CommonNote_Tab_Flow,
         v_InCommonNoteFlow,
         v_CommonNoteTabName,
         v_UsingRole,
         v_ShowType,
         v_OrderCode,
         v_CreateDoctorID,
         v_CreateDoctorName,
         to_char(sysdate, 'yyyyMMddhh24Miss'),
         '1');
    else
      null;
    end if;
  end;

  --��ӻ��޸Ĳ������õ���Ŀ
  PROCEDURE usp_AddorModInCommonitem(v_InCommonNote_Item_Flow varchar2,
                                     v_InCommonNote_Tab_Flow  varchar2,
                                     v_InCommonNoteFlow       varchar2,
                                     v_CommonNote_Item_Flow   varchar2,
                                     v_CommonNote_Tab_Flow    varchar2,
                                     v_CommonNoteFlow         varchar2,
                                     v_DataElementFlow        varchar2,
                                     v_DataElementId          varchar2,
                                     v_DataElementName        varchar2,
                                     v_OtherName              varchar2,
                                     v_OrderCode              varchar2,
                                     v_IsValidate             varchar2,
                                     v_CreateDoctorID         varchar2,
                                     v_CreateDoctorName       varchar2,
                                     v_CreateDateTime         varchar2,
                                     v_Valide                 varchar2,
                                     v_ValueXml               clob,
                                     v_RecordDate             varchar2,
                                     v_RecordTime             varchar2,
                                     v_RecordDoctorId         varchar2,
                                     v_RecordDoctorName       varchar2,
                                     v_GroupFlow              varchar2) as
    i_countr   integer := 0;
    i_countcol integer := 0;
  begin
    --������
    select count(*)
      into i_countr
      from incommonnote_row r
     where r.rowflow = v_GroupFlow;
    if i_countr <= 0 then
      insert into incommonnote_row
        (rowflow,
         incommonnote_tab_flow,
         incommonnoteflow,
         commonnote_tab_flow,
         commonnoteflow,
         createdoctorid,
         createdoctorname,
         createdatetime,
         valide,
         recorddate,
         recordtime,
         recorddoctorid,
         recorddoctorname)
      values
        (v_GroupFlow,
         v_incommonnote_tab_flow,
         v_incommonnoteflow,
         v_commonnote_tab_flow,
         v_commonnoteflow,
         v_createdoctorid,
         v_createdoctorname,
         to_char(sysdate, 'yyyyMMddhh24Miss'),
         '1',
         v_recorddate,
         v_recordtime,
         v_recorddoctorid,
         v_recorddoctorname);
    else
      update incommonnote_row
         set valide           = v_valide,
             recorddate       = v_recorddate,
             recordtime       = v_recordtime,
             recorddoctorid   = v_recorddoctorid,
             recorddoctorname = v_recorddoctorname
       where rowflow = v_GroupFlow;
    end if;
  
    select count(*)
      into i_countcol
      from incommonnote_column c
     where c.incommonnote_item_flow = v_InCommonNote_Item_Flow;
    if i_countcol <= 0 then
      insert into incommonnote_column
        (incommonnote_item_flow,
         rowflow,
         valuexml,
         commonnote_item_flow,
         incommonnote_tab_flow,
         incommonnoteflow)
      values
        (v_incommonnote_item_flow,
         v_GroupFlow,
         v_valuexml,
         v_commonnote_item_flow,
         v_incommonnote_tab_flow,
         v_InCommonNoteFlow);
    else
      update incommonnote_column
         set valuexml = v_valuexml
       where incommonnote_item_flow = v_incommonnote_item_flow;
    end if;
  end;

  --��ӻ��޸�ʹ�õ�����
  PROCEDURE usp_AddInCommonType(v_CommonNote_Item_Flow  varchar2,
                                v_InCommonNote_Tab_Flow varchar2,
                                v_DataElementFlow       varchar2,
                                v_OtherName             varchar2) as
    i_count integer := 0;
  begin
    select count(*)
      into i_count
      from incommonnote_column_type i
     where i.commonnote_item_flow = v_CommonNote_Item_Flow
       and i.incommonnote_tab_flow = v_InCommonNote_Tab_Flow;
    if i_count <= 0 then
      insert into incommonnote_column_type
        (commonnote_item_flow,
         incommonnote_tab_flow,
         dataelementflow,
         othername)
      values
        (v_commonnote_item_flow,
         v_incommonnote_tab_flow,
         v_dataelementflow,
         v_othername);
    else
      null;
    end if;
  end;

  --��ȡ������Ϣ ����������Ϣ�����ڿ�����Ϣ
  PROCEDURE usp_GetInpatient(v_noofInpat varchar2, o_result out empcurtype) as
  
  begin
    open o_result for
      select inpatient.*,
             (select department.name
                from department
               where department.id = inpatient.outhosdept) as departname,
             (select ward.name
                from ward
               where ward.id = inpatient.outhosward) as wardname,
             (select diagnosis.name
                from diagnosis
               where diagnosis.icd = inpatient.admitdiagnosis) as diagnosisname,
             (select dictionary_detail.name
                from dictionary_detail
               where dictionary_detail.detailid = inpatient.sexid
                 and dictionary_detail.categoryid = '3') as sex
        from inpatient
       where noofinpat = v_noofInpat;
  end;

  --��ȡһ��ĵ�������
  PROCEDURE usp_GetInCommomItemDay(v_commonNoteFlow varchar2,
                                   v_date           varchar2,
                                   v_dept           varchar2,
                                   v_ward           varchar2,
                                   o_result         out empcurtype) as
  begin
    open o_result for
      select *
        from incommonnote_item_view item
       where item.incommonnoteflow in
            
             (select i.incommonnoteflow
                from incommonnote i
               where i.commonnoteflow = v_commonNoteFlow
                 and i.valide = '1'
                 and i.currdepartid = v_dept
                 and i.currwardid = v_ward)
            
         and item.incommonnote_tab_flow in
            
             (select tab.incommonnote_tab_flow
                from incommonnote_tab tab
               where tab.incommonnoteflow in
                     (select i.incommonnoteflow
                        from incommonnote i
                       where i.commonnoteflow = v_commonNoteFlow
                         and i.valide = '1')
                 and tab.ordercode =
                     (select min(ordercode)
                        from incommonnote_tab
                       where incommonnote_tab.incommonnoteflow in
                             (select ia.incommonnoteflow
                                from incommonnote ia
                               where ia.commonnoteflow = v_commonNoteFlow
                                 and ia.valide = '1')))
         and item.valide = '1'
         and item.recorddate = v_date;
  
  end;

  --������Ŀ��ȡ������Ϣ
  PROCEDURE usp_GetInpatientSim(v_incommonnoteitemflow varchar2,
                                o_result               out empcurtype) as
  begin
    open o_result for
      select i.name, i.patid, i.outbed
        from inpatient i
       where i.noofinpat =
             (select ic.noofinpatient
                from incommonnote ic
               where ic.incommonnoteflow =
                     (select it.incommonnoteflow
                        from incommonnote_item_view it
                       where it.incommonnote_item_flow =
                             v_incommonnoteitemflow));
  end;

  --��ȡ���˵�����һ������
  PROCEDURE usp_GetIncommonNew(v_noofinpat      varchar2,
                               v_commonnoteflow varchar2,
                               o_result         out empcurtype) as
  begin
    open o_result for
      select *
        from (select *
                from incommonnote i
               where i.noofinpatient = v_noofinpat
                 and i.commonnoteflow = v_commonnoteflow
                 and i.valide = '1'
               order by i.createdatetime desc)
       where rownum <= 1;
  end;

  --��ȡĳ������д��ڶ�����
  PROCEDURE usp_GetIncommonTabCount(v_incommonnote_tab_flow varchar2,
                                    o_result                out empcurtype) as
  begin
    open o_result for
      select count(*)
        from incommonnote_row i
       where i.incommonnote_tab_flow = v_incommonnote_tab_flow
         and i.valide = '1';
  end;

  --��ȡ��ǰ������ǰ���ҵ����в���
  --xlb 2013-01-18
  PROCEDURE usp_GetDeptAndWardInpatient(
                                        
                                        v_outhosdept varchar,
                                        v_outhosward varchar,
                                        o_result     out empcurtype) as
  begin
    open o_result for
      select i.noofinpat,
             i.name,
             i.outbed,
             i.outhosdept,
             i.patid,
             i.outhosward
        from inpatient i
       where i.outhosdept = v_outhosdept
         and i.outhosward = v_outhosward
         and i.status in ('1500', '1501')
       order by i.outbed;
  end;

  --����ͬ��ʱ������Ӥ��״̬��ĸ��һ�� ��ͨ�õ����޹�  xll 20130307
  PROCEDURE usp_ChangeBabyStatus as
  begin
    update inpatient chinpatient
       set chinpatient.status =
           (select binpatient.status
              from inpatient binpatient
             where binpatient.noofinpat = chinpatient.mother)
     where chinpatient.isbaby = 1
       and mother in (select binpatient.noofinpat
                        from inpatient binpatient
                       where binpatient.isbaby = 0);
  end;

  --������޸�ͨ�õ�tongji
  PROCEDURE usp_AddOrModCommonNoteCount(v_CommonNoteCountFlow VARCHAR2,
                                        v_CommonNoteFlow      VARCHAR2,
                                        v_ItemCount           VARCHAR2,
                                        v_Hour12Name          VARCHAR2,
                                        v_Hour12Time          VARCHAR2,
                                        v_Hour24Name          VARCHAR2,
                                        v_Hour24Time          VARCHAR2,
                                        v_Valide              VARCHAR2) AS
    v_count integer;
  BEGIN
    select count(*)
      into v_count
      from commonnotecount
     where commonnotecount.commonnoteflow = v_CommonNoteFlow;
    IF v_count <= 0 THEN
      insert into commonnotecount
        (commonnotecountflow,
         commonnoteflow,
         itemcount,
         hour12name,
         hour12time,
         hour24name,
         hour24time,
         valide)
      values
        (v_CommonNoteCountFlow,
         v_CommonNoteFlow,
         v_ItemCount,
         v_Hour12Name,
         v_Hour12Time,
         v_Hour24Name,
         v_Hour24Time,
         v_Valide);
    ELSE
      update commonnotecount
         set itemcount  = v_ItemCount,
             hour12name = v_Hour12Name,
             hour12time = v_Hour12Time,
             hour24name = v_Hour24Name,
             hour24time = v_Hour24Time,
             valide     = v_Valide
       where commonnoteflow = v_CommonNoteFlow;
    end if;
  END;

  --������ʷ��¼
  PROCEDURE usp_AddPrintHistory(v_phflow          VARCHAR2,
                                v_printrecordflow VARCHAR2,
                                v_startpage       integer,
                                v_endpage         integer,
                                v_printpages      integer,
                                v_printdocid      VARCHAR2,
                                v_printdatetime   VARCHAR2,
                                v_printtype       VARCHAR2) AS
  
  BEGIN
    insert into printhistorynurse
      (phflow,
       printrecordflow,
       startpage,
       endpage,
       printpages,
       printdocid,
       printdatetime,
       printtype)
    values
      (v_phflow,
       v_printrecordflow,
       v_startpage,
       v_endpage,
       v_printpages,
       v_printdocid,
       v_printdatetime,
       v_printtype);
  END;

  --���뻤���ݵ���ʷ��¼
  PROCEDURE usp_AddInCommHistory(v_historyflow      varchar2,
                                 v_rowflow          varchar2,
                                 v_createdoctorid   varchar2,
                                 v_createdoctorname varchar2,
                                 v_createdatetime   varchar2,
                                 v_recorddate       varchar2,
                                 v_recordtime       varchar2,
                                 v_recorddoctorid   varchar2,
                                 v_recorddoctorname varchar2,
                                 v_valide varchar2 ) AS
  
  BEGIN
    insert into incommonnote_rowhis
      (historyflow,
       rowflow,
       createdoctorid,
       createdoctorname,
       createdatetime,
       recorddate,
       recordtime,
       recorddoctorid,
       recorddoctorname,
       valide)
    values
      (v_historyflow,
       v_rowflow,
       v_createdoctorid,
       v_createdoctorname,
       v_createdatetime,
       v_recorddate,
       v_recordtime,
       v_recorddoctorid,
       v_recorddoctorname,
       v_valide);
  END;

  --���뻤���ݵ���ʷ��¼ ��
  PROCEDURE usp_AddInCommColHistory(v_incommonnote_item_flow varchar2,
                                    v_hisrowflow             varchar2,
                                    v_valuexml               varchar2,
                                    v_commonnote_item_flow   varchar2,
                                    v_incommonnote_tab_flow  varchar2,
                                    v_incommonnoteflow       varchar2) AS
  
  BEGIN
    insert into incommonnote_columnhis
      (incommonnote_item_flow,
       hisrowflow,
       valuexml,
       commonnote_item_flow,
       incommonnote_tab_flow,
       incommonnoteflow)
    values
      (v_incommonnote_item_flow,
       v_hisrowflow,
       v_valuexml,
       v_commonnote_item_flow,
       v_incommonnote_tab_flow,
       v_incommonnoteflow);
  END;
  
  
    --���˻����ݴӵڼ�ҳ��ʼ��ӡ
  PROCEDURE usp_AddOrModIncommPagefrom(v_incommonnoteflow   VARCHAR2,
                                   v_PageFrom   VARCHAR2) AS
    v_count integer;
  BEGIN
    select count(*)
      into v_count
      from incommonprintfrompage
     where incommonprintfrompage.incommonnoteflow = v_incommonnoteflow;
    IF v_count <= 0 THEN
      insert into incommonprintfrompage( incommonnoteflow,pagefrom)
      values
        (v_incommonnoteflow,v_PageFrom);
    ELSE
      update incommonprintfrompage cn
         set cn.pagefrom=v_PageFrom
       where cn.incommonnoteflow = v_incommonnoteflow;
    end if;
  END;

end EMR_CommonNote;
/
