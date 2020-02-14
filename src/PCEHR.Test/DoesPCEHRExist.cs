using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;
using System.Net;

namespace PCEHR.Test
{
  [TestClass]
  public class DoesPCEHRExist
  {
    [TestMethod]
    public void Run()
    {

      //Get Certificate and Header objects
      CertAndHeaderInfo CertAndHeaderInfo = Support.CertAndHeaderFactory.Get(
        certSerial: "06fba6",
        serialHPIO: "8003629900019338",
        patientType: Support.PatientType.CalebDerrington);

      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = CertAndHeaderInfo.Certificate;

      // Create PCEHR header
      CommonPcehrHeader header = CertAndHeaderInfo.Header;

      // Instantiate the client
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/doesPCEHRExist"
      // Production endpoint is "https://services.ehealth.gov.au/doesPCEHRExist"
      DoesPCEHRExistClient doesPcehrExistClient = new DoesPCEHRExistClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/doesPCEHRExist"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        // Invoke the service
        doesPCEHRExistResponse response = doesPcehrExistClient.DoesPCEHRExist(header);

        Console.WriteLine($"Success Full at : {DateTime.Now.ToString()}");
        Console.WriteLine($"PCEHR Exists: {response.PCEHRExists.ToString()}");
        Console.WriteLine($"Access Code Required: {response.accessCodeRequired.ToString()}");        
        Console.WriteLine($"AccessCodeRequiredSpecified: {response.accessCodeRequiredSpecified.ToString()}");
        
        // Get the soap request and response
        string soapRequest = doesPcehrExistClient.SoapMessages.SoapRequest;
        string soapResponse = doesPcehrExistClient.SoapMessages.SoapResponse;
      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }
  }
}
