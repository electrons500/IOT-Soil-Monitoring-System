var datatables;

$(document).ready(function () {
    datatables = $('#farmTable').DataTable({
        "ajax": {
            "url": "/Farm/GetRegisteredFarmAndTheirMapLocationList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

           
            {
                "data": "farmName",
                "width": "20%",

            },
            {
                "data": "farmLocation",
                "width": "20%",

            },
            {
                "data": "latitude",
                "width": "15%",

            },
            {
                "data": "longitude",
                "width": "15%",

            },
            {
                "data": "maplocationId",
                "width": "30%",
                "render": function (data) {
                    return `<div class='text-center'>

                        <a class='btn btn-primary' href='/Farm/EditFarmMapLocationDetails/${data}' title='Edit map location' ><span class='fas fa-edit'></span></a>
                        <a class='btn btn-secondary text-white' href='/Farm/FarmMaplocationDetails?id=${data}' title='View map location' style='cursor:pointer'><span class='fas fa-plus'>  </span></a>
                       
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