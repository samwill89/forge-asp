﻿/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Forge Partner Development
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

$(document).ready(function () {


    var floorPlanIDs = ['floorPanel1', 'floorPanel2', 'floorPanel3'];
    var archPanelIDs = ['archPanel1', 'archPanel2', 'archPanel3'];
    floorPlanIDs.forEach(id => {
        var storyNumber = id[id.length - 1];
        $('#' + id).append(`<ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation"><a href="#1bed${storyNumber}f" aria-controls="1bed${storyNumber}f" role="tab" data-toggle="tab">1 Bed</a></li>
                                    <li role="presentation"><a href="#2bed${storyNumber}f" aria-controls="2bed${storyNumber}f" role="tab" data-toggle="tab">2 Bed</a></li>
                                    <li role="presentation" class="active"><a href="#3bed${storyNumber}f" aria-controls="3bed${storyNumber}f" role="tab" data-toggle="tab">3 Bed</a></li>
                                    <li role="presentation"><a href="#4bed${storyNumber}f" aria-controls="4bed${storyNumber}f" role="tab" data-toggle="tab">4+ Bed</a></li>
                                </ul>

                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane" id="1bed${storyNumber}f">
                                        <div class="row" id="floorPlansList${storyNumber}1">

                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="2bed${storyNumber}f">
                                        <div class="row" id="floorPlansList${storyNumber}2">

                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane active" id="3bed${storyNumber}f">
                                        <div class="row" id="floorPlansList${storyNumber}3">

                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="4bed${storyNumber}f">
                                        <div class="row" id="floorPlansList${storyNumber}4">

                                        </div>
                                    </div>
                                </div>

                                <div class="row text-center">
                                    <button class="btn btn-default pickPlan">PICK PLAN</button>
                                    <button class="btn btn-default modify" id="modify">MODIFY</button>
                                </div>`)
    });

    archPanelIDs.forEach(id => {
        var storyNumber = id[id.length - 1];
        $('#' + id).append(`<ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation"><a href="#1bed${storyNumber}a" aria-controls="1bed${storyNumber}a" role="tab" data-toggle="tab">1 Bed</a></li>
                                    <li role="presentation"><a href="#2bed${storyNumber}a" aria-controls="2bed${storyNumber}a" role="tab" data-toggle="tab">2 Bed</a></li>
                                    <li role="presentation" class="active"><a href="#3bed${storyNumber}a" aria-controls="3bed${storyNumber}a" role="tab" data-toggle="tab">3 Bed</a></li>
                                    <li role="presentation"><a href="#4bed${storyNumber}a" aria-controls="4bed${storyNumber}a" role="tab" data-toggle="tab">4+ Bed</a></li>
                                </ul>

                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane" id="1bed${storyNumber}a">
                                        <div class="row" id="archFormList${storyNumber}1">

                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="2bed${storyNumber}a">
                                        <div class="row" id="archFormList${storyNumber}2">

                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane active" id="3bed${storyNumber}a">
                                        <div class="row" id="archFormList${storyNumber}3">

                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="4bed${storyNumber}a">
                                        <div class="row" id="archFormList${storyNumber}4">

                                        </div>
                                    </div>
                                </div>

                                <div class="row text-center">
                                    <button class="btn btn-default pickPlan">PICK PLAN</button>
                                    <button class="btn btn-default modify" id="modify">MODIFY</button>
                                </div>`)
    });



    $('.pickFacade').click(function () {
        $("#accordion2").hide();
        $("#accordion1").show();
        //console.log(csvData[currentSelectedStyle]['Style']);

        TestOBjectsData(csvData[currentSelectedStyle]['Style'].toLowerCase());
    })

    $('.modify').click(function () {
        $("#accordion1").hide();
        $("#accordion3").show();
        PrepareUniqueValues();
        populateExteriorMenu();
    })


    $("#plans").click(function () {
        
    })

    $("#facades").click(function () {

    })

    $("#interior").click(function () {

    })

    document.addEventListener('keydown', event => {
        if (event.keyCode == 88) {
            var j = 0;
            csvData.forEach(item => {
                if (j === 0) {
                    Object.keys(item).forEach(key => {
                        uniqueCSVData[key] = [];
                    });
                    j++;
                }

                Object.keys(item).forEach(key => {
                    var newArr = uniqueCSVData[key];
                    if (item[key] !== "") {
                        item[key].split(" ").forEach(el => {
                            newArr.push(el);
                        })
                        //newArr.push(item[key]);
                    }
                    uniqueCSVData[key] = newArr;
                });
            });
            Object.keys(uniqueCSVData).forEach(key => {
                arr = uniqueCSVData[key];
                uniq = [...new Set(arr)];
                uniqueCSVData[key] = uniq;
            })
            //console.log(uniqueCSVData);
        }
    });

});
var currentFacadeStyle;
var uniqueCSVData = {};
function PrepareUniqueValues() {
    var j = 0;
    csvData.forEach(item => {
        if (j === 0) {
            Object.keys(item).forEach(key => {
                uniqueCSVData[key] = [];
            });
            j++;
        }

        Object.keys(item).forEach(key => {
            var newArr = uniqueCSVData[key];
            if (item[key] !== "") {
                item[key].split(" ").forEach(el => {
                    newArr.push(el);
                })
                //newArr.push(item[key]);
            }
            uniqueCSVData[key] = newArr;
        });
    });
    Object.keys(uniqueCSVData).forEach(key => {
        arr = uniqueCSVData[key];
        uniq = [...new Set(arr)];
        uniqueCSVData[key] = uniq;
    })


    var wallTypes = [
        'Stone Yellow',
        'Stone Orange',
        'Siding White',
        'Siding Eggshell',
        'Siding Red',
        'Siding Medium Blue',
        'Siding Dark Green',
        'Siding Dark Blue',
        'Siding Brown',
        'Siding Black',
        'Brick Red',
        'Brick Brown',
        'BoardBatten White',
        'BoardBatten Eggshell'
    ];

    var rails = [
        'Regal',
        'Medium1',
        'Medium2',
        'Royal'
    ];

    var winStyle = [
        'SH1x - Regal',
        'SH2x - Regal',
        'SH1x - Royal',
        'SH2x - Royal',
        'Fixed1x - Medium'
    ];


    //Preparing the walls & materials section
    $('#baseMaterialsList').empty();
    uniqueCSVData['BaseMaterial_T'].forEach(el => {
        wallTypes.forEach(type => {
            if (type.startsWith(el)) {
                col = type.slice(el.length + 1);
                $('#baseMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el} ${col}">
                    <img class="img-responsive base-materials" src ="http://placehold.jp/100x100.png?text=${el}+${col}" alt ="${el} ${col}" onClick="ModifyCSVForWallType(this, 'BaseMaterial_T', 'BaseMaterialColor_T', '${el}', '${col}')" />
                                    </div >`);
            }
        })
    })

    $('#mainMaterialsList').empty();
    uniqueCSVData['MainMaterial_T'].forEach(el => {
        wallTypes.forEach(type => {
            if (type.startsWith(el)) {
                col = type.slice(el.length + 1);
                $('#mainMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el} ${col}">
                    <img class="img-responsive main-materials" src ="http://placehold.jp/100x100.png?text=${el}+${col}" alt ="${el} ${col}" onClick="ModifyCSVForWallType(this, 'MainMaterial_T', 'MainMaterialColor_T', '${el}', '${col}')" />
                                    </div >`);
            }
        })
    })


    $('#accentMaterialsList').empty();
    uniqueCSVData['AccentMaterial_T'].forEach(el => {
        wallTypes.forEach(type => {
            if (type.startsWith(el)) {
                col = type.slice(el.length + 1);
                $('#accentMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el} ${col}">
                    <img class="img-responsive accent-materials" src ="http://placehold.jp/100x100.png?text=${el}+${col}" alt ="${el} ${col}" onClick="ModifyCSVForWallType(this, 'AccentMaterial_T', 'AccentMaterialColor_T', '${el}', '${col}')" />
                                    </div >`);
            }
        })
    })

    $('#datumsplbase').empty();
    uniqueCSVData['DatumSplBaseProfile_T'].forEach(el => {
        $('#datumsplbase').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive datumsplbase" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'DatumSplBaseProfile_T', '${el}')" />
                                    </div >`);
    })


    //Preparing the Columns section
    $('#colColor').empty();
    Object.keys(colorsInAray).forEach(colAr => {
        colorsInAray[colAr].forEach(col => {
            $('#colColor').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${col}">
                <img class="img-responsive colColor" src ="http://placehold.jp/100x100.png?text=${col}" alt ="${col}" onClick="ModifyCSV(this, 'ColumnColor_T', '${colAr}')" />
                                </div >`);
        })
    })   

    $('#colCapitalList').empty();
    uniqueCSVData['ColumnCapitalStyle_T'].forEach(el => {
        $('#colCapitalList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive colCapital" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'ColumnCapitalStyle_T', '${el}')" />
                                    </div >`);
    })

    $('#colBaseList').empty();
    uniqueCSVData['ColumnBaseStyle_T'].forEach(el => {
        $('#colBaseList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive colbase" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'ColumnBaseStyle_T', '${el}')" />
                                    </div >`);
    })


    //Preparing the windows and trims sections
    $('#winStylesList').empty();
    winStyle.forEach(el => {
        $('#winStylesList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive winstyle" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'WinStyle_T', '${el}')" />
                                    </div >`);
    })

    $('#winTrimTop').empty();
    uniqueCSVData['WinTrimTop_T'].forEach(el => {
        $('#winTrimTop').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive wintrimtop" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'WinTrimTop_T', '${el}')" />
                                    </div >`);
    })

    $('#winTrimSide').empty();
    uniqueCSVData['WinTrimSide_T'].forEach(el => {
        $('#winTrimSide').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive wintrimside" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'WinTrimSide_T', '${el}')" />
                                    </div >`);
    })

    $('#winTrimBottom').empty();
    uniqueCSVData['WinTrimBottom_T'].forEach(el => {
        $('#winTrimBottom').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive wintrimbottom" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'WinTrimBottom_T', '${el}')" />
                                    </div >`);
    })

    $('#winTrimColor').empty();
    Object.keys(colorsInAray).forEach(colAr => {
        colorsInAray[colAr].forEach(col => {
            $('#winTrimColor').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${col}">
                <img class="img-responsive wintrimcolor" src ="http://placehold.jp/100x100.png?text=${col}" alt ="${col}" onClick="ModifyCSV(this, 'WinTrimColor_T', '${colAr}')" />
                                </div >`);
        })
    })  

    $('#winShutterColor').empty();
    Object.keys(colorsInAray).forEach(colAr => {
        colorsInAray[colAr].forEach(col => {
            $('#winShutterColor').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${col}">
                <img class="img-responsive winshuttercolor" src ="http://placehold.jp/100x100.png?text=${col}" alt ="${col}" onClick="ModifyCSV(this, 'WinShutterColor_T', '${colAr}')" />
                                </div >`);
        })
    }) 

    $('#doorsStylesList').empty();
    uniqueCSVData['DoorStyle_T'].forEach(el => {
        $('#doorsStylesList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive doorStyle" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'DoorStyle_T', '${el}')" />
                                    </div >`);
    })

    $('#doorsTrimTop').empty();
    uniqueCSVData['DoorTrimTop_T'].forEach(el => {
        $('#doorsTrimTop').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive doortrimtop" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'DoorTrimTop_T', '${el}')" />
                                    </div >`);
    })

    $('#doorsTrimSide').empty();
    uniqueCSVData['DoorTrimSide_T'].forEach(el => {
        $('#doorsTrimSide').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive doortrimside" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'DoorTrimSide_T', '${el}')" />
                                    </div >`);
    })

    $('#doorsTrimColor').empty();
    Object.keys(colorsInAray).forEach(colAr => {
        colorsInAray[colAr].forEach(col => {
            $('#doorsTrimColor').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${col}">
                <img class="img-responsive doortrimcolor" src ="http://placehold.jp/100x100.png?text=${col}" alt ="${col}" onClick="ModifyCSV(this, 'DoorTrimColor_T', '${colAr}')" />
                                </div >`);
        })
    }) 



    //Preparing the porch section
    $('#porchStyleList').empty();
    uniqueCSVData['PorchStyle_T'].forEach(el => {
        $('#porchStyleList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive porchStyle" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'PorchStyle_T', '${el}')" />
                                    </div >`);
    })

    $('#porchSpan').empty();
    uniqueCSVData['PorchSpan_T'].forEach(el => {
        $('#porchSpan').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive porchSpan" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'PorchSpan_T', '${el}')" />
                                    </div >`);
    })

    $('#porchRailTop').empty();
    rails.forEach(el => {
        $('#porchRailTop').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive porchRailTop" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'RailTop_T', '${el}')" />
                                    </div >`);
    })

    $('#porchRailBottom').empty();
    rails.forEach(el => {
        $('#porchRailBottom').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive porchRailBottom" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'RailBottom_T', '${el}')" />
                                    </div >`);
    })

    $('#porchRailBal').empty();
    rails.forEach(el => {
        $('#porchRailBal').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive porchRailBal" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'RailBal_T', '${el}')" />
                                    </div >`);
    })





}
function logKey(e) {
    if (e.code == 'KeyX') {

    }
}

