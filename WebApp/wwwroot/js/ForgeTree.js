/////////////////////////////////////////////////////////////////////
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


    //Preparing the walls & materials section
    $('#baseMaterialsList').empty();
    uniqueCSVData['BaseMaterial_T'].forEach(el => {
        $('#baseMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive base-materials" src ="images/walls_materials/${el}.png" alt ="${el}" onClick="ModifyCSV(this, 'BaseMaterial_T', '${el}')" />
                                    </div >`);
    })
    $('#baseMaterialsColors').empty();
    uniqueCSVData['BaseMaterialColor_T'].forEach(el => {
        $('#baseMaterialsColors').append(`<option>${el}</option>`);
    })


    $('#mainMaterialsList').empty();
    uniqueCSVData['MainMaterial_T'].forEach(el => {
        $('#mainMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive base-materials" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'MainMaterial_T', '${el}')" />
                                    </div >`);
    })
    $('#mainMaterialsColors').empty();
    uniqueCSVData['MainMaterialColor_T'].forEach(el => {
        $('#mainMaterialsColors').append(`<option>${el}</option>`);
    })

    $('#accentMaterialsList').empty();
    uniqueCSVData['AccentMaterial_T'].forEach(el => {
        $('#accentMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive base-materials" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'AccentMaterial_T', '${el}')" />
                                    </div >`);
    })
    $('#accentMaterialsColors').empty();
    uniqueCSVData['AccentMaterialColor_T'].forEach(el => {
        $('#accentMaterialsColors').append(`<option>${el}</option>`);
    })



    //Preparing the Columns section
    $('#colCapitalList').empty();
    uniqueCSVData['ColumnCapitalStyle_T'].forEach(el => {
        $('#colCapitalList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive base-materials" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'ColumnCapitalStyle_T', '${el}')" />
                                    </div >`);
    })
    $('#columnColor').empty();
    uniqueCSVData['ColumnColor_T'].forEach(el => {
        $('#columnColor').append(`<option>${el}</option>`);
    })

    $('#colBaseList').empty();
    uniqueCSVData['ColumnBaseStyle_T'].forEach(el => {
        $('#colBaseList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${el}">
                    <img class="img-responsive base-materials" src ="http://placehold.jp/100x100.png?text=${el}" alt ="${el}" onClick="ModifyCSV(this, 'ColumnBaseStyle_T', '${el}')" />
                                    </div >`);
    })
    $('#columnPlinthHeight').empty();
    uniqueCSVData['ColumnPlinthHeight_T'].forEach(el => {
        $('#columnPlinthHeight').append(`<option>${el}</option>`);
    })











    //$('#secondaryWindowsList').empty();
    //for (var x = 1; x <= 9; x++) {
    //    $('#secondaryWindowsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${x}">
    //                <img class="img-responsive" src ="images/windows/${x}.png" alt ="${x}" />
    //                                </div >`);
    //}

    //console.log(uniqueCSVData);
}
function logKey(e) {
    if (e.code == 'KeyX') {

    }
}

function populateExteriorMenu() {
    console.log(csvData);
    console.log(uniqueCSVData);
}

function ModifyCSV(element, key, value) {
    csvData[currentSelectedStyle][key] = value;
    $(".base-materials").each(function () {
        $(this).removeClass("activeStyle");
    });
    $(element).addClass("activeStyle");
}

function updateCSV(element, key) {
    csvData[currentSelectedStyle][key] = element.selectedOptions[0].value;
}


function TestOBjectsData(facadestyle) {
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
                $("#forgeViewer").html('Translation started! Model will load when ready..');
            }
        });
  });
}
