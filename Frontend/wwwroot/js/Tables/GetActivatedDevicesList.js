﻿var datatables;

$(document).ready(function () {
    datatables = $('#activatedArduinoTable').DataTable({
        "ajax": {
            "url": "/Arduino/GetActivatedDevicesList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

            {
                "data": "serialNumber",
                "width": "20%",

            },
            {
                "data": "vid",
                "width": "15%",

            },
            {
                "data": "pid",
                "width": "15%",

            },
            {
                "data": "bn",
                "width": "15%",

            },
            {
                "data": "isActivated",
                "width": "15%",
                "render": function (data) {
                    return `<div class='text-center'>

                            <span style='color:#95C942;'  class='fa fa-check fa-2x'></span>

                        </div>`

                }
            },
           

            {
                "data": "arduinoId",
                "width": "20%",
                "render": function (data) {
                    return `<div class='text-center'>
                        
                        <a class='btn btn-primary'  href='/Arduino/UpdateArduino?id=${data}' title='Edit'><span class='fas fa-edit'></span></a>
                        <a class='btn btn-secondary'  href='/Arduino/ArduinoDetails?id=${data}' title='Details'><span class='fas fa-glasses'></span></a>
                        <a class='btn btn-danger text-white' onClick=Delete('/Arduino/DeleteArduino?id=${data}') title='Delete' style='cursor:pointer' ><span class='fas fa-trash-alt'></span></a>

                       
                        </div>`

                }
            }

        ],
        "language": {
            "processing": '<div class="spinner-border text-primary"></div>',
            zeroRecords: "No record found"
        },
        "width": "100%",
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        responsive: true
      

    });

    new $.fn.dataTable.FixHeader(datatables);
});

function Delete(path) {
    swal({
        title: "Are you sure you want to delete?",
        text: "deletion cannot be undone",
        icon: "warning",
        buttons: true,
        dangermode: true

    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: path,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message, "Congratulations");
                        // swal("Congratulation!", data.message, "success");
                        datatables.ajax.reload();
                    } else {
                        toastr.error(data.message, "Error");
                        // swal("Sorry!", data.message, "Error");
                    }
                }
            })
        }

    })
}