using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.RemoveDocument;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCEHR.Sample;

namespace PCEHR.Test
{
  [TestClass]
  public class RemoveDocument
  {
    [TestMethod]
    public void Remove()
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

      // Create the client
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/removeDocument"
      // production endpoint is "https://services.ehealth.gov.au:443/removeDocument"
      RemoveDocumentClient removeDocumentClient = new RemoveDocumentClient(
          new Uri("https://b2b.ehealthvendortest.health.gov.au/removeDocument"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        var request = new removeDocument()
        {
          // this should be the value of the ExternalIdentifier "XDSDocumentEntry.uniqueId" in the GetDocumentList response
          documentID = "2.25.311256170906902265756795034001543718058",
          // reasonForRemoval should be one of:
          // removeDocumentReasonForRemoval.IncorrectIdentity
          // removeDocumentReasonForRemoval.ElectToRemove
          // removeDocumentReasonForRemoval.Withdrawn
          reasonForRemoval = removeDocumentReasonForRemoval.IncorrectIdentity
        };

        // Invoke the service
        var responseStatus = removeDocumentClient.RemoveDocument(header, request);

        // Get the soap request and response
        string soapRequest = removeDocumentClient.SoapMessages.SoapRequest;
        string soapResponse = removeDocumentClient.SoapMessages.SoapResponse;
      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }
    
  }


}

