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

    
    prepareLists();

    //$('#clearAccount').click(clearAccount);
    //$('#defineActivityShow').click(defineActivityModal);
    $('#createAppBundleActivity').click(createAppBundleActivity);
    //$('#startWorkitem').click(startWorkitem);

    startConnection();

    $(".selectBaseMaterial").click(function () {
        currentSelectedBucket = csvData[currentSelectedStyle]['Style'].toLowerCase() + 'bucket';
        startWorkitem();
    })

    

    document.addEventListener('keydown', event => {
        if (event.keyCode == 65) {    
            currentSelectedBucket = csvData[currentSelectedStyle]['Style'].toLowerCase() + 'bucket';
            console.log(currentSelectedBucket);
            console.log(currentSelectedModel);
            console.log(csvData[currentSelectedStyle]);
        }
    });
    getAllData();

});
var csvData;
var currentSelectedStyle = 0;
var currentSelectedBucket = "";
var currentSelectedModel = "";
// This function will retrieve the csv file found at WebApp/wwwroot/CSVData/BIM Quote Facades.csv with the help of the API 'api/forge/appdata/all'
// that can be found at wwwroot/Controllers/AppDataController.cs
// Then it populates the first menud (#accordion2) with this data
function getAllData() {
    jQuery.ajax({
        url: 'api/forge/appdata/all',
        method: 'GET',
        success: function (result) {
            // assigning the result to a variable as we will need it later
            csvData = result;
            // This loop will replace every '-' to '_' as these are the variable names (can't contain '-') in the back-end
            // for example. 'WinTrimBottom-T' will be 'WinTrimBottom_T'
            csvData.forEach((element) => {
                Object.keys(element).forEach(function (key) {
                    if (key.indexOf('-') != -1) {
                        var newstr = key.replace('-', '_');
                        element[newstr] = element[key];
                        delete element[key];
                    }
                });
            });
            var i = 1;
            var styles = Object.keys(result[0]); // This variable will contain all the parameters in the CSV, like "MainMaterial_T", "BaseMaterial_T", and so on
            styles.splice(0, 1); // The first element is just the word 'style' and it's not a real parameter
            $('#popularityList').empty(); // make sure that nothing is in theis div element
            // In this loop, we will use the 'result' variable to populate the list of styles in the CSV file, here, we will show only the first 9
            // Also. the images can be found in "WebApp\wwwroot\images\facades" and everr images is named as the name of this style
            // We also bind every image with an onClick event that calls the GetStyleData() function
            result.forEach(function (item) {
                if (i <= 9) {
                    $('#popularityList').append(`<div class="col-sm-4 inner-img" data-toggle="tooltip" data-placement="right" title="${item.Style}">
                    <img id="style${i - 1}" class="img-responsive styles" src ="images/facades/${item.Style}.jpg" alt ="${item.Style}" onClick="GetStyleData('${i-1}')" />
                                    </div >`);
                    i++;
                }
            });
            
        }
    });
}

// This function will be called whenever a style is selected
// And it's responsible for displaying the border around the image of that style and also to save the current style index for later
function GetStyleData(index) {
    currentSelectedStyle = index;
    $(".styles").each(function () {
        $(this).removeClass("activeStyle");
    });
    $('#style' + index).addClass("activeStyle");
}

function logKey(e) {
    console.log("asds");
    if (e.code == 'KeyX') {
        console.log("Clicked!");
        //startWorkitem();
    }
}

function prepareLists() {
    list('activity', '/api/forge/designautomation/activities');
    list('engines', '/api/forge/designautomation/engines');
    list('localBundles', '/api/appbundles');
}

function list(control, endpoint) {
    $('#' + control).find('option').remove().end();
    jQuery.ajax({
        url: endpoint,
        success: function (list) {
            if (list.length === 0)
                $('#' + control).append($('<option>', { disabled: true, text: 'Nothing found' }));
            else
                list.forEach(function (item) { $('#' + control).append($('<option>', { value: item, text: item })); })
        }
    });
}


function fillCount( result ) {

    for (var elem in result) {
        if (elem === 'total')
            continue;
        $('#' + elem)[0].innerText = result[elem];
    }
}

function clearAccount() {
    if (!confirm('Clear existing activities & appbundles before start. ' +
        'This is useful if you believe there are wrong settings on your account.' +
        '\n\nYou cannot undo this operation. Proceed?')) return;

    jQuery.ajax({
        url: 'api/forge/designautomation/account',
        method: 'DELETE',
        success: function () {
            prepareLists();
            writeLog('Account cleared, all appbundles & activities deleted');
        }
    });
}

function defineActivityModal() {
    $("#defineActivityModal").modal();
}

