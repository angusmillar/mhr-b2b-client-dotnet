using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;
using System.Net;
using System.Net.Security;

namespace PCEHR.Test
{
  [TestClass]
  public class GainAccess
  {
    [TestMethod]
    public void Run()
    {
      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = Support.CertificateHelper.GetCertificate();
      
      // Create the PCEHR header
      var pcehrHeader = Support.PcehrHeaderHelper.CreateHeaderDerringtonCaleb();
     
      // Instantiate the client
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/gainPCEHRAccess"
      // production endpoint is "https://services.ehealth.gov.au/gainPCEHRAccess"
      GainPCEHRAccessClient gainPcehrAccessClient = new GainPCEHRAccessClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/gainPCEHRAccess"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      // Create the access request
      gainPCEHRAccessPCEHRRecord accessRequest = new gainPCEHRAccessPCEHRRecord();
      // if gaining access without a code, authorisationDetails is not required
      // if gaining access with a code, include authorisationDetails, accessCode and accessType
      // if gaining emergency access, include authorisationDetails and set accessType to “EmergencyAccess”
      // Only include the below to pass an access code or gain emergency access
      accessRequest.authorisationDetails = new gainPCEHRAccessPCEHRRecordAuthorisationDetails();

      // "patient access code" is not required if the patient has open access for there record
      accessRequest.authorisationDetails.accessCode = "";

      accessRequest.authorisationDetails.accessType = gainPCEHRAccessPCEHRRecordAuthorisationDetailsAccessType.AccessCode;


      gainPCEHRAccessResponseIndividual individual = new gainPCEHRAccessResponseIndividual();

      try
      {
        // Invoke the service
        responseStatusType responseStatus = gainPcehrAccessClient.GainPCEHRAccess(pcehrHeader, accessRequest, out individual);

        // Get the soap request and response
        string soapRequest = gainPcehrAccessClient.SoapMessages.SoapRequest;
        string soapResponse = gainPcehrAccessClient.SoapMessages.SoapResponse;
      }
      catch (FaultException fex)
      {
        // Handle any errors
      }

    }
  }
}
