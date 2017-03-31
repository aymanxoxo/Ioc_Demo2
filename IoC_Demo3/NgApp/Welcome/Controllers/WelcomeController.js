angular.module('IocApp').controller('welcomeCtrl', function ($scope, $state, welcomeService, userAuthenticated) {
    $scope.welcomeMessage = 'This quick angular tutorial help to encrypt and decrypt variable using crypto.js. I am using Angularjs Crypto angular plugin for encryption and decryption.Crypto.js is very powerful library which is is used to encrypt and decrypt variable, forms data and any header parameters.You can create encrypted string using your salt code so that user could not decrypt your data. Normally programmer are using BASE64 string which can decrypt easily without any effort because they are using same salt or algorithm not user defined.There are a lot of online website providing functionality to decrypt BASE64 string. salt string is a user defined public key which is uses for encryption and decryption data.So both party have same public key(salt) to encrypt and decrypt data.';

    $scope.actions = {};

    $scope.actions.start = function () {
        if(userAuthenticated.get())
            $state.go('main.questions')
    }

    $scope.getUserAuth = function () {
        return userAuthenticated.get();
    }

});