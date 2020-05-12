using System;
namespace Dech.Hal.Banking.Contracts
{
    public enum ExitCodes 
    {
        Failed = -1,
        Success = 0,
        SignToolNotInPath = 1,
        AssemblyDirectoryBad = 2,
        PFXFilePathBad = 4,
        PasswordMissing = 8,
        SignFailed = 16,
        UnknownError = 32
    }
}
