function ChangeActivation(userId) {
    $.ajax({
        url: "/Users/ChangeActivation",
        type: "PUT",
        dataType: "json",
        data: {
            userId: userId
        },
        success: function (response) {
            debugger;
            if (response.success) {
                // If success is true, do something with the success message
                toastr.success('تم تغيير حاله المستخدم بنجاح');
                // setInterval(function () { window.location.href = "/Advertisments/Index" }, 2000);
            } else {
                toastr.error(response.message);
                // setInterval(function () { window.location.href = "/Advertisments/Index" }, 2000);
            }
        },
        failure: function (info) {

        }
    });
}

function PassId(userId) {
    $('#delete span').text(userId);
    $('#sent-notification span').text(userId);
}

function Remove(btn) {
    $(btn).attr('disabled', true);
    var userId = $(btn).siblings('span').text();
    $("#delete").modal('hide');
    $.ajax({
        url: "/Users/Remove",
        type: "DELETE",
        dataType: "json",
        data: {
            userId: userId
        },
        success: function (response) {
            debugger;
            if (response.success) {
                toastr.success('تم حذف المستخدم بنجاح');
                setInterval(function () { window.location.href = "/Users/Owners" }, 2000);
            } else {
                toastr.error(response.message);
            }
        },
        failure: function (info) {
            toastr.error('حدث خطأ ما');
        }
    });
}

function SendNotification(btn) {
    $(btn).attr('disabled', true);
    var userId = $(btn).siblings('span').text();
    var title = $('#adress').val();
    var content = $('#notify-content').val();

    //if (title.trim() == "") {
    //    toastr.warning('ادخل عنوان الاشعار');
    //    $(btn).attr('disabled', false);
    //    return;
    //}
    if (content.trim() == "") {
        toastr.warning('ادخل نص الاشعار');
        $(btn).attr('disabled', false);
        return;
    }

    $.ajax({
        url: "/Users/SendNotification",
        type: "POST",
        dataType: "json",
        data: {
            userId: userId,
            title: title,
            content: content
        },
        success: function (result) {
            $(btn).attr('disabled', false);
            $("#sent-notification").modal('hide');
            toastr.success('تم ارسال الاشعار بنجاح');
            $('#adress').val("");
            $('#notify-content').val("");
        },
        failure: function (info) {
            toastr.error('حدث خطأ ما');
        }
    });
}
