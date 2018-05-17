CREATE OR REPLACE PACKAGE EMR_VIEW_TOOL IS
  TYPE empcurtype IS REF CURSOR;

  --**********************************���γ�Ժ��������**************************************
  --��ȡ���˻�����Ϣ
  PROCEDURE usp_inpatient_info(v_patnoofhis VARCHAR2,
                               o_result     OUT empcurtype);

  --��ȡ��ʷ����
  PROCEDURE usp_history_inpatient(v_patnoofhis VARCHAR2,
                                  o_result     OUT empcurtype);

  --��ȡ���˲���
  PROCEDURE usp_inpatient_emr(v_noofinpat VARCHAR2,
                              o_result    OUT empcurtype);

  --��ȡ���˲���
  PROCEDURE usp_inpatient_emr2(v_id VARCHAR2, o_result OUT empcurtype);

  --**********************************�ż�����ʷ��������**************************************

  --��ȡ���˻�����Ϣ�����
  PROCEDURE usp_inpatient_info_clinic(v_patnoofhis VARCHAR2,
                                      o_result     OUT empcurtype);

  --��ȡ��ʷ���ˡ����
  PROCEDURE usp_history_inpatient_clinic(v_patnoofhis VARCHAR2,
                                         o_result     OUT empcurtype);

  --��ȡ���˲��������
  PROCEDURE usp_inpatient_emr_clinic(v_noofinpat VARCHAR2,
                                     o_result    OUT empcurtype);

  --��ȡ���˲��������
  PROCEDURE usp_inpatient_emr2_clinic(v_id     VARCHAR2,
                                      o_result OUT empcurtype);

  --��ȡ������Ӳ����ĺ�Ԫ�ص�ֵ
  PROCEDURE usp_macro_inpatient_clinic(v_noofinpat VARCHAR2,
                                       o_result    OUT empcurtype);

  --********************************������Ϣά��******************************************
  --��ȡ������Ӳ����ĺ�Ԫ�ص�ֵ
  PROCEDURE usp_get_inpatient_list(v_deptID         VARCHAR2,
                                   v_visitTypeID    VARCHAR2,
                                   v_name           VARCHAR2,
                                   v_patID          VARCHAR2,
                                   v_visitTimeBegin date,
                                   v_visitTimeEnd   date,
                                   o_result         OUT empcurtype);

  --ͨ�������Ż�������ץȡ����
  PROCEDURE usp_get_inpatient_list2(v_patID  VARCHAR2,
                                    v_name   VARCHAR2,
                                    o_result OUT empcurtype);

  --��ȡ��������б�                                   
  PROCEDURE usp_get_dept_list(o_result OUT empcurtype);

  --********************************���ԣ����ڽ���XML*************************************
  PROCEDURE usp_xml_content(v_recordDetailID varchar2,
                            v_roElementName  varchar2,
                            o_result         OUT empcurtype);

