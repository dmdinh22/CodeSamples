
        (function () {
            "use strict"

            angular.module('aicApp')
                .factory('$userService', userServiceFactory);

            userServiceFactory.$inject = ['$baseService', '$aic'];

            function userServiceFactory($baseService, $aic) {
                var aaicServiceObject = aic.services.user;

                var newService = $baseService.merge(true, {}, aaicServiceObject, $baseService);
                newService.selection = {};

                return newService;
            }
        })(); // userServiceFactory

        (function () {
            "use strict";

            angular.module('aicApp')
                .controller('userController', UserController)

            UserController.$inject = ['$scope', '$baseController', '$userService', 'toastr', '$modal', '$messageService', '$modalService', '$workHistoryService', '$jobService', '$educationService'];

            function UserController($scope, $baseController, $userService, $toastr, $modal, $messageService, $modalService, $workHistoryService, $jobService, $educationService) {
                var vm = this;

                //instantiating vars
                vm.items = [];
                vm.myEdu = [];
                vm.myJobs = [];
                vm.matchedItems = [];
                vm.jobItem = {};
                vm.newConvo = {};
                vm.jobCoName = {};
                vm.companyItem = {};
                vm.referralRequest = {};
                vm.referralJobItem = null;
                vm.currentUserId = aic.page.personId;
                vm.pageSearch = null;
                vm.searchQuery = $("#searchQuery").val();
                vm.jobId = window.location.href.substr(window.location.href.lastIndexOf('/') + 1); //$("jobId").val();

                // Dependency injections
                vm.$userService = $userService;
                vm.$scope = $scope;
                vm.$modal = $modal;
                vm.$modalService = $modalService;
                vm.$messageService = $messageService;
                vm.toastr = toastr;
                vm.$workHistoryService = $workHistoryService;
                vm.$jobService = $jobService;
                vm.$educationService = $educationService;

                $baseController.merge(vm, $baseController);
                vm.notify = vm.$userService.getNotifier($scope);

                // HOISTING
                vm.setImage = _setImage;
                vm.searchUser = _searchUser;
                vm.getSearchSuccess = _getSearchSuccess;
                vm.email = _email;
                vm.getProfiles = _getProfiles;
                vm.getProfilesSuccess = _getProfilesSuccess;
                vm.getMyJobs = _getMyJobs;
                vm.getMyJobSuccess = _getMyJobSuccess;
                vm.openModal = _openModal;
                vm.postConversationMsg = _postConversationMsg;
                vm.postConversationMsgSuccess = _postConversationMsgSuccess;
                vm.requestReferralSuccess = _requestReferralSuccess;
                vm.getJobInfo = _getJobInfo;
                vm.getJobInfoSuccess = _getJobInfoSuccess;
                vm.intersect = _intersect;
                vm.arrayMapper = _arrayMapper;
                vm.distinct = _distinct;
                vm.getMyEdu = _getMyEdu;
                vm.getMyEduSuccess = _getMyEduSuccess;

                // Server Pagination
                vm.pageNum = 1;
                vm.pageSize = 5;
                vm.totalRows = null;
                vm.pageChanged = _pageChanged;

                // THE FOLD
                render();

                function render() {
                    console.log("person id:", aic.page.personId);

                    vm.getMyJobs();
                    vm.getMyEdu();
                    vm.getJobInfo();

                } //render

                function _setImage(item) {
                    if (item.profileImageUrl == null) {
                        return '/public-assets/images/people/defaultAvatar.jpg';
                    }
                    else {
                        return 'https://aic-training.s3-us-west-2.amazonaws.com/C30/' + item.profileImageUrl;
                    }
                } //_setImage

                function _searchUser() {
                    if (vm.searchQuery != null) {
                        vm.$userService.userSearchUser(vm.pageSearch, vm.currentUserId, _getSearchSuccess);
                    }
                    else {
                        $toastr.error("You didn't search anyone.");
                    }
                } // _searchUser

                function _getSearchSuccess(data, textStatus, jqXHR) {
                    vm.notify(function () {
                        vm.items = []; //initialize array to clear on each search
                        var resultsArray = data.items || [];

                        for (var i = 0; i < resultsArray.length; i++) {
                            // mapping the company names into arrays
                            var resultObject = resultsArray[i];
                            console.log("resultObject", resultObject)

                            var referralWHArray = resultObject.companies;
                            var mappedReferralWHArray = referralWHArray.map(vm.arrayMapper);

                            var myWHArray = vm.myJobs;
                            if (myWHArray != null) {
                                var mappedMyWHArray = myWHArray.map(vm.arrayMapper);
                            };

                            var referralEduArray = resultObject.schools;
                            if (referralEduArray != null) {
                                var mappedReferralEduArray = referralEduArray.map(vm.arrayMapper);
                            };

                            var myEduArray = vm.myEdu;
                            if (myEduArray != null) {
                                var mappedMyEduArray = myEduArray.map(vm.arrayMapper);
                            };

                            console.log("mappedReferralEduArray", mappedReferralEduArray);
                            console.log("mappedMyEduArray", mappedMyEduArray);

                            // find array instersection with function
                            if (mappedReferralWHArray != null && mappedMyWHArray != null) {
                                var commonWH = vm.intersect(mappedReferralWHArray, mappedMyWHArray);
                            };
                            if (mappedReferralEduArray != null && mappedMyEduArray != null) {
                                var commonEdu = vm.intersect(mappedReferralEduArray, mappedMyEduArray);
                            };

                            console.log("commonEdu", commonEdu);

                            // if commWH exists, push into vm.items array for search results
                            if ((commonWH != null && commonWH.length > 0) || (commonEdu != null && commonEdu.length > 0)) {
                                vm.items.push(resultObject);
                            }
                        }
                        if (vm.items.length == 0) {
                            $toastr.error("There are no results. Did you check for any misspellings?");
                        }
                    })
                } //_getSearchSuccess

                function _intersect(a, b) {
                    var t;
                    if (b.length > a.length) t = b, b = a, a = t; // indexOf to loop over shorter
                    return a
                        .filter(function (e) {
                            var result = b.indexOf(e) > -1; // returns true if there is a match between the two arrays
                            return result;
                        })
                        .filter(function (e, i, c) { // extra step to remove duplicates
                            return c.indexOf(e) === i;
                        });
                }//_intersect

                function _distinct() {
                    var vm = {};
                    var myWHArray = vm.myJobs;

                    vm.items = data.items;

                    var matchedCompanies = [];

                    myWHArray.forEach(function (job) {
                        if (vm.items.some(function (item) {
                            return item.companies.some(function (company) {
                                return company.nameOfCompany == job.company;
                            });
                        }))
                            matchedCompanies.push(job.company);
                    });

                    return matchedCompanies;
                }; //_distinct

                function _arrayMapper(obj, i) {
                    return obj.company || obj.nameOfCompany || obj.name || obj.school;
                } //_arrayMapper

                function _getMyJobs() {
                    vm.$workHistoryService.getByPersonId(vm.currentUserId, vm.getMyJobSuccess);
                } // _getMyJobs

                function _getMyJobSuccess(data, textStatus, jqXHR) {
                    vm.notify(function () {
                        vm.myJobs = data.items;
                    })
                    console.log("vm.myJobs", vm.myJobs);

                    // check if current user has work history
                    if (vm.myJobs != null) {
                        // get profiles with matching companies
                        vm.getProfiles();
                    }
                }; //_getMyJobSuccess

                function _getMyEdu() {
                    vm.$educationService.getByPersonId(vm.currentUserId, vm.getMyEduSuccess);
                } //_getMyEdu

                function _getMyEduSuccess(data, textStatus, jqXHR) {
                    vm.notify(function () {
                        vm.myEdu = data.items;
                        console.log("vm.myEdu", vm.myEdu);
                    })

                    // check if current user has work history
                    if (vm.myEdu != null) {
                        // get profiles with matching education
                        vm.getProfiles();
                    }
                }; // _getMyEduSuccess

                function _getJobInfo() {
                    // get job info from job id selected
                    console.log("job id:", vm.jobId);

                    vm.$jobService.getById(vm.jobId, vm.getJobInfoSuccess)
                } // _getJobInfo

                function _getJobInfoSuccess(data, textStatus, jqXHR) {
                    vm.notify(function () {
                        vm.referralJobItem = data.item;
                    })
                    if (vm.referralJobItem != null) {
                        vm.jobCoName = vm.referralJobItem.companyName;
                    }
                } //_getJobInfoSuccess

                function _getProfiles() {
                    vm.$userService.getProfilesWSameCompany(vm.currentUserId, vm.pageNum, vm.pageSize, vm.getProfilesSuccess);
                }; //_getProfiles

                function _getProfilesSuccess(data, textStatus, jqXHR) {
                    vm.notify(function () {
                        vm.matchedItems = data.items;

                        //setting total amount of rows from db
                        vm.totalRows = vm.matchedItems[0].totalRows;
                    })
                }; //_getProfilesSuccess

                function _pageChanged(num) {
                    vm.pageNum = num;
                    //console.log("Page = " + vm.pageNum);
                    vm.$userService.getProfilesWSameCompany(vm.currentUserId, vm.pageNum, vm.pageSize, vm.getProfilesSuccess);
                } //_pageChanged

                function _email() {
                    console.log("EmailFired")
                    window.location.href = "/profileng/search/email/" + vm.jobId;
                }//_email

                function _openModal(object) {
                    // mapping matching job history from page list into array
                    if (object.company != null) {
                        var referralResultsWHArray = [];
                        var listObjCompany = object.company;
                        referralResultsWHArray.push(listObjCompany);
                        console.log("referralResultsWHArray", referralResultsWHArray);
                        if (referralResultsWHArray != null) {
                            var myWHArray = vm.myJobs;
                            var mappedMyWHArray1 = myWHArray.map(vm.arrayMapper);
                            var mappedCommonWH1 = vm.intersect(referralResultsWHArray, mappedMyWHArray1);
                        }
                    }

                    // mapping matching education from page list into array
                    if (object.school != null) {
                        var referralResultsEduArray = [];
                        var listObjSchool = object.school;
                        referralResultsEduArray.push(listObjSchool);
                        if (referralResultsEduArray != null) {
                            var mappedReferralResultsEduArray = referralResultsEduArray.map(vm.arrayMapper);
                            if (vm.myEdu != null) {
                                var myEduArray = vm.myEdu;
                                var mappedMyEduArray1 = myEduArray.map(vm.arrayMapper);
                                var mappedCommonEdu1 = vm.intersect(referralResultsEduArray, mappedMyEduArray1);
                            }
                        }
                    }

                    // mapping matching work history from search results into an array
                    var referralWHArray = object.companies;
                    if (referralWHArray != null) {
                        var mappedReferralWHArray = referralWHArray.map(vm.arrayMapper);
                        if (vm.myJobs != null) {
                            var myWHArray = vm.myJobs;
                            var mappedMyWHArray = myWHArray.map(vm.arrayMapper);
                            var mappedCommonWH = vm.intersect(mappedReferralWHArray, mappedMyWHArray);
                        }
                    }

                    // mapping matching education from search into an array
                    var referralEduArray = object.schools;
                    if (referralEduArray != null) {
                        var mappedReferralEduArray = referralEduArray.map(vm.arrayMapper);
                        if (vm.myEdu != null) {
                            var myEduArray = vm.myEdu;
                            var mappedMyEduArray = myEduArray.map(vm.arrayMapper);
                            var mappedCommonEdu = vm.intersect(mappedReferralEduArray, mappedMyEduArray);
                        }
                    }

                    // setting matching history (edu or workhistory)
                    if (object.school != null && mappedCommonEdu1.length != 0) {
                        vm.newConvo.commonWH = object.school;
                    } else if (object.company != null && mappedCommonWH1.length != 0) {
                        vm.newConvo.commonWH = object.company;
                    } else if (mappedCommonWH != null && mappedCommonWH.length != 0) {
                        vm.newConvo.commonWH = mappedCommonWH;
                    } else if (mappedCommonEdu != null && mappedCommonEdu.length != 0) {
                        vm.newConvo.commonWH = mappedCommonEdu;
                    };

                    // setting referrer infor from object for textarea
                    vm.newConvo.referrerId = object.personID || object.personId;
                    vm.newConvo.candidateId = aic.page.personId;
                    vm.newConvo.referrerName = object.fullName || object.firstName + " " + object.lastName;
                    vm.newConvo.job = vm.jobCoName;
                    vm.newConvo.referralUrl = aic.page.baseUrl + "/referrals/sender/" + vm.newConvo.candidateId + "/job/" + vm.jobId;

                    // object for referral request table
                    vm.referralRequest = {
                        "referrerId": vm.newConvo.referrerId,
                        "candidateId": vm.newConvo.candidateId,
                        "jobId": vm.jobId,
                        "message": "",
                        "accepted": false
                    } // referralRequest

                    // object for messages table
                    vm.newConvo = {
                        "senderId": aic.page.guid,
                        "subject": "Request for referral",
                        "content": "Hello " + vm.newConvo.referrerName + ", " + "You may remember me from " + vm.newConvo.commonWH
                        + ". I saw a job posting for " + vm.newConvo.job + " and thought I would be an excellent candidate for this position. I was wondering if you could take a moment to refer me? Thank you for your help. "
                        + vm.newConvo.referralUrl
                    }; //newConvo

                    vm.newConvo.receiverId = object.userId || object.complexUserId; // referrerId

                    //open modal instance
                    var modalInstance = $modal.open({
                        //template for modal
                        templateUrl: '/Scripts/aic/send-message-modal/ReferralMessageModal.html',
                        //define modal controller
                        controller: 'ModalInstanceCtrlr',
                        //get data
                        resolve: {
                            newConvo: function () {
                                //console.log("in newconvo fn:", vm.newConvo);
                                return vm.newConvo;
                            }
                        }
                    }); //modalInstance

                    // get new data when modal returns ok button
                    modalInstance.result.then(function (newConvo) {
                        vm.newConvo = newConvo
                        if (vm.senderId != vm.newConvo.receiverId) {
                            // service to post conversation & msg
                            vm.postConversationMsg(vm.newConvo);

                            vm.$userService.requestReferral(vm.referralRequest, vm.requestReferralSuccess);
                        } else {
                            toastr.error("Can't send messages to yourself!");
                        }
                    });
                }//_openModal

                function _postConversationMsg(convo) {
                    vm.receiverName = convo.referrerName;
                    vm.receiverId = convo.receiverId;

                    console.log("convo", convo);

                    vm.$messageService.postConversationMsg(convo, vm.postConversationMsgSuccess)

                } //_postConversationMsg

                function _postConversationMsgSuccess(data, textStatus, jqXHR) {
                    console.log("message delivered.")
                } //_postConversationMsgSuccess

                function _requestReferralSuccess(data, textStatus, jqXHR) {
                    $toastr.success("Referral request sent successfully!");
                } //_requestReferralSuccess
            }
        })(); // userController

        (function () {
            angular.module('aicApp').controller('ModalInstanceCtrlr', ModalInstanceController);

            ModalInstanceController.$inject = ['$scope', '$modalInstance', 'newConvo', '$sce'];

            function ModalInstanceController($scope, $modalInstance, newConvo, $sce) {

                var vm = this;
                vm.$scope = $scope;
                vm.$modalInstance = $modalInstance;

                vm.$scope.newConvo = newConvo;

                vm.$scope.ok = _ok;
                vm.$scope.cancel = _cancel;
                vm.$scope.resize = _resize;
                vm.$scope.handleTextAreaHeight = _handleTextAreaHeight;

                function _ok() {
                    vm.$scope.newConvo.receiverId = newConvo.receiverId;
                    vm.$scope.newConvo.senderId = newConvo.senderId;

                    vm.$modalInstance.close(vm.$scope.newConvo);
                }; //_ok

                function _cancel() {
                    vm.$modalInstance.dismiss('cancel');
                    //console.log("dismiss", vm.$scope.newConvo)
                }; //_cancel

                function _resize() {
                    $("textarea").height($("textarea")[0].scrollHeight);
                }; //_resize

                function _handleTextAreaHeight(e) {
                    var element = e.target;

                    element.style.overflow = 'hidden';
                    element.style.height = 0;
                    element.style.height = element.scrollHeight + 'px';
                }; //_handleTextAreaHeight

                window.setTimeout(vm.$scope.resize, 1);
            }
        })(); // MODAL IIFE
