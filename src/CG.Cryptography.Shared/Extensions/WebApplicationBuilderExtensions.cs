﻿
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static partial class WebApplicationBuilderExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds the services required to support cryptographic
    /// operations using shared credentials.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder
    /// to use for the operation.</param>
    /// <param name="sectionName">The configuration section to use for 
    /// the operation. This parameter defaults to <c>Cryptography</c></param>
    /// <param name="bootstrapLogger">The bootstrap logger to use for 
    /// the operation.</param>
    /// <param name="serviceLifetime">The service lifetime to use for the 
    /// operation. This parameter defaults to <c>ServiceLifetime.Singleton</c></param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddCryptographyWithSharedKeys(
        this WebApplicationBuilder webApplicationBuilder,
        string sectionName = "Cryptography",
        ILogger? bootstrapLogger = null,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

        // Tell the world what we are about to do.
        bootstrapLogger?.LogInformation(
            "Configuring shared cryptographic startup options from " +
            "the {section} section",
            sectionName
            );

        // Configure the shared cryptography options.
        webApplicationBuilder.Services.ConfigureOptions<SharedCryptographyOptions>(
            webApplicationBuilder.Configuration.GetSection(sectionName),
            out var options
            );

        // Tell the world what we are about to do.
        bootstrapLogger?.LogInformation(
            "Wiring up the cryptography library using {lifetime} lifetime",
            Enum.GetName(serviceLifetime)
            );

        // Register the shared cryptographer object.
        webApplicationBuilder.Services.Add<ICryptographer, SharedCryptographer>(
            serviceLifetime
            );

        // Return the application builder.
        return webApplicationBuilder;
    }

    #endregion
}
