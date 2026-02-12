using NUnit.Framework;
using Allure.Net.Commons;

[SetUpFixture]
public class AllureSetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        AllureLifecycle.Instance.CleanupResultDirectory();
    }
}