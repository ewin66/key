create or replace package EMRORDER is

  -- Author  : Xianglingbo
  -- Created : 2013/4/8 16:21:43
  -- Purpose : ҽ����������Ժ�ģ����
  
  TYPE empcurtype IS REF CURSOR;
  
  
    PROCEDURE  GETORDER(

                      v_InpatId VARCHAR2,
                      v_DeptId  VARCHAR2,
                      v_Type    VARCHAR2,
                      v_BeginDate VARCHAR2,
                      v_EndDate   VARCHAR2,
                      o_result   OUT empcurtype);
                      
  PROCEDURE  GETORDER2(

                      v_InpatId VARCHAR2,
                      v_DeptId  VARCHAR2 default '',
                      v_Type    VARCHAR2,
                      v_BeginDate VARCHAR2 default '',
                      v_EndDate   VARCHAR2 default '',
                      o_result   OUT empcurtype);
                      
 -- PROCEDURE  proc_GetAllIemMainPageExcept (o_result out empcurtype);
end ;
/
create or replace package body EMRORDER is

  ------------------------------------------------------------------ҽ������洢���� ���ڲ��Ժ�չʾ Add by xlb 2013-04-08------------------------------------------------------------
  PROCEDURE  GETORDER(
                      v_InpatId VARCHAR2,
                      v_DeptId  VARCHAR2,
                      v_Type    VARCHAR2,
                      v_BeginDate VARCHAR2,
                      v_EndDate   VARCHAR2,
                      o_result   OUT empcurtype) as
                      
 v_sql VARCHAR2(4000);--��ʱҽ����sql
 v_sql2 VARCHAR2(4000);--����ҽ����sql   
--p_BeginDate VARCHAR2(50);
--p_EndDate   VARCHAR2(50);      
BEGIN

      
  v_sql :=  'select  starttime as "��ʼʱ��" ,madeordercontrol as "ҽ������" ,executetime as "ִ��ʱ��", 
     startdocname as "ҽʦǩ��" ,
     startnursename  as "��ʿǩ��" from yd_shortmadeorder s where ';
     -- s.noofhis= '||v_InpatId;
     
     v_sql2 :=' select  starttime as "����ʱ��",madeordercontrol as "ҽ������", startdocname as "ҽʦǩ��", 
  startdocid as "ҽʦ���",startnursename as "��ʿǩ��",startnurseid as "��ʿ���",
  endtime as "ֹͣʱ��", enddocname as "ֹͣҽʦǩ��",endnursename as "ֹͣ��ʿǩ��"
  from yd_longmadeorder l where ';
  -- l.noofhis='||v_InpatId;   
     
--p_BeginDate := v_BeginDate || ' 00:00:00';
--p_EndDate :=v_EndDate || ' 23:59:59';
  
 if v_Type='0'  --��ʱҽ��
 then

     if v_DeptId  is not null
     then
         v_sql :=v_sql || 'and deptid= '|| v_DeptId;
     end if;
     if v_BeginDate is not null
      then
        v_sql :=v_sql || 'and  starttime >='''||  v_BeginDate  ||  ' 00:00:00''';

     end if;
     if v_EndDate is not null
       then
        v_sql :=v_sql || 'and starttime <=''' || v_EndDate || ' 23:59:59''';

   end if;
   v_sql :=v_sql || 'order by starttime';
   
 open o_result for v_sql;
    
   else
  if v_DeptId is not null
     then
         v_sql2 :=v_sql2 || 'and deptid= '|| v_DeptId;
     end if;
     if v_BeginDate is not null
      then
      v_sql2 :=v_sql2 || 'and  starttime >='''||v_BeginDate||  ' 00:00:00''';
        
     end if;
     if v_EndDate is not null
       then
       v_sql2 :=v_sql2 || 'and starttime <=''' || v_EndDate || ' 23:59:59''';
   end if;
   v_sql2 :=v_sql2 || 'order by starttime';
   
 open o_result for v_sql2;
  END IF;
END;
  
  
  ----------------------------------------------------------ҽ�����  ���Ժ���ʾ����  Author xlb 2013-04-10-------------------------------------------------
   PROCEDURE  GETORDER2(

                      v_InpatId VARCHAR2,
                      v_DeptId  VARCHAR2 default '',
                      v_Type    VARCHAR2,
                      v_BeginDate VARCHAR2 default '',
                      v_EndDate   VARCHAR2 default '',
                      o_result   OUT   empcurtype) as
                      
                      p_begindate VARCHAR2(50);
                      p_EndDate VARCHAR2(50);
BEGIN
  open o_result for  
  select'' from dual;
  if v_BeginDate is not null
  then  
    -----���򴫹���ֻ��ȡ���� 
  p_begindate :=v_BeginDate || ' 00:00:00';
  END if;
  if v_EndDate is not null
  then
  p_EndDate :=v_EndDate || ' 23:59:59';
  END IF;
  if v_Type='0'
  THEN
     open o_result for  
     select  starttime as "ҽ��ʱ��"  ,madeordercontrol as "ҽ������" ,executetime as "ִ��ʱ��", 
     startdocname as "����ҽ��" ,
     startnursename  as "ִ�л�ʿ",deptid as "��������" from yd_shortmadeorder s where
     -- s.noofhis=v_InpatId
     --and
      s.deptid like '%'||v_DeptId||'%'  and to_date(s.starttime,'yyyy-mm-dd hh24:mi:ss') >= to_date(nvl(trim(p_begindate),'1000-01-01 00:00:00 '),'yyyy-MM-dd HH24:mi:ss') --��ʼʱ��Ϊ������1000-01-01 00:00:00���
     and to_date(s.starttime,'yyyy-mm-dd hh24:mi:ss') <= to_date(nvl(trim(p_EndDate),'9000-01-01 23:59:59 '),'yyyy-MM-dd HH24:mi:ss') order by s.starttime;--����ʱ��Ϊ������1000-01-01 00:00:00���
     
     else
       
    
     open o_result for    
     select  starttime as "����ʱ��",madeordercontrol as "ҽ������", startdocname as "ҽʦǩ��", 
     startdocid as "ҽʦ���",startnursename as "��ʿǩ��",startnurseid as "��ʿ���",
     endtime as "ֹͣʱ��", enddocname as "ֹͣҽʦǩ��",endnursename as "ֹͣ��ʿǩ��",deptid as "��������"
     from yd_longmadeorder s where
     -- s.noofhis=v_InpatId
     --and
      s.deptid like '%'|| v_DeptId ||'%'  and s.starttime is not null and  to_date(s.starttime,'yyyy-MM-dd hh24:mi:ss') >= to_date(nvl(trim(p_begindate ),'1000-01-01 00:00:00 '),'yyyy-MM-dd HH24:mi:ss')
     and to_date(s.starttime,'yyyy-MM-dd hh24:mi:ss') <= to_date(nvl(trim(p_EndDate),'9000-01-01 23:59:59'),'yyyy-MM-dd HH24:mi:ss') order by s.starttime;
     END IF;        
  
                     
end;

  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
end ;--package end
/
