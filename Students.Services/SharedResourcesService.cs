using System.Globalization;
using System.Resources;
using Students.Interfaces;
using Students.Resources;

namespace Students.Services;

public class SharedResourcesService : ISharedResourcesService
{
    #region Properties And Ctor

    public SharedResourcesService()
    {
    }

    #endregion // Properties And Ctor

    #region ISharedResourceService Implementation

    /// <inheritdoc/>
    public string GetString(string resourceId)
    {
        var currentCultureInfo = Thread.CurrentThread.CurrentCulture;
        var result = GetString(resourceId, currentCultureInfo);

        return result;
    }

    /// <inheritdoc/>
    public string GetString(string resourceId, CultureInfo cultureInfo)
    {
        string result = string.Empty;
        ResourceManager resourceManager;

        try
        {
            if (string.Equals(cultureInfo.Name, "pl-PL"))
            {
                resourceManager = new ResourceManager("Students.Resources.ResourcesPolish", typeof(ResourcesPolish).Assembly);
            }
            else if (string.Equals(cultureInfo.Name, "en-US") ||
                    string.Equals(cultureInfo.Name, "en-GB"))
            {
                resourceManager = new ResourceManager("Students.Resources.ResourcesEnglish", typeof(ResourcesEnglish).Assembly);
            }
            else
            {
                throw new NotSupportedException($"Culture {cultureInfo.Name} is not supported.");
            }

            if (resourceManager is not null)
            {
                var nullableResult = resourceManager.GetString(resourceId, cultureInfo);
                if (nullableResult is null)
                {
                    result = string.Empty;
                }
                else
                {
                    result = nullableResult.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }

        return result;
    }

    /// <inheritdoc/>
    public string this[string resourceId]
    {
        get
        {
            var result = GetString(resourceId);
            return result;
        }
    }

    #endregion // ISharedResourceService Implementation
}
