#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;

//using System.Runtime.Remoting.Messaging;

#endregion

namespace DrectSoft.Core
{
   ///// <summary>
   ///// Event
   ///// </summary>
   ///// <param name="sender"></param>
   ///// <param name="e"></param>
   //public delegate void TraceEventHandler(object sender, TraceEventArgs e);

   /// <summary>
   /// 跟踪类事件参数TraceEventArgs
   /// </summary>
   public class TraceEventArgs : EventArgs
   {
      private string _info;

      /// <summary>
      /// 跟踪信息
      /// </summary>
      public string Info
      {
         get { return _info; }
      }

      /// <summary>
      /// Ctor
      /// </summary>
      /// <param name="info"></param>
      public TraceEventArgs(string info)
      {
         _info = info;
      }

   }

   #region IAccount

   /// <summary>
   /// 帐户接口,处理一些帐户信息及操作
   /// </summary>
   public interface IAccount
   {
      /// <summary>
      /// 登录帐户的用户
      /// </summary>
      IUser User { get;}


      /// <summary>
      /// 用户登录函数
      /// </summary>
      /// <param name="userId">用户代码</param>
      /// <param name="password">密码</param>
      /// <param name="type">登录方式0：通过登录界面登录，1：医生工作站跳转</param>
      /// <returns>一个包含了用户信息的xml字符串</returns>
      IUser Login(string userId, string password,int type);
      /// <summary>
      /// 更改密码
      /// </summary>
      /// <param name="userId">用户代码</param>
      /// <param name="oldPassword">老密码</param>
      /// <param name="newPassword">新密码</param>
      void ChangePassword(string userId, string oldPassword, string newPassword);
      /// <summary>
      /// 创建一个新用户
      /// </summary>
      /// <param name="user">IUser接口对象</param>
      /// <param name="initPassword">初始密码</param>
      void CreateNewUser(IUser user, string initPassword);

      /// <summary>
      /// 更新用户的信息
      /// </summary>
      /// <param name="user">IUser接口对象</param>
      void UpdateUserInfo(IUser user);

      /// <summary>
      /// OnInfoAdded
      /// </summary>
      event EventHandler<TraceEventArgs> InfoAddedDelegate;

      /// <summary>
      /// Copy from Advanced .Net Remoting.CHM
      /// </summary>
      /// <param name="msg"></param>
      void BroadcastMessage(String msg);

      /// <summary>
      /// 判断帐户是否有模块权限
      /// </summary>
      /// <param name="assemblyName"></param>
      /// <param name="className"></param>
      /// <returns></returns>
      bool HasPermission(string assemblyName, string className);

      /// <summary>
      /// 判断帐户是否有方法权限
      /// </summary>
      /// <param name="assemblyName"></param>
      /// <param name="className"></param>
      /// <param name="functionName"></param>
      /// <returns></returns>
      bool HasPermission(string assemblyName, string className, string functionName);

      /// <summary>
      /// 判断是否为系统管理员
      /// </summary>
      /// <returns></returns>
      bool IsAdministrator();

      /// <summary>
      /// 通过用户Id得到用户名称和登录系统的权限,仅用于登录时使用
      /// </summary>
      /// <param name="userId"></param>
      /// <param name="system"></param>
      /// <param name="available"></param>
      /// <returns></returns>
      string GetAUserName(string userId, out bool available);
   }

   #endregion

   #region BroadcastEventWrapper

   /// <summary>
   /// Copy from Advanced .Net Remoting.CHM
   /// </summary>
   public class BroadcastEventWrapper : MarshalByRefObject
   {
      /// <summary>
      /// 自定义消息到达事件
      /// </summary>
      public event EventHandler<TraceEventArgs> MessageArrivedLocally;

      /// <summary>
      /// 调用定义的事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      public void LocallyHandleMessageArrived(object sender, TraceEventArgs e)
      {
         // forward the message to the client
         MessageArrivedLocally(sender, e);
      }

      /// <summary>
      /// 重载生命期设置
      /// </summary>
      /// <returns></returns>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public override object InitializeLifetimeService()
      {
         return null;
      }
   }
   #endregion
}

