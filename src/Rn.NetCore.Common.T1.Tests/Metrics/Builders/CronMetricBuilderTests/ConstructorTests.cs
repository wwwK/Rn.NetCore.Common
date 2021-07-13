using NUnit.Framework;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Enums;

namespace Rn.NetCore.Common.T1.Tests.Metrics.Builders.CronMetricBuilderTests
{
  [TestFixture]
  public class ConstructorTests
  {
    [Test]
    public void CronMetricBuilder_Given_Constructed_ShouldDefault_Tags()
    {
      // arrange
      var builder = new CronMetricBuilder().Build();

      // assert
      Assert.AreEqual(string.Empty, builder.Tags[CronMetricBuilder.Tags.CronClass]);
      Assert.AreEqual(string.Empty, builder.Tags[CronMetricBuilder.Tags.CronMethod]);
      Assert.AreEqual(string.Empty, builder.Tags[CronMetricBuilder.Tags.Category]);
      Assert.AreEqual(string.Empty, builder.Tags[CronMetricBuilder.Tags.SubCategory]);
      Assert.AreEqual(string.Empty, builder.Tags[CoreMetricTag.CustomTag1]);
      Assert.AreEqual(string.Empty, builder.Tags[CoreMetricTag.CustomTag2]);
      Assert.AreEqual(string.Empty, builder.Tags[CoreMetricTag.CustomTag3]);
      Assert.AreEqual(string.Empty, builder.Tags[CoreMetricTag.CustomTag4]);
      Assert.AreEqual(string.Empty, builder.Tags[CoreMetricTag.CustomTag5]);
    }

    [Test]
    public void CronMetricBuilder_Given_Constructed_ShouldDefault_Fields()
    {
      // arrange
      var builder = new CronMetricBuilder().Build();

      // assert
      Assert.AreEqual(0, builder.Fields[CronMetricBuilder.Fields.QueryCount]);
      Assert.AreEqual(0, builder.Fields[CronMetricBuilder.Fields.ResultsCount]);
      Assert.AreEqual(0, builder.Fields[CoreMetricField.CustomInt1]);
      Assert.AreEqual(0, builder.Fields[CoreMetricField.CustomInt2]);
      Assert.AreEqual(0, builder.Fields[CoreMetricField.CustomInt3]);
      Assert.AreEqual(0, builder.Fields[CoreMetricField.CustomInt4]);
      Assert.AreEqual(0, builder.Fields[CoreMetricField.CustomInt5]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomLong1]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomLong2]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomLong3]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomTiming1]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomTiming2]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomTiming3]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomTiming4]);
      Assert.AreEqual((long) 0, builder.Fields[CoreMetricField.CustomTiming5]);
    }
  }
}
