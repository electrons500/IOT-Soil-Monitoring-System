var datatables;

$(document).ready(function () {
    datatables = $('#activatedDevicesOnsiteAndUnActiveTable').DataTable({
        "ajax": {
            "url": "/Arduino/GetActivatedDevicesRegisterdByAgricExtensionOfficerThatAreOnsiteAndUnactiveList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

            {
                "data": "serialNumber",
                "width": "20%",

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
                "data": "isOnsite",
                "width": "15%",
                "render": function (data) {
                    return `<div class='text-center'>

                              <span style='color:#95C942;'  class='fa fa-check fa-2x'></span>
                        </div>`

                }
            },
            {
                "data": "isActive",
                "width": "15%",
                "render": function (data) {
                    return `<div class='text-center'>
                               <span style='color:red;'  class='fa fa-times fa-2x'></span>
                        </div>`

                }
            },
           

            {
                "data": "arduinoId",
                "width": "20%",
                "render": function (data) {
                    return `<div class='text-center'>
                        
                       
                        <a class='btn btn-danger text-white' onClick=SetArduinoToActive('/Arduino/SetArduinoToActive?id=${data}') title='set to active' style='cursor:pointer' ><span class='fa fa-paper-plane-o'></span> set to active</a>

                       
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

function SetArduinoToActive(path) {
    swal({
        title: "Are you sure you want to set to active?",
        text: "Proceed if the device is on the farm and working",
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