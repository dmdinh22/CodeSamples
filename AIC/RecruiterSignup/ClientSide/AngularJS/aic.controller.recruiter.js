        (function () {
            "use strict";
            angular.module('aicApp').controller('recruiterController', RecruiterController);

            RecruiterController.$inject = ['$scope', '$http', '$baseController', '$recruiterService', '$timeout', '$fileUploadService', 'toastr'];

            function RecruiterController($scope, $http, $baseController, $recruiterService, $timeout, $fileUploadService) {
                var vm = this;
                vm.$scope = $scope;
                vm.$recruiterService = $recruiterService;
                vm.$fileUploadService = $fileUploadService;
                vm.postRecruiter = _postRecruiter;
                vm.postRecruiterSuccess = _postRecruiterSuccess;
                vm.submit = _submit;
                vm.$http = $http;
                vm.buttonDisable = 0;
                vm.toggleForms = _toggleForms;
                vm.resetForms = _resetForms;
                vm.showBusinessInfoOne = true;
                vm.showBusinessInfoTwo = false;
                vm.handleFileSelect = _handleFileSelect;
                vm.uploadPicSuccess = _uploadPicSuccess;
                vm.getByPersonIdSuccess = _getByPersonIdSuccess;
                vm.regex = '^((https?|ftp)://)?([A-Za-z]+\\.)?[A-Za-z0-9-]+(\\.[a-zA-Z]{1,4}){1,2}(/.*\\?.*)?$';


                $baseController.merge(vm, $baseController);
                vm.notify = vm.$recruiterService.getNotifier($scope);

                render()

                function render() {
                    if ($.inArray("Recruiter", aic.page.roles) >= 0) {
                        return self.location = '/Recruiter/Dashboard';
                    }
                }

                function _postRecruiter(payload) {
                    vm.$fileUploadService.UploadPicAJAX(vm.companyLogoData, vm.uploadPicSuccess);
                }

                function _postRecruiterSuccess(data, textStatus, jqXHR) {
                    toastr.success("We are processing your request. Please login again to verify your new credentials.", { timeOut: 3000 });
                    $timeout(function () {
                        self.location = '/Home/Index';
                    }, 2000);
                }

                function _resetForms(form) {
                    console.log(form)
                    vm.payload = {};
                    form.$setPristine();
                    vm.showBusinessInfoOne = true;
                    vm.showBusinessInfoTwo = false;
                }

                function _toggleForms() {
                    vm.showBusinessInfoOne = false;
                    vm.showBusinessInfoTwo = true;
                }

                function _submit(form) {
                    console.log(vm.payload);
                    vm.buttonDisable = 1;
                    $timeout(function () {
                        vm.buttonDisable = 0;
                    }, 4000);
                    vm.postRecruiter(vm.payload);
                }

                function _uploadPicSuccess(data, textStatus, jqXHR) {
                    console.log(data)
                    vm.notify(function () {
                        vm.$fileUploadService.getFileByFileId(data.item, vm.getByPersonIdSuccess);
                    });
                }

                function _getByPersonIdSuccess(data, textStatus, jqXHR) {
                    vm.notify(function () {
                        if (data.item == null) {
                            vm.payload.CompanyLogoUrl = "http://www.jobztime.com/companies_logos/logo.png";
                            vm.$recruiterService.PostRecruiter(vm.payload, vm.postRecruiterSuccess);
                        } else {
                            vm.payload.CompanyLogoUrl = data.item.fileUrl;
                            vm.$recruiterService.PostRecruiter(vm.payload, vm.postRecruiterSuccess);
                        }


                    });

                }

                function _handleFileSelect(evt) {
                    vm.file = evt.currentTarget.files[0];
                    var formData = new FormData();
                    formData.append("CompanyLogo", vm.file, "CompanyLogo");
                    vm.companyLogoData = formData;

                };
                angular.element(document.querySelector('#companyLogo')).on('change', vm.handleFileSelect);
            }
        })();