﻿function imgLoad(input, imgId) {
    let _imgBox = document.getElementById(imgId);

    if (input.files && input.files[0]) {

        let reader = new FileReader();

        reader.onload = function (e) {
            _imgBox.setAttribute('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
