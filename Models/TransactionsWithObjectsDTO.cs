using System;
using System.Collections.Generic;

#nullable disable

namespace FCBlockchain.Models
{
    public class TransactionsWithObjectsDTO
    {
        public int id { get; set; }
        public int? Sender { get; set; }
        public int? Receiver { get; set; }
        public string SendAddress { get; set; }
        public string ReceiveAddress { get; set; }
        public object SendObject { get; set; }
        public object ReceiveObject { get; set; }
        public DateTime? Data { get; set; }
    }
}
