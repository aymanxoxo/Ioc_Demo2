angular.module('IocApp')
.factory('encryptionService', function () {
    var service = {};

    service.encrypt = function (key, data) {
        var pKey = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(key));
        var rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
        rsa.FromXmlString(pKey);
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        var encryptedBytes = rsa.Encrypt(bytes, true);
        var encryptedString = System.Convert.ToBase64String(encryptedBytes);
        return encryptedString;
    }

    return service;
})