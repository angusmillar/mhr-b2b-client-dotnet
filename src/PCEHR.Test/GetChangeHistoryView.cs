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
      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = Support.CertificateHelper.GetCertificate();

      // Create PCEHR header
      CommonPcehrHeader header = Support.PcehrHeaderHelper.CreateHeader(Support.PatientType.CalebDerrington);
      

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
