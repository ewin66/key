create or replace package EMRSheetReport is

  -- Private type declarations
  TYPE empcurtype IS REF CURSOR;

  ----------������Ϣͳ��          xll 2013-08-05
  PROCEDURE usp_GetQCDieInfo(v_deptid    VARCHAR DEFAULT '',
                             v_timebegin VARCHAR DEFAULT '',
                             v_timeend   VARCHAR DEFAULT '',
                             o_result    OUT empcurtype);

  --------������Ϣͳ��
  PROCEDURE usp_GetQCOperatInfo(v_deptid    VARCHAR,
                                v_timebegin VARCHAR DEFAULT '',
                                v_timeend   VARCHAR DEFAULT '',
                                o_result    OUT empcurtype);

  --------������ҳ��ѯ
  PROCEDURE usp_IemBaseInfo(v_deptid    VARCHAR,
                            v_timebegin VARCHAR DEFAULT '',
                            v_timeend   VARCHAR DEFAULT '',
                            v_userid    VARCHAR,
                            o_result    OUT empcurtype);

  --------�ż������
  PROCEDURE usp_DiagInfoMZ(v_deptid    VARCHAR,
                           v_timebegin VARCHAR DEFAULT '',
                           v_timeend   VARCHAR DEFAULT '',
                           v_diagid    VARCHAR,
                           o_result    OUT empcurtype);
