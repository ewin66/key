using System;
using System.Collections.Generic;
using System.Text;

namespace HuangF.Sys.Date
{
    /// <summary>
    /// ��֧ö������
    /// </summary>
    [Serializable]
    public enum DiZhiTypes
    {
        /// <summary>
        /// ��
        /// </summary>
        Zi = 1,

        /// <summary>
        /// ��
        /// </summary>
        Chou,

        /// <summary>
        /// ��
        /// </summary>
        Yin,

        /// <summary>
        /// î 
        /// </summary>
        Mao,

        /// <summary>
        /// ��
        /// </summary>
        Chen,

        /// <summary>
        /// ��
        /// </summary>
        Si,

        /// <summary>
        /// ��
        /// </summary>
        Wu,

        /// <summary>
        /// δ
        /// </summary>
        Wei,

        /// <summary>
        /// ��
        /// </summary>
        Shen,

        /// <summary>
        /// ��
        /// </summary>
        You,

        /// <summary>
        /// ��
        /// </summary>
        Xu,

        /// <summary>
        /// ��
        /// </summary>
        Hai
    }


    /// <summary>
    /// ��֧��
    /// </summary>
    [Serializable]
    public sealed class DiZhi
    {
        /// <summary>
        /// 
        /// </summary>
        private static string[] names = { "��", "��", "��", "î", "��", "��", "��", "δ", "��", "��", "��", "��" };

        /// <summary>
        /// ��ȡָ����֧����������
        /// </summary>
        /// <param name="adz">ָ���ĵ�֧</param>
        /// <returns>��֧����</returns>
        private static string GetName(DiZhiTypes adz)
        {            
            int i = (int)adz;
            return names[i - 1];
        }

        /// <summary>
        /// ������֧ö�ٴ���
        /// </summary>
        private readonly DiZhiTypes id;


        /// <summary>
        /// ���캯��
        /// </summary>
        public DiZhi()
        {
            id = DiZhiTypes.Zi;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dz">��֧ö��ֵ</param>
        public DiZhi(DiZhiTypes dz)
        {
            id = dz;
        }


        /// <summary>
        /// ���㽫��֧����i���ֵ
        /// </summary>
        /// <param name="adz">��֧</param>
        /// <param name="i">Ҫ���ӵ�����</param>
        /// <returns>ĳһ��֧ö������iֵ��ĵ�֧</returns>
        private DiZhiTypes inc(DiZhiTypes adz, int i)
        {
            int j = i % 12;
            if (j == 0) return adz;
            int dz = ((int)adz) + j;
            if (dz > 12)
                dz = dz - 12;
            else if (dz < 1)
                dz = dz + 12;
            return (DiZhiTypes)dz;
        }

        /// <summary>
        /// ��ȡ����ǰʵ������iֵ��ĵ�֧ʵ��
        /// </summary>
        /// <param name="i">���ӵ���ֵ</param>
        /// <returns>��֧ʵ��</returns>
        public DiZhi Inc(int i)
        {
            DiZhiTypes dz = inc(id, i);
            return new DiZhi(dz);
        }


        /// <summary>
        /// ��ȡ��ǰ��֧�����һ����������һ��Ϊ��,������һ��Ϊ��
        /// </summary>
        /// <returns></returns>
        public DiZhi Next()
        {
            return Inc(1);
        }


        /// <summary>
        /// ��ȡ��ǰ��֧ǰ���һ����������ǰһ��Ϊ��,�ӵ�ǰһ��Ϊ��
        /// </summary>
        /// <returns></returns>
        public DiZhi Prior()
        {
            return Inc(-1);
        }

        /// <summary>
        /// ��֧����������
        /// </summary>
        public string Name
        {
            get { return GetName(id); }
        }

        /// <summary>
        /// ��֧��ö��ֵ����
        /// </summary>
        public DiZhiTypes ID
        {
            get { return id; }
        }

        /// <summary>
        /// ��֧������ֵ
        /// </summary>
        public int IntValue
        {
            get { return (int)id; }
        }

    }//end of class dizhi

}
