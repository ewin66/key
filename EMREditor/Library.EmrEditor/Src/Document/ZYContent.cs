using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
///////////////////////���л���Ҫ������
using System.Drawing;
using System.Windows.Forms;

namespace DrectSoft.Library.EmrEditor.Src.Document
{
    /// <summary>
    /// ��Ȼ���Ե��Ӳ����ĵ����ݹ�����
    /// </summary>
    /// <remarks>����������ά��һ���б����б���ȫ��Ϊ<link>DrectSoft.Library.EmrEditor.Src.Document.ZYTextElement</link>���ͣ�
    /// ����ɾ���޸ĺͲ���Ԫ�أ����Ӳ����ı��ĵ�������Խ�������ά���Լ��Ŀ���ʾԪ�ص��б�
    /// ��Ԫ�ػ�ʹ��<a href="#ZYTextDocumentLib.ZYContent.SelectStart">SelectStart</a>���Ժ�<a href="#ZYTextDocumentLib.ZYContent.SelectLength">SelectLength</a>����
    /// �������ĵ��еĲ�����ѡ�����򣬲��ṩһϵ�к�����������������������
    /// ���⻹�ṩ��һЩ�����ĵ����ݵ�ͨ������
    /// ������ʹ��<link>ZYTextDocumentLib.IEMRContentDocument</link>�ӿ��������ĵ�������
    /// </remarks>
    /// 
    [Serializable]
    public class ZYContent
    {
        private ZYTextDocument myDocument = null;
        /// <summary>
        /// ������ʾ��Ԫ�ؼ���,��ZYDocument�Ĺ��캯���г�ʼ��
        /// </summary>
        private System.Collections.ArrayList myElements = null;

        /// <summary>
        /// mfb ����������һ��������ڵ�����λ��
        /// ����ǵ�һ�δ��ĵ�,���λ��Ĭ���ڵ�һ��Ԫ�ش�,��ȻΪ0
        /// </summary>
        private int intSelectStart = 0;
        /// <summary>
        /// mfb ����������һ�����ѡ��ĳ���,���߽в���
        /// ���Ϊ0, ������,��û�л�ѡ.
        /// </summary>
        private int intSelectLength = 0;

        private string strFixLenText = null;

        private bool bolModified = false;
        private bool bolAutoClearSelection = true;

        private int intLastXPos = -1;

        private bool bolLineEndFlag = false;


        public bool LineEndFlag
        {
            get { return bolLineEndFlag; }
        }
        /// <summary>
        /// �������������ĵ�����
        /// </summary>
        public ZYTextDocument Document
        {
            get { return myDocument; }
            set { myDocument = value; }
        }

        /// <summary>
        /// �Ƿ��Զ����ѡ��״̬,��ΪTrue������λ���޸�ʱ���Զ�����SelectLength���ԣ��������ݾɵĲ�����λ�ü���SelectLength����
        /// </summary>
        public bool AutoClearSelection
        {
            get { return bolAutoClearSelection; }
            set
            {
                bolAutoClearSelection = value;
            }
        }

        /// <summary>
        /// ������ʾ��Ԫ���б�
        /// </summary>
        public System.Collections.ArrayList Elements
        {
            get { return myElements; }
            set
            {
                myElements = value;
                bolModified = false;
                strFixLenText = null;
                this.SetSelection(0, 0);
            }
        }

        public int IndexOf(ZYTextElement e)
        {
            return myElements.IndexOf(e);
        }

        /// <summary>
        /// ����,�����ĵ������Ƿ�ı�
        /// </summary>
        public bool Modified
        {
            get { return bolModified; }
            set { bolModified = value; }
        }



        #region ��ȡԪ�غ���Ⱥ
        /// <summary>
        /// ��õ�ǰ�����ǰ�����һ���ַ�Ԫ��
        /// </summary>
        /// <returns>������ַ�Ԫ�أ���û�ҵ��򷵻ؿ�����</returns>
        public ZYTextChar GetPreChar()
        {
            for (int iCount = (intSelectStart == 0 && myElements.Count > 1 ? 1 : intSelectStart - 1); iCount >= 0; iCount--)
                if (myElements[iCount] is ZYTextChar)
                {
                    return (ZYTextChar)myElements[iCount];
                }
            return null;
        }


        /// <summary>
        /// <para>����ģʽ</para>
        /// <para>��ù������N��Ԫ�أ�0Ϊ��괦Ԫ�أ�A��</para>
        /// <para>1 Ϊ ��3�� �Դ�����   ���� 1 2 3 ��� A B C ����</para>
        /// <para>ά��ģʽ</para>
        /// <para>��ȡ����Ҳ��N��Ԫ�أ�0Ϊ��괦Ԫ�أ�A��</para>
        /// <para>1 Ϊ ��B�� �Դ�����   ���� 1 2 3 ��� A B C ����</para>
        /// </summary>
        /// <returns>������ַ�Ԫ�أ���û�ҵ��򷵻ؿ�����</returns>
        public ZYTextChar GetPreChar(int index)
        {
            int iCount = (intSelectStart == 0 && myElements.Count > 1 ? -index : intSelectStart - index);
            //С������ֵ�����ط�ά���ַ�
            if (iCount < 0)
                return new ZYTextChar();
            if (myElements[iCount] is ZYTextChar)
            {
                return (ZYTextChar)myElements[iCount];
            }
            return new ZYTextChar();
        }
        /// <summary>
        /// <para>����ģʽ</para>
        /// <para>��ù���Ҳ��N��Ԫ�أ�0Ϊ��괦Ԫ�أ�A��</para>
        /// <para>1 Ϊ ��B�� �Դ�����   ���� 1 2 3 ��� A B C ����</para>
        /// <para>ά��ģʽ</para>
        /// <para>��ȡ�������N��Ԫ�أ�0Ϊ��괦Ԫ�أ�A��</para>
        /// <para>1 Ϊ ��3�� �Դ�����   ���� 1 2 3 ��� A B C ����</para>
        /// </summary>
        /// <returns></returns>
        public ZYTextChar GetFontChar(int index)
        {
            int iCount = (intSelectStart == 0 && myElements.Count > 1 ? index : intSelectStart + index);
            //�������ַ����������ط�ά���ַ�
            if (iCount > myElements.Count - 1)
                return new ZYTextChar();
            if (myElements[iCount] is ZYTextChar)
            {
                return (ZYTextChar)myElements[iCount];
            }
            return new ZYTextChar();
        }
        /// <summary>
        /// ������λ��
        /// </summary>
        public int SelectStart
        {
            get { return intSelectStart; }
            set
            {
                if (bolAutoClearSelection)
                    this.SetSelection(value, 0);
                else
                    this.SetSelection(value, intSelectStart - value);
            }
        }

        /// <summary>
        /// �����һ��
        /// </summary>
        public ZYTextLine PreLine
        {
            get
            {
                try
                {
                    ZYTextLine myLine = this.CurrentLine;
                    if (myDocument.Lines.IndexOf(myLine) > 0)
                    {
                        for (int iCount = intSelectStart - 1; iCount >= 0; iCount--)
                        {
                            ZYTextElement myElement = (ZYTextElement)myElements[iCount];
                            if (myElement.OwnerLine != myLine)
                                return myElement.OwnerLine;
                        }
                        return null;
                    }
                    else
                        return myLine;
                }
                catch { }
                return null;
            }
        }

        /// <summary>
        /// �����һ��
        /// </summary>
        public ZYTextLine NextLine
        {
            get
            {
                try
                {
                    ZYTextLine myLine = this.CurrentLine;
                    if (myDocument.Lines.IndexOf(myLine) < myDocument.Lines.Count - 1)
                    {
                        for (int iCount = intSelectStart + 1; iCount < myElements.Count; iCount++)
                        {
                            ZYTextElement myElement = (ZYTextElement)myElements[iCount];
                            if (myElement.OwnerLine != myLine)
                                return myElement.OwnerLine;
                        }
                        return null;
                    }
                    else
                        return myLine;
                }
                catch { }
                return null;
            }
        }

        /// <summary>
        /// ��õ�ǰ��
        /// </summary>
        public ZYTextLine CurrentLine
        {
            get
            {
                if (myElements.Count == 0)
                    return null;
                else
                {
                    if (myElements != null && intSelectStart >= 0 && intSelectStart < myElements.Count)
                    {
                        ZYTextLine myLine = ((ZYTextElement)myElements[intSelectStart]).OwnerLine;
                        if (this.bolLineEndFlag && myDocument.Lines.IndexOf(myLine) > 0)
                            return (ZYTextLine)myDocument.Lines[myDocument.Lines.IndexOf(myLine) - 1];
                        else
                            return myLine;
                    }
                    else
                        return ((ZYTextElement)myElements[myElements.Count - 1]).OwnerLine;
                }
            }
        }// CurrenLine 

        /// <summary>
        /// ��õ�ǰԪ��
        /// </summary>
        public ZYTextElement CurrentElement
        {
            get
            {
                ZYTextElement myElement = null;
                if (myElements.Count == 0)
                    return null;
                else
                {

                    if (myElements != null && intSelectStart >= 0 && intSelectStart < myElements.Count)
                    {
                        myElement = (ZYTextElement)myElements[intSelectStart];
                    }

                    else
                    {
                        myElement = (ZYTextElement)myElements[myElements.Count - 1];
                    }
                    return myElement;
                }
            }
            set
            {
                if (myElements.Contains(value))
                    this.MoveSelectStart(myElements.IndexOf(value));
                intSelectStart = this.FixIndex(intSelectStart);
                Debug.WriteLine("���õ�ǰԪ�� " + value + " value.RealTop:" + value.RealTop);
            }
        }

        /// <summary>
        /// ����ģʽ
        /// ��ù������N��Ԫ�أ�0Ϊ��괦Ԫ�أ�A��
        /// 1 Ϊ ��3�� �Դ�����   ���� 1 2 3 ��� A B C ����
        /// </summary>
        /// <returns>�����Ԫ�أ���û�ҵ��򷵻ؿ�����</returns>
        public ZYTextElement GetLeftElement(int index)
        {
            int iCount = (intSelectStart == 0 && myElements.Count > 1 ? -index : intSelectStart - index);
            //С������ֵ�����ط�ά���ַ�
            if (iCount < 0)
                return new ZYTextElement();
            if (myElements[iCount] is ZYTextElement)
            {
                return (ZYTextElement)myElements[iCount];
            }
            return new ZYTextElement();
        }
        /// <summary>
        /// ����ģʽ
        /// ��ù���Ҳ��N��Ԫ�أ�0Ϊ��괦Ԫ�أ�A��
        /// 1 Ϊ ��B�� �Դ�����   ���� 1 2 3 ��� A B C ����
        /// </summary>
        /// <returns></returns>
        public ZYTextElement GetRightElement(int index)
        {
            int iCount = (intSelectStart == 0 && myElements.Count > 1 ? index : intSelectStart + index);
            //�������ַ����������ط�ά���ַ�
            if (iCount > myElements.Count - 1)
                return new ZYTextElement();
            if (myElements[iCount] is ZYTextElement)
            {
                return (ZYTextElement)myElements[iCount];
            }
            return new ZYTextElement();
        }

        /// <summary>
        /// ��õ�ǰѡ�е�Ԫ��,��û��ѡ��Ԫ�ػ�ѡ�ж��Ԫ���򷵻ؿ�
        /// </summary>
        public ZYTextElement CurrentSelectElement
        {
            get
            {
                if (myElements.Count == 0 || (intSelectLength != 1 && intSelectLength != -1))
                    return null;
                else
                    return (ZYTextElement)myElements[this.AbsSelectStart];
            }
            set
            {
                if (myElements.Contains(value))
                {
                    this.SetSelection(myElements.IndexOf(value) + 1, -1);
                }
            }
        }
        /// <summary>
        /// ��õ�ǰλ�õ�ǰһ��Ԫ��
        /// </summary>
        public ZYTextElement PreElement
        {
            get
            {
                if (myElements != null && myElements.Count > 0 && intSelectStart > 0 && intSelectStart < myElements.Count)
                    return (ZYTextElement)myElements[intSelectStart - 1];
                else
                    return null;
            }
        }

        /// <summary>
        /// ���ָ��Ԫ�ص�ǰһ��Ԫ��
        /// </summary>
        /// <param name="refElement">ָ����Ԫ��</param>
        /// <returns>��Ԫ�ص�ǰһ��Ԫ����û�ҵ��򷵻ؿ�</returns>
        public ZYTextElement GetPreElement(ZYTextElement refElement)
        {
            int index = myElements.IndexOf(refElement);
            if (index >= 1)
                return (ZYTextElement)myElements[index - 1];
            else
                return null;
        }

