(function () {
    'use strict';

    angular
        .module('publicApp')
        .factory('viewRecipeService', ViewRecipeService);

    ViewRecipeService.$inject = ['$http', '$q'];

    function ViewRecipeService($http, $q) {
        return {
            getAllRecipes: _getAllRecipes
        };

        function _getAllRecipes() {
            return $http.get('/api/recipe/getall').then(success).catch(error);
        }

        function success(resp) {
            return resp;
        }

        function error(err) {
            return $q.reject(err);
        }
    }


})();