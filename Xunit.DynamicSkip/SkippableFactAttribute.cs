using Xunit;
using Xunit.Sdk;

namespace DynamicSkipExample
{
    [XunitTestCaseDiscoverer("DynamicSkipExample.XunitExtensions.SkippableFactDiscoverer", "DynamicSkip")]
    public class SkippableFactAttribute : FactAttribute { }
}