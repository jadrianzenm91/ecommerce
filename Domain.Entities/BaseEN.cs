using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BaseEN
    {
            public DateTime D_DATE_CREATE { get; set; }
            public bool? B_ACTIVE { get; set; }
            public DateTime? D_DATE_UPDATE { get; set; }
            public string V_USER_UPDATE { get; set; }
            public string V_USER_CREATE { get; set; }
            public string V_USER { get; set; }
            public string V_Control { get; set; }
            public string V_DATE_CREATE
            {
                get
                {
                    if (D_DATE_CREATE == null)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return D_DATE_CREATE.ToString(Infrastructure.CrossCutting.Constantes.Formaters.FormatDateTime.ToString());
                    }
                }
            }
            public string V_DATE_UPDATE
            {
                get
                {
                    if (D_DATE_UPDATE == null)
                    {
                        return string.Empty;

                    }
                    else
                    {
                        return Convert.ToDateTime(D_DATE_UPDATE).ToString(Infrastructure.CrossCutting.Constantes.Formaters.FormatDateTime.ToString());
                    }
                }
            }
        
    }
}
