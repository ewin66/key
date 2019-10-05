
namespace DrectSoft.Core
{
    /// <summary>
    /// ȡ�����ݴ洢�Ķ���
    /// </summary>
    public static class DataAccessFactory
    {
        private static SqlDataAccess _sqlDataAccessInstance;

        /// <summary>
        /// �õ�һ�����ݷ��ʶ���
        /// </summary>
        /// <returns></returns>
        public static SqlDataAccess GetSqlDataAccess()
        {
            _sqlDataAccessInstance = new SqlDataAccess();
            return _sqlDataAccessInstance;
        }

        /// <summary>
        /// ͨ��ָ����DbName�õ����ݷ��ʶ���
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns>null: �������������ݿ�</returns>
        public static IDataAccess GetSqlDataAccess(string dbName)
        {
            _sqlDataAccessInstance = new SqlDataAccess(dbName);
            return _sqlDataAccessInstance;
        }

        /// <summary>
        /// ȡ��Cache�������������
        /// </summary>
        /// <returns></returns>
        public static string CacheComConfig
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Ĭ�ϵ����ݷ������ӡ�
        /// �Ժ����Զ���Ŀؼ������������Ҫʹ�����ݷ���ʱ�ô����ԡ�
        /// </summary>
        public static IDataAccess DefaultDataAccess
        {
            get
            {
                if (_defaultDataAccess == null)
                    _defaultDataAccess = new SqlDataAccess();

                return _defaultDataAccess;
            }
            set
            {
                if (value != null)
                    _defaultDataAccess = value;
            }
        }
        private static IDataAccess _defaultDataAccess;
    }
}

