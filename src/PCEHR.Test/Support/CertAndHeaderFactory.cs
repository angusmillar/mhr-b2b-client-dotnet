using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCEHR.Test.Support
{
  public static class CertAndHeaderFactory
  {
    public static CertAndHeaderInfo Get(string certSerial, string serialHPIO, PatientType patientType)
    {
      var result = new CertAndHeaderInfo();
      result.Certificate = Support.CertificateHelper.GetCertificate(certSerial);
      result.Header = Support.PcehrHeaderHelper.CreateHeader(patientType, serialHPIO);
      return result;
    }
  }
}
