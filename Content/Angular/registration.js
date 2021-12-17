var app = angular.module("RegdApp", ['ui.bootstrap.modal', 'angularjs-dropdown-multiselect', 'ngMaterial']);

app.directive('validNumber', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^0-9\.]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }
                }
                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('validFiveNumber', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^0-9\.]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }
                }
                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 5);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('alphabetOnly', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^a-zA-Z\s]/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
        }
    };
});

app.directive('hideZero', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            element.on('input change', function () {
                if (this.value === '0' || this.value === '.') {
                    this.value = null;
                }
            });
        }
    };
});

app.directive('mobileNo', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            element.on('input change', function () {
                if (this.value === '0' || this.value === '1' || this.value === '2' || this.value === '3' || this.value === '4' || this.value === '5' || this.value === '6') {
                    this.value = null;
                }
            });
        }
    };
});

app.directive('numbersOnly', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^0-9]/g, '');
                var negativeCheck = clean.split('-');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }
                }
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('addressOnly', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^a-zA-Z0-9\s\.\/\-\,]/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
        }
    };
});

app.directive('referenceNo', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^a-zA-Z0-9\/\-]/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('capitalize', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            var capitalize = function (inputValue) {
                if (inputValue == undefined) inputValue = '';
                var capitalized = inputValue.toUpperCase();
                if (capitalized !== inputValue) {
                    ngModelCtrl.$setViewValue(capitalized);
                    ngModelCtrl.$render();
                }
                return capitalized;
            }
            ngModelCtrl.$parsers.push(capitalize);
            capitalize(scope[attrs.ngModel]);
        }
    };
});

app.directive('alphaNumeric', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^a-zA-Z0-9\/]/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('khataPlotNo', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^0-9\/]/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        template: '<div class="loading">Loading&#8230;</div>',
        link: function (scope, elm, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };
            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.css('display', 'block');
                } else {
                    elm.css('display', 'none');
                }
            });
        }
    };
}]);

