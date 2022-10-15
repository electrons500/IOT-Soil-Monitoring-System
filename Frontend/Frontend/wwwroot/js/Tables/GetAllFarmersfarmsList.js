var datatables;

$(document).ready(function () {
    datatables = $('#farmerFarmTable').DataTable({
        "ajax": {
            "url": "/Farm/GetAllFarmersfarmsList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

           
            {
                "data": "farmName",
                "width": "25%",

            },
            {
                "data": "location",
                "width": "20%",

            },
            {
                "data": "regionName",
                "width": "20%",

            },
            {
                "data": "soilCategoryName",
                "width": "15%",

            },
            {
                "data": "farmId",
                "width": "20%",
                "render": function (data) {
                    return `<div class='text-center'>

                        <a class='btn btn-light' href='/SoilData/SoilDataFromDevice?id=${data}' title='Check soil data' style='background: #8E24AA;color:#ffffff;cursor:pointer'><span class='fas fa-temperature-half'></span></a>
                        <a class='btn btn-light' href='/Farm/UpdateFarmerFarm/${data}' title='Edit farm information' style='background: #95C942;color:#ffffff;cursor:pointer'><span class='fas fa-edit'></span></a>
                        <a class='btn btn-secondary text-white' href='/Farm/GetFarmerfarmDetails?id=${data}' title='Add farm information' style='cursor:pointer'><span class='fas fa-plus'>  </span></a>
                        <a class='btn btn-danger text-white' onClick=Delete('/Farm/DeleteFarm?id=${data}') title='Delete farm information' style='cursor:pointer' ><span class='fas fa-trash-alt'></span></a>

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