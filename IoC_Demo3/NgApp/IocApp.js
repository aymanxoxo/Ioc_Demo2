angular.module('IocApp', ['ngMessages', 'ui.router'])
.config(function ($stateProvider, $locationProvider) {
    $stateProvider
    .state('main',
        {
            views: {
                'login': {
                    templateUrl: '/NgApp/Account/Views/Login.html',
                    controller: 'loginCtrl'
                },
                'content': {
                    templateUrl: '/NgApp/Content/Views/content.html',
                    //controller: 'welcomeCtrl'
                }
            }
        })
    .state('main.questions', {
        templateUrl: '/NgApp/Questions/Views/questions.html',
        controller: 'questionsCtrl'

    })
    .state('main.welcome', {
        templateUrl: '/NgApp/Welcome/Views/welcome.html',
        controller: 'welcomeCtrl'

    })
    .state('main.register', {
        templateUrl: '/NgApp/Account/Views/Register.html',
        controller: 'registerCtrl'
    });
    $locationProvider.html5Mode(true);
})

angular.module('IocApp').controller('tempCtrl', function ($scope, $state) {
    $state.go('main.welcome');
});