
namespace CG.Cryptography.Shared;

/// <summary>
/// This class is a shared implementation of the <see cref="ICryptographer"/>
/// interface.
/// </summary>
internal class SharedCryptographer : CryptographerBase
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the shared key for this cryptographer.
    /// </summary>
    internal protected readonly byte[] _sharedKey;

    /// <summary>
    /// This field contains the shared IV for this cryptographer.
    /// </summary>
    internal protected readonly byte[] _sharedIV;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="SharedCryptographer"/>
    /// class.
    /// </summary>
    /// <param name="sharedCryptographyOptions">The shared cryptography 
    /// options to use with this cryptographer.</param>
    /// <param name="logger">The logger to use with this class.</param>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more parameters are missing, or invalid.</exception>
    public SharedCryptographer(
        IOptions<SharedCryptographyOptions> sharedCryptographyOptions,
        ILogger<ICryptographer> logger
        ) : base(logger)
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(sharedCryptographyOptions, nameof(sharedCryptographyOptions));

        // Generate the shared credentials.
        var tuple = GenerateKeyAndIVAsync(
            sharedCryptographyOptions.Value.SharedPassword,
            sharedCryptographyOptions.Value.SharedSalt
            ).Result;

        // Save the results.
        _sharedKey = tuple.Item1;
        _sharedIV = tuple.Item2;    
    }

    #endregion
}
