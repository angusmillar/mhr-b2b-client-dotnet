using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.GetChangeHistoryView;

namespace PCEHR.Test
{
  [TestClass]
  public class GetChangeHistoryView
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
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getChangeHistoryView"
      // production endpoint is "https://services.ehealth.gov.au/getChangeHistoryView"
      GetChangeHistoryViewClient changeHistoryViewClient = new GetChangeHistoryViewClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getChangeHistoryView"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        // Invoke the service
        // if you run GetDocumentList first, you will get a list of document ids in the response
        // the "value" attribute in the <ns3:ExternalIdentifier> element provides the unique document id
        var changeHistoryView = changeHistoryViewClient.GetChangeHistoryView(
            header, new getChangeHistoryView() { documentID = "2.25.33982669477651998710317483962006911980" }
            );

      }
      catch (FaultException e)
      {
        // Handle any errors
      }
    }

  }
}
