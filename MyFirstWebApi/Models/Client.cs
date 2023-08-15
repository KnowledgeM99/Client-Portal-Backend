using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApi.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public string IdNumber { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CellNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Navigation property
        public ICollection<Policy> Policies { get; set; }
    }

    [Table("Policy")]
    public class Policy
    {
        [Key]
        public string PolicyNumber { get; set; }
        public decimal Premium { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal IntermediaryFee { get; set; }
        public decimal BrokerFee { get; set; }
        public DateTime PolicyStartDate { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }

        // Navigation property
        public Client Client { get; set; }
    }
}
