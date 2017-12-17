(function () {
    'use strict';
    var app = angular.module("publicApp" + '.routes', []);

    app.config(_configureStates);

    _configureStates.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider'];

    function _configureStates($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/main/home/home.html',
                title: 'home'
            })
            .state({
                name: 'share',
                url: '/recipe/make',
                templateUrl: '/app/main/recipes/makeRecipe.html',
                title: 'share',
                controller: 'recipeController as recipeCtrl'
            })
            .state({
                name: 'login',
                url: '/login',
                templateUrl: '/app/main/NAV/login/Login.html',
                title: 'login',
                controller: 'loginController as loginCtrl'
            })
            .state({
                name: 'register',
                url: '/register',
                templateUrl: '/app/main/NAV/register/Register.html',
                title: 'register',
                controller: 'registerController as registerCtrl'
            });
    }
})();