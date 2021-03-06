﻿using Core.Dao;
using LogicUniversityStore.Model;
using LogicUniversityStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controller
{
    public class NotificationController
    {
        NotificationDao objDao;
        public NotificationController()
        {
            objDao = new NotificationDao();
        }

        public void CreateNotification(int senderId, string message, NotificationStatus status, Roles receiverRole, int receiverDeptId)
        {
            Notification objNot = new Notification()
            {
                Description = message,
                SenderUserID = senderId,
                status = status.ToString(),
                ReceiverRoleID = (int)receiverRole,
                ReceiverRole = objDao.db.Roles.Where(rol => rol.RoleID == (int)receiverRole).FirstOrDefault(),
                ReceiverDeptID = receiverDeptId
            };
            objDao.CreateNotification(objNot);
        }

        public void ChangeNotification(int userId, int notId, NotificationStatus status)
        {
            objDao.ClearNotification(userId, notId, status);
        }

        public List<Notification> GetNotificationsByRoleCode(string roleCode, int deptId)
        {
            return objDao.GetNotificationsByRoleCode(roleCode, deptId);
        }

        public List<Notification> GetNotificationsByRoleCodeAnd(string roleCode, string notType)
        {
            return objDao.GetNotificationsByRoleCodeAndType(roleCode, notType);
        }

        public void SendEmail(string from, string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(from);
                to.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(val => mail.To.Add(new MailAddress(val)));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vasu4dworld@gmail.com", "Test@321!");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
