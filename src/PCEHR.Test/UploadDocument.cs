using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCEHR.Sample;
using System;
using System.IO;
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
  public class UploadDocument
  {
    [TestMethod]
    public void Upload()
    {
      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = Support.CertificateHelper.GetCertificate();

      // Create PCEHR header
      CommonPcehrHeader header = Support.PcehrHeaderHelper.CreateHeader(Support.PatientType.CalebDerrington);

      // Create the client
      // SVT endpoint is https://b2b.ehealthvendortest.health.gov.au/uploadDocument
      // production endpoint is https://services.ehealth.gov.au/uploadDocument
      UploadDocumentClient uploadDocumentClient = new UploadDocumentClient(
          new Uri("https://b2b.ehealthvendortest.health.gov.au/uploadDocument"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      byte[] packageBytes = File.ReadAllBytes(@"C:\temp\MyHealthRecordTools\CDAPackager\Output\LastOutputRun\CdaPackage.zip"); // Create a package

      // Create a request to register a new document on the PCEHR.
      // Create a request to register a new document on the PCEHR.
      // Format codes and format code names are not fixed, and it is recommended for them to be configurable.
      // formatCode is the Template Package ID for each clinical document, formatCodeName is the Document type
      // please find specific details for each clinical document type on https://digitalhealth.gov.au/implementation-resources/clinical-documents
      // formatCodeName can be read in Table 3 of the Document Exchange Service Technical Service Specification
      // For example (formateCodeName - formatCode):
      // "eHealth Dispense Record" - 1.2.36.1.2001.1006.1.171.5
      // "Pathology Report" - 1.2.36.1.2001.1006.1.220.4
      // "Diagnostic Imaging Report" - 1.2.36.1.2001.1006.1.222.4
      //  "Discharge Summary" - 1.2.36.1.2001.1006.1.20000.18
      ProvideAndRegisterDocumentSetRequestType request = uploadDocumentClient.CreateRequestForNewDocument(
          packageBytes,
          "1.2.36.1.2001.1006.1.20000.18",
          "Discharge Summary",
          //You must chooose a valid type below
          HealthcareFacilityTypeCodes.Hospitals,
          PracticeSettingTypes.GeneralHospital
          );

      // To supercede / amend an existing document, the same UploadDocument call is used. However, the request is 
      // prepared using the CreateRequestForReplacement function.

      // Note that the new document must have a different UUID/GUID to the one it is replacing.
      // the uuidOfDocumentToReplace must be converted to OID format and include the repository OID. 
      // (i.e. a document being replaced in the My Health Record repository is)

      //ProvideAndRegisterDocumentSetRequestType request = uploadDocumentClient.CreateRequestForReplacement(
      //   packageBytes,
      //   "1.2.36.1.2001.1006.1.220.4",
      //    "Pathology Report",
      //   HealthcareFacilityTypeCodes.Hospitals,
      //   PracticeSettingTypes.GeneralHospital,
      //   "2.25.311256170906902265756795034001543718058" //Document Id of doc to replace
      //   );

      // When uploading to the NPDR where the repository unique ID, document size and hash may need to be included
      // in the metadata, use the utility function below.

      // uploadDocumentClient.AddRepositoryIdAndCalculateHashAndSize(request, "REPOSITORY_UNIQUE_ID");

      try
      {
        // Invoke the service
        RegistryResponseType registryResponse = uploadDocumentClient.UploadDocument(header, request);

        // Get the soap request and response
        string soapRequest = uploadDocumentClient.SoapMessages.SoapRequest;
        string soapResponse = uploadDocumentClient.SoapMessages.SoapResponse;        
      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }

  }


}

