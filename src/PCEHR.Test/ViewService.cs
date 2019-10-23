using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCEHR.Sample;

namespace PCEHR.Test
{
  [TestClass]
  public class ViewService
  {
    [TestMethod]
    public void GetPathologyView()
    {
      var client = new GetViewClientSample();
      client.SampleForPathXmlResponses();
    }

    [TestMethod]
    public void GetHealthRecordOverview()
    {
      var client = new GetViewClientSample();
      client.SampleForHroResponse();
    }
  }
}
