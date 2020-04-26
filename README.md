# GEncryption
Simple encryption library using AES encryption

## Usage
### To Encrypt
``` csharp
    IEncryptionService service = new EncryptionService();
    var enryptedText = service.Encrypt("plain text", "key", "salt");
```
### To Decrypt
``` csharp
    IEncryptionService service = new EncryptionService();
    var enryptedText = service.Decrypt("encrypted text", "key", "salt");
```

