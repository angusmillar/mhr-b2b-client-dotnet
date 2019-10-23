using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCEHR.Sample;

namespace PCEHR.Test
{
  [TestClass]
  public class GainAccess
  {
    [TestMethod]
    public void GainAccessToMHR()
    {
      var client = new GainPCEHRAccessClientSample();
      client.Sample();
    }
  }
}
