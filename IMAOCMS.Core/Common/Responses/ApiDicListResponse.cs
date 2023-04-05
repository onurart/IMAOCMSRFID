using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.Common.Responses;

public class ApiDicListResponse
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public Dictionary<int,int> Data { get; set; }
}
