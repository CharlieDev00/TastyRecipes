﻿(function () {
    'use strict';
    var app = angular.module("publicApp" + '.routes', []);

    app.config(_configureStates);

    _configureStates.$inject = ['$stateProvider', '$locationProvider'];

    function _configureStates($stateProvider, $locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/main/home/home.html',
                title: 'home'
            });
    }
})();