using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.BizBus.Service {
    /// <summary>
    /// ����key������Ψһ��ʾ����ÿһ�����񶼿����ø�keyΨһ��ʾ
    /// ��ʵ������ʹ��fullname����Ϊkey
    /// </summary>
    public class ServiceKey {
        private Type type;      //��������
        private string id;      //����id

        /// <summary>
        /// ���캯��
        /// </summary>
        public ServiceKey()
            : this(null, null) {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="type">��������</param>
        /// <param name="id">����id</param>
        public ServiceKey(Type type, string id) {
            this.type = type;
            this.id = id;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="desc">��������</param>
        public ServiceKey(ServiceDesc desc) {
            type = desc.ServiceType;
            id = desc.Key;
        }

        /// <summary>
        /// ��ȡ����ID
        /// </summary>
        public string ID {
            get { return id; }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public Type ServiceType {
            get { return type; }
        }

        /// <summary>
        /// �е�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            ServiceKey other = obj as ServiceKey;

            if (other == null)
                return false;

            return (Equals(type, other.type) && Equals(id, other.id));
        }

        /// <summary>
        /// ��ϣֵ
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            int hashForType = type == null ? 0 : type.GetHashCode();
            int hashForID = id == null ? 0 : id.GetHashCode();
            return hashForType ^ hashForID;
        }
    }
}