function populateExteriorMenu() {
    //console.log(csvData);
    //console.log(uniqueCSVData);
}
//var colors = ['Boreal', 'White', 'Dark', 'Earth', 'Desert'];
var colorsInAray = {
    "Boreal": ['Dark Green', 'Medium Blue'],
    "White": ['White', 'Eggshell'],
    "Dark": ['Black', 'Dark Blue'],
    "Earth": ['Red', 'Brown'],
    "Desert": ['Yellow', 'Orange']
};
function ModifyCSV(element, key, value) {
    console.log(element);
    className = element.classList[1];
    csvData[currentSelectedStyle][key] = value;
    $(`.${className}`).each(function () {
        $(this).removeClass("activeStyle");
    });
    $(element).addClass("activeStyle");
}

function ModifyCSVForWallType(element, key1, key2, value1, value2) {
    console.log(element);
    className = element.classList[1];
    csvData[currentSelectedStyle][key1] = value1;
    Object.keys(colorsInAray).forEach(e => {
        if (colorsInAray[e].includes(value2)) {
            csvData[currentSelectedStyle][key2] = e;
        }
    })  
    $(`.${className}`).each(function () {
        $(this).removeClass("activeStyle");
    });
    $(element).addClass("activeStyle");
}

function updateCSV(element, key) {
    csvData[currentSelectedStyle][key] = element.selectedOptions[0].value;
}
function updateCSVWithSlider(element, key, textId) {
    $(textId)[0].innerText = element.value;
    csvData[currentSelectedStyle][key] = element.value;
}

