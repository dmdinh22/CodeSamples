aic.page.jobId = $("#workHistoryId").val();
console.log("viewmodel jobId:", aic.page.jobId);

(function () {
    "use strict";

    angular.module('aicApp')
        .controller('workHistoryController', WorkHistoryController);

    WorkHistoryController.$inject = ['$scope', '$baseController', '$workHistoryService', '$companyService', '$window', 'toastr'];

    function WorkHistoryController($scope, $baseController, $workHistoryService, $companyService, $window, $toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.$workHistoryService = $workHistoryService;
        //Added for search
        vm.$companyService = $companyService; // search functionality
        $baseController.merge(vm, $baseController);
        vm.notify = vm.$workHistoryService.getNotifier($scope);

        //initializing
        vm.jobItems = [];
        vm.jobItem = {}; //can be empty object or null for angularjs
        vm.submitBtnTxt = 'Submit'; // setting default button text
        vm.jobForm = {}; // initialize an empty object to set the parameter passed in
        vm.contractOptions = ["Yes", "No"];

        vm.maxStartDate = new Date(); // setmax start date to today
        vm.maxEndDate = new Date(); // setmax end date to today

        // setting view model id to current route id
        vm.jobId = aic.page.jobId;
        console.log("viewmodel jobId:", vm.jobId);

        // HOISTING
        vm.submitForm = _submitForm;
        vm.submitJobSuccess = _submitJobSuccess;
        vm.editJobSuccess = _editJobSuccess;
        vm.getJobById = _getJobById;
        vm.getJobByIdSuccess = _getJobByIdSuccess;

        //added for company search
        vm.selected = {};
        vm.select = _select;
        vm.reSearch = _reSearch;
        vm.getCompanySuccess = _getCompanySuccess;
        vm.refreshResults = _refreshResults;

        //  ------THE FOLD----- like the aic.startUP function
        render();

        function render() {
            console.log('rendering create/edit Work History...');

            //logic to check whether or not we're in EDIT mode
            if (vm.jobId) {
                console.log("jobId:", vm.jobId);

                // set the text of button to Edit
                vm.submitBtnTxt = 'Edit';

                // get data by id to hydrate the form
                vm.getJobById(vm.jobId);
            }
        }; //render

        function _submitForm(form) { //isValid

            // setting initialized object to param passed in
            vm.jobForm = form;
            // checking the object
            console.log(vm.jobForm);

            //console.log(vm.selected);

            if (vm.jobForm.$valid) { //isValid
                console.log('submitting new job!');
                
                if (vm.jobId) {
                    // calling PUT ajax service from our service page
                    console.log(vm.jobId);

                    vm.$workHistoryService.put(vm.jobId, vm.jobItem, vm.editJobSuccess);
                } else {

                    // setting the viewmodel object to the binded ng-models
                    vm.jobItem.personID = aic.page.personId;
                    vm.jobItem.companyID = vm.selected.id;
                    vm.jobItem.company = vm.selected.name;

                    // calling POST ajax service from aic.services.phone.js
                    vm.$workHistoryService.post(vm.jobItem, vm.submitJobSuccess);
                }
            }
        }; //_submitForm

        function _submitJobSuccess(data, textStatus, jqXHR) {
            console.log('submitted work history Successfully!');

            $toastr.success("Work history submitted succesfully!", { timeout: 2000 });

            location.replace(document.referrer);
        }; //_submitJobSuccess

        function _editJobSuccess(data, textStatus, jqXHR) {
            console.log('edited work history successfully!')

            $toastr.success("Work history edited succesfully!", { timeout: 2000 });

            location.replace(document.referrer);
        }; //_editJobSuccess

        function _getJobById() {
            console.log("in _getJobById", vm.jobId);

            // calling GET by id ajax service from our service page
            vm.$workHistoryService.getById(vm.jobId, vm.getJobByIdSuccess);
        }; //_getJobById

        function _getJobByIdSuccess(data, textStatus, jqXHR) {
            // this receives the data and calls the special
            //notify method that will trigger ng to refresh UI
            console.log("_getJobByIdSuccess");
            2
            console.log(data.item);

            if (data.item.personID != aic.page.personId) {
                window.location.href = '/profilepageng/landing'
            } else {

                vm.notify(function () {
                    vm.jobItem = data.item;

                    vm.selected.name = vm.jobItem.company;

                    vm.jobItem.dateStarted = new Date(vm.jobItem.dateStarted);
                    if (vm.jobItem.dateEnded != null) {
                        vm.jobItem.dateEnded = new Date(vm.jobItem.dateEnded);
                    } else {
                        vm.jobItem.dateEnded = new Date();
                    }
                })
            };
        }; //_getJobByIdSuccess

        // ## BELOW IS FOR COMPANY SEARCH ##

        // selecting the highlighted saerch result
        function _select() {
            vm.$workHistoryService.selection = vm.selected;
            console.log("LOOK HERE");
            console.log(vm.selected);
        }; //_select

        // this searches the database with the typed query and formats the query in the proper formatting
        function _reSearch(query) {
            // only fire off the search if the query is >= 3
            if (query.length >= 3) {
                vm.$workHistoryService.companySearch(query, _getCompanySuccess);
            }
        }; //_reSearch

        function _getCompanySuccess(data, textStatus, jqXHR) {
            vm.notify(function () {
                // setting the view model's items to the items property of the data object
                vm.jobItems = data.items;
            });
        }; //_getCompanySuccess

        //  use the refresh attribute in ui-select-choices directive 
        // to update the underlying list with the user input ($select.search).
        function _refreshResults($select) { //$select stands for the corresponding ui-select instance. 
            // setting a variable search for the 
            var search = $select.search,
                list = angular.copy($select.items),
                FLAG = -1;
            //remove last user input
            list = list.filter(function (item) {
                return item.id !== FLAG;
            });

            if (!search) {
                //use the predefined list
                $select.items = list;
            }
            else {
                //manually add user input and set selection
                var userInputItem = {
                    id: FLAG,
                    name: search
                };
                $select.items = [userInputItem].concat(list);
                $select.selected = userInputItem;
            }
        }
    }
})(); //WorkHistoryController