end EMRSheetReport;
/
create or replace package body EMRSheetReport is

  ----------������Ϣͳ��          xll 2013-08-05
  PROCEDURE usp_GetQCDieInfo(v_deptid    VARCHAR DEFAULT '',
                             v_timebegin VARCHAR DEFAULT '',
                             v_timeend   VARCHAR DEFAULT '',
                             o_result    OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select a.patid סԺ��,
             a.name ��������,
             (case
               when a.SexID = 1 then
                '��'
               when a.SexID = 2 then
                'Ů'
               else
                'δ֪'
             end) as �Ա�,
             F_CYZDNAMEStr2012(i.iem_mainpage_no) as ��Ժ���,
             --( select d.diagnosis_name from iem_mainpage_diagnosis_2012 d where d.iem_mainpage_no=i.iem_mainpage_no and d.valide='1' and d.diagnosis_type_id='7' and rownum<=1) as ��Ժ���,
             a.outhosdate ��Ժʱ��,
             a.outhosdate ����ʱ��,
             round(sysdate - to_date(a.outhosdate, 'yyyy-mm-dd hh24:mi:ss'),
                   0) ��������,
             (select u.name from users u where u.id = i.vs_employee_code) ����ҽ��
        from inpatient a
        left join iem_mainpage_basicinfo_2012 i
          on i.noofinpat = a.noofinpat
       where i.outhostype = 5
         and (a.outhosdept = v_deptid or v_deptid is null or
             v_deptid = '0000')
         and to_date(a.outhosdate, 'yyyy-mm-dd hh24:mi:ss') between
             to_date(v_timebegin, 'yyyy-mm-dd') and
             to_date(v_timeend, 'yyyy-mm-dd');
  END;

  --------������Ϣͳ��
  PROCEDURE usp_GetQCOperatInfo(v_deptid    VARCHAR,
                                v_timebegin VARCHAR DEFAULT '',
                                v_timeend   VARCHAR DEFAULT '',
                                o_result    OUT empcurtype) AS
  BEGIN
  
    OPEN o_result FOR
      select a.patid סԺ��,
             a.name ��������,
             (case
               when a.SexID = 1 then
                '��'
               when a.SexID = 2 then
                'Ů'
               else
                'δ֪'
             end) as �Ա�,
             (select d.diagnosis_name
                from iem_mainpage_diagnosis_2012 d
               where d.iem_mainpage_no = i.iem_mainpage_no
                 and d.valide = '1'
                 and d.diagnosis_type_id = '7'
                 and rownum <= 1) as ��Ҫ���,
             f_operationnamestr2012(i.iem_mainpage_no) as ����,
             a.outhosdate ��Ժʱ��,
             a.outhosdate ��Ժʱ��,
             (select u.name from users u where u.id = i.vs_employee_code) ����ҽ��
        from inpatient a
        left join iem_mainpage_basicinfo_2012 i
          on i.noofinpat = a.noofinpat
       where a.status in ('1502', '1503')
         and (select count(*)
                from iem_mainpage_operation_2012 o
               where o.iem_mainpage_no = i.iem_mainpage_no
                 and o.valide = 1) > 0
         and (a.outhosdept = v_deptid or v_deptid is null or
             v_deptid = '0000')
         and to_date(a.outhosdate, 'yyyy-mm-dd hh24:mi:ss') between
             to_date(v_timebegin, 'yyyy-mm-dd') and
             to_date(v_timeend, 'yyyy-mm-dd');
  END;

  --------������ҳ��ѯ
  PROCEDURE usp_IemBaseInfo(v_deptid    VARCHAR,
                            v_timebegin VARCHAR DEFAULT '',
                            v_timeend   VARCHAR DEFAULT '',
                            v_userid    VARCHAR,
                            o_result    OUT empcurtype) AS
  BEGIN
  
    OPEN o_result FOR
      select distinct inp.noofrecord ������,
                      inp.name ����,
                      inp.patid סԺ��,
                      inp.agestr ����,
                      decode(round((TO_DATE(inp.outhosdate,
                                            'yyyy-mm-dd hh24:mi:ss') -
                                   TO_DATE(inp.admitdate,
                                            'yyyy-mm-dd hh24:mi:ss'))),
                             0,
                             1,
                             round((TO_DATE(inp.outhosdate,
                                            'yyyy-mm-dd hh24:mi:ss') -
                                   TO_DATE(inp.admitdate,
                                            'yyyy-mm-dd hh24:mi:ss')))) סԺ����,
                      (case
                        when inp.SexID = 1 then
                         '��'
                        when inp.SexID = 2 then
                         'Ů'
                        else
                         'δ֪'
                      end) as �Ա�,
                      dept.name ����,
                      user3.name ����������,
                      diag1.name ��Ժ���,
                      (select i.diagnosis_name
                         from iem_mainpage_diagnosis_2012 i
                        where i.iem_mainpage_no = temp.iem_mainpage_no
                          and i.valide = '1'
                          and i.diagnosis_type_id = '7'
                          and rownum <= 1) ��Ҫ��Ժ���,
                      basic.create_time ��������ʱ��,
                      temp.OperName ��������,
                      (case
                        when lengthb(inp.admitdate) > 10 then
                         substr(inp.admitdate, 0, 10)
                      end) as ��Ժʱ��,
                      (case
                        when lengthb(inp.outhosdate) > 10 then
                         substr(inp.outhosdate, 0, 10)
                      end) as ��Ժʱ��,
                      user1.name ����ҽ��,
                      
                      inp.INCOUNT סԺ����
        from (select distinct inp.patid,
                              bas.iem_mainpage_no,
                              wmsys.wm_concat(ope.operation_name || '(' ||
                                              ope.operation_date || ')') OperName
                from iem_mainpage_basicinfo_2012 bas
                left join inpatient inp
                  on inp.noofinpat = bas.noofinpat
                left join iem_mainpage_operation_2012 ope
                  on ope.iem_mainpage_no = bas.iem_mainpage_no
               where ope.valide = 1
               group by inp.patid, bas.iem_mainpage_no) temp
        left join iem_mainpage_basicinfo_2012 basic
          on basic.iem_mainpage_no = temp.iem_mainpage_no
        left join inpatient inp
          on basic.noofinpat = inp.noofinpat
        left join Department dept
          on inp.admitdept = dept.id
        left join diagnosis diag1
          on inp.admitdiagnosis = diag1.icd
        left join users user1
          on user1.id = inp.RESIDENT
        left join users user2
          on user2.id = inp.CHIEF
        left join users user3
          on user3.id = basic.create_user
       where basic.valide = 1
         and (basic.CREATE_USER = v_userid or v_userid = '' or
             v_userid is null)
         and To_Date(substr(nvl(basic.create_time, '2999-01-01'), 0, 10),
                     'yyyy-mm-dd') >= to_date(v_timebegin, 'yyyy-mm-dd')
         and To_Date(substr(nvl(basic.create_time, '1990-01-01'), 0, 10),
                     'yyyy-mm-dd') <= to_date(v_timeend, 'yyyy-mm-dd')
         and (inp.admitdept = v_deptid or v_deptid is null or v_deptid = '' or
             v_deptid = '0000');
  END;

  --------�ż������
  PROCEDURE usp_DiagInfoMZ(v_deptid    VARCHAR,
                           v_timebegin VARCHAR DEFAULT '',
                           v_timeend   VARCHAR DEFAULT '',
                           v_diagid    VARCHAR,
                           o_result    OUT empcurtype) AS
  BEGIN
  
    OPEN o_result FOR
      select a.patid סԺ��,
             a.name ����,
             (case
               when a.SexID = 1 then
                '��'
               when a.SexID = 2 then
                'Ů'
               else
                'δ֪'
             end) as �Ա�,
             a.agestr ����,
             diag.name �ż��������,
             a.admitdate ��Ժʱ��,
             imb.xzzaddress as סַ,
             dept.name ��������,
             
             u.name ����ҽ��
        from inpatient a
        left join iem_mainpage_basicinfo_2012 imb
          on imb.noofinpat = a.noofinpat
        left join iem_mainpage_diagnosis_2012 imd
          on imd.iem_mainpage_no = imb.iem_mainpage_no
        left join diagnosis diag
          on imd.diagnosis_code = diag.markid
        left join department dept
          on a.admitdept = dept.id
        left join users u
          on a.resident = u.id
       where (a.admitdept = v_deptid or v_deptid is null or
             v_deptid = '0000')
         and imd.diagnosis_type_id = '13'
         and imd.valide = '1'
         and imb.valide = '1'
         and to_date(a.inwarddate, 'yyyy-mm-dd hh24:mi:ss') between
             to_date(v_timebegin, 'yyyy-mm-dd hh24:mi:ss') and
             to_date(v_timeend, 'yyyy-mm-dd hh24:mi:ss')
         and (imd.diagnosis_code = v_diagid or v_diagid is null or v_diagid='');
  
  END;

end EMRSheetReport;
/
