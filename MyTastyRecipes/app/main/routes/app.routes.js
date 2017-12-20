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
                title: 'home',
                controller: 'homeController as homeCtrl'
            })
            .state({
                name: 'links',
                url: '/bakeries',
                templateUrl: '/app/main/links/links.html',
                title: 'links',
                controller: 'linksController as linksCtrl'
            })
            .state({
                name: 'saved',
                url: '/SavedBakeries',
                templateUrl: '/app/main/links/savedLinks.html',
                title: 'saved',
                controller: 'savedLinksController as savedCtrl'
            })
            .state({
                name: 'recipe',
                url: '/viewRecipes',
                templateUrl: '/app/main/recipes/viewRecipes/recipes.html',
                title: 'recipe',
                controller: 'viewRecipeController as viewCtrl'
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