        /// <summary>
        /// ���ָ��Ԫ�صĺ�һ��Ԫ��
        /// </summary>
        /// <param name="refElement">ָ����Ԫ��</param>
        /// <returns>��Ԫ�ص�ǰһ��Ԫ�أ���û���ҵ��򷵻ؿ�</returns>
        public ZYTextElement GetNextElement(ZYTextElement refElement)
        {
            int index = myElements.IndexOf(refElement);
            if (index >= 0 && index < myElements.Count - 1)
                return (ZYTextElement)myElements[index + 1];
            else
                return null;
        }

        protected int intUserLevel = 0;
        /// <summary>
        /// ��ǰ�û��ȼ�
        /// </summary>
        public int UserLevel
        {
            get { return intUserLevel; }
            set { intUserLevel = value; }
        }
        public bool IsLock(int index)
        {
            if (index >= 0)
            {
                for (int iCount = index; iCount < myElements.Count; iCount++)
                {
                    if (myElements[iCount] is ZYTextLock)
                    {
                        ZYTextLock Lock = (ZYTextLock)myElements[iCount];
                        if (Lock.Level >= intUserLevel)
                            return true;
                    }


                }

                #region bwy : �̶��ı�����ɾ
                if (myElements[index] is ZYFixedText || myElements[index] is ZYText)
                {
                    return true;
                }
                #endregion bwy :
            }
            return false;
        }

        public bool IsLock(ZYTextElement element)
        {
            //			if( element is ZYTextFlag )
            //				return ! myDocument.Info.DesignMode ;
            //			if( element is ZYTextLock )
            //				return ! myDocument.Info.DesignMode ;
            //			int index = myElements.IndexOf( element );
            //			if( index >= 0 )
            //			{
            //				return IsLock( index );
            //			}
            return false;
        }

        /// <summary>
        /// �ж�һ��Ԫ���Ƿ�ǰԪ��
        /// </summary>
        /// <param name="myElement"></param>
        /// <returns></returns>
        public bool isCurrentElement(ZYTextElement myElement)
        {
            return (this.CurrentElement == myElement && intSelectLength == 0);
        }

        /// <summary>
        /// ��ò���������е����е�Ԫ��
        /// </summary>
        /// <returns>Ԫ���б�</returns>
        public System.Collections.ArrayList GetCurrentLineElements()
        {
            intSelectStart = this.FixIndex(intSelectStart);
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            int LineIndex = myElement.LineIndex;
            // ��õ�ǰ�е�һ��Ԫ�صı��
            int StartIndex = 0;
            for (int iCount = intSelectStart - 1; iCount >= 0; iCount--)
            {
                myElement = (ZYTextElement)myElements[iCount];
                if (myElement.LineIndex != LineIndex)
                {
                    StartIndex = iCount + 1;
                    break;
                }
            }
            // ��䵱ǰ��Ԫ���б�
            System.Collections.ArrayList myList = new System.Collections.ArrayList();
            for (int iCount = StartIndex; iCount < myElements.Count; iCount++)
            {
                myElement = (ZYTextElement)myElements[iCount];
                if (myElement.LineIndex == LineIndex)
                {
                    myList.Add(myElement);
                }
                else
                    break;
            }
            return myList;
        }

        #endregion


        /// <summary>
        /// ���ص�ǰ�����׵Ŀհ��ַ���
        /// </summary>
        /// <returns>���׵Ŀհ��ַ���</returns>
        public string GetCurrentLineHeadBlank()
        {
            intSelectStart = this.FixIndex(intSelectStart);
            System.Collections.ArrayList myList = this.GetCurrentLineElements();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            for (int iCount = 0; iCount < myList.Count; iCount++)
            {
                ZYTextChar myChar = myList[iCount] as ZYTextChar;
                if (myChar == null)
                    break;
                else
                {
                    if (char.IsWhiteSpace(myChar.Char))
                        myStr.Append(myChar.Char);
                    else
                        break;
                }
            }
            return myStr.ToString();
        }

        #region ********************* ѡ������ *********************

        /// <summary>
        /// ѡ������ĳ���,��С��0
        /// </summary>
        public int SelectLength
        {
            get { return intSelectLength; }
            set { intSelectLength = value; }
        }
        /// <summary>
        /// ѡ������ľ��Կ�ʼλ��
        /// </summary>
        public int AbsSelectStart
        {
            get { return (intSelectLength > 0 ? intSelectStart : intSelectStart + intSelectLength); }
        }
        /// <summary>
        /// ѡ������ľ��Խ���λ��
        /// </summary>
        public int AbsSelectEnd
        {
            get
            {
                int intValue;
                if (intSelectLength >= 0)
                    intValue = intSelectStart + intSelectLength;//- 1;
                else
                    intValue = intSelectStart;// -1;

                if (intValue >= myElements.Count - 1)
                    intValue = myElements.Count - 1;
                return intValue;
            }
        }

