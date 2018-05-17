using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using DrectSoft.Common.Eop;
using System.Globalization;
using System.Data.SqlClient;
using System.Windows.Forms;
using DrectSoft.FrameWork.WinForm.Plugin;

namespace DrectSoft.Core.DoctorAdvice
{
   /// <summary>
   /// �������ҽ�����ҽ����֮���ת���Լ����ݱ���
   /// </summary>
   public class SuiteOrderHandle
   {
      #region public properties
      /// <summary>
      /// ��������¼��
      /// </summary>
      public DataTable SuiteMasterTable
      {
         get
         {
            if (_suiteDataSet == null)
               return null;
            else
               return _suiteDataSet.Tables[0];
         }
      }

      /// <summary>
      /// ������ϸ���ݱ�
      /// </summary>
      public DataTable SuiteDetailTable
      {
         get
         {
            if (_suiteDataSet == null)
               return null;
            else
               return _suiteDataSet.Tables[1];
         }
      }
      private DataSet _suiteDataSet;

      /// <summary>
      /// ��ǰ����ĳ���ҽ�������
      /// </summary>
      public decimal CurrentSuiteNo
      {
         get { return _currentSuiteNo; }
         set
         {
            _currentSuiteNo = value;
            DoAfterSwitchSuite();
         }
      }
      private decimal _currentSuiteNo;

      /// <summary>
      /// ת����ʱҽ���ĳ���ҽ�����ݣ�ͬʱҲ���޸ĺ�����ݣ�
      /// </summary>
      public DataTable TempOrderTable
      {
         get { return _tempOrderTable; }
      }
      private DataTable _tempOrderTable;

      /// <summary>
      /// ת�ɳ���ҽ���ĳ���ҽ�����ݣ�ͬʱҲ���޸ĺ�����ݣ�
      /// </summary>
      public DataTable LongOrderTable
      {
         get { return _longOrderTable; }
      }
      private DataTable _longOrderTable;
      #endregion

      #region private variables & properties
      private IDataAccess m_SqlExecutor;
      private ICustomMessageBox m_MessageBox;
      private GenerateShortCode m_GenShortCode;
      private bool m_QueryOnly; // �Ƿ���ǲ�ѯ����(Ӱ�쵽��ʼ�����ݵĴ���)
      private DataTable m_TransTable; // ������ϸ��ҽ����ת�����м��

      private SqlParameter[] InsertMasterParas
      {
         get
         {
            if (_insertMasterParas == null)
            {
               _insertMasterParas = new SqlParameter[] {
                   new SqlParameter(ConstSchemaNames.SuiteColName, SqlDbType.VarChar)
                  ,new SqlParameter(ConstSchemaNames.SuiteColPy, SqlDbType.VarChar)
                  ,new SqlParameter(ConstSchemaNames.SuiteColWb, SqlDbType.VarChar)
                  ,new SqlParameter(ConstSchemaNames.SuiteColDeptCode, SqlDbType.VarChar)
                  ,new SqlParameter(ConstSchemaNames.SuiteColWardCode, SqlDbType.VarChar)
                  ,new SqlParameter(ConstSchemaNames.SuiteColDoctorId, SqlDbType.VarChar)
                  ,new SqlParameter(ConstSchemaNames.SuiteColApplyRange, SqlDbType.Int)
                  ,new SqlParameter(ConstSchemaNames.SuiteColMemo, SqlDbType.VarChar)
               };
            }

            return _insertMasterParas;
         }
      }
      private SqlParameter[] _insertMasterParas;
      #endregion

      #region ctor
      /// <summary>
      /// 
      /// </summary>
      /// <param name="app"></param>
      /// <param name="queryOnly">�Ƿ���ǲ�ѯ����(Ӱ�쵽��ʼ�����ݵĴ���)</param>
      public SuiteOrderHandle(IEmrHost app, bool queryOnly)
      {
         m_SqlExecutor = app.SqlHelper;
         m_MessageBox = app.CustomMessageBox;
         m_GenShortCode = new GenerateShortCode(app.SqlHelper);
         m_QueryOnly = queryOnly;
          
         InsertMasterParas[3].Value = app.User.CurrentDeptId;
         InsertMasterParas[4].Value = app.User.CurrentWardId;
         InsertMasterParas[5].Value = app.User.DoctorId;

         //GenerateSuiteData();
         //InitializeTableSchema();
      }
      #endregion

