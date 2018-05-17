using System;
using System.Collections.Generic;
using System.Text;

namespace HuangF.Sys.Date
{
    /// <summary>
    /// ��֧��,����ɺ͵�֧�����
    /// </summary>
    [Serializable]
    public class GanZhi
    {
        /// <summary>
        /// ���
        /// </summary>
        private readonly TianGan _tg;

        /// <summary>
        /// ��֧
        /// </summary>
        private readonly DiZhi _dz;

        /// <summary>
        /// ���캯��
        /// </summary>
        public GanZhi()
        {
            _tg = new TianGan(TianGanTypes.Jia);
            _dz = new DiZhi(DiZhiTypes.Zi);
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="atg">���ö��</param>
        /// <param name="adz">��֧ö��</param>
        public GanZhi(TianGanTypes atg, DiZhiTypes adz)
        {
            _tg = new TianGan(atg);
            _dz = new DiZhi(adz);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="atg">���</param>
        /// <param name="adz">��֧</param>
        public GanZhi(TianGan atg, DiZhi adz)
        {
            _tg = atg;
            _dz = adz;
        }

        /// <summary>
        /// ��ȡ����ָ������ֵ��ĸ�֧ʵ��
        /// </summary>
        /// <param name="i">���ӵ���ֵ</param>
        /// <returns>��֧</returns>
        public GanZhi Inc(int i)
        {
            TianGan t = _tg.Inc(i);
            DiZhi d = _dz.Inc(i);
            return new GanZhi(t, d);
        }

        /// <summary>
        /// ���
        /// </summary>
        public TianGan Gan
        {
            get { return _tg; }
        }


        /// <summary>
        /// ��֧
        /// </summary>
        public DiZhi Zhi
        {
            get { return _dz; }
        }

        /// <summary>
        /// ��֧������
        /// </summary>
        public string Name
        {
            get { return _tg.Name + _dz.Name; }
        }

    }//end of class GanZhi

}
