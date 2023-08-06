var datatables;

$(document).ready(function () {
    datatables = $('#SoildataFromDeviceTable').DataTable({
        "ajax": {
            "url": "/SoilData/ViewSoilDataFromDeviceByAdminList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [


            {
                "data": "serialNumber",
                "width": "15%",

            },
            {
                "data": "soilMoisture",
                "width": "10%",

            },
            {
                "data": "temperature",
                "width": "10%",

            },
            {
                "data": "humidity",
                "width": "10%",

            },
            {
                "data": "date",
                "width": "10%",

            },

            {
                "data": "soilDataId",
                "width": "15%",
                "render": function (data) {
                    return `<div class='text-center'>

                        <a class='btn btn-light' href='/SoilData/ViewSoilData/${data}' title='View as charts' style='background: #95C942;color:#ffffff;cursor:pointer'><span class='fas fa-chart-line'></span></a>
                        <a class='btn btn-danger text-white' onClick=Delete('/SoilData/DeleteSoilData?id=${data}') title='Delete soil data' style='cursor:pointer' ><span class='fas fa-trash-alt'></span></a>

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
                        // toastr.success(data.message,"Congratulations");
                        swal("Congratulations!", data.message, "success");
                        datatables.ajax.reload();
                    } else {
                        //toastr.error(data.message, "Error");
                        swal("Sorry!", data.message, "Error");
                    }
                }
            })
        }

    })
}