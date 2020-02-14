using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCEHR.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.GetView;
using Nehta.VendorLibrary.PCEHR.PrescriptionAndDispenseView;
using Nehta.VendorLibrary.PCEHR.HealthRecordOverview;
using Nehta.VendorLibrary.PCEHR.PathologyReportView;
using Nehta.VendorLibrary.PCEHR.AdvanceCarePlanningView;
using Nehta.VendorLibrary.PCEHR.DiagnosticImagingReportView;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;
using Nehta.VendorLibrary.PCEHR.MedicareOverview;

namespace PCEHR.Test
{
  [TestClass]
  public class ViewService
  {
    [TestMethod]
    public void GetPathologyView()
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
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
      // production endpoint is "https://services.ehealth.gov.au/getView"
      GetViewClient getViewClient = new GetViewClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getView"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        getView request = new getView()
        {
          // Creates a pathologyReportView
          view = new pathologyReportView()
          {
            //2 years = 365 * 2 Days
            fromDate = DateTime.Now.Subtract(new TimeSpan((365 * 2), 0, 0, 0)),
            toDate = DateTime.Now,
            // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
            // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
            // If the specification doesn't specify the version number then it is 1.0
            versionNumber = "1.0"
          }

        };

        var responseStatus = getViewClient.GetView(header, request);

        // Convert XML response into Class for pathologyReportView
        XmlDocument xml = new XmlDocument();
        xml.PreserveWhitespace = true;
        xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
        pathologyReportViewResponse data = new pathologyReportViewResponse();
        data = (pathologyReportViewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

        // Get the soap request and response
        string soapRequest = getViewClient.SoapMessages.SoapRequest;
        string soapResponse = getViewClient.SoapMessages.SoapResponse;

        foreach (var item in data.pathologyReport.OrderByDescending(x => x.reportInformation.CDAeffectiveTime))
        {
          Console.WriteLine($"");
          Console.WriteLine($"##################################################################################");
          Console.WriteLine($"DocumentId: {item.reportInformation.documentId}");
          Console.WriteLine($"DocumentLink: {item.reportInformation.documentLink}");
          Console.WriteLine($"ReportName: {item.reportInformation.reportName}");
          Console.WriteLine($"PathologistLocalReportId: {item.reportInformation.pathologistLocalReportId}");
          Console.WriteLine($"ReportStatus(code): {item.reportInformation.reportStatus.code}");
          Console.WriteLine($"DateTimeRequested: {item.testRequesterInformation.dateTimeRequested}");
          Console.WriteLine($"CDAeffectiveTime: {item.reportInformation.CDAeffectiveTime}");
          Console.WriteLine($"DateTimeReportAuthored: {item.reportInformation.dateTimeReportAuthored}");
          Console.WriteLine($"DateTimeAuthorisation: {item.reportInformation.dateTimeAuthorisation}");
          foreach (var Report in item.pathologyTestResult)
          {
            Console.WriteLine($"  -------------------------------------------------");
            Console.WriteLine($"  TestResultName(code): {Report.testResultName.code}");
            Console.WriteLine($"  TestResultName(displayName): {Report.testResultName.displayName}");
            Console.WriteLine($"  TestResultName(originalText): {Report.testResultName.originalText}");
            Console.WriteLine($"  PathologyDiscipline(code): {Report.pathologyDiscipline.code}");
            Console.WriteLine($"  OverallTestResultStatus(code): {Report.overallTestResultStatus.code}");
            Console.WriteLine($"  SpecimenCollectionDate: {Report.specimenCollectionDate}");
          }


        }

      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }

    [TestMethod]
    public void GetDiagnosticImagingView()
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
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
      // production endpoint is "https://services.ehealth.gov.au/getView"
      GetViewClient getViewClient = new GetViewClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getView"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        getView request = new getView()
        {
          // Creates a diagnosticImagingReportView
          view = new diagnosticImagingReportView()
          {
            //2 years = 365 * 2 Days
            fromDate = new DateTime(2016, 1, 1),
            toDate = DateTime.Now,
            // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
            // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
            // If the specification doesn't specify the version number then it is 1.0
            versionNumber = "1.0"
          }
        };

        var responseStatus = getViewClient.GetView(header, request);

