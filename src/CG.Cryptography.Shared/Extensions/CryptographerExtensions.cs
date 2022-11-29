
namespace CG.Cryptography;

/// <summary>
/// This class contains extension methods related to the <see cref="ICryptographer"/>
/// type.
/// </summary>
public static partial class CryptographerExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method encrypts the given string using AES and a shared set
    /// of credentials.
    /// </summary>
    /// <param name="cryptographer">The cryptographer to use for the 
    /// operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns an encrypted string.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async Task<string> AesEncryptAsync(
        this ICryptographer cryptographer,
        string value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptographer, nameof(cryptographer));

        // Can we take a shortcut?
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }
                
        // Can we get shared credentials?
        if (!cryptographer.TryGetSharedCredentials(
            out var sharedKey,
            out var sharedIV
            ))
        {
            // Panic!!
            throw new CryptographicException(
                message: $"Shared credentials not supported, or not available"
                );
        }

        // Call the overload.
        return await cryptographer.AesEncryptAsync(
            sharedKey,
            sharedIV,
            value,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method encrypts the given bytes using AES and a shared set
    /// of credentials.
    /// </summary>
    /// <param name="cryptographer">The cryptographer to use for the 
    /// operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns the encrypted 
    /// bytes.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async Task<byte[]> AesEncryptAsync(
        this ICryptographer cryptographer,
        byte[] value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptographer, nameof(cryptographer));

        // Can we take a shortcut?
        if (value is null || !value.Any())
        {
            return Array.Empty<byte>();
        }

        // Can we get shared credentials?
        if (!cryptographer.TryGetSharedCredentials(
            out var sharedKey,
            out var sharedIV
            ))
        {
            // Panic!!
            throw new CryptographicException(
                message: $"Shared credentials not supported, or not available"
                );
        }

        // Call the overload.
        return await cryptographer.AesEncryptAsync(
            sharedKey,
            sharedIV,
            value,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method decrypts the given string using AES and a shared set
    /// of credentials.
    /// </summary>
    /// <param name="cryptographer">The cryptographer to use for the 
    /// operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns a decrypted string.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async Task<string> AesDecryptAsync(
        this ICryptographer cryptographer,
        string value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptographer, nameof(cryptographer));

        // Can we take a shortcut?
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        // Can we get shared credentials?
        if (!cryptographer.TryGetSharedCredentials(
            out var sharedKey,
            out var sharedIV
            ))
        {
            // Panic!!
            throw new CryptographicException(
                message: $"Shared credentials not supported, or not available"
                );
        }

        // Call the overload.
        return await cryptographer.AesDecryptAsync(
            sharedKey,
            sharedIV,
            value,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <summary>
    /// This method decrypts the given bytes using AES and a shared set
    /// of credentials.
    /// </summary>
    /// <param name="cryptographer">The cryptographer to use for the 
    /// operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns the decrypted 
    /// bytes.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async Task<byte[]> AesDecryptAsync(
        this ICryptographer cryptographer,
        byte[] value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptographer, nameof(cryptographer));

        // Can we take a shortcut?
        if (value is null || !value.Any())
        {
            return Array.Empty<byte>();
        }

        // Can we get shared credentials?
        if (!cryptographer.TryGetSharedCredentials(
            out var sharedKey,
            out var sharedIV
            ))
        {
            // Panic!!
            throw new CryptographicException(
                message: $"Shared credentials not supported, or not available"
                );
        }

        // Call the overload.
        return await cryptographer.AesDecryptAsync(
            sharedKey,
            sharedIV,
            value,
            cancellationToken
            ).ConfigureAwait(false);
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method looks for shared credentials.
    /// </summary>
    /// <param name="cryptographer">The cryptographer to use for the operation.</param>
    /// <param name="sharedKey">A shared key, if the return value is <c>true</c>
    /// or and empty byte array, otherwise.</param>
    /// <param name="sharedIV">A shared initialization vector, if the return value 
    /// is <c>true</c> or and empty byte array, otherwise.</param>
    /// <returns><c>true</c> if the operation succeeds, <c>false</c> otherwise.</returns>
    private static bool TryGetSharedCredentials(
        this ICryptographer cryptographer,
        out byte[] sharedKey,
        out byte[] sharedIV
        )
    {
        // Make the compiler happy.
        sharedKey = Array.Empty<byte>();
        sharedIV = Array.Empty<byte>();

        // Ensure the type is valid.
        if (cryptographer is not SharedCryptographer)
        {
            return false; // No shared credentials!
        }

        // Get the shared credentials.
        sharedKey = (cryptographer as SharedCryptographer)?._sharedKey
            ?? Array.Empty<byte>();
        sharedIV = (cryptographer as SharedCryptographer)?._sharedIV
            ?? Array.Empty<byte>();

        // Ensure the credentials are valid.
        if (!sharedKey.Any() || !sharedIV.Any())
        {
            return false;  // No shared credentials!
        }

        // We got the credentials.
        return true;
    }

    #endregion
}
