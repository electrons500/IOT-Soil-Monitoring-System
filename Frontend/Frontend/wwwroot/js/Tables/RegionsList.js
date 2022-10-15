var datatables;

$(document).ready(function () {
    datatables = $('#emptable').DataTable({
        "ajax": {
            "url": "/Regions/RegionsList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "regionId",
                "width": "20%",

            },

            {
                "data": "regionName",
                "width": "60%",

            },

            {
                "data": "regionId",
                "width": "20%",
                "render": function (data) {
                    return `<div class='text-center'>
                        
                        <a class='btn btn-primary' href='/Regions/EditRegion?id=${data}' title='Edit'><span class='fas fa-edit'></span></a>
                        <a class='btn btn-danger text-white' onClick=Delete('/Regions/DeleteRegion?id=${data}') title='Delete' style='cursor:pointer' ><span class='fas fa-trash-alt'></span></a>

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
                        //toastr.success(data.message, "Congratulations");
                       swal("Congratulation!", data.message, "success");
                        datatables.ajax.reload();
                    } else {
                       // toastr.error(data.message, "Error");
                        swal("Sorry!", data.message, "Error");
                    }
                }
            })
        }

    })
}