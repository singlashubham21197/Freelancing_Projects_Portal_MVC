using System;

namespace Freelancing_Projects_Portal_MVC.Models
{
    //Models client details
    public class Client
    {
        //Client id
        public int Id { get; set; }

        //Client name
        public string Name { get; set; }

        //Client email
        public string Email { get; set; }

        //is a senior or not
        public Boolean Vetran { get; set; }
    }
}
