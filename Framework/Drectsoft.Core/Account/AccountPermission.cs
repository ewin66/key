using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DrectSoft.Core;

namespace DrectSoft.Core
{
    /// <summary>
    /// �ʻ�Ȩ��
    /// </summary>
    public class AccountPermission
    {
        private string _userId = string.Empty;
        private string _roleIds = string.Empty;
        private Collection<IPermission> _permission = new Collection<IPermission>();
        private PermissionDal _permissionDal;

        /// <summary>
        /// �û�����
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// ��ɫ�����б�
        /// </summary>
        public string RoleIds
        {
            get { return _roleIds; }
            set { _roleIds = value; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        private AccountPermission(Users user)
        {
            _userId = user.Id;
            _roleIds = user.GWCodes;
            _permissionDal = new PermissionDal();
        }

        private void GetPermission()
        {
            _permission = _permissionDal.GetAllRolePermission(_roleIds);
        }

        /// <summary>
        /// ����һ���û�������Ȩ�޼���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Collection<IPermission> GetUserPermission(Users user)
        {
            AccountPermission instance = new AccountPermission(user);
            instance.GetPermission();
            return instance._permission;
        }

    }
}
