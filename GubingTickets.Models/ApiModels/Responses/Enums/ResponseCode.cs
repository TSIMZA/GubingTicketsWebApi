using System;
using System.Collections.Generic;
using System.Text;

namespace GubingTickets.Models.ApiModels.Responses.Enums
{
    public enum ResponseCode: byte
    {
        UnknownError=0,
        Success=1,
        DataNotFound = 2,

        SqlException=254,
        InvalidModel=255
    }
}
