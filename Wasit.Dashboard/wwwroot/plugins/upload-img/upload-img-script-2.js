const uniqueId = () => {
  const dateString = Date.now().toString(36);
  const randomness = Math.random().toString(36).substr(2);
  return dateString + randomness;
};

const appendUploadedFile = (file, parent, fileName, multiple) => {
  const valid_type =
    !file.type.includes("image") && !file.type.includes("application");
  if (valid_type) {
    return;
  }
  
  !multiple && parent.find(".preview").remove();
  parent.find(".placeholder").addClass("d-none");

  console.log("11");
  let fileInput = $(`<input type="file" class="d-none" name="${fileName}">`);
  console.log("22");
  const dataTransfer = new DataTransfer();
  dataTransfer.items.add(file);
  fileInput.get(0).files = dataTransfer.files;
  console.log("333");
  console.log(file);
  const id = uniqueId();

  let valid_type_2;
  if (file.type.includes("image")) {
    valid_type_2 = `<a href='${URL.createObjectURL(
      file
    )}' + data-fancybox="gallery"></a>
        <img src="${URL.createObjectURL(file)}">`;
  }
  if (file.type.includes("application")) {
    valid_type_2 = `<div class="parent-pdf">
    <img src="img/pdf-img.png" class="pdf-upload-img"><span>${file.name}</span>
    </div>`;
  }

  let box = `
  <span class="preview upload-preview" id="${id}">
  ${valid_type_2}
      <button class="remove" type="button">
          <i class="fa fa-remove"></i>
      </button>
      
  </span>`;

  parent.append(box);
  $("#" + id).append(fileInput);
  console.log(fileInput);
};

// change event
$(".custom-img-upload .upload-input").change(function (event) {
  const parent = $(this).siblings(".img");
  const fileName = $(this).data("name");
  const multible = $(this).attr("multiple") ? true : false;
  const files = event.target.files;
  if (multible) {
    for (var i = 0; i < files.length; i++) {
      appendUploadedFile(files[i], parent, fileName, multible);
    }
  } else {
    appendUploadedFile(files[0], parent, fileName);
  }
  $(this).val("");
});

// remove uploaded image
$(".custom-img-upload").on("click", ".remove", function () {
  $(this)
    .parents(".custom-img-upload")
    .find(".placeholder")
    .removeClass("d-none");
  $(this).parents(".preview").remove();
});

// ***Here is the code for converting "image source" (url) to "Base64".***

const toBase64 = (url) =>
  fetch(url)
    .then((response) => response.blob())
    .then(
      (blob) =>
        new Promise((resolve, reject) => {
          const reader = new FileReader();
          reader.onloadend = () => resolve(reader.result);
          reader.onerror = reject;
          reader.readAsDataURL(blob);
        })
    );

// ***Here is code for converting "Base64" to javascript "File Object".***

function dataURLtoFile(base64, filename) {
  var arr = base64.split(","),
    mime = arr[0].match(/:(.*?);/)[1],
    bstr = atob(arr[1]),
    n = bstr.length,
    u8arr = new Uint8Array(n);
  while (n--) {
    u8arr[n] = bstr.charCodeAt(n);
  }
  return new File([u8arr], filename, { type: mime });
}

const renderUploadedImages = (id, url) => {
  // *** Calling both function ***

  toBase64(url).then((base64) => {
    const fileName = url.substring(url.lastIndexOf("/") + 1);
    var fileData = dataURLtoFile(base64, fileName);

    const parent = $("#" + id).siblings(".img");
    const multible = $("#" + id).attr("multiple") ? true : false;
    appendUploadedFile(fileData, parent, id, multible);
  });
};

const runUploadedImages = (id, images) => {
  for (var i = 0; i < images.length; i++) {
    renderUploadedImages(id, images[i]);
  }
};