function updateCSVWithCheckBox(element, key) {
    if (key === 'WinBay_T') {
        if (element.checked) {
            csvData[currentSelectedStyle][key] = "TRUE";
        }
        else {
            csvData[currentSelectedStyle][key] = "FALSE";
        }
    } else {
        if (element.checked) {
            csvData[currentSelectedStyle][key] = "Yes";
        }
        else {
            csvData[currentSelectedStyle][key] = "No";
        }
    }
    
}


function TestOBjectsData(facadestyle) {
    currentFacadeStyle = facadestyle;
    jQuery.ajax({
        url: '/api/forge/oss/buckets',
        method: 'GET',
        data: {
            'id': `${facadestyle}bucket`,
        },
        success: function (result) {


            var i = 1;
            //$('#floorPlansList').empty();
            result.forEach(function (item) {
                //console.log(item.text);
                var modelName = item.text.split('.')[0].split('_')[0];
                var storiesNumber = item.text.split('.')[0].split('_')[1];
                var bedCount = item.text.split('.')[0].split('_')[2];
                var area = item.text.split('.')[0].split('_')[3];
                $(`#floorPlansList${storiesNumber}${bedCount}`).append(`<div id="#${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.text}','${item.id}', 'plan', this)" data-toggle="tooltip" data-placement="right" title="${item.text}">
                    <img class="img-responsive floor-plan-style" src ="images/plans/${modelName}.png" alt ="${item.text}" />
                                    </div >`);

                $(`#archFormList${storiesNumber}${bedCount}`).append(`<div id="${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.text}','${item.id}', 'arch', this)" data-toggle="tooltip" data-placement="right" title="${item.text}">
                    <img class="img-responsive arch-form-style" src ="images/plans/${modelName}.png" alt ="${item.text}" />
                                    </div >`);
                //console.log(modelName);
                //console.log(storiesNumber);
                //console.log(bedCount);
                //console.log(area);
                // img source should be the same as the text of the model
                // onClick here will load the corresponding image

                //$('#floorPlansList').append(`<div id="#${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.id}', 'plan')" data-toggle="tooltip" data-placement="right" title="${item.text}">
                //    <img class="img-responsive" src ="images/plans/${modelName}.png" alt ="${item.text}" />
                //                    </div >`);

                //if (i % 3 == 0) {
                //    $('#floorPlansList').append(`<br />`);
                //}
                //i++;
            });

            //i = 1;
            //$('#archFormList').empty();
            //result.forEach(function (item) {
            //    // img source should be the same as the text of the model
            //    // onClick here will load the corresponding 3d model
            //    $('#archFormList').append(`<div id="${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.id}', 'arch')" data-toggle="tooltip" data-placement="right" title="${item.text}">
            //        <img class="img-responsive" src ="images/axons/${i}.png" alt ="${item.text}" />
            //                        </div >`);

            //    if (i % 3 == 0) {
            //        // onClick here will load the corresponding image
            //        $('#archFormList').append(`<br />`);
            //    }
            //    i++;
            //});



          
            
        }
    });
}

