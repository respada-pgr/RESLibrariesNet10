using System;
using System.Collections.Generic;
using System.Text;

namespace _3_LibraryServicesNet10.Services.Crypto.Models
{
    public class AesGcmCryptoConfig
    {
        public int TagSizeBytes { get; set; } = 16;
    }
}