        // Convert XML response into Class for diagnosticImagingReportView
        XmlDocument xml = new XmlDocument();
        xml.PreserveWhitespace = true;
        xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
        diagnosticImagingReportViewResponse data = new diagnosticImagingReportViewResponse();
        data = (diagnosticImagingReportViewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

        // Get the soap request and response
        string soapRequest = getViewClient.SoapMessages.SoapRequest;
        string soapResponse = getViewClient.SoapMessages.SoapResponse;

        foreach (var item in data.diagnosticImagingReport.OrderByDescending(x => x.reportInformation.CDAeffectiveTime))
        {
          Console.WriteLine($"");
          Console.WriteLine($"##################################################################################");
          Console.WriteLine($"DocumentId: {item.reportInformation.documentId}");
          Console.WriteLine($"DocumentLink: {item.reportInformation.documentLink}");
          Console.WriteLine($"ReportDescription: {item.reportInformation.reportDescription}");
          Console.WriteLine($"AccessionNumber: {item.reportInformation.accessionNumber}");
          Console.WriteLine($"ReportStatus(code): {item.reportInformation.reportStatus.code}");
          Console.WriteLine($"DateTimeRequested: {item.imagingRequesterInformation.dateTimeRequested}");
          Console.WriteLine($"CDAeffectiveTime: {item.reportInformation.CDAeffectiveTime}");
          Console.WriteLine($"DateTimeReportAuthored: {item.reportInformation.dateTimeReportAuthored}");
          Console.WriteLine($"DateTimeAuthorisation: {item.reportInformation.dateTimeAuthorisation}");
          foreach (var Report in item.imagingExaminationResult)
          {
            Console.WriteLine($"  -------------------------------------------------");
            Console.WriteLine($"  ExaminationResultName(code): {Report.examinationResultName.code}");
            Console.WriteLine($"  ExaminationResultName(displayName): {Report.examinationResultName.displayName}");
            Console.WriteLine($"  ExaminationResultName(originalText): {Report.examinationResultName.originalText}");
            Console.WriteLine($"  Modality(code): {Report.modality.code}");
            Console.WriteLine($"  OverallTestResultStatus(code): {Report.overallTestResultStatus.code}");
            Console.WriteLine($"  SpecimenCollectionDate: {Report.imagingServiceDateTime}");
          }
        }
      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }

    [TestMethod]
    public void GetHealthRecordOverview()
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
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
      // production endpoint is "https://services.ehealth.gov.au/getView"
      GetViewClient getViewClient = new GetViewClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getView"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        getView request = new getView()
        {
          // Creates a healthRecordOverView
          view = new healthRecordOverView()
          {
            clinicalSynopsisLength = 200,
            // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
            // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
            // If the specification doesn't specify the version number then it is 1.0
            versionNumber = "1.1"
          }
        };

        var responseStatus = getViewClient.GetView(header, request);

        // Get the soap request and response
        string soapRequest = getViewClient.SoapMessages.SoapRequest;
        string soapResponse = getViewClient.SoapMessages.SoapResponse;

        // Convert XML response into Class for healthRecordOverview
        XmlDocument xml = new XmlDocument();
        xml.PreserveWhitespace = true;
        xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
        healthRecordOverviewResponse data = new healthRecordOverviewResponse();
        data = (healthRecordOverviewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }

    [TestMethod]
    public void GetMedicareOverview()
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
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
      // production endpoint is "https://services.ehealth.gov.au/getView"
      GetViewClient getViewClient = new GetViewClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getView"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        getView request = new getView()
        {
          // For MedicareOverview
          view = new medicareOverview()
          {
            fromDate = DateTime.Now.AddDays(-10),
            toDate = DateTime.Now,
            //versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
            //https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
            //If the specification doesn't specify the version number then it is 1.0
            //Currently 1.0 and 1.1
            versionNumber = "1.1"
          }
        };

        var responseStatus = getViewClient.GetView(header, request);

        // Treat response like a getDocument - unzip package
        if (responseStatus.view != null)
        {
          var zipfile = responseStatus.view.data;
          // and Unzip
        }

        // Get the soap request and response
        string soapRequest = getViewClient.SoapMessages.SoapRequest;
        string soapResponse = getViewClient.SoapMessages.SoapResponse;
      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }

    [TestMethod]
    public void GetPrescriptionAndDispenseView()
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
      // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
      // production endpoint is "https://services.ehealth.gov.au/getView"
      GetViewClient getViewClient = new GetViewClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/getView"), cert, cert);

      // Add server certificate validation callback
      ServicePointManager.ServerCertificateValidationCallback += Support.CertificateHelper.ValidateServiceCertificate;

      try
      {
        getView request = new getView()
        {
          // For PrescriptionAndDispenseView
          view = new prescriptionAndDispenseView()
          {
            fromDate = DateTime.Now.AddDays(-10),
            toDate = DateTime.Now,
            //  // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
            //  // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
            //  // If the specification doesn't specify the version number then it is 1.0
            versionNumber = "1.0"
          }
        };

        var responseStatus = getViewClient.GetView(header, request);

        // Get the soap request and response
        string soapRequest = getViewClient.SoapMessages.SoapRequest;
        string soapResponse = getViewClient.SoapMessages.SoapResponse;

        // Treat response like a getDocument - unzip package
        if (responseStatus.view != null)
        {
          var zipfile = responseStatus.view.data;
          // and Unzip
        }

      }
      catch (FaultException fex)
      {
        // Handle any errors
      }
    }

    private static object DeserialiseElementToClass(XmlElement element, object doctype)
    {
      XmlDocument doc = new XmlDocument();
      doc.AppendChild(doc.ImportNode(element, true));
      XmlReader read = doc.CreateNavigator().ReadSubtree();
      XmlRootAttribute rootAttr = new XmlRootAttribute(element.LocalName);
      rootAttr.Namespace = element.NamespaceURI;
      XmlSerializer xs = new XmlSerializer(doctype.GetType(), rootAttr);
      object rv = xs.Deserialize(read);
      return (rv);
    }
  }
}
