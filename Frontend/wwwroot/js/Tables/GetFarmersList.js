var datatables;

$(document).ready(function () {
    datatables = $('#farmerTable').DataTable({
        "ajax": {
            "url": "/Farmer/GetFarmersList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

            {
                "data": "base64StringPic",
                "width": "10%",
                "render": function (data) {
                    return `<div >
                             <img src='data:image/*;base64,${data}' alt='farmer image' class='rounded-circle' width='50' height='50' />
                        </div>`

                }
            },
            {
                "data": "fullName",
                "width": "25%",

            },
            {
                "data": "address",
                "width": "15%",

            },
            {
                "data": "location",
                "width": "15%",

            },
            {
                "data": "contact",
                "width": "15%",

            },
            {
                "data": "farmerId",
                "width": "20%",
                "render": function (data) {
                    return `<div class='text-center'>
                                             
                        <a class='btn btn-primary'   href='/Farmer/EditFarmerDetails?id=${data}' title='Edit' ><span class='fas fa-edit'></span></a>
                        <a class='btn btn-secondary text-white' href='/Farmer/FarmerDetails/${data}' title='View farmer information' style='cursor:pointer'><span class='fas fa-eye'>  </span></a>
                        <a class='btn btn-light' text-white' href='/Farm/GetAllFarmsByFarmerId/${data}' title='Go to farmer farm' style='background: #95C942;color:#ffffff;cursor:pointer'><span class='fas fa-tree-city'>  </span></a>
                       
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