using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.VendorLibrary.PCEHR;

namespace PCEHR.Test.Support
{
  public static class PcehrHeaderHelper
  {
    
   //public static string UploadingHPIO = "8003629900019338";

    public static CommonPcehrHeader CreateHeader(PatientType patientType, string uploadingHPIO)
    {
      var pcehrHeader = new CommonPcehrHeader();
      string PatientName = string.Empty;
      switch (patientType)
      {
        case PatientType.FrankHarding:
          PatientName = "Frank Harding";
          pcehrHeader.IhiNumber = "8003608666701594";
          break;
        case PatientType.CalebDerrington:
          PatientName = "Caleb Derrington";
          pcehrHeader.IhiNumber = "8003608000045922";
          break;
        case PatientType.JackieHunt:
          PatientName = "Jackie Hunt";
          pcehrHeader.IhiNumber = "8003608500179916";
          break;
        default:
          break;
      }

      Console.WriteLine($"-Patient--------------------------------------------------------------");
      Console.WriteLine($"Name: {PatientName}");
      Console.WriteLine($"IHI: {pcehrHeader.IhiNumber}");
      Console.WriteLine($"----------------------------------------------------------------------");
      return Common(pcehrHeader, uploadingHPIO);
    }
    
    public static CommonPcehrHeader Common(CommonPcehrHeader CommonPcehrHeader, string uploadingHPIO)
    {
      //User ID should be a HPI-I if the user is HPI-I eligible (i.e. AHPRA registered)
      // If the user isn't HPI-I eligible (such as support staff and scientific staff) then set the user ID to a local ID and
      // Set pcehrHeader.UserIdType to PCEHRHeaderUserIDType.LocalSystemIdentifier;
      CommonPcehrHeader.UserId = "AngusM";
      CommonPcehrHeader.UserIdType = CommonPcehrHeaderUserIDType.LocalSystemIdentifier;
      CommonPcehrHeader.UserName = "Angus Millar";

      // "organisation name" and "organisation HPIO" can be found in the NASH PKI Test Kit
      // HPI-O is always 16 digits long
      CommonPcehrHeader.OrganisationName = "AngusADHA";
      CommonPcehrHeader.OrganisationId = uploadingHPIO;
      
      CommonPcehrHeader.ClientSystemType = CommonPcehrHeaderClientSystemType.CIS;

      // Below information can be found in the My Health Record Vendor Product Details Form
      // that you filled out and submitted
      CommonPcehrHeader.ProductPlatform = "Windows";
      CommonPcehrHeader.ProductName = "NEHTA HIPS";
      CommonPcehrHeader.ProductVersion = "6.1";
      CommonPcehrHeader.ProductVendor = "HIPS0001";

      return CommonPcehrHeader;
    }
  }
}