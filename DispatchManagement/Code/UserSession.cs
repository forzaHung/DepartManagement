using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement
{
    public class UserSession
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string ImagePath { get; set; }
        public string Token { get; set; }
    }
    public class MemberSession
    {
        public string SessionId { get; set; }
        public Guid AccID { get; set; }
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}