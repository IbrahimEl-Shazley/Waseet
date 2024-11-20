// preview-section- multiple img with fancybox
$(".profile-img-upload input").on("change", function (event) {
  var selectedImg = $(this)
    .parents(".preview-section-img")
    .find(".img-preview");
  console.log("smkdfhjsdf");
  for (var i = 0; i < event.target.files.length; i++) {
    let file = event.target.files[i];
    let getBase64 = (file) => {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
      }).then(function (result) {
        let item = "";
        item = `<div class="uploaded">
          <i class="fas fa-times remove-appendedd"></i>
          <a href='${result}' + data-fancybox="gallery"></a><img src="${result}">
          <input type="hidden" name="image[]" value="${result}" >
          </div> `
        selectedImg.append(item);
      });
    };
    getBase64(file);
  }
});

let profile_icon = () => {
  if ($(".img_profile .upload-preview").length > 0) {
    $(".img_profile .edit-icon").addClass("unactive");
  } else {
    $(".img_profile .edit-icon").removeClass("unactive");
  }
};

// upload single img

$(document).on("change", ".single-img-uploader input", function (event) {
  $(this).parents(".single-img-uploader").find(".upload-preview").remove();
  var single_append = $(this)
    .parents(".single-img-uploader")
    .find(".uploader_img");
  if (event.target.files.length > 0) {
    $(single_append).append(
      `<div class="upload-preview"><a href="${URL.createObjectURL(event.target.files[0])}" data-fancybox="gallery"></a>
      <img src= "${URL.createObjectURL(event.target.files[0])}"> 
      <i class="fas fa-times remove-img custom-style"></i>
      </div>`
    );
  }

  profile_icon();
});

// remove multiple img
$(document).on("click", ".remove-appendedd", function (e) {
  e.preventDefault();
  $(this).parent().remove();
});

// remove single img
$(document).on("click", ".remove-img", function () {
  $(this).parents(".upload-preview").remove();
  $(".single-img-uploader input").val("");

  profile_icon();
});
