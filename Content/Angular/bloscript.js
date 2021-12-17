var app = angular.module("BLODApp", ['ui.bootstrap.modal']);

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

app.directive('integerAll', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.on('keydown', function (event) {
                var keyCode = []
                if (attrs.allowNegative == "true") {
                    keyCode = [8, 9, 36, 35, 37, 39, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 109, 110, 173, 190, 189];
                }
                else {
                    var keyCode = [8, 9, 36, 35, 37, 39, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 110, 173, 190];
                }
                if (attrs.allowDecimal == "false") {
                    var index = keyCode.indexOf(190);
                    if (index > -1) {
                        keyCode.splice(index, 1);
                    }
                }
                if ($.inArray(event.which, keyCode) == -1) event.preventDefault();
                else {
                    var oVal = ngModelCtrl.$modelValue || '';
                    if ($.inArray(event.which, [109, 173]) > -1 && oVal.indexOf('-') > -1) event.preventDefault();
                    else if ($.inArray(event.which, [110, 190]) > -1 && oVal.indexOf('.') > -1) event.preventDefault();
                }
            })
            .on('blur', function () {
                if (element.val() == '' || parseFloat(element.val()) == 0.0 || element.val() == '-') {
                    ngModelCtrl.$setViewValue('');
                }
                else if (attrs.allowDecimal == "false") {
                    ngModelCtrl.$setViewValue(element.val());
                }
                else {
                    if (attrs.decimalUpto) {
                        var fixedValue = parseFloat(element.val()).toFixed(attrs.decimalUpto);
                    }
                    else { var fixedValue = parseFloat(element.val()).toFixed(2); }
                    ngModelCtrl.$setViewValue(fixedValue);
                }
                ngModelCtrl.$render();
                scope.$apply();
            });
            ngModelCtrl.$parsers.push(function (text) {
                var oVal = ngModelCtrl.$modelValue;
                var nVal = ngModelCtrl.$viewValue;
                if (parseFloat(nVal) != nVal) {
                    if (nVal === null || nVal === undefined || nVal == '' || nVal == '-') oVal = nVal;
                    ngModelCtrl.$setViewValue(oVal);
                    ngModelCtrl.$render();
                    return oVal;
                }
                else {
                    var decimalCheck = nVal.split('.');
                    if (!angular.isUndefined(decimalCheck[1])) {
                        if (attrs.decimalUpto)
                            decimalCheck[1] = decimalCheck[1].slice(0, attrs.decimalUpto);
                        else
                            decimalCheck[1] = decimalCheck[1].slice(0, 2);
                        nVal = decimalCheck[0] + '.' + decimalCheck[1];
                    }
                    ngModelCtrl.$setViewValue(nVal);
                    ngModelCtrl.$render();
                    return nVal;
                }
            });
            ngModelCtrl.$formatters.push(function (text) {
                if (text == '0' || text == null && attrs.allowDecimal == "false") return '0';
                else if (text == '0' || text == null && attrs.allowDecimal != "false" && attrs.decimalUpto == undefined) return '';
                else if (text == '0' || text == null && attrs.allowDecimal != "false" && attrs.decimalUpto != undefined) return parseFloat(0).toFixed(attrs.decimalUpto);
                else if (attrs.allowDecimal != "false" && attrs.decimalUpto != undefined) return parseFloat(text).toFixed(attrs.decimalUpto);
                else return parseFloat(text).toFixed(2);
            });
        }
    };
});

app.filter('capitalize', function () {
    return function (input) {
        return (!!input) ? input.charAt(0).toUpperCase() + input.substr(1).toLowerCase() : '';
    }
});