END; -- Package spec                                             -- Package spec
/
CREATE OR REPLACE PACKAGE BODY EMR_VIEW_TOOL IS
  --**********************************���γ�Ժ��������**************************************
  --��ȡ���˻�����Ϣ
  PROCEDURE usp_inpatient_info(v_patnoofhis VARCHAR2,
                               o_result     OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT i.name, decode(i.sexid, 1, '��', 2, 'Ů', 'δ֪') sex, i.birth
        FROM inpatient i
       WHERE i.patnoofhis = v_patnoofhis;
  END;

  --��ȡ��ʷ����
  PROCEDURE usp_history_inpatient(v_patnoofhis VARCHAR2,
                                  o_result     OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT i1.NAME inpatientname, i1.noofinpat, i1.inwarddate
        FROM inpatient i1
       WHERE EXISTS (SELECT 1
                FROM inpatient i2
               WHERE i2.patnoofhis = v_patnoofhis
                 AND i2.noofclinic = i1.noofclinic)
       ORDER BY i1.inwarddate DESC;
  END;

  --��ȡ���˲���
  PROCEDURE usp_inpatient_emr(v_noofinpat VARCHAR2,
                              o_result    OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT r.NAME, r.ID
        FROM recorddetail r
       WHERE r.noofinpat = v_noofinpat
         AND r.valid = 1
         AND (r.name like '%��Ժ%');
  END;

  --��ȡ���˲���
  PROCEDURE usp_inpatient_emr2(v_id VARCHAR2, o_result OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT r.content
        FROM recorddetail r
       WHERE r.ID = v_id
         AND r.valid = 1;
  END;

  --**********************************�ż�����ʷ��������**************************************

  --��ȡ���˻�����Ϣ�����
  PROCEDURE usp_inpatient_info_clinic(v_patnoofhis VARCHAR2,
                                      o_result     OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT i.name, decode(i.sexid, 1, '��', 2, 'Ů', 'δ֪') sex, i.birth
        FROM inpatient_clinic i
       WHERE i.patnoofhis = v_patnoofhis;
  END;

  --��ȡ��ʷ���ˡ����
  PROCEDURE usp_history_inpatient_clinic(v_patnoofhis VARCHAR2,
                                         o_result     OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT i1.NAME            inpatientname,
             i1.noofinpatclinic noofinpat,
             i1.visittime       inwarddate
        FROM inpatient_clinic i1
       WHERE EXISTS (SELECT 1
                FROM inpatient_clinic i2
               WHERE i2.patnoofhis = v_patnoofhis
                 AND i2.patid = i1.patid)
         AND i1.patnoofhis != v_patnoofhis
         AND i1.valid = '1'
       ORDER BY i1.visittime DESC;
  END;

  --��ȡ���˲��������
  PROCEDURE usp_inpatient_emr_clinic(v_noofinpat VARCHAR2,
                                     o_result    OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT r.NAME, r.ID
        FROM recorddetail_clinic r
       WHERE r.noofinpatclinic = v_noofinpat
         AND r.valid = 1
       ORDER BY r.id;
  END;

  --��ȡ���˲��������
  PROCEDURE usp_inpatient_emr2_clinic(v_id     VARCHAR2,
                                      o_result OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      SELECT r.content
        FROM recorddetail_clinic r
       WHERE r.ID = v_id
         AND r.valid = 1;
  END;

  --��ȡ������Ӳ����ĺ�Ԫ�ص�ֵ
  PROCEDURE usp_macro_inpatient_clinic(v_noofinpat VARCHAR2,
                                       o_result    OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select ic.name           PatName,
             dd1.name          PatSexName,
             ic.patid          PatID,
             ic.age            PatAge,
             ic.bedcode        PatBedCode,
             dd2.name          PatMaritalName,
             ic.country        PatCountryName,
             ic.nationality    PatNationalityName,
             ic.jobname        PatJobName,
             ic.contactaddress PatContactAddress
        from inpatient_clinic ic
        left outer join dictionary_detail dd1
          on dd1.categoryid = '3'
         and dd1.detailid = ic.sexid
         and dd1.valid = '1'
        left outer join dictionary_detail dd2
          on dd2.categoryid = '4'
         and dd2.detailid = ic.maritalid
         and dd2.valid = '1'
       where ic.noofinpatclinic = v_noofinpat
         and ic.valid = '1';
  END;

  --********************************������Ϣά��******************************************
  --��ȡ������Ӳ����ĺ�Ԫ�ص�ֵ
  PROCEDURE usp_get_inpatient_list(v_deptID         VARCHAR2,
                                   v_visitTypeID    VARCHAR2,
                                   v_name           VARCHAR2,
                                   v_patID          VARCHAR2,
                                   v_visitTimeBegin date,
                                   v_visitTimeEnd   date,
                                   o_result         OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select ic.noofinpatclinic,
             ic.visittype visittypeid,
             dd1.name visittypename,
             trunc(ic.visittime) visittime,
             ic.visitno,
             ic.roomname,
             ic.name,
             ic.patid,
             dd2.name sexname,
             ic.birth,
             ic.age,
             dd3.name maritalname,
             d.name deptname,
             ic.nationality,
             ic.country,
             ic.healthcardid,
             ic.contactaddress,
             ic.jobname,
             ic.visitdoctorid || '_' || u.name visitdoctorname,
             ic.bedcode
        from inpatient_clinic ic
        left outer join dictionary_detail dd1
          on dd1.categoryid = '13'
         and dd1.detailid = ic.visittype
        left outer join dictionary_detail dd2
          on dd2.categoryid = '3'
         and dd2.detailid = ic.sexid
        left outer join dictionary_detail dd3
          on dd3.categoryid = '4'
         and dd3.detailid = ic.maritalid
        left outer join users u
          on u.id = ic.visitdoctorid
        left outer join department d
          on d.id = ic.deptid
       where ic.deptid = v_deptID
         and ic.visittype = v_visitTypeID
         and ic.name like '%' || v_name || '%'
         and ic.patid like '%' || v_patID || '%'
         and trunc(ic.visittime) >= trunc(v_visitTimeBegin)
         and trunc(ic.visittime) <= trunc(v_visitTimeEnd)
         and ic.valid = '1'
       order by trunc(ic.visittime), ic.visitno desc;
  END;

  --ͨ�������Ż�������ץȡ����
  PROCEDURE usp_get_inpatient_list2(v_patID  VARCHAR2,
                                    v_name   VARCHAR2,
                                    o_result OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select ic.noofinpatclinic,
             ic.visittype visittypeid,
             dd1.name visittypename,
             trunc(ic.visittime) visittime,
             ic.visitno,
             ic.roomname,
             ic.name,
             ic.patid,
             dd2.name sexname,
             ic.birth,
             ic.age,
             dd3.name maritalname,
             d.name deptname,
             ic.nationality,
             ic.country,
             ic.healthcardid,
             ic.contactaddress,
             ic.jobname,
             ic.visitdoctorid || '_' || u.name visitdoctorname,
             ic.bedcode
        from inpatient_clinic ic
        left outer join dictionary_detail dd1
          on dd1.categoryid = '13'
         and dd1.detailid = ic.visittype
        left outer join dictionary_detail dd2
          on dd2.categoryid = '3'
         and dd2.detailid = ic.sexid
        left outer join dictionary_detail dd3
          on dd3.categoryid = '4'
         and dd3.detailid = ic.maritalid
        left outer join users u
          on u.id = ic.visitdoctorid
        left outer join department d
          on d.id = ic.deptid
       where ((v_patID is not null and ic.patid = v_patID) or
             (v_name is not null and ic.name = v_name))
         and ic.valid = '1';
  END;

  --��ȡ��������б�                                   
  PROCEDURE usp_get_dept_list(o_result OUT empcurtype) AS
  BEGIN
    OPEN o_result FOR
      select d.id, d.name, d.py, d.wb
        from department d
       where d.sort not in ('102', '103', '104')
         and d.valid = '1'
       order by d.id;
  END;

  --********************************���ԣ����ڽ���XML*************************************  
  PROCEDURE usp_xml_content(v_recordDetailID varchar2,
                            v_roElementName  varchar2,
                            o_result         OUT empcurtype) AS
    --����XML������ʵ��XMLPARSER.parser
    xmlPar XMLPARSER.parser := XMLPARSER.NEWPARSER;
    --����DOM�ĵ�����
    doc xmldom.DOMDocument;
  
    --roElementԪ��
    roElementNode xmldom.DOMNode;
  
    --pԪ�ؼ���
    paragraphElementNodes xmldom.DOMNodeList;
    --pԪ�ؼ��ϵ�����
    paragraphElementCount integer;
    --pԪ��
    paragraphElementNode xmldom.DOMNode;
  
    --pԪ�ص������ӽڵ㼯��
    childElementNodes xmldom.DOMNodeList;
    --pԪ�ص������ӽڵ����
    childElementNodesCount integer;
  
    --selementԪ�ؼ���
    selementNodes xmldom.DOMNodeList;
    --selementԪ�ؼ��ϸ���
    selementNodesCount integer;
    selementNode       xmldom.DOMNode;
    selementValue      nvarchar2(200);
  
    xmlClobData clob;
    v_result    nvarchar2(4000);
  
    --�����߼��жϵı�־λ
    isEnter integer := 0;
  
    --�ڵ����Լ���
    nodeAttributes xmldom.DOMNamedNodeMap;
  BEGIN
  
    --��ȡxml���ݣ�clob�ֶ��л�ȡ��������
    select content
      into xmlClobData
      from recorddetail
     where id = v_recordDetailID;
  
    --����xml����
    xmlparser.parseClob(xmlPar, xmlClobData);
    doc := xmlparser.getDocument(xmlPar);
  
    --�ͷŽ�����ʵ��
    xmlparser.freeParser(xmlPar);
  
    --��ȡ����PԪ��
    paragraphElementNodes := xmldom.getElementsByTagName(doc, 'p');
    paragraphElementCount := xmldom.getLength(paragraphElementNodes);
  
    --ѭ������ 
    For paragraphIndex in 0 .. paragraphElementCount - 1 LOOP
      --********ѭ������ BEGIN********
      BEGIN
        --��ȡ��ǰ����
        paragraphElementNode := xmldom.item(paragraphElementNodes,
                                            paragraphIndex);
        --��ȡ����������Ԫ��
        childElementNodes      := xmldom.getChildNodes(paragraphElementNode);
        childElementNodesCount := xmldom.getLength(childElementNodes);
        FOR childElementNodesIndex in 0 .. childElementNodesCount - 1 LOOP
          --********�����е�Ԫ�� BEGIN********
          BEGIN
            roElementNode := xmldom.item(childElementNodes,
                                         childElementNodesIndex);
          
            IF isEnter = 0 THEN
              --�ҵ���ʼRoElementԪ��
              BEGIN
                --��ȡ�����е�roElementԪ��
                IF (xmldom.getNodeName(roElementNode) = 'roelement') THEN
                  BEGIN
                    nodeAttributes := xmldom.getAttributes(roElementNode);
                  
                    --��ȡname == v_roleElementName ��roELementԪ��
                    IF (xmldom.getNodeValue(xmldom.getNamedItem(nodeAttributes,
                                                                'name')) =
                       v_roElementName) THEN
                      BEGIN
                        isEnter := 1;
                      END;
                    END IF;
                  END;
                END IF;
              END;
            ELSIF isEnter = 1 THEN
              BEGIN
                --�ж��Ƿ����������roElement�����������Ҫ�˳�ѭ��
                IF (xmldom.getNodeName(roElementNode) = 'roelement') THEN
                  BEGIN
                    isEnter := 2;
                    EXIT;
                  END;
                ELSE
                  BEGIN
                    IF (xmldom.getNodeName(roElementNode) = 'selement') THEN
                      --�����"��ѡ��"��Ҫ�����⴦��
                      BEGIN
                        nodeAttributes := xmldom.getAttributes(roElementNode);
                        selementValue  := xmldom.getNodeValue(xmldom.getNamedItem(nodeAttributes,
                                                                                  'text')); --��ȡselementԪ�ص�text���Ե�ֵ
                      
                        selementNodes      := xmldom.getChildNodes(roElementNode);
                        selementNodesCount := xmldom.getLength(selementNodes);
                      
                        FOR selementNodesIndex in 0 .. selementNodesCount - 1 LOOP
                          --ѭ��selementԪ�ص�������Ԫ��item
                          selementNode   := xmldom.item(selementNodes,
                                                        selementNodesIndex);
                          nodeAttributes := xmldom.getAttributes(selementNode);
                          IF xmldom.getNodeValue(xmldom.getNamedItem(nodeAttributes,
                                                                     'selected')) =
                             'true' THEN
                            --�ҳ�item������selectedΪtrue�Ľڵ�
                            selementValue := xmldom.getNodeValue(xmldom.getFirstChild(selementNode));
                          END IF;
                        END LOOP;
                      
                        v_result := v_result || selementValue;
                      END;
                    ELSIF (xmldom.getNodeName(roElementNode) = 'btnelement') THEN
                      --����ǡ���ť����Ҫ�����⴦��
                      BEGIN
                        nodeAttributes := xmldom.getAttributes(roElementNode);
                        selementValue  := xmldom.getNodeValue(xmldom.getNamedItem(nodeAttributes,
                                                                                  'print')); --��ȡbtnelementԪ�ص�print���Ե�ֵ
                        IF selementValue != 'False' THEN
                          BEGIN
                            v_result := v_result ||
                                        xmldom.getNodeValue(xmldom.getFirstChild(roElementNode));
                          END;
                        END IF;
                      END;
                    ELSE
                      v_result := v_result ||
                                  xmldom.getNodeValue(xmldom.getFirstChild(roElementNode));
                    END IF;
                  END;
                END IF;
              END;
            END IF;
          END;
          --********�����е�Ԫ�� END********
        END LOOP;
      END;
      --********ѭ������ END********
    END LOOP;
  
    OPEN o_result FOR
      select ltrim(ltrim(v_result, '��'), ':') as content from dual;
  END;

END;
/
