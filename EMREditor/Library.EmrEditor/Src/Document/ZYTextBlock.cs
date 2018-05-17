using DrectSoft.Library.EmrEditor.Src.Gui;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace DrectSoft.Library.EmrEditor.Src.Document
{
    /// <summary>
    /// ��ʾһ��Ԫ�ؼ��ϵ���������
    /// 
    /// </summary>
    public class ZYTextBlock : ZYTextContainer
    {
        #region bwy :
        string name = "";
        public string Code = "";
        //�Ȳ���WholeElement����ʹ������Ӹ���
        public ZYTextBlock()
        {
        }


        /// <summary>
        /// �Ƿ��Ǳ�ѡ��
        /// </summary>
        public bool MustClick = false;

        /// <summary>
        /// �Ƿ񱻵����ע��
        /// </summary>
        public bool Clicked = false;

        public new virtual string Name
        {
            get { return name; }
            set
            {
                name = value;
                //��������Text��set�¼�,ѡ��Ԫ����Ҫ��������Ϊ��ѡ�����ֵ�ʱ�򣬲�Ӧ������ʾԭ����ѡ�����ݡ�
                this.Text = value;
            }
        }

        public string text = "";
        public virtual string Text
        {
            get
            {
                return text;
            }

            set
            {
                this.ChildElements.Clear();
                //edit by Ukey zhang 2017-11-10 
                //foreach �޷��õ�ǰ���ַ����Ը���for
                if (WeiWenProcess.weiwen)
                {
                    for (int iCount = 0; iCount < value.Length; iCount++)
                    {
                        char myPreChar, myFontChar;
                        if (iCount == 0)
                            myPreChar = ' ';
                        else
                            myPreChar = value[iCount - 1];

                        if (iCount == value.Length - 1)
                            myFontChar = ' ';
                        else
                            myFontChar = value[iCount + 1];

                        ZYTextChar c = new ZYTextChar();
                        c.Char = WeiWenProcess.strPase(value[iCount], myPreChar, myFontChar);

                        Attributes.CopyTo(c.Attributes);
                        c.UpdateAttrubute();

                        c.Parent = this;
                        c.OwnerDocument = this.OwnerDocument;
                        this.ChildElements.Add(c);
                    }
                }
                else
                {
                    foreach (char myc in value)
                    {
                        ZYTextChar c = new ZYTextChar();
                        c.Char = myc;

                        Attributes.CopyTo(c.Attributes);
                        c.UpdateAttrubute();

                        c.Parent = this;
                        c.OwnerDocument = this.OwnerDocument;
                        this.ChildElements.Add(c);
                    }
                }
                text = value;
            }
        }

        #endregion bwy :

        /// <summary>
        /// �Ƿ��ǹؼ�����
        /// </summary>
        public bool KeyField
        {
            get { return myAttributes.GetString(ZYTextConst.c_KeyField) != "0"; }
            set { myAttributes.SetValue(ZYTextConst.c_KeyField, (value ? "1" : "0")); }
        }

        /// <summary>
        /// �Ƿ���ʾͻ����ʾ����
        /// </summary>
        protected bool bolStandOutBack = false;


        /// <summary>
        /// ��������������־Ϊ��������������־
        /// </summary>
        public override bool Locked
        {
            get { return myParent.Locked; }
            set { base.Locked = value; }
        }
        /// <summary>
        /// block����Ϊ��
        /// </summary>
        public override bool Block
        {
            get { return true; }
        }
        /// <summary>
        /// �����أ������ǿ�Ʒ���
        /// </summary>
        /// <returns></returns>
        public override bool isNewLine()
        {
            return false;
        }
        /// <summary>
        /// �����أ������ǿ�Ʒֶ���
        /// </summary>
        /// <returns></returns>
        public override bool isNewParagraph()
        {
            return false;
        }
        /// <summary>
        /// �����أ������ռһ��
        /// </summary>
        /// <returns></returns>
        public override bool OwnerWholeLine()
        {
            return false;
        }
        /// <summary>
        /// ������:ˢ����ͼ״̬,�ж��Ƿ���Ҫ���Ʊ���
        /// </summary>
        /// <remarks>���ı����ж��Ƿ���Ʊ���������Ϊ
        /// 1.�ĵ������ڴ�ӡģʽ
        /// 2.û��ѡ�е�����
        /// 3.�ĵ��ĵ�ǰ���������Ԫ��Ϊ���ı���������ı�����</remarks>
        public override void ResetViewState()
        {
            bolStandOutBack = false;
            if (myOwnerDocument.Content.SelectLength != 0)
                return;
            if (myOwnerDocument.Content.CurrentElement == this
                || myChildElements.Contains(myOwnerDocument.Content.CurrentElement)
                || myOwnerDocument.CurrentHoverElement == this)
                bolStandOutBack = true;
        }//void ResetViewState()

        /// <summary>
        /// �����أ���ð����������С����
        /// </summary>
        /// <returns></returns>
        public override System.Drawing.Rectangle GetContentBounds()
        {
            System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty;
            foreach (ZYTextElement myElement in myChildElements)
            {
                if (rect.IsEmpty)
                    rect = Bounds;
                else
                    rect = System.Drawing.Rectangle.Union(rect, myElement.Bounds);
            }
            return System.Drawing.Rectangle.Union(rect, this.Bounds);
        }


        /// <summary>
        /// ������:�����������ı����¼�,���»��ƶ���
        /// </summary>
        public override void HandleEnter()
        {
            RefreshForSelect();
        }
        /// <summary>
        /// ������:��������뿪�ı����¼�,���»��ƶ���
        /// </summary>
        public override void HandleLeave()
        {
            RefreshForSelect();
        }

        private void RefreshForSelect()
        {
            bool bolBack = this.bolStandOutBack;
            this.ResetViewState();
            if (bolBack != this.bolStandOutBack)
            {
                myOwnerDocument.RefreshElement(this);
            }
        }

        public override bool isTextElement()
        {
            return true;
        }
        StringFormat strFormat = StringFormat.GenericTypographic;
        Pen pen = new Pen(Color.Black);
        public override void DrawBackGround(ZYTextElement myElement)
        {
            if (myElement.Parent is ZYFixedText && this.OwnerDocument.Info.DocumentModel == DocumentModel.Edit)
            {
                return;
            }

            Rectangle rect = myElement.Bounds;

            if (myElement.Parent is ZYTextBlock)
            {
                if (myElement.Parent.LastElement == myElement && !WeiWenProcess.weiwen)
                    rect.Width -= 10;
            }


            //��ӡ״̬�����Ʊ���������״̬���Ʊ����������ܱ�����͸����
            if (this.OwnerDocument.Info.Printing || this.OwnerDocument.OwnerControl.bolLockingUI)
            {

            }
            else
            {
                switch (ZYEditorControl.ElementStyle)
                {
                    case "�»���":
                        pen.Color = ZYEditorControl.ElementBackColor;

                        pen.Width = 1;//DrectSoft.Library.EmrEditor.Src.Gui.GraphicsUnitConvert.Convert(2, GraphicsUnit.Pixel,GraphicsUnit.Document );
                        this.OwnerDocument.View.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);

                        if (myElement.Parent is ZYTextBlock)
                        {
                            if (myElement.Parent.LastElement == myElement)
                            {
                                this.OwnerDocument.View.DrawLine(pen, rect.Right, rect.Bottom - 5, rect.Right, rect.Bottom + 5);
                            }

                        }

                        break;
                    case "����ɫ":
                        this.OwnerDocument.View.FillRectangle(ZYEditorControl.ElementBackColor, rect);
                        break;
                }
                //base.DrawBackGround(myElement);
            }


            //��ʹ��ֻ��״̬����������ڼ��������У���ͬ�༭״̬
            if (this.OwnerDocument.OwnerControl.ActiveEditArea != null)
            {
                if (this.OwnerDocument.OwnerControl.ActiveEditArea.Top <= myElement.RealTop && myElement.RealTop + this.Height <= this.OwnerDocument.OwnerControl.ActiveEditArea.End)
                {
                    switch (ZYEditorControl.ElementStyle)
                    {
                        case "�»���":
                            pen.Color = ZYEditorControl.ElementBackColor;
                            pen.Width = 2;
                            this.OwnerDocument.View.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                            break;
                        case "����ɫ":
                            this.OwnerDocument.View.FillRectangle(ZYEditorControl.ElementBackColor, rect);
                            break;
                    }
                }
            }
        }

        public override bool HandleClick(int x, int y, MouseButtons Button)
        {
            return base.HandleClick(x, y, Button);
        }
        //˫����������
        public override bool HandleDblClick(int x, int y, MouseButtons Button)
        {
            this.Clicked = true;

            //��ǰ�ַ��������ж��Ƿ���[]{}��
            ZYTextElement curChar = this.OwnerDocument.GetElementByPos(x, y);
            //Debug.WriteLine("block handledbclick ��ǰԪ�� " + curChar);

            //ѡ����ַ���
            StringBuilder str = new StringBuilder();
            this.GetFinalText(str);
            int m = this.ChildElements.IndexOf(curChar);

            int tmpindex = -1;

            //������[]���������
            List<int> start = new List<int>();
            List<int> end = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '[')
                {
                    start.Add(i);
                }
                if (str[i] == ']')
                {
                    end.Add(i);
                }
            }

            //������{}���������
            List<int> startm = new List<int>();
            List<int> endm = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '{')
                {
                    startm.Add(i);
                }
                if (str[i] == '}')
                {
                    endm.Add(i);
                }
            }

            foreach (ZYTextElement ele in this.ChildElements)
            {
                if (ele.Bounds.Contains(x, y))
                {
                    this.Clicked = true;

                    if (this is ZYSelectableElement)
                    {
                        //�����ǰ����ѡ��ģ���е�һ�������滻ģ��
                        //�滻��ģ��
                        //����ԭ��ѡ��������չ������[xxx]ת����������ģ��Ԫ��
                        ArrayList al = new ArrayList();
                        if (ele == curChar)
                        {
                            #region bwy //ѭ��[]ƥ�����
                            for (int i = 0; i < start.Count; i++)
                            {
                                if (start[i] < m && m < end[i])
                                {
                                    //������Ҫչ��ģ��ľ��� 
                                    if (MessageBox.Show("ȷ��Ҫ��ѡ��չ��Ϊģ����", "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        this.OwnerDocument.BeginUpdate();
                                        this.OwnerDocument.BeginContentChangeLog();

                                        tmpindex = i;
                                        //չ��
                                        this.OwnerDocument._Delete();
                                        string f = "[]";
                                        bool isintmp = false;
                                        string tmpname = "";
                                        foreach (char c in str.ToString())
                                        {
                                            if (f.IndexOf(c) == 0)
                                            {
                                                isintmp = true;
                                                tmpname = "";
                                                continue;
                                            }
                                            if (f.IndexOf(c) == 1)
                                            {
                                                isintmp = false;
                                                //������Ϊtmpname��ģ��
                                                ZYTemplate tmp = new ZYTemplate();
                                                tmp.Name = tmpname;
                                                tmp.Parent = this.Parent;
                                                tmp.OwnerDocument = this.OwnerDocument;
                                                this.OwnerDocument._InsertBlock(tmp);

                                                al.Add(tmp);

                                                continue;
                                            }
                                            if (isintmp)
                                            {
                                                tmpname += c;
                                            }
                                            else
                                            {
                                                this.OwnerDocument.Content.InsertString(c.ToString());
                                            }
                                        }
                                        ZYTemplate tmp2 = al[tmpindex] as ZYTemplate;
                                        this.OwnerDocument.Content.CurrentElement = tmp2.FirstElement;
                                        tmp2.HandleDblClick(tmp2.FirstElement.RealLeft, tmp2.FirstElement.RealTop, Button);

                                        this.OwnerDocument.Content.SelectLength = 0;
                                        this.OwnerDocument.EndContentChangeLog();
                                        this.OwnerDocument.EndUpdate();

                                        Debug.WriteLine("Ӧ��չ��ģ�� " + (al[tmpindex] as ZYTemplate).Name);
                                        return true;
                                    }
                                }
                            }
                            #endregion bwy

                            #region bwy ѭ��{}ƥ���ÿһ��
                            for (int j = 0; j < startm.Count; j++)
                            {
                                //�����ǰԪ����ĳ����ʾ�м�
                                if (startm[j] < m && m < endm[j])
                                {
                                    //����¼����ʾ
                                    string tmpname = str.ToString().Substring(startm[j] + 1, endm[j] - startm[j] - 1);
                                    //������Ϊtmpname��¼����ʾ
                                    ZYPromptText p = new ZYPromptText();
                                    p.Name = tmpname;
                                    p.Parent = this.Parent;
                                    p.OwnerDocument = this.OwnerDocument;

                                    FormatFrm HelpWinx = new FormatFrm(p, this as ZYSelectableElement, startm[j], endm[j]);
                                    HelpWinx.Show();
                                    return true;
                                }
                            }
                            #endregion bwy



                        }

                        ImplementFrm HelpWin = new ImplementFrm((ZYSelectableElement)this);
                        HelpWin.Show();
                        //Debug.WriteLine("��ʾ��������OK");
                        return true;
                    }
                    if (this is ZYFormatDatetime || this is ZYFormatNumber || this is ZYFormatString || this is ZYPromptText)
                    {
                        FormatFrm HelpWin = new FormatFrm(this);
                        HelpWin.Show();
                        return true;
                    }

                    if (this is ZYTemplate)
                    {
                        this.OwnerDocument.ReplaceTemplate(this.Type, this.Name);
                        return true;
                    }

                    if (this is ZYReplace)
                    {
                        TextBoxFrm TextWin = new TextBoxFrm(this);
                        TextWin.ShowDialog();
                        return true;
                    }
                    if (this is ZYLookupEditor)
                    {
                        LookupEditorForm TextWin = new LookupEditorForm(this);
                        if (TextWin.NormalWordBook == null || TextWin.NormalWordBook == "")
                            return false;
                        TextWin.ShowDialog();
                        return true;
                    }
                }
            }

            return base.HandleDblClick(x, y, Button);

        }

        public override bool HandleMouseDown(int x, int y, System.Windows.Forms.MouseButtons Button)
        {
            //MessageBox.Show("HandleMouseDown");
            if (Button == MouseButtons.Right)
            {
                contextMenu.Show(Control.MousePosition);
            }
            return base.HandleMouseDown(x, y, Button);
        }

        public override bool ToXML(XmlElement myElement)
        {
            myElement.SetAttribute("mustclick", this.MustClick.ToString());
            myElement.SetAttribute("code", this.Code);
            //myElement.SetAttribute("clicked", this.Clicked.ToString());
            return true;
            //return base.ToXML(myElement);
        }

        public override bool FromXML(XmlElement myElement)
        {
            this.MustClick = myElement.GetAttribute("mustclick") != "" ? bool.Parse(myElement.GetAttribute("mustclick")) : false;
            this.Code = myElement.GetAttribute("code");
            //this.Clicked = bool.Parse(myElement.GetAttribute("clicked"));
            return true;
            //return base.FromXML(myElement);
        }
    }// class ZYTextBlock
}