function createAppBundleActivity() {
    startConnection(function () {
        writeLog("Defining appbundle and activity for " + $('#engines').val());
        $("#defineActivityModal").modal('toggle');
        createAppBundle(function () {
            createActivity(function () {
                prepareLists();
            })
        });
    });
}

function createAppBundle(cb) {
    jQuery.ajax({
        url: 'api/forge/designautomation/appbundles',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            zipFileName: $('#localBundles').val(),
            engine: $('#engines').val()
        }),
        success: function (res) {
            writeLog('AppBundle: ' + res.appBundle + ', v' + res.version);
            if (cb) cb();
        }
    });
}

function createActivity(cb) {
    jQuery.ajax({
        url: 'api/forge/designautomation/activities',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            zipFileName: $('#localBundles').val(),
            engine: $('#engines').val()
        }),
        success: function (res) {
            writeLog('Activity: ' + res.activity);
            if (cb) cb();
        }
    });
}


function startWorkitem() {
    let activityId = "DeleteElementsActivity+dev";
    if (activityId == null) { alert('Please select an activity'); return };

    if (activityId.toLowerCase() === "countitactivity+dev"
        || activityId.toLowerCase() === "deleteelementsactivity+dev" ) {
        startConnection(function () {
            var formData = new FormData();
            formData.append('objectId', currentSelectedModel);
            formData.append('bucketId', currentSelectedBucket);
            formData.append('activityId', activityId);
            formData.append('browerConnectionId', connectionId);
            formData.append('data', JSON.stringify(csvData[currentSelectedStyle]));
            writeLog('Start checking input file...');
            $.ajax({
                url: 'api/forge/designautomation/startworkitem',
                data: formData,
                processData: false,
                contentType: false,
                type: 'POST',
                success: function (res) {
                    writeLog('Workitem started: ' + res.workItemId);
                    $("#mainViewImg").hide();
                    $("#forgeViewer").show();
                    $("#forgeViewer").html(`Workitem started  <div class="loader"></div>`);
                }
            });
        });

    }
}

function writeLog(text) {
    console.log(text);
    //$('#outputlog').append('<div style="border-top: 1px dashed #C0C0C0">' + text + '</div>');
    //var elem = document.getElementById('outputlog');
    //elem.scrollTop = elem.scrollHeight;
}

var connection;
var connectionId;

function startConnection(onReady) {
    if (connection && connection.connectionState) { if (onReady) onReady(); return; }
    connection = new signalR.HubConnectionBuilder().withUrl("/api/signalr/forgecommunication").build();
    connection.start()
        .then(function () {
            connection.invoke('getConnectionId')
                .then(function (id) {
                    connectionId = id; // we'll need this...
                    //console.log(connectionId)
                    if (onReady) onReady();
                });
        });

    connection.on("downloadResult", function (url) {
       
    });

    connection.on("countItResult", function (result) {
        //fillCount(JSON.parse(result));
        writeLog(result);
    });
    connection.on("onComplete", function (message) {
        try {
            var j = JSON.parse(message);
            jQuery.ajax({
                url: '/api/forge/oss/buckets',
                method: 'GET',
                data: {
                    'id': 'generatedmodelsbucket',
                },
                success: function (result) {
                    var newModel;
                    var biggestTime = 0;
                    // Here, we know that the new model has the most recent time and thats how we decide that it's the model we want to view
                    result.forEach(function (item) {
                        if (parseInt(item.text.split('.')[0].split('_')[0]) > biggestTime) {
                            biggestTime = parseInt(item.text.split('.')[0].split('_')[0]);
                            newModel = item;
                        }
                    });

                    $("#forgeViewer").empty();
                    //console.log(currentFacadeStyle);
                    //console.log(biggestTime);
                    //console.log(newModel);
                    var urn = newModel.id;

                        jQuery.post({
                            url: '/api/forge/modelderivative/jobs',
                            contentType: 'application/json',
                            data: JSON.stringify({ 'bucketKey': 'generatedmodelsbucket', 'objectName': newModel.id, 'connectionId': connectionId }),
                            success: function (res) {
                                $("#mainViewImg").hide();
                                $("#forgeViewer").show();
                                $("#forgeViewer").html('Translation started! Model will load when ready <div class="loader"></div> ');
                            },
                            error: function (err) {
                                console.log(err);
                                var msgButton = `This file is not translated yet!
                                    <button class="btn btn-xs btn-info" onClick="translateMyObject('generatedmodelsbucket', '${newModel.id}')"><span class="glyphicon glyphicon-eye-open"></span>
                                    Start translation</button>`
                                $("#forgeViewer").html(msgButton);
                            }
                        });


                }
            });
        } catch (e) {
            
        }
    });
    connection.on("extractionFinished", function (data) {
        $("#forgeViewer").empty();
        launchViewer(data.resourceUrn);
    });

}
