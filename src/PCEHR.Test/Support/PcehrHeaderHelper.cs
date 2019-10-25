using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.VendorLibrary.PCEHR;

namespace PCEHR.Test.Support
{
  public static class PcehrHeaderHelper
  {
    public static string UploadingHPIO = "8003629900019338";
    public static CommonPcehrHeader CreateHeaderFrankHarding()
    {
      // Create the PCEHR header
      var pcehrHeader = new CommonPcehrHeader();
      pcehrHeader.IhiNumber = "8003608666701594";

      Console.WriteLine($"-Patient--------------------------------------------------------------");
      Console.WriteLine($"Name: Frank Harding");
      Console.WriteLine($"IHI: {pcehrHeader.IhiNumber}");
      Console.WriteLine($"----------------------------------------------------------------------");

      return Common(pcehrHeader);
    }

    public static CommonPcehrHeader CreateHeaderDerringtonCaleb()
    {
      var pcehrHeader = new CommonPcehrHeader();
      pcehrHeader.IhiNumber = "8003608000045922";

      Console.WriteLine($"-Patient--------------------------------------------------------------");
      Console.WriteLine($"Name: Derrington Caleb");
      Console.WriteLine($"IHI: {pcehrHeader.IhiNumber}");
      Console.WriteLine($"----------------------------------------------------------------------");
      return Common(pcehrHeader);
    }

    public static CommonPcehrHeader Common(CommonPcehrHeader CommonPcehrHeader)
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
      CommonPcehrHeader.OrganisationId = UploadingHPIO;

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