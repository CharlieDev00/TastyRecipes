(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('recipeController', RecipeController);

    RecipeController.$inject = ['$scope'];

    function RecipeController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log('Recipe_Controller');
        }
    }


})();