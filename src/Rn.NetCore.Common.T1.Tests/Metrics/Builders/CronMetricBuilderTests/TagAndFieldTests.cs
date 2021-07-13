using NUnit.Framework;
using Rn.NetCore.Common.Metrics.Builders;

namespace Rn.NetCore.Common.T1.Tests.Metrics.Builders.CronMetricBuilderTests
{
  [TestFixture]
  public class TagAndFieldTests
  {
    [Test]
    public void CronMetricBuilder_Tag_CronClass_ShouldReturn_ExpectedValue()
    {
      Assert.AreEqual(
        "cron_class",
        CronMetricBuilder.Tags.CronClass
      );
    }

    [Test]
    public void CronMetricBuilder_Tag_CronMethod_ShouldReturn_ExpectedValue()
    {
      Assert.AreEqual(
        "cron_method",
        CronMetricBuilder.Tags.CronMethod
      );
    }

    [Test]
    public void CronMetricBuilder_Tag_Category_ShouldReturn_ExpectedValue()
    {
      Assert.AreEqual(
        "category",
        CronMetricBuilder.Tags.Category
      );
    }

    [Test]
    public void CronMetricBuilder_Tag_SubCategory_ShouldReturn_ExpectedValue()
    {
      Assert.AreEqual(
        "sub_category",
        CronMetricBuilder.Tags.SubCategory
      );
    }


    [Test]
    public void CronMetricBuilder_Field_QueryCount_ShouldReturn_ExpectedValue()
    {
      Assert.AreEqual(
        "query_count",
        CronMetricBuilder.Fields.QueryCount
      );
    }

    [Test]
    public void CronMetricBuilder_Field_ResultsCount_ShouldReturn_ExpectedValue()
    {
      Assert.AreEqual(
        "results_count",
        CronMetricBuilder.Fields.ResultsCount
      );
    }
  }
}
