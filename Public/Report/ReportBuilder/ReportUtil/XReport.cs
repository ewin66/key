using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DrectSoft.Common.Report
{
    /// <summary>
    /// ������
    /// TODO: �й�STREAM�Ĵ�����Ľ�
    /// </summary>
    public class XReport : IDisposable
    {
        //private static string TEXTFmt = "��������� - {0}";
        /// <summary>
        /// ���ʱ�Զ�����
        /// </summary>
        private static bool AUTOSAVE = true;

        #region fields
        /// <summary>
        /// �ǣ����浽�ļ��С��񣺱��浽Stream��
        /// </summary>
        private bool m_Save2File;
        /// <summary>
        /// ��Ҫ��������Ĺ������в�����¿ؼ�
        /// </summary>
        private Collection<Type> m_NewControls;
        #endregion

        #region properties
        private string _fileName;
        /// <summary>
        /// �����ļ�����Ӧ�ð���·����Ϣ
        /// ע�⣺�ı��ļ������ı䱨��Ĳ���,���ı䱨�����<see cref="CurrentReport"/>
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                m_Save2File = true;
                _fileName = value;
                SetFileReport(out _currentReport);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public Stream ReportStream
        {
            get
            {
                if (_reportStream == null)
                    _reportStream = new MemoryStream();
                return _reportStream;
            }
            set
            {
                m_Save2File = false;
                _reportStream = value;
                SetStreamReport(out _currentReport);
            }
        }
        private Stream _reportStream;

        /// <summary>
        /// ����ѹ���ı���ģ��
        /// </summary>
        public string CompressReportStream
        {
            get
            {
                Stream report = ReportStream;
                report.Position = 0;
                byte[] buffer = new byte[report.Length];
                report.Read(buffer, 0, buffer.Length);

                MemoryStream ms = new MemoryStream();
                DeflateStream compressedzipStream = new DeflateStream(ms, CompressionMode.Compress, true);

                compressedzipStream.Write(buffer, 0, buffer.Length);
                compressedzipStream.Close();

                ms.Position = 0;
                byte[] buffZipXml = new byte[ms.Length];
                ms.Read(buffZipXml, 0, buffZipXml.Length);
                return Convert.ToBase64String(buffZipXml);
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    ReportStream = new MemoryStream();
                else
                {
                    string s = value;
                    byte[] buffer = Convert.FromBase64String(s);
                    MemoryStream ms = new MemoryStream(buffer);
                    ms.Position = 0;

                    DeflateStream dfs = new DeflateStream(ms, CompressionMode.Decompress, true);
                    StreamReader sr = new StreamReader(dfs, Encoding.UTF8);
                    string sXml = sr.ReadToEnd();
                    sr.Close();
                    dfs.Close();
                    ReportStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sXml));
                }
            }
        }

        /// <summary>
        /// ��ǰ�ı����޸�<see cref="FileName"/>�ļ������Զ��޸ı���      
        /// </summary>
        public XtraReport CurrentReport
        {
            get { return _currentReport; }
            //set { _currentReport = value; }
        }
        private XtraReport _currentReport;

        private DataSet _ds;
        /// <summary>
        /// ����/ȡ�õ�ǰ����<see cref="CurrentReport"/>������Դ      
        /// </summary>
        public DataSet DataSource
        {
            get { return _ds; }
            set
            {
                _ds = value;
                if (_currentReport != null)
                    _currentReport.DataSource = _ds;
            }
        }
        #endregion

        #region ctors
        /// <summary>
        /// ָ��Ҫ��ӡ�����ݼ��Ϻ��ļ������ļ��������ǲ����ڵģ��Զ��ڵ�һ��ʹ��ʱ�����ļ��������ļ���
        /// </summary>
        /// <param name="dt">���ݼ���</param>
        /// <param name="filename">�ļ���</param>
        public XReport(DataTable dt, string filename)
            : this(filename)
        {
            if (_ds == null)
                _ds = new DataSet("����Դ");
            if (dt == null)
                throw new ArgumentNullException("dt", "���ݱ�Ϊ��");
            _ds.Tables.Add(dt);
            _currentReport.DataSource = _ds;

        }

        /// <summary>
        /// ָ��Ҫ��ӡ�����ݼ��Ϻ��ļ���
        /// ���ļ��������ǲ����ڵ�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="filename">�ļ���</param>
        public XReport(DataSet ds, string filename)
            : this(filename)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "���ݼ�Ϊ��");
            _ds = ds;
            _currentReport.DataSource = _ds;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="reportStream"></param>
        public XReport(DataTable dt, Stream reportStream)
            : this()
        {
            _ds.Tables.Add(dt);
            ReportStream = reportStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressReportStream"></param>
        /// <param name="ds"></param>
        public XReport(string compressReportStream, DataSet ds)
            : this()
        {
            _ds = ds;
            CompressReportStream = compressReportStream;
            _currentReport.DataSource = _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressReportStream"></param>
        /// <param name="dt"></param>
        public XReport(string compressReportStream, DataTable dt)
            : this()
        {
            _ds.Tables.Add(dt);
            _currentReport.DataSource = _ds;
            CompressReportStream = compressReportStream;
        }

        private XReport(string filename)
            : this()
        {
            FileName = filename;
        }

        private XReport()
        {
            _ds = new DataSet("����Դ");
        }
        #endregion

        #region private methods
        /// <summary>
        /// ��ͼ���ļ��д��������������򴴽��µı���
        /// �������ݼ�
        /// </summary>
        /// <param name="rp">�������</param>
        /// <returns>����ֵΪ���ʾ�ļ������ڣ����´������ļ�</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private bool SetFileReport(out XtraReport rp)
        {
            if (string.IsNullOrEmpty(_fileName) || string.IsNullOrEmpty(_fileName.Trim()))
            {
                throw new ArgumentNullException(_fileName, "�ļ���Ϊ��");
            }
            bool runDesign = true;
            if (File.Exists(_fileName))
            {
                runDesign = true;
                rp = new XtraReport();
                try
                {
                    rp.LoadLayout(_fileName);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("����ļ����Ƿ���Ч\n", ex);
                }
            }
            else
            {
                try
                {
                    rp = XtraReport.FromFile(_fileName, true);
                    runDesign = false;
                }
                catch
                {
                    rp = new XtraReport();
                    runDesign = true;
                }
            }
            return runDesign;
        }

        /// <summary>
        /// �����д������������ܴ����µı����������ݼ�
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        private bool SetStreamReport(out XtraReport rp)
        {
            bool runDesign;
            if (_reportStream == null)
            {
                runDesign = true;
                rp = new XtraReport();
                try
                {

                    rp.LoadLayout(ReportStream);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    rp = XtraReport.FromStream(_reportStream, true);
                    runDesign = false;
                }
                catch
                {
                    rp = new XtraReport();
                    runDesign = true;
                }
            }
            return runDesign;
        }
        #endregion

        #region public methods
        /// <summary>
        /// �򱨱���ƽ���Ĺ������в����Զ���Ŀؼ�(��Ҫ����ʾDesignǰ����)
        /// </summary>
        /// <param name="newControls"></param>
        public void ExpandToolBox(Collection<Type> newControls)
        {
            m_NewControls = newControls;
        }

        /// <summary>
        /// ����Ԥ������
        /// </summary>
        public void ShowPreview()
        {
            _currentReport.DataSource = _ds;
            _currentReport.ShowPreview();
        }

        ///// <summary>
        ///// ������ƽ���
        ///// </summary>
        //public void Design()
        //{
        //    //��ʼ��������ƽ���    
        //    if (m_frm == null)
        //    {
        //        m_frm = new XRDesignForm();
        //        //m_frm.ShowInTaskbar = false;
        //        m_frm.WindowState = FormWindowState.Maximized;
        //        if (AUTOSAVE)
        //        {
        //            m_frm.ReportStateChanged += new ReportStateEventHandler(m_frm_ReportStateChanged);
        //            m_frm.Closing += new System.ComponentModel.CancelEventHandler(m_frm_Closing);
        //        }
        //        m_frm.TextChanged += new EventHandler(m_frm_TextChanged);
        //    }
        //    m_frm.FileName = _fileName;
        //    CurrentReport.DataSource = _ds;
        //    if ((m_NewControls != null) && (m_NewControls.Count > 0))
        //        CurrentReport.DesignerLoaded += new DesignerLoadedEventHandler(CurrentReport_DesignerLoaded);
        //    m_frm.OpenReport(_currentReport);
        //    m_frm.ShowDialog();
        //}

        private void CurrentReport_DesignerLoaded(object sender, DesignerLoadedEventArgs e)
        {
            IToolboxService ts = (IToolboxService)e.DesignerHost.GetService(typeof(IToolboxService));

            foreach (Type type in m_NewControls)
                ts.AddToolboxItem(new ToolboxItem(type));
        }

        /// <summary>
        /// ������ת����ͼƬ
        /// </summary>
        /// <returns></returns>
        public Image ConvertReportToImage()
        {
            Stream imageStream = new MemoryStream();
            _currentReport.DataSource = _ds;
            CurrentReport.ExportToImage(imageStream, ImageFormat.Png);
            return Image.FromStream(imageStream);
        }
        #endregion

        #region event handle

        #endregion

        #region IDisposable Members
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
            //if (m_frm != null)
            //{
            //   m_frm.Dispose();
            //   m_frm = null;
            //}
            if (_currentReport != null)
            {
                _currentReport.Dispose();
                _currentReport = null;
            }
            if (_ds != null)
            {
                _ds.Dispose();
                _ds = null;
            }
        }

        #endregion
    }
}
