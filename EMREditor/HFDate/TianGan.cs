using System;
using System.Collections.Generic;
using System.Text;

namespace HuangF.Sys.Date
{
    /// <summary>
    /// ���ö������
    /// </summary>
    [Serializable]
    public enum TianGanTypes
    {
        /// <summary>
        /// ��
        /// </summary>
        Jia = 1,

        /// <summary>
        /// ��
        /// </summary>
        Yi,

        /// <summary>
        /// ��
        /// </summary>
        Bing,

        /// <summary>
        /// ��
        /// </summary>
        Ding,

        /// <summary>
        /// ��
        /// </summary>
        Wu,

        /// <summary>
        /// ��
        /// </summary>
        Ji,

        /// <summary>
        /// ��
        /// </summary>
        Geng,

        /// <summary>
        /// ��
        /// </summary>
        Xin,

        /// <summary>
        /// ��
        /// </summary>
        Ren,

        /// <summary>
        /// ��
        /// </summary>
        Gui
    }


    /// <summary>
    /// �����
    /// </summary>
    [Serializable]
    public sealed class TianGan
    {
        /// <summary>
        /// ������ɵ���������
        /// </summary>
        /// <param name="atg">���ö��ֵ</param>
        /// <returns>��ɵ���������</returns>
        private static string GetName(TianGanTypes atg)
        {
            string[] s ={ "��", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
            int i = (int)atg;
            return s[i - 1];
        }


        /// <summary>
        /// ���㽫��ɼ���i���ֵ
        /// </summary>
        /// <param name="atg">���</param>
        /// <param name="i">Ҫ���ӵ�����</param>
        /// <returns>ĳһ���ö������iֵ������</returns>
        private TianGanTypes inc(TianGanTypes atg, int i)
        {
            int j = i % 10;
            if (j == 0) return atg;
            int tg = ((int)atg) + j;
            if (tg > 10)
                tg = tg - 10;
            else if (tg < 1)
                tg = tg + 10;
            return (TianGanTypes)tg;
        }

        /// <summary>
        /// ��ȡ����ǰ��ɼ�(��)ָ����ֵ������
        /// </summary>
        /// <param name="i">�Ӽ���ֵ</param>
        /// <returns>�����ʵ��</returns>
        public TianGan Inc(int i)
        {
            TianGanTypes tg = inc(id, i);
            return new TianGan(tg);
        }

        /// <summary>
        /// ��ȡ��ǰ��ɵ���һ��
        /// </summary>
        /// <returns></returns>
        public TianGan Next()
        {
            return Inc(1);
        }

        /// <summary>
        /// ��ȡ��ǰ��ɵ�ǰһ��
        /// </summary>
        /// <returns></returns>
        public TianGan Prior()
        {
            return Inc(-1);
        }

        /// <summary>
        /// ������������ö��ֵ
        /// </summary>
        private readonly TianGanTypes id;

        /// <summary>
        /// ������������ö��ֵ
        /// </summary>
        public TianGanTypes ID
        {
            get { return id; }
        }

        /// <summary>
        /// ��ɵ���������
        /// </summary>
        public int IntValue
        {
            get { return (int)id; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="aid">��ɵ�ö��ֵ</param>
        public TianGan(TianGanTypes aid)
        {
            id = aid;
        }

        /// <summary>
        /// ���캯����Ĭ�Ϲ��캯������ʵ����Ϊ���ס�
        /// </summary>
        public TianGan()
        {
            id = TianGanTypes.Jia;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="aid">��ɵ���������</param>
        public TianGan(int aid)
        {
            int i = aid % 10;
            if (i > 0)
                this.id = (TianGanTypes)i;
            else if (i < 0)
                this.id = (TianGanTypes)(10 + i);
            else
                this.id = TianGanTypes.Gui;
        }

        /// <summary>
        /// ��ɵ���������
        /// </summary>
        public string Name
        {
            get { return GetName(id); }
        }

    }//end of class TTianGan

}
