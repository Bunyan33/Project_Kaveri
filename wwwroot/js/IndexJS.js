$(document).ready(function () {

    ReadAllPatients();

});

$('#addPatient').click(function () {
    $('#PatientModal').modal('show');
    $('#modalTitle').text('Create Patient');
});

function Insert() {
    var formData = new Object();
    formData.Name = $('#Name').val();
    formData.Age = $('#Age').val();
    formData.City = $('#City').val();
    formData.Gender = $('.gender:checked').val();
    formData.Address = $('#Address').val();
    formData.PhoneNo = $('#PhoneNo').val();

    $.ajax({
        url: '/Patients/CreatePatient',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save data!');
            } else {
                HideModel();
                alert(response);
                ReadAllPatients();
            }
        },
        error: function () {
            alert('Unable to save patient data!');
        }
    });
}

function ReadAllPatients() {
    $.ajax({
        url: '/Patients/ReadAllPatients',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var obj = '';
                obj += '<tr>';
                obj += '<td class="text-center" colspan="8">' + 'Patients Data not available in the Database' + '</ td>';
                obj += '</ tr>';

                $('#table').html(obj);
            }
            else {
                var obj = '';
                $.each(response, function (index, item) {
                    console.log("Processing user:", item.id, item.Email, item.Gender, item.PhoneNum,
                        item.DataCreated, item.DataUpdated);

                    obj += '<tr>';
                    obj += '<td>' + item.id + '</ td>';
                    obj += '<td>' + item.name + '</ td>';
                    obj += '<td>' + item.age + '</ td>';

                    obj += '<td>' + item.gender + '</ td>';
                    obj += '<td>' + item.city + '</ td>';

                    obj += '<td>' + item.phoneNo + '</ td>';
                    obj += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a> </ td>';
                    obj += '</tr>';
                });
                $('#table').html(obj);
            }
        },
        error: function () {
            alert('Unable to read the Data');
        }
    });
}

function Edit(Id) {
    $.ajax({
        url: '/Patients/EditPatient?id=' + Id,
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to Read the patient data!');
            }
            else {
                $('#PatientModal').modal('show');
                $('#modalTitle').text('Update Patient');
                $('#submit').css('display', 'none');
                $('#Update').css('display', 'block');

                $('#Id').val(response.id);
                $('#Name').val(response.name);
                $('#Age').val(response.age);
                $('input[name="flexRadio"][value=' + response.gender + ']').prop('checked', true);
                $('#City').val(response.city);
                $('#Address').val(response.address);
                $('#PhoneNo').val(response.phoneNo);

            }

        },
        error: function () {
            alert('Unable to Read data!');
        }
    });
}

function Update() {
    var formData = new Object();
    formData.Id = $('#Id').val();
    formData.Name = $('#Name').val();
    formData.Age = $('#Age').val();
    formData.City = $('#City').val();
    formData.Gender = $('.gender:checked').val();
    formData.Address = $('#Address').val();
    formData.PhoneNo = $('#PhoneNo').val();


    $.ajax({
        url: '/Patients/UpdatePatient',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save Patient data!');
            } else {
                HideModel();
                ReadAllPatients();
                alert(response);

            }
        },
        error: function () {
            alert('Unable to save data!');
        }
    });
}

function Delete(Id) {
    if (confirm('Are you sure to delete this data?')) {

        $.ajax({
            url: '/Patients/DeletePatient?id=' + Id,
            type: 'POST',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to Delete the data!');
                }
                else {
                    alert(response);
                    ReadAllPatients();
                }

            },
            error: function () {
                alert('Unable to Delete the data!');
            }
        });
    }
}


function HideModel() {
    ClearData();
    $('#PatientModal').modal('hide');
    $('#submit').css('display', 'block');
    $('#Update').css('display', 'none');
}

function ClearData() {
    $('#Name').val('');
    $('#Age').val('');
    $('#City').val('');
    $('input[name="flexRadio"][value="Others"]').prop('checked', true);
    $('#Address').val('');
    $('#PhoneNo').val('');

}

function Avatar() {
    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $(".file-upload").on('change', function () {
        readURL(this);
    });

    $(".upload-button").on('click', function () {
        $(".file-upload").click();
    });
}