(function () {
    'use strict';

    angular
        .module('publicApp')
        .factory('recipeService', RecipeService);

    RecipeService.$inject = ['$http', '$q'];

    function RecipeService($http, $q) {
        return {
            createBase: _createBase,
            getBase: _getBase,
            fileUpload: _fileUpload,
            updateRecipe : _updateRecipe
        };

        function _createBase(data) {
            return $http.post('/api/recipe/create', data).then(success).catch(error);
        }

        function _getBase(id) {
            return $http.get('/api/recipe/getBaseRecipe/' + id).then(success).catch(error);
        }

        function _fileUpload(file) {
           return $http.post('/api/recipe/fileUpload', file).then(success).catch(error);
        }

        function _updateRecipe(data) {
            return $http.put('/api/recipe/update', data).then(success).catch(error);
        }

        function success(resp) {
            return resp;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();