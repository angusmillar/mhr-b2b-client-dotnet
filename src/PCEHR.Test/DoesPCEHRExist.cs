using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCEHR.Sample;

namespace PCEHR.Test
{
  [TestClass]
  public class DoesPCEHRExist
  {
    [TestMethod]
    public void PerformDoesPCEHRExist()
    {
      var client = new DoesPCEHRExistClientSample();
      client.Sample();
    }
  }
}
