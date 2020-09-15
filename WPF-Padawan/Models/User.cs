using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace WPF_Padawan.Models
{
    class User
    {
        [BsonId]
        public int Id { get; set; }

        [BsonField]
        public string Name { get; set; }

        [BsonField]
        public string Adress { get; set; }

        [BsonField]
        public string PhoneNumber { get; set; }

        public User(string Name, string Adress, string PhoneNumber)
        {
            this.Name = Name;
            this.Adress = Adress;
            this.PhoneNumber = PhoneNumber;
        }
    }
}
