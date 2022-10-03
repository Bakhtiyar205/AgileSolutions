$(document).ready(function () {
    //Create Birth Date
    let createBirthDate = $("#BirthDate");
    let birthDateExtra = $("#BirthDateExtra");
    let submitCreate = $("#submit-create");
    $(createBirthDate).change(function () {
        let choosedDate = new Date($(createBirthDate).val());
        let dMin = new Date(Date.now());
        dMin.setFullYear(dMin.getFullYear() - 65);
        let dMax = new Date(Date.now());
        dMax.setFullYear(dMax.getFullYear() - 18);
        let eventhandler = function (e) {
            e.preventDefault();
        }
        if (choosedDate.getTime() > dMin.getTime() && choosedDate.getTime() < dMax.getTime()) {
            $(submitCreate).unbind(eventhandler);
            $(birthDateExtra).addClass("d-none");
            $(birthDateExtra).removeClass("d-block");
            
        } else {
            $(birthDateExtra).html(`The date should be between ${dMin.toDateString()} and ${dMax.toDateString()}`);
            $(birthDateExtra).removeClass("d-none");
            $(birthDateExtra).addClass("d-block");
            $(submitCreate).bind(eventhandler);
        }
    });

    //CheckSerialNumber
    let serialNumber = $("#SerialNumber");
    let serialNumberExtra = $("#serial-number-extra");
    $(serialNumber).keyup(function () {
        let checkBool;
        let eventhandler = function (e) {
            e.preventDefault();
        }
        checkBool = /^\d+$/.test(serialNumber.val());
        if (!checkBool) {
            $(serialNumberExtra).html("Only digits are acceptable");
            $(submitCreate).bind(eventhandler);
            $(serialNumberExtra).addClass("d-block");
            $(serialNumberExtra).removeClass("d-none");
        } else {
            $(submitCreate).unbind(eventhandler);
            $(serialNumberExtra).addClass("d-none");
            $(serialNumberExtra).removeClass("d-block");
        }
    })
   
});