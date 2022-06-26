using System;
using System.Collections.Generic;

#nullable disable

namespace FCBlockchain.Models
{
    public partial class User
    {
        public User()
        {
            TransactionListReceiverNavigations = new HashSet<TransactionList>();
            TransactionListSenderNavigations = new HashSet<TransactionList>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<TransactionList> TransactionListReceiverNavigations { get; set; }
        public virtual ICollection<TransactionList> TransactionListSenderNavigations { get; set; }
    }
}
