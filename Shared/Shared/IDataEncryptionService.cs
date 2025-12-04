using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public interface IDataEncryptionService {
    string GenerateEncryptedString_Aes(string plainText, string key = "", string initVector = "");
    string GenerateDecryptedString_Aes(string cipherText, string key = "", string initVector = "");
}
