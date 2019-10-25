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
using Nehta.VendorLibrary.PCEHR.DocumentRepository;

namespace PCEHR.Test
{
  [TestClass]
  public class GetDocument
  {
    [TestMethod]
    public void Run()
    {
      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = Support.CertificateHelper.GetCertificate();

      // Create PCEHR header  (See PcehrHeaderHelper.cs)
      CommonPcehrHeader header = Support.PcehrHeaderHelper.CreateHeaderDerringtonCaleb();
      
     
      // Create the client
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getDocument"
      // production endpoint is "https://services.ehealth.gov.au/getDocument"
      GetDocumentClient getDocumentClient = new GetDocumentClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getDocument"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      // Create a request
      List<RetrieveDocumentSetRequestTypeDocumentRequest> request =
          new List<RetrieveDocumentSetRequestTypeDocumentRequest>();

      // Set the details of the document to retrieve
      request.Add(new RetrieveDocumentSetRequestTypeDocumentRequest()
      {
        // This should be the value of the ExternalIdentifier "XDSDocumentEntry.uniqueId" in the GetDocumentList response
        DocumentUniqueId = "2.25.33982669477651998710317483962006911980",
        // This should be the value of "repositoryUniqueId" in the GetDocumentList response
        RepositoryUniqueId = "1.2.36.1.2001.1007.10.8003640002000050"
      });

      try
      {
        // Invoke the service
        RetrieveDocumentSetResponseType response = getDocumentClient.GetDocument(header, request.ToArray());
      }
      catch (FaultException e)
      {
        // Handle any errors
      }
    }


  }
}
