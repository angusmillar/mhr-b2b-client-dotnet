using System;
using Nehta.VendorLibrary.Common;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace PCEHR.Test.Support
{
  public static class CertificateHelper
  {
    //My Certificate
    public static string CertificateSerialNumber = "06fba6";
    
    //Sonic's Cert
    //public static string CertificateSerialNumber = "07248d";
    public static X509Certificate2 GetCertificate()
    {
      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = X509CertificateUtil.GetCertificate(
          CertificateSerialNumber,
          X509FindType.FindBySerialNumber,
          StoreName.My,
          StoreLocation.LocalMachine,
          true
          );
      return cert;
    }

    public static X509Certificate2 GetCertificate(string certSerial)
    {
      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = X509CertificateUtil.GetCertificate(
          certSerial,
          X509FindType.FindBySerialNumber,
          StoreName.My,
          StoreLocation.LocalMachine,
          true
          );
      return cert;
    }

    public static bool ValidateServiceCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
      // Checks can be done here to validate the service certificate.
      // If the service certificate contains any problems or is invalid, return false. Otherwise, return true.
      // This example returns true to indicate that the service certificate is valid.
      return true;
    }
  }
}