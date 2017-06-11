(function () {
    "use strict";

    angular.module('aicApp')
        .controller('workHistoryController', WorkHistoryController);

    WorkHistoryController.$inject = ['$scope', '$baseController', '$workHistoryService', '$modalService', '$window', 'toastr'];

    function WorkHistoryController($scope, $baseController, $workHistoryService, $modalService, $window, $toastr) {

        var vm = this; // points to a new controller object
        vm.$scope = $scope; // sets scope to 'this' for view model
        vm.$workHistoryService = $workHistoryService;
        vm.$modalService = $modalService;

        vm.items = null;

        //initialize functions
        vm.getJob = _getJob;
        vm.getJobSuccess = _getJobSuccess;
        vm.goToEdit = _goToEdit;
        vm.goToCreate = _goToCreate;
        vm.deleteWorkHistory = _deleteWorkHistory;
        vm.deleteWorkHistorySuccess = _deleteWorkHistorySuccess;
        vm.openModal = _openModal;

        // -- this line to simulate inheritance
        $baseController.merge(vm, $baseController);

        // This is a wrapper for our smaill dependency on $scope
        vm.notify = vm.$workHistoryService.getNotifier($scope);
            
        // ------THE FOLD-----
        render();

        function render() {
            console.log('rendering...');

            vm.getJob();
        }; //render

        function _getJob() {
            vm.$workHistoryService.get(vm.getJobSuccess);
        }; //_getJob

        function _getJobSuccess(data, textStatus, jqXHR) {
            // this receives the data and calls the special
            //notify method that will trigger ng to refresh UI
            vm.notify(function () {
                vm.items = data.items;
                
            });
        }; //_getJobSuccess

        function _goToEdit(phone) {
            console.log(phone.id)

            $window.location.href = '/workhistoryNG/' + phone.id + '/edit';
        };//_goToEdit

        function _goToCreate() {
            console.log("In _goToCreate")

            $window.location.href = '/workhistoryNG/create';
        }; //_goToCreate

        function _deleteWorkHistory(job) {
            var index = vm.items.indexOf(job);
            var jobArray = vm.items;

            jobArray.splice(index, 1);
            vm.$workHistoryService.delete(job.id, vm.deleteWorkHistorySuccess);
        }

        function _deleteWorkHistorySuccess(data, textStatus, jqXHR) {
            console.log("Deleted successfully!");

            $toastr.success("Work history deleted succesfully!", { timeout: 2000 });
        }; //_deleteWorkHistorySuccess

        function _openModal(job) {

           var modalOptions = {
               closeButtonText: 'Cancel',
               actionButtonText: 'Delete',
               headerText: 'Delete Confirmation',
               bodyText: 'Are you sure you want to delete this work history entry?'
           };

           vm.$modalService.showModal({}, modalOptions).then(function (result) {
               var index = vm.items.indexOf(job);
               var jobArray = vm.items;

               jobArray.splice(index, 1);
               vm.$workHistoryService.delete(job.id, vm.deleteWorkHistorySuccess);
           });
        }; //_openModal

    }

})();
