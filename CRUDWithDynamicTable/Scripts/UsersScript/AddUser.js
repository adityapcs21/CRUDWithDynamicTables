$(document).ready(function () {
    $('#dataTable').DataTable({
        "ajax": {
            "url": "/Home/GetData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "UserId" },
            { "data": "FullName" },
            { "data": "UserEmail" },
            { "data": "CivilIdNumber" },
            { "data": "CarLicense" },

            {
                mRender: function (data, type, row) {

                    return '<a href="javascript:;" id="#EditUser" data-toggle="modal" data-target="#EditUser" value="Edit User" class="edit-cancel text-uppercase" onclick="return EditUser(' + row.UserId + ')">Edit</a>'
                }
            },
            {
                mRender: function (data, type, row) {

                    return '<a href="javascript:;"   onclick="return DeleteUser(' + row.UserId + ')">Delete</a>'
                }
            }
        ]
    });
});





function InsertUser() {
    debugger;
    var userName = $("#AName").val();
    var Email = $("#ALat").val();
    var civilid = $("#CivilName").val();
    var carLicense = $("#CName").val();
    var imagenBase64 = $("#pImageBase64").html();

    $.ajax({
        url: '/Home/InsertuS',
        type: 'POST',
        data: JSON.stringify({
            FullName: userName,
            UserEmail: Email,
            CivilIdNumber: civilid,
            CarLicense: carLicense,
            ProfilePic: imagenBase64


        }),
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (data) {
            //window.reload();
            //$("#addUser").hide();
            //$("#addUser").addClass('hide');
            $('#addUser').modal('hide');

        }

    });

}
function encodeImagetoBase64(element) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        $("#image").attr("src", reader.result);
        $("#pImageBase64").html(reader.result);

    }
    reader.readAsDataURL(file);
}


function DeleteUser(Id) {
    debugger;
    var result = confirm('Are you sure you wish to delete this record?');
    if (result) {
        $.ajax({
            url: '/Home/Delete?id=' + Id,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (data) {
                window.reload();
                //$("#addUser").hide();
                //$("#addUser").addClass('hide');
            }

        });
    }

}



//get data for updation....................................................................................................




function EditUser(Id) {
    debugger;
    $.ajax({
        url: '/Home/Edit?Id=' + Id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (data) {
            //debugger;
            console.log(data);

            $('#gid').val(data.UserId);
            $('#Name').val(data.FullName);
            $('#emailid').val(data.UserEmail);
            $('#CivilIdNumber').val(data.CivilIdNumber);
            $('#cLicense').val(data.CarLicense);


            var sno = 0;
            var div = $('#CName1');
            div.empty();

            data.CarDetails.forEach(function (event) {

                div.append("<label>CarLicense" + (sno + 1) + "</label>" +
                    "<input class='form-control' id='" + event.Id + "' name='CName1' type='text' value='" + event.CarLicense + "' />");

                sno++;
            });
        }
        //$("#editUser").hide();
        //$("#editUser").addClass('hide');


    });
}




function UpdateUser() {
    debugger;

    var UserId = $("#gid").val();
    var userName = $("#Name").val();
    var Email = $("#emailid").val();
    var CivilIdNumber = $("#CivilIdNumber").val();
    //var carLicense = $("#cLicense").val();
    var carLicense = $('#CName1 input').val();

    var Car = new Array();
    $("input[name='CName1']").each(function () {
        Car.push($(this).attr('id'));
    });

    var CarUser = new Array();
    Car.forEach(function (value) {
        CarUser.push({
            Id: value,
            CarLicense: $("#" + value).val()
        });
        console.log(CarUser);
    });

    $.ajax({
        url: '/Home/EdituS',
        type: 'POST',
        data: JSON.stringify({
            UserId: UserId,
            FullName: userName,
            UserEmail: Email,
            CivilIdNumber: CivilIdNumber,
            CarLicense: carLicense
        }),
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        success: function (data) {
            //window.reload();
            //console.log(data.success);
            $('#EditUser').modal('hide');

        }

    });
}
