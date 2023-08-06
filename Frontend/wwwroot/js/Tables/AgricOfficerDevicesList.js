var datatables;

$(document).ready(function () {
    datatables = $('#deviceTable').DataTable({
        "ajax": {
            "url": "/Dashboard/AgricOfficerDevicesList",
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
                "width": "10%",

            },
            {
                "data": "pid",
                "width": "10%",

            },
            {
                "data": "bn",
                "width": "20%",

            },
            {
                "data": "lastPowerOnDate",
                "width": "20%",

            },

            {
                "data": "lastPowerOnTime",
                "width": "20%",
               
            }

        ],
        "language": {
            "processing": '<div class="spinner-border text-primary"></div>',
            zeroRecords: "No record found"
        },
        "width": "100%",
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        responsive: true,


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
                        toastr.success(data.message,"Congratulations");
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