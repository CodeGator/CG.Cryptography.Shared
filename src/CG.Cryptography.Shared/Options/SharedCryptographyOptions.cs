
namespace CG.Orange.Cryptography.Shared.Options;

/// <summary>
/// This class contains configuration settings related to cryptographic 
/// operations.
/// </summary>
public class SharedCryptographyOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a shared password, used for cryptographic 
    /// operations that are not tied to a specific user.
    /// </summary>
    [Required]
    [MinLength(12)]
    [MaxLength(60)]
    public string SharedPassword { get; set; } = null!;

    /// <summary>
    /// This property contains a shared salt, used for cryptographic 
    /// operations that are not tied to a specific user.
    /// </summary>
    [Required]
    [MinLength(12)]
    [MaxLength(60)]
    public string SharedSalt { get; set; } = null!;

    #endregion
}
