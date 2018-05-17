using System;
using System.Collections.Generic;
using System.Text;

namespace YidanSoft.Core.Symbol
{
    /// <summary>
    /// �������ת�����������ṩ�������ת���������
    /// </summary>
    public abstract class ConvertController<S, T>
    {
        /// <summary>
        /// ִ��DoConvert����S���͵�origianlת��Ϊ����T
        /// ��origianlΪ�գ�����T���͵�Ĭ��ֵ
        /// </summary>
        /// <param name="origianl"></param>
        /// <returns>T</returns>
        public T DoConvert(S origianl)
        {
            try
            {
                if (object.Equals(origianl, default(S)))
                    return default(T);
                return DoSTConvert(origianl);
            }
            catch (Exception e) { throw e; }
        }
        /// <summary>
        /// DoConvert����ʵ��
        /// </summary>
        /// <param name="origianl"></param>
        /// <returns></returns>
        protected abstract T DoSTConvert(S origianl);
    }
}
