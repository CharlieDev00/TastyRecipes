(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('viewRecipeController', ViewRecipeController);

    ViewRecipeController.$inject = ['$scope', 'viewRecipeService'];

    function ViewRecipeController($scope, ViewRecipeService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;
        vm.viewRecipeService = ViewRecipeService;
        vm.getAllRecipes = _getAllRecipes;
        vm.getAllRecipesSuccess = _getAllRecipesSuccess;
        vm.getAllRecipesError = _getAllRecipesError;
        vm.allRecipesContent = {};

        function _onInit() {
            console.log('ViewRecipeController');
            vm.getAllRecipes();
        }

        function _getAllRecipes() {
            vm.viewRecipeService.getAllRecipes()
                .then(vm.getAllRecipesSuccess)
                .catch(vm.getAllRecipesError);
        }

        function _getAllRecipesSuccess(resp) {
            console.log(resp);
            vm.allRecipesContent = resp.data;
        }

        function _getAllRecipesError(err) {
            console.log(err);
        }
    }


})();