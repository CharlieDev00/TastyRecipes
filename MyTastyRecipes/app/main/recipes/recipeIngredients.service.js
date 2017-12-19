(function () {
    'use strict';

    angular
        .module('publicApp')
        .factory('recipeIngredientsService', RecipeIngredientsService);

    RecipeIngredientsService.$inject = ['$http', '$q'];

    function RecipeIngredientsService($http, $q) {
        return {
            createIngredient: _createIngredient,
            selectAllIngredients: _selectAllIngredients,
            selectIngredientById: _selectIngredientById,
            updateIngredient: _updateIngredient,
            deleteIngredient: _deleteIngredient
        };

        function _createIngredient(data) {
            return $http.post('/api/ingredients/create', data).then(success).catch(error);
        }

        function _selectAllIngredients() {
            return $http.get('/api/ingredients/getall').then(success).catch(error);
        }

        function _selectIngredientById(id) {
            return $http.get('/api/ingredients/get/'+ id).then(success).catch(error);
        }

        function _updateIngredient(data) {
            return $http.put('/api/ingredients/update' ,data).then(success).catch(error);
        }
        function _deleteIngredient(id) {
            return $http.delete('/api/ingredients/delete/' + id).then(success).catch(error);
        }

        function success(resp) {
            return resp;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();