        /// <summary>
        /// �ж�ָ����Ԫ���Ƿ���ѡ������
        /// </summary>
        /// <param name="myElement">�ĵ�Ԫ�ض���</param>
        /// <returns>�Ƿ���ѡ������</returns>
        public bool isSelected(ZYTextElement myElement)
        {
            if (intSelectLength == 0 || myElement == null)
                return false;
            else
            {
                if (myElement is TPTextCell)
                {
                    return (myElement as TPTextCell).Selected;
                }
                int index = myElement.Index;// myElements.IndexOf( myElement);
                if (intSelectLength > 0 && index >= intSelectStart && index < intSelectStart + intSelectLength)
                    return true;
                if (intSelectLength < 0 && index >= intSelectStart + intSelectLength && index < intSelectStart)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// �ж��Ƿ����ѡ�����Ŀ
        /// </summary>
        /// <returns></returns>
        public bool HasSelected()
        {
            return (intSelectLength != 0);
        }

        /// <summary>
        /// �ж��Ƿ�ѡ�����ı�
        /// </summary>
        /// <returns></returns>
        public bool HasSelectedText()
        {
            System.Collections.ArrayList myList = this.GetSelectElements();
            if (myList != null && myList.Count > 0)
            {
                foreach (ZYTextElement myElement in myList)
                {
                    if (myElement.isTextElement())
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ���ѡ�е��ı�
        /// </summary>
        /// <returns></returns>
        public string GetSelectedText()
        {
            System.Collections.ArrayList myList = this.GetSelectElements();
            if (myList != null && myList.Count > 0)
                return ZYTextElement.GetElementsText(myList);
            return null;
        }

        /// <summary>
        /// �ж��Ƿ�ѡ�����ı�
        /// </summary>
        /// <returns></returns>
        public bool HasSelectedChar()
        {
            System.Collections.ArrayList myList = this.GetSelectElements();
            if (myList != null && myList.Count > 0)
            {
                foreach (object obj in myList)
                {
                    if (obj is ZYTextChar)
                        return true;
                }
            }
            return false;
        }


        /// <summary>
        /// ������㾡���ƶ���ָ��λ��
        /// </summary>
        /// <param name="x">ָ��λ�õ�X����</param>
        /// <param name="y">ָ��λ�õ�Y����</param>
        public void MoveTo(int x, int y)
        {
            //intLastXPos = -1;
            if (myDocument != null)
            {
                ZYTextLine CurrentLine = null;

                //mfb 2009-7-14 ���˶�λ�еķ���
                foreach (ZYTextLine myLine in myDocument.Lines)
                {
                    Rectangle lineRec = new Rectangle(myLine.RealLeft, myLine.RealTop, myLine.ContentWidth, myLine.FullHeight);

                    if (lineRec.Contains(new Point(x, y)))
                    {
                        CurrentLine = myLine;
                        break;
                    }
                }
                // ��û���ҵ���ǰ�����������һ��Ϊ��ǰ��
                if (CurrentLine == null && myDocument.Lines.Count > 0)
                {
                    CurrentLine = (ZYTextLine)myDocument.Lines[myDocument.Lines.Count - 1];
                }

                // �������û���ҵ���ǰ����������ʧ�ܣ���������
                if (CurrentLine == null)
                    return;

                bool bolFlag = false;
                int index = 0;
                x -= CurrentLine.RealLeft;

                // ȷ����ǰԪ��,��ǰԪ���ǵ�ǰ�����ұ�Ե����ָ���ģ������Ԫ��
                ZYTextElement CurrentElement = null;
                //��ǰ����һ������,��һ���յ�PԪ��
                if (CurrentLine.Elements.Count == 0)
                {
                    CurrentElement = CurrentLine.Container;
                }
                foreach (ZYTextElement myElement in CurrentLine.Elements)
                {
                    if (WeiWenProcess.weiwen)
                    {
                        if (x > myElement.Left)
                        {
                            if (x < myElement.Left + myElement.Width / 2)
                                continue;
                            if (myElement.Parent.WholeElement)
                                return;
                            CurrentElement = myElement;
                            break;
                        }
                    }
                    else
                    {
                        if (x < myElement.Left + myElement.Width)
                        {
                            if (x > (myElement.Left + myElement.Width / 2))
                            {
                                continue;
                            }
                            if (myElement.Parent.WholeElement)
                                return;
                            CurrentElement = myElement;
                            break;
                        }

                    }
                }

                if (CurrentElement == null)
                {
                    // ��û�ҵ���ǰԪ�����ʾ��ǰλ�������ҳ�����ǰ�еķ�Χ
                    // ����ǰ���ѻ��з���β�����øû��з�Ϊ��ǰԪ��
                    // ��������Ϊ��ǰ�����һ��Ԫ�ص���һ��Ԫ��,��������β��־
                    CurrentElement = CurrentLine.LastElement;
                    if (CurrentLine.HasLineEnd)
                    {
                        index = myElements.IndexOf(CurrentLine.LastElement);
                    }
                    else
                    {
                        index = myElements.IndexOf(CurrentLine.LastElement) + 1;
                        bolFlag = true;
                    }
                }
                else
                {
                    //�ҵ��������ַ�
                    index = myElements.IndexOf(CurrentElement);
                    bolFlag = false;
                }
                // ������ǰԪ�����
                if (index > myElements.Count)
                {
                    index = myElements.Count - 1;
                    bolFlag = false;
                }
                if (index < 0)
                {
                    index = 0;
                    bolFlag = false;
                }
                this.MoveSelectStart(index);

                if (bolLineEndFlag != bolFlag)
                {
                    bolLineEndFlag = bolFlag;
                    ((ZYTextDocument)myDocument).UpdateTextCaret();
                }
            }
        }

        /// <summary>
        /// ����ѡ�������С
        /// </summary>
        /// <param name="NewSelectStart">�µ�ѡ������ʼ�����</param>
        /// <param name="NewSelectLength">��ѡ������ĳ���</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool SetSelection(int NewSelectStart, int NewSelectLength)
        {
            bolLineEndFlag = false;
            if (myElements == null || myElements.Count == 0)
            {
                return false;
            }

            int sourceIndex = 0; //ԭ��������λ��
            int sourceLength = 0; //ԭ����ѡ�񳤶�,δѡ����Ϊ0

            int newIndex = 0; //���µ�����λ��
            int newLength = 0; //���µ�ѡ�񳤶�

            int intTemp = 0;

            NewSelectStart = FixIndex(NewSelectStart);
            int iStep = (NewSelectStart > intSelectStart ? 1 : -1);

            bool bolZeroSelection = (NewSelectLength == 0);

            // ��ѡ������δ�����ı���ֱ���˳�����
            if (intSelectStart == NewSelectStart && intSelectLength == NewSelectLength)
                return true;
            int OldSelectStart = intSelectStart;

            //ZYTextElement OldElement = ( ZYTextElement  )myElements[OldSelectStart] ;

            // ���û��ѡ����Ԫ��,����˵δ���� "������" ѡ
            // ֻ��������һ�¶���,ûʲô��С�ֵ�.
            if (NewSelectLength == 0 && intSelectLength == 0)
            {
                //���汾�ε��������λ��
                intSelectStart = NewSelectStart;

                //���������Χ,��զ��զ��.�������Ƕ�λ����һ��Ԫ��,���Ƕ�λ�����һ��Ԫ��
                if (intSelectStart < 0)
                {
                    intSelectStart = 0;
                }
                if (intSelectStart >= myElements.Count)
                {
                    intSelectStart = myElements.Count - 1;
                }
                //��ȡ�������Ԫ��
                ZYTextElement NewElement = (ZYTextElement)myElements[intSelectStart];

                if (myDocument != null)
                {
                    myDocument.SelectionChanged(OldSelectStart, 0, intSelectStart, 0);
                }
                if (OldSelectStart >= 0 && OldSelectStart < myElements.Count)
                {
                    ((ZYTextElement)myElements[OldSelectStart]).HandleLeave();
                }
                ((ZYTextElement)myElements[intSelectStart]).HandleEnter();
                return true;
            }
            if (intSelectLength > 0)
            {
                sourceIndex = intSelectStart;
                sourceLength = intSelectStart + intSelectLength;
            }
            else //�ѵ�����С��0�����?
            {
                sourceIndex = intSelectStart + intSelectLength;
                sourceLength = intSelectStart;
            }
            if (NewSelectLength > 0)
            {
                newIndex = NewSelectStart;
                newLength = NewSelectStart + NewSelectLength;
            }
            else
            {
                newIndex = NewSelectStart + NewSelectLength;
                newLength = NewSelectStart;
            }
            if (sourceIndex > newIndex)
            {
                intTemp = sourceIndex;
                sourceIndex = newIndex;
                newIndex = intTemp;
            }
            if (sourceLength > newLength)
            {
                intTemp = sourceLength;
                sourceLength = newLength;
                newLength = intTemp;
            }
            if (newIndex > sourceLength)
            {
                intTemp = newIndex;
                newIndex = sourceLength;
                sourceLength = intTemp;
            }
            intSelectStart = NewSelectStart;
            intSelectLength = NewSelectLength;


            FixSelection();


            sourceIndex = FixIndex(sourceIndex);
            sourceLength = FixIndex(sourceLength);
            newIndex = FixIndex(newIndex);
            newLength = FixIndex(newLength);
            if (sourceIndex != newIndex)
            {
                for (int iCount = sourceIndex; iCount <= newIndex; iCount++)
                    if (((ZYTextElement)myElements[iCount]).HandleSelectedChange())
                        return false;
            }
            if (sourceLength != newLength)
            {
                for (int iCount = sourceLength; iCount <= newLength; iCount++)
                    if (((ZYTextElement)myElements[iCount]).HandleSelectedChange())
                        return false;
            }

            if (myDocument != null)
            {
                myDocument.SelectionChanged(sourceIndex, sourceLength, newIndex, newLength);
            }
            if (OldSelectStart >= 0 && OldSelectStart < myElements.Count)
            {
                ((ZYTextElement)myElements[OldSelectStart]).HandleLeave();
            }
            ((ZYTextElement)myElements[intSelectStart]).HandleEnter();
            return true;
        }

        /// <summary>
        /// ����Ԫ������Ա�֤��Ҫ��Ԫ���б�ķ�Χ��
        /// </summary>
        /// <param name="index">ԭʼ�����</param>
        /// <returns>����������</returns>
        private int FixIndex(int index)
        {
            if (index <= 0)
            {
                return 0;
            }
            if (index >= myElements.Count)
            {
                return myElements.Count - 1;
            }
            return index;
        }

        /// <summary>
        /// ѡ�����е�Ԫ��
        /// </summary>
        public void SelectAll()
        {
            this.SetSelection(0, myElements.Count);
        }

        #endregion

        /// <summary>
        /// �ƶ���ǰ������λ��
        /// </summary>
        /// <param name="index">�������µ�λ��</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool MoveSelectStart(int index)
        {
            index = this.FixIndex(index);
            int length = bolAutoClearSelection ? 0 : intSelectLength + intSelectStart - index;
            //Debug.WriteLine("zycontent MoveSelectStart ����ѡ��Χ " + index + "-" + length);
            return SetSelection(index, length);
        }

        /// <summary>
        /// ��������ƶ���ָ��Ԫ��ǰ��
        /// </summary>
        /// <param name="refElement">ָ����Ԫ��</param>
        /// <returns>�����Ƿ�ɹ�</returns>
        public bool MoveSelectStart(ZYTextElement refElement)
        {
            if (myElements.IndexOf(refElement) >= 0)
            {
                return MoveSelectStart(myElements.IndexOf(refElement));
            }
            return false;
        }

        /// <summary>
        /// �������ѡ�������Ԫ��
        /// </summary>
        /// <returns>��������ѡ�������Ԫ�ص��б�</returns>
        public System.Collections.ArrayList GetSelectElements()
        {
            if (myElements == null)
                return null;
            else
            {
                System.Collections.ArrayList myList = new System.Collections.ArrayList();
                int intEnd = this.AbsSelectEnd;
                for (int iCount = this.AbsSelectStart; iCount < intEnd; iCount++)
                    myList.Add(myElements[iCount]);
                return myList;
            }
        }

        /// <summary>
        /// �������ѡ������Ķ���Ԫ��mfb
        /// </summary>
        /// <returns>����ѡ�����������ж���Ԫ�ص��б�,������ʧ���򷵻ؿ�����</returns>
        public System.Collections.ArrayList GetSelectParagraph()
        {
            if (myElements == null)
                return null;
            else
            {
                System.Collections.ArrayList myList = new System.Collections.ArrayList();
                int intEnd = this.AbsSelectEnd;
                for (int iCount = this.AbsSelectStart; iCount <= intEnd; iCount++)
                {
                    if ((myElements[iCount] as ZYTextElement).Parent is ZYTextParagraph)
                    {
                        if (!myList.Contains((myElements[iCount] as ZYTextElement).Parent))
                        {
                            myList.Add((myElements[iCount] as ZYTextElement).Parent);
                        }
                    }
                }
                return myList;
            }
        }

        /// <summary>
        /// ����������֮�������Ԫ��
        /// </summary>
        /// <param name="Index1">���1</param>
        /// <param name="Index2">���2</param>
        /// <returns>���������ָ�����������֮�������Ԫ�ص��б����</returns>
        public System.Collections.ArrayList GetElementsRange(int Index1, int Index2)
        {
            if (myElements == null)
                return null;
            else
            {
                System.Collections.ArrayList myList = new System.Collections.ArrayList();
                int Temp = 0;
                if (Index1 > Index2)
                {
                    Temp = Index1;
                    Index1 = Index2;
                    Index2 = Temp;
                }
                Index1 = this.FixIndex(Index1);
                Index2 = this.FixIndex(Index2);

                for (int iCount = Index1; iCount < Index2; iCount++)
                    myList.Add(myElements[iCount]);
                return myList;
            }
        }

        /// <summary>
        /// ����ǰѡ�е�һ��Ԫ���򷵻ص�ǰѡ���Ԫ��,���򷵻ؿ�����
        /// </summary>
        /// <returns></returns>
        public ZYTextElement GetSelectElement()
        {
            if (intSelectLength == 1)
                return (ZYTextElement)myElements[intSelectStart];
            if (intSelectLength == -1)
                return (ZYTextElement)myElements[intSelectStart - 1];
            return null;
        }

        /// <summary>
        /// ���ѡ��������ı�����
        /// </summary>
        /// <returns></returns>
        public string GetSelectText()
        {
            return ZYTextElement.GetElementsText(this.GetSelectElements());
        }

        /// <summary>
        /// ʹ��ָ���ı��滻ѡ������
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public bool ReplaceSelection(string strText)
        {
            if (strText == null || strText.Length == 0)
                return false;
            this.DeleteSeleciton();
            this.InsertString(strText);
            bolModified = true;
            return true;
        }

        /// <summary>
        /// ����һ��Ԫ��
        /// </summary>
        /// <param name="myList"></param>
        public void InsertRangeElements(System.Collections.ArrayList myList)
        {
            if (myElements == null || myElements.Count == 0 || myList == null || myList.Count == 0)
                return;
            if (IsLock(intSelectStart))
                return;

            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            ZYTextContainer myParent = myElement.Parent;
            //������ZYTextBlock������Ԫ��
            if (myParent is ZYTextBlock)
            {
                myElement = myParent;
                myParent = myParent.Parent;
            }

            // ��ǰԪ�����ڵĸ���������������ַ�Ԫ��
            #region bwy
            //Ҫ�޸Ĵ˴����������EOFҪ�����µĶ��䣬����δ��
            ArrayList templist = new ArrayList();
            foreach (object o in myList)
            {
                if (o is ZYTextEOF)
                {
                    myParent.InsertRangeBefore(templist, myElement);
                    myParent.RefreshLine();

                    templist.Clear();
                    this.Document._InsertChar('\r');

                    myElement = (ZYTextElement)myElements[intSelectStart];
                    myParent = myElement.Parent;
                    //������ZYTextBlock������Ԫ��
                    if (myParent is ZYTextBlock)
                    {
                        myElement = myParent;
                        myParent = myParent.Parent;
                    }
                }
                else
                {
                    templist.Add(o);
                }
            }
            myParent.InsertRangeBefore(templist, myElement);
            myParent.RefreshLine();

            #endregion bwy
            //myParent.InsertRangeBefore(myList, myElement);//ԭ
            //myParent.RefreshLine();
            bolModified = true;
            // �ƶ���ǰ����㵽�������ַ�����ĩβ
            if (myDocument != null) myDocument.ContentChanged();
            this.AutoClearSelection = true;

            #region bwy : ������һ���Ϊ����
            this.MoveSelectStart(myElement);
            #endregion bwy :
        }

        /// <summary>
        /// �ڵ�ǰλ�ò���һ���ַ���
        /// </summary>
        /// <param name="strText">�ַ���</param>
        public void InsertString(string strText)
        {
            if (myElements == null || myElements.Count == 0) return;
            if (IsLock(intSelectStart))
                return;

            this.Document.BeginUpdate();
            this.Document.BeginContentChangeLog();

            ZYTextChar NewChar = null;
            ZYTextChar defChar = null;
            defChar = GetPreChar();

            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            ZYTextContainer myParent = myElement.Parent;

            //Add by wwj 2013-01-23
            //����ڽṹ��Ԫ��ǰճ�����ݵ�ʱ�򣬱�ճ����������ṹ��Ԫ�ص������ں�Ϊһ���Bug
            //�磺{a,b,{d,e,f}} -------���� a��bΪ�����ı��� d��e��f�ǽṹ��Ԫ�ص�����
            //���ʱ����b��d���м���������ı�c���������Ч��Ӧ���� {a,b,c,{d,e,f}} ������ {a,b,{c,d,e,f}}
            //ע�⣺ȷ����ǰԪ�����丸Ԫ���µĵ�һ����Ԫ��ʱ�Ž�����߼�
            if (myParent != null && !(myParent is ZYTextParagraph) && myParent.Parent is ZYTextParagraph && myParent.ChildElements.IndexOf(myElement) == 0)
            {
                myElement = myParent;
                myParent = myParent.Parent;
            }

            // �����ַ�������һϵ���ַ�Ԫ�ض���
            System.Collections.ArrayList myList = new System.Collections.ArrayList();


            for (int iCount = 0; iCount < strText.Length; iCount++)
            {
                if (strText[iCount] == '\n')
                {
                    myList.Add(new ZYTextLineEnd());
                }
                else if (strText[iCount] != '\r')
                {
                    char myPreChar, myFontChar;
                    if (iCount == 0)
                        myPreChar = ' ';
                    else
                        myPreChar = strText[iCount - 1];

                    if (iCount == strText.Length - 1)
                        myFontChar = ' ';
                    else
                        myFontChar = strText[iCount + 1];

                    NewChar = myElement.OwnerDocument.CreateChar(WeiWenProcess.strPase(strText[iCount], myPreChar, myFontChar));
                    if (defChar != null)
                        defChar.Attributes.CopyTo(NewChar.Attributes);

                    //�������һ���նδ�����ô��Ĭ�������СΪ׼ add by wwj 2012-05-29
                    if (myElement is ZYTextEOF && myElement == myParent.FirstElement)
                    {
                        NewChar.Attributes.SetValue(ZYTextConst.c_FontSize, myElement.OwnerDocument.OwnerControl.GetDefaultFontSize());
                    }
                    myList.Add(NewChar);
                }
            }

            //Add by wwj 2013-02-01 �Ƴ�ArrayListĩβ��ZYTextLineEndԪ��,��ֹ����Ӳ�س�����س����ӵ�����
            RemoveTheEndOFZYTextLineEnd(myList);

            // ��ǰԪ�����ڵĸ���������������ַ�Ԫ��
            myParent.InsertRangeBefore(myList, myElement);
            myParent.RefreshLine();
            bolModified = true;

            this.SelectLength = 0;
            this.Document.EndContentChangeLog();
            this.Document.EndUpdate();

            // �ƶ���ǰ����㵽�������ַ�����ĩβ
            if (myDocument != null) myDocument.ContentChanged();
            this.AutoClearSelection = true;
            this.MoveSelectStart(intSelectStart + myList.Count);//Modify by wwj 2013-02-01 �ƶ��������ȷ��λ��


        }

        /// <summary>
        /// �Ƴ�ArrayListĩβ��ZYTextLineEndԪ�� Add by wwj 2013-02-01
        /// </summary>
        /// <param name="list"></param>
        void RemoveTheEndOFZYTextLineEnd(ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[list.Count - 1] is ZYTextLineEnd)
                {
                    list.RemoveAt(list.Count - 1);
                }
                else
                {
                    return;
                }
            }
        }


        /// <summary>
        /// �ڵ�ǰλ�ò���һ���ַ���
        /// </summary>
        /// <param name="vChar">�ַ�</param>
        /// <returns>�������ַ�����</returns>
        public ZYTextChar InsertChar(char vChar)
        {
            if (myElements == null || myElements.Count == 0) return null;
            if (IsLock(intSelectStart))
            {
                return null;
            }

            if (intSelectStart < 0) intSelectStart = 0;
            if (intSelectStart >= myElements.Count) intSelectStart = myElements.Count - 1;

            ZYTextChar NewChar = null;
            // ������ͼ�ҵ���ǰ�����һ���ַ����͵�����
            ZYTextChar defChar = GetPreChar();
            ZYTextElement myElement = this.CurrentElement;
            ZYTextContainer myParent = myElement.Parent;
            ZYTextContainer grandParent = myElement.Parent.Parent;

            bool bolSetParent = false;
            if ((myParent is ZYTextBlock && myElement == myParent.GetFirstElement()) || myParent.WholeElement)
            {
                bolSetParent = true;
            }
            if (myElement.OwnerDocument.ContentChangeLog != null)
            {
                myElement.OwnerDocument.ContentChangeLog.CanLog = false;
            }
            NewChar = myElement.OwnerDocument.CreateChar(vChar);

            if (defChar != null)
            {
                defChar.Attributes.CopyTo(NewChar.Attributes);
                //���̳����±�
                NewChar.Attributes.SetValue(ZYTextConst.c_Sup, false);
                NewChar.Attributes.SetValue(ZYTextConst.c_Sub, false);
            }
            NewChar.CreatorIndex = this.Document.SaveLogs.CurrentIndex;
            #region bwy �������һ���նδ����������˻س����������С����ôӦ�������Ϊ׼ ����
            //**************************Modified by wwj 2012-05-29**********************************
            //if (myElement is ZYTextEOF && myElement == myParent.FirstElement && (myElement as ZYTextEOF).FontSize != 0)
            //{
            //    NewChar.Attributes.SetValue(ZYTextConst.c_FontSize, (myElement as ZYTextEOF).FontSize);
            //}
            //**************************************************************************************

            //NewChar.UpdateAttrubute();
            #endregion


            #region �������һ���նδ�����ô��Ĭ�������СΪ׼
            if (myElement is ZYTextEOF && myElement == myParent.FirstElement)
            {
                NewChar.Attributes.SetValue(ZYTextConst.c_FontSize, myElement.OwnerDocument.OwnerControl.GetDefaultFontSize());
            }
            #endregion
            if (myElement.OwnerDocument.ContentChangeLog != null)
            {
                myElement.OwnerDocument.ContentChangeLog.CanLog = true;
            }

            if (bolSetParent)
            {
                grandParent.InsertBefore(NewChar, myParent);
            }
            else
            {
                myParent.InsertBefore(NewChar, myElement);
            }

            bolModified = true;

            if (myDocument != null)
            {
                myDocument.ContentChanged();
            }

            this.AutoClearSelection = true;
            // �ƶ���ǰ����㵽�������ַ�����ĩβ update by Ukey zhang 2017-11-05ά�Ĺ�겻�øı�
            if (WeiWenProcess.weiwen)
                this.MoveSelectStart(intSelectStart + 1);
            else
            {
                this.MoveSelectStart(intSelectStart + 1);
            }


            return NewChar;
        }

        /// <summary>
        /// �ڵ�ǰλ�ò���һ��Ԫ��
        /// </summary>
        /// <param name="NewElement"></param>
        public void InsertElement(ZYTextElement NewElement)
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            ZYTextContainer myParent = myElement.Parent;

            #region bwy:
            //������ZYTextBlock������Ԫ��

            if (myParent is ZYTextBlock)
            {
                myElement = myParent;
                myParent = myParent.Parent;
            }
            #endregion bwy:


            if (myParent.InsertBefore(NewElement, myElement))
            {
                bolModified = true;
                if (myDocument != null)
                {
                    myDocument.ContentChanged();
                }
                this.AutoClearSelection = true;
                this.MoveSelectStart(intSelectStart + 1);
            }
        }

        /// <summary>
        /// �س�ʱ����һ������
        /// </summary>
        /// <param name="NewElement">һ���µĿն���Ԫ��</param>
        public void InsertParagraph(ZYTextElement NewElement)
        {
            if (myElements == null || myElements.Count == 0)
            {
                return;
            }
            bool isContentChange = false;

            //��ǰԪ��(����Ϊһ��eof��)
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            if (myElement.Parent is ZYTextBlock)
            {
                myElement = myElement.Parent;
            }
            //��ǰԪ�������Ķ������

            ZYTextElement parent = myElement.Parent;
            while (!(parent is ZYTextParagraph))
            {
                parent = parent.Parent;
            }
            ZYTextParagraph Paraparent = parent as ZYTextParagraph;//myElement.Parent as ZYTextParagraph;

            //��ǰԪ������������������
            //ZYTextContainer divParent = myElement.Parent.Parent;
            ZYTextContainer divParent = Paraparent.Parent;

            if (myElement == Paraparent.LastElement)//��ǰԪ��Ϊ���������һ��Ԫ��
            {
                divParent.InsertAfter(NewElement, Paraparent);
                myElement = (NewElement as ZYTextParagraph).FirstElement;
                isContentChange = true;
            }
            else if (myElement == Paraparent.FirstElement)//��ǰԪ��Ϊ�����е�һ��Ԫ��
            {
                divParent.InsertBefore(NewElement, Paraparent);
                myElement = (Paraparent as ZYTextParagraph).FirstElement;
                isContentChange = true;
            }
            else//��ǰԪ��Ϊ�����е�Ԫ��
            {
                int currentIndex = Paraparent.IndexOf(myElement);

                #region bwy
                ArrayList myList = new ArrayList();
                myList = Paraparent.ChildElements.GetRange(currentIndex, Paraparent.ChildElements.Count - currentIndex - 1);
                //copy ��ǰ��������Ԫ��
                System.Xml.XmlDocument myDoc = new System.Xml.XmlDocument();
                myDoc.PreserveWhitespace = true;
                myDoc.AppendChild(myDoc.CreateElement(ZYTextConst.c_ClipboardDataFormat));
                ZYTextElement.ElementsToXML(myList, myDoc.DocumentElement);

                //ɾ�������е�ǰԪ�غ��Ԫ��

                Paraparent.RemoveChildRange(myList);

                ZYTextParagraph secondPara = new ZYTextParagraph();
                divParent.InsertAfter(secondPara, Paraparent);

                //��ԭcopy�����뵽��һ��,past
                ArrayList newList = new ArrayList();

                this.Document.LoadElementsToList(myDoc.DocumentElement, newList);

                if (newList.Count > 0)
                {
                    foreach (ZYTextElement ele in newList)
                    {
                        ele.RefreshSize();
                    }
                    //this.InsertRangeElements(myList);
                    secondPara.InsertRangeBefore(newList, secondPara.LastElement);
                    myElement = newList[0] as ZYTextElement;

                }

                #endregion bwy

            }
            isContentChange = true;
            if (isContentChange)
            {
                bolModified = true;
                if (myDocument != null)
                {
                    myDocument.ContentChanged();
                }
            }
            this.AutoClearSelection = true;

            if (myElement is ZYTextBlock)
            {
                myElement = (myElement as ZYTextBlock).FirstElement;
            }

            //this.MoveSelectStart(intSelectStart + 1);
            this.MoveSelectStart(myElement);
        }

        #region �|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�| �����ط��� �|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|

        /// <summary>
        /// �ڵ�ǰ����㴦����һ�����
        /// </summary>
        /// <param name="NewElement">��ʾһ�����Ԫ��</param>
        public void InsertTable(ZYTextElement NewElement)
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];

            //��õ�ǰԪ�����ڵĶ���
            ZYTextContainer secondaryParent = GetParentByElement(myElement, ZYTextConst.c_P) as ZYTextContainer;

            ZYTextContainer rootParent = GetParentByElement(myElement, ZYTextConst.c_Div) as ZYTextDiv;

            //��Ԫ�������Ķ�������һ�����.
            rootParent.InsertAfter(NewElement, secondaryParent);
            //Ȼ���ٱ����ٲ���һ���µĶ���Ԫ��.
            rootParent.InsertAfter(secondaryParent, new ZYTextParagraph());

            bolModified = true;
            if (myDocument != null)
            {
                myDocument.ContentChanged();
            }
            this.AutoClearSelection = true;
        }

