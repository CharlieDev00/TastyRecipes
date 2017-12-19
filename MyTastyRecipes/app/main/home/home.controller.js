(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('homeController', HomeController);

    HomeController.$inject = ['$location'];

    function HomeController($location) {
        var vm = this;
        vm.$onInit = _onInit;
        vm.$location = $location;
        vm.goToRecipes = _goToRecipes;

        function _onInit() {
            console.log('HomeController');
        }

        function _goToRecipes() {
            vm.$location.path('/viewRecipes');
        }
    }


})();