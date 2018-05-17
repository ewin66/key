/*
 * ��1���޸���Ա��wwj 2012-07-24
 *      ���������ҽԺ�����ⵥ����ͬһʱ�����״̬������
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;
using DevExpress.XtraEditors;
using DrectSoft.Resources;
using DrectSoft.Core.NursingDocuments.UserControls;
using System.Drawing.Printing;
using System.IO;
using DevExpress.Utils;
using System.Xml;
using DrectSoft.Core.NursingDocuments.PublicSet;

namespace DrectSoft.Core.NursingDocuments.UserControls
{
    public partial class UCThreeMeasureTable : DevExpress.XtraEditors.XtraUserControl
    {
        #region ö��
        /// <summary>
        /// ������������ͼ����Ҫ����������
        /// </summary>
        public enum VitalSignsType
        {
            TiWen = 1,          //����
            MaiBo = 2,          //����
            HuXi = 3,           //����
            XinLv = 4,          //����
            WuLiJiangWen = 5,   //������
            XueYa = 6,          //Ѫѹ
            ZongRuLiang = 7,    //������
            ZongChuLiang = 8,   //�ܳ���
            YinLiuLiang = 9,    //������
            DaBianCiShu = 10,    //������
            ShenGao = 11,       //���
            TiZhong = 12,       //����
            GuoMingYaoWu = 13,  //����ҩ��
            TeShuZhiLiao = 14,  //��������
            //Other1 = 15,        //����1
            PainInfo = 15,        //����1 ��Ϊ ��ʹ��λ
            Other2 = 16,        //����2
            //add by ywk 
            WuLiShengWen = 17       //����2  ����Ϊ ��������

            //PaiChuLiang=18//�ų���(��������������������һ���ո���)ywk 2012��5��17�� 16:41:00


        }

        /// <summary>
        /// ���²����ķ�ʽ
        /// </summary>
        public enum VitalSignsTiWenType
        {
            //1: ����  2: Ҹ�� 3: ����
            KouWen = 8802,
            YeWen = 8801,
            GangWen = 8803
        }

        /// <summary>
        /// �߶�����
        /// </summary>
        public enum LineType
        {
            SolidLine = 1, //ֱ��
            DashLine = 2   //����
        }

        #endregion

        #region ����

        #region ���ⲿ���趨
        //ҽԺ����
        string m_FontFamily = "����"; //�����������ֵ�����
        int m_FontSizeHospitalName = 18;//�����������ֵ������С
        int m_HospitalNameY = 24; //������������Y�᷽���ϵľ���20

        //������
        string m_HeaderName = "��  ��  ��";
        int m_HeaderNameY = 65; //Y�᷽���ϵľ���//60
        int m_FontSizeHeaderName = 18;

        //���˻�����Ϣ
        DataTable m_DataTableTableBaseLine = new DataTable(); //���±��ϲ������е����ݣ���С���������������е�����
        #endregion

        #region ҳ���С�趨
        int m_PageWidth = 840;//�����趨PictureBox�Ŀ��
        int m_PageHeight = 1160;//�����趨PictureBox�ĸ߶� 1160
        #endregion

        #region �������趨

        int m_TableStartPointX = 10;  //������Ͻǵ�X�᷽���ϵ�����
        int m_TableStartPointY = 120; //������Ͻǵ�Y�᷽���ϵ�����120

        int m_FirstColumnWidth = 100;//����һ�еĿ�ȡ�Ҳ���ǡ����ڡ���һ�еĿ�ȡ�
        int m_FirstColumnHasSubColumnCount = 0;//��ʾ��������������ʱ�䡱�µ�һ�е�һ���м�������, ����m_ArrayListVitalSigns.count��������Ĭ�ϰ���3�У����¡�������������

        int m_LineHeight1 = 20;//�����������������ط����иߡ�������ͼ����ı����иߡ�20

        int m_Days = 7;//���±�һ����ʾ���졾Ĭ����ʾһ�ܵ����ݡ�

        int m_DayTimePoint = 6; //ÿ���ʱ�����,Ĭ��Ϊ6��ʱ��㡾Ĭ�������ʱ����� 2��6��10��14��18��22 ����ÿ���ʱ�����Ϊ6 *****�˴�ʱ��������ݿ�������******��
        DataTable m_DataTableDayTimePoint = new DataTable();//ÿ������ʱ��㡾Ĭ�������ʱ����� 2��6��10��14��18��22 *****�˴�ʱ��������ݿ�������******��
        int m_LineHeight2 = 14;//��������ÿС��߶ȣ����������������Ը߶ȺͿ����ȣ���������ͼ��ÿһС��ĸ߶ȡ���ȡ�

        int m_CellCount = 45; //��¼����ͼ��Y�᷽��һ���ж��ٸ�С����--------------40
        int m_CellCountInEveryDegree = 5; //�����ֵĺ���֮���ж��ٸ�С����

        ArrayList m_ArrayListVitalSigns = new ArrayList(); //��Ҫ��ʾ������ͼ�е�������ͨ���˱�����������Щ����Ӧ����ʾ������ͼ��,Ĭ��������������������������¡�
        ArrayList m_ArrayListOther = new ArrayList();//���±�ײ���������Щ�У���С�������������µ��У�������Ѫѹ�������������ȣ����������������У����������ϱ�����PatientInfo.DataTableHuXi���У�

        int m_DayTimePointXuYa = 2;//ÿ��Ѫѹ��ʱ�������ÿ�����Ѫѹ�Ĵ�����

        DateTime m_DataTimeAllocate = System.DateTime.Now;//����ָ��ĳһ�죬һ������ϵͳ��ǰʱ�䣬�벡�˵���Ժʱ����бȽϣ��õ����±��һ�����ڵ�ֵ
        DataTable m_DateTimeEveryColumnDateTime = new DataTable();//�������±���ÿ����ʾ��ʱ�䡾ÿ�����±�ĵ�һ�춼��ʾ����-��-�ա�,�����¸��µ�����ʱ��ʾ����-�ա���������ʾ���ա���

        //float m_PaintEventInformationPosition = 42f; //������Ժ��ת�롢���������䡢��Ժ����������Ϣ��д��λ�á�Ĭ��Ϊ��43�ȿ�ʼ��λ�����¿�ʼд��

        int m_RowCaptionIndent =10; //������������ͼ�·���ÿ�б��������ֵ Add By wwj 2012-05-15

        int m_ID = 0;//���ڱ�ʾ�ڻ��Ƹ�����ʱ��Ϊÿ��������ΨһID,�������VitalSignsPosition������ͼ��ÿ���ڵ㶼�����һ��ID������Ψһ��ʾ��
        ArrayList m_ArrayListPoint = new ArrayList();//����ͼ�и����������
        ArrayList m_ArrayListPointLine = new ArrayList();//����ͼ�и�����֮�������

        int m_Distance = 2;//���10�����ص�ʱ����������Ϊ�Ǻϲ���һ��
        ArrayList m_ArrayListConflictPoint = new ArrayList();//����ͼ�����غϵĵ�

        float m_TableHeight = 0;//���±���ܸ߶�

        bool m_IsShowTiWen = true; //�Ƿ���ʾ����,����CheckBox
        bool m_IsShowMaiBo = true; //�Ƿ���ʾ����,����CheckBox
        bool m_IsShowHuXi = true;  //�Ƿ���ʾ����,����CheckBox


        PatientInfoLocation patientInfoLocation = new PatientInfoLocation();//���˻�����Ϣ��λ����λ��

        Picture m_Picture = new Picture();//��Ҫ���Ƶ�ͼƬ�趨

        bool m_IsComputeLocation = false;//��ʾ�Ƿ���������ͼ�е������

        List<DataRow> m_EventSetting;//�¼����ã������Ƿ���ʾʱ�䡢��ʾλ��
        #endregion

        #endregion

        #region Property && Field

        string m_HospitalName = "";
        private string HospitalName
        {
            get
            {
                if (string.IsNullOrEmpty(m_HospitalName))
                {
                    //�õ�ҽԺ��Ϣ
                    GetHospitalName();
                }
                return m_HospitalName;
            }
        }

        private string m_Times = "";
        /// <summary>
        /// �������¼���ʱ��㣬��̬��� ywk 
        /// </summary>
        public string[] TimesArray
        {
            get
            {
                if (string.IsNullOrEmpty(m_Times))
                {
                    m_Times = PublicSet.MethodSet.GetConfigValueByKey("VITALSIGNSRECORDTIME");
                }
                string[] timeArray = m_Times.Split(',');
                return timeArray;
            }

        }

        //Add by wwj 2012-06-05 �������þ�����λ����ʾ

        /// <summary>
        /// Ĭ�ϴ�ӡA4ֽ
        /// </summary>
        private string m_DefaultPrintSize = "A4";

        /// <summary>
        /// Ĭ��������ͼ����ʾ ��������ʾ��ʽ 1.����ͼ����ʾ 2.������ͼ����ʾ
        /// </summary>
        private string m_HuXiShowType = "1";

        /// <summary>
        /// Ĭ����ʾ��ʹָ��
        /// </summary>
        private string m_IsShowTengTongZhiShu = "1";

        /// <summary>
        /// Ĭ�ϲ���12Сʱ��
        /// </summary>
        private string m_Is12Сʱ�� = "0";

        /// <summary>
        /// Ĭ�ϵ�һ��������ʾ���Ϸ� 1���Ϸ� 0���·�
        /// </summary>
        private string m_FirstHuXiUp = "1";

        /// <summary>
        /// ���˵�ĳ��״̬��������ͼ�ڵ�Ͽ������ 
        /// ��ʿ�ڻ������ⵥ����ʱ
        /// ���������ĳһʱ��㲻�ڣ���ô��һ������������Ͳ�����д���ڻ������ⵥ����ʱ���Ͽ���һ�㣨���˵��ǰ�����㲻��������һ��
        /// ���磺2��6��10���㣬���6��û��������������ô2��10���㲻�����ӡ�
        /// </summary>
        private List<string> m_�Ͽ�״̬ = new List<string>();

        /// <summary>
        /// ��¼��Ҫ�Ͽ����ߵ�X�����ϵ�����
        /// </summary>
        private List<int> m_Need�Ͽ�LocationX = new List<int>();

        /// <summary>
        /// ��ʼ�����ⵥ���� Add by wwj 2012-06-05 �������þ�����λ����ʾ
        /// </summary>
        public void InitThreeMeasureTableSetting()
        {
            string setting = PublicSet.MethodSet.GetConfigValueByKey("ThreeMeasureTableSetting");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(setting);
            m_DefaultPrintSize = doc.GetElementsByTagName("DefaultPageSize")[0].InnerText.Trim();
            //m_HuXiShowType = doc.GetElementsByTagName("HuXiShowType")[0].InnerText.Trim();
            m_IsShowTengTongZhiShu = doc.GetElementsByTagName("TengTongZhiShuShow")[0].InnerText.Trim();
            m_Is12Сʱ�� = doc.GetElementsByTagName("Is12Сʱ��")[0].InnerText.Trim();
            m_FirstHuXiUp = doc.GetElementsByTagName("FirstHuXiUp")[0].InnerText.Trim();
            string[] statuses = doc.GetElementsByTagName("�Ͽ�״̬")[0].InnerText.Trim().Split(',');
            m_�Ͽ�״̬.Clear();
            foreach (string status in statuses) m_�Ͽ�״̬.Add(status);
        }
        #endregion

        #region ���캯��, �����ʼ��Load
        public UCThreeMeasureTable()
        {
            InitializeComponent();
            pictureBoxMeasureTable.Paint += new PaintEventHandler(picture_Paint);
            LoadDataDefault();
        }

        public void LoadDataDefault()
        {
            try
            {
                if (this.DesignMode == true)
                {
                    return;
                }
                m_IsComputeLocation = true;

                PictureBox picture = this.pictureBoxMeasureTable;
                ThreeMeasureTableConfig threeMeasureTableConfig = new ThreeMeasureTableConfig();

                this.Font = new Font("����", 9.0f, FontStyle.Regular);

                picture.Width = m_PageWidth;
                picture.Height = m_PageHeight;
                picture.BackColor = Color.White;
                picture.Location = new Point(10, 10);

                //����������ֵ����������Щ��
                m_DataTableTableBaseLine = threeMeasureTableConfig.InitTableHead();

                //��ʼ��ÿ��ʱ������Ϣ
                m_DataTableDayTimePoint = threeMeasureTableConfig.GetTimePointDefault();
                m_DayTimePoint = m_DataTableDayTimePoint.Rows.Count;

                //��ʼ�� ������������ ���� ��������������������
                m_ArrayListVitalSigns = threeMeasureTableConfig.InitFirstColumnHasSubColumn();
                m_FirstColumnHasSubColumnCount = m_ArrayListVitalSigns.Count;

                //��ʼ�� Ѫѹ�����������ܳ�����������������������ߣ����أ�����ҩ�����������
                m_ArrayListOther = threeMeasureTableConfig.InitVitalSignsOtherAtTableBottom(m_DayTimePointXuYa);

                //��̬����Button
                CreateCheckBox();
            }
            catch (Exception e)
            {
                //todo  log ex

            }
        }

        public void LoadData()
        {
            if (this.DesignMode == true)
            {
                return;
            }

            //��ʾ�ڻ������ߵ�ʱ�����¼������ߵ������
            m_IsComputeLocation = false;

            PictureBox picture = this.pictureBoxMeasureTable;
            ThreeMeasureTableConfig threeMeasureTableConfig = new ThreeMeasureTableConfig();

            this.Font = new Font("����", 9.0f, FontStyle.Regular);

            picture.Width = m_PageWidth;
            picture.Height = m_PageHeight;
            picture.BackColor = Color.White;
            picture.Location = new Point(80, 10);

            //��ʼ�����ⵥ����
            InitThreeMeasureTableSetting();

            //��ʼ����������Դ
            PatientInfo patientInfo = new PatientInfo();
            patientInfo.InitData();


            //����������������ֵ
            DataTable dataTimeForColumns = GetDateTimeForColumns(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate);
            if (dataTimeForColumns.Rows.Count > 0)
            {
                patientInfo.GetNursingRecordByDate(dataTimeForColumns.Rows[0][0].ToString(),
                    dataTimeForColumns.Rows[dataTimeForColumns.Rows.Count - 1][0].ToString());
            }

            //����������ֵ����������Щ��
            m_DataTableTableBaseLine = threeMeasureTableConfig.InitTableHead();

            //��ʼ��ÿ��ʱ������Ϣ
            m_DataTableDayTimePoint = PublicSet.MethodSet.GetTimePoint();
            m_DayTimePoint = m_DataTableDayTimePoint.Rows.Count;

            //��ʼ�� ������������ ���� ��������������������
            m_ArrayListVitalSigns = threeMeasureTableConfig.InitFirstColumnHasSubColumn();
            if (m_HuXiShowType == "2")//�������þ���������ʾ��λ��
            {
                foreach (VitalSigns vs in m_ArrayListVitalSigns)
                {
                    if (vs.Name == UCThreeMeasureTable.VitalSignsType.HuXi.ToString())
                    {
                        m_ArrayListVitalSigns.Remove(vs);
                        break;
                    }
                }
            }
            m_FirstColumnHasSubColumnCount = m_ArrayListVitalSigns.Count;


            //��ʼ�� Ѫѹ�����������ܳ�����������������������ߣ����أ�����ҩ�����������
            m_ArrayListOther = threeMeasureTableConfig.InitVitalSignsOtherAtTableBottom(m_DayTimePointXuYa);

            //�õ�����Ҫ��ʾʱ����¼������������������ٵȲ���Ҫ��ʾ����ʱ��
            m_EventSetting = PublicSet.MethodSet.GetNotShowTimeEvent();

            //��̬����Button
            CreateCheckBox();

        }

        #region ��̬����Button
        /// <summary>
        /// ��̬����Button
        /// </summary>
        private void CreateCheckBox()
        {
            int xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + m_DayTimePoint * (m_LineHeight2 + 1) * m_Days + 14;

            for (int i = 0; i < m_ArrayListVitalSigns.Count; i++)
            {
                VitalSigns vitalSigns = m_ArrayListVitalSigns[i] as VitalSigns;
                if (vitalSigns != null)
                {
                    DevExpress.XtraEditors.CheckEdit checkEdit = new CheckEdit();
                    checkEdit.Location = new Point(xPoint, 200 + 40 * i);
                    checkEdit.Name = "checkEdit1";
                    checkEdit.Properties.Caption = GetVitalSignsTypeName(vitalSigns.Name);
                    checkEdit.Size = new System.Drawing.Size(75, 19);
                    checkEdit.Checked = true;
                    checkEdit.CheckedChanged += new EventHandler(checkEdit_CheckedChanged);
                    checkEdit.BringToFront();
                    this.pictureBoxMeasureTable.Controls.Add(checkEdit);
                }
            }
        }
        #endregion

        void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit checkEdit = sender as DevExpress.XtraEditors.CheckEdit;
            if (checkEdit != null)
            {
                if (checkEdit.Text == "����")
                {
                    m_IsShowTiWen = checkEdit.Checked;
                }
                else if (checkEdit.Text == "����")
                {
                    m_IsShowHuXi = checkEdit.Checked;
                }
                else if (checkEdit.Text == "����")
                {
                    m_IsShowMaiBo = checkEdit.Checked;
                }
            }
            pictureBoxMeasureTable.Refresh();
        }

        #endregion

        #region Paint �¼������ڻ��������û��ؼ�
        void picture_Paint(object sender, PaintEventArgs e)
        {
            #region ��ʼ��ͼ֮ǰ��������ڼ�¼�ı���
            m_ID = 0;
            #endregion

            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                PaintNurseDocument(pictureBox, g);
            }
        }

        private void PaintNurseDocument(PictureBox pictureBox, Graphics g)
        {
            //���Ʊ���
            PaintTitle(pictureBox, g);

            //���Ʋ��˻�����Ϣ
            PaintPatientBaseInformation(pictureBox, g);

            //���Ʊ��ĵ�һ����(С��������ͼ��������Ĳ���)
            PaintFirstPartOfTable(pictureBox, g);

            //���Ʊ��ĵڶ�����(С��������ͼ������)
            PaintSecondPartOfTable(pictureBox, g);

            //���Ʊ��ĵ�������(С��������ͼ��������Ĳ���)
            PaintThirdPartOfTable(pictureBox, g);

            //������ʾ����
            //PaintImagePrompt(pictureBox, g);
            PaintImagePromptBottom(pictureBox, g);
        }
        #endregion

        #region �������Ĳ���

        #region ���Ʊ���
        /// <summary>
        /// ���Ʊ���
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintTitle(PictureBox pictureBox, Graphics g)
        {
            //ҽԺ���ƣ���ĳĳҽԺ��
            FontFamily headFontFamily = new FontFamily(m_FontFamily);
            Font font = new Font(headFontFamily, m_FontSizeHospitalName, FontStyle.Regular);
            g.DrawString(HospitalName, font, Brushes.Black, (pictureBox.Width - TextRenderer.MeasureText(m_HospitalName, font).Width) / 2 - 35, m_HospitalNameY);

            //���⣬�����±�
            font = new Font(headFontFamily, m_FontSizeHeaderName, FontStyle.Bold);
            g.DrawString(m_HeaderName, font, Brushes.Black, (pictureBox.Width - TextRenderer.MeasureText(m_HeaderName, font).Width) / 2 - 35, m_HeaderNameY);

        }
        #endregion

        #region ���Ʋ��˻�����Ϣ��λ�ڱ��������ͱ������棩
        /// <summary>
        /// ���Ʋ��˻�����Ϣ��λ�ڱ��������ͱ������棩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="g"></param>
        private void PaintPatientBaseInformation(PictureBox pictureBox, Graphics g)
        {
            Font font = this.Font;
            string str = "����:";
            g.DrawString(str, font, Brushes.Black, patientInfoLocation.InpatientNameX, patientInfoLocation.InpatientInformationY);
            g.DrawString(PatientInfo.InpatientName, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.InpatientNameX, patientInfoLocation.InpatientInformationY);

            str = "����:";//���Ӥ�������ⵥ����ʾ��������λ�ӿ� eidt by ywk 2012��8��30�� 08:59:28
            g.DrawString(str, font, Brushes.Black, patientInfoLocation.AgeX+8, patientInfoLocation.InpatientInformationY);
            g.DrawString(PatientInfo.Age, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.AgeX, patientInfoLocation.InpatientInformationY);

            str = "�Ա�:";
            g.DrawString(str, font, Brushes.Black, patientInfoLocation.GenderX, patientInfoLocation.InpatientInformationY);
            g.DrawString(PatientInfo.Gender, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.GenderX, patientInfoLocation.InpatientInformationY);

            str = "�Ʊ�:";
            g.DrawString(str, font, Brushes.Black, patientInfoLocation.SectionX, patientInfoLocation.InpatientInformationY);
            g.DrawString(PatientInfo.Section, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.SectionX, patientInfoLocation.InpatientInformationY);

            str = "����:";
            g.DrawString(str, font, Brushes.Black, patientInfoLocation.BedCodeX, patientInfoLocation.InpatientInformationY);
            g.DrawString(PatientInfo.BedCode, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.BedCodeX, patientInfoLocation.InpatientInformationY);

            str = "��Ժ����:";
            g.DrawString(str, font, Brushes.Black, patientInfoLocation.InTimeX, patientInfoLocation.InpatientInformationY);
            g.DrawString(PatientInfo.InTime.Split(' ')[0], font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.InTimeX, patientInfoLocation.InpatientInformationY);


            //�����Ӥ��������ʾ��Ӥ��ĸ�׵�סԺ�����ţ����������ݿ��д洢��GUID���� add by ywk 2012��7��30�� 13:43:08 
            if (PatientInfo.IsBaby == "1")//�����Ӥ��
            {
                str = "סԺ������:";
                g.DrawString(str, font, Brushes.Black, patientInfoLocation.InpatientNoX, patientInfoLocation.InpatientInformationY);
                string babyPatid = PublicSet.MethodSet.GetPatData(PatientInfo.Mother).Rows[0]["Patid"].ToString();
                g.DrawString(babyPatid, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.InpatientNoX, patientInfoLocation.InpatientInformationY);
            }
            else
            {
                str = "סԺ������:";
                g.DrawString(str, font, Brushes.Black, patientInfoLocation.InpatientNoX, patientInfoLocation.InpatientInformationY);
                g.DrawString(PatientInfo.InpatientNo, font, Brushes.Black, TextRenderer.MeasureText(str, font).Width + patientInfoLocation.InpatientNoX, patientInfoLocation.InpatientInformationY);
            }
        }
        #endregion

        #endregion

        #region ���Ʊ�񲿷�

        #region ���Ʊ��ĵ�һ���֣������±���С����������ļ��У�һ����������ڡ�סԺ������������������ʱ��ȣ���Ҫ����m_TableBaseLineCount��������
        /// <summary>
        /// ���Ʊ��ĵ�һ���֣������±���С����������ļ��У�һ����������ڡ�סԺ������������������ʱ��ȣ���Ҫ����m_TableBaseLineCount��������
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintFirstPartOfTable(PictureBox pictureBox, Graphics g)
        {
            Pen penBold = new Pen(Color.Black, 2);
            Pen pen = Pens.Black;
            Font font = this.Font;

            //�˿�Ȳ�������������1���أ�����������ұ�2����
            int tableWidth = m_FirstColumnWidth/*��һ�еĿ��*/ + 1 + (m_LineHeight2 * m_DayTimePoint + m_DayTimePoint) * m_Days + 1;

            //��¼�¡�ʱ�䡱����һ�����ߵ�λ����Ϣ
            int currentLinePointY = m_TableStartPointY + (m_LineHeight1 + 1) * m_DataTableTableBaseLine.Rows.Count; //��¼�µ�ǰ���ߵ�Y���ϵ�ֵ
            int cruuentLineEndLinePintX = m_TableStartPointX + tableWidth; //��¼�º����յ�X�᷽���ֵ

            #region ���ƺ��ߺ�����

            //���ƺ���
            g.DrawLine(penBold, m_TableStartPointX, m_TableStartPointY, m_TableStartPointX + tableWidth, m_TableStartPointY);//���Ʊ����ϵ��µĵ�һ������
            for (int i = 1; i <= m_DataTableTableBaseLine.Rows.Count; i++)
            {
                int pointY = m_TableStartPointY + (m_LineHeight1 + 1) * i;
                g.DrawLine(pen, m_TableStartPointX, pointY, m_TableStartPointX + tableWidth, pointY);
            }

            //��������

            #region ��������ĸ߶�
            float tableHeight = m_DataTableTableBaseLine.Rows.Count * (m_LineHeight1 + 1) +
                (m_CellCount / m_CellCountInEveryDegree) * ((m_LineHeight2 + 1) * m_CellCountInEveryDegree + 1) +
                (m_CellCount % m_CellCountInEveryDegree) * (m_LineHeight2 + 1) +
                m_ArrayListOther.Count * (m_LineHeight1)+15;//+1 ywk    23


            //�жϺ�������һ����ʾ������������ͼ�б�ʾ�����������һ����ʾ������Ҫ���Ϻ�����һ�еĸ߶�
            if (checkIsContainHuXiInCurve() == false)
            {
                tableHeight += m_LineHeight2 * 2 + 1;//+1
            }

            m_TableHeight = tableHeight;
            #endregion


            g.DrawLine(penBold, m_TableStartPointX, m_TableStartPointY, m_TableStartPointX, m_TableStartPointY + tableHeight);//���Ʊ������ҵĵ�һ������
            for (int i = 0; i < m_Days; i++)
            {
                int pointX = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_DayTimePoint * m_LineHeight2 + m_DayTimePoint) * i;
                g.DrawLine(pen, pointX, m_TableStartPointY, pointX, m_TableStartPointY + tableHeight);
            }

            //���Ʊ������ҵ����һ������
            g.DrawLine(penBold, m_TableStartPointX + tableWidth, m_TableStartPointY, m_TableStartPointX + tableWidth, m_TableStartPointY + tableHeight);


            //����ʱ���һ�е�����
            if (CheckIsNeed(m_DataTableTableBaseLine, "ʱ   ��") != -1)
            {
                //�����ʱ����һ�У���ÿ��ĸ���ʱ�����ʾ����
                for (int i = 0; i < m_DayTimePoint * m_Days; i++)
                {
                    float xValue = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * i;
                    float yValue = m_TableStartPointY + (m_DataTableTableBaseLine.Rows.Count - 1) * (1 + m_LineHeight1);
                    g.DrawLine(Pens.Black, xValue, yValue, xValue, yValue + m_LineHeight1);
                }
            }
            #endregion

            #region �������ֺ�����

            //����m_DataTableTableBaseLine�ڱ���о�����ʾ�硰���ڡ�����סԺ�������� ����������������ʱ�䡱��
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;//�������
            centerFormat.LineAlignment = StringAlignment.Center;//�������

            for (int i = 0; i < m_DataTableTableBaseLine.Rows.Count; i++)
            {
                g.DrawString(m_DataTableTableBaseLine.Rows[i][0].ToString(), font, Brushes.Black,
                    new RectangleF(m_TableStartPointX + 1, m_TableStartPointY + 1 + (m_LineHeight1 + 1) * i, m_FirstColumnWidth, m_LineHeight1 + 2), centerFormat);
            }

            //���� ������ �е�����
            int rowIndex = 0;
            if (CheckIsNeed(m_DataTableTableBaseLine, "��   ��") != -1)
            {
                m_DateTimeEveryColumnDateTime = GetDateTimeForColumns(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate);

                if (m_DateTimeEveryColumnDateTime.Rows.Count > 0)
                {
                    for (int i = 0; i < m_Days; i++)
                    {
                        RectangleF rect = new RectangleF(m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * i + 1,
                            m_TableStartPointY + 1, (m_LineHeight2 + 1) * m_DayTimePoint, m_LineHeight1 + 1);
                        g.DrawString(m_DateTimeEveryColumnDateTime.Rows[i][1].ToString(), font, Brushes.Black, rect, centerFormat);
                    }
                }

                rowIndex++;
            }

            //���� סԺ���� �е�����
            if (CheckIsNeed(m_DataTableTableBaseLine, "סԺ����") != -1)
            {
                //if (m_DateTimeEveryColumnDateTime.Rows.Count > 0)
                //{
                //    for (int i = 0; i < m_Days; i++)
                //    {
                //        RectangleF rect = new RectangleF(m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * i + 1,
                //            m_TableStartPointY + m_LineHeight1 + 1 + 1, (m_LineHeight2 + 1) * m_DayTimePoint, m_LineHeight1 + 1);

                //        int inDays = (Convert.ToDateTime(m_DateTimeEveryColumnDateTime.Rows[i][0]) - Convert.ToDateTime(PatientInfo.InTime.Split(' ')[0])).Days + 1;
                //        g.DrawString(inDays.ToString(), font, Brushes.Black, rect, centerFormat);
                //    }
                //}

                foreach (DataRow dr in PatientInfo.DataTableDayOfHospital.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(dr["DateTime"].ToString());
                    string timePoint = dr["TimePoint"].ToString();
                    string value = dr["Value"].ToString();

                    float xPoint = computeLocationBottomX(dateTime, timePoint);
                    RectangleF rect = new RectangleF(xPoint, m_TableStartPointY + rowIndex * (m_LineHeight1 + 1) + 1, (m_LineHeight2 + 1) * m_DayTimePoint, m_LineHeight1 + 1);
                    g.DrawString(value, font, Brushes.Black, rect, centerFormat);
                }

                rowIndex++;
            }

            //���� ���������� �е�����
            if (CheckIsNeed(m_DataTableTableBaseLine, "����������") != -1)
            {
                foreach (DataRow dr in PatientInfo.DataTableOperationTime.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(dr["DateTime"].ToString());
                    string timePoint = dr["TimePoint"].ToString();
                    string value = dr["Value"].ToString();

                    float xPoint = computeLocationBottomX(dateTime, timePoint);
                    RectangleF rect = new RectangleF(xPoint, m_TableStartPointY + rowIndex * (m_LineHeight1 + 1) + 1, (m_LineHeight2 + 1) * m_DayTimePoint, m_LineHeight1 + 1);
                    g.DrawString(value, font, Brushes.Red, rect, centerFormat);
                }

                rowIndex++;
            }


            //����ʱ����е�����
            if (CheckIsNeed(m_DataTableTableBaseLine, "ʱ   ��") != -1)
            {
                DataTable Newdt = m_DataTableDayTimePoint.Clone();
                for (int i = 0; i < m_DataTableDayTimePoint.Rows.Count; i++)
                {
                    Newdt.ImportRow(m_DataTableDayTimePoint.Rows[i]);
                }
                //������ҽԺ����3 19 23 ʱ�������Ϊ��ɫ 
                for (int j = 0; j < Newdt.Rows.Count; j++)
                {
                    if (Newdt.Rows[j]["TimePoint"].ToString() == "3" || Newdt.Rows[j]["TimePoint"].ToString() == "19" || Newdt.Rows[j]["TimePoint"].ToString() == "23")
                    {
                        Newdt.Rows[j]["Color"] = "red";
                    }
                }

                for (int i = 0; i < m_DayTimePoint * m_Days; i++)
                {
                    float xValue = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * i;
                    float yValue = m_TableStartPointY + (m_DataTableTableBaseLine.Rows.Count - 1) * (1 + m_LineHeight1);
                    RectangleF rect = new RectangleF(xValue - 1, yValue, m_LineHeight2 + 4, m_LineHeight1 + 1);

                    Font fontTimePoint = new Font(this.Font.FontFamily, 8);
                    int index = i % m_DayTimePoint;

                    //�������þ����Ƿ���12Сʱ�� Add by wwj 2012-06-12
                    int timePoint = Convert.ToInt32(Newdt.Rows[index][0]);//m_DataTableDayTimePoint
                    if (m_Is12Сʱ�� == "1" && timePoint > 12)
                    {
                        timePoint -= 12;
                    }

                    g.DrawString(timePoint.ToString(), fontTimePoint,
                        GetBrushByColorName(Newdt.Rows[index][1].ToString()), rect, centerFormat);
                }
            }

            #endregion
        }
        #endregion

        #region ���Ʊ��ĵڶ����֣������±���С�������е��У�
        /// <summary>
        /// ���Ʊ��ĵڶ����֣������±���С�������е��У�
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintSecondPartOfTable(PictureBox pictureBox, Graphics g)
        {
            Font font = this.Font;
            Pen penBold = new Pen(Color.Black, 2);//���Ϊ2�ĺڱ�
            Pen pen = Pens.Black;//���Ϊ1�ĺڱ�
            Pen penLightBlue = Pens.LightBlue;//���Ϊ1������

            //�˿�Ȳ�������������1���أ�����������ұ�2����
            int tableWidth = m_FirstColumnWidth/*��һ�еĿ��*/ + 1 + (m_LineHeight2 * m_DayTimePoint + m_DayTimePoint) * m_Days + 1;

            //��¼�¡�ʱ�䡱����һ�����ߵ�λ����Ϣ
            int currentLinePointY = m_TableStartPointY + (m_LineHeight1 + 1) * m_DataTableTableBaseLine.Rows.Count; //��¼�µ�ǰ���ߵ�Y���ϵ�ֵ
            int cruuentLineEndLinePintX = m_TableStartPointX + tableWidth; //��¼�º����յ�X�᷽���ֵ


            #region **********************************************************���漰���¶ȵ�С������********************************************************

            //��ʱ��εķָ���(��ֱ����)
            for (int i = 0; i < m_Days; i++) //ѭ��ÿ��
            {
                int xPoint = 0;
                for (int j = 1; j <= m_DayTimePoint - 1; j++)//ѭ��ÿ��ʱ���
                {
                    xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * j + (m_DayTimePoint + m_DayTimePoint * m_LineHeight2) * i;
                    g.DrawLine(penLightBlue, xPoint, currentLinePointY + 1, xPoint,
                        currentLinePointY + m_CellCount / m_CellCountInEveryDegree * (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) - 1);//��ɫ���߶�
                }

                if (i != m_Days - 1)
                {
                    xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint + (m_DayTimePoint + m_DayTimePoint * m_LineHeight2) * i;
                    g.DrawLine(Pens.Red, xPoint, currentLinePointY + 1, xPoint,
                        currentLinePointY + m_CellCount / m_CellCountInEveryDegree * (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) - 1);//��ɫ���߶�
                }
            }

            //��ʱ��εķָ���(����)
            for (int i = 0; i < m_CellCount / m_CellCountInEveryDegree; i++)//�����֮��Ĵ���
            {
                int yPoint = 0;

                for (int j = 1; j <= m_CellCountInEveryDegree - 1; j++) //ÿһ��֮���ϸ��
                {
                    yPoint = currentLinePointY + (m_LineHeight2 + 1) * j + (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) * i;
                    g.DrawLine(penLightBlue, m_TableStartPointX + m_FirstColumnWidth + 2, yPoint, cruuentLineEndLinePintX - 2, yPoint);//��ɫ���߶�
                }

                if (i != m_CellCount / m_CellCountInEveryDegree - 1)
                {
                    yPoint = currentLinePointY + (m_LineHeight2 + 1) * m_CellCountInEveryDegree + 1/*���߿��Ϊ2�����µ�����*/ + (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) * i;
                    g.DrawLine(penBold, m_TableStartPointX + m_FirstColumnWidth + 2, yPoint, cruuentLineEndLinePintX - 2 + 1/*���ڴ��߱�ϸ��Ҫ��1�����أ���������Ҫ��1*/, yPoint);//С���������мӴֵĺ�ɫ�߶�
                }
                else
                {
                    //if (i == 3)
                    //{
                    //    yPoint = currentLinePointY + (m_LineHeight2 + 1) * m_CellCountInEveryDegree + (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) * i;
                    //    g.DrawLine(Pens.Red, m_TableStartPointX + 1, yPoint, cruuentLineEndLinePintX - 2, yPoint);//С�������������һ�������ĺ�ɫ�߶�

                    //}
                    //else
                    //{
                    yPoint = currentLinePointY + (m_LineHeight2 + 1) * m_CellCountInEveryDegree + (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) * i;
                    g.DrawLine(Pens.Black, m_TableStartPointX + 1, yPoint, cruuentLineEndLinePintX - 2, yPoint);//С�������������һ�������ĺ�ɫ�߶�

                    //}
                }
                Pen penBold1 = new Pen(Color.Red, 2);//���Ϊ2�ĺڱ�
                if (i == 5)//���¶�Ϊ43��iΪ3ThreeMeasureTableConfig��  xll���¶�Ϊ42 �ɽ�i��Ϊ4 ʵ��37��Ϊ����
                {
                    yPoint = currentLinePointY + (m_LineHeight2 + 1) * m_CellCountInEveryDegree + (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) * i;
                    g.DrawLine(penBold1, m_TableStartPointX + m_FirstColumnWidth + 2, yPoint, cruuentLineEndLinePintX - 2 + 1, yPoint);//С�������������һ�������ĺ�ɫ�߶�
                }
            }

            //��ʱ��εķָ���(��ֱ����),���ڸ������ڻ����ߵ������߱��ֶ�
            for (int i = 1; i < m_Days; i++) //ѭ��ÿ��
            {
                int xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_DayTimePoint + m_DayTimePoint * m_LineHeight2) * i;
                g.DrawLine(Pens.Black, xPoint, currentLinePointY + 1, xPoint,
                    currentLinePointY + m_CellCount / m_CellCountInEveryDegree * (m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree + 1) - 2);//��ɫ�߶�������߶Σ����������Ϊ��ɫ
            }

            #endregion

            #region ����С������ߵġ������������������������¡���
            //�ָ���������������� ��
            for (int i = 1; i <= m_FirstColumnHasSubColumnCount - 1; i++)
            {
                int xPoint = m_TableStartPointX + (m_FirstColumnWidth / m_FirstColumnHasSubColumnCount) * i;
                g.DrawLine(Pens.Black, xPoint, currentLinePointY + 1, xPoint,
                    currentLinePointY + m_CellCount / m_CellCountInEveryDegree * (2 + m_CellCountInEveryDegree * m_LineHeight2 + m_CellCountInEveryDegree - 1) - 2);
            }

            //���������������������������¡��ȵ�����
            Font fontSmall = new Font(this.Font.FontFamily, 8);
            for (int i = 0; i < m_ArrayListVitalSigns.Count; i++)
            {
                VitalSigns vs = m_ArrayListVitalSigns[i] as VitalSigns;

                if (vs != null)
                {
                    //�������ƣ��硰���¡�
                    string name = GetVitalSignsTypeName(vs.Name);
                    RectangleF rectangleFVitalSigns = new RectangleF(m_TableStartPointX - m_CellCountInEveryDegree - 1 + (m_FirstColumnWidth / m_FirstColumnHasSubColumnCount) * i, currentLinePointY + 2,
                        m_FirstColumnWidth / m_FirstColumnHasSubColumnCount + 14,
                        (TextRenderer.MeasureText("name", this.Font).Height) * 1.5f);

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    g.DrawString(name, font, Brushes.Black, rectangleFVitalSigns, sf);

                    //���Ƶ�λ���硰�����϶ȣ���
                    name = "(" + vs.Unit + ")";
                    rectangleFVitalSigns = new RectangleF(m_TableStartPointX - m_CellCountInEveryDegree - 1 + (m_FirstColumnWidth / m_FirstColumnHasSubColumnCount) * i,
                        currentLinePointY + (TextRenderer.MeasureText("name", this.Font).Height) * 1.5f,
                        m_FirstColumnWidth / m_FirstColumnHasSubColumnCount + 14,
                        (TextRenderer.MeasureText("name", this.Font).Height) * 1.5f);
                    g.DrawString(name, fontSmall, Brushes.Black, rectangleFVitalSigns, sf);
                }
            }

            //���������������������������¡��ȵĶ������ĳ߶�
            for (int i = 0; i < m_ArrayListVitalSigns.Count; i++)
            {
                VitalSigns vs = m_ArrayListVitalSigns[i] as VitalSigns;

                float maxValue = 0f;
                float value = 0f;
                float cellValue = 0f;

                if (vs != null)
                {
                    cellValue = vs.CellValue;
                    maxValue = vs.MaxValue;


                    float xPoint = m_TableStartPointX + (m_FirstColumnWidth / m_FirstColumnHasSubColumnCount + 1) * i;
                    float yPoint = 0f;

                    for (int j = 1; j < m_CellCount / m_CellCountInEveryDegree; j++)
                    {
                        value = maxValue - cellValue * m_CellCountInEveryDegree * j;

                        yPoint = currentLinePointY + ((m_LineHeight2 + 1) * m_CellCountInEveryDegree + 1) * j - TextRenderer.MeasureText(value.ToString(), this.Font).Height / 2;

                        RectangleF rectangleF = new RectangleF(xPoint, yPoint, m_FirstColumnWidth / m_FirstColumnHasSubColumnCount, TextRenderer.MeasureText(value.ToString(), this.Font).Height);
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        g.DrawString(value.ToString(), this.Font, Brushes.Black, rectangleF, sf);
                    }
                }
            }
            #endregion

            #region ���� ������Ժ��ת�롢���������䡢��Ժ��������,��������д����ʱ���⣬�������24Сʱ�ƣ���ȷ������(PatientInfo.DateTableEventInformation)
            PatientInfo patientInfo = new PatientInfo();
            int pointYOffset = 0; //Y�᷽���ϵ�ƫ����
            string lastTimePoint = "-1"; //��¼��һ���¼����ڵ�ʱ��㣬���ڴ�����ͬһʱ�����¼���һ����ʾ���������غ���һ��
            DateTime lastDateTime = DateTime.MinValue;
            m_Need�Ͽ�LocationX = new List<int>();
            //����Ӥ������ʾ���ʱ��
            string m_noofinpat = PatientInfo.NoOfInpat;//ȡ�ò��˵���ҳ���
            string m_isbaby = string.Empty;//�Ƿ���Ӥ�� 
            DataTable m_Dt = MethodSet.GetPatData(m_noofinpat);
            if (m_Dt != null && m_Dt.Rows != null && m_Dt.Rows.Count>0)
            {
                m_isbaby = m_Dt.Rows[0]["isbaby"].ToString();
            }
            for (int i = 0; i < PatientInfo.DateTableEventInformation.Rows.Count; i++)
            {
                DateTime dateTime = Convert.ToDateTime(PatientInfo.DateTableEventInformation.Rows[i]["DateTime"]);
                string timePoint = patientInfo.GetTimePointInTime(m_DataTableDayTimePoint, dateTime.ToString("yyyy-H").Split('-')[1]);
                string eventInformation = PatientInfo.DateTableEventInformation.Rows[i]["EventInformation"].ToString();
                string eventInformationTemp = eventInformation.TrimEnd('-');
                //string statusid = PatientInfo.DateTableEventInformation.Rows[i]["StatusID"].ToString();
                //if (m_isbaby == "1")//�����Ӥ�� add by ywk
                //{
                //    eventInformation = "";
                //    eventInformationTemp = "";
                //}
                float eventLocation = GetEventLocation(eventInformation.TrimEnd('-'));
                if (eventLocation <= 35)
                {
                    pointYOffset = 0;
                }

                if (lastTimePoint != timePoint || lastDateTime.Date != dateTime.Date)
                {
                    lastTimePoint = timePoint;
                    lastDateTime = dateTime;
                    pointYOffset = 0;
                }

                if (!CheckEventIsShowTime(eventInformation.TrimEnd('-')))
                {
                    eventInformation = eventInformation.TrimEnd('-');
                }
                else if (m_Is12Сʱ�� == "0") //�������þ����Ƿ���12Сʱ�� Add by wwj 2012-06-12
                {
                    eventInformation += patientInfo.GetHourAndMinute(dateTime);
                }
                else if (m_Is12Сʱ�� == "1") //�������þ����Ƿ���12Сʱ�� Add by wwj 2012-06-12
                {
                    eventInformation += patientInfo.GetHourAndMinute2(dateTime);
                }

                //if (m_isbaby == "1" && eventInformation.Contains("��Ժ"))//�����Ӥ�� add by ywk 2012��7��26��11:32:29
                //��������Ժ����ʾʱ�䣬��Ӧ��Ժ���Ҫ��ʾ��edit by ywk 2012��8��30�� 09:16:05
                    if (m_isbaby == "1" && eventInformation.Equals("��Ժ"))//�����Ӥ�� add by ywk 2012��7��26��11:32:29
                {
                    eventInformation = "";
                    eventInformationTemp = "";
                }
                #region ����� ��Ҫ���Ƶ��¼� �� ���� �� ʱ���
                int daySpan = 0;//�������¼��������µ��ϵ�һ�������ڵļ������
                if (m_DateTimeEveryColumnDateTime.Rows.Count > 0)
                {
                    DateTime firstDateTime = Convert.ToDateTime(m_DateTimeEveryColumnDateTime.Rows[0][0]);
                    daySpan = (dateTime - firstDateTime).Days;
                    //if (dateTime.Day - firstDateTime.Day > 0)
                    //{
                    //    continue;
                    //}
                    //������㣬�����һ�죬ʱ���������һ��ʱ����״̬ʱ����ڵ�ǰ����һ�����ڵ���ʾ����
                    //�Ӳ��ж�add by ywk
                    if (daySpan < 0 || daySpan >= m_Days || daySpan == 0 && !firstDateTime.ToString("yyyy-MM-dd").Equals(dateTime.ToString("yyyy-MM-dd"))) //��ʾ����¼��Ѿ��������������µ������ڷ�Χ������Ҫ�ų�
                    {
                        continue;
                    }
                }

                int eventTimePointSerialNumber = 0;//�����¼���ʱ�����һ���е����
                string eventTimePoint = timePoint;
                for (int j = 0; j < m_DataTableDayTimePoint.Rows.Count; j++)
                {
                    if (m_DataTableDayTimePoint.Rows[j][0].ToString() == eventTimePoint)
                    {
                        eventTimePointSerialNumber = j;
                        break;
                    }
                }
                #endregion

                #region ͨ�����µ����ֵ,��ÿ��С��������ֵ���������¼��������Ƶ�λ��
                int paintLocation = 0; //��ʾС������ϵ��µĻ����¼���Ϣ����ʼλ�ã���С������
                for (int j = 0; j < m_ArrayListVitalSigns.Count; j++)
                {
                    VitalSigns vs = m_ArrayListVitalSigns[j] as VitalSigns;
                    if (vs != null)
                    {
                        if (vs.Name.ToLower() == "TiWen".ToLower())
                        {
                            float maxValue = vs.MaxValue;
                            float cellValue = vs.CellValue;

                            //paintLocation = Convert.ToInt32((maxValue - m_PaintEventInformationPosition) / cellValue);
                            paintLocation = Convert.ToInt32((maxValue - eventLocation) / cellValue);
                        }
                    }
                }
                #endregion

                #region ���㿪ʼ���Ƶ���ʼ���꣬�жϲ���¼����Ҫ�Ͽ��ڵ������

                //��ʼ���Ƶ���ʼ����
                int xPointEvent = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * daySpan + (m_LineHeight2 + 1) * eventTimePointSerialNumber;
                int yPointEvent = currentLinePointY + (paintLocation / m_CellCountInEveryDegree) * ((m_LineHeight2 + 1) * m_CellCountInEveryDegree + 1)
                    + (paintLocation % m_CellCountInEveryDegree) * (m_LineHeight2 + 1)
                    + 2/*��������ΪЧ����������+2��ʹ�������ƶ�һ��*/
                    + pointYOffset;

                if (m_�Ͽ�״̬.Contains(eventInformationTemp))
                {
                    m_Need�Ͽ�LocationX.Add(xPointEvent/* + m_LineHeight2 / 2*/);
                }

                #endregion

                #region ����һ����һ���ֻ���
                Font fontEvent = new Font(this.Font.FontFamily, 8);
                int yPointIncrease = 0;//ÿ��Y�������ӵľ���

                for (int j = 1; j <= eventInformation.Length; j++)
                {
                    if (eventInformation[j - 1].ToString() == "-")
                    {
                        //�����¼����¼�ʱ��֮�������  xll 2012-10-30���� ���ɺ���  �人ʮ��ҽԺ
                        g.DrawLine(Pens.Black, xPointEvent + m_LineHeight2 / 2 + 1, yPointEvent + yPointIncrease + 1,
                            xPointEvent + m_LineHeight2 / 2 + 1, yPointEvent + yPointIncrease + m_LineHeight2 - 1);
                    }
                    else
                    {

                        //g.DrawString(eventInformation[j - 1].ToString(), fontEvent, Brushes.Red, xPointEvent, yPointEvent + yPointIncrease);

                        //��ٺ������״̬�ú�ɫ������дedit by ywk 2012��7��27�� 16:02:10
                        if (eventInformation.Contains("���") || eventInformation.Contains("���"))
                        {
                            g.DrawString(eventInformation[j - 1].ToString(), fontEvent, Brushes.Black, xPointEvent, yPointEvent + yPointIncrease);
                        }
                        else   //xll 2012-10-30���� ���ɺ���  �人ʮ��ҽԺ
                        {
                            g.DrawString(eventInformation[j - 1].ToString(), fontEvent, Brushes.Black, xPointEvent, yPointEvent + yPointIncrease);
                        }

                    }

                    //����ÿһ��֮�����Դ��߷ָ��������Ҫ�ж���������Ҫ���1
                    if ((paintLocation + j) % m_CellCountInEveryDegree == 0)
                    {
                        yPointIncrease += m_LineHeight2 + 2;
                    }
                    else
                    {
                        yPointIncrease += m_LineHeight2 + 1;
                    }
                }
                pointYOffset += yPointIncrease + m_LineHeight2 + ((paintLocation + eventInformation.Length + 1) % m_CellCountInEveryDegree == 0 ? 2 : 1);
                #endregion
            }
            #endregion

            PaintCurve(g, currentLinePointY);
        }

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="g"></param>
        /// <param name="currentLinePointY"></param>
        private void PaintCurve(Graphics g, int currentLinePointY/*���±���С�������������Ͻǵ�Y�᷽���ϵ�����*/)
        {
            SmoothingMode smoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (m_IsComputeLocation == false)//m_IsComputeLocation����false��ʾҪ���¼�������ͼ�и����������
            {
                //������ڼ�¼����ߵ�����
                m_ArrayListPointLine.Clear();
                m_ArrayListPoint.Clear();

                //���ñ�־λ
                m_IsComputeLocation = true;

                for (int i = 0; i < m_ArrayListVitalSigns.Count; i++)
                {
                    VitalSigns vitalSigns = m_ArrayListVitalSigns[i] as VitalSigns;
                    if (vitalSigns != null)
                    {
                        if (vitalSigns.Name == VitalSignsType.TiWen.ToString())
                        {
                            //����Ҫ���Ƶ�����
                            ComputeTiWen(currentLinePointY, g);

                            //����Ҫ���Ƶ�������
                            ComputeWuLiJiangWen(currentLinePointY, g);

                            ////����Ҫ���Ƶ���������
                            ComputeWuLiShengWen(currentLinePointY, g);
                        }
                        else if (vitalSigns.Name == VitalSignsType.MaiBo.ToString())
                        {
                            //����Ҫ���Ƶ�����
                            ComputeMaiBo(currentLinePointY, g);

                            //����Ҫ���Ƶ�����
                            ComputeXinLv(currentLinePointY, g);

                            //�����ʱ��������ʱ����ʵ�߰����ʺ�����������  Add By wwj 2012-06-25
                            ComputeXinLvMaiBoLine(currentLinePointY, g);
                        }
                        else if (vitalSigns.Name == VitalSignsType.HuXi.ToString())
                        {
                            //����Ҫ���Ƶĺ���
                            ComputeHuXi(currentLinePointY, g);
                        }
                    }
                }

                //����Ҫ���Ƶĳ�ͻ�ĵ�
                ComputeConflictPoint();
            }

            //�Ȼ��Ƶ����֮����߶Σ�Ȼ���ٻ��Ƶ�
            PaintPointLine(g, m_ArrayListPointLine);
            PaintPoint(g, m_ArrayListPoint, m_LineHeight2);

            g.SmoothingMode = smoothingMode;
        }


        #region ����Ҫ���Ƶ�"����","����","����","����","������","��������������" ��������������Ϣ���浽m_ArrayListPoint��,�����֮�����ߵ�������Ϣ���浽m_ArrayListPointLine��

        #region ����Ҫ���Ƶ�����
        /// <summary>
        /// ����Ҫ���Ƶ�����
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="g"></param>
        private void ComputeTiWen(int currentLinePointY, Graphics g)
        {
            PointF pointLast = new PointF(-100, -100);
            string linkNextLast = string.Empty;

            for (int i = 0; i < PatientInfo.DataTableTemperature.Rows.Count; i++)
            {
                m_ID++;

                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTableTemperature.Rows[i]["DateTime"]);
                string timePoint = PatientInfo.DataTableTemperature.Rows[i]["TimePoint"].ToString().Trim();
                string typeID = PatientInfo.DataTableTemperature.Rows[i]["TypeID"].ToString().Trim();//�¶ȵ����� 1: ����  2: Ҹ�� 3: ����
                string value = PatientInfo.DataTableTemperature.Rows[i]["Value"].ToString().Trim();
                string memo = PatientInfo.DataTableTemperature.Rows[i]["Memo"].ToString().Trim();
                string linkNext = PatientInfo.DataTableTemperature.Rows[i]["LinkNext"].ToString().Trim();

                PointF currentPoint = new PointF();

                #region ��û��ֵ��ʱ���������ѭ��
                if (value != "")
                {
                    currentPoint = ComupteLocaton(currentLinePointY, dateTime, timePoint, value, VitalSignsType.TiWen.ToString());
                }
                else
                {
                    continue;
                }
                #endregion


                if (i != 0 /*��ѭ������һ�����ʱ�����ڲ�֪���ڶ��������Ϣ���������ｫ��һ�����Թ�*/ && linkNextLast == "Y")
                {
                    #region ��¼��Ҫ���Ƶĵ����֮����߶ε���Ϣ
                    VitalSignsLine vitalSignsLine = new VitalSignsLine();
                    vitalSignsLine.MainType = VitalSignsType.TiWen.ToString();
                    vitalSignsLine.StartPointF = new PointF(pointLast.X + m_LineHeight2 / 2, pointLast.Y + m_LineHeight2 / 2);
                    vitalSignsLine.StartPointID = m_ID - 1;

                    vitalSignsLine.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                    vitalSignsLine.EndPointID = m_ID;

                    vitalSignsLine.ColorName = "Blue"; //����������������
                    vitalSignsLine.Type = Convert.ToString((int)LineType.SolidLine); //����ʵ��
                    m_ArrayListPointLine.Add(vitalSignsLine);
                    #endregion
                }

                pointLast = currentPoint;
                linkNextLast = linkNext;

                #region ��¼��Ҫ���Ƶĵ�����������
                VitalSignsPosition vitalSignsPosition = new VitalSignsPosition();
                vitalSignsPosition.PointF = currentPoint;
                vitalSignsPosition.ID = m_ID;
                vitalSignsPosition.Type = VitalSignsType.TiWen.ToString();
                vitalSignsPosition.SubType = typeID;

                vitalSignsPosition.Date = dateTime;
                vitalSignsPosition.TimePoint = timePoint;

                m_ArrayListPoint.Add(vitalSignsPosition);
                #endregion
            }
        }
        #endregion

        #region ����Ҫ���Ƶ�����
        /// <summary>
        /// ����Ҫ���Ƶ�����
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="g"></param>
        private void ComputeMaiBo(int currentLinePointY, Graphics g)
        {
            PointF pointLast = new PointF(-100, -100);
            string linkNextLast = string.Empty;
            m_MaiBoList = new List<VitalSignsPosition>();

            for (int i = 0; i < PatientInfo.DataTableMaiBo.Rows.Count; i++)
            {
                m_ID++;

                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTableMaiBo.Rows[i]["DateTime"]);
                string timePoint = PatientInfo.DataTableMaiBo.Rows[i]["TimePoint"].ToString().Trim();
                string value = PatientInfo.DataTableMaiBo.Rows[i]["Value"].ToString().Trim();
                string memo = PatientInfo.DataTableMaiBo.Rows[i]["Memo"].ToString().Trim();
                string linkNext = PatientInfo.DataTableMaiBo.Rows[i]["LinkNext"].ToString().Trim();

                PointF currentPoint = new PointF();

                #region ��û��ֵ��ʱ���������ѭ��
                if (value != "")
                {
                    currentPoint = ComupteLocaton(currentLinePointY, dateTime, timePoint, value, VitalSignsType.MaiBo.ToString());
                }
                else
                {
                    continue;
                }
                #endregion


                if (i != 0 /*��ѭ������һ�����ʱ�����ڲ�֪���ڶ��������Ϣ���������ｫ��һ�����Թ�*/ && linkNextLast == "Y")
                {
                    #region ��¼��Ҫ���Ƶĵ����֮����߶ε���Ϣ
                    VitalSignsLine vitalSignsLine = new VitalSignsLine();
                    vitalSignsLine.MainType = VitalSignsType.MaiBo.ToString();
                    vitalSignsLine.StartPointF = new PointF(pointLast.X + m_LineHeight2 / 2, pointLast.Y + m_LineHeight2 / 2);
                    vitalSignsLine.StartPointID = m_ID - 1;

                    vitalSignsLine.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                    vitalSignsLine.EndPointID = m_ID;

                    vitalSignsLine.ColorName = "Red";//�����ú���������
                    vitalSignsLine.Type = Convert.ToString((int)LineType.SolidLine); //����ʵ��
                    m_ArrayListPointLine.Add(vitalSignsLine);
                    #endregion
                }

                pointLast = currentPoint;
                linkNextLast = linkNext;

                #region ��¼��Ҫ���Ƶĵ�����������
                VitalSignsPosition vitalSignsPosition = new VitalSignsPosition();
                vitalSignsPosition.Value = value;
                vitalSignsPosition.PointF = currentPoint;
                vitalSignsPosition.ID = m_ID;
                vitalSignsPosition.Type = VitalSignsType.MaiBo.ToString();
                vitalSignsPosition.SubType = "";

                vitalSignsPosition.Date = dateTime;
                vitalSignsPosition.TimePoint = timePoint;

                m_ArrayListPoint.Add(vitalSignsPosition);
                m_MaiBoList.Add(vitalSignsPosition);
                #endregion
            }
        }
        #endregion

        #region ����Ҫ���Ƶ�����

        private void ComputeXinLv(int currentLinePointY, Graphics g)
        {
            PointF pointLast = new PointF(-100, -100);
            string linkNextLast = string.Empty;
            m_XinLvList = new List<VitalSignsPosition>();

            for (int i = 0; i < PatientInfo.DataTableXinLv.Rows.Count; i++)
            {
                m_ID++;

                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTableXinLv.Rows[i]["DateTime"]);
                string timePoint = PatientInfo.DataTableXinLv.Rows[i]["TimePoint"].ToString().Trim();
                string value = PatientInfo.DataTableXinLv.Rows[i]["Value"].ToString().Trim();
                string memo = PatientInfo.DataTableXinLv.Rows[i]["Memo"].ToString().Trim();
                string linkNext = PatientInfo.DataTableXinLv.Rows[i]["LinkNext"].ToString().Trim();
                string isSpecial = PatientInfo.DataTableXinLv.Rows[i]["IsSpecial"].ToString().Trim();

                PointF currentPoint = new PointF();

                #region ��û��ֵ��ʱ���������ѭ��
                if (value != "")
                {
                    currentPoint = ComupteLocaton(currentLinePointY, dateTime, timePoint, value, VitalSignsType.MaiBo.ToString());
                }
                else
                {
                    continue;
                }
                #endregion


                if (i == 0)
                {
                    #region �ҵ���һ��������������������

                    bool isBreak = false;
                    //for (int j = 0; j < m_ArrayListPoint.Count; j++)
                    for (int j = m_ArrayListPoint.Count - 1; j >= 0; j--)
                    {
                        VitalSignsPosition vsp = m_ArrayListPoint[j] as VitalSignsPosition;
                        if (vsp != null)
                        {
                            if (vsp.Type == VitalSignsType.MaiBo.ToString())
                            {
                                if (vsp.Date < dateTime || vsp.Date == dateTime && Convert.ToInt32(vsp.TimePoint) <= Convert.ToInt32(timePoint))//ͨ�������ҵ����Ӧ������
                                {
                                    int id = vsp.ID;

                                    #region �ҵ�������������ǰһ������

                                    //�����ж�Ӧ������
                                    if (vsp.Date == dateTime && vsp.TimePoint == timePoint)
                                    {
                                        #region �����ж�Ӧ������
                                        for (int m = 0; m < m_ArrayListPointLine.Count; m++)
                                        {
                                            VitalSignsLine vitalSignsLine = m_ArrayListPointLine[m] as VitalSignsLine;
                                            if (vitalSignsLine.EndPointID == id)
                                            {
                                                VitalSignsLine vsl = new VitalSignsLine();
                                                vsl.MainType = VitalSignsType.XinLv.ToString();
                                                vsl.StartPointF = vitalSignsLine.StartPointF;
                                                vsl.StartPointID = vitalSignsLine.StartPointID;

                                                vsl.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                                                vsl.EndPointID = m_ID;

                                                vsl.ColorName = "Red";//�����������ú���������
                                                vsl.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll���������߸�Ϊʵ��
                                                m_ArrayListPointLine.Add(vsl);

                                                isBreak = true;
                                                break;
                                            }
                                        }
                                        //add by ywk 
                                        //����������û�ж�Ӧ����������ֻҪֻ��һ�����ʾ�Ҫ����ʱ���������Ǹ���������
                                        if (PatientInfo.DataTableXinLv.Rows.Count == 1)
                                        {
                                            DateTime xinlv_date = Convert.ToDateTime(PatientInfo.DataTableXinLv.Rows[0]["DateTime"]);
                                            VitalSignsLine vsl1 = new VitalSignsLine();
                                            vsl1.MainType = VitalSignsType.XinLv.ToString();

                                            vsl1.StartPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                                            vsl1.StartPointID = vsp.ID;

                                            string spiteTime = xinlv_date.ToString("yyyy-MM-dd");
                                            DataRow[] dr = PatientInfo.DataTableMaiBo.Select(" DateTime='" + spiteTime + "' and TimePoint <>'" + vsp.TimePoint + "' ", " TimePoint ASC");
                                            //DataTable newDt = PatientInfo.DataTableMaiBo.Clone();
                                            DataTable newDt = new DataTable();
                                            newDt.Columns.Add("YY", typeof(Int32));
                                            newDt.Columns.Add("TimePoint");
                                            newDt.Columns.Add("DateTime");
                                            newDt.Columns.Add("Value");
                                            newDt.Columns.Add("Memo");
                                            newDt.Columns.Add("LinkNext");
                                            for (int u = 0; u < dr.Length; u++)
                                            {
                                                DataRow row = newDt.NewRow();
                                                row["YY"] = dr[u]["TimePoint"].ToString();
                                                row["DateTime"] = dr[u]["DateTime"].ToString();
                                                row["Value"] = dr[u]["Value"].ToString();
                                                row["TimePoint"] = dr[u]["TimePoint"].ToString();
                                                row["Memo"] = dr[u]["Memo"].ToString();
                                                row["LinkNext"] = dr[u]["LinkNext"].ToString();
                                                //Ҫȡ�ȵ�ǰʱ�����λ��
                                                if (Int32.Parse(dr[u]["TimePoint"].ToString()) > Int32.Parse(timePoint))
                                                {
                                                    newDt.Rows.Add(row);
                                                    DataColumn[] cols = new DataColumn[] { newDt.Columns["YY"] };
                                                    newDt.PrimaryKey = cols;
                                                    newDt.DefaultView.Sort = "YY ASC";
                                                    DataRow[] dr1 = newDt.Select(" 1=1 ", " YY ASC");

                                                    PointF currentPoint1 = ComupteLocaton(currentLinePointY, xinlv_date, dr1[0]["TimePoint"].ToString(), dr1[0]["Value"].ToString
                                                        (), VitalSignsType.MaiBo.ToString());
                                                    //VitalSignsLine vimtalSignsLine = m_ArrayListPointLine[1] as VitalSignsLine;
                                                    vsl1.EndPointF = new PointF(currentPoint1.X + 6, currentPoint1.Y + 6);
                                                    vsl1.EndPointID = 2;
                                                    vsl1.ColorName = "Red";//�����������ú���������
                                                    vsl1.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll��ʵ��
                                                }
                                                //������������һ���㣬����û������
                                                if (Int32.Parse(dr[u]["TimePoint"].ToString()) < Int32.Parse(timePoint))
                                                {
                                                }
                                            }

                                            m_ArrayListPointLine.Add(vsl1);
                                        }
                                        #endregion
                                    }
                                    else//����û�ж�Ӧ������
                                    {
                                        #region ����û�ж�Ӧ������
                                        VitalSignsLine vsl = new VitalSignsLine();
                                        vsl.MainType = VitalSignsType.XinLv.ToString();
                                        vsl.StartPointF = new PointF(vsp.PointF.X + m_LineHeight2 / 2, vsp.PointF.Y + m_LineHeight2 / 2);
                                        vsl.StartPointID = vsp.ID;

                                        vsl.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                                        vsl.EndPointID = m_ID;

                                        vsl.ColorName = "Red";//�����������ú���������
                                        vsl.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll��ʵ��
                                        m_ArrayListPointLine.Add(vsl);

                                        //���ֻ��һ������,Ҫ�����ʱ�������������� 
                                        //add by ywk 
                                        if (PatientInfo.DataTableXinLv.Rows.Count == 1)
                                        {
                                            DateTime xinlv_date = Convert.ToDateTime(PatientInfo.DataTableXinLv.Rows[0]["DateTime"]);
                                            VitalSignsLine vsl1 = new VitalSignsLine();
                                            vsl1.MainType = VitalSignsType.XinLv.ToString();

                                            vsl1.StartPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                                            vsl1.StartPointID = vsp.ID;

                                            string spiteTime = xinlv_date.ToString("yyyy-MM-dd");

                                            //�˴�����ʱ���ɸѡ����DataTable���������ݵı仯���򣬣���Ϊû���������������������˳������
                                            DataRow[] dr = PatientInfo.DataTableMaiBo.Select(" DateTime='" + spiteTime + "'", "TimePoint DESC");
                                            //���Դ˴�������һ��table������������Ϊ����!
                                            DataTable newDt = new DataTable();
                                            newDt.Columns.Add("YY", typeof(Int32));
                                            newDt.Columns.Add("TimePoint");
                                            newDt.Columns.Add("DateTime");
                                            newDt.Columns.Add("Value");
                                            newDt.Columns.Add("Memo");
                                            newDt.Columns.Add("LinkNext");
                                            for (int u = 0; u < dr.Length; u++)
                                            {
                                                DataRow row = newDt.NewRow();
                                                row["YY"] = dr[u]["TimePoint"].ToString();
                                                row["DateTime"] = dr[u]["DateTime"].ToString();
                                                row["Value"] = dr[u]["Value"].ToString();
                                                row["TimePoint"] = dr[u]["TimePoint"].ToString();
                                                row["Memo"] = dr[u]["Memo"].ToString();
                                                row["LinkNext"] = dr[u]["LinkNext"].ToString();
                                                //Ҫȡ�ȵ�ǰʱ�����λ��
                                                if (Int32.Parse(dr[u]["TimePoint"].ToString()) > Int32.Parse(timePoint))
                                                {
                                                    newDt.Rows.Add(row);
                                                    DataColumn[] cols = new DataColumn[] { newDt.Columns["YY"] };
                                                    newDt.PrimaryKey = cols;
                                                    newDt.DefaultView.Sort = "YY ASC";
                                                    DataRow[] dr1 = newDt.Select(" 1=1 ", " YY ASC");
                                                    PointF currentPoint1 = ComupteLocaton(currentLinePointY, xinlv_date, dr1[0]["TimePoint"].ToString(), dr1[0]["Value"].ToString
                                                        (), VitalSignsType.MaiBo.ToString());
                                                    //VitalSignsLine vimtalSignsLine = m_ArrayListPointLine[1] as VitalSignsLine;
                                                    vsl1.EndPointF = new PointF(currentPoint1.X + 6, currentPoint1.Y + 6);
                                                    vsl1.EndPointID = 2;
                                                    vsl1.ColorName = "Red";//�����������ú���������
                                                    vsl1.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll��ʵ��
                                                }
                                                //���ʺ���û����������������ԭ��һ��
                                                if (Int32.Parse(dr[u]["TimePoint"].ToString()) < Int32.Parse(timePoint))
                                                {

                                                }
                                            }
                                            m_ArrayListPointLine.Add(vsl1);
                                        }
                                        isBreak = true;
                                        break;
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                        }

                        if (isBreak == true)
                        {
                            break;
                        }
                    }

                    #endregion
                }
                else if (linkNextLast == "Y")
                {
                    #region ��¼��Ҫ���Ƶĵ����֮����߶ε���Ϣ
                    VitalSignsLine vitalSignsLine = new VitalSignsLine();
                    vitalSignsLine.MainType = VitalSignsType.XinLv.ToString();
                    vitalSignsLine.StartPointF = new PointF(pointLast.X + m_LineHeight2 / 2, pointLast.Y + m_LineHeight2 / 2);
                    vitalSignsLine.StartPointID = m_ID - 1;

                    vitalSignsLine.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                    vitalSignsLine.EndPointID = m_ID;

                    vitalSignsLine.ColorName = "Red";//�����ú���������
                    vitalSignsLine.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll��ʵ��
                    m_ArrayListPointLine.Add(vitalSignsLine);
                    #endregion

                    #region �ҵ����һ��������������������

                    if (i == PatientInfo.DataTableXinLv.Rows.Count - 1)
                    {
                        bool isBreak = false;
                        for (int j = 0; j < m_ArrayListPoint.Count; j++)
                        {
                            VitalSignsPosition vsp = m_ArrayListPoint[j] as VitalSignsPosition;
                            if (vsp != null)
                            {
                                if (vsp.Type == VitalSignsType.MaiBo.ToString())
                                {
                                    if (vsp.Date > dateTime || vsp.Date == dateTime && Convert.ToInt32(vsp.TimePoint) >= Convert.ToInt32(timePoint))//ͨ�������ҵ����Ӧ������
                                    {
                                        int id = vsp.ID;

                                        #region �ҵ������������ĺ�һ������

                                        //�����ж�Ӧ������
                                        if (vsp.Date == dateTime && vsp.TimePoint == timePoint)
                                        {
                                            #region �����ж�Ӧ������
                                            for (int m = 0; m < m_ArrayListPointLine.Count; m++)
                                            {
                                                VitalSignsLine line = m_ArrayListPointLine[m] as VitalSignsLine;
                                                if (line.StartPointID == id)
                                                {
                                                    VitalSignsLine vsl = new VitalSignsLine();
                                                    vsl.MainType = VitalSignsType.XinLv.ToString();
                                                    vsl.StartPointF = line.EndPointF;
                                                    vsl.StartPointID = line.EndPointID;

                                                    vsl.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                                                    vsl.EndPointID = m_ID;

                                                    vsl.ColorName = "Red";//�����������ú���������
                                                    vsl.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll��ʵ��
                                                    m_ArrayListPointLine.Add(vsl);

                                                    isBreak = true;
                                                    break;
                                                }
                                            }
                                            #endregion
                                        }
                                        else//����û�ж�Ӧ������
                                        {
                                            #region ����û�ж�Ӧ������

                                            VitalSignsLine vsl = new VitalSignsLine();
                                            vsl.MainType = VitalSignsType.XinLv.ToString();
                                            vsl.StartPointF = new PointF(vsp.PointF.X + m_LineHeight2 / 2, vsp.PointF.Y + m_LineHeight2 / 2);
                                            vsl.StartPointID = vsp.ID;

                                            vsl.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                                            vsl.EndPointID = m_ID;

                                            vsl.ColorName = "Red";//�����������ú���������
                                            vsl.Type = Convert.ToString((int)LineType.SolidLine); //�������� xll��ʵ��
                                            m_ArrayListPointLine.Add(vsl);

                                            isBreak = true;
                                            break;

                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                            }

                            if (isBreak == true)
                            {
                                break;
                            }
                        }
                    }
                    #endregion
                }

                pointLast = currentPoint;
                linkNextLast = linkNext;

                #region ��¼��Ҫ���Ƶĵ�����������
                VitalSignsPosition vitalSignsPosition = new VitalSignsPosition();
                vitalSignsPosition.Value = value;
                vitalSignsPosition.PointF = currentPoint;
                vitalSignsPosition.ID = m_ID;
                vitalSignsPosition.Type = VitalSignsType.XinLv.ToString();
                vitalSignsPosition.SubType = "";
                vitalSignsPosition.IsSpecial = isSpecial;

                vitalSignsPosition.Date = dateTime;
                vitalSignsPosition.TimePoint = timePoint;

                m_ArrayListPoint.Add(vitalSignsPosition);
                m_XinLvList.Add(vitalSignsPosition);
                #endregion
            }
        }

        #endregion

        #region �����ʱ��������ʱ����ʵ�߰����ʺ����������� Add By wwj 2012-06-25

        private void ComputeXinLvMaiBoLine(int currentLinePointY, Graphics g)
        {
            foreach (VitalSignsPosition xinlvPosition in m_XinLvList)//����
            {
                foreach (VitalSignsPosition maiboPosition in m_MaiBoList)//����
                {
                    if (xinlvPosition.Date == maiboPosition.Date && xinlvPosition.TimePoint == maiboPosition.TimePoint)
                    {
                        //���ʱ�������
                        if (Convert.ToDouble(xinlvPosition.Value) > Convert.ToDouble(maiboPosition.Value))
                        {
                            VitalSignsLine vsl = new VitalSignsLine();
                            vsl.MainType = VitalSignsType.XinLv.ToString();
                            vsl.StartPointF = new PointF(xinlvPosition.PointF.X + m_LineHeight2 / 2, xinlvPosition.PointF.Y + m_LineHeight2 / 2); ;
                            vsl.StartPointID = xinlvPosition.ID;

                            vsl.EndPointF = new PointF(maiboPosition.PointF.X + m_LineHeight2 / 2, maiboPosition.PointF.Y + m_LineHeight2 / 2); ; ;
                            vsl.EndPointID = maiboPosition.ID;

                            vsl.ColorName = "Red";//�����������ú���������
                            vsl.Type = Convert.ToString((int)LineType.SolidLine); //��������
                            m_ArrayListPointLine.Add(vsl);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��¼��Ҫ���Ƶ����ʵ�λ��
        /// </summary>
        private List<VitalSignsPosition> m_XinLvList;

        /// <summary>
        /// ��¼��Ҫ���Ƶ�������λ��
        /// </summary>
        private List<VitalSignsPosition> m_MaiBoList;

        #endregion

        #region ����Ҫ���Ƶĺ���
        /// <summary>
        /// ����Ҫ���Ƶ�����
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="g"></param>
        private void ComputeHuXi(int currentLinePointY, Graphics g)
        {
            PointF pointLast = new PointF(-100, -100);
            string linkNextLast = string.Empty;

            for (int i = 0; i < PatientInfo.DataTableHuXi.Rows.Count; i++)
            {
                m_ID++;

                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[i]["DateTime"]);
                string timePoint = PatientInfo.DataTableHuXi.Rows[i]["TimePoint"].ToString().Trim();
                string value = PatientInfo.DataTableHuXi.Rows[i]["Value"].ToString().Trim();
                string memo = PatientInfo.DataTableHuXi.Rows[i]["Memo"].ToString().Trim();
                string linkNext = PatientInfo.DataTableHuXi.Rows[i]["LinkNext"].ToString().Trim();
                string IsSpecial = PatientInfo.DataTableHuXi.Rows[i]["IsSpecial"].ToString().Trim();

                PointF currentPoint = new PointF();

                #region ��û��ֵ��ʱ���������ѭ��
                if (value != "")
                {
                    currentPoint = ComupteLocaton(currentLinePointY, dateTime, timePoint, value, VitalSignsType.HuXi.ToString());
                }
                else
                {
                    continue;
                }
                #endregion


                if (i != 0 /*��ѭ������һ�����ʱ�����ڲ�֪���ڶ��������Ϣ���������ｫ��һ�����Թ�*/ && linkNextLast == "Y")
                {
                    #region ��¼��Ҫ���Ƶĵ����֮����߶ε���Ϣ
                    VitalSignsLine vitalSignsLine = new VitalSignsLine();

                    vitalSignsLine.MainType = VitalSignsType.HuXi.ToString();
                    vitalSignsLine.StartPointF = new PointF(pointLast.X + m_LineHeight2 / 2, pointLast.Y + m_LineHeight2 / 2);
                    vitalSignsLine.StartPointID = m_ID - 1;

                    vitalSignsLine.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                    vitalSignsLine.EndPointID = m_ID;

                    vitalSignsLine.ColorName = "Blue";//����������������
                    vitalSignsLine.Type = Convert.ToString((int)LineType.SolidLine); //����ʵ��
                    m_ArrayListPointLine.Add(vitalSignsLine);
                    #endregion
                }

                pointLast = currentPoint;
                linkNextLast = linkNext;

                #region ��¼��Ҫ���Ƶĵ�����������
                VitalSignsPosition vitalSignsPosition = new VitalSignsPosition();
                vitalSignsPosition.PointF = currentPoint;
                vitalSignsPosition.ID = m_ID;
                vitalSignsPosition.Type = VitalSignsType.HuXi.ToString();
                vitalSignsPosition.SubType = "";

                vitalSignsPosition.Date = dateTime;
                vitalSignsPosition.TimePoint = timePoint;

                vitalSignsPosition.IsSpecial = IsSpecial;
                m_ArrayListPoint.Add(vitalSignsPosition);
                #endregion
            }
        }
        #endregion



        #region ����Ҫ���Ƶ���������
        /// <summary>
        /// ����Ҫ���Ƶ��������� addd by ywk 
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="g"></param>
        private void ComputeWuLiShengWen(int currentLinePointY, Graphics g)
        {
            for (int i = 0; i < PatientInfo.DataTableWuLiShengWen.Rows.Count; i++)
            {
                m_ID++;

                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTableWuLiShengWen.Rows[i]["DateTime"]);
                string timePoint = PatientInfo.DataTableWuLiShengWen.Rows[i]["TimePoint"].ToString().Trim();
                string value = PatientInfo.DataTableWuLiShengWen.Rows[i]["Value"].ToString().Trim();
                string linkNext = PatientInfo.DataTableWuLiShengWen.Rows[i]["LinkNext"].ToString().Trim();

                PointF currentPoint = new PointF();

                #region ��û��ֵ��ʱ���������ѭ��
                if (value != "")
                {
                    currentPoint = ComupteLocaton(currentLinePointY, dateTime, timePoint, value, VitalSignsType.TiWen.ToString());
                }
                else
                {
                    continue;
                }
                #endregion

                #region ��¼��Ҫ���Ƶĵ����֮����߶ε���Ϣ�����������ҵ�������ǰ���¶�
                VitalSignsLine vitalSignsLine = new VitalSignsLine();

                for (int j = 0; j < m_ArrayListPoint.Count; j++)
                {
                    VitalSignsPosition vsp = m_ArrayListPoint[j] as VitalSignsPosition;
                    if (vsp.Type == VitalSignsType.TiWen.ToString())
                    {
                        if (vsp.Date == dateTime && vsp.TimePoint == timePoint)
                        {
                            vitalSignsLine.MainType = VitalSignsType.WuLiShengWen.ToString();
                            vitalSignsLine.StartPointF = new PointF(vsp.PointF.X + m_LineHeight2 / 2, vsp.PointF.Y + m_LineHeight2 / 2); //��������ǰ���µ�����
                            vitalSignsLine.StartPointID = vsp.ID;

                            vitalSignsLine.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                            vitalSignsLine.EndPointID = m_ID;

                            vitalSignsLine.ColorName = "Blue";//��������������������
                            vitalSignsLine.Type = Convert.ToString((int)LineType.DashLine); //��������
                            m_ArrayListPointLine.Add(vitalSignsLine);

                            break;
                        }
                    }
                }
                #endregion

                #region ��¼��Ҫ���Ƶĵ�����������

                VitalSignsPosition vitalSignsPosition = new VitalSignsPosition();
                vitalSignsPosition.PointF = currentPoint;
                vitalSignsPosition.ID = m_ID;
                vitalSignsPosition.Type = VitalSignsType.WuLiShengWen.ToString();
                vitalSignsPosition.SubType = "";

                vitalSignsPosition.Date = dateTime;
                vitalSignsPosition.TimePoint = timePoint;

                m_ArrayListPoint.Add(vitalSignsPosition);

                #endregion
            }
        }
        #endregion

        #region ����Ҫ���Ƶ�������
        /// <summary>
        /// ����Ҫ���Ƶ�������
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="g"></param>
        private void ComputeWuLiJiangWen(int currentLinePointY, Graphics g)
        {
            for (int i = 0; i < PatientInfo.DataTableWuLiJiangWen.Rows.Count; i++)
            {
                m_ID++;

                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTableWuLiJiangWen.Rows[i]["DateTime"]);
                string timePoint = PatientInfo.DataTableWuLiJiangWen.Rows[i]["TimePoint"].ToString().Trim();
                string value = PatientInfo.DataTableWuLiJiangWen.Rows[i]["Value"].ToString().Trim();
                string linkNext = PatientInfo.DataTableWuLiJiangWen.Rows[i]["LinkNext"].ToString().Trim();

                PointF currentPoint = new PointF();

                #region ��û��ֵ��ʱ���������ѭ��
                if (value != "")
                {
                    currentPoint = ComupteLocaton(currentLinePointY, dateTime, timePoint, value, VitalSignsType.TiWen.ToString());
                }
                else
                {
                    continue;
                }
                #endregion

                #region ��¼��Ҫ���Ƶĵ����֮����߶ε���Ϣ�����������ҵ�������ǰ���¶�
                VitalSignsLine vitalSignsLine = new VitalSignsLine();

                for (int j = 0; j < m_ArrayListPoint.Count; j++)
                {
                    VitalSignsPosition vsp = m_ArrayListPoint[j] as VitalSignsPosition;
                    if (vsp.Type == VitalSignsType.TiWen.ToString())
                    {
                        if (vsp.Date == dateTime && vsp.TimePoint == timePoint)
                        {
                            vitalSignsLine.MainType = VitalSignsType.WuLiJiangWen.ToString();
                            vitalSignsLine.StartPointF = new PointF(vsp.PointF.X + m_LineHeight2 / 2, vsp.PointF.Y + m_LineHeight2 / 2); //������ǰ���µ�����
                            vitalSignsLine.StartPointID = vsp.ID;

                            vitalSignsLine.EndPointF = new PointF(currentPoint.X + m_LineHeight2 / 2, currentPoint.Y + m_LineHeight2 / 2);
                            vitalSignsLine.EndPointID = m_ID;

                            vitalSignsLine.ColorName = "Red";//�������ú���������
                            vitalSignsLine.Type = Convert.ToString((int)LineType.DashLine); //��������
                            m_ArrayListPointLine.Add(vitalSignsLine);

                            break;
                        }
                    }
                }
                #endregion

                #region ��¼��Ҫ���Ƶĵ�����������

                VitalSignsPosition vitalSignsPosition = new VitalSignsPosition();
                vitalSignsPosition.PointF = currentPoint;
                vitalSignsPosition.ID = m_ID;
                vitalSignsPosition.Type = VitalSignsType.WuLiJiangWen.ToString();
                vitalSignsPosition.SubType = "";

                vitalSignsPosition.Date = dateTime;
                vitalSignsPosition.TimePoint = timePoint;

                m_ArrayListPoint.Add(vitalSignsPosition);

                #endregion
            }
        }
        #endregion

        #region ��������ͼ��ֵ������
        /// <summary>
        /// ��������ͼ��ֵ������
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="currentDateTime"></param>
        /// <param name="testTimePoint"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private PointF ComupteLocaton(int currentLinePointY/*���±���С�������������Ͻǵ�Y�᷽���ϵ�����*/, DateTime currentDateTime, string testTimePoint, string value, string type)
        {
            #region ����� ��Ҫ���Ƶ��¼� �� ���� �� ʱ���
            int daySpan = 0;//�¶Ȳ���ʱ��������µ��ϵ�һ�������ڵļ������
            if (m_DateTimeEveryColumnDateTime.Rows.Count > 0)
            {
                DateTime firstDateTime = Convert.ToDateTime(m_DateTimeEveryColumnDateTime.Rows[0][0]);
                daySpan = (currentDateTime - firstDateTime).Days;

                if (daySpan < 0 || daySpan >= m_Days) //�Ѿ��������������µ������ڷ�Χ������Ҫ�ų�
                {
                    return new PointF(-100, -100);
                }
            }

            int eventTimePointSerialNumber = 0;//�¶Ȳ�����ʱ�����һ���е����
            for (int j = 0; j < m_DataTableDayTimePoint.Rows.Count; j++)
            {
                if (m_DataTableDayTimePoint.Rows[j][0].ToString() == testTimePoint)
                {
                    eventTimePointSerialNumber = j;
                    break;
                }
            }
            #endregion

            float xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * daySpan + (m_LineHeight2 + 1) * eventTimePointSerialNumber;
            float yPoint = 0;
            float maxValue = GetMaxValue(type);//�õ����ֵ
            float cellValue = GetCellValue(type); //�õ�ÿ��С��������ֵ

            if (value.Trim() != "")
            {
                float currentValue = (float)Convert.ToDouble(value);

                int cellCount = Convert.ToInt32(Math.Floor((maxValue - currentValue) / cellValue)); //������¶�ֵ����ǰ�¶�ֵ�ж��ٸ�С����
                float excess = (maxValue - currentValue) % cellValue;

                yPoint = currentLinePointY
                    + (cellCount / m_CellCountInEveryDegree) * ((m_LineHeight2 + 1) * m_CellCountInEveryDegree + 1) //ÿ��������֮��
                    + (cellCount % m_CellCountInEveryDegree) * (m_LineHeight2 + 1) //С����֮��
                    + (excess / cellValue) * m_LineHeight2; //С������
            }

            //return new PointF(xPoint + 1/*����Ч�����ã������������2����΢��*/, yPoint - m_LineHeight2 / 2 + 1/*����Ч�����ã������������2���е���*/);
            return new PointF(xPoint + 5, yPoint - m_LineHeight2 / 2 + 5);//edit by ywk
        }
        #endregion

        #region ����Ҫ���Ƶ��غϵĲ���
        private void ComputeConflictPoint()
        {
            VitalSignsPosition vsp = new VitalSignsPosition();
            m_ArrayListConflictPoint = vsp.GetmConflictPoint(m_ArrayListPoint, m_ArrayListPointLine, m_LineHeight2, m_Distance);
        }

        #endregion

        #endregion

        #region ���������еĸ�����,�Լ�������֮�������

        /// <summary>
        /// ���������еĸ�����
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="arrayListPoint">�����������Ϣ������</param>
        /// <param name="cellHeight">С����ĸߣ�����</param>
        private void PaintPoint(Graphics g, ArrayList arrayListPoint, int cellHeight)
        {
            //����ָ��ͼƬ�ĵ�
            #region ����û���غϵĵ�
            for (int i = 0; i < arrayListPoint.Count; i++)
            {
                VitalSignsPosition vitalSignsPosition = arrayListPoint[i] as VitalSignsPosition;
                if (vitalSignsPosition != null)
                {
                    RectangleF rect = new RectangleF(new PointF(vitalSignsPosition.PointF.X + 2, vitalSignsPosition.PointF.Y + 2), new SizeF(cellHeight - 4, cellHeight - 4));
                    RectangleF rectSmall = new RectangleF(new PointF(vitalSignsPosition.PointF.X + 1 + 2, vitalSignsPosition.PointF.Y + 1 + 2),
                        new SizeF(cellHeight - 2 - 4, cellHeight - 2 - 4));

                    if (vitalSignsPosition.Type == VitalSignsType.TiWen.ToString())//����
                    {
                        if (m_IsShowTiWen == true)
                        {
                            string typeID = vitalSignsPosition.SubType;
                            if (typeID == Convert.ToString((int)VitalSignsTiWenType.KouWen)) //����
                            {
                                //g.DrawImage(m_Picture.BitmapT1, rect);
                                m_Picture.DrawKouWen(g, rect.X, rect.Y);
                            }
                            else if (typeID == Convert.ToString((int)VitalSignsTiWenType.YeWen)) //Ҹ��
                            {
                                //g.DrawImage(m_Picture.BitmapT2, rectSmall);
                                m_Picture.DrawYeXia(g, rect.X, rect.Y);
                            }
                            else if (typeID == Convert.ToString((int)VitalSignsTiWenType.GangWen)) //����
                            {
                                //g.DrawImage(m_Picture.BitmapT3, rect);
                                m_Picture.DrawGangWen(g, rect.X, rect.Y);
                            }
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsType.MaiBo.ToString()) //����
                    {
                        if (m_IsShowMaiBo == true)
                        {
                            //g.DrawImage(m_Picture.BitmapMaiBo, rect);
                            m_Picture.DrawMaiBo(g, rect.X, rect.Y);
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsType.XinLv.ToString()) //����
                    {
                        if (m_IsShowMaiBo == true)
                        {
                            if (vitalSignsPosition.IsSpecial == "Y") //��ʾʹ��������
                            {
                                //g.DrawImage(m_Picture.BitmapQiBoQi, rect);
                                m_Picture.DrawQiBoQi(g, rect.X, rect.Y);
                            }
                            else
                            {
                                //g.DrawImage(m_Picture.BitmapXinlv, rect);
                                m_Picture.DrawXinLv(g, rect.X, rect.Y);
                            }
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsType.HuXi.ToString()) //����
                    {
                        if (m_IsShowHuXi == true)
                        {
                            if (vitalSignsPosition.IsSpecial == "Y") //��ʾʹ���˺�����
                            {
                                //g.DrawImage(m_Picture.BitmapHuXiSpecial, rect);
                                m_Picture.DrawHuXiJi(g, rect.X, rect.Y);
                            }
                            else
                            {
                                //g.DrawImage(m_Picture.BitmapHuXi, rect);
                                m_Picture.DrawHuXi(g, rect.X, rect.Y);
                            }
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsType.WuLiJiangWen.ToString()) //������
                    {
                        if (m_IsShowTiWen == true)
                        {
                            //g.DrawImage(m_Picture.BitmapWuLiJiangWen, rect);
                            m_Picture.DrawWuLiJiangWen(g, rect.X, rect.Y);
                        }
                    }

                        //������������
                    else if (vitalSignsPosition.Type == VitalSignsType.WuLiShengWen.ToString()) //��������
                    {
                        if (m_IsShowTiWen == true)
                        {
                            //g.DrawImage(m_Picture.BitmapWuLiJiangWen, rect);
                            m_Picture.DrawWuLiShengWen(g, rect.X, rect.Y);
                        }
                    }
                }
            }
            #endregion

            #region �������غϲ��ֵĵ�
            for (int i = 0; i < m_ArrayListConflictPoint.Count; i++)
            {
                VitalSignsPosition vitalSignsPosition = m_ArrayListConflictPoint[i] as VitalSignsPosition;
                if (vitalSignsPosition != null)
                {
                    //�ж�������Ƿ���Ҫ�����
                    if (vitalSignsPosition.IsDraw == false)
                    {
                        continue;
                    }

                    RectangleF rect = new RectangleF(new PointF(vitalSignsPosition.PointF.X + 2, vitalSignsPosition.PointF.Y + 2),
                        new SizeF(cellHeight - 4, cellHeight - 4));

                    if (vitalSignsPosition.Type == VitalSignsPosition.ConflictPointType.MaiBoTiWenKou.ToString())//����������(��ǻ)�ص�
                    {
                        if (m_IsShowMaiBo == true && m_IsShowTiWen == true)
                        {
                            //g.DrawImage(m_Picture.BitmapNursMaiBoTiWenKou, rect);
                            m_Picture.DrawMaiBoTiWenKou(g, rect.X, rect.Y);
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsPosition.ConflictPointType.MaiBoTiWenGang.ToString())//����������(����)�ص�
                    {
                        if (m_IsShowMaiBo == true && m_IsShowTiWen == true)
                        {
                            //g.DrawImage(m_Picture.BitmapNursMaiBoTiWenGang, rect);
                            m_Picture.DrawMaiBoTiWenGang(g, rect.X, rect.Y);
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsPosition.ConflictPointType.MaiBoTiWenYe.ToString())//����������(Ҹ��)�ص�
                    {
                        if (m_IsShowMaiBo == true && m_IsShowTiWen == true)
                        {
                            //g.DrawImage(m_Picture.BitmapNursMaiBoTiWenYe, rect);
                            m_Picture.DrawMaiBoTiWenYe(g, rect.X, rect.Y);
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsPosition.ConflictPointType.HuXiMaiBo.ToString())//��������������
                    {
                        if (m_IsShowHuXi == true && m_IsShowMaiBo == true)
                        {
                            //g.DrawImage(m_Picture.BitmapNursHuXiMaiBo, rect);
                            m_Picture.DrawHuXiMaiBo(g, rect.X, rect.Y);
                        }
                    }
                    else if (vitalSignsPosition.Type == VitalSignsPosition.ConflictPointType.TiWenHuXiMaiBo.ToString())//���¡���������������һ������
                    {
                        if (m_IsShowMaiBo == true && m_IsShowHuXi == true && m_IsShowTiWen == true)
                        {
                            //g.DrawImage(m_Picture.BitmapNursTiWenHuXiMaiBo, rect);
                            m_Picture.DrawTiWenHuXiMaiBo(g, rect.X, rect.Y);
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// ���������и�����֮�������
        /// </summary>
        /// <param name="g"></param>
        /// <param name="arrayListPointLine"></param>
        private void PaintPointLine(Graphics g, ArrayList arrayListPointLine)
        {
            PointF startPoint = new PointF();
            PointF endPoint = new PointF();
            string type = string.Empty;

            for (int i = 0; i < arrayListPointLine.Count; i++)
            {
                VitalSignsLine vitalSignsLine = arrayListPointLine[i] as VitalSignsLine;
                if (vitalSignsLine != null)
                {
                    startPoint = vitalSignsLine.StartPointF;
                    endPoint = vitalSignsLine.EndPointF;

                    #region �ж��߶��Ƿ���Ҫ����
                    bool isContinue = false;

                    //if (startPoint.Y != endPoint.Y)
                    //{
                        foreach (int pointX in m_Need�Ͽ�LocationX)
                        {
                            if (startPoint.X <= pointX && pointX + m_LineHeight2 <= endPoint.X)
                            {
                                isContinue = true;
                            }
                        }
                    //}
                    if (isContinue)
                    {
                        continue;
                    }
                    #endregion

                    type = vitalSignsLine.Type;
                    Pen pen = new Pen(GetBrushByColorName(vitalSignsLine.ColorName));

                    //���Ƶ��ڵ�֮���ֱ��
                    //�˴���������??
                    if ((vitalSignsLine.MainType.ToString() == VitalSignsType.TiWen.ToString() || vitalSignsLine.MainType.ToString() == VitalSignsType.WuLiJiangWen.ToString() ||
                        vitalSignsLine.MainType.ToString() == VitalSignsType.WuLiShengWen.ToString())
                        && m_IsShowTiWen == false)
                    {
                        continue;
                    }
                    if ((vitalSignsLine.MainType.ToString() == VitalSignsType.MaiBo.ToString() || vitalSignsLine.MainType.ToString() == VitalSignsType.XinLv.ToString())
                        && m_IsShowMaiBo == false)
                    {
                        continue;
                    }
                    if (vitalSignsLine.MainType.ToString() == VitalSignsType.HuXi.ToString() && m_IsShowHuXi == false)
                    {
                        continue;
                    }

                    if (type == Convert.ToString((int)LineType.SolidLine))
                        //g.DrawLine(pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);edit by ywk 
                        g.DrawLine(pen, startPoint.X - 3, startPoint.Y - 3, endPoint.X - 3, endPoint.Y - 3);
                    else if (type == Convert.ToString((int)LineType.DashLine))
                    {
                        pen.DashPattern = new float[] { 3f, 3f };
                        //g.DrawLine(pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);edit by ywk 
                        g.DrawLine(pen, startPoint.X - 3, startPoint.Y - 3, endPoint.X - 3, endPoint.Y - 3);
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region ���Ʊ��ĵ�������
        /// <summary>
        /// ���Ʊ��ĵ�������
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintThirdPartOfTable(PictureBox pictureBox, Graphics g)
        {
            //��¼С��������ͼ�µ�һ��Y�᷽���ֵ
            int currentLinePointY = m_TableStartPointY + (m_LineHeight1 + 1) * m_DataTableTableBaseLine.Rows.Count
                + (m_CellCount / m_CellCountInEveryDegree) * ((m_LineHeight2 + 1) * m_CellCountInEveryDegree + 1)
                + (m_CellCount % m_CellCountInEveryDegree) * (m_LineHeight2 + 1);

            //��¼С��������ͼ�µ�һ��X�᷽���ֵ
            int currentLinePointX = m_TableStartPointX;

            //�˿�Ȳ�������������1���أ�����������ұ�2����
            int tableWidth = m_FirstColumnWidth/*��һ�еĿ��*/ + 1 + (m_LineHeight2 * m_DayTimePoint + m_DayTimePoint) * m_Days + 1;

            #region ���� ��ˢ ����
            Pen penBlack = Pens.Black;
            Pen penBlackBold = new Pen(Brushes.Black, 2);
            Brush brushBlack = Brushes.Black;
            Brush brushRed = Brushes.Red;
            Font fontNormal = this.Font;
            Font fontSmall = new Font(fontNormal.FontFamily, fontNormal.Size - 1.5f);

            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;

            StringFormat leftFormat = new StringFormat();
            leftFormat.Alignment = StringAlignment.Near;
            leftFormat.LineAlignment = StringAlignment.Center;
            #endregion

            #region ����-------���������������ͼ����ʾ������Ҫ�Զ�����һ����ʾ

            //if (PatientInfo.DataTableHuXi.Rows.Count > 0)
            {

                //�жϺ����Ƿ���ʾ����ͼ��
                bool isHasHuXi = checkIsContainHuXiInCurve();

                //���ں�����������ͼ�У�����Ҫ�������һ���н�������������ʾ����
                if (isHasHuXi == false)
                {

                    #region ���ƺ��� ���� ��ߵ�����

                    //�ڵ�����һ������ʾ����������
                    g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight2 * 2, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight2 * 2);

                    //���ƺ������и���ʱ��ε�����
                    for (int i = 1; i < m_DayTimePoint * m_Days; i++)
                    {
                        if (i % m_DayTimePoint != 0)
                        {
                            int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * i;
                            int startPointY = currentLinePointY;
                            int endPointX = startPointX;
                            int endPointY = startPointY + m_LineHeight2 * 2;

                            g.DrawLine(penBlack, new Point(startPointX, startPointY), new Point(endPointX, endPointY));
                        }
                    }

                    //���ƺ��������Լ���λ
                    g.DrawString("����(��/��)", fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 2, m_FirstColumnWidth, m_LineHeight2 * 2), leftFormat);

                    #endregion

                    #region �ȼ�����Ƶ�λ�ã�Ȼ���ٽ�����Ƴ���
                    //��������ֵ���Ƶ���Ӧ��λ����
                    DateTime currentDateTime = DateTime.MinValue;//���ڼ�¼��ǰѭ����ֵ

                    ArrayList list = new ArrayList();

                    //��¼��ǰ�����������������ж�������ʾ������λ��
                    int drawHuXiIndex = 0;

                    //ͨ�����þ�����һ����������ʾλ�ã����Ϸ������·�
                    int modValue = 0;//�����������жϺ���λ�õ�����
                    if (m_FirstHuXiUp == "0")
                    {
                        modValue = 1;
                    }

                    for (int i = 0; i < PatientInfo.DataTableHuXi.Rows.Count; i++)
                    {
                        currentDateTime = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[i][0]);

                        #region ����һ���¼�����Ĵ���
                        int sameDateCount = 1;
                        for (int j = i + 1; j < PatientInfo.DataTableHuXi.Rows.Count; j++)
                        {
                            DateTime dt = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[j][0]);
                            if (currentDateTime == dt)
                            {
                                sameDateCount++;
                            }
                        }
                        #endregion

                        //ÿ�ռ�¼����2�����ϣ�Ӧ������Ӧ����Ŀ�����½����¼����1�κ���Ӧ����¼���Ϸ�
                        int m = 0;
                        for (; m < sameDateCount; m++)
                        {
                            currentDateTime = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[i + m]["DateTime"]);
                            string timePoint = PatientInfo.DataTableHuXi.Rows[i + m]["TimePoint"].ToString().Trim();
                            string value = PatientInfo.DataTableHuXi.Rows[i + m]["Value"].ToString().Trim();
                            string memo = PatientInfo.DataTableHuXi.Rows[i + m]["Memo"].ToString().Trim();
                            string linkNext = PatientInfo.DataTableHuXi.Rows[i + m]["LinkNext"].ToString().Trim();
                            string IsSpecial = PatientInfo.DataTableHuXi.Rows[i + m]["IsSpecial"].ToString().Trim();

                            float locationPointX = computeLocationBottomX(currentDateTime, timePoint);//����������X�᷽���ϵ�����
                            {
                                RectangleF rectForHuXiJi = new RectangleF(locationPointX + 1, currentLinePointY + 1, m_LineHeight2 - 1, m_LineHeight2 - 1);
                                if (IsSpecial == "Y")//���ƺ�����
                                {
                                    //g.DrawImage(m_Picture.BitmapHuXiSpecial, rectForHuXiJi);//������ʾ������
                                    m_Picture.DrawHuXiJi(g, rectForHuXiJi.X, rectForHuXiJi.Y);
                                }
                                else
                                {

                                    if (drawHuXiIndex % 2 == modValue)//ÿ���1�κ���Ӧ����¼���Ϸ������������ν����¼ xll�� һ�ű�����к�����������
                                    {
                                        g.DrawString(value, fontSmall, brushBlack, locationPointX, currentLinePointY + m_LineHeight2 - TextRenderer.MeasureText(value, fontSmall).Height);//��ʾ���Ϸ�
                                    }
                                    else
                                    {
                                        g.DrawString(value, fontSmall, brushBlack, locationPointX, currentLinePointY + m_LineHeight2 * 2 - TextRenderer.MeasureText(value, fontSmall).Height);//��ʾ���·�
                                    }
                                    drawHuXiIndex++;
                                }
                            }
                        }
                        i += m - 1;
                    }
                    #endregion

                    currentLinePointY += m_LineHeight2 * 2 + 1;
                }
            }

            #endregion

            #region ����Ѫѹ�����������ܳ���������������ߣ����أ�����ҩ��������ƣ�����1�� ����2������1������2

            //Add by wwj 2012-06-05 �������þ�����λ����ʾ
            int needAddCount = 0;
            for (int i = 0; i < m_ArrayListOther.Count; i++)
            {
                VitalSignsOther vso = m_ArrayListOther[i] as VitalSignsOther;
                if (vso != null)
                {
                    if (vso.Name == VitalSignsType.XueYa.ToString()) //Ѫѹ
                    {
                        #region Ѫѹ
                        int timePointOfDay = vso.TimePointOfDay;//ÿ���¼Ѫѹ��ʱ������Ŀ
                        int xuYaCellWidth = (m_LineHeight2 + 1) * m_DayTimePoint / timePointOfDay;//ѪѹÿһС��Ŀ��
                        string bloodPressureFlag = MethodSet.GetConfigValueByKey("BloodPressureFlag");

                        //����Ѫѹ�ĺ���
                        g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 + 1, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight1 + 1);
                        #region "����Ѫѹÿ����������� - ��������"
                        if (bloodPressureFlag == "0")
                        {
                            for (int m = 0; m < m_Days; m++)
                            {
                                for (int j = 1; j < timePointOfDay; j++)
                                {
                                    int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * m + xuYaCellWidth * j;
                                    int startPointY = currentLinePointY;
                                    int endPointX = startPointX;
                                    int endPointY = currentLinePointY + m_LineHeight1;

                                    g.DrawLine(penBlack, startPointX, startPointY, endPointX, endPointY);
                                }
                            }
                        }

                        #endregion
                        //���ơ�Ѫѹ��
                        string unit = vso.Unit.Trim().Length > 0 ? "(" + vso.Unit + ")" : "";
                        g.DrawString(GetVitalSignsTypeName(vso.Name) + unit, fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 2, m_FirstColumnWidth, m_LineHeight1), leftFormat);

                        //����Ѫѹ��ֵ
                        //by cyq 2012-09-06 17:00
                        #region "����Ѫѹ��ֵ"
                        var xueYaEnu = PatientInfo.DateTableXueYa.AsEnumerable();
                        for (int m = 0; m < PatientInfo.DateTableXueYa.Rows.Count; m++)
                        {
                            DateTime dateTime = Convert.ToDateTime(PatientInfo.DateTableXueYa.Rows[m]["DateTime"]);
                            string timePoint = PatientInfo.DateTableXueYa.Rows[m]["TimePoint"].ToString();
                            int count = xueYaEnu.Where(p => DateTime.Parse(p["DateTime"].ToString()).ToString("yyyy-MM-dd") == dateTime.ToString("yyyy-MM-dd")).CopyToDataTable().Rows.Count;
                            //�������� by cyq 2012-09-07 09:30
                            if (count == 2 && bloodPressureFlag != "0")
                            {
                                int num = new PatientInfo().GetDayOfCurrentDays(m_DataTimeAllocate, dateTime) - 1;
                                for (int j = 1; j < timePointOfDay; j++)
                                {
                                    int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * num + xuYaCellWidth * j;
                                    int startPointY = currentLinePointY;
                                    int endPointX = startPointX;
                                    int endPointY = currentLinePointY + m_LineHeight1;

                                    g.DrawLine(penBlack, startPointX, startPointY, endPointX, endPointY);
                                }
                            }

                            string value = PatientInfo.DateTableXueYa.Rows[m]["Value"].ToString();
                            float width = (m_DayTimePoint * (m_LineHeight2 + 1)) / m_DayTimePointXuYa + 2;
                            float startPointXX = computeLocationBottomX(dateTime, timePoint) - 1;
                            if (bloodPressureFlag != "0" && count == 1)//����
                            {
                                //by cyq 2012-09-07 09:30
                                //����һ��ֻ�а���ļ�¼ʱ
                                //1�������ӿ����Ϊ��һ����ӵĿ��
                                //2����������¼Ϊ����ļ�¼������ʼλ�ü�ȥ������ӿ�ȵ�һ��
                                width = width * 2;
                                if (int.Parse(timePoint) >= 12)
                                {
                                    startPointXX -= (m_LineHeight2 + 1) * (m_DayTimePoint / 2);
                                }
                            }
                            float startPointYY = currentLinePointY;
                            g.DrawString(value, fontSmall, brushBlack, new RectangleF(startPointXX, startPointYY, width, m_LineHeight1), centerFormat);
                        }
                        #endregion

                        currentLinePointY += m_LineHeight1 + 1;
                        #endregion
                    }
                    else
                    {
                        #region ���� ���������ܳ�����������������������ߣ����أ�����ҩ��������ƣ�����1������2

                        DataTable data = new DataTable();

                        if (vso.Name == VitalSignsType.ZongRuLiang.ToString())    //������
                        {
                            data = PatientInfo.DataTableZongRuLiang;
                        }
                        else if (vso.Name == VitalSignsType.ZongChuLiang.ToString())//�ܳ���
                        {
                            data = PatientInfo.DataTableZongChuLiang;
                        }
                        else if (vso.Name == VitalSignsType.YinLiuLiang.ToString())//������
                        {
                            data = PatientInfo.DataTableYinLiuLiang;
                        }
                        else if (vso.Name == VitalSignsType.DaBianCiShu.ToString())//������
                        {
                            data = PatientInfo.DataTableDaBianCiShu;
                        }
                        else if (vso.Name == VitalSignsType.ShenGao.ToString())//���
                        {
                            data = PatientInfo.DataTableShenGao;
                        }
                        else if (vso.Name == VitalSignsType.TiZhong.ToString())//����
                        {
                            data = PatientInfo.DataTableTiZhong;
                        }
                        else if (vso.Name == VitalSignsType.GuoMingYaoWu.ToString())//����ҩ��
                        {
                            data = PatientInfo.DataTableGuoMinYaoWu;
                        }
                        else if (vso.Name == VitalSignsType.TeShuZhiLiao.ToString())//��������
                        {
                            data = PatientInfo.DataTableTeShuZhiLiao;
                        }
                        //else if (vso.Name == VitalSignsType.Other1.ToString())//����1
                        //{
                        //    data = PatientInfo.DataTableOther1;
                        //}
                        //else if (vso.Name == VitalSignsType.PainInfo.ToString())//����1 ���ڸ�Ϊ��ʹ
                        //{
                        //    data = PatientInfo.DataTablePainInfo;
                        //}
                        else if (vso.Name == VitalSignsType.Other2.ToString())//����2 ��Ϊ���������˴�ע�͵�
                        {
                            data = PatientInfo.DataTableOther2;
                        }

                        int timePointOfDay = vso.TimePointOfDay;//ÿ���¼��������ʱ������Ŀ
                        int xuYaCellWidth = (m_LineHeight2 + 1) * m_DayTimePoint / timePointOfDay;//��������ÿһС��Ŀ��

                        //���ƺ���
                        g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 + 5,
                            currentLinePointX + tableWidth,
                            currentLinePointY + m_LineHeight1 + 5);//3  Ѫѹ�ĺ��߻���

                        #region ��ʹ�����ж�
                        //��ʹ��λ����Ѫѹ����---------- add by ywk
                        if (vso.Name == VitalSignsType.PainInfo.ToString()) //����ʹ�Ĵ���
                        {
                            #region  ��ʹ

                            #region ���ڵķ���
                            int TengTongCellWidth = (m_LineHeight2 + 1) * m_DayTimePoint / timePointOfDay;//��ʹÿһС��Ŀ��

                            //������ʹ�ĺ���
                            g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 + 5, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight1 + 5);//+1 ywk   ��ʹ�ĺ��߻���

                            //���ơ���ʹ��
                            string unit = vso.Unit.Trim().Length > 0 ? "(" + vso.Unit + ")" : "";
                            g.DrawString(GetVitalSignsTypeName(vso.Name) + unit, fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 4, m_FirstColumnWidth, m_LineHeight1), leftFormat);


                            //������ʹÿ�����������
                            for (int m = 0; m < m_Days; m++)
                            {
                                for (int j = 1; j < timePointOfDay; j++)
                                {
                                    int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * m + TengTongCellWidth * j;
                                    int startPointY = currentLinePointY;
                                    int endPointX = startPointX;
                                    int endPointY = currentLinePointY + m_LineHeight1 + 5;

                                    g.DrawLine(penBlack, startPointX, startPointY, endPointX, endPointY);
                                }
                            }

                            //������ʹ��ֵ
                            for (int m = 0; m < PatientInfo.DataTablePainInfo.Rows.Count; m++)
                            {
                                DateTime dateTime = Convert.ToDateTime(PatientInfo.DataTablePainInfo.Rows[m]["DateTime"]);
                                string timePoint = PatientInfo.DataTablePainInfo.Rows[m]["TimePoint"].ToString();
                                //int indx = -1;
                                //if (PatientInfo.DataTablePainInfo.Rows[m]["Indx"] == null || PatientInfo.DataTablePainInfo.Rows[m]["Indx"].ToString() == "")
                                //{
                                //    continue;
                                //}
                                //indx = int.Parse(PatientInfo.DataTablePainInfo.Rows[m]["Indx"].ToString());
                                string value = PatientInfo.DataTablePainInfo.Rows[m]["Value"].ToString();

                                float startPointX = computeLocationBottomX(dateTime, timePoint) - 1;
                                float startPointY = currentLinePointY;

                                //float width = (m_DayTimePoint * (m_LineHeight2 + 1)) / m_DayTimePointXuYa + 2;

                                if (timePoint == TimesArray[1].ToString() || timePoint == TimesArray[3].ToString() || timePoint == TimesArray[5].ToString())//��1����¼���·������������ν����¼(m)3,7,11,15,19,23(�˴�����������)7  15  23
                                //if (indx == 1 || indx == 3 || indx == 5)//��1����¼���·������������ν����¼(m)3,7,11,15,19,23(�˴�����������)7  15  23
                                {
                                    g.DrawString(value, fontNormal, brushRed, startPointX + 3, startPointY + m_LineHeight2 - TextRenderer.MeasureText(value, fontNormal).Height);//��ʾ���Ϸ�
                                }
                                else
                                {
                                    g.DrawString(value, fontNormal, brushRed, startPointX + 3, startPointY + m_LineHeight2 * 2 - TextRenderer.MeasureText(value, fontNormal).Height);//��ʾ���·�
                                }
                                //DateTime currentDateTime = DateTime.MinValue;//���ڼ�¼��ǰѭ����ֵ
                                //currentDateTime = Convert.ToDateTime(PatientInfo.DataTablePainInfo.Rows[i][0]);

                                //#region ����һ���¼��ʹ�Ĵ���
                                //int sameDateCount = 1;
                                //for (int j = i + 1; j < PatientInfo.DataTablePainInfo.Rows.Count; j++)
                                //{
                                //    DateTime dt = Convert.ToDateTime(PatientInfo.DataTablePainInfo.Rows[j][0]);
                                //    if (currentDateTime == dt)
                                //    {
                                //        sameDateCount++;
                                //    }
                                //}
                                //#endregion

                                ////���½����¼����1��Ӧ����¼���·�
                                //int h = 0;
                                //for (; h < sameDateCount; h++)
                                //{
                                //    currentDateTime = Convert.ToDateTime(PatientInfo.DataTablePainInfo.Rows[i + h]["DateTime"]);
                                //    string timePoint = PatientInfo.DataTablePainInfo.Rows[i +h]["TimePoint"].ToString().Trim();
                                //    string value = PatientInfo.DataTablePainInfo.Rows[i + h]["Value"].ToString().Trim();
                                //    //float locationPointX = computeLocationBottomX(currentDateTime, timePoint);//��ʹ������X�᷽���ϵ�����
                                //    float startPointX = computeLocationBottomX(currentDateTime, timePoint) - 1;
                                //    float startPointY = currentLinePointY;                                  
                                //    if (h % 2 == 0)//���º��ϣ������¼
                                //    {
                                //        g.DrawString(value, fontNormal, brushRed, startPointX + 3, startPointY + m_LineHeight2 - TextRenderer.MeasureText(value, fontNormal).Height - 2);//��ʾ���Ϸ�
                                //    }
                                //    else
                                //    {
                                //        g.DrawString(value, fontNormal, brushRed, startPointX + 3, startPointY + m_LineHeight2 * 2 - TextRenderer.MeasureText(value, fontNormal).Height - 2);//��ʾ���·�
                                //    }
                                //}
                                //i += h - 1;
                            }
                            #endregion

                            #region ����������
                            //�ڵ�����һ������ʾ��ʹ������
                            //g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight2 * 2, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight2 * 2);

                            ////������ʹ���и���ʱ��ε�����
                            //for (int n = 1; n < m_DayTimePoint * m_Days; n++)
                            //{
                            //    if (n % m_DayTimePoint != 0)
                            //    {
                            //        int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * n;
                            //        int startPointY = currentLinePointY;
                            //        int endPointX = startPointX;
                            //        int endPointY = startPointY + m_LineHeight2 * 2;

                            //        g.DrawLine(penBlack, new Point(startPointX, startPointY), new Point(endPointX, endPointY));
                            //    }
                            //}

                            ////���ƺ��������Լ���λ
                            //g.DrawString("��ʹָ��", fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 1, m_FirstColumnWidth, m_LineHeight2 * 2), leftFormat);



                            //DateTime currentDateTime = DateTime.MinValue;//���ڼ�¼��ǰѭ����ֵ

                            //ArrayList list = new ArrayList();
                            //for (int k = 0; k < PatientInfo.DataTableHuXi.Rows.Count; k++)
                            //{
                            //    currentDateTime = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[k][0]);

                            //    #region ����һ���¼�����Ĵ���
                            //    int sameDateCount = 1;
                            //    for (int j = k + 1; j < PatientInfo.DataTableHuXi.Rows.Count; j++)
                            //    {
                            //        DateTime dt = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[j][0]);
                            //        if (currentDateTime == dt)
                            //        {
                            //            sameDateCount++;
                            //        }
                            //    }
                            //    #endregion

                            //    //ÿ�ռ�¼��ʹ2�����ϣ�Ӧ������Ӧ����Ŀ�����½����¼����1����ʹӦ����¼���Ϸ�
                            //    int m = 0;
                            //    for (; m < sameDateCount; m++)
                            //    {
                            //        currentDateTime = Convert.ToDateTime(PatientInfo.DataTablePainInfo.Rows[k + m]["DateTime"]);
                            //        string timePoint = PatientInfo.DataTablePainInfo.Rows[k + m]["TimePoint"].ToString().Trim();
                            //        string value = PatientInfo.DataTablePainInfo.Rows[k + m]["Value"].ToString().Trim();


                            //        float locationPointX = computeLocationBottomX(currentDateTime, timePoint);//��ʹ������X�᷽���ϵ�����

                            //        if (sameDateCount == 1)//һ���¼��ʹһ�Σ�������Ŀ�о�����ʾ
                            //        {
                            //            g.DrawString(value, fontSmall, brushRed, new RectangleF(locationPointX - 1, currentLinePointY, m_LineHeight2 + 2, m_LineHeight2 * 2), centerFormat);//������ʾ
                            //        }
                            //        else
                            //        {
                            //            RectangleF rectForHuXiJi = new RectangleF(locationPointX + 1, currentLinePointY + 1, m_LineHeight2 - 1, m_LineHeight2 - 1);
                            //            //if (IsSpecial == "Y")//���ƺ�����
                            //            //{
                            //            //    //g.DrawImage(m_Picture.BitmapHuXiSpecial, rectForHuXiJi);//������ʾ������
                            //            //    m_Picture.DrawHuXiJi(g, rectForHuXiJi.X, rectForHuXiJi.Y);
                            //            //}
                            //            //else
                            //            //{
                            //                if (m % 2 == 0)//ÿ��ĵ�1����ʹӦ����¼���Ϸ������������ν����¼
                            //                {
                            //                    g.DrawString(value, fontSmall, brushRed, locationPointX, currentLinePointY + m_LineHeight2 - TextRenderer.MeasureText(value, fontSmall).Height);//��ʾ���Ϸ�
                            //                }
                            //                else
                            //                {
                            //                    g.DrawString(value, fontSmall, brushRed, locationPointX, currentLinePointY + m_LineHeight2 * 2 - TextRenderer.MeasureText(value, fontSmall).Height);//��ʾ���·�
                            //                }
                            //            //}
                            //        }
                            //    }
                            //    i += m - 1;
                            //}
                            #endregion
                            #endregion
                        }

                        #endregion

                        //������λ����(��ʹ�����������棬�������Ϊ����Ѫѹ����)
                        if (vso.Name != VitalSignsType.PainInfo.ToString() && vso.Name != VitalSignsType.XueYa.ToString())//����
                        //if (vso.Name != VitalSignsType.Other2.ToString())//������ȥ������2���жϣ�
                        {
                            string unit = vso.Unit.Trim().Length > 0 ? "(" + vso.Unit + ")" : "";
                            g.DrawString(GetVitalSignsTypeName(vso.Name) + unit, fontNormal, brushBlack,
                                new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 6, m_FirstColumnWidth, m_LineHeight1), leftFormat);//Y+2  +4

                            //������ֵ
                            for (int m = 0; m < data.Rows.Count; m++)
                            {
                                DateTime dateTime = Convert.ToDateTime(data.Rows[m]["DateTime"]);
                                string timePoint = data.Rows[m]["TimePoint"].ToString();
                                //int indx = -1;
                                //if (data.Rows[m]["Indx"] == null || data.Rows[m]["Indx"].ToString() == "")
                                //{
                                //    continue;
                                //}
                                //indx = int.Parse(data.Rows[m]["Indx"].ToString());
                                string value = data.Rows[m]["Value"].ToString();


                                float startPointX = computeLocationBottomX(dateTime, timePoint);
                                float startPointY = currentLinePointY;

                                float width = m_DayTimePoint * (m_LineHeight2 + 1);

                                if (vso.Name == VitalSignsType.DaBianCiShu.ToString())//������
                                {
                                    #region ���ơ���������
                                    if (value.Split(':').Length == 3)
                                    {
                                        string[] valueDaBianXiShuX = value.Split(':');
                                        string valueTemp = valueDaBianXiShuX[0] + valueDaBianXiShuX[1] + "/" + valueDaBianXiShuX[2];
                                        /*
                                         * ���ڴ������Ƚ����⣬��������Ҫ�������⴦��
                                         * ��������������޴�㣬�ԡ�0����ʾ���೦�����ԡ�E����ʾ�����Ӽ�¼������
                                         * ����1/E��ʾ�೦����1�Σ�0/E��ʾ�೦�����ű㣻11/E��ʾ�����ű�1�ι೦�����ű�1�Σ�
                                         * ��������ʾ���ʧ���������ʾ�˹�����
                                         */
                                        if (value.Trim().StartsWith("��") || value.Trim().StartsWith("��"))
                                        {
                                            g.DrawString(valueDaBianXiShuX[0].ToString(), fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 2, width, m_LineHeight1), centerFormat);
                                        }
                                        else if (value.Trim() != "::")
                                        {
                                            float startPaintDaBianXiShuX = startPointX + (width - TextRenderer.MeasureText(valueTemp, fontNormal).Width) / 2;
                                            int increaseWidth;

                                            if (valueDaBianXiShuX.Length == 3)
                                            {
                                                if (string.IsNullOrEmpty(valueDaBianXiShuX[1].ToString()) && string.IsNullOrEmpty(valueDaBianXiShuX[2].ToString()))
                                                {

                                                    increaseWidth = TextRenderer.MeasureText(valueDaBianXiShuX[0], fontNormal).Width - 4;
                                                    g.DrawString(valueDaBianXiShuX[0], fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 9);
                                                }
                                                else
                                                {
                                                    g.DrawString(valueDaBianXiShuX[0], fontNormal, brushBlack, startPaintDaBianXiShuX, startPointY + 9);
                                                    increaseWidth = TextRenderer.MeasureText(valueDaBianXiShuX[0], fontNormal).Width - 4;

                                                    g.DrawString(valueDaBianXiShuX[2], fontSmall, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 8);
                                                    increaseWidth += TextRenderer.MeasureText(valueDaBianXiShuX[2], fontSmall).Width - 5;

                                                    g.DrawString("/", fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth + 2, startPointY + 9);//--����/��λ��
                                                    increaseWidth += TextRenderer.MeasureText("/", fontNormal).Width - 4;

                                                    g.DrawString(valueDaBianXiShuX[1], fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 9);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //��Ϊ��������ʱ��ʲô������ʾ
                                        }
                                    }
                                    #endregion
                                }
                                else if (vso.Name == VitalSignsType.GuoMingYaoWu.ToString())//����ҩ��
                                {
                                    //����ҩ�������Ԫ�����ݾ��Ӻ���ʾ���������;�����ʾ
                                    //add by ywk 2012��5��21�� 09:46:02
                                    int valueWidth = TextRenderer.MeasureText(value, fontNormal).Width;
                                    if (valueWidth > 77)//������Ԫ�񳤶ȵĴ���
                                    {
                                        //g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 5, width * m_Days, m_LineHeight1 + 2), leftFormat);//+2������λ������ֵ��λ��    
                                        g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 5, width * m_Days, m_LineHeight1 + 2), leftFormat);//+2������λ������ֵ��λ��  
                                    }
                                    else//������ʾ
                                    {
                                        g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 5, width, m_LineHeight1 + 2), centerFormat);//+2������λ������ֵ��λ��
                                    }
                                }
                                else
                                {
                                    g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 5, width, m_LineHeight1 + 2), centerFormat);//+2������λ������ֵ��λ��
                                }
                            }
                        }
                        currentLinePointY += m_LineHeight1 + 2;//2
                        #endregion
                    }
                }

            }

            //���Ʊ������һ������
            g.DrawLine(penBlackBold, currentLinePointX, currentLinePointY + 4, currentLinePointX + tableWidth, currentLinePointY + 4);//Y+3  +4�������ײ��ĺ���

            #endregion

            #region ע�� ����Ѫѹ�����������ܳ���������������ߣ����أ�����ҩ��������ƣ�����1�� ����2

            ////Add by wwj 2012-06-05 �������þ�����λ����ʾ
            //for (int i = m_ArrayListOther.Count - 1; i >= 0; i--)
            //{
            //    VitalSignsOther vso = m_ArrayListOther[i] as VitalSignsOther;
            //    if (vso.Name == VitalSignsType.PainInfo.ToString() && m_IsShowTengTongZhiShu == "0")
            //    {
            //        m_ArrayListOther.RemoveAt(i);
            //    }
            //}

            ////�����ų�����
            //g.DrawString("��", fontNormal, brushBlack, new RectangleF(currentLinePointX + 20, currentLinePointY + 4, m_FirstColumnWidth, m_LineHeight1));
            //g.DrawString("��", fontNormal, brushBlack, new RectangleF(currentLinePointX + 20, currentLinePointY + 26, m_FirstColumnWidth, m_LineHeight1));
            //g.DrawString("��", fontNormal, brushBlack, new RectangleF(currentLinePointX + 20, currentLinePointY + 46, m_FirstColumnWidth, m_LineHeight1));

            //for (int i = 0; i < m_ArrayListOther.Count; i++)
            //{
            //    VitalSignsOther vso = m_ArrayListOther[i] as VitalSignsOther;
            //    if (vso != null)
            //    {
            //        //�Ȼ��ų�����������������,���и��ո��У�add by ywk 2012��5��17�� 17:02:21
            //        if (vso.Name == VitalSignsType.DaBianCiShu.ToString() || vso.Name == VitalSignsType.ZongChuLiang.ToString())//�ж�Ϊ������ʱ���ͻ�
            //        {
            //            //�Ȼ�����
            //            int xPoint = m_TableStartPointX + (m_FirstColumnWidth / m_FirstColumnHasSubColumnCount);
            //            g.DrawLine(Pens.Black, xPoint, currentLinePointY - 20, xPoint,
            //                currentLinePointY + m_LineHeight1 * 2 + 3);//15  17

            //            //���ų���������ĺ��� 
            //            g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 * 3 + 4, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight1 * 3 + 4);
            //            //���ų�����Ԫ���ڵ��������ݣ����������������ո��У�
            //            for (int k = 1; k < 2; k++)
            //            {
            //                int pointY = currentLinePointY + (m_LineHeight1) * k + 1;
            //                g.DrawLine(penBlack, currentLinePointX + 58, pointY, currentLinePointX + tableWidth, pointY);
            //            }

            //            //����������������������
            //            DataTable data = new DataTable();
            //            if (vso.Name == VitalSignsType.DaBianCiShu.ToString())
            //            {
            //                data = PatientInfo.DataTableDaBianCiShu;
            //            }
            //            else if (vso.Name == VitalSignsType.ZongChuLiang.ToString())
            //            {
            //                data = PatientInfo.DataTableZongChuLiang;//�ܳ�����Ϊ����
            //            }
            //            g.DrawString(GetVitalSignsTypeName(vso.Name), fontNormal, brushBlack, new RectangleF(currentLinePointX + 84, currentLinePointY + 2, m_FirstColumnWidth, m_LineHeight1), leftFormat);
            //            //������Ӧ��ֵ 
            //            for (int j = 0; j < data.Rows.Count; j++)
            //            {
            //                DateTime dateTime = Convert.ToDateTime(data.Rows[j]["DateTime"]);
            //                string timePoint = data.Rows[j]["TimePoint"].ToString();
            //                string value = data.Rows[j]["Value"].ToString();
            //                float startPointX = computeLocationBottomX(dateTime, timePoint);
            //                float startPointY = currentLinePointY;
            //                float width = m_DayTimePoint * (m_LineHeight2 + 1);

            //                if (vso.Name == VitalSignsType.DaBianCiShu.ToString())//������
            //                {
            //                    #region ���ơ���������
            //                    if (value.Split(':').Length == 3)
            //                    {
            //                        string[] valueDaBianXiShuX = value.Split(':');
            //                        string valueTemp = valueDaBianXiShuX[0] + valueDaBianXiShuX[1] + "/" + valueDaBianXiShuX[2];
            //                        /*
            //                         * ���ڴ������Ƚ����⣬��������Ҫ�������⴦��
            //                         * ��������������޴�㣬�ԡ�0����ʾ���೦�����ԡ�E����ʾ�����Ӽ�¼������
            //                         * ����1/E��ʾ�೦����1�Σ�0/E��ʾ�೦�����ű㣻11/E��ʾ�����ű�1�ι೦�����ű�1�Σ�
            //                         * ��������ʾ���ʧ���������ʾ�˹�����
            //                         */
            //                        if (value.Trim().StartsWith("��") || value.Trim().StartsWith("��"))
            //                        {
            //                            g.DrawString(valueDaBianXiShuX[0].ToString(), fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 2, width, m_LineHeight1), centerFormat);
            //                        }
            //                        else if (value.Trim() != "::")
            //                        {
            //                            float startPaintDaBianXiShuX = startPointX + (width - TextRenderer.MeasureText(valueTemp, fontNormal).Width) / 2;
            //                            int increaseWidth;

            //                            if (valueDaBianXiShuX.Length == 3)
            //                            {
            //                                if (string.IsNullOrEmpty(valueDaBianXiShuX[1].ToString()) && string.IsNullOrEmpty(valueDaBianXiShuX[2].ToString()))
            //                                {

            //                                    increaseWidth = TextRenderer.MeasureText(valueDaBianXiShuX[0], fontNormal).Width - 4;
            //                                    g.DrawString(valueDaBianXiShuX[0], fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 4);//9
            //                                }
            //                                else
            //                                {
            //                                    g.DrawString(valueDaBianXiShuX[0], fontNormal, brushBlack, startPaintDaBianXiShuX, startPointY + 4);//9
            //                                    increaseWidth = TextRenderer.MeasureText(valueDaBianXiShuX[0], fontNormal).Width - 4;

            //                                    g.DrawString(valueDaBianXiShuX[2], fontSmall, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 3);//Y+8
            //                                    increaseWidth += TextRenderer.MeasureText(valueDaBianXiShuX[2], fontSmall).Width - 5;

            //                                    g.DrawString("/", fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth + 2, startPointY + 4);//--����/��λ��//9
            //                                    increaseWidth += TextRenderer.MeasureText("/", fontNormal).Width - 4;

            //                                    g.DrawString(valueDaBianXiShuX[1], fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 4);//9
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            //��Ϊ��������ʱ��ʲô������ʾ
            //                        }
            //                    }
            //                    #endregion
            //                }
            //                else//�˴���������
            //                {
            //                    g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 1, width, m_LineHeight1 + 2), centerFormat);//+5������λ������ֵ��λ��
            //                }
            //            }
            //            currentLinePointY += m_LineHeight1 + 1;
            //        }


            //        #region ע�͵���Ѫѹ
            //        //if (vso.Name == VitalSignsType.XueYa.ToString()) //Ѫѹ
            //        //{
            //        //    #region Ѫѹ
            //        //    int timePointOfDay = vso.TimePointOfDay;//ÿ���¼Ѫѹ��ʱ������Ŀ
            //        //    int xuYaCellWidth = (m_LineHeight2 + 1) * m_DayTimePoint / timePointOfDay;//ѪѹÿһС��Ŀ��

            //        //    //����Ѫѹ�ĺ���
            //        //    g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 + 1, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight1 + 1);

            //        //    //���ơ�Ѫѹ��
            //        //    string unit = vso.Unit.Trim().Length > 0 ? "(" + vso.Unit + ")" : "";
            //        //    g.DrawString(GetVitalSignsTypeName(vso.Name) + unit, fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 2, m_FirstColumnWidth, m_LineHeight1), leftFormat);

            //        //    //����Ѫѹÿ�����������
            //        //    for (int m = 0; m < m_Days; m++)
            //        //    {
            //        //        for (int j = 1; j < timePointOfDay; j++)
            //        //        {
            //        //            int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * m + xuYaCellWidth * j;
            //        //            int startPointY = currentLinePointY;
            //        //            int endPointX = startPointX;
            //        //            int endPointY = currentLinePointY + m_LineHeight1;

            //        //            g.DrawLine(penBlack, startPointX, startPointY, endPointX, endPointY);
            //        //        }
            //        //    }

            //        //    //����Ѫѹ��ֵ
            //        //    for (int m = 0; m < PatientInfo.DateTableXueYa.Rows.Count; m++)
            //        //    {
            //        //        DateTime dateTime = Convert.ToDateTime(PatientInfo.DateTableXueYa.Rows[m]["DateTime"]);
            //        //        string timePoint = PatientInfo.DateTableXueYa.Rows[m]["TimePoint"].ToString();
            //        //        string value = PatientInfo.DateTableXueYa.Rows[m]["Value"].ToString();

            //        //        float startPointX = computeLocationBottomX(dateTime, timePoint) - 1;
            //        //        float startPointY = currentLinePointY;

            //        //        float width = (m_DayTimePoint * (m_LineHeight2 + 1)) / m_DayTimePointXuYa + 2;
            //        //        g.DrawString(value, fontSmall, brushBlack, new RectangleF(startPointX, startPointY, width, m_LineHeight1), centerFormat);
            //        //    }

            //        //    currentLinePointY += m_LineHeight1 + 1;
            //        //    #endregion
            //        //}
            //        #endregion
            //        else
            //        {
            //            #region ����Һ������
            //            DataTable data = new DataTable();

            //            if (vso.Name == VitalSignsType.ZongRuLiang.ToString())    //Һ������
            //            {
            //                data = PatientInfo.DataTableZongRuLiang;
            //                g.DrawString(GetVitalSignsTypeName(vso.Name), fontNormal, brushBlack,
            //                 new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 24, m_FirstColumnWidth, m_LineHeight1), leftFormat);//ywk Y+6 (������λ���Ƶ�����)

            //                for (int m = 0; m < data.Rows.Count; m++)
            //                {
            //                    DateTime dateTime = Convert.ToDateTime(data.Rows[m]["DateTime"]);
            //                    string timePoint = data.Rows[m]["TimePoint"].ToString();
            //                    string value = data.Rows[m]["Value"].ToString();
            //                    float startPointX = computeLocationBottomX(dateTime, timePoint);
            //                    float startPointY = currentLinePointY + 22;
            //                    float width = m_DayTimePoint * (m_LineHeight2 + 1);
            //                    g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 1, width, m_LineHeight1 + 2), centerFormat);//+5������λ������ֵ��λ��
            //                }
            //                //currentLinePointY += m_LineHeight1 + 1;
            //            }
            //            #endregion

            //            #region ����Ѫѹ
            //            else if (vso.Name == VitalSignsType.XueYa.ToString()) //Ѫѹ
            //            {
            //                #region Ѫѹ
            //                int timePointOfDay = vso.TimePointOfDay;//ÿ���¼Ѫѹ��ʱ������Ŀ
            //                int xuYaCellWidth = (m_LineHeight2 + 1) * m_DayTimePoint / timePointOfDay;//ѪѹÿһС��Ŀ��

            //                //����Ѫѹ�ĺ���
            //                g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 * 5 + 7, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight1 * 5 + 7);

            //                //���ơ�Ѫѹ��
            //                string unit = vso.Unit.Trim().Length > 0 ? "(" + vso.Unit + ")" : "";
            //                g.DrawString(GetVitalSignsTypeName(vso.Name) + unit, fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 87, m_FirstColumnWidth, m_LineHeight1), leftFormat);

            //                //����Ѫѹÿ�����������
            //                for (int m = 0; m < m_Days; m++)
            //                {
            //                    for (int j = 1; j < timePointOfDay; j++)
            //                    {
            //                        int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * m + xuYaCellWidth * j;
            //                        int startPointY = currentLinePointY + 86;
            //                        int endPointX = startPointX;
            //                        int endPointY = currentLinePointY + 86 + m_LineHeight1;

            //                        g.DrawLine(penBlack, startPointX, startPointY, endPointX, endPointY);
            //                    }
            //                }

            //                //����Ѫѹ��ֵ
            //                for (int m = 0; m < PatientInfo.DateTableXueYa.Rows.Count; m++)
            //                {
            //                    DateTime dateTime = Convert.ToDateTime(PatientInfo.DateTableXueYa.Rows[m]["DateTime"]);
            //                    string timePoint = PatientInfo.DateTableXueYa.Rows[m]["TimePoint"].ToString();
            //                    string value = PatientInfo.DateTableXueYa.Rows[m]["Value"].ToString();

            //                    float startPointX = computeLocationBottomX(dateTime, timePoint);
            //                    float startPointY = currentLinePointY + 86;

            //                    float width = (m_DayTimePoint * (m_LineHeight2 + 1)) / m_DayTimePointXuYa + 2;
            //                    g.DrawString(value, fontSmall, brushBlack, new RectangleF(startPointX, startPointY, width, m_LineHeight1), centerFormat);
            //                }
            //                //currentLinePointY += m_LineHeight1 + 1;
            //                #endregion
            //            }
            //            #endregion

            //            #region ����  ���أ�����ҩ�����1������2
            //            if (vso.Name == VitalSignsType.TiZhong.ToString())//����
            //            {
            //                data = PatientInfo.DataTableTiZhong;
            //                g.DrawString(GetVitalSignsTypeName(vso.Name), fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 67, m_FirstColumnWidth, m_LineHeight1), leftFormat);
            //                for (int m = 0; m < data.Rows.Count; m++)
            //                {
            //                    DateTime dateTime = Convert.ToDateTime(data.Rows[m]["DateTime"]);
            //                    string timePoint = data.Rows[m]["TimePoint"].ToString();
            //                    string value = data.Rows[m]["Value"].ToString();
            //                    float startPointX = computeLocationBottomX(dateTime, timePoint);
            //                    float startPointY = currentLinePointY + 67;
            //                    float width = m_DayTimePoint * (m_LineHeight2 + 1);
            //                    g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 1, width, m_LineHeight1 + 2), centerFormat);//+5������λ������ֵ��λ��
            //                }
            //                g.DrawLine(penBlack, currentLinePointX, currentLinePointY + 67 + (m_LineHeight1), currentLinePointX + tableWidth, currentLinePointY + 67 + (m_LineHeight1));
            //            }
            //            if (vso.Name == VitalSignsType.GuoMingYaoWu.ToString())//ҩ�����
            //            {
            //                //float width = m_DayTimePoint * (m_LineHeight2 + 1);//+1

            //                data = PatientInfo.DataTableGuoMinYaoWu;
            //                g.DrawString(GetVitalSignsTypeName(vso.Name), fontNormal, brushBlack, new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 89, m_FirstColumnWidth, m_LineHeight1), leftFormat);


            //                for (int m = 0; m < data.Rows.Count; m++)
            //                {
            //                    DateTime dateTime = Convert.ToDateTime(data.Rows[m]["DateTime"]);
            //                    string timePoint = data.Rows[m]["TimePoint"].ToString();
            //                    string value = data.Rows[m]["Value"].ToString();
            //                    float startPointX = computeLocationBottomX(dateTime, timePoint);
            //                    float startPointY = currentLinePointY + 88;
            //                    //float width = m_DayTimePoint * (m_LineHeight2 + 1);
            //                    //g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 1, width , m_LineHeight1 + 2), centerFormat);//+5������λ������ֵ��λ��
            //                    //���ڳ�����Ԫ������ݴ���
            //                    int valueWidth = TextRenderer.MeasureText(value, fontNormal).Width;
            //                    if (valueWidth > 77)
            //                    {
            //                        //int eventTimePointSerialNumber = 0;//�¶Ȳ�����ʱ�����һ���е����
            //                        //for (int j = 0; j < m_DataTableDayTimePoint.Rows.Count; j++)
            //                        //{
            //                        //    if (m_DataTableDayTimePoint.Rows[j][0].ToString() == timePoint)
            //                        //    {
            //                        //        eventTimePointSerialNumber = j;
            //                        //        break;
            //                        //    }
            //                        //}
            //                        //int daySpan = 0;//�¶Ȳ���ʱ��������µ��ϵ�һ�������ڵļ������
            //                        //DateTime firstDateTime = Convert.ToDateTime(m_DateTimeEveryColumnDateTime.Rows[0][0]);
            //                        //daySpan = (dateTime - firstDateTime).Days;

            //                        float width = m_DayTimePoint * (m_LineHeight2 + 1);
            //                        //float xPoint1 = /*m_TableStartPointX + */m_FirstColumnWidth  + (m_LineHeight2 +1) * m_DayTimePoint * daySpan + (m_LineHeight2 ) * eventTimePointSerialNumber;
            //                        g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 1, width * m_Days, m_LineHeight1 + 2), leftFormat);//+5������λ������ֵ��λ��
            //                    }
            //                    else
            //                    {
            //                        float width = m_DayTimePoint * (m_LineHeight2 + 1);
            //                        g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 1, width, m_LineHeight1 + 2), centerFormat);//+5������λ������ֵ��λ��
            //                    }
            //                }
            //            }
            //            //ʣ��Ļ����5������
            //            for (int l = 1; l < 3; l++)
            //            {
            //                int pointY = currentLinePointY + 131 + (m_LineHeight1) * l;//109
            //                g.DrawLine(penBlack, currentLinePointX, pointY, currentLinePointX + tableWidth, pointY);
            //            }
            //            #region ע�͵���
            //            //g.DrawString(GetVitalSignsTypeName(vso.Name), fontNormal, brushBlack, new RectangleF(currentLinePointX + 84, currentLinePointY + 2, m_FirstColumnWidth, m_LineHeight1), leftFormat);
            //            //DataTable data = new DataTable();

            //            //if (vso.Name == VitalSignsType.ZongRuLiang.ToString())    //������
            //            //{
            //            //    data = PatientInfo.DataTableZongRuLiang;
            //            //}
            //            //else if (vso.Name == VitalSignsType.ZongChuLiang.ToString())//�ܳ���
            //            //{
            //            //    data = PatientInfo.DataTableZongChuLiang;
            //            //}

            //            //else if (vso.Name == VitalSignsType.DaBianCiShu.ToString())//������
            //            //{
            //            //    data = PatientInfo.DataTableDaBianCiShu;
            //            //}


            //            //else if (vso.Name == VitalSignsType.Other2.ToString())//����2 ��Ϊ���������£��˴�ע�͵�
            //            //{
            //            //    data = PatientInfo.DataTableOther2;
            //            //}


            //            //int timePointOfDay = vso.TimePointOfDay;//ÿ���¼��������ʱ������Ŀ
            //            //int xuYaCellWidth = (m_LineHeight2 + 1) * m_DayTimePoint / timePointOfDay;//��������ÿһС��Ŀ��

            //            ////���ƺ���
            //            //g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight1 + 5,
            //            //    currentLinePointX + tableWidth,
            //            //    currentLinePointY + m_LineHeight1 + 5);//5  Ѫѹ�ĺ��߻���s

            //            ////������λ����(��ʹ�����������棬�������Ϊ����Ѫѹ����)
            //            //if (vso.Name != VitalSignsType.PainInfo.ToString() && vso.Name != VitalSignsType.Other2.ToString() && vso.Name != VitalSignsType.XueYa.ToString())//����
            //            //{
            //            //    string unit = vso.Unit.Trim().Length > 0 ? "(" + vso.Unit + ")" : "";
            //            //    g.DrawString(GetVitalSignsTypeName(vso.Name) + unit, fontNormal, brushBlack,
            //            //        new RectangleF(currentLinePointX + m_RowCaptionIndent, currentLinePointY + 5, m_FirstColumnWidth, m_LineHeight1), leftFormat);//ywk Y+6 (������λ���Ƶ�����)

            //            //    //������ֵ
            //            //    for (int m = 0; m < data.Rows.Count; m++)
            //            //    {
            //            //        DateTime dateTime = Convert.ToDateTime(data.Rows[m]["DateTime"]);
            //            //        string timePoint = data.Rows[m]["TimePoint"].ToString();
            //            //        string value = data.Rows[m]["Value"].ToString();


            //            //        float startPointX = computeLocationBottomX(dateTime, timePoint);
            //            //        float startPointY = currentLinePointY;

            //            //        float width = m_DayTimePoint * (m_LineHeight2 + 1);//+1

            //            //        if (vso.Name == VitalSignsType.DaBianCiShu.ToString())//������
            //            //        {
            //            //            #region ���ơ���������(��ע�͵�)
            //            //            //if (value.Split(':').Length == 3)
            //            //            //{
            //            //            //    string[] valueDaBianXiShuX = value.Split(':');
            //            //            //    string valueTemp = valueDaBianXiShuX[0] + valueDaBianXiShuX[1] + "/" + valueDaBianXiShuX[2];
            //            //            //    /*
            //            //            //     * ���ڴ������Ƚ����⣬��������Ҫ�������⴦��
            //            //            //     * ��������������޴�㣬�ԡ�0����ʾ���೦�����ԡ�E����ʾ�����Ӽ�¼������
            //            //            //     * ����1/E��ʾ�೦����1�Σ�0/E��ʾ�೦�����ű㣻11/E��ʾ�����ű�1�ι೦�����ű�1�Σ�
            //            //            //     * ��������ʾ���ʧ���������ʾ�˹�����
            //            //            //     */
            //            //            //    if (value.Trim().StartsWith("��") || value.Trim().StartsWith("��"))
            //            //            //    {
            //            //            //        g.DrawString(valueDaBianXiShuX[0].ToString(), fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 2, width, m_LineHeight1), centerFormat);//+2
            //            //            //    }
            //            //            //    else if (value.Trim() != "::")
            //            //            //    {
            //            //            //        float startPaintDaBianXiShuX = startPointX + (width - TextRenderer.MeasureText(valueTemp, fontNormal).Width) / 2;
            //            //            //        int increaseWidth;

            //            //            //        if (valueDaBianXiShuX.Length == 3)
            //            //            //        {
            //            //            //            if (string.IsNullOrEmpty(valueDaBianXiShuX[1].ToString()) && string.IsNullOrEmpty(valueDaBianXiShuX[2].ToString()))
            //            //            //            {

            //            //            //                increaseWidth = TextRenderer.MeasureText(valueDaBianXiShuX[0], fontNormal).Width - 4;
            //            //            //                g.DrawString(valueDaBianXiShuX[0], fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 5);//+6
            //            //            //            }
            //            //            //            else
            //            //            //            {
            //            //            //                g.DrawString(valueDaBianXiShuX[0], fontNormal, brushBlack, startPaintDaBianXiShuX, startPointY + 6);
            //            //            //                increaseWidth = TextRenderer.MeasureText(valueDaBianXiShuX[0], fontNormal).Width - 4;

            //            //            //                g.DrawString(valueDaBianXiShuX[2], fontSmall, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 5);
            //            //            //                increaseWidth += TextRenderer.MeasureText(valueDaBianXiShuX[2], fontSmall).Width - 5;

            //            //            //                g.DrawString("/", fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth + 2, startPointY + 6);//--����/��λ��
            //            //            //                increaseWidth += TextRenderer.MeasureText("/", fontNormal).Width - 4;

            //            //            //                g.DrawString(valueDaBianXiShuX[1], fontNormal, brushBlack, startPaintDaBianXiShuX + increaseWidth, startPointY + 6);
            //            //            //            }
            //            //            //        }
            //            //            //    }
            //            //            //    else
            //            //            //    {
            //            //            //        //��Ϊ��������ʱ��ʲô������ʾ
            //            //            //    }
            //            //            //}
            //            //            #endregion
            //            //        }
            //            //        else if (vso.Name == VitalSignsType.GuoMingYaoWu.ToString())//����ҩ��
            //            //        {
            //            //            g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 5, width * m_Days, m_LineHeight1 + 2), leftFormat);//+2������λ������ֵ��λ��
            //            //        }
            //            //        else
            //            //        {
            //            //            g.DrawString(value, fontNormal, brushBlack, new RectangleF(startPointX, startPointY + 5, width, m_LineHeight1 + 2), centerFormat);//+2������λ������ֵ��λ��
            //            //        }
            //            //    }
            //            //}
            //            //currentLinePointY += m_LineHeight1 + 2;//2
            //            #endregion
            //            #endregion
            //        }
            //    }

            //}
            ////���Ʊ������һ������
            ////g.DrawLine(penBlackBold, currentLinePointX, currentLinePointY + 4, currentLinePointX + tableWidth, currentLinePointY + 4);//Y+3  +4�������ײ��ĺ���
            //g.DrawLine(penBlackBold, currentLinePointX, currentLinePointY + 172, currentLinePointX + tableWidth, currentLinePointY + 172);//���һ�У������ų����Ĳ�¼ �������˱��߶ȣ�(���ĺ���)172
            #endregion
        }

        #region ��������ͼ���������еĺ�����һ��
        /*
        /// <summary>
        /// ��������ͼ���������еĺ���һ��
        /// </summary>
        /// <param name="g"></param>
        /// <param name="penBlack"></param>
        /// <param name="brushBlack"></param>
        /// <param name="brushRed"></param>
        /// <param name="fontNormal"></param>
        /// <param name="fontSmall"></param>
        /// <param name="currentLinePointX"></param>
        /// <param name="currentLinePointY"></param>
        /// <param name="tableWidth"></param>
        /// <param name="centerFormat"></param>
        private void PaintHuXiAtBottom(Graphics g, Pen penBlack, Brush brushBlack, Brush brushRed, Font fontNormal, Font fontSmall,
            int currentLinePointX, int currentLinePointY, int tableWidth, StringFormat centerFormat)
        {

            #region ���ƺ��� ���� ��ߵ�����

            //�ڵ�����һ������ʾ����������
            g.DrawLine(penBlack, currentLinePointX, currentLinePointY + m_LineHeight2 * 2, currentLinePointX + tableWidth, currentLinePointY + m_LineHeight2 * 2);

            //���ƺ������и���ʱ��ε�����
            for (int i = 1; i < m_DayTimePoint * m_Days; i++)
            {
                if (i % m_DayTimePoint != 0)
                {
                    int startPointX = currentLinePointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * i;
                    int startPointY = currentLinePointY;
                    int endPointX = startPointX;
                    int endPointY = startPointY + m_LineHeight2 * 2;

                    g.DrawLine(penBlack, new Point(startPointX, startPointY), new Point(endPointX, endPointY));
                }
            }

            //���ƺ��������Լ���λ
            g.DrawString("����(��/��)", fontNormal, brushBlack, new RectangleF(currentLinePointX, currentLinePointY + 1, m_FirstColumnWidth, m_LineHeight2 * 2), centerFormat);

            #endregion

            #region �ȼ�����Ƶ�λ�ã�Ȼ���ٽ�����Ƴ���
            //��������ֵ���Ƶ���Ӧ��λ����
            DateTime currentDateTime = DateTime.MinValue;//���ڼ�¼��ǰѭ����ֵ

            ArrayList list = new ArrayList();
            for (int i = 0; i < PatientInfo.DataTableHuXi.Rows.Count; i++)
            {
                currentDateTime = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[i][0]);

                #region ����һ���¼�����Ĵ���
                int sameDateCount = 1;
                for (int j = i + 1; j < PatientInfo.DataTableHuXi.Rows.Count; j++)
                {
                    DateTime dt = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[j][0]);
                    if (currentDateTime == dt)
                    {
                        sameDateCount++;
                    }
                }
                #endregion

                //ÿ�ռ�¼����2�����ϣ�Ӧ������Ӧ����Ŀ�����½����¼����1�κ���Ӧ����¼���Ϸ�
                int m = 0;
                for (; m < sameDateCount; m++)
                {
                    currentDateTime = Convert.ToDateTime(PatientInfo.DataTableHuXi.Rows[i + m][0]);
                    string timePoint = PatientInfo.DataTableHuXi.Rows[i + m][1].ToString().Trim();
                    string value = PatientInfo.DataTableHuXi.Rows[i + m][2].ToString().Trim();
                    string memo = PatientInfo.DataTableHuXi.Rows[i + m][3].ToString().Trim();
                    string linkNext = PatientInfo.DataTableHuXi.Rows[i + m][4].ToString().Trim();
                    string IsSpecial = PatientInfo.DataTableHuXi.Rows[i + m][5].ToString().Trim();

                    float locationPointX = computeLocationBottomX(currentDateTime, timePoint);//����������X�᷽���ϵ�����

                    if (sameDateCount == 1)//һ���¼����һ�Σ�������Ŀ�о�����ʾ
                    {
                        g.DrawString(value, fontSmall, brushRed, new RectangleF(locationPointX - 1, currentLinePointY, m_LineHeight2 + 2, m_LineHeight2 * 2), centerFormat);//������ʾ
                    }
                    else
                    {
                        RectangleF rectForHuXiJi = new RectangleF(locationPointX + 1, currentLinePointY + 1, m_LineHeight2 - 1, m_LineHeight2 - 1);
                        if (IsSpecial == "Y")//���ƺ�����
                        {
                            g.DrawImage(m_Picture.BitmapHuXiSpecial, rectForHuXiJi);//������ʾ������
                        }
                        else
                        {
                            if (m % 2 == 0)//ÿ��ĵ�1�κ���Ӧ����¼���Ϸ������������ν����¼
                            {
                                g.DrawString(value, fontSmall, brushRed, locationPointX, currentLinePointY + m_LineHeight2 - TextRenderer.MeasureText(value, fontSmall).Height);//��ʾ���Ϸ�
                            }
                            else
                            {
                                g.DrawString(value, fontSmall, brushRed, locationPointX, currentLinePointY + m_LineHeight2 * 2 - TextRenderer.MeasureText(value, fontSmall).Height);//��ʾ���·�
                            }
                        }
                    }
                }
                i += m - 1;
            }
            #endregion
        }
        */
        #endregion

        #region  ��������ͼ������λ���ݵĵĺ����꣬������Ѫѹ����������������ߣ����صȣ����ܰ�������, ��Ժ����, ������������

        /// <summary>
        /// ��������ͼ������λ���ݵĵĺ����꣬������Ѫѹ�������������ȣ����ܰ���������
        /// </summary>
        /// <param name="currentLinePointY"></param>
        /// <param name="currentDateTime"></param>
        /// <param name="testTimePoint"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private float computeLocationBottomX(DateTime currentDateTime/*����������*/, string testTimePoint/*������ʱ���*/)
        {
            #region ����� ��Ҫ���Ƶ��¼� �� ���� �� ʱ���

            int daySpan = 0;//�¶Ȳ���ʱ��������µ��ϵ�һ�������ڵļ������
            if (m_DateTimeEveryColumnDateTime.Rows.Count > 0)
            {
                DateTime firstDateTime = Convert.ToDateTime(m_DateTimeEveryColumnDateTime.Rows[0][0]);
                daySpan = (currentDateTime - firstDateTime).Days;

                if (daySpan < 0 || daySpan >= m_Days) //�Ѿ��������������µ������ڷ�Χ������Ҫ�ų�
                {
                    return -100;
                }
            }

            int eventTimePointSerialNumber = 0;//�¶Ȳ�����ʱ�����һ���е����
            for (int j = 0; j < m_DataTableDayTimePoint.Rows.Count; j++)
            {
                if (m_DataTableDayTimePoint.Rows[j][0].ToString() == testTimePoint)
                {
                    eventTimePointSerialNumber = j;
                    break;
                }
            }
            #endregion

            float xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + (m_LineHeight2 + 1) * m_DayTimePoint * daySpan + (m_LineHeight2 + 1) * eventTimePointSerialNumber;

            return xPoint;
        }
        #endregion


        #endregion

        #region ������ʾ����

        /// <summary>
        /// �������ұߵ���ʾ����
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintImagePrompt(PictureBox pictureBox, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            int xPoint = m_TableStartPointX + m_FirstColumnWidth + 1 + m_DayTimePoint * (m_LineHeight2 + 1) * m_Days + 14;
            int yPoint = 350;

            VitalSigns vs = new VitalSigns();
            vs.PaintImagePrompt(g, xPoint, yPoint, this.Font);
            PaintPageCount(pictureBox, g);
        }

        /// <summary>
        /// ���Ƶײ�����ʾ���֣���ҳ�������� ��������
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintImagePromptBottom(PictureBox pictureBox, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            int xPoint = m_TableStartPointX;
            int yPoint = Convert.ToInt32(m_TableStartPointY + m_TableHeight + 3);//+30 ywk  +6

            VitalSigns vs = new VitalSigns();
            Font m_font = new Font(this.Font.FontFamily, this.Font.Size * 0.959f, FontStyle.Regular);
            vs.PaintImagePrompt2(g, xPoint, yPoint, m_font);//this.Font�޸�ͼ���С edit by ywk 
            //PaintPageCount(pictureBox, g);//���Ƶײ���ҳ��ȥ�� yw 2012��5��18�� 11:53:41
            //���Ƶڼ��� add by  ywk 2012��5��17�� 15:21:00
            PaintWeekCount(pictureBox, g);
        }
        /// <summary>
        /// �����������ⵥ���Ͻǻ��ơ��ڼ��ܡ�
        /// add by  ywk 2012��5��17�� 15:21:31
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintWeekCount(PictureBox pictureBox, Graphics g)
        {
            string currentPageCount = Convert.ToString((m_DataTimeAllocate - Convert.ToDateTime(PatientInfo.InTime.Split(' ')[0])).Days / m_Days + 1);

            int width = m_FirstColumnWidth + 1 + m_DayTimePoint * (m_LineHeight2 + 1) * m_Days + 14;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            Font font = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Regular);
            g.DrawString("��  " + currentPageCount + "  ��", font, Brushes.Black, pictureBox.Width - TextRenderer.MeasureText(m_HeaderName, font).Width - 110, m_HeaderNameY + 5);
        }

        #endregion

        #region ����ҳ��

        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="g"></param>
        private void PaintPageCount(PictureBox pictureBox, Graphics g)
        {
            string currentPageCount = Convert.ToString((m_DataTimeAllocate - Convert.ToDateTime(PatientInfo.InTime.Split(' ')[0])).Days / m_Days + 1);

            int width = m_FirstColumnWidth + 1 + m_DayTimePoint * (m_LineHeight2 + 1) * m_Days + 14;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            Font font = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Regular);
            g.DrawString("��  " + currentPageCount + "  ҳ", font, Brushes.Black,
                new RectangleF(m_TableStartPointX, m_TableStartPointY + m_TableHeight + 25, width, 20), sf);//+30 ywk +35
        }
        #endregion

        #endregion

        #region ����(private)

        private void ClearVariable()
        {

        }

        /// <summary>
        /// �ж�����ͼ���Ƿ���ʾ�������������ʾ��������ô���������߸�������������һ�е���ʽ��ʾ
        /// </summary>
        /// <returns></returns>
        private bool checkIsContainHuXiInCurve()
        {
            for (int i = 0; i < m_ArrayListVitalSigns.Count; i++)
            {
                VitalSigns vs = m_ArrayListVitalSigns[i] as VitalSigns;

                if (vs != null)
                {
                    if (vs.Name == VitalSignsType.HuXi.ToString())//����
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// �õ�������һС������ֵ
        /// </summary>
        /// <returns></returns>
        private float GetCellValue(string type)
        {
            float cellValue = 0;
            //�õ��¶��趨�����ֵ
            for (int j = 0; j < m_ArrayListVitalSigns.Count; j++)
            {
                VitalSigns vs = (VitalSigns)m_ArrayListVitalSigns[j];
                if (vs.Name == type)
                {
                    cellValue = vs.CellValue;
                }
                else if (vs.Name == type)
                {
                    cellValue = vs.CellValue;
                }
                else if (vs.Name == type)
                {
                    cellValue = vs.CellValue;
                }
            }
            return cellValue;
        }

        /// <summary>
        /// �õ����µ����ֵ
        /// </summary>
        /// <returns></returns>
        private float GetMaxValue(string type)
        {
            float maxValue = 0;
            //�õ��¶��趨�����ֵ
            for (int j = 0; j < m_ArrayListVitalSigns.Count; j++)
            {
                VitalSigns vs = (VitalSigns)m_ArrayListVitalSigns[j];
                if (vs.Name == type)
                {
                    maxValue = vs.MaxValue;
                }
                else if (vs.Name == type)
                {
                    maxValue = vs.MaxValue;
                }
                else if (vs.Name == type)
                {
                    maxValue = vs.MaxValue;
                }
            }
            return maxValue;
        }

        /// <summary>
        /// ������ɫ�õ���ˢ
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        private Brush GetBrushByColorName(string colorName)
        {
            Brush brush;
            if (colorName.ToLower() == "red")
            {
                brush = Brushes.Red;
            }
            else if (colorName.ToLower() == "blue")
            {
                brush = Brushes.Blue;
            }
            else
            {
                brush = Brushes.Black;
            }
            return brush;
        }

        /// <summary>
        /// �ж�����Դ���Ƿ�������
        /// </summary>
        /// <param name="dataTableTableBaseLine"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private int CheckIsNeed(DataTable dataTableTableBaseLine, string columnName)
        {
            for (int i = 0; i < dataTableTableBaseLine.Rows.Count; i++)
            {
                if (dataTableTableBaseLine.Rows[i][0].ToString() == columnName)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// ȷ�����µ���ÿ�е�����(��������Ժʱ���ָ��ʱ���������±���ÿ����ʾ������)
        /// </summary>
        /// <param name="inDate">��Ժ����</param>
        /// <param name="allocateDate">ָ��������</param>
        /// <returns></returns>
        private DataTable GetDateTimeForColumns(DateTime inDate, DateTime allocateDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BeforeConvertDate"); //ת��ǰ��ֵ
            dt.Columns.Add("ConvertedDate"); //ת�����ֵ

            string str1 = PatientInfo.InTime.Split(' ')[0];//��Ժ
            inDate = Convert.ToDateTime(str1);

            allocateDate = Convert.ToDateTime(allocateDate.ToString("yyyy-MM-dd"));

            DateTime dateTimeBeginTime = new DateTime(inDate.Year, inDate.Month, inDate.Day);
            int weeks = (allocateDate - inDate).Days / m_Days;
            dateTimeBeginTime = dateTimeBeginTime.AddDays(m_Days * weeks);

            for (int i = 0; i < m_Days; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dateTimeBeginTime;
                dr[1] = "";
                dateTimeBeginTime = dateTimeBeginTime.AddDays(1);
                dt.Rows.Add(dr);
            }

            if (Convert.ToDateTime(dt.Rows[0][0]).ToString("yyyy-MM-dd") == inDate.ToString("yyyy-MM-dd") //סԺ������ҳ��1�� ����д��-��-�գ��磺2010��03��26��
                || Convert.ToDateTime(dt.Rows[0][0]).ToString("MM-dd") == "01-01") //����ȵ�1������д��-��-�գ��磺2010��03��26��
            {
                dt.Rows[0][1] = Convert.ToDateTime(dt.Rows[0][0]).ToString("yyyy-MM-dd");
            }
            else //����ҳ�����µ��ĵ�1�� ��д��-�գ���03-26��
            {
                dt.Rows[0][1] = Convert.ToDateTime(dt.Rows[0][0]).ToString("MM-dd");
            }

            for (int i = dt.Rows.Count - 1; i > 0; i--)
            {
                DateTime dateTime1 = Convert.ToDateTime(dt.Rows[i][0]);
                DateTime dateTime2 = Convert.ToDateTime(dt.Rows[i - 1][0]);

                if (dateTime1.Year > dateTime2.Year)
                {
                    dt.Rows[i][1] = dateTime1.ToString("yyyy-MM-dd");
                }
                else if (dateTime1.Month > dateTime2.Month)
                {
                    dt.Rows[i][1] = dateTime1.ToString("MM-dd");
                }
                else if (dateTime1.Day > dateTime2.Day)
                {
                    dt.Rows[i][1] = dateTime1.ToString("dd");
                }
            }

            return dt;
        }

        /// <summary>
        /// ͨ���������������õ����������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetVitalSignsTypeName(string type)
        {
            string returnValue = string.Empty;

            if (type == "TiWen")
            {
                returnValue = "����";
            }
            else if (type == "MaiBo")
            {
                returnValue = "����";
            }
            else if (type == "HuXi")
            {
                returnValue = "����";
            }
            else if (type == "LinLv")
            {
                returnValue = "����";
            }
            else if (type == "WuLiJiangWen")
            {
                returnValue = "������";
            }
            else if (type == "XueYa")
            {
                returnValue = "Ѫ ѹ";
            }
            else if (type == "ZongRuLiang")
            {
                returnValue = "����";//��������ΪҺ������
            }
            else if (type == "ZongChuLiang")
            {
                returnValue = "����";// ��Ϊ����
            }
            else if (type == "YinLiuLiang")
            {
                returnValue = "������";
            }
            else if (type == "DaBianCiShu")
            {
                returnValue = "������";
            }
            else if (type == "ShenGao")
            {
                returnValue = "���";
            }
            else if (type == "TiZhong")
            {
                returnValue = "�� ��";
            }
            else if (type == "GuoMingYaoWu")
            {
                returnValue = "ҩ �� �� ��";
            }
            else if (type == "TeShuZhiLiao")
            {
                returnValue = "��������";
            }
            else if (type == "Other1")
            {
                returnValue = "����1";
            }
            else if (type == "Other2")
            {
                returnValue = "����";
           }

            else if (type == "PainInfo")
            {
                returnValue = "��ʹָ��";
            }

            return returnValue;

            /*
            TiWen = 1,          //����
            MaiBo = 2,          //����
            HuXi = 3,           //����
            XinLv = 4,          //����
            WuLiJiangWen = 5,   //������
            XueYa = 6,          //Ѫѹ
            ZongRuLiang = 7,    //������
            ZongChuLiang = 8,   //�ܳ���
            YinLiuLiang = 9,    //������
            DaBianCiShu = 10,    //������
            ShenGao = 11,       //���
            TiZhong = 12,       //����
            GuoMingYaoWu = 13,  //����ҩ��
            TeShuZhiLiao = 14,  //��������
            Other1 = 15,        //����1
            Other2 = 16         //����2
            */
        }

        /// <summary>
        /// ͨ���ò�����Ժ���ں�ָ�����ڵõ�������ָ������ʱ�Ĳ�����Ϣ
        /// </summary>
        private void GetBedIDAndDeptName(DateTime inTime, DateTime allocateDateTime, string noOfInpat)
        {
            //���ڲ�����Ժ��Ŀ��Ҳ�����������ҿ��ܷ����仯����������Ҫ�ȵ����ĳ�����±��е�һ������ں�
            //ͨ��BedInfo�õ���ǰ���������ʱ���Ŀ��ҺͲ�����
            DataTable dataTableDate = GetDateTimeForColumns(inTime, allocateDateTime);
            DateTime firstDateOfTable = DateTime.MinValue; //ĳ�����±��е�һ�������
            if (dataTableDate.Rows.Count > 0)
            {
                firstDateOfTable = Convert.ToDateTime(dataTableDate.Rows[0][0]);
                DataTable dt = PublicSet.MethodSet.GetPatientBedInfoByDate(noOfInpat, firstDateOfTable);
                if (dt.Rows.Count > 0)
                {
                    PatientInfo.BedCode = dt.Rows[0]["FormerBedID"].ToString();
                    PatientInfo.Section = dt.Rows[0]["DeptName"].ToString();
                    //this.m_HospitalName = InitHospitalName(dt.Rows[0]["HospitalName"].ToString());
                }
            }
        }

        private string InitHospitalName(string hospitalName)
        {
            string name = string.Empty;

            for (int i = 0; i < hospitalName.Length - 1; i++)
            {
                name += hospitalName[i] + " ";
            }

            name += hospitalName[hospitalName.Length - 1];

            return name;
        }

        private void GetHospitalName()
        {
            if (this.DesignMode == false)
            {
                m_HospitalName = InitHospitalName(PublicSet.MethodSet.GetHospitalName());
            }
        }

        /// <summary>
        /// �ж�ʱ���Ƿ���Ҫ��ʾʱ��
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private bool CheckEventIsShowTime(string eventName)
        {
            foreach (DataRow dr in m_EventSetting)
            {
                if (dr["name"].ToString().Trim() == eventName.Trim())
                {
                    if (dr["isshowtime"].ToString() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// �õ��¼�����λ��
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private float GetEventLocation(string eventName)
        {
            foreach (DataRow dr in m_EventSetting)
            {
                if (dr["name"].ToString().Trim() == eventName.Trim())
                {
                    return float.Parse(dr["showlocation"].ToString());
                }
            }
            return 42;
        }
        #endregion

        #region ����(public)

        /// <summary>
        /// �жϡ���һ�ܡ��Ƿ���ʾ���߼�
        /// </summary>
        /// <returns></returns>
        public bool DateTimeLogicForLastWeek()
        {
            DataTable dataTableForColumns = GetDateTimeForColumns(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate);
            if (dataTableForColumns.Rows.Count > 0)
            {
                DateTime dt1 = Convert.ToDateTime(dataTableForColumns.Rows[0][0]).AddDays(-m_Days);
                DateTime dt2 = Convert.ToDateTime(PatientInfo.InTime);

                if ((dt1 - dt2).Days >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// �������±����ʾ��ʱ�䣬�������1Ϊ���죬�����±�����ʾ�����ڷ�Χ��������
        /// </summary>
        /// <param name="dateTimeAllocate"></param>
        public void SetAllocateDateTime(DateTime dateTimeAllocate)
        {
            m_DataTimeAllocate = dateTimeAllocate;

            GetBedIDAndDeptName(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate, PatientInfo.NoOfInpat); ;
        }

        public void SetAllocateDateTime()
        {
            m_DataTimeAllocate = Convert.ToDateTime(PatientInfo.InTime);

            GetBedIDAndDeptName(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate, PatientInfo.NoOfInpat); ;
        }

        /// <summary>
        /// �������±����ʾ��ʱ�䣬�������1Ϊ���죬�����±�����ʾ�����ڷ�Χ��������
        /// </summary>
        /// <param name="days"></param>
        public void SetAllocateDateTime(int days)
        {
            m_DataTimeAllocate = m_DataTimeAllocate.AddDays(days);

            GetBedIDAndDeptName(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate, PatientInfo.NoOfInpat);
        }

        /// <summary>
        /// ���ڴ���ת��ת�����˵Ĵ�λ�Ϳ��ҵĴ���
        /// </summary>
        /// <param name="dataTablePatientInfo"></param>
        public void SetPatientInfo(DataTable dataTablePatientInfo)
        {
            if (dataTablePatientInfo.Rows.Count > 0)
            {
                PatientInfo patientInfo = new PatientInfo();
                patientInfo.InitPatientInfo(dataTablePatientInfo);

                GetBedIDAndDeptName(Convert.ToDateTime(PatientInfo.InTime), m_DataTimeAllocate, PatientInfo.NoOfInpat);
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void PrintDocument()
        {
            PrintForm printDocumentForm = new PrintForm();
            printDocumentForm.DefaultPageSize = m_DefaultPrintSize;

            //�����ⵥ���Ƶ�ͼƬ��
            Bitmap bmp = new Bitmap(this.pictureBoxMeasureTable.Width - 60
                , pictureBoxMeasureTable.Height
                , System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            string filePath = "C:\\" + Guid.NewGuid().ToString() + ".wmf";
            Metafile mf = new Metafile(filePath, g.GetHdc(), rect, MetafileFrameUnit.Pixel);

            g = Graphics.FromImage(mf);
            PaintNurseDocument(pictureBoxMeasureTable, g);
            g.Save();
            g.Dispose();

            try
            {
                printDocumentForm.InitPreviewControl(mf, bmp, this);
                printDocumentForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mf.Dispose();
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// ��ȡ��ӡ��bitmap���� xll ���ڵ���
        /// </summary>
        /// <returns></returns>
        public Bitmap GetbitMap()
        {
            try
            {
                //�����ⵥ���Ƶ�ͼƬ��
                Bitmap bmp = new Bitmap(this.pictureBoxMeasureTable.Width - 60
                    , pictureBoxMeasureTable.Height
                    , System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g = Graphics.FromImage(bmp);
                PaintNurseDocument(pictureBoxMeasureTable, g);
                g.Save();
                g.Dispose();
                return bmp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ������ӡ
        /// </summary>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        public void PrintAllDocument()
        {
            DateTime patientInTime = Convert.ToDateTime(PatientInfo.InTime);
            PrintAllForm printAllDocumentForm = new PrintAllForm(patientInTime);
            printAllDocumentForm.DefaultPageSize = m_DefaultPrintSize;
            printAllDocumentForm.StartPosition = FormStartPosition.CenterScreen;
            DialogResult result = printAllDocumentForm.ShowDialog();
            if (result != DialogResult.OK) return;

            SetWaitDialogCaption("���ڻ�ȡ���ݣ�");
            DateTime dtFrom = printAllDocumentForm.DateTimeFrom;
            DateTime dtTo = printAllDocumentForm.DateTimeTo;

            int fromDay = (dtFrom - patientInTime).Days;
            int toDay = (dtTo - patientInTime).Days;

            int fromWeek = Convert.ToInt32(Math.Floor(fromDay / 7.0f));
            int toWeek = Convert.ToInt32(Math.Floor(toDay / 7.0f));

            List<MetaFileInfo> list = new List<MetaFileInfo>();
            for (int indexWeek = fromWeek; indexWeek <= toWeek; indexWeek++)
            {
                SetWaitDialogCaption("���ڻ�ȡ" + patientInTime.AddDays(indexWeek * 7).ToString("yyyy-MM-dd") + "�����ݣ�");

                SetAllocateDateTime(patientInTime.AddDays(indexWeek * 7));
                LoadData();
                this.Refresh();

                //�����ⵥ���Ƶ�ͼƬ��
                Bitmap bmp = new Bitmap(this.pictureBoxMeasureTable.Width - 60
                    , pictureBoxMeasureTable.Height
                    , System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                string filePath = "C:\\" + Guid.NewGuid().ToString() + ".wmf";
                Metafile mf = new Metafile(filePath, g.GetHdc(), rect, MetafileFrameUnit.Pixel);
                g = Graphics.FromImage(mf);

                PaintNurseDocument(pictureBoxMeasureTable, g);

                g.Save();
                g.Dispose();
                list.Add(new MetaFileInfo(filePath, bmp, mf));
            }

            SetWaitDialogCaption("����������ӡ��");
            printAllDocumentForm.Print(list);
            HideWaitDialog();
        }
        #endregion

        private void UCThreeMeasureTable_Click(object sender, EventArgs e)
        {
            simpleButton1.Focus();
        }

        private void pictureBoxMeasureTable_Click(object sender, EventArgs e)
        {
            simpleButton1.Focus();
        }

        WaitDialogForm m_WaitDialog;
        private void SetWaitDialogCaption(string caption)
        {
            if (m_WaitDialog == null)
                m_WaitDialog = new WaitDialogForm("���ڼ�������......", "�����Ժ�");
            if (!m_WaitDialog.Visible)
                m_WaitDialog.Visible = true;
            m_WaitDialog.Caption = caption;


        }

        private void HideWaitDialog()
        {
            if (m_WaitDialog != null)
                m_WaitDialog.Hide();
        }
    }

}