        /// <summary>
        /// mfb �ڲ���㴦����������
        /// </summary>
        /// <param name="RowNum">Ҫ���������</param>
        /// <param name="IsAfter">�Ƿ��ڲ��������</param>
        public void InsertRows(int RowNum, bool IsAfter)
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;
            //��õ�ǰԪ��
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];

            //��õ�ǰ���
            TPTextTable currentTable = GetParentByElement(myElement, ZYTextConst.c_Table) as TPTextTable;
            if (currentTable != null)
            {
                //��õ�ǰ��
                TPTextRow currentRow = GetParentByElement(myElement, ZYTextConst.c_Row) as TPTextRow;


                //��õ�ǰ�����ڱ�������
                int rowIndex = currentTable.IndexOf(currentRow);

                //����ڵ�ǰ��ǰ����
                for (int k = 0; k < RowNum; k++)
                {
                    if (!IsAfter)
                    {
                        currentTable.InsertRow(rowIndex, currentRow);
                    }
                    else
                    {
                        //�����һ��֮�����
                        if (rowIndex == currentTable.AllRows.Count - 1)
                        {
                            currentTable.AddRow(currentRow);
                        }
                        else
                        {
                            currentTable.InsertRow(rowIndex + 1, currentRow);
                        }
                    }
                }
                //�������ñ�������е�Ԫ��ı߿���
                currentTable.SetEveryCellBorderWidth();
            }
            bolModified = true;
            if (myDocument != null)
            {
                myDocument.RefreshSize();
                myDocument.ContentChanged();
            }
            this.AutoClearSelection = true;
        }

        /// <summary>
        /// mfb �ڲ���㴦�������ɸ���
        /// </summary>
        /// <param name="columnNum">Ҫ���������</param>
        /// <param name="IsAfter">�Ƿ��ڲ����֮�����</param>
        public void InsertColumns(int columnNum, bool IsAfter)
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;

            //��õ�ǰԪ��
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];

            //��õ�ǰ���
            TPTextTable currentTable = GetParentByElement(myElement, ZYTextConst.c_Table) as TPTextTable;
            if (currentTable != null)
            {
                //��õ�ǰ��
                TPTextRow currentRow = GetParentByElement(myElement, ZYTextConst.c_Row) as TPTextRow;

                //��õ�ǰ��Ԫ��
                TPTextCell currentCell = GetParentByElement(myElement, ZYTextConst.c_Cell) as TPTextCell;

                //��õ�ǰ�����ڱ�������
                int rowIndex = currentTable.IndexOf(currentRow);

                //��õ�ǰ��Ԫ�������е�����
                int cellIndex = currentRow.IndexOf(currentCell);

                for (int k = 0; k < columnNum; k++)
                {
                    //����ڵ�ǰ��ǰ����
                    if (!IsAfter)
                    {
                        currentTable.InsertColumns(cellIndex, currentCell);
                    }
                    else
                    {
                        //��������һ����Ԫ��
                        if (cellIndex == currentRow.Cells.Count - 1)
                        {
                            currentTable.AddColumns(currentCell);
                        }
                        else
                        {
                            currentTable.InsertColumns(cellIndex + 1, currentCell);
                        }
                    }
                }
                //�������ñ�������е�Ԫ��ı߿���
                currentTable.SetEveryCellBorderWidth();
            }
            bolModified = true;
            if (myDocument != null)
            {
                myDocument.RefreshSize();
                myDocument.ContentChanged();
            }
            this.AutoClearSelection = true;

        }

        /// <summary>
        /// mfb ɾ��ѡ��Ԫ�����ڵı��
        /// </summary>
        public void DeleteTable()
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;
            //�������ѡ���˶�������,���ж���Щ�����ǲ��Ƕ��ڱ���ڵ�.
            //�������ɾ�����,��������򲻽��ж���
            if (HasSelected())
            {
                int StartIndex = this.AbsSelectStart;
                int EndIndex = this.AbsSelectEnd;
                int iLen = (intSelectLength > 0 ? intSelectLength : 0 - intSelectLength);

                System.Collections.ArrayList myList = this.GetSelectElements();

                //�Ƿ�����ѡ���Ԫ�ض���table��
                bool isParentTable = true;

                foreach (ZYTextElement ele in myList)
                {
                    if (GetParentByElement(ele, ZYTextConst.c_Table) == null)
                    {
                        isParentTable = false;
                        break;
                    }
                }
                if (isParentTable)
                {
                    //���ѡ��Ԫ���б�ĵ�һ��Ԫ��
                    ZYTextElement myElement = (ZYTextElement)myElements[StartIndex];
                    ZYTextElement currentTable = GetParentByElement(myElement, ZYTextConst.c_Table);

                    ZYTextContainer bodyElement = GetRootElement(myElement) as ZYTextContainer;

                    bodyElement.RemoveChild(currentTable);
                }
            }
            else
            {
                //��õ�ǰԪ��
                ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];

                //��õ�ǰ���
                TPTextTable currentTable = GetParentByElement(myElement, ZYTextConst.c_Table) as TPTextTable;

                if (currentTable != null)
                {
                    ZYTextContainer bodyElement = GetRootElement(myElement) as ZYTextContainer;
                    bodyElement.RemoveChild(currentTable);
                }
            }

            bolModified = true;
            if (myDocument != null)
            {
                myDocument.RefreshSize();
                myDocument.ContentChanged();
            }
            this.AutoClearSelection = true;
        }

        /// <summary>
        /// mfb ɾ��ѡ��Ԫ�����ڵ���
        /// </summary>
        public void DeleteRows()
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;

            //��õ�ǰԪ��
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            //��õ�ǰ���
            TPTextTable currentTable = GetParentByElement(myElement, ZYTextConst.c_Table) as TPTextTable;
            if (currentTable != null)
            {
                //��������ѡ���Ԫ�����������б�
                ArrayList rowList = new ArrayList();
                foreach (TPTextRow row in currentTable)
                {
                    foreach (TPTextCell cell in row)
                    {
                        if (true == cell.CanAccess)
                        {
                            rowList.Add(row);
                            break;
                        }
                    }
                }

                //��������ѡ�����,��ɾ��֮
                foreach (TPTextRow rowElement in rowList)
                {
                    currentTable.AllRows.Remove(rowElement);
                    currentTable.RemoveChild(rowElement);
                }

                currentTable.SetEveryCellBorderWidth();
            }

            bolModified = true;
            if (myDocument != null)
            {
                myDocument.RefreshSize();
                myDocument.ContentChanged();
            }
            this.AutoClearSelection = true;
        }

        /// <summary>
        /// mfb ɾ��ѡ��Ԫ�����ڵ���
        /// </summary>
        public void DeleteColumns()
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;

            //��õ�ǰԪ��
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            //��õ�ǰ���
            TPTextTable currentTable = GetParentByElement(myElement, ZYTextConst.c_Table) as TPTextTable;
            if (currentTable != null)
            {
                bool isFind = false;
                List<int> needDelCol = new List<int>();
                foreach (TPTextRow row in currentTable)
                {
                    foreach (TPTextCell cell in row)
                    {
                        if (true == cell.CanAccess)
                        {
                            isFind = true;
                            needDelCol.Add(row.IndexOf(cell));
                        }
                    }
                    if (isFind)
                    {
                        goto DeleteColumn;
                    }
                }
            DeleteColumn:
                foreach (int column in needDelCol)
                {
                    //TODO: ���ﻹ��Ҫ����һ��.
                    //��ΪҪɾ�����ж��ǽ����ŵ�,ɾ�����һ����. ������е�����
                    //��֮Ҳ��仯(�Զ���1),����ʼ���õ�һ���к�Ӧ������ȷ��.
                    //������Ȼ��ʱ�ﵽ,���Ǹо����ǻ���,�߼����Ͻ�.
                    currentTable.DeleteColumn(needDelCol[0]);
                }
                currentTable.SetEveryCellBorderWidth();
            }
            bolModified = true;
            if (myDocument != null)
            {
                myDocument.RefreshSize();
                myDocument.ContentChanged();
            }
            this.AutoClearSelection = true;
        }

        /// <summary>
        /// mfb �жϵ�ǰԪ��(������)�Ƿ���һ��cell��
        /// </summary>
        /// <returns></returns>
        public bool IsParaInCell(ZYTextElement CurrentElement)
        {
            if (CurrentElement.GetXMLName() == ZYTextConst.c_Div)
            {
                return false;
            }
            if (CurrentElement.Parent.GetXMLName() == ZYTextConst.c_Cell)
            {
                return true;
            }
            return IsParaInCell(CurrentElement.Parent);
        }

        /// <summary>
        /// mfb ��ȡ��ǰԪ�������ĸ���Ԫ��
        /// </summary>
        /// <param name="currentElement">��ǰԪ��</param>
        /// <param name="findName">Ҫ�ҵĸ���Ԫ�ص�xmlName</param>
        /// <returns></returns>
        public ZYTextElement GetParentByElement(ZYTextElement currentElement, string findName)
        {
            if (currentElement == null)
            {
                return null;
            }
            if (currentElement.GetXMLName() == findName)
            {
                return currentElement;
            }
            return GetParentByElement(currentElement.Parent, findName);
        }


        #endregion �|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�| end �����ز��뷽�� end �|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|�|


        /// <summary>
        ///  mfb ���Ҷ���bodyԪ��
        /// </summary>
        /// <param name="CurrentElement">The current element.</param>
        /// <returns></returns>
        public ZYTextElement GetRootElement(ZYTextElement CurrentElement)
        {
            if (CurrentElement.Parent != null)
            {
                return GetRootElement(CurrentElement.Parent);
            }
            return CurrentElement;
        }


        /// <summary>
        /// mfb ��õ�ǰԪ�����������������Ԫ��
        /// </summary>
        /// <param name="CurrentElement">The current element.</param>
        /// <returns></returns>
        public ZYTextElement GetSecondaryElement(ZYTextElement CurrentElement)
        {
            if (CurrentElement.Parent.Parent.GetXMLName() != "div")
            {
                return GetRootElement(CurrentElement.Parent);
            }
            return CurrentElement.Parent;
        }



        /// <summary>
        /// ����ǩ��
        /// </summary>
        /// <param name="NewElement"></param>
        public void InsertLock(ZYTextElement NewElement)
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            ZYTextContainer myParent = myElement.Parent;
            if (this.CurrentLine.Elements.Count > 1)
            {
                ZYTextParagraph myP = new ZYTextParagraph();
                myP.OwnerDocument = this.myDocument;
                this.InsertElement(myP);
            }
            //int i = 0;
            this.InsertString("                             ");
            if (myParent.InsertBefore(NewElement, myElement))
            {
                //myParent.RefreshLineFast( myParent.IndexOf( NewElement)); 
                bolModified = true;
                if (myDocument != null)
                {
                    myDocument.ContentChanged();
                }
                this.AutoClearSelection = true;
                //this.myDocument.SetAlign(ParagraphAlignConst.Right);

                this.MoveSelectStart(intSelectStart + 1);
            }
        }

        /// <summary>
        /// ɾ��ѡ�������Ԫ��
        /// </summary>
        /// <param name="flag">ɾ����ʶ��true ȫ��ɾ����false �̶��ı���ɾ��</param>
        public void DeleteSeleciton(bool flag)
        {
            this.Document.DeleteFlag = flag;
            DeleteSeleciton();
            this.Document.DeleteFlag = false;

        }
        /// <summary>
        /// ɾ��ѡ�������Ԫ��  
        /// </summary>
        public bool DeleteSeleciton()//Modify by wwj 2013-02-01 ���ӷ���ֵ
        {
            //һ��Ҫ����Undo/redo �ļ�����������
            if (myElements == null || myElements.Count == 0) return false;
            if (IsLock(intSelectStart))
                return false;

            ArrayList alp = this.GetSelectParagraph();

            //���ԵĿ�ʼ�����λ�ã�һ����ǰС���

            int StartIndex = this.AbsSelectStart;
            int EndIndex = this.AbsSelectEnd;

            int iLen = (intSelectLength > 0 ? intSelectLength : 0 - intSelectLength);
            bool bolChanged = false;

            ///ѡ����ַ��б�
            System.Collections.ArrayList mySelList = this.GetSelectElements();
            //ɾ���б�
            System.Collections.ArrayList myRemoveList = new System.Collections.ArrayList();
            //ɾ��������Ԫ���б�
            System.Collections.ArrayList myRealRemoveList = new System.Collections.ArrayList();

            myRealRemoveList = this.Document.GetRealElements(mySelList);

            int intDelete = 0;
            foreach (ZYTextElement ele in myRealRemoveList)
            {
                intDelete = myDocument.isDeleteElement(ele);
            }
            if (intDelete == 0)
            {
                //myRealRemoveList = this.Document.GetRealElements(mySelList);
                if (!this.Document.DeleteFlag)
                {
                    if (this.Document.Info.DocumentModel != DocumentModel.Design)
                    {
                        foreach (ZYTextElement ele in mySelList)
                        {
                            if (ele.Parent is ZYFixedText || ele.Parent is ZYText)
                            {
                                MessageBox.Show("ѡ��Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                            //Add By wwj 2012-03-30 �ж϶�ZYButton��ɾ������
                            if (ele is ZYButton && !((ZYButton)ele).CanDelete)
                            {
                                MessageBox.Show("ѡ��Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                            //Add By wwj 2013-08-01 �ж϶�ZYFlag��ɾ������
                            if (ele is ZYFlag && !((ZYFlag)ele).CanDelete)
                            {
                                MessageBox.Show("ѡ��Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                    }
                }

                #region ��ȡ��ǰ��ѡ�еı��,��ѡ�б��������Ǵӱ��ͷѡ�����β Add by wwj 2013-01-21
                Dictionary<ZYTextElement, TPTextTable> selectionTableDict = new Dictionary<ZYTextElement, TPTextTable>();
                foreach (ZYTextElement ele in mySelList)
                {
                    ZYTextElement element = GetParentByElement(ele, ZYTextConst.c_Table);
                    if (element != null)
                    {
                        TPTextTable tb = element as TPTextTable;
                        if (tb != null && !selectionTableDict.ContainsValue(tb))
                        {
                            ZYTextElement lastElement = tb.GetLastElement();
                            if (mySelList.Contains(lastElement))
                            {
                                selectionTableDict.Add(ele, tb);
                            }
                        }
                    }
                }
                #endregion

                for (int i = 0; i < alp.Count; i++)
                {
                    ZYTextParagraph p = alp[i] as ZYTextParagraph;
                    int pIndexStart = p.FirstElement.Index;

                    if (p.FirstElement is ZYTextBlock)
                    {
                        pIndexStart = (p.FirstElement as ZYTextBlock).FirstElement.Index;
                    }

                    //p.LastElement ��ԶӦ�� EOF 
                    int pIndexEnd = p.LastElement.Index;

                    if (StartIndex <= pIndexStart && pIndexEnd <= EndIndex)
                    {
                        //���Ҫɾ���Ķ����ڵ�Ԫ���ڣ�����ɾ�����еĶ���,ÿ����Ԫ�������ٱ���һ���س����������ٱ���һ������ Add By wwj 2012-04-24 �����Ԫ������ɾ����Bug
                        ZYTextElement textElement = this.GetParentByElement(p, ZYTextConst.c_Cell);
                        if (textElement != null)
                        {
                            if (((ZYTextContainer)textElement).ChildCount > 1)
                            {
                                //����ɾ��
                                p.Parent.RemoveChild(p);
                                p.Parent.RefreshLine();
                                StartIndex += p.ChildCount;
                            }
                            else
                            {
                                //ɾ��ѡ�񲿷�Ԫ��
                                myRemoveList = Elements.GetRange(StartIndex, EndIndex - StartIndex);
                                //ת��Ϊ����Ԫ��
                                myRealRemoveList = this.Document.GetRealElements(myRemoveList);
                                p.RemoveChildRange(myRealRemoveList);
                                p.RefreshLine();
                            }
                        }
                        else
                        {
                            //Add by wwj 2013-02-01 ���Ӷ���ɾ��ǰ���ж�
                            //������ճ������֮ǰ�����ѡ�е����ݣ������ǰΪ��������ɾ������
                            //���Ӵ��ж�Ϊ�˷�ֹ������ճ�����ݺ���ֳ��е����
                            if (StartIndex != EndIndex)
                            {
                                //����ɾ��
                                p.Parent.RemoveChild(p);
                                p.Parent.RefreshLine();
                            }
                        }
                    }
                    //ѡ���ڶ��м�
                    else if (pIndexStart <= StartIndex && EndIndex <= pIndexEnd)
                    {
                        //ɾ��ѡ�񲿷�Ԫ��
                        myRemoveList = Elements.GetRange(StartIndex, EndIndex - StartIndex);
                        //ת��Ϊ����Ԫ��
                        myRealRemoveList = this.Document.GetRealElements(myRemoveList);
                        p.RemoveChildRange(myRealRemoveList);
                        p.RefreshLine();
                    }

                    //�ǵ�һ����ѡ��� 
                    else if (StartIndex > pIndexStart)
                    {
                        //ɾ��ѡ�񲿷�Ԫ��
                        myRemoveList = Elements.GetRange(StartIndex, p.LastElement.Index - StartIndex);
                        //ת��Ϊ����Ԫ��
                        myRealRemoveList = this.Document.GetRealElements(myRemoveList);
                        p.RemoveChildRange(myRealRemoveList);
                        p.RefreshLine();
                    }
                    //�����һ����ѡ���
                    else if (pIndexEnd > EndIndex)
                    {
                        myRemoveList = Elements.GetRange(pIndexStart, EndIndex - pIndexStart);
                        myRealRemoveList = this.Document.GetRealElements(myRemoveList);
                        p.RemoveChildRange(myRealRemoveList);
                        p.RefreshLine();
                    }

                }

                //��ԭStartIndex������ֵ Add By wwj 2012-04-24
                StartIndex = this.AbsSelectStart;

                //����ĵ���һ���ζ�û���ˣ���Ҫnewһ���¶Σ������ĵ��У������޷�����
                if (this.Document.RootDocumentElement.ChildCount == 0)
                {
                    //MessageBox.Show("һ���նζ�û���ˣ��������һ��");
                    ZYTextParagraph myP = new ZYTextParagraph();
                    myP.OwnerDocument = this.Document;
                    this.InsertParagraph(myP);
                }

                #region ɾ��ѡ�еı�� Add by wwj 2013-01-21
                if (selectionTableDict.Count > 0)
                {
                    foreach (KeyValuePair<ZYTextElement, TPTextTable> pair in selectionTableDict)
                    {
                        ZYTextContainer bodyElement = GetRootElement(pair.Key) as ZYTextContainer;
                        bodyElement.RemoveChild(pair.Value);
                    }
                }
                #endregion
            }
            bolChanged = true;
            if (bolChanged)
            {
                bolModified = true;
                FixSelection();
                if (myDocument != null) myDocument.ContentChanged();
                this.SetSelection(StartIndex, 0);
                FixSelection();
            }
            #region bwy : ����ɾ����ĩβ�ı���������»��߻��ϻ��ߣ�ˢ���������
            this.Document.OwnerControl.Invalidate();
            #endregion bwy :

            return true;
        }

        /// <summary>
        /// ���ѡ����������,�����ݴ������޸�֮
        /// </summary>
        private void FixSelection()
        {
            if (intSelectStart >= myElements.Count)
                intSelectStart = myElements.Count - 1;
            if (intSelectStart < 0)
                intSelectStart = 0;
            if (intSelectLength > 0 && (intSelectStart + intSelectLength > myElements.Count))
                intSelectLength = 0;
            if (intSelectLength < 0 && (intSelectStart + intSelectLength < 0))
                intSelectLength = 0;
        }

        /// <summary>
        /// ɾ����ǰԪ��,��������0:ȷ��ɾ��Ԫ�� 1:��ɾ����Ԫ�� 2:�Ը�Ԫ�ؽ����߼�ɾ��
        /// </summary>
        /// <param name="flag">���ݴ˲�������ɾ���̶�Ԫ��ʱ���Ƿ���Ҫ������ʾ</param>
        /// <returns>�������</returns>
        public int DeleteCurrentElement(params object[] flag)
        {
            if (myElements == null || myElements.Count == 0) return 1;
            if (IsLock(intSelectStart)) return 1;

            if (this.CheckSelectStart())
            {
                ZYTextElement myElement = this.CurrentElement;
                //����ǹ̶��ı���ɾ��
                //����ǹ̶��ı������ڱ༭״����ɾ��
                if ((myElement.Parent is ZYFixedText || myElement.Parent is ZYText) && this.Document.Info.DocumentModel != DocumentModel.Design)
                {
                    if (flag.Length > 0)
                    {
                        MessageBox.Show("ɾ����Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    return 1;
                }

                #region Add By wwj 2013-08-01 ����ڲ���ɾ����Ԫ��ZYButton��ZYFlag֮ǰ��λ���󣬰���Delete����Ԫ�ر�ɾ��������
                if (myElement is ZYButton)
                {
                    if (!((ZYButton)myElement).CanDelete)
                    {
                        MessageBox.Show("ɾ����Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return 1;
                    }
                }
                else if (myElement is ZYFlag)
                {
                    if (!((ZYFlag)myElement).CanDelete)
                    {
                        MessageBox.Show("ɾ����Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return 1;
                    }
                }
                #endregion

                // ����ǰԪ�ز������һ��Ԫ����ɾ��֮
                if (myElement != myElements[myElements.Count - 1])
                {

                    ZYTextElement afterElement = (ZYTextElement)myElements[intSelectStart + 1];

                    ZYTextParagraph parentPara = myElement.Parent as ZYTextParagraph;
                    ZYTextContainer parentDiv = myElement.Parent.Parent;
                    #region bwy :
                    if (myElement.Parent is ZYTextBlock)
                    {
                        if (myElements.Count > intSelectStart + myElement.Parent.ChildCount)//Add by wwj 2013-05-07 �����ɾ���ĵ�ĩβ�Ľṹ��Ԫ�س��������
                        {
                            afterElement = (ZYTextElement)myElements[intSelectStart + myElement.Parent.ChildCount];
                        }
                        parentPara = myElement.Parent.Parent as ZYTextParagraph;
                        parentDiv = myElement.Parent.Parent.Parent;
                    }
                    #endregion bwy :

                    int intDelete = myDocument.isDeleteElement(myElement);
                    if (intDelete == 0)
                    {
                        //bool isEndOfLine = false;

                        //���������ڴ�Ϊһ���ն���,��ɾ���������
                        if (myElement == parentPara.FirstElement && parentPara.ChildCount == 1 && parentPara.FirstElement is ZYTextEOF && !IsParaInCell(myElement))
                        {
                            //��ȡ��ǰ�������һ������.
                            object tmpEle = parentDiv.ChildElements[parentDiv.ChildElements.IndexOf(parentPara) + 1];
                            //�������table,����������,����ִ���κζ���.
                            if (!(tmpEle is TPTextTable))
                            {
                                myElement = (tmpEle as ZYTextParagraph).FirstElement;
                                parentDiv.RemoveChild(parentPara);
                                Document.RefreshElements();
                            }

                        }
                        //�������ڶ�������Ԫ�ش�,�Ҷ��䲻Ϊ��,��ϲ�������䵽�ϸ�������.
                        else if (myElement == parentPara.LastElement && parentPara.ChildCount > 1)
                        {
                            int currentParaIndex = parentDiv.IndexOf(parentPara);

                            //���������еĵ�һ��Ԫ��
                            if (currentParaIndex < parentDiv.ChildCount - 1)
                            {
                                for (int i = 0; i < parentPara.ChildCount - 1; i++)
                                {
                                    #region ����ڶ����ĩβ����Delete��ʱ�����ֽṹ��Ԫ�غϲ������ 2013-05-17
                                    //Add by wwj 2013-05-17
                                    ZYTextElement paragraph = null;
                                    ZYTextElement afterElementNew = null;
                                    GetParagraph(afterElement, out paragraph, out afterElementNew);
                                    (parentPara.ChildElements[i] as ZYTextElement).Parent = (ZYTextParagraph)paragraph;
                                    ((ZYTextParagraph)paragraph).InsertBefore((parentPara.ChildElements[i] as ZYTextElement), afterElementNew);

                                    //Delete by wwj 2013-05-17
                                    //(parentPara.ChildElements[i] as ZYTextElement).Parent = afterElement.Parent;
                                    //afterElement.Parent.InsertBefore((parentPara.ChildElements[i] as ZYTextElement), afterElement); 
                                    #endregion

                                    //(afterElement.Parent.ChildElements[i] as ZYTextElement).Parent = parentPara;
                                    //parentPara.InsertBefore((afterElement.Parent.ChildElements[i] as ZYTextElement), parentPara.LastElement);
                                }
                                parentDiv.RemoveChild(parentPara);
                            }
                        }
                        else if (myElement != parentPara.LastElement && !(myElement is ZYTextContainer))
                        {
                            bool bolSetParent = false;
                            if (myElement.Parent.WholeElement)
                            {
                                bolSetParent = true;
                            }
                            if (myElement.Parent is ZYTextBlock && myElement == myElement.Parent.GetLastElement())
                            {
                                bolSetParent = true;
                            }
                            if (bolSetParent)
                            {
                                myElement = myElement.Parent;
                            }
                            parentPara.RemoveChild(myElement);
                        }

                        #region bwy :
                        if (myElement != parentPara.LastElement && myElement.Parent is ZYTextBlock)
                        {
                            parentPara.RemoveChild(myElement.Parent);
                        }
                        #endregion bwy :
                        bolModified = true;
                        myDocument.ContentChanged();

                        this.SetSelection(intSelectStart, 0);
                    }
                    else if (intDelete == 2)
                    {
                        this.SetSelection(intSelectStart + 1, 0);
                    }
                    return intDelete;
                }
            }
            return 1;
        }

        /// <summary>
        /// Add by wwj 2013-05-17
        /// ��ȡԪ��element���ڵĶ���paragraph���Լ�Ԫ��element�ڶ���paragraph�е�Ԫ��textElement
        /// ��Ԫ��elementΪ�����ı�ʱ��element == textElement
        /// ��Ԫ��elementΪ�ṹ��Ԫ���е�ĳ������ʱ��element != textElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="paragraph"></param>
        /// <param name="textElement"></param>
        public void GetParagraph(ZYTextElement element, out ZYTextElement paragraph, out ZYTextElement textElement)
        {
            paragraph = element.Parent;
            textElement = element;
            if (!(paragraph is ZYTextParagraph))
            {
                paragraph = element.Parent.Parent;
                textElement = element.Parent;
            }
        }

        /// <summary>
        /// ɾ����ǰԪ��ǰһ��Ԫ��,��������0:ȷ��ɾ��Ԫ�� 1:��ɾ����Ԫ�� 2:�Ը�Ԫ�ؽ����߼�ɾ��
        /// </summary>
        /// <returns>�������</returns>
        public int DeletePreElement(params object[] flag)
        {
            if (myElements == null || myElements.Count == 0)
            {
                return 1;
            }
            if (IsLock(intSelectStart - 1))
            {
                return 1;
            }
            //�������Ƭ�ĵ����м�,�����ٿ�ʼ,Ҳ����ĩβ.
            if (intSelectStart > 0 && intSelectStart < myElements.Count)
            {
                //�ж��Ƿ������ĵ��Ľ�β,�ж��������Ϊ.
                //��������һ��Ԫ��,��ô��ɾ�����Ԫ��ʱmyElements.Count��ֵ��仯.
                //��֮intSelectStartҲ����ű�,�����������������.
                bool isLastElement = (intSelectStart == (myElements.Count - 1)) ? true : false;

                //������Ǹ�Ԫ��
                ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
                //���ǰ���Ǹ�Ԫ��
                ZYTextElement preElement = (ZYTextElement)myElements[intSelectStart - 1];
                //����ǹ̶��ı������ڱ༭״����ɾ��
                if ((preElement.Parent is ZYFixedText || preElement.Parent is ZYText) && this.Document.Info.DocumentModel != DocumentModel.Design)
                {
                    if (flag.Length > 0)
                    {
                        MessageBox.Show("ɾ����Χ�а����̶����ݣ�����ɾ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    return 1;
                }
                //Add By wwj 2012-03-30 �ж϶�ZYButton��ɾ������
                if (preElement is ZYButton) if (!((ZYButton)preElement).CanDelete) return 1;

                //Add By wwj 2013-08-01 �ж϶�ZYFlag��ɾ������
                if (preElement is ZYFlag) if (!((ZYFlag)preElement).CanDelete) return 1;

                //��ǰԪ�صĸ�Ԫ��
                ZYTextParagraph parentPara = null;
                //��ǰԪ�صĸ�Ԫ�صĸ�Ԫ��(�ڱ����Ϊcell,����ͨ�ĵ���Ϊbody)
                ZYTextContainer parentDiv = null;
                //�����ǰԪ����ZYTextBlock, ��ZYTextBlock��Ϊһ�����崦��
                if (myElement.Parent is ZYTextBlock)
                {
                    myElement = myElement.Parent;
                    parentPara = myElement.Parent as ZYTextParagraph;
                    parentDiv = parentPara.Parent;
                }
                else
                {
                    parentPara = myElement.Parent as ZYTextParagraph;
                    parentDiv = myElement.Parent.Parent;
                }

                int intDelete = myDocument.isDeleteElement(preElement);
                int OldIndex = myElements.IndexOf(preElement);
                if (intDelete == 0)
                {
                    //����ǿ��кϲ�����һ��,��myElement����preElement.
                    //����Ǵ����ݵ��кϲ�����һ��, ��myElementҪ�ϲ��еĵ�һ��Ԫ��,��preElementΪ�ϲ����е����һ��Ԫ��
                    #region �ϲ���������

                    //���������ڴ�Ϊһ���ն���,�Ҳ���cell��.��ɾ���������
                    if (myElement == parentPara.FirstElement && parentPara.ChildCount == 1 && parentPara.FirstElement is ZYTextEOF && !IsParaInCell(myElement))
                    {
                        //��ȡ��ǰ�������һ������.
                        object tmpEle = parentDiv.ChildElements[parentDiv.ChildElements.IndexOf(parentPara) - 1];
                        //�������table,����������,����ִ���κζ���.
                        if (!(tmpEle is TPTextTable))
                        {
                            myElement = (tmpEle as ZYTextParagraph).LastElement;
                            parentDiv.RemoveChild(parentPara);
                            myDocument.RefreshElements();
                        }
                    }

                    //���������ڴ�Ϊһ���ն���,����cell��.
                    //�ҵ�ǰ���䲻��cell�ĵ�һ��Ԫ��
                    else if (myElement == parentPara.FirstElement && parentPara.ChildCount == 1 && parentPara.FirstElement is ZYTextEOF &&
                        IsParaInCell(myElement) && parentPara != parentDiv.FirstElement)
                    {
                        myElement = (parentDiv.ChildElements[parentDiv.ChildElements.IndexOf(parentPara) - 1] as ZYTextParagraph).LastElement;
                        parentDiv.RemoveChild(parentPara);
                        myDocument.RefreshElements();
                    }

                    //�������ڶ���ĵ�һ��Ԫ�ش�,�Ҷ��䲻Ϊ��,��ϲ�������䵽�ϸ�������.
                    else if (myElement == parentPara.FirstElement && parentPara.ChildCount > 1 && !(parentPara.FirstElement is ZYTextEOF))
                    {
                        int currentParaIndex = parentDiv.IndexOf(parentPara);

                        //Add by wwj 2012-05-29 �������ڱ��ĵ�Ԫ���У����λ����ǰ�棬����ɾ������������һ���лᱨ��������������жϣ����ⱨ�� todo
                        if (currentParaIndex - 1 < 0) return 1;

                        object tmpEle = parentDiv.ChildElements[currentParaIndex - 1];
                        //���������еĵ�һ��Ԫ��
                        if (currentParaIndex > 0 && !(tmpEle is TPTextTable))
                        {
                            //����һ���ڴ�����
                            System.Xml.XmlDocument myDoc = new System.Xml.XmlDocument();
                            myDoc.PreserveWhitespace = true;
                            myDoc.AppendChild(myDoc.CreateElement(ZYTextConst.c_ClipboardDataFormat));

                            //copy ��ǰ��������Ԫ�ص�һ���б��У�EOF����
                            ArrayList myList = new ArrayList();
                            for (int i = 0; i < parentPara.ChildCount - 1; i++)
                            {
                                myList.Add(parentPara.ChildElements[i]);
                            }
                            //���б��е�����Ԫ����xml����ʽ�浽�ڴ���
                            ZYTextElement.ElementsToXML(this.Document.GetRealElements(myList), myDoc.DocumentElement);

                            //��ԭcopy�����뵽��һ��,past
                            parentDiv.RemoveChild(parentPara);
                            myList.Clear();
                            this.Document.LoadElementsToList(myDoc.DocumentElement, myList);
                            if (myList.Count > 0)
                            {
                                foreach (ZYTextElement ele in myList)
                                {
                                    ele.RefreshSize();
                                }
                                preElement.Parent.InsertRangeBefore(myList, preElement.Parent.LastElement);
                                myDocument.RefreshElements();
                                myElement = preElement.Parent.ChildElements[preElement.Parent.ChildElements.Count - myList.Count - 1] as ZYTextElement;
                            }
                        }
                    }
                    #endregion


                    //�����ǰԪ�ز��ǵ�ǰ����ĵ�һ��Ԫ��, ��ǰһ��Ԫ�ز�������
                    else if (myElement != parentPara.FirstElement && !(preElement is ZYTextContainer))
                    {
                        bool bolSetParent = false;
                        if (preElement.Parent.WholeElement)
                        {
                            bolSetParent = true;
                        }
                        if (preElement.Parent is ZYTextBlock && preElement == preElement.Parent.GetLastElement())
                        {
                            bolSetParent = true;
                        }
                        if (bolSetParent)
                        {
                            preElement = preElement.Parent;
                        }
                        parentPara.RemoveChild(preElement);

                        #region bwy
                        myDocument.RefreshElements();
                        parentPara.RefreshLine();
                        #endregion bwy
                    }

                    bolModified = true;

                    //���������ĵ��Ľ�β
                    if (isLastElement)
                    {
                        this.SetSelection(intSelectStart, 0);
                    }
                    else
                    {
                        //������Ԫ����һ�ο�ͷ���˸�ʱ�����λ�ò��Ե�����
                        if (myElement is ZYTextBlock)
                        {
                            this.MoveSelectStart((myElement as ZYTextBlock).FirstElement);
                        }
                        else
                        {
                            this.MoveSelectStart(myElement);
                        }
                    }

                    myDocument.ContentChanged();

                }
                else if (intDelete == 2)
                {
                    this.SetSelection(OldIndex, 0);
                }
                return intDelete;

            }
            return 1;
        }// void DeletePreElement


        /// <summary>
        /// ��������ı�����
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return ZYTextElement.GetElementsText(myElements);
        }


        /// <summary>
        /// ��ò����ǰ��һ�����ʵ���ʼλ��
        /// </summary>
        /// <returns></returns>
        public int GetPreWordIndex()
        {
            //intSelectStart = this.FixIndex( intSelectStart );
            int index = -1;
            ZYTextLine myLine = this.CurrentLine;
            for (int iCount = intSelectStart - 1; iCount >= 0; iCount--)
            {
                if (myElements[iCount] is ZYTextChar)
                {
                    ZYTextChar myChar = (ZYTextChar)myElements[iCount];
                    if (char.IsLetter(myChar.Char) && myChar.OwnerLine == myLine)
                        index = iCount;
                    else
                        break;
                }
                else
                    break;
            }
            return index;
        }// int GetPreWordIndex()

        /// <summary>
        /// ��ò����ǰ��һ�����ʵ���ʼλ��
        /// </summary>
        /// <param name="myElement">ָ����Ԫ�ض���</param>
        /// <returns></returns>
        public int GetPreWordIndex(ZYTextElement myElement)
        {
            //intSelectStart = this.FixIndex( intSelectStart );
            int index = -1;
            if (myElement == null || myElements.Contains(myElement) == false)
                return -1;
            for (int iCount = myElements.IndexOf(myElement) - 1; iCount >= 0; iCount--)
            {
                if (myElements[iCount] is ZYTextChar)
                {
                    if (char.IsLetter((myElements[iCount] as ZYTextChar).Char))
                        index = iCount;
                    else
                        break;
                }
                else
                    break;
            }
            return index;
        }// int GetPreWordIndex()


        /// <summary>
        /// ��ò����ǰ�ĵ���
        /// </summary>
        /// <returns>��õĵ��ʣ����������򷵻ؿ�����</returns>
        public string GetPreWord()
        {
            int index = this.GetPreWordIndex();
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            ZYTextChar myChar = null;
            if (index >= 0)
            {
                for (int iCount = index; iCount < intSelectStart; iCount++)
                {
                    myChar = myElements[iCount] as ZYTextChar;
                    if (myChar != null)
                    {
                        if (char.IsLetter(myChar.Char))
                        {
                            myStr.Append(myChar.Char);
                        }
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            if (myStr.Length == 0)
                return null;
            else
                return myStr.ToString();
        }// string GetPreWord()

        /// <summary>
        /// ���ָ��Ԫ��ǰ�ĵ���
        /// </summary>
        /// <param name="myElement">ָ����Ԫ�ض���</param>
        /// <returns>��õĵ��ʣ����������򷵻ؿ�����</returns>
        public string GetPreWord(ZYTextElement myElement)
        {
            int index = this.GetPreWordIndex(myElement);
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            ZYTextChar myChar = null;
            if (index >= 0)
            {
                for (int iCount = index; iCount < myElements.Count; iCount++)
                {
                    myChar = myElements[iCount] as ZYTextChar;
                    if (myChar != null)
                    {
                        if (char.IsLetter(myChar.Char))
                            myStr.Append(myChar.Char);
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            if (myStr.Length == 0)
                return null;
            else
                return myStr.ToString();
        }// string GetPreWord()

        /// <summary>
        /// ���ָ����Χ�ڵ�Ԫ�ص��ı�
        /// </summary>
        /// <param name="intStartIndex">ѡ������Ŀ�ʼ���</param>
        /// <param name="intEndIndex">ѡ������Ľ������</param>
        /// <returns>ѡ�����������е�Ԫ�ص��ı�</returns>
        public string GetRangeText(int intStartIndex, int intEndIndex)
        {
            intStartIndex = FixIndex(intStartIndex);
            intEndIndex = FixIndex(intEndIndex);
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            for (int iCount = intStartIndex; iCount <= intEndIndex; iCount++)
            {
                myStr.Append(((ZYTextElement)myElements[iCount]).ToEMRString());
            }
            return myStr.ToString();
        }

        /// <summary>
        /// �ڲ�ʹ�õĻ��ͬԪ���б�����ȵ��ַ����ı�
        /// </summary>
        /// <returns></returns>
        internal string GetFixLenText()
        {
            if (myElements == null)
                return null;
            else
            {
                if (bolModified == false && strFixLenText != null)
                    return strFixLenText;
                else
                {
                    char[] vChar = new char[myElements.Count];
                    ZYTextChar myChar = null;
                    for (int iCount = 0; iCount < myElements.Count; iCount++)
                    {
                        myChar = myElements[iCount] as ZYTextChar;
                        if (myChar == null)
                            vChar[iCount] = (char)1;
                        else
                            vChar[iCount] = myChar.Char;
                    }
                    strFixLenText = new string(vChar);
                    return strFixLenText;
                }
            }
        }



        #region ���ң��滻����Ⱥ
        /// <summary>
        /// �����ַ���,���ҵ�������ѡ������Ϊ�ҵ����ַ���
        /// </summary>
        /// <param name="strText">��Ҫ���ҵ��ַ���</param>
        /// <returns>�Ƿ��ҵ��ַ���</returns>
        public bool FindText(string strText)
        {
            if (strText != null && strText.Length != 0)
            {

                GetFixLenText();
                if (strFixLenText != null)
                {
                    int Index = strFixLenText.IndexOf(strText, this.SelectStart);
                    if (Index >= 0)
                    {
                        this.Document.Content.MoveSelectStart(Index);
                        this.Document.OwnerControl.ScrollViewtopToCurrentElement();
                        this.SetSelection(Index + strText.Length, 0 - strText.Length);
                    }
                    return (Index >= 0);
                }
            }
            return false;
        }

        /// <summary>
        /// �滻�ַ���
        /// </summary>
        /// <param name="strFind">��Ҫ���ҵ��ַ���</param>
        /// <param name="strReplace">��Ҫ�滻���ַ���</param>
        /// <returns>�Ƿ��滻���ַ���</returns>
        public bool ReplaceText(string strFind, string strReplace, out string msg)
        {
            msg = "";
            if (this.GetSelectText() == strFind)
            {
                //���Ҫ�滻��������Ԫ���У������滻
                bool flag = true;
                foreach (ZYTextElement ele in this.GetSelectElements())
                {
                    if (ele.Parent is ZYTextBlock || ele is ZYElement)
                    {
                        msg = "ֻ���滻���ı��������滻Ԫ���ڲ��ı���";
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    this.ReplaceSelection(strReplace);
                }
            }
            return true;
        }

        /// <summary>
        /// �滻���е��ַ���
        /// </summary>
        /// <param name="strFind">���ҵ��ַ���</param>
        /// <param name="strReplace">�滻���ַ���</param>
        /// <returns>�滻�Ĵ���</returns>
        public int ReplaceTextAll(string strFind, string strReplace)
        {
            int iCount = 0;
            this.SetSelection(0, 0);
            while (FindText(strFind))
            {
                this.ReplaceSelection(strReplace);
                iCount++;
            }
            return iCount;
        }
        #endregion


        #region �ƶ���ǰ�����λ�õĺ���Ⱥ
        /// <summary>
        /// ������������ƶ�һ��
        /// </summary>
        public void MoveUpOneLine()
        {
            ZYTextLine myLine = this.PreLine;
            if (myLine != null)
            {
                if (intLastXPos <= 0)
                {
                    ZYTextElement StartElement = (ZYTextElement)myElements[intSelectStart];
                    intLastXPos = StartElement.RealLeft;
                }
                for (int i = 0; i < myLine.Elements.Count; i++)//foreach (ZYTextElement myElement in myLine.Elements)
                {
                    ZYTextElement myElement = myLine.Elements[i] as ZYTextElement;
                    if (myElement.RealLeft >= intLastXPos)
                    {
                        #region bwy:
                        //�����Ԫ����
                        if (myElement.Parent is ZYTextBlock)
                        {
                            myElement = (myElement.Parent as ZYTextBlock).FirstElement;
                        }
                        #endregion bwy:
                        this.MoveSelectStart(myElement);
                        return;
                    }
                }
                this.MoveSelectStart(myLine.LastElement);
            }
            #region comment
            // MoveDownOneLine

            //
            //			ZYTextElement StartElement =(ZYTextElement) myElements[intSelectStart];
            //			//int OldLineIndex = StartElement.LineIndex ;
            //			int OldLeft	 = intLastXPos ;
            //			if( intLastXPos <= 0 )
            //			{
            //				OldLeft = StartElement.RealLeft + StartElement.Width  ;
            //				intLastXPos = OldLeft ;
            //			}
            //
            //			ZYTextElement myElement = null;
            //			bool bolLineChanged = false;
            //			ZYTextElement LastParent = StartElement.Parent ;
            //			//int LineIndex = 0 ;
            //			ZYTextLine OldLine = StartElement.OwnerLine ;
            //			for( int iCount = intSelectStart - 1 ; iCount >= 0  ; iCount -- )
            //			{
            //				myElement = ( ZYTextElement ) myElements[iCount];
            //				if( bolLineChanged == false && ( myElement.OwnerLine != OldLine  ))
            //				{
            //					bolLineChanged = true;
            //					OldLine = myElement.OwnerLine ;
            //				}
            //				if( bolLineChanged)
            //				{
            //					if( myElement.OwnerLine != OldLine  )
            //					{
            //						this.MoveSelectStart( iCount +1 );
            //						break;
            //					}
            //
            //					if( myElement.RealLeft <= OldLeft )
            //					{
            //						this.MoveSelectStart( iCount ) ;
            //						break;
            //					}
            //				}
            //				LastParent = myElement.Parent ;
            //			}
            //this.MoveSelectStart(0);
            #endregion
        }

        /// <summary>
        /// ������������ƶ�һ��
        /// </summary>
        public void MoveDownOneLine()
        {
            ZYTextLine myLine = this.NextLine;
            if (myLine != null)
            {
                if (intLastXPos <= 0)
                {
                    ZYTextElement StartElement = (ZYTextElement)myElements[intSelectStart];
                    intLastXPos = StartElement.RealLeft;
                }
                for (int i = 0; i < myLine.Elements.Count; i++)//foreach (ZYTextElement myElement in myLine.Elements)
                {
                    ZYTextElement myElement = myLine.Elements[i] as ZYTextElement;
                    if (myElement.RealLeft >= intLastXPos)
                    {
                        #region bwy:
                        //�����Ԫ����
                        if (myElement.Parent is ZYTextBlock)
                        {
                            myElement = this.GetNextElement((myElement.Parent as ZYTextBlock).LastElement);
                        }
                        #endregion bwy:
                        this.MoveSelectStart(myElement);
                        return;
                    }
                }
                this.MoveSelectStart(myLine.LastElement);
            }// MoveDownOneLine
        }

        /// <summary>
        /// ������������ƶ�һ��Ԫ��
        /// </summary>
        public void MoveLeft()
        {
            intLastXPos = -1;
            if (intSelectStart > 0)
            {
                //����ƶ�����Ԫ���ڲ���������
                ZYTextElement ele = this.Elements[intSelectStart - 1] as ZYTextElement;
                if (ele.Parent is ZYTextBlock)
                {
                    intSelectStart = this.Elements.IndexOf((ele.Parent as ZYTextBlock).FirstElement);
                    this.MoveSelectStart(intSelectStart);
                }
                else
                {
                    this.MoveSelectStart(intSelectStart - 1);
                }
            }
        }

        /// <summary>
        /// ������������ƶ�һ��Ԫ��
        /// </summary>
        public void MoveRight()
        {
            intLastXPos = -1;
            if (intSelectStart < myElements.Count - 1)
            {
                //����ƶ�����Ԫ���ڲ���������
                ZYTextElement ele = this.Elements[intSelectStart] as ZYTextElement;
                if (ele.Parent is ZYTextBlock)
                {
                    intSelectStart = this.Elements.IndexOf((ele.Parent as ZYTextBlock).LastElement);
                    this.MoveSelectStart(intSelectStart + 1);
                }
                else
                {
                    this.MoveSelectStart(intSelectStart + 1);
                }
            }
        }

        /// <summary>
        /// ��������ƶ�����ǰ�е����һ��Ԫ�ش�
        /// </summary>
        public void MoveEnd()
        {
            try
            {
                ZYTextLine myLine = this.CurrentLine;
                if (myLine != null && bolLineEndFlag == false)
                {
                    intLastXPos = -1;
                    //this.CurrentElement = myLine.LastElement;
                    ZYTextElement ele = myLine.LastElement;
                    if (ele.Parent is ZYTextBlock)
                    {
                        ele = (ele.Parent as ZYTextBlock).FirstElement;
                        this.MoveSelectStart(ele);
                        return;
                    }

                    if (ele.isNewLine())//(myLine.LastElement.isNewLine())
                    {
                        this.MoveSelectStart(ele);//(myLine.LastElement);
                    }
                    else
                    {
                        //this.MoveSelectStart(myElements.IndexOf(myLine.LastElement) + 1);

                        this.MoveSelectStart(myElements.IndexOf(ele) + 1);
                        bolLineEndFlag = true;
                        ((ZYTextDocument)myDocument).UpdateTextCaret();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// �ƶ���ǰ����㵽��ǰ�е�����
        /// </summary>
        public void MoveHome()
        {
            ZYTextLine myLine = null;
            myLine = this.CurrentLine;
            if (myLine != null)
            {
                intLastXPos = -1;

                #region bwy:
                ZYTextElement ele = myLine.FirstElement;
                if (ele.Parent is ZYTextBlock && ele != ele.Parent.FirstElement)
                {
                    ele = this.GetNextElement((ele.Parent as ZYTextBlock).LastElement);
                }
                int FirstIndex = myElements.IndexOf(ele);
                #endregion bwy:
                // ��õ�һ���ǿհ��ַ�Ԫ�ص����
                int FirstNBlank = 0;

                foreach (ZYTextElement myElement in myLine.Elements)
                {
                    ZYTextChar myChar = myElement as ZYTextChar;
                    if (myChar == null || char.IsWhiteSpace(myChar.Char) == false)
                    {
                        FirstNBlank = myLine.Elements.IndexOf(myElement);
                        break;
                    }
                }
                if (FirstNBlank == 0 || intSelectStart == (FirstIndex + FirstNBlank))
                {
                    this.MoveSelectStart(FirstIndex);
                }
                else
                {
                    this.MoveSelectStart(FirstIndex + FirstNBlank);
                }
            }
        }// void MoveHome

        /// <summary>
        /// ���ָ��λ�ô���Ԫ��
        /// 2009-7-2 22:00 mfb����ʵ��. ������MoveTo(x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ZYTextElement GetElementAt(int x, int y)
        {
            if (myDocument != null && myDocument.Lines != null && myDocument.Lines.Count > 0)
            {
                ZYTextLine CurrentLine = null;

                foreach (ZYTextLine myLine in myDocument.Lines)
                {
                    if (myLine.RealTop + myLine.Height >= y)
                    {
                        if (myLine.RealLeft <= x && (myLine.RealLeft + myLine.ContentWidth) >= x)
                        {
                            CurrentLine = myLine;
                            break;
                        }
                    }
                }
                if (CurrentLine == null && myDocument.Lines.Count > 0)
                    CurrentLine = (ZYTextLine)myDocument.Lines[myDocument.Lines.Count - 1];

                if (CurrentLine == null)
                    return null;
                x -= CurrentLine.RealLeft;


                ZYTextElement CurrentElement = null;

                if (CurrentLine.Elements.Count == 0)
                {
                    CurrentElement = CurrentLine.Container;
                }
                foreach (ZYTextElement myElement in CurrentLine.Elements)
                {
                    if (WeiWenProcess.weiwen)
                    {
                        //ά����ʽ������X������ ��->�ң���ȡ�ַ��� ��<-�� 
                        if (x > myElement.Left && x < myElement.Left + myElement.Width)
                        {
                            if (x > (myElement.Left + myElement.Width))
                            {
                                continue;
                            }
                            if (myElement.Parent.WholeElement)
                                return null;
                            CurrentElement = myElement;
                            break;
                        }
                    }
                    else
                    {
                        if (x < myElement.Left + myElement.Width)
                        {
                            //ΪʲôС���˻������
                            if (x > (myElement.Left + myElement.Width))
                            {
                                continue;
                            }
                            if (myElement.Parent.WholeElement)
                                return null;
                            CurrentElement = myElement;
                            break;
                        }
                    }
                }

                if (CurrentElement == null)
                {
                    CurrentElement = CurrentLine.LastElement;
                }
                return CurrentElement;

            }
            return null;
        }



        /// <summary>
        /// ��⵱ǰ�Ĳ����λ���Ƿ���ȷ
        /// </summary>
        /// <returns></returns>
        private bool CheckSelectStart()
        {
            if (myElements == null)
                return false;
            else
                return (intSelectStart >= 0 && intSelectStart <= myElements.Count - 1);
        }



        /// <summary>
        /// �ƶ���ǰ�����
        /// </summary>
        /// <param name="iStep">������ƶ�����</param>
        public void MoveStep(int iStep)
        {
            ZYTextElement StartElement = (ZYTextElement)myElements[intSelectStart];
            this.MoveTo(StartElement.RealLeft, StartElement.RealTop + iStep);
        }


        #endregion

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public ZYContent()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// �ж��Ƿ����ɾ��ѡ��Ԫ��,���ѡ�ж����Ԫ�������������ɾ�� Add By wwj 2012-04-24
        /// </summary>
        /// <returns></returns>
        public bool CanDeleteSelectElement()
        {
            //��ʾ�Ƿ���Ԫ���ڵ�Ԫ����ⲿ
            bool isHasElementOutCell = false;

            System.Collections.ArrayList myList = this.GetSelectElements();
            List<ZYTextElement> cellList = new List<ZYTextElement>();
            foreach (ZYTextElement ele in myList)
            {
                ZYTextElement textElement = this.GetParentByElement(ele, ZYTextConst.c_Cell);
                if (textElement != null)
                {
                    //�����ڵ�Ԫ���ڵģ������ڵ�Ԫ����ģ�������ɾ��Ԫ��
                    if (isHasElementOutCell) return false;

                    if (!cellList.Contains(textElement))
                    {
                        cellList.Add(textElement);
                        if (cellList.Count > 1)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    isHasElementOutCell = true;//��ʾ��Ԫ���ڵ�Ԫ����ⲿ

                    //����ѡ�б�����Ԫ�أ���ѡ���˱���е�Ԫ��������ɾ������
                    if (cellList.Count > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// �õ��༭���е�һ����� Add by wwj 2012-05-29
        /// </summary>
        /// <returns></returns>
        public TPTextTable GetFirstTable()
        {
            ArrayList list = myElements;
            for (int i = 0; i < list.Count; i++)
            {
                ZYTextElement myElement = (ZYTextElement)myElements[i];
                ZYTextElement table = GetParentByElement(myElement, ZYTextConst.c_Table);
                if (table != null && table is TPTextTable)
                {
                    return ((TPTextTable)table).Clone();
                }
            }
            return null;
        }

        /// <summary>
        /// ���õ�Ԫ���Ƿ���Ի��� Add By wwj 2012-06-06
        /// </summary>
        public void SetTableCellCanInsertEnter(bool canEnter)
        {
            if (myElements == null || myElements.Count == 0)
                return;
            if (this.IsLock(intSelectStart))
                return;

            //��õ�ǰԪ��
            ZYTextElement myElement = (ZYTextElement)myElements[intSelectStart];
            //��õ�ǰ���
            TPTextTable currentTable = GetParentByElement(myElement, ZYTextConst.c_Table) as TPTextTable;
            if (currentTable != null)
            {
                List<TPTextCell> listCells = new List<TPTextCell>();
                foreach (TPTextRow row in currentTable)
                {
                    foreach (TPTextCell cell in row)
                    {
                        if (true == cell.CanAccess)
                        {
                            cell.CanInsertEnter = canEnter;
                        }
                    }
                }
            }
        }
    }
}