function loadModel(modelName, modelID, type, element) {
    currentSelectedModel = modelName;
    //console.log(element.firstChild.nextSibling);
    if (type == "plan") {
        $(".floor-plan-style").each(function () {
            $(this).removeClass("activeStyle");
        });
        $(element.firstChild.nextSibling).addClass("activeStyle");
        //element.children(":first")
        var src = document.getElementById('#'+modelID).firstElementChild.getAttribute('src');
        $("#mainViewImg").attr("src", src);
        $("#plans").attr("src", src);
        $("#facades").attr("src", 'images/axons/' + src.split('/').slice(-1)[0]);
        $("#facades").unbind().click(function () {
            loadModel(modelID, 'arch');
        })
        $("#interior").attr("src", 'images/sides/' + src.split('/').slice(-1)[0]);
        $("#interior").unbind().click(function () {
            loadModel(modelID, 'arch');
        })
    }

    if (type == "arch") {

        $(".arch-form-style").each(function () {
            $(this).removeClass("activeStyle");
        });
        $(element.firstChild.nextSibling).addClass("activeStyle");
        $("#mainViewImg").hide();
        $("#forgeViewer").show();

        var src = document.getElementById(modelID).firstElementChild.getAttribute('src').split('/').slice(-1)[0];
        $("#plans").attr("src", 'images/plans/' + src);
        $("#facades").attr("src", 'images/axons/' + src);
        $("#facades").unbind().click(function () {
            loadModel(modelID, 'arch');
        })
        $("#interior").attr("src", 'images/sides/' + src);
        $("#interior").unbind().click(function () {
            loadModel(modelID, 'arch');
        })

        $("#forgeViewer").empty();
        var urn = modelID;
        getForgeToken(function (access_token) {
            console.log(access_token);
            jQuery.ajax({
                url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/' + urn + '/manifest',
                headers: { 'Authorization': 'Bearer ' + access_token },
                success: function (res) {
                    if (res.status === 'success') launchViewer(urn);
                    else $("#forgeViewer").html('The translation job still running: ' + res.progress + '. Please try again in a moment.');
                },
                error: function (err) {
                    var msgButton = `This file is not translated yet!
                                    <button class="btn btn-xs btn-info" onClick="translateMyObject('generatedmodelsbucket', '${modelID}')"><span class="glyphicon glyphicon-eye-open"></span>
                                    Start translation</button>`
                    $("#forgeViewer").html(msgButton);
                }
            });
        })
    }

}

