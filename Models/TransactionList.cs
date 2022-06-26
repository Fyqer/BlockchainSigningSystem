using System;
using System.Collections.Generic;

#nullable disable

namespace FCBlockchain.Models
{
    public partial class TransactionList
    {
        public int Id { get; set; }
        public int? Sender { get; set; }
        public int? Receiver { get; set; }
        public DateTime? Date { get; set; }
        public string SendAddress { get; set; }
        public string ReceiveAddress { get; set; }

        public virtual User ReceiverNavigation { get; set; }
        public virtual User SenderNavigation { get; set; }
    }
}
