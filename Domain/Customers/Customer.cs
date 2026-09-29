using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers
{

    public class Customer
    {
        public Guid Id { get; private set; }
        public UserId UserId { get; private set; }
        public string Email { get; private set; } = null!;

        private Customer() { }

        public Customer(UserId userId, string email)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Email = email;
        }
    }
}