function createNewBucket() {
  var bucketKey = $('#newBucketKey').val();
  var policyKey = $('#newBucketPolicyKey').val();
  jQuery.post({
    url: '/api/forge/oss/buckets',
    contentType: 'application/json',
    data: JSON.stringify({ 'bucketKey': bucketKey, 'policyKey': policyKey }),
    success: function (res) {
      $('#appBuckets').jstree(true).refresh();
      $('#createBucketModal').modal('toggle');
    },
    error: function (err) {
      if (err.status === 409)
        alert('Bucket already exists - 409: Duplicated')
      console.log(err);
    }
  });
}

function prepareAppBucketTree() {
    console.log("Here 1");
  $('#appBuckets').jstree({
    'core': {
      'themes': { "icons": true },
      'data': {
        "url": '/api/forge/oss/buckets',
        "dataType": "json",
        'multiple': false,
        "data": function (node) {
          return { "id": node.id };
        }
      }
    },
    'types': {
      'default': {
        'icon': 'glyphicon glyphicon-question-sign'
      },
      '#': {
        'icon': 'glyphicon glyphicon-cloud'
      },
      'bucket': {
        'icon': 'glyphicon glyphicon-folder-open'
      },
      'object': {
        'icon': 'glyphicon glyphicon-file'
      }
    },
    "plugins": ["types", "state", "sort", "contextmenu"],
    contextmenu: { items: autodeskCustomMenu }
  }).on('loaded.jstree', function () {
      $('#appBuckets').jstree('open_all');
      console.log("Here 2");
  }).bind("activate_node.jstree", function (evt, data) {
      console.log(data);
    if (data !== null && data.node !== null && data.node.type === 'object') {
      if (data.node.text.indexOf('.txt') > 0) return;
      $("#forgeViewer").empty();
      var urn = data.node.id;
      getForgeToken(function (access_token) {
        jQuery.ajax({
          url: 'https://developer.api.autodesk.com/modelderivative/v2/designdata/' + urn + '/manifest',
          headers: { 'Authorization': 'Bearer ' + access_token },
          success: function (res) {
            if (res.status === 'success') launchViewer(urn);
            else $("#forgeViewer").html('The translation job still running: ' + res.progress + '. Please try again in a moment.');
          },
          error: function (err) {
            var msgButton = 'This file is not translated yet! ' +
              '<button class="btn btn-xs btn-info" onclick="translateObject()"><span class="glyphicon glyphicon-eye-open"></span> ' +
              'Start translation</button>'
            $("#forgeViewer").html(msgButton);
          }
        });
      })
    }
  });
}

