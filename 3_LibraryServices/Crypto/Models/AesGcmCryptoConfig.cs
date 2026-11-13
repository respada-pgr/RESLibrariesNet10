using System;
using System.Collections.Generic;
using System.Text;

namespace _3_LibraryServices.Crypto.Models
{
    public class AesGcmCryptoConfig
    {
        public int TagSizeBytes { get; set; } = 16;
    }
}
