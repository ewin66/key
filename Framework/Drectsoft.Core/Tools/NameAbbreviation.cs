using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core
{
    /// <summary>
    /// ���ֵ���д��
    /// </summary>
    public class NameAbbreviation
    {
        private string _spellAbbreviation = "";
        private string _wubiAbbreviation = "";

        #region INameAbbreviation Members

        /// <summary>
        /// ƴ����д
        /// </summary>
        public string ABOfSpell
        {
            get
            {
                return _spellAbbreviation;
            }
            set
            {
                _spellAbbreviation = value;
            }
        }

        /// <summary>
        /// �����д
        /// </summary>
        public string ABOfWubi
        {
            get
            {
                return _wubiAbbreviation;
            }
            set
            {
                _wubiAbbreviation = value;
            }
        }

        #endregion
}
}