      #region public methods
      /// <summary>
      /// ������ĳ���ҽ������ͬ����DataRow�У������浽����
      /// </summary>
      /// <param name="serialNo"></param>
      /// <param name="suiteObject"></param>
      public void SynchAndSaveMasterData(decimal serialNo, SuiteOrder suiteObject)
      {
         if (suiteObject == null)
            return;
         DataRow[] matchRows = SuiteMasterTable.Select(ConstSchemaNames.SuiteDetailColSuiteSerialNo + " = " + serialNo);
         if (matchRows.Length == 1)
         {
            //if (String.IsNullOrEmpty(suiteObject.Py))
            {
               // ��������ƴ���������д
               string[] shortCodes = m_GenShortCode.GenerateStringShortCode(suiteObject.Name);
               suiteObject.Py = shortCodes[0];
               suiteObject.Wb = shortCodes[1];
            }
            // ͬ�����Ժ�DataRow��ֵ
            PersistentObjectFactory.SetDataRowValueFromObject(matchRows[0], suiteObject);
            // ���޸�ͬ�������ݿ���
            m_SqlExecutor.UpdateTable(SuiteMasterTable, ConstSchemaNames.SuiteTableName, false);
            //SuiteMasterTable.AcceptChanges();
         }
         else
            m_MessageBox.MessageShow("����ҽ�������д������˳��������½��룡", CustomMessageBoxKind.ErrorOk);
      }

      /// <summary>
      /// ����ָ�����͵��³���ҽ������¼
      /// </summary>
      /// <returns></returns>
      public decimal AddNewMasterRecord(DataApplyRange suiteType)
      {
         // �ڵ�ǰ�����²���һ���¼�¼�������ݿ��У�
         string insertCmd = String.Format(CultureInfo.CurrentCulture
            , ConstSqlSentences.FormatInsertSuite
            , ConstSchemaNames.SuiteTableName);
         string newName = String.Format(CultureInfo.CurrentCulture
            , "({0}) {1}", "�³���", SuiteMasterTable.Rows.Count + 1);

         InsertMasterParas[0].Value = newName;
         InsertMasterParas[1].Value = "";
         InsertMasterParas[2].Value = "";
         InsertMasterParas[6].Value = Convert.ToInt32(suiteType);
         InsertMasterParas[7].Value = "";

         int newSuiteSerialNo;
         m_SqlExecutor.ExecuteNoneQuery(insertCmd, InsertMasterParas, out newSuiteSerialNo);
         // �����ݿ��ж����²���ļ�¼���ϲ�����ǰ��Master����
         DataTable newRecords = m_SqlExecutor.ExecuteDataTable(String.Format(CultureInfo.CurrentCulture
            , ConstSqlSentences.FormatSelectSuite
            , ConstSchemaNames.SuiteTableName, newSuiteSerialNo));
         SuiteMasterTable.Merge(newRecords, true, MissingSchemaAction.Ignore);

         return (decimal)newSuiteSerialNo;
      }

      /// <summary>
      /// ɾ��ָ���ĳ���ҽ��
      /// </summary>
      /// <param name="serialNo"></param>
      public void DeleteMasterRecord(decimal serialNo)
      {
         if ((serialNo > 0)
            && (m_MessageBox.MessageShow("ȷ��Ҫɾ����ǰ��¼��", CustomMessageBoxKind.QuestionYesNo) == DialogResult.Yes))
         {
            // ɾ����Ӧ��ϸ, ɾ������¼
            string delCmd = String.Format(CultureInfo.CurrentCulture
               , ConstSqlSentences.FormatDeleteSuiteData
               , ConstSchemaNames.SuiteDetailTableName
               , ConstSchemaNames.SuiteTableName
               , serialNo);
            try
            {
               m_SqlExecutor.ExecuteNoneQuery(delCmd);
               // ��DataTable���Ƴ�
               DataRow[] matchRows = SuiteMasterTable.Select(ConstSchemaNames.SuiteDetailColSuiteSerialNo + " = " + serialNo);
               foreach (DataRow row in matchRows)
                  row.Delete();
               matchRows = SuiteDetailTable.Select(ConstSchemaNames.SuiteDetailColSuiteSerialNo + " = " + serialNo);
               foreach (DataRow row in matchRows)
                  row.Delete();
            }
            catch
            {
               m_MessageBox.MessageShow("ɾ����¼����������!", CustomMessageBoxKind.ErrorYes);
            }
         }
      }