app.controller("BLODCtrl", function ($scope, $http, $timeout) {

    var token = document.getElementsByName('__RequestVerificationToken')[0].value;

    $scope.approval = false;
    $scope.DROPDOWNLIST = false;

    $scope.getall = [];
    var Userlist = function () {
        $http.get('GetUserList').then(function success(response) {
            $scope.getall = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };
    Userlist();

    var clear = function () {
        $scope.DocDetali = null;
    };
    clear();

    $scope.getDeptCode = function (dc) {
        if (dc == "AAO") { $scope.deptCode = "01"; } else if (dc == "AHO") { $scope.deptCode = "02"; } else { $scope.deptCode = "03"; }
    };

    //$scope.BloSelectGO = function () {
    //    mydata = { PreviousReferenceNo: $scope.referenceNo, NewDeptCode: deptcode };
    //    var req = {
    //        method: 'POST',
    //        url: 'IntegratedProjectChangebyBLO',
    //        headers: {
    //            '__RequestVerificationToken': token,
    //        },
    //        data: { X: mydata }
    //    };
    //    $http(req).then(function success(response) {
    //        if (response.data == "Project is sent to the concerned BLO.") {
    //            alert(response.data);
    //            document.getElementById('Bloselect').style.display = "none";
    //            $timeout(function () {
    //                document.getElementById("btnEdit1").click();
    //            }, 1);
    //            Userlist();
    //        }
    //    }, function error(response) {
    //        alert(response.status);
    //    });
    //};

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

    $scope.viewpdffile = function () {
        $http.get('DisplayPDF?refno=' + $scope.AaoRefno.refno, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, '', '_blank');
        }, function error(response) {
            alert(response.status);
        });
    };

    var landdocument = function () {
        if ($scope.AaoRefno !== null) {
            $http.get('GetDocumentfiles?refno=' + $scope.AaoRefno.refno).then(function success(response) {
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
        }
    };

    $scope.refnoChange = function () {
        if ($scope.AaoRefno.refno != null) {
            $http.get('CheckFeasibility?refno=' + $scope.AaoRefno.refno).then(function success(response) {
                $scope.t = response.data;
                if (response.data == "Feasibility Study is prepared.") {
                    $scope.DROPDOWNLIST = true;
                }
                else if (response.data == "Integrated Project.") {
                    $scope.notFeasibleView = true;
                    $scope.Integratedproject = true;
                    $http.get('GetDeptOfficer?referenceNo=' + $scope.AaoRefno.refno).then(function success(response) {
                        $scope.getDeptOfficer = response.data;
                    }, function error(response) {
                        alert(response.status);
                    });
                }
                else if (response.data == "Integrated Project is already sent.") {
                    alert(response.data);
                    $scope.notFeasibleView = true;
                }
                else {
                    alert(response.data);
                    $scope.notFeasibleView = true;

                }
            }, function error(response) {
                alert(response.status);
            });
            landdocument();
        }
        else {
            $scope.notFeasibleView = false;
        }
    };

    $scope.dropdownvalue = function (value) {
        if (value == "DPR") {
            $scope.showModal1 = true;
            $scope.approval = true;
            $scope.getDetails();
        }
        else if (value == "Registration") {
            $scope.showModal = true;
            $scope.approval = true;
            $scope.getRDetails();
        }
        else {
            $scope.showFeasibilityReport = true;
            $scope.approval = true;
            $scope.getFDetails();
        }
    };

    $scope.getFDetails = function () {
        $http.get('GetFeasibilityReport?referenceNo=' + $scope.AaoRefno.refno).then(function success(response) {
            $scope.m = response.data;
            if ($scope.m != null) {
                if ($scope.m.PreviousExperience == true) { $scope.m.PreviousExperience = 'Yes'; }
                if ($scope.m.RoadConnectivity == true) { $scope.m.RoadConnectivity = 'Yes'; }
                if ($scope.m.ElectrificationStatus == true) { $scope.m.ElectrificationStatus = 'Yes'; }
                if ($scope.m.PollutionControlClearanceStatus == true) { $scope.m.PollutionControlClearanceStatus = 'Yes'; }
                if ($scope.m.BankConsentLetterStatus == true) { $scope.m.BankConsentLetterStatus = 'Yes'; }
                if ($scope.m.PreviousExperience == false) { $scope.m.PreviousExperience = 'No'; }
                if ($scope.m.RoadConnectivity == false) { $scope.m.RoadConnectivity = 'No'; }
                if ($scope.m.ElectrificationStatus == false) { $scope.m.ElectrificationStatus = 'No'; }
                if ($scope.m.PollutionControlClearanceStatus == false) { $scope.m.PollutionControlClearanceStatus = 'No'; }
                if ($scope.m.BankConsentLetterStatus == false) { $scope.m.BankConsentLetterStatus = 'No'; }
                $scope.txtDFV = $scope.m.DistanceFromVillage;
                $scope.txtSF = $scope.m.SelfFinance;
                $scope.txtUL = $scope.m.UncensoredLoan;
                $scope.txtMC = $scope.m.MarginComponent;
                $scope.rbPE = $scope.m.PreviousExperience;
                $scope.rbRC = $scope.m.RoadConnectivity;
                $scope.rbES = $scope.m.ElectrificationStatus;
                $scope.rbPCCS = $scope.m.PollutionControlClearanceStatus;
                $scope.rbBCLS = $scope.m.BankConsentLetterStatus;
                var l = $scope.m.UserName.substring(0, 3);
                $scope.lblDesignation;
                if (l == "bvo") { $scope.lblDesignation = "Block Veternary Officer / AVAS"; } else if (l == "aao") { $scope.lblDesignation = "Assistant Agriculture Officer"; } else if (l == "aho") { $scope.lblDesignation = "Assistant Horticulture Officer"; } else if (l == "afo") { $scope.lblDesignation = "Assistant Fishery Officer"; }
                landPhoto($scope.m.Photo);
                $http.get('BLODetail').then(function success(response) {
                    var p = response.data;
                    if (p != null && p != "") {
                        $scope.labelbloName = p.Name;
                        bloSignature(p.Sign);
                        $scope.bloDetail = true;
                        $scope.approveDetail = false;
                    }
                    else {
                        alert('Contact your DNO to Upload the required details.');
                        document.getElementById('ApproveModal').style.display = 'none'
                        $timeout(function () {
                            document.getElementById("btnCloseAppM").click();
                        }, 1);
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.getRDetails = function () {
        $http.get('DisplayAll?refno=' + $scope.AaoRefno.refno).then(function success(response) {
            $scope.t = response.data;
            if ($scope.t != null) {
                $scope.applicantType = $scope.t.ApplicantType;
                $scope.ProjectName = $scope.t.Project;
                $scope.txtCapacity = $scope.t.Capacity;
                $scope.txtAnnualIncome = $scope.t.AnnualIncome;
                $scope.txtPresentOccupation = $scope.t.PresentOccupation;
                $scope.rbPreviousExperience = $scope.t.PreviousExperience;
                $scope.rbGroupType = $scope.t.GroupTypeCode;
                $scope.txtFirmName = $scope.t.FirmName;
                $scope.txtAddressForCorrespondence = $scope.t.CorrespondenceAddress;
                $scope.txtPermanentAddress = $scope.t.PermanentAddress;
                $scope.txtPin = $scope.t.Pincode;
                $scope.txtEmailID = $scope.t.EmailID;
                $scope.txtPPSM = $scope.t.ProductsProduced;
                $scope.rbFinanceType = $scope.t.FinanceType;
                if ($scope.rbFinanceType == "Bank") {
                    $scope.ifBank = true;
                }
                $scope.ddlBank = $scope.t.BankCode;
                $scope.ddlBranch = $scope.t.BranchCode;
                $scope.rbAC = $scope.t.ApproximateCost
                $scope.txtOwnContribution = $scope.t.OwnContribution;
                $scope.txtBankLoan = $scope.t.BankLoan;
                $scope.txtTotal = $scope.t.TotalCost;
                passphoto($scope.t.Photo);
                uploadedsign($scope.t.Signature);
                $http.get('Alllandrecord?refno=' + $scope.AaoRefno.refno).then(function success(response) {
                    $scope.landRecord = response.data;
                    $scope.localRU = response.data[0].Locality;
                }, function error(response) {
                    alert(response.status);
                });
                $http.get('AllGroupMembers?refno=' + $scope.AaoRefno.refno).then(function success(response) {
                    $scope.grMem = response.data;
                }, function error(response) {
                    alert(response.status);
                });
                $http.get('GetIProjects?referenceNo=' + $scope.AaoRefno.refno).then(function success(response) {
                    $scope.iProjects = response.data;
                }, function error(response) {
                    alert(response.status);
                });
                $http.get('GetDocumentfiles?refno=' + $scope.AaoRefno.refno).then(function success(response) {
                    $scope.result = response.data;
                }, function error(response) {
                    alert(response.status);
                });
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.getDetails = function () {
        $http.get('GetDetails?referenceNo=' + $scope.AaoRefno.refno).then(function success(response) {
            $scope.z = response.data;
            var k = response.data;
            $scope.LandDevelopment = k.Development;
            $scope.Fencing = k.Fencing;
            $scope.LandSubTotal = k.LandTotal;
            $scope.Building = k.Building;
            $scope.Shed = k.Shed;
            $scope.OfficeCumStore = k.OfficeCumStore;
            $scope.Godown = k.Godown;
            $scope.PlantArea = k.PlantArea;
            $scope.OtherNecessaryConstruction = k.OtherNecessaryConstruction;
            $scope.CivilConstructionSubTotal = k.CivilConstructionTotal;
            $scope.WellOrRiver = k.WellOrRiver;
            $scope.PumpPipeline = k.PumpPipeLine
            $scope.Tank = k.Tank;
            $scope.SprinklerFogger = k.SprinklerFogger;
            $scope.WaterSupplySubTotal = k.WaterSupplyTotal;
            $scope.InstallationFitting = k.InstallationFitting;
            $scope.Transformer = k.Transformer;
            $scope.DGSet = k.DGSet;
            $scope.ElectrificationSubTotal = k.ElectrificationTotal;
            $scope.EquipmentMachinery = k.Equipment;
            $scope.ManualOrMotor = k.ManualOrMotor;
            $scope.PlantMachinerySubTotal = k.PlantMachineryTotal;
            $scope.Livestock = k.Livestock;
            $scope.LivestockType = k.LivestockType;
            if (k.LivestockType != null) {
                var arrL = k.LivestockType.split(',');
                if (arrL.includes('Cow')) { $scope.LivestockTypeC = true; } if (arrL.includes('Buffalo')) { $scope.LivestockTypeB = true; } if (arrL.includes('Sheep')) { $scope.LivestockTypeS = true; } if (arrL.includes('Goat')) { $scope.LivestockTypeG = true; } if (arrL.includes('Pig')) { $scope.LivestockTypeP = true; }
            }
            $scope.PoultryBreeder = k.PoultryBreeder;
            $scope.BroodFish = k.BroodFish;
            $scope.Plant = k.Plant;
            $scope.AnimalPlantSubTotal = k.AnimalPlantTotal;
            $scope.FixedAsset = k.FixedAsset;
            $scope.MLivestock = k.MLivestock;
            $scope.Bird = k.Bird;
            $scope.Cultivation = k.Cultivation;
            $scope.MiscellaneousTotal = k.MiscellaneousTotal;
            $scope.RawMaterial = k.RawMaterial;
            $scope.SalaryLabour = k.Salary;
            $scope.RepairingMaintainenceEtc = k.Maintenance;
            $scope.RecurringExpenditureSubTotal = k.RecurringExpenditureTotal;
            $scope.TotalCapitalInvestmentCost = k.TotalCapitalInvestmentCost;
            $scope.TotalProjectCost = k.TotalProjectCost;
            $scope.OwnInvestment = k.OwnInvestment;
            $scope.areaOwnInvestment = k.OwnInvestmentRemark;
            $scope.TermLoan = k.TermLoan;
            $scope.areaTermLoan = k.TermLoanRemark;
            $scope.WorkingCapitalLoan = k.WorkingCapitalLoan;
            $scope.areaWorkingCapitalLoan = k.WorkingCapitalLoanRemark;
            $scope.AnyOtherSource = k.OtherSource;
            $scope.areaAnyOtherSource = k.OtherSourceRemark;
            $scope.TotalFinance = k.TotalAmount;
            $scope.BreakEvenPoint = k.BreakEvenPoint;
            $scope.DSCR = k.DSCR;
            $scope.IRR = k.IRR;
            $scope.referenceNo = $scope.AaoRefno.refno;
            getProfitabilityProjectionDetails();
            getCashFlowStatementsDetails();
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.LandSum = function () {
        var k = (parseFloat($scope.LandDevelopment) + parseFloat($scope.Fencing)).toFixed(2);
        (k == "NaN") ? $scope.LandSubTotal = "" : $scope.LandSubTotal = k;
    };

    $scope.CivilConstructionSum = function () {
        var k = (parseFloat($scope.Building) + parseFloat($scope.Shed) + parseFloat($scope.OfficeCumStore) + parseFloat($scope.Godown) + parseFloat($scope.PlantArea) + parseFloat($scope.OtherNecessaryConstruction)).toFixed(2);
        (k == "NaN") ? $scope.CivilConstructionSubTotal = "" : $scope.CivilConstructionSubTotal = k;
    };

    $scope.WaterSupplySum = function () {
        var k = (parseFloat($scope.WellOrRiver) + parseFloat($scope.PumpPipeline) + parseFloat($scope.Tank) + parseFloat($scope.SprinklerFogger)).toFixed(2);
        (k == "NaN") ? $scope.WaterSupplySubTotal = "" : $scope.WaterSupplySubTotal = k;
    };

    $scope.ElectrificationSum = function () {
        var k = (parseFloat($scope.InstallationFitting) + parseFloat($scope.Transformer) + parseFloat($scope.DGSet)).toFixed(2);
        (k == "NaN") ? $scope.ElectrificationSubTotal = "" : $scope.ElectrificationSubTotal = k;
    };

    $scope.PlantMachinerySum = function () {
        var k = (parseFloat($scope.EquipmentMachinery) + parseFloat($scope.ManualOrMotor)).toFixed(2);
        (k == "NaN") ? $scope.PlantMachinerySubTotal = "" : $scope.PlantMachinerySubTotal = k;
    };

    $scope.AnimalPlantSum = function () {
        if ($scope.LivestockTypeC || $scope.LivestockTypeB || $scope.LivestockTypeS || $scope.LivestockTypeG || $scope.LivestockTypeP) {
            var k = (parseFloat($scope.Livestock) + parseFloat($scope.PoultryBreeder) + parseFloat($scope.BroodFish) + parseFloat($scope.Plant)).toFixed(2);
        }
        else if (($scope.LivestockTypeC == false || $scope.LivestockTypeC == undefined) && ($scope.LivestockTypeB == false || $scope.LivestockTypeB == undefined) && ($scope.LivestockTypeS == false || $scope.LivestockTypeS == undefined) && ($scope.LivestockTypeG == false || $scope.LivestockTypeG == undefined) && ($scope.LivestockTypeP == false || $scope.LivestockTypeP == undefined)) {
            var k = (parseFloat($scope.PoultryBreeder) + parseFloat($scope.BroodFish) + parseFloat($scope.Plant)).toFixed(2);
        }
        (k == "NaN") ? $scope.AnimalPlantSubTotal = "" : $scope.AnimalPlantSubTotal = k;
    };

    $scope.MiscellaneousSum = function () {
        var k = (parseFloat($scope.FixedAsset) + parseFloat($scope.MLivestock) + parseFloat($scope.Bird) + parseFloat($scope.Cultivation)).toFixed(2);
        (k == "NaN") ? $scope.MiscellaneousTotal = "" : $scope.MiscellaneousTotal = k;
    };

    $scope.CapitalInvestmentSum = function () {
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal == undefined && $scope.WaterSupplySubTotal == undefined && $scope.ElectrificationSubTotal == undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal == undefined && $scope.ElectrificationSubTotal == undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal == undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal !== undefined && $scope.MiscellaneousTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal) + parseFloat($scope.AnimalPlantSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal !== undefined && $scope.MiscellaneousTotal !== undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal) + parseFloat($scope.AnimalPlantSubTotal) + parseFloat($scope.MiscellaneousTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalCapitalInvestmentCost = "" : $scope.TotalCapitalInvestmentCost = k;
        }
    };

    $scope.RecurringExpenditureSum = function () {
        var k = (parseFloat($scope.RawMaterial) + parseFloat($scope.SalaryLabour) + parseFloat($scope.RepairingMaintainenceEtc)).toFixed(2);
        (k == "NaN") ? $scope.RecurringExpenditureSubTotal = "" : $scope.RecurringExpenditureSubTotal = k;
    };

    $scope.TotalSum = function () {
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal == undefined && $scope.WaterSupplySubTotal == undefined && $scope.ElectrificationSubTotal == undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal == undefined && $scope.ElectrificationSubTotal == undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal == undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal == undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal == undefined && $scope.MiscellaneousTotal == undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal !== undefined && $scope.MiscellaneousTotal == undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal) + parseFloat($scope.AnimalPlantSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal !== undefined && $scope.MiscellaneousTotal !== undefined && $scope.RecurringExpenditureSubTotal == undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloatparseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal) + parseFloat($scope.AnimalPlantSubTotal) + parseFloat($scope.MiscellaneousTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
        if ($scope.LandSubTotal !== undefined && $scope.CivilConstructionSubTotal !== undefined && $scope.WaterSupplySubTotal !== undefined && $scope.ElectrificationSubTotal !== undefined && $scope.PlantMachinerySubTotal !== undefined && $scope.AnimalPlantSubTotal !== undefined && $scope.MiscellaneousTotal !== undefined && $scope.RecurringExpenditureSubTotal !== undefined) {
            var k = (parseFloat($scope.LandSubTotal) + parseFloat($scope.CivilConstructionSubTotal) + parseFloat($scope.WaterSupplySubTotal) + parseFloat($scope.ElectrificationSubTotal) + parseFloat($scope.PlantMachinerySubTotal) + parseFloat($scope.AnimalPlantSubTotal) + parseFloat($scope.MiscellaneousTotal) + parseFloat($scope.RecurringExpenditureSubTotal)).toFixed(2);
            (k == "NaN") ? $scope.TotalProjectCost = "" : $scope.TotalProjectCost = k;
        }
    };

    $scope.MeansOfFinanceSum = function () {
        var k = (parseFloat($scope.OwnInvestment) + parseFloat($scope.TermLoan) + parseFloat($scope.WorkingCapitalLoan) + parseFloat($scope.AnyOtherSource)).toFixed(2);
        (k == "NaN") ? $scope.TotalFinance = "" : $scope.TotalFinance = k;
    };

    $scope.profitabilityProjectionDetails = [];
    var getProfitabilityProjectionDetails = function () {
        $http.get('GetProfitabilityProjectionDetails?referenceNo=' + $scope.AaoRefno.refno).then(function success(response) {
            $scope.profitabilityProjectionDetails = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.cashFlowStatements = [];
    var getCashFlowStatementsDetails = function () {
        $http.get('GetCashFlowStatementDetails?referenceNo=' + $scope.AaoRefno.refno).then(function success(response) {
            $scope.cashFlowStatements = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    var passphoto = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('passpic').setAttribute("src", obj);
    };

    var landPhoto = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('landpic').setAttribute("src", obj);
    };

    var uploadedsign = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('photoSignature').setAttribute("src", obj);
    };

    $scope.cancel = function () {
        $scope.showModal = false;
        $scope.showModal1 = false;
        $scope.showFeasibilityReport = false;
    };

    var bloconfirmation = function () {
        mydata = { ReferenceNo: $scope.AaoRefno.refno };
        var req = {
            method: 'POST',
            url: 'bloapproval',
            headers: {
                '__RequestVerificationToken': token,
            },
            data: { x: mydata, refno: $scope.AaoRefno.refno }
        };
        $http(req).then(function successCallback(response) {
            alert(response.data);
            Userlist();
            clear();
        }, function errorCallback(response) {
            alert(response.status);
        });
    };

    $scope.approveM = function () {
        $http.get('BLODetail').then(function success(response) {
            var p = response.data;
            if (p != null && p != "") {
                $scope.lblbloName = p.Name;
                bloSignature(p.Sign);
                $scope.approveDetail = false;
                $scope.bloDetail = true;
            }
            else {
                alert('Contact your DNO to Upload the required details.');
                document.getElementById('ApproveModal').style.display = 'none'
                $timeout(function () {
                    document.getElementById("btnCloseAppM").click();
                }, 1);
                $scope.approveDetail = false;
                $scope.bloDetail = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.rejectM = function () {
        $http.get('BLODetail').then(function success(response) {
            var f = response.data;
            if (f != null && f != "") {
                $scope.lblRbloName = f.Name;
                bloSignat(f.Sign);
                $scope.rejectDetail = false;;
                $scope.rejectR = true;
                $scope.bloRDetail = true;
            }
            else {
                alert('Contact your DNO to Upload the required details.');
                document.getElementById('RejectModal').style.display = 'none'
                $timeout(function () {
                    document.getElementById("btnCloseRejM").click();
                }, 1);
                $scope.rejectDetail = false;
                $scope.rejectR = false;
                $scope.bloRDetail = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    var bloSignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('bloSign').setAttribute("src", obj);
        document.getElementById('labelBloSign').setAttribute("src", obj);
    };

    var bloSignat = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('bloRSign').setAttribute("src", obj);
    };

    $scope.openMA = function () {
        $http.get('CheckFeasibilityPending?refNo=' + $scope.AaoRefno.refno).then(function success(response) {
            if (response.data.includes("Feasibility Report is pending at")) {
                document.getElementById('ApproveModal').style.display = 'none';
                $timeout(function () {
                    document.getElementById("btnCloseAppM").click();
                }, 1);
                $scope.approveDetail = false;
                $scope.bloDetail = false;
                return alert(response.data);
            }
            else {
                $scope.approveDetail = true;
                $scope.bloDetail = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.openMR = function () {
        $http.get('CheckFeasibilityPending?refno=' + $scope.AaoRefno.refno).then(function success(response) {
            if (response.data.includes("Feasibility Report is pending at")) {
                document.getElementById('RejectModal').style.display = 'none';
                $timeout(function () {
                    document.getElementById("btnCloseRejM").click();
                }, 1);
                $scope.rejectDetail = false;
                $scope.rejectR = false;
                $scope.bloRDetail = false;
                return alert(response.data);
            }
            else {
                $scope.rejectDetail = true;
                $scope.rejectR = false;
                $scope.bloRDetail = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.SendBLO = function () {
        if ($scope.ddlBLO != null && $scope.ddlBLO != "" && $scope.ddlBLO != undefined) {
            mydata = { PreviousReferenceNo: $scope.AaoRefno.refno, NewDeptCode: $scope.deptCode };
            var req = {
                method: 'POST',
                url: 'IntegratedProjectChangebyBLO',
                headers: {
                    '__RequestVerificationToken': token,
                },
                data: { X: mydata }
            };
            $http(req).then(function success(response) {
                if (response.data == "Project is sent to the concerned BLO.") {
                    alert(response.data);
                    $scope.Integratedproject = false;
                    $scope.notFeasibleView = false;
                    Userlist();
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert("Please select a Block Level Officer.");
        }
    };

    $scope.submitBLORecord = function () {
        var myData = { ReferenceNo: $scope.AaoRefno.refno };
        var myReq = {
            method: 'POST',
            url: 'BLOFeasible',
            headers: {
                '__RequestVerificationToken': token,
            },
            data: { x: myData }
        };
        $http(myReq).then(function success(response) {
            alert(response.data);
            $scope.approval = false;
            $scope.DROPDOWNLIST = false;
            document.getElementById('ApproveModal').style.display = 'none';
            $timeout(function () {
                document.getElementById("btnCloseAppM").click();
            }, 1);
            Userlist();
            clear();
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.notsubmitBLORecord = function () {
        if ($scope.txtReasonOne != undefined || $scope.txtReasonTwo != undefined || $scope.txtReasonThree != undefined) {
            ($scope.txtReasonOne == undefined) ? $scope.txtReasonOne = "" : $scope.txtReasonOne = $scope.txtReasonOne;
            ($scope.txtReasonTwo == undefined) ? $scope.txtReasonTwo = "" : $scope.txtReasonTwo = $scope.txtReasonTwo;
            ($scope.txtReasonThree == undefined) ? $scope.txtReasonThree = "" : $scope.txtReasonThree = $scope.txtReasonThree;
            var myData = { ReferenceNo: $scope.AaoRefno.refno, RejectionReason: $scope.txtReasonOne + "<br />" + $scope.txtReasonTwo + "<br />" + $scope.txtReasonThree };
            var myReq = {
                method: 'POST',
                url: 'BLONotFeasible',
                headers: {
                    '__RequestVerificationToken': token,
                },
                data: { x: myData }
            };
            $http(myReq).then(function success(response) {
                alert(response.data);
                $scope.approval = false;
                $scope.DROPDOWNLIST = false;
                $scope.rejectDetail = true;
                $scope.rejectR = false;
                $scope.bloRDetail = false;
                document.getElementById('RejectModal').style.display = 'none';
                $timeout(function () {
                    document.getElementById("btnCloseRejM").click();
                }, 1);
                Userlist();
                clear();
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please enter the Rejection reason.');
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
            data: { refno: $scope.AaoRefno.refno, pdfland: mydata }
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
            data: { refno: $scope.AaoRefno.refno, pdflandrec: mydata }
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
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
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
            data: { referenceNo: $scope.AaoRefno.refno, identityProof: mydata }
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
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
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
            data: { refno: $scope.AaoRefno.refno, BankCL: mydata }
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
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.print = function (printSection) {
        var innerContents = document.getElementById(printSection).innerHTML;
        var popupWinindow = window.open('', '_blank', 'width=800, height=600, toolbar=1, titlebar=1, resizable=1, location=0, status=1, menubar=0, scrollbars=1');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"><script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script><style>.printhide{display:none;}@media print{.printhide{display:none;}}</style><script>$(document).ready(function(){$(".printhide").hide();$(".printhide").addClass("hide");});</script></head><body onload="window.print()">' + '<script>$(document).ready(function(){$(".printhide").hide();$(".printhide").addClass("hide");});</script>' + innerContents + '<script>$(document).ready(function(){$(".printhide").hide();$(".printhide").addClass("hide");});</script>' + '</html>');
        popupWinindow.document.close();
    };

    //var zerooOneRegex = new RegExp("^(\.[0-9]{1,2}|0|1|0\.[0-9]{1,2}|1\.0{1,2})$");
    //$scope.analyze = function (value) {
    //    if (zerooOneRegex.test(value)) {
    //        $scope.errorMessage = "";
    //        $scope.errorField = false;
    //        if ($scope.txtBreakEvenPoint != undefined && $scope.txtDSCR != undefined && $scope.txtIRR != undefined) {
    //            document.getElementById('btnUpdateKeyBusinessMatrix').disabled = false;
    //        }
    //        else {
    //            document.getElementById('btnUpdateKeyBusinessMatrix').disabled = true;
    //        }
    //    }
    //    else if (value == undefined) {
    //        $scope.errorMessage = "";
    //        $scope.errorField = false;
    //        document.getElementById('btnUpdateKeyBusinessMatrix').disabled = true;
    //    }
    //    else {
    //        $scope.errorField = true;
    //        $scope.errorMessage = "The value of Break Even Point must be between 0.00 and 1.00";
    //        document.getElementById('btnUpdateKeyBusinessMatrix').disabled = true;
    //    }
    //};

    //$scope.updateBankBranch = function () {
    //    var bcl;
    //    if ($scope.ddlBank != null && $scope.ddlBranch != null && (($scope.t.BankConsentLeter != '' && document.getElementById('BankCL').files.length > 0) || ($scope.t.BankConsentLeter == '' && document.getElementById('BankCL').files.length == 0))) {
    //        bcl = document.getElementById('base64textarea').value;
    //        var objBankBranch = { ReferenceNo: $scope.AaoRefno.refno, BankCode: $scope.ddlBank.intId, BranchCode: $scope.ddlBranch.intBranchId, BankConsentLetter: bcl };
    //        var myReq = {
    //            method: 'POST',
    //            url: 'UpdateBankBranch',
    //            headers: {
    //                '__RequestVerificationToken': token
    //            },
    //            data: { x: objBankBranch }
    //        };
    //        $http(myReq).then(function success(response) {
    //            var t = response.data;
    //            if (t == true) {
    //                alert('Records successfully updated.');
    //                document.getElementById('bankbranchModal').style.display = "none";
    //                document.getElementById('btnBankBranchModal').click();
    //                $scope.getRDetails();
    //                bcl = null;
    //                $scope.clearBankBranch();
    //            }
    //            else {
    //                alert('Oops! Something is wrong. Please try again.');
    //            }
    //        }, function error(response) {
    //            alert(response.status);
    //        });
    //    }
    //    else {
    //        document.getElementById('BankCL').value = null;
    //        document.getElementById('base64textarea').value = null;
    //        bcl = null;
    //        alert('Please fill all the fields.');
    //    }
    //};

    $scope.piLabel = true;
    $scope.bank = [];
    $scope.branch = [];
    $scope.showTBHLpi = function () {
        $scope.piLabel = false;
        $scope.piTextBox = true;
        if ($scope.rbFinanceType == 'Bank') {
            $http.get('GetBank').then(function success(response) {
                $scope.bank = response.data;
                $scope.ifBank = true;
            }, function error(response) {
                alert(response.status);
            });
            if ($scope.ddlBank != null) {
                $http.get('GetBranch?bankCode=' + $scope.ddlBank).then(function success(response) {
                    $scope.branch = response.data;
                    $scope.ifBank = true;
                }, function error(response) {
                    alert(response.status);
                });
            }
            $scope.txtBankLoan = $scope.t.BankLoan;
        }
        if ($scope.t.FinanceType == 'Self' && $scope.rbFinanceType == 'Bank') {
            $scope.ddlBank = null;
            $scope.ddlBranch = null;
        }
    };

    $scope.getBranch = function (a) {
        if (a != null) {
            $http.get('GetBranch?bankCode=' + a).then(function success(response) {
                $scope.branch = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }
    };

    $scope.updateSum = function () {
        if ($scope.rbFinanceType == "Self") {
            $scope.txtBankLoan = 0;
        }
        if ($scope.txtBankLoan == 0) {
            var k = parseFloat($scope.txtOwnContribution).toFixed(2);
            (k == "NaN") ? $scope.txtTotal = "" : $scope.txtTotal = k;
        }
        else {
            var k = (parseFloat($scope.txtOwnContribution) + parseFloat($scope.txtBankLoan)).toFixed(2);
            (k == "NaN") ? $scope.txtTotal = "" : $scope.txtTotal = k;
        }
    };

    $scope.showLHTBpi = function () {
        if ($scope.txtPPSM != null && $scope.txtPPSM != undefined && $scope.txtPPSM != "" && $scope.rbFinanceType != null && $scope.ddlBank != null && $scope.ddlBranch != null && $scope.rbAC != null && $scope.txtOwnContribution != null && $scope.txtTotal != null) {
            var objpi = { ReferenceNo: $scope.AaoRefno.refno, ProductToBeProducedOrMarketed: $scope.txtPPSM, Capacity: $scope.txtCapacity, ProjectName: $scope.ProjectName, FinanceType: $scope.rbFinanceType, BankCode: $scope.ddlBank, BranchCode: $scope.ddlBranch, ApproximateCost: $scope.rbAC, OwnContribution: $scope.txtOwnContribution, BankLoan: $scope.txtBankLoan };
            var myReq = {
                method: 'POST',
                url: 'UpdateProjectInformations',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { x: objpi }
            };
            $http(myReq).then(function success(response) {
                var t = response.data;
                if (t == 'Record updated successfully.') {
                    alert(t);
                    $scope.piLabel = true;
                    $scope.piTextBox = false;
                    $scope.getRDetails();
                }
                else if (t == false) {
                    alert('Please fill all the fields.');
                    $scope.showTBHLpi();
                }
                else {
                    alert(t);
                    $scope.showTBHLpi();
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please fill all the fields.');
            $scope.showTBHLpi();
        }
    };

    $scope.aadLabel = true;
    $scope.showTBHLaad = function () {
        $scope.aadLabel = false;
        $scope.aadTextBox = true;
    };

    $scope.showLHTBaad = function () {
        if ($scope.txtAddressForCorrespondence != null && $scope.txtAddressForCorrespondence != undefined && $scope.txtAddressForCorrespondence != "" && $scope.txtPermanentAddress != null && $scope.txtPermanentAddress != undefined && $scope.txtPermanentAddress != "" && $scope.txtPin != null && $scope.txtPin != undefined && $scope.txtPin != "") {
            var objAAD = { ReferenceNo: $scope.AaoRefno.refno, CorrespondenceAddress: $scope.txtAddressForCorrespondence, PermanentAddress: $scope.txtPermanentAddress, Pin: $scope.txtPin, EmailID: $scope.txtEmailID };
            var myReq = {
                method: 'POST',
                url: 'UpdateApplicantAddressDetails',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { x: objAAD }
            };
            $http(myReq).then(function success(response) {
                var t = response.data;
                if (t == true) {
                    alert('Records successfully updated.');
                    $scope.aadLabel = true;
                    $scope.aadTextBox = false;
                    $scope.getRDetails();
                }
                else if (t == false) {
                    alert('This EmailID is already in use.');
                    $scope.txtEmailID = null;
                    $scope.showTBHLaad();
                }
                else {
                    alert('Oops! Something is wrong. Please try again.');
                    $scope.showTBHLaad();
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please fill all the fields.');
            $scope.showTBHLaad();
        }
    };

    $scope.adLabel = true;
    $scope.grouptype = [];
    $scope.showTBHLad = function () {
        $scope.adLabel = false;
        $scope.adTextBox = true;
        $http.get('GetGroupTypes').then(function success(response) {
            $scope.grouptype = response.data;
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.getCV = function (a) {
        $scope.rbGroupType = a;
    };

    $scope.showLHTBad = function () {
        if ($scope.rbPreviousExperience != null && $scope.txtAnnualIncome != null && $scope.txtAnnualIncome != undefined && $scope.txtAnnualIncome != "" && $scope.txtPresentOccupation != null && $scope.txtPresentOccupation != undefined && $scope.txtPresentOccupation != "" && (($scope.applicantType == 'Group' && $scope.rbGroupType != null && $scope.txtFirmName != null && $scope.txtFirmName != undefined && $scope.txtFirmName != "") || ($scope.applicantType == 'Individual' && $scope.rbGroupType == null && ($scope.txtFirmName == null || $scope.txtFirmName == undefined || $scope.txtFirmName == "")))) {
            var objAD = { ReferenceNo: $scope.AaoRefno.refno, AnnualIncome: $scope.txtAnnualIncome, PresentOccupation: $scope.txtPresentOccupation, PreviousExperience: $scope.rbPreviousExperience, GroupTypeCode: $scope.rbGroupType, FirmName: $scope.txtFirmName };
            var myReq = {
                method: 'POST',
                url: 'UpdateApplicantDetails',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { x: objAD }
            };
            $http(myReq).then(function success(response) {
                var t = response.data;
                if (t == true) {
                    alert('Records successfully updated.');
                    $scope.adLabel = true;
                    $scope.adTextBox = false;
                    $scope.getRDetails();
                }
                else {
                    alert('Oops! Something is wrong. Please try again.');
                    $scope.showTBHLad();
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please fill all the fields.');
            $scope.showTBHLad();
        }
    };

    $scope.fLabel = true;
    $scope.showTBHL = function () {
        $scope.fTextBox = true;
        $scope.fLabel = false;
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
            data: { refno: $scope.AaoRefno.refno, GraduationCertificate: mydata }
        }
        $http(req).then(function success(response) {
            var t = response.data;
            if (t != null && t != "") {
                alert(t);
                document.getElementById('updatePTCCGAAD').value = null;
                document.getElementById('updatePTCCGAADModal').style.display = "none";
                //$timeout(function () {
                //    document.getElementById("btnEditPTCCGAAD").click();
                //}, 1);
            }
            else {
                alert('Something is wrong.');
            }
        }, function error(response) {
            alert(response.status);
        });
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

    $scope.showLHTB = function () {
        if ($scope.rbPE != null && $scope.rbRC != null && $scope.txtDFV != null && $scope.txtDFV != undefined && $scope.txtDFV != "" && $scope.rbES != null && $scope.rbPCCS != null && $scope.txtSF != null && $scope.txtSF != undefined && $scope.txtSF != "" && $scope.txtUL != null && $scope.txtUL != undefined && $scope.txtUL != "" && $scope.txtMC != null && $scope.txtMC != undefined && $scope.txtMC != "" && $scope.rbBCLS != null) {
            if ($scope.rbPE == 'Yes') { $scope.rbPE = true; }
            if ($scope.rbRC == 'Yes') { $scope.rbRC = true; }
            if ($scope.rbES == 'Yes') { $scope.rbES = true; }
            if ($scope.rbPCCS == 'Yes') { $scope.rbPCCS = true; }
            if ($scope.rbBCLS == 'Yes') { $scope.rbBCLS = true; }
            if ($scope.rbPE == 'No') { $scope.rbPE = false; }
            if ($scope.rbRC == 'No') { $scope.rbRC = false; }
            if ($scope.rbES == 'No') { $scope.rbES = false; }
            if ($scope.rbPCCS == 'No') { $scope.rbPCCS = false; }
            if ($scope.rbBCLS == 'No') { $scope.rbBCLS = false; }
            var objFeasibility = { ReferenceNo: $scope.AaoRefno.refno, PreviousExperience: $scope.rbPE, RoadConnectivity: $scope.rbRC, ElectrificationStatus: $scope.rbES, PollutionControlClearanceStatus: $scope.rbPCCS, BankConsentLetterStatus: $scope.rbBCLS, DistanceFromVillage: $scope.txtDFV, SelfFinance: $scope.txtSF, UncensoredLoan: $scope.txtUL, MarginComponent: $scope.txtMC };
            var myReq = {
                method: 'POST',
                url: 'UpdateFeasibility',
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { x: objFeasibility }
            };
            $http(myReq).then(function success(response) {
                var t = response.data;
                if (t == true) {
                    alert('Records successfully updated.');
                    $scope.fTextBox = false;
                    $scope.fLabel = true;
                    $scope.getFDetails();
                }
                else {
                    alert('Oops! Something is wrong. Please try again.');
                    $scope.showTBHL();
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please fill all the fields.');
            $scope.showTBHL();
        }
    };

    //$scope.cLabel = true;
    //$scope.showTextBoxHideLabel = function () {
    //    $scope.cTextBox = true;
    //    $scope.cLabel = false;
    //};

    //$scope.showLabelHideTextBox = function () {
    //    if ($scope.txtCapacity != null && $scope.txtCapacity != undefined && $scope.txtCapacity != "") {
    //        $http.get('CheckCapacity?projectName=' + $scope.ProjectName + '&capacity=' + $scope.txtCapacity).then(function success(response) {
    //            var z = response.data;
    //            if (z == "OK") {
    //                var objCapacity = { ReferenceNo: $scope.AaoRefno.refno, Capacity: $scope.txtCapacity };
    //                var myReq = {
    //                    method: 'POST',
    //                    url: 'UpdateCapacity',
    //                    headers: {
    //                        '__RequestVerificationToken': token
    //                    },
    //                    data: { x: objCapacity }
    //                };
    //                $http(myReq).then(function success(response) {
    //                    var t = response.data;
    //                    if (t == true) {
    //                        alert('Records successfully updated.');
    //                        $scope.cTextBox = false;
    //                        $scope.cLabel = true;
    //                        $scope.getRDetails();
    //                    }
    //                    else {
    //                        alert('Oops! Something is wrong. Please try again.');
    //                        $scope.showTextBoxHideLabel();
    //                    }
    //                }, function error(response) {
    //                    alert(response.status);
    //                });
    //            }
    //            else {
    //                alert(z);
    //                $scope.showTextBoxHideLabel();
    //                $scope.txtCapacity = null;
    //            }
    //        }, function error(response) {
    //            alert(response.status);
    //        });
    //    }
    //    else {
    //        alert('Please enter the Capacity for Project.');
    //        $scope.showTextBoxHideLabel();
    //    }
    //};

    $scope.clearBankBranch = function () {
        document.getElementById('BankCL').value = null;
        document.getElementById('base64textarea').value = null;
        $scope.ddlBank = null;
        $scope.ddlBranch = null;
        $scope.bank = [];
        $scope.branch = [];
    };

    $scope.updateInvestmentLand = function () {
        var capitalInvestmentLand = { ReferenceNo: $scope.AaoRefno.refno, Development: $scope.LandDevelopment, Fencing: $scope.Fencing, Total: $scope.LandSubTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateInvestmentLand?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { cil: capitalInvestmentLand, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('landModal').style.display = "none";
                document.getElementById('btnLandModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateInvestmentCivilConstruction = function () {
        var capitalInvestmentCivilConstruction = { ReferenceNo: $scope.AaoRefno.refno, Building: $scope.Building, Shed: $scope.Shed, OfficeCumStore: $scope.OfficeCumStore, Godown: $scope.Godown, PlantArea: $scope.PlantArea, OtherNecessaryConstruction: $scope.OtherNecessaryConstruction, Total: $scope.CivilConstructionSubTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateInvestmentCivilConstruction?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { cicc: capitalInvestmentCivilConstruction, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('civilConstructionModal').style.display = "none";
                document.getElementById('btnCivilConstructionModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateInvestmentWaterSupply = function () {
        var capitalInvestmentWaterSupply = { ReferenceNo: $scope.AaoRefno.refno, WellOrRiver: $scope.WellOrRiver, PumpPipeLine: $scope.PumpPipeline, Tank: $scope.Tank, SprinklerFogger: $scope.SprinklerFogger, Total: $scope.WaterSupplySubTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateInvestmentWaterSupply?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { ciws: capitalInvestmentWaterSupply, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('waterSupplyModal').style.display = "none";
                document.getElementById('btnWaterSupplyModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateInvestmentElectrification = function () {
        var capitalInvestmentElectrification = { ReferenceNo: $scope.AaoRefno.refno, InstallationFitting: $scope.InstallationFitting, Transformer: $scope.Transformer, DGSet: $scope.DGSet, Total: $scope.ElectrificationSubTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateInvestmentElectrification?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { cie: capitalInvestmentElectrification, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('electrificationModal').style.display = "none";
                document.getElementById('btnElectrificationModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateInvestmentPlantMachinery = function () {
        var capitalInvestmentPlantMachinery = { ReferenceNo: $scope.AaoRefno.refno, Equipment: $scope.EquipmentMachinery, ManualOrMotor: $scope.ManualOrMotor, Total: $scope.PlantMachinerySubTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateInvestmentPlantMachinery?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { cipm: capitalInvestmentPlantMachinery, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('plantMachineryModal').style.display = "none";
                document.getElementById('btnPlantMachineryModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    var arrULivestock = '';
    $scope.updateCheckArr = function () {
        arrULivestock = '';
        if ($scope.LivestockTypeC) { arrULivestock = 'Cow'; }
        if ($scope.LivestockTypeB) { arrULivestock += ',Buffalo'; }
        if ($scope.LivestockTypeS) { arrULivestock += ',Sheep'; }
        if ($scope.LivestockTypeG) { arrULivestock += ',Goat'; }
        if ($scope.LivestockTypeP) { arrULivestock += ',Pig'; }
        if (arrULivestock.charAt(0) === ',') { arrULivestock = arrULivestock.substr(1); }
        $scope.AnimalPlantSum();
        $scope.TotalSum();
        $scope.CapitalInvestmentSum();
    };

    $scope.updateInvestmentAnimalPlant = function () {
        $scope.updateCheckArr();
        if ((arrULivestock == '' && ($scope.Livestock == undefined || $scope.Livestock == '')) || (arrULivestock != '' && ($scope.Livestock != undefined || $scope.Livestock == ''))) {
            var capitalInvestmentAnimalPlant = { ReferenceNo: $scope.AaoRefno.refno, LiveStockType: arrULivestock, LiveStock: $scope.Livestock, PoultryBreeder: $scope.PoultryBreeder, BroodFish: $scope.BroodFish, Plant: $scope.Plant, Total: $scope.AnimalPlantSubTotal };
            var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
            var myReq = {
                method: 'POST',
                url: 'UpdateInvestmentAnimalPlant?referenceNo=' + $scope.AaoRefno.refno,
                headers: {
                    '__RequestVerificationToken': token
                },
                data: { ciap: capitalInvestmentAnimalPlant, ci: capitalInvestment }
            };
            $http(myReq).then(function success(response) {
                var t = response.data;
                if (t == true) {
                    alert('Records successfully updated.');
                    document.getElementById('animalPlantCostModal').style.display = "none";
                    document.getElementById('btnAnimalPlantCostModal').click();
                    $scope.getDetails();
                }
                else {
                    alert('Oops! Something is wrong. Please try again.');
                }
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please enter the Livestock amount for the selected Livestock.');
            $scope.Livestock = undefined;
            $scope.LivestockTypeC = false;
            $scope.LivestockTypeB = false;
            $scope.LivestockTypeS = false;
            $scope.LivestockTypeG = false;
            $scope.LivestockTypeP = false;
            $scope.AnimalPlantSum();
            $scope.TotalSum();
            $scope.CapitalInvestmentSum();
        }
    };

    $scope.updateInvestmentMiscellaneous = function () {
        var capitalInvestmentMiscellaneous = { ReferenceNo: $scope.AaoRefno.refno, FixedAsset: $scope.FixedAsset, Livestock: $scope.MLivestock, Bird: $scope.Bird, Cultivation: $scope.Cultivation, Total: $scope.MiscellaneousTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateInvestmentMiscellaneous?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { cim: capitalInvestmentMiscellaneous, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('miscellaneousModal').style.display = "none";
                document.getElementById('btnMiscellaneousModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateCapitalRecurring = function () {
        var recurringExpenditure = { ReferenceNo: $scope.AaoRefno.refno, RawMaterial: $scope.RawMaterial, Salary: $scope.SalaryLabour, Maintenance: $scope.RepairingMaintainenceEtc, Total: $scope.RecurringExpenditureSubTotal };
        var capitalInvestment = { ReferenceNo: $scope.AaoRefno.refno, TotalCapitalInvestmentCost: $scope.TotalCapitalInvestmentCost, TotalProjectCost: $scope.TotalProjectCost };
        var myReq = {
            method: 'POST',
            url: 'UpdateRecurringExpenditure?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: { re: recurringExpenditure, ci: capitalInvestment }
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('recurringExpenditureModal').style.display = "none";
                document.getElementById('btnRecurringExpenditureModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateMeansOfFinance = function () {
        var myData = { ReferenceNo: $scope.AaoRefno.refno, OwnInvestment: $scope.OwnInvestment, OwnInvestmentRemark: $scope.areaOwnInvestment, TermLoan: $scope.TermLoan, TermLoanRemark: $scope.areaTermLoan, WorkingCapitalLoan: $scope.WorkingCapitalLoan, WorkingCapitalLoanRemark: $scope.areaWorkingCapitalLoan, OtherSource: $scope.AnyOtherSource, OtherSourceRemark: $scope.areaAnyOtherSource, TotalAmount: $scope.TotalFinance };
        var myReq = {
            method: 'POST',
            url: 'UpdateMeansOfFinance?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: myData
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('meansOfFinanceModal').style.display = "none";
                document.getElementById('btnMeansOfFinanceModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.editProfitabilityProjection = function (a, b) {
        $http.get('GetProfitabilityProjectionData?referenceNo=' + a + '&year=' + b).then(function success(response) {
            var k = response.data;
            if (k !== null || k != "") {
                $scope.PYear = k.Year;
                $scope.GrossRevenue = k.GrossRevenue;
                $scope.TotalExpenditure = k.TotalExpenditure;
                $scope.GrossProfit = k.GrossProfit;
                $scope.NetProfit = k.NetProfit;
                var year = new Date().getFullYear().toString();
                if (b == year) {
                    $scope.npInteger = true;
                    $scope.ppInteger = false;
                }
                else {
                    $scope.ppInteger = true;
                    $scope.npInteger = false;
                }
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateProfitabilityProjection = function () {
        var myData = { ReferenceNo: $scope.AaoRefno.refno, Year: $scope.PYear, GrossRevenue: $scope.GrossRevenue, TotalExpenditure: $scope.TotalExpenditure, GrossProfit: $scope.GrossProfit, NetProfit: $scope.NetProfit };
        var myReq = {
            method: 'POST',
            url: 'UpdateProfitabilityProjection?referenceNo=' + $scope.AaoRefno.refno + '&year=' + $scope.PYear,
            headers: {
                '__RequestVerificationToken': token
            },
            data: myData,
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('profitabilityProjectionModal').style.display = "none";
                document.getElementById('btnProfitabilityProjectionModal').click();
                getProfitabilityProjectionDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.editCashFlowStatement = function (a, b) {
        $http.get('GetCashFlowStatementData?referenceNo=' + a + '&year=' + b).then(function success(response) {
            var k = response.data;
            if (k !== null) {
                $scope.CYear = k.Year;
                $scope.TotalCashInflow = k.TotalCashInflow;
                $scope.TotalCashOutflow = k.TotalCashOutflow;
                $scope.NetCashInflow = k.NetCashInflow;
                var year = new Date().getFullYear().toString();
                if (b == year) {
                    $scope.ncInteger = true;
                    $scope.pcInteger = false;
                }
                else {
                    $scope.pcInteger = true;
                    $scope.ncInteger = false;
                }
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateCashFlowStatement = function () {
        var myData = { ReferenceNo: $scope.AaoRefno.refno, Year: $scope.CYear, TotalCashInflow: $scope.TotalCashInflow, TotalCashOutflow: $scope.TotalCashOutflow, NetCashInflow: $scope.NetCashInflow };
        var myReq = {
            method: 'POST',
            url: 'UpdateCashFlowStatement?referenceNo=' + $scope.AaoRefno.refno + '&year=' + $scope.CYear,
            headers: {
                '__RequestVerificationToken': token
            },
            data: myData,
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('cashFlowStatementModal').style.display = "none";
                document.getElementById('btnCashFlowStatementModal').click();
                getCashFlowStatementsDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.updateKeyBusinessMatrix = function () {
        var myData = { ReferenceNo: $scope.AaoRefno.refno, BreakEvenPoint: $scope.BreakEvenPoint, DSCR: $scope.DSCR, IRR: $scope.IRR };
        var myReq = {
            method: 'POST',
            url: 'UpdateKeyBusinessMatrix?referenceNo=' + $scope.AaoRefno.refno,
            headers: {
                '__RequestVerificationToken': token
            },
            data: myData
        };
        $http(myReq).then(function success(response) {
            var t = response.data;
            if (t == true) {
                alert('Records successfully updated.');
                document.getElementById('keyBusinessMatrixModal').style.display = "none";
                document.getElementById('btnKeyBusinessMatrixModal').click();
                $scope.getDetails();
            }
            else {
                alert('Oops! Something is wrong. Please try again.');
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewPTCCGAAD = function (a) {
        $http.get('viewPTCCGAAD?refno=' + $scope.AaoRefno.refno + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewBankCL = function (a) {
        $http.get('ViewBankCL?refno=' + $scope.AaoRefno.refno + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewCISBankCC = function (a) {
        $http.get('viewCISBankCC?refno=' + $scope.AaoRefno.refno + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewDocumentFile = function (a) {
        $http.get('DisplayPDFDocument?refno=' + $scope.AaoRefno.refno + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewIDProof = function (a) {
        $http.get('DisplayIDProof?refno=' + $scope.AaoRefno.refno + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };
});