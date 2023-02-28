using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.Request;

public class BaseRequest
{
    public int ComPort { get; set; }
    public int Baudrate { get; set; }
}
