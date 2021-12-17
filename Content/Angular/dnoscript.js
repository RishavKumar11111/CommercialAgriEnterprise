var app = angular.module("DNODApp", ['ui.bootstrap.modal']);

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

app.filter('capitalize', function () {
    return function (input) {
        return (!!input) ? input.charAt(0).toUpperCase() + input.substr(1).toLowerCase() : '';
    }
});

app.controller("DNODCtrl", function ($scope, $http, $timeout) {

    var token = document.getElementsByName('__RequestVerificationToken')[0].value;

    var dnoSignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('dnoSign').setAttribute("src", obj);
    };

    var dnoASignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('dnoISign').setAttribute("src", obj);
    };

    var dnoRSignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('dnoRISign').setAttribute("src", obj);
    };

    var dnoSignat = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('dnoRSign').setAttribute("src", obj);
    };

    var dnoGSignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('dnoGSign').setAttribute("src", obj);
    };

    var approveID;
    $scope.approve = function (a) {
        approveID = a;
        $scope.dnoDetail = false;
        $scope.approveDetail = true;
    };

    var backBLOID;
    $scope.backTB = function (a) {
        backBLOID = a;
    };

    $scope.NotRecommendedReason = function (a) {
        $http.get('DNONotRecommendedReason?ReferenceNo=' + a).then(function success(response) {
            var reason = response.data;
            var k = reason.split('<br />');
            $scope.lblDNOReason1 = k[0] == "" ? "" : k[0];
            $scope.lblDNOReason2 = k[1] == "" ? "" : k[1];
            $scope.lblDNOReason3 = k[2] == "" ? "" : k[2];
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.approveDetail = true;
    $scope.approveM = function () {
        $http.get('DNODetail').then(function success(response) {
            var p = response.data;
            if (p != null && p != "") {
                $scope.lbldnoName = p.Name;
                dnoSignature(p.Sign);
                dnoASignature(p.Sign);
                $scope.approveDetail = false;
                $scope.dnoDetail = true;
            }
            else {
                alert('Upload the required details.');
                document.getElementById('ApproveModal').style.display = 'none'
                $timeout(function () {
                    document.getElementById("btnCloseAppM").click();
                }, 1);
                document.getElementById('ApproveDDAModal').style.display = 'none'
                $timeout(function () {
                    document.getElementById("btnCloseAppDM").click();
                }, 1);
                $scope.approveDetail = false;
                $scope.dnoDetail = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.submitDNORecord = function () {
        var myData = { ReferenceNo: approveID };
        var myReq = {
            method: 'POST',
            url: 'DNORecommended',
            headers: {
                '__RequestVerificationToken': token,
            },
            data: { x: myData }
        };
        $http(myReq).then(function success(response) {
            alert(response.data);
            getapprove();
            getfarmingProject();
            $scope.approveDetail = true;
            $scope.dnoDetail = false;
            document.getElementById('ApproveModal').style.display = 'none';
            $timeout(function () {
                document.getElementById("btnCloseAppM").click();
            }, 1);
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.submitDDARecord = function () {
        var myData = { ReferenceNo: approveID };
        var myReq = {
            method: 'POST',
            url: 'DDARecommended',
            headers: {
                '__RequestVerificationToken': token,
            },
            data: { x: myData }
        };
        $http(myReq).then(function success(response) {
            alert(response.data);
            getapprove();
            getfarmingProject();
            $scope.approveDetail = true;
            $scope.dnoDetail = false;
            document.getElementById('ApproveDDAModal').style.display = 'none';
            $timeout(function () {
                document.getElementById("btnCloseAppDM").click();
            }, 1);
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.RevertToBLO = function () {
        var myData = { ReferenceNo: backBLOID };
        var myReq = {
            method: 'POST',
            url: 'DNOBackToBLO',
            headers: {
                '__RequestVerificationToken': token,
            },
            data: { x: myData }
        };
        $http(myReq).then(function success(response) {
            getapprove();
            getfarmingProject();
            alert(response.data);
            document.getElementById('BackToBLOModal').style.display = 'none';
            $timeout(function () {
                document.getElementById("btnCloseBM").click();
            }, 1);
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.RevertDDAToBLO = function () {
        var myData = { ReferenceNo: backBLOID };
        var myReq = {
            method: 'POST',
            url: 'DDABackToBLO',
            headers: {
                '__RequestVerificationToken': token,
            },
            data: { x: myData }
        };
        $http(myReq).then(function success(response) {
            getapprove();
            getfarmingProject();
            alert(response.data);
            document.getElementById('BackToDDABLOModal').style.display = 'none';
            $timeout(function () {
                document.getElementById("btnCloseBDM").click();
            }, 1);
        }, function error(response) {
            alert(response.status);
        });
    };

    var rejectionID;
    $scope.reject = function (a) {
        rejectionID = a;
        $scope.dnoRDetail = false;
        $scope.rejectDetail = true;
        $scope.rejectR = false;
    };

    $scope.rejectDetail = true;
    $scope.rejectM = function () {
        $http.get('DNODetail').then(function success(response) {
            var f = response.data;
            if (f != null && f != "") {
                $scope.lblRdnoName = f.Name;
                dnoSignat(f.Sign);
                dnoRSignature(f.Sign);
                $scope.rejectDetail = false;
                $scope.rejectR = true;
                $scope.dnoRDetail = true;
            }
            else {
                alert('Upload the required details.');
                document.getElementById('RejectModal').style.display = 'none'
                $timeout(function () {
                    document.getElementById("btnCloseRejM").click();
                }, 1);
                document.getElementById('RejectDDAModal').style.display = 'none';
                $timeout(function () {
                    document.getElementById("btnCloseRejDM").click();
                }, 1);
                $scope.rejectDetail = false;
                $scope.rejectR = false;
                $scope.dnoRDetail = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.notsubmitDNORecord = function () {
        if ($scope.txtReasonOne != undefined || $scope.txtReasonTwo != undefined || $scope.txtReasonThree != undefined) {
            ($scope.txtReasonOne == undefined) ? $scope.txtReasonOne = "" : $scope.txtReasonOne = $scope.txtReasonOne;
            ($scope.txtReasonTwo == undefined) ? $scope.txtReasonTwo = "" : $scope.txtReasonTwo = $scope.txtReasonTwo;
            ($scope.txtReasonThree == undefined) ? $scope.txtReasonThree = "" : $scope.txtReasonThree = $scope.txtReasonThree;
            var myData = { ReferenceNo: rejectionID, RejectionReason: $scope.txtReasonOne + "<br />" + $scope.txtReasonTwo + "<br />" + $scope.txtReasonThree };
            var myReq = {
                method: 'POST',
                url: 'DNONotRecommended',
                headers: {
                    '__RequestVerificationToken': token,
                },
                data: { x: myData }
            };
            $http(myReq).then(function success(response) {
                alert(response.data);
                getapprove();
                getfarmingProject();
                $scope.rejectDetail = true;
                $scope.rejectR = false;
                $scope.dnoRDetail = false;
                document.getElementById('RejectModal').style.display = 'none';
                $timeout(function () {
                    document.getElementById("btnCloseRejM").click();
                }, 1);
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please enter the Rejection reason.');
            return false;
        }
    };

    $scope.notsubmitDDARecord = function () {
        if ($scope.txtDReasonOne != undefined || $scope.txtDReasonTwo != undefined || $scope.txtDReasonThree != undefined) {
            ($scope.txtDReasonOne == undefined) ? $scope.txtDReasonOne = "" : $scope.txtDReasonOne = $scope.txtDReasonOne;
            ($scope.txtDReasonTwo == undefined) ? $scope.txtDReasonTwo = "" : $scope.txtDReasonTwo = $scope.txtDReasonTwo;
            ($scope.txtDReasonThree == undefined) ? $scope.txtDReasonThree = "" : $scope.txtDReasonThree = $scope.txtDReasonThree;
            var myData = { ReferenceNo: rejectionID, DDARejectionReason: $scope.txtDReasonOne + "<br />" + $scope.txtDReasonTwo + "<br />" + $scope.txtDReasonThree };
            var myReq = {
                method: 'POST',
                url: 'DDANotRecommended',
                headers: {
                    '__RequestVerificationToken': token,
                },
                data: { x: myData }
            };
            $http(myReq).then(function success(response) {
                alert(response.data);
                getapprove();
                getfarmingProject();
                $scope.rejectDetail = true;
                $scope.rejectR = false;
                $scope.dnoRDetail = false;
                document.getElementById('RejectDDAModal').style.display = 'none';
                $timeout(function () {
                    document.getElementById("btnCloseRejDM").click();
                }, 1);
            }, function error(response) {
                alert(response.status);
            });
        }
        else {
            alert('Please enter the Rejection reason.');
            return false;
        }
    };

    $scope.NotFeasibleReason = function (a) {
        $http.get('BlonotfeasibleReason?ReferenceNo=' + a).then(function success(response) {
            var reason = response.data;
            var k = reason.split('<br />');
            $scope.lblReason1 = k[0] == "" ? "" : k[0];
            $scope.lblReason2 = k[1] == "" ? "" : k[1];
            $scope.lblReason3 = k[2] == "" ? "" : k[2];
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.getApproval = [];
    var getapprove = function () {
        $http.get('GetApprovalList').then(function success(response) {
            if (response.data.length > 0) {
                $scope.Errorshow = false;
                $scope.getApproval = response.data;
            }
            else {
                $scope.getApproval = [];
                $scope.Errorshow = true;
                $scope.ErrorMSg = "No data found";
            }
        }, function error(response) {
            alert(response.status);
        });
    };
    getapprove();

    $scope.getIntegratedFarming = [];
    var getfarmingProject = function () {
        $http.get('GetIntegratedFarming').then(function success(response) {
            if (response.data.length > 0) {
                $scope.Errorshow1 = false;
                $scope.getIntegratedFarming = response.data;
            }
            else {
                $scope.getIntegratedFarming = [];
                $scope.Errorshow1 = true;
                $scope.ErrorMSg1 = "No data found";
            }
        }, function error(response) {
            alert(response.status);
        });
    };
    getfarmingProject();
    
    $scope.DaoRegdDtls = [];
    $scope.open = function (a) {
        $http.get('GetRegdDetails?refno=' + a).then(function success(response) {
            $scope.t = response.data;
            $scope.referenceNo = a;
            if ($scope.t != null) {
                if ($scope.t.EducationalCertificate != "") { $scope.AckeduCert = true; } else { $scope.AckeduCert = false; }
                if ($scope.t.BankConsentLeter != "") { $scope.AckBCL = true; } else { $scope.AckBCL = false; }
                if ($scope.t.CISBankConsentLeter != "") { $scope.AckCISBCC = true; } else { $scope.AckCISBCC = false; }
                passphoto($scope.t.Photo);
                uploadedsign($scope.t.Signature);
                $http.get('Alllandrecord?refno=' + a).then(function success(response) {
                    $scope.landRecord = response.data;
                    $scope.localRU = response.data[0].Locality;
                }, function error(response) {
                    alert(response.status);
                });
                $http.get('AllGroupMembers?refno=' + a).then(function success(response) {
                    $scope.grMem = response.data;
                }, function error(response) {
                    alert(response.status);
                });
                $http.get('GetIProjects?referenceNo=' + a).then(function success(response) {
                    $scope.iProjects = response.data;
                }, function error(response) {
                    alert(response.status);
                });
                $http.get('GetDocumentfiles?refno=' + a).then(function success(response) {
                    $scope.result = response.data;
                }, function error(response) {
                    alert(response.status);
                });
            }
            $scope.showModal = true;
        }, function error(response) {
            alert(response.status);
        });
    };

    var passphoto = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('passpic').setAttribute("src", obj);
    };

    var uploadedsign = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('photoSignature').setAttribute("src", obj);
    };

    $scope.viewpdffile = function (refno) {
        $http.get('DisplayPDF?refno=' + refno, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, '', '_blank');
        }, function error(response) {
            alert(response.status);
        });
    };

    var landdocument = function (a) {
        $http.get('GetDocumentfiles?refno=' + a).then(function success(response) {
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
    };

    $scope.openmodal = function (a) {
        $http.get('GetDetails?referenceNo=' + a).then(function success(response) {
            $scope.z = response.data;
            $scope.referenceNo = a;
            getProfitabilityProjectionDetails();
            getCashFlowStatementsDetails();
        }, function error(response) {
            alert(response.status);
        });
        $scope.profitabilityProjectionDetails = [];
        var getProfitabilityProjectionDetails = function () {
            $http.get('GetProfitabilityProjectionDetails?referenceNo=' + a).then(function success(response) {
                $scope.profitabilityProjectionDetails = response.data;
            }, function error(response) {
                alert(response.status);
            });
        };
        $scope.cashFlowStatements = [];
        var getCashFlowStatementsDetails = function () {
            $http.get('GetCashFlowStatementDetails?referenceNo=' + a).then(function success(response) {
                $scope.cashFlowStatements = response.data;
            }, function error(response) {
                alert(response.status);
            });
        };
        $scope.showModal1 = true;
    };

    $scope.ok = function () {
        $scope.showModal = false;
    };

    $scope.cancel = function () {
        $scope.showModal = false;
        $scope.showModal1 = false;
        $scope.showFeasibilityReport = false;
        $scope.CheckDNONS = false;
    };

    $scope.printTo = function () {
        var innerContents = document.getElementById('printSectionId').innerHTML;
        var popupWinindow = window.open('', '_blank', 'width=600,height=700,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"></head><body onload="window.print()">' + innerContents + '</html>');
        popupWinindow.document.close();
    };

    var landPhoto = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('landpic').setAttribute("src", obj);
    };

    $scope.openfeasibility = function (a) {
        $http.get('GetFeasibilityReport?referenceNo=' + a).then(function success(response) {
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
                var l = $scope.m.UserName.substring(0, 3);
                $scope.lblDesignation;
                if (l == "bvo") { $scope.lblDesignation = "Block Veternary Officer / AVAS"; } else if (l == "aao") { $scope.lblDesignation = "Assistant Agriculture Officer"; } else if (l == "aho") { $scope.lblDesignation = "Assistant Horticulture Officer"; } else if (l == "afo") { $scope.lblDesignation = "Assistant Fishery Officer"; }
                landPhoto($scope.m.Photo);
                $http.get('BLODetail?refNo=' + a).then(function success(response) {
                    var p = response.data;
                    if (p != null && p != "") {
                        $scope.labelbloName = p.Name;
                        bloSignature(p.Sign);
                        $scope.bloDetail = true;
                    }
                    else {
                        alert('Contact your DNO to Upload the required details.');
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
        }, function error(response) {
            alert(response.status);
        });
        $scope.showFeasibilityReport = true;
    };

    var bloSignature = function (a) {
        var obj = "data:image/jpeg;base64," + a;
        document.getElementById('labelBloSign').setAttribute("src", obj);
    };

    $scope.ApproveGoAhead = function (a) {
        $scope.referenceNo = a;
        $http.get('CheckDnolist?refno=' + a).then(function success(response) {
            var t = response.data;
            if (t.includes("Already generated.")) {
                alert('The Go-Ahead Letter has been generated already.');
            }
            else if (t.includes(true)) {
                alert('The Go-Ahead Letter has been generated already.');
            }
            else {
                $scope.CheckDNONS = true;
                $http.get('DNODetail').then(function success(response) {
                    var p = response.data;
                    if (p != null && p != "") {
                        $scope.lblGDNOName = p.Name;
                        dnoGSignature(p.Sign);
                    }
                    else {
                        alert('Upload the required details.');
                    }
                }, function error(response) {
                    alert(response.status);
                });
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.submitGDNORecord = function () {
        var req = {
            method: 'POST',
            url: 'DNORecordSave',
            headers: {
                '__RequestVerificationToken': token
            },
            data: { refNo: $scope.referenceNo }
        };
        $http(req).then(function successCallback(response) {
            var t = response.data;
            if (t.includes("Go-Ahead Letter is generated.")) {
                alert(response.data);
                $scope.CheckDNONS = false;
            }
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.uploadImage = function (files) {
        var ext = files[0].name.match(/\.(.+)$/)[1];
        if (!(angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'jpg')) {
            alert("Invalid image file, must select a *.jpeg ,*.jpg  file.");
            document.getElementById('DnoSignature').value = null;
            DnoSignature.focus();
            return false;
        }
        if (files.fileSize == -1) {
            alert("Couldn't load image file size.  Please try to save again.");
            return false;
        }
        else {
            var as = files[0].size;
            if (files[0].size > 51200) {
                document.getElementById('DnoSignature').value = null;
                alert("File is too large, must select file under 50kb.");
                DnoSignature.focus();
                return false;
            }
            else {
                return;
            }
        }
    };

    $scope.print = function (printSection) {
        var innerContents = document.getElementById(printSection).innerHTML;
        var popupWinindow = window.open('', '_blank', 'width=800, height=600, toolbar=1, titlebar=1, resizable=1, location=0, status=1, menubar=0, scrollbars=1');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"></head><body onload="window.print()">' + innerContents + '</html>');
        popupWinindow.document.close();
    };

    $scope.viewPTCCGAAD = function (a) {
        $http.get('viewPTCCGAAD?refno=' + $scope.referenceNo + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewBankCL = function (a) {
        $http.get('ViewBankCL?refno=' + $scope.referenceNo + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewCISBankCC = function (a) {
        $http.get('viewCISBankCC?refno=' + $scope.referenceNo + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewDocumentFile = function (a) {
        $http.get('DisplayPDFDocument?refno=' + $scope.referenceNo + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };

    $scope.viewIDProof = function (a) {
        $http.get('DisplayIDProof?refno=' + $scope.referenceNo + '&filetype=' + a, { responseType: 'arraybuffer' }).then(function success(response) {
            var file = new Blob([response.data], { type: 'application/pdf' })
            var fileUrl = URL.createObjectURL(file);
            window.open(fileUrl, "", "_blank", 'width=500,height=400');
        }, function error(response) {
            alert(response.status);
        });
    };
});