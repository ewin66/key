insert into APPCFG (CONFIGKEY, NAME, VALUE, DESCRIPT, PARAMTYPE, CFGKEYSET, DESIGN, CLIENTFLAG, HIDE, VALID, CREATEDATE, SORTID)
values ('isCnMaiPage', '�Ƿ�ʹ����ҽ������ҳ', '1', '1����ҽ������ҳ��0����ҽ������ҳ', 1, null, null, 0, 0, 1, null, null);
-- Add/modify columns 
alter table IEM_MAINPAGE_DIAGNOSIS_SX add diagnosis_orien NUMBER(1);
-- Add comments to the columns 
comment on column IEM_MAINPAGE_DIAGNOSIS_SX.diagnosis_orien
  is '��Ϸ�λ��1.��� 2.�Ҳ� 3.���� 4.˫��';