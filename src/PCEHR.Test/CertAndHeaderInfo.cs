using Nehta.VendorLibrary.PCEHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PCEHR.Test
{
  public class CertAndHeaderInfo
  {
    public X509Certificate2 Certificate { get; set; }
    public CommonPcehrHeader Header { get; set; }
  }
}
