using culqi.net;
using Domain.Entities;
using Domain.Repository.ParametroValor;
using Infrastructure.CrossCutting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Master
{
    public class PaymentCard : IPaymentCard
    {
        Security security = null;

        public PaymentCard(string public_key, string secret_key)
		{
           
			security = new Security();
            security.public_key = public_key;
            security.secret_key = secret_key;
		}

        public string CreateToken()
        {
            Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"card_number", "4111111111111111"},
				{"cvv", "123"},
				{"expiration_month", 09},
				{"expiration_year", 2020},
				{"email", "sisadri@gmail.com"}
			};
            return new Token(security).Create(map);
        }
        public string CreateCharge(Dictionary<string, object> map)
        {
            return new Charge(security).Create(map);
        }

        public void Dispose()
        {
            
        }
    }
}