      /// <summary>
      /// ���浱ǰ����ҽ������ϸ����
      /// </summary>
      public void SaveSuiteDetailData()
      {
         //������ϵ�����
         DataRow[] delRows = SuiteDetailTable.Select(ConstSchemaNames.SuiteDetailColSuiteSerialNo + " = " + CurrentSuiteNo);
         foreach (DataRow row in delRows)
            SuiteDetailTable.Rows.Remove(row);

         // ��ҽ�������ݺϲ���������ϸ��(��Ϊ��Ҫ���ϲ������״̬��Ϊ���У����Բ���AddRow�ķ�ʽ)
         foreach (DataRow row in TempOrderTable.Rows)
            AddNewRowToDetailTable(row, true);
         foreach (DataRow row in LongOrderTable.Rows)
            AddNewRowToDetailTable(row, false);

         // ɾ�����ݿ��д���Ŷ�Ӧ����ϸ��¼
         m_SqlExecutor.ExecuteNoneQuery(String.Format(ConstSqlSentences.FormatDeleteSuiteDetail, ConstSchemaNames.SuiteDetailTableName, CurrentSuiteNo));
         // ����
         m_SqlExecutor.UpdateTable(SuiteDetailTable, ConstSchemaNames.SuiteDetailTableName, false);
         //SuiteDetailTable.AcceptChanges();
      }
      #endregion

      #region custom event handler
      /// <summary>
      /// �л���ǰ����ĳ���ҽ���¼�
      /// </summary>
      public event EventHandler AfterSwitchSuite
      {
         add { onAfterSwitchSuite = (EventHandler)Delegate.Combine(onAfterSwitchSuite, value); }
         remove { onAfterSwitchSuite = (EventHandler)Delegate.Remove(onAfterSwitchSuite, value); }
      }
      private EventHandler onAfterSwitchSuite;

      private void FireAfterSwitchSuite()
      {
         if (onAfterSwitchSuite != null)
            onAfterSwitchSuite(this, new EventArgs());
      }
      #endregion

      #region private methods
      private void GenerateSuiteData()
      {
         // ��ȡ����, ����ReleationShip
         //string selMasterCmd = String.Format("select * from {0} where syfw = 2900 or (syfw = 2901 and e.ksdm = @ksdm) or (syfw = 2903 and e.ysdm = @ysdm) order by name"
         //   , ConstSchemaNames.SuiteTableName);
         //string selDetailCmd = String.Format("select a.* from {0} a, {1} b where a.ctyzxh = b.ctyzxh and (b.syfw = 2900 or (b.syfw = 2901 and b.ksdm = @ksdm) or (b.syfw = 2903 and b.ysdm = @ysdm)) order by ctmxxh"
         //   , ConstSchemaNames.SuiteDetailTableName, ConstSchemaNames.SuiteTableName);

         SqlParameter[] paras = new SqlParameter[] { 
             InsertMasterParas[3]
            ,InsertMasterParas[4]
            ,InsertMasterParas[5]
            , new SqlParameter("yzlr", SqlDbType.Int)
         };
         if (m_QueryOnly)
            paras[3].Value = 1;
         else
            paras[3].Value = 0;

         _suiteDataSet = m_SqlExecutor.ExecuteDataSet(ConstSchemaNames.ProcQueryOrderSuite, paras, CommandType.StoredProcedure);
         SuiteMasterTable.TableName = ConstSchemaNames.SuiteTableName;
         if (!m_QueryOnly)
         {
            m_SqlExecutor.ResetTableSchema(SuiteMasterTable, ConstSchemaNames.SuiteTableName);
            m_SqlExecutor.ResetTableSchema(SuiteDetailTable, ConstSchemaNames.SuiteDetailTableName);
         }
         //DataColumn[] keyCols = new DataColumn[] { SuiteMasterTable.Columns["ctyzxh"], SuiteMasterTable.Columns["yzbz"] };
         //DataColumn[] foreignKeyCols = new DataColumn[] { SuiteDetailTable.Columns["ctyzxh"], SuiteDetailTable.Columns["yzbz"] };
         //_suiteDataSet.Relations.Add("SuiteDetail", keyCols, foreignKeyCols);
      }

      private void DoAfterSwitchSuite()
      {
         TransformSuiteDetailData(true, TempOrderTable);
         TransformSuiteDetailData(false, LongOrderTable);
         FireAfterSwitchSuite();
      }

