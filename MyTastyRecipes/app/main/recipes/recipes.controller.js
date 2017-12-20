(function () {
    'use strict';

    angular
        .module('publicApp')
        .controller('recipeController', RecipeController);

    RecipeController.$inject = ['$scope', '$location','recipeService', 'recipeIngredientsService'];

    function RecipeController($scope, $location, RecipeService, RecipeIngredientsService) {
        var vm = this; 
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$onInit = _onInit;
        vm.recipeService = RecipeService;
        vm.fileUpload = _fileUpload;
        vm.fileUploadSuccess = _fileUploadSuccess;
        vm.fileUploadError = _fileUploadError;
        vm.createBaseSuccess = _createBaseSuccess;
        vm.createBaseError = _createBaseError;
        vm.getRecipeBase = _getRecipeBase;
        vm.getBaseSuccess = _getBaseSuccess;
        vm.getBaseError = _getBaseError;
        vm.updateRecipe = _updateRecipe;
        vm.updateRecipeSuccess = _updateRecipeSuccess;
        vm.updateRecipeError = _updateRecipeError;
        vm.cropper = {};
        vm.cropper.sourceImage = null;
        vm.cropper.croppedImage = null;
        vm.recipeBase = {};
        vm.recipeBaseContent = {};
        vm.uploadImage = {};
        vm.bounds = {};
        vm.bounds.left = 0;
        vm.bounds.right = 0;
        vm.bounds.top = 0;
        vm.bounds.bottom = 0;
        vm.name;
        vm.number;
        vm.measurementInput;
        vm.measurementOption;
        vm.timeInput;
        vm.timeOption;
        vm.yieldInput;
        vm.instructions;
        vm.ingredientInput;
        vm.recipeId;
        vm.hide = true;
        vm.show = true;
        vm.showAdd = true;
        vm.hideUpdate = true;
        vm.recipeContents = {};

        //will movce to its own controller at a later time
        vm.recipeIngredientsService = RecipeIngredientsService;
        vm.postIngredient = _postIngredient;
        vm.postIngredientSuccess = _postIngredientSuccess;
        vm.postIngredientError = _postIngredientError;
        vm.getAllIngredients = _getAllIngredients;
        vm.selectAllSuccess = _selectAllSuccess;
        vm.selectAllError = _selectAllError;
        vm.getIngredientById = _getIngredientById;
        vm.selectByIdSuccess = _selectByIdSuccess;
        vm.selectByIdError = _selectByIdError;
        vm.updateIngredient = _updateIngredient;
        vm.updateSuccess = _updateSuccess;
        vm.updateError = _updateError;
        vm.deleteIngredient = _deleteIngredient;
        vm.deleteSuccess = _deleteSuccess;
        vm.deleteError = _deleteError;
        vm.ingredients = {};
        vm.allIngredients = {};
        vm.selectedId;
        vm.update;

        vm.testFunction = _testFunction;

        function _onInit() {
            console.log('Recipe_Controller');
        }

        function _testFunction() {
            vm.ingredients = {
                'measurements': vm.measurementInput + ' ' + vm.measurementOption,
                'ingredient': vm.ingredientInput,
                'recipeId': vm.recipeId
            };

            console.log(vm.ingredients);
        }

        function _fileUpload() {
            var image = vm.cropper.croppedImage;
            var imageInfo = image.split(",");
            var getExtension = imageInfo[0].split("/");
            var extension = getExtension[1].split(";");
            vm.uploadImage.encodedImageFile = imageInfo[1];
            vm.uploadImage.fileExtension = "." + extension[0];

            vm.recipeService.fileUpload(vm.uploadImage)
                .then(vm.fileUploadSuccess)
                .catch(vm.fileUploadError);
        }

        function _fileUploadSuccess(resp) {
            console.log(resp.data);
            vm.recipeBase = {
                'name': vm.name,
                'imageUrl': resp.data
            }
            vm.recipeService.createBase(vm.recipeBase)
                .then(vm.createBaseSuccess)
                .catch(vm.createBaseError);
        }
       
        function _fileUploadError(err) {
            console.log(err);
        }

        function _createBaseSuccess(resp) {
            console.log(resp);
            vm.hide = false;
            vm.show = false;
            vm.recipeId = resp.data;

            vm.getRecipeBase(vm.recipeId);
        }

        function _createBaseError(err) {
            console.log(err);
        }

        function _getRecipeBase(id) {
            vm.recipeService.getBase(id)
                .then(vm.getBaseSuccess)
                .catch(vm.getBaseError);
        }

        function _getBaseSuccess(resp) {
            console.log(resp);
            vm.recipeBaseContent = resp.data;
        }

        function _getBaseError(err) {
            console.log(err);
        }

        function _updateRecipe() {
            vm.recipeContents = {
                'id': vm.recipeId,
                'name': vm.recipeBaseContent.Name,
                'imageUrl': vm.recipeBaseContent.FileId,
                'number': vm.timeInput,
                'time': vm.timeOption,
                'yields': vm.yieldsInput,
                'instructions': vm.instructions
            }
            vm.recipeService.updateRecipe(vm.recipeContents)
                .then(vm.updateRecipeSuccess)
                .catch(vm.updateRecipeError);
        }

        function _updateRecipeSuccess(resp) {
            console.log(resp);
            vm.$location.path('/viewRecipes');

        }

        function _updateRecipeError(err) {
            console.log(err);
        }

        //will move to its own controller at a later time
        function _postIngredient() {
            vm.ingredients = {
                'number': vm.measurementInput,
                'measurements': vm.measurementOption,
                'ingredient': vm.ingredientInput,
                'recipeId': vm.recipeId
            };
            vm.recipeIngredientsService.createIngredient(vm.ingredients)
                .then(vm.postIngredientSuccess)
                .catch(vm.postIngredientError);
        }

        function _postIngredientSuccess(resp) {
            console.log(resp);
            vm.measurementInput = null;
            vm.measurementOption = null;
            vm.ingredientInput = null;
            vm.getAllIngredients();
        }

        function _postIngredientError(err) {
            console.log(err);
        }

        function _getAllIngredients() {
            vm.recipeIngredientsService.selectAllIngredients()
                .then(vm.selectAllSuccess)
                .catch(vm.selectAllError);
        }

        function _selectAllSuccess(resp) {
            console.log(resp);
            vm.allIngredients = resp.data;
        }

        function _selectAllError(err) {
            console.log(err);
        }

        function _getIngredientById(id) {
            vm.recipeIngredientsService.selectIngredientById(id)
                .then(vm.selectByIdSuccess)
                .catch(vm.selectByIdError);
        }

        function _selectByIdSuccess(resp) {
            console.log(resp.data);
            vm.update = resp.data;
            vm.selectedId = vm.update.Id;
            vm.measurementInput = vm.update.Number;
            vm.measurementOption = vm.update.Measurements;
            vm.ingredientInput = vm.update.Ingredient;
            vm.showAdd = false;
            vm.hideUpdate = false;
        }

        function _selectByIdError(err) {
            console.log(err);
        }

        function _updateIngredient() {
            vm.ingredients = {
                'id': vm.selectedId,
                'number': vm.measurementInput,
                'measurements': vm.measurementOption,
                'ingredient': vm.ingredientInput,
                'recipeId': vm.recipeId
            };
            vm.recipeIngredientsService.updateIngredient(vm.ingredients)
                .then(vm.updateSuccess)
                .catch(vm.updateError);
        }

        function _updateSuccess(resp) {
            console.log(resp);
            vm.measurementInput = null;
            vm.measurementOption = null;
            vm.ingredientInput = null;
            vm.showAdd = true;
            vm.hideUpdate = true;
            vm.getAllIngredients();
        }

        function _updateError(err) {
            console.log(err);
        }

        function _deleteIngredient(id) {
            vm.recipeIngredientsService.deleteIngredient(id)
                .then(vm.deleteSuccess)
                .catch(vm.deleteError);
        }

        function _deleteSuccess(resp) {
            console.log(resp);
            vm.getAllIngredients();
        }

        function _deleteError(err) {
            console.log(err);
        }
    }


})();