app.controller("RegdCtrl", function ($scope, $http, $timeout, $mdDialog) {

    var token = document.getElementsByName('__RequestVerificationToken')[0].value;

    var showAlert = function (ev) {
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title('Alert')
                .textContent(ev)
                .ariaLabel('Alert Dialog Demo')
                .ok('OK')
                .targetEvent(ev)
        );
    };

    $scope.landrecvalidation = true;
    $scope.rbApplicantType = false;
    $scope.rbFinanceType = false;
    $scope.rbIsCISAvailed = false;
    $scope.rbIsLeasedLand = false;
    $scope.Question = true;
    $scope.compRegd = true;
    $scope.FirstInput = true;
    $scope.FirstLabel = false;
    $scope.SecondInput = true;
    $scope.SecondLabel = false;
    $scope.ThirdInput = true;
    $scope.ThirdLabel = false;
    $scope.FarmerDetails = false;
    $scope.inputFarmerID = true;
    $scope.labelFarmerID = false;
    $scope.removeLand = false;
    $scope.Complted = false;
    $scope.MainPage = false;
    $scope.FirstSave = false;
    $scope.SecondSave = true;
    $scope.ThirdSave = true;
    $scope.addFile = true;
    $scope.showButtonGenerate = false;
    $scope.registerDocumentsLabel = false;
    $scope.registerDocumentsInput = true;
    $scope.lblGT = true;
    $scope.updatedGT = false;
    $scope.lblGM = true;
    $scope.updatedGM = false;
    $scope.hideBtnVEIM = true;
    $scope.rbLocality = 'Rural';

    var referencenumber;
    var farmerID;
    var mobilenumber;
    var DistCodeforUpdate;

    $scope.dropdownSetting = {
        scrollable: true,
        scrollableHeight: '300px',
        keyboardControls: true,
        enableSearch: true,
        styleActive: true,
        groupByTextProvider: function (groupValue) { if (groupValue === '01') { return 'Agriculture'; } else if (groupValue === '02') { return 'Horticulture'; } else if (groupValue === '03') { return 'Fishery'; } else if (groupValue === '04') { return 'Animal Resources Development'; } }, groupBy: 'code'
    };

    var clearDArrays = function () {
        $scope.proTypeData = [];
        $scope.proTypeData1 = [];
        $scope.proTypeData2 = [];
        $scope.codeIDProType = [];
        $scope.uniqueCodes = [];
        $scope.codeIDProType1 = [];
        $scope.uniqueCodes1 = [];
        $scope.codeIDProType2 = [];
        $scope.uniqueCodes2 = [];
    };

    $scope.proTypeData = [];
    $scope.codeIDProType = [];
    $scope.uniqueCodes = [];
    $scope.submitProData = function () {
        clearDArrays();
        if ($scope.ProTypeSelected.length > 1) {
            angular.forEach($scope.ProTypeSelected, function (value, index) {
                $scope.codeIDProType.push({ codeID: value.code });
            });
            for (i = 0; i < $scope.codeIDProType.length; i++) {
                if ($scope.uniqueCodes.indexOf($scope.codeIDProType[i].codeID) === -1) {
                    $scope.uniqueCodes.push($scope.codeIDProType[i].codeID);
                }
            }
            if ($scope.uniqueCodes.length > 1) {
                clearDArrays();
                angular.forEach($scope.ProTypeSelected, function (value, index) {
                    $scope.proTypeData.push({ Code: value.id, Name: value.label, Capacity: value.capacity, CapacityUnit: value.cUnit });
                });
            }
            else {
                alert('Please select projects from multiple departments for Integrated Farming.');
                clearDArrays();
            }
        }
        else {
            alert('Please select more than 1 project for Integrated Farming.');
            clearDArrays();
        }
    };

    $scope.proTypeData1 = [];
    $scope.codeIDProType1 = [];
    $scope.uniqueCodes1 = [];
    $scope.submitProData1 = function () {
        clearDArrays();
        if ($scope.ProTypeSelected1.length > 1) {
            angular.forEach($scope.ProTypeSelected1, function (value, index) {
                $scope.codeIDProType1.push({ codeID: value.code });
            });
            for (i = 0; i < $scope.codeIDProType1.length; i++) {
                if ($scope.uniqueCodes1.indexOf($scope.codeIDProType1[i].codeID) === -1) {
                    $scope.uniqueCodes1.push($scope.codeIDProType1[i].codeID);
                }
            }
            if ($scope.uniqueCodes1.length > 1) {
                clearDArrays();
                angular.forEach($scope.ProTypeSelected1, function (value, index) {
                    $scope.proTypeData1.push({ Code: value.id, Name: value.label, Capacity: value.capacity, CapacityUnit: value.cUnit });
                });
            }
            else {
                alert('Please select projects from multiple departments for Integrated Farming.');
                clearDArrays();
            }
        }
        else {
            alert('Please select more than 1 project for Integrated Farming.');
            clearDArrays();
        }
    };

    $scope.proTypeData2 = [];
    $scope.codeIDProType2 = [];
    $scope.uniqueCodes2 = [];
    $scope.submitProData2 = function () {
        clearDArrays();
        if ($scope.ProTypeSelected2.length > 1) {
            angular.forEach($scope.ProTypeSelected2, function (value, index) {
                $scope.codeIDProType2.push({ codeID: value.code });
            });
            for (i = 0; i < $scope.codeIDProType2.length; i++) {
                if ($scope.uniqueCodes2.indexOf($scope.codeIDProType2[i].codeID) === -1) {
                    $scope.uniqueCodes2.push($scope.codeIDProType2[i].codeID);
                }
            }
            if ($scope.uniqueCodes2.length > 1) {
                clearDArrays();
                angular.forEach($scope.ProTypeSelected2, function (value, index) {
                    $scope.proTypeData2.push({ Code: value.id, Name: value.label, Capacity: value.capacity, CapacityUnit: value.cUnit });
                });
            }
            else {
                alert('Please select projects from multiple departments for Integrated Farming.');
                clearDArrays();
            }
        }
        else {
            alert('Please select more than 1 project for Integrated Farming.');
            clearDArrays();
        }
    };

    $scope.resetFarmerID = function () {
        $scope.inputFarmerID = true;
        $scope.labelFarmerID = false;
        $scope.rTShow = false
        $scope.rbRelationType = null;
        $scope.relationFields = false;
        $scope.emptyField();
    };

    $scope.CheckNo = function () {
        $scope.Question = false;
        $scope.MainPage = true;
    };

    $scope.CheckYes = function () {
        $scope.compRegd = false;
        $scope.RegdAdr = false;
        $scope.CheckExpansion = false;
    };

    $scope.ExpNo = function () {
        $scope.RegdAdr = true;
        $scope.CheckExpansion = false;
    };

    $scope.ExpYes = function () {
        $scope.RegdAdr = false;
        $scope.CheckExpansion = true;
    };

    $scope.capacityUnit = [];
    $http.get('GetCapacityUnit').then(function success(response) {
        $scope.capacityUnit = response.data;
    }, function error(response) {
        alert(response.status);
    });

    var thirdSubmit = function () {
        $http.get('ThirdSubmit?refno=' + referencenumber).then(function success(response) {
            getalllandRecord();
            var t = response.data;
            if (t != null && t != "") {
                $scope.ThirdInput = false;
                $scope.ThirdLabel = true;
                $scope.ThirdSave = false;
                $scope.lblrbIsLeasedLand = t.leasedland;
                if (t.leasedlan == "Yes") {
                    $scope.lblrbIsLeasedLand = true;
                    $scope.lblRelationship = t.relation;
                }
                $scope.lblDistrict = t.dist;
                $scope.lblBlock = t.block;
                $scope.lblGP = t.GP;
                $scope.lblVillage = t.Village;
                $scope.rbIsLeasedLand = t.leasedland;
                $scope.relationship.Code = t.relationID;
                $scope.district.DistrictCode = t.distID;
                $scope.gp.GPCode = t.GPID;
                $scope.village.VillageCode = t.villageID;
                $scope.gp1.GPCode = t.GPID;
            }
            else {
                if (t == "") {
                    $scope.ThirdInput = true;
                    $scope.ThirdLabel = false;
                }
            }
            $http.get('ThirdFormLandDetails?refno=' + $scope.PutReferenceNumber).then(function success(response) {
                $scope.landdetails = response.data;
                $http.get('GetDocumentfiles?refno=' + referencenumber).then(function success(response) {
                    var t = response.data;
                    if (t.pdffile1 != null) {
                        $scope.firstpdf = true;
                    }
                    if (t.pdffile2 != null) {
                        $scope.Secondpdf = true;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }, function error(response) {
                alert(response.status);
            });
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.district = [];
    $scope.gp = [];
    $scope.landdetails = [];
    $scope.GEtAllBYRefno = function () {
        if ($scope.chkregister.$valid) {
            $http.get('LastFormDtls?refno=' + $scope.PutReferenceNumber /*+ '&VoterID=' + digest($scope.txtVoterID)*/ + '&AadharNO=' + digest($scope.txtAadharNo)).then(function success(response) {
                var t = response.data;
                if (t.includes("success")) {
                    $scope.Question = false;
                    $scope.MainPage = false;
                    $scope.Complted = true;
                    referencenumber = $scope.PutReferenceNumber;
                    getvillagedetails();
                    getgpdetails();
                    firstSubmitDetails();
                    SecondSubmitDetails();
                    fetchBeneficiarydocdtls();
                    $scope.lblrefno = $scope.PutReferenceNumber;
                    thirdSubmit();
                }
                else if (t.includes("Invalid")) {
                    if (t.includes("Reference")) {
                        alert(t);
                        $scope.PutReferenceNumber = null;
                        return false;
                    }
                    if (t.includes("AadharNumber")) {
                        $scope.txtAadharNo = null;
                        alert(t);
                        return false;
                    }
                    if (t.includes("VoterID")) {
                        alert(t);
                        $scope.txtVoterID = null;
                        return false;
                    }
                }
                else {
                    $scope.Question = false;
                    $scope.MainPage = true;
                    referencenumber = $scope.PutReferenceNumber;
                    getvillagedetails();
                    getgpdetails();
                    firstSubmitDetails();
                    SecondSubmitDetails();
                    fetchBeneficiarydocdtls();
                    thirdSubmit();
                }
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    var fetchBeneficiarydocdtls = function () {
        $http.get('GetDOcumentDtls?refno=' + referencenumber).then(function successCallback(response) {
            var d = response.data;
            if (d != null && d != "") {
                passportphoto(d.PhotoSIgn);
                beneficiarysignature(d.Sign);
                $scope.idp = true;
                $scope.registerDocumentsLabel = true;
                $scope.registerDocumentsInput = false;
            }
            else {
                $scope.registerDocumentsLabel = false;
                $scope.registerDocumentsInput = true;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.submitRegistration = function () {
        var chkd = document.getElementById("checkSubmit").checked;
        if (chkd == true) {
            var myReq = {
                method: 'POST',
                url: 'SubmitRegistration',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { referenceNo: referencenumber }
            };
            $http(myReq).then(function success(response) {
                var k = response.data;
                alert("Registration done sucessfully.");
                $scope.MainPage = false;
                $scope.Complted = true;
                $scope.lblrefno = k;
                $scope.Question = false;
                referencenumber = k;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert("Please accept the undertaking before submit.");
        }
    };

    $scope.viewDocumentFile = function (a) {
        $http.get('DisplayPDFDocument?refno=' + referencenumber + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewPTCCGAAD = function (a) {
        $http.get('viewPTCCGAAD?refno=' + referencenumber + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewCISBankCC = function (a) {
        $http.get('viewCISBankCC?refno=' + referencenumber + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewBankCL = function (a) {
        $http.get('ViewBankCL?refno=' + referencenumber + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewIDProof = function (a) {
        $http.get('DisplayIDProof?refno=' + referencenumber + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.elements = [];
    $scope.generateTextbox = function () {
        if ($scope.elements.length == 0) {
            $scope.elements = [];
            for (var counter = 0; counter < $scope.txtNoOfMembers; counter++) {
                $scope.elements.push({ id: counter, value: '', heq: null });
            }
            $scope.PrefixMenmerid = $scope.txtFarmerID.substr(0, 4).toUpperCase();
        }
    };

    $scope.elems = [];
    $scope.genTextBox = function () {
        if ($scope.elems.length == 0) {
            $scope.elems = [];
            for (var counter = 0; counter < $scope.groupMemberIDs.length; counter++) {
                $scope.elems.push({ id: counter, value: '', heq: null });
            }
            $scope.PrefixMenmerid = $scope.txtFarmerID.substr(0, 4).toUpperCase();
        }
    };

    $scope.initializeTextbox = function () {
        $scope.elements = [];
        if ($scope.txtNoOfMembers >= "1" && $scope.txtNoOfMembers <= "99") {
            $scope.showButtonGenerate = true;
            $scope.ValidateFarmer = false;
        }
        else {
            $scope.showButtonGenerate = false;
            $scope.ValidateFarmer = false;
            $scope.ddlHighestEducationalQualification = null;
            $scope.txtAnnualIncome = '';
            $scope.txtPresentOccupation = '';
            $scope.rbPreviousExperience = false;
            $scope.txtEmailID = '';
            $scope.txtAddressForCorrespondence = '';
            $scope.txtPermanentAddress = '';
            $scope.txtPin = '';
        }
    };

    $scope.GetDist = [];
    $scope.GetBlock = [];
    $scope.GP = [];
    $scope.Village = [];
    $http.get('GetDistrictAll').then(function success(response) {
        $scope.GetDist = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.GetRDistrict = [];
    $http.get('GetAllRDistrict').then(function success(response) {
        $scope.GetRDistrict = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.getblock = function (distcode) {
        $scope.resetVillage();
        if (distcode != undefined) {
            $http.get('GetAllBlock?DistCode=' + distcode).then(function success(response) {
                $scope.GetBlock = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.ddlBlock = null;
            $scope.ddlGP = null;
            $scope.ddlVillage = null;
        }
    };

    $scope.GetRTehsil = [];
    $scope.getRTehsil = function (rDCode) {
        $scope.resetRVillage();
        if (rDCode != undefined) {
            $http.get('GetAllRTehsil?rDCode=' + rDCode).then(function success(response) {
                $scope.GetRTehsil = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.ddlRTehsil = null;
            $scope.ddlRICircle = null;
            $scope.ddlRVillage = null;
        }
    };

    $scope.getgp = function (blockcode) {
        $scope.resetVillage();
        if (blockcode != undefined) {
            $http.get('GetAllGP?BlockCode=' + blockcode).then(function success(response) {
                $scope.GP = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.ddlGP = null;
            $scope.ddlVillage = null;
        }
    };

    $scope.GetRTehsil = [];
    $scope.getRICircle = function (dCode, tCode) {
        $scope.resetRVillage();
        if (tCode != undefined) {
            $http.get('GetAllRICircle?rDCode=' + dCode + '&rTCode=' + tCode).then(function success(response) {
                $scope.GetRICircle = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.ddlRICircle = null;
            $scope.ddlRVillage = null;
        }
    };

    $scope.getvillage = function (gpcode) {
        $scope.resetVillage();
        if (gpcode != undefined) {
            $http.get('GetAllVillage?Gp=' + gpcode).then(function success(response) {
                $scope.Village = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.ddlVillage = null;
        }
    };

    $scope.getRVillage = function (dCode, tCode, riCode) {
        $scope.resetRVillage();
        if (dCode != undefined && tCode != undefined && riCode != undefined) {
            $http.get('GetAllRVillage?dCode=' + dCode + '&tCode=' + tCode).then(function success(response) {
                $scope.GetRVillage = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.ddlRVillage = null;
        }
    };

    $scope.getvill = function (a) {
        $scope.bhulekhRecordupdate = false;
        $scope.bhulekhRecord = false;
        $scope.Village = null;
        if (a != undefined) {
            $http.get('VillagePopulate?gpCode=' + a).then(function success(response) {
                $scope.allVillageList = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    $scope.relationship = [];
    $scope.relationshiplist = [];
    $http.get('GetRelationship').then(function success(response) {
        $scope.relationship = response.data;
        $scope.relationshiplist = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.reUseData = [];
    var myData = [];
    $scope.FarmerValidationchk = function () {
        myData = [];
        var g = null;
        angular.forEach($scope.elements, function (i) {
            if (i.value != "") {
                if (i.heq != null) {
                    var k = {};
                    var gid = $scope.txtFarmerID.substr(0, 4).toUpperCase();
                    k.GroupMemberFarmerID = gid + i.value;
                    k.HighestEducationalQualificationCode = i.heq;
                    k.ID = i.id;
                    k.Value = i.value;
                    myData.push(k);
                    $scope.reUseData = myData;
                }
                else {
                    g = "Highest Educational Qualification must be entered for all Group Member(s)";
                }
            }
            else {
                g = "Group Member Farmer ID for all members are required";
            }
        });
        if (g != null) {
            alert(g);
        }
        else {
            var req = {
                method: 'POST',
                url: 'SubmitData',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { memberDetail: myData, farmerID: $scope.txtFarmerID }
            };
            $http(req).then(function success(response) {
                var result = response.data;
                if (result.includes("invalid")) {
                    alert('Farmer ID(s) ' + result);
                    $scope.ValidateFarmer = false;
                }
                else if (result.includes("fields")) {
                    alert(result);
                    $scope.ValidateFarmer = false;
                }
                else if (result.includes("different")) {
                    alert(result);
                    $scope.ValidateFarmer = false;
                }
                else if (result.includes("Group member farmer ID should")) {
                    alert(result);
                    $scope.ValidateFarmer = false;
                }
                else if (result.includes("already exists")) {
                    alert(result);
                    $scope.ValidateFarmer = false;
                }
                else {
                    alert('All the Farmer ID(s) are valid.')
                    document.getElementById('GenerateTextboxModal').style.display = "none";
                    $timeout(function () {
                        document.getElementById("btnGenerate").click();
                    }, 1);
                    $scope.ValidateFarmer = true;
                    return false;
                }
                var arr = result.split(',');
                for (var i = 0; i < arr.length; i++) {
                    var a = arr[i];
                    if (a.includes('already exists')) {
                        var l = a.replace("Group member farmer ID", "").trim();
                    }
                    angular.forEach($scope.elements, function (j) {
                        if (a.replace("is invalid", "").trim() == ($scope.PrefixMenmerid + j.value).toUpperCase().trim()) {
                            j.value = '';
                        }
                        else if (a.replace("Group member farmer ID should not match with", "").trim() == ($scope.PrefixMenmerid + j.value).toUpperCase().trim()) {
                            j.value = '';
                        }
                        else if (l.replace("already exists", "").trim() == ($scope.PrefixMenmerid + j.value).toUpperCase().trim()) {
                            j.value = '';
                        }
                    });
                }
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    $scope.closeGroupModal = function () {
        var k = [];
        var z;
        var g = [];
        var a;
        angular.forEach($scope.elements, function (i) {
            if (i.value != "" && i.heq != null) {
                a = "No Problem";
                g.push(a);
            }
            else {
                a = "Problem";
                g.push(a);
            }
        });
        if (g.indexOf("No Problem") != -1 && $scope.reUseData.length < $scope.txtNoOfMembers) {
            alert("You must validate Farmer ID to proceed.");
            document.getElementById('GenerateTextboxModal').style.display = "none";
            $timeout(function () {
                document.getElementById("btnGenerate").click();
            }, 1);
            $scope.ValidateFarmer = false;
            return false;
        }
        else if (g.indexOf("Problem") != -1) {
            alert("You must fill all details related to the Farmer ID(s) & validate to proceed.");
            document.getElementById('GenerateTextboxModal').style.display = "none";
            $timeout(function () {
                document.getElementById("btnGenerate").click();
            }, 1);
            $scope.ValidateFarmer = false;
            return false;
        }
        angular.forEach($scope.elements, function (i) {
            angular.forEach($scope.reUseData, function (j) {
                if (i.id == j.ID) {
                    if (i.value == j.Value && i.heq == j.HighestEducationalQualificationCode) {
                        z = "OK";
                        k.push(z);
                    }
                    else if (i.value != j.Value || i.heq != j.HighestEducationalQualificationCode) {
                        z = "Not OK";
                        k.push(z);
                    }
                }
            });
        });
        if (k.indexOf("Not OK") != -1) {
            alert("You have changed the Group Member Farmer ID details. You must validate again in order to proceed.");
            $scope.ValidateFarmer = false;
            return false;
        }
        else {
            document.getElementById('GenerateTextboxModal').style.display = "none";
            $timeout(function () {
                document.getElementById("btnGenerate").click();
            }, 1);
            $scope.ValidateFarmer = true;
        }
    };

    $scope.closeGMModal = function () {
        var k = [];
        var z;
        var g = [];
        var a;
        angular.forEach($scope.elems, function (i) {
            if (i.value != "" && i.eq != null) {
                a = "No Problem";
                g.push(a);
            }
            else {
                a = "Problem";
                g.push(a);
            }
        });
        if (g.indexOf("No Problem") != -1 && $scope.reUseGMData.length < $scope.groupMemberIDs.length) {
            alert("You must validate Farmer ID to update Group Member Farmer ID details.");
            document.getElementById('ViewEditMemberModal').style.display = "none";
            $timeout(function () {
                document.getElementById("btnModal").click();
            }, 1);
            return false;
        }
        else if (g.indexOf("Problem") != -1) {
            alert("You must fill all details related to the Farmer ID(s) & validate to proceed.");
            document.getElementById('ViewEditMemberModal').style.display = "none";
            $timeout(function () {
                document.getElementById("btnModal").click();
            }, 1);
            return false;
        }
    };

    $scope.groupTypeSubmit = function () {
        if ($scope.getMgroupvalue != undefined && $scope.checkedM != undefined && $scope.getMGroupName != undefined) {
            $scope.lblGT = false;
            $scope.updatedGT = true;
            alert('The Group Type selected is ' + $scope.getMGroupName);
            document.getElementById('ViewEditGTModal').style.display = "none";
            $timeout(function () {
                document.getElementById("btnGTModal").click();
            }, 1);
        }
        else {
            alert('Please select a group type.');
        }
    };

    $scope.closeGTModal = function () {
        if ($scope.getMgroupvalue != undefined && $scope.checkedM != undefined && $scope.getMGroupName != undefined) {
            $scope.lblGT = true;
            $scope.updatedGT = false;
            alert('Click on Submit to update the Group Type.');
            $scope.getMgroupvalue = undefined;
            $scope.checkedM = undefined;
            $scope.getMGroupName = undefined;
            angular.forEach($scope.grouptype, function (i) {
                i.Selected = false;
            });
        }
        if ($scope.checkedM == undefined) {
            $scope.lblGT = true;
            $scope.updatedGT = false;
            $scope.getMgroupvalue = undefined;
            $scope.checkedM = undefined;
            $scope.getMGroupName = undefined;
        }
    };

    $scope.reUseGMData = [];
    var dataFarmerID = [];
    $scope.groupFarmerIDCheck = function () {
        var g = null;
        dataFarmerID = [];
        angular.forEach($scope.elems, function (i) {
            if (i.value != "") {
                if (i.eq != null) {
                    var k = {};
                    var gid = $scope.lblFarmerID.substr(0, 4).toUpperCase();
                    k.GroupMemberFarmerID = gid + i.value;
                    k.HighestEducationalQualificationCode = i.eq;
                    dataFarmerID.push(k);
                    $scope.reUseGMData = dataFarmerID;
                }
                else {
                    g = "Highest Educational Qualification must be entered for all Group Member(s)";
                }
            }
            else {
                g = "Group Member Farmer ID for all members are required";
            }
        });
        if (g != null) {
            alert(g);
        }
        else {
            var req = {
                method: 'POST',
                url: 'SubmitData',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { memberDetail: dataFarmerID, farmerID: $scope.lblFarmerID, referenceNo: referencenumber }
            };
            $http(req).then(function success(response) {
                var result = response.data;
                if (result.includes("invalid")) {
                    alert('Farmer ID(s) ' + result);
                }
                else if (result.includes("fields")) {
                    alert(result);
                }
                else if (result.includes("different")) {
                    alert(result);
                }
                else if (result.includes("Group member farmer ID should")) {
                    alert(result);
                }
                else if (result.includes("already exists")) {
                    alert(result);
                }
                else {
                    alert('All the Farmer ID(s) are valid.')
                    document.getElementById('ViewEditMemberModal').style.display = "none";
                    $timeout(function () {
                        document.getElementById("btnModal").click();
                    }, 1);
                    $scope.lblGM = false;
                    $scope.updatedGM = true;
                    return false;
                }
                var arr = result.split(',');
                var u;
                for (var i = 0; i < arr.length; i++) {
                    var a = arr[i];
                    if (a.includes('already exists')) {
                        var l = a.replace("Group member farmer ID", "").trim();
                    }
                    angular.forEach($scope.elems, function (j) {
                        if (a.replace("is invalid", "").trim() == ($scope.PrefixMenmerid + j.value).toUpperCase().trim()) {
                            j.value = '';
                        }
                        else if (a.replace("Group member farmer ID should not match with", "").trim() == ($scope.PrefixMenmerid + j.value).toUpperCase().trim()) {
                            j.value = '';
                        }
                        else if (l.replace("already exists", "").trim() == ($scope.PrefixMenmerid + j.value).toUpperCase().trim()) {
                            j.value = '';
                        }
                    });
                }
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    $scope.uploadBankCCFile = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('updateBankCC').value = null;
            alert("Invalid Bank Clearance Certificate, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Bank Clearance Certificate file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('updateBankCC').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadBankCLFile = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('updateBankCL').value = null;
            alert("Invalid Bank Consent Letter, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Bank Consent Letter file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('updateBankCL').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadPTCCGAADFile = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('updatePTCCGAAD').value = null;
            alert("Invalid Cast / Graduation file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Cast / Graduation file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('updatePTCCGAAD').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadCasteGraduation = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('casteGraduation').value = null;
            alert("Invalid Cast / Graduation file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Cast / Graduation file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('casteGraduation').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadCasteGraduationUPDT = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('updtCasteGraduation').value = null;
            alert("Invalid Cast / Graduation file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Cast / Graduation file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('updtCasteGraduation').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadBankConsent = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('bankConsent').value = null;
            alert("Invalid Bank Consent file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Bank Consent file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('bankConsent').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadBankClearance = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('bankClearance').value = null;
            alert("Invalid Bank Clearance file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Bank Clearance file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('bankClearance').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadBankClearanceUPDT = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('updtBankClearance').value = null;
            alert("Invalid Bank Clearance file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Bank Clearance file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('updtBankClearance').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadBankConsentUPDT = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('updtBankCL').value = null;
            alert("Invalid Bank Consent Letter, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load Bank Consent Letter file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 512000) {
                document.getElementById('updtBankCL').value = null;
                alert("File is too large, you must select file under 500KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadImage = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'jpg')) {
            alert("Invalid image file, you must select a *.jpg , *.jpeg  file.");
            document.getElementById('Imagefile').value = null;
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load image file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 51200) {
                document.getElementById('Imagefile').value = null;
                alert("File is too large, you must select file under 50kb.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadidentityproof = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'pdf')) {
            document.getElementById('FrcUpld').value = null;
            alert("Invalid ID Proof file, you must select a *.pdf file.");
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load land Document file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 409600) {
                document.getElementById('FrcUpld').value = null;
                alert("File is too large, you must select file under 400KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadSign = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'jpg')) {
            alert("Invalid image file, you must select a *.jpg , *.jpeg  file.");
            document.getElementById('signfile').value = null;
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load image file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 51200) {
                document.getElementById('signfile').value = null;
                alert("File is too large, you must select file under 50KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadlandfile1 = function (files) {
        if (files.length > 0) {
            var ext = files[0].name.match(/\.(.+)$/)[1];
            if (!(angular.lowercase(ext) === 'pdf')) {
                document.getElementById('LanddocumentPdf1').value = null;
                alert("Invalid Land Document file, you must select a *.pdf file.");
                return false;
            }
            if (files.fileSize == -1) {
                alert("Couldn't load land Document file size. Please try to save again.");
                return false;
            }
            else {
                var as = files[0].size;
                if (files[0].size > 972800) {
                    document.getElementById('LanddocumentPdf1').value = null;
                    alert("File is too large, you must select file under 950KB.");
                    return false;
                }
                else {
                    return;
                }
            }
        }
        else {
            return false;
        }
    };

    $scope.uploadlandfile2 = function (files) {
        if (files.length > 0) {
            var ext = files[0].name.match(/\.(.+)$/)[1];
            if (!(angular.lowercase(ext) === 'pdf')) {
                document.getElementById('LanddocumentPdf2').value = null;
                alert("Invalid Land Document file, you must select a .pdf file.");
                return false;
            }
            if (files.fileSize == -1) {
                alert("Couldn't load land Document file size. Please try to save again.");
                return false;
            }
            else {
                var as = files[0].size;
                if (files[0].size > 972800) {
                    alert("File is too large, you must select file under 950KB.");
                    document.getElementById('LanddocumentPdf2').value = null;
                    return false;
                }
                else {
                    return;
                }
            }
        }
        else {
            return false;
        }
    };

    $scope.uploadlandfile3 = function (files) {
        if (files.length > 0) {
            var ext = files[0].name.match(/\.(.+)$/)[1];
            if (!(angular.lowercase(ext) === 'pdf')) {
                document.getElementById('updtlanddocument1').value = null;
                alert("Invalid Land Document file, you must select a .pdf file.");
                return false;
            }
            if (files.fileSize == -1) {
                alert("Couldn't load land Document file size. Please try to save again.");
                return false;
            }
            else {
                var as = files[0].size;
                if (files[0].size > 972800) {
                    alert("File is too large, you must select file under 950KB.");
                    document.getElementById('updtlanddocument1').value = null;
                    return false;
                }
                else {
                    return;
                }
            }
        }
        else {
            return false;
        }
    };

    $scope.uploadlandfile4 = function (files) {
        if (files.length > 0) {
            var ext = files[0].name.match(/\.(.+)$/)[1];
            if (!(angular.lowercase(ext) === 'pdf')) {
                document.getElementById('updatelanddocument2').value = null;
                alert("Invalid Land Document file, you must select a .pdf file.");
                return false;
            }
            if (files.fileSize == -1) {
                alert("Couldn't load land Document file size. Please try to save again.");
                return false;
            }
            else {
                var as = files[0].size;
                if (files[0].size > 972800) {
                    alert("File is too large, you must select file under 950KB.");
                    document.getElementById('updatelanddocument2').value = null;
                    return false;
                }
                else {
                    return;
                }
            }
        }
        else {
            return false;
        }
    };

    $scope.uploadlandfile5 = function (files) {
        if (files.length > 0) {
            var ext = files[0].name.match(/\.(.+)$/)[1];
            if (!(angular.lowercase(ext) === 'pdf')) {
                document.getElementById('updateIDProof').value = null;
                alert("Invalid ID Proof file, you must select a .pdf file.");
                return false;
            }
            if (files.fileSize == -1) {
                alert("Couldn't load land Document file size. Please try to save again.");
                return false;
            }
            else {
                var as = files[0].size;
                if (files[0].size > 409600) {
                    alert("File is too large, you must select file under 400KB.");
                    document.getElementById('updateIDProof').value = null;
                    return false;
                }
                else {
                    return;
                }
            }
        }
        else {
            return false;
        }
    };

    $scope.uploadlandfile6 = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'jpg')) {
            alert("Invalid image file, you must select a *.jpg , *.jpeg  file.");
            document.getElementById('updatePhoto').value = null;
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load image file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 51200) {
                document.getElementById('updatePhoto').value = null;
                alert("File is too large, you must select file under 50KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.uploadlandfile7 = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'jpg')) {
            alert("Invalid image file, you must select a *.jpg , *.jpeg  file.");
            document.getElementById('updateSign').value = null;
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load image file size. Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 51200) {
                document.getElementById('updateSign').value = null;
                alert("File is too large, you must select file under 50KB.");
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.EmailCheck = function (emailid) {
        $http.get('Chk_Emaiid?EmailID=' + $scope.txtEmailID).then(function (response) {
            var t = response.data;
            if (t != "" && t != null) {
                alert(t);
                $scope.txtEmailID = "";
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.checkEmailID = function (emailID, referenceNo) {
        $http.get('CheckEmailID?referenceNo=' + referencenumber + '&emailID=' + $scope.textEmailID).then(function (response) {
            var t = response.data;
            if (t != "" && t != null) {
                alert(t);
                $scope.textEmailID = "";
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    var firstSubmitDetails = function () {
        $http.get('FirstSubmit?refno=' + referencenumber).then(function success(response) {
            $scope.Question = false;
            $scope.FirstSave = false;
            $scope.t = response.data;
            if ($scope.t != "") {
                $scope.FirstLabel = true;
                $scope.FirstInput = false;
                farmerID = $scope.t.FarmerID;
                mobilenumber = $scope.t.MobileNo;
                $scope.lblFarmerID = $scope.t.FarmerID;
                $scope.lblApptype = $scope.t.Apptype;
                if ($scope.t.Apptype == "Group") {
                    $scope.GDisabled = false;
                    $scope.IDisabled = true;
                    getGroupMemberIDs();
                    loadGroupType();
                    $scope.lblNoOfMember = $scope.t.NoOfMembers;
                    $scope.lblFirmName = $scope.t.FirmName;
                    $scope.textFirmName = $scope.t.FirmName;
                    $scope.groupTypeCode = $scope.t.GroupTypeCode;
                    $scope.lblGroupType = $scope.t.GroupTypeName;
                }
                else {
                    $scope.IDisabled = false;
                    $scope.GDisabled = true;
                }
                $http.get('GetGraduationDocument?refno=' + referencenumber).then(function success(response) {
                    if ($scope.t.QualificationID == "4") {
                        $scope.PTCCGAAD = true;
                    }
                    else {
                        $scope.PTCCGAAD = false;
                    }
                }, function error(response) {
                    alert(response.status);
                });
                $scope.lblQualification = $scope.t.Qualification;
                $scope.educationalQualification.Code = $scope.t.QualificationID;
                $scope.lblAnnualincome = $scope.t.Anualincome;
                $scope.lblPrevExp = $scope.t.PrevExp;
                $scope.lblEmailID = $scope.t.EmailID == null ? "NA" : $scope.t.EmailID;
                $scope.textEmailID = $scope.t.EmailID;
                $scope.lblCorespondenceAddress = $scope.t.CorespondenceAddress;
                $scope.lblPermanentAddress = $scope.t.PermanentAddress;
                $scope.lblPresentOccupation = $scope.t.PresentOccupation;
                $scope.txtFarmerID = $scope.t.FarmerID;
                $scope.txtAnnualIncome = $scope.t.Anualincome;
                $scope.txtPresentOccupation = $scope.t.PresentOccupation;
                $scope.rbPreviousExperience = $scope.t.PrevExp.trim();
                $scope.txtEmailID = $scope.t.EmailID;
                $scope.lblPincode = $scope.t.Pincode;
                $scope.txtPin = $scope.t.Pincode;
                $scope.txtAddressForCorrespondence = $scope.t.CorespondenceAddress;
                $scope.txtPermanentAddress = $scope.t.PermanentAddress;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.village = [];
    $scope.villagelist = [];
    var getvillagedetails = function () {
        $http.get('GetVillageList?farmerid=' + farmerID + '&refno=' + referencenumber).then(function success(response) {
            $scope.village = response.data;
            $scope.villagelist = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.gp1 = [];
    $scope.gplist = [];
    var getgpdetails = function () {
        $http.get('GetGPList?farmerid=' + farmerID + '&refno=' + referencenumber).then(function success(response) {
            $scope.gp1 = response.data;
            $scope.gplist = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.allGPList = [];
    var gpPopulate = function () {
        $http.get('GPPopulate?blockCode=' + $scope.lblBlockCode).then(function success(response) {
            $scope.allGPList = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.allVillageList = [];
    var villagePopulate = function () {
        $http.get('VillagePopulate?gpCode=' + $scope.dlGP).then(function success(response) {
            $scope.allVillageList = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.IPs = [];
    var getIntegratedProjects = function () {
        $http.get('GetIProjects?referenceNo=' + referencenumber).then(function success(response) {
            $scope.IPs = response.data;
        }, function error(response) {
            alert(response.status);
        });
    }

    $scope.groupMemberIDs = [];
    var getGroupMemberIDs = function () {
        $http.get('GetGroupMemberIDs?referenceNo=' + referencenumber).then(function success(response) {
            $scope.groupMemberIDs = response.data;
            angular.forEach($scope.groupMemberIDs, function (i) {
                var k = i.GroupMemberFarmerID.substr(4, i.GroupMemberFarmerID.length);
                $scope.PrefixMenmerid = i.GroupMemberFarmerID.substr(0, 4);
            });
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateFirstRecord = function () {
        var pTCG;
        if ($scope.educationalQualification.Code == "4") {
            if (document.getElementById('updtCasteGraduation').files.length > 0) {
                pTCG = document.getElementById('base64textarea15').value;
            }
            else {
                document.getElementById('updtCasteGraduation').value = null;
                document.getElementById('base64textarea15').value = null;
                pTCG = null;
            }
        }
        else {
            document.getElementById('updtCasteGraduation').value = null;
            document.getElementById('base64textarea15').value = null;
            pTCG = null;
        }
        var myData = []
        if ($scope.educationalQualification.Code != null && $scope.txtAnnualIncome != null && $scope.txtPresentOccupation != null && $scope.rbPreviousExperience != null && $scope.txtPin != null && $scope.txtAddressForCorrespondence != null && $scope.txtPermanentAddress != null) {
            if ($scope.lblApptype == 'Individual') {
                myData = { ReferenceNo: referencenumber, HighestEducationalQualificationCode: $scope.educationalQualification.Code, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.textEmailID, Pin: $scope.txtPin, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, CastGraduationCertificate: pTCG };
            }
            else {
                if ($scope.getMgroupvalue != undefined) {
                    myData = { ReferenceNo: referencenumber, FirmName: $scope.textFirmName, GroupTypeCode: $scope.getMgroupvalue, HighestEducationalQualificationCode: $scope.educationalQualification.Code, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.textEmailID, Pin: $scope.txtPin, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, CastGraduationCertificate: pTCG };
                }
                else {
                    myData = { ReferenceNo: referencenumber, FirmName: $scope.textFirmName, GroupTypeCode: $scope.groupTypeCode, HighestEducationalQualificationCode: $scope.educationalQualification.Code, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.textEmailID, Pin: $scope.txtPin, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, CastGraduationCertificate: pTCG };
                }
            }
            var myData2 = [];
            if ($scope.elems.length != 0) {
                angular.forEach($scope.elems, function (i) {
                    var k = {};
                    var gid = $scope.lblFarmerID.substr(0, 4).toUpperCase();
                    k.GroupMemberFarmerID = gid + "" + i.value;
                    k.HighestEducationalQualificationCode = i.eq;
                    myData2.push(k);
                });
            }
            else {
                angular.forEach($scope.groupMemberIDs, function (i) {
                    var k = {};
                    k.GroupMemberFarmerID = i.GroupMemberFarmerID
                    k.HighestEducationalQualificationCode = i.HighestEducationalQualificationCode;
                    myData2.push(k);
                });
            }
            var myreq = {
                method: 'POST',
                url: 'UpdateBeneficiaryDtls',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { x: myData, memberDetails: myData2 }
            };
            $http(myreq).then(function success(response) {
                var k = response.data;
                if (k.includes('updated')) {
                    alert(k);
                    firstSubmitDetails();
                    document.getElementById('FirstForm').style.display = "none";
                    $timeout(function () {
                        document.getElementById("modifyFirstForm").click();
                    }, 1);
                }
                else {
                    alert(k);
                }
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    var SecondSubmitDetails = function () {
        $http.get('SecondSubmit?refno=' + referencenumber).then(function success(response) {
            $scope.t = response.data;
            if ($scope.t != "" && $scope.t != null) {
                $scope.SecondInput = false;
                $scope.SecondLabel = true;
                $scope.SecondSave = false;
                $scope.lblDepartment = $scope.t.Department;
                $scope.lblProjectType = $scope.t.ProjectType;
                $scope.lblProductsToBeProduced = $scope.t.ProductsToBeProduced;
                $scope.lblrbIsCISAvailed = $scope.t.rbIsCISAvailed;
                $scope.lblFinanceType = $scope.t.FinanceType;
                $scope.lblOwnContribution = $scope.t.OwnContribution;
                $scope.lblCISProjectType = $scope.t.CISProjectType;
                $scope.lblCISLocation = $scope.t.CISLocation;
                $scope.txtLoc = $scope.t.CISLocation;
                $scope.lblCISYear = $scope.t.CISYear;
                $scope.txtYCIS = $scope.t.CISYear;
                $scope.lblCISFinanceType = $scope.t.CISFinanceType;
                $scope.lblCISAmount = $scope.t.CISAmount;
                $scope.txtCAmount = $scope.t.CISAmount;
                $scope.rbFinType = $scope.t.CISFinanceType;
                if ($scope.t.rbIsCISAvailed == "Yes") {
                    $scope.cisBCC = true;
                }
                else {
                    $scope.cisBCC = true;
                }
                if ($scope.t.FinanceType == "Bank") {
                    $scope.iffinancetypeisbank = true;
                }
                else {
                    $scope.iffinancetypeisbank = false;
                }
                if ($scope.t.BCL == "Yes") {
                    $scope.bcl = true;
                }
                else {
                    $scope.bcl = false;
                }
                if ($scope.t.Capacity != null && $scope.t.Capacity != "") {
                    $scope.CapacityandUnitshow = true;
                    $scope.lblCapacity = $scope.t.Capacity;
                    $scope.lblCapacityUnit = $scope.t.CapacityUnit;
                    $scope.Projecttypesuccess = true;
                }
                else {
                    $scope.CapacityandUnitshow = false;
                    $scope.Projecttypesuccess = false;
                }
                if ($scope.lblProjectType == 'Integrated Farming') {
                    getIntegratedProjects();
                }
                ProjectTypeDept();
                getBranchbyBank();
                $scope.lblBankLoan = $scope.t.BankLoan;
                $scope.lblBankName = $scope.t.BankName;
                $scope.lblBranchName = $scope.t.BranchName;
                $scope.lblTotalCost = $scope.t.TotalCost;
                $scope.txtProductsToBeProduced = $scope.t.ProductsToBeProduced;
                $scope.txtCapacity = $scope.t.Capacity;
                $scope.ddlCapacity = $scope.t.CapacityUnit;
                $scope.rbIsCISAvailed = $scope.t.rbIsCISAvailed;
                $scope.txtOwnContribution = $scope.t.OwnContribution;
                $scope.txtBankLoan = $scope.t.BankLoan;
                $scope.txtTotal = $scope.t.TotalCost;
                $scope.department.Code = $scope.t.DepartmentID;
                $scope.rbFinanceType = $scope.t.FinanceType;
                $scope.rbApproximateCost = $scope.t.approxCost;
                $scope.bank.intId = $scope.t.BankCode;
                $scope.branch.intBranchId = $scope.t.BranchCode;
                $scope.pType.Project_Code = $scope.t.CISProjectTypeCode;
            }
            else {
                $scope.SecondInput = true;
                $scope.SecondLabel = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.resetCSelection = function () {
        $scope.ddlCapacity = null;
    };

    $scope.checkICapacity = function (a, b) {
        if (a < b / 2) {
            alert('The Capacity must be equal to or above ' + b / 2);
        }
    };

    $scope.UpdateProjectInfo = function () {
        if ($scope.rbApproximateCost == "Below 1 Crore") {
            if (parseInt($scope.txtTotal) >= "10000000") {
                alert("Total cost must be below one crore.");
                return false;
            }
        }
        else {
            if (parseInt($scope.txtTotal) < "10000000") {
                alert("Total cost must be one crore or above.");
                return false;
            }
        }
        var bankCC;
        if ($scope.rbFinType == "Bank") {
            if (document.getElementById('updtBankClearance').files.length > 0) {
                bankCC = document.getElementById('base64textarea16').value;
            }
            else {
                document.getElementById('updtBankClearance').value = null;
                document.getElementById('base64textarea16').value = null;
                bankCC = null;
            }
        }
        else {
            document.getElementById('updtBankClearance').value = null;
            document.getElementById('base64textarea16').value = null;
            bankCC = null;
        }
        var bankCL;
        if ($scope.rbFinanceType == "Bank") {
            if (document.getElementById('updtBankCL').files.length > 0) {
                bankCL = document.getElementById('base64textarea19').value;
            }
            else {
                document.getElementById('updtBankCL').value = null;
                document.getElementById('base64textarea19').value = null;
                bankCL = null;
            }
        }
        else {
            document.getElementById('updtBankCL').value = null;
            document.getElementById('base64textarea19').value = null;
            bankCL = null;
        }
        var myData3 = [];
        if ($scope.AllProject.Code == "04P15") {
            if ($scope.proTypeData1.length > 0) {
                $scope.IPs.length = 0;
            }
            if ($scope.IPs.length > 0) {
                angular.forEach($scope.IPs, function (i) {
                    var u = {};
                    u.ProjectTypeCode = i.ProjectTypeCode;
                    u.Capacity = i.Capacity;
                    u.CapacityUnit = i.Unit;
                    myData3.push(u);
                });
            }
            else if ($scope.proTypeData1.length > 0) {
                angular.forEach($scope.proTypeData1, function (i) {
                    var u = {};
                    u.ProjectTypeCode = i.Code;
                    u.Capacity = i.Capacity;
                    u.CapacityUnit = i.CapacityUnit;
                    myData3.push(u);
                });
            }
            else if ($scope.proTypeData2.length > 0) {
                angular.forEach($scope.proTypeData2, function (i) {
                    var u = {};
                    u.ProjectTypeCode = i.Code;
                    u.Capacity = i.ICapacity;
                    u.CapacityUnit = i.CapacityUnit;
                    myData3.push(u);
                });
            }
            if (myData3.length == 0) {
                alert('Please enter the Integrated Projects to updated.');
                return false;
            }
        }
        else {
            myData3 = [];
        }
        var mydata = [];
        if ($scope.rbFinanceType == "Bank") {
            if ($scope.Projecttypesuccess == true) {
                if ($scope.rbIsCISAvailed == 'Yes') {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.rbFinanceType != null && $scope.bank.intId != null && $scope.branch.intBranchId != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtBankLoan != null && $scope.pType.Project_Code != null && $scope.txtLoc != null && $scope.txtYCIS != null && $scope.rbFinType != null && $scope.txtCAmount != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, FinanceType: $scope.rbFinanceType, BankCode: $scope.bank.intId, BranchCode: $scope.branch.intBranchId, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, CISProjectTypeCode: $scope.pType.Project_Code, CISLocation: $scope.txtLoc, CISAvailedYear: $scope.txtYCIS, CISFinanceType: $scope.rbFinType, CISAmount: $scope.txtCAmount, CISBankClearanceCertificate: bankCC, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.rbFinanceType != null && $scope.bank.intId != null && $scope.branch.intBranchId != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtBankLoan != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, FinanceType: $scope.rbFinanceType, BankCode: $scope.bank.intId, BranchCode: $scope.branch.intBranchId, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
            else {
                if ($scope.rbIsCISAvailed == 'Yes') {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.rbFinanceType != null && $scope.bank.intId != null && $scope.branch.intBranchId != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtBankLoan != null && $scope.pType.Project_Code != null && $scope.txtLoc != null && $scope.txtYCIS != null && $scope.rbFinType != null && $scope.txtCAmount != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, FinanceType: $scope.rbFinanceType, BankCode: $scope.bank.intId, BranchCode: $scope.branch.intBranchId, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, CISProjectTypeCode: $scope.pType.Project_Code, CISLocation: $scope.txtLoc, CISAvailedYear: $scope.txtYCIS, CISFinanceType: $scope.rbFinType, CISAmount: $scope.txtCAmount, CISBankClearanceCertificate: bankCC, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.rbFinanceType != null && $scope.bank.intId != null && $scope.branch.intBranchId != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtBankLoan != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, FinanceType: $scope.rbFinanceType, BankCode: $scope.bank.intId, BranchCode: $scope.branch.intBranchId, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
        }
        else {
            if ($scope.Projecttypesuccess == true) {
                if ($scope.rbIsCISAvailed == 'Yes') {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.rbFinanceType != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.pType.Project_Code != null && $scope.txtLoc != null && $scope.txtYCIS != null && $scope.rbFinType != null && $scope.txtCAmount != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, FinanceType: $scope.rbFinanceType, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, CISProjectTypeCode: $scope.pType.Project_Code, CISLocation: $scope.txtLoc, CISAvailedYear: $scope.txtYCIS, CISFinanceType: $scope.rbFinType, CISAmount: $scope.txtCAmount, CISBankClearanceCertificate: bankCC };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.rbFinanceType != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, FinanceType: $scope.rbFinanceType, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
            else {
                if ($scope.rbIsCISAvailed == 'Yes') {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.rbFinanceType != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.pType.Project_Code != null && $scope.txtLoc != null && $scope.txtYCIS != null && $scope.rbFinType != null && $scope.txtCAmount != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, FinanceType: $scope.rbFinanceType, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, CISProjectTypeCode: $scope.pType.Project_Code, CISLocation: $scope.txtLoc, CISAvailedYear: $scope.txtYCIS, CISFinanceType: $scope.rbFinType, CISAmount: $scope.txtCAmount, CISBankClearanceCertificate: bankCC };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.department.Code != null && $scope.AllProject.Code != null && $scope.txtProductsToBeProduced != null && $scope.rbFinanceType != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null) {
                        mydata = { ReferenceNo: referencenumber, ProjectTypeCode: $scope.AllProject.Code, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, FinanceType: $scope.rbFinanceType, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
        }
        var myreq = {
            method: 'POST',
            url: 'UpdateProjectInformationDetails',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { x: mydata, IPD: myData3, selectCapacity: $scope.ddlCapacity, totalcost: $scope.txtTotal }
        };
        $http(myreq).then(function success(response) {
            var k = response.data;
            if (k.includes('updated')) {
                alert(k);
                SecondSubmitDetails();
                document.getElementById('SecondForm').style.display = "none";
                $timeout(function () {
                    document.getElementById("modifySecondForm").click();
                }, 1);
            }
            else {
                alert(k);
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    var PinCodeRegex = new RegExp("\^(d[0-9])");
    $scope.pinflag = false;
    $scope.pinText;
    $scope.pinStrength = {};
    $scope.analyze = function (value) {
        if (PinCodeRegex.test(value)) {
            $scope.pinflag = false;
        }
        else {
            $scope.pinStrength["color"] = "red";
            $scope.pinText = " * Invalid Pin Code";
            $scope.pinflag = true;
        }
    };

    $scope.gtype = {};
    $scope.getGroupValue;
    $scope.getValue = function (val1, val2) {
        $scope.getGroupValue = val1;
        $scope.checked = val2;
    };

    $scope.getMgroupvalue;
    $scope.getMvalue = function (val1, val2, val3) {
        $scope.getMgroupvalue = val1;
        $scope.checkedM = val2;
        $scope.getMGroupName = val3;
    };

    $scope.ApplicationProjectInfoSubmit = function () {
        if ($scope.rbApproximateCost == "Below 1 Crore") {
            if (parseInt($scope.txtTotal) >= "10000000") {
                alert("Total cost must be below one crore.");
                return false;
            }
        }
        else {
            if (parseInt($scope.txtTotal) < "10000000") {
                alert("Total cost must be one crore or above.");
                return false;
            }
        }
        if ($scope.FarmerMobileNumber == "" || $scope.FarmerMobileNumber == null || $scope.FarmerMobileNumber == undefined) {
            mobilenumber = mobilenumber;
        }
        else {
            mobilenumber = $scope.FarmerMobileNumber;
        }
        var pTCG;
        if ($scope.ddlHighestEducationalQualification == "4") {
            if (document.getElementById('casteGraduation').files.length > 0) {
                pTCG = document.getElementById('base64textarea12').value;
            }
            else {
                document.getElementById('casteGraduation').value = null;
                document.getElementById('base64textarea12').value = null;
                pTCG = null;
            }
        }
        else {
            document.getElementById('casteGraduation').value = null;
            document.getElementById('base64textarea12').value = null;
            pTCG = null;
        }
        var myData3 = [];
        var msg = "";
        if ($scope.ddlProjectType.Code == "04P15") {
            angular.forEach($scope.proTypeData, function (i) {
                if ((i.ICapacity != undefined && i.CapacityUnit != null) || (i.ICapacity == undefined && i.CapacityUnit == null)) {
                    var u = {};
                    u.ProjectTypeCode = i.Code;
                    u.Capacity = i.ICapacity;
                    u.CapacityUnit = i.CapacityUnit;
                    myData3.push(u);
                }
                else {
                    msg = "The Capacity must be entered incase of Integrated Farming.";
                    myData3 = [];
                    return false;
                }
            });
            if (msg != "") {
                myData3 = [];
                alert(msg);
                return false;
            }
            if (myData3.length == 0) {
                alert('Please enter the Integrated Projects to updated.');
                return false;
            }
        }
        else {
            myData3 = [];
        }
        var myData2R1 = [];
        if ($scope.rbApplicantType == 'Group') {
            if ($scope.rbRelationType == 'Self') {
                if ($scope.txtFirmName != undefined && $scope.txtNoOfMembers != undefined && $scope.checked != undefined && $scope.checked == true && $scope.getGroupValue != "" && $scope.getGroupValue != undefined && $scope.elements.length > 0 && $scope.rbRelationType != null) {
                    var myDataR1 = { FarmerID: $scope.txtFarmerID.toUpperCase(), RelationWithFIDType: $scope.rbRelationType, MobileNo: mobilenumber, BeneficiaryType: $scope.rbApplicantType, NoOfMember: $scope.txtNoOfMembers, FirmName: $scope.txtFirmName, GroupTypeCode: $scope.getGroupValue, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.txtEmailID, HighestEducationalQualificationCode: $scope.ddlHighestEducationalQualification, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, Pin: $scope.txtPin, CastGraduationCertificate: pTCG };
                    angular.forEach($scope.elements, function (i) {
                        var k = {};
                        var gid = $scope.txtFarmerID.substr(0, 4).toUpperCase();
                        k.GroupMemberFarmerID = gid + "" + i.value;
                        k.HighestEducationalQualificationCode = i.heq;
                        myData2R1.push(k);
                    });
                }
                else {
                    alert('Please select the Group Type, No of Group Members, Relation Type & enter the Firm Name.');
                    return false;
                }
            }
            else {
                if ($scope.txtFirmName != undefined && $scope.txtNoOfMembers != undefined && $scope.checked != undefined && $scope.checked == true && $scope.getGroupValue != "" && $scope.getGroupValue != undefined && $scope.elements.length > 0 && $scope.rbRelationType != null && $scope.ddlRelationFID != null && $scope.txtRVoterID != undefined && $scope.txtRAadhaarNo != undefined) {
                    var myDataR1 = { FarmerID: $scope.txtFarmerID.toUpperCase(), RelationWithFIDType: $scope.rbRelationType, RelationWithFID: $scope.ddlRelationFID, RelationApplicantName: $scope.lblApplicantName, RVoterIDNo: $scope.txtRVoterID, RAadhaarNo: $scope.txtRAadhaarNo, MobileNo: mobilenumber, BeneficiaryType: $scope.rbApplicantType, NoOfMember: $scope.txtNoOfMembers, FirmName: $scope.txtFirmName, GroupTypeCode: $scope.getGroupValue, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.txtEmailID, HighestEducationalQualificationCode: $scope.ddlHighestEducationalQualification, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, Pin: $scope.txtPin, CastGraduationCertificate: pTCG };
                    angular.forEach($scope.elements, function (i) {
                        var k = {};
                        var gid = $scope.txtFarmerID.substr(0, 4).toUpperCase();
                        k.GroupMemberFarmerID = gid + "" + i.value;
                        k.HighestEducationalQualificationCode = i.heq;
                        myData2R1.push(k);
                    });
                }
                else {
                    alert('Please select the Group Type, No of Group Members, Relation Type, Voter ID No., Aadhaar No. & enter the Firm Name.');
                    return false;
                }
            }
            
        }
        else {
            if ($scope.rbRelationType == 'Self') {
                var myDataR1 = { FarmerID: $scope.txtFarmerID.toUpperCase(), RelationWithFIDType: $scope.rbRelationType, MobileNo: mobilenumber, BeneficiaryType: $scope.rbApplicantType, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.txtEmailID, HighestEducationalQualificationCode: $scope.ddlHighestEducationalQualification, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, Pin: $scope.txtPin, CastGraduationCertificate: pTCG };
            }
            else {
                var myDataR1 = { FarmerID: $scope.txtFarmerID.toUpperCase(), RelationWithFIDType: $scope.rbRelationType, RelationWithFID: $scope.ddlRelationFID, RelationApplicantName: $scope.lblApplicantName, RVoterIDNo: $scope.txtRVoterID, RAadhaarNo: $scope.txtRAadhaarNo, MobileNo: mobilenumber, BeneficiaryType: $scope.rbApplicantType, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, PreviousExperience: $scope.rbPreviousExperience, EmailID: $scope.txtEmailID, HighestEducationalQualificationCode: $scope.ddlHighestEducationalQualification, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, Pin: $scope.txtPin, CastGraduationCertificate: pTCG };
            }
        }
        var myData3R1 = [];
        if ($scope.rbFinanceType == false) { $scope.rbFinanceType = "Self"; }
        if ($scope.rbIsCISAvailed == false) { $scope.rbIsCISAvailed = "No"; }
        if ($scope.rbFT == undefined || $scope.rbFT == false) { $scope.rbFT = "Self"; }
        var bankCC;
        if ($scope.rbFT == "Bank") {
            if (document.getElementById('bankClearance').files.length > 0) {
                bankCC = document.getElementById('base64textarea11').value;
            }
            else {
                document.getElementById('bankClearance').value = null;
                document.getElementById('base64textarea11').value = null;
                bankCC = null;
            }
        }
        else {
            document.getElementById('bankClearance').value = null;
            document.getElementById('base64textarea11').value = null;
            bankCC = null;
        }
        var bankCL;
        if ($scope.rbFinanceType == "Bank") {
            if (document.getElementById('bankConsent').files.length > 0) {
                bankCL = document.getElementById('base64textarea17').value;
            }
            else {
                document.getElementById('bankConsent').value = null;
                document.getElementById('base64textarea17').value = null;
                bankCL = null;
            }
        }
        else {
            document.getElementById('bankConsent').value = null;
            document.getElementById('base64textarea17').value = null;
            bankCL = null;
        }
        if ($scope.rbFinanceType == "Bank") {
            if ($scope.Projecttypesuccess == true) {
                if ($scope.rbIsCISAvailed == "Yes") {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtBankLoan != null && $scope.txtBankLoan != "0" && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.ddlBank.intId != null && $scope.ddlBranch.intBranchId != null && $scope.txtTotal != null && $scope.ddlPT != null && $scope.txtLocation != null && $scope.txtYearCIS != null && $scope.txtCISAmount != null && $scope.rbFT != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, BankCode: $scope.ddlBank.intId, BranchCode: $scope.ddlBranch.intBranchId, CISProjectTypeCode: $scope.ddlPT.Code, CISLocation: $scope.txtLocation, CISAvailedYear: $scope.txtYearCIS, CISAmount: $scope.txtCISAmount, CISFinanceType: $scope.rbFT, CISBankClearanceCertificate: bankCC, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtBankLoan != null && $scope.txtBankLoan != "0" && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.ddlBank.intId != null && $scope.ddlBranch.intBranchId != null && $scope.txtTotal != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, BankCode: $scope.ddlBank.intId, BranchCode: $scope.ddlBranch.intBranchId, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
            else {
                if ($scope.rbIsCISAvailed == "Yes") {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtBankLoan != null && $scope.txtBankLoan != "0" && $scope.ddlBank.intId != null && $scope.ddlBranch.intBranchId != null && $scope.txtTotal != null && $scope.ddlPT != null && $scope.txtLocation != null && $scope.txtYearCIS != null && $scope.txtCISAmount != null && $scope.rbFT != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, BankCode: $scope.ddlBank.intId, BranchCode: $scope.ddlBranch.intBranchId, CISProjectTypeCode: $scope.ddlPT.Code, CISLocation: $scope.txtLocation, CISAvailedYear: $scope.txtYearCIS, CISAmount: $scope.txtCISAmount, CISFinanceType: $scope.rbFT, CISBankClearanceCertificate: bankCC, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtBankLoan != null && $scope.txtBankLoan != "0" && $scope.ddlBank.intId != null && $scope.ddlBranch.intBranchId != null && $scope.txtTotal != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan, BankCode: $scope.ddlBank.intId, BranchCode: $scope.ddlBranch.intBranchId, BankConsentLetter: bankCL };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
        }
        else {
            if ($scope.Projecttypesuccess == true) {
                if ($scope.rbIsCISAvailed == "Yes") {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.txtTotal != null && $scope.ddlPT != null && $scope.txtLocation != null && $scope.txtYearCIS != null && $scope.txtCISAmount != null && $scope.rbFT != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, BankCode: null, BranchCode: null, CISProjectTypeCode: $scope.ddlPT.Code, CISLocation: $scope.txtLocation, CISAvailedYear: $scope.txtYearCIS, CISAmount: $scope.txtCISAmount, CISFinanceType: $scope.rbFT, CISBankClearanceCertificate: bankCC };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
                else {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtCapacity != null && $scope.ddlCapacity != null && $scope.txtTotal != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, Capacity: $scope.txtCapacity, CapacityUnit: $scope.ddlCapacity, BankCode: null, BranchCode: null };
                    }
                    else {
                        alert("Please fill up all the fields.");
                        return false;
                    }
                }
            }
            else {
                if ($scope.rbIsCISAvailed == "Yes") {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtTotal != null && $scope.ddlPT != null && $scope.txtLocation != null && $scope.txtYearCIS != null && $scope.txtCISAmount != null && $scope.rbFT != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankCode: null, BranchCode: null, CISProjectTypeCode: $scope.ddlPT.Code, CISLocation: $scope.txtLocation, CISAvailedYear: $scope.txtYearCIS, CISAmount: $scope.txtCISAmount, CISFinanceType: $scope.rbFT, CISBankClearanceCertificate: bankCC };
                    }
                    else {
                        return false;
                    }
                }
                else {
                    if ($scope.ddlDepartment != null && $scope.ddlProjectType != null && $scope.rbFinanceType != null && $scope.txtProductsToBeProduced != null && $scope.rbIsCISAvailed != null && $scope.rbApproximateCost != null && $scope.txtOwnContribution != null && $scope.txtOwnContribution != "0" && $scope.txtTotal != null) {
                        myData3R1 = { DepartmentCode: $scope.ddlDepartment.Code, ProjectTypeCode: $scope.ddlProjectType.Code, FinanceType: $scope.rbFinanceType, ProductToBeProducedOrMarketed: $scope.txtProductsToBeProduced, IsCISAvailedPreviously: $scope.rbIsCISAvailed, ApproximateCost: $scope.rbApproximateCost, OwnContribution: $scope.txtOwnContribution, BankCode: null, BranchCode: null };
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        var req = {
            method: 'POST',
            url: 'Submitappandprojectdetails',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { BD: myDataR1, memberDetail: myData2R1, IPD: myData3, emailID: $scope.txtEmailID, BPD: myData3R1, pCode: $scope.ddlProjectType.Code, gender: $scope.t.GenderName, catagory: $scope.t.CategoryName, totalcost: $scope.txtTotal }
        };
        $http(req).then(function successCallback(response) {
            var t = response.data;
            if (t != "" && t != null) {
                referencenumber = t;
                if (t.includes("above")) {
                    alert(t);
                    return false;
                }
                if (t.includes("below")) {
                    alert(t);
                    return false;
                }
                if (t.includes('No more projects')) {
                    alert(t);
                    return false;
                }
                if (t.includes('The Capacity must be entered')) {
                    alert(t);
                    return false;
                }
                if (t.includes('The target value has not been set')) {
                    alert(t);
                    return false;
                }
                if (t.includes('Please enter the Integrated')) {
                    alert(t);
                    return false;
                }
                if (t.includes('subsidy')) {
                    alert(t);
                    return false;
                }
                if (t.includes('Preferential')) {
                    alert(t);
                    return false;
                }
                if (t.includes('required details')) {
                    alert(t);
                    return false;
                }
                if (t.includes('Bank Consent')) {
                    alert(t);
                    return false;
                }
                if (t.includes('possess a Farmer ID')) {
                    alert(t);
                    return false;
                }
                if (t.includes('project with Farmer ID')) {
                    alert(t);
                    return false;
                }
                if (t.includes('applied as the relative')) {
                    alert(t);
                    return false;
                }
                if (t.includes('related to your Relations')) {
                    alert(t);
                    return false;
                }
                if (t.includes('invalid')) {
                    alert(t);
                    $scope.resetFarmerID();
                    return false;
                }
                if (t.includes('10%')) {
                    alert(t);
                    return false;
                }
                alert('Records saved successfully.\nNote your Reference No. "' + t + '" for future correspondence.');
                getvillagedetails();
                getgpdetails();
                SecondSubmitDetails();
                firstSubmitDetails();
                $scope.SecondInput = false;
                $scope.SecondLabel = true;
            }
        }, function errorCallback(response) {
            alert(response.status);
        });
    };

    $scope.showAdd = function () {
        if ($scope.txtKhataNumber !== undefined && $scope.txtPlotNumber !== undefined && (($scope.ddlDistrict != undefined && $scope.ddlBlock != undefined && $scope.ddlGP != undefined && $scope.ddlVillage !== undefined) || ($scope.ddlRDistrict != undefined && $scope.ddlRTehsil != undefined && $scope.ddlRICircle != undefined && $scope.ddlRVillage !== undefined)) && $scope.txtAreaE != undefined && $scope.ddlUnitAOE != undefined && $scope.ddlRelationship !== undefined) {
            $scope.ShowAddland = true;
        }
        else {
            $scope.ShowAddland = false;
        }
    };

    $scope.checkZero = function () {
        if ($scope.bhulandrec.AreaInAcre == "0" && $scope.ddlUnitAOE == "Area in Acre") {
            alert("Area in Acre cannot be chosen");
            $scope.txtAreaE = null;
            $scope.ddlUnitAOE = null;
        }
        else if ($scope.bhulandrec.AreaInHectare == "0" && $scope.ddlUnitAOE == "Area in Hectare") {
            alert("Area in Hectare cannot be chosen");
            $scope.txtAreaE = null;
            $scope.ddlUnitAOE = null;
        }
    };

    $scope.checkO = function () {
        if (($scope.bhulekhRecordupdate.lblAreaInAcre == "0" && $scope.ddlUnitExec == "Area in Acre") || ($scope.lblAreaInAcre == "0" && $scope.ddlUnitExec == "Area in Acre")) {
            alert("Area in Acre cannot be chosen");
            $scope.txtAreaExec = null;
            $scope.ddlUnitExec = null;
        }
        else if (($scope.bhulekhRecordupdate.lblAreaInHectare == "0" && $scope.ddlUnitExec == "Area in Acre") || ($scope.lblAreaInHectare == "0" && $scope.ddlUnitExec == "Area in Hectare")) {
            alert("Area in Hectare cannot be chosen");
            $scope.txtAreaExec = null;
            $scope.ddlUnitExec = null;
        }
    };

    $scope.LandrecordBeneficiary = [];
    $scope.AddLandrecordDetails = function () {
        if ($scope.txtKhataNumber != null && $scope.txtPlotNumber != null && (($scope.ddlDistrict != null && $scope.ddlBlock != null && $scope.ddlGP != null && $scope.ddlVillage != null) || ($scope.ddlRDistrict != null && $scope.ddlRTehsil != null && $scope.ddlRICircle != null && $scope.ddlRVillage !== null)) && $scope.bhulandrec.TenantName != null && $scope.bhulandrec.Kisam != null && $scope.bhulandrec.AreaInHectare != null && $scope.bhulandrec.AreaInAcre != null && $scope.txtAreaE != null && $scope.ddlUnitAOE != null && $scope.ddlRelationship != null) {
            var area = null;
            if ($scope.ddlUnitAOE == "Area in Acre") {
                area = $scope.bhulandrec.AreaInAcre;
            }
            else if ($scope.ddlUnitAOE == "Area in Hectare") {
                area = $scope.bhulandrec.AreaInHectare;
            }
            if ($scope.txtAreaE <= area) {
                if ($scope.txtAreaE != "0") {
                    if ($scope.ddlDistrict != null && $scope.ddlBlock != null && $scope.ddlGP != null && $scope.ddlVillage != null) {
                        if ($scope.LandrecordBeneficiary.length !== 0) {
                            angular.forEach($scope.LandrecordBeneficiary, function (arr) {
                                if (arr.DistrictCode === $scope.ddlDistrict.RevenueDistrictCode && arr.BlockCode === $scope.ddlBlock.BlockCode && arr.GPCode === $scope.ddlGP.GPCode && arr.VillageCode === $scope.ddlVillage.VillageCode && arr.KhataNo === $scope.txtKhataNumber && arr.PlotNo === $scope.txtPlotNumber) {
                                    alert('Duplicate record must not be entered.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                                if (arr.DistrictCode != $scope.ddlDistrict.RevenueDistrictCode && arr.BlockCode != $scope.ddlBlock.BlockCode) {
                                    alert('District and Block must be same for all records.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                            });
                            angular.forEach($scope.LandrecordBeneficiary, function (arr) {
                                if (arr.DistrictCode === $scope.ddlDistrict.RevenueDistrictCode && arr.BlockCode === $scope.ddlBlock.BlockCode && arr.GPCode === $scope.ddlGP.GPCode && arr.VillageCode === $scope.ddlVillage.VillageCode && arr.KhataNo === $scope.txtKhataNumber && arr.PlotNo === $scope.txtPlotNumber) {
                                    alert('Duplicate record must not be entered.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                                if (arr.DistrictCode != $scope.ddlDistrict.RevenueDistrictCode && arr.BlockCode != $scope.ddlBlock.BlockCode) {
                                    alert('District and Block must be same for all records.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                                else {
                                    if ($scope.txtKhataNumber != null && $scope.txtPlotNumber != null && $scope.ddlDistrict != null && $scope.ddlBlock != null && $scope.ddlGP != null && $scope.ddlVillage != null && $scope.bhulandrec.TenantName != null && $scope.bhulandrec.Kisam != null && $scope.bhulandrec.AreaInHectare != null && $scope.bhulandrec.AreaInAcre != null && $scope.txtAreaE != null) {
                                        $scope.BLKName = $scope.ddlBlock.BlockName;
                                        $scope.GName = $scope.ddlGP.GPName;
                                        $scope.LandrecordBeneficiary.push({ DistrictCode: $scope.ddlDistrict.RevenueDistrictCode, DistCode: $scope.ddlDistrict.DistrictCode, DistrictName: $scope.ddlDistrict.DistrictName, BlockCode: $scope.ddlBlock.BlockCode, BlockName: $scope.ddlBlock.BlockName, GPCode: $scope.ddlGP.GPCode, GPName: $scope.ddlGP.GPName, VillageCode: $scope.ddlVillage.VillageCode, VillageName: $scope.ddlVillage.VillageName, KhataNo: $scope.txtKhataNumber, PlotNo: $scope.txtPlotNumber, TenantName: $scope.bhulandrec.TenantName, Kisam: $scope.bhulandrec.Kisam, AreaInHectare: $scope.bhulandrec.AreaInHectare, AreaInAcre: $scope.bhulandrec.AreaInAcre, AreaOfExec: $scope.txtAreaE, UnitAOE: $scope.ddlUnitAOE, RelationshipCode: $scope.ddlRelationship.Code, Relationship: $scope.ddlRelationship.Name });
                                        $scope.landDetailsrcd = true;
                                        $scope.ClearLandDtls();
                                        $scope.bhulekhRecord = false;
                                        $scope.ShowAddland = false
                                        $scope.Addbtn = true;
                                    }
                                }
                            });
                        }
                        else {
                            $scope.BLKName = $scope.ddlBlock.BlockName;
                            $scope.GName = $scope.ddlGP.GPName;
                            $scope.LandrecordBeneficiary.push({ DistrictCode: $scope.ddlDistrict.RevenueDistrictCode, DistCode: $scope.ddlDistrict.DistrictCode, DistrictName: $scope.ddlDistrict.DistrictName, BlockCode: $scope.ddlBlock.BlockCode, BlockName: $scope.ddlBlock.BlockName, GPCode: $scope.ddlGP.GPCode, GPName: $scope.ddlGP.GPName, VillageCode: $scope.ddlVillage.VillageCode, VillageName: $scope.ddlVillage.VillageName, KhataNo: $scope.txtKhataNumber, PlotNo: $scope.txtPlotNumber, TenantName: $scope.bhulandrec.TenantName, Kisam: $scope.bhulandrec.Kisam, AreaInHectare: $scope.bhulandrec.AreaInHectare, AreaInAcre: $scope.bhulandrec.AreaInAcre, AreaOfExec: $scope.txtAreaE, UnitAOE: $scope.ddlUnitAOE, RelationshipCode: $scope.ddlRelationship.Code, Relationship: $scope.ddlRelationship.Name });
                            $scope.landDetailsrcd = true;
                            $scope.ClearLandDtls();
                            $scope.bhulekhRecord = false;
                            $scope.ShowAddland = false
                            $scope.Addbtn = true;
                        }
                    }
                    else if ($scope.ddlRDistrict != null && $scope.ddlRTehsil != null && $scope.ddlRICircle != null && $scope.ddlRVillage !== null) {
                        if ($scope.LandrecordBeneficiary.length !== 0) {
                            angular.forEach($scope.LandrecordBeneficiary, function (arr) {
                                if (arr.DistrictCode === $scope.ddlRDistrict.DCode && arr.TehsilCode === $scope.ddlRTehsil.TCode && arr.RICircleCode === $scope.ddlRICircle.riCode && arr.VillageCode === $scope.ddlRVillage.VCode && arr.KhataNo === $scope.txtKhataNumber && arr.PlotNo === $scope.txtPlotNumber) {
                                    alert('Duplicate record must not be entered.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                                if (arr.DistrictCode != $scope.ddlRDistrict.DCode && arr.TehsilCode != $scope.ddlRTehsil.TCode) {
                                    alert('District and Block must be same for all records.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                            });
                            angular.forEach($scope.LandrecordBeneficiary, function (arr) {
                                if (arr.DistrictCode === $scope.ddlRDistrict.DCode && arr.TehsilCode === $scope.ddlRTehsil.TCode && arr.RICircleCode === $scope.ddlRICircle.riCode && arr.VillageCode === $scope.ddlRVillage.VCode && arr.KhataNo === $scope.txtKhataNumber && arr.PlotNo === $scope.txtPlotNumber) {
                                    alert('Duplicate record must not be entered.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                                if (arr.DistrictCode != $scope.ddlRDistrict.DCode && arr.TehsilCode != $scope.ddlRTehsil.TCode) {
                                    alert('District and Block must be same for all records.');
                                    $scope.landDetailsrcd = true;
                                    $scope.ClearLandDtls();
                                    $scope.bhulekhRecord = false;
                                    $scope.ShowAddland = false;
                                }
                                else {
                                    if ($scope.txtKhataNumber != null && $scope.txtPlotNumber != null && $scope.ddlRDistrict != null && $scope.ddlRTehsil != null && $scope.ddlRICircle != null && $scope.ddlRVillage != null && $scope.bhulandrec.TenantName != null && $scope.bhulandrec.Kisam != null && $scope.bhulandrec.AreaInHectare != null && $scope.bhulandrec.AreaInAcre != null && $scope.txtAreaE != null && $scope.ddlUnitAOE != null && $scope.ddlRelationship != null) {
                                        $scope.TehName = $scope.ddlRTehsil.TName;
                                        $scope.RICName = $scope.ddlRICircle.riName;
                                        $scope.LandrecordBeneficiary.push({
                                            DistrictCode: $scope.ddlRDistrict.DCode, DistCode: $scope.ddlRDistrict.LGDDistrictCode, DistrictName: $scope.ddlRDistrict.DName, BlockCode: $scope.ddlRTehsil.LGDBlockCode, TehsilCode: $scope.ddlRTehsil.TCode, TehsilName: $scope.ddlRTehsil.TName, RICircleCode: $scope.ddlRICircle.riCode, RICircleName: $scope.ddlRICircle.riName, VillageCode: $scope.ddlRVillage.VCode, VillageName: $scope.ddlRVillage.voName, KhataNo: $scope.txtKhataNumber, PlotNo: $scope.txtPlotNumber, TenantName: $scope.bhulandrec.TenantName, Kisam: $scope.bhulandrec.Kisam, AreaInHectare: $scope.bhulandrec.AreaInHectare, AreaInAcre: $scope.bhulandrec.AreaInAcre, AreaOfExec: $scope.txtAreaE, UnitAOE: $scope.ddlUnitAOE, RelationshipCode: $scope.ddlRelationship.Code, Relationship: $scope.ddlRelationship.Name
                                        });
                                        $scope.landDetailsrcd = true;
                                        $scope.ClearLandDtls();
                                        $scope.bhulekhRecord = false;
                                        $scope.ShowAddland = false
                                        $scope.Addbtn = true;
                                    }
                                }
                            });
                        }
                        else {
                            $scope.TehName = $scope.ddlRTehsil.TName;
                            $scope.RICName = $scope.ddlRICircle.riName;
                            $scope.LandrecordBeneficiary.push({ DistrictCode: $scope.ddlRDistrict.DCode, DistCode: $scope.ddlRDistrict.LGDDistrictCode, DistrictName: $scope.ddlRDistrict.DName, BlockCode: $scope.ddlRTehsil.LGDBlockCode, TehsilCode: $scope.ddlRTehsil.TCode, TehsilName: $scope.ddlRTehsil.TName, RICircleCode: $scope.ddlRICircle.riCode, RICircleName: $scope.ddlRICircle.riName, VillageCode: $scope.ddlRVillage.VCode, VillageName: $scope.ddlRVillage.voName, KhataNo: $scope.txtKhataNumber, PlotNo: $scope.txtPlotNumber, TenantName: $scope.bhulandrec.TenantName, Kisam: $scope.bhulandrec.Kisam, AreaInHectare: $scope.bhulandrec.AreaInHectare, AreaInAcre: $scope.bhulandrec.AreaInAcre, AreaOfExec: $scope.txtAreaE, UnitAOE: $scope.ddlUnitAOE, RelationshipCode: $scope.ddlRelationship.Code, Relationship: $scope.ddlRelationship.Name });
                            $scope.landDetailsrcd = true;
                            $scope.ClearLandDtls();
                            $scope.bhulekhRecord = false;
                            $scope.ShowAddland = false
                            $scope.Addbtn = true;
                        }
                    }
                }
                else {
                    alert('The Area of Execution must not be Zero.');
                    $scope.txtAreaE = null;
                    $scope.ddlUnitAOE = null;
                }
            }
            else {
                alert('The Area of Execution must be less than or equal to the Total Land Area.');
                $scope.txtAreaE = null;
                $scope.ddlUnitAOE = null;
            }
        }
        else {
            alert('Please select all the fields.');
            return false;
        }
    };

    $scope.ClearOtherLand = function () {
        $scope.landDetailsrcd = false;
        $scope.bhulekhRecord = false;
        $scope.ShowAddland = false;
        $scope.Addbtn = false;
        $scope.LandrecordBeneficiary = [];
    };

    $scope.ClearLandDtls = function () {
        $scope.txtKhataNumber = null;
        $scope.txtPlotNumber = null;
        $scope.bhulandrec.TenantName = null;
        $scope.bhulandrec.Kisam = null;
        $scope.bhulandrec.AreaInHectare = null;
        $scope.bhulandrec.AreaInAcre = null;
        $scope.ddlDistrict = null;
        $scope.ddlRDistrict = null;
        $scope.ddlBlock = null;
        $scope.ddlRTehsil = null;
        $scope.ddlGP = null;
        $scope.ddlRICircle = null;
        $scope.ddlVillage = null;
        $scope.ddlRVillage = null;
        $scope.ddlRelationship = null;
        $scope.landrecvalidation = false;
        $scope.txtAreaE = null;
        $scope.ddlUnitAOE = null;
    };

    $scope.removelanddetails = function () {
        $scope.LandrecordBeneficiary.splice(idx, 1);
        if ($scope.LandrecordBeneficiary.length <= 0) {
            $scope.landrecvalidation = true;
            $scope.landDetailsrcd = false;
            $scope.Addbtn = false;
        }
        else {
            $scope.landrecvalidation = false;
            $scope.landDetailsrcd = true;
        }
        $scope.bhulekhRecord = false;
    };

    $scope.getIndex = function (a) {
        idx = a;
    };

    $scope.AddMoreFile = function () {
        if (document.getElementById('LanddocumentPdf1').files.length > 0) {
            $scope.AddFileupload = true;
            $scope.removeLand = true;
            $scope.addFile = false;
        }
        else {
            alert('Please upload atleast one land document. ');
            return false;
        }
    };

    $scope.RemoveFile = function () {
        document.getElementById('base64textarea5').value = null;
        document.getElementById('LanddocumentPdf2').value = null;
        $scope.removeLand = false;
        $scope.AddFileupload = false;
        $scope.addFile = true;
    };

    $scope.LandRecordSubmit = function () {
        var lndrec = [];
        angular.forEach($scope.LandrecordBeneficiary, function (i) {
            lndrec.push({ DistrictName: i.DistrictName, DistrictCode: i.DistCode, BlockName: i.BlockName, BlockCode: i.BlockCode, TehsilName: i.TehsilName, TehsilCode: i.TehsilCode, GPName: i.GPName, GPCode: i.GPCode, RICircleName: i.RICircleName, RICircleCode: i.RICircleCode, VillageName: i.VillageName, VillageCode: i.VillageCode, KhataNo: i.KhataNo, PlotNo: i.PlotNo, TenantName: i.TenantName, Kisam: i.Kisam, AreaInHectare: i.AreaInHectare, AreaInAcre: i.AreaInAcre, ExecutionArea: i.AreaOfExec, UnitExecution: i.UnitAOE, RelationshipCode: i.RelationshipCode });
        });
        if (document.getElementById("LanddocumentPdf1").files.length == 0) {
            alert("Plesase Upload a Land Document");
            return false;
        }
        if ($scope.removeLand == true) {
            if (document.getElementById("LanddocumentPdf2").files.length == 0) {
                alert("Plesase upload additional Land Document.");
                return false;
            }
        }
        if (document.getElementById("LanddocumentPdf1").files.length > 0) {
            if ($scope.LandrecordBeneficiary.length <= 0) {
                alert("Please Add Atleast one LandDetails");
                return false;
            }
            var landpdffile1 = document.getElementById("base64textarea4").value;
            var landpdffile2 = document.getElementById("base64textarea5").value;
            var mydata = { LandPdf1: landpdffile1, LandPdf2: landpdffile2 };
            var req = {
                method: 'POST',
                url: 'LandRecordSubmitDetails',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { x: lndrec, farmerID: farmerID, refno: referencenumber, pdfland: mydata, locality: $scope.rbLocality }
            };
            $http(req).then(function successCallback(response) {
                var t = response.data;
                getalllandRecord();
                if (t != null) {
                    if (t.includes("entered")) {
                        alert(t);
                        $scope.LandrecordBeneficiary = [];
                        $scope.landDetailsrcd = false;
                        $scope.landrecvalidation = true;
                        return false;
                    }
                    if (t.includes("all records")) {
                        alert(t);
                        $scope.LandrecordBeneficiary = [];
                        $scope.landDetailsrcd = false;
                        $scope.landrecvalidation = true;
                        return false;
                    }
                    if (t.includes("already exist.")) {
                        alert(t);
                        $scope.LandrecordBeneficiary = [];
                        $scope.landDetailsrcd = false;
                        $scope.landrecvalidation = true;
                        return false;
                    }
                    else {
                        alert("Records saved successfully.");
                        $http.get('ThirdSubmit?refno=' + t).then(function success(response) {
                            var L = response.data;
                            if (L != null) {
                                $scope.ThirdInput = false;
                                $scope.ThirdLabel = true;
                                $scope.ThirdSave = false;
                                $scope.lblRelationship = L.relation;
                                $scope.lblVillage = L.Village;
                                $scope.relationship.Code = L.relationID;
                                $scope.district.DistrictCode = L.distID;
                                $scope.gp.GPCode = L.GPID;
                                $scope.village.VillageCode = L.villageID;
                                $scope.gp1.GPCode = L.GPID;
                            }
                            else {
                                $scope.ThirdInput = true;
                                $scope.ThirdLabel = false;
                            }
                            $http.get('ThirdFormLandDetails?refno=' + referencenumber).then(function success(response) {
                                var t = response.data
                                if (t != null || t != "") {
                                    $scope.landdetails = t;
                                }
                                $http.get('GetDocumentfiles?refno=' + referencenumber).then(function success(response) {
                                    var t = response.data;
                                    if (t.pdffile1 != null) {
                                        $scope.firstpdf = true;
                                    }
                                    if (t.pdffile2 != null) {
                                        $scope.Secondpdf = true;
                                    }
                                }, function error(response) {
                                    alert(response.status);
                                });
                            }, function error(response) {
                                alert(response.status);
                            });
                        }, function error(response) {
                            alert(response.status);
                        });
                    }
                }
                else {
                    return false;
                }
            }, function errorCallback(response) {
                alert(response.status);
            });
        }
        else {
            return false;
        }
    };

    $scope.resetBank = function () {
        $scope.txtBankLoan = null;
        $scope.updateSum();
    };

    $scope.resetMBank = function () {
        $scope.txtBankLoan = null;
        $scope.updateSum();
    };

    $scope.UpdatePTCCGAADFile = function () {
        if (document.getElementById("updatePTCCGAAD").files.length == 0) {
            alert("Plesase upload a Graduation Certificate.");
            return false;
        }
        if (document.getElementById("updatePTCCGAAD").files.length > 0) {
            var ptcDoc = document.getElementById("base64textarea14").value;
            var mydata = { CastGraduationCertificate: ptcDoc };
        }
        var req = {
            method: 'POST',
            url: 'UpdateGradCert',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { refno: referencenumber, GraduationCertificate: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updatePTCCGAAD').value = null;
                document.getElementById('updatePTCCGAADModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEditPTCCGAAD").click();
                }, 1);
                $http.get('GetGraduationDocument?refno=' + referencenumber).then(function success(response) {
                    var t = response.data;
                    if (t.CastGraduationCertificate != null) {
                        $scope.PTCCGAAD = true;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateBankCCFile = function () {
        if (document.getElementById("updateBankCC").files.length == 0) {
            alert("Plesase upload a Bank Clearance Certificate.");
            return false;
        }
        if (document.getElementById("updateBankCC").files.length > 0) {
            var bankDoc = document.getElementById("base64textarea13").value;
            var mydata = { CISBankClearanceCertificate: bankDoc };
        }
        var req = {
            method: 'POST',
            url: 'UpdateBankDoc',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { refno: referencenumber, BankCertificate: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updateBankCC').value = null;
                document.getElementById('updateBCCModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEditBank").click();
                }, 1);
                $http.get('GetBankDocument?refno=' + referencenumber).then(function success(response) {
                    var t = response.data;
                    if (t.CISBankClearanceCertificate != null) {
                        $scope.cisBCC = true;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateBankCLFile = function () {
        if (document.getElementById("updateBankCL").files.length == 0) {
            alert("Plesase upload a Bank Consent Letter.");
            return false;
        }
        if (document.getElementById("updateBankCL").files.length > 0) {
            var bankCL = document.getElementById("base64textarea18").value;
            var mydata = { BankConsentLetter: bankCL };
        }
        var req = {
            method: 'POST',
            url: 'UpdateBankCL',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { refno: referencenumber, BankCL: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updateBankCL').value = null;
                document.getElementById('updateBCLModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEditBankCL").click();
                }, 1);
                $http.get('GetBankCLDoc?refno=' + referencenumber).then(function success(response) {
                    var t = response.data;
                    if (t.BankConsentLetter != null) {
                        $scope.bcl = true;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateIPs = function () {
        var msg = "";
        if ($scope.proTypeData1.length > 0) {
            angular.forEach($scope.proTypeData1, function (i) {
                if ((i.ICapacity != undefined && i.CapacityUnit == null) || (i.ICapacity == undefined && i.CapacityUnit != null)) {
                    msg = "The Capacity must be entered incase of Integrated Farming.";
                    return false;
                }
            });
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                angular.forEach($scope.proTypeData1, function (i) {
                    i.Capacity = i.ICapacity;
                });
                document.getElementById('ViewEditIPModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnVEIM").click();
                }, 1);
            }
        }
        if ($scope.proTypeData1.length == 0) {
            alert('Please enter the Integrated Projects to updated.');
            return false;
        }
    };

    $scope.clearproTypeData1 = function () {
        $scope.proTypeData1 = [];
    };

    $scope.UpdateLandDocument1 = function () {
        if (document.getElementById("updtlanddocument1").files.length == 0) {
            alert("Plesase upload a Land Document");
            return false;
        }
        if (document.getElementById("updtlanddocument1").files.length > 0) {
            var landpdffile1 = document.getElementById("base64textarea6").value;
            var mydata = { LandPdf1: landpdffile1 };
        }
        var req = {
            method: 'POST',
            url: 'UpdateLandDoc1',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { refno: referencenumber, pdfland: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updtlanddocument1').value = null;
                document.getElementById('updateLandModal1').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEdit1").click();
                }, 1);
                $http.get('GetDocumentfiles?refno=' + referencenumber).then(function success(response) {
                    var t = response.data;
                    if (t.pdffile1 != null) {
                        $scope.firstpdf = true;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateLandDocument2 = function () {
        if (document.getElementById("updatelanddocument2").files.length == 0) {
            alert("Plesase upload a Land Document");
            return false;
        }
        if (document.getElementById("updatelanddocument2").files.length > 0) {
            var landpdffile2 = document.getElementById("base64textarea7").value;
            var mydata = { LandPdf2: landpdffile2 };
        }
        var req = {
            method: 'POST',
            url: 'UpdateLandDoc2',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { refno: referencenumber, pdflandrec: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updatelanddocument2').value = null;
                document.getElementById('updateLandModal2').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEdit2").click();
                }, 1);
                $http.get('GetDocumentfiles?refno=' + referencenumber).then(function success(response) {
                    var t = response.data;
                    if (t.pdffile2 != null) {
                        $scope.Secondpdf = true;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateIDP = function () {
        if (document.getElementById("updateIDProof").files.length == 0) {
            alert("Plesase upload an Identity Proof");
            return false;
        }
        if (document.getElementById("updateIDProof").files.length > 0) {
            var idProof = document.getElementById("base64textarea8").value;
            var mydata = { IdentityProof: idProof };
        }
        var req = {
            method: 'POST',
            url: 'UpdateIDProof',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { referenceNo: referencenumber, identityProof: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updateIDProof').value = null;
                document.getElementById('updateIDProofModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEditIDProof").click();
                }, 1);
                $scope.registerDocumentsLabel = true;
                $scope.registerDocumentsInput = false;
                fetchBeneficiarydocdtls();
                $scope.idp = true;
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateP = function () {
        if (document.getElementById("updatePhoto").files.length == 0) {
            alert("Plesase upload a Photo");
            return false;
        }
        if (document.getElementById("updatePhoto").files.length > 0) {
            var photo = document.getElementById("base64textarea9").value;
            var mydata = { UPhoto: photo };
        }
        var req = {
            method: 'POST',
            url: 'UpdatePhoto',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { referenceNo: referencenumber, p: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updatePhoto').value = null;
                document.getElementById('updatePhotoModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEditPhoto").click();
                }, 1);
                $scope.registerDocumentsLabel = true;
                $scope.registerDocumentsInput = false;
                fetchBeneficiarydocdtls();
                $scope.idp = true;
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.UpdateS = function () {
        if (document.getElementById("updateSign").files.length == 0) {
            alert("Plesase upload a Photo");
            return false;
        }
        if (document.getElementById("updateSign").files.length > 0) {
            var sign = document.getElementById("base64textarea10").value;
            var mydata = { USign: sign };
        }
        var req = {
            method: 'POST',
            url: 'UpdateSign',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { referenceNo: referencenumber, s: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updateSign').value = null;
                document.getElementById('updateSignModal').style.display = "none";
                $timeout(function () {
                    document.getElementById("btnEditSign").click();
                }, 1);
                $scope.registerDocumentsLabel = true;
                $scope.registerDocumentsInput = false;
                fetchBeneficiarydocdtls();
                $scope.idp = true;
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.editLandRecord = function (i) {
        $scope.bhulekhRecord = false;
        $scope.bhulekhRecordupdate = true;
        $scope.txtKhataNumber = i.KhataNo;
        $scope.lblKhataNo = i.KhataNo;
        $scope.txtPlotNumber = i.PlotNo;
        $scope.lblPlotNo = i.PlotNo;
        $scope.lblDistrict = i.DistName;
        $scope.lblDistrictCode = i.DistCode;
        $scope.lblBlockCode = i.BlockCode;
        $scope.rDistCode = i.RDistCode;
        $scope.lblBlock = i.BlockName;
        $scope.dlGP = i.GPCode;
        $scope.lblLocality = i.Locality;
        $scope.gramPanchayatCode = i.GPCode;
        $scope.Village = i.VillageCode;
        $scope.lblVillageCode = i.VillageCode;
        $scope.Relationship = i.RelationshipCode;
        $scope.lblTenantName = i.TenantName;
        $scope.lblKisam = i.Kisam;
        $scope.lblAreaInHectare = i.AreaInHectare;
        $scope.lblAreaInAcre = i.AreaInAcre;
        $scope.txtAreaExec = i.ExecArea;
        $scope.ddlUnitExec = i.ExecUnit;
        DistCodeforUpdate = i.DistCode;
        gpPopulate();
        villagePopulate();
    };

    $scope.resetVillage = function () {
        $scope.bhulekhRecord = false;
        $scope.ddlVillage = null;
    };

    $scope.resetRVillage = function () {
        $scope.bhulekhRecord = false;
        $scope.ddlRVillage = null;
    };

    $scope.resetModalVillage = function () {
        $scope.bhulekhRecordupdate = false;
        $scope.bhulekhRecord = false;
        $scope.Village = null;
        $scope.dlGP = null;
    };

    $scope.RVillageChange = function () {
        if ($scope.txtKhataNumber !== undefined && $scope.txtPlotNumber !== undefined && $scope.ddlRVillage !== undefined) {
            $http.get('GetBeneficiaryBhulekhRecord?dCode=' + $scope.ddlRDistrict.DCode + '&tCode=' + $scope.ddlRTehsil.TCode + '&vCode=' + $scope.ddlRVillage.VCode + '&khataNo=' + $scope.txtKhataNumber + '&plotNo=' + $scope.txtPlotNumber).then(function success(response) {
                if (response.data != "") {
                    $scope.bhulandrec = response.data;
                    $scope.bhulekhRecord = true;
                }
                else {
                    alert("No record found for these land details.");
                    $scope.bhulekhRecord = false;
                    $scope.ddlVillage = null;
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            return null;
        }
    };

    $scope.VillagedataChange = function () {
        if ($scope.txtKhataNumber !== undefined && $scope.txtPlotNumber !== undefined && $scope.ddlVillage !== undefined) {
            $http.get('GetBeneficiaryLandrecord?khataNo=' + $scope.txtKhataNumber + '&plotNo=' + $scope.txtPlotNumber + '&rDistrictID=' + $scope.ddlDistrict.RevenueDistrictCode + '&villageCode=' + $scope.ddlVillage.VillageCode).then(function success(response) {
                if (response.data != "") {
                    $scope.bhulandrec = response.data;
                    $scope.bhulekhRecord = true;
                }
                else {
                    alert("No record found for these land details.");
                    $scope.bhulekhRecord = false;
                    $scope.ddlVillage = null;
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            return null;
        }
    };

    $scope.VillagedataChange1 = function () {
        if ($scope.txtKhataNumber !== undefined && $scope.txtPlotNumber !== undefined && $scope.Village !== undefined) {
            $http.get('GetBeneficiaryLandrecord?Khatano=' + $scope.txtKhataNumber + '&Plotno=' + $scope.txtPlotNumber + '&rDistrictID=' + $scope.rDistCode + '&villagecode=' + $scope.Village).then(function success(response) {
                if (response.data != "") {
                    $scope.bhulandrec = response.data;
                    $scope.bhulekhRecordupdate = false;
                    $scope.bhulekhRecord = true;
                }
                else {
                    alert("No record found for these land details.");
                    $scope.bhulekhRecordupdate = false;
                    $scope.bhulekhRecord = false;
                    $scope.Village = null;
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            $scope.bhulekhRecordupdate = false;
            $scope.bhulekhRecord = false;
            return false;
        }
    };

    $scope.UpdateLandRecord = function () {
        var area = null;
        if ($scope.ddlUnitExec == "Area in Acre") {
            if ($scope.bhulekhRecord == true) {
                area = $scope.bhulandrec.AreaInAcre;
            }
            else if ($scope.bhulekhRecordupdate == true) {
                area = $scope.lblAreaInAcre;
            }
        }
        else if ($scope.ddlUnitExec == "Area in Hectare") {
            if ($scope.bhulekhRecord == true) {
                area = $scope.bhulandrec.AreaInHectare;
            }
            else if ($scope.bhulekhRecordupdate == true) {
                area = $scope.lblAreaInHectare;
            }
        }
        if ($scope.txtAreaExec <= area) {
            if ($scope.txtAreaExec != "0") {
                if ($scope.txtKhataNumber !== undefined && $scope.txtKhataNumber !== null && $scope.txtPlotNumber !== undefined && $scope.txtPlotNumber !== null && $scope.Village !== null && $scope.Relationship !== null && $scope.lblDistrict != undefined && $scope.lblBlock != undefined && $scope.dlGP != null && $scope.txtAreaExec != undefined && $scope.txtAreaExec != null && $scope.ddlUnitExec != undefined && $scope.ddlUnitExec != null) {
                    if ($scope.bhulekhRecord == true) {
                        var mydata = { ReferenceNo: referencenumber, DistrictCode: $scope.lblDistrictCode, BlockCode: $scope.lblBlockCode, VillageCode: $scope.Village, KhataNo: $scope.txtKhataNumber, PlotNo: $scope.txtPlotNumber, TenantName: $scope.bhulandrec.TenantName, Kisam: $scope.bhulandrec.Kisam, AreaInHectare: $scope.bhulandrec.AreaInHectare, AreaInAcre: $scope.bhulandrec.AreaInAcre, RelationshipCode: $scope.Relationship, GPCode: $scope.dlGP, ExecutionArea: $scope.txtAreaExec, UnitExecution: $scope.ddlUnitExec, Locality: $scope.lblLocality };
                    }
                    else if ($scope.bhulekhRecordupdate == true) {
                        var mydata = { ReferenceNo: referencenumber, DistrictCode: $scope.lblDistrictCode, BlockCode: $scope.lblBlockCode, VillageCode: $scope.Village, KhataNo: $scope.txtKhataNumber, PlotNo: $scope.txtPlotNumber, TenantName: $scope.lblTenantName, Kisam: $scope.lblKisam, AreaInHectare: $scope.lblAreaInHectare, AreaInAcre: $scope.lblAreaInAcre, RelationshipCode: $scope.Relationship, GPCode: $scope.dlGP, ExecutionArea: $scope.txtAreaExec, UnitExecution: $scope.ddlUnitExec, Locality: $scope.lblLocality };
                    }
                    else if ($scope.bhulekhRecord == false && $scope.bhulekhRecordupdate == false) {
                        alert("Land record detail is not available.");
                        return false;
                    }
                    else {
                        return false;
                    }
                    var req = {
                        method: 'POST',
                        url: 'UpdateLandRecord?gpCode=' + $scope.gramPanchayatCode + '&villageCode=' + $scope.lblVillageCode + '&khataNo=' + $scope.lblKhataNo + '&plotNo=' + $scope.lblPlotNo,
                        headers: {
                            '__RequestVerificationToken': token
                        },
                        data: { x: mydata }
                    };
                    $http(req).then(function successCallback(response) {
                        var t = response.data;
                        if (t != null || t == false) {
                            alert(t);
                            document.getElementById('ThirdForm').style.display = "none";
                            $timeout(function () {
                                document.getElementById("btnLandEdit").click();
                            }, 1);
                            getalllandRecord();
                            $http.get('ThirdSubmit?refno=' + t).then(function success(response) {
                                var L = response.data;
                                if (L != null) {
                                    $scope.ThirdInput = false;
                                    $scope.ThirdLabel = true;
                                    $scope.ThirdSave = false;
                                    $scope.lblRelationship = L.relation;
                                    $scope.lblVillage = L.Village;
                                    $scope.relationship.Code = L.relationID;
                                    $scope.district.DistrictCode = L.distID;
                                    $scope.gp.GPCode = L.GPID;
                                    $scope.village.VillageCode = L.villageID;
                                    $scope.gp1.GPCode = L.GPID;
                                }
                                else {
                                    $scope.ThirdInput = true;
                                    $scope.ThirdLabel = false;
                                }
                                $http.get('ThirdFormLandDetails?refno=' + referencenumber).then(function success(response) {
                                    var t = response.data
                                    if (t != null || t != "") {
                                        $scope.landdetails = t;
                                    }
                                    $http.get('GetDocumentfiles?refno=' + referencenumber).then(function success(response) {
                                        var t = response.data;
                                        if (t.pdffile1 != null) {
                                            $scope.firstpdf = true;
                                        }
                                        if (t.pdffile2 != null) {
                                            $scope.Secondpdf = true;
                                        }
                                    }, function error(response) {
                                        alert(response.status);
                                    });
                                }, function error(response) {
                                    alert(response.status);
                                });
                            }, function error(response) {
                                alert(response.status);
                            });
                        }
                        else {
                            return false;
                        }
                    }, function errorCallback(response) {
                        alert(response.status);
                    });
                }
                else {
                    alert('Please select all the fields.');
                    $scope.txtKhataNumber = null;
                    $scope.txtPlotNumber = null;
                    $scope.Village = null;
                    $scope.dlGP = null;
                    $scope.bhulekhRecord = null;
                    $scope.bhulekhRecordupdate = null;
                    $scope.Relationship = null;
                    $scope.txtAreaExec = null;
                    $scope.ddlUnitExec = null;
                    return false;
                }
            }
            else {
                alert('The Area of Execution must not be Zero.');
                $scope.txtAreaExec = null;
                $scope.ddlUnitExec = null;
            }
        }
        else {
            alert('The Area of Execution must be less than or equal to the Total Land Area.');
            $scope.txtAreaExec = null;
            $scope.ddlUnitExec = null;
        }
    };

    $scope.DocementSubmit = function () {
        if (document.getElementById('Imagefile').files.length == 0) {
            alert("Please Upload Image");
            return false;
        }
        if (document.getElementById('FrcUpld').files.length == 0) {
            alert("Please Upload IdentityProof");
            return false;
        }
        if (document.getElementById("signfile").files.length == 0) {
            alert("Please Upload Full Signature");
            return false;
        }
        if ($scope.PutReferenceNumber != null || $scope.PutReferenceNumber != "") {
            refno = $scope.PutReferenceNumber;
        }
        var imageupld = document.getElementById("base64textarea1").value;
        var documntpld = document.getElementById("base64textarea2").value;
        var signature = document.getElementById("base64textarea3").value;
        var x = { ReferenceNo: referencenumber, Photograph: imageupld, IdentityProof: documntpld, Signature: signature };
        var mydata2 = { BLOStatus: 0, ReferenceNo: referencenumber };
        var mydata3 = { DNOStatus: 0, ReferenceNo: referencenumber }
        var req = {
            method: 'POST',
            url: 'SubmitDocumentdtls',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { mydata: x, y: mydata2, z: mydata3 }
        };
        $http(req).then(function successCallback(response) {
            var t = response.data;
            if (t.includes("Invalid")) {
                alert(response.data);
            }
            else {
                alert("Records submitted successfully.");
            }
            fetchBeneficiarydocdtls();
        }, function errorCallback(response) {
            alert(response.status);
        });
    };

    $scope.printTo = function (printSectionId) {
        var innerContents = document.getElementById(printSectionId).innerHTML;
        var popupWinindow = window.open('', '_blank', 'width=1020,height=900,toolbar=1,resizable=1,location=0, status=0, menubar=0, scrollbars=1');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"></head><body onload="window.print()">' + innerContents + '</html>');
        popupWinindow.document.close();
    };

    $scope.emptyField = function () {
        $scope.txtFirmName = '';
        $scope.txtNoOfMembers = '';
        $scope.FarmerMobileNumber = '';
        $scope.elements = [];
        $scope.validGroup = false;
        $scope.hideFields();
    };

    $scope.hideFields = function () {
        $scope.FarmerDetails = false;
        $scope.ValidateFarmer = false;
        $scope.txtFarmerID = "";
        $scope.ddlHighestEducationalQualification = null;
        $scope.txtAnnualIncome = '';
        $scope.txtPresentOccupation = '';
        $scope.rbPreviousExperience = false;
        $scope.txtEmailID = '';
        $scope.txtAddressForCorrespondence = '';
        $scope.txtPermanentAddress = '';
        $scope.inputFarmerID = true;
        $scope.labelFarmerID = false;
        $scope.txtPin = '';
        $scope.rbRelationType = null;
        $scope.ddlRelationFID = null;
        $scope.txtRVoterID = '';
        $scope.txtRAadhaarNo = '';
        $scope.relationFields = false;
        $scope.relationSelf = false;
        $scope.rTShow = false;
        $scope.rbRelationName = false;
        $scope.relationMul = [];
    };

    $scope.updateSum = function () {
        if ($scope.txtBankLoan == null) {
            var k = parseFloat($scope.txtOwnContribution).toFixed(2);
            (k == "NaN") ? $scope.txtTotal = "" : $scope.txtTotal = k;
        }
        else {
            var k = (parseFloat($scope.txtOwnContribution) + parseFloat($scope.txtBankLoan)).toFixed(2);
            (k == "NaN") ? $scope.txtTotal = "" : $scope.txtTotal = k;
        }
    };

    $scope.educationalQualification = [];
    $http.get('GetEducationalQualification').then(function success(response) {
        $scope.educationalQualification = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.showHidePTCCG = function (a) {
        (a == "4") ? $scope.PTCCG = true : $scope.PTCCG = false; document.getElementById('casteGraduation').value = null; document.getElementById('base64textarea12').value = null;
    };

    $scope.showHideUPDTPTCCG = function (a) {
        (a == "4") ? $scope.UPDTPTCCG = true : $scope.UPDTPTCCG = false; document.getElementById('updtCasteGraduation').value = null; document.getElementById('base64textarea12').value = null;
    };

    $scope.department = [];
    $http.get('GetDepartment').then(function success(response) {
        $scope.department = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.pType = [];
    $http.get('ProjectType').then(function success(response) {
        $scope.pType = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.projectType = [];
    $scope.populateProjectType = function (a) {
        $http.get('GetProjectType?departmentCode=' + a).then(function success(response) {
            $scope.AllProject = [];
            $scope.AllProject = response.data;
            $scope.projectType = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    var ProjectTypeDept = function () {
        $http.get('GetAllProjectType?deptcode=' + $scope.t.DepartmentID).then(function success(response) {
            $scope.AllProject = response.data;
            $scope.AllProject.Code = $scope.t.ProjectTypeID;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.bank = [];
    $http.get('GetBank').then(function success(response) {
        $scope.bank = response.data;
    }, function error(response) {
        alert(response.status);
    });

    $scope.branch = [];
    $scope.populateBranch = function (a) {
        $http.get('GetBranch?bankCode=' + a).then(function success(response) {
            $scope.branch = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    var getBranchbyBank = function () {
        if ($scope.t.BankCode != null) {
            $http.get('GetAllBranch?bankCode=' + $scope.t.BankCode).then(function success(response) {
                $scope.branch = response.data;
                $scope.branch.intBranchId = $scope.t.BranchCode;
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    $scope.grouptype = [];
    var loadGroupType = function () {
        $http.get('GetGroupTypes').then(function success(response) {
            $scope.grouptype = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.showRelationType = function () {
        if ($scope.txtFarmerID == "" || $scope.txtFarmerID == "" || $scope.txtFarmerID == undefined) {
            alert("FarmerID Should not be Left Blank");
            $scope.rTShow = false;
            $scope.inputFarmerID = true;
            $scope.labelFarmerID = false;
            return false;
        }
        else {
            $http.get('CheckExistFarmerID?farmerID=' + $scope.txtFarmerID).then(function success(response) {
                var t = response.data;
                if (t != null && t != "") {
                    if (t.includes("already")) {
                        alert("Farmer ID " + $scope.txtFarmerID.toUpperCase() + " is already assigned a project.");
                        $scope.txtFarmerID = "";
                        $scope.rTShow = false;
                        $scope.inputFarmerID = true;
                        $scope.labelFarmerID = false;
                        return false;
                    }
                }
                else {
                    $scope.rTShow = true;
                    $scope.inputFarmerID = false;
                    $scope.labelFarmerID = true;
                }
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    $scope.confirmRelation = function (i) {
        if (confirm('Are you sure the Applicant Name should be ' + i + '?')) {
            $scope.lblApplicantName = i;
            $scope.relSin = true;
            $scope.relMul = false;
        }
        else {
            $scope.lblApplicantName = null;
            $scope.relSin = false;
            $scope.relMul = true;
        }
    };

    $scope.changeAN = function () {
        $scope.lblApplicantName = null;
        $scope.relSin = false;
        $scope.relMul = true;
        document.getElementsByName("RelationName").checked = false;
    }

    $scope.SHRF = function () {
        $http.get('FIDRelationship?farmerID=' + $scope.txtFarmerID.toUpperCase()).then(function success(response) {
            $scope.fidRelationship = response.data;
            if ($scope.fidRelationship.length == 0) {
                $scope.relationFields = false;
                alert('No relations are found in this Farmer ID');
            }
        }, function error(response) {
            alert(response.status);
        });
        $scope.relationFields = true;
        $scope.txtFirmName = '';
        $scope.txtNoOfMembers = '';
        $scope.FarmerMobileNumber = '';
        $scope.elements = [];
        $scope.validGroup = false;
        $scope.FarmerDetails = false;
        $scope.ValidateFarmer = false;
        $scope.ddlHighestEducationalQualification = null;
        $scope.txtAnnualIncome = '';
        $scope.txtPresentOccupation = '';
        $scope.rbPreviousExperience = false;
        $scope.txtEmailID = '';
        $scope.txtAddressForCorrespondence = '';
        $scope.txtPermanentAddress = '';
        $scope.txtPin = '';
        $scope.ddlRelationFID = null;
        $scope.txtRVoterID = '';
        $scope.txtRAadhaarNo = '';
        $scope.rbRelationName = false;
        $scope.relationMul = [];
    };

    $scope.relSin = true;
    $scope.validateFarmerID = function () {
        if ($scope.rbRelationType == null) { alert("Relation Type must be selected."); return false; }
        if ($scope.rbRelationType == "Relative" && $scope.ddlRelationFID == null) {
            alert("Relation must be selected.");
            return false;
        }
        if ($scope.txtFarmerID == "" || $scope.txtFarmerID == "" || $scope.txtFarmerID == undefined) {
            alert("FarmerID Should not be Left Blank");
            return false;
        }
        $http.get('CheckExistFarmerID?farmerID=' + $scope.txtFarmerID).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                if (t.includes("already")) {
                    alert("Farmer ID " + $scope.txtFarmerID.toUpperCase() + " is already assigned a project.");
                    $scope.txtFarmerID = "";
                    return false;
                }
            }
            else {
                $http.get('GetApplicantDetail?farmerID=' + $scope.txtFarmerID.toUpperCase() + '&rType=' + $scope.rbRelationType + '&relation=' + $scope.ddlRelationFID).then(function success(response) {
                    $scope.t = response.data;
                    if ($scope.t != "") {
                        if ($scope.t.ApplicantName.includes(',')) {
                            $scope.relationMul = $scope.t.ApplicantName.split(',');
                            $scope.relMul = true;
                            $scope.relSin = false;
                            $scope.lblApplicantName = null;
                        }
                        else {
                            $scope.relMul = false;
                            $scope.relSin = true;
                            $scope.lblApplicantName = $scope.t.ApplicantName;
                            $scope.relationMul = [];
                        }
                        $scope.showButtonGenerate = false;
                        if (($scope.t.VoterIDCardNo != "" && $scope.t.VoterIDCardNo != "NA" && $scope.rbRelationType == "Self") || (($scope.t.VoterIDCardNo == null || $scope.t.VoterIDCardNo == "" || $scope.t.VoterIDCardNo == "NA") && $scope.rbRelationType == "Relative")) {
                            if (($scope.t.AadharIDCardNo != "" && $scope.t.AadharIDCardNo != "NA" && $scope.rbRelationType == "Self") || (($scope.t.AadharIDCardNo == null || $scope.t.AadharIDCardNo == "" || $scope.t.AadharIDCardNo == "NA") && $scope.rbRelationType == "Relative")) {
                                if ($scope.t.MobileNo != "" && $scope.t.MobileNo != "NA") {
                                    mobilenumber = $scope.t.MobileNo;
                                    $scope.validMobileno = true;
                                    $scope.FarmerDetails = true;
                                    if ($scope.rbApplicantType == "Group") {
                                        $scope.validGroup = true;
                                        $scope.inputFarmerID = false;
                                        $scope.labelFarmerID = true;
                                        loadGroupType();
                                    }
                                    else {
                                        $scope.ValidateFarmer = true;
                                        $scope.inputFarmerID = false;
                                        $scope.labelFarmerID = true;
                                    }
                                    if ($scope.rbRelationType == "Self") {
                                        $scope.relationSelf = true;
                                        $scope.relationFields = false;
                                    }
                                    else {
                                        $scope.relationSelf = false;
                                        $scope.relationFields = true;
                                    }
                                }
                                else {
                                    $scope.InvalidMobileno = true;
                                    $scope.FarmerDetails = true;
                                    $scope.inputFarmerID = false;
                                    $scope.labelFarmerID = true;
                                    if ($scope.rbApplicantType == "Group") {
                                        $scope.validGroup = true;
                                        $scope.inputFarmerID = false;
                                        $scope.labelFarmerID = true;
                                        loadGroupType();
                                    }
                                    else {
                                        $scope.ValidateFarmer = true;
                                        $scope.inputFarmerID = false;
                                        $scope.labelFarmerID = true;
                                    }
                                }
                            }
                            else {
                                alert("Aadhaar number is not updated. Please update your Aadhaar number at nearest AAO.");
                                $scope.ValidateFarmer = false;
                                $scope.FarmerDetails = false;
                                $scope.validGroup = false;
                                $scope.inputFarmerID = true;
                                $scope.labelFarmerID = false;
                                $scope.emptyField();
                            }
                        }
                        else {
                            alert("Voter ID is not available. ");
                            $scope.ValidateFarmer = false;
                            $scope.FarmerDetails = false;
                            $scope.validGroup = false;
                            $scope.inputFarmerID = true;
                            $scope.labelFarmerID = false;
                            $scope.emptyField();
                        }
                    }
                    else {
                        $scope.FarmerDetails = false;
                        alert("You have entered an invalid Farmer ID");
                        $scope.txtFarmerID = "";
                        $scope.inputFarmerID = true;
                        $scope.labelFarmerID = false;
                        $scope.emptyField();
                        return false;
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    var open = function () {
        $scope.showModal = true;
    };

    $scope.landRecord = [];
    $scope.grMem = [];
    $scope.iProjects = [];
    $scope.ApplicantAcknowledgement = function () {
        $http.get('GetApplicantAcknowledgement?refno=' + referencenumber).then(function success(response) {
            $scope.t = response.data;
            passphoto($scope.t.Photo);
            uploadedsign($scope.t.Signature);
            $http.get('Alllandrecord?refno=' + referencenumber).then(function success(response) {
                $scope.landRecord = response.data;
                $scope.localRU = response.data[0].Locality;
            }, function error(response) {
                alert(response.status);
            });
            $http.get('AllGroupMembers?refno=' + referencenumber).then(function success(response) {
                $scope.grMem = response.data;
            }, function error(response) {
                alert(response.status);
            });
            $http.get('GetIProjects?referenceNo=' + referencenumber).then(function success(response) {
                $scope.iProjects = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }, function error(response) {
            alert(response.status);
        });
    };

    var image = function bin2string(array) {
        var result = "";
        for (var i = 0; i < array.length; ++i) {
            result += (String.fromCharCode(array[i]));
        }
        return result;
    };

    var passphoto = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('passpic').setAttribute("src", obj);
    };

    var passportphoto = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('passportpic').setAttribute("src", obj);
        document.getElementById('repassportpic').setAttribute("src", obj);
    };

    var uploadedsign = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('photoSignature').setAttribute("src", obj);
    };

    var beneficiarysignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('beneficiarySignature').setAttribute("src", obj);
        document.getElementById('rebeneficiarySignature').setAttribute("src", obj);
    };

    $scope.AllLAndRec = [];
    var getalllandRecord = function () {
        $http.get('Alllandrecord?refno=' + referencenumber).then(function success(response) {
            $scope.AllLAndRec = response.data;
            if (response.data.length != 0) {
                $scope.localityRU = response.data[0].Locality;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.ProType = [];
    $scope.ProTypeSelected = [];
    $scope.ChangeProjectType = function () {
        $scope.txtCapacity = "";
        $scope.ddlCapacity = null;
        if ($scope.ddlProjectType.Code == "04P15") {
            $http.get('ProjectType').then(function (data) {
                angular.forEach(data.data, function (value, index) {
                    $scope.ProType.push({ id: value.Code, label: value.Name, code: value.Code.substring(0, 2), capacity: value.MinCapacity, cUnit: value.CapacityUnit });
                });
            });
        }
        else {
            $scope.ProType = [];
            $scope.ProTypeSelected = [];
            $scope.proTypeData = [];
        }
        if ($scope.ddlProjectType !== undefined) {
            $http.get('ProjecttypeChange?pCode=' + $scope.ddlProjectType.Code).then(function success(response) {
                var t = response.data;
                if (t != null && t != "") {
                    if (t == "success") {
                        $scope.Projecttypesuccess = true;
                    }
                    else {
                        $scope.Projecttypesuccess = false;
                    }
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            return false;
        }
    };

    $scope.ProType1 = [];
    $scope.ProTypeSelected1 = [];
    $scope.updtIPModal = function () {
        if ($scope.AllProject.Code == "04P15") {
            $scope.ProType1 = [];
            $scope.ProTypeSelected1 = [];
            $scope.proTypeData1 = [];
            $http.get('ProjectType').then(function (data) {
                angular.forEach(data.data, function (value, index) {
                    $scope.ProType1.push({ id: value.Code, label: value.Name, code: value.Code.substring(0, 2), capacity: value.MinCapacity, cUnit: value.CapacityUnit });
                });
            });
        }
        else {
            $scope.ProType1 = [];
            $scope.ProTypeSelected1 = [];
            $scope.proTypeData1 = [];
        }
    };

    $scope.ProType2 = [];
    $scope.ProTypeSelected2 = [];
    $scope.ChangeProjectTypeupdt = function () {
        if ($scope.AllProject.Code == "04P15") {
            $scope.ProType2 = [];
            $scope.ProTypeSelected2 = [];
            $scope.proTypeData2 = [];
            $scope.proTypeData1 = [];
            getIntegratedProjects();
            $http.get('ProjectType').then(function (data) {
                angular.forEach(data.data, function (value, index) {
                    $scope.ProType2.push({ id: value.Code, label: value.Name, code: value.Code.substring(0, 2), capacity: value.MinCapacity, cUnit: value.CapacityUnit });
                });
            });
        }
        else {
            $scope.ProType2 = [];
            $scope.ProTypeSelected2 = [];
            $scope.proTypeData2 = [];
        }
        $scope.txtCapacity = "";
        $scope.ddlCapacity = null;
        if ($scope.AllProject.Code !== undefined) {
            $http.get('ProjecttypeChange?pCode=' + $scope.AllProject.Code).then(function success(response) {
                var t = response.data;
                if (t != null && t != "") {
                    if (t == "success") {
                        $scope.Projecttypesuccess = true;
                    }
                    else {
                        $scope.Projecttypesuccess = false;
                    }
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            return false;
        }
    };

    $scope.ChangeProjectCapacity = function () {
        if ($scope.ddlProjectType !== null && $scope.txtCapacity !== undefined && $scope.txtCapacity !== "" && $scope.ddlCapacity !== null) {
            $http.get('ProjectCapacityChange?pCode=' + $scope.ddlProjectType.Code + '&Capacity=' + $scope.txtCapacity + '&Cunit=' + $scope.ddlCapacity).then(function success(response) {
                var t = response.data;
                if (t != null && t != "") {
                    if (t != "success") {
                        alert(t);
                        $scope.txtCapacity = "";
                        $scope.ddlCapacity = null;
                        return false;
                    }
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('The Capacity must be entered before selecting the unit.');
            $scope.ddlCapacity = null;
        }
    };

    $scope.ChangeProjectCapacityforchk = function () {
        if ($scope.AllProject !== null && $scope.txtCapacity !== undefined && $scope.txtCapacity !== "" && $scope.ddlCapacity !== null) {
            $http.get('ProjectCapacityChange?pCode=' + $scope.AllProject.Code + '&Capacity=' + $scope.txtCapacity + '&Cunit=' + $scope.ddlCapacity).then(function success(response) {
                var t = response.data;
                if (t != null && t != "") {
                    if (t != "success") {
                        alert(t);
                        $scope.txtCapacity = "";
                        $scope.ddlCapacity = null;
                        return false;
                    }
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('The Capacity must be entered before selecting the unit.');
            $scope.ddlCapacity = null;
        }
    };

    var handleFileSelect1 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea1").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('Imagefile').addEventListener('change', handleFileSelect1, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect2 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea2").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('FrcUpld').addEventListener('change', handleFileSelect2, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect3 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea3").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        var el = document.getElementById('signfile').addEventListener('change', handleFileSelect3, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect4 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea4").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('LanddocumentPdf1').addEventListener('change', handleFileSelect4, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect5 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea5").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('LanddocumentPdf2').addEventListener('change', handleFileSelect5, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect6 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea6").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updtlanddocument1').addEventListener('change', handleFileSelect6, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect7 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea7").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        window.document.getElementById('updatelanddocument2').addEventListener('change', handleFileSelect7, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect8 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea8").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updateIDProof').addEventListener('change', handleFileSelect8, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect9 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea9").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updatePhoto').addEventListener('change', handleFileSelect9, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect10 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea10").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updateSign').addEventListener('change', handleFileSelect10, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect11 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea11").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('bankClearance').addEventListener('change', handleFileSelect11, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect12 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea12").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('casteGraduation').addEventListener('change', handleFileSelect12, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect13 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea13").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updateBankCC').addEventListener('change', handleFileSelect13, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect14 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea14").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updatePTCCGAAD').addEventListener('change', handleFileSelect14, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect15 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea15").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updtCasteGraduation').addEventListener('change', handleFileSelect15, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect16 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea16").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updtBankClearance').addEventListener('change', handleFileSelect16, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect17 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea17").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('bankConsent').addEventListener('change', handleFileSelect17, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect18 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea18").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updateBankCL').addEventListener('change', handleFileSelect18, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };

    var handleFileSelect19 = function (evt) {
        var files = evt.target.files;
        var file = files[0];
        if (files && file) {
            var reader = new FileReader();
            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                document.getElementById("base64textarea19").value = btoa(binaryString);
            };
            reader.readAsBinaryString(file);
        }
    };
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.getElementById('updtBankCL').addEventListener('change', handleFileSelect19, false);
    } else {
        alert('The File APIs are not fully supported in this browser.');
    };
});