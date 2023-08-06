var datatables;

$(document).ready(function () {
    datatables = $('#farmTable').DataTable({
        "ajax": {
            "url": "/Farm/FarmsRegisteredByOfficerList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

           
            {
                "data": "farmName",
                "width": "20%",

            },
            {
                "data": "location",
                "width": "15%",

            },
            {
                "data": "regionName",
                "width": "15%",

            },
            {
                "data": "farmerName",
                "width": "20%",

            },
            {
                "data": "farmerContact",
                "width": "15%",

            },

            {
                "data": "farmId",
                "width": "15%",
                "render": function (data) {
                    return `<div class='text-center'>

                        <a class='btn btn-primary' href='/Farm/UpdateFarmerFarm/${data}' title='Edit farm information' ><span class='fas fa-edit'></span></a>
                        <a class='btn btn-secondary text-white' href='/Farm/GetFarmerFarmDetails?id=${data}' title='View farm information' style='cursor:pointer'><span class='fas fa-plus'>  </span></a>
                       
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