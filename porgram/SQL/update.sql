insert into APPCFG (CONFIGKEY, NAME, VALUE, DESCRIPT, PARAMTYPE, CFGKEYSET, DESIGN, CLIENTFLAG, HIDE, VALID, CREATEDATE, SORTID)
values ('isCnMaiPage', '�Ƿ�ʹ����ҽ������ҳ', '1', '1����ҽ������ҳ��0����ҽ������ҳ', 1, null, null, 0, 0, 1, null, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('1', null, '����Ŀ', 'AXM', 'RAH', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('2', null, '������', 'DBZ', 'UUT', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('3', null, '�����ַ�ֵ', 'ABZBZ', 'RUTWW', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('4', null, '���������ط��飨DRG��', 'JBZDXGBZDRG', 'UUYOSUWXdrg', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('5', null, '������', 'ACR', 'RYJ', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('6', null, '����ͷ', 'ART', 'RWU', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('9', null, '����', 'QT', 'AW', 'bb', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('0', null, '��ͨ', 'PT', 'UC', '94', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('1', null, '����סԺ�ľ����໼��', 'ZQZYDJSLHZ', 'TAWBROPOKF', '94', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('2', null, '���չػ�����', 'LZGHBC', 'JXUNUY', '94', 1, null);

insert into dictionary_detail (DETAILID, MAPID, NAME, PY, WB, CATEGORYID, VALID, MEMO)
values ('3', null, '���ڿ���סԺ����', 'ZQKFZYBC', 'TAYTWBUY', '94', 1, null);
-- Add/modify columns 
alter table IEM_MAINPAGE_DIAGNOSIS_SX add diagnosis_orien NUMBER(1);
-- Add comments to the columns 
comment on column IEM_MAINPAGE_DIAGNOSIS_SX.diagnosis_orien
  is '��Ϸ�λ��1.��� 2.�Ҳ� 3.���� 4.˫��';
  
-- Add/modify columns 
alter table INPATIENT add PAYWAY VARCHAR2(1);
-- Add comments to the columns 
comment on column INPATIENT.PAYWAY
  is 'ҽ��֧����ʽ';
  
-- Add/modify columns 
alter table INPATIENT add specas varchar2(1);
-- Add comments to the columns 
comment on column INPATIENT.specas
  is '���ⲡ��';
  
  -- Add/modify columns 
alter table IEM_MAINPAGE_BASICINFO_SX add hospital_sense VARCHAR2(12);
-- Add comments to the columns 
comment on column IEM_MAINPAGE_BASICINFO_SX.hospital_sense
  is 'Ժ�ڸ�Ⱦ����';
  
    -- Add/modify columns 
alter table IEM_MAINPAGE_BASICINFO_SX add hospital_sense_name VARCHAR2(50);
-- Add comments to the columns 
comment on column IEM_MAINPAGE_BASICINFO_SX.hospital_sense_name
  is 'Ժ�ڸ�Ⱦ';
  