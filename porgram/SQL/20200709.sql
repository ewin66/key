prompt Importing table APPCFG...
set feedback off
set define off
insert into APPCFG (CONFIGKEY, NAME, VALUE, DESCRIPT, PARAMTYPE, CFGKEYSET, DESIGN, CLIENTFLAG, HIDE, VALID, CREATEDATE, SORTID)
values ('Namerec', '�Զ��岡������', '0', '0-������ 1-����', 1, null, null, 0, 0, 1, to_date('09-07-2020', 'dd-mm-yyyy'), null);

-- Add comments to the columns 
comment on column IEM_MAINPAGE_OPERATION_SX.operation_date
  is '����ʱ��';
comment on column IEM_MAINPAGE_OPERATION_SX.iem_mainpage_operation_no
  is '�������';
comment on column IEM_MAINPAGE_OPERATION_SX.iem_mainpage_no
  is '�������';
comment on column IEM_MAINPAGE_OPERATION_SX.operation_code
  is '��������';
comment on column IEM_MAINPAGE_OPERATION_SX.operation_name
  is '��������';
comment on column IEM_MAINPAGE_OPERATION_SX.execute_user1
  is '����';
comment on column IEM_MAINPAGE_OPERATION_SX.execute_user2
  is 'һ��';
comment on column IEM_MAINPAGE_OPERATION_SX.execute_user3
  is '����';
comment on column IEM_MAINPAGE_OPERATION_SX.anaesthesia_type_id
  is '����ʽ';
comment on column IEM_MAINPAGE_OPERATION_SX.close_level
  is '�п����ϵȼ�';
comment on column IEM_MAINPAGE_OPERATION_SX.anaesthesia_user
  is '����ҽʦ';
comment on column IEM_MAINPAGE_OPERATION_SX.valide
  is '�Ƿ���Ч0-��Ч  1-��Ч';
comment on column IEM_MAINPAGE_OPERATION_SX.create_user
  is '������';
comment on column IEM_MAINPAGE_OPERATION_SX.create_time
  is '����ʱ��';
comment on column IEM_MAINPAGE_OPERATION_SX.cancel_user
  is 'ɾ����';
comment on column IEM_MAINPAGE_OPERATION_SX.cancel_time
  is 'ɾ��ʱ��';
comment on column IEM_MAINPAGE_OPERATION_SX.operation_level
  is '�����ȼ�';
prompt Done.
