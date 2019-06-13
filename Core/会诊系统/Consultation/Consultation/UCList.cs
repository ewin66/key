using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DrectSoft.FrameWork.WinForm.Plugin;
using DrectSoft.Wordbook;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DrectSoft.Common;
using Consultation.NEW;
using DrectSoft.Common.Ctrs.DLG;
using DrectSoft.DSSqlHelper;
using DrectSoft.Core.Consultation.NEW.Enum;
using DrectSoft.Service;

namespace DrectSoft.Core.Consultation
{
    public partial class UCList : DevExpress.XtraEditors.XtraUserControl
    {
        private IEmrHost m_App;

        /// <summary>
        /// ���캯��
        /// </summary>
        public UCList()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ���ι��캯��
        /// </summary>
        /// <param name="app"></param>
        public UCList(IEmrHost app)
            : this()
        {
            try
            {
                m_App = app;
                BindPopupMenu();
                BindLookUpEditorData();
                simpleButtonSearch.Click += new EventHandler(simpleButtonSearch_Click);
                InitGridControl();
                InitDateEdit();
                //Search();
                textEditName.Focus();
                DS_SqlHelper.CreateSqlHelper();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ʼ���б�
        /// </summary>
        private void InitGridControl()
        {
            try
            {
                gridViewList.OptionsSelection.EnableAppearanceFocusedRow = true;
                gridViewList.OptionsSelection.EnableAppearanceFocusedCell = false; ;
                gridViewList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                gridViewList.DoubleClick += new EventHandler(gridViewList_DoubleClick);
                gridViewList.MouseDown += new MouseEventHandler(gridViewList_MouseDown);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ʼ��ʱ��
        /// edit by xlb 2013-03-15
        /// </summary>
        private void InitDateEdit()
        {
            try
            {
                dateEditConsultDateBegin.EditValue = System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                dateEditConsultDateEnd.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �󶨿ؼ��б�����Ԫ
        /// </summary>
        private void BindLookUpEditorData()
        {
            try
            {
                BindUrgency();
                BindBed();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// У�鷽��
        /// Add by xlb 2013-03-13
        /// </summary>
        private bool Check(ref string message)
        {
            try
            {
                //��ʼʱ��
                DateTime dateBegin = dateEditConsultDateBegin.DateTime.Date;
                //����ʱ��
                DateTime dateEnd = dateEditConsultDateEnd.DateTime.Date;
                if (dateBegin > dateEnd)
                {
                    message = "��ʼʱ�䲻�ܴ��ڽ���ʱ��";
                    dateEditConsultDateBegin.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ���Ҽ��˵�
        private DevExpress.XtraBars.BarButtonItem barButtonItemConsultationInfo;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMedicalInfo;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCancelMyApplyInfo;// �����Ļ���ȡ��

        /// <summary>
        /// ���Ҽ��˵�
        /// </summary>
        private void BindPopupMenu()
        {
            try
            {
                barButtonItemConsultationInfo = new DevExpress.XtraBars.BarButtonItem();
                barButtonItemConsultationInfo.Caption = "������Ϣ";
                barButtonItemConsultationInfo.Id = 1;
                barButtonItemConsultationInfo.Name = "ConsultationInfo";
                barButtonItemConsultationInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemConsultationInfo_ItemClick);

                barButtonItemMedicalInfo = new DevExpress.XtraBars.BarButtonItem();
                barButtonItemMedicalInfo.Caption = "������Ϣ";
                barButtonItemMedicalInfo.Id = 2;
                barButtonItemMedicalInfo.Name = "MedicalInfo";
                barButtonItemMedicalInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemMedicalInfo_ItemClick);

                //��������ȡ���˵�
                barButtonItemCancelMyApplyInfo = new DevExpress.XtraBars.BarButtonItem();
                barButtonItemCancelMyApplyInfo.Caption = "ȡ������";
                barButtonItemCancelMyApplyInfo.Id = 3;
                barButtonItemCancelMyApplyInfo.Name = "CancelConsuApply";
                barButtonItemCancelMyApplyInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemCancelMyApplyInfo_ItemClick);


                popupMenuList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemConsultationInfo),
                new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemMedicalInfo),
                new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCancelMyApplyInfo)
            });

                barManagerList.Items.AddRange(new DevExpress.XtraBars.BarItem[] { 
                barButtonItemConsultationInfo, 
                barButtonItemMedicalInfo,
                barButtonItemCancelMyApplyInfo
            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region �󶨻�������̶�
        private void BindUrgency()
        {
            try
            {
                lookUpEditorUrgency.Kind = WordbookKind.Sql;
                lookUpEditorUrgency.ListWindow = lookUpWindowUrgency;
                BindUrgencyWordBook(GetConsultationData("6", "66"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindUrgencyWordBook(DataTable dataTableData)
        {
            try
            {
                for (int i = 0; i < dataTableData.Columns.Count; i++)
                {
                    if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "name")
                    {
                        dataTableData.Columns[i].Caption = "������";
                    }
                }

                Dictionary<string, int> colWidths = new Dictionary<string, int>();
                colWidths.Add("NAME", 120);
                SqlWordbook wordBook = new SqlWordbook("Urgency", dataTableData, "ID", "NAME", colWidths, "ID//NAME//PY//WB");
                lookUpEditorUrgency.SqlWordbook = wordBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region �󶨵�ǰ�����Ĵ�λ

        private void BindBed()
        {
            try
            {
                lookUpEditorBed.Kind = WordbookKind.Sql;
                lookUpEditorBed.ListWindow = lookUpWindowBed;
                BindBedWordBook(GetConsultationData("7", m_App.User.CurrentWardId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindBedWordBook(DataTable dataTableData)
        {
            try
            {
                for (int i = 0; i < dataTableData.Columns.Count; i++)
                {
                    if (dataTableData.Columns[i].ColumnName.ToLower().Trim() == "id")
                    {
                        dataTableData.Columns[i].Caption = "��λ��";
                    }
                }

                Dictionary<string, int> colWidths = new Dictionary<string, int>();
                colWidths.Add("ID", 120);
                SqlWordbook wordBook = new SqlWordbook("Bed", dataTableData, "ID", "ID", colWidths, "ID");
                lookUpEditorBed.SqlWordbook = wordBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private DataTable GetConsultationData(string typeID, string param)
        {
            try
            {
                if (Dal.DataAccess.App == null)
                {
                    Dal.DataAccess.App = m_App;
                }
                DataTable dataTableConsultationData = new DataTable();
                dataTableConsultationData = Dal.DataAccess.GetConsultationData("", typeID, param);
                return dataTableConsultationData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ѯ��ť�¼� 
        /// Edit by xlb 2013-03-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                if (!Check(ref message))
                {
                    MessageBox.Show(message);
                    return;
                }
                Search();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��ѯ
        /// edit by xlb 2013-03-18
        /// </summary>
        private void Search()
        {
            try
            {
                string consultTimeBegin = dateEditConsultDateBegin.DateTime.ToString("yyyy-MM-dd");
                string consultTimeEnd = dateEditConsultDateEnd.DateTime.ToString("yyyy-MM-dd");
                string consultType = "";
                string urgency = lookUpEditorUrgency.CodeValue;
                string name = textEditName.Text;
                string patientSN = textEditPatientSN.Text;
                string bedCode = lookUpEditorBed.CodeValue;

                List<string> userList =DS_SqlService.GetConfigValueByKey("TimeLimitUsers").Split(',').ToList();
                string deptID = m_App.User.CurrentDeptId;
                if (null != userList && userList.Count > 0)
                {
                    if (userList.Contains(m_App.User.Id) || (m_App.User.Id.Length >= 6 && userList.Contains(m_App.User.Id.Substring(m_App.User.Id.Length - 4, 4))))
                    {
                        deptID = null;
                    }
                }
                DataTable dt = GetConsultationDataLimit("22", consultTimeBegin, consultTimeEnd, consultType, urgency, name, patientSN, bedCode, deptID);
                //dt = Dal.DataAccess.GetConsultationData("22", consultTimeBegin, consultTimeEnd, consultType, urgency, name, patientSN, bedCode, m_App.User.CurrentDeptId);
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    string statusID = dt.Rows[i]["StateID"].ToString();
                    string qiandao = dt.Rows[i]["ISSIGNIN"].ToString();
                    if (statusID == Convert.ToString((int)ConsultStatus.ApplySave))
                    {
                        dt.Rows.RemoveAt(i);
                        continue;
                    }
                    string fukuan = dt.Rows[i]["ISPAY"].ToString();
                    if (!checkEditHaveConsultation.Checked)//�ѻ���
                    {
                        if (statusID == Convert.ToString((int)ConsultStatus.RecordeComplete)
                            || statusID == Convert.ToString((int)ConsultStatus.RecordeSave))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                    }

                    if (!checkEditReject.Checked)//�ѷ��
                    {
                        if (statusID == Convert.ToString((int)ConsultStatus.Reject))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                    }
                    //add by ywk  �����ѳ���״̬ɸѡ
                    if (!chkchexiao.Checked)//�ѷ��
                    {
                        if (statusID == Convert.ToString((int)ConsultStatus.UndoConsultion))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                    }

                    // edit by tj 2012-10-26 
                    if (CommonObjects.IsNeedVerifyInConsultation)
                    {
                        if (!checkEditWaitApprove.Checked)//��ǩ��
                        {
                            if (statusID == Convert.ToString((int)ConsultStatus.WaitApprove))
                            {
                                dt.Rows.RemoveAt(i);
                                continue;
                            }
                        }
                    }

                    if (!checkEditWaitConsultation.Checked)//������
                    {
                        if (statusID == Convert.ToString((int)ConsultStatus.WaitConsultation))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                    }

                    if (!chkCancel.Checked)//��ȡ��
                    {
                        if (statusID == Convert.ToString((int)ConsultStatus.CancelConsultion))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                    }
                    // add ǩ���ͽɷ�״̬���� by xlb 2012-12-11
                    if (!chkqiandao.Checked)//ǩ��
                    {

                    }
                    else
                    {
                        if (qiandao.Trim() == "δǩ��" || qiandao.Trim().Contains("δǩ��"))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                    }
                    if (!chkfukuan.Checked)//�ɷ�״̬
                    {
                        continue;
                    }
                    else
                    {
                        if (fukuan.Trim() == "δ����" || fukuan.Trim().Contains("δ����"))
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }

                    }
                }
                gridControlList.DataSource = dt;
                if (dt == null || dt.Rows.Count == 0)
                {
                   MessageBox.Show("δ�ҵ���Ӧ��¼");
                }
                m_App.PublicMethod.ConvertGridDataSourceUpper(gridViewList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ȡȫ�����ҵļ�¼(�ʿ���ԱȨ��)
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-15</date>
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="consultTimeBegin"></param>
        /// <param name="consultTimeEnd"></param>
        /// <param name="consultTypeID"></param>
        /// <param name="urgencyTypeID"></param>
        /// <param name="name"></param>
        /// <param name="patID"></param>
        /// <param name="bedID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private DataTable GetConsultationDataLimit(string typeID, string consultTimeBegin, string consultTimeEnd, string consultTypeID, string urgencyTypeID, string name, string patID, string bedID, string deptID)
        {
            try
            {
                //edit by ywk 2012��11��5��9:18:50 ����ҽԺ�Ļ������ݻ���ʱ��Ҫȡ����ʱ��  edit by wangj 2013 2 22 ����ʱ�䵼�³����ظ�����   �޸� edit by wangj 2013 3 1 �������ҽʦǰ���Ӧ   
                string sqlStr = @"select distinct inp.Name as PatientName, inp.PatID, inp.OutBed,ca.ConsultApplySn,ca.ispassed, ca.NoOfInpat, ca.UrgencyTypeID, cd2. Name  as UrgencyTypeName,ca.ConsultTypeID, cd1. Name  as ConsultTypeName, ca.Abstract, ca.Purpose,
                                ca.ApplyUser, u1. Name  as ApplyUserName, ca.ApplyTime, ca.Director,u2. Name  as DirectorName,ca.consulttime,/*cf.reachtime as ConsultTime,*/ ca.ConsultLocation, ca.StateID,
                                cd3. Name  as StatusName, ca.ConsultSuggestion, ca.FinishTime, ca.RejectReason,(case ca.ISPAY when 0 then 'δ����' when 1 then '�Ѹ���' else 'δ����' end) ISPAY,ca.Valid, 
                                ca.CreateUser, ca.CreateTime,cap.appdept,crp.appname,crp.ISSIGNIN,crp.REACHTIME,d.id as DeptID,d.Name as DeptName
                                from ConsultApply ca 
                                left join (select consultapplysn,
                    wmsys.wm_concat(case ISSIGNIN
                                      when '1' then
                                       '��ǩ��'
                                      when '0' then
                                       'δǩ��'
                                      else
                                       'δǩ��'
                                    end) ISSIGNIN,
                    wm_concat(REACHTIME) REACHTIME,wmsys.wm_concat(EMPLOYEENAME) appname
               from consultrecorddepartment
              where VALID = 1  
              group by consultapplysn) crp
    on ca.CONSULTAPPLYSN = crp.CONSULTAPPLYSN
                                left join (select sn, wmsys.wm_concat(deptname) appdept from 
                                  (select distinct consultapplysn sn, DEPARTMENTNAME deptname from  consultrecorddepartment where VALID = 1) GROUP BY SN) 
                                  cap on ca.CONSULTAPPLYSN = cap.sn
                                  
                                left outer join CategoryDetail cd1 on cd1.CategoryID = '65' and cd1.ID = ca.ConsultTypeID
                                left outer join CategoryDetail cd2 on cd2.CategoryID = '66' and cd2.ID = ca.UrgencyTypeID
                                left outer join CategoryDetail cd3 on cd3.CategoryID = '67' and cd3.ID = ca.StateID
                                left outer join Users u1 on u1.ID = ca.ApplyUser and u1.Valid = '1'
                                left outer join Users u2 on u2.ID = ca.Director and u2.Valid = '1'
                                left outer join InPatient inp on inp.NoOfInpat = ca.NoOfInpat
                                left outer join Department d on d.ID = u1.DeptId and d.Valid = '1' 
                                    left outer join consultrecorddepartment cf on cf.consultapplysn=ca.consultapplysn and cf.valid='1'
                            where ca.Valid = '1' ";
                sqlStr += @" and inp. Status  in ('1500','1501','1502','1504','1505','1506','1507')  and exists
  (select *
    from ConsultApplyDepartment cad
    where cad.ConsultApplySn = ca.ConsultApplySn )";
                if (!string.IsNullOrEmpty(consultTimeBegin))
                {
                    sqlStr += @" and to_char(to_date(ca.ConsultTime, 'yyyy-mm-dd HH24:mi:ss'), 'yyyy-mm-dd HH24:mi:ss') >= '" + consultTimeBegin + " 00:00:00" + "' ";
                }
                if (!string.IsNullOrEmpty(consultTimeEnd))
                {
                    sqlStr += @" and to_char(to_date(ca.ConsultTime, 'yyyy-mm-dd HH24:mi:ss'), 'yyyy-mm-dd HH24:mi:ss') <= '" + consultTimeEnd + " 23:59:59" + "' ";
                }
                if (!string.IsNullOrEmpty(consultTypeID))
                {
                    sqlStr += @" and ca.ConsultTypeID= '" + consultTypeID + "' ";
                }
                if (!string.IsNullOrEmpty(urgencyTypeID))
                {
                    sqlStr += @" and ca.UrgencyTypeID='" + urgencyTypeID + "' ";
                }
                if (!string.IsNullOrEmpty(deptID))
                {
                    sqlStr += @" and d.id ='" + deptID + "' ";
                }
                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr += @" and inp.Name  like '%" + name + "%' ";
                }
                if (!string.IsNullOrEmpty(patID))
                {
                    sqlStr += @" and inp.PatID like '%" + patID + "%' ";
                }
                if (!string.IsNullOrEmpty(bedID))
                {
                    sqlStr += @" and inp.OutBed = '" + bedID + "' ";
                }

                sqlStr += " order by ConsultTime ";
                return m_App.SqlHelper.ExecuteDataTable(sqlStr, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ˫���б��¼�
        /// edit by Yanqiao.Cai 2012-11-05
        /// add try ... catch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //edit by Yanqiao.Cai 2012-11-05 ˫�������¼�(ֱ�ӷ���)
                GridHitInfo hitInfo = gridViewList.CalcHitInfo(gridControlList.PointToClient(Cursor.Position));
                if (hitInfo.RowHandle < 0)
                {
                    return;
                }
                ShowConsultationInfo();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �鿴������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barButtonItemMedicalInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DataRow dr = gridViewList.GetDataRow(gridViewList.FocusedRowHandle);
                if (dr != null)
                {
                    string noOfFirstPage = dr["NoOfInpat"].ToString();
                    string stateID = dr["StateID"].ToString();
                    string deptName = dr["DeptName"].ToString();

                    if (stateID == Convert.ToString((int)ConsultStatus.WaitConsultation))
                    {
                        if (deptName != m_App.User.CurrentDeptName)
                        {
                            m_App.EmrAllowEdit = false;
                        }
                        m_App.ChoosePatient(Convert.ToDecimal(noOfFirstPage), FloderState.None.ToString());

                        m_App.LoadPlugIn("DrectSoft.Core.MainEmrPad.dll", DS_BaseService.GetUCEmrInputPath());
                        m_App.EmrAllowEdit = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ����ȡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barButtonItemCancelMyApplyInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (m_App.CustomMessageBox.MessageShow(string.Format("�Ƿ�ȷ��ȡ�������뵥��"), CustomMessageBoxKind.QuestionOkCancel) == DialogResult.OK)
                {
                    DataRow dr = gridViewList.GetDataRow(gridViewList.FocusedRowHandle);
                    if (dr != null)
                    {
                        string ConsApplyNo = dr["ConsultApplySn"].ToString();

                        //if (dr["ispassed"].ToString()=="1")//�Ѿ����ͨ���Ĳ���ȡ�� ywk 2013��6��13�� 15:19:43
                        //{
                        //    m_App.CustomMessageBox.MessageShow("�����¼�Ѿ���ˣ��޷�ȡ��");
                        //    return;
                        //}
                        //������ʹ���˿�ȡ������

                        if (dr["IsPay"].ToString() == "�Ѹ���")
                        {
                            m_App.CustomMessageBox.MessageShow("�����ѽɷѣ��޷�ȡ��");
                            return;
                        }
                        Dal.DataAccess.CancelConsultationData(ConsApplyNo, "6770");
                        m_App.CustomMessageBox.MessageShow("����ȡ���ɹ�");
                        Search();//ˢ������
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �鿴������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barButtonItemConsultationInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ShowConsultationInfo();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        private void ShowConsultationInfo()
        {
            DataRow dr = gridViewList.GetDataRow(gridViewList.FocusedRowHandle);
            if (dr != null)
            {
                string noOfFirstPage = dr["NoOfInpat"].ToString();
                string consultTypeID = dr["ConsultTypeID"].ToString();
                string consultApplySn = dr["ConsultApplySn"].ToString();
                string stateID = dr["StateID"].ToString();
                FrmRecordConsult frmRecord = new FrmRecordConsult();
                DataTable dtReord= GetConsultationDepartmentBySN(consultApplySn);
                #region OLD
                //�ڻ����嵥ģ��ֻ�в�ѯ������Ϣ���ܣ����������д������Ϣ�����Ƴ�
                //add by yxy
                //if (consultTypeID == Convert.ToString((int)ConsultType.One))
                //{
                //    if (stateID == Convert.ToString((int)ConsultStatus.WaitApprove) 
                //        || stateID == Convert.ToString((int)ConsultStatus.WaitConsultation) //  ������ ����
                //        || stateID == Convert.ToString((int)ConsultStatus.Reject))//���
                //    {
                //        FormApproveForOne formApprove = new FormApproveForOne(noOfFirstPage, m_App, consultApplySn);
                //        //todo why����ǩ����ǩ��ϢҪ������˰�ť?
                //        formApprove.ReadOnlyControl();
                //        formApprove.StartPosition = FormStartPosition.CenterParent;
                //        formApprove.ShowDialog();
                //    }
                //    //else if (stateID == Convert.ToString((int)ConsultStatus.WaitConsultation) //  ������ ����
                //    //    || stateID == Convert.ToString((int)ConsultStatus.Reject))
                //    //{
                //    //    FormApproveForOne formApprove = new FormApproveForOne(noOfFirstPage, m_App, consultApplySn);
                //    //    //todo why����ǩ����ǩ��ϢҪ������˰�ť?
                //    //    formApprove.ReadOnlyControl();
                //    //    formApprove.StartPosition = FormStartPosition.CenterParent;
                //    //    formApprove.ShowDialog();
                //    //}
                //    else if (stateID == Convert.ToString((int)ConsultStatus.RecordeSave) || stateID == Convert.ToString((int)ConsultStatus.RecordeComplete))
                //    {
                //        FormRecordForOne formRecord = new FormRecordForOne(noOfFirstPage, m_App, consultApplySn);
                //        formRecord.ReadOnlyControl();
                //        formRecord.StartPosition = FormStartPosition.CenterParent;
                //        formRecord.ShowDialog();
                //        //FormRecordForOne formRecordForOne = new FormRecordForOne(noOfFirstPage, m_App, consultApplySn);
                //        //formRecordForOne.StartPosition = FormStartPosition.CenterParent;
                //        //formRecordForOne.ShowDialog();
                //    }
                //}
                //else
                //{

                //if (stateID == Convert.ToString((int)ConsultStatus.WaitApprove)
                //    || stateID == Convert.ToString((int)ConsultStatus.WaitConsultation) //��ǩ�� ������
                //    || stateID == Convert.ToString((int)ConsultStatus.Reject))//���
                //{
                //    FormApproveForMultiply formApprove = new FormApproveForMultiply(noOfFirstPage, m_App, consultApplySn);
                //    formApprove.ReadOnlyControl();
                //    formApprove.StartPosition = FormStartPosition.CenterParent;
                //    formApprove.ShowDialog();
                //}
                //else if (stateID == Convert.ToString((int)ConsultStatus.RecordeSave) || stateID == Convert.ToString((int)ConsultStatus.RecordeComplete))
                //{
                //    FormRecordForMultiply formRecord = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                //    //todo why����ǩ����ǩ��ϢҪ������˰�ť?
                //    //���������д������Ϣ�����Ƴ����ڶ�Ӧ�Ļ����¼�����˵��嵥ģ��
                //    formRecord.ReadOnlyControl();
                //    formRecord.StartPosition = FormStartPosition.CenterParent;
                //    formRecord.ShowDialog();
                //}
                #endregion

                if (stateID == Convert.ToString((int)ConsultStatus.WaitApprove))//�����
                {
                    FrmConsultForReview frmApprove = new FrmConsultForReview(noOfFirstPage, m_App, consultApplySn, true/*ֻ��*/);
                    if (frmApprove == null)
                    {
                        return;
                    }
                    frmApprove.StartPosition = FormStartPosition.CenterParent;
                    frmApprove.ShowDialog();
                }
                else if (stateID == Convert.ToString((int)ConsultStatus.WaitConsultation))//������
                {
                    #region ע�� by xlb 2013-03-08
                    //����������ڸ�Ϊֻ�������˽�����д�������
                    //if (dr["applyuser"].ToString() == m_App.User.Id)// ��ǰ��¼���������ˣ����Խ�����˲��� 
                    //{
                    //    FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                    //    formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                    //    formRecrodForMultiply.ShowDialog();
                    //}
                    //else
                    //{
                    //FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                    //formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                    //formRecrodForMultiply.ReadOnlyControl();
                    //formRecrodForMultiply.ShowDialog();
                    //}
                    //FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                    //formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                    //formRecrodForMultiply.ShowDialog();
                    #endregion
                    if (dtReord.Rows.Count > 1)
                    {

                       frmRecord = new FrmRecordConsult(noOfFirstPage, consultApplySn, m_App, true, ConsultRecordForWrite.MultiEmployee);
                    }
                    else
                    {
                       frmRecord = new FrmRecordConsult(noOfFirstPage, consultApplySn, m_App, true);
                    }
                    if (frmRecord == null)
                    {
                        return;
                    }
                    frmRecord.StartPosition = FormStartPosition.CenterParent;
                    frmRecord.ShowDialog();
                }
                else if (stateID == Convert.ToString((int)ConsultStatus.CancelConsultion))//��ȡ��
                {
                    #region ע�� by xlb 2013-03-08
                    //FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                    //formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                    //formRecrodForMultiply.ReadOnlyControl();
                    //formRecrodForMultiply.ShowDialog();
                    #endregion
                    FormApplyForMultiply frmapply = new FormApplyForMultiply(noOfFirstPage, m_App, consultApplySn, true);
                    if (frmapply == null)
                    {
                        return;
                    }
                    frmapply.StartPosition = FormStartPosition.CenterParent;
                    frmapply.ShowDialog();
                }
                else if (stateID == Convert.ToString((int)ConsultStatus.Reject))//��� ��������ҳ��
                {

                    #region ע�� by xlb 2013-03-08
                    //FormConsultationApply formApply = new FormConsultationApply(syxh.ToString(), m_App, true);
                    //formApply.StartPosition = FormStartPosition.CenterParent;
                    //formApply.ShowApprove(consultApplySn);
                    //formApply.ShowDialog(); 

                    //FormConsultationApply formApply = new FormConsultationApply(noOfFirstPage, m_App, consultApplySn);
                    //formApply.StartPosition = FormStartPosition.CenterParent;
                    //formApply.ShowApprove(consultApplySn);
                    //formApply.ReadOnlyControl();
                    //formApply.ShowDialog();
                    #endregion
                    FormApplyForMultiply frmapply = new FormApplyForMultiply(noOfFirstPage, m_App, consultApplySn, true);
                    if (frmapply == null)
                    {
                        return;
                    }
                    frmapply.StartPosition = FormStartPosition.CenterParent;
                    frmapply.ShowDialog();
                }
                else if (stateID == Convert.ToString((int)ConsultStatus.RecordeSave) || stateID == Convert.ToString((int)ConsultStatus.RecordeComplete))//��ɣ�����
                {
                    #region ע��by xlb 2013-03-08
                    //FormRecordForMultiply formRecrodForMultiply = new FormRecordForMultiply(noOfFirstPage, m_App, consultApplySn);
                    //formRecrodForMultiply.StartPosition = FormStartPosition.CenterParent;
                    //formRecrodForMultiply.ReadOnlyControl();
                    //formRecrodForMultiply.ShowDialog();
                    #endregion
                    if (dtReord.Rows.Count > 1)
                    {

                        frmRecord = new FrmRecordConsult(noOfFirstPage, consultApplySn, m_App, true, ConsultRecordForWrite.MultiEmployee);
                    }
                    else
                    {
                        frmRecord = new FrmRecordConsult(noOfFirstPage, consultApplySn, m_App, true);
                    }
                    if (frmRecord == null)
                    {
                        return;
                    }
                    frmRecord.StartPosition = FormStartPosition.CenterParent;
                    frmRecord.ShowDialog();
                }
                else if (stateID == Convert.ToString((int)ConsultStatus.ApplySave))//�������뱣�� ��������ҳ�治�ɱ༭
                {
                    FormApplyForMultiply frmapply = new FormApplyForMultiply(noOfFirstPage, m_App, consultApplySn, true);
                    if (frmapply == null)
                    {
                        return;
                    }
                    frmapply.StartPosition = FormStartPosition.CenterParent;
                    frmapply.ShowDialog();
                }

                Search();
            }
        }
        
        /// <summary>
        /// ͨ�����뵥�Ż�ȡʵ��������¼��Ϣ
        /// Add by xlb 2013-03-19
        /// </summary>
        /// <param name="consultApplySN"></param>
        /// <returns></returns>
        private DataTable GetConsultationDepartmentBySN(string consultApplySN)
        {
            try
            {
                string sqlGetConsultationDepartment = "select * from consultrecorddepartment where consultapplysn = '{0}' and valid = '1'";
                return DS_SqlHelper.ExecuteDataTable(string.Format(sqlGetConsultationDepartment, consultApplySN), System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����
        /// </summary>
        /// <returns></returns>
        private decimal GetCurrentPat()
        {
            try
            {
                if (gridViewList.FocusedRowHandle < 0)
                {
                    return -1;
                }
                DataRow dataRow = gridViewList.GetDataRow(gridViewList.FocusedRowHandle);
                if (dataRow == null)
                {
                    return -1;
                }
                return Convert.ToDecimal(dataRow["NoOfInpat"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// �����嵥�б��Ҽ����Ƶ���ʾ�˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    GridHitInfo hit = gridViewList.CalcHitInfo(e.X, e.Y);

                    if (hit.RowHandle >= 0)
                    {
                        DataRow dr = gridViewList.GetDataRow(hit.RowHandle);
                        if (dr != null)
                        {
                            string statID = dr["StateID"].ToString();
                            barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            //ֻ�С�������ļ�¼���ܲ鿴����
                            if (statID == Convert.ToString((int)ConsultStatus.WaitConsultation))// || statID == Convert.ToString((int)ConsultStatus.RecordeSave)   �����¼����״̬ʱ  ȡ����ʾ �鿴�������� 
                            {
                                barButtonItemMedicalInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                barButtonItemMedicalInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            //������Ļ��ﵥ�ݵ�������������Լ�,�ҵ���״̬Ϊ��6730�����6720����ˣ�����ʾ����ȡ���˵�
                            //����������
                            string applyuser = dr["ApplyUser"].ToString();
                            #region ע��by xlb 2013-03-14
                            //ȡ������Ȩ��
                            //string QuitLevel = GetConfigValueByKey("ConsultQuitLevel");
                            //if (string.IsNullOrEmpty(QuitLevel))
                            //{
                            //    MessageBox.Show("���������");
                            //    return;
                            //}
                            //DataTable dtGrade = DS_SqlHelper.ExecuteDataTable(string.Format("select grade from users where id={0}",m_App.User.Id));
                            //if (dtGrade.Rows.Count == 0 || dtGrade == null || string.IsNullOrEmpty(dtGrade.Rows[0][0].ToString()))
                            //{
                            //    barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            //}
                            ////ָ��ҽʦ����
                            //string DocLevel = QuitLevel.Split('|')[0];
                            //string AudioList = QuitLevel.Split('|')[1];
                            //if (DocLevel.Contains(","))//�ж��ָ������
                            //{
                            //    string[] listLevel = DocLevel.Split(',');//�����б�
                            //    foreach (var item in listLevel)
                            //    {
                            //        if (item.Equals(dtGrade.Rows[0][0]))//ָ�������б��е�ǰ��½�˼��������ȡ������
                            //        {
                            //            barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            //            break;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (DocLevel.Equals(dtGrade.Rows[0][0]))
                            //    {
                            //        barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            //    }
                            //}
                            //if (AudioList.Contains(","))
                            //{
                            //    Audio = AudioList.Split(',');
                            //}
                            #endregion
                            if (applyuser == m_App.User.Id)
                            {
                                if (statID == "6730" || statID == "6720")
                                {
                                    barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                }
                                else
                                {
                                    barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                            }
                            else
                            {
                                barButtonItemCancelMyApplyInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            this.popupMenuList.ShowPopup(this.barManagerList, (Cursor.Position));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// /����EXCel �˲�����ҽԺ����
        /// add by ywk  2012��9��3�� 14:13:56
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExprtExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //gridControlList.ExportToXls(
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "����Excel";
                saveFileDialog.Filter = "Excel�ļ�(*.xls)|*.xls";
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    //gridViewCardList.ExportToXls(saveFileDialog.FileName);
                    //gridControlList.ExportToXls(saveFileDialog.FileName, true);
                    options.SheetName = "�����嵥";
                    options.ShowGridLines = true;
                    string caption = gridViewList.ViewCaption;
                    gridViewList.ViewCaption = options.SheetName;
                    gridControlList.ExportToXls(saveFileDialog.FileName, options);
                    gridViewList.ViewCaption = caption;

                    m_App.CustomMessageBox.MessageShow("�����ɹ�");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        #region ==================ע�� �й��÷���===============

        ///// <summary>
        ///// �õ�������Ϣ
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public string GetConfigValueByKey(string key)
        //{
        //    try
        //    {
        //        if (m_App == null)
        //        {
        //            return "";
        //        }
        //        string sql1 = " select * from appcfg where configkey = '" + key + "'  ";
        //        DataTable dt = m_App.SqlHelper.ExecuteDataTable(sql1, CommandType.Text);
        //        string config = string.Empty;
        //        if (dt.Rows.Count > 0)
        //        {
        //            config = dt.Rows[0]["value"].ToString();
        //        }
        //        return config;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        /// <summary>
        /// �س��л�����
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2012-10-19</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    DS_Common.cbx_KeyPress(sender);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-10-25</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                DS_Common.AutoIndex(e);
            }
            catch (Exception ex)
            {
               MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCList_Load(object sender, EventArgs e)
        {
            try
            {
                //���ù��ڻ�����˵�Tabҳ�Ƿ���ʾ edit by tj 2012-10-26
                if (!CommonObjects.IsNeedVerifyInConsultation)
                {
                    checkEditWaitApprove.Visible = false;
                    chkCancel.Location = checkEditReject.Location;
                    checkEditReject.Location = checkEditWaitApprove.Location;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// �����¼�
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-13</date>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(1, ex);
            }
        }

        /// <summary>
        /// ���÷���
        /// <auth>Yanqiao.Cai</auth>
        /// <date>2011-11-13</date>
        /// </summary>
        private void Reset()
        {
            try
            {
                InitDateEdit();
                textEditName.Text = string.Empty;
                textEditPatientSN.Text = string.Empty;
                lookUpEditorUrgency.CodeValue = string.Empty;
                lookUpEditorBed.CodeValue = string.Empty;
                checkEditWaitConsultation.Checked = true;
                checkEditHaveConsultation.Checked = false;
                checkEditWaitApprove.Checked = true;
                checkEditReject.Checked = false;
                chkCancel.Checked = false;
                textEditName.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
