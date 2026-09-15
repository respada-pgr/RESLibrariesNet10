using System;
using System.Collections.Generic;
using System.Text;

namespace _3_LibraryServicesNet10.Services.Crypto.Models
{
    public class Pbkdf2CryptoConfig
    {
        public int SaltSizeBytes { get; set; } = 16;
        public int IvSizeBytes { get; set; } = 12;
        public int KeySizeBytes { get; set; } = 32;
        public int TagSizeBytes { get; set; } = 16;
        public int Pbkdf2Iterations { get; set; } = 100_000;
    }
}
