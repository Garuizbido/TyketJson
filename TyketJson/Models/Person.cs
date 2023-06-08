using System;
namespace TyketJson.Models
{
	
		
        public class Address
        {
            public string formatted_address { get; set; }
            public string street_address { get; set; }
            public string city_name { get; set; }
            public string state_name { get; set; }
            public string country { get; set; }
            public string country_name { get; set; }
            public string zipcode { get; set; }
        }

        public class Person
        {
            public string id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string full_name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string phone_number { get; set; }
            public int age { get; set; }
            public string date_of_birth { get; set; }
            public double yearly_income { get; set; }
            public int followers { get; set; }
            public int following { get; set; }
            public string biography { get; set; }
            public string website { get; set; }
            public Address address { get; set; }
        }
}

