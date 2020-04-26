using System;
using Xunit;

namespace GEncryption.Test
{
    public class UnitTest1
    {
        //string Encrypt(string toEncrypt, string encryptionKey, string salt)
        [Theory]
        [InlineData("", "test key")]
        [InlineData(" ", "test key")]
        [InlineData(null, "test key")]
        [InlineData("test salt", "")]
        [InlineData("test salt", " ")]
        [InlineData("test salt", null)]
        [InlineData(null, null)]
        [InlineData(" ", " ")]
        public void Encrypt_WithBlankValues_ThrowsException(string salt, string key)
        {
            //Arrange
            IEncryptionService service = new EncryptionService();
            var toEncrypt = "Plain text";

            //Act
            //Assert
            Exception exception = Assert.Throws<ArgumentException>(() => service.Encrypt(toEncrypt, key, salt));
        }

        [Theory]
        [InlineData("", "test key")]
        [InlineData(" ", "test key")]
        [InlineData(null, "test key")]
        [InlineData("test salt", "")]
        [InlineData("test salt", " ")]
        [InlineData("test salt", null)]
        [InlineData(null, null)]
        [InlineData(" ", " ")]
        public void Decrypt_WithInvalidParams_ThrowsException(string salt, string key)
        {
            //Arrange
            IEncryptionService service = new EncryptionService();
            var cypherText = "Gobly gook";

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => service.Decrypt(cypherText, key, salt));
        }

        [Theory]
        [InlineData("Test toEncrypt", "0kPQ+n3lUMNwyp87FW5gg1tZBt+oPlRqF7dKQr/NCEA=", "SaltySalt", "SecretKey")]
        [InlineData("Test toEncrypt", "jGb+fLJBMmgCzpaAZWjtDXNddTbjs9liSATF6J4XFBc=", "SaltySalt2", "SecretKey")]
        public void Encrypt_WithValidParams_ReturnsEncryptedText(string toEncrypt, string expected, string salt, string key)
        {
            //Arrange
            IEncryptionService service = new EncryptionService();

            //Act
            var actual = service.Encrypt(toEncrypt, key, salt);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0kPQ+n3lUMNwyp87FW5gg1tZBt+oPlRqF7dKQr/NCEA=", "Test toEncrypt", "SaltySalt", "SecretKey")]
        [InlineData("jGb+fLJBMmgCzpaAZWjtDXNddTbjs9liSATF6J4XFBc=", "Test toEncrypt", "SaltySalt2", "SecretKey")]
        public void Decrypt_WithValidParams_ReturnsOriginalText(string encrypted, string expected, string salt, string key)
        {
            //Arrange
            IEncryptionService service = new EncryptionService();

            //Act
            var actual = service.Decrypt(encrypted, key, salt);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