function autodeskCustomMenu(autodeskNode) {
  var items;

  switch (autodeskNode.type) {
    case "bucket":
      items = {
        uploadFile: {
          label: "Upload file",
          action: function () {
            var treeNode = $('#appBuckets').jstree(true).get_selected(true)[0];
            uploadFile(treeNode);
          },
          icon: 'glyphicon glyphicon-cloud-upload'
        }
      };
      break;
    case "object":
      items = {
        translateFile: {
          label: "Translate",
          action: function () {
            var treeNode = $('#appBuckets').jstree(true).get_selected(true)[0];
            translateObject(treeNode);
          },
          icon: 'glyphicon glyphicon-eye-open'
        }
      };
      break;
  }

  return items;
}

function uploadFile(node) {
  $('#hiddenUploadField').click();
  $('#hiddenUploadField').change(function () {
    if (this.files.length === 0) return;
    var file = this.files[0];
    switch (node.type) {
      case 'bucket':
        var formData = new FormData();
        formData.append('inputFile', file);
        formData.append('bucketKey', node.id);
        $.ajax({
          url: '/api/forge/oss/objects',
          data: formData,
          processData: false,
          contentType: false,
          type: 'POST',
          success: function (data) {
            $('#appBuckets').jstree(true).refresh_node(node);
          }
        });
        break;
    }
  });
}

