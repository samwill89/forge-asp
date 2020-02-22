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
    TestOBjectsData();

    $('.pickPlan').click(function () {
        $("#accordion1").hide();
        $("#accordion2").show();
    })

    $('#modify').click(function () {
        $("#accordion2").hide();
        $("#accordion3").show();
    })


    $("#plans").click(function () {
        
    })

    $("#facades").click(function () {

    })

    $("#interior").click(function () {

    })
    //$("#facades").attr("src", 'images/axons/' + src.split('/').slice(-1)[0]);
    //$("#interior").attr("src", 'images/sides/' + src.split('/').slice(-1)[0]);
  //prepareAppBucketTree();
  //$('#refreshBuckets').click(function () {
  //  $('#appBuckets').jstree(true).refresh();
  //});

  //$('#createNewBucket').click(function () {
  //  createNewBucket();
  //});

  //$('#createBucketModal').on('shown.bs.modal', function () {
  //  $("#newBucketKey").focus();
  //})
});

function logKey(e) {
    if (e.code == 'KeyX') {
        TestOBjectsData();
    }
}



function TestOBjectsData() {
    jQuery.ajax({
        url: '/api/forge/oss/buckets',
        method: 'GET',
        data: {
            'id': "facadedemobucket",
        },
        success: function (result) {

            var i = 1;
            $('#floorPlansList').empty();
            result.forEach(function (item) {
                // img source should be the same as the text of the model
                // onClick here will load the corresponding image
                $('#floorPlansList').append(`<div id="#${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.id}', 'plan')" data-toggle="tooltip" data-placement="right" title="${item.text}">
                    <img class="img-responsive" src ="images/plans/${i}.png" alt ="${item.text}" />
                                    </div >`);

                if (i % 3 == 0) {
                    // onClick here will load the corresponding image
                    $('#floorPlansList').append(`<br />`);
                }
                i++;
            });

            i = 1;
            $('#archFormList').empty();
            result.forEach(function (item) {
                // img source should be the same as the text of the model
                // onClick here will load the corresponding 3d model
                $('#archFormList').append(`<div id="${item.id}" class="col-sm-4 inner-img" onClick="loadModel('${item.id}', 'arch')" data-toggle="tooltip" data-placement="right" title="${item.text}">
                    <img class="img-responsive" src ="images/axons/${i}.png" alt ="${item.text}" />
                                    </div >`);

                if (i % 3 == 0) {
                    // onClick here will load the corresponding image
                    $('#archFormList').append(`<br />`);
                }
                i++;
            });


            $('#baseMaterialsList').empty();

            for (var x = 1; x <= 6; x++) {
                $('#baseMaterialsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${x}">
                    <img class="img-responsive" src ="images/walls_materials/${x}.png" alt ="${x}" />
                                    </div >`);
            }

            $('#secondaryWindowsList').empty();

            for (var x = 1; x <= 9; x++) {
                $('#secondaryWindowsList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${x}">
                    <img class="img-responsive" src ="images/windows/${x}.png" alt ="${x}" />
                                    </div >`);
            }
            
        }
    });
}

function loadModel(modelID, type) {

    if (type == "plan") {
        //var src = $("#" + modelID).children(":first").attr("src");
        var src = document.getElementById('#'+modelID).firstElementChild.getAttribute('src');
        //console.log(src.split('/').slice(-1)[0]);
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
