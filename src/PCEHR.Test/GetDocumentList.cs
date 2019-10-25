﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace PCEHR.Test
{
  [TestClass]
  public class GetDocumentList
  {
    [TestMethod]
    public void Run()
    {      
      // NASH certificate should be used here, NOT the HI certificate the NASH certificate can be found in the NASH PKI Test Kit
      // certificate needs to be installed in the right place
      // The "Issue To" field of a NASH certificate looks like general (or something different)."HPI-O".electronichealth.net.au
      // "Serial Number" can be found in the details once the certificate is installed.e.g. in Windows, certificates can be found in Certs.msc

      // Obtain the certificate for use with TLS and signing
      X509Certificate2 cert = Support.CertificateHelper.GetCertificate();
      
      // Create PCEHR header
      CommonPcehrHeader header = Support.PcehrHeaderHelper.CreateHeaderDerringtonCaleb();      

      

      // Instantiate the client
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getDocumentList"
      // production endpoint is "https://services.ehealth.gov.au/getDocumentList"
      GetDocumentListClient documentListClient = new GetDocumentListClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getDocumentList"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      // Create a query 
      AdhocQueryBuilder adhocQueryBuilder = new AdhocQueryBuilder(header.IhiNumber, new[] { DocumentStatus.Approved });
      adhocQueryBuilder.ServiceStartTimeFrom = new ISO8601DateTime(new DateTime(2010, 10, 16));
      adhocQueryBuilder.ServiceStopTimeTo = new ISO8601DateTime(new DateTime(2019, 10, 25));
      adhocQueryBuilder.ClassCode = new List<ClassCodes>() { ClassCodes.PathologyResultReport };
      // To further filter documents, build on the adhocQueryBuilder helper functions
      // For example, filtering on document type
      // adhocQueryBuilder.ClassCode = new List<ClassCodes>() {ClassCodes.DischargeSummary};
      // See Table 3 XDSDocumentEntry Document Type and Class Code value set from 
      // the Document Exchange Service Technical Service Specification

      // Create the request using the query
      AdhocQueryRequest queryRequest = adhocQueryBuilder.BuildRequest();


      try
      {
        // Invoke the service
        AdhocQueryResponse queryResponse = documentListClient.GetDocumentList(header, queryRequest);

        // Get the soap request and response
        string soapRequest = documentListClient.SoapMessages.SoapRequest;
        string soapResponse = documentListClient.SoapMessages.SoapResponse;

        // Process data into a more simple model
        if (queryResponse.RegistryObjectList.ExtrinsicObject == null)
        {
          Console.WriteLine($"Total Documents: 0");
        }
        else
        {
          XdsRecord[] data = XdsMetadataHelper.ProcessXdsMetadata(queryResponse.RegistryObjectList.ExtrinsicObject);

          // For displaying the data in a list
          Console.WriteLine($"Total Documents {data.Count().ToString()}");
          foreach (var row in data)
          {
            Console.WriteLine($"----------------------------------------------------------------------");
            Console.WriteLine($"DocumentId: {row.documentId}");
            Console.WriteLine($"Repository Unique Id: {row.repositoryUniqueId}");
            Console.WriteLine($"ClassCode DisplayName: {row.classCodeDisplayName}");
            Console.WriteLine($"TypeCode DisplayName: {row.typeCodeDisplayName}");
            Console.WriteLine($"Status: {row.status}");
            Console.WriteLine($"Remove Reason: {row.removeReason}");
            Console.WriteLine($"Record Version: {row.recordVersion}");
            



            // Convert dates from UTC to local time
            //row.creationTimeUTC.ToLocalTime();
            //row.serviceStopTimeUTC.ToLocalTime();

            // Document name
            //row.classCodeDisplayName 

            // Organisation
            //row.authorInstitution.institutionName 

            // Organisation Type
            //row.healthcareFacilityTypeCodeDisplayName

            // Identifiers to retrieve the document
            //row.repositoryUniqueId  
            //row.documentId 
          }
        }        
      }
      catch (FaultException e)
      {
        // Handle any errors
      }
    }

  }
}