      private void TransformSuiteDetailData(bool isTemp, DataTable orderTable)
      {
         string orderSerialNoField;
         if (isTemp)
         {
            orderSerialNoField = ConstSchemaNames.OrderTempColSerialNo;
            SuiteDetailTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture
               , ConstSqlSentences.FormatSuiteDetailFilter
               , CurrentSuiteNo
               , OrderManagerKind.ForTemp);
         }
         else
         {
            orderSerialNoField = ConstSchemaNames.OrderLongColSerialNo;
            SuiteDetailTable.DefaultView.RowFilter = String.Format(CultureInfo.CurrentCulture
               , ConstSqlSentences.FormatSuiteDetailFilter 
               , CurrentSuiteNo
               ,OrderManagerKind.ForLong);
         }

         // ����ҽ������ϸ���ݺϲ���ָ����ҽ������
         m_TransTable.Clear();
         m_TransTable.Merge(SuiteDetailTable.DefaultView.ToTable(), false, MissingSchemaAction.Ignore);
         orderTable.Clear();
         orderTable.Merge(m_TransTable, false, MissingSchemaAction.Ignore);

         // ���������ź�ҽ��״̬
         GroupPositionKind gpKind;
         object newGroupSerialNo = -1;
         for (int index = 0; index < orderTable.Rows.Count; index++)
         {
             gpKind = (GroupPositionKind)Convert.ToInt32(orderTable.Rows[index]["GroupFlag"]);

            if ((gpKind == GroupPositionKind.SingleOrder) || (gpKind == GroupPositionKind.GroupStart))
               newGroupSerialNo = orderTable.Rows[index][orderSerialNoField];            
            orderTable.Rows[index][ConstSchemaNames.OrderColGroupSerialNo] = newGroupSerialNo;

            //orderTable.Rows[index]["yzzt"] = (int)OrderState.New;
         }
      }

      private void InitializeTableSchema()
      {
         _tempOrderTable = CreateAndSetOrderTable( ConstSchemaNames.TempOrderTableName);
         _longOrderTable = CreateAndSetOrderTable(ConstSchemaNames.LongOrderTableName);
         
         m_TransTable = SuiteDetailTable.Clone();
         // ������ҽ�����в�����Ϊ�յ��У�������Ĭ��ֵ
         m_TransTable.Columns.AddRange(new DataColumn[] {
             new DataColumn("syxh", typeof(decimal))
            ,new DataColumn("fzxh", typeof(decimal))
            ,new DataColumn("bqdm", typeof(string))
            ,new DataColumn("ksdm", typeof(string))
            ,new DataColumn("lrysdm", typeof(string))
            ,new DataColumn("lrrq", typeof(string))
            ,new DataColumn("yzzt", typeof(int))
            ,new DataColumn("tsbj", typeof(int))
            });
         m_TransTable.Columns["syxh"].DefaultValue = 1;
         m_TransTable.Columns["fzxh"].DefaultValue = 1;
         m_TransTable.Columns["bqdm"].DefaultValue = "";
         m_TransTable.Columns["ksdm"].DefaultValue = "";
         m_TransTable.Columns["lrysdm"].DefaultValue = "00";
         m_TransTable.Columns["lrrq"].DefaultValue = "";
         m_TransTable.Columns["yzzt"].DefaultValue = (int)OrderState.New;
         m_TransTable.Columns["tsbj"].DefaultValue = 0;
      }

      private DataTable CreateAndSetOrderTable(string tableName)
      {
         DataTable result = m_SqlExecutor.ExecuteDataTable("select * from " + tableName + " where 1 = 2");
         m_SqlExecutor.ResetTableSchema(result, tableName);
         return result;
      }

      private void AddNewRowToDetailTable(DataRow sourceRow, bool isTemp)
      {
         if ((sourceRow.RowState == DataRowState.Deleted) || (sourceRow.RowState == DataRowState.Detached))
            return;

         DataRow newRow = SuiteDetailTable.NewRow();

         foreach (DataColumn col in sourceRow.Table.Columns)
            if (SuiteDetailTable.Columns.Contains(col.ColumnName))
               newRow[col.ColumnName] = sourceRow[col];

         newRow[ConstSchemaNames.SuiteDetailColSuiteSerialNo] = CurrentSuiteNo;
         if (isTemp)
            newRow[ConstSchemaNames.SuiteDetailColOrderFlag] = OrderManagerKind.ForTemp;
         else
            newRow[ConstSchemaNames.SuiteDetailColOrderFlag] = OrderManagerKind.ForLong;

         SuiteDetailTable.Rows.Add(newRow);
      }
      #endregion
   }
}
