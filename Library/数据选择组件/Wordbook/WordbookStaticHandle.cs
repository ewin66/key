/*****************************************************************************\
**             Yindansoft & DrectSoft Software Co. Ltd.                          **
**                                                                           **
**             ���ֵ����йص�һЩ��̬�����Ͷ���                                **
**                                                                           **
**                                                                           **
\*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DrectSoft.Common.Eop;
using DrectSoft.Wordbook.Schema;
using System.Xml.Serialization;
using DrectSoft.Core;
using System.Linq;

namespace DrectSoft.Wordbook
{
    /// <summary>
    /// �ֵ��ྲ̬��Ϣ�ʹ����ֵ������ļ��ľ�̬����
    /// </summary>
    public static class WordbookStaticHandle
    {
        #region public properties
        /// <summary>
        /// �����ֵ����(key: �ֵ����ı�ʶ value: �ֵ���������)
        /// </summary>
        public static Dictionary<string, string> WordbookCatalogs
        {
            get
            {
                if (_wordbookCatalogs == null)
                {
                    _wordbookCatalogs = new Dictionary<string, string>();

                    foreach (WordbookCatalog catalog in AllWordbook.Catalogs)
                        _wordbookCatalogs.Add(catalog.CatalogName, catalog.Caption);
                }
                return _wordbookCatalogs;
            }
        }
        private static Dictionary<string, string> _wordbookCatalogs;

        /// <summary>
        /// ���й̶��Ĵ����ֵ�����Ϣ�б�
        /// </summary>
        public static Collection<WordbookInfo> WordbookList
        {
            get
            {
                if (_wordbookList == null)
                {
                    _wordbookList = new Collection<WordbookInfo>();

                    foreach (WordbookCatalog catalog in AllWordbook.Catalogs)
                        foreach (Wordbook.Schema.Wordbook book in catalog.Wordbooks)
                            _wordbookList.Add(new WordbookInfo(catalog.Caption, book.Caption, book.WordbookName));
                }

                return _wordbookList;
            }
        }
        private static Collection<WordbookInfo> _wordbookList;
        #endregion

        #region private variables & properties
        private static Assembly m_WordbookAssembly;

        private static Wordbooks AllWordbook
        {
            get
            {

                if (_allWordbook == null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Wordbooks));
                    Stream wordbookConfig = BasicSettings.GetConfig(BasicSettings.WordbookSetting);
                    _allWordbook = serializer.Deserialize(wordbookConfig) as Wordbooks;
                }
                return _allWordbook;
            }
        }
        private static Wordbooks _allWordbook;
        #endregion

        #region private methods
        private static string ConvertBookNameToFullName(string name)
        {
            char[] separator = new char[] { '.' };
            string[] pieceOfName = name.Split(separator);
            if (pieceOfName.Length == 0)
                throw new ArgumentException("�ֵ�����Ϊ��");
            // �ֹ���Function������ת�����Ժ�ģ����������޸ĺ����ȥ��������
            if (pieceOfName[pieceOfName.Length - 1] == "Function")
                pieceOfName[pieceOfName.Length - 1] = "BiologyFunction";
            return "DrectSoft.Wordbook." + pieceOfName[pieceOfName.Length - 1];
        }
        #endregion

        #region public methods
        /// <summary>
        /// ͨ���ֵ����ƻ�ȡ��Ӧ���ֵ���ʵ��
        /// </summary>
        /// <param name="name">�ֵ����������ȫ�ƻ������+�ֵ�����</param>
        /// <returns>������ȷ��������Ӧʵ���������쳣</returns>
        public static BaseWordbook GetWordbook(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(MessageStringManager.GetString("NullParameterName"));
            // ��Assembly.LoadFrom()��Ҫ�����ͬӦ�ó���������⣬
            // �����ô˷�ʽ�õ��ֵ���ĳ��򼯣������з��䴦��
            // Ŀǰ��������������������к�Ӱ�죬������еĻ���ֻ�ܻ�����Ӳ����ķ�ʽ
            Type bookType = null;
            if (m_WordbookAssembly == null)
            {
                Assembly[] assemlys = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in assemlys)
                {
                    //MessageBox.Show(assembly.GetName().Name);
                    // ͨ���Ƚ�Assembly�����Ƶõ��ֵ���ĳ���
                    if (assembly.GetName().Name == "DrectSoft.Basic.Wordbook")
                    {
                        m_WordbookAssembly = assembly;
                        break;
                    }
                }
            }

            bookType = m_WordbookAssembly.GetType(ConvertBookNameToFullName(name));

            if (bookType == null)
            {
                //MessageBox.Show("Could not found the wordbook");
                return null;
            }
            else
            {
                BaseWordbook result = (BaseWordbook)System.Activator.CreateInstance(bookType); ;
                result.CacheTime = 0;
                return result;
            }
        }

        /// <summary>
        /// ͨ���ֵ����ƻ�ȡ��Ӧ���ֵ���ʵ��
        /// </summary>
        /// <param name="name">�ֵ�������ģ�ȫ�ƻ������+�ֵ�����</param>
        /// <returns>������ȷ��������Ӧʵ���������쳣</returns>
        public static BaseWordbook GetWordbookByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(MessageStringManager.GetString("NullParameterName"));


            var wordbooks = WordbookList.Where(wd => wd.Name.Equals(name));

            try
            {
                WordbookInfo info = wordbooks.First();
                return GetWordbook(info.TypeName);
            }
            catch
            {
                return null;
            }


        }

        /// <summary>
        /// ���ݴ����Wordbook�ؼ���Ϣ�ַ��������ֵ�ʵ��
        /// </summary>
        /// <param name="keyInfo">�ֵ�Ĺؼ���Ϣ��ɵ��ַ���</param>
        /// <returns>�ֵ���ʵ��</returns>
        public static BaseWordbook GetWordbookByString(string keyInfo)
        {
            if (String.IsNullOrEmpty(keyInfo))
                return null;
            //throw new ArgumentNullException("�ֵ�����Ϊ��");

            // �������ֵ�������
            int p = keyInfo.IndexOf(SeparatorSign.OtherSeparator);
            if (p < 0)
                return null;
            //throw new ArgumentException("δ�����ֵ�����");
            keyInfo = keyInfo.Substring(p + 3, keyInfo.Length - p - 3);
            p = keyInfo.IndexOf(SeparatorSign.OtherSeparator);
            if (p < 0)
                return null;

            BaseWordbook wordbook = GetWordbook(keyInfo.Substring(0, p));
            if (wordbook != null)
                wordbook.ParameterValueComb = keyInfo.Substring(p + 3, keyInfo.Length - p - 3);
            return wordbook;
        }

        /// <summary>
        /// ��ȡָ���ֵ�Ĳ�ѯ��䶨��
        /// </summary>
        /// <param name="bookName">ָ�����ֵ�����</param>
        /// <returns>��ѯ���</returns>
        public static string GetWordbookQuerySentence(string bookName)
        {
            Schema.Wordbook book = GetSourceWordbookByName(bookName);
            if (book != null)
                return PersistentObjectFactory.GetQuerySentenceByName(book.QuerySentence);
            else
                throw new ArgumentOutOfRangeException("ȱ��ָ���ֵ�Ĳ�ѯ��䶨��");
        }
        #endregion

        #region internal methods
        /// <summary>
        /// �����ֵ����ƣ��ҵ���Ӧ��ԭʼ���ֵ���ʵ��
        /// </summary>
        /// <param name="bookName">�ɷ��������ֵ�����ɵ�����</param>
        /// <returns></returns>
        internal static Schema.Wordbook GetSourceWordbookByName(string bookName)
        {
            string[] pieceOfBookName = bookName.Split('.');
            foreach (WordbookCatalog catalog in AllWordbook.Catalogs)
            {
                if (catalog.CatalogName != pieceOfBookName[0])
                    continue;

                foreach (Schema.Wordbook book in catalog.Wordbooks)
                    if (book.WordbookName == pieceOfBookName[1])
                        return book;
            }

            return null;
        }
        #endregion
    }
}