function translateObject(node) {
  $("#forgeViewer").empty();
    if (node === null || node === undefined)  node = $('#appBuckets').jstree(true).get_selected(true)[0];

    startConnection(function () {
        var bucketKey = node.parents[0];
        var objectKey = node.id;
        jQuery.post({
            url: '/api/forge/modelderivative/jobs',
            contentType: 'application/json',
            data: JSON.stringify({ 'bucketKey': bucketKey, 'objectName': objectKey, 'connectionId': connectionId }),
            success: function (res) {
                $("#forgeViewer").html('Translation started! Model will load when ready  <div class="loader"></div> ');
                
            }
        });
  });
}

function translateMyObject(bucketKey, objectKey) {

    $("#forgeViewer").empty();
    startConnection(function () {
        console.log(bucketKey);
        console.log(objectKey);
        console.log(connectionId);
        jQuery.post({
            url: '/api/forge/modelderivative/jobs',
            contentType: 'application/json',
            data: JSON.stringify({ 'bucketKey': bucketKey, 'objectName': objectKey, 'connectionId': connectionId }),
            success: function (res) {
                $("#forgeViewer").html('Translation started! Model will load when ready  <div class="loader"></div> ');
                //$("#forgeViewer").html('<span class="loader"></span>');
                //console.log(res.progress);
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
}


function ShowPlansMenu() {
    $("#accordion2").hide();
    $("#accordion3").hide();
    $("#accordion4").hide();
    $("#accordion1").show();
}

function ShowExteriorMenu() {
    $("#accordion1").hide();
    $("#accordion2").hide();
    $("#accordion4").hide();
    $("#accordion3").show();
}

function ShowMyModelsMenu() {
    $("#accordion1").hide();
    $("#accordion2").hide();
    $("#accordion3").hide();
    $("#accordion4").show();
    getMyModels();
}

function getMyModels() {
    jQuery.ajax({
        url: '/api/forge/oss/buckets',
        method: 'GET',
        data: {
            'id': 'generatedmodelsbucket',
        },
        success: function (result) {
            var i = 1;
            result.forEach(function (item) {
                //var modelName = item.text.split('.')[0].split('_')[0];
                //var storiesNumber = item.text.split('.')[0].split('_')[1];
                //var bedCount = item.text.split('.')[0].split('_')[2];
                //var area = item.text.split('.')[0].split('_')[3];

                $('#mymodels').append(`<div id="${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.text}','${item.id}', 'arch', this)" data-toggle="tooltip" data-placement="right" title="${item.text}">
                    <img class="img-responsive arch-form-style" src ="http://placehold.jp/100x100.png?text=${item.text}" alt ="${item.text}" />
                                    </div >`);

            });

        }
    });
}